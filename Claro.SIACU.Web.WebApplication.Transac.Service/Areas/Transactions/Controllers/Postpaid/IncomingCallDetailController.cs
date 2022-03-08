using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.Helpers;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using CONSTANTSSIACPO = Claro.SIACU.Transac.Service.ConstantsSiacpo;
using KEY = Claro.ConfigurationManager;
using System.Collections;
using AuditRequest = Claro.Entity.AuditRequest;
using Claro.Helpers.Transac.Service;
using CONSTANTADDITIONALSERVICEPOSTPAID = Claro.SIACU.Transac.Service.Constants.ADDITIONALSERVICESPOSTPAID;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class IncomingCallDetailController : Controller
    {
        private readonly PostTransacService.PostTransacServiceClient oServicePostPaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacServiceClient();

        private static string gNameClient = "";
        private static string gPhoneNumber = "";
        private static string gDateInConsult = "";
        private static string gCacDac = "";
        private static string gCaseId = "";
        private static string gTempTypePhone = "";
        private static string gAmount = "";
        private static List<QueryAssociatedLines> listPrint = new List<QueryAssociatedLines>();
        private static PostIncomingCallDetail gObjPost = new PostIncomingCallDetail();

        //
        // GET: /Transactions/PostIncomingCallDetail/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostpaidIncomingCallDetail()
        {
            Claro.Web.Logging.Configure();
            return PartialView("~/Areas/Transactions/Views/IncomingCallDetail/PostpaidIncomingCallDetail.cshtml");
        }

        //*********************************************
        //Obtiene Datos que se muestran en el CONSULTAR
        //*********************************************
        
        public List<QueryAssociatedLines> GetQueryAssociatedLines(string idsession, string phoneNumber, string dateIni, string dateEnd, int typequery)
        {

            List<QueryAssociatedLines> list = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(idsession);
            try
            {
                QueryAssociatedLinesResponsePostPaid objresponse = GetQueryAssociatedLinesResponse(phoneNumber, dateIni, dateEnd, typequery, audit);
                list = MappingListQueryAssociatedLines(objresponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idsession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return list;
        }

        private List<QueryAssociatedLines> MappingListQueryAssociatedLines(QueryAssociatedLinesResponsePostPaid objQueryAssociatedLinesResponse)
        {
            List<QueryAssociatedLines> list = new List<QueryAssociatedLines>();
            QueryAssociatedLines entity;
            foreach (var item in objQueryAssociatedLinesResponse.Total)
            {
                entity = new QueryAssociatedLines()
                {
                    NroOrd = item.NroOrd,
                    MSISDN = item.MSISDN,
                    CallNumber = item.CallNumber,
                    CallDate = item.CallDate,
                    CallTime = item.CallTime,
                    CallDuration = item.CallDuration

                };
                list.Add(entity);
            }

            return list;
        }

        private QueryAssociatedLinesResponsePostPaid GetQueryAssociatedLinesResponse(string PhoneNumber, string Date_Ini, string Date_End, int TypeQuery, PostTransacService.AuditRequest audit)
        {
            QueryAssociatedLinesRequestPostPaid objQueryAssociatedLinesRequest = new QueryAssociatedLinesRequestPostPaid()
            {
                PhoneNumber = PhoneNumber,
                Date_Ini = Date_Ini,
                Date_End = Date_End,
                TypeQuery = TypeQuery,
                audit = audit
            };
            QueryAssociatedLinesResponsePostPaid objQueryAssociatedLinesResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.QueryAssociatedLinesResponsePostPaid>
                (() => { return oServicePostPaid.GetListQueryAssociatedLines(objQueryAssociatedLinesRequest); });
            return objQueryAssociatedLinesResponse;
        }

        //****************************
        //Obtiene datos para exportar
        //****************************
        public string GetPostpaidIncomingCallExportExcel(PostIncomingCallDetail objPost, int typequery)
        {
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: ingresa a GetPostpaidIncomingCallExportExcel ", "datos de entrada" + typequery);

            string path;
            ExcelHelper oExcelHelper = new ExcelHelper();
            List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel> list = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel>();


            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);
            Models.Postpaid.ExportExcelModel objExportExcel = new Models.Postpaid.ExportExcelModel();

            try
            {
                QueryAssociatedLinesResponsePostPaid objResponse = GetExportExcelResponse(objPost, typequery, audit);

                list = PostpaidIncomingCallExportExcel(objResponse);
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: GetPostpaidIncomingCallExportExcel ", "list" + list);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPost.idSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: GetPostpaidIncomingCallExportExcel ", "Error: " + ex.Message);

            }

            objExportExcel.ListExportExcel = list;
            objExportExcel.NameClient = objPost.NameClient;
            objExportExcel.PhoneNumber = objPost.telephone;
            objExportExcel.DateInConsult = string.Format("{0} - {1}", objPost.txtStartDate, objPost.txtEndDate);
            objExportExcel.CacDac = objPost.ddlCACDACSelected;





            List<int> lstHeaderPlan = ValidateExportExcel(objExportExcel.ListExportExcel);
            path = oExcelHelper.ExportExcel(objExportExcel, Claro.SIACU.Transac.Service.TemplateExcel.CONST_EXPORT_POSTPAIDINCOMINGCALL, lstHeaderPlan);
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: GetPostpaidIncomingCallExportExcel ", "salida" + path);

            return path;
        }

        private List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel> PostpaidIncomingCallExportExcel(QueryAssociatedLinesResponsePostPaid objCallDetailSummary)
        {
            List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel> list = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel>();
            Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall.ExportExcel entity;
            foreach (var item in objCallDetailSummary.Total)
            {
                entity = new Helpers.Postpaid.PostpaidIncomingCall.ExportExcel()
                {
                    Id = item.NroOrd,
                    PhoneNumber = item.MSISDN,
                    IncomingPhoneNumber = item.CallNumber,
                    DateIncomingCall = item.CallDate,
                    HourIncomingCall = item.CallTime,
                    Duration = item.CallDuration
                };
                list.Add(entity);
            }
            return list;
        }

        private QueryAssociatedLinesResponsePostPaid GetExportExcelResponse(PostIncomingCallDetail objPost, int TypeQuery, PostTransacService.AuditRequest audit)
        {
            QueryAssociatedLinesRequestPostPaid objGetExportExcelRequest = new QueryAssociatedLinesRequestPostPaid()
            {
                PhoneNumber = objPost.telephone,
                Date_Ini = objPost.dateIni,
                Date_End = objPost.dateEnd,
                TypeQuery = TypeQuery,
                audit = audit
            };
            QueryAssociatedLinesResponsePostPaid objGetExportExcelResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.QueryAssociatedLinesResponsePostPaid>
                (() => { return oServicePostPaid.GetListQueryAssociatedLines(objGetExportExcelRequest); });
            return objGetExportExcelResponse;
        }

        public List<int> ValidateExportExcel(List<Helpers.Postpaid.PostpaidIncomingCall.ExportExcel> objRelationPlan)
        {
            List<int> list = Enumerable.Range(7, 6).ToList();

            foreach (Helpers.Postpaid.PostpaidIncomingCall.ExportExcel item in objRelationPlan)
            {
                if (item.Id == Claro.Constants.NumberOneString) { list.Remove(7); }
                if (item.PhoneNumber == Claro.Constants.NumberOneString) { list.Remove(8); }
                if (item.DateIncomingCall == Claro.Constants.NumberOneString) { list.Remove(9); }
                if (item.HourIncomingCall == Claro.Constants.NumberOneString) { list.Remove(10); }
                if (item.IncomingPhoneNumber == Claro.Constants.NumberOneString) { list.Remove(11); }
                if (item.Duration == Claro.Constants.NumberOneString) { list.Remove(12); }
                break;
            }
            return list;
        }

        //********************
        //Controlador imprimir
        //********************

        public ActionResult PostpaidIncomingCallDetailPrint()
        {

            //List<Helpers.Postpaid.PostpaidIncomingCall.QueryAssociatedLines> listPrint = listPrint as List<Helpers.Postpaid.PostpaidIncomingCall.QueryAssociatedLines>;
            ConsultData(ref gObjPost);
            //llenado de variables globales
            gNameClient = gObjPost.NameClient;
            gPhoneNumber = CONSTANTS.strCincuentaYUno + gObjPost.telephone;
            gDateInConsult = string.Format("{0} - {1}", gObjPost.txtStartDate, gObjPost.txtEndDate);
            gCacDac = gObjPost.ddlCACDACSelected;

            PostIncomingCallDetail objmodel = new PostIncomingCallDetail()
            {   
                NameClient = gNameClient,
                telephone = gPhoneNumber,
                DateInConsult = gDateInConsult,
                ddlCACDACSelected = gCacDac,
                lstQueryAssociatedLines = gObjPost.lstQueryAssociatedLines

            };
            return View(objmodel);
        }

        public JsonResult AccesForm()
        {
            
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(ConfigurationManager.AppSettings("strLlamadasEntImprimir"));
            lstMessage.Add(ConfigurationManager.AppSettings("strLlamadasEntExportar"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Load(PostIncomingCallDetail objPost)
        {
            
            string strTempTypePhone = string.Empty;
            strTempTypePhone = ValidatePermission(objPost);
            //strTempTypePhone = "POSTPAGO";

            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Load");
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: LOAD IncomingCallDetail ", "Message" + msg);

            if (!(strTempTypePhone.Equals("FIJO POST") || strTempTypePhone.Equals("POSTPAGO")))
            {
                objPost.StatusCode = "E";
                objPost.StatusMessage = KEY.AppSettings("strMsgTransNoHabilPlanDetLLamEnt");
                return Json(objPost, JsonRequestBehavior.AllowGet);

            }

            gTempTypePhone = strTempTypePhone;

            Start(ref objPost);


            gAmount = objPost.Amount.ToString();
            objPost.StatusCode = "OK";
            return Json(objPost, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Consult(PostIncomingCallDetail objPost)
        {

            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Consult");
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: 270492 ", "Message" + msg);
            CommonServicesController convert2010 = new CommonServicesController();
            string strTempTypePhone = gTempTypePhone;
            string strType = "";
            string strClass = "";
            string strSubClass = "";
            string ParamIn = "";
            string ParamOut = "";

            ParamIn = "TelfConsulta = " + objPost.telephone + "; FechaINI = " + objPost.txtStartDate + "; FechaFin = " + objPost.txtEndDate;


            if (LoadTypification(ref objPost, KEY.AppSettings("strTipificacionConsulta"), strTempTypePhone, ref strType, ref strClass, ref strSubClass))
            {
                var msg1= string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Consult-LoadTypification");
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: 270492 ", "Message" + msg1);
                if (ProcessTransactionInteraction(ref objPost, KEY.AppSettings("strTipificacionConsulta"), strType, strClass, strSubClass))
                {
                    var msg2 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Consult-ProcessTransactionInteraction");
                    Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: 270492 ", "Message" + msg2);
                    if (!objPost.StatusCode.Equals("OK"))
                        return Json(objPost, JsonRequestBehavior.AllowGet);


                    string result = ConsultData(ref objPost);
                    
                    if (string.IsNullOrEmpty(result))
                    {
                        objPost.StatusCode = "OK_A";
                        objPost.StatusMessage = KEY.AppSettings("strMsgGeneraTipiXInfoDetLLamEnt");
                    }
                    else
                    {
                        UpdateInteraction(objPost, result);  
                    }

                }
            }
            



            InsertAudit(ref objPost, objPost.NameUserLoging, "gConstEvtLlamadasEntrantesConsultar", "Accion: Consultar Desde: " + objPost.txtStartDate + " Hasta: " + objPost.txtEndDate +
                " Respuesta SP: " + objPost.AuditHidden, convert2010.GetNumber(objPost.idSession, false, objPost.telephone));



            ParamOut = objPost.StatusMessage;
            RegisterLogTrx(objPost, "gConstEvtLlamadasEntrantesConsultar", "strTipificacionConsulta", "Consultar", objPost.codOption, convert2010.GetNumber(objPost.idSession, false, objPost.telephone), Functions.CheckStr(0), Functions.CheckStr(0), ParamIn, ParamOut);

            //listPrint = objPost.lstQueryAssociatedLines;
            gObjPost = objPost;
            


            return Json(objPost, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(PostIncomingCallDetail objPost)
        {
            CommonServicesController convert2010 = new CommonServicesController();
            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Save");
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: Save IncomingCallDetail ", "Message" + msg);
            string strType = "";
            string strClass = "";
            string strSubClass = "";
            string resultError = "";
            string ParamIn = "";
            string ParamOut = "";



            string strTempTypePhone = gTempTypePhone;

            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: strTempTypePhone ", "strTempTypePhone" + strTempTypePhone);

            ParamIn = "TelfConsulta = " + objPost.telephone + "; FechaINI = " + objPost.txtStartDate + "; FechaFin = " + objPost.txtEndDate;



            if (LoadTypification(ref objPost,KEY.AppSettings("strTipificacionGuardar"), strTempTypePhone, ref strType, ref strClass, ref strSubClass))
            {
                if (ProcessTransactionInteraction(ref objPost, KEY.AppSettings("strTipificacionGuardar"), strType, strClass, strSubClass))
                {
                    objPost.StatusCode = "OK_A";
                    UpdateInteraction(objPost, resultError);
                }
            }

            InsertAudit(ref objPost, objPost.NameUserLoging, "gConstEvtLlamadasEntrantesGuardar", "Accion: Guardar Desde: " + objPost.txtStartDate + " Hasta: " + objPost.txtEndDate +
                " Respuesta Interaccion: " + objPost.AuditHidden, convert2010.GetNumber(objPost.idSession, false, objPost.telephone));



            ParamOut = objPost.StatusMessage;
            RegisterLogTrx(objPost, "gConstEvtLlamadasEntrantesGuardar", "strTipificacionGuardar", "Imprimir", objPost.codOption, convert2010.GetNumber(objPost.idSession, false, objPost.telephone), Functions.CheckStr(0), Functions.CheckStr(0), ParamIn, ParamOut);
            return Json(objPost, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Export(PostIncomingCallDetail objPost)
        {
            CommonServicesController convert2010 = new CommonServicesController();
            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Export");
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: Export IncomingCallDetail ", "Message" + msg);
            string ParamIn = "";
            string ParamOut = "";
            ParamIn = "TelfConsulta = " + objPost.telephone + "; FechaINI = " + objPost.txtStartDate + "; FechaFin = " + objPost.txtEndDate;

            try
            {
                if (objPost.profileExp.Equals(CONSTANTS.strUno))
                {
                    if (objPost.EmailProfileAuthorized != null)
                    {
                        GetSendEmail(objPost);
                    }
                }

                if (objPost.telephone.Substring(0, 2) != CONSTANTS.strCincuentaYUno)
                {
                    objPost.telephone = CONSTANTS.strCincuentaYUno + objPost.telephone;
                }

                objPost.dateIni = Functions.ConvertirFecha2(objPost.txtStartDate);
                objPost.dateEnd = Functions.ConvertirFecha2(objPost.txtEndDate);

                int typeQuery = CONSTANTS.numeroUno;


                if (objPost.chkGenerateOCC_IsChecked == "T")
                {
                    if (objPost.PaymentOCC.Equals(true))
                    {
                        string strAction = "Exportar";
                        if (GenerateOCC(objPost, strAction) == 0)
                        {
                            objPost.StatusCode = "OK";
                            objPost.StatusMessage = KEY.AppSettings("strMsgSeRealizoCobroOCCDetLLamEnt");
                            UpdateInteraction(objPost, CONSTANTS.ConstExportar);
                            objPost.PaymentOCC = false;
                        }
                        else
                        {
                            objPost.StatusCode = "OK";
                            objPost.StatusMessage = "Se realizó la Exportación con éxito";
                            return Json(objPost, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        objPost.StatusCode = "OK";
                        objPost.StatusMessage = KEY.AppSettings("strMsgSeRealizoCobroOCCDetLLamEnt");
                        UpdateInteraction(objPost, CONSTANTS.ConstExportar);
                    }
                }
                else
                {
                    objPost.StatusMessage = "Se realizó la Exportación con éxito";
                    UpdateInteraction(objPost, CONSTANTS.ConstExportar);
                }


                InsertAudit(ref objPost, objPost.NameUserLoging, "gConstEvtLlamadasEntrantesExportar", "Accion: Exportar Desde: " + objPost.txtStartDate + " Hasta: " + objPost.txtEndDate +
                   " Respuesta OCC: " + objPost.AuditHidden + "Usuario Validador: " + objPost.ProfileAuthorized, convert2010.GetNumber(objPost.idSession, false, objPost.telephone));



                ParamOut = objPost.StatusMessage;
                RegisterLogTrx(objPost, "gConstEvtLlamadasEntrantesExportar", "strTipificacionGuardar", "Exportar", objPost.codOption,
                    convert2010.GetNumber(objPost.idSession, false, objPost.telephone), Functions.CheckStr(0), Functions.CheckStr(0), ParamIn, ParamOut);


                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Entra a  GetPostpaidIncomingCallExportExcel", " desde export");

                objPost.Path = GetPostpaidIncomingCallExportExcel(objPost, typeQuery);
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: Fin de Export IncomingCallDetail ", "path" + objPost.Path);
                objPost.StatusCode = "OK";
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("667", "667", ex.Message);
                Claro.Web.Logging.Error("666", "666", ex.Message);
            }
            
            //objPost.StatusMessage = "Se realizó la Exportación con éxito";
            return Json(objPost, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Print(PostIncomingCallDetail objPost)
        {
            CommonServicesController convert2010 = new CommonServicesController();

            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "Print");
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: Print IncomingCallDetail ", "Message" + msg);
            string ParamIn = "";
            string ParamOut = "";
            ParamIn = "TelfConsulta = " + objPost.telephone + "; FechaINI = " + objPost.txtStartDate + "; FechaFin = " + objPost.txtEndDate;

            if (objPost.profilePrint.Equals(CONSTANTS.strUno))
            {
                if (objPost.EmailProfileAuthorized != null)
                {
                    GetSendEmail(objPost);
                }
            }

            if (objPost.telephone.Substring(0, 2) != CONSTANTS.strCincuentaYUno)
            {
                objPost.telephone = CONSTANTS.strCincuentaYUno + objPost.telephone;
            }

            string dateIni = Functions.ConvertirFecha2(objPost.txtStartDate);
            string dateEnd = Functions.ConvertirFecha2(objPost.txtEndDate);

            int typeQuery = CONSTANTS.numeroUno;


            if (objPost.chkGenerateOCC_IsChecked == "T")
            {           
                if (objPost.PaymentOCC.Equals(true))
                {
                    string strAction = "Imprimir";
                    if (GenerateOCC(objPost, strAction) == 0)
                    {
                        objPost.StatusCode = "OK";
                        objPost.StatusMessage = KEY.AppSettings("strMsgSeRealizoCobroOCCDetLLamEnt");
                        UpdateInteraction(objPost, CONSTANTS.ConstImprimir);
                        objPost.PaymentOCC = false;
                    }
                    else
                    {
                        objPost.StatusCode = "OK";
                        objPost.StatusMessage = "Se realizó la Impresión con éxito";
                        UpdateInteraction(objPost, objPost.StatusMessage);
                        return Json(objPost, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    objPost.StatusCode = "OK";
                    objPost.StatusMessage = KEY.AppSettings("strMsgSeRealizoCobroOCCDetLLamEnt");
                    UpdateInteraction(objPost, CONSTANTS.ConstImprimir);
                }
            }
            else
            {
                objPost.StatusMessage = "Se realizó la Impresión con éxito";
                UpdateInteraction(objPost, CONSTANTS.ConstImprimir);
            }

            
            InsertAudit(ref objPost, objPost.NameUserLoging, "gConstEvtLlamadasEntrantesImprimir", "Accion: Imprimir Desde: " + objPost.txtStartDate + " Hasta: " + objPost.txtEndDate +
               " Respuesta OCC: " + objPost.AuditHidden + "Usuario Validador: " + objPost.ProfileAuthorized, convert2010.GetNumber(objPost.idSession, false, objPost.telephone));



            ParamOut = objPost.StatusMessage;
            RegisterLogTrx(objPost, "gConstEvtLlamadasEntrantesImprimir", "strTipificacionGuardar", "Imprimir", objPost.codOption, convert2010.GetNumber(objPost.idSession, false, objPost.telephone), Functions.CheckStr(0), Functions.CheckStr(0), ParamIn, ParamOut);
            objPost.StatusCode = "OK";
            return Json(objPost, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Constancy(PostIncomingCallDetail objPost)
        {
            //string strCaseId = gCaseId;
            ParametersGeneratePDF objConstancy = new ParametersGeneratePDF();
            objConstancy.StrCentroAtencionArea = objPost.ddlCACDACSelected;
            objConstancy.StrTitularCliente = objPost.NameClient;
            objConstancy.StrRepresLegal = objPost.LegalAgent;
            objConstancy.StrTipoDocIdentidad = objPost.TypeDocument;
            objConstancy.StrNroDocIdentidad = objPost.strDocumentNumber;
            objConstancy.StrFechaTransaccionProgram = DateTime.Now.ToShortDateString();
            objConstancy.StrCasoInter = gCaseId;
            objConstancy.StrFecInicialReporte = objPost.txtStartDate;
            objConstancy.StrFecFinalReporte = objPost.txtEndDate;
            objConstancy.StrNroServicio = objPost.telephone;
            objConstancy.StrMontoOCC = gAmount;
            objConstancy.strEnvioCorreo = CONSTANTS.Variable_NO;
            objConstancy.StrEmail = string.Empty;
            objConstancy.StrNotas = objPost.txttaskNote;
            objConstancy.StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
                KEY.AppSettings("strConstArchivoSIACPOConfigMsg"));
            objConstancy.StrContenidoComercial2 =
                Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                    KEY.AppSettings("strConstArchivoSIACPOConfigMsg"));
            objConstancy.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaLlamadasEntrantesPost");
            objConstancy.StrNombreArchivoTransaccion = KEY.AppSettings("strDetalleLlamadasEntrantesTransac");

            CommonServicesController obj = new CommonServicesController();
            GenerateConstancyResponseCommon objGeneratePdf = obj.GenerateContancyPDF(objPost.idSession, objConstancy);
            string strError = objGeneratePdf.ErrorMessage;
            bool blnSuccess = objGeneratePdf.Generated;
            //string strNamePdf = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", objConstancy.StrCasoInter,
            //    Functions.CheckStr(DateTime.Now.Day), Functions.CheckStr(DateTime.Now.Month),
            //    Functions.CheckStr(DateTime.Now.Year),CONSTANTADDITIONALSERVICEPOSTPAID.NameTransactionIncomingCallDetailPostpaid,CONSTANTS.strCero);
            //objPost.RoutePdf = strNamePdf;


            objPost.RoutePdf = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}_{7}_{8}.pdf", "output\\", objConstancy.StrCarpetaPDFs, objConstancy.StrCarpetaTransaccion, objConstancy.StrCasoInter,
                Functions.CheckStr(DateTime.Now.ToString("dd")), Functions.CheckStr(DateTime.Now.ToString("MM")),
                Functions.CheckStr(DateTime.Now.ToString("yyyy")), objConstancy.StrNombreArchivoTransaccion, CONSTANTS.strCero);
            //objPost.RoutePdf = objGeneratePdf.FullPathPDF;
            objPost.RoutePdf = objPost.RoutePdf.Replace("\\", "//");

            return Json(objPost, JsonRequestBehavior.AllowGet);
        }

        #region funciones privadas
  
        private void Start(ref PostIncomingCallDetail objPost)
        {

            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);
            List<AmountIncomingCallHelper> listAmountIncomingCallHelper = null;

            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "Start", "SIACU_SP_OBTENER_DATO");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            try
            {


                AmountIncomingCallResponse obj = GetAmountIncomingCall(ConfigurationManager.AppSettings("MontoDetLlamEntPost"), audit);
                listAmountIncomingCallHelper = MappingAmountIncomingCall(obj);

                objPost.Amount = listAmountIncomingCallHelper[listAmountIncomingCallHelper.Count - 1].ValorN;


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
        }
        private void GetSendEmail(PostIncomingCallDetail objPost)
        {
            string strTemplateEmail = TemplateEmail(objPost);
            string strTo = objPost.EmailProfileAuthorized;
            string strSender = KEY.AppSettings("CorreoServicioAlCliente");
            string strSubject = KEY.AppSettings("gConstAsuntoDetLlamadasEntrantes");

            SendEmailResponseCommon objResponse = new SendEmailResponseCommon();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);


            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "GetSendEmail");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);


            SendEmailRequestCommon objRequest = new SendEmailRequestCommon()
            {
                strTo = strTo,
                strSender = strSender,
                strSubject = strSubject,
                strMessage = strTemplateEmail,
                audit = audit
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => 
                {
                    return oServiceCommon.GetSendEmail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

        }

        private string TemplateEmail(PostIncomingCallDetail objPost)
        {
            string strNameUser = objPost.NameUserLoging; //objPost.NameEmp + " " + objPost.SecondNameEmp;
            string strContent = string.Empty;
            strContent = "<html>";
            strContent += "<head>";
            strContent += "<style type='text/css'>";
            strContent += "<!--";
            strContent += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strContent += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strContent += "-->";
            strContent += "</style>";
            strContent += "<body>";
            strContent += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Estimado(a) " + strNameUser + "</td>";
            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td class='Estilo1'>Se le informa que su Código y Password de Autorización ha sido utilizado para realizar una Transacción relacionada a Detalle de Llamadas Entrantes desde las siguientes entradas</td></tr>";
            strContent += "<tr><td height='10'></td>";

            strContent += "<tr>";
            strContent += "<td align='center'>";
            strContent += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Nro. Telefónico :</td><td class='Estilo1'>" + objPost.telephone + "</td></tr>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Cuenta :</td><td class='Estilo1'>" + objPost.Account + "</td></tr>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Usuario Logueado:</td><td class='Estilo1'>" + objPost.ProfileAuthorized + "</td></tr>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Terminal o Computador :</td><td class='Estilo1'>" + HttpContext.Request.UserHostAddress + "</td></tr>";
            strContent += "<tr><td width='180' class='Estilo2' height='22'>Fecha y Hora :</td><td class='Estilo1'>" + DateTime.Now.ToString() + "</td></tr>";
            strContent += "</Table>";
            strContent += "</td></tr>";

            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td class='Estilo1'>Saludos Cordiales,</td></tr>";
            strContent += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td height='10'></td>";
            strContent += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strContent += "</table>";
            strContent += "</body>";
            strContent += "</html>";

            return strContent;

        }

        private int GenerateOCC(PostIncomingCallDetail objPost, string Action)
                {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);

            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "GenerateOCC", "SIACU_SP_AJUSTE_POR_RECLAMOS");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            
            Int32 iResult = 0;
            string strCodOCC = KEY.AppSettings("sLlamadasEntrantes");
            string strDateEnd = Functions.ConvertirFecha2(DateTime.Now.ToShortDateString());
            string strPeriod = KEY.AppSettings("strPeriodoOCC");

            objPost.CaseId = gCaseId;

            string strComment = objPost.telephone + "C" + objPost.CaseId;

            AdjustForClaimsResponse objResponse = new AdjustForClaimsResponse();

            AdjustForClaimsRequest objRequest = new AdjustForClaimsRequest()
            {
                Amount = Convert.ToDouble(gAmount.ToString()),
                CodClient = Convert.ToInt64(objPost.CustomerId),
                CodOCC = strCodOCC,
                Comment = strComment,
                DateVig = strDateEnd,
                NumQuota = strPeriod,
                audit = audit
            };



            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.AdjustForClaimsResponse>(() =>
                {
                    return oServicePostPaid.GetAdjustForClaims(objRequest);
                });

                iResult = Convert.ToInt(objResponse.Result);

                if (iResult != 0)
                {
                    objPost.StatusCode = "E";
                    objPost.StatusMessage = KEY.AppSettings("strErrTransOCC") + Action;
                    objPost.AuditHidden = objPost.StatusMessage;
                    return iResult;
                }
                else
                {
                    objPost.Payment = KEY.AppSettings("strCobroRelizoAlOCC") + Action + ".";
                    objPost.AuditHidden = iResult + " - " + KEY.AppSettings("strCobroRelizoAlOCC") + Action + ".";
                    objPost.PaymentOCC = false;

                }


            }
            catch (Exception ex)
            {
                iResult = -1;
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Error: " + ex.Message);

                
            }
            return iResult;
        }
        private string ValidatePermission(PostIncomingCallDetail objPost)
        {
            string strNameTypePhone = "";

            if (!string.IsNullOrEmpty(objPost.contractID))
            {
                string var = "";
                string[] var2;
                string[] var3;
                string[] var4;
                string varTFI = "";
                string[] varTFI2;
                string[] varTFI3;
                string message = "";
                string message2 = "";
                string strCodResult = "";
                int intCodPlanTariff = 0;
                int flagFound = 0;

                List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
                List<ParameterTerminalTPIHelper> list2 = new List<ParameterTerminalTPIHelper>();
                CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);
                PostTransacService.AuditRequest auditP = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);

                var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ValidatePermission");
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: ValidatePermission", "Message" + msg);


                try
                {


                    int intCodParam = Convert.ToInt(KEY.AppSettings("gObtenerParametroTerminalTPI"));
                    int intCodParam2 = Convert.ToInt(KEY.AppSettings("gObtenerParametroSoloTFIPostpago"));


                    ParameterTerminalTPIResponse objResponse = GetParameterTerminalTPI(intCodParam, audit);
                    list = MappingParameterTerminalTPI(objResponse);
                    message = objResponse.Message;

                    objResponse = GetParameterTerminalTPI(intCodParam2, audit);
                    list2 = MappingParameterTerminalTPI(objResponse);
                    message2 = objResponse.Message;

                    var = list[0].ValorC.ToString();
                    varTFI = list2[0].ValorC.ToString();

                    DataLineResponsePostPaid objData = GetDataLine(objPost.contractID, auditP);

                    strCodResult = objData.StrResponse;

                    intCodPlanTariff = Convert.ToInt(objData.DataLine.CodPlanTariff);

                    if (!string.IsNullOrEmpty(var))
                    {
                        var2 = var.Split(';');
                        for (int i = 0; i < var2.Length - 1; i++)
                        {
                            var3 = var2[i].Split(':');
                            if (var3.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(var3[1]))
                                {
                                    var4 = var3[1].Split(',');
                                    for (int j = 0; j < var4.Length; j++)
                                    {
                                        if (intCodPlanTariff == Functions.CheckInt(var4[j].Trim()))
                                        {
                                            strNameTypePhone = var3[0].Trim();
                                            flagFound = 1;
                                            break;
                                        }
                                        else
                                        {
                                            strNameTypePhone = CONSTANTS.strTipoLinea_POSTPAGO;
                                            flagFound = 0;
                                        }
                                    }
                                }
                            }
                            if (flagFound == 1)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        strNameTypePhone = CONSTANTS.strTipoLinea_POSTPAGO;
                    }

                    if (flagFound == 0)
                    {
                        if (!string.IsNullOrEmpty(varTFI))
                        {
                            varTFI2 = varTFI.Split(';');
                            for (int x = 0; x < varTFI2.Length - 1; x++)
                            {
                                varTFI3 = varTFI2[x].Split(',');
                                for (int y = 0; y < varTFI3.Length; y++)
                                {
                                    if (intCodPlanTariff == Functions.CheckInt(varTFI3[y].Trim()))
                                    {
                                        strNameTypePhone = CONSTANTS.strTipoLinea_FIJO_POST;
                                        flagFound = 1;
                                        break;
                                    }
                                    else
                                    {
                                        strNameTypePhone = CONSTANTS.strTipoLinea_POSTPAGO;
                                        flagFound = 0;
                                    }
                                }
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: ValidatePermission", "Message" + ex.Message);
                }
            }
            else
            {
                strNameTypePhone = string.Empty;
            }

            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: ValidatePermission", "Message de salida" + strNameTypePhone);

            return strNameTypePhone.ToUpper();
        }
        private bool LoadTypification(ref PostIncomingCallDetail objPost, string strTransaction, string strTempTypePhone, ref string strType, ref string strClass, ref string strSubClass)  
        {
            bool result = false;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);

            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "LoadTypification", "SIACU_SP_OBTENER_TIPIFICACION");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: 270492", "Message" + msg);
            
            
            
            try
            {
                var list = GetTypificationByTransaction(strTransaction, audit).ListTypification;

                foreach (var item in list)
                {
                    Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: 270492", "item.TIPO" + item.TIPO + " item.CLASE" + item.CLASE + " item.SUBCLASE" + item.SUBCLASE);
                    if (item.TIPO.ToUpper().Equals(strTempTypePhone.ToUpper()))
                    {
                        strType = item.TIPO;
                        strClass = item.CLASE;
                        strSubClass = item.SUBCLASE;
                        result = true;
                    }

                }

                if (result == false)
                {
                    objPost.StatusCode = "E";
                    objPost.StatusMessage = "No se reconoce la tipificación de esta transacción.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: LoadTypification", "Message" + ex.Message);
            }
            Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: DataInteraction", "Message" + "/" + strType + "/" + strClass + "/" + strSubClass + "/" + result);
            
            return result;
        }
        private bool ProcessTransactionInteraction(ref PostIncomingCallDetail objPost,string strTransaction, string strType, string strClass, string strSubClass)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);

            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: 270492", "Message" + msg);


            CommonServicesController convert2010 = new CommonServicesController();

            var msg2 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: entra a convert2010.GetNumber", "Message" + msg2);
            string strMsisdn = convert2010.GetNumber(objPost.idSession, false, objPost.telephone);
            var msg3 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: sale de convert2010.GetNumber y entra a DataInteraction", "Message" + msg3);
            
            
            Iteraction oInteraction = DataInteraction(ref objPost, strMsisdn, strType, strClass, strSubClass);

            var msg4 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: sale de DataInteraction", "Message" + msg4);
            

            InsertTemplateInteraction oTemplateInteraction = new InsertTemplateInteraction();

            bool blnValidate = false;
            bool blnExecTransaction;

            blnExecTransaction = true;

            string strUserSystem = "";
            string strUserApp = "";
            string strPassUser = "";
            string strMessage = "";
            string strMsg = "";

            strUserSystem = KEY.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            strPassUser = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            var msg5 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: entra a DataTemplateInteraction", "Message" + msg5);
            oTemplateInteraction = DataTemplateInteraction(ref objPost, ref blnValidate, strTransaction, strMsisdn);

            var msg6 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: sale de DataTemplateInteraction", "Message" + msg6);

            if (blnValidate == false)
            {
                objPost.StatusCode = "E";
                objPost.StatusMessage = "La transacción no se realizó, porque no pudo grabarse la interación.";
                return false;
            }

            InsertGeneralRequest objRequest = new InsertGeneralRequest()
            {
                Interaction = oInteraction,
                InteractionTemplate = oTemplateInteraction,
                vNroTelefono = strMsisdn,
                vUSUARIO_SISTEMA = strUserSystem,
                vUSUARIO_APLICACION = strUserApp,
                vPASSWORD_USUARIO = strPassUser,
                vEjecutarTransaccion = blnExecTransaction,
                audit = audit
            };
            var msg7 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: entra a GetInsertInteractionBusiness", "Message" + msg7);
            InsertGeneralResponse oSave = GetInsertInteractionBusiness(objRequest);
            var msg8 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: sale de GetInsertInteractionBusiness", "Message" + msg8);


            objPost.CaseId = oSave.rInteraccionId;

            gCaseId = objPost.CaseId;

            if (Functions.IsNumeric(oSave.rInteraccionId))
            {
                oSave.rCodigoRetornoTransaccion = Functions.CheckStr(CONSTANTS.numeroCero);
            }
            if (oSave.rFlagInsercion != "OK" && oSave.rFlagInsercion != "")
            {
                objPost.StatusCode = "E";
                objPost.StatusMessage = "Al crear interacción en clarify: " + oSave.rMsgText;
                return false;
            }
            else if (oSave.rFlagInsercionInteraccion != "OK" && oSave.rFlagInsercionInteraccion != "")
            {
                strMessage = Functions.GetValueFromConfigSiacUnico("MensajeErrorTipificacionTransaccion");
                strMsg = string.Format("<br>El nro de interacción es : {0}.<br>", oSave.rInteraccionId);
                objPost.StatusCode = "E";
                objPost.StatusMessage = strMessage + "<br>Por el siguiente error : " + oSave.rMsgTextInteraccion + strMsg;
                return false;
            }
            else if (oSave.rFlagInsercionInteraccion == "OK" && oSave.rFlagInsercion == "OK")
            {
                objPost.StatusCode = "OK";
                objPost.StatusMessage = "La Transaccion se realiza exitosamente.";
            }

            var msg9 = string.Format("Controlador: {0}, Metodo: {1}, Mensaje: {2}", "IncomingCallDetailController", "ProcessTransactionInteraction", objPost.StatusMessage);
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: mostrar mensaje", "Message" + msg9);


            objPost.AuditHidden = objPost.StatusMessage;
            var msg1 = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "ProcessTransactionInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: 270492", "Message datos de salida" + msg1);
            return true;
        }
        private InsertTemplateInteraction DataTemplateInteraction(ref PostIncomingCallDetail objPost, ref bool blnValidate, string strTransaction, string strMsisdn)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);
            InsertTemplateInteraction oTemplateData = new InsertTemplateInteraction();
            var msg = string.Format("Controlador: {0}, Metodo: {1}", "IncomingCallDetailController", "DataTemplateInteraction");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            try
            {
                blnValidate = true;


                oTemplateData._NOMBRE_TRANSACCION = strTransaction;
                oTemplateData._X_CLARO_NUMBER = strMsisdn;
                oTemplateData._P_USUARIO_ID = objPost.UserLogin;
                oTemplateData._X_INTER_15 = objPost.ddlCACDACSelected;
                oTemplateData._X_FIRST_NAME = objPost.strNombres;
                oTemplateData._X_LAST_NAME = objPost.strApellidos;
                oTemplateData._X_DOCUMENT_NUMBER = objPost.strDocumentNumber;
                oTemplateData._X_REFERENCE_PHONE = objPost.strReferencePhone;
                oTemplateData._X_INTER_20 = objPost.txtStartDate;
                oTemplateData._X_INTER_21 = objPost.txtEndDate;

                if (!string.IsNullOrEmpty(objPost.txttaskNote))
                {
                    oTemplateData._X_INTER_30 = objPost.txttaskNote + ". Detalle Llamadas Entrantes - Desde: " + objPost.txtStartDate +
                                                " Hasta: " + objPost.txtEndDate + " - ";
                }
                else
                {
                    oTemplateData._X_INTER_30 = "Detalle Llamadas Entrantes - Desde: " + objPost.txtStartDate +
                                                " Hasta: " + objPost.txtEndDate + " - ";
                }



                if (objPost.chkGenerateOCC_IsChecked == "T")
                {
                    oTemplateData._X_FLAG_REGISTERED = "1";

                    if (objPost.Amount == 0.00)
                    {
                        oTemplateData._X_ADJUSTMENT_AMOUNT = 0.00;
                    }
                    else
                    {
                        //oTemplateData._X_ADJUSTMENT_AMOUNT = Math.Round(objPost.Amount * Functions.CheckDbl(HelperLog.ObtenerValorConfigSiacUnico("Igv")), 2);
                        oTemplateData._X_ADJUSTMENT_AMOUNT = Math.Round(objPost.Amount * Functions.CheckDbl(Functions.GetValueFromConfigSiacUnico("Igv")), 2);
                    }
                }
                else
                {
                    oTemplateData._X_FLAG_REGISTERED = "0";
                    oTemplateData._X_ADJUSTMENT_AMOUNT = 0.00;
                }

                if (!string.IsNullOrEmpty(objPost.ProfileAuthorized))
                {
                    oTemplateData._X_POSITION = objPost.ProfileAuthorized;
                }
                else
                {
                    oTemplateData._X_POSITION = objPost.UserLogin;
                }



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: DataTemplateInteraction", "Message" + ex.Message);
            }
            return oTemplateData;
        }
        private Iteraction DataInteraction(ref PostIncomingCallDetail objPost, string strMsisdn, string strType, string strClass, string strSubClass)
        {
            Iteraction objInteraction = new Iteraction();
            try
            {
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: DataInteraction", "Message" + strMsisdn + "/" + strType + "/" + strClass + "/" + strSubClass);

                strType = CommonServicesController.ValidatePlanTFI(strType, objPost.flagTFI);

                
                objInteraction.OBJID_CONTACTO = objPost.ObjId_Contact;
                objInteraction.FECHA_CREACION = DateTime.Now.ToString();
                objInteraction.TELEFONO = strMsisdn;
                objInteraction.TIPO = strType;
                objInteraction.CLASE = strClass;
                objInteraction.SUBCLASE = strSubClass;
                objInteraction.AGENTE = objPost.UserLogin;
                objInteraction.TIPO_INTER = KEY.AppSettings("AtencionDefault");
                objInteraction.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
                objInteraction.RESULTADO = KEY.AppSettings("Ninguno");
                objInteraction.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
                objInteraction.FLAG_CASO = CONSTANTS.strCero;





                if (!string.IsNullOrEmpty(objPost.txttaskNote))
                {
                    objInteraction.NOTAS = string.Format("{0}. Detalle Llamadas Entrantes - Desde: {1}  Hasta: {2}  - ", objPost.txttaskNote, objPost.txtStartDate, objPost.txtEndDate);
                }
                else
                {
                    objInteraction.NOTAS = string.Format("Detalle Llamadas Entrantes - Desde: {0}  Hasta: {1}  - ", objPost.txtStartDate, objPost.txtEndDate);
                }
                 
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: DataInteraction", "Message" + ex.Message);
            }
            return objInteraction; 
                
        }
        private string ConsultData(ref PostIncomingCallDetail objPost)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);

            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "ConsultData", "SIACU_SP_TIM113_CONS_LINEAS_ASOC");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);

            string NumberPhone = objPost.telephone;
            string DateStart = Functions.ConvertirFecha2(objPost.txtStartDate);
            string DateEnd = Functions.ConvertirFecha2(objPost.txtEndDate);
            int TypeCode;
            string result = "";

            if (NumberPhone.Substring(0, 2) != "51")
            {
                NumberPhone = CONSTANTS.strCincuentaYUno + NumberPhone;
            }

            try
            {
                TypeCode = CONSTANTS.numeroUno;
                //Session("arrListaDetalle") = arrListaDetalle no se implementa porque el exportar se gestiona desde otro metodo
            
                objPost.lstQueryAssociatedLines = GetQueryAssociatedLines(objPost.idSession, NumberPhone, DateStart, DateEnd, TypeCode);



                var msg1 = string.Format("Controlador: {0}, Metodo: {1}, objPost.lstQueryAssociatedLines: {2}", "IncomingCallDetailController", "ConsultData", objPost.lstQueryAssociatedLines.Count);
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: 27041992", "Message" + msg1);


                if (objPost.lstQueryAssociatedLines.Count > 0)
                {
                    objPost.DescriptionTotalRecords = CONSTANTSSIACPO.ConstTotalRegistros + Functions.CheckStr(objPost.lstQueryAssociatedLines.Count);
                }
                else
                {
                    objPost.DescriptionTotalRecords = CONSTANTSSIACPO.ConstTotalRegistros + Functions.CheckStr(objPost.lstQueryAssociatedLines.Count);
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPost.idSession, audit.transaction, ex.Message);
                result = ex.Message;
            }


            //result = objPost.StatusMessage;

            return result;
        }
        private bool UpdateInteraction(PostIncomingCallDetail objPost, string ResultError)
        {
            bool blnResult = false;
            string p_ObjId = "";
            string p_Order = ""; 
            string p_Text = "";
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objPost.idSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "UpdateInteraction", "SIACU_SP_UPDATE_INTERACT_X_AUDIT");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);


            p_ObjId = objPost.CaseId;
            p_Order = "F";



            if (objPost.Payment != "")
            {
                p_Text = objPost.Payment;
                objPost.Payment = "";
            }
            else
            {
                p_Text = ResultError + ". ";
            }

            try
            {
                UpdateInteractionResponse objInteraction = UpdateCallBusiness(p_ObjId,p_Text,p_Order,audit);
                blnResult = objInteraction.ProcessSuccess;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPost.idSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Error: " + ex.Message);
            }


            return blnResult;
        }
        private void InsertAudit(ref PostIncomingCallDetail objPost, string strNameUserLoging, string strCodEvent, string strText, string strPhoneOpd)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "InsertAudit", "strWebServiceSeguridad");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            string strTransac = KEY.AppSettings(strCodEvent.Trim());
            string strService = KEY.AppSettings("gConstEvtServicio_ModCP");
            string strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strNameClient = strNameUserLoging;
            string strIpServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationIp();
            string strNameServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationName();
            string strAccuntUser = objPost.UserLogin;
            string strAmount = Functions.CheckStr(objPost.Amount);
            string strPhone = strPhoneOpd;
            bool result = false;



            try
            {
                SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strAccuntUser,
                    vIpCliente = strIpClient,
                    vIpServidor = strIpServer,
                    vMonto = strAmount,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNameServer,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = strText,
                    vTransaccion = strTransac,
                    audit = audit
                };
                SaveAuditResponseCommon SaveAudit = SaveResponse(objRequest);
                result = SaveAudit.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPost.idSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objPost.idSession, "Transaction: " + audit.transaction, "Error al registrar la DataThroughWebServicesServiceReference.DataThroughWebServicesServiceClient: " + ex.Message);
            }

        }
        private bool RegisterLogTrx(PostIncomingCallDetail objPost, string strCodEvent, string transaction, string strAction,string codOption, string NumberPhone, string strInteraction, string strtypification, string strParamIn, string strParamOut)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objPost.idSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "RegisterLogTrx", "SIACU_SP_INSERTAR_LOG_TRX");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            string strApplication = "SIACPO";
            string strUser = objPost.UserLogin;
            string strClient = "";
            string strIpClient = HttpContext.Request.UserHostAddress;

            try
            {
                strClient = System.Net.Dns.GetHostByAddress(strIpClient).HostName;
            }
            catch
            {
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Error en el RegistroLogTrx. IpCliente: " + strIpClient + " /Hostname: " + strClient);
            }

            if (string.IsNullOrEmpty(strClient))
            {
                strClient = strIpClient;
            }

            string strIpServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationIp();
            string strNameServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationName();

            string strTransaction = KEY.AppSettings(transaction.Trim());
            bool outPut = true;
            string strFlagInsert = "";
            string strMessage = "";
            string strOption = codOption;
            string strActionEvent = KEY.AppSettings(strCodEvent.Trim());

            try
            {
                strActionEvent = strAction + " - " + strActionEvent;
                InsertLogTrxRequestCommon objRequest = new InsertLogTrxRequestCommon()
                {
                    Accion = strAction,
                    Aplicacion = strApplication,
                    IdInteraction = strInteraction,
                    IdTypification = strtypification,
                    InputParameters = strParamIn,
                    IPPCClient = strIpClient,
                    IPServer = strIpServer,
                    NameServer = strNameServer,
                    Opcion = strOption,
                    OutpuParameters = strParamOut,
                    PCClient = strClient,
                    Phone = NumberPhone,
                    Transaccion = strTransaction,
                    User = strUser,
                    audit = audit
                };

                InsertLogTrx(objRequest, ref strFlagInsert);
                if (strFlagInsert.Equals("OK"))
                {
                    outPut = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format("Error en el registro del LogTrx : {0}", ex.Message));
                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Error en el registro del LogTrx: " + ex.Message);
                outPut = false;
                
            }

            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message " + outPut + "Accion " + strAction);

            return outPut;
        }
        private void InsertLogTrx(InsertLogTrxRequestCommon request, ref string flagInsertion)
        {

            InsertLogTrxResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<InsertLogTrxResponseCommon>(() =>
            {
                return oServiceCommon.InsertLogTrx(request);
            });
            flagInsertion = objResponse.FlagInsertion;
        }
        private SaveAuditResponseCommon SaveResponse(SaveAuditRequestCommon objRequest)
        {
            SaveAuditResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() =>
            {
                return oServiceCommon.SaveAudit(objRequest);
            });
            return objResponse;
        }


        private DatTempInteractionResponse GetDataTemplateInteraction(string strIdSession, string strCaseId)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            DatTempInteractionResponse objResponse = new  DatTempInteractionResponse();
            DatTempInteractionRequest objRequest = new DatTempInteractionRequest()
            {
                vInteraccionID = strCaseId,
                audit = audit
            };

                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oServiceCommon.GetInfoInteractionTemplate(objRequest);
                });
            return objResponse;
        }
        private UpdateInteractionResponse UpdateCallBusiness(string p_ObjId, string p_Text, string p_Order, PostTransacService.AuditRequest audit)
        {
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "UpdateCallBusiness", "SIACU_SP_UPDATE_INTERACT_X_AUDIT");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            UpdateInteractionRequest objRequest = new UpdateInteractionRequest()
            {
                InteractId = p_ObjId,
                Text = p_Text,
                Order = p_Order,
                audit = audit
            };
            UpdateInteractionResponse objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.UpdateInteractionResponse>(
                () =>
                {
                    return oServicePostPaid.GetUpdateInteraction(objRequest);
                });
            return objResponse;
        }
        private TypificationResponse GetTypificationByTransaction(string strTransactionName, CommonTransacService.AuditRequest audit)
        {
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "GetTypificationByTransaction", "SIACU_SP_OBTENER_TIPIFICACION");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            TypificationRequest objRequest = new TypificationRequest()
            {
                TRANSACTION_NAME = strTransactionName,
                audit = audit
            };
            TypificationResponse objResponse = Claro.Web.Logging
                .ExecuteMethod<CommonTransacService.TypificationResponse>(
                    () =>
                    {
                        return oServiceCommon.GetTypification(objRequest);
                    });
            return objResponse;
        }
        private InsertGeneralResponse GetInsertInteractionBusiness(InsertGeneralRequest objRequest)
        {
            InsertGeneralResponse objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.InsertGeneralResponse>(() =>
            {
                return oServiceCommon.GetinsertInteractionGeneral(objRequest);
            });
            return objResponse;
        }
        private List<AmountIncomingCallHelper> MappingAmountIncomingCall(AmountIncomingCallResponse objResponse)
        {
            List<AmountIncomingCallHelper> list = new List<AmountIncomingCallHelper>();
            AmountIncomingCallHelper entity;
            foreach (var item in objResponse.ListAmountIncomingCall)
            {
                entity = new AmountIncomingCallHelper()
                {
                    Description = item.Description,
                    ValorC = item.ValorC,
                    ValorN = item.ValorN,
                    ValorL = item.ValorL
                };
                list.Add(entity);
            }
            return list;
        }
        private AmountIncomingCallResponse GetAmountIncomingCall(string name, PostTransacService.AuditRequest audit)
        {
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "GetAmountIncomingCall", "SIACU_SP_OBTENER_DATO");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            AmountIncomingCallRequest objRequest = new AmountIncomingCallRequest()
            {
                Name = name,
                audit = audit
            };
            AmountIncomingCallResponse objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.AmountIncomingCallResponse>(() =>
            {
                return oServicePostPaid.GetAmountIncomingCall(objRequest);
            });
            return objResponse;
        }
        private List<ParameterTerminalTPIHelper> MappingParameterTerminalTPI(ParameterTerminalTPIResponse objResponse)
        {
            List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
            ParameterTerminalTPIHelper entity;
            foreach (var item in objResponse.ListParameterTeminalTPI)
            {
                entity = new ParameterTerminalTPIHelper()
                {
                    ParameterID = item.ParameterID,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    ValorC = item.ValorC,
                    ValorN = item.ValorN,
                    ValorL = item.ValorL
                };
                list.Add(entity);
            }
            return list;
        }
        private ParameterTerminalTPIResponse GetParameterTerminalTPI(int ParameterID, CommonTransacService.AuditRequest audit)
        {
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "GetParameterTerminalTPI", "SIACU_OBTENER_PARAMETRO");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest()
            {
                ParameterID = ParameterID,
                audit = audit
            };
            ParameterTerminalTPIResponse objResponse = Claro.Web.Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
            {
                return oServiceCommon.GetParameterTerminalTPI(objRequest);
            });
            return objResponse;
        }
        private DataLineResponsePostPaid GetDataLine(string contractID, PostTransacService.AuditRequest audit)
        {
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "IncomingCallDetailController", "GetDataLine", "SIACPostpagoConsultas");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
            {
                ContractID = contractID,
                audit = audit
            };
            DataLineResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.DataLineResponsePostPaid>(() =>
            {
                return oServicePostPaid.GetDataLine(objRequest);
            });
            return objResponse;
        }

        #endregion
    }
}