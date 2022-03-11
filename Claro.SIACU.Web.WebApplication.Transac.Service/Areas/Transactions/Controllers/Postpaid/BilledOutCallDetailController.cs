using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Constant = Claro.SIACU.Transac.Service.Constants;
using EXCEL = Claro.Helpers.Transac.Service;
using System.Reflection;
using Claro.Helpers.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
//using ModelPospaid = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.PostpaidBilledOutCallDetails;
using ModelPospaid = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using AutoMapper;
using PostpaidService = Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using HelperGeneral = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using System.Text;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class BilledOutCallDetailController : CommonServicesController
    {        
        private readonly PreTransacService.PreTransacServiceClient oPreTransacService = new PreTransacService.PreTransacServiceClient();
        private readonly PostTransacService.PostTransacServiceClient oServicePostPaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        private readonly CommonServicesController commonController = new CommonServicesController();
        //
        // GET: /Transactions/BilledOutCallDetails/
        public ActionResult PostpaidBilledOutCallDetail()
        { 
            return PartialView();
        }

        public JsonResult GenerateDataForPrinting(string idSession, string profileCode, HelperGeneral.CustomerData customer)
        {
            ModelPospaid.BilledOutCallDetailModel objModel = (ModelPospaid.BilledOutCallDetailModel)Session["ModelOCDB_Postpaid"];
            objModel.IdSession = idSession;
            objModel.StatusCode = "";

            if (string.IsNullOrEmpty(objModel.Telephone) || string.IsNullOrEmpty(objModel.HidInvoiceNumber))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_NullParameters;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }

            string[] strVariables = objModel.HidInvoiceNumberWithDates.Split('$');
            objModel.HidInvoiceNumber = strVariables[0];

            if (!(Functions.IsNumeric(objModel.Telephone) && Functions.IsNumeric(objModel.HidInvoiceNumber)))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_IncorrectParameters;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }

            if (customer.Type.Equals(Constant.GstrBusiness)) objModel.LblTypeCustomer = ConstantsSiacpo.ConstRazonSocial;
            else objModel.LblTypeCustomer = ConstantsSiacpo.ConstCliente;
            objModel.FullNameCustomer = customer.FullName;
            objModel.AccountCustomer = customer.Account;
            objModel.Telephone = customer.Telephone;
            Session["ModelOCDB_Postpaid"] = objModel;

            RegisterAudit(objModel, profileCode, KEY.AppSettings("gConstEvtImprimirDetLlamFact"), objModel.StrDescription);

            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoad(string idSession,string strPhone, string idContract,string flagPlatform ,string[] arrPermissions, string codePlanTariff)
        {
            Claro.Web.Logging.Configure();
            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "Entro a Load"); 

            ModelPospaid.BilledOutCallDetailModel objModel = new ModelPospaid.BilledOutCallDetailModel();
            objModel.IdSession = idSession;
            objModel.FlagPlatform = flagPlatform;

            string strArrPermissions = string.Join(",", arrPermissions);

            string strTempTypePhone = ValidatePermission(idSession, idContract);
            if (!(strTempTypePhone.Equals(Constant.strTipoLinea_FIJO_POST) ||
                strTempTypePhone.Equals(Constant.strTipoLinea_POSTPAGO)))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_TransactionNotEnabledForPlan;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
            objModel.HidTempTypePhone = strTempTypePhone;

            string strRestrictedPlans = Functions.GetValueFromConfigSiacUnico(Constant.Key_RestrictedPlansConsultation);
            if (Functions.IsRestrictedPlan(codePlanTariff, strRestrictedPlans, strArrPermissions))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_RestrictedInformation;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
            // ===== Inicio =====
            objModel.Telephone =  GetNumber(idSession,false, strPhone);
            if (String.IsNullOrEmpty(objModel.Telephone) || objModel.Telephone.Equals("0") ) {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_GeneralDataNotChargeForLine;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
            objModel.ListYears = Functions.GetListValuesXML("ListaAnnosLlamada",Constant.strCero,Constant.SiacutDataXML) ;
            objModel.ListMonths = Functions.GetListValuesXML("ListaMeses", Constant.strCero, Constant.SiacutDataXML);
            objModel.HidCurrentDate =DateTime.Now.ToString("yyyyMM");  
            // ==================
            GeneratePermissions(ref  objModel, strArrPermissions);
             
            objModel.ListCACDAC = commonController.GetListCacDac(idSession);

            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPathExportExcel(string idSession, HelperGeneral.CustomerData customer, string profileCode, string plan)
        {
            ExcelHelper oExcelHelper = new ExcelHelper();
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);

            ModelPospaid.BilledOutCallDetailModel objModel = (ModelPospaid.BilledOutCallDetailModel)Session["ModelOCDB_Postpaid"];
            objModel.IdSession = idSession;
            objModel.StatusCode = "";
            try
            { 
                if (string.IsNullOrEmpty(objModel.Telephone) || string.IsNullOrEmpty(objModel.HidInvoiceNumber))
                {
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = "Se han enviado parámetros nulos a la página.";
                    return Json(objModel, JsonRequestBehavior.AllowGet);
                }

                string[] strVariables = objModel.HidInvoiceNumberWithDates.Split('$');
                objModel.HidInvoiceNumber = strVariables[0]; 

                if (!(Functions.IsNumeric(objModel.Telephone) && Functions.IsNumeric(objModel.HidInvoiceNumber)))
                {
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = "Se han enviado parámetros incorrectos a la página.";
                    return Json(objModel, JsonRequestBehavior.AllowGet);
                }

                if (customer.Type.Equals("BUSINESS")) objModel.LblTypeCustomer = "Razón Social";
                else objModel.LblTypeCustomer = "Cliente:";
                objModel.FullNameCustomer = customer.FullName;
                objModel.AccountCustomer = customer.Account;
                objModel.Plan = plan;
                objModel.Telephone = customer.Telephone;

                RegisterAudit(objModel, profileCode, KEY.AppSettings("gConstEvtExportDetLlamFact_BilledOutCallDetail"), objModel.StrDescription);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            objModel.StatusCode = Constant.StatusCode_OK;
            List<int> lstHelperPlan = Enumerable.Range(0, 10).ToList();
            objModel.PathExcel = oExcelHelper.ExportExcel(objModel, Claro.SIACU.Transac.Service.TemplateExcel.CONST_EXPORT_POSTPAID_BILLEDOUTCALLDETAIL, lstHelperPlan);

            return Json(objModel);
        }

        public JsonResult GetSave(string idSession, string profileCode, string numberContract,string flagSecurity,  HelperGeneral.CustomerData customer,
            string flagSendEmail,string email,string codeMonth, string nameMonth, string codeYear,
            string hidTypePD, string hidClassPD, string hidSubClassPD, string hidTransaction, string strinteractionId, string nameCACDAC, string FechaInicio, string FechaFin,
            string strFinalNotes
            )
        {
            ModelPospaid.BilledOutCallDetailModel objModel = (ModelPospaid.BilledOutCallDetailModel)Session["ModelOCDB_Postpaid"];
            objModel.IdSession = idSession;
            objModel.Telephone = GetNumber(idSession, false, customer.Telephone);
            objModel.FlagSecurity = flagSecurity;   
             
            RegisterAudit(objModel, profileCode, KEY.AppSettings("gConstEvtConsultaDetLlamFact"), objModel.StrDescription);

            string[] strVariables = objModel.HidInvoiceNumberWithDates.Split('$');
            objModel.HidInvoiceNumber = strVariables[0]; 
            RegisterTransaction(ref objModel, customer, profileCode, numberContract,nameMonth, codeYear, flagSendEmail, email,
              hidTypePD, hidClassPD, hidSubClassPD, hidTransaction);


            var NAME_PDF = GetConstancyPDF(idSession, profileCode, numberContract, flagSecurity, customer,
             flagSendEmail, email, codeMonth,  nameMonth,  codeYear,
             hidTypePD, hidClassPD, hidSubClassPD, hidTransaction, strinteractionId, nameCACDAC, FechaInicio, FechaFin, strFinalNotes);

            objModel.RutaPdf = NAME_PDF;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
        public string GetConstancyPDF(string idSession, string profileCode, string numberContract, string flagSecurity, HelperGeneral.CustomerData customer,
            string flagSendEmail, string email, string codeMonth, string nameMonth, string codeYear,
            string hidTypePD, string hidClassPD, string hidSubClassPD, string hidTransaction, string strinteractionId, string nameCACDAC, string FechaInicio, string FechaFin,
            string strFinalNotes
            )
        {
            string NAME_PDF = string.Empty;


            FixedTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(idSession);

            try
            {

                CommonTransacService.ParametersGeneratePDF oParameter = new CommonTransacService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = KEY.AppSettings("strDetalleLlamadasSalienteFormatoTransac"),
                    
                    StrCarpetaTransaccion =KEY.AppSettings("strCarpetaDetalleLlamadasSalientesFacturado"),
                    StrCentroAtencionArea = nameCACDAC,
                    StrTitularCliente = customer.FirstName + ' ' + customer.LastName,
                    StrRepresLegal = customer.LegalRepresentative,
                    StrTipoDocIdentidad = customer.DocumentType,
                    StrNroDocIdentidad = customer.NumberDocument,
                    StrFechaTransaccionProgram = DateTime.Today.ToShortDateString(),
                    strCasoInteraccion = strinteractionId, 
                    StrCasoInter = strinteractionId,
                    StrFecInicialReporte = FechaInicio,
                    StrFecFinalReporte = FechaFin,
                    StrNroServicio = customer.Telephone,
                    strEnvioCorreo = flagSendEmail=="T"?"SI":"NO",
                    StrEmail = email,
                    StrNotas = strFinalNotes,
                    StrContenidoComercial = Functions.GetValueFromConfigFile("PostpaidCallDetailContentCommercial",
                KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
                    StrContenidoComercial2 = Functions.GetValueFromConfigFile("PostpaidCallDetailContentCommercial2",
                KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
                };

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }

        public JsonResult GetSearch(string idSession, string CurrentId, string strPhone, string idCustomer, string idContact, string profileCode, string flagSecurity, string flagPlatform, string strTempTypePhone,
            string codeMonth,string nameMonth,string codeYear  )
        {
            string FechaInicio = string.Empty;
            string FechaFin = string.Empty;
            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GetSearch"); 
            ModelPospaid.BilledOutCallDetailModel objModel = new ModelPospaid.BilledOutCallDetailModel();
            objModel.IdSession = idSession;
            objModel.Telephone = GetNumber(idSession, false, strPhone);
            objModel.FlagSecurity = flagSecurity;
            objModel.FlagPlatform = flagPlatform;

            try
            {
                string hidTransactionPost = KEY.AppSettings("gConstkeyTransaccionConsultaDetalleLLamadaPostpago");
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", String.Format("hidTransactionPost: {0};strTempTypePhone:{1}", hidTransactionPost, strTempTypePhone)); // Temporal
                HelperGeneral.Typification typification = commonController.LoadTypification(objModel.IdSession, hidTransactionPost, strTempTypePhone);
                if (typification == null || string.IsNullOrEmpty(typification.Type))
                {
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = Constant.Message_TypificationNotRecognized;
                    return Json(objModel, JsonRequestBehavior.AllowGet);
                }
                objModel.HidTypePD = typification.Type;
                objModel.HidClassPD = typification.Class;
                objModel.HidSubClassPD = typification.SubClass;

                string interactionId = String.Empty, flagInsertion = String.Empty, msgText = String.Empty;

                string strFinalNotes = String.Format(Constant.BilledOutCallPostpaid.Message_InfoCallsDetailOfTheMonth, nameMonth, codeYear);
                CommonService.Iteraction interaction = commonController.InteractionData(objModel.IdSession, idContact, objModel.Telephone,
                    strFinalNotes, typification.Type, typification.Class, typification.SubClass, "");
                interaction.AGENTE = CurrentId;
                if (String.IsNullOrEmpty(idContact))
                {
                    interaction.OBJID_CONTACTO = "0";
                }
                else
                {
                    interaction.OBJID_CONTACTO = idContact;
                }
                commonController.InsertBusinessInteraction2(interaction, objModel.IdSession, out interactionId, out flagInsertion, out msgText);
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "Paso11"); 
                objModel.strinteractionId = interactionId;
                objModel.strFinalNotes = strFinalNotes;
                if ((!String.IsNullOrEmpty(flagInsertion)) && !flagInsertion.Equals(Constant.Message_OK))
                {
                    
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = String.Format(Constant.BilledOutCallPostpaid.Message_ErrorCreatingInteraction, msgText);
                    return Json(objModel, JsonRequestBehavior.AllowGet);
                }
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "Paso12"); 
                string strSelectedPeriod = codeYear + codeMonth.PadLeft(2, '0');
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", String.Format("RunSearch|| idCustomer:{0};profileCode:{1};strSelectedPeriod:{2}",
                    idCustomer, profileCode, strSelectedPeriod)); 
                
                RunSearch(ref FechaInicio, ref FechaFin, ref objModel, idCustomer, profileCode, strSelectedPeriod);
                Claro.Web.Logging.Info("993760253", "SalientesFacturado", "salio del runsearch");
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", String.Format("flagPlatform:{0};", flagPlatform)); 
                // ===== Guardando datos relacionados con la busqueda ====
                ModelPospaid.BilledOutCallDetailModel objModelMemory = new ModelPospaid.BilledOutCallDetailModel()
                {
                    Telephone = objModel.Telephone,
                    FlagPlatform = flagPlatform,
                    ListCallsDetail = objModel.ListCallsDetail,
                    StrDescription = objModel.StrDescription,
                    HidInvoiceNumberWithDates = objModel.HidInvoiceNumberWithDates,
                    StrTotal = objModel.StrTotal,
                    StrTotalSMS = objModel.StrTotalSMS,
                    StrTotalMMS = objModel.StrTotalMMS,
                    StrTotalGPRS = objModel.StrTotalGPRS,
                    StrTotalRegistration = objModel.StrTotalRegistration,
                    StrFinalCharge = objModel.StrFinalCharge
                };
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "ListCallsDetail.Count:" + objModel.ListCallsDetail.Count); // Temporal
                Session["ModelOCDB_Postpaid"] = objModelMemory;
                // ===== Fin del guardado ====
                objModel.StatusCode = Constant.StatusCode_OK;
            }
            catch (Exception ex) {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = ex.Message;
                Claro.Web.Logging.Error("Persquash", "SalientesFacturado", "Error:" + ex.Message); // Temporal
            }
            objModel.FechaInicio = FechaInicio;
            objModel.FechaFin = FechaFin;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSendEmail(string idSession 
            )
        {
            ModelPospaid.BilledOutCallDetailModel objModel = (ModelPospaid.BilledOutCallDetailModel)Session["ModelOCDB_Postpaid"];

            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostpaidBilledOutCallDetailPrint(string plan)
        {
            ModelPospaid.BilledOutCallDetailModel objModel = (ModelPospaid.BilledOutCallDetailModel)Session["ModelOCDB_Postpaid"];

            string[] strVariables = objModel.HidInvoiceNumberWithDates.Split('$'); 
            string strStartDate = Functions.GetDDMMYYYYAsDateTime(strVariables[1]).ToString("yyyyMMdd");
            string strEndDate = Functions.GetDDMMYYYYAsDateTime(strVariables[2]).ToString("yyyyMMdd");
             
            objModel.Plan = plan;
            objModel.StrDateRange = String.Format(Constant.BilledOutCallPostpaid.Message_DateRange, strStartDate, strEndDate);
             
            return View(objModel);
        }

        #region funciones privadas 
        private string CreateEmailHeader(string pstrTitle, string pstrCAC, string pstrDate, string pstrHeadLine, string pstrCaseInteraction,
                                         string pstrRepresentative, string pstrNumberClaro, string pstrDocumentType, string pstrNumberDocument)
        {
            string strHeader = "";
            strHeader = "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strHeader += "				<tr><td width='100%' class='Estilo1'>Estimado Cliente:</td></tr>";
            strHeader += "				<tr><td height='10'></td>";
            if (pstrTitle == "Servicio de Variación de Débito / Crédito Manual Corporativo")
                strHeader += "				<tr><td class='Estilo1'>Por la presente queremos informarle que se realizo un ajuste al saldo de su linea corporativa</td></tr>";
            else
                strHeader += "				<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " + pstrTitle + " fue atendida.</td></tr>";

            strHeader += "				<tr><td height='10'></td>";
            strHeader += "				<tr>";
            strHeader += "					<td align='center'>";
            strHeader += "						<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strHeader += "							<TR>";
            strHeader += "								<TD width='180' class='Estilo2' height='22'>CAC/DAC:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrCAC + "</TD>";
            strHeader += "								<TD width='20'>&nbsp;</TD>";
            strHeader += "								<TD width='180' class='Estilo2'>Fecha:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrDate + "</TD>";
            strHeader += "							</TR>";
            strHeader += "							<TR>";
            strHeader += "								<TD width='180' class='Estilo2' height='22'>Titular:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrHeadLine + "</TD>";
            strHeader += "								<TD width='20'>&nbsp;</TD>";
            strHeader += "								<TD width='180' class='Estilo2'>Caso / Interacción:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrCaseInteraction + "</TD>";
            strHeader += "							</TR>";
            strHeader += "							<TR>";
            strHeader += "								<TD width='180' class='Estilo2' height='22'>Representante Legal:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrRepresentative + "</TD>";
            strHeader += "								<TD width='20'>&nbsp;</TD>";
            strHeader += "								<TD width='180' class='Estilo2'>Nro. Servicio:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrNumberClaro + "</TD>";
            strHeader += "							</TR>";
            strHeader += "							<TR>";
            strHeader += "								<TD width='180' class='Estilo2' height='22'>Tipo Doc. Identidad:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrDocumentType + "</TD>";
            strHeader += "								<TD width='20'>&nbsp;</TD>";
            strHeader += "								<TD width='180'></TD>";
            strHeader += "								<TD></TD>";
            strHeader += "							</TR>";
            strHeader += "							<TR>";
            strHeader += "								<TD width='180' class='Estilo2' height='22'>Nro. Documento:</TD>";
            strHeader += "								<TD class='Estilo1'>" + pstrNumberDocument + "</TD>";
            strHeader += "								<TD width='20'>&nbsp;</TD>";
            strHeader += "								<TD width='180'></TD>";
            strHeader += "								<TD></TD>";
            strHeader += "							</TR>";
            strHeader += "						</Table>";
            strHeader += "					</td>";
            strHeader += "				</tr>";
            strHeader += "			</table>";
             
            return strHeader; 
        }

        private string CreateEmailMessage(CommonService.InsertTemplateInteraction objTemplateInteraction, HelperGeneral.CustomerData customer, string idCase)
        {
            string strmessage = string.Empty;

            strmessage = "<html>";
            strmessage += "<head>";
            strmessage += "<style type='text/css'>";
            strmessage += "<!--";
            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strmessage += "-->";
            strmessage += "</style>";
            strmessage += "<body>";
            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";//vcaso ver si es hidcasoid
            strmessage += "<tr><td>";
            strmessage += CreateEmailHeader("Detalle de Llamada", objTemplateInteraction._X_INTER_15, DateTime.Now.ToString(), customer.FirstName, idCase, customer.LegalRepresentative, objTemplateInteraction._X_CLARO_NUMBER, customer.DocumentType, customer.NumberDocument);
            strmessage += "</td></tr>";

            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr>";
            strmessage += "		<td align='center'>";
            strmessage += "			<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += "				<TR>";
            strmessage += "					<TD width='180' class='Estilo2' height='22'>Meses Solicitados:</TD>";
            strmessage += "					<TD class='Estilo1'>" + objTemplateInteraction._X_INTER_29 + "</TD>";
            strmessage += "				</TR>";
            strmessage += "			</Table>";
            strmessage += "		</td>";
            strmessage += "</tr>";

            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";

            return strmessage;
        }
       
        private bool GenerateListCallsDetail(ref ModelPospaid.BilledOutCallDetailModel objModel,
            string type, string sourceData)
        {
            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC01"); // Temporal
            if (type.Equals(Constant.Letter_L))
            {
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC02"); // Temporal
                string strTotal = "";
                PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objModel.IdSession);
                if (!String.IsNullOrEmpty(objModel.HidInvoiceNumberWithDates)) {
                    string[] strFechas = objModel.HidInvoiceNumberWithDates.Split('$');
                    string strFechaInicial = Functions.GetDDMMYYYYAsDateTime(strFechas[1]).ToShortDateString();
                    string strFechaFinal = Functions.GetDDMMYYYYAsDateTime(strFechas[2]).ToString("yyyyMMdd");
                    
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC03"); // Temporal
                    if (objModel.FlagPlatform.Equals(Constant.Letter_C))
                    {
                        Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC04"); // Temporal
                        PostTransacService.BillPostDetailRequestTransactions objListBill = new PostTransacService.BillPostDetailRequestTransactions();
                        objListBill.audit = audit;
                        objListBill.vFECHA_FIN = "a";
                        objListBill.vFECHA_INI = "a";
                        objListBill.vFlag = "l";
                        objListBill.vFLAG_777 = "w";
                        objListBill.vFLAG_DETALLE = "";
                        objListBill.vFLAG_GPRS = "";
                        objListBill.vFLAG_INTERNACIONA = "";
                        objListBill.vFLAG_NACIONAL = "";
                        objListBill.vFLAG_SMS_MMS = "";
                        objListBill.vFLAG_TIPO_VISUAL = "";
                        objListBill.vFLAG_VAS = "";
                        objListBill.vMSISDN = "";
                        objListBill.vSeguridad = "";
                        PostTransacService.BillPostDetailResponseTransactions objResponse = oServicePostPaid.GetBillPostDetail(objListBill);

                        //objModel.ListCallsDetail = MappingListCallsDetail(objResponse.GetBillPostDetail.ToList()); 
                        ////list01 = ListBill.GetBillPostDetail; //ListBill =>objResponse
                        //InvoiceList.lista1 = list01;
                        objModel.StrDateRange = strFechaInicial + "$" + strFechaFinal;
                    }
                    else
                    {
                        Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC05"); // Temporal
                        PostTransacService.ListCallDetailResponseTransactions objResponse = new PostTransacService.ListCallDetailResponseTransactions();
                        PostTransacService.ListCallDetailPDIResponseTransactions objResponsePDI = new PostTransacService.ListCallDetailPDIResponseTransactions();
                        if (sourceData == Constant.strUno)
                        {
                            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC06"); // Temporal
                            PostTransacService.ListCallDetailRequestTransactions objListCall = new PostTransacService.ListCallDetailRequestTransactions();
                            objListCall.audit = audit;
                            objListCall.vINVOICENUMBER = strFechas[0];
                            objListCall.vTELEFONO = objModel.Telephone;
                            objListCall.vSeguridad = objModel.FlagSecurity;
                            objResponse = oServicePostPaid.GetListCallDetail(objListCall);
                            objModel.ListCallsDetail = MappingListCallsDetail(objResponse.GetListCallDetail);
                        }
                        else
                        {
                            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC07"); // Temporal
                            PostTransacService.ListCallDetailPDIRequestTransactions objListCallPdi = new PostTransacService.ListCallDetailPDIRequestTransactions();
                            objListCallPdi.audit = audit;
                            objListCallPdi.vINVOICENUMBER = strFechas[0];
                            objListCallPdi.vSeguridad = objModel.FlagSecurity;
                            objListCallPdi.vTELEFONO = objModel.Telephone;
                            objResponsePDI = oServicePostPaid.GetListCallDetailPDI(objListCallPdi);
                            objModel.ListCallsDetail = MappingListCallsDetail(objResponse.GetListCallDetail);
                        }
                        objModel.StrDateRange = String.Format("Del {0} al {1}", strFechas[1], strFechas[2]);
                    } 
                }
                

                if (objModel.ListCallsDetail != null && objModel.ListCallsDetail.Count > 0)
                {
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC08"); // Temporal
                    if (objModel.FlagPlatform == "C")
                    {
                        //strTotal = GetTotalCallsMonthPrepaidControl(InvoiceList.lista1);
                        objModel.LitFinalCharge = "Total Saldo:";
                    }
                    else
                    {
                        strTotal = GetTotalTR_DetailCall(objModel.ListCallsDetail);
                        objModel.LitFinalCharge = "Total Consumo:";
                    }
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "GLC09"); // Temporal
                    string[] arrTotal = strTotal.Split(';');
                    objModel.StrTotal = Functions.GetFormatHHMMSS(Functions.CheckInt64(Functions.CheckDbl(arrTotal[1], 2)));
                    objModel.StrTotalSMS= Functions.CheckInt64(Functions.CheckDbl(arrTotal[2], 2)).ToString("0");
                    objModel.StrTotalMMS= Functions.CheckInt64(Functions.CheckDbl(arrTotal[3], 2)).ToString("0");
                    objModel.StrTotalGPRS= Functions.CheckInt64(Functions.CheckDbl(arrTotal[4], 2)).ToString("0") + " KB";
                    objModel.StrTotalRegistration = objModel.ListCallsDetail.Count.ToString();
                    objModel.StrFinalCharge= "S/. " + Functions.CheckStr(Functions.CheckDbl(arrTotal[0], 4)).ToString();
                     
                    objModel.StrDescription = "Consulta de Detalle de LLamadas retornó registros";
                    return true;
                }
                else {
                    objModel.StrTotal ="0.00";
                    objModel.StrTotalSMS= "0";
                    objModel.StrTotalMMS=  "0";
                    objModel.StrTotalGPRS="0 KB";
                    objModel.StrTotalRegistration =  "0";
                    objModel.StrFinalCharge= "S/. 0.00";

                    objModel.StrDescription = "Consulta de Detalle de LLamadas NO retornó registros";

                    objModel.ListCallsDetail = new List<BilledOutCallDetailsPost>();
                    return false;
                }
            }
            objModel.StrDescription = "Consulta de Detalle de LLamadas no realizada";
            return false;
        }

        private void GeneratePermissions(ref ModelPospaid.BilledOutCallDetailModel objModel, string strPermissions)
        {
            string strKeySendMail = KEY.AppSettings("gConstkeySendMailNotifiDL");
            string strKeySeeDestinationNumber = KEY.AppSettings("gConstKeySeeNumbDestTrans");
            string strKeyPrint = KEY.AppSettings("gConstEvtPrintDetailCall");
            string strKeyShowPrint = KEY.AppSettings("gConstEvtButtonPrintDetailCall");
            string strKeyExport = KEY.AppSettings("gConstEvtExportCallDetail");
            string strKeyShowExport = KEY.AppSettings("gConstEvtButtonExportDeatilCall");
            string strKeySearch = KEY.AppSettings("gConstEvtSearchCallDetail");

            if (strPermissions.IndexOf(strKeySendMail) >= 0)
                objModel.FlagEmail = Constant.Letter_C;
            else
                objModel.FlagEmail = Constant.Letter_F;

            if (strPermissions.IndexOf(strKeySeeDestinationNumber) >= 0)
                objModel.FlagSecurity = Constant.strUno;
            else
                objModel.FlagSecurity = Constant.strCero;

            if (strPermissions.IndexOf(strKeySearch) >= 0)
                objModel.FlagSearch = Constant.Letter_T;
            else
                objModel.FlagSearch = Constant.Letter_F;

            if (strPermissions.IndexOf(strKeyShowPrint) >= 0)
            {
                if (strPermissions.IndexOf(strKeyPrint) >= 0)
                    objModel.FlagPrint = Constant.Letter_T;
                else
                    objModel.FlagPrint = Constant.Letter_F;
            }
            else
                objModel.FlagPrint = Constant.Letter_I;

            if (strPermissions.IndexOf(strKeyShowExport) >= 0)
            {
                if (strPermissions.IndexOf(strKeyExport) >= 0)
                    objModel.FlagExport = Constant.Letter_T;
                else
                    objModel.FlagExport = Constant.Letter_F;
            }
            else
                objModel.FlagExport = Constant.Letter_I;

        }

        private string GetTotalTR_DetailCall(List<BilledOutCallDetailsPost> list)
        {
            double Total = 0;
            double TotalMIN = 0;
            double TotalSMS = 0;
            double TotalMMS = 0;
            double TotalGPRS = 0;
            string[] Cantidad;
            double Consumo = 0;
             
            if (list != null)
            {
                try
                {
                    foreach (BilledOutCallDetailsPost item in list)
                    {
                        Cantidad = item.Consumption.Split(char.Parse(":"));

                        if (Cantidad.Length.Equals(1))
                        {
                            Consumo = Functions.CheckDbl(Cantidad[0]);
                        }
                        else
                        {
                            if (Cantidad.Length.Equals(2))
                                Consumo = (Functions.CheckDbl(Cantidad[0]) * 60) + Functions.CheckDbl(Cantidad[1]);
                            else
                                Consumo = (Functions.CheckDbl(Cantidad[0]) * 3600) + (Functions.CheckDbl(Cantidad[1]) * 60) + Functions.CheckDbl(Cantidad[2]);
                        }

                        if (item.CallType.ToUpper().IndexOf("LLAMADA") != -1 || item.CallType.ToUpper().IndexOf("MOC") != -1)
                        {
                            TotalMIN += Consumo;
                        }
                        else
                        {
                            if (item.CallType.ToUpper().IndexOf("SMS") != -1)
                            {
                                TotalSMS += Consumo;
                            }
                            else
                            {
                                if (item.CallType.ToUpper().IndexOf("MMS") != -1)
                                {
                                    TotalMMS += Consumo;
                                }
                                else
                                {
                                    if (item.CallType.ToUpper().IndexOf("GPRS") != -1)
                                    {

                                        TotalGPRS += Consumo;
                                    }
                                }
                            }
                        }

                        Total += Functions.CheckDbl(item.OriginalCharge);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return Functions.CheckStr(Total) + ";" + Functions.CheckStr(TotalMIN) + ";" + Functions.CheckStr(TotalSMS) + ";" + Functions.CheckStr(TotalMMS) + ";" + Functions.CheckStr(TotalGPRS);
        }

        private CommonTransacService.InsertTemplateInteraction InteractionTemplateData( string nameTransaction, string strPhoneNumber, string hidInvoiceNumber,
            string firstName, string lastName, string strLegalRepresentative,string strNumberDocument,
            string nameMonth, string codeYear, string flagSendEmail,string email )
        {
            CommonTransacService.InsertTemplateInteraction entity =
                new CommonTransacService.InsertTemplateInteraction();
            try
            {
                entity._NOMBRE_TRANSACCION = nameTransaction;
                entity._X_CLARO_NUMBER = strPhoneNumber;
                entity._X_DOCUMENT_NUMBER = strNumberDocument;
                entity._X_FIRST_NAME = firstName;
                entity._X_LAST_NAME = lastName;
                entity._X_NAME_LEGAL_REP = strLegalRepresentative;
                entity._X_DNI_LEGAL_REP = strLegalRepresentative;
                entity._X_FLAG_REGISTERED = (flagSendEmail == "T") ? Constant.strUno : Constant.strCero; 
                entity._X_EMAIL = email;
                entity._X_INTER_15 = String.Empty;
                entity._X_INTER_16 = hidInvoiceNumber;
                entity._X_INTER_29 = nameMonth + "-" +codeYear ;
                //Observacion
                entity._X_INTER_5 = (flagSendEmail == "T") ? Constant.strUno : Constant.strCero;
                entity._X_REGISTRATION_REASON = email; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }
         
        private void RegisterAudit(ModelPospaid.BilledOutCallDetailModel objModel, string strProfileCode,
            string strCodeEvent, string strDescription)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);

            if (objModel.FlagPlatform.Equals("Pers")) return;

            try
            {
                double perfilCode;
                double dblEventCode = Double.Parse(strCodeEvent);
                string strIdTransaccion = KEY.AppSettings("gConstIDTransaccionAplicacionPostpago_BilledOutCallDetail") + Functions.CadenaAleatoria();
                string strAmmount = Constant.strCero;
                string strTransaction = strCodeEvent;
                string strService = KEY.AppSettings("gConstEvtServicio_BilledOutCallDetail"); 
                  
                if (!String.IsNullOrEmpty(strProfileCode))
                {
                    string[] arrProfiles = strProfileCode.Split(',');
                    perfilCode = Double.Parse(arrProfiles[0]);
                }

                string[,] arrAuditDetail = new string[3, 3];
                arrAuditDetail[0, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnTelephone;
                arrAuditDetail[0, 1] = objModel.Telephone;
                arrAuditDetail[1, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnInvoiceNumber;
                arrAuditDetail[1, 1] = objModel.HidInvoiceNumberWithDates;
                arrAuditDetail[2, 0] = Constant.Notes_OutgoingCallsNBP.NameColumnTypeTransaction;
                arrAuditDetail[2, 1] = Constant.GstrTransactionCallDetail;
                StringBuilder sbText = new StringBuilder();
                for (int i = 0; i < Math.Sqrt(arrAuditDetail.Length); i++)
                {
                    if ((!String.IsNullOrEmpty(arrAuditDetail[i, 0])) && (!String.IsNullOrEmpty(arrAuditDetail[i, 1])))
                    {
                        sbText.Append(" " + arrAuditDetail[i, 0] + " : " + arrAuditDetail[i, 1]);
                    }
                }
                string strText = ((String.IsNullOrEmpty(strDescription)) ? "" : strDescription + " - ") + sbText.ToString();

                commonController.RegisterAuditGeneral(objModel.IdSession, objModel.Telephone, strAmmount, strText, strService, strTransaction);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
        }

        private bool RegisterTransaction(ref ModelPospaid.BilledOutCallDetailModel objModel, HelperGeneral.CustomerData customer,
             string profileCode, string numberContract,string nameMonth, string codeYear, string flagSendEmail, string email, 
            string hidTypePD, string hidClassPD, string hidSubClassPD, string hidTransaction
            )
        {
            bool executeTransaction = true;
            string strFinalNotes = String.Format(Constant.BilledOutCallPostpaid.Message_InfoCallsDetailOfTheMonth, nameMonth, codeYear);
            CommonService.Iteraction interaction = commonController.InteractionData(objModel.IdSession, customer.IdContact, objModel.Telephone,
                strFinalNotes, hidTypePD, hidClassPD, hidSubClassPD, "");
             
            CommonService.InsertTemplateInteraction interactionTemplateData = InteractionTemplateData(hidTransaction, objModel.Telephone, objModel.HidInvoiceNumber,
                customer.FirstName, customer.LastName, customer.LegalRepresentative, customer.NumberDocument,
                nameMonth, codeYear, flagSendEmail, email);

            CommonService.InsertGeneralResponse objResponse = GetInsertBusinessInteractionResponse(objModel.IdSession, objModel.Telephone,
                interaction, interactionTemplateData, executeTransaction);
            objModel.HidIdCase = objResponse.rInteraccionId;

            if ((!String.IsNullOrEmpty(objResponse.rFlagInsercion)) && !objResponse.rFlagInsercion.Equals(Constant.Message_OK))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_ErrorCreatingInteraction, objResponse.rMsgText);
                return false;
            }
            if ((!String.IsNullOrEmpty(objResponse.rFlagInsercionInteraccion)) && !objResponse.rFlagInsercionInteraccion.Equals("OK"))
            {
                string message01 = Functions.GetValueFromConfigSiacUnico(Constant.Notes_IncomingCallsPrepaid.KeyXML_MessageErrorTypifTransact);
                string message02 = String.Format(Constant.Notes_IncomingCallsPrepaid.NotesNumberInteraction, objResponse.rInteraccionId);
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_ErrorInsercionInteraccion, message01, objResponse.rMsgTextInteraccion, message02);
                return false;
            }

            if (flagSendEmail.Equals(Constant.Letter_T)) {
                SendEmail(ref objModel, customer, interactionTemplateData);
            }
            else
                objModel.StatusCode = Constant.StatusCode_OK;
            RegisterAudit(objModel, profileCode, KEY.AppSettings("gConstEvtDetalleLlamadaPostpago_BilledOutCallDetail"), objModel.StatusMessage); 
            return true;
        }

        private void RunSearch(ref string FechaInicio, ref string FechaFin, ref ModelPospaid.BilledOutCallDetailModel objModel, string idCustomer, string profileCode, string strSelectedPeriod)
        {
            
            String strDataSource = "";
            if (!string.IsNullOrEmpty(objModel.Telephone))
            {
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS01"); 
                PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objModel.IdSession);
                int intNMonth = Int32.Parse(Functions.GetValueFromConfigSiacUnico(Constant.Key_LastMonthDBTO));
                DateTime dtTemp = DateTime.Now.AddMonths(-1);
                DateTime dtLastFactDate = new DateTime(dtTemp.Year, dtTemp.Month , 1);
                DateTime dtBillDateLimit = dtLastFactDate.AddMonths(-intNMonth);
                int intMonthYearLimit;

                if (Functions.GetValueFromConfigSiacUnico(Constant.Key_ConfCallDetail) == Constant.strCero ) 
                    intMonthYearLimit = Int32.Parse(dtBillDateLimit.ToString("yyyyMM")) ; 
                else
                    intMonthYearLimit = Int32.Parse(Functions.GetValueFromConfigSiacUnico(Constant.Key_LastMonthDBTOTOPE));
                int intMonthYearSelected = Int32.Parse(strSelectedPeriod);
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS06"); 
                PostTransacService.ListInvoiceResponseTransactions list1 = null;
                PostTransacService.ListInvoice_PDIResponseTransactions list2 = null;

                if (intMonthYearSelected <= intMonthYearLimit)
                {
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS07"); 
                    strDataSource = Constant.strDos;
                    PostTransacService.ListInvoiceRequestTransactions objListInvoiceRequest = new PostTransacService.ListInvoiceRequestTransactions();
                    objListInvoiceRequest.vCODCLIENTE = idCustomer;
                    objListInvoiceRequest.strIdSession = audit.Session;
                    objListInvoiceRequest.strTransaction = audit.transaction;
                    objListInvoiceRequest.audit = audit;
                    list1 = oServicePostPaid.GetListInvoice(objListInvoiceRequest);
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS09"); 
                }
                else {
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS10"); 
                    strDataSource = Constant.strUno;
                    PostTransacService.ListInvoice_PDIRequestTransactions objListInvoice_PDIRequest = new PostTransacService.ListInvoice_PDIRequestTransactions();
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS11"); 
                    objListInvoice_PDIRequest.vCODCLIENTE = idCustomer;
                    objListInvoice_PDIRequest.strIdSession = audit.Session;
                    objListInvoice_PDIRequest.strTransaction = audit.transaction;
                    objListInvoice_PDIRequest.audit = audit;
                    list2 = oServicePostPaid.GetListInvoicePDI(objListInvoice_PDIRequest);
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS12"); 
                }

                if (list1 != null) 
                {
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS13"); 
                    InvoiceTransactions item = null;
                    if (list1.GetListInvoice != null)
                    {
                        item = list1.GetListInvoice.FirstOrDefault(x => x.PERIODO == strSelectedPeriod);
                    }
                    if (item != null)
                    {
                        FechaInicio = item.FECHAINICIO;
                        FechaFin = item.FECHAFIN;
                        objModel.HidInvoiceNumberWithDates = item.INVOICENUMBER + "$" + item.FECHAINICIO + "$" + item.FECHAFIN;
                        objModel.StrDayEmission = item.FECHAEMISION.Split('/')[0];
                        Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS14"); // Temporal
                    }
                }
                else if (list2 != null) 
                {
                    Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS15"); // Temporal
                    Claro.Web.Logging.Info("993760253", "SalientesFacturado", strSelectedPeriod); // Temporal
                    
                    Invoice_PDITransactions item =null;
                    if (list2.GetListInvoicePDI != null) {
                        item = list2.GetListInvoicePDI.FirstOrDefault(x => x.PERIODO == strSelectedPeriod);
                    }

                    // ===== Bloque Temporal =====
                    if (item == null && list2.GetListInvoicePDI != null) {
                        foreach (Invoice_PDITransactions invoice in list2.GetListInvoicePDI) {
                            Claro.Web.Logging.Info("Persquash", "RunSearch", string.Format("Periodo: ", invoice.PERIODO)); // Temporal
                        }
                    }
                    // ===== Fin del Bloque Temporal =====

                    Claro.Web.Logging.Info("993760253", "SalientesFacturado", String.Format("item|| item:{0}",item));
                    if (item != null)
                    {
                        FechaInicio = item.FECHAINICIO;
                        FechaFin = item.FECHAFIN;
                        objModel.HidInvoiceNumberWithDates = item.INVOICENUMBER + "$" + item.FECHAINICIO + "$" + item.FECHAFIN;
                        objModel.StrDayEmission = item.FECHAEMISION.Split('/')[0];
                        Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS16"); // Temporal
                    }
                }
            }
            Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "antes del RS17");
            if (!string.IsNullOrEmpty(objModel.Telephone))
            {
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS17"); // Temporal
                string strType = Constant.Letter_L ;
                bool result = GenerateListCallsDetail(ref objModel, strType, strDataSource);
                //RegisterAudit(objModel, profileCode, objModel.StrDateRange, strType, KEY.AppSettings("gConstEvtConsultaDetLlamFact"), objModel.StrDescription); 
                RegisterAudit(objModel, profileCode, KEY.AppSettings("gConstEvtConsultaDetLlamFact"), objModel.StrDescription);
                Claro.Web.Logging.Info("Persquash", "SalientesFacturado", "RS19"); // Temporal
            }
            else {
                objModel.ListCallsDetail = new List<BilledOutCallDetailsPost>(); 
            }
        }
         
        private void SendEmail(ref ModelPospaid.BilledOutCallDetailModel objModel ,HelperGeneral.CustomerData customer,CommonService.InsertTemplateInteraction objTemplateInteraction)
        {
            //If Me.hidFlagPlataforma.Value = "C" Then
            //    strAdjunto = CrearArchivoDetalleLlamadaControl()
            //Else
            //    strAdjunto = CrearArchivoDetalleLlamada()
            //End If

            string strEmailMessage = CreateEmailMessage(objTemplateInteraction, customer, objModel.HidIdCase);

            string strDestinatarios = objTemplateInteraction._X_EMAIL;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objModel.IdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strTo = strDestinatarios,
                    strSender = strRemitente,
                    strMessage = strEmailMessage,
                    strSubject = Constant.BilledOutCallPostpaid.NameModule
                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oCommonTransacService.GetSendEmail(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }
            
            if (objGetSendEmailResponse.Exit.Equals(Constant.Message_OK)  ){
                objModel.StatusCode = Constant.StatusCode_OK ;
                objModel.StatusMessage  = Constant.BilledOutCallPostpaid.Message_SendMailSuccessful;
            }else{
                objModel.StatusCode = Constant.StatusCode_Error ;
                objModel.StatusMessage = Constant.BilledOutCallPostpaid.Message_SendMailUnsuccessful;
            }
             
        }
        private string ValidatePermission(string idSession, string idContract)
        {
            Claro.Web.Logging.Error("222222", idSession, idContract);
            string nameTypePhone = "";

            if (!String.IsNullOrEmpty(idContract))
            {
                string variable = "", variableTFI = "", codeResultDL = "", codeTariffPlan = "";
                string[] array01, array02, array03;
                PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(idSession);
                CommonService.AuditRequest auditC = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);

                try
                {
                    int intCodParam = Convert.ToInt(KEY.AppSettings("gObtenerParametroTerminalTPI"));
                    int intCodParam2 = Convert.ToInt(KEY.AppSettings("gObtenerParametroSoloTFIPostpago"));
                    
                    CommonService.ParameterTerminalTPIResponse objResponse = GetParameterTerminalTPIResponse(intCodParam, auditC);
                    var objList = objResponse.ListParameterTeminalTPI;

                    objResponse = GetParameterTerminalTPIResponse(intCodParam2, auditC);
                    var objListTFI = objResponse.ListParameterTeminalTPI;

                    variable = objList[0].ValorC;
                    variableTFI = objListTFI[0].ValorC;

                    PostpaidService.DataLineResponsePostPaid objDataLineResponse = GetDataLineResponse(idContract, audit);
                    codeResultDL = objDataLineResponse.StrResponse;
                    codeTariffPlan = (objDataLineResponse.DataLine.TariffPlan == null) ? "" : objDataLineResponse.DataLine.TariffPlan;

                    bool successfulSearch = false;
                    if (!string.IsNullOrEmpty(variable) && (!string.IsNullOrEmpty(codeTariffPlan)))
                    {
                        array01 = variable.Split(';');
                        for (int i = 0; i < array01.Length - 1; i++)
                        {
                            array02 = array01[i].Split(':');
                            if (array02 != null && array02.Length > 1 && (!String.IsNullOrEmpty(array02[1])))
                            {
                                array03 = array02[1].Split(',');
                                for (int j = 0; j < array03.Length; j++)
                                {
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

                    if (!successfulSearch)
                    {
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

                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }
            }
            Claro.Web.Logging.Error("222222", "11111", nameTypePhone);
            return nameTypePhone;
        }
        #endregion

        #region Funciones Mapping
        private List<BilledOutCallDetailsPost> MappingListCallsDetail(List<ListCallDetailTransactions> listDB)
        {
            List<BilledOutCallDetailsPost> list = new List<BilledOutCallDetailsPost>();
            BilledOutCallDetailsPost entity = null;
            int i = 1;
            foreach (var item in listDB)
            { 
                entity = new BilledOutCallDetailsPost()
                {
                    Nro = i++,
                    StrDate = item.FECHA ,
                    StrHour = item.HORA,
                    DestinationPhone = item.TELEFONO_DESTINO,
                    Consumption = item.CONSUMO ,
                    OriginalCharge = item.CARGO_ORIGINAL,
                    CallType = item.TIPO_LLAMADA,
                    Destiny = item.DESTINO ,
                    Operator = item.OPERADOR
                };
                list.Add(entity);
            }
            return list;
        }
        #endregion

        #region Funciones WS
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

        private CommonService.InsertGeneralResponse GetInsertBusinessInteractionResponse(string idsession, string telephone, CommonService.Iteraction interaction, CommonService.InsertTemplateInteraction interactionTemplate,
            bool executeTransaction)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idsession);
            CommonService.InsertGeneralRequest request = new CommonService.InsertGeneralRequest()
            {
                audit = audit,
                Interaction = interaction,
                InteractionTemplate = interactionTemplate,
                vEjecutarTransaccion = executeTransaction,
                vNroTelefono = telephone,
                vUSUARIO_APLICACION = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago"),
                vUSUARIO_SISTEMA = commonController.CurrentUser(idsession),
                vPASSWORD_USUARIO = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago")
            };

            CommonService.InsertGeneralResponse objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.InsertGeneralResponse>(() =>
            {
                return oCommonTransacService.GetinsertInteractionGeneral(request); ;
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
                return oCommonTransacService.GetParameterTerminalTPI(objRequest);
            });
            return objResponse;
        }
        #endregion
       
	}
}