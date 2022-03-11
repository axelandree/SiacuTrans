using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oTransacServ = Claro.SIACU.Transac.Service;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using System.Collections;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using Util = Claro.SIACU.Transac.Service.DataUtil;
using FixedService = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Transac.Service;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
 

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class AdditionalPointsController :  CommonServicesController
    {
        private readonly PostTransacService.PostTransacServiceClient _oServicePostpaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        private static string intNroOST = string.Empty;
        private static FixedTransacService.BEETAAuditoriaResponseCapacityHFC _BEETAAuditoriaResponseCapacityHFC = new BEETAAuditoriaResponseCapacityHFC();
 
        // GET: /Transactions/AdditionalPoints/

        public ActionResult HFCAdditionalPoints()
        {
            int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
            return PartialView();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetParameter()
        {
            Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter ", "Entra a GetParameter");

            Model.HFC.AdditionalPointsModel oModel = new Model.HFC.AdditionalPointsModel();
            if (Request.IsAjaxRequest()) {

                try
                {

                    oModel.strMensajeErrorConsultaIGV = KEY.AppSettings("strMensajeErrorConsultaIGV", "");

                    oModel.strServerName = Request.ServerVariables["SERVER_NAME"];
                    oModel.strLocalAddress = Request.ServerVariables["LOCAL_ADDR"];
                    oModel.strHostName = Request.UserHostName;

                    oModel.strTitlePageAdditionalPoints = KEY.AppSettings("gConstKeyTituloTranGenVisTecMan", "");

                    oModel.strMessageConfirmAdditionsPoints = KEY.AppSettings("gConstKeyPreguntaGenVisTecMan", "");

                   
                    oModel.strMessageEnterMail = Functions.GetValueFromConfigFile("gConstKeyIngreseCorreo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstKeyIngreseCorreo", oModel.strMessageEnterMail);

                    
                    oModel.strMessageValiateMail = Functions.GetValueFromConfigFile("gConstKeyCorreoIncorrecto", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstKeyCorreoIncorrecto", oModel.strMessageValiateMail);

                    
                    oModel.strMessageValidatePointCare = Functions.GetValueFromConfigFile("gConstMsgSelCacDac", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgSelCacDac", oModel.strMessageValidatePointCare);

                   
                    oModel.strMessageValidatePhone = Functions.GetValueFromConfigFile("gConstMsgTlfDSNum", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgTlfDSNum",oModel.strMessageValidatePhone);

                    
                    oModel.strMessageValidateTimeZone = Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgNSFranjaHor",  oModel.strMessageValidateTimeZone);

                   
                    oModel.strMessageValidateSchedule = Functions.GetValueFromConfigFile("gConstMsgNVAgendamiento", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgNVAgendamiento", oModel.strMessageValidateSchedule );

                    oModel.strDateServer = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strDateServer", oModel.strDateServer);
                    
                    int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
                    oModel.strDateNew = DateTime.Now.AddDays(number).ToShortDateString();


                       oModel.strCustomerRequestId = KEY.AppSettings("strConstCodigoASolicitudDelCliente", "");
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strConstCodigoASolicitudDelCliente", oModel.strCustomerRequestId);
                 

                   
                    oModel.strJobTypeComplementarySalesHFC = Functions.GetValueFromConfigFile("strCodigoTipoTrabajoHFCVentaComplementaria", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strCodigoTipoTrabajoHFCVentaComplementaria",  oModel.strJobTypeComplementarySalesHFC);
                   
                    oModel.strMessageConsultationDisabilityNotAvailable = KEY.AppSettings("gStrMsjConsultaCapacidadNoDisp", "");
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gStrMsjConsultaCapacidadNoDisp", oModel.strMessageConsultationDisabilityNotAvailable);
                    
                    oModel.strMessageOK = Functions.GetValueFromConfigFile("gConstMsgOVExito", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgOVExito",oModel.strMessageOK);
                    
                    oModel.strRouteSiteInitial = KEY.AppSettings("strRutaSiteInicio", "");
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strRutaSiteInicio", oModel.strRouteSiteInitial );
                   
                    oModel.strJobTypeDefault = KEY.AppSettings("strTipTraOrdenVisitaDefaultConfig", "");
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strTipTraOrdenVisitaDefaultConfig", oModel.strJobTypeDefault);
                 
                    oModel.strJobTypeLoyalty = KEY.AppSettings("TipoTrabajo_HFCFIDELIZACION", "");
                       Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - TipoTrabajo_HFCFIDELIZACION",  oModel.strJobTypeLoyalty);
                   
                    oModel.strJobTypeMaintenance = KEY.AppSettings("TipoTrabajo_HFCMANTENIMIENTO", "");
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - TipoTrabajo_HFCMANTENIMIENTO", oModel.strJobTypeMaintenance);
                   
                    oModel.strJobTypeMaintenance_Bs = KEY.AppSettings("TipoTrabajo_HFCMANTENIMIENTO_Bs", "");
                     Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - TipoTrabajo_HFCMANTENIMIENTO_Bs", oModel.strJobTypeMaintenance_Bs );
                    
                    oModel.strJobTypeRetention = KEY.AppSettings("TipoTrabajo_HFC_RETENCION", "");
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - TipoTrabajo_HFC_RETENCION", oModel.strJobTypeRetention);
                    
                    oModel.strJobTypePoints = KEY.AppSettings("TipoTrabajo_HFCSIAC_PUNTO", "");
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - TipoTrabajo_HFCSIAC_PUNTO",oModel.strJobTypePoints);
                    
                    oModel.strMessageMaxProgDay = Functions.GetValueFromConfigFile("strMsgMaxProgxDia", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgMaxProgxDia", oModel.strMessageMaxProgDay );

                    oModel.strMessageDateAppNotLowerNow = Functions.GetValueFromConfigFile("strMsgFechaAppNoMenorActual", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgFechaAppNoMenorActual", oModel.strMessageDateAppNotLowerNow);
                    
                    oModel.strMessageGenericBackOffice = Functions.GetValueFromConfigFile("strMsgGeneraBackOffice", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgGeneraBackOffice", oModel.strMessageGenericBackOffice);
                    
                    oModel.strMessageGenericBackOfficeBucked = Functions.GetValueFromConfigFile("strMsgGeneraBackOfficeBucked", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgGeneraBackOfficeBucked",  oModel.strMessageGenericBackOfficeBucked);

                   
                    oModel.strMessageCustomerContractEmpty = Functions.GetValueFromConfigFile("strMsgConsultaCustomerContratoVacio", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgConsultaCustomerContratoVacio", oModel.strMessageCustomerContractEmpty );


                    oModel.strMessageNotServiceCableInternet = Functions.GetValueFromConfigFile("strTextoNoTieneServicioCableYOInternetActivoLTE", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strTextoNoTieneServicioCableYOInternetActivo",oModel.strMessageNotServiceCableInternet);


                    
                    oModel.strMessageNotTimeZoneHourETA = Functions.GetValueFromConfigFile("gConstMsgNSFranjaHorETA", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - gConstMsgNSFranjaHorETA", oModel.strMessageNotTimeZoneHourETA);

                   
                    oModel.strMessageForcesSSTTETA = Functions.GetValueFromConfigFile("strMsgObligaSSTTETA", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strMsgObligaSSTTETA", oModel.strMessageForcesSSTTETA);

                    oModel.strHourServer = DateTime.Now.ToString("HH:mm");

                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter - strAdditionalPointHFCCost", KEY.AppSettings("strAdditionalPointHFCCost", ""));
                    oModel.strAdditionaPointHFCCost = KEY.AppSettings("strAdditionalPointHFCCost", "");

                    oModel.strMessageValidationETA = KEY.AppSettings("strMessageETAValidation");

                    oModel.strMensajeTransaccionFTTH = KEY.AppSettings("strMensajeBackOfficeFTTH"); //RONALDRR - PUNTOS ADICIONALES
                    oModel.strPlanoFTTH = KEY.AppSettings("strPlanoFTTH"); //RONALDRR - PUNTOS ADICIONALES

                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter ", ex.Message);
                     
                }
                
            }

            Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter ", "sale de GetParameter");

            return Json(oModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetJobType(string strIdSession)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetJobType ", "Entra a GetJobType");
            string strIdAdditionalPoints = ConfigurationManager.AppSettings("strIdTipoTrabajoAdditionalPoints").ToString();

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            
            if (Request.IsAjaxRequest())
            {
                FixedTransacService.JobTypesResponseHfc objJobTypesResponse;
                FixedTransacService.JobTypesRequestHfc objJobTypesRequest = new FixedTransacService.JobTypesRequestHfc
                {
                    p_tipo = Convert.ToInt(oTransacServ.Constants.strUno),
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
                };

                try
                {
                    objJobTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                    {
                        return new FixedTransacService.FixedTransacServiceClient().GetJobTypesVisitOrder(objJobTypesRequest);
                    });

                    if (objJobTypesResponse.JobTypes != null)
                    {
                        if (objJobTypesResponse.JobTypes.Count > 0)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;
                            foreach (var item in objJobTypesResponse.JobTypes.Where(x => x.tiptra == strIdAdditionalPoints))
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                oGenericItem.Code = item.tiptra;
                                oGenericItem.Description = item.descripcion;
                                lstGenericItem.Add(oGenericItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            else {
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetJobType ", "sale de GetJobType con la lista lstGenericItem" + Functions.CheckStr(lstGenericItem.Count));

            return Json(new { data = lstGenericItem });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMotivoSot(string strIdSession) {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetMotivoSot ", "entra a GetMotivoSot");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            if (Request.IsAjaxRequest())
            {
                CommonTransacService.MotiveSotResponseCommon objMotiveSotResponseCommon;
                CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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
                            return _oServiceCommon.GetMotiveSot(objMotiveSotRequestCommon);
                        });

                    if (objMotiveSotResponseCommon.getMotiveSot != null) {
                        if (objMotiveSotResponseCommon.getMotiveSot.Count > 0) {
                            Helpers.CommonServices.GenericItem oGenericItem = null;
                            foreach (CommonTransacService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                oGenericItem.Code = item.Code;
                                oGenericItem.Description = item.Description;
                                lstGenericItem.Add(oGenericItem);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }
            }
            else {
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetMotivoSot ", "sale de GetMotivoSot con valor de la lista lstGenericItem " + Functions.CheckStr(lstGenericItem.Count));

            return Json(new { data = lstGenericItem });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDocumentType(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetDocumentType ", "entra a GetDocumentType");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            if (Request.IsAjaxRequest())
            {
                CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
                try
                {
                    if (oModel.strJobTypes != null)
                    {
                        if (oModel.strJobTypes.IndexOf(".|") != Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                        {
                            oModel.strJobTypes = oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2);
                        }
                    }
                    var GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
                    {
                        return _oServiceCommon.GetDocumentTypeCOBS(oModel.IdSession, audit.transaction, Functions.GetValueFromConfigFile("IdListaTipoServSOT", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));
                    });


                    if (GetDocumentType != null)
                    {
                        if (GetDocumentType.Count > 0)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;

                            foreach (var item in GetDocumentType)
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                if (!string.IsNullOrEmpty(oModel.strInternetValue))
                                {
                                    if (oModel.strInternetValue.ToUpper() == "T" && item.Code == ConstantsHFC.numeroUno.ToString())
                                    {
                                        oGenericItem.Code = item.Code;
                                        oGenericItem.Description = item.Description;
                                        lstGenericItem.Add(oGenericItem);
                                    }
                                }

                                if (!string.IsNullOrEmpty(oModel.strCellPhoneValue))
                                {
                                    if (oModel.strCellPhoneValue.ToUpper() == "T" && item.Code == ConstantsHFC.numeroDos.ToString())
                                    {
                                        oGenericItem.Code = item.Code;
                                        oGenericItem.Description = item.Description;
                                        lstGenericItem.Add(oGenericItem);
                                    }
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }
            }
            else {
            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetDocumentType ", "sale de GetDocumentType de la lista lstGenericItem " + Functions.CheckStr(lstGenericItem.Count));
            
            return Json(new { data = lstGenericItem }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAttachedQuantity(string strIdSession)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetAttachedQuantity ", "entra a GetAttachedQuantity");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            if (Request.IsAjaxRequest())
            {
                CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                try
                {
                    
                    var _lstGenericItem = App_Code.Common.GetXMLList("ListaNumeroDeAnexos");
                    Helpers.CommonServices.GenericItem oGenericItem = null;
                    foreach (FixedService.GenericItem item in _lstGenericItem)
                    {
                        oGenericItem = new Helpers.CommonServices.GenericItem();
                        oGenericItem.Code = item.Codigo;
                        oGenericItem.Description = item.Descripcion;
                        lstGenericItem.Add(oGenericItem);
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            else {
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetAttachedQuantity ", "sale de GetAttachedQuantity con valores de la lista lstGenericItem " + Functions.CheckStr(lstGenericItem.Count));

            return Json(new { data = lstGenericItem });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderType(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetOrderType ", "entra a GetOrderType");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;

            if (Request.IsAjaxRequest())
            {
                string strTipoOrdEta = string.Empty;
                OrderTypesResponseHfc objOrderTypesResponse = new OrderTypesResponseHfc();
                OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
                FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                objOrderTypesRequest.audit = audit;
                OrderSubTypesRequestHfc objSubTypesRequest = new OrderSubTypesRequestHfc();
                OrderSubTypesResponseHfc objOrderSubTypesResponseHfc = new OrderSubTypesResponseHfc();

                if (oModel.strJobTypes != null)
                {

                    if (oModel.strJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                        objSubTypesRequest.av_cod_tipo_trabajo = oModel.strJobTypes;
                    else
                        objSubTypesRequest.av_cod_tipo_trabajo = oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2);

                    try
                    {
                        objSubTypesRequest.audit = audit;
                        objOrderSubTypesResponseHfc = Claro.Web.Logging.ExecuteMethod<OrderSubTypesResponseHfc>(() =>
                        {
                            return new FixedTransacServiceClient().GetOrderSubTypeWork(objSubTypesRequest);
                        });

                        if (objOrderSubTypesResponseHfc.OrderSubTypes != null)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;
                            foreach (var aux in objOrderSubTypesResponseHfc.OrderSubTypes)
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                oGenericItem.Code = aux.COD_SUBTIPO_ORDEN + "|" + aux.TIEMPO_MIN + "|" + aux.ID_SUBTIPO_ORDEN;
                                oGenericItem.Description = aux.DESCRIPCION;
                                oGenericItem.Code2 = aux.TIPO_SERVICIO;
                                lstGenericItem.Add(oGenericItem);
                            }
                        }

                        objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
                        {
                            audit = audit,
                            av_cod_tipo_trabajo = objSubTypesRequest.av_cod_tipo_trabajo,
                            av_cod_contrato = oModel.strContractId
                        };
                        objResponseValidate = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubType>(() => { return _oServiceFixed.GetValidationSubTypeWork(objResquest); });

                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                        throw new Exception(ex.Message);
                    }
                }

            }
            else
            {

            }


            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetOrderType ", "sale de GetOrderType con valores de la lista lstGenericItem " + Functions.CheckStr(lstGenericItem.Count));

            return Json(new { data = lstGenericItem, typeValidate = objResponseValidate });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductDetailt(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetProductDetailt ", "entra a GetProductDetailt");

            FixedTransacService.ServiceDTHResponse objBEDecoServicesResponseFixed = new ServiceDTHResponse();
            if (Request.IsAjaxRequest())
            {
                FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                FixedTransacService.ServiceDTHRequest objBEDecoServicesRequestFixed = new ServiceDTHRequest();

                try
                {
                    objBEDecoServicesRequestFixed.audit = _audit;
                    objBEDecoServicesRequestFixed.strCustomerId = oModel.strCustomerId;
                    objBEDecoServicesRequestFixed.strCoid = oModel.strContractId;


                    objBEDecoServicesResponseFixed = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ServiceDTHResponse>(() =>
                    {
                        return _oServiceFixed.GetServiceDTH(objBEDecoServicesRequestFixed);
                    });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, _audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetProductDetailt ", "sale de GetProductDetailt con valores de la lista objBEDecoServicesResponseFixed.ListDecoServices " + Functions.CheckStr(objBEDecoServicesResponseFixed.ListServicesDTH.Count));

            return Json(new { data = objBEDecoServicesResponseFixed.ListServicesDTH });
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetValidateETA(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidateETA ", "entra a GetValidateETA");

            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

            if (Request.IsAjaxRequest())
            {
                string p_origen, p_idplano, p_ubigeo, p_tipserv, p_outCodZona, v_TipoOrden;
                int p_tiptra = 0, p_outIndica = 0;

                p_origen = KEY.AppSettings("gConstHFCOrigen");
                p_idplano = oModel.strCodePlanInst;
                p_ubigeo = string.Empty;

                if (oModel.strJobTypes != null)
                {
                    if (oModel.strJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                    {
                        p_tiptra = Convert.ToInt(oModel.strJobTypes);
                    }
                    else
                    {
                        p_tiptra = Convert.ToInt(oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2));
                    }
                }
                p_tipserv = KEY.AppSettings("gConstHFCTipoServicio");
                p_outCodZona = string.Empty;
                p_outIndica = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;

                OrderTypesResponseHfc objOrderTypesResponse = null;
                OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
                objOrderTypesRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

                if (oModel.strJobTypes != null)
                {

                    if (oModel.strJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                    {
                        objOrderTypesRequest.vIdtiptra = oModel.strJobTypes;
                    }
                    else
                    {
                        objOrderTypesRequest.vIdtiptra = oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2);
                    }

                    objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<OrderTypesResponseHfc>(() =>
                    {
                        return new FixedTransacServiceClient().GetOrderType(objOrderTypesRequest);
                    });

                }

                if (objOrderTypesResponse.ordertypes == null)
                {
                    v_TipoOrden = oTransacServ.Constants.PresentationLayer.NumeracionMENOSUNO;
                }
                else
                {
                    if (objOrderTypesResponse.ordertypes.Count() == 0)
                    {
                        v_TipoOrden = oTransacServ.Constants.PresentationLayer.NumeracionMENOSUNO;
                    }
                    else
                    {
                        v_TipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;
                    }
                }
                ETAFlowRequestHfc objETAFlowRequestHfc = new ETAFlowRequestHfc();
                ETAFlowResponseHfc objETAFlowResponseHfc;
                objETAFlowRequestHfc.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                objETAFlowRequestHfc.an_tipsrv = p_tipserv;
                objETAFlowRequestHfc.an_tiptra = p_tiptra;
                objETAFlowRequestHfc.as_origen = p_origen;
                objETAFlowRequestHfc.av_idplano = oModel.strCodePlanInst;
                objETAFlowRequestHfc.av_ubigeo = p_ubigeo;

                objETAFlowResponseHfc = Claro.Web.Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().ETAFlowValidate(objETAFlowRequestHfc);
                });

                objGenericItem.Codigo =  objETAFlowResponseHfc.ETAFlow.an_indica.ToString();

                if (objETAFlowResponseHfc.ETAFlow.as_codzona != null)
                {
                    if (objETAFlowResponseHfc.ETAFlow.as_codzona.Length > 0)
                    {
                        if (objETAFlowResponseHfc.ETAFlow.as_codzona.ToUpper() != "NULL")
                        {
                            objGenericItem.Codigo2 = objETAFlowResponseHfc.ETAFlow.as_codzona + "|" + p_idplano + "|" + v_TipoOrden;
                        }
                        else {
                            objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
                        }
                    }
                    else {
                        objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
                    }
                }
                else {
                    objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
                }

            

                switch (objETAFlowResponseHfc.ETAFlow.an_indica)
                {
                    case -1:
                        objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strMsgNoExistePlano", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -2:
                        objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strMsgNoExisteUbigeo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -3:
                        objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strMsgNoExistePlanoUbigeo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -4:
                        objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strMsgNoExisteTipoTrabajo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -5:
                        objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strMsgNoExisteTipoServicio", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case 1:
                        objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.CriterioMensajeOK;
                        break;
                    case 0:
                        objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.PresentationLayer.CriterioMensajeNOOK;
                        break;
                }
            }
            else { 
            
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidateETA ", "sale de GetValidateETA con valor en objGenericItem" + Functions.CheckStr(objGenericItem.Codigo));

            return Json(new { data = objGenericItem });
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="objScheduleRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ValidateSchedule(string strIdSession, CommonTransacService.ScheduleRequest objScheduleRequest)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ValidateSchedule ", "entra a ValidateSchedule");

            bool bResult = false;
            Helpers.CommonServices.GenericItem objGenericItem = new Helpers.CommonServices.GenericItem();

            CommonTransacService.AuditRequest audit =
            App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            try
            {
                if (objScheduleRequest.vJobTypes.Contains(".|"))
                    objScheduleRequest.vJobTypes = objScheduleRequest.vJobTypes.Substring(0, objScheduleRequest.vJobTypes.Length - 2);
             
               objScheduleRequest.audit = audit;

               bResult =
                        Claro.Web.Logging.ExecuteMethod(() =>
                        {
                            return _oServiceCommon.ValidateSchedule(objScheduleRequest);
                        });

               if (bResult)
                   objGenericItem.Description = Claro.Constants.NumberSixty.ToString();
               else
                   objGenericItem.Description = Claro.Constants.NumberZero.ToString();

            }
            catch (Exception ex)
            {
                
                Claro.Web.Logging.Error(strIdSession, objScheduleRequest.audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ValidateSchedule ", "sale de ValidateSchedule con valor en objGenericItem" + Functions.CheckStr(objGenericItem.Code));

            return Json(new { data = objGenericItem });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AdditionalPointsSave(Model.HFC.AdditionalPointsModel oModel) {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: AdditionalPointsSave ", "entra a AdditionalPointsSave");
            
            Model.HFC.AdditionalPointsModel oAdditionalPointsModel = new Model.HFC.AdditionalPointsModel();
            oAdditionalPointsModel.bErrorTransac = false;
            string strResult = string.Empty;
            if (Request.IsAjaxRequest())
            {
                FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                FixedTransacService.GenericSotResponse objGenericSotResponse = new GenericSotResponse();
                

                oModel.strCodeTipification = KEY.AppSettings("TRANSACCION_PUNTO_ADICIONAL_HFC", "");
                try
                {

                     
                    if (!GetValidateCustomerID(oModel))
                    {
                        oAdditionalPointsModel.bErrorTransac = true;
                        oAdditionalPointsModel.strMessageErrorTransac = oModel.strMessageErrorTransac;
                        return Json(oAdditionalPointsModel);
                    }

                    var objInteractionModel = new Model.InteractionModel();
                    var oPlantillaDat = new Model.TemplateInteractionModel();

                    objInteractionModel = DatInteraction(oModel);
                    oPlantillaDat = GetDatTemplateInteractionAdditionalPoints(oModel);
                    var strNroTelephone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + oModel.strCustomerId;
                    var strUserSession = string.Empty;
                    var strUserAplication = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                    var strPassUser = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                    RegisterLog(oModel.IdSession, "AdditionalPointsSave", "Guadar Transacción" + string.Empty);

                    var resultInteraction = InsertInteraction(objInteractionModel, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.strCustomerId);

                   

                    var lstaDatTemplate = new List<string>();
                    foreach (KeyValuePair<string, object> par in resultInteraction)
                    {
                        lstaDatTemplate.Add(par.Value.ToString());
                    }

                    RegisterLog(oModel.IdSession, "AdditionalPointsSave", "Case ID" + lstaDatTemplate[3]);

                    oAdditionalPointsModel.strCaseID = lstaDatTemplate[3];
                    oModel.strCaseID = lstaDatTemplate[3];
                    oAdditionalPointsModel.bErrorTransac = false;
                    oAdditionalPointsModel.bErrorGenericCodSot = false;

                    RegisterLog(oModel.IdSession, "AdditionalPointsSave", "Transaccion Mensaje" + lstaDatTemplate[0]);

                    if (lstaDatTemplate[0] != ConstantsHFC.PresentationLayer.CriterioMensajeOK && oAdditionalPointsModel.strCaseID == string.Empty)
                    {
                        Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                        oAdditionalPointsModel.bErrorTransac = true;
                        oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        return Json(oAdditionalPointsModel);
                    }

                    if ((oAdditionalPointsModel.strCaseID.Length > 1) && (Convert.ToInt(oModel.strValidateETA) == ConstantsHFC.numeroUno || Convert.ToInt(oModel.strValidateETA) == ConstantsHFC.numeroDos))
                    {
                        if (oModel.strDateProgramming != null || oModel.strDateProgramming != string.Empty)
                        {
                            if (oModel.strSchedule != null)
                    {
                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GetRegisterEtaSelection", "Inicio");
                                try
                                {
                                    FixedTransacService.InsertETASelectionResponse objInsertETASelectionResponse = null;
                                    FixedTransacService.InsertETASelectionRequest objInsertETASelectionRequest = null;
                                    objInsertETASelectionResponse = new FixedTransacService.InsertETASelectionResponse();
                                    objInsertETASelectionRequest = new FixedTransacService.InsertETASelectionRequest()
                                    {
                                        audit = audit,
                                        vidconsulta = Functions.CheckInt(oModel.strRequestActId),
                                        vidInteraccion = oTransacServ.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - oModel.strRequestActId.Trim().Length) + oModel.strRequestActId.Trim(),
                                        vfechaCompromiso = DateTime.Parse(oModel.strDateProgramming),
                                        vfranja = oModel.strScheduleValue.Split('+')[0],
                                        vid_bucket = oModel.strScheduleValue.Split('+')[1]//model.FranjaHorariaETA.Split('+')[1]
                                    };
                                    objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return _oServiceFixed.GetInsertETASelection(objInsertETASelectionRequest); });
                                }
                                catch (Exception ex)
                                {
                                    RegisterLog(oModel.IdSession, "AdditionalPointsSave", ex.Message);
                                    Claro.Web.Logging.Error(oModel.IdSession, oModel.strTransaction, ex.Message);
                                }
                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GetRegisterEtaSelection", "FIN -> Result" + strResult);
                    }
                        }
                    }
                    if (oAdditionalPointsModel.strCaseID != string.Empty)
                    {

                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "Inicio");
                        objGenericSotResponse = GenerarSOT(oModel);
                        if (string.IsNullOrEmpty(oModel.strCodSOT)) 
                        {
                            RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "Erro No se genero");
                            throw new System.ArgumentException("No se genero la SOT");
                        }
                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "FIN");

                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rCodSot:" + objGenericSotResponse.rCodSot);
                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rCodRes:" + objGenericSotResponse.rCodRes);
                        RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rDescRes: " + objGenericSotResponse.rDescRes);

                        oAdditionalPointsModel.strCodSOT = objGenericSotResponse.rCodSot;
                        oModel.strCodSOT = objGenericSotResponse.rCodSot;

                        oModel.bErrorGenericCodSot = true;
                        oAdditionalPointsModel.bErrorGenericCodSot = true;

                        if (objGenericSotResponse != null)
                        {

                            if (objGenericSotResponse.rCodRes == ConstantsHFC.strUno)
                            {

                                oModel.bErrorGenericCodSot = false;
                                oAdditionalPointsModel.bErrorGenericCodSot = false;
                                try
                                {
                                    oModel.strCodSOT = objGenericSotResponse.rCodSot;

                                    RegisterLog(oModel.IdSession, "AdditionalPointsSave->SaveCost", "Inicio");
                                    //SaveCost(oModel);
                                    GrabarCambioDireccionPostal(oModel);
                                    GrabarRegistroOCC(oModel);
                                    RegisterLog(oModel.IdSession, "AdditionalPointsSave->SaveCost", "FIN");
                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Save Fidelidad ", ex.Message);
                                }


                            }
                            else
                            {
                                oAdditionalPointsModel.bErrorTransac = true;
                                oAdditionalPointsModel.strMessageErrorTransac = objGenericSotResponse.rDescRes;
                            }
                        }

                        if (!oModel.bErrorGenericCodSot)
                        {
                            try
                            {
                                GenerateContancy(oModel);
                            }
                            catch (Exception ex)
                            {
                                RegisterLog(oModel.IdSession, "Constancia-ERROR", ex.Message);
                            }
                           

                            oAdditionalPointsModel.bGeneratedPDF = oModel.bGeneratedPDF;
                            oAdditionalPointsModel.strFullPathPDF = oModel.strFullPathPDF;
                            if (oModel.bSendMail)
                            {
                                try
                                {
                                     
                                    byte[] attachFile = null;
                                    CommonServicesController objCommonServices = new CommonServicesController();
                                    
                                    string strAdjunto = string.IsNullOrEmpty(oModel.strFullPathPDF) ? string.Empty : oModel.strFullPathPDF.Substring(oModel.strFullPathPDF.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                                    var ResultMAil = string.Empty;
                                    if (objCommonServices.DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, oModel.strFullPathPDF, out attachFile))
                                         SendCorreo(oModel, strAdjunto, attachFile);

                                    
                                     
                                }
                                catch (Exception ex)
                                {
                                    RegisterLog(oModel.IdSession, "AdditionalPointsSave->SendCorreo", "Error =>" + ex.Message);
                                }

                            }
                        }

                    }



                    if (lstaDatTemplate[0] != ConstantsHFC.PresentationLayer.CriterioMensajeOK && lstaDatTemplate[0] != string.Empty)
                    {
                        Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                        oAdditionalPointsModel.bErrorTransac = true;
                        oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        return Json(oAdditionalPointsModel);
                    }

                    string[,] strDetails = new string[3, 3];

                    strDetails[0, 0] = "Telefono";
                    strDetails[0, 1] = string.Empty;
                    strDetails[0, 2] = "Telefono";

                    strDetails[1, 0] = "InvoiceNumber";
                    strDetails[1, 1] = oModel.strMonthEmision + "" + oModel.strYearEmision;
                    strDetails[1, 2] = "Número de Invoice Number";

                    strDetails[2, 0] = "Tipo Transaccion";
                    strDetails[2, 1] = oModel.strTransaction;
                    strDetails[2, 2] = "Transacción Puntos Adicionales";

                    int count = ((strDetails.Length / 4) - 1);
                    var sbText = new System.Text.StringBuilder();
                    for (int i = 0; i < count; i++)
                    {
                        if (strDetails.GetValue(i, 1) != null && strDetails.GetValue(i, 2) != null)
                        {
                            sbText.Append(" " + strDetails.GetValue(i, 1) + " : ");
                            sbText.Append(strDetails.GetValue(i, 2));
                        }
                    }
                    var keyAppSettings = "strConstArchivoSIACUTHFCConfig";
                    var fileName = "DescAuditVisitMant";
                    var strDescription = Functions.GetValueFromConfigFile(fileName, KEY.AppSettings(keyAppSettings));
                    var strTransaction = KEY.AppSettings("gConstKeyTransOrdenVisitaMantenimiento");
                    var strService = KEY.AppSettings("gConstEvtServicio_ModCP");
                    var strText = sbText + "" + strDescription;
                    SaveAuditM(strTransaction, strService, strText, oModel.strTelephone, oModel.strFullName, oModel.IdSession, oModel.Sn, oModel.IpServidor);
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: AdditionalPointsSave ", "sale de AdditionalPointsSave");

                    return Json(oAdditionalPointsModel);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                    oAdditionalPointsModel.bErrorTransac = true;
                    oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    return Json(oAdditionalPointsModel);
                }
            }
            else {
                
                oAdditionalPointsModel.bErrorTransac = true;
                oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: AdditionalPointsSave ", "sale de AdditionalPointsSave");
                
                return Json(oAdditionalPointsModel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Model.InteractionModel DatInteraction(Model.HFC.AdditionalPointsModel model)
        {
            Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: DatInteraction ", "entra a DatInteraction");

            var objInteractionModel = new Model.InteractionModel();
            var tipification = GetTypificationHFC(model.IdSession, model.strCodeTipification);
            tipification.ToList().ForEach(x =>
            {
                objInteractionModel.Type = x.Type;
                objInteractionModel.Class = x.Class;
                objInteractionModel.SubClass = x.SubClass;
                objInteractionModel.InteractionCode = x.InteractionCode;
                objInteractionModel.TypeCode = x.TypeCode;
                objInteractionModel.ClassCode = x.ClassCode;
                objInteractionModel.SubClassCode = x.SubClassCode;
            });



            string strFlgRegistrado = ConstantsHFC.strUno;
            var phone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = phone,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
            objInteractionModel.DateCreaction = DateTime.Now.ToString("MM/dd/yyyy");
            objInteractionModel.Telephone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId;
            objInteractionModel.Type = objInteractionModel.Type;
            objInteractionModel.Class = objInteractionModel.Class;
            objInteractionModel.SubClass = objInteractionModel.SubClass;
            objInteractionModel.TypeInter = KEY.AppSettings("AtencionDefault");
            objInteractionModel.Method = KEY.AppSettings("MetodoContactoTelefonoDefault");
            objInteractionModel.Result = KEY.AppSettings("Ninguno");
            objInteractionModel.MadeOne = ConstantsHFC.strCero;
            objInteractionModel.Note = model.strNote;
            objInteractionModel.FlagCase = ConstantsHFC.strCero;
            objInteractionModel.UserProces = KEY.AppSettings("USRProcesoSU");
            objInteractionModel.Contract = model.strContractId;
            objInteractionModel.Plan = model.strPostalCode;
            objInteractionModel.Agenth = model.strlogin;
            Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: DatInteraction ", "sale de DatInteraction");

            return objInteractionModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInteractionModel"></param>
        /// <param name="oPlantillaDat"></param>
        /// <param name="strNroTelephone"></param>
        /// <param name="strUserSession"></param>
        /// <param name="strUserAplication"></param>
        /// <param name="strPassUser"></param>
        /// <param name="boolEjecutTransaction"></param>
        /// <param name="strIdSession"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        private Dictionary<string, object> InsertInteraction(Model.InteractionModel objInteractionModel,
                                                           Model.TemplateInteractionModel oPlantillaDat,
                                                           string strNroTelephone,
                                                           string strUserSession,
                                                           string strUserAplication,
                                                           string strPassUser,
                                                           bool boolEjecutTransaction,
                                                           string strIdSession,
                                                           string strCustomerId)
        {

            Claro.Web.Logging.Info("Session: 26161651", "Transaction: InsertInteraction ", "entra a InsertInteraction");

            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            string strTelefono;

            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            string strFlgRegistrado = ConstantsHFC.strUno;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            
            var result = new Dictionary<string, string>();
            if (ContingenciaClarify != ConstantsHFC.blcasosVariableSI)
            {
                result = GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                result = GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }

            var model = new List<string>();
            foreach (KeyValuePair<string, string> par in result)
            {
                model.Add(par.Value);
            }

            var rInteraccionId = model[0];

            var dictionaryResponse = new Dictionary<string, object>();
            if (rInteraccionId != string.Empty)
            {
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }
            dictionaryResponse.Add("rInteraccionId", rInteraccionId);
            Claro.Web.Logging.Info("Session: 26161651", "Transaction: InsertInteraction ", "sale de InsertInteraction");

            return dictionaryResponse;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        public FixedTransacService.GenerateSOTResponseFixed registrarEtaSot(Model.HFC.AdditionalPointsModel oModel)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            objRequestGenerateSOT.vCoID = oModel.strCodSOT;
            objRequestGenerateSOT.idSubTypeWork = oModel.strSubTypeWork.Split('|')[2];
            objRequestGenerateSOT.vFranja = oModel.strScheduleValue.Split('+')[0];
            objRequestGenerateSOT.idBucket = oModel.strScheduleValue.Split('+')[1];
            objRequestGenerateSOT.vFeProg = oModel.strDateProgramming;
            try
            {
                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "IN registrarEtaSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return _oServiceFixed.registraEtaSot(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format("OUT registrarEtaSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <param name="strAdjunto"></param>
        /// <param name="attachFile"></param>
        private void SendCorreo(Model.HFC.AdditionalPointsModel oModel, string strAdjunto, byte[] attachFile)
        {

            RegisterLog(oModel.IdSession, "AdditionalPointsSave->SendCorreo", "INICIO");
            RegisterLog(oModel.IdSession, "AdditionalPointsSave->SendCorreo", "Adjunto =>" + strAdjunto);
            string strResul = string.Empty;

            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                var oDatTemplateInteraction = GetInfoInteractionTemplate(oModel.IdSession, oModel.strCaseID);

                string strTemplateEmail = TemplateEmail();
                string strTIEMailAsunto =  KEY.AppSettings("strAdditionalPointsAction");


                string strDestinatarios = oModel.strEmail;
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

                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(
                    () => {
                        return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); 
                    });

                if (objGetSendEmailResponse.Exit ==  ConstantsHFC.CriterioMensajeOK)
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
                RegisterLog(oModel.IdSession, "AdditionalPointsSave->SendCorreo", "ERROR=>"+ ex.Message);
            }
           
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private FixedTransacService.GenericSotResponse GenerarSOT(Model.HFC.AdditionalPointsModel oModel)
        {

            bool bResult = false;

            string strDesError = string.Empty;
            int codError, intSOT, vtipTra;
            intSOT = ConstantsHFC.numeroCero;
            FixedTransacService.GenericSotRequest objGenericSotRequest = new GenericSotRequest();
            FixedTransacService.GenericSotResponse objGenericSotResponse = new GenericSotResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            try
            {

                string strObservation = string.Empty;


                string vstrSchedule = string.Empty;

                if (oModel.strSchedule == null) 
                {
                    vstrSchedule = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                }
                else 
                {
                    vstrSchedule = oModel.strSchedule;
                }

                if (oModel.strJobTypes.Contains(".|"))
                    oModel.strJobTypes = oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2);

                string aux = "0000000000";
                string vCodRe = (oModel.strRequestActId == null) ? "" : oModel.strRequestActId;


                strObservation = (oModel.strValidateETA == ConstantsHFC.numeroUno.ToString() ? oModel.strNote + "" + vCodRe.PadLeft(10, '0') : oModel.strNote);

                try
                {
                   
                    
                    objGenericSotRequest.audit = audit;
                    objGenericSotRequest.vCuId = oModel.strCustomerId;
                    objGenericSotRequest.vCoId = oModel.strContractId;
                    objGenericSotRequest.vTipTra = Convert.ToInt(oModel.strJobTypes);
                    objGenericSotRequest.vFeProg = Functions.CheckDate(oModel.strDateProgramming).ToShortDateString(); 
                    objGenericSotRequest.vFranja = vstrSchedule;
                    objGenericSotRequest.vCodMotivo = oModel.strMotiveSot;
                    objGenericSotRequest.vPlano = oModel.strCodePlanInst;
                    objGenericSotRequest.vUser = oModel.strlogin;
                    objGenericSotRequest.idTipoServ = oModel.strServicesType;
                    objGenericSotRequest.cargo = string.Empty;
                    objGenericSotRequest.vintCantidadAnexo = Convert.ToInt((oModel.strAttachedQuantity == "-1" || oModel.strAttachedQuantity == string.Empty) ? ConstantsHFC.numeroCero.ToString() : oModel.strAttachedQuantity);
                    objGenericSotRequest.vObserv = strObservation;

                    oModel.strNote = oModel.strNote != null ? oModel.strNote.Replace('|', '-') : string.Empty;
                    if (Functions.CheckInt(oModel.strRequestActId) > ConstantsHFC.numeroCero)
                    {
                        if (oModel.strDateProgramming != null || oModel.strDateProgramming != string.Empty)
                        {
                            if (oModel.strSchedule != null)
                            {
                                objGenericSotRequest.vObserv = oModel.strNote + "|" + ConstantsHFC.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - oModel.strRequestActId.Trim().Length) + oModel.strRequestActId.Trim() + "|";
                            }
                            else
                            {
                                objGenericSotRequest.vObserv = oModel.strNote;
                            }
                        }
                        else
                        {
                            objGenericSotRequest.vObserv = oModel.strNote;
                        }
                    }
                    else
                    {
                        objGenericSotRequest.vObserv = oModel.strNote;
                    }

                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vCuId:" + oModel.strCustomerId);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vCoId:" + oModel.strContractId);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vTipTra:" + oModel.strJobTypes);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vFeProg:" + oModel.strDateProgramming);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vFranja:" + vstrSchedule);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vCodMotivo:" + oModel.strMotiveSot);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vPlano:" + oModel.strCodePlanInst);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vUser:" + oModel.strlogin);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request idTipoServ:" + oModel.strServicesType);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vintCantidadAnexo:" + objGenericSotRequest.vintCantidadAnexo);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "Request vObserv:" + strObservation);

                    objGenericSotResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenericSotResponse>(() => {
                        return _oServiceFixed.GetGenericSOT(objGenericSotRequest);
                    });

                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "response rCodSot:" + objGenericSotResponse.rCodSot);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "response rDescRes:" + objGenericSotResponse.rDescRes);
                    RegisterLog(oModel.IdSession, "AdditionalPointSave->GenerarSOT", "response rCodRes:" + objGenericSotResponse.rCodRes);

                    if (objGenericSotResponse.rCodRes.Equals(ConstantsHFC.numeroUno.ToString())) {
                        bResult = true;
                    }
 
                }
                catch (Exception ex)
                {

                    Claro.Web.Logging.Error(objGenericSotRequest.audit.Session, objGenericSotRequest.audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }
      

                  intNroOST =objGenericSotResponse.rCodSot;
                  oModel.strCodSOT = intNroOST;

                  Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Generacion SOT ", objGenericSotResponse.rCodSot);
                  Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Generacion SOT Mensaje ", objGenericSotResponse.rDescRes);
                  Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Resultado ", objGenericSotResponse.rCodRes);

                  audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

                  CommonTransacService.AuditRequest Common_audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);

                  if (!bResult)
                  {
                      CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request();
                      CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new UpdatexInter30Response();

                      objUpdateInter30Request.p_objid = oModel.strCaseID;
                      objUpdateInter30Request.audit = Common_audit;
                      objUpdateInter30Request.p_texto = Functions.GetValueFromConfigFile("strMensajeDeError",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                      objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                      {
                          return _oServiceCommon.GetUpdatexInter30(objUpdateInter30Request);
                      });

                      oModel.strMessageErrorTransac = objUpdateInter30Request.p_texto;

                  }
                  else {
                      FixedTransacService.UpdateInter29Request objUpdateInter29Request = new UpdateInter29Request();
                      FixedTransacService.UpdateInter29Response objUpdateInter29Response = new UpdateInter29Response();

                      objUpdateInter29Request.p_objid = oModel.strCaseID;
                      objUpdateInter29Request.audit = audit;
                      objUpdateInter29Request.p_texto = objGenericSotResponse.rCodSot; 
                      objUpdateInter29Request.p_orden = ConstantsHFC.PresentationLayer.gstrVariableI;

                      objUpdateInter29Response = Claro.Web.Logging.ExecuteMethod<FixedTransacService.UpdateInter29Response>(() =>
                      {
                          return _oServiceFixed.GetUpdateInter29(objUpdateInter29Request);
                      });


                  }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGenericSotRequest.audit.Session, objGenericSotRequest.audit.transaction, ex.Message);
                objGenericSotResponse = null;
                throw new Exception(audit.transaction);
            }
            return objGenericSotResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="FranjaHorariaETA"></param>
        /// <returns></returns>
        private string GetHourAgendaETA(string IdSession, string FranjaHorariaETA)
        {
            string strHora = string.Empty;
            try
            {
                strHora = Functions.GetValueFromConfigFile("strDefectoHorario", KEY.AppSettings("strConstArchivoSIACUTHFCConfig", ""));
                var listaHorariosAux = Functions.GetListValuesXML("ListaFranjasHorariasETA", "", "HFCDatos.xml");
                foreach (var item in listaHorariosAux)
                {
                    if (FranjaHorariaETA.Split('+')[0] == item.Code)
                    {
                        strHora = item.Code2;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                  
            }
            
            return strHora;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="objTimeZoneVM"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTimeZone(string strIdSession, TimeZoneVM objTimeZoneVM)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetTimeZone ", "entra a GetTimeZone");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();
            
            objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strSeleccionar",KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
            objGenericItem.Codigo = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionMENOSUNO;

            try
            {
                if (objTimeZoneVM.vValidateETA == Claro.SIACU.Transac.Service.Constants.strUno)
                {
                    lstGenericItem = ObtieneFranjasHorarias(objTimeZoneVM, strIdSession);
                }
                else
                {
                    if (objTimeZoneVM.vJobTypes != null)
                    {
                        if (objTimeZoneVM.vJobTypes.Contains(".|"))
                        {
                            if (objTimeZoneVM.vJobTypes != KEY.AppSettings("TipoTrabajo_HFC_RETENCION"))
                            {

                                FixedTransacService.TimeZonesResponseHfc objFranjasHorariasResponseHfc = new TimeZonesResponseHfc();
                                FixedTransacService.TimeZonesRequestHfc objFranjasHorariasRequestHfc = new TimeZonesRequestHfc();

                                FixedTransacService.AuditRequest auditFixed = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

                                try
                                {
                                    objFranjasHorariasRequestHfc.audit = auditFixed;
                                    objFranjasHorariasRequestHfc.strAnTiptra = objTimeZoneVM.vJobTypes.Substring(0, objTimeZoneVM.vJobTypes.Length - 2);
                                    objFranjasHorariasRequestHfc.strAnUbigeo = objTimeZoneVM.vUbigeo;
                                    objFranjasHorariasRequestHfc.strAdFecagenda = objTimeZoneVM.vCommitmentDate;

                                    objFranjasHorariasResponseHfc = Claro.Web.Logging.ExecuteMethod<FixedTransacService.TimeZonesResponseHfc>(() =>
                                    {
                                        return _oServiceFixed.GetTimeZones(objFranjasHorariasRequestHfc);
                                    });



                                    foreach (FixedTransacService.TimeZone _item in objFranjasHorariasResponseHfc.TimeZones)
                                    {
                                        objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

                                        objGenericItem.Agrupador = _item.TIPTRA;
                                        objGenericItem.Codigo = _item.CODCON;
                                        objGenericItem.Codigo2 = _item.CODCUADRILLA;
                                        objGenericItem.Descripcion = _item.HORA;
                                        objGenericItem.Estado = _item.COLOR;

                                        lstGenericItem.Add(objGenericItem);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Error(objFranjasHorariasRequestHfc.audit.Session, objFranjasHorariasRequestHfc.audit.transaction, ex.Message);
                                    throw new Exception(auditFixed.transaction);
                                }
                            }
                            else
                            {
                                lstGenericItem = App_Code.Common.GetXMLList("FranjasHorariasXML");
                            }
                        }
                        else
                        {
                            lstGenericItem = App_Code.Common.GetXMLList("FranjasHorariasXML");

                        }
                    }
                    else
                    {
                        lstGenericItem = App_Code.Common.GetXMLList("FranjasHorariasXML");
                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetTimeZone ", "sale de GetTimeZone");
            
            return Json(new { data = lstGenericItem });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTimeZoneVM"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        private ArrayList ObtieneFranjasHorarias(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ObtieneFranjasHorarias ", "entra a ObtieneFranjasHorarias");

            ArrayList listaHorarios = new ArrayList();
            ArrayList listaHorariosAux = new ArrayList();
            FixedService.GenericItem objHorarioAux = new FixedService.GenericItem();
            FixedTransacService.AuditRequest Audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            string idTran, ipApp, nomAp, usrAp;
            try
            {
                idTran = App_Code.Common.GetTransactionID();
                ipApp = App_Code.Common.GetApplicationIp();
                nomAp = KEY.AppSettings("NombreAplicacion");
                usrAp = App_Code.Common.CurrentUser;

                String objDisponibilidad  = objTimeZoneVM.vSubJobTypes.Split('|')[1];

                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

                int fID = Convert.ToInt(Functions.GetValueFromConfigFile("strDiasConsultaCapacidad",KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));
                DateTime[] dDate = new DateTime[fID];

                dDate[0] = dInitialDate;

                for (int i = 1; i < fID; i++)
                {
                    dInitialDate = dInitialDate.AddDays(1);
                    dDate[i] = dInitialDate;
                }

                Boolean vExistSesion = false;
                string strUbicacion = Functions.GetValueFromConfigFile("strConstArchivoSIACUTHFCConfig", "strCodigoUbicacion");
                string[] vUbicaciones = { strUbicacion };
                string v1, v2, v3, v4, v5, v6, v7, v8;

                v1 = Functions.GetValueFromConfigFile("strCalcDuracion",KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v2 = Functions.GetValueFromConfigFile("strCalcDuracionEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v3 = Functions.GetValueFromConfigFile("strCalcTiempoViaje", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v4 = Functions.GetValueFromConfigFile("strCalcTiempoViajeEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v5 = Functions.GetValueFromConfigFile("strCalcHabTrabajo", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v6 = Functions.GetValueFromConfigFile("strCalcHabTrabajoEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v7 = Functions.GetValueFromConfigFile("strObtenerZonaUbi", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v8 = Functions.GetValueFromConfigFile("strObtenerZonaUbiEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));


                String vHabTra = String.Empty;
                vHabTra = Functions.GetValueFromConfigFile("strCodigoHabilidad", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));

                string[] vEspacioTiempo = { string.Empty };
                string[] HabilidadTrabajo = { vHabTra };


                FixedTransacService.BEETACampoActivityHfc oObj1 = new BEETACampoActivityHfc();
                FixedTransacService.BEETACampoActivityHfc oObj2 = new BEETACampoActivityHfc();
                FixedTransacService.BEETACampoActivityHfc oObj3 = new BEETACampoActivityHfc();
                
                string Aux = "0000000000";

                oObj1.Nombre = Functions.GetValueFromConfigFile("strCampActZonaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj1.Valor = objTimeZoneVM.vHistoryETA.Split('|')[0];

                oObj2.Nombre = Functions.GetValueFromConfigFile("strCampActMapaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj2.Valor = objTimeZoneVM.vHistoryETA.Split('|')[0].PadLeft(10, '0') + objTimeZoneVM.vHistoryETA.Split('|')[1];

                oObj3.Nombre = Functions.GetValueFromConfigFile("strCampActSubtipoCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj3.Valor = objTimeZoneVM.vSubJobTypes.Split('|')[0];


                FixedTransacService.BEETACampoActivityHfc[] oCampoactivity = { oObj1, oObj2, oObj3 };

                FixedTransacService.BEETAParamRequestCapacityHfc oObj2P = new FixedTransacService.BEETAParamRequestCapacityHfc();
                oObj2P.Campo = string.Empty;
                oObj2P.Valor = string.Empty;

                List<BEETAParamRequestCapacityHfc> lst_BEETAParamRequestCapacityHfc = new List<BEETAParamRequestCapacityHfc>();
                lst_BEETAParamRequestCapacityHfc.Add(oObj2P);

                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc oListaPRQ = new BEETAListaParamRequestOpcionalCapacityHfc {
                    ParamRequestCapacities = lst_BEETAParamRequestCapacityHfc
                }; 
               
                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc[] oListaCapcReq = { oListaPRQ };
                FixedTransacService.BEETAAuditoriaResponseCapacityHFC oCapacityResponse = new BEETAAuditoriaResponseCapacityHFC();

                Boolean vOut = false;

                BEETAAuditoriaRequestCapacityHFC objBEETAAuditoriaRequestCapacityHFC = new BEETAAuditoriaRequestCapacityHFC();
                objBEETAAuditoriaRequestCapacityHFC.pIdTrasaccion =  idTran;
                objBEETAAuditoriaRequestCapacityHFC.pIP_APP = ipApp;
                objBEETAAuditoriaRequestCapacityHFC.pAPP = nomAp;
                objBEETAAuditoriaRequestCapacityHFC.pUsuario =  usrAp;
                objBEETAAuditoriaRequestCapacityHFC.vFechas = dDate.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vUbicacion = vUbicaciones.ToList();
              
                if (v1=="1")
                 objBEETAAuditoriaRequestCapacityHFC.vCalcDur =  true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDur = false;

                if (v1 == "2")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = false;

                if (v1 == "3")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = false;

                if (v1 == "4")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = false;

                if (v1 == "5")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = false;

                if (v1 == "6")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = false;

                if (v1 == "7")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = false;

                if (v1 == "8")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = false;

               
                objBEETAAuditoriaRequestCapacityHFC.vEspacioTiempo = vEspacioTiempo.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vHabilidadTrabajo = HabilidadTrabajo.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vCampoActividad = oCampoactivity.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vListaCapReqOpc = oListaCapcReq.ToList();

                objBEETAAuditoriaRequestCapacityHFC.audit = Audit;
                if (_BEETAAuditoriaResponseCapacityHFC != null)
                {
                    if (_BEETAAuditoriaResponseCapacityHFC.ObjetoCapacity != null)
                    {

                        foreach (BEETAEntidadcapacidadType objBEETAEntidadcapacidadType in _BEETAAuditoriaResponseCapacityHFC.ObjetoCapacity)
                        {
                            if (objBEETAEntidadcapacidadType.Fecha == Convert.ToDate(objTimeZoneVM.vCommitmentDate))
                            {
                                oCapacityResponse = _BEETAAuditoriaResponseCapacityHFC;
                                vOut = true;
                                break;
                            }
                        }
                        if (!vOut)
                        {
                            oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                            {
                                return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                            });
                        }
                    }
                    else {

                        oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                        {
                            return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                        });

                        _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;

                    }
                   
                }
                else {
                    oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                    {
                        return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                    });

                    _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;
                }

                var ocap = oCapacityResponse.ObjetoCapacity;
                int vstrDispoMin = Convert.ToInt(Functions.GetValueFromConfigFile("strDisponMinima",KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));

                string sCadAux = string.Empty;


                var listaHorariosAux_ = Functions.GetListValuesXML("ListaFranjasHorariasETA", "", "HFCDatos.xml");
                foreach (var item in listaHorariosAux_)
                {
                    objHorarioAux = new FixedService.GenericItem();
                    objHorarioAux.Codigo = item.Code;
                    objHorarioAux.Descripcion = item.Description;
                    objHorarioAux.Codigo3 = string.Empty;
                    objHorarioAux.Codigo2 = string.Empty;
                    listaHorarios.Add(objHorarioAux);
                }

                int vstrDispoAuxMax;
                string vstrUbicaMax;

                foreach (FixedService.GenericItem item in listaHorariosAux)
                {
                    objHorarioAux = new FixedService.GenericItem();

                    if (ocap != null)
                    {
                        if (ocap.Count > 0)
                        {
                            vstrDispoAuxMax = 0;
                            vstrUbicaMax = string.Empty;

                            objHorarioAux.Descripcion = item.Descripcion;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Codigo;
                            objHorarioAux.Codigo3 = String.Empty;
                            objHorarioAux.Estado = "RED";


                            foreach (BEETAEntidadcapacidadType oent in ocap)
                            {
                                objHorarioAux.Descripcion2 = oent.Disponible.ToString();
                                if (item.Codigo == oent.EspacioTiempo && dDate[0] == oent.Fecha) {
                                    if (vstrDispoMin <= Convert.ToInt(oent.Disponible)) {
                                        if (Convert.ToInt(objDisponibilidad) < Convert.ToInt(oent.Disponible)) {

                                            if (Convert.ToInt(vstrDispoAuxMax) < Convert.ToInt(oent.Disponible)) {
                                                vstrDispoAuxMax = Convert.ToInt(oent.Disponible);
                                                vstrUbicaMax = oent.Ubicacion;
                                                objHorarioAux.Estado = "WHITE";
                                                objHorarioAux.Codigo3 = oent.Ubicacion;
                                            }
                                        }
                                    }
                                }
                            }

                            listaHorarios.Add(objHorarioAux);
                        }
                        else {
                            objHorarioAux.Descripcion = item.Descripcion;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Codigo;
                            objHorarioAux.Codigo3 = String.Empty;
                            objHorarioAux.Estado = "RED";
                            listaHorarios.Add(objHorarioAux);
                        }
                    }
                    else {
                        objHorarioAux.Descripcion = item.Descripcion;
                        objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                        objHorarioAux.Codigo = item.Codigo;
                        objHorarioAux.Codigo3 = String.Empty;
                        objHorarioAux.Estado = "RED";
                        listaHorarios.Add(objHorarioAux);
                    }
                }

                if (listaHorarios.Count > 0)
                {
                    string idReq = string.Empty;

                    RegistrarHistorialConsultasEta(
                                                    strIdSession,
                                                    ocap,
                                                    Convert.ToString(dDate[0]),
                                                    Convert.ToInt(objTimeZoneVM.vHistoryETA.Split('|')[0]),
                                                    objTimeZoneVM.vHistoryETA.Split('|')[1],
                                                    objTimeZoneVM.vHistoryETA.Split('|')[2],
                                                    objTimeZoneVM.vSubJobTypes.Split('|')[0],
                                                    objDisponibilidad,
                                                    strUbicacion,
                                                    ref idReq
                                                    );

                    foreach (FixedService.GenericItem item in listaHorarios)
                    {
                        item.Codigo2 = idReq;
                    }
 
                }
                else {
                  FixedService.GenericItem  objHorAuxError = new FixedService.GenericItem();
                  objHorAuxError.Codigo = "-1";
                  objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrCarFraHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); 
                  listaHorarios.Add(objHorAuxError);
                }
            }
            catch (Exception)
            {
 
                FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                objHorAuxError.Codigo = "-1";
                objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrorWs", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                listaHorarios.Add(objHorAuxError);
                return listaHorarios;
               
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ObtieneFranjasHorarias ", "sale de ObtieneFranjasHorarias");

            return listaHorarios;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="ocapacity"></param>
        /// <param name="fecha"></param>
        /// <param name="cod_zona"></param>
        /// <param name="cod_plano"></param>
        /// <param name="codTipoOrden"></param>
        /// <param name="codSubTipoOrden"></param>
        /// <param name="vTiempoTrabajo"></param>
        /// <param name="strUbicacion"></param>
        /// <param name="idRequest"></param>
        /// <returns></returns>
        public static string RegistrarHistorialConsultasEta(string IdSession, List<BEETAEntidadcapacidadType> ocapacity, string fecha,int cod_zona, string cod_plano,string codTipoOrden, string codSubTipoOrden, string vTiempoTrabajo, string strUbicacion, ref string idRequest)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "Transaction: RegistrarHistorialConsultasEta ", "entra a RegistrarHistorialConsultasEta");

            string strResp = string.Empty;
            string strMsg = string.Empty;
            int vIdReq = 0;

            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(IdSession);
            FixedTransacService.RegisterEtaRequest objRegisterEtaRequest = new FixedTransacService.RegisterEtaRequest();
            FixedTransacService.RegisterEtaResponse objRegisterEtaResponse = new FixedTransacService.RegisterEtaResponse();
            objRegisterEtaRequest.vfecha_venta = fecha;
            objRegisterEtaRequest.vcod_zona = cod_zona;
            objRegisterEtaRequest.vcod_plano = cod_plano;
            objRegisterEtaRequest.vtipo_orden = codTipoOrden;
            objRegisterEtaRequest.vsubtipo_orden = codSubTipoOrden;
            objRegisterEtaRequest.vtiempo_trabajo = Convert.ToInt(vTiempoTrabajo);
            objRegisterEtaRequest.vidreturn = vIdReq;
            objRegisterEtaRequest.audit = audit;

           
            try
            {
                vIdReq = Claro.Web.Logging.ExecuteMethod<int>(() =>
                {
                    return new FixedTransacServiceClient().registraEtaRequest(objRegisterEtaRequest);
                });


                try
                {
                    foreach (BEETAEntidadcapacidadType item in ocapacity)
                    {
                        objRegisterEtaResponse.vidconsulta = vIdReq;
                        objRegisterEtaResponse.vdia = item.Fecha;
                        objRegisterEtaResponse.vfranja = item.EspacioTiempo;
                        objRegisterEtaResponse.vestado = Convert.ToInt(item.Disponible);
                        objRegisterEtaResponse.vquota = Convert.ToInt(item.Cuota);
                        objRegisterEtaResponse.vid_bucket = (item.Ubicacion == null ? string.Empty : item.Ubicacion);
                        objRegisterEtaResponse.vresp = string.Empty;

                        strResp = Claro.Web.Logging.ExecuteMethod<string>(() =>
                        {
                            return new FixedTransacServiceClient().registraEtaResponse(objRegisterEtaResponse);
                        });

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objRegisterEtaRequest.audit.Session, objRegisterEtaRequest.audit.transaction, ex.Message);
                }
                

                idRequest = vIdReq.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.audit.Session, objRegisterEtaRequest.audit.transaction, ex.Message);
                throw new Exception(objRegisterEtaRequest.audit.transaction);
            }

            Claro.Web.Logging.Info("Session: " + IdSession, "Transaction: RegistrarHistorialConsultasEta ", "sale de RegistrarHistorialConsultasEta, mensaje: " + strMsg);

            return strMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public  Model.TemplateInteractionModel GetDatTemplateInteractionAdditionalPoints(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetDatTemplateInteractionAdditionalPoints ", "entra a GetDatTemplateInteractionAdditionalPoints");
             
            var oPlantCampDat = new Model.TemplateInteractionModel();
            oPlantCampDat.X_MARITAL_STATUS = DateTime.Now.ToShortDateString();
            oPlantCampDat.X_INTER_1 = oModel.strAmount; 
            oPlantCampDat.X_INTER_2 = HttpContext.Server.HtmlEncode(oModel.strLegalDepartament); 
            oPlantCampDat.X_INTER_4 = HttpContext.Server.HtmlEncode(oModel.strLegalProvince);  
            oPlantCampDat.X_INTER_6 = HttpContext.Server.HtmlEncode(oModel.strLegalDistrict);
            oPlantCampDat.X_INTER_15 = HttpContext.Server.HtmlEncode(oModel.strDescCacDac);  
            oPlantCampDat.X_INTER_16 = oModel.strCodSOT;
            oPlantCampDat.X_INTER_29 = oModel.strCodSOT;  
            oPlantCampDat.X_INTER_18 = HttpContext.Server.HtmlEncode(oModel.strTelephone);
            oPlantCampDat.X_DISTRICT = HttpContext.Server.HtmlEncode(oModel.strLegalBuilding);
            oPlantCampDat.X_INTER_17 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strJobTypes) || oModel.strJobTypes == "-1") ? "" : oModel.strDescJobType);
            oPlantCampDat.X_INTER_19 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strMotiveSot) || oModel.strMotiveSot == "-1") ? "" : oModel.strDescMotive); 
            oPlantCampDat.X_INTER_20 = HttpContext.Server.HtmlEncode(oModel.strDateProgramming);
            oPlantCampDat.X_INTER_21 =HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strServicesType) || oModel.strServicesType == "-1") ? "" : oModel.strDescServicesType);
            oPlantCampDat.X_INTER_22 = (string.IsNullOrEmpty(oModel.strAttachedQuantity) || oModel.strAttachedQuantity == "-1") ? 0 : Convert.ToDouble(oModel.strAttachedQuantity);

            if (oModel.iFidelidad.ToString() == Claro.Constants.NumberOneString)
            {
                oModel.strFidelidad = ConstantsSiacpo.SiDatoValorTexto;
            }
            else
            {
                oModel.strFidelidad = ConstantsSiacpo.NoDatoValorTexto;
            }
            oPlantCampDat.X_INTER_3 = oModel.strFidelidad;
            oPlantCampDat.X_INTER_30 = HttpContext.Server.HtmlEncode(oModel.strNote);
            oPlantCampDat.X_FIRST_NAME = HttpContext.Server.HtmlEncode(oModel.strFirstName); 
            oPlantCampDat.X_LAST_NAME = HttpContext.Server.HtmlEncode(oModel.strLastName); 
            oPlantCampDat.X_DOCUMENT_NUMBER = HttpContext.Server.HtmlEncode(oModel.strDocumentNumber);
            oPlantCampDat.X_CLAROLOCAL6 = KEY.AppSettings("strAdditionalPointsAction", "");

            if (oModel.bSendMail) {
                if (!string.IsNullOrEmpty(oModel.strEmail))
                {
                    oPlantCampDat.X_REGISTRATION_REASON = oModel.strEmail;
                    oPlantCampDat.X_INTER_5 = "1"; 
                }
                else {
                    oPlantCampDat.X_REGISTRATION_REASON = string.Empty;
                    oPlantCampDat.X_INTER_5 = "0"; 
                }
            }
 
            oPlantCampDat.X_CLARO_NUMBER = HttpContext.Server.HtmlEncode(oModel.strContractId); 
            oPlantCampDat.X_TYPE_DOCUMENT = HttpContext.Server.HtmlEncode(oModel.strDocumentType); 
            oPlantCampDat.X_ADDRESS5 = HttpContext.Server.HtmlEncode(oModel.strAddressInst); 
            oPlantCampDat.X_CITY = HttpContext.Server.HtmlEncode(oModel.strUbigeoInst);
            oPlantCampDat.X_ADDRESS = oModel.strAddress;
            oPlantCampDat.X_CLAROLOCAL2 = HttpContext.Server.HtmlEncode(oModel.strCountry);
            oPlantCampDat.X_INTER_7 = oModel.strDateProgramming;
            oPlantCampDat.X_CLAROLOCAL1 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strJobTypes) || oModel.strJobTypes == "-1") ? "" : oModel.strDescJobType);
            oPlantCampDat.X_CLAROLOCAL4 = HttpContext.Server.HtmlEncode(oModel.strBusinessName);
            oPlantCampDat.X_NAME_LEGAL_REP = oModel.strLegalRepresent;
            oPlantCampDat.X_DEPARTMENT = oModel.strLegalDepartament;
            oPlantCampDat.X_CLAROLOCAL3 = oModel.strPostalCode;
            oPlantCampDat.X_TYPE_DOCUMENT = oModel.strtypeCliente;
            oPlantCampDat.X_CLARO_LDN1 = HttpContext.Server.HtmlEncode(oModel.strDocumentType);

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetDatTemplateInteractionAdditionalPoints ", "sale de GetDatTemplateInteractionAdditionalPoints");

            return oPlantCampDat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public JsonResult MessagePartialView(string strIdSession)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: MessagePartialView ", "entra a MessagePartialView");

            List<string> Item = new List<string>();
            Item.Add(Functions.GetValueFromConfigFile("strMensajeSeleTTra", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeSeleFAge", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeErrCodUbi", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeAgenVali", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeAgenNoVali", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeSeleSubTipOrd", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(Functions.GetValueFromConfigFile("strMensajeSeleFranjaDispo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            Item.Add(KEY.AppSettings("strIdQuintaFranja"));
            Item.Add(KEY.AppSettings("strTimerFranja"));

            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: MessagePartialView ", "sale de MessagePartialView Item" + Item);

            return Json(Item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private FixedTransacService.ValidateCustomerIdResponse GetValidaCustomerID(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidaCustomerID ", "entra a GetValidaCustomerID");

            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            FixedTransacService.ValidateCustomerIdResponse oValidateCustomerIdResponse = new ValidateCustomerIdResponse();
            FixedTransacService.ValidateCustomerIdRequest oValidateCustomerIdRequest = new FixedTransacService.ValidateCustomerIdRequest();
            oValidateCustomerIdRequest.audit = _audit;
            oValidateCustomerIdRequest.Phone = KEY.AppSettings("gConstKeyCustomerInteract")  + oModel.strCustomerId;
            try
            {
                oValidateCustomerIdResponse =
                           Claro.Web.Logging.ExecuteMethod<FixedTransacService.ValidateCustomerIdResponse>(() =>
                           {
                               return _oServiceFixed.GetValidateCustomerId(oValidateCustomerIdRequest);
                           });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, oValidateCustomerIdRequest.audit.transaction, ex.Message);
                throw new Exception(_audit.transaction);
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidaCustomerID ", "sale de GetValidaCustomerID");

            return oValidateCustomerIdResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <param name="ContactObjId"></param>
        /// <returns></returns>
        private FixedTransacService.CustomerResponse GetRegisterCustomerId(Model.HFC.AdditionalPointsModel oModel, int ContactObjId=0)
        {
            FixedTransacService.CustomerResponse objkResponse;
            FixedTransacService.AuditRequest audit =
            App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            FixedTransacService.Customer objRequest = new FixedTransacService.Customer();
            objRequest.audit = audit;
            objRequest.Telephone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + oModel.strCustomerId;
            objRequest.User = oModel.strlogin;
            objRequest.Name = HttpContext.Server.HtmlEncode(oModel.strFullName);
            objRequest.LastName = HttpContext.Server.HtmlEncode(oModel.strLastName);
            objRequest.BusinessName = HttpContext.Server.HtmlEncode(oModel.strBusinessName);
            objRequest.DocumentType = HttpContext.Server.HtmlEncode(oModel.strDocumentType);
            objRequest.DocumentNumber = HttpContext.Server.HtmlEncode(oModel.strDocumentNumber);
            objRequest.Address = HttpContext.Server.HtmlEncode(oModel.strAddress);
            objRequest.District = HttpContext.Server.HtmlEncode(oModel.strDistrict);
            objRequest.Departament = HttpContext.Server.HtmlEncode(oModel.strDepartament);
            objRequest.Province = HttpContext.Server.HtmlEncode(oModel.strProvince);
            objRequest.ContactCode = ContactObjId.ToString();
            
            objRequest.Modality = HttpContext.Server.HtmlEncode(((oModel.strModality == null) ? "" : oModel.strModality));
            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.CustomerResponse>(() =>
                    {
                        return _oServiceFixed.GetRegisterCustomerId(objRequest);
                    });

                if (objkResponse.vFlagConsulta == KEY.AppSettings("gConstKeyStrResultInsertCusID"))
                {
                    objkResponse.Resultado = false;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return objkResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private bool GetValidateCustomerID(Model.HFC.AdditionalPointsModel oModel) {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidateCustomerID ", "entra a GetValidateCustomerID");

         bool bResult = true;
         FixedTransacService.ValidateCustomerIdResponse oValidateCustomerIdResponse = new FixedTransacService.ValidateCustomerIdResponse();
         FixedTransacService.CustomerResponse oCustomerResponse = new FixedTransacService.CustomerResponse();
            
            oValidateCustomerIdResponse = GetValidaCustomerID(oModel);
             if (!oValidateCustomerIdResponse.resultado)
             {
                int ContactObjId = oValidateCustomerIdResponse.ContactObjID;
                 oCustomerResponse = GetRegisterCustomerId(oModel);
                 if (!oCustomerResponse.Resultado)
                 {
                     bResult = false;

                 }
                 oModel.strMessageErrorTransac = oCustomerResponse.rMsgText;
             }

             Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetValidateCustomerID ", "sale de GetValidateCustomerID con valor de bResult" + Functions.CheckStr(bResult));

         return bResult;
        }
     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        private void GenerateContancy(Models.HFC.AdditionalPointsModel oModel)
        {
            Models.HFC.AdditionalPointsModel oAdditionalPointsModel = new Model.HFC.AdditionalPointsModel();
            GenerateConstancyResponseCommon response = null;
            try
            {

                    RegisterLog(oModel.IdSession, "GenerateContancy", "strCaseID: " + oModel.strCaseID);

                    if (string.IsNullOrEmpty(oModel.strCaseID)) {
                        throw new Exception("No se ha encontrado el codigo de transacción");
                    }


                    var getDatTemplateInteraction = GetInfoInteractionTemplate(oModel.IdSession, oModel.strCaseID);
                    ParametersGeneratePDF parameters = new ParametersGeneratePDF();

                    parameters.StrFechaTransaccionProgram = getDatTemplateInteraction.X_MARITAL_STATUS;
                    parameters.StrCentroAtencionArea = getDatTemplateInteraction.X_INTER_15;
                    parameters.StrTitularCliente = string.Format("{0} {1}", getDatTemplateInteraction.X_FIRST_NAME,
        getDatTemplateInteraction.X_LAST_NAME);
                    parameters.StrRepresLegal = getDatTemplateInteraction.X_NAME_LEGAL_REP;
                    parameters.StrTipoDocIdentidad = getDatTemplateInteraction.X_CLARO_LDN1;
                    parameters.StrNroDocIdentidad = getDatTemplateInteraction.X_DOCUMENT_NUMBER;
                    parameters.strCasoInteraccion = oModel.strCaseID;
                    parameters.StrCasoInter = oModel.strCaseID;
                    parameters.strContrato = getDatTemplateInteraction.X_CLARO_NUMBER;
                    parameters.strTransaccionDescripcion = KEY.AppSettings("strAdditionalPointsAction", "");
                    parameters.StrCostoTransaccion = (getDatTemplateInteraction.X_INTER_1 == null) ? "0" : getDatTemplateInteraction.X_INTER_1;

                    try
                    {
                        if (getDatTemplateInteraction != null)
                        {
                            if (getDatTemplateInteraction.X_INTER_29.Contains("-"))
                            {
                                string nroSot = getDatTemplateInteraction.X_INTER_29.Split('-')[0];
                                parameters.strNroSot = nroSot;
                            }
                            else
                            {
                                parameters.strNroSot = getDatTemplateInteraction.X_INTER_29;
                            }
                        }
                        else
                        {
                            parameters.strNroSot = string.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        parameters.strNroSot = string.Empty;
                    }    

                    parameters.strCostoTransaccion1 = (getDatTemplateInteraction.X_INTER_1 == null) ? "0" : getDatTemplateInteraction.X_INTER_1;

                    parameters.StrCantidadCc = getDatTemplateInteraction.X_INTER_22.ToString();
                    parameters.strDireccionClienteActual = getDatTemplateInteraction.X_ADDRESS;
                    parameters.StrCodigoLocalA = getDatTemplateInteraction.X_CLAROLOCAL3;

                    parameters.strRefTransaccionActual = oModel.strReference;
                    parameters.strPaisClienteActual = getDatTemplateInteraction.X_CLAROLOCAL2;

                    parameters.strDepClienteActual = getDatTemplateInteraction.X_INTER_2;
                    parameters.strProvClienteActual = getDatTemplateInteraction.X_INTER_4;
                    parameters.strDistritoClienteActual = getDatTemplateInteraction.X_INTER_6;

                    parameters.StrEmail = getDatTemplateInteraction.X_REGISTRATION_REASON;

                    if (getDatTemplateInteraction.X_INTER_5==ConstantsHFC.strUno)
                        parameters.strEnvioCorreo = ConstantsHFC.Variable_SI;
                    else
                        parameters.strEnvioCorreo = ConstantsHFC.Variable_NO; ;


                 
                    parameters.StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                    parameters.StrContenidoComercial2 = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));

                    parameters.StrCarpetaTransaccion = KEY.AppSettings("strFolderAdditionalPointsHFC","");
                    parameters.StrNombreArchivoTransaccion = KEY.AppSettings("StrNombreArchivoTransaccionAdditionalPoint", "");
                    parameters.strAccionEjecutar = KEY.AppSettings("strAdditionalPointsAction","");

                    RegisterLog(oModel.IdSession, "GenerateContancy", "request strFolderAdditionalPointsHFC: " + parameters.StrCarpetaTransaccion);
                    RegisterLog(oModel.IdSession, "GenerateContancy", "request StrNombreArchivoTransaccionAdditionalPoint: " + parameters.StrNombreArchivoTransaccion);
                    RegisterLog(oModel.IdSession, "GenerateContancy", "request strAdditionalPointsAction: " + parameters.strAccionEjecutar);

                    parameters.StrTipoTransaccion = "HFC"; 

                    response = GenerateContancyPDF(oModel.IdSession, parameters);

                    RegisterLog(oModel.IdSession, "GenerateContancy", "Response Generated: " + response.Generated.ToString());
                    RegisterLog(oModel.IdSession, "GenerateContancy", "Response FullPathPDF: " + response.FullPathPDF);
                    RegisterLog(oModel.IdSession, "GenerateContancy", "Response ErrorMessage: " + response.ErrorMessage);

                    oModel.bGeneratedPDF = response.Generated;
                    oModel.strFullPathPDF = response.FullPathPDF;

                    Logging.Info("Persquash", "GenerateContancy",
                        string.Format("Result={0}, fullPathPDF={1} ", response.Generated, response.FullPathPDF));
                    if (!response.Generated)
                    {
                        Logging.Info("Persquash", "GenerateContancy", string.Format("Error={0} ", response.ErrorMessage));
                    }
                   


            }
            catch (Exception ex)
            {
                oAdditionalPointsModel.bErrorTransac = true;
                oAdditionalPointsModel.strMessageErrorTransac = ex.Message;
                RegisterLog(oModel.IdSession, "GenerateContancy", "Response ERROR: " + ex.Message);
                oModel.bGeneratedPDF = false;
                oModel.strFullPathPDF = string.Empty;
            }

        }

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="Method"></param>
        /// <param name="Message"></param>
        private void RegisterLog(string IdSession, string Method, string Message)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "AdditionalPointsHFC", Method + ": " + Message);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string TemplateEmail()
        {
            string strmessage = string.Empty;
            var strHtml = new System.Text.StringBuilder();

            strHtml.Append("<html>");
            strHtml.Append("<head>");
            strHtml.Append("<style type='text/css'>");
            strHtml.Append("<!--");
            strHtml.Append(".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}");
            strHtml.Append(".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}");
            strHtml.Append("-->");
            strHtml.Append("</style>");
            strHtml.Append("<body>");
            strHtml.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>");

            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>Por la presente queremos informarle que su solicitud de Punto Adicional fue registrada y estará siendo atendida en el plazo establecido.</td></tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td align='center'>");
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>&nbsp;</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>Cordialmente</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>Atención al Cliente</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>");
            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }



        public void GrabarCambioDireccionPostal(Model.HFC.AdditionalPointsModel oModel)
        {
                bool salida = false;
                int FlagDirecFact =  ConstantsHFC.numeroCero;

                FixedTransacService.Customer oCustomer = new FixedTransacService.Customer();
                oCustomer.CustomerID = oModel.strCustomerId;
                FixedTransacService.InsertLoyaltyResponse objInsertLoyaltyResponse = null;
                FixedTransacService.AuditRequest auditFixed = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

                try
                {
                    FixedTransacService.InsertLoyaltyRequest objInsertLoyaltyRequest = new FixedTransacService.InsertLoyaltyRequest()
                    {
                        audit = auditFixed,
                        oCustomer = oCustomer,
                        vCodSoLot = oModel.strCodSOT,
                        vFlagDirecFact = FlagDirecFact,
                        vUser = oModel.strCurrentUser,
                        vFechaReg = DateTime.Now
                    };
                    objInsertLoyaltyRequest.oCustomer = oCustomer;
                    objInsertLoyaltyResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertLoyaltyResponse>(() =>
                    {
                        return _oServiceFixed.GetInsertLoyalty(objInsertLoyaltyRequest);
                    });

                    salida = objInsertLoyaltyResponse.rSalida;
                }
                catch (Exception ex)
                {
                    RegisterLog(oModel.IdSession, "GrabarCambioDireccionPostal-ERROR", ex.Message);
                }
        }
        public void GrabarRegistroOCC(Model.HFC.AdditionalPointsModel oModel)
        {
            RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "INICIO");
            bool salida = false;
          //  string sMonto = (decimal.Round((1 - Functions.CheckDecimal(oModel.strIGV)) * Functions.CheckDecimal(oModel.strAmount), 2)).ToString(); // SIN - IGV
            if (string.IsNullOrEmpty(oModel.strAmount)) oModel.strAmount = "0";
            if (string.IsNullOrEmpty(oModel.strIGV)) oModel.strIGV = "1";
            string sMonto = Math.Round((Convert.ToDouble(oModel.strAmount) / Convert.ToDouble(oModel.strIGV)), 2).ToString();

            sMonto = Functions.CheckDecimal(sMonto) > 0 ? sMonto : ConstantsHFC.PresentationLayer.NumeracionCERODECIMAL2;

            int sMes = DateTime.Now.Month + 1;
            int sAnio = DateTime.Now.Year;
            if (sMes == 13)
            {
                sMes = 1;
                sAnio = sAnio + 1;
            }

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            try
            {
                string sFecha = oModel.strCicloFact + "/" + sMes.ToString() + "/" + sAnio;
                string sComentario = ConfigurationManager.AppSettings("gConstComentarioPuntoAdicional");
                int Flag = 1;
                string strNomApp = ConfigurationManager.AppSettings("NombreAplicacion");
                DateTime FechaAct = DateTime.Now;
                FixedTransacService.SaveOCCResponse objSaveOCCResponse = null;
                FixedTransacService.SaveOCCRequest objSaveOCCRequest = new FixedTransacService.SaveOCCRequest()
                {
                    audit = audit,
                    vCodSot = Functions.CheckInt(oModel.strCodSOT),
                    vCustomerId = Functions.CheckInt(oModel.strCustomerId),
                    vFechaVig = Functions.CheckDate(sFecha),
                    vMonto = Functions.CheckDbl(sMonto),
                    vComentario = sComentario,
                    vflag = Flag,
                    vAplicacion = strNomApp,
                    vUsuarioAct = oModel.strCurrentUser,
                    vFechaAct = FechaAct,
                    vCodId = oModel.strContractId,
                };
 
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vCodSot" + objSaveOCCRequest.vCodSot);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vCustomerId" + objSaveOCCRequest.vCustomerId);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vFechaVig" + objSaveOCCRequest.vFechaVig);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vMonto" + objSaveOCCRequest.vMonto);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vComentario" + objSaveOCCRequest.vComentario);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vflag" + objSaveOCCRequest.vflag);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vAplicacion" + objSaveOCCRequest.vAplicacion);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vUsuarioAct" + objSaveOCCRequest.vUsuarioAct);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vUsuarioAct" + objSaveOCCRequest.vUsuarioAct);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vFechaAct" + objSaveOCCRequest.vFechaAct);
                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "vCodId" + objSaveOCCRequest.vCodId);

                objSaveOCCResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.SaveOCCResponse>(() =>
                {
                    return _oServiceFixed.GetSaveOCC(objSaveOCCRequest);
                });

                salida = objSaveOCCResponse.rSalida;

                RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "OK");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(oModel.IdSession, "", "OUT GrabarRegistroOCC() - HFC");
        }

	}
}