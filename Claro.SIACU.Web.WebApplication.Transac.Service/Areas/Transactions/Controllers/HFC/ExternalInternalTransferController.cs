using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using MODEL = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using System.IO;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Func = Claro.SIACU.Transac.Service;
using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class ExternalInternalTransferController : Controller
    {
        private readonly FixedTransacService.FixedTransacServiceClient oFixedTransacService = new FixedTransacService.FixedTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();

        #region Record Transaction - RecordTransactionIntExtBL
        public Dictionary<string, string> RecordTransactionIntExtBL(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {
            FixedTransacService.RecordTranferExtIntResponseFixed objGetRecordTransactionResponse = new FixedTransacService.RecordTranferExtIntResponseFixed();
            
            Dictionary<string, string> objResponse = new Dictionary<string, string>();
            Dictionary<string, string> objResponseInteraction = new Dictionary<string, string>();
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN RecordTransactionIntExtBL()");
            bool blnResponse = false;
            string lnkNumSot = string.Empty;
            string strMessage = string.Empty;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.Session = objGetRecordTransactionRequest.strIdSession;
            audit.transaction = objGetRecordTransactionRequest.strTransaction;
            audit.applicationName = objGetRecordTransactionRequest.strApplicationName;
            audit.ipAddress = objGetRecordTransactionRequest.strIpAddress;
            audit.userName = objGetRecordTransactionRequest.strUserName;
            try
            {
                MODEL.TemplateInteractionModel objPla = new MODEL.TemplateInteractionModel();
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN RecordTransactionIntExtBL() - RecordInteraction()");

                objResponseInteraction = RecordInteraction(objGetRecordTransactionRequest, out objPla);

                if (objResponseInteraction["hdnInterID"] != null)
                {
                    objResponse["hdnInterID"] = objResponseInteraction["hdnInterID"];
                    objResponse["strPath"] = objResponseInteraction["strPath"];
                }
                else
                {
                    objResponse["hdnInterID"] = CONSTANT.Constants.strCero;
                    objResponse["strPath"] = string.Empty;
                }
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, objResponse["hdnInterID"]);

                objGetRecordTransactionRequest.InterCasoID = objResponseInteraction["hdnInterID"];

                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, "RecordTransfer");
                blnResponse = RecordTransfer(objGetRecordTransactionRequest, out lnkNumSot, out strMessage);

                if (string.IsNullOrEmpty(lnkNumSot) && strMessage == ConfigurationManager.AppSettings("strMsgSOTEnCursoMP"))
                {
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, strMessage);
                    throw new System.ArgumentException(strMessage);
                }
                else if (string.IsNullOrEmpty(lnkNumSot))
                {
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Error no se genero la sot");
                    throw new System.ArgumentException("No se genero la SOT");
                }

                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, "RETURN RecordTransfer:    " + blnResponse.ToString());

                

                #endregion

                if (blnResponse)
                {
                    //Ini INICIATIVA167-FTTH
                    string strCodigoAuxiliarInternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarInternoFTTH");
                    string strCodigoAuxiliarExternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarExternoFTTH");
                    //Fin INICIATIVA167-FTTH

                    #region REGISTER CONSTANCY
                    if (objGetRecordTransactionRequest.InterCasoID != null || objGetRecordTransactionRequest.InterCasoID == string.Empty)
                    {
                        objGetRecordTransactionRequest.strNroSOT = lnkNumSot;
                        objResponse["strPath"] = GetConstancyPDF(objGetRecordTransactionRequest, objResponse["hdnInterID"]);//CONSTANCY               
                    }

                    GrabarCambioDireccionPostal(objGetRecordTransactionRequest, lnkNumSot);
                    GrabarRegistroOCC(objGetRecordTransactionRequest, lnkNumSot);

                    Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, "RecordTransactionIntExtBL" + objGetRecordTransactionRequest.strtypetransaction);
                    if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro || objGetRecordTransactionRequest.strtypetransaction == strCodigoAuxiliarInternoFTTH) //INICIATIVA167-FTTH
                    {
                        string strTransacionTI = ConfigurationManager.AppSettings("strConsTraI");
                        // Ini INICIATIVA167-FTTH
                        string strTransacionID;
                        if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro)
                            strTransacionID = ConfigurationManager.AppSettings("gConstKeyTransTrasladoInternoDTH");
                        else
                            strTransacionID = strCodigoAuxiliarInternoFTTH;
                        
                        InsertAudit(strTransacionID, strTransacionTI, objGetRecordTransactionRequest, audit);
                        // Fin INICIATIVA167-FTTH
                    }
                    else
                    {
                        string strTransacionTE = ConfigurationManager.AppSettings("strConsTraE");
                        // Ini INICIATIVA167-FTTH
                        string strTransacionID;
                        if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strTres)
                            strTransacionID = ConfigurationManager.AppSettings("gConstKeyTransTrasladoExternoDTH");
                        else
                            strTransacionID = strCodigoAuxiliarExternoFTTH;

                        InsertAudit(strTransacionID, strTransacionTE, objGetRecordTransactionRequest, audit);
                        // Fin INICIATIVA167-FTTH
                    }
                    string rutaConstancy = objResponse["strPath"];

                    //ENVIAR CORREO
                    if (objGetRecordTransactionRequest.chkEmailChecked == true)
                    {
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "SEND EMAIL - LTE == true");
                        byte[] attachFile = null;
                        CommonServicesController objCommonServices = new CommonServicesController();
                        //Nombre del archivo
                        string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                        var ResultMAil = string.Empty;
                        if (objCommonServices.DisplayFileFromServerSharedFile(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, rutaConstancy, out attachFile))
                            ResultMAil = GetSendEmail(objGetRecordTransactionRequest, strAdjunto, attachFile, audit);
                    }

                    objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.CriterioMensajeOK;
                    objGetRecordTransactionResponse.DescMessaTransfer = ConfigurationManager.AppSettings("strMsgTranGrabSatis");

                }
                else
                {
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GetUpdatexInter30()");
                   
                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                    {
                        audit = audit,
                        p_objid = objResponse["hdnInterID"],
                        p_texto = string.Format("{0}{1}", ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy"), objGetRecordTransactionRequest.txtNotText)
                    };
                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                    });


                    objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                    objGetRecordTransactionResponse.DescMessaTransfer = ConfigurationManager.AppSettings("strMensajeDeError");

                    if (strMessage == ConfigurationManager.AppSettings("strMsgSOTEnCursoMP"))
                    {
                        objGetRecordTransactionResponse.DescMessaTransfer = strMessage;
                    }

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GetUpdatexInter30()");
                }
            }
            catch (Exception ex)
            {
                objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                objGetRecordTransactionResponse.DescMessaTransfer = ex.Message;
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }

            objResponse["rResult"] = objGetRecordTransactionResponse.CodMessaTransfer;
            objResponse["rMsgText"] = objGetRecordTransactionResponse.DescMessaTransfer;
            objResponse["hdnInterID"] = objResponseInteraction["hdnInterID"];
            objResponse["rMsgTextInter"] = objResponseInteraction["rMsgTextInter"];
            objResponse["lnkNumSot"] = lnkNumSot;

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "lnkNumSot: " + lnkNumSot == null ? string.Empty : lnkNumSot);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Description: " + objGetRecordTransactionResponse.DescMessaTransfer == null ? string.Empty : objGetRecordTransactionResponse.DescMessaTransfer);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT RecordTransactionIntExtBL()");
            return objResponse;
        }
        public ActionResult HFCExternalInternalTransfer()
        {
            Claro.Web.Logging.Configure();
            int number = Convert.ToInt(ConfigurationManager.AppSettings("strIncrementDays", "0"));
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
            string session1 = "123123";
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(session1);
            ViewData["strApplicationName"] = audit.applicationName;
            ViewData["strIpAddress"] = audit.ipAddress;
            ViewData["strTransaction"] = audit.transaction;
            ViewData["strUserName"] = audit.userName;
            return View("~/Areas/Transactions/Views/ExternalInternalTransfer/HFCExternalInternalTransfer.cshtml");
        }
        public bool RecordTransfer(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out string lnkNumSot, out string strMessage)
        {

            string strError = string.Empty;
            string strResDes = string.Empty;
            int intResCod = CONSTANT.Constants.numeroCero;
            bool blnRes = false;
            lnkNumSot = string.Empty;
            int intNumberCero = CONSTANT.Constants.numeroCero;
            string hdnCodSotValue = string.Empty;
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = objGetRecordTransactionRequest.strApplicationName;
            audit.ipAddress = objGetRecordTransactionRequest.strIpAddress;
            audit.Session = objGetRecordTransactionRequest.strIdSession;
            audit.transaction = objGetRecordTransactionRequest.strTransaction;
            audit.userName = objGetRecordTransactionRequest.strUserName;

            CommonTransacService.AuditRequest auditCommon = new CommonTransacService.AuditRequest();
            auditCommon.applicationName = objGetRecordTransactionRequest.strApplicationName;
            auditCommon.ipAddress = objGetRecordTransactionRequest.strIpAddress;
            auditCommon.Session = objGetRecordTransactionRequest.strIdSession;
            auditCommon.transaction = objGetRecordTransactionRequest.strTransaction;
            auditCommon.userName = objGetRecordTransactionRequest.strUserName;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("IN RecordTransfer() - HFC -  strtypetransaction: {0}", objGetRecordTransactionRequest.strtypetransaction));


            string strCodigoAuxiliarInternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarInternoFTTH"); //INICIATIVA167-FTTH


            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "HFC - agendaGetValidaEta: " + objGetRecordTransactionRequest.agendaGetValidaEta);
            if (Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroUno || Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroDos)
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "EMPIEZA AgendaGetValidaEta: " + objGetRecordTransactionRequest.agendaGetValidaEta);
                #region AgendaGetValidaEta
                if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro || objGetRecordTransactionRequest.strtypetransaction == strCodigoAuxiliarInternoFTTH) //INICIATIVA167-FTTH
                {
                    if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
                    {
                        if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                        {
                            if (objGetRecordTransactionRequest.FranjaHora != null)
                            {
                        try
                        {
                            intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                            FixedTransacService.InsertETASelectionResponse objInsertETASelectionResponse = null;
                            FixedTransacService.InsertETASelectionRequest objInsertETASelectionRequest = null;
                            objInsertETASelectionResponse = new FixedTransacService.InsertETASelectionResponse();
                            objInsertETASelectionRequest = new FixedTransacService.InsertETASelectionRequest()
                            {
                                audit = audit,
                                vidconsulta = Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct),
                                vidInteraccion = CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim(),
                                        vfechaCompromiso = DateTime.Parse(objGetRecordTransactionRequest.FechaProgramada),
                                        vfranja = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[0],
                                        vid_bucket = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[1]//model.FranjaHorariaETA.Split('+')[1]
                            };
                            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Inicia GetInsertETASelection");
                             objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return oFixedTransacService.GetInsertETASelection(objInsertETASelectionRequest); });
                             Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Termina GetInsertETASelection");
                        }
                                catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                        }
                    }
                }
                    }
                }
                else
                {
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Inicia else AgendaGetValidaEta");
                    if (Functions.CheckInt64(objGetRecordTransactionRequest.InterCasoID) > CONSTANT.Constants.numeroUno)
                    {
                        if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                        {
                            if (objGetRecordTransactionRequest.FranjaHora != null)
                            {
                        try
                        {
                            intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                            FixedTransacService.InsertETASelectionResponse objInsertETASelectionResponse = null;
                            FixedTransacService.InsertETASelectionRequest objInsertETASelectionRequest = null;
                            objInsertETASelectionResponse = new FixedTransacService.InsertETASelectionResponse();
                            objInsertETASelectionRequest = new FixedTransacService.InsertETASelectionRequest()
                            {
                                audit = audit,
                                vidconsulta = Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct),
                                vidInteraccion = CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.InterCasoID.Trim().Length) + objGetRecordTransactionRequest.InterCasoID.Trim(),
                                        vfechaCompromiso = DateTime.Parse(objGetRecordTransactionRequest.FechaProgramada),
                                        vfranja = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[0],
                                        vid_bucket = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[1]
                            };
                            objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return oFixedTransacService.GetInsertETASelection(objInsertETASelectionRequest); });
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                        }
                    }
                }
                    }
                }
                #endregion
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "TERMINA AgendaGetValidaEta: " + objGetRecordTransactionRequest.agendaGetValidaEta);
            }
            if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro || objGetRecordTransactionRequest.strtypetransaction == strCodigoAuxiliarInternoFTTH) //INICIATIVA167-FTTH
            {
                #region TRANSFER INTERNAL
                FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
                objResponseGenerateSOT = RecordSotTrasnsferInternal(objGetRecordTransactionRequest, audit);
                hdnCodSotValue = objResponseGenerateSOT.IdGenerateSOT;
                strMessage = objResponseGenerateSOT.DescMessaTransfer;
                intResCod = Convert.ToInt(objResponseGenerateSOT.CodMessaTransfer);

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("objResponseGenerateSOT - HFC _ out cod:{0} Res:{1} Des{2}", objResponseGenerateSOT.IdGenerateSOT == null ? "" : objResponseGenerateSOT.IdGenerateSOT, objResponseGenerateSOT.CodMessaTransfer == null ? "" : objResponseGenerateSOT.CodMessaTransfer, objResponseGenerateSOT.DescMessaTransfer == null ? "" : objResponseGenerateSOT.DescMessaTransfer));
                #endregion
            }
            else
            {
                #region TRANSFER EXTERNAL
                FixedTransacService.InsertTransactionResponse objResponseGetRecordTransaction = new FixedTransacService.InsertTransactionResponse();
                objResponseGetRecordTransaction = GetRecordTransactionExternal(objGetRecordTransactionRequest, audit);
                hdnCodSotValue = objResponseGetRecordTransaction.intNumSot;
                strMessage = objResponseGetRecordTransaction.rstrResDes;
                intResCod = Convert.ToInt(objResponseGetRecordTransaction.rintResCod);
                #endregion
            }

            if (hdnCodSotValue != null && hdnCodSotValue != string.Empty && hdnCodSotValue.ToUpper() != "NULL")
            {

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT GetRecordTransaction- HFC _ CodSotValue {0}", hdnCodSotValue == null ? "" : hdnCodSotValue));
                if (Double.Parse(hdnCodSotValue) > 1)
                {
                    #region UPDATE INTERACTION 29 - REGISTRO OK
                    FixedTransacService.UpdateInter29Response objUpdateInter29Response = new FixedTransacService.UpdateInter29Response();
                    FixedTransacService.UpdateInter29Request objUpdateInter29Request = new FixedTransacService.UpdateInter29Request()
                    {

                        audit = audit,
                        p_objid = objGetRecordTransactionRequest.InterCasoID,
                        p_texto = hdnCodSotValue,
                        p_orden = CONSTANT.Constants.strLetraI
                    };

                    objUpdateInter29Response = Claro.Web.Logging.ExecuteMethod<FixedTransacService.UpdateInter29Response>(() =>
                    {
                        return oFixedTransacService.GetUpdateInter29(objUpdateInter29Request);
                    });

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT GetUpdateInter29 -  rFlagInsercion: {0}", objUpdateInter29Response.rFlagInsercion == null ? "" : objUpdateInter29Response.rFlagInsercion));

                    if (objUpdateInter29Response.rFlagInsercion == CONSTANT.Constants.CriterioMensajeOK)
                    {
                        lnkNumSot = hdnCodSotValue;
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, " UPDATE INTERACTION 29  - REGISTRO OK");
                    }
                    #endregion
                    else
                    #region UPDATE INTERACTION 30 - SOT EN CURSO
                    {
                        CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                        CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                        {
                            audit = auditCommon,
                            p_objid = objGetRecordTransactionRequest.InterCasoID,
                            p_texto = ConfigurationManager.AppSettings("strMsgTraOkCodSotOM")
                        };

                        objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                        {
                            return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                        });
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("UPDATE INTERACTION 30 - SOT EN CURSO - I-  rFlagInsercion: {0}", objUpdateInter30Response.rFlagInsercion));
                    }
                    #endregion
                    blnRes = true;
                }

                if (intResCod.Equals(CONSTANT.Constants.numeroTres))// Sot en curso                
                {
                    #region UPDATE INTERACTION 30 - SOT EN CURSO
                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                    {
                        audit = auditCommon,
                        p_objid = objGetRecordTransactionRequest.InterCasoID,
                        p_texto = ConfigurationManager.AppSettings("strMsgSOTEnCursoMP")
                    };

                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                    });

                    strMessage = ConfigurationManager.AppSettings("strMsgSOTEnCursoMP");

                    #endregion
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "UPDATE INTERACTION 30 - SOT EN CURSO - II");
                }
                
            }
            else //No se genero la Sot o Transaccion
            {               
               
                if (intResCod.Equals(CONSTANT.Constants.numeroTres))// Sot en curso
                #region UPDATE INTERACTION 30 - SOT EN CURSO
                {

                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                    {
                        audit = auditCommon,
                        p_objid = objGetRecordTransactionRequest.InterCasoID,
                        p_texto = ConfigurationManager.AppSettings("strMsgSOTEnCursoMP")
                    };

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "Inicia GetUpdatexInter30");

                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                    });


                    strMessage = ConfigurationManager.AppSettings("strMsgSOTEnCursoMP");
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "UPDATE INTERACTION 30 - SOT EN CURSO - III");
                }
                #endregion
                else
                #region UPDATE INTERACTION 30 - ERROR EN LA TRANSACCION
                {
                   
                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                    {
                        audit = auditCommon,
                        p_objid = objGetRecordTransactionRequest.InterCasoID,
                        p_texto = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy")
                    };

                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                    });

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT GetRecordTransaction - HFC _  p_texto: {0}", objUpdateInter30Request.p_texto));
                    strMessage = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy");

                }
                #endregion
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "strMessage: " + strMessage);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT RecordTransfer - HFC");
            return blnRes;
        }

        public FixedTransacService.GenerateSOTResponseFixed RecordSotTrasnsferInternal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, FixedTransacService.AuditRequest audit)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
           
            int intNumberCero = CONSTANT.Constants.numeroCero;
            if (objGetRecordTransactionRequest.FranjaHora == null || objGetRecordTransactionRequest.FranjaHora == string.Empty)
            {
                objRequestGenerateSOT.vFranja = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
                objRequestGenerateSOT.vFranja = objGetRecordTransactionRequest.FranjaHora;
            }


            if (objGetRecordTransactionRequest.agendaGetTipoTrabajo.Contains(".|"))
                objRequestGenerateSOT.vTipTra = Int32.Parse(objGetRecordTransactionRequest.agendaGetTipoTrabajo.Substring(0, objGetRecordTransactionRequest.agendaGetTipoTrabajo.Length - 2));
            else
                objRequestGenerateSOT.vTipTra = Int32.Parse(objGetRecordTransactionRequest.agendaGetTipoTrabajo);
            objGetRecordTransactionRequest.txtNotText = objGetRecordTransactionRequest.txtNotText != null ? objGetRecordTransactionRequest.txtNotText.Replace('|', '-') : string.Empty;

            if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
            {
                if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                {
                    if (objGetRecordTransactionRequest.FranjaHora != null)
                    {
                        if (objGetRecordTransactionRequest.strFlagReservation == "0") 
                        {
                            objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText + "|" + CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim() + "|";
                        }
                        else 
                        {
                        intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                            objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText + CONSTANT.Constants.NoteReservationToa + objGetRecordTransactionRequest.strNroOrderToa + CONSTANT.Constants.IdentifiacdorToa + "|" +
                                CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim() + "|";
                        }                        
                    }
                    else
                    {
                        objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText;
                    }
                }
                else
                {
                    objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText;
                }
            }
            else
            {
                objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText;
            }

            
            
            objRequestGenerateSOT.vCusID = objGetRecordTransactionRequest.CustomerID.Trim();
            objRequestGenerateSOT.vCoID = objGetRecordTransactionRequest.ConID.Trim();
            if (objGetRecordTransactionRequest.agendaGetFecha == null || objGetRecordTransactionRequest.agendaGetFecha == string.Empty)
            {
                objRequestGenerateSOT.vFeProg = DateTime.Now.ToShortDateString();
            }
            else
            {
            objRequestGenerateSOT.vFeProg = objGetRecordTransactionRequest.agendaGetFecha;
            }

            objRequestGenerateSOT.vPlano = objGetRecordTransactionRequest.PlanoIDCustomer;
            objRequestGenerateSOT.vCodMotivo = objGetRecordTransactionRequest.MotivoID.ToString();
            objRequestGenerateSOT.vUser = objGetRecordTransactionRequest.CurrentUser;
            objRequestGenerateSOT.Cargo = objGetRecordTransactionRequest.Cargo;
            objRequestGenerateSOT.idTipoServ = string.Empty;

            try
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN RecordSotTrasnsferInternal - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return oFixedTransacService.GetGenerateSOT(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT RecordSotTrasnsferInternal  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }

        public FixedTransacService.GenerateSOTResponseFixed registrarEtaSot(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);

            objRequestGenerateSOT.vCoID = objGetRecordTransactionRequest.CodSot;
            objRequestGenerateSOT.idSubTypeWork = objGetRecordTransactionRequest.strSubTypeWork.Split('|')[2];
            objRequestGenerateSOT.vFranja = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[0];
            objRequestGenerateSOT.idBucket = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[1];
            objRequestGenerateSOT.vFeProg = objGetRecordTransactionRequest.FechaProgramada;
            try
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "IN registrarEtaSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return oFixedTransacService.registraEtaSot(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT registrarEtaSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }

        public FixedTransacService.InsertTransactionResponse GetRecordTransactionExternal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, FixedTransacService.AuditRequest audit)
        {

            FixedTransacService.InsertTransactionResponse objTransferIntExtResponseFixed = new FixedTransacService.InsertTransactionResponse();
            
            FixedTransacService.InsertTransactionRequest objTransferIntExtRequestFixed = new FixedTransacService.InsertTransactionRequest();
            int intNumberCero = CONSTANT.Constants.numeroCero;
            FixedTransacService.Transfer oTransfer = new FixedTransacService.Transfer();
            objTransferIntExtRequestFixed.oTransfer = oTransfer;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("IN GetRecordTransactionExternal {0}", "Iniciando Proceso"));

            objTransferIntExtRequestFixed.oTransfer.USRREGIS = objGetRecordTransactionRequest.CurrentUser;
            objTransferIntExtRequestFixed.oTransfer.ConID = objGetRecordTransactionRequest.ConID.Trim().Trim();
            objTransferIntExtRequestFixed.oTransfer.CustomerID = objGetRecordTransactionRequest.CustomerID.Trim();

            //Inicio INICIATIVA167-FTTH
            string strCodigoAuxiliarExternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarExternoFTTH");
            if (objGetRecordTransactionRequest.strtypetransaction == strCodigoAuxiliarExternoFTTH)
                objTransferIntExtRequestFixed.oTransfer.TransTipo = ConfigurationManager.AppSettings("strKeyTipoTrasladoExternoFTTH");                
            else
            objTransferIntExtRequestFixed.oTransfer.TransTipo = ConfigurationManager.AppSettings("gConstKeyTipoTranTE");
            //Fin INICIATIVA167-FTTH

            objTransferIntExtRequestFixed.oTransfer.InterCasoID = objGetRecordTransactionRequest.InterCasoID;
            objTransferIntExtRequestFixed.oTransfer.FechaProgramada = DateTime.Now;
            objTransferIntExtRequestFixed.oTransfer.TipoVia = objGetRecordTransactionRequest.TipoVia;
            objTransferIntExtRequestFixed.oTransfer.NomVia = objGetRecordTransactionRequest.NomVia;

            if (objGetRecordTransactionRequest.chkSN)
                objTransferIntExtRequestFixed.oTransfer.NroVia = CONSTANT.Constants.numeroCero;
            else
                objTransferIntExtRequestFixed.oTransfer.NroVia = Convert.ToInt(objGetRecordTransactionRequest.NroVia);

            objTransferIntExtRequestFixed.oTransfer.TipoUrb = objGetRecordTransactionRequest.TipoUrb;
            objTransferIntExtRequestFixed.oTransfer.NomUrb = objGetRecordTransactionRequest.NomUrb;
            objTransferIntExtRequestFixed.oTransfer.NumMZ = objGetRecordTransactionRequest.hdnTipMzBloEdi;
            objTransferIntExtRequestFixed.oTransfer.NumLote = objGetRecordTransactionRequest.NumLote;
            objTransferIntExtRequestFixed.oTransfer.Ubigeo = objGetRecordTransactionRequest.Ubigeo.Trim();

            objTransferIntExtRequestFixed.oTransfer.ZonaID = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.PlanoID = objGetRecordTransactionRequest.PlanoID;
            objTransferIntExtRequestFixed.oTransfer.EdificioID = objGetRecordTransactionRequest.EdificioID;
            objTransferIntExtRequestFixed.oTransfer.Referencia = string.Format("{0} {1} {2}",objGetRecordTransactionRequest.ddlDepartment,objGetRecordTransactionRequest.hdnNumberDepartment,objGetRecordTransactionRequest.Referencia);

            objTransferIntExtRequestFixed.oTransfer.FranjaHora = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.NumCarta = CONSTANT.Constants.strCero;
            objTransferIntExtRequestFixed.oTransfer.FechaProgramada = Functions.CheckDate(objGetRecordTransactionRequest.FechaProgramada);

            if (objGetRecordTransactionRequest.FranjaHora == null || objGetRecordTransactionRequest.FranjaHora == string.Empty)
            {
                objTransferIntExtRequestFixed.oTransfer.FranjaHora = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
                objTransferIntExtRequestFixed.oTransfer.FranjaHora = objGetRecordTransactionRequest.FranjaHora;
            }
            
            objTransferIntExtRequestFixed.oTransfer.MotivoID = objGetRecordTransactionRequest.MotivoID;
            objTransferIntExtRequestFixed.oTransfer.Operador = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.Presuscrito = CONSTANT.Constants.strCero;
            objTransferIntExtRequestFixed.oTransfer.Publicar = CONSTANT.Constants.strCero;
            objTransferIntExtRequestFixed.oTransfer.TmCode = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.ListaCoSer = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.ListaSPCode = String.Empty;
            objTransferIntExtRequestFixed.oTransfer.Cargo = objGetRecordTransactionRequest.Cargo;

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("HFCLogNroVia - {0}", objTransferIntExtRequestFixed.oTransfer.NroVia));

            objGetRecordTransactionRequest.txtNotText = objGetRecordTransactionRequest.txtNotText != null ? objGetRecordTransactionRequest.txtNotText.Replace('|', '-') : string.Empty;

            if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
            {
                if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
            {
                    if (objGetRecordTransactionRequest.FranjaHora != null)
                    {
                        intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                        objTransferIntExtRequestFixed.oTransfer.Observacion = objGetRecordTransactionRequest.txtNotText + "|" + CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim(); // +"|";
            }
                    else
            {
                        objTransferIntExtRequestFixed.oTransfer.Observacion = objGetRecordTransactionRequest.txtNotText;
            }
                }
                else
            {
                    objTransferIntExtRequestFixed.oTransfer.Observacion = objGetRecordTransactionRequest.txtNotText;
                }
            }
            else
            {
                objTransferIntExtRequestFixed.oTransfer.Observacion = objGetRecordTransactionRequest.txtNotText;
            }
            
            try
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("IN WFC -GetRecordTransactionExternal HFC- {0}", "WFC - IN"));

                objTransferIntExtRequestFixed.audit = audit;

                objTransferIntExtResponseFixed = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertTransactionResponse>(() => { return oFixedTransacService.GetInsertTransaction(objTransferIntExtRequestFixed); });
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT WFC -GetRecordTransactionExternal HFC- Result: {0}", objTransferIntExtResponseFixed.rintResCod));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);

            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, string.Format("OUT GetRecordTransactionExternal -  {0}", "Fin de la transaccion EXternal"));

            return objTransferIntExtResponseFixed;
        }
        public JsonResult GetRecordTransactionIntExt(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objHfcBETransfer)
        {
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, objHfcBETransfer.strTransaction, "IN GetRecordTransactionIntExt() - HFC");
            Dictionary<string, string> ResultGetRecordTransactionIntExt = new Dictionary<string, string>();

            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, objHfcBETransfer.strTransaction, "IN RecordTransactionIntExtBL() - HFC");
            ResultGetRecordTransactionIntExt = RecordTransactionIntExtBL(objHfcBETransfer);
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, objHfcBETransfer.strTransaction, "OUT RecordTransactionIntExtBL() - HFC");

            MODEL.HFC.ExternalInternalTransferModel objFixedTransacServices = null;
            objFixedTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
            HELPERS.CommonServices.GenericItem ItemGenMessag = new HELPERS.CommonServices.GenericItem();

            ItemGenMessag.Code = ResultGetRecordTransactionIntExt["rResult"];
            ItemGenMessag.Description = ResultGetRecordTransactionIntExt["rMsgText"];
            ItemGenMessag.Code2 = ResultGetRecordTransactionIntExt["hdnInterID"];
            ItemGenMessag.Description2 = ResultGetRecordTransactionIntExt["rMsgTextInter"];
            ItemGenMessag.Number = ResultGetRecordTransactionIntExt["lnkNumSot"];
            ItemGenMessag.Code3 = ResultGetRecordTransactionIntExt["strPath"];

            objFixedTransacServices.ItemGeneric = ItemGenMessag;
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, objHfcBETransfer.strTransaction, "OUT GetRecordTransactionIntExt() - HFC  ItemGenMessag.Number: " + ItemGenMessag.Number != null ? ItemGenMessag.Number : "NO # SOT");
            return Json(new { data = objFixedTransacServices });
        }
        #endregion

        #region Record Interaction
        public Dictionary<string, string> RecordInteraction(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out  MODEL.TemplateInteractionModel oPlantillaDat)
        { 
            Dictionary<string, string> ResposeInteraction;
            var rResult = string.Empty;
            var rMsgText = string.Empty;
            bool blnValidate = false;
            var NAME_PDF = string.Empty;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN RecordInteraction() - HFC");
            var objInteractionModel = new MODEL.InteractionModel();

            objInteractionModel = DatInteraction(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, objGetRecordTransactionRequest.CustomerID, objGetRecordTransactionRequest.txtNotText, objGetRecordTransactionRequest.CurrentUser, objGetRecordTransactionRequest.ConID, objGetRecordTransactionRequest.PlanoID, objGetRecordTransactionRequest.strTransaction);

            oPlantillaDat = DatTemplateInteraction(objGetRecordTransactionRequest, out blnValidate);

            var UsuarioAplicacion =ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var passAplicacion = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN RecordInteraction - HFC _ blnValidate " + blnValidate.ToString());
            try
            {
                if (!blnValidate)
                {
                    
                    rMsgText = string.Format("{0} {1}",Functions.GetValueFromConfigFile("gConstKeyCambInteracTitular", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), Functions.GetValueFromConfigFile("strNoTransaccion" ,ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", CONSTANT.Constants.strCero},
                        {"rMsgTextInter", rMsgText},
                    };
                    return ResposeInteraction;
                }
                var DictResposeInteraction = InsertInteraction(objInteractionModel, oPlantillaDat, objGetRecordTransactionRequest.Telephone
                                            , objGetRecordTransactionRequest.CurrentUser, UsuarioAplicacion
                                            , passAplicacion, false
                                        , objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.CustomerID
                                        , objGetRecordTransactionRequest.strTransaction, objGetRecordTransactionRequest.strIpAddress, objGetRecordTransactionRequest.strUserName, objGetRecordTransactionRequest.strApplicationName);

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN var DictResposeInteraction - HFC");
                var strMsg = string.Empty;
                bool exitoInsercion = false;
         

                var listInteraction = new List<string>();
                DictResposeInteraction.ToList().ForEach(x =>
                {
                    listInteraction.Add(x.Value.ToString());
                });

                rResult = listInteraction[3];
                exitoInsercion = listInteraction[2].ToUpper() == CONSTANT.Constants.grstTrue.ToUpper() ? true : false;
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "IdTransaction: " + objGetRecordTransactionRequest.strTransaction, "Insert Interaction" + exitoInsercion.ToString());
                var strFlagInser = listInteraction[0];
                var strFlagInserInter = listInteraction[1];

                if (strFlagInser != CONSTANT.Constants.DAReclamDatosVariable_OK && strFlagInser.Equals(string.Empty))
                {

                    rMsgText = string.Format("{0} {1}", Functions.GetValueFromConfigFile("strMsgErrorCrearInterac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), listInteraction[0]);
                    ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", CONSTANT.Constants.strCero},
                        {"rMsgTextInter", rMsgText},
                    };
                    return ResposeInteraction;
                }

                if (strFlagInserInter != CONSTANT.Constants.DAReclamDatosVariable_OK && strFlagInser.Equals(string.Empty))
                {
                    CommonServicesController OCommonServicesController = new CommonServicesController();
                    rMsgText = string.Format("{0} {1} {2} {3} {4}", Functions.GetValueFromConfigFile("strMsgCreoInterErrTrans", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), listInteraction[3], Functions.GetValueFromConfigFile("strMsgSgteError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), listInteraction[1], listInteraction[0]);

                    ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", CONSTANT.Constants.strCero},
                        {"rMsgTextInter", rMsgText},
                    };
                    return ResposeInteraction;
                }

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN listInteraction[2]- HFC " + exitoInsercion);
                if (exitoInsercion)
                {
                        //NAME_PDF = GetConstancyPDF(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest, rResult);
                        if (NAME_PDF==string.Empty)
                        {
                            rMsgText = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                            ResposeInteraction = new Dictionary<string, string>
                                {
                                    {"hdnInterID", CONSTANT.Constants.strCero},
                                    {"rMsgText", rMsgText},
                                    {"strPath", NAME_PDF}
                                };
                        }
                }
            }
            catch (Exception ex)
            {
                rMsgText = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy");
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.InnerException.Message);
            }

            ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", rResult},
                        {"rMsgTextInter", rMsgText},
                        {"strPath", NAME_PDF}
                    };

            return ResposeInteraction;
        }
        public Dictionary<string, object> InsertInteraction(MODEL.InteractionModel objInteractionModel,
                                                    MODEL.TemplateInteractionModel oPlantillaDat,
                                                    string strNroTelephone,
                                                    string strUserSession,
                                                    string strUserAplication,
                                                    string strPassUser,
                                                    bool boolEjecutTransaction,
                                                    string strIdSession,
                                                    string strCustomerId,
                                                    string strTransaction,
                                                    string strIP,
                                                    string strUserName,
                                                    string strAplicationName)
        {
            string strTelefono;
            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;
            string ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            CommonServicesController oCommonServicesController = new CommonServicesController();
            Claro.Web.Logging.Info(strIdSession, strTransaction, "IN InsertInteraction - HFC");

            string strFlgRegistrado = CONSTANT.Constants.strUno;
            FixedTransacService.CustomerResponse objCustomerResponse;
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.Session = strIdSession;
            audit.applicationName = strAplicationName;
            audit.ipAddress = strIP;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CallDetailController", "InsertInteraction", "InsertInteraction : SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
            Claro.Web.Logging.Info(strIdSession, strTransaction, msg);
            FixedTransacService.GetCustomerRequest objGetCustomerRequest = new FixedTransacService.GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.CustomerResponse>(() =>
            {
                return oFixedTransacService.GetCustomer(objGetCustomerRequest);
            });

            if (objCustomerResponse.Customer != null)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "objCustomerResponse.Customer != null");
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            var result = new Dictionary<string, string>();
            
            #region VALIDACION CONTIGENCIA

            if (ContingenciaClarify != CONSTANT.Constants.blcasosVariableSI)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "GetInsertInteractionCLFY");
                result = oCommonServicesController.GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "GetInsertContingencyInteraction");
                result = oCommonServicesController.GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }

            #endregion

            var model = new List<string>();
            foreach (KeyValuePair<string, string> par in result)
            {
                model.Add(par.Value);
            }

            var rInteraccionId = model[0];

            var dictionaryResponse = new Dictionary<string, object>();
            if (rInteraccionId != string.Empty)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, rInteraccionId);
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = oCommonServicesController.InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "oCommonServicesController.InsertPlantInteraction");
                }
            }
            dictionaryResponse.Add("rInteraccionId", rInteraccionId);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "OUT InsertInteraction - HFC");
            return dictionaryResponse;
        }


        public MODEL.InteractionModel DatInteraction(string strIdsession, string typetransaction, string CUSTOMER_ID, string txtNot, string CurrentUser, string CONTRATO_ID, string CODIGO_PLANO_INST, string strtransaction)
        {
            string gstrTransaccionTrasladoIntHFC = "TRANSACCION_DTH_TRASLADO_INTERNO_HFC";
              string gstrTransaccionTrasladoExtHFC = "TRANSACCION_DTH_TRASLADO_EXTERNO_HFC";
              //Inicio INICIATIVA167-FTTH
              string gstrTransaccionTrasladoIntFTTH = ConfigurationManager.AppSettings("strTransaccionTrasladoInternoFTTH");
              string gstrTransaccionTrasladoExtFTTH = ConfigurationManager.AppSettings("strTransaccionTrasladoExternoFTTH");
              string strCodigoAuxiliarExternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarExternoFTTH");
              string strCodigoAuxiliarInternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarInternoFTTH");            
              //Fin INICIATIVA167-FTTH

              string tipoTran = string.Empty;
              Claro.Web.Logging.Info(strIdsession, strtransaction, "IN DatInteraction( " + strIdsession + ", " + typetransaction + "," + CUSTOMER_ID + "," + txtNot + "," + CurrentUser + "," + CONTRATO_ID + "," + CODIGO_PLANO_INST + ")");

              //Inicio INICIATIVA167-FTTH
              string strTelTipoHFC = string.Empty;
              if (typetransaction == strCodigoAuxiliarInternoFTTH || typetransaction==strCodigoAuxiliarExternoFTTH) {
                  strTelTipoHFC = ConfigurationManager.AppSettings("strTipoProductoFTTH");
              }else{
                  strTelTipoHFC= ConfigurationManager.AppSettings("gConstTipoHFC");
              }
              if (typetransaction.Equals(strCodigoAuxiliarInternoFTTH))
              {
                  tipoTran = gstrTransaccionTrasladoIntFTTH;
              }
              if (typetransaction.Equals(strCodigoAuxiliarExternoFTTH))
              {
                  tipoTran = gstrTransaccionTrasladoExtFTTH;
              }

              if (tipoTran == string.Empty) { 
              //Fin INICIATIVA167-FTTH

                  if (typetransaction.Equals(CONSTANT.Constants.strTres))
              {
                  tipoTran = gstrTransaccionTrasladoExtHFC;
              }
              else 
              {
                tipoTran = gstrTransaccionTrasladoIntHFC;
              }
              }
              Claro.Web.Logging.Info(strIdsession, strtransaction, "IN DatInteraction - tipoTran:  " + tipoTran);
              CommonServicesController OCommonServicesController = new CommonServicesController();
              var objInter = new MODEL.InteractionModel();

            try
            {
                  var tipification = OCommonServicesController.GetTypificationHFC(strIdsession, tipoTran);
                  
                  tipification = tipification.Where(y => y.Type == strTelTipoHFC).ToList();
                 
                  tipification.ToList().ForEach(x =>
                  {
                      objInter.Type = x.Type;
                      objInter.Class = x.Class;
                      objInter.SubClass = x.SubClass;
                      objInter.IdInteractio = x.InteractionCode;
                      objInter.TypeCode = x.TypeCode;
                      objInter.ClassCode = x.ClassCode;
                      objInter.SubClassCode = x.SubClassCode;
                  });

                  objInter.ObjidContacto = OCommonServicesController.GetCustomer( ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + Convert.ToInt(CUSTOMER_ID),strIdsession).ToString();
                  objInter.DateCreaction = DateTime.Now.ToString();
                  objInter.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + CUSTOMER_ID;
                  objInter.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                  objInter.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                  objInter.Result = ConfigurationManager.AppSettings("Ninguno");
                  objInter.MadeOne = CONSTANT.Constants.strCero;
                  objInter.Note = txtNot;
                  objInter.FlagCase = CONSTANT.Constants.strCero;
                  objInter.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                  objInter.Agenth = CurrentUser;
                  objInter.Contract = CONTRATO_ID;
                  objInter.Plan = CODIGO_PLANO_INST;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdsession, strtransaction, ex.Message);
               if (ex.InnerException.Message!= null)
                   Claro.Web.Logging.Error(strIdsession, strtransaction, ex.InnerException.Message);

            }
            return objInter;
        }

        public MODEL.TemplateInteractionModel DatTemplateInteraction(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out bool blnValidate)
        {
            Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, "IN DatTemplateInteraction()");
            string gstrTransaccionTrasladoIntHFC = "TRANSACCION_DTH_TRASLADO_INTERNO_HFC";
            string gstrTransaccionTrasladoExtHFC = "TRANSACCION_DTH_TRASLADO_EXTERNO_HFC";
            string tipoTran = string.Empty;

            //Inicio INICIATIVA167-FTTH
            string gstrTransaccionTrasladoIntFTTH = ConfigurationManager.AppSettings("strTransaccionTrasladoInternoFTTH");
            string gstrTransaccionTrasladoExtFTTH = ConfigurationManager.AppSettings("strTransaccionTrasladoExternoFTTH");
            string strCodigoAuxiliarExternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarExternoFTTH");
            string strCodigoAuxiliarInternoFTTH = ConfigurationManager.AppSettings("strCodigoAuxiliarInternoFTTH");

            if (objGetRecordTransactionRequest.strtypetransaction.Equals(strCodigoAuxiliarInternoFTTH))
            {
                tipoTran = gstrTransaccionTrasladoIntFTTH;
            }
            if (objGetRecordTransactionRequest.strtypetransaction.Equals(strCodigoAuxiliarExternoFTTH))
            {
                tipoTran = gstrTransaccionTrasladoExtFTTH;
            }

            if (tipoTran == string.Empty) { 
            //Fin INICIATIVA167-FTTH
            if (objGetRecordTransactionRequest.strtypetransaction.Equals(CONSTANT.Constants.strTres))
            {
                tipoTran = gstrTransaccionTrasladoExtHFC;
            }
            else
            {
                tipoTran = gstrTransaccionTrasladoIntHFC;                
            }
            }
            var objPla = new MODEL.TemplateInteractionModel()
            {
            
            NOMBRE_TRANSACCION = tipoTran,
            X_CLARO_NUMBER = objGetRecordTransactionRequest.CustomerID,

            X_REGISTRATION_REASON = objGetRecordTransactionRequest.nameCustomer,
            X_BASKET = objGetRecordTransactionRequest.RepreCustomer,
            X_MONTH = objGetRecordTransactionRequest.AddressCustomer,
            X_INTER_4 = objGetRecordTransactionRequest.NotAddressCustomer,
            X_INTER_1 = objGetRecordTransactionRequest.CountryCustomer,
            X_INTER_2 = objGetRecordTransactionRequest.CountryCustomerFac,
            X_INTER_3 = objGetRecordTransactionRequest.DepCustomer,
            X_INTER_5 = objGetRecordTransactionRequest.ProvCustomer,
            X_INTER_6 = objGetRecordTransactionRequest.DistCustomer,
            X_INTER_7 = objGetRecordTransactionRequest.IdEdifCustomer,
            X_INTER_16 = objGetRecordTransactionRequest.EmailCustomer,
            X_INTER_17 = objGetRecordTransactionRequest.urbLegalCustomer,

            X_INTER_18 = objGetRecordTransactionRequest.CodPos, //Codigo Postal
            X_NAME_LEGAL_REP = objGetRecordTransactionRequest.hdnUbiAct,
            X_LOT_CODE = ConfigurationManager.AppSettings("strConsTraI"),
            X_MODEL = String.Format("{0} {1}", objGetRecordTransactionRequest.agendaGetFecha, objGetRecordTransactionRequest.agendaGetCodigoFranja),

            X_INTER_15 = objGetRecordTransactionRequest.DescripCADDAC,
            X_REASON = DateTime.Now.ToShortDateString(),
            X_EMAIL = objGetRecordTransactionRequest.Email,
            X_INTER_30 = objGetRecordTransactionRequest.txtNotText,
            X_FLAG_CHARGE = objGetRecordTransactionRequest.PlanoIDCustomer,
            X_ICCID = objGetRecordTransactionRequest.CustomerID           
            };

            if (objGetRecordTransactionRequest.chkEmailChecked == true)
            {
                objPla.X_FLAG_REGISTERED = CONSTANT.Constants.PresentationLayer.NumeracionUNO;
            }
            else
            {
                objPla.X_FLAG_REGISTERED = CONSTANT.Constants.PresentationLayer.NumeracionCERO;
            }
            try
            {
                if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strTres || objGetRecordTransactionRequest.strtypetransaction == strCodigoAuxiliarExternoFTTH) //INICIATIVA167-FTTH
             {
                 objPla.X_ADDRESS = GenerarDireccion(objGetRecordTransactionRequest);//objGetRecordTransactionRequest.RefNoteDirec;
                     objPla.X_INTER_19 = objGetRecordTransactionRequest.DescrpCountry;

                     if (objGetRecordTransactionRequest.ddlNoteDepartment != null)
                     {
                         objPla.X_DEPARTMENT = objGetRecordTransactionRequest.hdnDepDes;
                     }
                     else
                     {
                        objPla.X_DEPARTMENT = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                     }
                    objPla.X_LOT_CODE = ConfigurationManager.AppSettings("strConsTraE");
                    objPla.X_CITY = objGetRecordTransactionRequest.hdnProDes;
                    objPla.X_DISTRICT = objGetRecordTransactionRequest.hdnDisDes;
                    objPla.X_INTER_20 = objGetRecordTransactionRequest.hdnCodEdi;
                    objPla.X_REFERENCE_ADDRESS = GenerarNotasDireccion(objGetRecordTransactionRequest);//objGetRecordTransactionRequest.RefNoteDirec;

                    if (objGetRecordTransactionRequest.hdnCodPos != null)
                         objPla.X_ZIPCODE = objGetRecordTransactionRequest.hdnCodPos.Trim(); //Codigo Postal 
                                   
                    objPla.X_INTER_21 = objGetRecordTransactionRequest.hdnCenPobDes;
                    objPla.X_FIXED_NUMBER = objGetRecordTransactionRequest.hdnCodPla;
                    if (objGetRecordTransactionRequest.chkUseChangeBillingChecked == true)
                     {
                        objPla.X_OLD_CLAROLOCAL1 = CONSTANT.Constants.PresentationLayer.NumeracionUNO;
                     }
                     else
                     {
                        objPla.X_OLD_CLAROLOCAL1 = objPla.X_FLAG_REGISTERED = CONSTANT.Constants.PresentationLayer.NumeracionCERO;
                     }
                     objPla.X_LASTNAME_REP = objGetRecordTransactionRequest.hdnUbiID;
                     objPla.X_LOT_CODE = ConfigurationManager.AppSettings("strConsTraE");
             }

                objPla.X_CLAROLOCAL3 = objGetRecordTransactionRequest.chkLoyalty == true ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO;
                objPla.X_CLAROLOCAL4 = objGetRecordTransactionRequest.Cargo;
                objPla.X_CLAROLOCAL5 = objGetRecordTransactionRequest.chkEmailChecked == true ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO;
                objPla.X_CLAROLOCAL6 = objGetRecordTransactionRequest.chkEmailChecked == true ? objGetRecordTransactionRequest.Email:string.Empty;
                objPla.X_TYPE_DOCUMENT = objGetRecordTransactionRequest.strtypeCliente;

             blnValidate = true;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, " DatTemplateInteraction()   catch (Exception ex)");
                blnValidate = false;
            }

            Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + objGetRecordTransactionRequest.strTransaction, "OUT DatTemplateInteraction()");
            return objPla;
        }
        #endregion

        #region CONSTANCY PDF
        public string GetConstancyPDF(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string strInteraction)
        {
            string NAME_PDF = string.Empty;
            Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GetConstancyPDF() - HFC");
            try
            {
                CommonTransacService.ParametersGeneratePDF oParameter = new CommonTransacService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strTrasladoInternoExternoFormatoTransac"),


                    strPuntoDeAtencion = objGetRecordTransactionRequest.DescripCADDAC,
                    strNroDoc = objGetRecordTransactionRequest.NumbDocRepreCustomer,
                    strFechaTransaccion = DateTime.Today.ToShortDateString(),
                    StrFechaTransaccionProgram = objGetRecordTransactionRequest.FechaProgramada,
                    strCasoInteraccion = strInteraction,
                    StrCasoInter = strInteraction,
                    strTransaccionDescripcion = objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro ? ConfigurationManager.AppSettings("strConsTraI") : ConfigurationManager.AppSettings("strConsTraE"),
                    StrCostoTransaccion = objGetRecordTransactionRequest.Cargo,
                    strDireccionClienteActual = objGetRecordTransactionRequest.AddressCustomer,
                    strRefTransaccionActual = objGetRecordTransactionRequest.RefAddressCustomer,
                    strDistritoClienteActual = objGetRecordTransactionRequest.DistCustomer,
                    strCodigoPostalActual = objGetRecordTransactionRequest.CodPos,
                    strPaisClienteActual = objGetRecordTransactionRequest.CountryCustomer,
                    strProvClienteActual = objGetRecordTransactionRequest.ProvCustomer,

                    strDirClienteDestino = GenerarDireccion(objGetRecordTransactionRequest), 
                    strRefTransaccionDestino = objGetRecordTransactionRequest.Referencia,
                    strDepClienteDestino = objGetRecordTransactionRequest.hdnDepDes,
                    strDistClienteDestino = objGetRecordTransactionRequest.hdnDisDes,
                    strAplicaCambioDirFact = objGetRecordTransactionRequest.chkUseChangeBillingChecked ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    strCodigoPostallDestino = objGetRecordTransactionRequest.hdnCodPos,
                    strPaisClienteDestino = objGetRecordTransactionRequest.PAIS_LEGAL,
                    strProvClienteDestino = objGetRecordTransactionRequest.hdnProDes,

                    strEnviomail = objGetRecordTransactionRequest.chkEmailChecked ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    strCorreoCliente = objGetRecordTransactionRequest.chkEmailChecked ? objGetRecordTransactionRequest.Email : string.Empty,

                    strCentroAtencion = objGetRecordTransactionRequest.DescripCADDAC,
                    strContratoCliente = objGetRecordTransactionRequest.ConID,
                    strDepClienteActual = objGetRecordTransactionRequest.DEPARTAMENTO,


                    //New
                    StrTitularCliente = objGetRecordTransactionRequest.nameCustomer,
                    StrRepresLegal = objGetRecordTransactionRequest.RepreCustomer,
                    StrTipoDocIdentidad = objGetRecordTransactionRequest.TypDocRepreCustomer,

                    StrCentroAtencionArea = objGetRecordTransactionRequest.DescripCADDAC,
                    StrNroDocIdentidad=objGetRecordTransactionRequest.NumbDocRepreCustomer,
                    strContrato = objGetRecordTransactionRequest.ConID,
                    StrCodigoLocalA = objGetRecordTransactionRequest.CodPos,

                    StrCodigoLocalB =objGetRecordTransactionRequest.hdnCodPos,
                    strCodigoPlanoDestino = objGetRecordTransactionRequest.hdnCodPla,
                    strEnvioCorreo = objGetRecordTransactionRequest.chkEmailChecked ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    StrEmail =  objGetRecordTransactionRequest.chkEmailChecked ? objGetRecordTransactionRequest.Email:string.Empty,
                    strNroSot = objGetRecordTransactionRequest.strNroSOT,
                    StrContenidoComercial2 = Functions.GetValueFromConfigFile("ExtIntTrasnferContentCommercial2",
                        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")),



                    strflagTipoTraslado = objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro ? CONSTANT.Constants.strCero : CONSTANT.Constants.strUno,
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTrasladoInternoExternoHFC")
                };

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyPDF(objGetRecordTransactionRequest.strIdSession, oParameter);

                NAME_PDF = response.FullPathPDF;
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GetConstancyPDF()-TRASLADO INTERNO/EXTERNO - HFC _ NAME_PDF: " + NAME_PDF);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }

            return NAME_PDF;
        }
        #endregion

        #region  REGISTRO OCC - UPDATE POSTAL ADDRESS
        public void GrabarCambioDireccionPostal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string lnkNumSot)
        {
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GrabarCambioDireccionPostal()");
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "chkUseChangeBillingChecked: " + objGetRecordTransactionRequest.chkUseChangeBillingChecked.ToString());
            
                #region InsertarTrasladoDireccion
                bool salida = false;
                int FlagDirecFact = objGetRecordTransactionRequest.chkUseChangeBillingChecked == true ? CONSTANT.Constants.numeroUno : CONSTANT.Constants.numeroCero;

                FixedTransacService.Customer oCustomer = new FixedTransacService.Customer();

                oCustomer.CustomerID = objGetRecordTransactionRequest.CustomerID;

                if (objGetRecordTransactionRequest.TipoVia != string.Empty)
                { //ddlStreet
                    oCustomer.Address = GenerarDireccion(objGetRecordTransactionRequest);
                }

                if (objGetRecordTransactionRequest.TipoUrb != string.Empty && objGetRecordTransactionRequest.NomUrb != string.Empty)
                { //ddlStreet
                    oCustomer.Reference = GenerarNotasDireccion(objGetRecordTransactionRequest);
                }

                if (objGetRecordTransactionRequest.hdnDisDes != string.Empty)
                {
                    oCustomer.District = objGetRecordTransactionRequest.hdnDisDes;
                }
                else
                {
                    oCustomer.District = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                }

                if (objGetRecordTransactionRequest.hdnProDes != null)
                {
                    oCustomer.Province = objGetRecordTransactionRequest.hdnProDes;
                }
                else
                {
                    oCustomer.Province = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                }
                if (objGetRecordTransactionRequest.hdnProDes != null)
                {
                    oCustomer.Province = objGetRecordTransactionRequest.hdnProDes;
                }
                else
                {
                    oCustomer.Province = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                }

                if (objGetRecordTransactionRequest.ddlDepDes != null)
                {
                    oCustomer.Departament = objGetRecordTransactionRequest.ddlDepDes;
                }
                else
                {
                    oCustomer.Departament = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                }

                if (objGetRecordTransactionRequest.DescrpCountry != null)
                {
                    oCustomer.LegalCountry = objGetRecordTransactionRequest.DescrpCountry;
                }
                else
                {
                    oCustomer.LegalCountry = ConfigurationManager.AppSettings("gConstKeyStrNoIndicado");
                }

                FixedTransacService.InsertLoyaltyResponse objInsertLoyaltyResponse = null;
                FixedTransacService.AuditRequest auditFixed = new FixedTransacService.AuditRequest();
                auditFixed.applicationName = objGetRecordTransactionRequest.strApplicationName;
                auditFixed.ipAddress = objGetRecordTransactionRequest.strIpAddress;
                auditFixed.Session = objGetRecordTransactionRequest.strIdSession;
                auditFixed.transaction = objGetRecordTransactionRequest.strTransaction;
                auditFixed.userName = objGetRecordTransactionRequest.strUserName;
                try
                {
                    FixedTransacService.InsertLoyaltyRequest objInsertLoyaltyRequest = new FixedTransacService.InsertLoyaltyRequest()
                    {
                        audit = auditFixed,
                        oCustomer = oCustomer,
                        vCodSoLot = lnkNumSot,
                        vFlagDirecFact = FlagDirecFact,
                        vUser = objGetRecordTransactionRequest.CurrentUser,
                        vFechaReg = DateTime.Now
                    };
                    objInsertLoyaltyRequest.oCustomer = oCustomer;
                    objInsertLoyaltyResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertLoyaltyResponse>(() =>
                    {
                        return oFixedTransacService.GetInsertLoyalty(objInsertLoyaltyRequest);
                    });

                    salida = objInsertLoyaltyResponse.rSalida;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                }

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "GetInsertLoyalty() - HFC _ salida: " + salida.ToString());
            #endregion  

            if (objGetRecordTransactionRequest.chkUseChangeBillingChecked)
            {                
                if (salida)
                {
                    #region UpdateDataPostalAddress
                    bool boolUpdPostal = false;
                    boolUpdPostal = UpdateDataPostalAddress(objGetRecordTransactionRequest, auditFixed);
                    if (boolUpdPostal)
                    {
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, boolUpdPostal.ToString());
                    }
                    #endregion
                }
                else
                {
                    string lblMenError;
                    lblMenError = Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
            }

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GrabarCambioDireccionPostal() - HFC");
        }
        
        public void GrabarRegistroOCC(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string lnkNumSot)
        {
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GrabarRegistroOCC()");
            bool salida = false;
            string sMonto =string.Empty;

            if(objGetRecordTransactionRequest.strIgv != null)
            {
                sMonto = (Functions.CheckDecimal(objGetRecordTransactionRequest.Cargo) / decimal.Round(Claro.Constants.NumberOne + Functions.CheckDecimal(objGetRecordTransactionRequest.strIgv), 2)).ToString();
            }
            else
            {
                sMonto = "0.00";
            }

            sMonto = Functions.CheckDecimal(sMonto) > 0 ? sMonto : CONSTANT.Constants.PresentationLayer.NumeracionCERODECIMAL2;
            
            int sMes = DateTime.Now.Month + 1;
            int sAnio = DateTime.Now.Year;
            if (sMes == 13)
            {
                sMes = 1;
                sAnio = sAnio + 1;
            }

            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = objGetRecordTransactionRequest.strApplicationName;
            audit.ipAddress = objGetRecordTransactionRequest.strIpAddress;
            audit.Session = objGetRecordTransactionRequest.strIdSession;
            audit.transaction = objGetRecordTransactionRequest.strTransaction;
            audit.userName = objGetRecordTransactionRequest.strUserName;
            try
            {
                string sFecha = objGetRecordTransactionRequest.CicloFact + "/" + sMes.ToString() + "/" + sAnio;
                string sComentario = ConfigurationManager.AppSettings("gConstComentarioTraslado");
                int Flag = objGetRecordTransactionRequest.chkLoyalty == true ? 0 : 1;
                string strNomApp = ConfigurationManager.AppSettings("NombreAplicacion");
                DateTime FechaAct = DateTime.Now;
                FixedTransacService.SaveOCCResponse objSaveOCCResponse = null;
                FixedTransacService.SaveOCCRequest objSaveOCCRequest = new FixedTransacService.SaveOCCRequest()
                {
                    audit = audit,
                    vCodSot = Functions.CheckInt(lnkNumSot),
                    vCustomerId = Functions.CheckInt(objGetRecordTransactionRequest.CustomerID),
                    vFechaVig = Functions.CheckDate(sFecha),
                    vMonto = Functions.CheckDbl(sMonto),
                    vComentario = sComentario,
                    vflag = Flag,
                    vAplicacion = strNomApp,
                    vUsuarioAct = objGetRecordTransactionRequest.CurrentUser,
                    vFechaAct = FechaAct,
                    vCodId = objGetRecordTransactionRequest.ConID,
                };

                objSaveOCCResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.SaveOCCResponse>(() =>
                {
                    return oFixedTransacService.GetSaveOCC(objSaveOCCRequest);
                });

                salida = objSaveOCCResponse.rSalida;

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "GetSaveOCC() - HFC _ salida:  " + salida.ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GrabarRegistroOCC() - HFC");
        }

        public bool UpdateDataPostalAddress(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, FixedTransacService.AuditRequest audit)
        {
            bool blnResult = false;
            string strGenerarDireccion = GenerarDireccion(objGetRecordTransactionRequest);
            string strReferencia = GenerarNotasDireccion(objGetRecordTransactionRequest);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN UpdateDataPostalAddress - LTE ");
            FixedTransacService.AddressUpdateFixedResponse objAddressUpdateResponse = null;

            FixedTransacService.AddressUpdateFixedRequest objAddressUpdateRequest = new FixedTransacService.AddressUpdateFixedRequest()
            {
                audit = audit,
                strIdCustomer = objGetRecordTransactionRequest.CustomerID,
                strDomicile = strGenerarDireccion,
                strReference = strReferencia,
                strDistrict = objGetRecordTransactionRequest.hdnDisDes,
                strProvince = objGetRecordTransactionRequest.hdnProDes,
                strCodPostal =objGetRecordTransactionRequest.hdnCodPos,
                StrDepartament = objGetRecordTransactionRequest.hdnDepDes,
                strCountryLegal = objGetRecordTransactionRequest.DescrpCountry
            };
            try
            {
                objAddressUpdateResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.AddressUpdateFixedResponse>(() =>
                {
                    return oFixedTransacService.GetUpdateAddress(objAddressUpdateRequest);
                });
                blnResult = objAddressUpdateResponse.blnResult;
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "GetUpdateAddress - HFC _ blnResult:" + blnResult.ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT UpdateDataPostalAddress - HFC ");

            return blnResult;
        }

        public string GenerarDireccion(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GenerarDireccion() - HFC ");
            string strBldDireccion = string.Empty;
            var BldDireccion = new System.Text.StringBuilder();
            try
            {

            if (objGetRecordTransactionRequest.hdnTipoVia != CONSTANT.Constants.PresentationLayer.CODIGO_SIN_NOMBRE)
                BldDireccion.AppendFormat("{0} {1}", objGetRecordTransactionRequest.TipoVia.Substring(0, 2), objGetRecordTransactionRequest.NomVia != null?objGetRecordTransactionRequest.NomVia:string.Empty);
            else
                BldDireccion.AppendFormat("{0} {1}", Functions.GetValueFromConfigFile("ValueNoTieneTipoVia", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")), objGetRecordTransactionRequest.NomVia != null ? objGetRecordTransactionRequest.NomVia : string.Empty);


            if (objGetRecordTransactionRequest.NroVia != null)
                BldDireccion.AppendFormat(" {0}", objGetRecordTransactionRequest.NroVia.Trim());

            if (objGetRecordTransactionRequest.NroVia != null && objGetRecordTransactionRequest.hdnTipMzBloEdi != null && objGetRecordTransactionRequest.NumLote != null)
                BldDireccion.AppendFormat(" {0}", CONSTANT.Constants.PresentationLayer.gstrVariableSNAbreviado);

            if (objGetRecordTransactionRequest.ddlTipMzBloEdi != null)
                BldDireccion.AppendFormat(" {0} {1}", objGetRecordTransactionRequest.ddlTipMzBloEdi, objGetRecordTransactionRequest.hdnTipMzBloEdi != null ? objGetRecordTransactionRequest.hdnTipMzBloEdi.ToUpper() : string.Empty);
            if (objGetRecordTransactionRequest.NumLote != null)
                BldDireccion.AppendFormat(" {0} {1}", CONSTANT.Constants.PresentationLayer.APOCOPE_LOTE, objGetRecordTransactionRequest.NumLote != null ? objGetRecordTransactionRequest.NumLote : string.Empty);

            if (objGetRecordTransactionRequest.ddlDepartment != null)
                BldDireccion.AppendFormat(" {0} {1}", objGetRecordTransactionRequest.hdnDepartment, objGetRecordTransactionRequest.hdnNumberDepartment != null ? objGetRecordTransactionRequest.hdnNumberDepartment : string.Empty);

            strBldDireccion = BldDireccion.ToString();
             }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
                
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "strBldDireccion: " + strBldDireccion);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GenerarDireccion() - HFC ");
            return strBldDireccion;
        }

        public string GenerarNotasDireccion(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {
          
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "IN GenerarNotasDireccion() - HFC ");
                string strBldNoteDireccion = string.Empty;
                var BldNoteDireccion = new System.Text.StringBuilder();
            try
            {
                if (objGetRecordTransactionRequest.TipoUrb != null)
                    BldNoteDireccion.AppendFormat("{0} {1}", objGetRecordTransactionRequest.hdnTipoUrb != null ? objGetRecordTransactionRequest.hdnTipoUrb.Substring(0, 2) : string.Empty, objGetRecordTransactionRequest.NomUrb != null? objGetRecordTransactionRequest.NomUrb: string.Empty);

                strBldNoteDireccion = string.Format("{0} {1}", BldNoteDireccion.ToString(), objGetRecordTransactionRequest.Referencia!= null ? objGetRecordTransactionRequest.Referencia: string.Empty);

            }
            catch(Exception ex) 
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, ex.Message);
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "strBldNoteDireccion: " + strBldNoteDireccion);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strTransaction, "OUT GenerarDireccion() - HFC");
            return strBldNoteDireccion;
        }
        #endregion

        #region EMAIL
        public string GetSendEmail(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer oExternalInternalTransfer, string strAdjunto, byte[] attachFile, CommonTransacService.AuditRequest audit)
        {
            string strResul = string.Empty;
             CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
              
            string strTemplateEmail = TemplateEmail(oExternalInternalTransfer);
            string strTIEMailAsunto = oExternalInternalTransfer.strtypetransaction == CONSTANT.Constants.strCuatro ? ConfigurationManager.AppSettings("strTrasladoInternoMailAsunto") : ConfigurationManager.AppSettings("strTrasladoExternoMailAsunto");

            string strDestinatarios = oExternalInternalTransfer.Email;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = audit,
                    strSender = strRemitente,
                    strTo = strDestinatarios,
                    strMessage = strTemplateEmail,
                    strAttached = strAdjunto,
                    strSubject = strTIEMailAsunto,
                    AttachedByte = attachFile
                };
            objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oCommonTransacService.GetSendEmailFixed(objGetSendEmailRequest); });

                if (objGetSendEmailResponse.Exit == CONSTANT.Constants.CriterioMensajeOK)
                {
                    strResul = Functions.GetValueFromConfigFile("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                else
                {
                    strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oExternalInternalTransfer.strIdSession, oExternalInternalTransfer.strTransaction, ex.Message);
                Claro.Web.Logging.Info(oExternalInternalTransfer.strIdSession, oExternalInternalTransfer.strTransaction, "Transfer Interno/Externo_ HFC  ERROR - GetSendEmail");
                strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return strResul;
        }

        public string TemplateEmail(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer vobjPlaInt)
        {
            string strmessage = string.Empty;
            var strHtml = new System.Text.StringBuilder();


            strmessage = "<html>";
            strmessage += "<head>";
            strmessage += "<style type='text/css'>";
            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strmessage += "</style>";
            strmessage += "</head>";
            strmessage += "<body>";
            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += "<tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            if (vobjPlaInt.strtypetransaction == CONSTANT.Constants.strCuatro)
                strmessage += "<tr><td width='180' class='Estilo1' height='22'>Por la Presente queremos informarle que su solicitud de Traslado Interno fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
            else
                strmessage += "<tr><td width='180' class='Estilo1' height='22'>Por la Presente queremos informarle que su solicitud de Traslado Externo fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";
            return strmessage;
        }


        #endregion

        #region RECORD AUDIT

        public void InsertAudit(string strTransacionID, string strTransacion, HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransaction, CommonTransacService.AuditRequest audit)
        {
            CommonServicesController commonController = new CommonServicesController();


            string strServicio = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            string strIPCliente =  System.Web.HttpContext.Current.Request.UserHostAddress;
            string strNomCli = objGetRecordTransaction.nameCustomer;
            string strCueUsu = objGetRecordTransaction.CurrentUser;
            string strTel = objGetRecordTransaction.CustomerID;
            int strMonto = CONSTANT.Constants.numeroCero;
            string strTexto = string.Format("/Ip Cliente: {0}/Usuario: {1}/Opcion: {2}/Fecha y Hora: {3}", strIPCliente, strCueUsu, strTransacion, DateTime.Now.ToString());

            CommonTransacService.SaveAuditRequestCommon objRegAuditRequest =
                new CommonTransacService.SaveAuditRequestCommon()
                {
                    audit = audit,
                    vTransaccion = strTransacionID,
                    vServicio = strServicio,
                    vIpCliente = strIPCliente,
                    vNombreCliente = strNomCli,
                    vIpServidor = audit.ipAddress,
                    vNombreServidor = audit.applicationName,
                    vCuentaUsuario = strCueUsu,
                    vTelefono = strTel,
                    vMonto = strMonto.ToString(),
                    vTexto = strTexto
                };
            CommonTransacService.SaveAuditResponseCommon objRegAuditResponse = new CommonTransacService.SaveAuditResponseCommon();

            try
            {
                objRegAuditResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() => { return oCommonTransacService.SaveAudit(objRegAuditRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransaction.strIdSession, objGetRecordTransaction.strTransaction, ex.Message);

            }
        }


        #endregion

        #region  LOAD - SELECT  - CONFIGURATION

        public JsonResult InitGetMessageConfiguration(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();

            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelDep"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelPro"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelDis"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstMsgNSFranjaHor"));
            lstMessage.Add(ConfigurationManager.AppSettings("strAlertaEstaSegGuarCam"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADISeCodPlano"));
            lstMessage.Add(CONSTANT.Constants.strCuatro);
            lstMessage.Add(CONSTANT.Constants.strTres);
            lstMessage.Add(CONSTANT.Constants.strCuatro);
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgTranGrabSatis"));
            lstMessage.Add(ConfigurationManager.AppSettings("hdnMessageSendMail"));
            lstMessage.Add(ConfigurationManager.AppSettings("ddlSOT"));
            lstMessage.Add(CONSTANT.Constants.CriterioMensajeOK);
            lstMessage.Add(ConfigurationManager.AppSettings("gStrMsjConsultaCapacidadNoDisp"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstTipTraTI"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstTipTraTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgNoHayEPECP"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeEmail"));
            lstMessage.Add(ConfigurationManager.AppSettings("strConsultationCoverageHFCTitle"));
            lstMessage.Add(ConfigurationManager.AppSettings("strConsultationCoverageHFCURL"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstOpcTIEHabFidelizar"));
            lstMessage.Add(CONSTANT.Constants.strCero);
            lstMessage.Add("MTE1P");
            lstMessage.Add("MTE2P");
            lstMessage.Add("MTE3P");
            lstMessage.Add("MTI");
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActivaRadioTrasladoInternoDTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActivaRadioTrasladoExternoDTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActCheckDirFactTrasladosDTH"));
            lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMessageETAValidation"));
            lstMessage.Add("srtCodOpcionTrasladoInternoHFC");
            lstMessage.Add("srtCodOpcionTrasladoExternoHFC");
            //Inicio INICIATIVA167-FTTH
            lstMessage.Add(ConfigurationManager.AppSettings("strMotivoSotFTTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strPlanoFTTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strCodigoAuxiliarExternoFTTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strCodigoAuxiliarInternoFTTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strTipoTrabajoTrasladoExternoFTTH"));
            lstMessage.Add(ConfigurationManager.AppSettings("strTipoTrabajoTrasladoInternoFTTH"));
            //Fin INICIATIVA167-FTTH
            return Json(lstMessage, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetListDataProducts(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            FixedTransacService.BEDecoServicesResponseFixed objProdDecoResponseCommon = null;//
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.BEDecoServicesRequestFixed objProDecoRequestCommon = new FixedTransacService.BEDecoServicesRequestFixed() //
            {
                audit = audit,
                vCoID = CONTRATO_ID,
                vCusID = CUSTOMER_ID
            };

            try
            {

                objProdDecoResponseCommon = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEDecoServicesResponseFixed>(() => { return oFixedTransacService.GetServicesDTH(objProDecoRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }

            MODEL.HFC.ExternalInternalTransferModel objFixedTransacServices = null;
            if (objProdDecoResponseCommon.ListDecoServices.Count>0)
            {
            objFixedTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
            List<HELPERS.HFC.ExternalInternalTransfer.DataProducts> ListDataProducts = new List<HELPERS.HFC.ExternalInternalTransfer.DataProducts>();

                foreach (FixedTransacService.BEDeco item in objProdDecoResponseCommon.ListDecoServices)
                {
            ListDataProducts.Add(new HELPERS.HFC.ExternalInternalTransfer.DataProducts()
            {

                        MaterialCode = item.codigo_material,
                        SapCode = item.codigo_sap,
                        SerieNumber = item.numero_serie,
                        AdressMac = item.macadress,
                        MaterialDescripcion = item.descripcion_material,
                        EquipmentType = item.tipo_equipo,
                        ProductId = item.id_producto,
                        Type = item.tipo_equipo,
                        ConvertType = item.macadress,
                        ServiceType = item.tipoServicio,
                        Headend = item.headend,
                        PricipalService = item.servicio_principal,
                        EphomeexChange = item.ephomeexchange
            });
                }
            objFixedTransacServices.ListDataProducts = ListDataProducts;
            }

            return Json(new { data = objFixedTransacServices });

        }
        public JsonResult GetStateType(string strIdSession, string idList, string strTransaction)
        {
            List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem> GetDocumentType = null;
            GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
            {
                return oCommonTransacService.GetDocumentTypeCOBS(strIdSession, strTransaction, idList);
            });

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (GetDocumentType != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listStateType = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in GetDocumentType)
                {
                    listStateType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listStateType;
            }
            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetDepartments(string strIdSession, string strIdTransaction, string strApplicationName, string strIpAddress, string strUserName)
        {
            var msg = string.Format("In GetDepartments - HFC");
            Claro.Web.Logging.Info(strIdSession, strIdTransaction, msg);
            CommonTransacService.DepartmentsPvuResponseCommon objDepartmentsResponseCommon = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strIdTransaction;
            audit.userName = strUserName;

            CommonTransacService.DepartmentsPvuRequestCommon objDepartmentsRequestCommon = new CommonTransacService.DepartmentsPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objDepartmentsResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.DepartmentsPvuResponseCommon>(() => { return oCommonTransacService.GetDepartmentsPVU(objDepartmentsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, ex.InnerException.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objDepartmentsResponseCommon != null && objDepartmentsResponseCommon != null)
            {

                Claro.Web.Logging.Info(strIdSession, strIdTransaction, "objDepartmentsResponseCommon");
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listDepartments = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objDepartmentsResponseCommon.ListDepartments)
                {
                    listDepartments.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listDepartments;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetProvinces(string strIdSession, string strDepartments, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            CommonTransacService.ProvincesPvuResponseCommon objProvincesResponseCommon = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.ProvincesPvuRequestCommon objProvincesRequestCommon = new CommonTransacService.ProvincesPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objProvincesRequestCommon.CodDep = strDepartments;
                objProvincesResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ProvincesPvuResponseCommon>(() => { return oCommonTransacService.GetProvincesPVU(objProvincesRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objProvincesResponseCommon != null && objProvincesResponseCommon != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listProvinces = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objProvincesResponseCommon.ListProvinces)
                {
                    listProvinces.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listProvinces;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetDistricts(string strIdSession, string strDepartments, string strProvinces, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            CommonTransacService.DistrictsPvuResponseCommon objDistrictsResponseCommon = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.DistrictsPvuRequestCommon objDistrictsRequestCommon = new CommonTransacService.DistrictsPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objDistrictsRequestCommon.CodDepart = strDepartments;
                objDistrictsRequestCommon.CodProv = strProvinces;
                objDistrictsResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.DistrictsPvuResponseCommon>(
                    () =>
                    { return oCommonTransacService.GetDistrictsPVU(objDistrictsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objDistrictsResponseCommon != null && objDistrictsResponseCommon != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listDistricts = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objDistrictsResponseCommon.ListDistricts)
                {
                    listDistricts.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listDistricts;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetZoneTypes(string strIdSession, string strTransaction, string strApplicationName, string strIpAddress, string strUserName)
        {
            CommonTransacService.ZoneTypeCobsResponseCommon objZoneTypeResponse = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.userName = strUserName;
            CommonTransacService.ZoneTypeCobsRequestCommon objZoneTypeCobsRequestCommon = new CommonTransacService.ZoneTypeCobsRequestCommon()
            {
                audit = audit
            };

            try
            {
                objZoneTypeResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ZoneTypeCobsResponseCommon>(() => { return oCommonTransacService.GetZoneTypeCOBS(objZoneTypeCobsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objZoneTypeResponse != null && objZoneTypeResponse != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listZoneType = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objZoneTypeResponse.ListZoneType)
                {
                    listZoneType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listZoneType;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetMzBloEdiType(string strIdSession, string strApli, string strIpe, string strTransaction, string strUserName)
        {
            CommonTransacService.MzBloEdiTypeResponseCommon objMzBloEdiTypeResponse = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApli;
            audit.ipAddress = strIpe;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.MzBloEdiTypeRequestCommon objMzBloEdiTypeRequest = new CommonTransacService.MzBloEdiTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objMzBloEdiTypeResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.MzBloEdiTypeResponseCommon>(() => { return oCommonTransacService.GetMzBloEdiTypePVU(objMzBloEdiTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objMzBloEdiTypeResponse != null && objMzBloEdiTypeResponse != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> ListMzBloEdiType = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objMzBloEdiTypeResponse.ListMzBloEdiType)
                {
                    ListMzBloEdiType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = ListMzBloEdiType;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetTipDptInt(string strIdSession, string strApli, string strIpe, string strTransaction, string strUserName)
        {
            CommonTransacService.TipDptIntResponseCommon objTipDptIntResponse = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApli;
            audit.ipAddress = strIpe;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.TipDptIntRequestCommon objTipDptIntRequest = new CommonTransacService.TipDptIntRequestCommon()
            {
                audit = audit
            };

            try
            {
                objTipDptIntResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.TipDptIntResponseCommon>(() => { return oCommonTransacService.GetTipDptInt(objTipDptIntRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objTipDptIntResponse != null && objTipDptIntResponse != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> ListTipDptInt = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objTipDptIntResponse.LisTipDptIntType)
                {
                    ListTipDptInt.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = ListTipDptInt;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetWorkSubType(string strIdSession, string strCodTypeWork, string strContractID, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            List<HELPERS.CommonServices.GenericItem> objListaEta = new List<HELPERS.CommonServices.GenericItem>();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;
            FixedTransacService.OrderSubTypesResponseHfc objResponse = new FixedTransacService.OrderSubTypesResponseHfc();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            MODEL.HFC.ExternalInternalTransferModel objFixedGetSubOrderType = null;
            try
            {

                FixedTransacService.AuditRequest auditreq = new FixedTransacService.AuditRequest();
                auditreq.applicationName = strApplicationName;
                auditreq.ipAddress = strIpAddress;
                auditreq.Session = strIdSession;
                auditreq.transaction = strTransaction;
                auditreq.userName = strUserName;

                objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
            {
                audit = auditreq,
                    av_cod_tipo_trabajo = strCodTypeWork
            };
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubTypesResponseHfc>(() => { return oFixedTransacService.GetOrderSubTypeWork(objResquest); });

            List<HELPERS.CommonServices.GenericItem> ListSubOrderType = new List<HELPERS.CommonServices.GenericItem>();
                if (objResponse != null && objResponse.OrderSubTypes != null)
            {
                objFixedGetSubOrderType = new MODEL.HFC.ExternalInternalTransferModel();

                foreach (FixedTransacService.OrderSubType item in objResponse.OrderSubTypes)
                {
                    ListSubOrderType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                            Code = string.Format("{0}|{1}|{2}", item.COD_SUBTIPO_ORDEN, item.TIEMPO_MIN, item.ID_SUBTIPO_ORDEN),
                            Description = item.DESCRIPCION,
                            Code2 = item.TIPO_SERVICIO
                    });
                }
                objFixedGetSubOrderType.ListGeneric = ListSubOrderType;
            }

                objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
                {
                    audit = auditreq,
                    av_cod_tipo_trabajo = strCodTypeWork,
                    av_cod_contrato = strContractID
                };
                objResponseValidate = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubType>(() => { return oFixedTransacService.GetValidationSubTypeWork(objResquest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return Json(new { data = objFixedGetSubOrderType, typeValidate = objResponseValidate });
        }

        public JsonResult GetWorkType(string strIdSession, string strTransacType, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            CommonTransacService.WorkTypeResponseCommon objWorkTypeResponseCommon = null;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.WorkTypeRequestCommon objWorkTypeRequestCommon = new CommonTransacService.WorkTypeRequestCommon()
            {
                audit = audit

            };

            try
            {
                objWorkTypeRequestCommon.TransacType = Convert.ToInt(strTransacType);
                objWorkTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.WorkTypeResponseCommon>(() => { return oCommonTransacService.GetWorkType(objWorkTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);               
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objWorkTypeResponseCommon != null && objWorkTypeResponseCommon.WorkTypes != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listWorkTypes = new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objWorkTypeResponseCommon.WorkTypes)
                {
                    listWorkTypes.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonTransacServices.ListGeneric = listWorkTypes;
            }

            return Json(new { data = objCommonTransacServices });
        }
        public JsonResult GetMotiveSot(string strIdSession, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {

            CommonTransacService.MotiveSotResponseCommon objMotiveSotResponseCommon = new CommonTransacService.MotiveSotResponseCommon() ;
            CommonTransacService.AuditRequest audit = new CommonTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            CommonTransacService.MotiveSotRequestCommon objMotiveSotRequestCommon =
                new CommonTransacService.MotiveSotRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objMotiveSotResponseCommon =
                    Claro.Web.Logging.ExecuteMethod<CommonTransacService.MotiveSotResponseCommon>(() =>
                    {
                        return oCommonTransacService.GetMotiveSot(objMotiveSotRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonServices = null;
            if (objMotiveSotResponseCommon != null && objMotiveSotResponseCommon.getMotiveSot != null)
            {
                objCommonServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listCacDacTypes =
                    new List<HELPERS.CommonServices.GenericItem>();

                foreach (CommonTransacService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
                {
                    listCacDacTypes.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.ListGeneric = listCacDacTypes;
            }

            return Json(new { data = objCommonServices });



        }
        public JsonResult GetUbigeoID(string strIdSession, string vstrDisID, string vstrDepID, string vstrProvID, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            
            FixedTransacService.IdUbigeoFixedResponse objreponse = new FixedTransacService.IdUbigeoFixedResponse();
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.IdUbigeoFixedRequest objrequest = new FixedTransacService.IdUbigeoFixedRequest()
            {
                audit = audit,
                strDepartment = vstrDepID,
                strProvince = vstrProvID,
                strDistrict = vstrDisID

            };
            try
            {
                objreponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.IdUbigeoFixedResponse>(() => { return oFixedTransacService.GetIdUbigeo(objrequest); });
            }
            catch (Exception)
            {
                
                throw;
            }


            HELPERS.CommonServices.GenericItem UbigeoID = new HELPERS.CommonServices.GenericItem()
            {
                Code = objreponse.strIdUbigeo
            };
            //obRequest.Command = BaseDatos.NOMBRE_SISACT_PKG_SOLICITUD + ".SECSS_CON_DISTRITO";
            return Json(new { data = UbigeoID.Code });
        }
        public JsonResult ObtenerCodigoPostal(string strIdSession, string vstrDisID, string strTransaction)
        {

             var msg = string.Format("In ObtenerCodigoPostal - HFC - vstrDisID : {0}", vstrDisID);

             Claro.Web.Logging.Info(strIdSession, strTransaction, msg);

            string strcodePostal = GetPostalCode(strIdSession, strTransaction, vstrDisID);

            msg = string.Format("Out ObtenerCodigoPostal - HFC - strcodePostal : {0}", strcodePostal);
            Claro.Web.Logging.Info(strIdSession, strTransaction, msg);
            return Json(new {data = strcodePostal});


        }
        public JsonResult GetListCenterPob(string strIdSession, string strUbigeo, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            FixedTransacService.ListTownCenterFixedResponse objListTownCenterResponse = null;
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.ListTownCenterFixedRequest objListTownCenterRequest = new FixedTransacService.ListTownCenterFixedRequest()
            {
                audit = audit,
                strUbigeo = strUbigeo
            };
            objListTownCenterResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ListTownCenterFixedResponse>(() =>
            {
                return oFixedTransacService.GetListTownCenter(objListTownCenterRequest);
            });

            MODEL.HFC.ExternalInternalTransferModel objFixedTransacServices = null;
            if (objListTownCenterResponse != null)
            {
                objFixedTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> ListTownCenter = new List<HELPERS.CommonServices.GenericItem>();
                foreach (FixedTransacService.GenericItem item in objListTownCenterResponse.ListItem)
                {
                    ListTownCenter.Add(new HELPERS.CommonServices.GenericItem()
                        {
                            IdMotive=item.Id_motivo,
                            Number=item.Numero,
                            Code = item.Codigo,
                            Description=item.Descripcion

                        });
                    objFixedTransacServices.ListGeneric = ListTownCenter;
                }

                //  obRequest.Command = BaseDatos.NOMBRE_PACKAGE_SGADBUAT_DTH + ".p_centro_poblado";               
            }
  
             return Json(new { data = objFixedTransacServices });
         
        }
        public string GetPostalCode(string strSession, string strTransaction, string strIdDistrito)
        {
            string strCode = string.Empty;
           List<HELPERS.CommonServices.GenericItem> listPostal = new List<HELPERS.CommonServices.GenericItem>();
         

       
           List<ItemGeneric> listaItem = Functions.GetListValuesXML("ListaCodigoPostal", "0", "HFCDatos.xml");
     
            ItemGeneric item= listaItem.Where(x => x.Code.Equals(strIdDistrito)).FirstOrDefault();
            
            if (item != null)
            {
                strCode = item.Description;
            }

            return strCode;
        }
        public JsonResult GetListEdificios(string strIdSession, string vstrCodPlano, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            FixedTransacService.ListEbuildingsFixedResponse objresponse = new FixedTransacService.ListEbuildingsFixedResponse();
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.ListEbuildingsFixedRequest objrequest = new FixedTransacService.ListEbuildingsFixedRequest()
            {

                audit = audit,
                strCodePlan = vstrCodPlano
            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ListEbuildingsFixedResponse>(() => { return oFixedTransacService.GetListEBuildings(objrequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            MODEL.HFC.ExternalInternalTransferModel objfixedEbuildings = null;
            List<HELPERS.CommonServices.GenericItem> ListEbuildings = new List<HELPERS.CommonServices.GenericItem>();
            if (objresponse != null)
            {
                objfixedEbuildings = new MODEL.HFC.ExternalInternalTransferModel();
                foreach (FixedTransacService.GenericItem item in objresponse.ListEbuildings)
                {
                    ListEbuildings.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        IdMotive = item.Id_motivo,
                        Number = item.Numero,
                        Code = item.Codigo,
                        Code2 = item.Codigo2,
                        Code3 = item.Codigo3,
                        Description = item.Descripcion,
                        Description2 = item.Descripcion2,
                        Type = item.Tipo,
                        Date = item.Fecha

                    });
                }
                objfixedEbuildings.ListGeneric = ListEbuildings;


            }

            return Json(new { data = objfixedEbuildings });

        }
        public JsonResult GetListPlanos(string strIdSession, string vCodUbigeo, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            FixedTransacService.ListPlansFixedResponse objresponse = new FixedTransacService.ListPlansFixedResponse();
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.ListPlansFixedRequest objrequest = new FixedTransacService.ListPlansFixedRequest()
            {
                audit = audit,
                strIdUbigeo = vCodUbigeo

            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ListPlansFixedResponse>(() => { return oFixedTransacService.GetListPlans(objrequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            MODEL.HFC.ExternalInternalTransferModel objFixedGetPlans = null;
            List<HELPERS.CommonServices.GenericItem> ListPlans = new List<HELPERS.CommonServices.GenericItem>();
            if (objresponse.ListPlans != null)
            {
                objFixedGetPlans = new MODEL.HFC.ExternalInternalTransferModel();
                foreach (FixedTransacService.GenericItem item in objresponse.ListPlans)
                {
                    ListPlans.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        IdMotive = item.Id_motivo,
                        Number = item.Numero,
                        Code = item.Codigo,
                        Code2 = item.Codigo2,
                        Code3 = item.Codigo3,
                        Date = item.Fecha
                    });
                }
                objFixedGetPlans.ListGeneric = ListPlans;
            }

            return Json(new { data = objFixedGetPlans });

        }
        public JsonResult GetCobertura(string strIdSession, string valNoteCenterPopulated, string strApplicationName, string strIpAddress, string strTransaction, string strUserName)
        {
            FixedTransacService.CoverageFixedResponse objresponse = new FixedTransacService.CoverageFixedResponse();
            FixedTransacService.AuditRequest audit = new FixedTransacService.AuditRequest();
            audit.applicationName = strApplicationName;
            audit.ipAddress = strIpAddress;
            audit.Session = strIdSession;
            audit.transaction = strTransaction;
            audit.userName = strUserName;
            FixedTransacService.CoverageFixedRequest objrequest = new FixedTransacService.CoverageFixedRequest()
            {
                audit = audit,
                strCob = valNoteCenterPopulated
            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.CoverageFixedResponse>(() => { return oFixedTransacService.GetCoverage(objrequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            HELPERS.CommonServices.GenericItem Cobertura = new HELPERS.CommonServices.GenericItem()
            {
                Code = objresponse.strCoverage
            };

            return Json(new { data = Cobertura });

        }

        #endregion

    }
}