using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails;
using Claro.Helpers.Transac.Service;
using Claro.Web;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.CallDetails;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC;
using System.Net;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class CallDetailsController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly COMMON.CommonTransacServiceClient _oServiceCommon = new COMMON.CommonTransacServiceClient();
        private const string ListCallsDetailHfcModel = "ListCallsDetailHfcModel";
        private const string ArrayBits = "ArrayBits";
        #region Variables Globales y Constantes

        private string blnSeguridad;

        private string gstrUserHostName;
        private string gstrLocalAddr;
        private string gstrServerName;

        #endregion

        // GET: /Transactions/BilledCallsDetail/
        public ActionResult HFCBilledCallsDetail()
        {
            var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "CallDetails", "HFCBilledCallsDetail", "Iniciando HFCBilledCallsDetail");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
            Logging.Configure();
            return View();
        }

        public ActionResult HFCUnbilledCallDetail()
        {
            Logging.Configure();
            return View();
        }

        #region Buscar
        public JsonResult GetSearch(BpelCallDetailModel model)
        {
            var BPEL = new BilledCallsDetailHfcModel();
            BPEL.ListExportExcel = new List<BilledCallsDetail>();
            try
            {
                var msg1 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetSearch", "BUSCAR", "Inicio de Busqueda");
                Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg1);

                BPEL = GetBilledCallsDetails(model);

                var msg2 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetSearch", "BUSCAR", "Fin Busqueda");
                Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg2);

                if (BPEL.ListExportExcel != null && BPEL.ListExportExcel.Count > 0)
                {
                    BPEL.ListExportExcel = BPEL.ListExportExcel.OrderBy(x => x.NroCustomer).ThenBy(x => Convert.ToDate(x.StrDate)).ThenBy(x => Convert.ToDate(x.StrHour)).ToList();
                    
                    var posList = 1;

                    foreach (var item in BPEL.ListExportExcel)
                    {
                        item.CurrentNumber = posList.ToString();
                        posList++;
                    }

                    Session["ListCallsDetailHfcModel"] = BPEL.ListExportExcel;

                    var strTotal = "";
                    string[] arrTotal = new string[10];
                    strTotal = GetTotalTR_Detail_Calls(BPEL.ListExportExcel);
                    arrTotal = strTotal.Split(';');

                    BPEL.Total = Functions.GetFormatHHMMSS(Functions.CheckInt64(Functions.CheckDbl(arrTotal[1], 2)));
                    BPEL.TotalSms = Functions.CheckInt64(Functions.CheckDbl(arrTotal[2], 2)).ToString();
                    BPEL.TotalMms = Functions.CheckInt64(Functions.CheckDbl(arrTotal[3], 2)).ToString();
                    BPEL.TotalGprs = Functions.CheckInt64(Functions.CheckDbl(arrTotal[4], 2)).ToString() + " " + ConstantsHFC.PresentationLayer.kitracKB;
                    BPEL.TotalRegistro = BPEL.ListExportExcel.Count().ToString();
                    BPEL.CargoFinal = "S/. " + Functions.CheckStr(Functions.CheckDbl(arrTotal[0], 4));

                    var msg3 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetSearch", "BUSCAR", "Total/TotalSms/TotalMms");
                    Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg3);
                }
            }
            catch (Exception e)
            {
                return Json(e);
            }


            return Json(new { data = BPEL });
        }
        public BilledCallsDetailHfcModel GetBilledCallsDetails(BpelCallDetailModel model)
        {
            BilledCallsDetailHfcModel BPEL = new BilledCallsDetailHfcModel();
            BPEL.ListExportExcel = new List<BilledCallsDetail>();

            var lstFacture = new List<BilledCallsDetail>();

            var msg1 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetBilledCallsDetails", "BUSCAR", "Telefono");
            Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg1);
            try
            {
                if (model.StrTelephone != string.Empty)
                {
                    var strType = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableL;
                    BpelCallDetailResponse objBpelCallDetailResponse;
                    var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.StrIdSession);

                    model.DetailCallRequestBpelModel.InteractionBpelModel.Phone = string.Format("{0}{1}",ConfigurationManager.AppSettings("gConstKeyCustomerInteract"),
                    model.DetailCallRequestBpelModel.ContactUserBpelModel.CustomerId);

                    var headerRequest = new HeaderRequestTypeBpel
                    {
                        Canal = "WEB",
                        IdAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                        UsuarioAplicacion = model.HeaderRequestTypeBpelModel.UsuarioAplicacion,
                        UsuarioSesion = model.HeaderRequestTypeBpelModel.UsuarioAplicacion,
                        IdTransaccionEsb ="", //pendiente por revisar
                        IdTransaccionNegocio = objAuditRequest.transaction,
                        NodoAdicional = "1"
                    };

                    //ContactUser
                    var contactUser = new ContactUserBpel
                    {
                        Usuario = model.DetailCallRequestBpelModel.ContactUserBpelModel.Usuario ?? "",
                        Nombres = model.DetailCallRequestBpelModel.ContactUserBpelModel.Nombres ?? "",
                        Apellidos = model.DetailCallRequestBpelModel.ContactUserBpelModel.Apellidos ?? "",
                        RazonSocial = model.DetailCallRequestBpelModel.ContactUserBpelModel.RazonSocial ?? "",
                        TipoDoc = model.DetailCallRequestBpelModel.ContactUserBpelModel.TipoDoc ?? "",
                        NumDoc = model.DetailCallRequestBpelModel.ContactUserBpelModel.NumDoc ?? "",
                        Domicilio = model.DetailCallRequestBpelModel.ContactUserBpelModel.Domicilio ?? "",
                        Distrito = model.DetailCallRequestBpelModel.ContactUserBpelModel.Distrito ?? "",
                        Departamento = model.DetailCallRequestBpelModel.ContactUserBpelModel.Departamento ?? "",
                        Provincia = model.DetailCallRequestBpelModel.ContactUserBpelModel.Provincia ?? "",
                        Modalidad = ConfigurationManager.AppSettings("gConstKeyStrModalidad")
                    };

                    //CustomerClfy
                    var customerClfy = new CustomerClfyBpel
                    {
                        Account = string.Empty,
                        ContactObjId = string.Empty,
                        FlagReg = model.DetailCallRequestBpelModel.CustomerClfyBpelModel.FlagReg
                    };

                    //Interaction
                    var strUno = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO;
                    var strCero = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO;
                    var interact = new InteractionBpel
                    {
                        Contactobjid = string.Empty,
                        Siteobjid = string.Empty,
                        Account = string.Empty,
                        Phone = model.DetailCallRequestBpelModel.InteractionBpelModel.Phone ?? "",
                        Tipo = model.StrHdnType ?? string.Empty,
                        Clase = model.StrHdnClase ?? string.Empty,
                        Subclase = model.StrHdnSubClass ?? string.Empty,
                        MetodoContacto = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault"),
                        TipoInter = ConfigurationManager.AppSettings("AtencionDefault"),
                        Agente = model.DetailCallRequestBpelModel.InteractionBpelModel.Agente,
                        UsrProceso = ConfigurationManager.AppSettings("USRProcesoSU"),
                        HechoEnUno = strCero,
                        Notas = model.DetailCallRequestBpelModel.InteractionBpelModel.Notas,
                        FlagCaso =strUno,
                        Resultado = ConfigurationManager.AppSettings("Ninguno"),
                        CoId = model.DetailCallRequestBpelModel.InteractionBpelModel.CoId,
                        CodPlano = model.DetailCallRequestBpelModel.InteractionBpelModel.CodPlano

                    };

                    //InteractionPlus
                    var StrEmail = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Email;
                    var interactPlus = new InteractionPlusBpel
                    {
                        ClaroNumber = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.ClaroNumber ?? "",
                        DniLegalRep = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.DniLegalRep ?? "",
                        DocumentNumber = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.DocumentNumber ?? "",
                        FirstName = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.FirstName ?? "",
                        LastName = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.LastName ?? "",
                        NameLegalRep = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.NameLegalRep ?? "",
                        FlagRegistered = (StrEmail != null) ? strUno : strCero,
                        Email = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Email ?? "",
                        Inter30 = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter30 ?? "",
                        Inter29 = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter29 ?? "",
                        Inter15 = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter15 ?? "",
                        Inter16 = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter15 ?? "",
                        Inter18 = model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter18 ?? "",
                        Birthday = DateTime.UtcNow.AddDays(-10).ToString("dd/MM/yyyy").Replace("/", ""),
                        ExpireDate = DateTime.UtcNow.AddDays(-10).ToString("dd/MM/yyyy").Replace("/", "")
                    };
                    //DetalleLlamadasRequestBpel
                    var objDetalleLlamadaRequest = new DetalleLlamadasRequestBpel
                    {
                        TipoConsulta = model.DetailCallRequestBpelModel.TipoConsulta ?? "",
                        Msisdn = model.DetailCallRequestBpelModel.Msisdn ?? "",
                        FechaInicio = string.Empty,
                        FechaFin = string.Empty,
                        ContactUserBpel = contactUser,
                        CustomerClfyBpel = customerClfy,
                        InteractionBpel = interact,
                        InteractionPlusBpel = interactPlus,
                        FlagConstancia = model.DetailCallRequestBpelModel.FlagConstancia ?? "",
                        IpCliente = model.DetailCallRequestBpelModel.IpCliente ?? "",
                        TipoConsultaContrato = model.DetailCallRequestBpelModel.TipoConsultaContrato ?? "",
                        ValorContrato = model.DetailCallRequestBpelModel.ValorContrato ?? "",
                        FlagContingencia = ConfigurationManager.AppSettings("gConstContingenciaClarify_SIACU"),
                        CodigoCliente = model.DetailCallRequestBpelModel.CodigoCliente ?? "",
                        FlagEnvioCorreo = model.DetailCallRequestBpelModel.FlagEnvioCorreo ?? "",
                        FlagGenerarOcc = model.DetailCallRequestBpelModel.FlagGenerarOcc ?? "",
                        InvoiceNumber = model.DetailCallRequestBpelModel.InvoiceNumber ?? "",
                        Periodo = model.DetailCallRequestBpelModel.Periodo ?? "",
                        TipoProducto = model.DetailCallRequestBpelModel.TipoProducto ?? ""
                    };

                    var objRequest = new BpelCallDetailRequest()
                    {
                        audit = objAuditRequest,
                        DetalleLlamadasRequestBpel = objDetalleLlamadaRequest,
                        HeaderRequestTypeBpel = headerRequest,
                        StrSecurity = ConstantsHFC.strCero
                    };

                    var msg3 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetBilledCallsDetails", "BPEL", "INICIANDO EL BPEL");
                    Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg3);

                    objBpelCallDetailResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetBilledCallsDetailHfC(objRequest));

                    BPEL.StrResponseCode = objBpelCallDetailResponse.StrResponseCode;
                    BPEL.StrResponseMessage = objBpelCallDetailResponse.StrResponseMessage;

                    BPEL.FechaCicloIni = objBpelCallDetailResponse.FechaCicloIni;
                    BPEL.FechaCicloFin = objBpelCallDetailResponse.FechaCicloFin;

                    var msg4 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetBilledCallsDetails", "BPEL", "LISTADO DE REGISTRO DEL BPEL");
                    Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + "", msg4);

                    if (objBpelCallDetailResponse.ListBilledCallsDetailHfc.Count > 0)
                    {
                        var tempLst = objBpelCallDetailResponse.ListBilledCallsDetailHfc;
                        lstFacture = Mapper.Map<List<BilledCallsDetail>>(tempLst);
                        BPEL.ListExportExcel = lstFacture;
                    }

                    var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "GetBilledCallsDetails", "BPEL", "FIN DEL LISTADO DE REGISTRO DEL BPEL");
                    Logging.Info("IdSession: " + model.StrIdSession, "Transaccion: " + model.StrTransaction, msg);
                }

            }
            catch (Exception e)
            {
                Logging.Error("IdSession: " + model.StrIdSession, "Transaccion: " + model.StrTransaction, e.Message);
            }

            return BPEL;
        }
        #endregion

        public JsonResult GetMonthYearLimit(string strIdSession)
        {
            string LastDate = string.Empty;
            LastDate = string.Format("{0}{1}{2}{3}", "01/", DateTime.Now.AddMonths(-1).Month.ToString(), "/", DateTime.Now.Year.ToString());
            int intMonth = Convert.ToInt(FunctionsSIACU.GetValueFromConfigFile("UltimosMesesDBTO", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
            var lastDateFacture = new DateTime(Convert.ToInt(LastDate.Split('/')[2]), Convert.ToInt(LastDate.Split('/')[1]), Convert.ToInt(LastDate.Split('/')[0]));
            var dateFactureLimit = lastDateFacture.AddMonths(-intMonth);
            int intMonthYearLimit = 0;

            if (Convert.ToInt(FunctionsSIACU.GetValueFromConfigFile("ConfDetalleLlamada", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))) == ConstantsHFC.numeroCero)
            {
                intMonthYearLimit = (dateFactureLimit.Year * 100) + dateFactureLimit.Month;
            }
            else
            {
                intMonthYearLimit = Convert.ToInt(FunctionsSIACU.GetValueFromConfigFile("UltimosMesesDBTOTOPE", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));

            }
            var hiddenMonthYearLimit = Convert.ToString(intMonthYearLimit.ToString());
            return Json(hiddenMonthYearLimit, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(ConfigurationManager.AppSettings(strIdSession, "strConstArchivoSIACUTHFCConfigMsg"));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaSeleccioneM", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaSeleccioneADSMA", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaSGenTip", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaNEDEMLL", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaDIEMAIL", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strMensajeEmail", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaEstaSegGuarCam", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaUstNoCuentaCASPRET", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaSeleccioneA", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaSeleccioneT", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaMsgNoHayLlam", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaMsgErrGu", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaMsgErrNoTra", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaMsgExitoTra", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strAlertaNecesitaAuto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("strMensajeMaxLongNota", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(FunctionsSIACU.GetValueFromConfigFile("hidMsgConsBusca", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
            lstMessage.Add(Request.ServerVariables["SERVER_NAME"]);
            lstMessage.Add(Request.ServerVariables["LOCAL_ADDR"]);
            lstMessage.Add(Request.UserHostName);
            lstMessage.Add(ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasFact"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstKeyDetalleLlamadasFacturadasHFC"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstKeyDetalleLlamadasFacturadasLTE"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnabledPermission(string StrCadOption)
        {
            var lstPermission = new List<string>();
            var strPermisos = StrCadOption;//Dim strPermisos As String = HFCPOST_Session.UsuarioAcceso.CadenaOpciones
            var strkeyConsultaCliente = ConfigurationManager.AppSettings("gConstkeyEnviarCorreoNotificacionDL");
            var strkeyVerNroDest = ConfigurationManager.AppSettings("gConstkeyVerNumeroDestinoTransaccion");
            var strKeyPrintDetail = ConfigurationManager.AppSettings("gConstEvtImprimirDetalleLlamada");
            var strKeyExportDetail = ConfigurationManager.AppSettings("gConstEvtExportarDetalleLlamada");
            var strKeyButonExport = ConfigurationManager.AppSettings("gConstEvtBotonExportarDetLlamada");
            var strKeyButonPrint = ConfigurationManager.AppSettings("gConstEvtBotonImprimirDetLlamada");
            var strKeyButonSearch = ConfigurationManager.AppSettings("gConstEvtBuscarDetalleLlamada");

            int chkSentEmail = strPermisos.IndexOf(strkeyConsultaCliente, StringComparison.Ordinal) + 1;
            int btnSecurity = strPermisos.IndexOf(strkeyVerNroDest, StringComparison.Ordinal) + 1;
            int hdnPermission = strPermisos.IndexOf(strKeyPrintDetail, StringComparison.Ordinal) + 1;
            int hdnPermisionExport = strPermisos.IndexOf(strKeyExportDetail, StringComparison.Ordinal) + 1;
            int btnExport = strPermisos.IndexOf(strKeyButonExport, StringComparison.Ordinal) + 1;
            int btnPrint = strPermisos.IndexOf(strKeyButonPrint, StringComparison.Ordinal) + 1;
            int hdnPermissionBus = strPermisos.IndexOf(strKeyButonSearch, StringComparison.Ordinal) + 1;

            if (chkSentEmail > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "chkSentEmail", false));
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "chkSentEmail", true));
            }

            if (btnSecurity > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnSecurity", ConstantsHFC.strUno));
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnSecurity", ConstantsHFC.strCero));
            }

            if (hdnPermission > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermission", ConstantsHFC.blcasosVariableSI));
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermission", ConstantsHFC.blcasosVariableNO));
            }

            if (hdnPermisionExport > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermisionExport", ConstantsHFC.blcasosVariableSI));//blcasosVariableSI
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermisionExport", ConstantsHFC.blcasosVariableNO));
            }

            if (btnExport > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnExport", true));//true
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnExport", false));
            }
            if (btnPrint > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnPrint", true));//true
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "btnPrint", false));
            }
            if (hdnPermissionBus > ConstantsHFC.numeroCero)
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermissionBus", ConstantsHFC.blcasosVariableSI));
            }
            else
            {
                lstPermission.Add(string.Format("{0}/{1}", "hdnPermissionBus", ConstantsHFC.blcasosVariableNO));
            }

            return Json(lstPermission, JsonRequestBehavior.AllowGet);
        }

        #region Exportar
        public JsonResult GetExportExcel(string strIdSession, string strStarDate, string strEndDate, string strNameUser, string strTypeProduct, string strCustomer)
        {
            ExcelHelper oExcelHelper = new ExcelHelper();
            BilledCallsDetailHfcModel objExportExcel = new BilledCallsDetailHfcModel();
            objExportExcel.ListExportExcel = (List<BilledCallsDetail>)Session["ListCallsDetailHfcModel"];
            var url = string.Empty;
            List<int> lstHelperPlan = new List<int>();
            int exito = 0;
            if (objExportExcel.ListExportExcel != null)
            {
                objExportExcel.StardDate = strStarDate;
                objExportExcel.EndDate = strEndDate;
                objExportExcel.Customer = strCustomer;

                objExportExcel.ListExportExcel.ToList().ForEach(y =>
                {
                    y.DestinationPhone = y.TelephoneDestExport;
                });
                exito = 1;
                lstHelperPlan = ValidateExportExcel(objExportExcel.ListExportExcel);
            }
            if (exito == ConstantsHFC.numeroUno)
            {
                FunctionsSIACU.GetValueFromConfigFile("strMensajeConsDetLlamRetReg", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            else
            {
                FunctionsSIACU.GetValueFromConfigFile("strMensajeConsDetLlamNoRetReg", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            if (strTypeProduct == ConfigurationManager.AppSettings("gConstTipoHFC"))
            {
                url = ConfigurationManager.AppSettings("CONSTEXPORT_HFCBILLEDCALLDETAILS");
            }
            else
            {
                url = ConfigurationManager.AppSettings("CONSTEXPORT_LTEBILLEDCALLDETAILS");
            }

            string path = oExcelHelper.ExportExcel(objExportExcel, url, lstHelperPlan,15);
            return Json(path);
        }
        public List<int> ValidateExportExcel(List<BilledCallsDetail> objRelationPlan)
        {
            List<int> list = Enumerable.Range(7, 6).ToList();

            foreach (BilledCallsDetail item in objRelationPlan)
            {
                if (item.CurrentNumber == Claro.Constants.NumberOneString) { list.Remove(7); }
                if (item.StrDate == Claro.Constants.NumberOneString) { list.Remove(8); }
                if (item.StrHour == Claro.Constants.NumberOneString) { list.Remove(9); }
                if (item.DestinationPhone == Claro.Constants.NumberOneString) { list.Remove(10); }
                if (item.NroCustomer == Claro.Constants.NumberOneString) { list.Remove(11); }
                if (item.Consumption == Claro.Constants.NumberOneString) { list.Remove(12); }
                if (item.CostSoles == Claro.Constants.NumberOneString) { list.Remove(13); }
                if (item.Type == Claro.Constants.NumberOneString) { list.Remove(14); }
                if (item.Destination == Claro.Constants.NumberOneString) { list.Remove(15); }
                if (item.Operator == Claro.Constants.NumberOneString) { list.Remove(16); }
                break;
            }

            return list;
        }
        #endregion

        public ActionResult HfcBilledCallsDetailPrint(string strIdSession, string strCustomer, string strCuenta, string strPlan, string strTypeCustomer, string strRazonSocial, string strTelephone, string strNameUser, string strSn, string strIpServidor, string strInvoiceNumber)
        {
            string strTelephoneCustomer = strTelephone;
            BilledCallsDetailHfcModel model = new BilledCallsDetailHfcModel();
            model.IdSession = strIdSession;
            model.ListExportExcel = new List<BilledCallsDetail>();
            var exito = ConstantsHFC.numeroCero;

            var msgError1 = string.Empty;

            if (strInvoiceNumber == null || strTelephoneCustomer == null)
            {
                msgError1 = FunctionsSIACU.GetValueFromConfigFile("strAlertaParamIncoPagina", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }

            if (strTypeCustomer.ToUpper() == SIACU.Transac.Service.Constants.PresentationLayer.gstrBusinessUpper)
            {
                model.Customer = strCustomer;
            }
            else
            {
                model.Customer = strRazonSocial;
            }

            model.Cuenta = strCuenta;
            model.Plan = strPlan;
            model.Telephone = strTelephoneCustomer;
            var strType = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableL;
            model.ListExportExcel = (List<BilledCallsDetail>)Session["ListCallsDetailHfcModel"];

            if (model.ListExportExcel != null && model.ListExportExcel.Count > 0)
            {
                exito = 1;
                var strTotal = string.Empty;
                string[] arrTotal = new string[10];
                strTotal = GetTotalTR_Detail_Calls(model.ListExportExcel);
                arrTotal = strTotal.Split(';');

                model.Total = Functions.GetFormatHHMMSS(Functions.CheckInt64(Functions.CheckDbl(arrTotal[1], 2)));
                model.TotalSms = Functions.CheckInt64(Functions.CheckDbl(arrTotal[2], 2)).ToString();
                model.TotalMms = Functions.CheckInt64(Functions.CheckDbl(arrTotal[3], 2)).ToString();
                model.TotalGprs = Functions.CheckInt64(Functions.CheckDbl(arrTotal[4], 2)) + " " + ConstantsHFC.PresentationLayer.kitracKB;
                model.TotalRegistro = model.ListExportExcel.Count().ToString();
                model.CargoFinal = "S/. " + Functions.CheckStr(Functions.CheckDbl(arrTotal[0], 4)).ToString();
            }
            else
            {
                model.Total = ConstantsHFC.PresentationLayer.kitracVariableCeroDecimalString;
                model.TotalSms = ConstantsHFC.PresentationLayer.NumeracionCERO;
                model.TotalMms = ConstantsHFC.PresentationLayer.NumeracionCERO;
                model.TotalGprs = ConstantsHFC.PresentationLayer.NumeracionCERO + " " + ConstantsHFC.PresentationLayer.kitracKB;
                model.TotalRegistro = "0";
                model.CargoFinal = "S/. " + ConstantsHFC.PresentationLayer.kitracVariableCeroDecimalString;
            }

            var strTransaction = ConfigurationManager.AppSettings("gConstEvtImprimirDetLlamFact");
            var strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            InsertAuditory(strTransaction, strService, strTelephone, strNameUser, strIdSession, strSn, strIpServidor, string.Empty, string.Empty);

            return View(model);

        }

        public JsonResult HFCUnbilledCallDetail_PageLoad(string strIdSession, string codPlanTarifario, string estadoAcceso, string strTransaction)
        {
            var sPlanesRestingidos = FunctionsSIACU.GetValueFromConfigFile("CadenaPlanesRestringidosParaConsulta", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            var resultRestrictPlan = RestrictPlan(codPlanTarifario, sPlanesRestingidos, estadoAcceso);//HFCPOST_Session.DatosLinea.Cod_Plan_Tarifario
            var dictionaryPageLoad = new Dictionary<string, string>
            {
                { "RestrictPlan", resultRestrictPlan.ToString() },
                { "strMensajeInformacionRestrin", FunctionsSIACU.GetValueFromConfigFile("strMensajeInformacionRestrin", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "strMsgDatosLinea", FunctionsSIACU.GetValueFromConfigFile("strMsgDatosLinea", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "gstrVariableT", ConstantsHFC.PresentationLayer.gstrVariableT },
                { "TituloPagina", ConfigurationManager.AppSettings("gConstKeyDetalleLlamadasFacturadasNoFacConst")},
                { "hdnMensaje1", FunctionsSIACU.GetValueFromConfigFile("strAlertaNecesitaAuto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje2", FunctionsSIACU.GetValueFromConfigFile("strAlertaUstNoCuentaCASPRET", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje3", FunctionsSIACU.GetValueFromConfigFile("strAlertaNecesitaSelTelef", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje4", FunctionsSIACU.GetValueFromConfigFile("strAlertaSGenTip", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje5", FunctionsSIACU.GetValueFromConfigFile("strAlertaNEDEMLL", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnSiteUrl",  ConfigurationManager.AppSettings("strRutaSiteInicio")},
                { "hdnMensaje12", FunctionsSIACU.GetValueFromConfigFile("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje13", FunctionsSIACU.GetValueFromConfigFile("strAlertaMsgNoHayLlam", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje14", FunctionsSIACU.GetValueFromConfigFile("strMsgFechasVacias", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hdnMensaje15", FunctionsSIACU.GetValueFromConfigFile("strMsgFechasInvalidas",ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                { "hidTransaccion", ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaDetalleLLamadaNoFacturado")},
                { "hdnLocalAdd", Request.ServerVariables["LOCAL_ADDR"]},
                { "hdnServName",  Request.ServerVariables["SERVER_NAME"]},
                { "hidCodOpcion", ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact")},
                { "startDateConfig",DateTime.Now.AddMonths(-1 * 3).ToShortDateString()}
            };

            gstrUserHostName = Request.UserHostName;
            gstrLocalAddr = Request.ServerVariables["LOCAL_ADDR"];
            gstrServerName = Request.ServerVariables["SERVER_NAME"];

            Logging.Info(strIdSession, strTransaction, "RestrictPlan " + dictionaryPageLoad["RestrictPlan"]);
            Logging.Info(strIdSession, strTransaction, "strMensajeInformacionRestrin " + dictionaryPageLoad["strMensajeInformacionRestrin"]);
            Logging.Info(strIdSession, strTransaction, "strMsgDatosLinea " + dictionaryPageLoad["strMsgDatosLinea"]);
            Logging.Info(strIdSession, strTransaction, "gstrVariableT " + dictionaryPageLoad["gstrVariableT"]);
            Logging.Info(strIdSession, strTransaction, "TituloPagina " + dictionaryPageLoad["TituloPagina"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje1 " + dictionaryPageLoad["hdnMensaje1"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje2 " + dictionaryPageLoad["hdnMensaje2"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje3 " + dictionaryPageLoad["hdnMensaje3"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje4 " + dictionaryPageLoad["hdnMensaje4"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje5 " + dictionaryPageLoad["hdnMensaje5"]);
            Logging.Info(strIdSession, strTransaction, "hdnSiteUrl " + dictionaryPageLoad["hdnSiteUrl"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje12 " + dictionaryPageLoad["hdnMensaje12"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje13 " + dictionaryPageLoad["hdnMensaje13"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje14 " + dictionaryPageLoad["hdnMensaje14"]);
            Logging.Info(strIdSession, strTransaction, "hdnMensaje15 " + dictionaryPageLoad["hdnMensaje15"]);
            Logging.Info(strIdSession, strTransaction, "hidTransaccion " + dictionaryPageLoad["hidTransaccion"]);
            Logging.Info(strIdSession, strTransaction, "hdnLocalAdd " + dictionaryPageLoad["hdnLocalAdd"]);
            Logging.Info(strIdSession, strTransaction, "hdnServName " + dictionaryPageLoad["hdnServName"]);
            Logging.Info(strIdSession, strTransaction, "hidCodOpcion " + dictionaryPageLoad["hidCodOpcion"]);

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult HFCUnbilledCallDetail_EnabledPermission(string strIdSession, string strPermisos, string product)
        {
            try
            {
                var dictionaryResponse = new Dictionary<string, object>();
                var strkeyVerNumeroDestino = ConfigurationManager.AppSettings("gConstkeyVerNumeroDestinoConsulta_HFCPOST");
                blnSeguridad = strPermisos.IndexOf(strkeyVerNumeroDestino, StringComparison.OrdinalIgnoreCase) + 1 > 0 ? ConstantsHFC.PresentationLayer.NumeracionUNO : ConstantsHFC.PresentationLayer.NumeracionCERO;

                var strKeyExportarLlamadaLinea = product == "HFC" ? ConfigurationManager.AppSettings("gConstEvtExportarDetaLlamadaLin_HFCPOST") : ConfigurationManager.AppSettings("gConstEvtExportarDetaLlamadaLin_LTEPOST");
                dictionaryResponse.Add("hdnPermisoExp", strPermisos.IndexOf(strKeyExportarLlamadaLinea, StringComparison.OrdinalIgnoreCase) + 1 > 0 ? ConstantsHFC.PresentationLayer.gstrVariableSIminus : ConstantsHFC.PresentationLayer.gstrVariableNOminus);

                var strKeyBotonExportar = product == "HFC" ? ConfigurationManager.AppSettings("gConstEvtBotonExportarDetLlamLin_HFCPOST") : ConfigurationManager.AppSettings("gConstEvtBotonExportarDetLlamLin_LTEPOST");
                dictionaryResponse.Add("btnExportar", strPermisos.IndexOf(strKeyBotonExportar, StringComparison.OrdinalIgnoreCase) + 1 > 0);

                var strKeyBotonBuscar = product == "HFC" ? ConfigurationManager.AppSettings("gConstEvtBuscarDetaLlamadaLin_HFCPOST") : ConfigurationManager.AppSettings("gConstEvtBuscarDetaLlamadaLin_LTEPOST");
                dictionaryResponse.Add("hdnPermisoBus", strPermisos.IndexOf(strKeyBotonBuscar, StringComparison.OrdinalIgnoreCase) + 1 > 0 ? ConstantsHFC.PresentationLayer.gstrVariableSIminus : ConstantsHFC.PresentationLayer.gstrVariableNOminus);

                return new JsonResult
                {
                    Data = dictionaryResponse,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, "strIdTransaction", ex.Message);
                return new JsonResult
                {
                    Data = "",
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        #region Guardar Detalle de Facturacion
        //Inicio de Guardar
        public JsonResult Save(BilledCallsDetailHfcModel model)
        {
            List<string> strInteractionId = new List<string>();
            try
            {
                model.ListExportExcel = (List<BilledCallsDetail>)Session["ListCallsDetailHfcModel"];

                strInteractionId = Transaction(model);
                Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + model.Transaction, "CALLDETAIL GUARDAR UNBILLED Respuesta de tamaño: " + strInteractionId.Count);
                return Json(strInteractionId);
            }
            catch (Exception e)
            {
                Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + model.Transaction, "CALLDETAIL GUARDAR UNBILLED Respuesta de tamaño: " + strInteractionId.Count);
                Logging.Error("IdSession: " + model.IdSession, "Transaccion: " + model.Transaction, e.Message);
            }

            return Json(strInteractionId);
        }

        //Generar la transaccion
        public List<string> Transaction(BilledCallsDetailHfcModel model)
        {
            var objInteractionModel = new InteractionModel();
            var oPlantillaDat = new TemplateInteractionModel();
            var listInteraction = new List<string>();

            var customerModel = Mapper.Map<CustomersDataModel>(model);
            try
            {
                //validateCustomerId
                var validateCustomerId = GetValidateCustomerId(customerModel, string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), model.CustomerId), model.IdSession);
                if (!validateCustomerId)
                {
                    listInteraction.Add(ConstantsHFC.DAReclamDatosVariableNO_OK);
                    listInteraction.Add(FunctionsSIACU.GetValueFromConfigFile("gConstKeyNoValidaCustomerID", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                }
                else
                {
                    //Datos de Interaccion
                    objInteractionModel = DatInteraction(model);
                    var blnEjectTransaction = true;

                    var strUserSession = string.Empty;
                    var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                    var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                    //DatosPlantillaInteracion
                    oPlantillaDat = GetDatTemplateInteraction(model.Transaction, model.Telephone, model.NroDoc,
                        model.Email, model.MonthEmision, model.YearEmision, model.DescCacDac,
                        string.Empty, model.Note,
                        model.NameComplet, model.LastName, model.RepresentLegal,
                        model.IdSession, model.ContractId);

                    var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.CustomerId;

                    //Grabar Interaccion
                    var resultInteraction = InsertInteraction(
                        objInteractionModel,
                        oPlantillaDat,
                        strNroTelephone,
                        strUserSession,
                        strUserAplication,
                        strPassUser,
                        true,
                        model.IdSession,
                        model.CustomerId);

                    var lstaDatTemplate = new List<string>();
                    foreach (KeyValuePair<string, object> par in resultInteraction)
                    {
                        lstaDatTemplate.Add(par.Value.ToString());
                        Logging.Info(strUserSession, strUserSession, "RESULTADOS INTERACCION: " + par.Value);
                    }

                    //Envio de Correo y Envio de Constancia
                    //if (!string.IsNullOrEmpty(model.Email))
                    //{
                    //    SendCorreo(lstaDatTemplate[3], model);
                    //}

                    string[,] strDetails = new string[3, 3];

                    strDetails[0, 0] = "Telefono";
                    strDetails[0, 1] = string.Empty;
                    strDetails[0, 2] = "Telefono";

                    strDetails[1, 0] = "InvoiceNumber";
                    strDetails[1, 1] = model.MonthEmision + "" + model.YearEmision;
                    strDetails[1, 2] = "Número de Invoice Number";

                    strDetails[2, 0] = "Tipo Transaccion";
                    strDetails[2, 1] = model.Transaction;
                    strDetails[2, 2] = "Transacción Detalle de Llamada";

                    int count = ((strDetails.Length / 4) - 1);
                    var sbText = new StringBuilder();
                    for (int i = 0; i < count; i++)
                    {
                        if (strDetails.GetValue(i, 1) != null && strDetails.GetValue(i, 2) != null)
                        {
                            sbText.Append(" " + strDetails.GetValue(i, 1) + " : ");
                            sbText.Append(strDetails.GetValue(i, 2));
                        }
                    }

                    var strDescription = FunctionsSIACU.GetValueFromConfigFile("strMensajeConsDetLlamRetReg", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    var strTransaction = ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamFact");
                    var strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
                    var strText = sbText + "" + strDescription;

                    //Inserta Auditoria
                    SaveAuditM(strTransaction, strService, strText, model.Telephone, model.NameComplet, model.IdSession, model.Sn, model.IpServidor);

                    resultInteraction.ToList().ForEach(x =>
                    {
                        listInteraction.Add(x.Value.ToString());
                    });
                }
            }
            catch (Exception e)
            {
                Logging.Error("IdSession: " + model.IdSession, "Transaccion: " + model.Transaction, e.Message);
            }

            foreach (var item in listInteraction)
            {
                Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + model.Transaction, "CALLDETAIL LISTA DATA INTERACTION: " + item);
            }

            return listInteraction;
        }

        //Obtener Datos de Interaccion
        public InteractionModel DatInteraction(BilledCallsDetailHfcModel model)
        {
            var objInteractionModel = new InteractionModel();
            //tipificacion
            var typeTypi = string.Empty;
            
            if(model.product.Equals("HFC")){
                typeTypi = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.HfcSave;
            }else if(model.product.Equals("LTE")){
                typeTypi = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.LteSave;
            }

            var tipification = GetTypificationHFC(model.IdSession, typeTypi);

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

            //ValidaCustomer_Id
            var phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.CustomerId;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CallDetailController", "DatInteraction", "SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
            Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, msg);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = phone,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
            objInteractionModel.DateCreaction = DateTime.Now.ToString("MM/dd/yyyy");
            objInteractionModel.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.CustomerId;
            objInteractionModel.Type = objInteractionModel.Type;
            objInteractionModel.Class = objInteractionModel.Class;
            objInteractionModel.SubClass = objInteractionModel.SubClass;
            objInteractionModel.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
            objInteractionModel.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
            objInteractionModel.Result = ConfigurationManager.AppSettings("Ninguno");
            objInteractionModel.MadeOne = ConstantsHFC.strCero;
            objInteractionModel.Note = model.Note;
            objInteractionModel.FlagCase = ConstantsHFC.strCero;
            objInteractionModel.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
            objInteractionModel.Contract = model.ContractId;
            objInteractionModel.Plan = model.Plan;
            objInteractionModel.Agenth = model.CurrentUser;

            return objInteractionModel;
        }

        //Insertar Interaccion
        public Dictionary<string, object> InsertInteraction(InteractionModel objInteractionModel,
            TemplateInteractionModel oPlantillaDat,
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
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CallDetailController", "InsertInteraction", "SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            var result = new Dictionary<string, string>();

            //Validacion de Contingencia
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

            if (!string.IsNullOrEmpty(rInteraccionId))
            {
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }

            dictionaryResponse.Add("rInteraccionId", rInteraccionId);
            Logging.Info(strIdSession, strIdSession, "CALL DETAIL rInteraccionId: " + rInteraccionId);
            return dictionaryResponse;
        }

        //Envio de Correo
        public void SendCorreo(string idSession, string strInteraccionId, BilledCallsDetailHfcModel model, string strAdjunto, byte[] attachFile)
        {
            var date = DateTime.Now.ToString("MM/dd/yyyy");
            //obtener datos de plantilla de interaccion
            var oDatTemplateInteraction = GetInfoInteractionTemplate(model.IdSession, strInteraccionId);

            var strDestinatarios = model.Email;

            var strMessage = "<html>";
            strMessage += "<head>";
            strMessage += "<style type='text/css'>";
            strMessage += "<!--";
            strMessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strMessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strMessage += "-->";
            strMessage += "</style>";
            strMessage += "<body>";
            strMessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strMessage += "<tr><td>";
            strMessage += CreateHeaderEmail("Detalle de Llamadas Salientes - Facturado", model.DescCacDac, date, model.NameComplet, strInteraccionId, model.RepresentLegal, oDatTemplateInteraction.X_CLARO_NUMBER, model.TypeDoc, model.NroDoc, model.Telephone);
            strMessage += "</td></tr>";        

            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strMessage += "</table>";
            strMessage += "</body>";
            strMessage += "</html>";

            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
            new CommonTransacService.SendEmailRequestCommon()
            {
                audit = AuditRequest,
                strSender = strRemitente,
                strTo = strDestinatarios,
                strMessage = strMessage,
                strAttached = strAdjunto,
                strSubject = ConfigurationManager.AppSettings("gConstAsuntoDetLlamadaSaliente"),
                AttachedByte = attachFile
            };

            try
            {
                objGetSendEmailResponse = Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>
                (
                    () => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); }
                );


                Logging.Info("666", "666", "INFO EMAIL CONTROLLER SALIENTES : " + objGetSendEmailResponse.Exit);
            }
            catch (Exception ex)
            {
                Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
            }
        }

        public string CreateArchiveDetailCall(BilledCallsDetailHfcModel model)
        {
            var strCustomer = string.Empty;
            var strRutaArchive = string.Empty;

            if (model.TypeClient.ToUpper() == SIACU.Transac.Service.Constants.PresentationLayer.gstrBusinessUpper)
            {
                strCustomer = "Razón Social: " + model.NameComplet;
            }
            else
            {
                strCustomer = "Cliente: " + model.RazonSocial;
            }

            var i = 0;
            var decTotalMont = Convert.ToDecimal(SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCeroDouble);
            var intTotalSMS = SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            var strTotalCall = string.Empty;
            var intHorTotales = SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            var intMinuteTotales = SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            var intSegTotales = SIACU.Transac.Service.Constants.PresentationLayer.kitracVariableCero;
            var strConsumo = string.Empty;
            var strType = string.Empty;
            var strHtml = new StringBuilder();

            strHtml.Append("<HTML>");
            strHtml.Append("<HEAD>");
            strHtml.Append("</HEAD>");
            strHtml.Append("<body leftMargin='0' topMargin='0' marginwidth='0' marginheight='0'>");
            strHtml.Append("<form id='frmPrincipal' name='frmPrincipal' method='post' runat='server'>");
            strHtml.Append("<div id='impresion'>");
            strHtml.Append("<font color=#333333>");

            strHtml.Append("<table cellSpacing='0' cellPadding='0' align='center' border='0' width='100%'>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ></td>");
            strHtml.Append("</tr>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ><b><font size='2' color='#333333'>CONSULTA DE DETALLE DE LLAMADAS</font></b></td>");
            strHtml.Append("</tr>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ><b><font size='2' color='#333333'></font></b>" + strCustomer + "</td>");
            strHtml.Append("</tr>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ><b><font size='2' color='#333333'>Cuenta: </font></b>" + model.Cuenta + "</td>");
            strHtml.Append("</tr>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ><b><font size='2' color='#333333'>Telefono: </font></b>" + model.Telephone + "</td>");
            strHtml.Append("</tr>");
            strHtml.Append("<tr>");
            strHtml.Append("    <td align='center' ><b><font size='2' color='#333333'>Periodo: </font></b>" + model.Periodo + "</td>");
            strHtml.Append("</tr>");
            strHtml.Append("</table>");

            strHtml.Append("<br>");

            strHtml.Append("<table cellSpacing='0' cellPadding='0' width='100%' border='1' bordercolor='#336699'>");
            strHtml.Append("<tr>");
            strHtml.Append("<td colspan='10' height='12'></td>");
            strHtml.Append("</tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("<td></td>");
            strHtml.Append("</tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Nro</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Fecha</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Hora</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Telefono Destino</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Nro Cliente</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Consumo</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Consumo (S/.)</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Tipo Servicio</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Destino</b></td>");
            strHtml.Append("<td bgcolor='#d9534f' style='color: #fff; font-size: 10px;'><b>Operador</b></td>");
            strHtml.Append("</tr>");

            int count = 0;
            
            model.ListExportExcel.ToList().ForEach(x =>
            {
                strHtml.Append("<tr>");
                strHtml.Append("<td align='center' style='color: #333333; font-size: 10px;'>" + (count + 1).ToString() + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.StrDate + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.StrHour + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.DestinationPhone + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.NroCustomer + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.Consumption + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.CargOriginal + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.TypeCalls + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.Destination + "</td>");
                strHtml.Append("<td align='center' style='color: #333333' font-size: 10px;'>" + x.Operator + "</td>");
                strHtml.Append("</tr>");

                strConsumo = x.Consumption;
                strType = x.TypeCalls;
                if (!string.IsNullOrEmpty(strType))
                {
                    if (strType.StartsWith("SMS-S"))
                    {
                        intTotalSMS += Convert.ToInt(strConsumo);
                    }
                    else if (strType.StartsWith("LLAMADA-S"))
                    {
                        if (strConsumo.Split(':').Length == 2)
                        {
                            strConsumo = "00:" + strConsumo;
                        }
                        var datConsumo = Convert.ToDate(strConsumo);
                        intHorTotales += datConsumo.Hour;
                        intMinuteTotales += datConsumo.Minute;
                        intSegTotales += datConsumo.Second;
                    }
                }
                decTotalMont += Convert.ToDecimal(x.CargOriginal);
                count++;
            });
            //Finalizando el cuerpo del documento
            strHtml.Append("</table>");

            //Calculando el total de minutos
            int intRestoSegundos = 0;
            // intMinuteTotales += Math.DivRem(intSegTotales, 60, intRestoSegundos);

            int intRestoMinute = 0;
            // intMinuteTotales += Math.DivRem(intMinuteTotales, 60, intRestoMinute);

            //Pie del documento
            strHtml.Append("</font>");
            strHtml.Append("</div>");
            strHtml.Append("</form>");
            strHtml.Append("</body>");
            strHtml.Append("</HTML>");
            var date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            strRutaArchive = CreateArcchivePdf(strHtml.ToString(), SIACU.Transac.Service.Constants.PresentationLayer.gstrCallFacture + date);
            return strRutaArchive;
        }
        #endregion

        public JsonResult LoadDates_HFC(string strIdSession, string cicloFacturacion, string cuenta)
        {
            var vFechaInicio = string.Empty;
            var vFecha = string.Empty;
            var vDia = string.Empty;
            var vFechaUltFact = string.Empty;

            vDia = cicloFacturacion;
            vFechaUltFact = GetLastDate_HFC(cuenta, strIdSession);

            if (!string.IsNullOrEmpty(vFechaUltFact))
            {
                vFechaUltFact = Convert.ToDate(vFechaUltFact).ToString("yyyyMM");
            }

            vFecha = DateTime.UtcNow.ToString("yyyyMM");
            if (!string.IsNullOrEmpty(vFechaUltFact))
            {
                if (vFechaUltFact == vFecha)
                {
                    vFecha = vDia + DateTime.UtcNow.ToShortDateString().Substring(2);
                    vFechaInicio = vFecha;
                }
                else
                {
                    vFecha = DateTime.UtcNow.AddMonths(-1).ToString("yyyyMM");
                    if (vFechaUltFact == vFecha)
                    {
                        vFecha = vDia + DateTime.UtcNow.AddMonths(-1).ToShortDateString().Substring(2);
                        vFechaInicio = vFecha;
                    }
                    else
                    {
                        vFecha = vDia + DateTime.UtcNow.AddMonths(-2).ToShortDateString().Substring(2);
                        vFechaInicio = vFecha;
                    }
                }
            }
            else
            {
                vFecha = vDia + DateTime.UtcNow.AddMonths(-1).ToShortDateString().Substring(2);
                vFechaInicio = vFecha;
            }

            var dictionaryResponse = new Dictionary<string, string>
            {
                {"txtFechaInicio", vFechaInicio},
                {"txtFechaFin", DateTime.UtcNow.AddHours(-5).ToShortDateString()},
                {"hidFechaIniTel", vFechaInicio},
                {"hidFechaFinTel", DateTime.UtcNow.AddHours(-5).ToShortDateString()}
            };

            return new JsonResult
            {
                Data = dictionaryResponse,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string GetLastDate_HFC(string codeCustomer, string strIdSession)
        {
            var objListItemVm = new List<HELPERS.CommonServices.ListItemVM>();
            var lastDate = string.Empty;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            FactureDBTORequest objFactureDbtoRequest = new FactureDBTORequest
            {
                audit = objAuditRequest,
                strCodeCustomer = codeCustomer
            };

            var objFactureDbtoResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetFactureDBTO(objFactureDbtoRequest));
            objFactureDbtoResponse.LstGenericItem.ToList().ForEach(item =>
            {
                var objItemVm = new HELPERS.CommonServices.ListItemVM()
                {
                    Code = item.Codigo ?? "",
                    Description = item.Descripcion ?? "",
                    Code2 = item.Codigo2 ?? "",
                };
                objListItemVm.Add(objItemVm);
            });

            if (objListItemVm.Count > 0)
            {
                lastDate = objListItemVm[0].Description;
            }

            var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "CallDetailController", "GetLastDate_HFC", "SIACU_TOLS_OBTENERDATOSDECUENTA");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objAuditRequest.transaction, msg);

            return lastDate;
        }

        //public JsonResult GetUnBilledCallsDetail(string strIdSession, string tlf, string vContratoId, string fInicio, string fFin, string strLocalAd, string strServName, string product)
        public JsonResult GetUnBilledCallsDetail(BpelCallDetailModel objViewModel)
        {
            var lstCallDetail = ListUnBilledCallsDetail(objViewModel);
            if (lstCallDetail.LstPhoneCall.Count > 0)
            {
                lstCallDetail.LstPhoneCall = lstCallDetail.LstPhoneCall.OrderBy(x => x.Telefono_Origen).ThenBy(x => Convert.ToDate(x.Fecha)).ThenBy(x => Convert.ToDate(x.Hora)).ToList();

                var posList = 1;

                foreach (var item in lstCallDetail.LstPhoneCall)
                {
                    item.NroRegistro = posList;
                    posList++;
                }

                Session["LstUnbilledCallDetail"] = lstCallDetail.LstPhoneCall;
            }

            return new JsonResult
            {
                Data = lstCallDetail,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //public List<UnBilledCallsDetail> ListUnBilledCallsDetail(string strIdSession, string strTransaction, string tlf, string vContratoId, string fInicio, string fFin, string strLocalAd, string strServName, string product)
        public CallDetailUnBilledVM ListUnBilledCallsDetail(BpelCallDetailModel objViewModel)
        {
            var responseFinal = new CallDetailUnBilledVM();

            var response = new List<UnBilledCallsDetail>();

            try
            {
                var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(objViewModel.StrIdSession);
                var strNameTransaction = objViewModel.DetailCallRequestBpelModel.TipoProducto == "HFC" ? ConstantsHFC.DETALLE_LLAMADAS_NO_SALIENTE.HfcBuscar : ConstantsHFC.DETALLE_LLAMADAS_NO_SALIENTE.LteBuscar;
                var tipification = GetTypificationHFC(objViewModel.StrIdSession, strNameTransaction);
                var result = tipification.FirstOrDefault(y => y.Type == objViewModel.DetailCallRequestBpelModel.TipoProducto);
                if (result == null)
                {
                    result = new TypificationModel(); ;
                }
                CallDetailResponse objResponse;

                var headerRequest = new HeaderRequestTypeBpel
                {
                    Canal = "WEB",
                    IdAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                    UsuarioAplicacion = objViewModel.HeaderRequestTypeBpelModel.UsuarioAplicacion,
                    UsuarioSesion = objViewModel.HeaderRequestTypeBpelModel.UsuarioAplicacion,
                    IdTransaccionEsb = "",
                    IdTransaccionNegocio = objAuditRequest.transaction
                };

                var contactUser = new ContactUserBpel
                {
                    Usuario = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Usuario,// "SA",
                    Nombres = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Nombres, //"jose",
                    Apellidos = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Apellidos, //"arriola",
                    RazonSocial = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.RazonSocial,//"CLARO",
                    TipoDoc = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.TipoDoc,//"DNI",
                    NumDoc = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.NumDoc, //"44388042",
                    Domicilio = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Domicilio, //"JOSE BENJAMIN Z",
                    Distrito = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Distrito, //"SJL",
                    Departamento = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Departamento, //"LIMA",
                    Provincia = objViewModel.DetailCallRequestBpelModel.ContactUserBpelModel.Provincia, //"LIMA",
                    Modalidad = ConfigurationManager.AppSettings("gConstKeyStrModalidad")//"LIBRE" ,           
                };

                var customerClfy = new CustomerClfyBpel
                {
                    Account = string.Empty,//objViewModel.DetailCallRequestBpelModel.CustomerClfyBpelModel.Account,
                    ContactObjId = string.Empty,
                    FlagReg = objViewModel.DetailCallRequestBpelModel.CustomerClfyBpelModel.FlagReg
                };


                var interact = new InteractionBpel
                {
                    Phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + objViewModel.DetailCallRequestBpelModel.CodigoCliente,
                    Tipo = result.Type, //"NO PRECISADO", BPEL
                    Clase = result.Class, //"NO PRECISADO", BPEL
                    Subclase = result.SubClass, //"NO PRECISADO", BPEL
                    TipoInter = ConfigurationManager.AppSettings("AtencionDefault"),
                    MetodoContacto = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault"),
                    Resultado = ConfigurationManager.AppSettings("Ninguno"),
                    HechoEnUno = ConstantsHFC.PresentationLayer.NumeracionCERO,
                    Notas = FunctionsSIACU.GetValueFromConfigFile("strMsgInfDetLlamNof", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")) + objViewModel.DetailCallRequestBpelModel.FechaInicio + " hasta " + objViewModel.DetailCallRequestBpelModel.FechaFin,
                    FlagCaso = ConstantsHFC.PresentationLayer.NumeracionCERO,
                    UsrProceso = ConfigurationManager.AppSettings("USRProcesoSU"),
                    CoId = objViewModel.DetailCallRequestBpelModel.InteractionBpelModel.CoId,
                    CodPlano = objViewModel.DetailCallRequestBpelModel.InteractionBpelModel.CodPlano,
                    Agente = objViewModel.DetailCallRequestBpelModel.InteractionBpelModel.Agente
                };

                var interactPlus = new InteractionPlusBpel
                {
                    ClaroNumber = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.ClaroNumber,
                    DocumentNumber = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.DocumentNumber,
                    FirstName = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.FirstName,
                    LastName = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.LastName,
                    NameLegalRep = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.NameLegalRep,
                    DniLegalRep = objViewModel.DetailCallRequestBpelModel.InteractionPlusBpelModel.DniLegalRep,
                    FlagRegistered = ConstantsHFC.PresentationLayer.NumeracionCERO,
                    Birthday = DateTime.UtcNow.AddDays(-10).ToString("dd/MM/yyyy").Replace("/", ""),
                    ExpireDate = DateTime.UtcNow.AddDays(-10).ToString("dd/MM/yyyy").Replace("/", ""),
                    Inter20 = objViewModel.DetailCallRequestBpelModel.FechaInicio,
                    Inter21 = objViewModel.DetailCallRequestBpelModel.FechaFin                 
                };

                var bodyRequest = new DetalleLlamadasRequestBpel
                {
                    TipoConsulta = objViewModel.DetailCallRequestBpelModel.TipoConsulta,
                    Msisdn = objViewModel.DetailCallRequestBpelModel.Msisdn,
                    //FechaInicio = objViewModel.DetailCallRequestBpelModel.FechaInicio.Replace("/", ""),
                    //FechaFin = objViewModel.DetailCallRequestBpelModel.FechaFin.Replace("/", ""),
                    FechaInicio = Convert.ToDate(objViewModel.DetailCallRequestBpelModel.FechaInicio).ToString("ddMMyyyy"),
                    FechaFin = Convert.ToDate(objViewModel.DetailCallRequestBpelModel.FechaFin).ToString("ddMMyyyy"),
                    ContactUserBpel = contactUser,
                    CustomerClfyBpel = customerClfy,
                    InteractionBpel = interact,
                    InteractionPlusBpel = interactPlus,
                    FlagConstancia = string.Empty,
                    IpCliente = Request.ServerVariables["LOCAL_ADDR"],
                    TipoConsultaContrato = objViewModel.DetailCallRequestBpelModel.TipoConsultaContrato,
                    ValorContrato = objViewModel.DetailCallRequestBpelModel.ValorContrato,
                    FlagContingencia = ConfigurationManager.AppSettings("gConstContingenciaClarify_SIACU"),
                    CodigoCliente = objViewModel.DetailCallRequestBpelModel.CodigoCliente,
                    FlagEnvioCorreo = string.Empty,
                    FlagGenerarOcc = string.Empty,
                    InvoiceNumber = string.Empty,
                    Periodo = string.Empty,
                    TipoProducto = objViewModel.DetailCallRequestBpelModel.TipoProducto
                };

                var objRequest = new BpelCallDetailRequest
                {
                    audit = objAuditRequest,
                    DetalleLlamadasRequestBpel = bodyRequest,
                    HeaderRequestTypeBpel = headerRequest
                };

                objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetCallDetail(objRequest));

                responseFinal.StrResponseCode = objResponse.StrResponseCode;
                responseFinal.StrResponseMessage = objResponse.StrResponseMessage;

                if (objResponse.LstPhoneCall.Count > 0)
                {
                    var tempLst = objResponse.LstPhoneCall;
                    response = Mapper.Map<List<UnBilledCallsDetail>>(tempLst);
                }

                var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "CallDetailController", "ListUnBilledCallsDetail", "SIACU_TOLS_OBTENERDATOSDECUENTA");
                Logging.Info("IdSession: " + objViewModel.StrIdSession, "Transaccion: " + objViewModel.StrTransaction, msg);
            }
            catch (Exception e)
            {
                Logging.Error("IdSession: " + objViewModel.StrIdSession, "Transaccion: " + objViewModel.StrTransaction, e.Message);
            }

            responseFinal.LstPhoneCall = response;
            return responseFinal;
        }

        public JsonResult GetExportExcel_UnBilled(string fechaInicio, string fechaFin, string strCustomer, string tipoProducto)
        {
            Logging.Info(string.Empty, string.Empty, "ENTRANDO EXPORTAR UNBILLED");
            var oExcelHelper = new ExcelHelper();
            var path = string.Empty;
            try
            {
                //var listCallDetails = ListUnBilledCallsDetail(objViewModel);
                var objExportExcel = new UnBilledCallsDetailHfcModel();
                objExportExcel.StardDate = fechaInicio;
                objExportExcel.EndDate = fechaFin;
                objExportExcel.Customer = strCustomer;

                objExportExcel.ListExportExcel = (List<UnBilledCallsDetail>) Session["LstUnbilledCallDetail"];//lstTemp;
                List<int> lstHelperPlan = new List<int>(1);
                Logging.Info(string.Empty, string.Empty, "VARIABLE URL EXCEL UNBILLED: " + "CONST_EXPORT_UNBILLED_CALLDETAILS_" + tipoProducto.ToUpper());
                var url = ConfigurationManager.AppSettings("CONST_EXPORT_UNBILLED_CALLDETAILS_" + tipoProducto.ToUpper());
                Logging.Info(string.Empty, string.Empty, "URL EXCEL UNBILLED: " + url);
                path = oExcelHelper.ExportExcel(objExportExcel, url, lstHelperPlan,20);
                Logging.Info(string.Empty, string.Empty, "PATH EXCEL UNBILLED: " + path);
            }
            catch (Exception ex)
            {
                Logging.Info(string.Empty, string.Empty, "EXCEL UNBILLED: " + ex.Message);
            }
            
            return Json(path);
        }

        #region Imprimir Constancia
        public JsonResult HfcBilledCallsDetailConstancy(string strIdSession, string strTitle, string strRepresentant, string strTypeDoc, string strNroDoc, string strCustomerId, string strCacDac, string strInteraccionId, string strTypeTransaction, string strTypeProduct, BilledCallsDetailHfcModel modelSave, string fechaCicloIni, string fechaCicloFin)
        {
            ViewData["strInteraccionId"] = strInteraccionId;
            var model = GetBilledCallsContancy(strIdSession, strInteraccionId);

            COMMON.GenerateConstancyResponseCommon response = null;
            COMMON.ParametersGeneratePDF parameters = new COMMON.ParametersGeneratePDF();
            var dateStart = new DateTime(Convert.ToInt(modelSave.YearEmision), Convert.ToInt(modelSave.MonthEmision), 1);
            var lastOfThisMonth = dateStart.AddDays(-1).Day;
            parameters.StrCentroAtencionArea = strCacDac; //Centro de Atencion
            parameters.StrTitularCliente = strTitle; //Titular
            parameters.StrNroServicio = model.StrNroClaro;
            parameters.StrRepresLegal = strRepresentant; //Representante
            parameters.StrTipoDocIdentidad = strTypeDoc; //Tipo de documento
            parameters.StrNroDocIdentidad = strNroDoc; //Numero de Documento
            parameters.StrFechaActivacion = model.StrDate; //Fecha
            parameters.StrContenidoComercial = Functions.GetValueFromConfigFile("UnbilledCallDetailContentCommercial", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
            parameters.StrContenidoComercial2 = Functions.GetValueFromConfigFile("UnbilledCallDetailContentCommercial2", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
            parameters.StrCasoInter = strInteraccionId; //Caso de interaccion
            parameters.StrNroDocFact = model.StrNroClaro; //Caso de interaccion
            parameters.StrEmail =  model.StrCorreoDl; //Caso de interaccion
            parameters.StrMontSolicitad =  model.StrMonthSolicit; //Meses Solicitado
            parameters.StrTipoTransaccion = strTypeTransaction; //Tipo de transaccion
            parameters.StrFecInicialReporte = fechaCicloIni;
            parameters.StrFecFinalReporte = fechaCicloFin;
            parameters.StrNotas = string.Empty;//modelSave.Note;
            parameters.strAccionEjecutar = ConfigurationManager.AppSettings("strDetalleLlamadaSalienteNoFacturadaAccionEjecutar");
            parameters.StrFechaTransaccionProgram = DateTime.UtcNow.ToShortDateString();
            parameters.StrCarpetaTransaccion = strTypeProduct == "HFC" ? ConfigurationManager.AppSettings("strCarpetaLlamadasSalientesHfc") : ConfigurationManager.AppSettings("strCarpetaLlamadasSalientesLte");
            parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strDetalleLlamadaSalienteNoFacturadaFormatoTransac");

            response = GenerateContancyPDF(strIdSession, parameters);
            if (response.Generated)
            {
                var rutaConstancy = response.FullPathPDF;
                //Envio de Correo y Envio de Constancia
                if (!string.IsNullOrEmpty(modelSave.Email))
                {
                    byte[] attachFile = null;
                    //Nombre del archivo
                    string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                    if (DisplayFileFromServerSharedFile(strIdSession, strIdSession, rutaConstancy, out attachFile))
                        SendCorreo(strIdSession, strInteraccionId, modelSave, strAdjunto, attachFile);
                }
            }
            

            Logging.Info(strIdSession, strIdSession, "CONSTANCIA CALLDETAIL Generated: " + response.Generated);
            Logging.Info(strIdSession, strIdSession, "CONSTANCIA CALLDETAIL ErrorMessage: " + response.ErrorMessage);
            Logging.Info(strIdSession, strIdSession, "CONSTANCIA CALLDETAIL FullPathPDF: " + response.FullPathPDF);
            Session["ArrayBits"] = response.EngineMessage;

            return Json(response);
        }

        public JsonResult GetBilledCallsContancyHeader(string strIdSession, string strInteraccionId)
        {
            bool flag = false;
            var model = new BilledCallsHfcConstancyModel();
            InfoInteractionTemplateResponse objInfoInteractionTemplateResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            InfoInteractionTemplateRequest objInfoInteractionTemplateRequest = new InfoInteractionTemplateRequest()
            {
                audit = audit,
                vInteraccionID = strInteraccionId
            };

            objInfoInteractionTemplateResponse = Logging.ExecuteMethod<InfoInteractionTemplateResponse>(() =>
            {
                return _oServiceFixed.GetInfoInteractionTemplate(objInfoInteractionTemplateRequest);
            });

            var result = objInfoInteractionTemplateResponse.InteractionTemplate;
            if (result.X_INTER_15 == string.Empty)
            {
                model.StrCadDac = SIACU.Transac.Service.Constants.PresentationLayer.gstrNoPrecisado;
            }
            else
            {
                model.StrCadDac = result.X_INTER_15;
            }

            if (result.ID_INTERACCION != null || result.TIENE_DATOS != null)
            {
                flag = true;
                model.StrNroClaro = result.X_CLARO_NUMBER;
                model.StrCorreoDl = result.X_EMAIL;
                model.StrMonthSolicit = result.X_INTER_29;
                if (result.X_FLAG_REGISTERED.Trim() == SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO)
                {
                    model.ChkCorreoDl = true;
                }
                else
                {
                    model.ChkCorreoDl = false;
                }
            }

            model.StrDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.StrContractId = result.X_INTER_18;

            return Json(model);
        }

        public BilledCallsHfcConstancyModel GetBilledCallsContancy(string strIdSession, string strInteraccionId)
        {
            var model = new BilledCallsHfcConstancyModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            InfoInteractionTemplateRequest objInfoInteractionTemplateRequest = new InfoInteractionTemplateRequest()
            {
                audit = audit,
                vInteraccionID = strInteraccionId
            };

            var objInfoInteractionTemplateResponse = Logging.ExecuteMethod<InfoInteractionTemplateResponse>(() =>
            {
                return _oServiceFixed.GetInfoInteractionTemplate(objInfoInteractionTemplateRequest);
            });

            var result = objInfoInteractionTemplateResponse.InteractionTemplate;
            if (result.X_INTER_15 == string.Empty)
            {
                model.StrCadDac = SIACU.Transac.Service.Constants.PresentationLayer.gstrNoPrecisado;
            }
            else
            {
                model.StrCadDac = result.X_INTER_15;
            }

            if (result.ID_INTERACCION != null || result.TIENE_DATOS != null)
            {
                model.StrNroClaro = result.X_CLARO_NUMBER;
                model.StrCorreoDl = result.X_EMAIL;
                model.StrMonthSolicit = result.X_INTER_29;
                if (result.X_FLAG_REGISTERED.Trim() == SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO)
                {
                    model.ChkCorreoDl = true;
                }
                else
                {
                    model.ChkCorreoDl = false;
                }
            }

            model.StrDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.StrContractId = result.X_INTER_18;

            return model;
        }
        #endregion

        public void InsertAuditory(string strTransaction, string strService, string strTelephone, string strNameComplet,
                                   string strIdSession, string strSn, string strIpServidor, string strStarDate, string strEndDate)
        {
            string[,] strDetails = new string[3, 3];

            strDetails[0, 0] = "Telefono";
            strDetails[0, 1] = strTelephone;
            strDetails[0, 2] = "Telefono";
            strDetails[1, 0] = "FechaInicio/InvoiceNumber";
            strDetails[1, 1] = strStarDate;
            strDetails[1, 2] = "Fecha Inicio o InvoiceNumber";
            strDetails[2, 0] = "FechaFin/InvoiceNumber";
            strDetails[2, 1] = strEndDate;
            strDetails[2, 2] = "Fecha Fin o InvoiceNumber";

            int count = ((strDetails.Length / 4) - 1);
            var sbText = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (strDetails.GetValue(i, 1) != null && strDetails.GetValue(i, 2) != null)
                {
                    sbText.Append(" " + strDetails.GetValue(i, 1) + " : ");
                    sbText.Append(strDetails.GetValue(i, 2));
                }
            }

            //Registrar Auditoria
            SaveAuditM(strTransaction, strService, sbText.ToString(), strTelephone, strNameComplet, strIdSession, strSn, strIpServidor);

        }

 
        public JsonResult AppConfig(string strIdSession)
        {
            Logging.Info(strIdSession, "123456", "strIdSession " + strIdSession);

            Dictionary<string, string> dictionaryPageLoad = new Dictionary<string, string>();

            dictionaryPageLoad["strVariableT"] = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableT;
            dictionaryPageLoad["strMsgDatosLinea"] = FunctionsSIACU.GetValueFromConfigFile("strMsgDatosLinea", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            dictionaryPageLoad["hdnSiteUrl"] = ConfigurationManager.AppSettings("strRutaSiteInicio");
            dictionaryPageLoad["hdnLocalAdd"] = Request.ServerVariables["LOCAL_ADDR"];
            dictionaryPageLoad["hdnServName"] = Request.ServerVariables["SERVER_NAME"];
            dictionaryPageLoad["strTransactionLTEDetCallFac"] = ConstantsHFC.gstrTransaccionLTEDetLlamFac;
            dictionaryPageLoad["strConstOpcDetailCallsFact"] = ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasFact");
            dictionaryPageLoad["hdnTipificationBuscarLte"] = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.LteBuscar;
            dictionaryPageLoad["hdnTipificationSaveLte"] = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.LteSave;
            dictionaryPageLoad["hdnTipificationBuscarHfc"] = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.HfcBuscar;
            dictionaryPageLoad["hdnTipificationSaveHfc"] = ConstantsHFC.DETALLE_LLAMADAS_SALIENTE.HfcSave;

            //Nuevos

            dictionaryPageLoad["hdnConstTransaction"] = ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamFact");
            dictionaryPageLoad["hdnConststrService"] = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            dictionaryPageLoad["hdnConstDescription"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeConsDetLlamRetReg", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult GetCustomerPhone(string strIdSession, string intIdContract, string strTypeProduct)
        {
            #region MyRegion
            //var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            //var objConsultationServiceByContractRequest = new ConsultationServiceByContractRequest()
            //{
            //    audit = objAuditRequest,
            //    strCodContrato = intIdContract,
            //    typeProduct = strTypeProduct
            //};

            //var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
            //{
            //    return _oServiceFixed.GetConsultationServiceByContract(objConsultationServiceByContractRequest);
            //});
            #endregion
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objRequest = new ConsultationServiceByContractRequest()
            {
                audit = objAuditRequest,
                strCodContrato = intIdContract,
                typeProduct = strTypeProduct
            };

            var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomerLineNumber(objRequest);
            });
            Logging.Info("IdSession: " + strIdSession, "Metodo: " + "GetCustomerPhone", objCustomerPhoneResponse.msisdn);
            var phone = objCustomerPhoneResponse.msisdn;
            return Json(phone, JsonRequestBehavior.AllowGet);
        }
    }
}