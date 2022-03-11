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
    public class ChangeEquipmentController : Controller
    {
        private readonly FixedTransacService.FixedTransacServiceClient oFixedTransacService = new FixedTransacService.FixedTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();

        public ActionResult HFCChangeEquipment()
        {
            Claro.Web.Logging.Configure();
            int number = Convert.ToInt(ConfigurationManager.AppSettings("strIncrementDays", "0"));
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
            return View("~/Areas/Transactions/Views/ChangeEquipment/HFCChangeEquipment.cshtml");
        }

        #region  LOAD - SELECT  - CONFIGURATION
        public JsonResult InitGetMessageConfiguration(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();


            //lstMessage.Add(ConfigurationManager.AppSettings("gConstMsgNSFranjaHor"));

            lstMessage.Add(ConfigurationManager.AppSettings("strHFCTipSvrCE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgTranGrabSatis"));

            lstMessage.Add(ConfigurationManager.AppSettings("hdnMessageSendMail"));
            lstMessage.Add(CONSTANT.Constants.CriterioMensajeOK);
            lstMessage.Add(ConfigurationManager.AppSettings("gStrMsjConsultaCapacidadNoDisp"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstTipTraCE"));

            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeEmail"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstOpcTIEHabFidelizar"));
            lstMessage.Add(CONSTANT.Constants.strCero);
            lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00"));

            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMessageETAValidation"));
            lstMessage.Add(ConfigurationManager.AppSettings("strHFCTipifCE"));
            lstMessage.Add(ConfigurationManager.AppSettings("strAlertaEstaSegGuarCam"));

            //RONALDRR - CAMBIO DE EQUIPO POR TECNOLOGIA - INI
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeNoAplicaFTTH")); 
            lstMessage.Add(ConfigurationManager.AppSettings("strPlanoFTTH"));
            //RONALDRR - CAMBIO DE EQUIPO POR TECNOLOGIA - FIN

            return Json(lstMessage, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetListDataProducts(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            FixedTransacService.BEDecoServicesResponseFixed objProdDecoResponseCommon = null;//
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objProDecoRequestCommon.audit.transaction, ex.Message);

            }

            MODEL.HFC.ChangeEquipmentModel objFixedTransacServices = null;
            if (objProdDecoResponseCommon.ListDecoServices.Count > 0)
            {
                objFixedTransacServices = new MODEL.HFC.ChangeEquipmentModel();
                List<HELPERS.HFC.ChangeEquipment.DataProducts> ListDataProducts = new List<HELPERS.HFC.ChangeEquipment.DataProducts>();

                foreach (FixedTransacService.BEDeco item in objProdDecoResponseCommon.ListDecoServices)
                {
                    ListDataProducts.Add(new HELPERS.HFC.ChangeEquipment.DataProducts()
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
        public JsonResult GetWorkSubType(string strIdSession, string strCodTypeWork, string strContractID)
        {
            List<HELPERS.CommonServices.GenericItem> objListaEta = new List<HELPERS.CommonServices.GenericItem>();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;
            FixedTransacService.OrderSubTypesResponseHfc objResponse = new FixedTransacService.OrderSubTypesResponseHfc();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            MODEL.HFC.ChangeEquipmentModel objFixedGetSubOrderType = null;
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
                    objFixedGetSubOrderType = new MODEL.HFC.ChangeEquipmentModel();

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
        public JsonResult GetWorkType(string strIdSession, string strTransacType)
        {
            CommonTransacService.WorkTypeResponseCommon objWorkTypeResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                Claro.Web.Logging.Error(strIdSession, objWorkTypeRequestCommon.audit.transaction, ex.Message);
            }

            MODEL.HFC.ChangeEquipmentModel objCommonTransacServices = null;
            if (objWorkTypeResponseCommon != null && objWorkTypeResponseCommon.WorkTypes != null)
            {
                objCommonTransacServices = new MODEL.HFC.ChangeEquipmentModel();
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
        public JsonResult GetMotiveSOTByTypeJob(string strIdSession, int IdTipoTrabajo)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objResponse = new FixedTransacService.MotiveSOTByTypeJobResponse();
            var objRequest = new FixedTransacService.MotiveSOTByTypeJobRequest()
            {
                audit = audit,
                tipTra = IdTipoTrabajo
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetMotiveSOTByTypeJob");
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.MotiveSOTByTypeJobResponse>(() =>
                {
                    return oFixedTransacService.GetMotiveSOTByTypeJob(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Total Reg : " + objResponse.List.Count);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Reg : " + Newtonsoft.Json.JsonConvert.SerializeObject(objResponse.List));
            return Json(new { data = objResponse.List });


        }
        #endregion

        #region Record Transaction - RecordTransactionIntExtBL
        public JsonResult GetRecordTransaction(HELPERS.HFC.ChangeEquipment.ChangeEquip objHfcBETransfer)
        {
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, "", "IN GetRecordTransaction() - HFC");
            Dictionary<string, string> ResultGetRecord = new Dictionary<string, string>();

            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, "", "IN RecordTransaction() - HFC");
            ResultGetRecord = RecordTransaction(objHfcBETransfer);
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, "", "OUT RecordTransaction() - HFC");

            MODEL.HFC.ChangeEquipmentModel objFixedTransacServices = null;
            objFixedTransacServices = new MODEL.HFC.ChangeEquipmentModel();
            HELPERS.CommonServices.GenericItem ItemGenMessag = new HELPERS.CommonServices.GenericItem();

            ItemGenMessag.Code = ResultGetRecord["rResult"];
            ItemGenMessag.Description = ResultGetRecord["rMsgText"];
            ItemGenMessag.Code2 = ResultGetRecord["hdnInterID"];
            ItemGenMessag.Description2 = ResultGetRecord["rMsgTextInter"];
            ItemGenMessag.Number = ResultGetRecord["lnkNumSot"];
            ItemGenMessag.Code3 = ResultGetRecord["strPath"];

            objFixedTransacServices.ItemGeneric = ItemGenMessag;
            Claro.Web.Logging.Info(objHfcBETransfer.strIdSession, "", "OUT GetRecordTransaction() - HFC  ItemGenMessag.Number: " + ItemGenMessag.Number != null ? ItemGenMessag.Number : "NO # SOT");
            return Json(new { data = objFixedTransacServices });
        }
        public Dictionary<string, string> RecordTransaction(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest)
        {
            FixedTransacService.RecordTranferExtIntResponseFixed objGetRecordTransactionResponse = new FixedTransacService.RecordTranferExtIntResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            Dictionary<string, string> objResponse = new Dictionary<string, string>();
            Dictionary<string, string> objResponseInteraction = new Dictionary<string, string>();
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "IN RecordTransaction()");
            bool blnResponse = false;
            string lnkNumSot = string.Empty;
            string strMessage = string.Empty;
            try
            {
                MODEL.TemplateInteractionModel objPla = new MODEL.TemplateInteractionModel();
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "IN RecordTransaction() - RecordInteraction()");
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
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + audit.transaction, objResponse["hdnInterID"]);

                objGetRecordTransactionRequest.InterCasoID = objResponseInteraction["hdnInterID"];

                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + audit.transaction, "RecordTransfer");
                blnResponse = RecordTransfer(objGetRecordTransactionRequest, out lnkNumSot, out strMessage);

                if (string.IsNullOrEmpty(lnkNumSot))
                {
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "RETURN RecordTransfer", "Erro No se genero");
                    throw new System.ArgumentException("No se genero la SOT");
                }

                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + audit.transaction, "RETURN RecordTransfer:    " + blnResponse.ToString());
                
                if (blnResponse)
                {
                    #region REGISTER CONSTANCY
                    if (objGetRecordTransactionRequest.InterCasoID != null || objGetRecordTransactionRequest.InterCasoID == string.Empty)
                    {
                        objGetRecordTransactionRequest.strNroSOT = lnkNumSot;
                       // objResponse["strPath"] = GetConstancyPDF(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest, objResponse["hdnInterID"]);//CONSTANCY               
                    }

                   // GrabarCambioDireccionPostal(objGetRecordTransactionRequest, lnkNumSot);
                    GrabarRegistroOCC(objGetRecordTransactionRequest, lnkNumSot);

                    Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: " + audit.transaction, "RecordTransaction" + objGetRecordTransactionRequest.strtypetransaction);

                    string strTransacionTI = ConfigurationManager.AppSettings("strConsTraCE");
                    InsertAudit(ConfigurationManager.AppSettings("gConstKeyCambioEquipoDTH"), strTransacionTI, objGetRecordTransactionRequest);


                    string rutaConstancy = "";//objResponse["strPath"];

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
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "IN GetUpdatexInter30()");
                    CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
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

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "OUT GetUpdatexInter30()");
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

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "lnkNumSot: " + lnkNumSot == null ? string.Empty : lnkNumSot);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "Description: " + objGetRecordTransactionResponse.DescMessaTransfer == null ? string.Empty : objGetRecordTransactionResponse.DescMessaTransfer);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "OUT RecordTransactionIntExtBL()");
            return objResponse;
        }
        public bool RecordTransfer(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest, out string lnkNumSot, out string strMessage)
        {

            string strError = string.Empty;
            string strResDes = string.Empty;
            int intResCod = CONSTANT.Constants.numeroCero;
            bool blnRes = false;
            lnkNumSot = string.Empty;
            string hdnCodSotValue = string.Empty;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("IN RecordTransfer() - HFC -  strtypetransaction: {0}", objGetRecordTransactionRequest.strtypetransaction));

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, "HFC - agendaGetValidaEta: " + objGetRecordTransactionRequest.agendaGetValidaEta);
            if (Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroUno || Functions.CheckInt(objGetRecordTransactionRequest.agendaGetValidaEta) == CONSTANT.Constants.numeroDos)
            {
                #region AgendaGetValidaEta
                
                    if (Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct) > CONSTANT.Constants.numeroCero)
                    {
                        if (objGetRecordTransactionRequest.FechaProgramada != null || objGetRecordTransactionRequest.FechaProgramada != string.Empty)
                        {
                            if (objGetRecordTransactionRequest.FranjaHora != null)
                            {
                                try
                                {
                                    FixedTransacService.InsertETASelectionResponse objInsertETASelectionResponse = null;
                                    FixedTransacService.InsertETASelectionRequest objInsertETASelectionRequest = null;
                                    objInsertETASelectionResponse = new FixedTransacService.InsertETASelectionResponse();
                                    objInsertETASelectionRequest = new FixedTransacService.InsertETASelectionRequest()
                                    {
                                        audit = audit,
                                        vidconsulta = Functions.CheckInt(objGetRecordTransactionRequest.hdnCodigoRequestAct),
                                        vidInteraccion = CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim(),
                                        vfechaCompromiso = DateTime.Parse(objGetRecordTransactionRequest.FechaProgramada),
                                        vfranja = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[0],
                                        vid_bucket = objGetRecordTransactionRequest.agendaGetCodigoFranja.Split('+')[1]//model.FranjaHorariaETA.Split('+')[1]
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
                
                #endregion
            }
            
                #region 
                FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
                objResponseGenerateSOT = RecordSot(objGetRecordTransactionRequest);
                hdnCodSotValue = objResponseGenerateSOT.IdGenerateSOT;
                strMessage = objResponseGenerateSOT.DescMessaTransfer;
                intResCod = Convert.ToInt(objResponseGenerateSOT.CodMessaTransfer);

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("objResponseGenerateSOT - HFC _ out cod:{0} Res:{1} Des{2}", objResponseGenerateSOT.IdGenerateSOT == null ? "" : objResponseGenerateSOT.IdGenerateSOT, objResponseGenerateSOT.CodMessaTransfer == null ? "" : objResponseGenerateSOT.CodMessaTransfer, objResponseGenerateSOT.DescMessaTransfer == null ? "" : objResponseGenerateSOT.DescMessaTransfer));
                #endregion
            
            

            if (hdnCodSotValue != null && hdnCodSotValue != string.Empty && hdnCodSotValue.ToUpper() != "NULL")
            {

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT GetRecordTransaction- HFC _ CodSotValue {0}", hdnCodSotValue == null ? "" : hdnCodSotValue));
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

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT GetUpdateInter29 -  rFlagInsercion: {0}", objUpdateInter29Response.rFlagInsercion == null ? "" : objUpdateInter29Response.rFlagInsercion));

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
                        Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("UPDATE INTERACTION 30 - SOT EN CURSO - I-  rFlagInsercion: {0}", objUpdateInter30Response.rFlagInsercion));
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
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strIdSession, "UPDATE INTERACTION 30 - SOT EN CURSO - II");
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

                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return oCommonTransacService.GetUpdatexInter30(objUpdateInter30Request);
                    });


                    strMessage = ConfigurationManager.AppSettings("strMsgSOTEnCursoMP");
                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "UPDATE INTERACTION 30 - SOT EN CURSO - III");
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

                    Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT GetRecordTransaction - HFC _  p_texto: {0}", objUpdateInter30Request.p_texto));
                    strMessage = ConfigurationManager.AppSettings("strMensajeErrorparaNotasClfy");

                }
                #endregion
            }
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "strMessage: " + strMessage);
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "OUT RecordTransfer - HFC");
            return blnRes;
        }
        public FixedTransacService.GenerateSOTResponseFixed RecordSot(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.strIdSession);

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
                        objRequestGenerateSOT.vObserv = objGetRecordTransactionRequest.txtNotText + "|" + CONSTANT.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim().Length) + objGetRecordTransactionRequest.hdnCodigoRequestAct.Trim() + "|";
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
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, "IN RecordSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return oFixedTransacService.GetGenerateSOT(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, audit.transaction, string.Format("OUT RecordSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }
        #endregion
        
        #region Record Interaction
        public Dictionary<string, string> RecordInteraction(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest, out  MODEL.TemplateInteractionModel oPlantillaDat)
        {
            Dictionary<string, string> ResposeInteraction;
            var rResult = string.Empty;
            var rMsgText = string.Empty;
            bool blnValidate = false;
            var NAME_PDF = string.Empty;
            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "IN RecordInteraction() - HFC");
            var objInteractionModel = new MODEL.InteractionModel();

            objInteractionModel = DatInteraction(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, objGetRecordTransactionRequest.CustomerID, objGetRecordTransactionRequest.txtNotText, objGetRecordTransactionRequest.CurrentUser, objGetRecordTransactionRequest.ConID, objGetRecordTransactionRequest.PlanoID);

            oPlantillaDat = DatTemplateInteraction(objGetRecordTransactionRequest, out blnValidate);

            var UsuarioAplicacion = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var passAplicacion = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "IN RecordInteraction - HFC _ blnValidate " + blnValidate.ToString());
            try
            {
                if (!blnValidate)
                {

                    rMsgText = string.Format("{0} {1}", Functions.GetValueFromConfigFile("gConstKeyCambInteracTitular", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), Functions.GetValueFromConfigFile("strNoTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
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
                    , objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.CustomerID);

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "IN var DictResposeInteraction - HFC");
                var strMsg = string.Empty;
                bool exitoInsercion = false;
                
                var listInteraction = new List<string>();
                DictResposeInteraction.ToList().ForEach(x =>
                {
                    listInteraction.Add(x.Value.ToString());
                });

                rResult = listInteraction[3];
                exitoInsercion = listInteraction[2].ToUpper() == CONSTANT.Constants.grstTrue.ToUpper() ? true : false;
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "" + objGetRecordTransactionRequest.strtypetransaction, "Insert Interaction" + exitoInsercion.ToString());
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

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.strIdSession, "", "IN listInteraction[2]- HFC " + exitoInsercion);
                if (exitoInsercion)
                {
                    //NAME_PDF = GetConstancyPDF(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest, rResult);
                    if (NAME_PDF == string.Empty)
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
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.Message);
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Error(objGetRecordTransactionRequest.strIdSession, objGetRecordTransactionRequest.strtypetransaction, ex.InnerException.Message);
            }

            ResposeInteraction = new Dictionary<string, string>
            {
                {"hdnInterID", rResult},
                {"rMsgTextInter", rMsgText},
                {"strPath", NAME_PDF}
            };

            return ResposeInteraction;
        }
        public Dictionary<string, object> InsertInteraction(MODEL.InteractionModel objInteractionModel, MODEL.TemplateInteractionModel oPlantillaDat,
            string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            string strTelefono;
            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;
            string ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            CommonServicesController oCommonServicesController = new CommonServicesController();
            Claro.Web.Logging.Info(strIdSession, "", "IN InsertInteraction - HFC");

            string strFlgRegistrado = CONSTANT.Constants.strUno;
            FixedTransacService.CustomerResponse objCustomerResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CallDetailController", "InsertInteraction", "InsertInteraction : SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
            Claro.Web.Logging.Info(strIdSession, "" + audit.transaction, msg);
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
                Claro.Web.Logging.Info(strIdSession, "", "objCustomerResponse.Customer != null");
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            var result = new Dictionary<string, string>();

            #region VALIDACION CONTIGENCIA

            if (ContingenciaClarify != CONSTANT.Constants.blcasosVariableSI)
            {
                Claro.Web.Logging.Info(strIdSession, "", "GetInsertInteractionCLFY");
                result = oCommonServicesController.GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                Claro.Web.Logging.Info(strIdSession, "", "GetInsertContingencyInteraction");
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
                Claro.Web.Logging.Info(strIdSession, "", rInteraccionId);
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = oCommonServicesController.InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                    Claro.Web.Logging.Info(strIdSession, "Transaccion: ", "oCommonServicesController.InsertPlantInteraction");
                }
            }
            dictionaryResponse.Add("rInteraccionId", rInteraccionId);
            Claro.Web.Logging.Info(strIdSession, "", "OUT InsertInteraction - HFC");
            return dictionaryResponse;
        }
        public MODEL.InteractionModel DatInteraction(string strIdsession, string typetransaction, string CUSTOMER_ID, string txtNot, string CurrentUser, string CONTRATO_ID, string CODIGO_PLANO_INST)
        {
            string tipoTran = string.Empty;
            Claro.Web.Logging.Info(strIdsession, "Transaccion: ", "IN DatInteraction( " + strIdsession + ", " + typetransaction + "," + CUSTOMER_ID + "," + txtNot + "," + CurrentUser + "," + CONTRATO_ID + "," + CODIGO_PLANO_INST + ")");
            string strTelTipoHFC = ConfigurationManager.AppSettings("gConstTipoHFC");
            
            tipoTran = ConfigurationManager.AppSettings("strHFCTipifCE"); ;
            
            Claro.Web.Logging.Info(strIdsession, "", "IN DatInteraction - tipoTran:  " + tipoTran);
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

                objInter.ObjidContacto = OCommonServicesController.GetCustomer(ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + Convert.ToInt(CUSTOMER_ID), strIdsession).ToString();
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
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Error(strIdsession, typetransaction, ex.InnerException.Message);

            }
            return objInter;
        }
        public MODEL.TemplateInteractionModel DatTemplateInteraction(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest, out bool blnValidate)
        {
            Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "IN DatTemplateInteraction()");
            string strIdSession = string.Empty;
            string strtypetransaction = string.Empty;
            
            string tipoTran = string.Empty;
           
            tipoTran = ConfigurationManager.AppSettings("strHFCTipifCE");
            
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
                X_LOT_CODE = ConfigurationManager.AppSettings("strConsTraCE"),
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
                Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: ", " DatTemplateInteraction()   catch (Exception ex)");
                blnValidate = false;
            }

            Claro.Web.Logging.Info("IdSession: " + objGetRecordTransactionRequest.strIdSession, "Transaccion: ", "OUT DatTemplateInteraction()");
            return objPla;
        }
        #endregion

        #region CONSTANCY PDF
        public string GetConstancyPDF(string strIdSession, HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest, string strInteraction)
        {
            string NAME_PDF = string.Empty;
            FixedTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "IN GetConstancyPDF() - HFC");
            try
            {
                CommonTransacService.ParametersGeneratePDF oParameter = new CommonTransacService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strCambioEquipoTecFormatoTransac"),
                    
                    strPuntoDeAtencion = objGetRecordTransactionRequest.DescripCADDAC,
                    strNroDoc = objGetRecordTransactionRequest.NumbDocRepreCustomer,
                    strFechaTransaccion = DateTime.Today.ToShortDateString(),
                    StrFechaTransaccionProgram = objGetRecordTransactionRequest.FechaProgramada,
                    strCasoInteraccion = strInteraction,
                    StrCasoInter = strInteraction,
                    strTransaccionDescripcion = ConfigurationManager.AppSettings("strConsTraCE"),
                    StrCostoTransaccion = objGetRecordTransactionRequest.Cargo,
                    strDireccionClienteActual = objGetRecordTransactionRequest.AddressCustomer,
                    strRefTransaccionActual = objGetRecordTransactionRequest.RefAddressCustomer,
                    strDistritoClienteActual = objGetRecordTransactionRequest.DistCustomer,
                    strCodigoPostalActual = objGetRecordTransactionRequest.CodPos,
                    strPaisClienteActual = objGetRecordTransactionRequest.CountryCustomer,
                    strProvClienteActual = objGetRecordTransactionRequest.ProvCustomer,

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
                    StrNroDocIdentidad = objGetRecordTransactionRequest.NumbDocRepreCustomer,
                    strContrato = objGetRecordTransactionRequest.ConID,
                    StrCodigoLocalA = objGetRecordTransactionRequest.CodPos,

                    StrCodigoLocalB = objGetRecordTransactionRequest.hdnCodPos,
                    strCodigoPlanoDestino = objGetRecordTransactionRequest.hdnCodPla,
                    strEnvioCorreo = objGetRecordTransactionRequest.chkEmailChecked ? CONSTANT.Constants.Variable_SI : CONSTANT.Constants.Variable_NO,
                    StrEmail = objGetRecordTransactionRequest.chkEmailChecked ? objGetRecordTransactionRequest.Email : string.Empty,
                    strNroSot = objGetRecordTransactionRequest.strNroSOT,
                    StrContenidoComercial2 = Functions.GetValueFromConfigFile("ExtIntTrasnferContentCommercial2",
                        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")),
                        
                    strflagTipoTraslado = objGetRecordTransactionRequest.strtypetransaction == CONSTANT.Constants.strCuatro ? CONSTANT.Constants.strCero : CONSTANT.Constants.strUno,
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaCambioEquipoHFC")
                };

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "OUT GetConstancyPDF()-CAMBIO DE EQUIPO POR TEC- HFC _ NAME_PDF: " + NAME_PDF);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }

            return NAME_PDF;
        }
        #endregion

        #region  REGISTRO OCC 
        public void GrabarRegistroOCC(HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransactionRequest, string lnkNumSot)
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
                string sComentario = ConfigurationManager.AppSettings("gConstComentarioCambioEq");
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
        #endregion

        #region EMAIL
        public string GetSendEmail(HELPERS.HFC.ChangeEquipment.ChangeEquip oChangeEquipment, string strAdjunto, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oChangeEquipment.strIdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strTemplateEmail = TemplateEmail(oChangeEquipment);
                string strTIEMailAsunto = ConfigurationManager.AppSettings("strCambioEquipoMailAsunto");

                string strDestinatarios = oChangeEquipment.Email;
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
                objGetSendEmailRequest =
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
                Claro.Web.Logging.Error(oChangeEquipment.strIdSession, AuditRequest.transaction, ex.Message);
                Claro.Web.Logging.Info(oChangeEquipment.strIdSession, AuditRequest.transaction, "Change Equipment_ HFC  ERROR - GetSendEmail");
                strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return strResul;
        }
        public string TemplateEmail(HELPERS.HFC.ChangeEquipment.ChangeEquip vobjPlaInt)
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
            strmessage += "<tr><td width='180' class='Estilo1' height='22'>Por la Presente queremos informarle que su solicitud de Cambio de Equipo fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
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
        public void InsertAudit(string strTransacionID, string strTransacion, HELPERS.HFC.ChangeEquipment.ChangeEquip objGetRecordTransaction)
        {
            CommonServicesController commonController = new CommonServicesController();


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

      

    }
}