using Claro.SIACU.Transac.Service; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using MODEL = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class ExternalInternalTransferController : Controller
    {
        private readonly FixedTransacService.FixedTransacServiceClient oFixedTransacService = new FixedTransacService.FixedTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();

        public ActionResult LTEExternalInternalTransfer()
        {
            return View("~/Areas/Transactions/Views/ExternalInternalTransfer/LTEExternalInternalTransfer.cshtml");
        }

        #region Record Transaction - RecordTransactionIntExtBL

        public Dictionary<string, string> RecordTransactionIntExtBL(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {
            FixedTransacService.RecordTranferExtIntResponseFixed objGetRecordTransactionResponse = new FixedTransacService.RecordTranferExtIntResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            Dictionary<string, string> objResponse = new Dictionary<string, string>();
            Dictionary<string, string> objResponseInteraction = new Dictionary<string, string>();
            string lnkNumSot = string.Empty;
            string strMessage = string.Empty;
            var msg = string.Format("IN RecordTransactionIntExtBL - LTE");
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, msg);
            bool blnResponse = false;
            try
            {
                MODEL.TemplateInteractionModel objPla = new MODEL.TemplateInteractionModel();
                #region RECORD INTERACTION
                objResponseInteraction = RecordInteraction(objGetRecordTransactionRequest, out objPla);

                if (objResponseInteraction["hdnInterID"] != null)
                {
                    objResponse["hdnInterID"] = objResponseInteraction["hdnInterID"];
                    objResponse["strPath"] = string.Empty;  
                }
                else
                {
                    objResponse["hdnInterID"] = CONSTANT.Constants.strCero;
                    objResponse["strPath"] = string.Empty;   
                }
                #endregion

                #region RECORD TRANSFER

                objGetRecordTransactionRequest.InterCasoID = objResponseInteraction["hdnInterID"];
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "objGetRecordTransactionRequest.InterCasoID: -  LTE " + objGetRecordTransactionRequest.InterCasoID);
                blnResponse =  RecordTransfer(objGetRecordTransactionRequest, out lnkNumSot,    out strMessage);

                #endregion


                if (blnResponse)
                {

                    #region REGISTER CONSTANCY
                    if (objGetRecordTransactionRequest.InterCasoID != null || objGetRecordTransactionRequest.InterCasoID == string.Empty)
                    {
                        objGetRecordTransactionRequest.strNroSOT = lnkNumSot;
                        objResponse["strPath"] = GetConstancyPDF(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest, objResponse["hdnInterID"]);//CONSTANCY
                    }

                    GrabarCambioDireccionPostal(objGetRecordTransactionRequest, lnkNumSot);
                    GrabarRegistroOCC(objGetRecordTransactionRequest, lnkNumSot);

                    #endregion

                    #region INSERT AUDIT
                    if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro)
                    {
                        string strTransacionTI = ConfigurationManager.AppSettings("strConsTraI");
                        InsertAudit(ConfigurationManager.AppSettings("gConstKeyTransTrasladoInternoLTE"), strTransacionTI, objGetRecordTransactionRequest);
                    }
                    else
                    {
                        if (objGetRecordTransactionRequest.chkUseChangeBillingChecked == true)
                        {
                            bool boolUpdPostal = false;
                            boolUpdPostal = UpdateDataPostalAddress(objGetRecordTransactionRequest);
                            if (boolUpdPostal) 
                            {
                                objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                                objGetRecordTransactionResponse.DescMessaTransfer = ConfigurationManager.AppSettings("strMensajeDeError");
                            }

                        }
                        string strTransacionTE = ConfigurationManager.AppSettings("strConsTraE");
                        InsertAudit(ConfigurationManager.AppSettings("gConstKeyTransTrasladoExternoLTE"), strTransacionTE, objGetRecordTransactionRequest);

                    }
                    #endregion

                    string rutaConstancy = objResponse["strPath"];

                    #region SEND EMAIL

                    //ENVIAR CORREO
                    if (objGetRecordTransactionRequest.chkEmailChecked == true)
                    {
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "SEND EMAIL - LTE == true");
                        byte[] attachFile = null;
                        CommonServicesController objCommonServices = new CommonServicesController();
                        //Nombre del archivo
                        string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                        var ResultMAil = string.Empty;
                        if (objCommonServices.DisplayFileFromServerSharedFile(objGetRecordTransactionRequest.strIdSession, audit.transaction, rutaConstancy, out attachFile))
                            ResultMAil = GetSendEmail(objGetRecordTransactionRequest, strAdjunto, attachFile);
                    }


                    objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.CriterioMensajeOK;
                    objGetRecordTransactionResponse.DescMessaTransfer = ConfigurationManager.AppSettings("strMsgTranGrabSatis");

                    #endregion
                }
                else
                {
                    #region UPDATE INTERACTION 30
                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new CommonTransacService.UpdatexInter30Response();
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new CommonTransacService.UpdatexInter30Request()
                    {
                        audit = auditCommon,
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
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "UPDATE INTERACTION 30");

                    #endregion
                }
            }
            catch (Exception ex)
            {
                objGetRecordTransactionResponse.CodMessaTransfer = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                objGetRecordTransactionResponse.DescMessaTransfer = ConfigurationManager.AppSettings("strMensajeDeError");
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
            }

            objResponse["rResult"] = objGetRecordTransactionResponse.CodMessaTransfer;
            objResponse["rMsgText"] = objGetRecordTransactionResponse.DescMessaTransfer;
            objResponse["hdnInterID"] = objResponseInteraction["hdnInterID"]; 
            objResponse["rMsgTextInter"] = objResponseInteraction["rMsgTextInter"];
            objResponse["lnkNumSot"] = lnkNumSot;

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession,audit.transaction, "lnkNumSot: " + lnkNumSot == null ? string.Empty : lnkNumSot);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT RecordTransactionIntExtBL - LTE");
            return objResponse;
        }
        public bool RecordTransfer(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out string lnkNumSot, out string strMessage)
        {
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN RecordTransfer - LTE");
            string strError = string.Empty;
            string strResDes = string.Empty;
            int intResCod = CONSTANT.Constants.numeroCero;
            bool blnRes = false;
            int intNumberCero = CONSTANT.Constants.numeroCero;
            lnkNumSot = string.Empty;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);

            string hdnCodSotValue = string.Empty;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("IN RecordTransfer - LTE -  strtypetransaction: {0}", objGetRecordTransactionRequest.strtypetransaction));

            if (Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroUno ||
                Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroDos)
            {
                #region AgendaGetValidaEta
                if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro)
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
                                        vid_bucket = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[1]
                                    };
                                    objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return oFixedTransacService.GetInsertETASelection(objInsertETASelectionRequest); });
                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Functions.CheckInt(objGetRecordTransactionRequest.InterCasoID) > CONSTANT.Constants.numeroUno)
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
                                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro)
            {
                #region TRANSFER INTERNAL
                FixedTransacService.RegisterTransactionLTEFixedResponse objResponseGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedResponse();
                objResponseGenerateSOT = RecordSotTrasnsferInternal(objGetRecordTransactionRequest);
                hdnCodSotValue = objResponseGenerateSOT.intNumSot;
                strMessage = objResponseGenerateSOT.strResDes;
                intResCod = Convert.ToInt(objResponseGenerateSOT.intResCod);

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT RecordSotTrasnsferInternal - LTE - out cod: {0}  Des: {1}", objResponseGenerateSOT.intNumSot == null ? string.Empty : objResponseGenerateSOT.intNumSot, objResponseGenerateSOT.strResDes == null ? string.Empty : objResponseGenerateSOT.strResDes));
                #endregion
            }
            else
            {
                #region TRANSFER EXTERNAL
                FixedTransacService.RegisterTransactionLTEFixedResponse objResponseGetRecordTransaction = new FixedTransacService.RegisterTransactionLTEFixedResponse();
                objResponseGetRecordTransaction = GetRecordTransactionExternal(objGetRecordTransactionRequest);
                hdnCodSotValue = objResponseGetRecordTransaction.intNumSot;
                strMessage = objResponseGetRecordTransaction.strResDes;
                intResCod = Convert.ToInt(objResponseGetRecordTransaction.intResCod);
                #endregion
            }
            if (hdnCodSotValue != null && hdnCodSotValue != string.Empty && hdnCodSotValue.ToUpper() != "NULL")
            {
                if (Double.Parse(hdnCodSotValue) > 1)
                {
                    #region UPDATE INTERACTION 29  - REGISTRO OK
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

                    if (objUpdateInter29Response.rFlagInsercion == CONSTANT.Constants.CriterioMensajeOK)
                    {
                       lnkNumSot = hdnCodSotValue;
                       Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, " UPDATE INTERACTION 29  - REGISTRO OK");
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
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "UPDATE INTERACTION 30 - SOT EN CURSO - I");
                    }
                    #endregion
                    blnRes = true;
                }
                if (intResCod.Equals(CONSTANT.Constants.numeroTres))
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

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "UPDATE INTERACTION 30 - SOT EN CURSO - II");
                }
            }
            else 
            {
                if (intResCod.Equals(CONSTANT.Constants.numeroTres))
                #region UPDATE INTERACTION 30 - SOT EN CURSO
                {
                   
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
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "UPDATE INTERACTION 30 - SOT EN CURSO - III");
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

                        strMessage = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy");
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, " UPDATE INTERACTION 30 - ERROR EN LA TRANSACCION");
                }
                #endregion

            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "strMessage: " + strMessage);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "OUT RecordTransfer - LTE");
          
            return blnRes;
        }
        public FixedTransacService.RegisterTransactionLTEFixedResponse RecordSotTrasnsferInternal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {

            bool boolFlajGenerarSOT = false;
            int intNumberCero = CONSTANT.Constants.numeroCero;
            FixedTransacService.RegisterTransactionLTEFixedRequest objRequestGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedRequest();
            FixedTransacService.RegisterTransactionLTEFixedResponse objResponseGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            FixedTransacService.RegisterTransaction objRegisterTransaction = new FixedTransacService.RegisterTransaction();

            objRegisterTransaction.CustomerID = objGetRecordTransactionRequest.CustomerID.Trim();
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN RecordSotTrasnsferInternal - LTE");
            if (objGetRecordTransactionRequest.agendaGetTipoTrabajo.Contains(".|"))
                objRegisterTransaction.TrabajoID = objGetRecordTransactionRequest.agendaGetTipoTrabajo.Substring(0, objGetRecordTransactionRequest.agendaGetTipoTrabajo.Length - 2);
            else
                 objRegisterTransaction.TrabajoID = objGetRecordTransactionRequest.agendaGetTipoTrabajo;

            objRegisterTransaction.ConID = objGetRecordTransactionRequest.ConID.Trim();
            objRegisterTransaction.CustomerID = objGetRecordTransactionRequest.CustomerID.Trim();

            objGetRecordTransactionRequest.txtNotText = objGetRecordTransactionRequest.txtNotText != null ? objGetRecordTransactionRequest.txtNotText.Replace('|', '-') : string.Empty;

            if (objGetRecordTransactionRequest.agendaGetFecha == null || objGetRecordTransactionRequest.agendaGetFecha == string.Empty)
            {
                objRegisterTransaction.FechaProgramada = DateTime.Now.ToShortDateString();
            }
            else
            {
                objRegisterTransaction.FechaProgramada = objGetRecordTransactionRequest.agendaGetFecha;
            }

            if (objGetRecordTransactionRequest.FranjaHora == null || objGetRecordTransactionRequest.FranjaHora == string.Empty)
            {
                objRegisterTransaction.FranjaHora = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
            objRegisterTransaction.FranjaHora = objGetRecordTransactionRequest.FranjaHora;
            }
            //objRegisterTransaction.FranjaHora = objGetRecordTransactionRequest.FranjaHora;
            objRegisterTransaction.FranjaHoraID = objRegisterTransaction.FranjaHora; //objGetRecordTransactionRequest.FranjaHora;
            objRegisterTransaction.MotivoID = objGetRecordTransactionRequest.MotivoID;

            objGetRecordTransactionRequest.txtNotText = objGetRecordTransactionRequest.txtNotText != null ? objGetRecordTransactionRequest.txtNotText.Replace('|', '-') : string.Empty;

            if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
            {
                if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                {
                    if (objGetRecordTransactionRequest.FranjaHora != null)
                    {
                        intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                        objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText + "|" + CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim() + "|";
                    }
                    else
                    {
            objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
                    }
                }
                else
            {
            objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
                }
            }
            else
                {
            objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
            }
                        
            objRegisterTransaction.CentroPobladoID = objGetRecordTransactionRequest.codCenPob;
            objRegisterTransaction.USRREGIS = objGetRecordTransactionRequest.CurrentUser;
            objRegisterTransaction.TransTipo = ConfigurationManager.AppSettings("gConstKeyLTETipoTranTI");
            
            string sMonto = string.Empty;
            sMonto = !string.IsNullOrEmpty(objGetRecordTransactionRequest.strIgv)
                     ? Functions.CheckStr(decimal.Round(Functions.CheckDecimal(objGetRecordTransactionRequest.Cargo) /
                       decimal.Round(CONSTANT.Constants.numeroUno + Functions.CheckDecimal(objGetRecordTransactionRequest.strIgv), 2)
                       , 2))  
                    : CONSTANT.Constants.PresentationLayer.NumeracionCERODECIMAL2;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN RecordSotTrasnsferInternal - LTE: Cargo: " + sMonto);
           
            objRegisterTransaction.Cargo = sMonto;
            objRegisterTransaction.CodReclamo=string.Empty;
            objRegisterTransaction.InterCasoID = objGetRecordTransactionRequest.InterCasoID;
            objRegisterTransaction.CodOCC = objGetRecordTransactionRequest.strCodOCC;
            
            try
            {
                objRequestGenerateSOT.objRegisterTransaction = objRegisterTransaction;
                objRequestGenerateSOT.audit= audit;

                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.RegisterTransactionLTEFixedResponse>(() => { return oFixedTransacService.LTERegisterTransaction(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("INTERNAL LTERegisterTransaction - LTE {0}", objResponseGenerateSOT.strResDes));


                boolFlajGenerarSOT = objResponseGenerateSOT.intResCod == CONSTANT.Constants.numeroUno?true:false;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT RecordSotTrasnsferInternal - LTE");
            return objResponseGenerateSOT;
        }
        public FixedTransacService.RegisterTransactionLTEFixedResponse GetRecordTransactionExternal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {

            FixedTransacService.RegisterTransactionLTEFixedRequest objRequestGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedRequest();
            FixedTransacService.RegisterTransactionLTEFixedResponse objResponseGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            FixedTransacService.RegisterTransaction objRegisterTransaction = new FixedTransacService.RegisterTransaction();

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("IN GetRecordTransactionExternal - LTE {0}", "Iniciando Proceso"));
            int intNumberCero = CONSTANT.Constants.numeroCero;
            objRegisterTransaction.USRREGIS = objGetRecordTransactionRequest.CurrentUser;
            objRegisterTransaction.ConID = objGetRecordTransactionRequest.ConID;
            objRegisterTransaction.CustomerID = objGetRecordTransactionRequest.CustomerID.Trim();
            objRegisterTransaction.TransTipo = ConfigurationManager.AppSettings("gConstKeyLTETipoTranTE");
            objRegisterTransaction.InterCasoID = objGetRecordTransactionRequest.InterCasoID;

            objRegisterTransaction.TipoVia = objGetRecordTransactionRequest.TipoVia;
            objRegisterTransaction.NomVia = objGetRecordTransactionRequest.NomVia;

            if (objGetRecordTransactionRequest.chkSN)
                objRegisterTransaction.NroVia = CONSTANT.Constants.numeroCero;
            else
                objRegisterTransaction.NroVia = Convert.ToInt(objGetRecordTransactionRequest.NroVia);

            objRegisterTransaction.TipoUrb = objGetRecordTransactionRequest.TipoUrb;
            objRegisterTransaction.NomUrb = objGetRecordTransactionRequest.NomUrb;
            objRegisterTransaction.NumMZ = objGetRecordTransactionRequest.hdnTipMzBloEdi;
            objRegisterTransaction.NumLote = objGetRecordTransactionRequest.NumLote;
            objRegisterTransaction.Ubigeo = objGetRecordTransactionRequest.Ubigeo.Trim();

            objRegisterTransaction.CentroPobladoID = objGetRecordTransactionRequest.codCenPob;
            objRegisterTransaction.EdificioID = objGetRecordTransactionRequest.EdificioID;
            objRegisterTransaction.Referencia = string.Format("{0} {1} {2}", objGetRecordTransactionRequest.ddlDepartment, objGetRecordTransactionRequest.hdnNumberDepartment, objGetRecordTransactionRequest.Referencia);
            objRegisterTransaction.EdificioID = objGetRecordTransactionRequest.EdificioID;
            objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;

            if (objGetRecordTransactionRequest.FranjaHora == null || objGetRecordTransactionRequest.FranjaHora == string.Empty)
            {
                objRegisterTransaction.FranjaHora = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
            objRegisterTransaction.FranjaHora = objGetRecordTransactionRequest.FranjaHora;
            }

            objRegisterTransaction.FranjaHoraID = objRegisterTransaction.FranjaHora;
            objRegisterTransaction.TrabajoID = objGetRecordTransactionRequest.agendaGetTipoTrabajo;

            objRegisterTransaction.ServicioID = ConfigurationManager.AppSettings("gConstLTETipoServicio");
            objRegisterTransaction.NumCarta = CONSTANT.Constants.strCero;
            if (objGetRecordTransactionRequest.agendaGetFecha == null || objGetRecordTransactionRequest.agendaGetFecha == string.Empty)
            {
                objRegisterTransaction.FechaProgramada = DateTime.Now.ToShortDateString();
            }
            else
            {
                objRegisterTransaction.FechaProgramada = objGetRecordTransactionRequest.agendaGetFecha;
            }
string sMonto = string.Empty;
            sMonto = !string.IsNullOrEmpty(objGetRecordTransactionRequest.strIgv) 
                     ? Functions.CheckStr(decimal.Round((Functions.CheckDecimal(objGetRecordTransactionRequest.Cargo) /
                                      decimal.Round(CONSTANT.Constants.numeroUno + Functions.CheckDecimal(objGetRecordTransactionRequest.strIgv), 2))
                                      , 2)) 
                : CONSTANT.Constants.PresentationLayer.NumeracionCERODECIMAL2;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN GetRecordTransactionExternal - LTE: Cargo: " + sMonto);
           
            objRegisterTransaction.Cargo = sMonto;
            objRegisterTransaction.MotivoID = objGetRecordTransactionRequest.MotivoID;
            objRegisterTransaction.CodOCC = objGetRecordTransactionRequest.strCodOCC;// 'SD-794552 - RPB

            objRegisterTransaction.Presuscrito = CONSTANT.Constants.strCero;
            objRegisterTransaction.Publicar = CONSTANT.Constants.strCero;

            objRegisterTransaction.CodOCC = objGetRecordTransactionRequest.strCodOCC;
            objRegisterTransaction.Ubigeo = objGetRecordTransactionRequest.Ubigeo;

            objGetRecordTransactionRequest.txtNotText = objGetRecordTransactionRequest.txtNotText != null ? objGetRecordTransactionRequest.txtNotText.Replace('|', '-') : string.Empty;
            
            if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
            {
                if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                {
                    if (objGetRecordTransactionRequest.FranjaHora != null)
            {
                intNumberCero = int.Parse(ConfigurationManager.AppSettings("strNumberInt"));
                objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText + "|" + CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, intNumberCero - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim();// +"|";
            }
            else
            {
                        objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
                    }
                }
                else
                {
                    objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
            }
            }
            else
            {
                objRegisterTransaction.Observacion = objGetRecordTransactionRequest.txtNotText;
            }
            

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("LTELogNroVia - {0}", objRegisterTransaction.NroVia));

            try
            {
                objRequestGenerateSOT.objRegisterTransaction = objRegisterTransaction;
                objRequestGenerateSOT.audit = audit;                
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.RegisterTransactionLTEFixedResponse>(() => { return oFixedTransacService.LTERegisterTransaction(objRequestGenerateSOT); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }

        public JsonResult GetRecordTransactionIntExt(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objHfcBETransfer)
        {
            #region  RECORD TRANSACTION

            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, "Transaccion: ", "IN GetRecordTransactionIntExt() - LTE");
            Dictionary<string, string> ResultGetRecordTransactionIntExt = new Dictionary<string, string>();
            ResultGetRecordTransactionIntExt = RecordTransactionIntExtBL(objHfcBETransfer);
            #endregion

            #region  OUT MESSAGES

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

            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, objHfcBETransfer.strIdSession, "OUT GetRecordTransactionIntExt() - LTE ");
            #endregion

            return Json(new { data = objFixedTransacServices });
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

        #endregion

        #region Record Interaction

        public Dictionary<string, string> RecordInteraction(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out MODEL.TemplateInteractionModel objTemplateInteractionModel)
        {
            Dictionary<string, string> ResposeInteraction;
            var rResult = string.Empty;
            var rMsgText = string.Empty;
            bool blnValidate = false;
            var NAME_PDF = string.Empty;

            MODEL.InteractionModel objInteractionModel;

            objInteractionModel = DatInteraction(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, objGetRecordTransactionRequest.CustomerID, objGetRecordTransactionRequest.txtNotText, objGetRecordTransactionRequest.CurrentUser, objGetRecordTransactionRequest.ConID, objGetRecordTransactionRequest.PlanoID);

            objTemplateInteractionModel = DatTemplateInteraction(objGetRecordTransactionRequest, out blnValidate);

            var UsuarioAplicacion = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var passAplicacion = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN RecordInteraction() - LTE");
            try
            {
                if (!blnValidate)
                {

                    rMsgText = string.Format("{0} {1}", Functions.GetValueFromConfigFile("gConstKeyCambInteracTitular", ConfigurationManager.AppSettings("strConstArchivoHFCPOSTConfigMsg")), Functions.GetValueFromConfigFile("strNoTransaccion", ConfigurationManager.AppSettings("strConstArchivoHFCPOSTConfigMsg")));
                    ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", CONSTANT.Constants.strCero},
                        {"rMsgTextInter", rMsgText},
                    };
                    return ResposeInteraction;
                }
                var DictResposeInteraction = InsertInteraction(objInteractionModel, objTemplateInteractionModel, objGetRecordTransactionRequest.Telephone
                                           , objGetRecordTransactionRequest.CurrentUser, UsuarioAplicacion
                                           , passAplicacion, false
                                       , objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.CustomerID);


                var strMsg = string.Empty;
                bool exitoInsercion = false;


                var listInteraction = new List<string>();
                DictResposeInteraction.ToList().ForEach(x =>
                {
                    listInteraction.Add(x.Value.ToString());
                });

                rResult = listInteraction[3];
                exitoInsercion = listInteraction[2].ToUpper() == CONSTANT.Constants.grstTrue.ToUpper() ? true : false;
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


                //if (exitoInsercion)
                //{
                //   // NAME_PDF = GetConstancyPDF(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest, rResult);
                //    if (NAME_PDF == string.Empty)
                //    {
                //       // rMsgText = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                //        ResposeInteraction = new Dictionary<string, string>
                //                {
                //                    {"hdnInterID", CONSTANT.Constants.strCero},
                //                    {"rMsgText", rMsgText},
                //                    {"strPath", NAME_PDF}
                //                };
                //    }


                //}
            }
            catch (Exception ex)
            {
                rMsgText = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy");
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
                throw new Exception(objGetRecordTransactionRequest.strtypetransaction);
            }

            ResposeInteraction = new Dictionary<string, string>
                    {
                        {"hdnInterID", rResult},
                        {"rMsgTextInter", rMsgText},
                         {"strPath", NAME_PDF}
                    };
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT RecordInteraction() - LTE");
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
                                                string strCustomerId)
        {
            string ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            string strTelefono;

            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;
            CommonServicesController oCommonServicesController = new CommonServicesController();

            string strFlgRegistrado = CONSTANT.Constants.strUno;
            FixedTransacService.CustomerResponse objCustomerResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CallDetailController", "InsertInteraction", "SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
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
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            var result = new Dictionary<string, string>();

            #region VALIDACION CONTIGENCIA

            if (ContingenciaClarify != CONSTANT.Constants.blcasosVariableSI)
            {
                result = oCommonServicesController.GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                result = oCommonServicesController.GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }

            var model = new List<string>();
            foreach (KeyValuePair<string, string> par in result)
            {
                model.Add(par.Value);
            }

            #endregion

            var rInteraccionId = model[0];

            var dictionaryResponse = new Dictionary<string, object>();
            if (rInteraccionId != string.Empty)
            {
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = oCommonServicesController.InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }
            dictionaryResponse.Add("rInteraccionId", rInteraccionId);

            return dictionaryResponse;
        }
        public MODEL.InteractionModel DatInteraction(string strIdsession, string typetransaction, string CUSTOMER_ID, string txtNot, string CurrentUser, string CONTRATO_ID, string CODIGO_PLANO_INST)
        {
            string gstrTransaccionTrasladoIntHFC = "TRANSACCION_DTH_TRASLADO_INTERNO_LTE";
            string gstrTransaccionTrasladoExtHFC = "TRANSACCION_DTH_TRASLADO_EXTERNO_LTE";
            string tipoTran = string.Empty;

            string strTelTipoHFC = ConfigurationManager.AppSettings("gConstTipoLTE");

            if (typetransaction.Equals(CONSTANT.Constants.strTres))
            {
                tipoTran = gstrTransaccionTrasladoExtHFC;
            }
            else
            {
                tipoTran = gstrTransaccionTrasladoIntHFC;
            }

            CommonServicesController OCommonServicesController = new CommonServicesController();
            MODEL.InteractionModel objInter = new MODEL.InteractionModel();
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
                objInter.ObjidContacto = OCommonServicesController.GetCustomerPhone(strIdsession, Convert.ToInt(CUSTOMER_ID)).ToString();
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
                Claro.Web.Logging.Error(strIdsession, typetransaction, ex.Message);

            }
            return objInter;
        }
        public MODEL.TemplateInteractionModel DatTemplateInteraction(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, out bool blnValidate)
        {
            string strIdSession = string.Empty;
            string strtypetransaction = string.Empty;
            string gstrTransaccionTrasladoIntHFC = "TRANSACCION_DTH_TRASLADO_INTERNO_LTE";
            string gstrTransaccionTrasladoExtHFC = "TRANSACCION_DTH_TRASLADO_EXTERNO_LTE";
            string tipoTran = string.Empty;

            if (objGetRecordTransactionRequest.strtypetransaction.Equals(CONSTANT.Constants.strTres))
            {
                tipoTran = gstrTransaccionTrasladoExtHFC;
            }
            else
            {
                tipoTran = gstrTransaccionTrasladoIntHFC;
            }

            MODEL.TemplateInteractionModel objPla = new MODEL.TemplateInteractionModel()
            {

                NOMBRE_TRANSACCION = tipoTran,
                X_CLARO_NUMBER = objGetRecordTransactionRequest.ConID,
                X_ADDRESS5 = objGetRecordTransactionRequest.nameCustomer,
                X_OLD_LAST_NAME = objGetRecordTransactionRequest.strtypeCustomer,
                X_CLARO_LDN1 = objGetRecordTransactionRequest.NumbDocRepreCustomer,
                X_CLARO_LDN2 = objGetRecordTransactionRequest.TypDocRepreCustomer,

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

                X_INTER_18 = objGetRecordTransactionRequest.CodPos, //Codigo Postal Actuales
                X_NAME_LEGAL_REP = objGetRecordTransactionRequest.hdnUbiAct,
                X_LOT_CODE = ConfigurationManager.AppSettings("strConsTraI"),
                X_MODEL = String.Format("{0} {1}", objGetRecordTransactionRequest.agendaGetFecha, objGetRecordTransactionRequest.agendaGetCodigoFranja),

                X_INTER_15 = objGetRecordTransactionRequest.DescripCADDAC,
                X_REASON = DateTime.Now.ToShortDateString(),
                X_EMAIL = objGetRecordTransactionRequest.Email,
                X_INTER_30 = objGetRecordTransactionRequest.txtNotText,
                X_FLAG_CHARGE = objGetRecordTransactionRequest.PlanoIDCustomer,
                X_ICCID = objGetRecordTransactionRequest.CustomerID.Trim()
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

                if (objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strTres)
                {
                    objPla.X_ADDRESS = GenerarDireccion(objGetRecordTransactionRequest);// GenerarDireccion(); RefNoteDirec
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
                        objPla.X_ZIPCODE = objGetRecordTransactionRequest.hdnCodPos.Trim(); //Codigo Postal Nuevo

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
                objPla.X_CLAROLOCAL6 = objGetRecordTransactionRequest.chkEmailChecked == true ? objGetRecordTransactionRequest.Email : string.Empty;
                objPla.X_TYPE_DOCUMENT = objGetRecordTransactionRequest.strtypeCliente;

                blnValidate = true;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strtypetransaction, ex.Message);
                blnValidate = false;
            }
            return objPla;
        }

        #endregion

        #region CONSTANCY PDF
        public string GetConstancyPDF(string strIdSession, HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string strInteraction)//(Areas.Transactions.Models.Postpaid.PostNewNumberModel oNewNumber, string strInteraction)
        {
            string NAME_PDF = string.Empty;

            Claro.Web.Logging.Info(strIdSession, "", "IN GetConstancyPDF- TRASLADO INTERNO/EXTERNO - LTE()");
            FixedTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            try
            {

                CommonTransacService.ParametersGeneratePDF oParameter = new CommonTransacService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strTrasladoInternoExternoFormatoTransac"),

                   

                    strPuntoDeAtencion = objGetRecordTransactionRequest.DescripCADDAC,
                    strNroDoc = objGetRecordTransactionRequest.NumbDocRepreCustomer,
                    strFechaTransaccion = DateTime.Today.ToShortDateString(),
                    StrFechaTransaccionProgram = DateTime.Today.ToShortDateString(),
                    strCasoInteraccion = strInteraction,
                    StrCasoInter = strInteraction,
                    strTransaccionDescripcion = objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro ? ConfigurationManager.AppSettings("strConsTraI") : ConfigurationManager.AppSettings("strConsTraE"),
                    StrCostoTransaccion = objGetRecordTransactionRequest.Cargo,
                    strDireccionClienteActual=objGetRecordTransactionRequest.AddressCustomer,
                    strRefTransaccionActual = objGetRecordTransactionRequest.RefAddressCustomer,
                    strDistritoClienteActual = objGetRecordTransactionRequest.DistCustomer,
                    strCodigoPostalActual = objGetRecordTransactionRequest.CodPos,
                    strPaisClienteActual = objGetRecordTransactionRequest.PAIS_LEGAL,
                    strProvClienteActual = objGetRecordTransactionRequest.ProvCustomer,

                    strDirClienteDestino = GenerarDireccion(objGetRecordTransactionRequest), 
                    strRefTransaccionDestino = objGetRecordTransactionRequest.Referencia,
                    strDepClienteDestino = objGetRecordTransactionRequest.hdnDepDes,
                    strDistClienteDestino = objGetRecordTransactionRequest.hdnDisDes,
                    strAplicaCambioDirFact = objGetRecordTransactionRequest.chkUseChangeBillingChecked ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    strCodigoPostallDestino = objGetRecordTransactionRequest.hdnCodPos,
                    strPaisClienteDestino = objGetRecordTransactionRequest.CountryCustomer,
                    strProvClienteDestino = objGetRecordTransactionRequest.hdnProDes,

                    strEnviomail = objGetRecordTransactionRequest.chkEmailChecked? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    strCorreoCliente = objGetRecordTransactionRequest.chkEmailChecked?objGetRecordTransactionRequest.Email:string.Empty,

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
                    StrEmail = objGetRecordTransactionRequest.chkEmailChecked ? objGetRecordTransactionRequest.Email : string.Empty,
                    strNroSot = objGetRecordTransactionRequest.strNroSOT,
                    StrContenidoComercial2 = Functions.GetValueFromConfigFile("ExtIntTrasnferContentCommercial2",
                        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")),



                    strflagTipoTraslado = objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro ? CONSTANT.Constants.strCero : CONSTANT.Constants.strUno,
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTrasladoInternoExternoLTE")
                };

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: ", "OUT GetConstancyPDF- TRASLADO INTERNO/EXTERNO - LTE()  NAME_PDF:   " + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }
      
        #endregion

        #region EMAIL
        public string GetSendEmail(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer oExternalInternalTransfer, string strAdjunto, byte[] attachFile)
        {
            string strTemplateEmail = TemplateEmail(oExternalInternalTransfer);
            string strTIEMailAsunto = oExternalInternalTransfer.strtypetransaction == CONSTANT.Constants.strCuatro ? ConfigurationManager.AppSettings("strTrasladoInternoMailAsunto") : ConfigurationManager.AppSettings("strTrasladoExternoMailAsunto");

            string strDestinatarios = oExternalInternalTransfer.Email;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oExternalInternalTransfer.strIdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = strRemitente,
                    strTo = strDestinatarios,
                    strMessage = strTemplateEmail,
                    strAttached = strAdjunto,
                    strSubject = strTIEMailAsunto,
                     AttachedByte = attachFile
                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oCommonTransacService.GetSendEmailFixed(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oExternalInternalTransfer.strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }

            string strResul = string.Empty;

            if (objGetSendEmailResponse.Exit == CONSTANT.Constants.CriterioMensajeOK)
            {
                strResul = Functions.GetValueFromConfigFile( "strMensajeEnvioOK",ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            else
            {
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
            strmessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";
            return strmessage;

        }

        #endregion

        #region RECORD AUDIT

        public void InsertAudit(string strTransacionID, string strTransacion, HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransaction)
        {

            string strServicio = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            string strIPCliente = System.Web.HttpContext.Current.Request.UserHostAddress;
            string strNomCli = objGetRecordTransaction.nameCustomer;
            string strCueUsu = objGetRecordTransaction.CurrentUser;
            string strTel = objGetRecordTransaction.CustomerID;
            int strMonto = CONSTANT.Constants.numeroCero;
            string strTexto = string.Format("/Ip Cliente: {0}/Usuario: {1}/Opcion: {2}/Fecha y Hora: {3}", strIPCliente, strCueUsu, strTransacion, DateTime.Now.ToString());

            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objGetRecordTransaction.strIdSession);
            CommonTransacService.SaveAuditRequestCommon objRegAuditRequest =
                new CommonTransacService.SaveAuditRequestCommon()
                {
                    audit = AuditRequest,
                    vTransaccion = strTransacionID,
                    vServicio = strServicio,
                    vIpCliente = strIPCliente,
                    vNombreCliente = strNomCli,
                    vIpServidor = AuditRequest.ipAddress,
                    vNombreServidor = AuditRequest.applicationName,
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
                Claro.Web.Logging.Error(objGetRecordTransaction.strIdSession, AuditRequest.transaction, ex.Message);

            }
        }


        #endregion

        #region  REGISTRO OCC - UPDATE POSTAL ADDRESS

        public void GrabarCambioDireccionPostal(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string lnkNumSot)
        {
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "IN GrabarCambioDireccionPostal()");
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "chkUseChangeBillingChecked: " + objGetRecordTransactionRequest.chkUseChangeBillingChecked.ToString());
            if (objGetRecordTransactionRequest.chkUseChangeBillingChecked)
            {
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
                FixedTransacService.AuditRequest auditFixed = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);

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
                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, auditFixed.transaction, ex.Message);
                }
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, " ", "GetInsertLoyalty() - HFC _ salida: " + salida.ToString());
                #endregion
                if (salida)
                {
                    #region UpdateDataPostalAddress
                    bool boolUpdPostal = false;
                    boolUpdPostal = UpdateDataPostalAddress(objGetRecordTransactionRequest);
                    if (boolUpdPostal)
                    {
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, " ", boolUpdPostal.ToString());
                    }
                    #endregion
                }
                else
                {
                    string lblMenError;
                    lblMenError = Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                }

            }
            else
            {
                #region InsertarTrasladoDireccion
                bool salida = false;
                int FlagDirecFact = objGetRecordTransactionRequest.chkUseChangeBillingChecked == true ? CONSTANT.Constants.numeroUno : CONSTANT.Constants.numeroCero;
                FixedTransacService.Customer oCustomer = new FixedTransacService.Customer();
                oCustomer.CustomerID = objGetRecordTransactionRequest.CustomerID;

                FixedTransacService.InsertLoyaltyResponse objInsertLoyaltyResponse = null;
                FixedTransacService.AuditRequest auditFixed = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
                FixedTransacService.InsertLoyaltyRequest objInsertLoyaltyRequest = new FixedTransacService.InsertLoyaltyRequest()
                {
                    audit = auditFixed,
                    oCustomer = oCustomer,
                    vCodSoLot = lnkNumSot,
                    vFlagDirecFact = FlagDirecFact,
                    vUser = objGetRecordTransactionRequest.CurrentUser,
                    vFechaReg = DateTime.Now
                };

                try
                {
                    objInsertLoyaltyResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertLoyaltyResponse>(() =>
                    {
                        return oFixedTransacService.GetInsertLoyalty(objInsertLoyaltyRequest);
                    });

                    salida = objInsertLoyaltyResponse.rSalida;
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "GetInsertLoyalty() - HFC _ salida: " + salida.ToString());
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objInsertLoyaltyRequest.audit.transaction, ex.Message);
                }
                #endregion
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "OUT GrabarCambioDireccionPostal() - HFC");
        }

        public void GrabarRegistroOCC(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest, string lnkNumSot)
        {
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "IN GrabarRegistroOCC()");
            bool salida = false;
            string sMonto = string.Empty;

            if (objGetRecordTransactionRequest.strIgv != null)
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

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
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

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "GetSaveOCC() - HFC _ salida:  " + salida.ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "OUT GrabarRegistroOCC() - HFC");
        }

        public bool UpdateDataPostalAddress(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {
            bool blnResult = false;
            string strGenerarDireccion = GenerarDireccion(objGetRecordTransactionRequest);
            string strReferencia = GenerarNotasDireccion(objGetRecordTransactionRequest);
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN UpdateDataPostalAddress - LTE ");
            FixedTransacService.AddressUpdateFixedResponse objAddressUpdateResponse = null;

            FixedTransacService.AddressUpdateFixedRequest objAddressUpdateRequest = new FixedTransacService.AddressUpdateFixedRequest()
            {
                audit = audit,                
                strIdCustomer = objGetRecordTransactionRequest.CustomerID,
                strDomicile = strGenerarDireccion,
                strReference = strReferencia,
                strDistrict = objGetRecordTransactionRequest.hdnDisDes,
                strProvince = objGetRecordTransactionRequest.hdnProDes,
                strCodPostal =objGetRecordTransactionRequest.CodPos,
                StrDepartament = objGetRecordTransactionRequest.hdnDepDes,
                strCountryLegal = objGetRecordTransactionRequest.DescrpCountry
            };
            try { 
                 objAddressUpdateResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.AddressUpdateFixedResponse>(() =>
                {
                    return oFixedTransacService.GetUpdateAddress(objAddressUpdateRequest);
                });
                 blnResult = objAddressUpdateResponse.blnResult;
                 Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "GetUpdateAddress - LTE _ blnResult:" + blnResult.ToString());
            }
            catch(Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT UpdateDataPostalAddress - LTE ");

            return blnResult;
        }

        public string GenerarDireccion(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN GenerarDireccion() - LTE ");
            string strBldDireccion = string.Empty;
            var BldDireccion = new System.Text.StringBuilder();
            try
            {

                if (objGetRecordTransactionRequest.hdnTipoVia != CONSTANT.Constants.PresentationLayer.CODIGO_SIN_NOMBRE)
                    BldDireccion.AppendFormat("{0} {1}", objGetRecordTransactionRequest.TipoVia.Substring(0, 2), objGetRecordTransactionRequest.NomVia != null ? objGetRecordTransactionRequest.NomVia : string.Empty);
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
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, ex.Message);

            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "strBldDireccion: " + strBldDireccion);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT GenerarDireccion() - LTE ");
            return strBldDireccion;
        }

        public string GenerarNotasDireccion(HELPERS.HFC.ExternalInternalTransfer.ExtIntTransfer objGetRecordTransactionRequest)
        {

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "IN GenerarNotasDireccion() - LTE ");
            string strBldNoteDireccion = string.Empty;
            var BldNoteDireccion = new System.Text.StringBuilder();
            try
            {
                if (objGetRecordTransactionRequest.TipoUrb != null)
                    BldNoteDireccion.AppendFormat("{0} {1}", objGetRecordTransactionRequest.hdnTipoUrb != null ? objGetRecordTransactionRequest.hdnTipoUrb.Substring(0, 2) : string.Empty, objGetRecordTransactionRequest.NomUrb != null ? objGetRecordTransactionRequest.NomUrb : string.Empty);

                strBldNoteDireccion = string.Format("{0} {1}", BldNoteDireccion.ToString(), objGetRecordTransactionRequest.Referencia != null ? objGetRecordTransactionRequest.Referencia : string.Empty);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, ex.Message);
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "strBldNoteDireccion: " + strBldNoteDireccion);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "OUT GenerarDireccion() - LTE ");

            return strBldNoteDireccion;
        }

        #endregion

        #region  LOAD - SELECT  - CONFIGURATION

        public JsonResult InitGetMessageConfiguration(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            
            Claro.Web.Logging.Info(strIdSession, strIdSession, "InitGetMessageConfiguration() - LTE - Inicio");

            try
            {
            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelDep"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelPro"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADIMensajeSelDis"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstMsgNSFranjaHor"));
            lstMessage.Add(ConfigurationManager.AppSettings("strAlertaEstaSegGuarCam"));
            lstMessage.Add(ConfigurationManager.AppSettings("strADISeCodPlano"));
            lstMessage.Add(CONSTANT.Constants.strCuatro); //gConstKeyTransTrasladoInternoDTH
                lstMessage.Add(CONSTANT.Constants.strTres); //gConstKeyTransTrasladoExternoDTH
            lstMessage.Add(CONSTANT.Constants.strCuatro);
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgTranGrabSatis"));
            lstMessage.Add(ConfigurationManager.AppSettings("hdnMessageSendMail"));
            lstMessage.Add(ConfigurationManager.AppSettings("ddlSOTTIELTE"));
            lstMessage.Add(CONSTANT.Constants.CriterioMensajeOK);
            lstMessage.Add(ConfigurationManager.AppSettings("gStrMsjConsultaCapacidadNoDisp"));
                lstMessage.Add(ConfigurationManager.AppSettings("gConstTipTraTILTE"));
                lstMessage.Add(ConfigurationManager.AppSettings("gConstTipTraTELTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgNoHayEPECP"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeEmail"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstOpcTIEHabFidelizarLTE"));
            lstMessage.Add(CONSTANT.Constants.strCero);
            lstMessage.Add(ConfigurationManager.AppSettings("cargoOCCTE1PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("cargoOCCTE2PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("cargoOCCTE3PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("cargoOCCTILTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActivaRadioTrasladoInternoLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActivaRadioTrasladoExternoLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strOpcActCheckDirFactTrasladosLTE"));
            //ObtenerCodigoOCC -cod
            lstMessage.Add(ConfigurationManager.AppSettings("codOCCTIPLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("codOCCTE1PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("codOCCTE2PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("codOCCTE3PLTE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"));
                lstMessage.Add(ConfigurationManager.AppSettings("strMessageETAValidation")); //ljacobo
                lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" +
                               DateTime.Now.Day.ToString("00")); //ljacobo
                lstMessage.Add(ConfigurationManager.AppSettings("srtCodOpcionTrasladoInternoLTE"));
                lstMessage.Add(ConfigurationManager.AppSettings("srtCodOpcionTrasladoExternoLTE"));
                lstMessage.Add(ConfigurationManager.AppSettings("CodTipServLteTI"));
                lstMessage.Add(ConfigurationManager.AppSettings("CodTipServLteTE"));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, ex.InnerException.Message);
            }
            //strAjusteNoRecon   -- No se reconoce la tipificación de esta transacción.

            var msg = string.Format("{0} : {1}", "lstMessage", lstMessage.Count.ToString());
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);


            return Json(lstMessage, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetListDataProducts(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            var msg = string.Format("InGetListDataProducts - LTE () Customer: {0} - contrato : {1}",  "", CUSTOMER_ID, CONTRATO_ID);
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            MODEL.HFC.ExternalInternalTransferModel objFixedTransacServices = null;
            FixedTransacService.ServicesLteFixedResponse objProdDecoResponseCommon = null;//
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ServicesLteFixedRequest objProDecoRequestCommon = new FixedTransacService.ServicesLteFixedRequest() //
            {
                audit = audit,
                strCoid = CONTRATO_ID,
                strCustomerId = CUSTOMER_ID
            };

            try
            {

                objProdDecoResponseCommon = Claro.Web.Logging
                    .ExecuteMethod<FixedTransacService.ServicesLteFixedResponse>(
                    () =>
                    {
                            return oFixedTransacService.GetServicesLte(objProDecoRequestCommon);
                    });


             if (objProdDecoResponseCommon.ListServicesLte.Count > 0)
            {
            objFixedTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                    List<HELPERS.HFC.ExternalInternalTransfer.DataProducts> ListDataProducts =
                        new List<HELPERS.HFC.ExternalInternalTransfer.DataProducts>();

            foreach (FixedTransacService.BEDeco item in objProdDecoResponseCommon.ListServicesLte)
                {
            ListDataProducts.Add(new HELPERS.HFC.ExternalInternalTransfer.DataProducts()
            {
                        ServiceType = item.tipo_servicio,
                        MaterialCode = item.codigo_material,
                        SapCode = item.codigo_sap,
                        SerieNumber = item.numero_serie,
                        AdressMac = item.macadress,
                        MaterialDescripcion = item.descripcion_material,
                        EquipmentType = item.tipo_equipo,
                        ProductId = item.id_producto,
                        Type = item.tipo_equipo,
                            ConvertType = item.macadress,
                        PricipalService = item.servicio_principal,
                        Headend = item.headend,
                        EphomeexChange = item.ephomeexchange
            });
                }
            objFixedTransacServices.ListDataProducts = ListDataProducts;

                    msg = string.Format("{0} : {1}", "ListDataProducts - LTE:  ",
                        objFixedTransacServices.ListDataProducts.Count.ToString());
                 Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProDecoRequestCommon.audit.transaction,
                    ex.InnerException.Message);
            }
            msg = string.Format("{0}", "OuGetListDataProducts - LTE ()");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);


            return Json(new { data = objFixedTransacServices });

        }

        public JsonResult GetStateType(string strIdSession, string idList)
        {
            var msg = string.Format("In GetStateType: {0}  - LTE", idList);
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem> GetDocumentType = null;

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            try
            {

            GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
            {
                return oCommonTransacService.GetDocumentTypeCOBS(strIdSession, audit.transaction, idList);
            });


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
            msg = string.Format("ListGeneric: {0} ", objCommonTransacServices.ListGeneric.Count.ToString());
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.InnerException.Message);
            }
            msg = string.Format("Out GetStateType: {0}  - LTE", idList);
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            return Json(new { data = objCommonTransacServices });
        }

        public JsonResult GetDepartments(string strIdSession)
        {
            var msg = string.Format("In GetDepartments - LTE ");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            CommonTransacService.DepartmentsPvuResponseCommon objDepartmentsResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.DepartmentsPvuRequestCommon objDepartmentsRequestCommon = new CommonTransacService.DepartmentsPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objDepartmentsResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.DepartmentsPvuResponseCommon>(() => { return oCommonTransacService.GetDepartmentsPVU(objDepartmentsRequestCommon); });
            
            if (objDepartmentsResponseCommon != null && objDepartmentsResponseCommon != null)
            {
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

                msg = string.Format("ListGeneric: {0} ", objCommonTransacServices.ListGeneric);
                Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDepartmentsRequestCommon.audit.transaction, ex.Message);
            }

            msg = string.Format("Out GetDepartments - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            return Json(new { data = objCommonTransacServices });
        }

        public JsonResult GetProvinces(string strIdSession, string strDepartments)
        {

            var msg = string.Format("In GetProvinces - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            CommonTransacService.ProvincesPvuResponseCommon objProvincesResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.ProvincesPvuRequestCommon objProvincesRequestCommon = new CommonTransacService.ProvincesPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objProvincesRequestCommon.CodDep = strDepartments;
                objProvincesResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ProvincesPvuResponseCommon>(() => { return oCommonTransacService.GetProvincesPVU(objProvincesRequestCommon); });
 
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
                msg = string.Format("ListGeneric: {0} ", objCommonTransacServices.ListGeneric);
                Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProvincesRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            msg = string.Format("In GetProvinces - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            return Json(new { data = objCommonTransacServices });
        }

        public JsonResult GetDistricts(string strIdSession, string strDepartments, string strProvinces)
        {
            CommonTransacService.DistrictsPvuResponseCommon objDistrictsResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objDistrictsRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
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
        public JsonResult GetZoneTypes(string strIdSession)
        {
            CommonTransacService.ZoneTypeCobsResponseCommon objZoneTypeResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objZoneTypeCobsRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
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

        public JsonResult GetMzBloEdiType(string strIdSession)
        {
            CommonTransacService.MzBloEdiTypeResponseCommon objMzBloEdiTypeResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objMzBloEdiTypeRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
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

        public JsonResult GetTipDptInt(string strIdSession)
        {
            var msg = string.Format("In GetTipDptInt - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg); 

            CommonTransacService.TipDptIntResponseCommon objTipDptIntResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objTipDptIntRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
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
            msg = string.Format("In GetTipDptInt - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            return Json(new { data = objCommonTransacServices });
        }


        //ObtenerTipoTrabajoLTE
        public JsonResult GetWorkType(string strIdSession, string strTransacType)
        {
            var msg = string.Format("In GetWorkType - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            FixedTransacService.JobTypesResponseHfc objWorkTypeResponseCommon = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.JobTypesRequestHfc objWorkTypeRequestCommon = new FixedTransacService.JobTypesRequestHfc()
            {
                audit = audit

            };

            try
            {
                objWorkTypeRequestCommon.p_tipo = Convert.ToInt(strTransacType);
                objWorkTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() => { return oFixedTransacService.GetJobTypeLte(objWorkTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objWorkTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objWorkTypeResponseCommon != null && objWorkTypeResponseCommon.JobTypes != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listWorkTypes = new List<HELPERS.CommonServices.GenericItem>();

                foreach (FixedTransacService.JobType item in objWorkTypeResponseCommon.JobTypes)
                {
                    listWorkTypes.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.tiptra,
                        Description = item.descripcion
                    });
                }
                objCommonTransacServices.ListGeneric = listWorkTypes;
            }
            msg = string.Format("Out GetWorkType - LTE");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);

            return Json(new { data = objCommonTransacServices });
        }

        //ObtenerTipoSubTrabajoLTE
        public JsonResult GetWorkSubType(string strIdSession, string strCodTypeWork, string strContractID)
        {
            List<HELPERS.CommonServices.GenericItem> objListaEta = new List<HELPERS.CommonServices.GenericItem>();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;
            FixedTransacService.OrderSubTypesResponseHfc objResponse = new FixedTransacService.OrderSubTypesResponseHfc();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            MODEL.HFC.ExternalInternalTransferModel objFixedGetSubOrderType = null;
            try
            {

                FixedTransacService.AuditRequest auditreq = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

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
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);

            }
            return Json(new { data = objFixedGetSubOrderType, typeValidate = objResponseValidate });
        }

        //ObtenerMotivosSotLTE
        public JsonResult GetMotiveSot(string strIdSession)
        {

            FixedTransacService.MotiveSoftLTEFixedResponse objMotiveSotResponseCommon;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.MotiveSoftLTEFixedRequest objMotiveSotRequestCommon =
                new FixedTransacService.MotiveSoftLTEFixedRequest()
                {
                    audit = audit
                };

            try
            {
                objMotiveSotResponseCommon =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.MotiveSoftLTEFixedResponse>(() =>
                    {
                        return oFixedTransacService.GetMotiveSoftLte(objMotiveSotRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonServices = null;
            if (objMotiveSotResponseCommon != null )
            {
                objCommonServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listMotive =
                    new List<HELPERS.CommonServices.GenericItem>();

                foreach (FixedTransacService.GenericItem item in objMotiveSotResponseCommon.listMotiveSoft)
                {
                    listMotive.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion
                    });
                }
                objCommonServices.ListGeneric = listMotive;
            }

            return Json(new { data = objCommonServices });



        }


        public JsonResult GetUbigeoID(string strIdSession, string vstrDisID, string vstrDepID, string vstrProvID)
        {
            
            FixedTransacService.IdUbigeoFixedResponse objreponse = new FixedTransacService.IdUbigeoFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
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
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.InnerException.Message);
            }


            HELPERS.CommonServices.GenericItem UbigeoID = new HELPERS.CommonServices.GenericItem()
            {
                Code = objreponse.strIdUbigeo
            };
            //obRequest.Command = BaseDatos.NOMBRE_SISACT_PKG_SOLICITUD + ".SECSS_CON_DISTRITO";
            return Json(new { data = UbigeoID.Code });
        }

        public JsonResult ObtenerCodigoPostal(string strIdSession, string vstrDisID)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            var msg = string.Format("In ObtenerCodigoPostal - LTE - vstrDisID : {0}", vstrDisID);
            Claro.Web.Logging.Info(strIdSession, audit.transaction, msg);


            string strcodePostal = GetPostalCode(strIdSession, audit.transaction, vstrDisID);

            msg = string.Format("Out ObtenerCodigoPostal - LTE - strcodePostal : {0}", strcodePostal);
            Claro.Web.Logging.Info(strIdSession, audit.transaction, msg);
            return Json(new {data = strcodePostal});

        }

        public JsonResult GetListCenterPob(string strIdSession, string strUbigeo)
        {
            FixedTransacService.ListTownCenterFixedResponse objListTownCenterResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
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

        //funcion obtener coodigo postal
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
        public JsonResult GetListEdificios(string strIdSession, string vstrCodPlano)
        {
            //obRequest.Command = BaseDatos.NOMBRE_PKG_GERHART_DTH + ".MANTSS_LISTA_EDIFICIOHFC";
            FixedTransacService.ListEbuildingsFixedResponse objresponse = new FixedTransacService.ListEbuildingsFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ListEbuildingsFixedRequest objrequest = new FixedTransacService.ListEbuildingsFixedRequest()
            {

                audit = audit,
                strCodePlan = vstrCodPlano
            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ListEbuildingsFixedResponse>(() => { return oFixedTransacService.GetListEBuildings(objrequest); });
            }
            catch (Exception)
            {

                throw;
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


            //List<HELPERS.CommonServices.GenericItem> DataListPlanos = new List<HELPERS.CommonServices.GenericItem>();

            //DataListPlanos.Add(new HELPERS.CommonServices.GenericItem()
            //{
            //    Code ="1",             
            //    Code2 = "Lima",
            //    Code3 = "Lima",
            //    Date = "20171010",
            //    Number = "Av. San Diego",
            //    Description = "10",
            //    Description2= "tipo. 20"
            //});

            return Json(new { data = objfixedEbuildings });

        }
        public JsonResult GetListPlanos(string strIdSession, string vCodUbigeo)
        {
            //obRequest.Command = BaseDatos.NOMBRE_PQ_INT_SISACT_CONSULTA + ".p_consulta_centro_poblado";
            FixedTransacService.ListPlansFixedResponse objresponse = new FixedTransacService.ListPlansFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ListPlansFixedRequest objrequest = new FixedTransacService.ListPlansFixedRequest()
            {
                audit = audit,
                strIdUbigeo = vCodUbigeo

            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ListPlansFixedResponse>(() => { return oFixedTransacService.GetListPlans(objrequest); });
            }
            catch (Exception)
            {

                throw;
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

        public JsonResult GetCobertura(string strIdSession, string valNoteCenterPopulated)
        {
            FixedTransacService.CoverageFixedResponse objresponse = new FixedTransacService.CoverageFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.CoverageFixedRequest objrequest = new FixedTransacService.CoverageFixedRequest()
            {
                audit = audit,
                strCob = valNoteCenterPopulated
            };
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.CoverageFixedResponse>(() => { return oFixedTransacService.GetCoverage(objrequest); });
            }
            catch (Exception)
            {

                throw;
            }
            // obRequest.Command = BaseDatos.NOMBRE_PACKAGE_PQ_INTEGRACION_DTH + ".p_cobertura";


            HELPERS.CommonServices.GenericItem Cobertura = new HELPERS.CommonServices.GenericItem()
            {
                Code = objresponse.strCoverage
            };

            return Json(new { data = Cobertura });

        }


        #endregion
        
    }
	
}