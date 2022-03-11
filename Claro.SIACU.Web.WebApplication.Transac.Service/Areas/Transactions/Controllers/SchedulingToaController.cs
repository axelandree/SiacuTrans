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
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using CONSTANT = Claro.SIACU.Transac.Service;
using Newtonsoft.Json;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class SchedulingToaController : CommonServicesController
    {
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        private static string intNroOST = string.Empty;
        private static FixedService.TransactionScheduled strMsjDispEta = new FixedService.TransactionScheduled();
        private static FixedService.TransactionScheduled strMsjBucket = new FixedService.TransactionScheduled();
        private static FixedService.TransactionScheduled strMsj_Conf_Bkt = new FixedService.TransactionScheduled();
        private FixedTransacService.BEETAAuditoriaResponseCapacityHFC _BEETAAuditoriaResponseCapacityHFC = new BEETAAuditoriaResponseCapacityHFC();


        [HttpPost]
        public JsonResult GetOrderType(Model.SchedulingToaModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "SchedulingToaController", "Metodo: GetOrderType");

            List<Helpers.CommonServices.GenericItem> lstGenericItem = new List<Helpers.CommonServices.GenericItem>();
            if (Request.IsAjaxRequest())
            {
                string strTipoOrdEta = string.Empty;
                OrderTypesResponseHfc objOrderTypesResponse = new OrderTypesResponseHfc();
                OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
                FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
                objOrderTypesRequest.audit = audit;
                OrderSubTypesRequestHfc objSubTypesRequest = new OrderSubTypesRequestHfc();
                OrderSubTypesResponseHfc objOrderSubTypesResponseHfc = new OrderSubTypesResponseHfc();

                if (oModel.StrJobTypes != null)
                {

                    if (oModel.StrJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                        objOrderTypesRequest.vIdtiptra = oModel.StrJobTypes;
                    else
                        objOrderTypesRequest.vIdtiptra = oModel.StrJobTypes.Substring(0, oModel.StrJobTypes.Length - 2);

                    try
                    {
                        objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<OrderTypesResponseHfc>(() =>
                        {
                            return new FixedTransacServiceClient().GetOrderType(objOrderTypesRequest);
                        });

                        if (objOrderTypesResponse.ordertypes != null)
                        {
                            if (objOrderTypesResponse.ordertypes.Count == 0)
                            {
                                strTipoOrdEta = ConstantsHFC.strMenosUno;
                            }
                            else
                            {
                                strTipoOrdEta = objOrderTypesResponse.ordertypes[0].VALOR;
                            }
                        }
                        else
                        {
                            strTipoOrdEta = ConstantsHFC.strMenosUno;
                        }


                        objSubTypesRequest.audit = audit;
                        objSubTypesRequest.av_cod_tipo_orden = strTipoOrdEta;

                        objOrderSubTypesResponseHfc = Claro.Web.Logging.ExecuteMethod<OrderSubTypesResponseHfc>(() =>
                        {
                            return new FixedTransacServiceClient().GetOrderSubType(objSubTypesRequest);
                        });

                        if (objOrderSubTypesResponseHfc.OrderSubTypes != null)
                        {
                            Helpers.CommonServices.GenericItem oGenericItem = null;
                            foreach (var aux in objOrderSubTypesResponseHfc.OrderSubTypes)
                            {
                                oGenericItem = new Helpers.CommonServices.GenericItem();
                                oGenericItem.Code = aux.COD_SUBTIPO_ORDEN + "|" + aux.TIEMPO_MIN;
                                oGenericItem.Description = aux.DESCRIPCION;
                                lstGenericItem.Add(oGenericItem);
                            }
                        }

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

            return Json(new { data = lstGenericItem });
        }

        [HttpPost]
        public JsonResult GetValidateEta(SchedulingToaModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "SchedulingToaController", "Método: GetValidateETA");

            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

            string p_origen, p_idplano, p_ubigeo, p_tipserv, p_outCodZona, v_TipoOrden;
            int p_tiptra = 0, p_outIndica = 0;

            p_origen = KEY.AppSettings("gConstHFCOrigen");
            p_idplano = oModel.StrCodePlanInst;
            p_ubigeo = oModel.StrCodeUbigeo;
            if ((p_idplano == null || p_idplano == "") && (p_ubigeo == null || p_ubigeo == ""))
            {
                objGenericItem.Codigo2 = "";
                objGenericItem.Codigo = "0";
                objGenericItem.Descripcion = "OK";
                return Json(new { data = objGenericItem });
            }


            if (oModel.StrJobTypes != null)
            {
                if (oModel.StrJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                {
                    p_tiptra = Convert.ToInt(oModel.StrJobTypes);
                }
                else
                {
                    p_tiptra = Convert.ToInt(oModel.StrJobTypes.Substring(0, oModel.StrJobTypes.Length - 2));
                }
            }
            

            if (oModel.StrTypeService == null)
            {
                p_tipserv = KEY.AppSettings("gConstHFCTipoServicio");
            }
            else
            {
                p_tipserv = oModel.StrTypeService;
            }


            p_outCodZona = string.Empty;
            p_outIndica = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;

            OrderTypesResponseHfc objOrderTypesResponse = null;
            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
            objOrderTypesRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            if (oModel.StrJobTypes != null)
            {

                if (oModel.StrJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                {
                    objOrderTypesRequest.vIdtiptra = oModel.StrJobTypes;
                }
                else
                {
                    objOrderTypesRequest.vIdtiptra = oModel.StrJobTypes.Substring(0, oModel.StrJobTypes.Length - 2);
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
            ETAFlowRequestHfc objEtaFlowRequestHfc = new ETAFlowRequestHfc();
            ETAFlowResponseHfc objEtaFlowResponseHfc;
            objEtaFlowRequestHfc.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            objEtaFlowRequestHfc.an_tipsrv = p_tipserv;
            objEtaFlowRequestHfc.an_tiptra = p_tiptra;
            objEtaFlowRequestHfc.as_origen = p_origen;
            objEtaFlowRequestHfc.av_idplano = oModel.StrCodePlanInst;
            objEtaFlowRequestHfc.av_ubigeo = p_ubigeo;

            objEtaFlowResponseHfc = Claro.Web.Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
            {
                return new FixedTransacServiceClient().ETAFlowValidate(objEtaFlowRequestHfc);
            });

            if ((p_idplano == null || p_idplano == "") && (p_ubigeo != null || p_ubigeo != ""))
            {
                p_idplano = p_ubigeo;
            }

            objGenericItem.Codigo = objEtaFlowResponseHfc.ETAFlow.an_indica.ToString();

            if (objEtaFlowResponseHfc.ETAFlow.as_codzona != null)
            {
                if (objEtaFlowResponseHfc.ETAFlow.as_codzona.Length > 0)
                {
                    if (objEtaFlowResponseHfc.ETAFlow.as_codzona.ToUpper() != "NULL")
                    {
                        objGenericItem.Codigo2 = objEtaFlowResponseHfc.ETAFlow.as_codzona + "|" + p_idplano + "|" + v_TipoOrden;
                    }
                    else
                    {
                        objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
                    }
                }
                else
                {
                    objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
                }
            }
            else
            {
                objGenericItem.Codigo2 = "|" + p_idplano + "|" + v_TipoOrden;
            }



            switch (objEtaFlowResponseHfc.ETAFlow.an_indica)
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
                case 2:
                    objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.CriterioMensajeOK;
                    break;
                case 0:
                    objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.PresentationLayer.CriterioMensajeNOOK;
                    break;
            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "SchedulingToaController - GetValidateETA ", "objGenericItem.sCodigo: " + Functions.CheckStr(objGenericItem.Codigo));

            return Json(new { data = objGenericItem });
        }

        [HttpPost]
        public JsonResult GetValidateEtaReservation(SchedulingToaModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "SchedulingToaController", "Método: GetValidateEtaReservation");

            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

            string p_tipserv;
            int p_tiptra = 0;


            if (oModel.StrTypeService == null)
            {
                p_tipserv = KEY.AppSettings("gConstHFCTipoServicio");
            }
            else
            {
                p_tipserv = oModel.StrTypeService;
            }

            p_tiptra = Convert.ToInt(oModel.StrJobTypes);

            ETAFlowRequestHfc objEtaFlowRequestHfc = new ETAFlowRequestHfc();
            ETAFlowResponseHfc objEtaFlowResponseHfc;
            objEtaFlowRequestHfc.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            objEtaFlowRequestHfc.an_tipsrv = p_tipserv;
            objEtaFlowRequestHfc.an_tiptra = p_tiptra;

            objEtaFlowResponseHfc = Claro.Web.Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
            {
                return new FixedTransacServiceClient().ETAFlowValidateReservation(objEtaFlowRequestHfc);
            });

            objGenericItem.Codigo = objEtaFlowResponseHfc.ETAFlow.an_indica.ToString();

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "SchedulingToaController - GetValidateETA ", "objGenericItem.sCodigo: " + Functions.CheckStr(objGenericItem.Codigo));

            return Json(new { data = objGenericItem });
        }

        [HttpPost]
        public JsonResult ValidateSchedule(string strIdSession, CommonTransacService.ScheduleRequest objScheduleRequest)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: ValidateSchedule");

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
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ValidateSchedule ", "objGenericItem.Code: " + Functions.CheckStr(objGenericItem.Code));

            return Json(new { data = objGenericItem });
        }

        [HttpPost]
        public JsonResult CancelaReservaToaGenerada(string strIdSession, SchedulingToaModel objScheduleRequest)
        {
            if (objScheduleRequest.StrNroOrden == null) 
            {
                return null;
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: GenerarReservaToa");

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.EtaInboundToaRequest objRequest = new FixedTransacService.EtaInboundToaRequest();
            FixedTransacService.EtaInboundToaResponse objResponse = new FixedTransacService.EtaInboundToaResponse();
            FixedTransacService.InboundEtaPropiedades objPropiedades = new FixedTransacService.InboundEtaPropiedades();
            FixedTransacService.InboundEtaComandos objComandos = new FixedTransacService.InboundEtaComandos();
            FixedTransacService.InboundEtaOrdenTrabajo objOrden = new FixedTransacService.InboundEtaOrdenTrabajo();

            string codResult = string.Empty;
            string mjsResult = string.Empty;
            bool updateResult = false;
            try
            {
                List<string> confSot = new List<string>();
                string configSot = Functions.GetValueFromConfigFile("strConfiguracionSOT", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                confSot.Add(configSot);

                List<string> confInv = new List<string>();
                string configInventario = Functions.GetValueFromConfigFile("strConfiguracionInventario", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                confInv.Add(configInventario);

                objRequest.audit = audit;

                objPropiedades.tipoCarga = Functions.GetValueFromConfigFile("strTipoCarga", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objPropiedades.configuracionSOT = confSot;
                objPropiedades.configuracionInventario = confInv;
                objRequest.propiedades = objPropiedades;

                objComandos.tipoComando = Functions.GetValueFromConfigFile("strTipoComandoCancel", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objRequest.comando = objComandos;

                objOrden.nroOrden = objScheduleRequest.StrNroOrden;
                objRequest.ordenTrabajo = objOrden;

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.EtaInboundToaResponse>(() =>
                {
                    return _oServiceFixed.PostGestionarCancelaToa(objRequest);
                });

                codResult = objResponse.codigoError;
                mjsResult = Functions.GetValueFromConfigFile("strMsjResultCancelToa", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));

                ReservaToa objRequestReserva = new ReservaToa()
                {
                    strNroOrden = objScheduleRequest.StrNroOrden,
                    strValor = CONSTANT.Constants.numeroCero.ToString(),
                    strTipoTransaccion = CONSTANT.Constants.numeroUno
                };

                updateResult = UpdateValueToa(strIdSession, objRequestReserva);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                objResponse.codigoError = CONSTANT.Constants.strMenosUno;
                objResponse.descripcionError = Functions.GetValueFromConfigFile("strMsjResultCancelToaError", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - GenerarReservaToa  ", "sale de GenerarReservaToa");

            return Json(new { codResult = codResult, mjsResult = mjsResult });
        }

        [HttpPost]
        public JsonResult GenerarReservaToa(string strIdSession, SchedulingToaModel objScheduleRequest)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: CancelaReservaToa");

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.EtaInboundToaRequest objRequest = new FixedTransacService.EtaInboundToaRequest();
            FixedTransacService.EtaInboundToaResponse objResponse = new FixedTransacService.EtaInboundToaResponse();
            FixedTransacService.InboundEtaPropiedades objPropiedades = new FixedTransacService.InboundEtaPropiedades();
            FixedTransacService.InboundEtaComandos objComandos = new FixedTransacService.InboundEtaComandos();
            FixedTransacService.InboundEtaOrdenTrabajo objOrden = new FixedTransacService.InboundEtaOrdenTrabajo();
            FixedTransacService.InboundEtaDetalleOrdenTrabajo objOrdenDetalle = new FixedTransacService.InboundEtaDetalleOrdenTrabajo();
            string codResult = string.Empty;
            string mjsResult = string.Empty;
            string nroOrden = string.Empty;
            bool updateResult = false;
            try
            {
                string strZona = objScheduleRequest.StrJobTypes.Split('|')[0];
                string strPlano = objScheduleRequest.StrJobTypes.Split('|')[1];
                string strTipoOrden = objScheduleRequest.StrJobTypes.Split('|')[2];
                string strSubtipoOrden = objScheduleRequest.StrTypeService.Split('|')[0];
                string strDuracion = objScheduleRequest.StrTypeService.Split('|')[1];
                string strFranja = objScheduleRequest.StrFranjaHoraria.Split('+')[0];
                string stridBucket = objScheduleRequest.StrFranjaHoraria.Split('+')[1];

                if (objScheduleRequest.StrNroOrden == "0") 
                {
                    objScheduleRequest.StrNroOrden = "";
                }
                ReservaToa objRequestReserva = new ReservaToa() {
                    strNroOrden = objScheduleRequest.StrNroOrden,
                    strIdConsulta = int.Parse(objScheduleRequest.StrIdConsulta),
                    strFranja = strFranja,
                    strDiaReserva = DateTime.Parse(objScheduleRequest.StrDate),
                    strIdBucket = stridBucket,
                    strCodZona = strZona,
                    strCodPlano = strPlano,
                    strTipoOrden = strTipoOrden,
                    strSubTipoOrden = strSubtipoOrden,
                };
                
                nroOrden = GetNroOrden(strIdSession,objRequestReserva);

                if (nroOrden == CONSTANT.Constants.strMenosUno) 
                {
                    mjsResult = Functions.GetValueFromConfigFile("strMsjResultReservaToa", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    return Json(new { codResult = CONSTANT.Constants.strMenosUno, mjsResult = mjsResult, nroOrden = nroOrden });
                }

                List<string> confSot = new List<string>();
                string configSot = Functions.GetValueFromConfigFile("strConfiguracionSOT", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                confSot.Add(configSot);

                List<string> confInv = new List<string>();
                string configInventario = Functions.GetValueFromConfigFile("strConfiguracionInventario", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                confInv.Add(configInventario);

                objRequest.audit = audit;

                objPropiedades.modoCargaPropiedades = Functions.GetValueFromConfigFile("strModoCargaPropiedadesReplace", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objPropiedades.modoProcesamiento = Functions.GetValueFromConfigFile("strModoProcesamiento", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objPropiedades.tipoCarga = Functions.GetValueFromConfigFile("strTipoCarga", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objPropiedades.configuracionSOT = confSot;
                objPropiedades.configuracionInventario = confInv;
                objPropiedades.fechaTransaccion = DateTime.Now;
                objRequest.propiedades = objPropiedades;

                objComandos.fechaAsignacion = DateTime.Parse(objScheduleRequest.StrDate);
                objComandos.tipoComando = Functions.GetValueFromConfigFile("strTipoComando", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objComandos.idContrata = stridBucket;
                objComandos.idContrataError = Functions.GetValueFromConfigFile("strIdContrataError", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objRequest.comando = objComandos;

                objOrden.nroOrden = nroOrden;
                objOrden.tipoTrabajo = strTipoOrden;
                objOrden.franjasHorariasOrdenTrabajo = strFranja;
                objOrden.duracion = strDuracion;
                objOrden.tiempoRecordatorioMinutos = Functions.GetValueFromConfigFile("strTiempoRecordatorioMinutos", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                
                List<InboundEtaDetalleOrdenTrabajo> listDetail = new List<InboundEtaDetalleOrdenTrabajo>();

                objOrdenDetalle.clave = Functions.GetValueFromConfigFile("strCampActSubtipoCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objOrdenDetalle.valor = strSubtipoOrden;
                listDetail.Add(objOrdenDetalle);

                objOrdenDetalle = new FixedTransacService.InboundEtaDetalleOrdenTrabajo();
                objOrdenDetalle.clave = Functions.GetValueFromConfigFile("strCampActZonaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objOrdenDetalle.valor = strZona;
                listDetail.Add(objOrdenDetalle);

                objOrdenDetalle = new FixedTransacService.InboundEtaDetalleOrdenTrabajo();
                objOrdenDetalle.clave = Functions.GetValueFromConfigFile("strCampActMapaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                objOrdenDetalle.valor = strPlano.PadLeft(10, '0');
                listDetail.Add(objOrdenDetalle);

                objOrden.propiedades = listDetail;
                                
                objRequest.ordenTrabajo = objOrden;

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.EtaInboundToaResponse>(() =>
                {
                    return _oServiceFixed.PostGestionarOrdenesToa(objRequest);
                });

                codResult = objResponse.codigoError;
                mjsResult = Functions.GetValueFromConfigFile("strMsjResultReservaToa", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));

                objRequestReserva = new ReservaToa()
                {
                    strNroOrden = nroOrden,
                    strValor = objResponse.idETA,
                    strTipoTransaccion = CONSTANT.Constants.numeroDos
                };

                updateResult = UpdateValueToa(strIdSession,objRequestReserva);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                objResponse.codigoError = CONSTANT.Constants.strMenosUno;
                objResponse.descripcionError = Functions.GetValueFromConfigFile("strMsjResultReservaToaError", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - CancelaReservaToa  ", "sale de GenerarReservaToa");

            return Json(new { codResult = codResult, mjsResult = mjsResult, nroOrden = nroOrden });
        }

        public string GetNroOrden(string strIdSession, ReservaToa objRequestReserva)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: GetNroOrden");

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.HistoryToaRequest objRequest = new FixedTransacService.HistoryToaRequest();
            FixedTransacService.HistoryToaResponse objResponse = new FixedTransacService.HistoryToaResponse();

            try
            {
                objRequest.audit = audit;
                objRequest.strNroOrden = objRequestReserva.strNroOrden;
                objRequest.strIdConsulta = objRequestReserva.strIdConsulta;
                objRequest.strFranja = objRequestReserva.strFranja;
                objRequest.strDiaReserva = objRequestReserva.strDiaReserva;
                objRequest.strIdBucket = objRequestReserva.strIdBucket;
                objRequest.strCodZona = objRequestReserva.strCodZona;
                objRequest.strCodPlano = objRequestReserva.strCodPlano;
                objRequest.strTipoOrden = objRequestReserva.strTipoOrden;
                objRequest.strSubTipoOrden = objRequestReserva.strSubTipoOrden;

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.HistoryToaResponse>(() =>
                {
                    return _oServiceFixed.GetHistoryToa(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                objResponse.strNroOrden = CONSTANT.Constants.strCero;

            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - GetNroOrden  ", "sale Nro Orden: " + objResponse.strNroOrden);

            return objResponse.strNroOrden;
        }


        public bool UpdateValueToa(string strIdSession, ReservaToa objRequestReserva)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: GetNroOrden");

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.HistoryToaRequest objRequest = new FixedTransacService.HistoryToaRequest();
            FixedTransacService.HistoryToaResponse objResponse = new FixedTransacService.HistoryToaResponse();
            bool result = false;
            try
            {
                objRequest.audit = audit;
                objRequest.strNroOrden = objRequestReserva.strNroOrden;
                objRequest.strValor = objRequestReserva.strValor;
                objRequest.strTipoTransaccion = objRequestReserva.strTipoTransaccion;

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.HistoryToaResponse>(() =>
                {
                    return _oServiceFixed.GetUpdateHistoryToa(objRequest);
                });

                result = true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                objResponse.strNroOrden = CONSTANT.Constants.strCero;

            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - GetNroOrden  ", "sale Nro Orden: " + objResponse.strNroOrden);

            return result;
        }

        [HttpPost]
        public JsonResult GetTimeZone(string strIdSession, TimeZoneVM objTimeZoneVM)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController ", "Método: GetTimeZone");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();

            objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strSeleccionar", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
            objGenericItem.Codigo = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionMENOSUNO;

            try
            {
                if (objTimeZoneVM.vValidateETA != Claro.SIACU.Transac.Service.Constants.strCero)
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
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ValidateSchedule  ", "sale de GetTimeZone");

            return Json(new { data = lstGenericItem });
        }

        private ArrayList ObtieneFranjasHorarias(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController", "ObtieneFranjasHorarias");

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

                String objDisponibilidad = objTimeZoneVM.vSubJobTypes.Split('|')[1];

                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

                int fID = Convert.ToInt(Functions.GetValueFromConfigFile("strDiasConsultaCapacidad", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));
                DateTime[] dDate = new DateTime[fID];

                dDate[0] = dInitialDate;

                for (int i = 1; i < fID; i++)
                {
                    dInitialDate = dInitialDate.AddDays(1);
                    dDate[i] = dInitialDate;
                }

                Boolean vExistSesion = false;
                string strUbicacion = Functions.GetValueFromConfigFile("strCodigoUbicacion", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                string[] vUbicaciones = { strUbicacion };
                string v1, v2, v3, v4, v5, v6, v7, v8;

                v1 = Functions.GetValueFromConfigFile("strCalcDuracion", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
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
                oObj2.Valor = objTimeZoneVM.vHistoryETA.Split('|')[1].PadLeft(10, '0');

                oObj3.Nombre = Functions.GetValueFromConfigFile("strCampActSubtipoCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj3.Valor = objTimeZoneVM.vSubJobTypes.Split('|')[0];


                FixedTransacService.BEETACampoActivityHfc[] oCampoactivity = { oObj1, oObj2, oObj3 };

                FixedTransacService.BEETAParamRequestCapacityHfc oObj2P = new FixedTransacService.BEETAParamRequestCapacityHfc();
                oObj2P.Campo = string.Empty;
                oObj2P.Valor = string.Empty;

                List<BEETAParamRequestCapacityHfc> lst_BEETAParamRequestCapacityHfc = new List<BEETAParamRequestCapacityHfc>();
                lst_BEETAParamRequestCapacityHfc.Add(oObj2P);

                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc oListaPRQ = new BEETAListaParamRequestOpcionalCapacityHfc
                {
                    ParamRequestCapacities = lst_BEETAParamRequestCapacityHfc
                };

                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc[] oListaCapcReq = { oListaPRQ };
                FixedTransacService.BEETAAuditoriaResponseCapacityHFC oCapacityResponse = new BEETAAuditoriaResponseCapacityHFC();

                Boolean vOut = false;

                BEETAAuditoriaRequestCapacityHFC objBEETAAuditoriaRequestCapacityHFC = new BEETAAuditoriaRequestCapacityHFC();
                objBEETAAuditoriaRequestCapacityHFC.pIdTrasaccion = idTran;
                objBEETAAuditoriaRequestCapacityHFC.pIP_APP = ipApp;
                objBEETAAuditoriaRequestCapacityHFC.pAPP = nomAp;
                objBEETAAuditoriaRequestCapacityHFC.pUsuario = usrAp;
                objBEETAAuditoriaRequestCapacityHFC.vFechas = dDate.ToList();

                if (v1 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDur = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDur = false;

                if (v2 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = false;

                if (v3 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = false;

                if (v4 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = false;

                if (v5 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = false;

                if (v6 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = false;

                if (v7 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = false;

                if (v8 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = false;

                int intFlagCamposAdicionales = int.Parse(ConfigurationManager.AppSettings("strFlagCamposAdicionalesConsultaTOA"));
                if (intFlagCamposAdicionales == CONSTANT.Constants.numeroUno)
                {
                    objBEETAAuditoriaRequestCapacityHFC.vUbicacion = vUbicaciones.ToList();
                    objBEETAAuditoriaRequestCapacityHFC.vEspacioTiempo = vEspacioTiempo.ToList();
                    objBEETAAuditoriaRequestCapacityHFC.vHabilidadTrabajo = HabilidadTrabajo.ToList();
                }
                objBEETAAuditoriaRequestCapacityHFC.vCampoActividad = oCampoactivity.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vListaCapReqOpc = oListaCapcReq.ToList();

                objBEETAAuditoriaRequestCapacityHFC.audit = Audit;

                Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Trama Envio Agenda: " + JsonConvert.SerializeObject(objBEETAAuditoriaRequestCapacityHFC));
                
                if (_BEETAAuditoriaResponseCapacityHFC != null)
                {
                    if (_BEETAAuditoriaResponseCapacityHFC.ObjetoCapacity != null)
                    {
                        Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Consume GetETAAuditoriaRequestCapacity");
                            oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                            {
                                return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                            });

                        _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;
                    }
                    else
                    {
                        Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Consume GetETAAuditoriaRequestCapacity");
                        oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                        {
                            return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                        });

                        _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;

                    }

                }
                else
                {
                    Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Consume GetETAAuditoriaRequestCapacity");
                    oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                    {
                        return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                    });

                    _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;
                }

                var ocap = oCapacityResponse.ObjetoCapacity;
                string codOcap = oCapacityResponse.CodigoRespuesta;
                if (ocap == null)
                {
                    string strCorreoCC = (ConfigurationManager.AppSettings("CorreoDestinatarioAgendamientoCC").ToString() == " " ? string.Empty : ConfigurationManager.AppSettings("CorreoDestinatarioAgendamientoCC").ToString());
                    FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                    objHorAuxError.Codigo = CONSTANT.Constants.strMenosDos;
                    listaHorarios.Add(objHorAuxError);
                    if (objTimeZoneVM.vValidateETA == CONSTANT.Constants.strDos)
                    {
                        string MensajeEmail = string.Empty;
                        Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                        {
                            strIdSession = strIdSession,
                            strTo = ConfigurationManager.AppSettings("CorreoDestinatarioAgendamiento"),
                            strSubject = ConfigurationManager.AppSettings("strCorreoAsuntoAgendamiento"),
                            strMessage = BodyEmail(objBEETAAuditoriaRequestCapacityHFC, strMsjDispEta.CODIGOC, objTimeZoneVM),
                            strCC = strCorreoCC
                        };
                        MensajeEmail = GetSendEmailConst(objSendEmail);

                    }
                    return listaHorarios;
                }
                if (codOcap != "0")
                {
                    string strCorreoCC = (ConfigurationManager.AppSettings("CorreoDestinatarioAgendamientoCC").ToString() == " " ? string.Empty : ConfigurationManager.AppSettings("CorreoDestinatarioAgendamientoCC").ToString());
                    if (oCapacityResponse.MensajeRespuesta.Contains(strMsjBucket.DESCRIPCION_DET)) 
                    {
                        FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                        objHorAuxError.Codigo = CONSTANT.Constants.strMenosCuatro;
                        listaHorarios.Add(objHorAuxError);
                        if (objTimeZoneVM.vValidateETA == CONSTANT.Constants.strDos)
                        {
                            string MensajeEmail = string.Empty;
                            Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                            {
                                strIdSession = strIdSession,
                                strTo = ConfigurationManager.AppSettings("CorreoDestinatarioAgendamiento"),
                                strSubject = ConfigurationManager.AppSettings("strCorreoAsuntoAgendamiento"),
                                strMessage = BodyEmail(objBEETAAuditoriaRequestCapacityHFC, strMsjBucket.CODIGOC, objTimeZoneVM),
                                strCC = strCorreoCC
                            };
                            MensajeEmail = GetSendEmailConst(objSendEmail);

                        }
                        return listaHorarios;
                    }
                    else if (oCapacityResponse.MensajeRespuesta.Contains(strMsj_Conf_Bkt.DESCRIPCION_DET))
                    {
                        FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                        objHorAuxError.Codigo = CONSTANT.Constants.strMenosTres;
                        listaHorarios.Add(objHorAuxError);
                        if (objTimeZoneVM.vValidateETA == CONSTANT.Constants.strDos)
                        {
                            string MensajeEmail = string.Empty;
                            Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                            {
                                strIdSession = strIdSession,
                                strTo = ConfigurationManager.AppSettings("CorreoDestinatarioAgendamiento"),
                                strSubject = ConfigurationManager.AppSettings("strCorreoAsuntoAgendamiento"),
                                strMessage = BodyEmail(objBEETAAuditoriaRequestCapacityHFC, strMsj_Conf_Bkt.CODIGOC, objTimeZoneVM),
                                strCC = strCorreoCC
                            };
                            MensajeEmail = GetSendEmailConst(objSendEmail);

                        }
                        return listaHorarios;
                    }
                    else if (oCapacityResponse.MensajeRespuesta.Contains(strMsjDispEta.DESCRIPCION_DET))
                    {
                        FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                        objHorAuxError.Codigo = CONSTANT.Constants.strMenosCinco;
                        listaHorarios.Add(objHorAuxError);
                        if (objTimeZoneVM.vValidateETA == CONSTANT.Constants.strDos)
                        {
                            string MensajeEmail = string.Empty;
                            Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                            {
                                strIdSession = strIdSession,
                                strTo = ConfigurationManager.AppSettings("CorreoDestinatarioAgendamiento"),
                                strSubject = ConfigurationManager.AppSettings("strCorreoAsuntoAgendamiento"),
                                strMessage = BodyEmail(objBEETAAuditoriaRequestCapacityHFC, strMsjDispEta.CODIGOC, objTimeZoneVM),
                                strCC = strCorreoCC
                            };
                            MensajeEmail = GetSendEmailConst(objSendEmail);

                        }
                        return listaHorarios;
                    }
                    else 
                    {
                        FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                        objHorAuxError.Codigo = CONSTANT.Constants.strMenosCuatro;
                        objHorAuxError.Descripcion = ConfigurationManager.AppSettings("srtMsjDefaultErrorFranjaHoraria").ToString();
                        listaHorarios.Add(objHorAuxError);

                        return listaHorarios;
                    }
                    
                }
                if (ocap.Count() == 0)
                {
                    FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                    objHorAuxError.Codigo = CONSTANT.Constants.strMenosUno;
                    objHorAuxError.Descripcion = "Volver a intentar mas tarde";
                    listaHorarios.Add(objHorAuxError);
                    return listaHorarios;
                }

                List<BEETAEntidadcapacidadType> listGenerico = new List<BEETAEntidadcapacidadType>();

                Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Trama completa Bucket: " + JsonConvert.SerializeObject(ocap));

                foreach (var item in ocap)
                {
                    //Validar por fecha porque la franja puede traer varias fechas
                    if (listGenerico.Exists(x => x.EspacioTiempo == item.EspacioTiempo && x.Fecha == item.Fecha))
                    {
                        var dataSelected = listGenerico.Find(x => x.EspacioTiempo == item.EspacioTiempo && x.Fecha == item.Fecha && x.Disponible <= item.Disponible);
                        if (dataSelected != null)
                        {
                            listGenerico.Remove(dataSelected);
                            dataSelected.Cuota = item.Cuota;
                            dataSelected.Disponible = item.Disponible;
                            dataSelected.Ubicacion = item.Ubicacion;
                            listGenerico.Add(dataSelected);
                        }
                    }
                    else
                    {
                        BEETAEntidadcapacidadType data = new BEETAEntidadcapacidadType();
                        data.Cuota = item.Cuota;
                        data.Disponible = item.Disponible;
                        data.EspacioTiempo = item.EspacioTiempo;
                        data.ExtensionData = item.ExtensionData;
                        data.Fecha = item.Fecha;
                        data.HabilidadTrabajo = item.HabilidadTrabajo;
                        data.Ubicacion = item.Ubicacion;
                        listGenerico.Add(data);
                    }
                }

                ocap = listGenerico;

                Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController - ObtieneFranjasHorarias", "Trama filtrada Bucket: " + JsonConvert.SerializeObject(ocap));
                int vstrDispoMin = Convert.ToInt(Functions.GetValueFromConfigFile("strDisponMinima", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));

                string sCadAux = string.Empty;


                var listaHorariosAux_ = Functions.GetListValuesXML("ListaFranjasHorarias", CONSTANT.Constants.strUno, "SiacutData.xml");
                
                int vstrDispoAuxMax;
                string vstrUbicaMax;

                //foreach (FixedService.GenericItem item in listaHorariosAux)
                DateTime dTodayDate = DateTime.Now;
                TimeSpan tTodayTime = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss"));
                //TimeSpan tTodayTime = TimeSpan.Parse("13:30");
                TimeSpan tTimeLast = DateTime.Now.TimeOfDay;
                DateTime dDateSelected = DateTime.Parse(objTimeZoneVM.vCommitmentDate);
                int intNumberBlockedStrips = ConstantsHFC.numeroCero;
                int intValidationRule = ConstantsHFC.numeroUno;
                bool boolBucketError = false;
                bool boolUseHourBreak = false;
                string[] vstrHoraRefrigerio = ConfigurationManager.AppSettings("tHoraRefrigerio").ToString().Split('-');
                string vstrQuintaFranja = ConfigurationManager.AppSettings("strIdQuintaFranja").ToString();
                TimeSpan tInitialBreak = TimeSpan.Parse(vstrHoraRefrigerio[0].ToString());
                TimeSpan tFinalBreak = TimeSpan.Parse(vstrHoraRefrigerio[1].ToString());

                if (objTimeZoneVM.vValidationAbbreviation == ConfigurationManager.AppSettings("srtReglaValidacionLTE").ToString())
                {
                    intValidationRule = 0;
                }
                else if (objTimeZoneVM.vValidationAbbreviation == ConfigurationManager.AppSettings("srtReglaValidacionManttos").ToString())
                {
                    intValidationRule = 0;
                }
                else if (objTimeZoneVM.vValidationAbbreviation == ConfigurationManager.AppSettings("srtReglaValidacionAlta").ToString())
                {
                    intValidationRule = 1;
                }

                foreach (var item in listaHorariosAux_) //Evalenzs
                {
                    objHorarioAux = new FixedService.GenericItem();
                    string[] strRangoHoras = item.Description.Split('-');
                    TimeSpan tInitialRange = TimeSpan.Parse(strRangoHoras[0]);
                    TimeSpan tFinalRange = TimeSpan.Parse(strRangoHoras[1]);

                    if (ocap != null)
                    {
                        if (ocap.Count > 0)
                        {
                            vstrDispoAuxMax = 0;
                            vstrUbicaMax = string.Empty;

                            objHorarioAux.Descripcion = item.Code2; //item.Descripcion;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Code; //item.Codigo;
                            objHorarioAux.Codigo3 = item.Code2;
                            objHorarioAux.Estado = CONSTANT.Constants.strRed;


                            foreach (BEETAEntidadcapacidadType oent in ocap)
                            {
                                objHorarioAux.Descripcion2 = oent.Disponible.ToString();

                                if (item.Code == oent.EspacioTiempo && dDate[0] == oent.Fecha)
                                {
                                    if (vstrDispoMin <= Convert.ToInt(oent.Disponible))
                                    {
                                        if (Convert.ToInt(objDisponibilidad) <= Convert.ToInt(oent.Disponible))
                                        {

                                            if (Convert.ToInt(vstrDispoAuxMax) < Convert.ToInt(oent.Disponible))
                                            {

                                                if (dTodayDate.Date == dDateSelected.Date)
                                                {
                                                    if (vstrQuintaFranja == item.Code && objTimeZoneVM.vIdTipoServicio != ConfigurationManager.AppSettings("strTipoServicioQuintaFranja").ToString())
                                                    {
                                                        continue;                                                      
                                                    }
                                                    else if (tTodayTime >= tFinalRange)
                                                    {
                                                        objHorarioAux.Descripcion = item.Code2;
                                                        objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                                                        objHorarioAux.Codigo = item.Code;
                                                        objHorarioAux.Codigo3 = String.Empty;
                                                        objHorarioAux.Estado = CONSTANT.Constants.strRed;
                                                    }
                                                    else
                                                    {
                                                        if (tInitialRange <= tTodayTime && tTodayTime < tFinalRange)
                                                        {
                                                            intNumberBlockedStrips++;
                                                            tTimeLast = tFinalRange;
                                                            objHorarioAux.Descripcion = item.Code2;
                                                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                                                            objHorarioAux.Codigo = item.Code;
                                                            objHorarioAux.Codigo3 = String.Empty;
                                                            objHorarioAux.Estado = CONSTANT.Constants.strRed;
                                                        }
                                                        else if ((tInitialBreak <= tTodayTime && tTodayTime < tFinalBreak) && (boolUseHourBreak == false))
                                                        {
                                                            intNumberBlockedStrips++;
                                                            if (intNumberBlockedStrips <= intValidationRule)
                                                            {
                                                                intNumberBlockedStrips++;
                                                                boolUseHourBreak = true;
                                                                tTimeLast = tFinalRange;
                                                                objHorarioAux.Descripcion = item.Code2;
                                                                objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                                                                objHorarioAux.Codigo = item.Code;
                                                                objHorarioAux.Codigo3 = String.Empty;
                                                                objHorarioAux.Estado = CONSTANT.Constants.strRed;
                                                            }
                                                            else
                                                            {
                                                                vstrDispoAuxMax = Convert.ToInt(oent.Disponible);
                                                                vstrUbicaMax = oent.Ubicacion;
                                                                objHorarioAux.Estado = CONSTANT.Constants.strWhite;
                                                                objHorarioAux.Codigo3 = oent.Ubicacion;
                                                                objHorarioAux.Valor_C = item.Description.Split('-')[0];
                                                                if (oent.Ubicacion == null || oent.Ubicacion == "")
                                                                {
                                                                    boolBucketError = true;
                                                                }
                                                            }

                                                        }
                                                        else if (intNumberBlockedStrips <= intValidationRule)
                                                        {
                                                            objHorarioAux.Descripcion = item.Code2;
                                                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                                                            objHorarioAux.Codigo = item.Code;
                                                            objHorarioAux.Codigo3 = String.Empty;
                                                            objHorarioAux.Estado = CONSTANT.Constants.strRed;
                                                            intNumberBlockedStrips++;
                                                        }
                                                        else
                                                        {
                                                            vstrDispoAuxMax = Convert.ToInt(oent.Disponible);
                                                            vstrUbicaMax = oent.Ubicacion;
                                                            objHorarioAux.Estado = CONSTANT.Constants.strWhite;
                                                            objHorarioAux.Codigo3 = oent.Ubicacion;
                                                            objHorarioAux.Valor_C = item.Description.Split('-')[0];
                                                            if (oent.Ubicacion == null || oent.Ubicacion == "")
                                                            {
                                                                boolBucketError = true;
                                                            }
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    if (vstrQuintaFranja == item.Code && objTimeZoneVM.vIdTipoServicio != ConfigurationManager.AppSettings("strTipoServicioQuintaFranja").ToString())
                                                    {
                                                        continue;
                                                    }
                                                    vstrDispoAuxMax = Convert.ToInt(oent.Disponible);
                                                    vstrUbicaMax = oent.Ubicacion;
                                                    objHorarioAux.Estado = CONSTANT.Constants.strWhite;
                                                    objHorarioAux.Codigo3 = oent.Ubicacion;
                                                    objHorarioAux.Valor_C = item.Description.Split('-')[0];
                                                    if (oent.Ubicacion == null || oent.Ubicacion == "")
                                                    {
                                                        boolBucketError = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (tInitialRange <= tTodayTime && tTodayTime < tFinalRange)
                                                {
                                                    intNumberBlockedStrips++;
                                                }
                                                else if ((tInitialBreak <= tTodayTime && tTodayTime < tFinalBreak) && (boolUseHourBreak == false))
                                                {
                                                    intNumberBlockedStrips++;
                                                    if (intNumberBlockedStrips <= intValidationRule)
                                                    {
                                                        intNumberBlockedStrips++;
                                                    }
                                                    boolUseHourBreak = true;

                                                }
                                                else if (intNumberBlockedStrips <= intValidationRule)
                                                {
                                                    intNumberBlockedStrips++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (tInitialRange <= tTodayTime && tTodayTime < tFinalRange)
                                            {
                                                intNumberBlockedStrips++;
                                            }
                                            else if ((tInitialBreak <= tTodayTime && tTodayTime < tFinalBreak) && (boolUseHourBreak == false))
                                            {
                                                intNumberBlockedStrips++;
                                                if (intNumberBlockedStrips <= intValidationRule)
                                                {
                                                    intNumberBlockedStrips++;
                                                }
                                                boolUseHourBreak = true;
                                            }
                                            else if (intNumberBlockedStrips <= intValidationRule)
                                            {
                                                intNumberBlockedStrips++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (tInitialRange <= tTodayTime && tTodayTime < tFinalRange)
                                        {
                                            intNumberBlockedStrips++;
                                        }
                                        else if ((tInitialBreak <= tTodayTime && tTodayTime < tFinalBreak) && (boolUseHourBreak == false))
                                        {
                                            intNumberBlockedStrips++;
                                            if (intNumberBlockedStrips <= intValidationRule)
                                            {
                                                intNumberBlockedStrips++;
                                            }
                                            boolUseHourBreak = true;
                                        }
                                        else if (intNumberBlockedStrips <= intValidationRule)
                                        {
                                            intNumberBlockedStrips++;
                                        }
                                    }
                                    listaHorarios.Add(objHorarioAux);
                                }
                            }
                        }
                        else
                        {
                            objHorarioAux.Descripcion = item.Code2;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Code;
                            objHorarioAux.Codigo3 = String.Empty;
                            objHorarioAux.Estado = CONSTANT.Constants.strRed;
                            listaHorarios.Add(objHorarioAux);
                        }
                    }
                    else
                    {
                        objHorarioAux.Descripcion = item.Code2;
                        objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                        objHorarioAux.Codigo = item.Code;
                        objHorarioAux.Codigo3 = String.Empty;
                        objHorarioAux.Estado = CONSTANT.Constants.strRed;
                        listaHorarios.Add(objHorarioAux);
                    }
                }

                if (boolBucketError == true && objTimeZoneVM.vValidateETA == "2")
                {
                    string MensajeEmail = string.Empty;
                    Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                    {
                        strIdSession = strIdSession,
                        strTo = ConfigurationManager.AppSettings("CorreoDestinatarioAgendamiento"),
                        strSubject = ConfigurationManager.AppSettings("strCorreoAsuntoAgendamiento"),
                        strMessage = BodyEmail(objBEETAAuditoriaRequestCapacityHFC, strMsjBucket.CODIGOC, objTimeZoneVM)
                    };
                    MensajeEmail = GetSendEmailConst(objSendEmail);
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
                else
                {
                    FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                    objHorAuxError.Codigo = CONSTANT.Constants.strMenosUno;
                    objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrCarFraHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    listaHorarios.Add(objHorAuxError);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session: " + strIdSession, "SchedulingToaController-ObtieneFranjasHorarias ", ex.Message);
                FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                objHorAuxError.Codigo = "-1";
                objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrorWs", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                listaHorarios.Add(objHorAuxError);
                return listaHorarios;

            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "SchedulingToaController -ObtieneFranjasHorarias ", "sale de ObtieneFranjasHorarias");

            return listaHorarios;
        }

        public static string RegistrarHistorialConsultasEta(string IdSession, List<BEETAEntidadcapacidadType> ocapacity, string fecha, int cod_zona, string cod_plano, string codTipoOrden, string codSubTipoOrden, string vTiempoTrabajo, string strUbicacion, ref string idRequest)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "SchedulingToaController", "RegistrarHistorialConsultasEta");

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
                        objRegisterEtaResponse.audit = audit;
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

            Claro.Web.Logging.Info("Session: " + IdSession, "SchedulingToaController - RegistrarHistorialConsultasEta ", "Mensaje: " + strMsg);

            return strMsg;
        }

        #region EMAIL
        public string GetSendEmailConst(Model.SendEmailModel objSendEmail)
        {
            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objSendEmail.strIdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = ConfigurationManager.AppSettings("CorreoServicioAlCliente"),
                    strTo = objSendEmail.strTo,
                    strSubject = objSendEmail.strSubject,
                    strMessage = TemplateEmail(objSendEmail.strMessage),
                    strAttached = objSendEmail.strAttached,
                    AttachedByte = objSendEmail.byteAttached,
                    strCC = objSendEmail.strCC
                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSendEmail.strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }
            string strResul = string.Empty;

            Claro.Web.Logging.Info("Session: " + objSendEmail.strIdSession, "SchedulingToaController - RegistrarHistorialConsultasEta ", "Mensaje: " + strResul);

            return strResul;
        }

        public string TemplateEmail(string strMessage)
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
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'><b>Estimados </b></td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>Su apoyo en la revision de la obervacion del  impedimento en la generacion del Agendamiento TOA en SIACUNICO.</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td width='180' class='Estilo1' height='60'>");
            strHtml.Append(strMessage);
            strHtml.Append("</td></tr>");
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
            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }

        public string BodyEmail(BEETAAuditoriaRequestCapacityHFC objBEETAAuditoriaRequestCapacityHFC, string strMsj, TimeZoneVM objTimeZoneVM)
        {

            var strHtml = new System.Text.StringBuilder();

            strHtml.Append("<p><b>Fecha Transacción: </b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</p>");
            strHtml.Append("<p><b>ID Transacción: </b>" + objBEETAAuditoriaRequestCapacityHFC.pIdTrasaccion.ToString() + "</p>");
            //strHtml.Append("<p><b>ID App: </b>" + objBEETAAuditoriaRequestCapacityHFC.pIP_APP.ToString() + "</p>");
            strHtml.Append("<p><b>Aplicación: </b>" + objBEETAAuditoriaRequestCapacityHFC.pAPP.ToString() + "</p>");
            strHtml.Append("<p><b>Customer: </b>" + objBEETAAuditoriaRequestCapacityHFC.pUsuario.ToString() + "</p>");
            //strHtml.Append("<p><b>DNI: </b>" + "" + "</p>");
            strHtml.Append("<p><b>Contrato: </b>" + objTimeZoneVM.vContractID + "</p>");
            strHtml.Append("<p><b>Fecha visita: </b>" + string.Join(",", objBEETAAuditoriaRequestCapacityHFC.vFechas) + "</p>");
            //strHtml.Append("<p><b>Ubicación-Bucket: </b>" + string.Join(",", objBEETAAuditoriaRequestCapacityHFC.vUbicacion[0].ToString()) + "</p>");
            //strHtml.Append("<p><b>Calcular Duracion: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcDur.ToString() + "</p>");
            //strHtml.Append("<p><b>Calcular Duracion Espec: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec.ToString() + "</p>");
            //strHtml.Append("<p><b>Calcular Tiempo Viaje: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje.ToString() + "</p>");
            //strHtml.Append("<p><b>Calcular Tiempo Viaje Espec: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec.ToString() + "</p>");
            //strHtml.Append("<p><b>Calcular Habilidad Trabajo: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo.ToString() + "</p>");
            //strHtml.Append("<p><b>Calcular Habilidad Trabajo Espec: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec.ToString() + "</p>");
            //strHtml.Append("<p><b>Obtener Ubicacion Zona: </b>" + objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona.ToString() + "</p>");
            //strHtml.Append("<p><b>Obtener Ubicacion Zona Espec: </b>" + objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec.ToString() + "</p>");            
            strHtml.Append("<p><b>Tipo de trabajo: </b>" + objTimeZoneVM.vJobTypes + "</p>");
            strHtml.Append("<p><b>Tipo orden de Trabajo: </b>" + objTimeZoneVM.vHistoryETA.Split('|')[2] + "</p>");
            strHtml.Append("<p><b>Subtipo orden de Trabajo: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCampoActividad[2].Valor + "</p>");
            strHtml.Append("<p><b>Zona: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCampoActividad[0].Valor + "</p>");
            strHtml.Append("<p><b>Mapa: </b>" + objBEETAAuditoriaRequestCapacityHFC.vCampoActividad[1].Valor + "</p>");
            //strHtml.Append("<p><b>Capacidad Opcional: </b>" + string.Join(",", objBEETAAuditoriaRequestCapacityHFC.vListaCapReqOpc[0].ToString()) + "</p>");
            strHtml.Append("<p><b>Mensaje TOA: </b>" + strMsj + "</p>");
            return strHtml.ToString();

        }
        #endregion

        public JsonResult GetSchedulingRule(string strIdSession, CommonTransacService.ScheduleRequest objScheduleRequest)
        {
            string strIdParametro = ConfigurationManager.AppSettings("strIdParametroReglaValidacion");
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objResponse = new FixedTransacService.TransactionScheduledResponse();
            var objRequest = new FixedTransacService.TransactionScheduledRequest()
            {
                audit = audit,
                vstrIdParametro = strIdParametro
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: GetTransactionScheduled", "Inicio Método : GetTransactionScheduled");
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.TransactionScheduledResponse>(() =>
                {
                    return _oServiceFixed.GetSchedulingRule(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: GetSchedulingRule", "Fín Método : GetSchedulingRule");

            return Json(new { data = objResponse.ListTransactionScheduled });
        }

        public JsonResult MessageScheduling(string strIdSession)
        {
            string strIdParametro = ConfigurationManager.AppSettings("strIdMensajeValidacion");
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objResponse = new FixedTransacService.TransactionScheduledResponse();
            var objRequest = new FixedTransacService.TransactionScheduledRequest()
            {
                audit = audit,
                vstrIdParametro = strIdParametro
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: MessageScheduling", "Inicio Método : MessageScheduling");
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.TransactionScheduledResponse>(() =>
                {
                    return _oServiceFixed.GetSchedulingRule(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            strMsjBucket = objResponse.ListTransactionScheduled.Find(x => x.ABREVIATURA_DET == ConfigurationManager.AppSettings("strMsjBucket"));
            strMsjDispEta = objResponse.ListTransactionScheduled.Find(x => x.ABREVIATURA_DET == ConfigurationManager.AppSettings("strMsjDispEta"));
            strMsj_Conf_Bkt = objResponse.ListTransactionScheduled.Find(x => x.ABREVIATURA_DET == ConfigurationManager.AppSettings("strMsj_Conf_Bkt"));
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: MessageScheduling", "Fín Método : MessageScheduling");

            return Json(new { data = objResponse.ListTransactionScheduled });
        }

    }
}