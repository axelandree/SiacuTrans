using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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
using COMMON = Claro.SIACU.Entity.Transac.Service.Postpaid;
using CONSTANT = Claro.SIACU.Transac.Service;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using Constant = Claro.SIACU.Transac.Service;
using Claro.Web;
using Claro.SIACU.Transac.Service;
using Newtonsoft.Json;
using CSTS = Claro.SIACU.Transac.Service;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using System.Text;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class FidelizacionServicesController : CommonServicesController
    {
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacService.CommonTransacServiceClient();
        private readonly FixedTransacService.FixedTransacServiceClient _oServiceFixed = new FixedTransacService.FixedTransacServiceClient();

        private string hidTipoServ;
        Model.InteractionModel objInteractionTemp = new Model.InteractionModel();


        public ActionResult LTEFidelizacionServices()
        {

            ViewData["strTempTrans"] = ConfigurationManager.AppSettings("strCodigoTransRetCanServ");
            ViewData["gConstkeyConsProg"] = ConfigurationManager.AppSettings("gConstkeyConsProg");
            ViewData["gConstTipoHFC"] = ConfigurationManager.AppSettings("gConstTipoHFC");
            ViewData["gConstCodParametroDiasMinimo"] = ConfigurationManager.AppSettings("gConstCodParametroDiasMinimo");
            ViewData["strTempTrans"] = ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Substring(ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Length - 3, 3);
            ViewData["gConstDiasHabiles"] = ConfigurationManager.AppSettings("gConstDiasHabiles");
            ViewData["gConstFlagRetensionCancelacionEstado"] = ConfigurationManager.AppSettings("gConstFlagRetensionCancelacionEstado");
            ViewData["gConstKeyCoCanServ"] = ConfigurationManager.AppSettings("gConstKeyCoCanServ");
            ViewData["gConstkeyReasonRCS"] = ConfigurationManager.AppSettings("gConstkeyReasonRCS");
            ViewData["gConstkeyCodSerRCS"] = ConfigurationManager.AppSettings("gConstkeyCodSerRCS");


            ViewData["kitracVariableCero"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            ViewData["NumeracionCERO"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO;
            ViewData["kitracVariableDos"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableDos;
            ViewData["NumeracionCERO"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO;
            ViewData["gstrTipoServDTH"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrTipoServDTH;
            ViewData["intCodErrorCons2"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            ViewData["kitracVariableCeroDouble"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCeroDouble;
            ViewData["NumeracionDOS"] = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionDOS;

            ViewData["gObtenerParametroTerminalTPI"] = ConfigurationManager.AppSettings("gObtenerParametroTerminalTPI");
            ViewData["gObtenerParametroSoloTFIPostpago"] = ConfigurationManager.AppSettings("gObtenerParametroSoloTFIPostpago");
            ViewData["sCodigoTransRetCanServLTE"] = ConfigurationManager.AppSettings("strCodigoTransRetCanServLTE");

            //---2208
            int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");  
            //---2208
            ViewData["strIdCargoFijo"] = ConfigurationManager.AppSettings("strIdCargoFijo");
            ViewData["strIdServicioAdicional"] = ConfigurationManager.AppSettings("strIdServicioAdicional");
            ViewData["strIdTipoCargoFijo"] = ConfigurationManager.AppSettings("strIdTipoCargoFijo");
            ViewData["strIdTipoServAdic"] = ConfigurationManager.AppSettings("strIdTipoServAdic");
            //Fase 2
            ViewData["strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE"] = ConfigurationManager.AppSettings("gstrTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE");
            ViewData["strValidaDeco"] = ConfigurationManager.AppSettings("gConstFilterDecos");

            /*PROY-32650*/
            ViewData["strCboSelMotivoSOTLTE"] = ConfigurationManager.AppSettings("strCboSelMotivoSOTLTE");

            return PartialView();
        }

        public JsonResult IniLoadPage(string strIdSession, string strContratoID, string strListNumImportar, string strNroTelefono, string CadenaOpciones, string CodeTipification)
        {
            bool resultado = false;

            try
            {
                string strTempTrans = ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Substring(ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Length - 3, 3);
                string gConstTipoHFC = ConfigurationManager.AppSettings("gConstTipoHFC");
                string strNombreTipoTelef = string.Empty;
                string gstrTipoServDTH = Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrTipoServDTH;
                string strFecMinimaCancel = string.Empty;
                int intNroDias = 0;
                string strNumDiasHabiles = ConfigurationManager.AppSettings("strRetentionCancelServicesNumDiasHabiles");
                bool FlatReintegro = false;
                string strFechaIni = string.Empty;
                double dlbCodNuevoPlan = 0;
                bool habilitaFecha = false;
                string Message = string.Empty;
                string valorIgv = string.Empty;
                

                CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServLTE");

                Model.InteractionModel oTipificacion = new Model.InteractionModel();
                FixedTransacService.RetentionCancelServicesResponse oPenalidad = null;
                FixedTransacService.AddDayWorkResponse oDayLabour = null;


                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Metodo : IniLoadPage", "Message : " + Message);

                if (strListNumImportar == null || strListNumImportar == string.Empty)
                {// --------  Tipificacion
                    if (strTempTrans == gConstTipoHFC)
                    {

                        strNombreTipoTelef = ValidarPermiso(strIdSession, strContratoID, strListNumImportar);
                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Metodo : LoadPage - ValidarPermiso", "strNombreTipoTelef : " + strNombreTipoTelef);
                    }
                    else
                    {

                        strNombreTipoTelef = gstrTipoServDTH;
                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Metodo : IniLoadPage", "strNombreTipoTelef : " + strNombreTipoTelef);
                    }
                    hidTipoServ = strNombreTipoTelef;


                    oTipificacion = CargarTificacion(strIdSession, CodeTipification);
                    objInteractionTemp = oTipificacion;

                }// --------  Tipificacion


                if (oTipificacion.FlagCase == "OK")
                {

                    #region "DiasLaborables"

                    if (strNumDiasHabiles == string.Empty || strNumDiasHabiles == null) { intNroDias = 0; } else { intNroDias = 1; }

                    strFechaIni = DateTime.Now.ToShortDateString();
                    oDayLabour = GetAddDayWork(strIdSession, strFechaIni, intNroDias);



                    if (oDayLabour.FechaResultado == string.Empty)
                    {


                        oDayLabour.FechaResultado = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstDiasHabiles")).Parameter.Value_C);

                    }

                    #endregion

                    // GetPenalidad
                    oPenalidad = ObtainPenalty(strIdSession, strTempTrans, gConstTipoHFC, strNroTelefono, dlbCodNuevoPlan, strContratoID);

                    strFecMinimaCancel = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstCodParametroDiasMinimo")).Parameter.Value_C);
                    Claro.Web.Logging.Info("IdSession: " + strIdSession, "Método : LoadPage  ", " strFecMinimaCancel : " + strFecMinimaCancel);


                    string strHidenKeyCam = string.Empty;
                    string strHidenKeyAcc = string.Empty;



                    strHidenKeyCam = ObtienePermiso(ConfigurationManager.AppSettings("gConstkeyCamFecCan"), CadenaOpciones);
                    strHidenKeyAcc = ObtienePermiso(ConfigurationManager.AppSettings("gConstkeyAccSegNiv"), CadenaOpciones);

                    Claro.Web.Logging.Info("IdSession: " + strIdSession, " Método : LoadPage ", " strHidenKeyCam : " + strHidenKeyCam + " strHidenKeyAcc : " + strHidenKeyAcc);

                    if (CadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfRetCanPenalidad")) > -1)
                    {
                        FlatReintegro = false;
                    }
                    else
                    {
                        FlatReintegro = true;
                    }


                    if (CadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfActualizaFechaProgramacion")) > -1)
                    {
                        habilitaFecha = true;
                    }
                    else
                    {
                        habilitaFecha = false;
                    }
                    valorIgv = GetCommonConsultIgv(strIdSession).igvD.ToString(System.Globalization.CultureInfo.InvariantCulture);

                    resultado = true;
                }
                else
                {

                    resultado = false;
                    Message = oTipificacion.Result;
                }

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Fín Método : IniLoadPage", "resultado : " + resultado + " FechaResultado : " + oDayLabour.FechaResultado + " FlatReintegro : " + FlatReintegro + " PenalidaAPADECE : " + oPenalidad.PenalidaAPADECE + "habilitaFecha : " + habilitaFecha);
                return Json(new { data = resultado, Message, oDayLabour.FechaResultado, FlatReintegro, oPenalidad.PenalidaAPADECE, habilitaFecha, valorIgv });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession" + strIdSession, "Message Error : ", ex.Message);
                ex.Message.ToString();
                return Json(new { data = ex.Message.ToString() });
            }



        }

        public string ObtienePermiso(string strKey, string CadenaOpciones)
        {
            string resultadoHiden;
            if (CadenaOpciones.IndexOf(strKey) != -1)
            {
                resultadoHiden = "S";

            }
            else
            {
                resultadoHiden = string.Empty;
            }

            return resultadoHiden;
        }

        public FixedTransacService.RetentionCancelServicesResponse ObtainPenalty(string strIdSession, string strTempTrans, string gConstTipoHFC, string strNroTelefono, double dlbCodNuevoPlan, string strContratoID)
        {

            string strPagoApadece;
            DateTime FechaPenalidad = DateTime.Now;
            double VariableCero = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            double VariableCeroDouble = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCeroDouble;
            FixedTransacService.RetentionCancelServicesResponse oApadeceCancel;
            FixedTransacService.RetentionCancelServicesResponse oPenalidadExt;
            FixedTransacService.RetentionCancelServicesResponse oResultado;

            if (strTempTrans == gConstTipoHFC)
            {

                oApadeceCancel = new RetentionCancelServicesResponse();
                oApadeceCancel = GetDataBSCSExt(strIdSession, strNroTelefono, dlbCodNuevoPlan);



                if (oApadeceCancel.Resultado.ToString() == CONSTANT.Constants.Value)
                {

                    oPenalidadExt = new RetentionCancelServicesResponse();
                    oPenalidadExt = GetPenalidadExt(strIdSession, strNroTelefono, FechaPenalidad, oApadeceCancel.NroFacturas, oApadeceCancel.CargoFijoActual,
                                                oApadeceCancel.CargoFijoNuevoPlan, 30, oApadeceCancel.CargoFijoNuevoPlan);


                    if (oPenalidadExt.PenalidaAPADECE == VariableCero || oPenalidadExt.PenalidaAPADECE == VariableCeroDouble || oPenalidadExt.PenalidaAPADECE == double.NaN)
                    {
                        oApadeceCancel = new RetentionCancelServicesResponse();
                        oApadeceCancel = GetApadeceCancelRet(strIdSession, Convert.ToInt(strNroTelefono), Convert.ToInt(strContratoID));
                        oApadeceCancel.PenalidaAPADECE = oPenalidadExt.PenalidaAPADECE;

                        if (oApadeceCancel.ValorApadece == VariableCeroDouble || oApadeceCancel.ValorApadece == double.NaN)
                        {
                            strPagoApadece = CONSTANT.Constants.strCero;

                        }
                        else
                        {
                            strPagoApadece = CONSTANT.Constants.strUno;
                        }

                    }

                }

            }
            else
            {


                oApadeceCancel = GetApadeceCancelRet(strIdSession, Convert.ToInt(strNroTelefono), Convert.ToInt(strContratoID));
                oApadeceCancel.PenalidadPCS = 0;
            }

            if (oApadeceCancel.CodMessage != CONSTANT.Constants.Message_OK)
            {
                oApadeceCancel.PenalidadPCS = CONSTANT.Constants.numeroCero;
                oApadeceCancel.PenalidaAPADECE = CONSTANT.Constants.numeroCero;
                oApadeceCancel.CargoFijoNuevoPlan = CONSTANT.Constants.numeroCero;

            }
            oResultado = new RetentionCancelServicesResponse();
            oResultado.PenalidadPCS = Math.Round(oApadeceCancel.PenalidadPCS, 2); //hdnPenalidadPCS
            oResultado.PenalidaAPADECE = Math.Round(oApadeceCancel.ValorApadece, 2);//  txtReintegro
            oResultado.CargoFijoNuevoPlan = Math.Round(oApadeceCancel.PenalidaAPADECE + oApadeceCancel.PenalidadPCS, 2); //hdnTotalPenalidad


            return oResultado;

        }

        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgLineaPOTP", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValueTipoTrabajoBajaTOTAL", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstFlagRetensionCancelacion"));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValueMotivoSOTDefecto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValueTipoTrabajoDefecto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strFlagInhabTipTraMotSot", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstFlagRetensionCancelacionEstado"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstPerfHayCaso"));
            lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00"));
            lstMessage.Add(DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year);
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgSelTr", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgSelMot", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgSelSubMot", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgSelAc", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgErrCampNumeri", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMensajeEsperaLoader", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("CambNumSinCosto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("CostoCambioNumeroConsumer", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("SusTempSinCostoReconexion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));

            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEstadoContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEstadoContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("flagRestringirAccesoTemporalCR", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgOpcionTemporalmenteInhabilitada", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgDebeCargLinea", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("gConstMsgLineaStatSuspe", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

            lstMessage.Add(ConfigurationManager.AppSettings("strConsLineaDesaActiva"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsjCantidadLimiteDecos"));

            //Llave para TOA LTE:
            lstMessage.Add(ConfigurationManager.AppSettings("CodTipServLtePA"));
            lstMessage.Add(ConfigurationManager.AppSettings("strShowChkPromAjustFact"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListarAccionesRC(string strIdSession)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaAccionesResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objlistaAccionesRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    vNivel = Convert.ToInt(ConfigurationManager.AppSettings("gConstPerfil_AsesorCAC"))
                };

            try
            {
                objlistaAccionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetListarAccionesRC(objlistaAccionesRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objlistaAccionesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objlistaAccionesResponse != null && objlistaAccionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objlistaAccionesResponse.AccionTypes)
                {
                    Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                        new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                    if (item.Cod_tipo_servicio == CONSTANT.Constants.numeroTres)
                    {
                        oCacDacTypeVM.Code = item.Codigo;
                        oCacDacTypeVM.Description = item.Descripcion;
                        listCacDacTypes.Add(oCacDacTypeVM);
                    }
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetMotCancelacion(string strIdSession)
        {
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "GetMotCancelacion: ", "Inicio Metodo : GetMotCancelacion");

            int strTipoLista = CONSTANT.Constants.numeroTres;
            FixedTransacService.RetentionCancelServicesResponse objMotCancelacionesResponse = null;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.vEstado = CONSTANT.Constants.numeroUno;
            objRequest.vTipoLista = strTipoLista;
            objRequest.audit = audit;
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "GetMotCancelacion: ", "objRequest.vEstado :" + CONSTANT.Constants.numeroUno + "objRequest.vTipoLista " + strTipoLista);

            try
            {
                objMotCancelacionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetMotCancelacion(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objMotCancelacionesResponse != null && objMotCancelacionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objMotCancelacionesResponse.AccionTypes)
                {
                    listCacDacTypes.Add(new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "GetMotCancelacion: ", "CacDacTypes Total REg " + objCommonServices.CacDacTypes.Count);
            }

            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetSubMotiveCancel(string strIdSession, int IdMotive)
        {
            FixedTransacService.RetentionCancelServicesResponse objSubMotiveResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objSubMotiveRequest = new FixedTransacService.RetentionCancelServicesRequest()
            {
                audit = audit,
                vIdMotive = IdMotive
            };

            try
            {
                objSubMotiveResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                {
                    return _oServiceFixed.GetSubMotiveCancel(objSubMotiveRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objSubMotiveRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objSubMotiveResponse != null && objSubMotiveResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objSubMotiveResponse.AccionTypes)
                {
                    listCacDacTypes.Add(new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetTypeWork(string strIdSession)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            JobTypesResponseHfc objJobTypeResponse;
            JobTypesRequestHfc objJobTypesRequest = new JobTypesRequestHfc()
            {
                audit = audit,
                p_tipo = Claro.SIACU.Transac.Service.Constants.numeroSeis,
            };

            try
            {
                objJobTypeResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                {
                    return _oServiceFixed.GetJobTypes(objJobTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            List<Helpers.CommonServices.GenericItem> List = null;

            if (objJobTypeResponse != null && objJobTypeResponse.JobTypes != null)
            {
                List = new List<Helpers.CommonServices.GenericItem>();
                for (int i = 0; i < objJobTypeResponse.JobTypes.Count; i++)
                {
                    List.Add(new Helpers.CommonServices.GenericItem()
                    {
                        Code = objJobTypeResponse.JobTypes[i].FLAG_FRANJA.Equals(Claro.SIACU.Transac.Service.Constants.strUno) ? objJobTypeResponse.JobTypes[i].tiptra + ".|" : objJobTypeResponse.JobTypes[i].tiptra, //Codigo
                        Code2 = objJobTypeResponse.JobTypes[i].FLAG_FRANJA, //Codigo2
                        Description = objJobTypeResponse.JobTypes[i].descripcion //Descripcion
                    });
                }
            }

            return Json(new { data = List });
        }

        public JsonResult GetSubTypeWork(string strIdSession, int IntIdTypeWork)
        {
            FixedTransacService.RetentionCancelServicesResponse objTypeWorkResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objTypeWorkRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objTypeWorkRequest.vIdTypeWork = IntIdTypeWork;
            objTypeWorkRequest.audit = audit;


            try
            {
                objTypeWorkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetSubTypeWork(objTypeWorkRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTypeWorkRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objTypeWorkResponse != null && objTypeWorkResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listTypeWork =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objTypeWorkResponse.AccionTypes)
                {
                    listTypeWork.Add(new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listTypeWork;
            }

            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetOrderSubType(string strIdSession, string strTipoTrabajo)
        {
            List<Helpers.CommonServices.GenericItem> List = null;
            Helpers.CommonServices.GenericItem item = null;
            string strTipoOrdEta = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objOrderTypesResponse = new OrderTypesResponseHfc();
            var objOrderTypesRequest = new OrderTypesRequestHfc();
            var objOrderSubTypesResponse = new OrderSubTypesResponseHfc();
            var objOrderSubTypesRequest = new OrderSubTypesRequestHfc();

            try
            {
                objOrderTypesRequest.audit = audit;
                objOrderTypesRequest.vIdtiptra = strTipoTrabajo;

                objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderTypesResponseHfc>(() => { return _oServiceFixed.GetOrderType(objOrderTypesRequest); });

                if (objOrderTypesResponse != null && objOrderTypesResponse.ordertypes.Count == 0)
                    strTipoOrdEta = Claro.SIACU.Transac.Service.Constants.strMenosUno;
                else
                    strTipoOrdEta = objOrderTypesResponse.ordertypes[0].VALOR;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objOrderTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            try
            {
                objOrderSubTypesRequest.audit = audit;
                objOrderSubTypesRequest.av_cod_tipo_orden = strTipoOrdEta;

                objOrderSubTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubTypesResponseHfc>(() => { return _oServiceFixed.GetOrderSubType(objOrderSubTypesRequest); });

                if (objOrderSubTypesResponse.OrderSubTypes != null)
                {
                    List = new List<Helpers.CommonServices.GenericItem>();
                    foreach (var aux in objOrderSubTypesResponse.OrderSubTypes)
                    {
                        item = new Helpers.CommonServices.GenericItem();
                        item.Code = aux.COD_SUBTIPO_ORDEN + "|" + aux.TIEMPO_MIN; //Codigo
                        item.Description = aux.DESCRIPCION; //Descripcion
                        List.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objOrderSubTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = List });
        }
        public JsonResult GetMotive_SOT(string strIdSession, string vIdTypeWork)
        {
            var objOrderTypesRequest = new RetentionCancelServicesRequest();
            var objOrderTypesResponse = new RetentionCancelServicesResponse();

            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            objOrderTypesRequest.audit = audit;
            objOrderTypesRequest.vIdTypeWork = Convert.ToInt(vIdTypeWork);

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : GetMotive_SOT");
            try
            {
                objOrderTypesResponse =
                    Claro.Web.Logging.ExecuteMethod<RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetMotiveSOT(objOrderTypesRequest);

                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objOrderTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            Models.CommonServices objCommonServices = null;

            if (objOrderTypesResponse != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objOrderTypesResponse.AccionTypes)
                {
                    Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                         new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();
                    {
                        oCacDacTypeVM.Code = item.Codigo;
                        oCacDacTypeVM.Description = item.Descripcion;
                        listCacDacTypes.Add(oCacDacTypeVM);
                    }
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Metodo : GetMotive_SOT");

            return Json(new { data = objCommonServices.CacDacTypes });

        }

        public FixedTransacService.AddDayWorkResponse GetAddDayWork(string strIdSession, string strFechaIni, int intNroDias)
        {
            FixedTransacService.AddDayWorkResponse objkResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.AddDayWorkRequest objRequest = new FixedTransacService.AddDayWorkRequest();
            objRequest.audit = audit;
            objRequest.FechaInicio = strFechaIni;
            objRequest.NumeroDias = intNroDias;
            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.AddDayWorkResponse>(() =>
                    {
                        return _oServiceFixed.GetAddDayWork(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            return objkResponse;
        }

        public JsonResult GetSoloTFIPostpago(string strIdSession, int parametroID, string Strmessage)
        {
            FixedTransacService.RetentionCancelServicesResponse objkResponse;
            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.ParameterID = parametroID;
            objRequest.Message = Strmessage;


            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetSoloTFIPostpago(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            return Json(new { data = objkResponse });
        }

        public CommonTransacService.TypificationResponse GetTypification(string strIdSession, string strTransactionName)
        {
            CommonTransacService.TypificationResponse objTypificationResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.TypificationRequest objTypificationRequest = new CommonTransacService.TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = ConfigurationManager.AppSettings("strCodigoTransRetCanServLTE");

            try
            {
                objTypificationResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.TypificationResponse>(() => { return oServiceCommon.GetTypification(objTypificationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objTypificationResponse;
        }


        public JsonResult GetValidateCustomerId(string strIdSession, string Phone)
        {
            FixedTransacService.ValidateCustomerIdResponse objkResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.ValidateCustomerIdRequest objRequest = new FixedTransacService.ValidateCustomerIdRequest();
            objRequest.audit = audit;
            objRequest.Phone = Phone;

            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.ValidateCustomerIdResponse>(() =>
                    {
                        return _oServiceFixed.GetValidateCustomerId(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            return Json(new { data = objkResponse });
        }


        public FixedTransacService.RetentionCancelServicesResponse GetDataBSCSExt(string strIdSession, string NroTelefono, double CodNuevoPlan)
        {
            FixedTransacService.RetentionCancelServicesResponse objkResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.CodNuevoPlan = CodNuevoPlan;

            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.ObtenerDatosBSCSExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objkResponse;
        }

        public FixedTransacService.RetentionCancelServicesResponse GetPenalidadExt(string strIdSession, string NroTelefono, DateTime FechaPenalidad, double NroFacturas, double CargoFijoActual,
                                          double CargoFijoNuevoPlan, double DiasxMes, double CodNuevoPlan)
        {
            FixedTransacService.RetentionCancelServicesResponse objResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.FechaPenalidad = FechaPenalidad;
            objRequest.NroFacturas = NroFacturas;
            objRequest.CargoFijoActual = CargoFijoActual;
            objRequest.CargoFijoNuevoPlan = CargoFijoNuevoPlan;
            objRequest.DiasxMes = DiasxMes;
            objRequest.CodNuevoPlan = CodNuevoPlan;

            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.ObtenerDatosBSCSExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objResponse;
        }


        public FixedTransacService.RetentionCancelServicesResponse GetApadeceCancelRet(string strIdSession, int NroTelefono, int codId)
        {
            FixedTransacService.RetentionCancelServicesResponse objResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest =
                new FixedTransacService.RetentionCancelServicesRequest();

            objRequest.audit = audit;
            objRequest.Phone = NroTelefono;
            objRequest.CodId = codId;


            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetApadeceCancelRet(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objResponse;
        }

        public ActionResult HFCRetencionCancelacionServicio_Const_Imp_R()
        {

            return View();
        }

        public ActionResult HFCRetencionCancelacionServicio_Const_Imp_NR()
        {

            return View();
        }

        public Model.InteractionModel CargarTificacion(string IdSession, string CodeTipification)
        {

            var objInteraction = new Model.InteractionModel();

            var tipification = GetTypificationHFC(IdSession, CodeTipification);

            if (tipification != null)
            {
                tipification.ToList().ForEach(x =>
                {
                    objInteraction.Type = x.Type;
                    objInteraction.Class = x.Class;
                    objInteraction.SubClass = x.SubClass;
                    objInteraction.InteractionCode = x.InteractionCode;
                    objInteraction.TypeCode = x.TypeCode;
                    objInteraction.ClassCode = x.ClassCode;
                    objInteraction.SubClassCode = x.SubClassCode;
                    objInteraction.FlagCase = CONSTANT.Constants.CriterioMensajeOK;
                });
            }
            else
            {
                objInteraction.Result = CONSTANT.Constants.ADDITIONALSERVICESPOSTPAID.strNotTypification;
                objInteraction.FlagCase = "NO OK ";

            }

            return objInteraction;
        }

        public FixedTransacService.Interaction DatosCaso(Model.HFC.RetentionCancelServicesModel oModel)  // No Retenido
        {
            FixedTransacService.Interaction objInteractionModel = new FixedTransacService.Interaction();

            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : DatosCaso");

            //tipificacion
            var objInteraction = new Model.InteractionModel();
            objInteraction = CargarTificacion(oModel.IdSession, oModel.CodeTipification);

            if (objInteraction.FlagCase == "OK")
            {
                //ObtenerCliente
                var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;


                objInteractionModel.OBJID_CONTACTO = GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
                objInteractionModel.TELEFONO = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + oModel.CustomerId;
                objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
                objInteractionModel.TIPIFICACION = objInteraction.Type.Trim();
                objInteractionModel.OBJID_SITE = oModel.OBJID_SITE;
                objInteractionModel.CLASE = objInteraction.Class.Trim();
                objInteractionModel.SUBCLASE = objInteraction.SubClass.Trim();
                objInteractionModel.CONTRATO = oModel.ContractId;
                objInteractionModel.PLANO = oModel.CodePlanInst;
                objInteractionModel.COLA = CommonServicesController.getNombreEnviaCola(oModel.IdAccion, 2);
                objInteractionModel.PRIORIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.SEVERIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                objInteractionModel.TIPO_INTERACCION = ConfigurationManager.AppSettings("AtencionDefault");
                objInteractionModel.NOTAS = oModel.Note;
                objInteractionModel.FLAG_INTERACCION = CONSTANT.Constants.strUno;
                objInteractionModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                objInteractionModel.USUARIO_ID = oModel.CurrentUser;


            }
            else
            {
                objInteractionModel.RESULTADO = objInteraction.Result;
                objInteractionModel.FLAG_INSERCION = objInteraction.FlagCase;

            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Metodo : DatosCaso");
            return objInteractionModel;
        }


        public Dictionary<string, object> InsertInteraction(Model.InteractionModel objInteractionModel, // Retenido
                                                           Model.TemplateInteractionModel oPlantillaDat,
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
            string strFlgRegistrado = ConstantsHFC.strUno;


            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            #region Obtener Cliente
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
            #endregion

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
            if (rInteraccionId != string.Empty && oPlantillaDat != null)
            {
                dictionaryResponse = InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
            }
            dictionaryResponse.Add("rInteraccionId", rInteraccionId);

            return dictionaryResponse;

        }

        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.HFC.RetentionCancelServicesModel model, string strNombreArchivoPDF, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.IdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = "Solicitud de Cancelación de Servicio";
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                Claro.Web.Logging.Info("IdSession: " + model.IdSession, "strInteraccionId: " + strInteraccionId, "Inicio Método : GetSendEmail");
                #region "Body Email"
                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>";

                if (model.Accion == "R")
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Por la presente queremos informarle que su intención de Cancelación de Servicio ha quedado sin efecto por aceptación suya.</td></tr>";
                }
                else
                {

                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Por la presente queremos informarle que su solicitud de Cancelación de Servicio fue atendida.</td></tr>";
                }

                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>Cordialmente</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
                objGetSendEmailRequest =
                    new CommonTransacService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = strAdjunto,
                        strSubject = strAsunto,
                        AttachedByte = attachFile
                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); });

                if (objGetSendEmailResponse.Exit == CONSTANT.Constants.CriterioMensajeOK)
                {
                    strResul = Functions.GetValueFromConfigFile("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                else
                {
                    strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                Claro.Web.Logging.Info("IdSession: " + model.IdSession, "strInteraccionId: " + strInteraccionId, "Fín Método : GetSendEmail - strResul : " + strResul);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.IdSession, AuditRequest.transaction, ex.Message);
                Claro.Web.Logging.Info(model.IdSession, AuditRequest.transaction, "Retencion Cancelación Services_LTE  ERROR - GetSendEmail");
                strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return strResul;
        }


        public Model.InteractionModel DatosInteraccion(Model.HFC.RetentionCancelServicesModel oModel) // Retenido 
        {

            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);

            try
            {
                // Get Datos de la Tipificacion
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : DatosInteraccion");

                objInteraction = CargarTificacion(oModel.IdSession, oModel.CodeTipification);

                var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;


                oInteraccion.ObjidContacto = GetCustomer(strNroTelephone, oModel.IdSession) + oModel.CustomerId;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;
                oInteraccion.Type = objInteraction.Type; // Type
                oInteraccion.Class = objInteraction.Class;
                oInteraccion.SubClass = objInteraction.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = CONSTANT.Constants.strCero;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Code_Plane_Inst;
                oInteraccion.FlagCase = CONSTANT.Constants.strCero;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;



                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Metodo : DatosInteraccion");
            }
            catch (Exception ex)
            {

                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            return oInteraccion;

        }

        public List<string> GrabaInteraccion(Model.HFC.RetentionCancelServicesModel oModel) //OK
        {
            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServLTE");
            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + oModel.CustomerId;
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = new Model.InteractionModel();


            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            try
            {

                strUserSession = oModel.CurrentUser;
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : GrabaInteraccion");

                //Obtener Datos de Plantilla de Interaccion
                oPlantillaDat = GetDataTemplateInteraction(oModel); //X_INTER_8  - OK

                // Datos de la transacción
                oInteraccion = DatosInteraccion(oModel); //ObjidContacto  - OK

                // Graba Interacciones
                var resultInteraction = InsertInteraction(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.CustomerId);


                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != ConstantsHFC.PresentationLayer.CriterioMensajeOK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    throw new Exception(Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

                }
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Metodo : GrabaInteraccion");
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return lstaDatTemplate;
        }

        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.HFC.RetentionCancelServicesModel oModel) //Retenido
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            try
            {
                oPlantCampDat.X_INTER_8 = (oModel.Reintegro == string.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.Reintegro));
                oPlantCampDat.X_INTER_9 = Convert.ToDouble(CONSTANT.Constants.strCero);
                oPlantCampDat.X_CHARGE_AMOUNT = oPlantCampDat.X_INTER_8;
                oPlantCampDat.X_OPERATION_TYPE = oModel.DesMotivos;
                oPlantCampDat.X_REGISTRATION_REASON = oModel.DesAccion;
                oPlantCampDat.X_FLAG_OTHER = (oModel.hidSupJef == CONSTANT.Constants.gstrVariableS ? CONSTANT.Constants.numeroUno.ToString() : CONSTANT.Constants.numeroCero.ToString());
                oPlantCampDat.X_EXPIRE_DATE = string.IsNullOrEmpty(oModel.DateProgrammingSot) ? DateTime.UtcNow : Convert.ToDate(oModel.DateProgrammingSot);
                oPlantCampDat.X_FIXED_NUMBER = string.Empty;
                oPlantCampDat.X_CLARO_NUMBER = oModel.NroCelular;
                oPlantCampDat.X_REASON = oModel.Accion;
                oPlantCampDat.X_INTER_16 = oModel.DesSubMotivo;
                oPlantCampDat.X_INTER_15 = oModel.DescCacDac;
                oPlantCampDat.X_ADJUSTMENT_AMOUNT = (oModel.TotalInversion == string.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.TotalInversion));

                if (oPlantCampDat.X_REASON == "NR")
                {
                    oPlantCampDat.X_FLAG_REGISTERED = oModel.PagoAPADECE;
                    oPlantCampDat.X_MODEL = string.Empty;

                }

                oPlantCampDat.X_ZIPCODE = oModel.NroCelular;
                oPlantCampDat.X_INTER_18 = string.Empty;

                if (oModel.TypeClient.ToUpper().Equals(CONSTANT.Constants.PresentationLayer.gstrConsumer.ToUpper()))
                {
                    oPlantCampDat.X_EMAIL = oModel.NameComplet;
                    oPlantCampDat.X_NAME_LEGAL_REP = string.Empty;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.DocumentNumber;

                }
                else
                {
                    oPlantCampDat.X_EMAIL = oModel.RazonSocial;
                    oPlantCampDat.X_NAME_LEGAL_REP = oModel.RepresentLegal;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.DNI_RUC;

                }

                oPlantCampDat.X_LASTNAME_REP = oModel.TypeDoc.ToUpper();
                oPlantCampDat.X_PHONE_LEGAL_REP = oModel.TelefonoReferencia;

                oPlantCampDat.X_FLAG_LEGAL_REP = oModel.CodTypeClient;
                oPlantCampDat.X_ADDRESS = oModel.AdressDespatch;
                oPlantCampDat.X_INTER_1 = oModel.Reference;
                oPlantCampDat.X_DEPARTMENT = oModel.Departament_Fact;
                oPlantCampDat.X_DISTRICT = oModel.District_Fac;
                oPlantCampDat.X_INTER_2 = oModel.Pais_Fac;
                oPlantCampDat.X_INTER_3 = oModel.Provincia_Fac;
                oPlantCampDat.X_INTER_20 = CONSTANT.Constants.strCero;
                //For LTE
                oPlantCampDat.X_ADJUSTMENT_REASON = oModel.ContractId;
                oPlantCampDat.X_TYPE_DOCUMENT = oModel.TypeClient;
                oPlantCampDat.X_INTER_4 = oModel.CustomerId;
                oPlantCampDat.X_INTER_5 = oModel.Account;
                if (oModel.Flag_Email)
                {
                    oPlantCampDat.X_CLARO_LDN1 = "1";
                    oPlantCampDat.X_INTER_29 = oModel.Email;
                }
                else {
                    oPlantCampDat.X_CLARO_LDN1 = "0";
                    oPlantCampDat.X_INTER_29 = string.Empty;
                }
            }
            catch (Exception ex)
            {

                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return oPlantCampDat;
        }

        public string CalculaDiasHabiles(string NroDias)
        {
            int intDiasHabiles = Convert.ToInt(NroDias);
            DateTime dtFechaSum = DateTime.Now;
            string ResultadoFecha = string.Empty;
            string strListaNoHabiles = ConfigurationManager.AppSettings("gListaDiasNoHabiles");
            int intCantDias = oTransacServ.Constants.numeroVeinte;
            int intCont = 0;

            for (int i = 0; i < intCantDias; i++)
            {
                if (strListaNoHabiles.IndexOf(DateTime.Now.AddDays(i).DayOfWeek.ToString().ToUpper().Trim()) == -1)
                {
                    dtFechaSum = DateTime.Now.AddDays(i);
                    intCont += 1;

                }
                if (intCont == intDiasHabiles)
                {
                    break;
                }
            }

            ResultadoFecha = string.Format(dtFechaSum.ToString("dd/MM/yyyy"));
            return ResultadoFecha;



        }

        public FixedTransacService.CaseTemplate DatosPlantillaCaso(Model.HFC.RetentionCancelServicesModel oModel)
        {
            FixedTransacService.CaseTemplate oPlantillaCampoData = new FixedTransacService.CaseTemplate();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : DatosPlantillaCaso");
            try
            {
                oPlantillaCampoData.X_CAS_8 = (oModel.Reintegro == String.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.Reintegro));
                oPlantillaCampoData.X_CAS_9 = Convert.ToDouble(CONSTANT.Constants.numeroCero);
                oPlantillaCampoData.MONTO_RECARGA = oPlantillaCampoData.X_CAS_8;
                oPlantillaCampoData.X_OPERATOR_PROBLEM = oModel.DesMotivos;
                oPlantillaCampoData.X_CAS_3 = (oModel.Accion == CONSTANT.Constants.strLetraR ? "Retenido" : "No Retenido");
                oPlantillaCampoData.X_CAS_7 = (oModel.hidSupJef == CONSTANT.Constants.strLetraS ? CONSTANT.Constants.strUno : CONSTANT.Constants.strCero);
                oPlantillaCampoData.X_SUSPENSION_DATE = DateTime.Now;
                oPlantillaCampoData.X_FIXED_NUMBER = String.Empty;
                oPlantillaCampoData.NRO_TELEFONO = oModel.NroCelular;
                oPlantillaCampoData.X_CAS_16 = oModel.DesSubMotivo;
                oPlantillaCampoData.X_CAS_5 = oModel.DesSubMotivo;
                oPlantillaCampoData.X_CAS_15 = oModel.DescCacDac;
                oPlantillaCampoData.X_CAS_30 = oModel.Note;
                oPlantillaCampoData.X_CAS_4 = (oModel.TotalInversion == String.Empty ? CONSTANT.Constants.strCero : oModel.TotalInversion);
                oPlantillaCampoData.X_CAS_6 = oModel.DesAccion;


                oPlantillaCampoData.X_COMPLAINT_AMOUNT = (oModel.TotalInversion == String.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.TotalInversion));


                if (oPlantillaCampoData.X_CAS_3.Equals("No Retenido"))
                {
                    oPlantillaCampoData.X_FLAG_OTHER_PROBLEMS = oModel.PagoAPADECE;
                    oPlantillaCampoData.X_MODEL = String.Empty;
                }

                oPlantillaCampoData.X_ADDRESS = oModel.AdressDespatch;
                oPlantillaCampoData.X_CAS_1 = oModel.Reference;
                oPlantillaCampoData.X_CAS_2 = oModel.Departament_Fact;
                oPlantillaCampoData.X_CAS_17 = oModel.District_Fac;
                oPlantillaCampoData.X_CAS_18 = oModel.Pais_Fac;
                oPlantillaCampoData.X_CAS_19 = oModel.Provincia_Fac;
                oPlantillaCampoData.X_CAS_20 = CONSTANT.Constants.strCero;
                oPlantillaCampoData.X_CAS_21 = oModel.CodePlanInst;

                // For LTE
                oPlantillaCampoData.X_CUSTOMER_SEGMENT = oModel.ContractId;
                oPlantillaCampoData.X_DEACTIVATION_REASON = oModel.TypeClient;

                if (oModel.TypeClient.ToUpper().Equals("CONSUMER"))
                {
                    oPlantillaCampoData.X_CUSTOMER_NAME = oModel.NameComplet;
                    oPlantillaCampoData.X_DOCUMENT_NUMBER = oModel.DocumentNumber;

                }
                else
                {

                    oPlantillaCampoData.X_CUSTOMER_NAME = oModel.RazonSocial;
                    oPlantillaCampoData.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                }
                oPlantillaCampoData.X_DIAL_TYPE = oModel.TypeDoc.ToUpper();

                if (oModel.Flag_Email)
                {
                    oPlantillaCampoData.X_FLAG_GPRS = "1";
                    oPlantillaCampoData.X_CAS_29 = oModel.Email;
                }
                else
                {
                    oPlantillaCampoData.X_FLAG_GPRS = "0";
                    oPlantillaCampoData.X_CAS_29 = string.Empty;
                }

                oPlantillaCampoData.X_CAS_30 = oModel.Note;


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Metodo : DatosPlantillaCaso");
            return oPlantillaCampoData;
        }
       
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.HFC.RetentionCancelServicesModel oModel)
        {
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string documentName = string.Empty;
            string strInteraccionId = strIdInteraction;
            bool generado = false;
            string strTypeTransaction = string.Empty;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;


            InteractionServiceRequestHfc objInteractionServiceRequest = null;

            ParametersGeneratePDF parameters = new ParametersGeneratePDF();
            strNombreArchivo = "RETENCION_CANCELACION";

            Claro.Web.Logging.Info(" Método : " + "GetConstancyPDF", "strTypeTransaction: " + strTypeTransaction, "Inicio");
            try
            {
                strTexto = CSTS.Functions.GetValueFromConfigFile("strMsgRetencionCancelConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                if (oModel.Accion == "R") // Retenido
                {
                    strTypeTransaction = ConfigurationManager.AppSettings("strNombreArchivo_Retencion");

                    parameters = new ParametersGeneratePDF();
                    parameters.StrCentroAtencionArea = oModel.DescCacDac;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrCustomerId = oModel.CustomerId;
                    parameters.StrFechaTransaccionProgram = oModel.fechaActual;
                    parameters.strFechaTransaccion = oModel.fechaActual;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrMotivoCancelacion = oModel.DesMotivos;
                    parameters.StrSubMotivoCancel = (oModel.DesSubMotivo == "Seleccionar" ? "" : oModel.DesSubMotivo);
                    parameters.StrAccion = oModel.DesAccion;
                    parameters.StrTipoTransaccion = strTypeTransaction;
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoLTE");
                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;  //RETENCION
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrContenidoComercial2 = strTexto;
                    documentName = strNombreArchivo;
                }
                else // No Retenido(NR)
                {
                    strTypeTransaction = ConfigurationManager.AppSettings("strNombreArchivo_Cancelacion");

                    parameters = new ParametersGeneratePDF();
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrSegmento = oModel.Segmento;
                    parameters.StrProductos = oModel.ProductType;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.strNroDoc = oModel.DNI_RUC;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrMotivoCancelacion = oModel.DesMotivos;
                    parameters.StrSubMotivoCancel = oModel.DesSubMotivo;
                    parameters.strContrato = oModel.ContractId;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.strDireccionInstalcion = oModel.AdressDespatch;
                    parameters.StrFechaTransaccionProgram = oModel.DateProgrammingSot;
                    parameters.strFechaTransaccion = oModel.fechaActual;
                    parameters.StrTipoTransaccion = strTypeTransaction;
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoLTE");
                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;  //CANCELACION
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrFechaCancel = oModel.DateProgrammingSot;
                    parameters.strFechaHoraAtención = Convert.ToString(DateTime.Now);
                    parameters.StrContenidoComercial2 = strTexto;
                    documentName = strNombreArchivo;

                }

                GenerateConstancyResponseCommon response = GenerateContancyPDF(strIdSession, parameters);
                nombrepath = response.FullPathPDF;
                generado = response.Generated;

                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                Claro.Web.Logging.Info("nombrepath: " + nombrepath, "generado: " + generado, "documentName : " + documentName);

                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Fín ", "nombrepath : " + strNombreArchivo);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);

            }

            return listResponse;
        }


        public bool Auditoria(Model.HFC.RetentionCancelServicesModel oModel)
        {
            bool FlatResultado;

            string strusuarioAutoriza = oModel.CurrentUser;  // validar
            string strAmmount = CONSTANT.Constants.strCero;
            string[,] strDetails = new string[8, 3];
            string strService = KEY.AppSettings("gServicioLlamadaSalienteNoFact");
            string ConstEventGraRC = ConfigurationManager.AppSettings("gConstEventGraRC");

            strDetails[0, 0] = "Número Claro";
            strDetails[0, 1] = oModel.NroCelular;
            strDetails[0, 2] = "Número Claro";

            strDetails[1, 0] = "Código Contrato";
            strDetails[1, 1] = oModel.ContractId;
            strDetails[1, 2] = "Número Claro";

            strDetails[2, 0] = "Transacción Realizada";
            strDetails[2, 1] = (oModel.Accion == "R" ? "Retenido" : "No Retenido");
            strDetails[2, 2] = "Transacción Realizada";

            strDetails[3, 0] = "Motivo Cancelación";
            strDetails[3, 1] = oModel.DesMotivos;
            strDetails[3, 2] = "Motivo Cancelación";

            strDetails[4, 0] = "Acción";
            strDetails[4, 1] = oModel.DesAccion;
            strDetails[4, 2] = "Acción";

            strDetails[5, 0] = "Autoriza Acción Segundo Nivel";
            strDetails[5, 1] = (oModel.hidSupJef == "S" ? strusuarioAutoriza : string.Empty);
            strDetails[5, 2] = "Autoriza Acción Segundo Nivel";

            strDetails[6, 0] = "Fecha Programada para la Cancelación";
            strDetails[6, 1] = string.Empty; //         Validar
            strDetails[6, 2] = "Fecha Programada para la Cancelación";

            strDetails[7, 0] = "CAC/DAC";
            strDetails[7, 1] = oModel.DescCacDac;
            strDetails[7, 2] = "CAC/DAC";

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


            FlatResultado = RegisterAuditGeneral(oModel.IdSession, oModel.Telephone, strAmmount, sbText.ToString(), strService, ConstEventGraRC);

            return FlatResultado;
        }

        public FixedTransacService.Interaction InsertCaso(FixedTransacService.Interaction oCaso, FixedTransacService.CaseTemplate oPlantillaCaso, Model.HFC.RetentionCancelServicesModel oModel,
                                                string strLetraR)
        {


            FixedTransacService.Interaction oResponseCase = new FixedTransacService.Interaction();
            FixedTransacService.CaseTemplate oPlantillaResponse = new FixedTransacService.CaseTemplate();

            string ContingenciaClarify = System.Configuration.ConfigurationManager.AppSettings["gConstContingenciaClarify"];
            bool resultado = false;
            string IdCaso = string.Empty;

            string strFlgRegistrado = ConstantsHFC.strUno;

            if (oCaso.OBJID_CONTACTO == null || oCaso.OBJID_CONTACTO == "0" || oCaso.OBJID_CONTACTO == "")
            {
                //ObtenerCliente
                var phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + oModel.CustomerId;
                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
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
                    return GetCustomerData(objGetCustomerRequest, oModel.IdSession);
                });
                if (objCustomerResponse != null)
                {

                    oCaso.OBJID_CONTACTO = objCustomerResponse.contactobjid;
                    oCaso.OBJID_SITE = objCustomerResponse.Customer.SiteCode;

                }

            }
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo : InsertarCaso ", " oCaso.OBJID_CONTACTO : " + oCaso.OBJID_CONTACTO + " OBJID_SITE  : " + oCaso.OBJID_SITE);

            if (oCaso.OBJID_SITE == null || oCaso.OBJID_SITE == "0" || oCaso.OBJID_SITE == "")
            {
                //ObtenerCliente
                var phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + oModel.CustomerId;
                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                {
                    audit = audit,
                    vPhone = phone,
                    vAccount = string.Empty,
                    vContactobjid1 = oModel.ContractId,
                    vFlagReg = strFlgRegistrado
                };
                objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() =>
                {
                    return GetCustomerData(objGetCustomerRequest, oModel.IdSession);
                });
                if (objCustomerResponse != null)
                {
                    oCaso.OBJID_SITE = objCustomerResponse.Customer.SiteCode;

                }

            }
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo : InsertarCaso", " OBJID_SITE  : " + oCaso.OBJID_SITE);

            if (strLetraR == "N")
            {
                if (ContingenciaClarify != "SI")
                {
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo :", "GetCreateCase ");
                    oResponseCase = GetCreateCase(oCaso);
                    IdCaso = oResponseCase.CASO_ID;
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo : GetCreateCase", "- IdCaso :" + IdCaso);
                    resultado = true;
                }
                else
                {
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo : ", "GetInsertCase ");
                    oResponseCase = GetInsertCase(oCaso);
                    IdCaso = oResponseCase.CASO_ID;
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, " Inicio Metodo : GetCreateCase ", "- IdCaso :" + IdCaso);
                    resultado = true;
                }

            }
            else
            {

                resultado = true;
            }


            if (resultado && (IdCaso != null && IdCaso != "" && oPlantillaCaso != null))
            {

                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Inicio Metodo :", "GuardarPlantilla ");

                oPlantillaCaso.ID_CASO = IdCaso;
                oPlantillaResponse = GuardarPlantilla(oPlantillaCaso, strLetraR);

                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Inicio Metodo : GuardarPlantilla ", " ID_CASO " + oPlantillaResponse.ID_CASO);
                resultado = true;
            }
            oResponseCase.CASO_ID = oPlantillaResponse.ID_CASO;
            oResponseCase.FLAG_INSERCION_CASO = oPlantillaResponse.FLAG_INSERCION;
            oResponseCase.MESSAGE_CASO = oPlantillaResponse.MESSAGE;
            //Envia el CasoId

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Inicio Metodo : GuardarPlantilla ", " ID_CASO : " + oPlantillaResponse.ID_CASO + " FLAG_INSERCION_CASO : " + oResponseCase.FLAG_INSERCION_CASO + " oResponseCase.MESSAGE_CASO : " + oResponseCase.MESSAGE_CASO);
            return oResponseCase;
        }

        public FixedTransacService.CaseTemplate GuardarPlantilla(FixedTransacService.CaseTemplate oPlantilla, string vEstadoForm)
        {
            string ContingenciaClarify = string.Empty;
            ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            FixedTransacService.CaseTemplate oResponse = new FixedTransacService.CaseTemplate();

            if (vEstadoForm == CONSTANT.Constants.strLetraN)
            {
                if (ContingenciaClarify != CONSTANT.Constants.Variable_SI)
                {

                    oResponse = GetInsertTemplateCase(oPlantilla);

                }
                else
                {

                    oResponse = GetInsertTemplateCaseContingent(oPlantilla);

                }


            }
            else
            {
                // Actualizar la plantilla
                if (ContingenciaClarify != CONSTANT.Constants.Variable_SI)
                {

                    oResponse = ActualizaPlantillaCaso(oPlantilla);
                }


            }

            return oResponse;
        }

        public bool GetDesactivatedContract_LTE(Model.HFC.RetentionCancelServicesModel oModel)
        {

            bool resultado = false;


            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            FixedTransacService.Customer objContratoReq = new FixedTransacService.Customer();

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio  Metodo : GetDesactivatedContract_LTE : ");
            try
            {

                objContratoReq.audit = audit;
                objContratoReq.audit.Session = oModel.IdSession;

                objContratoReq.ApplicationName = ConfigurationManager.AppSettings("gConstTipoLTE");
                objContratoReq.UserApplication = oModel.CurrentUser;

                objContratoReq.AreaPCs = oModel.AreaPCs;
                objContratoReq.Des_CAC = oModel.DescCacDac;
                objContratoReq.BillingCycle = oModel.BillingCycle;
                objContratoReq.CustomerID = oModel.CustomerId;
                objContratoReq.ContractID = oModel.ContractId;
                objContratoReq.Account = oModel.Account;
                objContratoReq.CodigoInteraction = string.Empty;
                objContratoReq.Cod_Motive = oModel.CodMotiveSot;
                objContratoReq.CodigoService = ConfigurationManager.AppSettings("gConstkeyCodSerRCSLTE");
                objContratoReq.Date_Present = Convert.ToDate(oModel.fechaActual); //validar


                objContratoReq.FechaProgramacion = oModel.FechaProgramacion.Substring(6, 4) + "-" + oModel.FechaProgramacion.Substring(3, 2) + "-" + oModel.FechaProgramacion.Substring(0, 2);
                objContratoReq.DateProgrammingSot = oModel.DateProgrammingSot.Substring(6, 4) + "-" + oModel.DateProgrammingSot.Substring(3, 2) + "-" + oModel.DateProgrammingSot.Substring(0, 2);
                objContratoReq.FlagNdPcs = oModel.flagNdPcs;
                if (Convert.ToDouble(oModel.TotalInversion) > 0)
                {
                    objContratoReq.FlagOccApadece = "1";
                }
                else
                {
                    objContratoReq.FlagOccApadece = "0";
                }

                objContratoReq.MailUserAplication = oModel.Email;
                objContratoReq.MontoFidelizacion = oModel.MontoFidelizacion;
                objContratoReq.MontoPCs = oModel.MontoPCs;
                objContratoReq.AmountPenalty = oModel.Reintegro;
                objContratoReq.MotivePCS = oModel.MotivePCS;
                objContratoReq.Msisdn = oModel.Msisdn;
                objContratoReq.DocumentNumber = oModel.DocumentNumber;
                objContratoReq.Observation = oModel.Observation;
                objContratoReq.Reason = ConfigurationManager.AppSettings("gConstkeyReasonRCSLTE");
                objContratoReq.SubMotivePCS = oModel.SubMotivePCS;
                objContratoReq.CustomerType = oModel.TypeClient;
                objContratoReq.TypeServices = ConfigurationManager.AppSettings("gConstTipoHFCLTE");
                objContratoReq.TypeWork = oModel.TypeWork;
                objContratoReq.Trace = oModel.Trace;
                objContratoReq.Assessor = oModel.CurrentUser;

                objContratoReq.FringeHorary = oModel.vSchedule;

                resultado = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.GetDesactivatedContract_LTE(objContratoReq);
                });

                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín  Metodo : GetDesactivatedContract_LTE : ");

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(oModel.IdSession, objContratoReq.audit.transaction, ex.Message);
                if (ex.InnerException != null)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, objContratoReq.audit.transaction, "Error GetDesactivatedContract : " + ex.InnerException.ToString());
                }
            }
            return resultado;


        }

        #region "Constancia"

         #endregion

        public JsonResult GetTransactionScheduled(string strIdSession, string strContratoID)
        {
            bool Resultado = true;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objResponse = new FixedTransacService.TransactionScheduledResponse();
            var objRequest = new FixedTransacService.TransactionScheduledRequest()
            {
                audit = audit,
                vstrCoId = strContratoID,
                vstrCuenta = string.Empty,
                vstrFDesde = string.Empty,
                vstrFHasta = string.Empty,
                vstrEstado = string.Empty,
                vstrAsesor = string.Empty,
                vstrTipoTran = string.Empty,
                vstrCodInter = string.Empty,
                vstrCacDac = string.Empty
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.TransactionScheduledResponse>(() =>
                {
                    return _oServiceFixed.GetTransactionScheduled(objRequest);
                });

                if (objResponse != null && objResponse.ListTransactionScheduled.Count > 0)
                {
                    foreach (var item in objResponse.ListTransactionScheduled)
                    {
                        if (item.SERVC_ESTADO.Equals(Claro.Constants.NumberZeroString) || item.SERVC_ESTADO.Equals(Claro.Constants.NumberOneString)|| item.SERVC_ESTADO.Equals(Claro.Constants.NumberTwoString))
                        { Resultado = false; break; }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);

            }

            return Json(new { data = Resultado });
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
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Reg : " + JsonConvert.SerializeObject(objResponse.List));
            return Json(new { data = objResponse.List });

        }

        public JsonResult SaveTransactionFidelizacion(Model.HFC.RetentionCancelServicesModel oModel)
        {
            string vInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string vDesInteraction = string.Empty;
            bool ResultadoAudit = false;
            string strRutaArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            var errorMessage = "";
            oModel.fechaActual = DateTime.Now.ToShortDateString();


            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionFidelizacion");

            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransFideliServLTE");

            //---32650---
            var result = false;
            oModel.idCampana = Convert.ToInt(ConfigurationManager.AppSettings("strIdCampanaFideLTE"));
            oModel.typeHFCLTE = Model.HFC.typeHFCLTE.LTE;
            oModel.typeRETEFIDE = Model.HFC.typeRETEFIDE.FIDE;
            setIdContactoSide(oModel);

            var objRequest = new FixedTransacService.RegisterInteractionAdjustRequest();

            string[] strRegistrarExportarSap = null, StrRegistrarDetalleInteraccion = null; 
            strRegistrarExportarSap= ConfigurationManager.AppSettings("strRegistrarExportarSap").Split('|');
            StrRegistrarDetalleInteraccion = ConfigurationManager.AppSettings("strRegistrarDetalleInteraccion").Split('|');

            objRequest.RegistrarInteraccion = new RegistrarInteraccion();
            objRequest.RegistrarDetalleInteraccion = new RegistrarDetalleInteraccion();
            objRequest.RegistrarCabeceraDoc = new RegistrarCabeceraDoc();
            objRequest.RegistrarDetalleDoc = new RegistrarDetalleDoc();
            objRequest.RegistrarAjusteOAC = new RegistrarAjusteOAC();
            objRequest.RegistrarExportarSap = new RegistrarExportarSap();

            //INI seteo de datos para registrar la interaccion de registro ajuste
            objRequest.RegistrarInteraccion.piContactObjId1 = oModel.objIdContacto;
            objRequest.RegistrarInteraccion.piSiteObjId1 = oModel.objIdSite;
            objRequest.RegistrarInteraccion.piPhone = "H" + oModel.CustomerId;
            objRequest.RegistrarInteraccion.piTipo = "LTE"; //ya que se encuenta en el controlador de HFC
            objRequest.RegistrarInteraccion.piCodPlano = oModel.CodePlanInst;
            objRequest.RegistrarInteraccion.piAgente = App_Code.Common.CurrentUser;
            objRequest.pLTE = "1"; //Antes,SI ES lte=1, sino 0. PERO ultimo AF dijo que SIEMPRE sera 0.

            objRequest.RegistrarDetalleInteraccion.piFirstName = oModel.name;
            objRequest.RegistrarDetalleInteraccion.piLastName = oModel.LastName;
            objRequest.RegistrarDetalleInteraccion.piInter15 = StrRegistrarDetalleInteraccion[7];

            objRequest.RegistrarCabeceraDoc.piCicloFact = oModel.BillingCycle;
            objRequest.RegistrarCabeceraDoc.piIdCliente = oModel.CustomerId;

            if (string.IsNullOrEmpty(oModel.Modalidad))
            { objRequest.RegistrarCabeceraDoc.piIdTipCliente = "2"; }
            else { objRequest.RegistrarCabeceraDoc.piIdTipCliente = (oModel.Modalidad.ToLower() == (System.Configuration.ConfigurationManager.AppSettings["ConstCorporativo"].ToLower()) ? "1" : "2"); } /* si es corporativo,1:2*/

            
            objRequest.RegistrarCabeceraDoc.piNumDoc = oModel.NroDoc;
            objRequest.RegistrarCabeceraDoc.piClienteCta = oModel.Account ;
            objRequest.RegistrarCabeceraDoc.piCiudad = oModel.Departament_Fact;
            
            objRequest.RegistrarCabeceraDoc.piDireccion = oModel.AdressDespatch;
            
            objRequest.RegistrarCabeceraDoc.piNumIdentFiscal = oModel.DNI_RUC;
            string oTelef = string.Empty; if (!string.IsNullOrEmpty(oModel.Msisdn)) { oTelef = oModel.Msisdn; } else { oTelef = "H" + oModel.CustomerId; }
            objRequest.RegistrarDetalleDoc.ListaRegistroDetDocum = new List<RegistroDetDocum>
            {
                new RegistroDetDocum(){ pTelefono = oTelef }
            };

            objRequest.RegistrarAjusteOAC.piCodCuenta = oModel.CustomerId;
            objRequest.RegistrarExportarSap.piTextoCab = strRegistrarExportarSap[6]; //si es retencion posicion 5, si es fidelizacion posicion 6.

            string strIdInteract = string.Empty, strIdDocAut = string.Empty;
            switch (oModel.flagCargFijoServAdic)
            {
                case Claro.Constants.NumberZeroString: //Cargo fijo
                    var promo = true;
                        if (oModel.aplicaPromoFact) //si esta marcado el check de aplica descuento a boleta actual
                        {
                            if (!RegisterInteractionAdjust(oModel.IdSession, objRequest, oModel.DiscountDescription, ref strIdInteract, ref strIdDocAut, oModel.BillingCycle, oModel.Msisdn))
                            {promo = false;}
                            else { oModel.mesVal = (Convert.ToInt(oModel.mesVal) - 1).ToString(); }// luego de registrar el ajuste, le aplicamos el bono, quitando el mes de la boleta aplicada al ajuste. 
                            oModel.Note = oModel.Note + "Código de Interaccion de Registro Ajuste Sar: " + strIdInteract + ", código de DocAut: " + strIdDocAut;
                        }
                        if (promo)
                        {
                            //oModel.mesVal = MesPeriodo;
                            result = RegisterBonoDiscount(oModel);
                            if (!result)
                            {
                                errorMessage = ConfigurationManager.AppSettings("gConstMsgRegBonoCF");
                            }
                            else
                            {
                                //oModel.flagCargFijoServAdic = Claro.Constants.NumberOneString;
                            }
                         
                       }
                       else
                        errorMessage = ConfigurationManager.AppSettings("gConstMsgAjusReciPagoCF");
                       break;
                case Claro.Constants.NumberOneString: //Servicio adicional
                    var resp = RegisterActiDesaBonoDesc(oModel);
                    if (resp == Claro.Constants.NumberZeroString)
                        result = true;
                    else
                        errorMessage = resp;
                    break;
                default:
                    result = true;
                    break;
            }

            if (result)
            {
                var oInteraccion = GenerateInteraccion(oModel);
                vInteractionId = TipificarRetencionFidelizacion(oModel.IdSession, oInteraccion, oModel);
                oModel.CodigoInteraction = vInteractionId;

                vDesInteraction = (!string.IsNullOrEmpty(vInteractionId) ? ConfigurationManager.AppSettings("strMsgTranGrabSatis") : ConfigurationManager.AppSettings("strMensajeDeError"));
                if (vInteractionId != "")
                {
                    GetConstancyPDFCFSA(oModel.IdSession, vInteractionId, oModel);
                    strRutaArchivo = oModel.strFullPathPDF;

                    if (oModel.Flag_Email)
                    {
                        if (oModel.updateDataMen)
                        {
                            string messag;
                            if (ActualizarDatosMenores(oModel, out messag))
                            {
                                oModel.Note = messag;
                                TipificarDatosMenores(ConfigurationManager.AppSettings("strCodigoTransDatosMenorLTE"), oModel);
                            }
                        }

                        byte[] attachFile = null;
                        string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                        if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                        {
                            MensajeEmail = GetSendEmailCFSA(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
                        }
                    }

                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionFidelizacion - vDesInteraction : " + vDesInteraction);
                    ResultadoAudit = Auditoria(oModel);
                }
                
            }


            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionFidelizacion" + " " + "Parametros Output : vDesInteraction" + vDesInteraction + "vFlagInteraction : " + vFlagInteraction + "vInteractionId " + vInteractionId + "ResultadoAudit : " + ResultadoAudit);
            return Json(new { vDesInteraction, MensajeEmail, vInteractionId, strRutaArchivo, errorMessage });
        }

        public Model.InteractionModel GenerateInteraccion(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var oInteraccion = new Model.InteractionModel();

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            try
            {
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : GrabaInteraccion");
                oInteraccion = DatosInteraccionCFSA(oModel);
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }
            return oInteraccion;
        }

        public Model.InteractionModel DatosInteraccionCFSA(Model.HFC.RetentionCancelServicesModel oModel) // Retenido & No Retenido
        {
            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                // Get Datos de la Tipificacion
                objInteraction = CargarTificacion(oModel.IdSession, oModel.CodeTipification);

                oInteraccion.ObjidContacto = oModel.objIdContacto;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;
                oInteraccion.Type = objInteraction.Type;
                oInteraccion.Class = objInteraction.Class;
                oInteraccion.SubClass = objInteraction.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = CONSTANT.Constants.strCero;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.FlagCase = CONSTANT.Constants.strCero;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
                oInteraccion.ObjidSite = oModel.objIdSite;
                oInteraccion.Cuenta = oModel.Cuenta;

                oModel.clase = objInteraction.Class;
                oModel.subClase = objInteraction.SubClass;
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Método : DatosInteraccion");

            return oInteraccion;

        }

        public JsonResult SaveTransactionRetentionDeco(Model.HFC.RetentionCancelServicesModel oModel, InstallUninstallDecoderModel objViewModel)
        {
            string errorMessage = string.Empty;
            string responseMessage = string.Empty;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionFidelizacionDeco");
            //Instalar Deco
            #region InstalarDeco
            JsonResult response = null;
            try
            {
                objViewModel.InsInteractionPlusModel.Inter22 = objViewModel.InsInteractionPlusModel.Inter22.Replace(',', '.');
                objViewModel.InsInteractionPlusModel.Inter23 = objViewModel.InsInteractionPlusModel.Inter23.Replace(',', '.');
                objViewModel.InsInteractionPlusModel.Inter24 = objViewModel.InsInteractionPlusModel.Inter24.Replace(',', '.');
                objViewModel.StrIdSession = oModel.IdSession;
                objViewModel.StrContractId = oModel.ContractId;
                objViewModel.StrCustomerId = oModel.CustomerId;

                 response = new UninstallInstallationOfDecoderController().ExecuteTransaction(objViewModel);
                 string sotDevuelta = response.Data.GetType().GetProperty("SotNumber").GetValue(response.Data, null).ToString();
                 responseMessage = response.Data.GetType().GetProperty("ResponseMessage").GetValue(response.Data, null).ToString();
                 oModel.SOT = sotDevuelta;

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, oModel.Transaction, ex.Message);
            }
            #endregion

            #region RegistrarDescuento

            
            oModel.typeHFCLTE = Model.HFC.typeHFCLTE.LTE;
            oModel.typeRETEFIDE = Model.HFC.typeRETEFIDE.FIDE;
            
            string strNombreArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strEstadoForm = string.Empty;
            string vDesInteraction = string.Empty;
            string vInteractionId = string.Empty, strRutaArchivo = string.Empty;
            string vFlagInteraction = string.Empty;
            bool ResultadoAudit = false;
            if (!string.IsNullOrEmpty(oModel.SOT))
            {
                oModel.idCampana = Convert.ToInt(ConfigurationManager.AppSettings("strIdCampanaFideLTE"));
            
                var result = RegisterBonoDiscount(oModel);
                if (result)
                {

                    oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransFideliServLTE");

                    //---32650---
                    var oInteraccion = GenerateInteraccion(oModel);
                    vInteractionId = TipificarRetencionFidelizacion(oModel.IdSession, oInteraccion, oModel);
                    oModel.CodigoInteraction = vInteractionId;

                    vDesInteraction = (!string.IsNullOrEmpty(vInteractionId) ? ConfigurationManager.AppSettings("strMsgTranGrabSatis") : ConfigurationManager.AppSettings("strMensajeDeError"));
                    if (vInteractionId != "")
                    {
                        GetConstancyPDFCFSA(oModel.IdSession, vInteractionId, oModel);
                        strRutaArchivo = oModel.strFullPathPDF;

                        if (oModel.Flag_Email)
                        {
                            if (oModel.updateDataMen)
                            {
                                string messag;
                                if (ActualizarDatosMenores(oModel, out messag))
                                {
                                    oModel.Note = messag;
                                    TipificarDatosMenores(ConfigurationManager.AppSettings("strCodigoTransDatosMenorLTE"), oModel);
                                }
                            }

                            byte[] attachFile = null;
                            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                            if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                            {
                                MensajeEmail = GetSendEmailCFSA(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
                            }
                        }

                        Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionFidelizacionDeco - vDesInteraction : " + vDesInteraction);
                        ResultadoAudit = Auditoria(oModel);
                    }
                    else
                    {
                        errorMessage = ConfigurationManager.AppSettings("gConstMsgRegBonoCF");
                    }
                  }                    
            } 
            else  
            {
                errorMessage = responseMessage;
            }
                            
            #endregion
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionFidelizacionDeco" + " " + "Parametros Output : vDesInteraction" + vDesInteraction + "vFlagInteraction : " + vFlagInteraction + "vInteractionId " + vInteractionId + "ResultadoAudit : " + ResultadoAudit);
            return Json(new { vDesInteraction, MensajeEmail, vInteractionId, strRutaArchivo, errorMessage });
        }
        

        [HttpPost]
        public JsonResult GetTimeZoneAdicional(string strIdSession, TimeZoneVM objTimeZoneVM)
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetTimeZone ", "entra a GetTimeZone");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            try
            {
                lstGenericItem = ObtieneFranjasHorariasAdicional(objTimeZoneVM, strIdSession);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: GetTimeZone ", "sale de GetTimeZone");
            return Json(new { data = lstGenericItem });
        }

    }
}