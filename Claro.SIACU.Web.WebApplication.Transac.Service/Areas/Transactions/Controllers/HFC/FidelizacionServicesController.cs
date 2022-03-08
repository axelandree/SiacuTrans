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
using MODELD = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using GetRegistarInstaDecoAdiHFC = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class FidelizacionServicesController : CommonServicesController
    {
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacService.CommonTransacServiceClient();
        private readonly FixedTransacService.FixedTransacServiceClient _oServiceFixed = new FixedTransacService.FixedTransacServiceClient();

        // GET: /Transactions/RetentionCancelServices/
        private string hidTipoServ;
        Model.InteractionModel objInteractionTemp = new Model.InteractionModel();

        public ActionResult HFCFidelizacionServices()
        {
            try
            {
                ViewData["strTempTrans"] = ConfigurationManager.AppSettings("strCodigoTransRetCanServ");
                ViewData["gConstkeyConsProg"] = ConfigurationManager.AppSettings("gConstkeyConsProg");
                ViewData["gConstTipoHFC"] = ConfigurationManager.AppSettings("gConstTipoHFC");
                ViewData["gConstCodParametroDiasMinimo"] = ConfigurationManager.AppSettings("gConstCodParametroDiasMinimo");
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
                ViewData["strCodigoTransRetCanServHFC"] = ConfigurationManager.AppSettings("strCodigoTransRetCanServHFC");
                int number = Convert.ToInt(KEY.AppSettings("strIncrementDays", "0"));
                ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
                ViewData["strDateNew"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
                ViewData["strIdCargoFijo"] = ConfigurationManager.AppSettings("strIdCargoFijo");
                ViewData["strIdServicioAdicional"] = ConfigurationManager.AppSettings("strIdServicioAdicional");
                ViewData["strIdAumentoVelocidad"] = ConfigurationManager.AppSettings("strIdAumentoVelocidad");
                ViewData["strIdTipoCargoFijo"] = ConfigurationManager.AppSettings("strIdTipoCargoFijo");
                ViewData["strIdTipoServAdic"] = ConfigurationManager.AppSettings("strIdTipoServAdic");
                ViewData["strValidaDeco"] = ConfigurationManager.AppSettings("gConstFilterDecos");
                /*PROY-32650*/
                ViewData["strCboSelMotivoSOTHFC"] = ConfigurationManager.AppSettings("strCboSelMotivoSOTHFC");
                ViewData["strMonthsFide"] = ConfigurationManager.AppSettings("strMonthsFide");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { data = ex.Message.ToString() });
            }


            return PartialView();
        }

        public JsonResult LoadPage(string strIdSession, string strContratoID, string strListNumImportar, string strNroTelefono, string CadenaOpciones)
        {
            bool resultado = false;

            try
            {
                string CodeTipification = string.Empty;
                string strTempTrans = ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Substring(ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Length - 3, 3);
                string gConstTipoHFC = ConfigurationManager.AppSettings("gConstTipoHFC");
                string strNombreTipoTelef = string.Empty;
                string gstrTipoServDTH = Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrTipoServDTH;
                string strFecMinimaCancel = string.Empty;
                int intNroDias = 1;
                string strNumDiasHabiles = ConfigurationManager.AppSettings("strRetentionCancelServicesNumDiasHabiles");
                bool FlatReintegro = false;
                string strFechaIni = string.Empty;
                double dlbCodNuevoPlan = 0;
                bool habilitaFecha = false;
                string Message = string.Empty;
                string valorIgv = string.Empty;


                CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServHFC");

                Model.InteractionModel oTipificacion = new Model.InteractionModel();
                FixedTransacService.RetentionCancelServicesResponse oPenalidad = null;
                FixedTransacService.AddDayWorkResponse oDayLabour = null;


                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "Message : " + Message);

                if (strListNumImportar == null || strListNumImportar == string.Empty)
                {// --------  Tipificacion
                    if (strTempTrans != gConstTipoHFC) //Evalenzs ==
                    {

                        strNombreTipoTelef = ValidarPermiso(strIdSession, strContratoID, strListNumImportar);
                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "strNombreTipoTelef : " + strNombreTipoTelef);
                    }
                    else
                    {

                        strNombreTipoTelef = gstrTipoServDTH;
                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "strNombreTipoTelef : " + strNombreTipoTelef);
                    }
                    hidTipoServ = strNombreTipoTelef;


                    oTipificacion = CargarTificacion(strIdSession, CodeTipification);
                    objInteractionTemp = oTipificacion;

                }// --------  Tipificacion

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "oTipificacion.FlagCase  : " + oTipificacion.FlagCase);
                if (oTipificacion.FlagCase == CONSTANT.Constants.CriterioMensajeOK)
                {

                    #region "DiasLaborables"


                    strFechaIni = DateTime.Now.ToShortDateString();
                    oDayLabour = GetAddDayWork(strIdSession, strFechaIni, (strNumDiasHabiles != string.Empty ? Convert.ToInt(strNumDiasHabiles) : intNroDias));

                    Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "oDayLabour.FechaResultado  : " + oDayLabour.FechaResultado);


                    if (oDayLabour.FechaResultado == string.Empty)
                    {


                        oDayLabour.FechaResultado = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstDiasHabiles")).Parameter.Value_C);
                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : LoadPage", "oDayLabour.FechaResultado  : " + oDayLabour.FechaResultado);
                    }

                    #endregion

                    // GetPenalidad
                    oPenalidad = ObtainPenalty(strIdSession, strTempTrans, gConstTipoHFC, strNroTelefono, dlbCodNuevoPlan, strContratoID);

                    strFecMinimaCancel = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstCodParametroDiasMinimo")).Parameter.Value_C);

                    Claro.Web.Logging.Info("IdSession: " + strIdSession, " Método : LoadPage  -  strFecMinimaCancel : ", strFecMinimaCancel);

                    string strHidenKeyCam = string.Empty;
                    string strHidenKeyAcc = string.Empty;



                    strHidenKeyCam = ObtienePermiso(ConfigurationManager.AppSettings("gConstkeyCamFecCan"), CadenaOpciones);
                    strHidenKeyAcc = ObtienePermiso(ConfigurationManager.AppSettings("gConstkeyAccSegNiv"), CadenaOpciones);

                    Claro.Web.Logging.Info("IdSession: " + strIdSession, " Método : LoadPage  ", "strHidenKeyCam : " + strHidenKeyCam + " strHidenKeyAcc : " + strHidenKeyAcc);

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

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Fín Método : LoadPage", "resultado : " + resultado + " FechaResultado : " + oDayLabour.FechaResultado + " FlatReintegro : " + FlatReintegro + " PenalidaAPADECE : " + oPenalidad.PenalidaAPADECE + "habilitaFecha : " + habilitaFecha);
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
            if (CadenaOpciones.Contains(strKey))
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

            string strPagoApadece = string.Empty;
            DateTime FechaPenalidad = DateTime.Now;
            double VariableCero = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            double VariableCeroDouble = Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCeroDouble;

            FixedTransacService.RetentionCancelServicesResponse oApadeceCancel = null;
            FixedTransacService.RetentionCancelServicesResponse oPenalidadExt = null;
            FixedTransacService.RetentionCancelServicesResponse oResultado = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            try
            {
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : ObtainPenalty");
                if (strTempTrans != gConstTipoHFC) //  ==
                {

                    oApadeceCancel = new RetentionCancelServicesResponse();
                    oApadeceCancel = GetDataBSCSExt(strIdSession, strNroTelefono, dlbCodNuevoPlan);

                    Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, " Método : ObtainPenalty - oApadeceCancel.Resultado : " + oApadeceCancel.Resultado.ToString());

                    if (oApadeceCancel.Resultado.ToString() == CONSTANT.Constants.Value)
                    {

                        oPenalidadExt = new RetentionCancelServicesResponse();
                        oPenalidadExt = GetPenalidadExt(strIdSession, strNroTelefono, FechaPenalidad, oApadeceCancel.NroFacturas, oApadeceCancel.CargoFijoActual,
                                                    oApadeceCancel.CargoFijoNuevoPlan, 30, oApadeceCancel.CargoFijoNuevoPlan);

                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, " Método : ObtainPenalty - oPenalidadExt.PenalidaAPADECE : " + oPenalidadExt.PenalidaAPADECE);


                        oApadeceCancel.ValorApadece = oPenalidadExt.PenalidaAPADECE;
                        oApadeceCancel.PenalidadPCS = oPenalidadExt.PenalidadPCS;
                        oApadeceCancel.PenalidaAPADECE = oPenalidadExt.PenalidaAPADECE;

                        Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, oApadeceCancel.ValorApadece.ToString());

                        if (oPenalidadExt.PenalidaAPADECE == VariableCero || oPenalidadExt.PenalidaAPADECE == VariableCeroDouble || oPenalidadExt.PenalidaAPADECE == double.NaN)
                        {
                            oApadeceCancel = new RetentionCancelServicesResponse();
                            oApadeceCancel = GetApadeceCancelRet(strIdSession, Convert.ToInt(strNroTelefono), Convert.ToInt(strContratoID));
                            oApadeceCancel.PenalidaAPADECE = oPenalidadExt.PenalidaAPADECE;

                            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, " Método : ObtainPenalty - oApadeceCancel.ValorApadece : " + oApadeceCancel.ValorApadece);
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

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : ObtainPenalty - oResultado.PenalidadPCS : " + oResultado.PenalidadPCS);
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : ObtainPenalty - oResultado.PenalidaAPADECE : " + oResultado.PenalidaAPADECE);
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : ObtainPenalty - oResultado.CargoFijoNuevoPlan : " + oResultado.CargoFijoNuevoPlan);

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : ObtainPenalty PenalidadPCS : " + oResultado.PenalidadPCS + " PenalidaAPADECE : " + oResultado.PenalidaAPADECE + " CargoFijoNuevoPlan : " + oResultado.CargoFijoNuevoPlan);
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
            lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00")); //9
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
            lstMessage.Add(ConfigurationManager.AppSettings("strMessageETAValidation"));
            lstMessage.Add(ConfigurationManager.AppSettings("gCantidadLimiteDeEquipos"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsjCantidadLimiteDecos"));
            lstMessage.Add(ConfigurationManager.AppSettings("strShowChkPromAjustFact"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListarAccionesRC(string strIdSession)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaAccionesResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objlistaAccionesRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    vNivel = Convert.ToInt(ConfigurationManager.AppSettings("gConstPerfil_AsesorCAC"))
                };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetListarAccionesRC");
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

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetListarAccionesRC - Total Registros : " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetMotCancelacion(string strIdSession)
        {
            int strTipoLista = CONSTANT.Constants.numeroTres;
            FixedTransacService.RetentionCancelServicesResponse objMotCancelacionesResponse = null;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.vEstado = CONSTANT.Constants.numeroUno;
            objRequest.vTipoLista = strTipoLista;
            objRequest.audit = audit;

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetMotCancelacion");
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
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotCancelacion listCacDacTypes = " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetSubMotiveCancel(string strIdSession, int IdMotive)
        {
            FixedTransacService.RetentionCancelServicesResponse objSubMotiveResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objSubMotiveRequest = new FixedTransacService.RetentionCancelServicesRequest()
            {
                audit = audit,
                vIdMotive = IdMotive
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetSubMotiveCancel");

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

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetSubMotiveCancel Total Registros = " + objCommonServices.CacDacTypes.Count);

            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetTypeWork(string strIdSession)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            JobTypesResponseHfc objJobTypeResponse;
            JobTypesRequestHfc objJobTypesRequest = new JobTypesRequestHfc()
            {
                audit = audit,
                p_tipo = Claro.SIACU.Transac.Service.Constants.numeroCinco
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetTypeWork");

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
                        Code2 = objJobTypeResponse.JobTypes[i].FLAG_FRANJA,
                        Description = objJobTypeResponse.JobTypes[i].descripcion
                    });
                }
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetTypeWork - Total Reg : " + List.Count);
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

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetSubTypeWork");
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

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetSubTypeWork - Total Reg : " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetMotive_SOT(string strIdSession)
        {

            CommonTransacService.MotiveSotResponseCommon objMotiveSotResponseCommon;
            CommonTransacService.AuditRequest audit =
                           App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetMotive_SOT");

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
                        return oServiceCommon.GetMotiveSot(objMotiveSotRequestCommon);

                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            Models.CommonServices objCommonServices = null;

            if (objMotiveSotResponseCommon != null && objMotiveSotResponseCommon.getMotiveSot != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listMotiveSot =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (CommonTransacService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
                {
                    listMotiveSot.Add(new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description,
                    });
                }
                objCommonServices.CacDacTypes = listMotiveSot;
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotive_SOT - Total Registros : " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });



        }
        public FixedTransacService.AddDayWorkResponse GetAddDayWork(string strIdSession, string strFechaIni, int intNroDias)
        {
            FixedTransacService.AddDayWorkResponse objkResponse = new FixedTransacService.AddDayWorkResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.AddDayWorkRequest objRequest = new FixedTransacService.AddDayWorkRequest();
            objRequest.audit = audit;
            objRequest.FechaInicio = strFechaIni;
            objRequest.NumeroDias = intNroDias;

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetAddDayWork");
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

            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetAddDayWork");
            return objkResponse;
        }

        public CommonTransacService.TypificationResponse GetTypification(string strIdSession, string strTransactionName)
        {
            CommonTransacService.TypificationResponse objTypificationResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.TypificationRequest objTypificationRequest = new CommonTransacService.TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = ConfigurationManager.AppSettings("strCodigoTransRetCanServHFC");

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetTypification");
            try
            {
                objTypificationResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.TypificationResponse>(() => { return oServiceCommon.GetTypification(objTypificationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);

            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetTypification");
            return objTypificationResponse;
        }

        public FixedTransacService.RetentionCancelServicesResponse GetDataBSCSExt(string strIdSession, string NroTelefono, double CodNuevoPlan)
        {
            FixedTransacService.RetentionCancelServicesResponse objkResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.CodNuevoPlan = CodNuevoPlan;


            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetDataBSCSExt");
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

            }


            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetDataBSCSExt");
            return objkResponse;
        }

        public FixedTransacService.RetentionCancelServicesResponse GetPenalidadExt(string strIdSession, string NroTelefono, DateTime FechaPenalidad, double NroFacturas, double CargoFijoActual,
                                          double CargoFijoNuevoPlan, double DiasxMes, double CodNuevoPlan)
        {
            FixedTransacService.RetentionCancelServicesResponse objResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt");



            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.FechaPenalidad = FechaPenalidad;
            objRequest.NroFacturas = NroFacturas;
            objRequest.CargoFijoActual = CargoFijoActual;
            objRequest.CargoFijoNuevoPlan = CargoFijoNuevoPlan;
            objRequest.DiasxMes = DiasxMes;
            objRequest.CodNuevoPlan = CodNuevoPlan;

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - NroTelefono" + NroTelefono);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - FechaPenalidad" + FechaPenalidad);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - NroFacturas" + NroFacturas);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - CargoFijoActual" + CargoFijoActual);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - CargoFijoNuevoPlan" + CargoFijoNuevoPlan);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - DiasxMes" + DiasxMes);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetPenalidadExt  - CodNuevoPlan" + CodNuevoPlan);

            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetObtainPenalidadExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);

            }


            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetPenalidadExt");
            return objResponse;
        }


        public FixedTransacService.RetentionCancelServicesResponse GetApadeceCancelRet(string strIdSession, int NroTelefono, int codId)
        {
            FixedTransacService.RetentionCancelServicesResponse objResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.RetentionCancelServicesRequest objRequest =
                new FixedTransacService.RetentionCancelServicesRequest();

            objRequest.audit = audit;
            objRequest.Phone = NroTelefono;
            objRequest.CodId = codId;

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetApadeceCancelRet");
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

            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetApadeceCancelRet");
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

            Claro.Web.Logging.Info("IdSession: " + IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification);
            try
            {

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
                        Claro.Web.Logging.Info("IdSession: " + IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "FlagCase : " + objInteraction.FlagCase);
                    });
                }
                else
                {
                    objInteraction.Result = CONSTANT.Constants.ADDITIONALSERVICESPOSTPAID.strNotTypification;
                    objInteraction.FlagCase = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                    Claro.Web.Logging.Info("IdSession: " + IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "Result : " + objInteraction.Result);
                    Claro.Web.Logging.Info("IdSession: " + IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "FlagCase : " + objInteraction.FlagCase);

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, "Método : CargarTificacion ", ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + IdSession, "Método : CargarTificacion Fín", "CodeTipification : " + CodeTipification + "FlagCase :" + objInteraction.FlagCase);
            return objInteraction;
        }

        public FixedTransacService.Interaction DatosCaso(Model.HFC.RetentionCancelServicesModel model)
        {
            FixedTransacService.Interaction objInteractionModel = new FixedTransacService.Interaction();

            //tipificacion
            Model.InteractionModel objInteraction;
            objInteraction = CargarTificacion(model.IdSession, model.CodeTipification);

            if (objInteraction.FlagCase == CONSTANT.Constants.CriterioMensajeOK)
            {
                //ObtenerCliente
                var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + model.CustomerId;


                objInteractionModel.OBJID_CONTACTO = GetCustomer(strNroTelephone, model.IdSession);  //Get Customer = strObjId
                objInteractionModel.TELEFONO = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.CustomerId;
                objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
                objInteractionModel.TIPIFICACION = objInteraction.Type;
                objInteractionModel.OBJID_SITE = model.OBJID_SITE;
                objInteractionModel.CLASE = objInteraction.Class;
                objInteractionModel.SUBCLASE = objInteraction.SubClass;
                objInteractionModel.CONTRATO = model.ContractId;
                objInteractionModel.PLANO = model.Plan;
                objInteractionModel.COLA = CommonServicesController.getNombreEnviaCola(model.IdAccion, 1);
                objInteractionModel.PRIORIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.SEVERIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                objInteractionModel.TIPO_INTERACCION = ConfigurationManager.AppSettings("AtencionDefault");
                objInteractionModel.NOTAS = model.Note;
                objInteractionModel.FLAG_INTERACCION = CONSTANT.Constants.strUno;
                objInteractionModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                objInteractionModel.USUARIO_ID = model.CurrentUser;


            }
            else
            {
                objInteractionModel.RESULTADO = objInteraction.Result;
                objInteractionModel.FLAG_INSERCION = objInteraction.FlagCase;

            }

            return objInteractionModel;
        }


        public Dictionary<string, object> InsertInteraction(Model.InteractionModel objInteractionModel,
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
            Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "Inicio Método : InsertInteraction");

            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            //Validacion de Contingencia
            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            Dictionary<string, string> result;
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

            Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "Inicio Método : InsertInteraction - rInteraccionId : " + rInteraccionId);
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
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
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
                Claro.Web.Logging.Info(model.IdSession, AuditRequest.transaction, "Retencion Cancelación Services_HFC  ERROR - GetSendEmail");
                strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return strResul;
        }

        public Model.InteractionModel DatosInteraccion(Model.HFC.RetentionCancelServicesModel oModel) // Retenido & No Retenido
        {

            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                // Get Datos de la Tipificacion
                objInteraction = CargarTificacion(oModel.IdSession, oModel.CodeTipification);

                var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;


                oInteraccion.ObjidContacto = GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
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


            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Método : DatosInteraccion");

            return oInteraccion;

        }

        public List<string> GrabaInteraccion(Model.HFC.RetentionCancelServicesModel oModel)
        {

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
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : GrabaInteraccion");

                oPlantillaDat = GetDataTemplateInteraction(oModel);

                oInteraccion = DatosInteraccion(oModel);
                var resultInteraction = InsertInteraction(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.CustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != ConstantsHFC.PresentationLayer.CriterioMensajeOK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, CSTS.Functions.GetValueFromConfigFile("strMensajeDeError", "strConstArchivoSIACUTHFCPOSTConfigMsg"));
                    throw new Exception(CSTS.Functions.GetValueFromConfigFile("strMensajeDeError", "strConstArchivoSIACUTHFCPOSTConfigMsg"));

                }

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Método : GrabaInteraccion FlagMessage : " + lstaDatTemplate[0]);
            return lstaDatTemplate;
        }

        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            try
            {
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetDataTemplateInteraction");

                oPlantCampDat.X_INTER_8 = (oModel.Reintegro == string.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.Reintegro));
                oPlantCampDat.X_INTER_9 = Convert.ToDouble(CONSTANT.Constants.strCero);
                oPlantCampDat.X_CHARGE_AMOUNT = oPlantCampDat.X_INTER_8;
                oPlantCampDat.X_OPERATION_TYPE = oModel.DesMotivos;
                oPlantCampDat.X_REGISTRATION_REASON = oModel.DesAccion;
                oPlantCampDat.X_FLAG_OTHER = (oModel.hidSupJef == CONSTANT.Constants.gstrVariableS ? CONSTANT.Constants.numeroUno.ToString() : CONSTANT.Constants.numeroCero.ToString());
                oPlantCampDat.X_EXPIRE_DATE = Convert.ToDate(oModel.FechaCompromiso);
                oPlantCampDat.X_FIXED_NUMBER = string.Empty;
                oPlantCampDat.X_CLARO_NUMBER = oModel.NroCelular;
                oPlantCampDat.X_REASON = oModel.Accion;
                oPlantCampDat.X_INTER_16 = oModel.DesSubMotivo;
                oPlantCampDat.X_INTER_15 = oModel.DescCacDac;
                oPlantCampDat.X_ADJUSTMENT_AMOUNT = (oModel.TotalInversion == string.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.TotalInversion));

                if (oPlantCampDat.X_REASON == CONSTANT.Constants.gstrNoRetenido)
                {
                    oPlantCampDat.X_FLAG_REGISTERED = oModel.PagoAPADECE;
                    oPlantCampDat.X_MODEL = string.Empty;

                }

                oPlantCampDat.X_ZIPCODE = oModel.NroCelular;
                oPlantCampDat.X_INTER_18 = string.Empty;


                if (oModel.TypeClient.ToUpper().Equals("CONSUMER"))
                {
                    oPlantCampDat.X_NAME_LEGAL_REP = string.Empty;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.NroDoc;

                }
                else
                {
                    oPlantCampDat.X_NAME_LEGAL_REP = oModel.RepresentLegal;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.DNI_RUC;

                }

                oPlantCampDat.X_LASTNAME_REP = oModel.TypeDoc.ToUpper();
                oPlantCampDat.X_PHONE_LEGAL_REP = oModel.TelefonoReferencia;
                oPlantCampDat.X_FLAG_LEGAL_REP = oModel.TypeClient;
                oPlantCampDat.X_ADDRESS = oModel.AdressDespatch;
                oPlantCampDat.X_INTER_1 = oModel.Reference;
                oPlantCampDat.X_DEPARTMENT = oModel.Departament_Fact;
                oPlantCampDat.X_DISTRICT = oModel.District_Fac;
                oPlantCampDat.X_INTER_2 = oModel.Pais_Fac;
                oPlantCampDat.X_INTER_3 = oModel.Provincia_Fac;
                oPlantCampDat.X_INTER_20 = CONSTANT.Constants.strCero;


                oPlantCampDat.X_CLARO_LDN1 = oModel.Flag_Email == true ? CONSTANT.Constants.strUno : CONSTANT.Constants.strCero; //Validar

                if (oPlantCampDat.X_CLARO_LDN1 == CONSTANT.Constants.strUno)
                {
                    oPlantCampDat.X_INTER_29 = oModel.Email;
                } else {
                    oPlantCampDat.X_INTER_29 = string.Empty;
                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fín Método : GetDataTemplateInteraction");
            return oPlantCampDat;

        }

        public JsonResult SaveTransactionRetention(Model.HFC.RetentionCancelServicesModel oModel)
        {
            string vInteractionId;
            string vFlagInteraction;
            string vDesInteraction;
            bool ResultadoAudit;
            string strRutaArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            var errorMessage = "";
            oModel.fechaActual = DateTime.Now.ToShortDateString();

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionRetention");

            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServHFC");
            List<string> strInteractionId = GrabaInteraccion(oModel);

            vDesInteraction = strInteractionId[0].ToString();
            vFlagInteraction = strInteractionId[2].ToString();
            vInteractionId = strInteractionId[3].ToString();
       
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionRetention - vFlagInteraction : " + vFlagInteraction + " vInteractionId : " + vInteractionId + " vDesInteraction : " + vDesInteraction);

            if (vDesInteraction == "OK")
            {
                vDesInteraction = ConfigurationManager.AppSettings("strMsgTranGrabSatis");

                //Generar constancia
               
                GetConstancyPDF(oModel.IdSession, vInteractionId, oModel);
                strRutaArchivo = oModel.strFullPathPDF;
                

                //-----------------
                //ENVIAR CORREO
                if (oModel.Flag_Email)
                {
                    byte[] attachFile;
                    //Nombre del archivo
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                    if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                    MensajeEmail = GetSendEmail(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile);
                }

            }
            else
            {
                vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionRetention - vDesInteraction : " + vDesInteraction);



            ResultadoAudit = Auditoria(oModel);


            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveTransactionRetention" + " " + "Parametros Output : vDesInteraction" + vDesInteraction + "vFlagInteraction : " + vFlagInteraction + "vInteractionId " + vInteractionId + "ResultadoAudit : " + ResultadoAudit);
            return Json(new { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail, errorMessage });
        }

        public string CalculaDiasHabiles(string NroDias)
        {
            int intDiasHabiles = Convert.ToInt(NroDias);
            DateTime dtFechaSum = DateTime.Now;
            string ResultadoFecha = string.Empty;
            string strListaNoHabiles = ConfigurationManager.AppSettings("gListaDiasNoHabiles");
            int intCantDias = oTransacServ.Constants.numeroVeinte;
            int intCont = 0;

            Claro.Web.Logging.Info("Ejecuta Método : ", "CalculaDiasHabiles", "Inicio");

            try
            {

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
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error("Método : CalculaDiasHabiles - ", "Menssage de Error", ex.Message);
            }

            Claro.Web.Logging.Info("Ejecuta Método : ", "CalculaDiasHabiles", "Fín - ResultadoFecha : " + ResultadoFecha);
            return ResultadoFecha;



        }

        public JsonResult GetValidateETA(string strIdSession, string vJobTypes, string vPlansId)
        {
            string strTipoOrden = string.Empty;

            string strorigen = string.Empty;
            string strIdplano = string.Empty;
            string strUbigeo = string.Empty;
            string strTipoServ = string.Empty;



            strorigen = KEY.AppSettings("gConstHFCOrigen");
            strIdplano = vPlansId;
            strTipoServ = KEY.AppSettings("gConstHFCTipoServicio");


            ETAFlowRequestHfc objETAFlowRequestHfc = new ETAFlowRequestHfc();
            ETAFlowResponseHfc objETAFlowResponseHfc;
            objETAFlowRequestHfc.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            objETAFlowRequestHfc.an_tipsrv = strTipoServ;
            objETAFlowRequestHfc.an_tiptra = Convert.ToInt(vJobTypes);
            objETAFlowRequestHfc.as_origen = strorigen;
            objETAFlowRequestHfc.av_idplano = strIdplano;
            objETAFlowRequestHfc.av_ubigeo = strUbigeo; OrderTypesResponseHfc objOrderTypesResponse = null;
            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
            objOrderTypesRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem();


            try
            {
                Claro.Web.Logging.Info("IdSession: ", strIdSession, "Inicio Método : GetValidateETA");

                #region "Valida ETA"

                if (vJobTypes != null)
                {

                    if (vJobTypes.IndexOf(".|") == Convert.ToInt(Claro.SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableMenosUno))
                    {
                        objOrderTypesRequest.vIdtiptra = vJobTypes;
                    }
                    else
                    {
                        objOrderTypesRequest.vIdtiptra = vJobTypes.Substring(0, vJobTypes.Length - 2);
                    }

                    objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<OrderTypesResponseHfc>(() =>
                    {
                        return new FixedTransacServiceClient().GetOrderType(objOrderTypesRequest);
                    });

                }

                if (objOrderTypesResponse.ordertypes == null)
                {
                    strTipoOrden = oTransacServ.Constants.PresentationLayer.NumeracionMENOSUNO;
                }
                else
                {
                    if (objOrderTypesResponse.ordertypes.Count() == 0)
                    {
                        strTipoOrden = oTransacServ.Constants.PresentationLayer.NumeracionMENOSUNO;
                    }
                    else
                    {
                        strTipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;
                    }
                }





                objETAFlowResponseHfc = Claro.Web.Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().ETAFlowValidate(objETAFlowRequestHfc);
                });

                objGenericItem.Descripcion = string.Empty;
                objGenericItem.Codigo = objETAFlowResponseHfc.ETAFlow.an_indica.ToString();
                objGenericItem.Codigo2 = objETAFlowResponseHfc.ETAFlow.as_codzona + "|" + strIdplano + "|" + strTipoOrden;

                Claro.Web.Logging.Info("IdSession: ", strIdSession, "Método : GetValidateETA- objGenericItem.Codigo " + objGenericItem.Codigo + "objGenericItem.Codigo2: " + objGenericItem.Codigo2);

                Claro.Web.Logging.Info("IdSession: ", strIdSession, "Fín Método : GetValidateETA - Codigo : " + objGenericItem.Codigo);

                switch (objETAFlowResponseHfc.ETAFlow.an_indica)
                {
                    case -1:
                        objGenericItem.Descripcion = CSTS.Functions.GetValueFromConfigFile("strMsgNoExistePlano", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -2:
                        objGenericItem.Descripcion = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -3:
                        objGenericItem.Descripcion = CSTS.Functions.GetValueFromConfigFile("strMsgNoExistePlanoUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -4:
                        objGenericItem.Descripcion = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteTipoTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case -5:
                        objGenericItem.Descripcion = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteTipoServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        break;
                    case 1:
                        objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.CriterioMensajeOK;
                        break;
                    case 0:
                        objGenericItem.Descripcion = Claro.SIACU.Transac.Service.Constants.PresentationLayer.CriterioMensajeNOOK;
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, "Método : GetValidateETA", ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: ", strIdSession, "Fín Método : GetValidateETA - Codigo" + objGenericItem.Codigo + " Descripcion : " + objGenericItem.Descripcion);

            return Json(new { data = objGenericItem });

        }

        public FixedTransacService.CaseTemplate DatosPlantillaCaso(Model.HFC.RetentionCancelServicesModel oModel)
        {
            FixedTransacService.CaseTemplate oPlantillaCampoData = new FixedTransacService.CaseTemplate();

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Inicio Método : DatosPlantillaCaso", "Inicio");


            oPlantillaCampoData.X_CAS_8 = (oModel.Reintegro == String.Empty ? CONSTANT.Constants.numeroCero : Convert.ToDouble(oModel.Reintegro));
            oPlantillaCampoData.X_CAS_9 = Convert.ToDouble(CONSTANT.Constants.numeroCero);
            oPlantillaCampoData.MONTO_RECARGA = oPlantillaCampoData.X_CAS_8;
            oPlantillaCampoData.X_OPERATOR_PROBLEM = oModel.vMotiveSot;// validar

            oPlantillaCampoData.X_CAS_3 = (oModel.Accion == CONSTANT.Constants.strLetraR ? "Retenido" : "No Retenido");
            oPlantillaCampoData.X_CAS_7 = (oModel.hidSupJef == CONSTANT.Constants.strLetraS ? CONSTANT.Constants.strUno : CONSTANT.Constants.strCero);
            oPlantillaCampoData.X_SUSPENSION_DATE = Convert.ToDate(oModel.FechaCompromiso);
            oPlantillaCampoData.X_FIXED_NUMBER = String.Empty;
            oPlantillaCampoData.NRO_TELEFONO = oModel.NroCelular;
            oPlantillaCampoData.X_CAS_16 = oModel.DesSubMotivo;
            oPlantillaCampoData.X_CAS_5 = oModel.DesSubMotivo;
            oPlantillaCampoData.X_CAS_15 = oModel.DescCacDac;
            oPlantillaCampoData.X_CAS_30 = oModel.Note;
            oPlantillaCampoData.X_CAS_4 = (oModel.TotalInversion == String.Empty ? CONSTANT.Constants.strCero : oModel.TotalInversion);
            oPlantillaCampoData.X_CAS_6 = oModel.DesAccion;

            if (oPlantillaCampoData.X_CAS_3.Equals("No Retenido"))
            {
                oPlantillaCampoData.X_FLAG_OTHER_PROBLEMS = oModel.PagoAPADECE;
                oPlantillaCampoData.X_MODEL = String.Empty;
            }

            oPlantillaCampoData.X_ADDRESS = oModel.AdressDespatch;
            oPlantillaCampoData.X_CAS_1 = oModel.Reference;
            oPlantillaCampoData.X_CAS_2 = oModel.Departament_Fact;
            oPlantillaCampoData.X_CAS_17 = oModel.District;
            oPlantillaCampoData.X_CAS_18 = oModel.Pais_Fac;
            oPlantillaCampoData.X_CAS_19 = oModel.Provincia;
            oPlantillaCampoData.X_CAS_20 = CONSTANT.Constants.strCero;
            oPlantillaCampoData.X_CAS_21 = oModel.PlaneCodeBilling;
            oPlantillaCampoData.X_CAS_30 = oModel.Note;
            if (oModel.Flag_Email)
            {
                oPlantillaCampoData.X_FLAG_GPRS = "1";
                oPlantillaCampoData.X_CAS_29 = oModel.Destinatarios;


            }else {

                oPlantillaCampoData.X_FLAG_GPRS = "0";
                oPlantillaCampoData.X_CAS_29 = string.Empty;

            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Fín Método : DatosPlantillaCaso", "Fín");
            return oPlantillaCampoData;
        }

        public bool Auditoria(Model.HFC.RetentionCancelServicesModel oModel)
        {
            bool FlatResultado;
            string strusuarioAutoriza = oModel.CurrentUser;  // validar
            string strAmmount = CONSTANT.Constants.strCero;
            string[,] strDetails = new string[8, 3];
            string strService = KEY.AppSettings("gServicioLlamadaSalienteNoFact");
            string ConstEventGraRC = ConfigurationManager.AppSettings("gConstEventGraRC");

            Claro.Web.Logging.Info(oModel.IdSession, "Transaccion", "Begin Auditoria");

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
            strDetails[6, 1] = string.Empty;
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


            Claro.Web.Logging.Info(oModel.IdSession, "Transaccion", "End Auditoria");
            return FlatResultado;
        }

        public FixedTransacService.Interaction InsertCaso(FixedTransacService.Interaction oCaso, FixedTransacService.CaseTemplate oPlantillaCaso, Model.HFC.RetentionCancelServicesModel oModel,
                                                string strEstadoForm)
        {

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Método : InsertCaso : ", "Inicio InsertCaso");

            FixedTransacService.Interaction oResponseCase = new FixedTransacService.Interaction();
            FixedTransacService.CaseTemplate oPlantillaResponse = new FixedTransacService.CaseTemplate();
            string ContingenciaClarify = System.Configuration.ConfigurationManager.AppSettings["gConstContingenciaClarify"];
            bool resultado = false;
            string IdCaso = string.Empty;

            string strFlgRegistrado = ConstantsHFC.strUno;

            try
            {
                if (oCaso.OBJID_CONTACTO == null || oCaso.OBJID_CONTACTO == "0" || oCaso.OBJID_CONTACTO == "")
                {
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
                if (oCaso.OBJID_SITE == null || oCaso.OBJID_SITE == "0" || oCaso.OBJID_SITE == "")
                {
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
                if (strEstadoForm == "N")
                {
                    if (ContingenciaClarify != "SI")
                    {
                        oResponseCase = GetCreateCase(oCaso);
                        IdCaso = oResponseCase.CASO_ID;
                        resultado = true;
                    }
                    else
                    {
                        oResponseCase = GetInsertCase(oCaso);
                        IdCaso = oResponseCase.CASO_ID;
                        resultado = true;
                    }
                }
                else
                {
                    resultado = true;
                }

                if (resultado && (IdCaso != null && IdCaso != "" && oPlantillaCaso != null))
                {   
                    oPlantillaCaso.ID_CASO = IdCaso;
                    oPlantillaResponse = GuardarPlantillaCaso(oPlantillaCaso, strEstadoForm);
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, "Error : ", ex.Message);
            }
            oResponseCase.CASO_ID = oPlantillaResponse.ID_CASO;
            oResponseCase.FLAG_INSERCION_CASO = oPlantillaResponse.FLAG_INSERCION;
            oResponseCase.MESSAGE_CASO = oPlantillaResponse.MESSAGE;
            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Método : InsertCaso : ", "Fín InsertCaso - CASO_ID : " + oResponseCase.CASO_ID + "FLAG_INSERCION : " + oResponseCase.FLAG_INSERCION + "oResponseCase.MESSAGE_CASO :" + oResponseCase.MESSAGE_CASO);
            return oResponseCase;
        }

        public FixedTransacService.CaseTemplate GuardarPlantillaCaso(FixedTransacService.CaseTemplate oPlantilla, string vEstadoForm)
        {
            string ContingenciaClarify = string.Empty;
            ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            FixedTransacService.CaseTemplate oResponse = new FixedTransacService.CaseTemplate();
            Claro.Web.Logging.Info("Transaccion: ", "GuardarPlantilla", "Inicio : GuardarPlantilla");

            try
            {
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
                    if (ContingenciaClarify != CONSTANT.Constants.Variable_SI)
                    {
                        oResponse = ActualizaPlantillaCaso(oPlantilla);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Método GuardarPlantilla : ", "Error : ", ex.Message);
            }
            Claro.Web.Logging.Info("Transaccion: ", "GuardarPlantilla", "Fín : GuardarPlantilla");
            return oResponse;
        }

        public bool GetDesactivatedContract(Model.HFC.RetentionCancelServicesModel oModel)
        {

            bool resultado = false;
            FixedTransacService.AuditRequest audit =
        App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            FixedTransacService.Customer objRequestCliente = new FixedTransacService.Customer();
            try
            {
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, " Método : Inicio GetDesactivatedContract");

                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "CustomerID==>" + oModel.CustomerId);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Account==>" + oModel.Account);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "ContractId==>" + oModel.ContractId);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "DescCacDac==>" + oModel.DescCacDac);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "BillingCycle==>" + oModel.BillingCycle);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Msisdn==>" + oModel.Msisdn);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Reason==>" + ConfigurationManager.AppSettings("gConstkeyReasonRCS"));
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "vMotiveSot==>" + oModel.vMotiveSot);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Date_Present==>" + oModel.fechaActual);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "flagNdPcs==>" + oModel.flagNdPcs);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "TotalInversion==>" + oModel.TotalInversion);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "FlagOccApadece==>" + oModel.FlagOccApadece);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "MontoFidelizacion==>" + oModel.MontoFidelizacion);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "MontoPCs==>" + oModel.MontoPCs);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "FechaProgramacion==>" + oModel.FechaProgramacion);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "vServicesType==>" + oModel.vServicesType);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "DNI_RUC==>" + oModel.DNI_RUC);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "PlaneCodeBilling==>" + oModel.PlaneCodeBilling);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "SubMotivePCS==>" + oModel.SubMotivePCS);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Reintegro==>" + oModel.Reintegro);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Email==>" + oModel.Email);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "AreaPCs==>" + oModel.AreaPCs);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "CodigoService==>" + ConfigurationManager.AppSettings("gConstkeyCodSerRCS"));
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "DateProgrammingSot==>" + oModel.DateProgrammingSot.Substring(6, 4) + "-" + oModel.DateProgrammingSot.Substring(3, 2) + "-" + oModel.DateProgrammingSot.Substring(0, 2));
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "ValidaETA==>" + (oModel.ValidaETA == "1" ? ObtenerHoraAgendaETA(oModel.IdSession, oModel.vSchedule) : oModel.vSchedule));
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "Trace==>" + CONSTANT.Constants.strUno);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "TypeWork==>" + oModel.TypeWork);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "CurrentUser==>" + oModel.CurrentUser);
                RegisterLog(oModel.IdSession, "GetDesactivatedContract", "TypeWork==>" + oModel.Observation + "" + (oModel.IdConsulta == null ? string.Empty : oModel.IdConsulta.PadLeft(10, '0')));

                objRequestCliente.audit = audit;
                objRequestCliente.audit.Session = oModel.IdSession;
                objRequestCliente.ApplicationName = ConfigurationManager.AppSettings("gConstTipoHFC");
                objRequestCliente.UserApplication = oModel.CurrentUser;
                objRequestCliente.CustomerID = oModel.CustomerId;
                objRequestCliente.Account = oModel.Account;
                objRequestCliente.ContractID = oModel.ContractId;
                objRequestCliente.Des_CAC = oModel.DescCacDac;
                objRequestCliente.BillingCycle = oModel.BillingCycle;
                objRequestCliente.Msisdn = oModel.Msisdn;
                objRequestCliente.Reason = ConfigurationManager.AppSettings("gConstkeyReasonRCS");
                objRequestCliente.Cod_Motive = oModel.vMotiveSot;
                objRequestCliente.Date_Present = Convert.ToDate(oModel.fechaActual);
                objRequestCliente.FlagNdPcs = oModel.flagNdPcs;
                if (Convert.ToDouble(oModel.TotalInversion) > 0)
                {
                    objRequestCliente.FlagOccApadece = CONSTANT.Constants.strUno;
                }
                else
                {
                    objRequestCliente.FlagOccApadece = CONSTANT.Constants.strCero;
                }
                objRequestCliente.MontoFidelizacion = oModel.MontoFidelizacion;
                objRequestCliente.MontoPCs = oModel.MontoPCs;
                objRequestCliente.FechaProgramacion = oModel.FechaProgramacion.Substring(6, 4) + "-" + oModel.FechaProgramacion.Substring(3, 2) + "-" + oModel.FechaProgramacion.Substring(0, 2);
                objRequestCliente.TypeServices = oModel.vServicesType;
                objRequestCliente.DocumentNumber = oModel.DNI_RUC;
                objRequestCliente.PlaneCodeBilling = oModel.PlaneCodeBilling;
                objRequestCliente.SubMotivePCS = oModel.SubMotivePCS;
                objRequestCliente.CustomerType = oModel.TypeClient;
                objRequestCliente.Observation = oModel.Observation;
                objRequestCliente.MotivePCS = oModel.MotivePCS;
                objRequestCliente.AmountPenalty = oModel.Reintegro;
                objRequestCliente.Email = oModel.Email;
                objRequestCliente.AreaPCs = oModel.AreaPCs;
                objRequestCliente.CodigoInteraction = string.Empty;
                objRequestCliente.CodigoService = ConfigurationManager.AppSettings("gConstkeyCodSerRCS");
                objRequestCliente.DateProgrammingSot = oModel.DateProgrammingSot.Substring(6, 4) + "-" + oModel.DateProgrammingSot.Substring(3, 2) + "-" + oModel.DateProgrammingSot.Substring(0, 2);

                objRequestCliente.FringeHorary = (oModel.ValidaETA == "1" ? ObtenerHoraAgendaETA(oModel.IdSession, oModel.vSchedule) : oModel.vSchedule);
                objRequestCliente.Trace = CONSTANT.Constants.strUno;
                objRequestCliente.TypeWork = oModel.TypeWork;
                objRequestCliente.Assessor = oModel.CurrentUser;

                objRequestCliente.Observation = oModel.Observation + "" + (oModel.IdConsulta == null ? string.Empty : oModel.IdConsulta.PadLeft(10, '0'));

                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, " Método : Ejecutar GetDesactivatedContract");

                resultado = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.GetDesactivatedContract(objRequestCliente);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, objRequestCliente.audit.transaction, "Error GetDesactivatedContract : " + ex.Message);

                if (ex.InnerException != null)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, objRequestCliente.audit.transaction, "Error GetDesactivatedContract : " + ex.InnerException.ToString());
                }

            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, " Método : Fín GetDesactivatedContract - Resultado : " + resultado);
            return resultado;
        }

        private string ObtenerHoraAgendaETA(string strIdSession, string cboHorario)
        {
            ArrayList lstGenericItem = new ArrayList();
            string strHora = string.Empty;
            strHora = CSTS.Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCPOSTConfig"));
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: ObtenerHoraAgendaETA", "Inicio Método : ObtenerHoraAgendaETA");
            try
            {
                lstGenericItem = App_Code.Common.GetXMLList("ListaFranjasHorariasETA");

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in lstGenericItem)
                {
                    string[] Aux = cboHorario.Split('+');
                    if (Aux[0] == "")
                    {
                        strHora = item.Codigo2;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session : " + strIdSession, "Error : ", ex.Message);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: ObtenerHoraAgendaETA", "Fín Método : ObtenerHoraAgendaETA");
            return strHora;
        }

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

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: GetTransactionScheduled", "Inicio Método : GetTransactionScheduled");
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
                        if (item.SERVC_ESTADO.Equals(Claro.Constants.NumberZeroString) || item.SERVC_ESTADO.Equals(Claro.Constants.NumberOneString) || item.SERVC_ESTADO.Equals(Claro.Constants.NumberTwoString))
                        { Resultado = false; break; }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                Resultado = false;
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: GetTransactionScheduled", "Fín Método : GetTransactionScheduled");

            return Json(new { data = Resultado });
        }

        public JsonResult GetMotiveSOTByTypeJobs(string strIdSession, int IdTipoTrabajo)
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
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Total Reg : " + objResponse.List.Count);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotiveSOTByTypeJob Reg : " + JsonConvert.SerializeObject(objResponse.List));
            return Json(new { data = objResponse.List });
        }

        #region Obtener PDF
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.HFC.RetentionCancelServicesModel oModel)
        {
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strInteraccionId = strIdInteraction;
            bool generado = false;
            string strTypeTransaction = string.Empty;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;

            InteractionServiceRequestHfc objInteractionServiceRequest = null;
            ParametersGeneratePDF parameters = new ParametersGeneratePDF();
            strNombreArchivo = "RETENCION_CANCELACION";
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Inicio ", "strNombreArchivo : " + strNombreArchivo);
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
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoHFC");
                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrContenidoComercial2 = strTexto;
                    Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Inicio ", "StrTipoTransaccion : " + strTypeTransaction);
                }
                else // No Retenido(NR)
                {
                    strTypeTransaction = ConfigurationManager.AppSettings("strNombreArchivo_Cancelacion");
                    parameters = new ParametersGeneratePDF();
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrSegmento = oModel.Segmento;
                    parameters.StrProductos = oModel.ProductType;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrMotivoCancelacion = oModel.DesMotivos;
                    parameters.StrSubMotivoCancel = oModel.DesSubMotivo;
                    parameters.strContrato = oModel.ContractId;
                    parameters.strDireccionInstalcion = oModel.AdressDespatch;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.StrFechaTransaccionProgram = oModel.DateProgrammingSot;
                    parameters.strFechaTransaccion = oModel.fechaActual;
                    parameters.StrTipoTransaccion = strTypeTransaction;
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionNoRetenidoHFC");
                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrFechaCancel = oModel.DateProgrammingSot;
                    parameters.strFechaHoraAtención = Convert.ToString(DateTime.Now);
                    parameters.StrContenidoComercial2 = strTexto;
                    Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Inicio ", "StrTipoTransaccion : " + strTypeTransaction);
                }

                GenerateConstancyResponseCommon response = GenerateContancyPDF(strIdSession, parameters);
                nombrepath = response.FullPathPDF;
                generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);

                Claro.Web.Logging.Info("nombrepath: " + nombrepath, "generado: " + generado, "strNombreArchivo : " + strNombreArchivo);
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Fín ", "nombrepath : " + nombrepath);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }
        #endregion

        private void RegisterLog(string IdSession, string Method, string Message)
        {
            Claro.Web.Logging.Info(IdSession, "RetentionCancelHFC", Method + ": " + Message);

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

            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransFideliServHFC");

            //---32650---
            var result = false;
            oModel.idCampana = Convert.ToInt(ConfigurationManager.AppSettings("strIdCampanaFideHFC"));
            oModel.typeHFCLTE = Model.HFC.typeHFCLTE.HFC;
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
            objRequest.RegistrarInteraccion.piTipo = "HFC"; //ya que se encuenta en el controlador de HFC
            objRequest.RegistrarInteraccion.piCodPlano = oModel.CodePlanInst;
            objRequest.RegistrarInteraccion.piAgente = App_Code.Common.CurrentUser;
            objRequest.pLTE = "0";

            objRequest.RegistrarDetalleInteraccion.piFirstName = oModel.name;
            objRequest.RegistrarDetalleInteraccion.piLastName = oModel.LastName;
            objRequest.RegistrarDetalleInteraccion.piInter15 = StrRegistrarDetalleInteraccion[7];
            objRequest.RegistrarDetalleInteraccion.piClarolocal1 = oModel.PaqueteODeco;

            objRequest.RegistrarCabeceraDoc.piCicloFact = oModel.BillingCycle;
            objRequest.RegistrarCabeceraDoc.piIdCliente = oModel.CustomerId;

            if (string.IsNullOrEmpty(oModel.Modalidad))
            { objRequest.RegistrarCabeceraDoc.piIdTipCliente = "2"; }
            else { objRequest.RegistrarCabeceraDoc.piIdTipCliente = (oModel.Modalidad.ToLower() == (System.Configuration.ConfigurationManager.AppSettings["ConstCorporativo"].ToLower()) ? "1" : "2"); } /* si es corporativo,1:2*/

            
            objRequest.RegistrarCabeceraDoc.piNumDoc = oModel.NroDoc;
            objRequest.RegistrarCabeceraDoc.piClienteCta = oModel.Account ;
            objRequest.RegistrarCabeceraDoc.piCiudad = oModel.Departament_Fact;
            
            objRequest.RegistrarCabeceraDoc.piDireccion = oModel.AdressDespatch;
            
            objRequest.RegistrarCabeceraDoc.piNumIdentFiscal = oModel.DNI_RUC; //[deni o ruc de sesion]
            string oTelef = string.Empty; if (!string.IsNullOrEmpty(oModel.Msisdn)) { oTelef = oModel.Msisdn; } else { oTelef = "H" + oModel.CustomerId; }
            objRequest.RegistrarDetalleDoc.ListaRegistroDetDocum = new List<RegistroDetDocum>
            {
                new RegistroDetDocum(){ pTelefono = oTelef }
            };

            objRequest.RegistrarAjusteOAC.piCodCuenta = oModel.CustomerId;
            objRequest.RegistrarExportarSap.piTextoCab = strRegistrarExportarSap[6]; //si es retencion posicion 5, si es fidelizacion posicion 6.
            //string MesPeriodo = oModel.mesVal.Split('|')[0].Trim();
            //string MesValor = oModel.mesVal.Split('|')[1].Trim();
            //FIN seteo de datos para registrar la interaccion de registro ajuste 
            string strIdInteract = string.Empty, strIdDocAut = string.Empty;
            switch (oModel.flagCargFijoServAdic)
            {
                
                case Claro.Constants.NumberZeroString: //Cargo fijo
                    var promo = true;
                        if (oModel.aplicaPromoFact) //si esta marcado el check de aplica descuento a boleta actual
                        {
                            //oModel.mesVal = MesValor;
                            if (!RegisterInteractionAdjust(oModel.IdSession, objRequest, oModel.DiscountDescription, ref strIdInteract, ref strIdDocAut, oModel.BillingCycle, oModel.Msisdn))  //if (!RegisterSAR(oModel))
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
                           // oModel.flagCargFijoServAdic = Claro.Constants.NumberOneString;
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
                case Claro.Constants.NumberTwoString: // Aumento de velocidad
                    var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(oModel.IdSession);
                    GetRegisterBonoSpeedRequest objrequest = new GetRegisterBonoSpeedRequest()
                    {
                        MessageRequest = new CommonTransacService.GetRegisterBonoSpeedMessageRequest()
                        {
                            Header = new CommonTransacService.HeadersRequest()
                            {
                                HeaderRequest = new CommonTransacService.HeaderRequest()
                                {
                                    consumer = Claro.ConfigurationManager.AppSettings("consumer"),
                                    country = Claro.ConfigurationManager.AppSettings("country"),
                                    dispositivo = App_Code.Common.GetClientIP(),
                                    language = Claro.ConfigurationManager.AppSettings("language"),
                                    modulo = Claro.ConfigurationManager.AppSettings("modulo"),
                                    msgType = Claro.ConfigurationManager.AppSettings("msgType"),
                                    operation = Claro.ConfigurationManager.AppSettings("operation"),
                                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                    system = Claro.ConfigurationManager.AppSettings("system"),
                                    timestamp = DateTime.Now.ToString(),
                                    userId = App_Code.Common.CurrentUser,
                                    wsIp = App_Code.Common.GetApplicationIp(),//"172.19.91.216",
                                    VarArg = Claro.ConfigurationManager.AppSettings("VarArg"),
                                }
                            },
                            Body = new GetRegisterBonoSpeedBodyRequest()
                            {
                                bonoId = oModel.bonoId,
                                coId = oModel.codId,
                                periodo = oModel.PeriodoBono
                            }

                        }

                    };
                    objrequest.audit = objAuditRequest;
                    GetRegisterBonoSpeedResponse objresponseRS = GetRegisterBonoSpeed(objrequest, oModel.IdSession);
                    result = true;
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
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Ingresa a generar Constancia");
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
                            TipificarDatosMenores(ConfigurationManager.AppSettings("strCodigoTransDatosMenorHFC"), oModel);
                        }
                    }

                    byte[] attachFile = null;
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                    if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                    {
                        MensajeEmail = GetSendEmailCFSA(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
                    }
                }
                }
                

                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionFidelizacion - vDesInteraction : " + vDesInteraction);
                ResultadoAudit = Auditoria(oModel);
            }


            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionRetention" + " " + "Parametros Output : vDesInteraction" + vDesInteraction + "vFlagInteraction : " + vFlagInteraction + "vInteractionId " + vInteractionId + "ResultadoAudit : " + ResultadoAudit);
            return Json(new { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail, errorMessage });
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

                oInteraccion.ObjidContacto = oModel.objIdContacto;// GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
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

        public JsonResult SaveTransactionRetentionDeco(Model.HFC.RetentionCancelServicesModel oModel, Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE.InstallUninstallDecoderModel objViewModel)
        {
            var errorMessage = "";
            string vInteractionId = string.Empty;
            string vDesInteraction = string.Empty;
            bool ResultadoAudit = false;
            string strRutaArchivo = string.Empty;
            string vFlagInteraction = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            
            #region ObtenerSiteObj

            setIdContactoSide(oModel);
            #endregion
            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServHFC");
            #region WSUninstallInstallDecosLTE-RegisterInstaDecoActHFC
            //Instalar deco y descuento Servicio WSUninstallInstallDecosLTE-RegisterInstaDecoActHFC.
            RegistarInstaDecoAdiHFCResponse ResultRegistarInstaDeco = new RegistarInstaDecoAdiHFCResponse();

            Customer customer = new Customer()
            {
                LastName = objViewModel.InsInteractionPlusModel.LastName,
                ContractID = oModel.ContractId,
                CustomerID = oModel.CustomerId,
                Name = oModel.name,
                FullName = objViewModel.AuditRegister.strNombreCliente,
                LegalCountry = objViewModel.ImplementLoyalty.Pais,
                Departament = objViewModel.ImplementLoyalty.Departamento,
                Province = objViewModel.ImplementLoyalty.Provincia,
                District = objViewModel.ImplementLoyalty.Distrito,
                Address = objViewModel.InsInteractionPlusModel.Address,
                Email = oModel.Email,
                DocumentType = objViewModel.InsInteractionPlusModel.TypeDocument,
                DocumentNumber = objViewModel.InsInteractionPlusModel.DocumentNumber,
                BillingCycle = oModel.BillingCycle,
                BusinessName = objViewModel.InsInteractionPlusModel.Reason,
                PhoneReference = oModel.TelefonoReferencia,
                Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId,//oModel.NroCelular
                CodigoService = oModel.CodigoService,
                LegalAgent = oModel.RepresentLegal,
                SiteCode = oModel.objIdSite,
                ContactCode = oModel.objIdContacto,
                Assessor = oModel.CodigoAsesor
            };

            ServiceByPlan servicioPlan = new ServiceByPlan()
            {
                CodServiceType = "",
                ServiceType = objViewModel.Decos[0].ServiceType,
                IDEquipment = objViewModel.Decos[0].IdEquipo,
                Sncode = objViewModel.Decos[0].SnCode,
                Spcode = objViewModel.Decos[0].SpCode,
                GroupServ = objViewModel.Decos[0].ServiceGroup,
                Codtipequ = objViewModel.Decos[0].CodTipEquipo,
                CantEquipment = objViewModel.Decos[0].Quantity,//Cantidad Equipo
                CodServSisact = objViewModel.Decos[0].CodServicePvu,
                DesServSisact = objViewModel.Decos[0].desc
            };

            List<GetRegistarInstaDecoAdiHFC.listaRequestOpcional> lista = new List<GetRegistarInstaDecoAdiHFC.listaRequestOpcional>();
            GetRegistarInstaDecoAdiHFC.listaRequestOpcional obj;
            
            obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
            {
                campo = "FechaActivacion",
                valor = oModel.FechaActivacion.ToString("yyyy-MM-dd HH:mm:ss")
            };
            lista.Add(obj);

            obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
            {
                campo = "EnvioCorreo",
                valor = (oModel.Flag_Email) ? "SI" : "NO"
            };
            lista.Add(obj);

            obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
            {
                campo = "AplicaETA",
                valor = (oModel.ValidaETA != "0") ? "SI" : "NO"
            };
            lista.Add(obj);

            if (!oModel.ValidaETA.Equals(SIACU.Transac.Service.Constants.strCero))
            {
                obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
                {
                    campo = "IdSubtipoOrden",//"SubTipoOrden",
                    valor = objViewModel.RegistrarEta.SubTipoOrden//.SubTipoOrden
                };
                lista.Add(obj);

                obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
                {
                    campo = "CodigoBucket",
                    valor = objViewModel.RegistrarEta.Idbucket
                };
                lista.Add(obj);

                obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
                {
                    campo = "IdFranjaSeleccion",
                    valor = objViewModel.RegistrarEtaSeleccion.IdConsulta//obtenido del sp req
                };
                lista.Add(obj);

                obj = new GetRegistarInstaDecoAdiHFC.listaRequestOpcional()
                {
                    campo = "IdInteraccionFranja",
                    valor = objViewModel.RegistrarEtaSeleccion.IdInteraccion//obtenido del sp agregándole ceros
                };
                lista.Add(obj);
            }

            RegistarInstaDecoAdiHFCRequest objRequest = new RegistarInstaDecoAdiHFCRequest()
            {
                audit = audit,
                Customer = customer,
                ServiceByPlan = servicioPlan,
                AplicacaBono = "1",
                CodigoCampana = ConfigurationManager.AppSettings("strIdCampanaFideHFC"),
                CanalAtencion = oModel.DescCacDac,
                CargoBono = oModel.MontoDescuento,
                CargoFijoCIGV = objViewModel.Decos[0].Costocigv.Replace(',', '.'),
                CargoFijoSIGV = objViewModel.Decos[0].Costosing.Replace(',', '.'),
                CodigoPlano = oModel.Plan,
                CodigoSistema = ConfigurationManager.AppSettings("strCodigoSistema"),
                CodigoTipoTrabajo = objViewModel.SotPending.StrTipTra,
                CodigoUbigeo = oModel.Ubigeo,
                CodigoZona = "0",
                CostoInstalacion = oModel.costoWSInst,
                EstadoLinea = oModel.EstadoLinea,
                FechaActivacion = oModel.FechaActivacion,
                FechaProgramacion = DateTime.Parse(objViewModel.RegistrarEtaSeleccion.FechaCompromiso),
                Fidelizar = "SI",
                FlagTBono = "1",
                FranjaHoraria = objViewModel.RegistrarEtaSeleccion.Franja,
                MontoIGV = oModel.ValorIGV,
                NumeroClaro = objViewModel.InsInteractionPlusModel.ClaroNumber,
                PeriodoBono = oModel.mesVal,
                CurrentPlan = oModel.PlanActual,
                listaRequestOpcional = lista
            };
            try
            {
                ResultRegistarInstaDeco = Claro.Web.Logging.ExecuteMethod<RegistarInstaDecoAdiHFCResponse>(
                    () =>
                    {
                        return _oServiceFixed.RegistarInstaDecoAdiHFC(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }
            #endregion


            if (ResultRegistarInstaDeco.ResponseCode == ConstantsHFC.strCero || ResultRegistarInstaDeco.ResponseCode == ConstantsHFC.numeroSeis.ToString())
            {
                oModel.SOT = ResultRegistarInstaDeco.CodSolot;
                //====== INI Flujo de servicios de tipificar y generacion de constancia. TAL como funciona para retencion cancelacion de fase 1.
                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Ingresa a generar la interaccion");
                var oInteraccion = GenerateInteraccion(oModel);
                vInteractionId = TipificarRetencionFidelizacion(oModel.IdSession, oInteraccion, oModel);
                oModel.CodigoInteraction = vInteractionId;
                vDesInteraction = (!string.IsNullOrEmpty(vInteractionId) ? ConfigurationManager.AppSettings("strMsgTranGrabSatis") : ConfigurationManager.AppSettings("strMensajeDeError"));
                if (vInteractionId != "")
                {
                    vDesInteraction += (ResultRegistarInstaDeco.ResponseCode == ConstantsHFC.numeroSeis.ToString() ? ". " + ResultRegistarInstaDeco.ResponseMessage : "");
                    Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Ingresa a generar Constancia");
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
                            TipificarDatosMenores(ConfigurationManager.AppSettings("strCodigoTransDatosMenorHFC"), oModel);
                        }
                    }

                    byte[] attachFile = null;
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                    if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                    {
                        MensajeEmail = GetSendEmailCFSA(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
                    }
                }
                }
                else
                {
                    errorMessage = ResultRegistarInstaDeco.ResponseMessage;
                }
                

                Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionRetentionDeco - vDesInteraction : " + vDesInteraction);
                ResultadoAudit = Auditoria(oModel);
                ////====== FIN Flujo de servicios de tipificar y generacion de constancia. TAL como funciona para retencion cancelacion de fase 1.
            
            }

            Claro.Web.Logging.Info("IdSession: " + oModel.IdSession, "Transaccion: " + audit.transaction, "Fin Método : SaveTransactionRetentionDeco" + " " + "Parametros Output : vDesInteraction" + vDesInteraction + "vFlagInteraction : " + vFlagInteraction + "vInteractionId " + vInteractionId + "ResultadoAudit : " + ResultadoAudit);
            return Json(new { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail, errorMessage });

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
        /// <summary>
        /// Metodo que consulta los Bonos Aumento de Velocidad
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        /// <item>PROY-140319 IDEA-140331 Bono aumenta velocidad Internet fijo</item>
        public JsonResult GetConsultServiceBono(CommonTransacService.GetConsultServiceBonoRequest objRequest, string strIdSession)
        {
            CommonTransacService.GetConsultServiceBonoResponse objConsultServiceResponse = null;
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(strIdSession);
            string strValidaBono = "";
            ArrayList dataList = new ArrayList();

            try
            {
                objRequest.audit = objAuditRequest;

                objConsultServiceResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetConsultServiceBonoResponse>(() =>
                {
                    return oServiceCommon.GetConsultServiceBono(objRequest, objAuditRequest.Session);
                });
                if (objConsultServiceResponse != null)
                {
                    if (objConsultServiceResponse.MessageResponse.Body.flagBonoActual == 0)
                    {
                        strValidaBono = ConfigurationManager.AppSettings("gConstMsgBonoFlag0");

                    }
                    if (objConsultServiceResponse.MessageResponse.Body.bonoActual != null && objConsultServiceResponse.MessageResponse.Body.bonoActual.Count > 0)
                    {
                        var listBAVFide = objConsultServiceResponse.MessageResponse.Body.bonoActual;

                        for (int i = 0; i < listBAVFide.Count; i++)
                        {
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].descBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].descBonoActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].fecActBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].fecActBonoActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].velocidadFinalActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].velocidadFinalActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].periodoBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].periodoBonoActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].fecVigenBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].fecVigenBonoActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].snCodeBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].snCodeBonoActual;
                            objConsultServiceResponse.MessageResponse.Body.bonoActual[i].nombreBonoActual = objConsultServiceResponse.MessageResponse.Body.bonoActual[i].nombreBonoActual;

                            dataList.Add(listBAVFide[i]);
                            }

                        if (objConsultServiceResponse.MessageResponse.Body.flagBonoActual == 2 && !string.IsNullOrEmpty(objConsultServiceResponse.MessageResponse.Body.bonoActual[0].nombreBonoActual)
                              )
                        {
                            strValidaBono = string.Format(ConfigurationManager.AppSettings("gConstMsgBonoIncremento"),
                                objConsultServiceResponse.MessageResponse.Body.bonoActual[0].nombreBonoActual);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);

            }
            return Json(new { objConsultServiceResponse, strValidaBono, Data = dataList }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Metodo que permite registrar Ingremento de velocidad
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>

        GetRegisterBonoSpeedResponse GetRegisterBonoSpeed(CommonTransacService.GetRegisterBonoSpeedRequest objRequest, string strIdSession)
        {
            CommonTransacService.GetRegisterBonoSpeedResponse objResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetRegisterBonoSpeedResponse>(() =>
                {
                    return oServiceCommon.GetRegisterBonoSpeed(objRequest, objAuditRequest.Session);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return objResponse;
        }

        /// <summary>
        /// [PostValidateDeliveryBAVFide] Método que consulta los clientes con Full Claro Fijo que ya cuentan con bono de fidelización por aumento de velocidad.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>PostValidarEntregaBAVResponse</returns>
        /// <remarks>GetValidateCollaborator</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>/03/2020.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>05/03/2020.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-""- Convivencia de bono</Mot></item></list>
        public JsonResult PostValidateDeliveryBAVFide(CommonTransacService.PostValidateDeliveryBAVRequest objRequest, string strIdSession, string coId)
        {
            CommonTransacService.PostValidateDeliveryBAVResponse objPostValidateDeliveryBAVFideResponse = null;
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(strIdSession);
            string strMsjInfoValidateDeliveryBAVFide = "";

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objAuditRequest.transaction, "Inicio Método : PostValidateDeliveryBAVFide");
            try
            {
                objRequest = new PostValidateDeliveryBAVRequest()
                {
                    MessageRequest = new PostValidateDeliveryBAVMessageRequest()
                    {
                        Header = new CommonTransacService.HeadersRequest()
                        {
                            HeaderRequest = new CommonTransacService.HeaderRequest()
                            {
                                consumer = Claro.ConfigurationManager.AppSettings("consumer"),
                                country = Claro.ConfigurationManager.AppSettings("country"),
                                dispositivo = App_Code.Common.GetClientIP(),
                                language = Claro.ConfigurationManager.AppSettings("language"),
                                modulo = Claro.ConfigurationManager.AppSettings("modulo"),
                                msgType = Claro.ConfigurationManager.AppSettings("msgType"),
                                operation = Claro.ConfigurationManager.AppSettings("operation"),
                                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                system = Claro.ConfigurationManager.AppSettings("system"),
                                timestamp = DateTime.Now.ToString(),
                                userId = App_Code.Common.CurrentUser,
                                wsIp = App_Code.Common.GetApplicationIp(),//"172.19.91.216",
                                VarArg = Claro.ConfigurationManager.AppSettings("VarArg"),
                            }
                        },
                        Body = new PostValidateDeliveryBAVBodyRequest()
                        {
                            coId = coId,
                            meses = ConfigurationManager.AppSettings("strMonthsFide"),
                            codSubClase = ConfigurationManager.AppSettings("strModalitycodSubClase").Split('|')[1],
                        }
                    }

                };
                objRequest.audit = objAuditRequest;

                objPostValidateDeliveryBAVFideResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.PostValidateDeliveryBAVResponse>(() =>
                {
                    return oServiceCommon.PostValidateDeliveryBAV(objRequest, objAuditRequest.Session);
                });

                if (objPostValidateDeliveryBAVFideResponse != null)
                {
                    if (objPostValidateDeliveryBAVFideResponse.MessageResponse.Body.codigoRespuesta == "0")
                    {
                        if (objPostValidateDeliveryBAVFideResponse.MessageResponse.Body.flagAplica == 1)
                        {
                            strMsjInfoValidateDeliveryBAVFide = string.Format(ConfigurationManager.AppSettings("strMsjInfoValidateDeliveryReteFideBAV").Split('|')[1],
                                ConfigurationManager.AppSettings("strMonthsFide"));
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objAuditRequest.transaction, "Fin Método : PostValidateDeliveryBAVFide" + " " + "Parametros de Salida : pCoId: " + objRequest.MessageRequest.Body.coId + ", pMeses: " + objRequest.MessageRequest.Body.meses + ", pCodSubClase: " + objRequest.MessageRequest.Body.codSubClase);
            return Json(new { objPostValidateDeliveryBAVFideResponse, strMsjInfoValidateDeliveryBAVFide }, JsonRequestBehavior.AllowGet);

        }
    }

}