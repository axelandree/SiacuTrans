using ModelPospaid=Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCode = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using HelperController = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.UnbilledOutCallDetail;
using PostpaidService = Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using CommonService= Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Transac.Service;
using KEY = Claro.ConfigurationManager;
using Constant = Claro.SIACU.Transac.Service.Constants;
using System.Text;
using Claro.Helpers.Transac.Service;
using HelperGeneral = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class UnbilledOutCallDetailController : Controller
    {
        private readonly PostpaidService.PostTransacServiceClient oServicePostPaid = new PostpaidService.PostTransacServiceClient();
        private readonly CommonService.CommonTransacServiceClient oServiceCommon = new CommonService.CommonTransacServiceClient();

        private readonly CommonServicesController commonController = new CommonServicesController();

        //
        // GET: /Transactions/OutgoingCallsDetailNB/
        public ActionResult PostpaidUnbilledOutCallDetail()
        {
            Claro.Web.Logging.Configure();
            return PartialView(); 
        }
         
        public JsonResult Load(string idSession,string[] arrPermissions, string flagPlatform, string billingCicle, string codePlanTariff, string clientAccount)
        {
            
            Claro.Web.Logging.Info("Persquash", "SalientesNoFacturado", "In JsonResult Load()"); // Temporal
            ModelPospaid.UnbilledOutCallDetail objModel = new ModelPospaid.UnbilledOutCallDetail();
            objModel.IdSession = idSession;
            objModel.FlagPlatform = flagPlatform;
            objModel.StatusCode = "";

            string strArrPermissions = string.Join(",", arrPermissions);
            //Cargando Planes Restringidos
            //string strRestrictedPlans = Claro.Helpers.HelperLog.ObtenerValorConfigSiacUnico("CadenaPlanesRestringidosParaConsulta");
            string strRestrictedPlans =Functions.GetValueFromConfigSiacUnico(Constant.Key_RestrictedPlansConsultation);
            if (Functions.IsRestrictedPlan(codePlanTariff, strRestrictedPlans, strArrPermissions))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Notes_OutgoingCallsNBP.Message_RestrictedInformation;
                Claro.Web.Logging.Info("Persquash", "SalientesNoFacturado", "Functions.IsRestrictedPlan(codePlanTariff, strRestrictedPlans, strArrPermissions)"); // Temporal
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }

            //Datos para la vista (Configuracion)
            EnableControls(ref objModel, strArrPermissions);
            EnablePermissions(ref objModel, strArrPermissions);
  
            //string codOpcion = ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact");

            LoadDefaultDates(ref objModel, billingCicle, clientAccount);
            if (objModel.StatusCode.Equals(Constant.StatusCode_Error))
            {
                Claro.Web.Logging.Info("Persquash", "SalientesNoFacturado", "objModel.StatusCode" + Constant.StatusCode_Error); // Temporal
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
            Claro.Web.Logging.Info("Persquash", "SalientesNoFacturado", "Out JsonResult Load("); // Temporal
            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCallDetailExportExcel(string idsession, string phoneNumber, string profileCode, string contractID)
        {
            ExcelHelper oExcelHelper = new ExcelHelper();
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idsession);

            ModelPospaid.UnbilledOutCallDetail objModel = (ModelPospaid.UnbilledOutCallDetail)Session["ObjController"];
             
            try
            {
                string strTelephone = commonController.GetNumber (idsession,false, phoneNumber);
                string strDescription = Constant.Notes_OutgoingCallsNBP.SuccessfulConsultation_CallsDetailNB;

                RegisterAudit(objModel, profileCode, strTelephone, strDescription);

                string paramIN = String.Format(Constant.Notes_OutgoingCallsNBP.ParametersIN_LogTrx, strTelephone, contractID, objModel.StrStartDate, objModel.StrEndDate);
                string paramOUT = String.Format(Constant.Notes_OutgoingCallsNBP.Message_TotalRegistration, objModel.ListCallsDetail.Count.ToString());
                 
                commonController.RegisterLogTrx(objModel.IdSession, strTelephone,Constant.strCero, Constant.strCero,paramIN, paramOUT,
                     ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact"), Constant.Action_Export,
                     ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamNoFact"), Constant.Notes_OutgoingCallsNBP.NameTransaction);

                objModel.Telephone = strTelephone;
                objModel.StrTotalRegistration = objModel.ListCallsDetail.Count.ToString(); 

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Message_DataNotCharged; 
            } 
            List<int> lstHelperPlan = Enumerable.Range(7, 6).ToList();
            string path = oExcelHelper.ExportExcel(objModel, Claro.SIACU.Transac.Service.TemplateExcel.CONST_EXPORT_POSTPAID_OUTGOINGCALLSNB, lstHelperPlan);
            
            return Json(path);
        }

        public JsonResult Search(string idSession,string contactID, string contractID, string codesProfiles,string phone,string strStartDate, string strEndDate, string security,
            string[] arrPermissions, string codePlanTariff, string flagPlatform)
        { 
            ModelPospaid.UnbilledOutCallDetail objModel = new ModelPospaid.UnbilledOutCallDetail();
            objModel.StrStartDate = strStartDate;
            objModel.StrEndDate = strEndDate;
            objModel.FlagPlatform = flagPlatform;
            objModel.IdSession = idSession;
            Claro.Web.Logging.Info("Session: " + objModel.IdSession, "Transaction: idSession ", "In - Search");
            string strArrPermissions = string.Join(",", arrPermissions); 
            //Cargando Planes Restringidos
            string strRestrictedPlans = Functions.GetValueFromConfigSiacUnico("CadenaPlanesRestringidosParaConsulta");
            if (Functions.IsRestrictedPlan(codePlanTariff, strRestrictedPlans, strArrPermissions))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Notes_OutgoingCallsNBP.Message_RestrictedInformation;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            } 
            string interactionId="",flagInsertion="", msgText="";

            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession); 
            try
            {

                Claro.Web.Logging.Info("Session: " + idSession, "Transaction: idSession ", "UnbilledOutCallDetailController:  LoadTyping()");

                HelperGeneral.Typification typification=LoadTypification(ref objModel, audit, contractID);

                Claro.Web.Logging.Info("Session: " + idSession, "Transaction: idSession ", "UnbilledOutCallDetailController:  strType: " + typification.Type + "strClass: " + typification.Class + "strSubClass: " + typification.SubClass);//Temporal

                if (!String.IsNullOrEmpty(objModel.StatusCode))
                {
                    return Json(objModel, JsonRequestBehavior.AllowGet);
                }
                 
                string strNotes = String.Format(Constant.Notes_OutgoingCallsNBP.NotesInteractionData, objModel.StrStartDate, objModel.StrEndDate);
                CommonService.Iteraction interaction = commonController.InteractionData(objModel.IdSession, contactID, phone, strNotes,
                    typification.Type, typification.Class, typification.SubClass, "");
                 
                commonController.InsertBusinessInteraction2(interaction, objModel.IdSession, out interactionId, out flagInsertion, out msgText);
                 
                if (flagInsertion.Equals(Constant.StatusCode_OK))
                { 
                    LoadGrid(ref objModel, contractID,codesProfiles, phone, security); 
                }
                else {
                    objModel.StatusMessage = msgText + " - " + Constant.Notes_OutgoingCallsNBP.Description_SaveInteractionClarify;
                }

                //Variable que se usaran para imprimir
                Session["ObjController"] = objModel; 
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Message_DataNotCharged; 
            }
            Claro.Web.Logging.Info("Session: " + objModel.IdSession, "Transaction: idSession ", "Out - Search");
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        #region Funciones Privadas
      
        //Habilita controles de pagina
        private void EnableControls(ref ModelPospaid.UnbilledOutCallDetail objModel, string strPermissions)
        {
            try
            {
                string strKeyBotonExportar = ConfigurationManager.AppSettings("gConstEvtBotonExportarDetLlamLin");
                if (strPermissions.IndexOf(strKeyBotonExportar) >= 0)
                    objModel.FlagShow_Export ="T";
                else
                    objModel.FlagShow_Export = "F"; 
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message); 
            }
        }

        //Habilita permisos al usuario
        private void EnablePermissions(ref ModelPospaid.UnbilledOutCallDetail objModel, string strPermissions)
        {
            try
            {
                string strkeyVerNumeroDestino = ConfigurationManager.AppSettings("gConstkeyVerNumeroDestinoConsulta_OCNBPostpaid");
                if (strPermissions.IndexOf(strkeyVerNumeroDestino) >= 0)
                    objModel.FlagSecurity = "1";
                else
                    objModel.FlagSecurity = "0";

                if (objModel.FlagShow_Export.Equals("T") )
                {
                    string strKeyExport= ConfigurationManager.AppSettings("gConstEvtExportarDetaLlamadaLin");
                    if (strPermissions.IndexOf(strKeyExport) >= 0)
                        objModel.FlagAuthorization_Export = "T";
                    else
                        objModel.FlagAuthorization_Export = "F";
                }

                string strKeySearch = ConfigurationManager.AppSettings("gConstEvtBuscarDetaLlamadaLin");
                if (strPermissions.IndexOf(strKeySearch) >= 0)
                    objModel.FlagAuthorization_Search  = "T";
                else
                    objModel.FlagAuthorization_Search = "F";
                 
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message); 
            }
        }

        //Carga la lista de detalles de llamadas y los totales
        private PostpaidService.CallDetailNBDB1ResponsePostPaid ExecuteCallDetailNBSummary_DB1(string idSession, string contractID, string security, string strStartDate, string strEndDate,
            ref List<HelperController.CallDetail> listCallsDetail, ref string msgError, ref string summaryTotal)
        {
            PostpaidService.CallDetailNBDB1ResponsePostPaid objResponse = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(idSession);
            try
            {
                
                objResponse = GetCallDetailNBResponse_DB1(contractID, security, strStartDate, strEndDate, audit);
                listCallsDetail = MappingListCallDetail_DB1(objResponse.ListCallDetail.ToList());
                msgError = objResponse.MsgError;
                summaryTotal = objResponse.SummaryTotal;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session , audit.transaction, ex.Message);
                throw new Exception(audit.transaction); 
            }
            return objResponse;
        }

        //Carga la lista de detalles de llamadas y los totales
        private PostpaidService.CallDetailNBDB1BSCSResponsePostPaid ExecuteCallDetailNBSummary_DB1_BSCS(string idSession, string contractID, string security, string strStartDate, string strEndDate,
            string strYesterday, string strToday, ref List<HelperController.CallDetail> listCallsDetail, ref string msgError, ref string summaryTotal)
        {
            PostpaidService.CallDetailNBDB1BSCSResponsePostPaid objResponse = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(idSession);
            try
            {
               
                objResponse = GetCallDetailNBResponse_DB1_BSCS(contractID, security, strStartDate, strEndDate,
                    strYesterday, strToday, audit);
                listCallsDetail = MappingListCallDetail_DB1(objResponse.ListCallDetail.ToList());
                msgError = objResponse.MsgError;
                summaryTotal = objResponse.SummaryTotal;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return objResponse;
        }
         
        //Obtener la ultima fecha de facturacion
        private DateTime? GetLastBillingDate(string idSession, string customerCode)
        {
            DateTime? fecha = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(idSession);
            try
            {
                List<Helpers.Postpaid.UnbilledOutCallDetail.BillData> listBill;
                Claro.Web.Logging.Info("Persquash", audit.transaction, "V0001");
                PostpaidService.BillDataResponsePostPaid objResponse = GetBillDataResponse(customerCode, audit);
                Claro.Web.Logging.Info("Persquash", audit.transaction, objResponse.ListBillSummary.Count.ToString());

                listBill = MappingListBillData(objResponse);
                Claro.Web.Logging.Info("Persquash", audit.transaction, listBill.Count.ToString());
                if (listBill.Count > 0)
                {
                    Claro.Web.Logging.Info("Persquash", audit.transaction, listBill[0].Description);
                    fecha = DateTime.Parse(listBill[0].Description);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return fecha;
        }

        //Cargar fechas por defecto
        private bool LoadDefaultDates(ref ModelPospaid.UnbilledOutCallDetail objModel, string billingCicle, string customerAccount)
        {
            Claro.Web.Logging.Info(objModel.IdSession, "SalientesNoFacturado", "IN LoadDefaultDates()"); 
            string tempStartDate = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            string vDia = (String.IsNullOrEmpty(billingCicle)) ? "01" : billingCicle;
            string vFechaUltFact = "";

            DateTime? dFechaUltFact = GetLastBillingDate(objModel.IdSession, customerAccount);
            if (dFechaUltFact != null)
                vFechaUltFact = ((DateTime)dFechaUltFact).ToString("yyyyMM");

            string vFecha = DateTime.Now.ToString("yyyyMM");

            if (!string.IsNullOrEmpty(vFechaUltFact))
            {
                if (vFechaUltFact.Equals(vFecha))
                {
                    vFecha = vDia + DateTime.Now.ToShortDateString().Substring(2);
                    tempStartDate = vFecha;
                }
                else
                {
                    vFecha = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                    if (vFechaUltFact.Equals(vFecha))
                    {
                        vFecha = vDia + DateTime.Now.AddMonths(-1).ToShortDateString().Substring(2);
                        tempStartDate = vFecha;
                    }
                    else
                    {
                        vFecha = vDia + DateTime.Now.AddMonths(-2).ToShortDateString().Substring(2);
                        tempStartDate = vFecha;
                    }
                }
            }
            else
            {
                vFecha = vDia + DateTime.Now.AddMonths(-1).ToShortDateString().Substring(2);
                tempStartDate = vFecha;
            }

            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            DateTime dStartDate = new DateTime();
            if (!DateTime.TryParse(tempStartDate, culture, System.Globalization.DateTimeStyles.AssumeLocal, out dStartDate))
            {
                //objModel.StatusCode = Constant.StatusCode_Error;
                //objModel.StatusMessage = Constant.Message_ErrorGetDeadlines;
                //Claro.Web.Logging.Error(objModel.IdSession, "LoadDefaultDates",
                //    String.Format("LoadDefaultDates", "vFechaUltFact:{0}; vDia:{1}; tempStartDate{2}",
                //    vFechaUltFact, vDia, tempStartDate));
                //return false; 
                objModel.StrStartDate = DateTime.Now.ToString("dd/MM/yyyy");
                objModel.StrMinimumDate = DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy");
            }
            else {
                objModel.StrStartDate = dStartDate.ToString("dd/MM/yyyy");
                objModel.StrMinimumDate = dStartDate.ToString("dd/MM/yyyy"); 
            } 
            objModel.StrEndDate = DateTime.Now.ToString("dd/MM/yyyy"); 
            objModel.StrMaximumDate = DateTime.Now.ToString("dd/MM/yyyy");

            Claro.Web.Logging.Info(objModel.IdSession, "SalientesNoFacturado", "OUT LoadDefaultDates()"); 
            return true;
        }

        private void LoadGrid(ref ModelPospaid.UnbilledOutCallDetail objModel, string contractID, string profileCode, string phone, string security)
        { 
            if (objModel.FlagPlatform.Equals("C"))
            {
                //    ddlBolsaLlamada.SelectedIndex = 0
                //    ddlBolsa.Visible = False
                //    CargarDatosPrepago(Request.QueryString("pstrTelefono"))
            }
            else {
                LoadPostpaidData(ref objModel,contractID,profileCode, phone ,security);
            }  
        }

        private HelperGeneral.Typification LoadTypification(ref ModelPospaid.UnbilledOutCallDetail objModel, CommonService.AuditRequest audit, string contractID)
        {
            HelperGeneral.Typification typification = new HelperGeneral.Typification(); 
            try
            {
                string tempTypePhone = ValidatePermission(objModel.IdSession, contractID);
                if (tempTypePhone.Equals(Constant.PresentationLayer.HCTNUMEROCPLANSOLOTFI)) tempTypePhone = Constant.strTipoLinea_FIJO_POST;

                Claro.Web.Logging.Info("Session: " + objModel.IdSession, "Transaction: idSession ", "tempTypePhone:  " + tempTypePhone);

                string transactionName =KEY.AppSettings("gConstkeyTransaccionConsultaDetalleLLamadaNoFacturado");
                Claro.Web.Logging.Info("Session: " + objModel.IdSession, "Transaction: idSession ", "UnbilledOutCallDetailController- LoadTyping transactionName: " + transactionName);
                
                typification = commonController.LoadTypification(objModel.IdSession, transactionName, tempTypePhone);
                if (String.IsNullOrEmpty(typification.Type))
                { 
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = Constant.Message_TypificationNotRecognized;
                } 
            }
            catch (Exception ex) {
                Claro.Web.Logging.Error(objModel.IdSession ,audit.transaction, ex.Message);
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Message_TypificationNotCharged; 
            }
            return typification;
        }

        private void LoadPostpaidData(ref ModelPospaid.UnbilledOutCallDetail objModel, string contractID, string profileCode, string strPhone, string security)
        { 
            List<HelperController.CallDetail> listCallsDetail = new List<HelperController.CallDetail>(); 

            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession); 
            try
            {
                if (!(String.IsNullOrEmpty(contractID)) && !contractID.Equals(Constant.strCero))
                {
                    string formatSartDate, formatEndDate;
                    IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                    DateTime dStartDate = DateTime.Parse(objModel.StrStartDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    DateTime dEndDate = DateTime.Parse(objModel.StrEndDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    DateTime dToday = DateTime.Now;

                    formatSartDate = dStartDate.ToString("yyyyMMdd");
                    formatEndDate = dEndDate.ToString("yyyyMMdd"); 

                    string msgError = "", summaryTotal=""; 
                    if ((dEndDate - dToday).Days < 0)
                    {
                        ExecuteCallDetailNBSummary_DB1(objModel.IdSession, contractID, security, objModel.StrStartDate, objModel.StrEndDate,
                            ref listCallsDetail, ref msgError, ref summaryTotal); 
                    }
                    else
                    {
                        if ((dStartDate - dToday).Days >= 0)
                        {
                            ExecuteCallDetailNBSummary_DB1(objModel.IdSession, contractID, security, objModel.StrStartDate, objModel.StrEndDate, 
                                ref listCallsDetail, ref msgError, ref summaryTotal); 
                        }
                        else
                        {
                            string formatYesterday = dToday.AddDays(-1) .ToString("yyyyMMdd");
                            ExecuteCallDetailNBSummary_DB1_BSCS(objModel.IdSession, contractID, security, objModel.StrStartDate, objModel.StrEndDate,
                                dToday.AddDays(-1).ToString("dd/MM/yyy"), dToday.ToString("dd/MM/yyy"), 
                                ref listCallsDetail, ref msgError, ref summaryTotal); 
                        }
                    } 
                    string strDescription = "", paramIN = "", paramOUT = "";
                    //int exito = 0;
                    if (String.IsNullOrEmpty(msgError))
                    {
                        objModel.StatusCode = Constant.StatusCode_OK;
                        objModel.ListCallsDetail = listCallsDetail;
                        strDescription = Constant.Notes_OutgoingCallsNBP.SuccessfulConsultation_CallsDetailNB;
                        //exito = 1;

                        string[] arrTotal = summaryTotal.Split(';');

                        objModel.StrTotal = Functions.GetFormatHHMMSS(Functions.CheckInt64(Functions.CheckDbl(arrTotal[1], 2))); 
                        objModel.StrTotalSMS = Functions.CheckInt64(Functions.CheckDbl(arrTotal[2], 2)).ToString(); 
                    }
                    else {
                        objModel.StatusCode = Constant.StatusCode_Error;
                        objModel.StatusMessage =String.Format (Constant.Notes_OutgoingCallsNBP.Message_ErrorWCF,msgError);
                        strDescription = Constant.Notes_OutgoingCallsNBP.UnsuccessfulConsultation_CallsDetailNB;
                        //exito = 0;
                    } 
                    RegisterAudit(objModel, profileCode, strPhone, strDescription); 
                    paramIN = String.Format(Constant.Notes_OutgoingCallsNBP.ParametersIN_LogTrx, strPhone, contractID, objModel.StrStartDate, objModel.StrEndDate);
                    paramOUT = String.Format(Constant.Notes_OutgoingCallsNBP.Message_TotalRegistration, objModel.ListCallsDetail.Count.ToString());
                    
                    commonController.RegisterLogTrx(objModel.IdSession, strPhone, Constant.strCero, Constant.strCero, paramIN, paramOUT,
                    ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact"), Constant.Action_Search,
                    ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamNoFact"), Constant.Notes_OutgoingCallsNBP.NameTransaction); 
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction); 
            } 
        }

        private void RegisterAudit(ModelPospaid.UnbilledOutCallDetail objModel, string strProfileCode, string strPhone,
            string strDescription)
        {
            CommonService.AuditRequest audit= App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);
            
            // === Inicio Temporal ===
            if (objModel.FlagPlatform.Equals("Pers")) return;
            // =======================
            try
            {
                // ==== Seteo de Variables ====
                double perfilCode;
                //double eventCode = Double.Parse(KEY.AppSettings("gConstEvtConsultaDetLlamNoFact_OCNBPostpaid"));
                double opcionCode = Double.Parse(KEY.AppSettings("gConstOpcDetalleLlamadasNoFact"));
                string strAmmount = Constant.strCero;
                string strService = KEY.AppSettings("gServicioLlamadaSalienteNoFact");
                string strIdTransaccion = KEY.AppSettings("gConstIDTransaccionAplicacionPostpago_OCNBPostpaid") + Functions.CadenaAleatoria();
                string strTransaction = KEY.AppSettings("gConstEvtConsultaDetLlamNoFact_OCNBPostpaid");

                if (!String.IsNullOrEmpty(strProfileCode))
                {
                    string[] arrProfiles = strProfileCode.Split(',');
                    perfilCode = Double.Parse(arrProfiles[0]);
                }

                string[,] arrAuditDetail = new string[3, 3];
                arrAuditDetail[0, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnTelephone;
                arrAuditDetail[0, 1] = strPhone;
                //arrAuditDetail[0, 2] = "Telefono";
                arrAuditDetail[1, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnStartDate;
                arrAuditDetail[1, 1] = objModel.StrStartDate;
                //arrAuditDetail[1, 2] = "Fecha Inicio";
                arrAuditDetail[2, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnEndDate;
                arrAuditDetail[2, 1] = objModel.StrEndDate;
                //arrAuditDetail[2, 2] = "Fecha Fin";

                // ==== Generacion de Variables ====
                StringBuilder sbText = new StringBuilder();
                for (int i = 0; i < Math.Sqrt(arrAuditDetail.Length); i++)
                {
                    if ((!String.IsNullOrEmpty(arrAuditDetail[i, 0])) && (!String.IsNullOrEmpty(arrAuditDetail[i, 1])))
                    {
                        sbText.Append(" " + arrAuditDetail[i, 0] + " : " + arrAuditDetail[i, 1]);
                    }
                }
                string strText = ((String.IsNullOrEmpty(strDescription)) ? "" : strDescription + " - ") + sbText.ToString();
                
                // === Ejecucion de la Funcion ===
                commonController.RegisterAuditGeneral(objModel.IdSession, strPhone, strAmmount, strText, strService, strTransaction); 
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
        }
         
        private string  ValidatePermission(string idSession,string contractID) {
            string nameTypePhone = "";
            
            if (!String.IsNullOrEmpty(contractID)) { 
                string variable = "", variableTFI = "", codeResultDL = "", codeTariffPlan="";
                string[] array01, array02, array03;
                CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
                PostpaidService.AuditRequest auditP = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(idSession);
                
                try
                {
                    int intCodParam = Convert.ToInt(KEY.AppSettings("gObtenerParametroTerminalTPI"));
                    int intCodParam2 = Convert.ToInt(KEY.AppSettings("gObtenerParametroSoloTFIPostpago"));

                    CommonService.ParameterTerminalTPIResponse objResponse = GetParameterTerminalTPIResponse(intCodParam, audit);
                    var objList = objResponse.ListParameterTeminalTPI;
                    //message = objResponse.Message;

                    objResponse = GetParameterTerminalTPIResponse(intCodParam2, audit);
                    var objListTFI = objResponse.ListParameterTeminalTPI;
                    //messageTFI = objResponse.Message;

                    variable = objList[0].ValorC;
                    variableTFI = objListTFI[0].ValorC;

                    PostpaidService.DataLineResponsePostPaid objDataLineResponse = GetDataLineResponse(contractID, auditP);
                    codeResultDL = objDataLineResponse.StrResponse;
                    //intCodeTariffPlan = Int32.Parse(objDataLineResponse.DataLine.TariffPlan);
                    codeTariffPlan = (objDataLineResponse.DataLine.TariffPlan == null) ? "" : objDataLineResponse.DataLine.TariffPlan;

                    bool successfulSearch = false;
                    if (!string.IsNullOrEmpty(variable) && (!string.IsNullOrEmpty(codeTariffPlan)))
                    {
                        array01 = variable.Split(';');
                        for (int i = 0; i < array01.Length-1; i++) { 
                            array02=array01[i].Split(':');
                            if (array02 != null && array02 .Length>1 && (!String.IsNullOrEmpty(array02[1])))
                            {
                                array03 = array02[1].Split(',');
                                for (int j = 0; j < array03.Length; j++) {
                                    if (codeTariffPlan.Equals(array03[j].Trim()))
                                    {
                                        nameTypePhone = array03[0].Trim();
                                        successfulSearch = true;
                                        break;
                                    }
                                }
                            }
                            if (successfulSearch) break;
                        }    
                    }

                    if (!successfulSearch) {
                        if (!string.IsNullOrEmpty(variableTFI) && (!string.IsNullOrEmpty(codeTariffPlan)))
                        {
                            array01 = variableTFI.Split(';');
                            for (int i = 0; i < array01.Length - 1; i++)
                            {
                                array02 = array01[i].Split(',');
                                if (array02 != null)
                                {
                                    for (int j = 0; j < array02.Length; j++)
                                    {
                                        if (codeTariffPlan.Equals(array02[j].Trim()))
                                        {
                                            nameTypePhone = Constant.strTipoLinea_FIJO_POST;
                                            successfulSearch = true;
                                            break;
                                        }
                                    }
                                }
                                if (successfulSearch) break;
                            }
                        }
                    }

                    if (!successfulSearch) nameTypePhone = Constant.strTipoLinea_POSTPAGO;

                    string strgConsHabilitaCambioTrasladoNumeroSoloTFI = "";

                    string[] strCHCTNSoloTFI,strCHCTNSoloTFI2;
                    if (!string.IsNullOrEmpty(variableTFI) && (!string.IsNullOrEmpty(codeTariffPlan)))
                    {
                        array01 = variableTFI.Split(';');
                        for (int i = 0; i < array01.Length - 1; i++)
                        {
                            array02 = array01[i].Split(',');
                            if (array02 != null)
                            {
                                for (int j = 0; j < array02.Length; j++)
                                {
                                    if (codeTariffPlan.Equals(array02[j].Trim()))
                                    {
                                        strCHCTNSoloTFI= strgConsHabilitaCambioTrasladoNumeroSoloTFI.Split(';');
                                        strCHCTNSoloTFI2 = strCHCTNSoloTFI[0].Split(',');
                                        for (int k = 0; k < strCHCTNSoloTFI2.Length; k++)
                                        {
                                            if (codeTariffPlan.Equals(strCHCTNSoloTFI2[k]))
                                            {
                                                nameTypePhone = Constant.PresentationLayer.HCTNUMEROCPLANSOLOTFI;
                                                successfulSearch = true;
                                                break;
                                            }
                                        } 
                                    }
                                    if (successfulSearch) break;
                                }
                            }
                            if (successfulSearch) break;
                        }
                    }
                }
                catch (Exception ex) {
                    Claro.Web.Logging.Error(idSession, audit.transaction, ex.Message);
                    throw new Exception(audit.transaction); 
                }   
            } 
            return nameTypePhone;
        }
        #endregion

        #region Funciones WS
        private PostpaidService.BillDataResponsePostPaid GetBillDataResponse(string customerCode, PostpaidService.AuditRequest audit)
        {
            PostpaidService.BillDataRequestPostPaid objRequest = new PostpaidService.BillDataRequestPostPaid()
            {
                CustomerCode = customerCode,
                audit = audit
            };
            PostpaidService.BillDataResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.BillDataResponsePostPaid>(() =>
            {
                return oServicePostPaid.GetListBillSummary(objRequest);
            });
            return objResponse;
        }

        private PostpaidService.CallDetailNBDB1ResponsePostPaid GetCallDetailNBResponse_DB1(string contractID, string security,
            string strStartDate, string strEndDate, PostpaidService.AuditRequest audit)
        {
            PostpaidService.CallDetailNBDB1RequestPostPaid objRequest = new PostpaidService.CallDetailNBDB1RequestPostPaid
            {
                ContractID = contractID,
                Security = security,
                StrStartDate = strStartDate,
                StrEndDate = strEndDate,
                audit = audit
            };
            PostpaidService.CallDetailNBDB1ResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.CallDetailNBDB1ResponsePostPaid>(() =>
            {
                return oServicePostPaid.GetCallDetailNB_DB1(objRequest);
            });
            return objResponse;
        }

        private PostpaidService.CallDetailNBDB1BSCSResponsePostPaid GetCallDetailNBResponse_DB1_BSCS(string contractID, string security,
            string strStartDate, string strEndDate, string strYesterday,string strToday,PostpaidService.AuditRequest audit)
        {
            PostpaidService.CallDetailNBDB1BSCSRequestPostPaid objRequest = new PostpaidService.CallDetailNBDB1BSCSRequestPostPaid
            {
                ContractID = contractID,
                Security = security,
                StrStartDate = strStartDate,
                StrEndDate = strEndDate,
                StrYesterday =strYesterday,
                StrToday =strToday,
                audit = audit
            };
            PostpaidService.CallDetailNBDB1BSCSResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.CallDetailNBDB1BSCSResponsePostPaid>(() =>
            {
                return oServicePostPaid.GetCallDetailNB_DB1_BSCS(objRequest);
            });
            return objResponse;
        }

        private PostpaidService.DataLineResponsePostPaid GetDataLineResponse(string contractID, PostTransacService.AuditRequest audit)
        {
            PostpaidService.DataLineRequestPostPaid objRequest = new PostpaidService.DataLineRequestPostPaid()
            {
                ContractID = contractID,
                audit = audit
            };
            PostpaidService.DataLineResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.DataLineResponsePostPaid>(() =>
            {
                return oServicePostPaid.GetDataLine(objRequest);
            });

            return objResponse;
        }

        private CommonService.ParameterTerminalTPIResponse GetParameterTerminalTPIResponse(int parameterID, CommonService.AuditRequest audit)
        {
            CommonService.ParameterTerminalTPIRequest objRequest = new CommonService.ParameterTerminalTPIRequest()
            {
                ParameterID = parameterID,
                audit = audit
            };
            CommonService.ParameterTerminalTPIResponse objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.ParameterTerminalTPIResponse>(() =>
            {
                return oServiceCommon.GetParameterTerminalTPI(objRequest);
            });
            return objResponse;
        }

        private CommonService.TypificationResponse GetTypificationResponse(CommonService.AuditRequest audit,string transactionName) {

            CommonService.TypificationRequest request = new CommonService.TypificationRequest()
            {
                TRANSACTION_NAME = transactionName,
                audit = audit
            };
            CommonService.TypificationResponse response = Claro.Web.Logging.ExecuteMethod<CommonService.TypificationResponse>(() =>
            {
                return oServiceCommon.GetTypification(request);
            });
            return response;
        }
          
        #endregion


        #region Funciones Mapping
        private List<HelperController.BillData> MappingListBillData(PostpaidService.BillDataResponsePostPaid objResponse)
        {
            List<HelperController.BillData> list = new List<HelperController.BillData>();
            HelperController.BillData entity = null;
            foreach (var item in objResponse.ListBillSummary)
            {
                entity = new HelperController.BillData()
                {
                    Code = item.Code,
                    Code2 = item.Code2,
                    Description = item.Description
                };
                list.Add(entity);
            }
            return list;
        }

        private List<HelperController.CallDetail> MappingListCallDetail_DB1(List<PostpaidService.CallDetailGeneric> listCallDetail)
        {
            List<HelperController.CallDetail> list = new List<HelperController.CallDetail>();
            HelperController.CallDetail entity = null;
            foreach (var item in listCallDetail)
            {
                entity = new HelperController.CallDetail()
                {
                    StrOrder = item.VlrNumber,
                    StrDate = item.StrDate,
                    StrHour = item.StrHour,
                    DestinationPhone = item.DestinationPhone,
                    StrQuantity = item.Quantity_FormatHHMMSS,
                    OriginalAmount = item.OriginalAmount,
                    TariffPlan = item.TariffPlan,
                    Tariff = item.Tariff,
                    Type = item.Type,
                    Tariff_Zone = item.Tariff_Zone,
                    Operator = item.Operator,
                    Horary = item.Horary,
                    CallType = item.CallType,
                    FinalAmount = item.FinalAmount
                };
                list.Add(entity);
            }

            return list;
        } 

        #endregion
    }
}