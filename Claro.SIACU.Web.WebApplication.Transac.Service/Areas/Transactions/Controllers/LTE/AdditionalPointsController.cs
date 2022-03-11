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
using Helpers = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using Util = Claro.SIACU.Transac.Service.DataUtil;
using FixedService = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Transac.Service;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class AdditionalPointsController : CommonServicesController
    {
        private readonly PostTransacService.PostTransacServiceClient _oServicePostpaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        private static string intNroOST = string.Empty;
        private static FixedTransacService.BEETAAuditoriaResponseCapacityHFC _BEETAAuditoriaResponseCapacityHFC = new BEETAAuditoriaResponseCapacityHFC();

        public ActionResult LTEAdditionalPoints()
        {
            int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetParameter()
        {
            Claro.Web.Logging.Info("Session: 123456", "Transaction: GetParameter ", "Entra a GetParameter");

            Model.LTE.AdditionalPointsModel oModel = new Model.LTE.AdditionalPointsModel();

            if (Request.IsAjaxRequest())
            {
                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "Ingreso al IsAjaxRequest");
                try
                {
                    oModel.strMensajeErrorConsultaIGV = KEY.AppSettings("strMensajeErrorConsultaIGV","");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMensajeErrorConsultaIGV" + oModel.strMensajeErrorConsultaIGV);

                    RegisterLog(oModel.IdSession, "GetParameter", "Obteniendo Valores WebConfig y Archivos XML");

                    oModel.strServerName = Request.ServerVariables["SERVER_NAME"];
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strServerName :" + oModel.strServerName);

                    oModel.strLocalAddress = Request.ServerVariables["LOCAL_ADDR"];
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strLocalAddress :" + oModel.strLocalAddress);
                    
                    oModel.strHostName = Request.UserHostName;
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strHostName :" + oModel.strHostName);

                    oModel.strTitlePageAdditionalPoints = KEY.AppSettings("gConstKeyTituloTranGenVisTecMan", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strTitlePageAdditionalPoints :" + oModel.strTitlePageAdditionalPoints);

                    oModel.strMessageConfirmAdditionsPoints = KEY.AppSettings("gConstKeyPreguntaGenVisTecMan", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageConfirmAdditionsPoints :" + oModel.strMessageConfirmAdditionsPoints);

                    oModel.strMessageEnterMail = Functions.GetValueFromConfigFile("gConstKeyIngreseCorreo", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageEnterMail :" + oModel.strMessageEnterMail);

                    oModel.strMessageValidateMail = Functions.GetValueFromConfigFile("gConstKeyCorreoIncorrecto", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidateMail :" + oModel.strMessageValidateMail);

                    oModel.strMessageValidatePointCare = Functions.GetValueFromConfigFile("gConstMsgSelCacDac", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidatePointCare :" + oModel.strMessageValidatePointCare);

                    oModel.strMessageValidatePhone = Functions.GetValueFromConfigFile("gConstMsgTlfDSNum", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidatePhone :" + oModel.strMessageValidatePhone);

                    oModel.strMessageValidateTimeZone = Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidateTimeZone :" + oModel.strMessageValidateTimeZone);

                    oModel.strMessageValidateSchedule = Functions.GetValueFromConfigFile("gConstMsgNVAgendamiento", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidateSchedule :" + oModel.strMessageValidateSchedule);

                    oModel.strDateServer = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strDateServer :" + oModel.strDateServer);

                    int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
                    oModel.strDateNew = DateTime.Now.AddDays(number).ToShortDateString();
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strDateNew :" + oModel.strDateNew);

                    oModel.strCustomerRequestId = KEY.AppSettings("strConstCodigoASolicitudDelClienteLTE", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strCustomerRequestId :" + oModel.strCustomerRequestId);

                    oModel.strJobTypeComplementarySalesLTE = Functions.GetValueFromConfigFile("strCodigoTipoTrabajoLTEPuntoAdicional", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strJobTypeComplementarySalesLTE :" + oModel.strJobTypeComplementarySalesLTE);

                    oModel.strMessageConsultationDisabilityNotAvailable = KEY.AppSettings("gStrMsjConsultaCapacidadNoDisp", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "gStrMsjConsultaCapacidadNoDisp :" + oModel.strMessageConsultationDisabilityNotAvailable);

                    oModel.strMessageOK = Functions.GetValueFromConfigFile("gConstMsgOVExito", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageOK :" + oModel.strMessageOK);

                    oModel.strRouteSiteInitial = KEY.AppSettings("strRutaSiteInicio", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strRutaSiteInicio :" + oModel.strRouteSiteInitial);

                    //oModel.strJobTypeDefault = KEY.AppSettings("strTipTraOrdenVisitaDefaultConfig", "");
                    //RegisterLog(oModel.IdSession, "GetParameter", "strJobTypeDefault :" + oModel.strJobTypeDefault);
                    oModel.strJobTypeDefault = Functions.GetValueFromConfigFile("strCodigoTipoTrabajoLTEPuntoAdicional", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strJobTypeDefault :" + oModel.strJobTypeDefault);
                    
                    oModel.strHourServer = DateTime.Now.ToString("HH:mm");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strHourServer :" + oModel.strHourServer);

                    oModel.strAdditionalPointLTECost = KEY.AppSettings("strAdditionalPointHFCCost", "0.00");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strAdditionalPointLTECost :" + oModel.strAdditionalPointLTECost);
     
                    oModel.strCustomerRequestId = KEY.AppSettings("strConstCodigoASolicitudDelClienteLTE", "");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strCustomerRequestId :" + oModel.strCustomerRequestId);

                    oModel.strMessageNotServiceCableInternet = Functions.GetValueFromConfigFile("strTextoNoTieneServicioCableYOInternetActivoLTE", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageNotServiceCableInternet :" + oModel.strMessageNotServiceCableInternet);
                    
                    oModel.strMessageCustomerContractEmpty = Functions.GetValueFromConfigFile("strMsgConsultaCustomerContratoVacio", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageCustomerContractEmpty :" + oModel.strDateServer);
                    
                    oModel.strMessageValidationETA = KEY.AppSettings("strMessageETAValidation");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "strMessageValidationETA :" + oModel.strMessageValidationETA);

                    oModel.strServicesType = KEY.AppSettings("CodTipServLtePA");
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "Tipo de servicio :" + oModel.strServicesType);
                    
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetParameter", "ERROR :" + ex.Message);
                }

            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetParameter ", "sale de GetParameter");

            return Json(oModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetJobType(string strIdSession)
        {
            RegisterLog(strIdSession, "GetJobType", "Inicio del Evento");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();

            if (Request.IsAjaxRequest())
            {
                RegisterLog(strIdSession, "GetJobType", "Es un Evento Ajax");

                FixedTransacService.JobTypesResponseHfc objJobTypesResponse;
                FixedTransacService.JobTypesRequestHfc objJobTypesRequest = new FixedTransacService.JobTypesRequestHfc
                {
                    p_tipo = Convert.ToInt(oTransacServ.Constants.strUno),
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
                };

                RegisterLog(strIdSession, "GetJobType", "Parametros de Entrada p_tipo: " + oTransacServ.Constants.strUno);

                try
                {
                    objJobTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                    {
                        return new FixedTransacService.FixedTransacServiceClient().GetJobTypeLte(objJobTypesRequest);
                    });

                    RegisterLog(strIdSession, "GetJobType", "Total de Lista de Trabajos LTE: " + objJobTypesResponse.JobTypes.Count);

                    if (objJobTypesResponse.JobTypes != null)
                    {
                        if (objJobTypesResponse.JobTypes.Count > 0)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;

                           string tipTraLTE = Functions.GetValueFromConfigFile("strCodigoTipoTrabajoLTEPuntoAdicional", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));

                           RegisterLog(strIdSession, "GetJobType", "Filtro de Trabajo LTE: " + tipTraLTE);
                           

                           foreach (var item in objJobTypesResponse.JobTypes.Where(x => x.tiptra == tipTraLTE))
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                oGenericItem.Code = item.tiptra;
                                oGenericItem.Description = item.descripcion;
                                lstGenericItem.Add(oGenericItem);

                                RegisterLog(strIdSession, "GetJobType", "Load Filtro de Trabajo Code: " + oGenericItem.Code + " - Description: " + oGenericItem.Description);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                }
            }
            else
            {
                RegisterLog(strIdSession, "GetJobType","NO ES UN EVENTO AJAX");
            }
            return Json(new { data = lstGenericItem });
        }
        [HttpPost]
        public JsonResult GetMotivoSot(string strIdSession)
        {
            RegisterLog(strIdSession, "GetMotivoSot", "Inicio del Evento");

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

                    if (objMotiveSotResponseCommon.getMotiveSot != null)
                    {
                        if (objMotiveSotResponseCommon.getMotiveSot.Count > 0)
                        {
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
            else
            {
                // Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
            }
            return Json(new { data = lstGenericItem });
        }

        [HttpPost]
        public JsonResult GetMotiveSOTByTypeJob2(string strIdSession, string strIdTipoTrabajo) 
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objResponse = new FixedTransacService.MotiveSOTByTypeJobResponse();

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();

            var objRequest = new FixedTransacService.MotiveSOTByTypeJobRequest()
            { 
                audit = audit,
                tipTra = int.Parse(strIdTipoTrabajo)
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetMotiveSOTByTypeJob");
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.MotiveSOTByTypeJobResponse>(() =>
                {
                    return _oServiceFixed.GetMotiveSOTByTypeJob(objRequest);
                });

                if (objResponse != null)
                {
                    if (objResponse.List.Count > 0)
                    {
                        Helpers.CommonServices.GenericItem oGenericItem = null;
                        foreach (var item in objResponse.List)
                        {
                            oGenericItem = new Helpers.CommonServices.GenericItem();
                            oGenericItem.Code = item.Codigo;
                            oGenericItem.Description = item.Descripcion;
                            lstGenericItem.Add(oGenericItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Total Reg : " + objResponse.List.Count);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Reg : " + Newtonsoft.Json.JsonConvert.SerializeObject(objResponse.List));
            return Json(new { data = lstGenericItem });
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
                    return _oServiceFixed.GetMotiveSOTByTypeJob(objRequest);
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
        [HttpPost]
        public JsonResult GetDocumentType(Model.LTE.AdditionalPointsModel oModel)
        {
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
                    //if (vJobTypes.Equals(Functions.GetValueFromConfigFile( "strConstArchivoSIACUTHFCConfig", "strCodigoTipoTrabajoHFCMantenimiento")))
                    //{
                    var GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
                    {
                        return _oServiceCommon.GetDocumentTypeCOBS(oModel.IdSession, audit.transaction, Functions.GetValueFromConfigFile("IdListaTipoServSOT", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));
                    });
                    //}
                    //else if (vJobTypes.Equals(Functions.GetValueFromConfigFile( "strConstArchivoSIACUTHFCConfig", "strIDListaTipoServicioSOTVentaComplementaria")))
                    //{
                    //    GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
                    //    {
                    //        return _oServiceCommon.GetDocumentTypeCOBS(strIdSession, audit.transaction, Functions.GetValueFromConfigFile( "strConstArchivoSIACUTHFCConfig", "strIDListaTipoServicioSOTVentaComplementaria"));
                    //    });
                    //}


                    if (GetDocumentType != null)
                    {
                        if (GetDocumentType.Count > 0)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;

                            foreach (var item in GetDocumentType)
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();

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
            else
            {
                // Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
            }


            return Json(new { data = lstGenericItem }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAttachedQuantity(string strIdSession)
        {
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
            else
            {
                // Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
            }
            return Json(new { data = lstGenericItem });
        }
        [HttpPost]
        public JsonResult GetOrderType(Model.LTE.AdditionalPointsModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetOrderType ", "entra a GetOrderType");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;

            if (Request.IsAjaxRequest())
            {
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
            else {
                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetOrderType", "NO ES UN EVENTO AJAX");
            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: GetOrderType ", "sale de GetOrderType con valores de la lista lstGenericItem " + Functions.CheckStr(lstGenericItem.Count));

            return Json(new { data = lstGenericItem, typeValidate = objResponseValidate });
            }

        [HttpPost]
        public JsonResult GetProductDetailt(Model.LTE.AdditionalPointsModel oModel)
        {
            FixedTransacService.ServicesLteFixedResponse objBEDecoServicesResponseFixed = new ServicesLteFixedResponse();
            if (Request.IsAjaxRequest())
            {
                FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                FixedTransacService.ServicesLteFixedRequest objBEDecoServicesRequestFixed = new ServicesLteFixedRequest();

                try
                {
                    objBEDecoServicesRequestFixed.audit = _audit;
                    objBEDecoServicesRequestFixed.strCustomerId = oModel.strCustomerId;
                    objBEDecoServicesRequestFixed.strCoid = oModel.strContractId;


                    objBEDecoServicesResponseFixed = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ServicesLteFixedResponse>(() =>
                    {
                        return _oServiceFixed.GetServicesLte(objBEDecoServicesRequestFixed);
                    });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, _audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return Json(new { data = objBEDecoServicesResponseFixed.ListServicesLte });
        }
        [HttpPost]
        public JsonResult GetValidateETA(Model.HFC.AdditionalPointsModel oModel)
        {
            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

            if (Request.IsAjaxRequest())
            {
                string p_origen, p_idplano, p_ubigeo, p_tipserv, p_outCodZona, v_TipoOrden;
                int p_tiptra = 0, p_outIndica = 0;

                p_origen = KEY.AppSettings("gConstHFCOrigen");
                p_idplano = oModel.strCodePlanInst; ;
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

                objGenericItem.Codigo = objETAFlowResponseHfc.ETAFlow.an_indica.ToString();
                objGenericItem.Codigo2 = objETAFlowResponseHfc.ETAFlow.as_codzona + "|" + p_idplano + "|" + v_TipoOrden;

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
            else
            {

            }
            return Json(new { data = objGenericItem });
        }

        [HttpPost]
        public JsonResult LoadConstPrint(string strIdSession, string strTranscacion)
        {
            Models.TemplateInteractionModel oTemplateInteractionModel = new Model.TemplateInteractionModel();
            List<string> Item = new List<string>();
            try
            {
                oTemplateInteractionModel = GetInfoInteractionTemplate(strIdSession, strTranscacion);
                Item.Add(ConstantsHFC.numeroUno.ToString());
            }
            catch (Exception)
            {
                Item.Add(ConstantsHFC.numeroCero.ToString());
                Item.Add(Functions.GetValueFromConfigFile("strMensajeProblemaLoad", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                throw;
            }
            return Json(new { data = oTemplateInteractionModel, Items = Item });
        }
        

        [HttpPost]
        public JsonResult AdditionalPointsSave(Model.LTE.AdditionalPointsModel oModel)
        {
            Model.LTE.AdditionalPointsModel oAdditionalPointsModel = new Model.LTE.AdditionalPointsModel();
            oAdditionalPointsModel.bErrorTransac = false;
            string strResult = string.Empty;
            
            if (Request.IsAjaxRequest())
            {
                FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                //FixedTransacService.GenericSotResponse objGenericSotResponse = new GenericSotResponse();
                oModel.strCurrentUser = App_Code.Common.CurrentUser;
                oModel.strCodeTipification = KEY.AppSettings("TRANSACCION_PUNTO_ADICIONAL_LTE", "");

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

                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave", "Guadar Transacción" + string.Empty);

                    var resultInteraction = InsertInteraction(objInteractionModel, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.strCustomerId);

                    var lstaDatTemplate = new List<string>();
                    foreach (KeyValuePair<string, object> par in resultInteraction)
                    {
                        lstaDatTemplate.Add(par.Value.ToString());
                    }

                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave", "Case ID" + lstaDatTemplate[3]);

                    oAdditionalPointsModel.strCaseID = lstaDatTemplate[3];
                    oModel.strCaseID = lstaDatTemplate[3];
                    oAdditionalPointsModel.bErrorTransac = false;
                    oAdditionalPointsModel.bErrorGenericCodSot = false;

                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave", "Transaccion Mensaje" + lstaDatTemplate[0]);

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
                                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GetRegisterEtaSelection", "Inicio");

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
                                        vid_bucket = oModel.strScheduleValue.Split('+')[1]
                                    };
                                    objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return _oServiceFixed.GetInsertETASelection(objInsertETASelectionRequest); });
                                }
                                catch (Exception ex)
                    {
                                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave", ex.Message);
                                    Claro.Web.Logging.Error("Session: " + oModel.IdSession, oModel.strTransaction, ex.Message);
                                }

                                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GetRegisterEtaSelection", "FIN -> Result" + strResult);
                            }
                        }                        
                    }

                    if (oAdditionalPointsModel.strCaseID != string.Empty)
                    {
                        Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GetGenerarSotLTE", "Inicio");
                        FixedTransacService.RegisterTransactionLTEFixedResponse oRegisterTransactionLTEFixedResponse = GetGenerarSotLTE(oModel);
                        Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GetGenerarSotLTE", "FIN");

                        Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rCodSot:" + oRegisterTransactionLTEFixedResponse.intNumSot);
                        Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rCodRes:" + oRegisterTransactionLTEFixedResponse.intResCod);
                        Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "response rDescRes: " + oRegisterTransactionLTEFixedResponse.strResDes);
                        if (string.IsNullOrEmpty(oRegisterTransactionLTEFixedResponse.intNumSot))
                        {
                            RegisterLog(oModel.IdSession, "AdditionalPointsSave->GenerarSOT", "Erro No se genero");
                            throw new System.ArgumentException("No se genero la SOT");
                        }
                        oAdditionalPointsModel.strCodSOT = oRegisterTransactionLTEFixedResponse.intNumSot;
                        oModel.strCodSOT = oRegisterTransactionLTEFixedResponse.intNumSot;

                        oModel.bErrorGenericCodSot = true;
                        oAdditionalPointsModel.bErrorGenericCodSot = true;

                        if (oRegisterTransactionLTEFixedResponse != null)
                        {
                            if (oRegisterTransactionLTEFixedResponse.intResCod == ConstantsHFC.numeroUno)
                            {
                                oModel.bErrorGenericCodSot = false;
                                oAdditionalPointsModel.bErrorGenericCodSot = false;
                                try
                                {
                                    oModel.strCodSOT = oRegisterTransactionLTEFixedResponse.intNumSot;

                                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->SaveCost", "Inicio");
                                    //SaveCostLTE(oModel);
                                    GrabarCambioDireccionPostal(oModel);
                                    GrabarRegistroOCC(oModel);
                                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "AdditionalPointsSave->SaveCost", "FIN");

                                    //if (oRegisterTransactionLTEFixedResponse.intNumSot.Length > 0)
                                    //{
                                    //    if (Functions.CheckInt(oModel.strValidateETA) == ConstantsHFC.numeroUno || Functions.CheckInt(oModel.strValidateETA) == ConstantsHFC.numeroDos)
                                    //    {
                                    //        if (oModel.strDateProgramming != null || oModel.strDateProgramming != string.Empty)
                                    //        {
                                    //            if (oModel.strSchedule != null)
                                    //            {
                                    //                oModel.strCodSOT = oRegisterTransactionLTEFixedResponse.intNumSot;
                                    //                registrarEtaSot(oModel);
                                    //            }
                                    //        }
                                    //    }
                                    //}

                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Error("Session: " + oModel.IdSession, "Transaction: Save Fidelidad ", ex.Message);
                                }

                            }
                            else
                            {
                                oAdditionalPointsModel.bErrorTransac = true;
                                if (string.IsNullOrEmpty(oRegisterTransactionLTEFixedResponse.strResDes))
                                {
                                    oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                                }
                                else
                                {
                                    oAdditionalPointsModel.strMessageErrorTransac = oRegisterTransactionLTEFixedResponse.strResDes;
                                }
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
                                Claro.Web.Logging.Error("Session: " + oModel.IdSession, "Constancy-ERROR", ex.Message);
                            }


                            oAdditionalPointsModel.bGeneratedPDF = oModel.bGeneratedPDF;
                            oAdditionalPointsModel.strFullPathPDF = oModel.strFullPathPDF;
                            if (oModel.bSendMail)
                            {
                                try
                                {
                                    byte[] attachFile = null;
                                    CommonServicesController objCommonServices = new CommonServicesController();
                                    //Nombre del archivo
                                    string strAdjunto = string.IsNullOrEmpty(oModel.strFullPathPDF) ? string.Empty : oModel.strFullPathPDF.Substring(oModel.strFullPathPDF.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                                    var ResultMAil = string.Empty;
                                    if (objCommonServices.DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, oModel.strFullPathPDF, out attachFile))
                                        SendCorreo(oModel, strAdjunto, attachFile);
                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Error("Session: " + oModel.IdSession, "AdditionalPointsSave->SendMail", "Error =>" + ex.Message);
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
                    //Registrar Auditoria
                    SaveAuditM(strTransaction, strService, strText, oModel.strTelephone, oModel.strFullName, oModel.IdSession, oModel.Sn, oModel.IpServidor);
                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: AdditionalPointsSave ", "sale de AdditionalPointsSave");

                    return Json(oAdditionalPointsModel);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                    oAdditionalPointsModel.bErrorTransac = true;
                    oAdditionalPointsModel.strMessageErrorTransac = ex.Message;
                    return Json(oAdditionalPointsModel);
                }
            }
            else
            {

                oAdditionalPointsModel.bErrorTransac = true;
                oAdditionalPointsModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: AdditionalPointsSave ", "sale de AdditionalPointsSave");

                return Json(oAdditionalPointsModel);
            }
        }

        public FixedTransacService.GenerateSOTResponseFixed registrarEtaSot(Model.LTE.AdditionalPointsModel oModel)
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

        private Model.InteractionModel DatInteraction(Model.LTE.AdditionalPointsModel model)
        {
            var objInteractionModel = new Model.InteractionModel();
            try
            {
                //tipificacion
                RegisterLog(model.IdSession, "DatInteraction", "INICIO");
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

                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.Type" + objInteractionModel.Type);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.Class" + objInteractionModel.Class);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.SubClass" + objInteractionModel.SubClass);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.InteractionCode" + objInteractionModel.InteractionCode);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.TypeCode" + objInteractionModel.TypeCode);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.ClassCode" + objInteractionModel.ClassCode);
                RegisterLog(model.IdSession, "DatInteraction", "objInteractionModel.SubClassCode" + objInteractionModel.SubClassCode);


                //objInteractionModel.Type = KEY.AppSettings("TypeLTEAdditionalPoint");
                //objInteractionModel.Class = KEY.AppSettings("ClassLTEAdditionalPoint");
                //objInteractionModel.SubClass = KEY.AppSettings("SubClassLTEAdditionalPoint");
                //objInteractionModel.TypeCode = KEY.AppSettings("TypeCodeLTEAdditionalPoint");
                //objInteractionModel.ClassCode = KEY.AppSettings("ClassCodeLTEAdditionalPoint");
                //objInteractionModel.SubClassCode = KEY.AppSettings("SubClassCodeLTEAdditionalPoint");

                string strFlgRegistrado = ConstantsHFC.strUno;
                //ObtenerCliente
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


                RegisterLog(model.IdSession, "DatInteraction", "ObjidContacto" + objInteractionModel.ObjidContacto);
                RegisterLog(model.IdSession, "DatInteraction", "DateCreaction" + objInteractionModel.DateCreaction);
                RegisterLog(model.IdSession, "DatInteraction", "Telephone" + objInteractionModel.Telephone);
                RegisterLog(model.IdSession, "DatInteraction", "TypeInter" + objInteractionModel.TypeInter);
                RegisterLog(model.IdSession, "DatInteraction", "Method" + objInteractionModel.Method);
                RegisterLog(model.IdSession, "DatInteraction", "Result" + objInteractionModel.Result);
                RegisterLog(model.IdSession, "DatInteraction", "MadeOne" + objInteractionModel.MadeOne);
                RegisterLog(model.IdSession, "DatInteraction", "Note" + objInteractionModel.Note);
                RegisterLog(model.IdSession, "DatInteraction", "FlagCase" + objInteractionModel.FlagCase);
                RegisterLog(model.IdSession, "DatInteraction", "UserProces" + objInteractionModel.UserProces);
                RegisterLog(model.IdSession, "DatInteraction", "Contract" + objInteractionModel.Contract);
                RegisterLog(model.IdSession, "DatInteraction", "Plan" + objInteractionModel.Plan);
                RegisterLog(model.IdSession, "DatInteraction", "Agenth" + objInteractionModel.Agenth);
 
            }
            catch (Exception ex)
            {
                RegisterLog(model.IdSession, "DatInteraction-ERROR", ex.Message);
                
            }
            

            return objInteractionModel;
        }
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
            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            string strTelefono;

            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
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

            //Validacion de Contingencia
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

            return dictionaryResponse;

        }

       
        public ActionResult LTEAdditionalPointsConstPrint()
        {
            ViewData["CaseID"] = "";
            if (Request.QueryString["casoId"] != null)
            {
                ViewData["CaseID"] = Request.QueryString["casoId"];
            }

            ViewData["lblClosing"] = Functions.GetValueFromConfigFile("strTextoClausulaATOV", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            return PartialView();
        }
        private FixedTransacService.GenericSotResponse GenerarSOT(Model.LTE.AdditionalPointsModel oModel)
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

                // fProg = oModel.vdDateProgramming;
                //  fHoria = (oModel.vValidateETA == ConstantsHFC.numeroUno.ToString() ? ObtenerHoraAgendaETA(oModel.IdSession, oModel.vSchedule) : oModel.vSchedule); //IIf(CInt(agenda.GetValidaEta) = NumeracionUNO, ObtenerHoraAgendaETA(), agenda.GetFranja)

                if (oModel.strJobTypes.Contains(".|"))
                    oModel.strJobTypes = oModel.strJobTypes.Substring(0, oModel.strJobTypes.Length - 2);

                //string aux = "0000000000";
                //string vCodRe = (oModel.strRequestActId == null) ? "" : oModel.strRequestActId;


                //strObservation = (oModel.strValidateETA == ConstantsHFC.numeroUno.ToString() ? oModel.strNote + "" + aux.Substring(10 - vCodRe.Length, 10) + vCodRe : oModel.strNote);

                try
                {


                    objGenericSotRequest.audit = audit;
                    objGenericSotRequest.vCuId = oModel.strCustomerId;
                    objGenericSotRequest.vCoId = oModel.strContractId;
                    objGenericSotRequest.vTipTra = Convert.ToInt(oModel.strJobTypes);
                    objGenericSotRequest.vFeProg ="";
                    objGenericSotRequest.vFranja = "";
                    objGenericSotRequest.vCodMotivo = oModel.strMotiveSot;
                    objGenericSotRequest.vPlano = oModel.strCodePlanInst;
                    objGenericSotRequest.vUser = oModel.strlogin;
                    objGenericSotRequest.idTipoServ = oModel.strServicesType;
                    objGenericSotRequest.cargo = string.Empty;
                    objGenericSotRequest.vintCantidadAnexo = Convert.ToInt((oModel.strAttachedQuantity == "-1" || oModel.strAttachedQuantity == string.Empty) ? ConstantsHFC.numeroCero.ToString() : oModel.strAttachedQuantity);
                    objGenericSotRequest.vObserv = strObservation;
                    //RegisterTransactionLTE
                    objGenericSotResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenericSotResponse>(() =>
                    {
                        return _oServiceFixed.GetGenericSOT(objGenericSotRequest);
                    });


                    if (objGenericSotResponse.rCodRes.Equals(ConstantsHFC.numeroUno.ToString()))
                    {
                        bResult = true;
                    }

                }
                catch (Exception ex)
                {

                    Claro.Web.Logging.Error(objGenericSotRequest.audit.Session, objGenericSotRequest.audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }


                intNroOST = objGenericSotResponse.rCodSot;


                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

                CommonTransacService.AuditRequest Common_audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);

                if (!bResult)
                {
                    CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request();
                    CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new UpdatexInter30Response();

                    objUpdateInter30Request.p_objid = oModel.strCaseID;
                    objUpdateInter30Request.audit = Common_audit;
                    objUpdateInter30Request.p_texto = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                    objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                    {
                        return _oServiceCommon.GetUpdatexInter30(objUpdateInter30Request);
                    });

                }
                else
                {
                    FixedTransacService.UpdateInter29Request objUpdateInter29Request = new UpdateInter29Request();
                    FixedTransacService.UpdateInter29Response objUpdateInter29Response = new UpdateInter29Response();

                    objUpdateInter29Request.p_objid = oModel.strCaseID;
                    objUpdateInter29Request.audit = audit;
                    objUpdateInter29Request.p_texto = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
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
      
        public static string RegistrarHistorialConsultasEta(string IdSession, List<BEETAEntidadcapacidadType> ocapacity, string fecha, int cod_zona, string cod_plano, string codTipoOrden, string codSubTipoOrden, string vTiempoTrabajo, string strUbicacion, ref string idRequest)
        {
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
                //vIdReq = Claro.Web.Logging.ExecuteMethod<int>(() =>
                //{
                //    return new FixedTransacServiceClient().registraEtaRequest(objRegisterEtaRequest);
                //});


                //foreach (BEETAEntidadcapacidadType item in ocapacity)
                //{
                //    objRegisterEtaResponse.vidconsulta = vIdReq;
                //    objRegisterEtaResponse.vdia =   item.Fecha.ToString();
                //    objRegisterEtaResponse.vfranja = item.EspacioTiempo;
                //    objRegisterEtaResponse.vestado = Convert.ToInt(item.Disponible);
                //    objRegisterEtaResponse.vquota = Convert.ToInt(item.Cuota);
                //    objRegisterEtaResponse.vid_bucket = (item.Ubicacion == null ? string.Empty : item.Ubicacion);
                //    objRegisterEtaResponse.vresp = string.Empty;

                //    strResp = Claro.Web.Logging.ExecuteMethod<string>(() =>
                //    {
                //        return new FixedTransacServiceClient().registraEtaResponse(objRegisterEtaResponse);
                //    });

                //}

                //idRequest = vIdReq.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.audit.Session, objRegisterEtaRequest.audit.transaction, ex.Message);
                throw new Exception(objRegisterEtaRequest.audit.transaction);
            }
            return strMsg;
        }
        private FixedTransacService.DetailTransExtraResponse SaveCost(FixedTransacService.DetailTransExtraRequest objDetailTransExtraRequest)
        {
            FixedTransacService.DetailTransExtraResponse objDetailTransExtraResponse = new DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.DetailTransExtraResponse>(() =>
                {
                    return new FixedTransacServiceClient().REGISTRA_COSTO_PA(objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailTransExtraRequest.audit.Session, objDetailTransExtraRequest.audit.transaction, ex.Message);
                //throw new Exception(ex.Message);
            }
            return objDetailTransExtraResponse;
        }

        public Model.TemplateInteractionModel GetDatTemplateInteractionAdditionalPoints(Model.LTE.AdditionalPointsModel oModel)
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();
            oPlantCampDat.X_MARITAL_STATUS = DateTime.Now.ToShortDateString();
            oPlantCampDat.X_INTER_1 = oModel.strAmount; // Monto
            oPlantCampDat.X_INTER_2 = HttpContext.Server.HtmlEncode(oModel.strLegalDepartament);
            oPlantCampDat.X_INTER_4 = HttpContext.Server.HtmlEncode(oModel.strLegalProvince);
            oPlantCampDat.X_INTER_6 = HttpContext.Server.HtmlEncode(oModel.strLegalDistrict);
            oPlantCampDat.X_INTER_15 = HttpContext.Server.HtmlEncode(oModel.strDescCacDac);
            oPlantCampDat.X_INTER_16 = HttpContext.Server.HtmlEncode(oModel.strCodSOT);
            oPlantCampDat.X_INTER_29 = HttpContext.Server.HtmlEncode(oModel.strCodSOT);
            oPlantCampDat.X_INTER_18 = HttpContext.Server.HtmlEncode(oModel.strTelephone);
            oPlantCampDat.X_DISTRICT = HttpContext.Server.HtmlEncode(oModel.strLegalBuilding);
            oPlantCampDat.X_INTER_17 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strJobTypes) || oModel.strJobTypes == "-1") ? "" : oModel.strDescJobType);
            oPlantCampDat.X_INTER_19 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strMotiveSot) || oModel.strMotiveSot == "-1") ? "" : oModel.strDescMotive);
            oPlantCampDat.X_INTER_20 = HttpContext.Server.HtmlEncode(oModel.strDateProgramming);
            oPlantCampDat.X_INTER_21 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strServicesType) || oModel.strServicesType == "-1") ? "" : oModel.strDescServicesType);
            oPlantCampDat.X_INTER_22 = (string.IsNullOrEmpty(oModel.strAttachedQuantity) || oModel.strAttachedQuantity == "-1") ? 0 : Convert.ToDouble(oModel.strAttachedQuantity);
 
            //oPlantCampDat.X_INTER_3 = oModel.iFidelidad.ToString(); //Flag de Fidelidad

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

            if (oModel.bSendMail)
            {
                if (!string.IsNullOrEmpty(oModel.strEmail))
                {
                    oPlantCampDat.X_REGISTRATION_REASON = oModel.strEmail;
                    oPlantCampDat.X_INTER_5 = "1"; //Flag Email
                }
                else
                {
                    oPlantCampDat.X_REGISTRATION_REASON = string.Empty;
                    oPlantCampDat.X_INTER_5 = "0"; //Flag Email
                }
            }

            oPlantCampDat.X_CLARO_NUMBER = HttpContext.Server.HtmlEncode(oModel.strContractId);
            oPlantCampDat.X_TYPE_DOCUMENT = HttpContext.Server.HtmlEncode(oModel.strDocumentType);
            oPlantCampDat.X_ADDRESS5 = HttpContext.Server.HtmlEncode(oModel.strAddressInst);
            oPlantCampDat.X_CITY = HttpContext.Server.HtmlEncode(oModel.strUbigeoInst);
            oPlantCampDat.X_ADDRESS = oModel.strAddress;
            oPlantCampDat.X_CLAROLOCAL2 = HttpContext.Server.HtmlEncode(oModel.strCountry);
            oPlantCampDat.X_CLAROLOCAL3 = oModel.strPostalCode;
            oPlantCampDat.X_CLAROLOCAL1 = HttpContext.Server.HtmlEncode((string.IsNullOrEmpty(oModel.strJobTypes) || oModel.strJobTypes == "-1") ? "" : oModel.strDescJobType);
            oPlantCampDat.X_INTER_7 =    oModel.strDateProgramming;
            oPlantCampDat.X_DEPARTMENT = oModel.strLegalDepartament;
            oPlantCampDat.X_CLAROLOCAL4 = HttpContext.Server.HtmlEncode(oModel.strBusinessName);
            oPlantCampDat.X_NAME_LEGAL_REP = oModel.strLegalRepresent;
            oPlantCampDat.X_TYPE_DOCUMENT = oModel.strtypeCliente;
            oPlantCampDat.X_CLARO_LDN1 = HttpContext.Server.HtmlEncode(oModel.strDocumentType);

            return oPlantCampDat;
        }
        
        private FixedTransacService.ValidateCustomerIdResponse GetValidaCustomerID(Model.LTE.AdditionalPointsModel oModel)
        {
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            FixedTransacService.ValidateCustomerIdResponse oValidateCustomerIdResponse = new ValidateCustomerIdResponse();
            FixedTransacService.ValidateCustomerIdRequest oValidateCustomerIdRequest = new FixedTransacService.ValidateCustomerIdRequest();
            oValidateCustomerIdRequest.audit = _audit;
            oValidateCustomerIdRequest.Phone = KEY.AppSettings("gConstKeyCustomerInteract") + oModel.strCustomerId;
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
            return oValidateCustomerIdResponse;
        }
        private FixedTransacService.CustomerResponse GetRegisterCustomerId(Model.LTE.AdditionalPointsModel oModel)
        {
            FixedTransacService.CustomerResponse objkResponse;
            FixedTransacService.AuditRequest audit =
            App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            FixedTransacService.Customer objRequest = new FixedTransacService.Customer();
            objRequest.audit = audit;
            objRequest.Telephone = KEY.AppSettings("gConstKeyCustomerInteract") + oModel.strCustomerId;
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
            objRequest.Modality = HttpContext.Server.HtmlEncode(oModel.strModality);
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
        private bool GetValidateCustomerID(Model.LTE.AdditionalPointsModel oModel)
        {
            bool bResult = true;
            FixedTransacService.ValidateCustomerIdResponse oValidateCustomerIdResponse = new FixedTransacService.ValidateCustomerIdResponse();
            FixedTransacService.CustomerResponse oCustomerResponse = new FixedTransacService.CustomerResponse();

            oValidateCustomerIdResponse = GetValidaCustomerID(oModel);
            if (oValidateCustomerIdResponse.resultado)
            {
                oCustomerResponse = GetRegisterCustomerId(oModel);
                if (oCustomerResponse.Resultado)
                {
                    bResult = false;
                }
                oModel.strMessageErrorTransac = oCustomerResponse.rMsgText;
            }
            return bResult;
        }



        
        private void GenerateContancy(Models.LTE.AdditionalPointsModel oModel)
        {
            RegisterLog(oModel.IdSession, "GenerateContancy", "Inicio de Generacion de Constancia");

            Models.LTE.AdditionalPointsModel oAdditionalPointsModel = new Model.LTE.AdditionalPointsModel();
            GenerateConstancyResponseCommon response = null;
            try
            {

                //if (Request.IsAjaxRequest())
                //{
                    RegisterLog(oModel.IdSession, "GenerateContancy", "oModel.strCaseID" + oModel.strCaseID);
                    if (string.IsNullOrEmpty(oModel.strCaseID))
                    {
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
                    parameters.strCostoTransaccion1 = (getDatTemplateInteraction.X_INTER_1 == null) ? "0" : getDatTemplateInteraction.X_INTER_1;
                    parameters.StrCantidadCc = getDatTemplateInteraction.X_INTER_22.ToString();
                    parameters.strDireccionClienteActual = getDatTemplateInteraction.X_ADDRESS;
                    parameters.StrCodigoLocalA = getDatTemplateInteraction.X_CLAROLOCAL3;

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
                        else {
                            parameters.strNroSot = string.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        parameters.strNroSot = string.Empty;
                    }
                   
                   
                    //parameters.strRefTransaccionActual = "";
                    parameters.strRefTransaccionActual = oModel.strReference;
                
                    parameters.strPaisClienteActual = getDatTemplateInteraction.X_CLAROLOCAL2;

                    parameters.strDepClienteActual = getDatTemplateInteraction.X_INTER_2;
                    parameters.strProvClienteActual = getDatTemplateInteraction.X_INTER_4;
                    parameters.strDistritoClienteActual = getDatTemplateInteraction.X_INTER_6;

                    parameters.StrEmail = getDatTemplateInteraction.X_REGISTRATION_REASON;

                    if (getDatTemplateInteraction.X_INTER_5 == ConstantsHFC.strUno)
                        parameters.strEnvioCorreo = ConstantsHFC.Variable_SI;
                    else
                        parameters.strEnvioCorreo = ConstantsHFC.Variable_NO;;


                    parameters.StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                    parameters.StrContenidoComercial2 = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                        ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));



                    parameters.StrCarpetaTransaccion = KEY.AppSettings("strFolderAdditionalPointsLTE", "");
                    parameters.StrNombreArchivoTransaccion = KEY.AppSettings("StrNombreArchivoTransaccionAdditionalPoint", "");
                    parameters.strAccionEjecutar = KEY.AppSettings("strAdditionalPointsAction", "");
                    parameters.StrTipoTransaccion = "LTE";

                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Resquest StrCarpetaTransaccion: " + parameters.StrCarpetaTransaccion);
                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Resquest StrNombreArchivoTransaccion: " + parameters.StrNombreArchivoTransaccion);
                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Resquest strAccionEjecutar: " + parameters.strAccionEjecutar);

                     
                    response = GenerateContancyPDF(oModel.IdSession, parameters);
                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Response Generated: " + response.Generated.ToString());
                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Response FullPathPDF: " + response.FullPathPDF);
                    RegisterLog(oModel.IdSession, "GenerateContancyLTE", "Response ErrorMessage: " + response.ErrorMessage);


                    oModel.bGeneratedPDF = response.Generated;
                    oModel.strFullPathPDF = response.FullPathPDF;
                    

                    Logging.Info("Persquash", "GenerateContancy",
                        string.Format("Result={0}, fullPathPDF={1} ", response.Generated, response.FullPathPDF));
                    if (!response.Generated)
                    {
                        Logging.Info("Persquash", "GenerateContancyLTE", string.Format("Error={0} ", response.ErrorMessage));
                    }
                   

                    Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Generacion de Constancia ", "RUTA : " + response.FullPathPDF);
                //}


            }
            catch (Exception ex)
            {
                oAdditionalPointsModel.bErrorTransac = true;
                oAdditionalPointsModel.strMessageErrorTransac = ex.Message;
                RegisterLog(oModel.IdSession, "GenerateContancyLTE", "ERROR" + ex.Message);
            }
            //return Json(response, JsonRequestBehavior.AllowGet);
        }



        public FixedTransacService.RegisterTransactionLTEFixedResponse GetGenerarSotLTE(Model.LTE.AdditionalPointsModel oModel)
        {// Externo

            RegisterLog(oModel.IdSession, "GetGenerarSotLTE", "Inicio de SOT");
            FixedTransacService.RegisterTransactionLTEFixedRequest objRequestGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedRequest();
            FixedTransacService.RegisterTransactionLTEFixedResponse objResponseGenerateSOT = new FixedTransacService.RegisterTransactionLTEFixedResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            FixedTransacService.RegisterTransaction objRegisterTransaction = new FixedTransacService.RegisterTransaction();

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, audit.transaction, string.Format("IN GetRecordTransactionExternal {0}", "Iniciando Proceso"));
            bool boolFlajGenerarSOT = false;
            string vstrSchedule = string.Empty;
            string strObservation = string.Empty;

            if (oModel.strSchedule == null)
            {
                vstrSchedule = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
                vstrSchedule = oModel.strSchedule;
            }

            string vCodRe = (oModel.strRequestActId == null) ? "" : oModel.strRequestActId;
            strObservation = ((oModel.strValidateETA == ConstantsHFC.numeroUno.ToString() || oModel.strValidateETA == ConstantsHFC.numeroDos.ToString()) ? oModel.strNote + "" + vCodRe.PadLeft(10, '0') : oModel.strNote);

            objRegisterTransaction.Cantidad = oModel.strAttachedQuantity;
            objRegisterTransaction.CentroPobladoID = (oModel.strCodePlanInst == null) ? "" : oModel.strCodePlanInst;
            objRegisterTransaction.ConID = (oModel.strContractId == null) ? "" : oModel.strContractId;
            objRegisterTransaction.CustomerID = (oModel.strCustomerId == null) ? "" : oModel.strCustomerId;
            objRegisterTransaction.FechaProg = oModel.strDateProgramming;
            objRegisterTransaction.FranjaHora = vstrSchedule;
            objRegisterTransaction.FranjaHoraID = oModel.strSchedule;
            objRegisterTransaction.InterCasoID = oModel.strCaseID;
            objRegisterTransaction.MotivoID = (oModel.strMotiveSot == null) ? 0 : Convert.ToInt(oModel.strMotiveSot);
            objRegisterTransaction.NroVia = ConstantsHFC.numeroCero;
            objRegisterTransaction.ServicioID = (oModel.strServicesType == null) ? "0" : oModel.strServicesType;
            objRegisterTransaction.TrabajoID = (oModel.strJobTypes == null) ? "0" : oModel.strJobTypes;
            objRegisterTransaction.USRREGIS = (oModel.strlogin == null) ? "" : oModel.strlogin;
            objRegisterTransaction.Observacion = oModel.strNote;
            
            if (oModel.strDateProgramming == null || oModel.strDateProgramming == string.Empty)
            {
                objRegisterTransaction.FechaProgramada = DateTime.Now.ToShortDateString();
            }
            else
            {
                objRegisterTransaction.FechaProgramada = oModel.strDateProgramming;
            }


            oModel.strNote = oModel.strNote != null ? oModel.strNote.Replace('|', '-') : string.Empty;
            if (Functions.CheckInt(oModel.strRequestActId) > ConstantsHFC.numeroCero)
            {
                if (oModel.strDateProgramming != null || oModel.strDateProgramming != string.Empty)
                {
                    if (oModel.strSchedule != null)
                    {
                        objRegisterTransaction.Observacion = oModel.strNote + "|" + ConstantsHFC.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - oModel.strRequestActId.Trim().Length) + oModel.strRequestActId.Trim() + "|";
                    }
                    else
                    {
                        objRegisterTransaction.Observacion = oModel.strNote;
                    }
                }
                else
                {
                    objRegisterTransaction.Observacion = oModel.strNote;
                }
            }
            else
            {
                objRegisterTransaction.Observacion = oModel.strNote;
            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request Cantidad:" + objRegisterTransaction.Cantidad);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request CentroPobladoID:" + objRegisterTransaction.CentroPobladoID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request ConID:" + objRegisterTransaction.ConID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request CustomerID:" + objRegisterTransaction.CustomerID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request FechaProg:" + objRegisterTransaction.FechaProg);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request FranjaHora:" + objRegisterTransaction.FranjaHora);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request FranjaHoraID:" + objRegisterTransaction.FranjaHoraID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request InterCasoID:" + objRegisterTransaction.InterCasoID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request MotivoID:" + objRegisterTransaction.MotivoID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request NroVia:" + objRegisterTransaction.NroVia.ToString());
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request ServicioID:" + objRegisterTransaction.ServicioID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request TrabajoID:" + objRegisterTransaction.TrabajoID);
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "GetGenerarSotLTE", "Request USRREGIS:" + objRegisterTransaction.USRREGIS);

            try
            {
                objRequestGenerateSOT.objRegisterTransaction = objRegisterTransaction;
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.RegisterTransactionLTEFixedResponse>(() => { return _oServiceFixed.LTERegisterTransaction(objRequestGenerateSOT); });


                if (objResponseGenerateSOT.intResCod == ConstantsHFC.numeroUno) {
                    boolFlajGenerarSOT = true;
                }

                RegisterLog(oModel.IdSession, "GetGenerarSotLTE", "Response intNumSot:" + objResponseGenerateSOT.intNumSot);
                RegisterLog(oModel.IdSession, "GetGenerarSotLTE", "Response intResCod:" + objResponseGenerateSOT.intResCod.ToString());
                RegisterLog(oModel.IdSession, "GetGenerarSotLTE", "Response strResDes:" + objResponseGenerateSOT.strResDes);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, oModel.strTransaction, ex.Message);
            }


            audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            CommonTransacService.AuditRequest Common_audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);

            if (!boolFlajGenerarSOT)
            {
                CommonTransacService.UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request();
                CommonTransacService.UpdatexInter30Response objUpdateInter30Response = new UpdatexInter30Response();

                objUpdateInter30Request.p_objid = oModel.strCaseID;
                objUpdateInter30Request.audit = Common_audit;
                objUpdateInter30Request.p_texto = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                objUpdateInter30Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdatexInter30Response>(() =>
                {
                    return _oServiceCommon.GetUpdatexInter30(objUpdateInter30Request);
                });

                oModel.strMessageErrorTransac = objUpdateInter30Request.p_texto;

            }
            else
            {
                FixedTransacService.UpdateInter29Request objUpdateInter29Request = new UpdateInter29Request();
                FixedTransacService.UpdateInter29Response objUpdateInter29Response = new UpdateInter29Response();

                objUpdateInter29Request.p_objid = oModel.strCaseID;
                objUpdateInter29Request.audit = audit;
                objUpdateInter29Request.p_texto = objResponseGenerateSOT.intNumSot;// Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                objUpdateInter29Request.p_orden = ConstantsHFC.PresentationLayer.gstrVariableI;

                objUpdateInter29Response = Claro.Web.Logging.ExecuteMethod<FixedTransacService.UpdateInter29Response>(() =>
                {
                    return _oServiceFixed.GetUpdateInter29(objUpdateInter29Request);
                });


            }

            return objResponseGenerateSOT;
        }

        private void SaveCostLTE(Model.LTE.AdditionalPointsModel oModel)
        {

            try
            {
                FixedTransacService.DetailTransExtraRequest oDetailTransExtraRequest = new DetailTransExtraRequest();
                FixedTransacService.DetailTransExtraResponse oDetailTransExtraResponse = new DetailTransExtraResponse();

                if (string.IsNullOrEmpty(oModel.strAmount)) oModel.strAmount = "0";
                if (string.IsNullOrEmpty(oModel.strIGV)) oModel.strIGV = "1";

                double dbAmountSG = Math.Round((Convert.ToDouble(oModel.strAmount) / Convert.ToDouble(oModel.strIGV)), 2);

                FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                oDetailTransExtraRequest.audit = _audit;
                oDetailTransExtraRequest.CodSolOt = Convert.ToInt(oModel.strCodSOT);
                oDetailTransExtraRequest.iCustomerId = Convert.ToInt(oModel.strCustomerId);
                oDetailTransExtraRequest.iFlag_Cobro = ConstantsHFC.numeroUno;
                oDetailTransExtraRequest.iFlagDireccFact = ConstantsHFC.numeroCero;
                oDetailTransExtraRequest.iMonto = dbAmountSG;
                oDetailTransExtraRequest.vAplicacion = string.Empty;
                oDetailTransExtraRequest.vCodId = oModel.strContractId;
                oDetailTransExtraRequest.vCodigoPostal = string.Empty;
                oDetailTransExtraRequest.vDepartamento = oModel.strDepartament;
                oDetailTransExtraRequest.vDireccionFacturacion = string.Empty;
                oDetailTransExtraRequest.vDistrito = oModel.strDistrict;
                oDetailTransExtraRequest.vFechaReg = DateTime.Now.ToShortDateString();
                oDetailTransExtraRequest.vNotaDireccion = string.Empty;
                oDetailTransExtraRequest.vUsuarioReg = oModel.strlogin;
                oDetailTransExtraRequest.vPais = (oModel.strCountry == null) ? "" : oModel.strCountry;
                oDetailTransExtraRequest.vProvincia = oModel.strProvince;
                oDetailTransExtraRequest.vObservacion = "Cobro de Fidelización Puntos Adicionales LTE";
                oDetailTransExtraRequest.vFechaVig = DateTime.Now.ToShortDateString();

                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request CodSolOt: " + oModel.strCodSOT);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request iCustomerId: " + oModel.strCustomerId);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request iFlag_Cobro: " + ConstantsHFC.numeroUno.ToString());
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request iFlagDireccFact: " + ConstantsHFC.numeroCero.ToString());
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request iMonto: " + oModel.strAmount);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vAplicacion: " + string.Empty);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vCodId : " + oModel.strContractId);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vCodigoPostal: " + string.Empty);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vDepartamento: " + oModel.strDepartament);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vDireccionFacturacion : " + string.Empty);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vDistrito : " + oModel.strDistrict);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vFechaReg : " + DateTime.Now.ToShortDateString());
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vNotaDireccion : " + string.Empty);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vUsuarioReg: " + oModel.strlogin);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vPais: " + oDetailTransExtraRequest.vPais);
                RegisterLog(oModel.IdSession, "SaveCostLTE", "Request vObservacion: Cobro de Fidelización Puntos Adicionales LTE");

                oDetailTransExtraResponse = SaveCost(oDetailTransExtraRequest);

                if (oDetailTransExtraResponse.iResultado == ConstantsHFC.numeroCero)
                {

                    ACTUALIZAR_COSTO_PA(oDetailTransExtraRequest);
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: SaveCost ", "Error: " + ex.Message);
            }

        }

        private FixedTransacService.DetailTransExtraResponse ACTUALIZAR_COSTO_PA(FixedTransacService.DetailTransExtraRequest objDetailTransExtraRequest)
        {
            FixedTransacService.DetailTransExtraResponse objDetailTransExtraResponse = new DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.DetailTransExtraResponse>(() =>
                {
                    return new FixedTransacServiceClient().ACTUALIZAR_COSTO_PA(objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailTransExtraRequest.audit.Session, objDetailTransExtraRequest.audit.transaction, ex.Message);
                //throw new Exception(ex.Message);
            }
            return objDetailTransExtraResponse;
        }


        private void RegisterLog(string IdSession, string Method, string Message)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "AdditionalPointsLTE", Method + ": " + Message);

        }

        private void SendCorreo(Model.LTE.AdditionalPointsModel oModel, string strAdjunto, byte[] attachFile)
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
                string strTIEMailAsunto = KEY.AppSettings("strAdditionalPointsAction");


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
                    () =>
                    {
                        return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest);
                    });

                if (objGetSendEmailResponse.Exit == ConstantsHFC.CriterioMensajeOK)
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
                RegisterLog(oModel.IdSession, "AdditionalPointsSave->SendCorreo", "ERROR=>" + ex.Message);
            }
             
        }

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


        public void GrabarCambioDireccionPostal(Model.LTE.AdditionalPointsModel oModel)
        {
            bool salida = false;
            int FlagDirecFact = ConstantsHFC.numeroCero;

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
        public void GrabarRegistroOCC(Model.LTE.AdditionalPointsModel oModel)
        {
            RegisterLog(oModel.IdSession, "GrabarRegistroOCC", "INICIO");
            bool salida = false;
            

            if (string.IsNullOrEmpty(oModel.strAmount)) oModel.strAmount = "0";
            if (string.IsNullOrEmpty(oModel.strIGV)) oModel.strIGV = "1";

            //double dbAmountSG = Math.Round((Convert.ToDouble(oModel.strAmount) / Convert.ToDouble(oModel.strIGV)), 2);
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