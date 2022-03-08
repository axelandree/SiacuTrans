using PostpaidController=Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Prepaid;
using KEY = Claro.ConfigurationManager;
using Constant = Claro.SIACU.Transac.Service.Constants;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using PrepaidService = Claro.SIACU.Web.WebApplication.Transac.Service.PreTransacService;
using HelperController = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.IncomingCallsDetail;
using HelperCommon = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using System.Text;
using Claro.SIACU.Transac.Service;
using ItemGeneric = Claro.SIACU.Transac.Service.ItemGeneric;
using Claro.Helpers;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Prepaid
{
    public class IncomingCallDetailController : Controller
    {
        private readonly CommonService.CommonTransacServiceClient oServiceCommon = new CommonService.CommonTransacServiceClient();
        private readonly PrepaidService.PreTransacServiceClient oServicePrepaid = new PrepaidService.PreTransacServiceClient(); 

        private readonly CommonServicesController commonController = new CommonServicesController();
         
        public ActionResult PrepaidIncomingCallDetail()
        {
            return PartialView();
        }
        //public JsonResult GenerateContancy(string idSession,string hidInteraction, HelperCommon.CustomerData client,
        //    string nameCACDAC, string strNotes)
        //{   
        //    Model.IncomingCallsDetail objModel = (Model.IncomingCallsDetail)Session["ModelICD_Prepaid"]; 
        //    CommonService.ParametersGeneratePDF parameters = new CommonService.ParametersGeneratePDF()
        //    {
        //        StrCentroAtencionArea = nameCACDAC,
        //        StrTitularCliente = client.FirstName + ' ' + client.LastName,
        //        StrRepresLegal = client.LegalRepresentative,
        //        StrTipoDocIdentidad = client.DocumentType,
        //        StrNroDocIdentidad = client.NumberDocument,
        //        StrFechaTransaccionProgram = DateTime.Now.ToShortDateString(),
        //        StrCasoInter = hidInteraction,
        //        StrFecInicialReporte = objModel.StrStartDate,
        //        StrFecFinalReporte = objModel.StrEndDate,
        //        StrNroServicio = objModel.NumberPhone,
        //        StrMontoOCC = objModel.Amount.ToString("#.##"),
        //        strEnvioCorreo = Constant.Variable_NO,
        //        StrEmail = String.Empty,
        //        StrNotas = strNotes,
        //        StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
        //            KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
        //        StrContenidoComercial2 = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
        //            KEY.AppSettings("strConstArchivoSIACPOConfigMsg")), 
        //        StrCarpetaTransaccion = ConfigurationManager.AppSettings("strDetalleLlamadasEntrantesTransac").ToString() + "\\" + Constant.StrTipoLinea_PREPAGO,
        //        StrTipoTransaccion = "IncomingCallPrepaid",
        //        StrNombreArchivoTransaccion = KEY.AppSettings("strDetalleLlamadasEntrantesTransac"), 
        //    };
        //    CommonService.GenerateConstancyResponseCommon response =commonController.GenerateContancyPDF(idSession, parameters);
             
        //    if (!response.Generated)
        //    { 
        //        objModel.StatusCode = Constant.StatusCode_Error;
        //        objModel.StatusMessage = String.Format(Constant.Message_ErrorGeneral, response.ErrorMessage);
        //        return Json(objModel, JsonRequestBehavior.AllowGet);
        //    } 
        //    objModel.StatusCode = Constant.StatusCode_OK;
        //    objModel.FullPathPDF = response.FullPathPDF;
        //    return Json(objModel, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GenerateDataForPrinting(string idSession, string strTelephone, string userAdmin, string clientAccount, string flagPrint, string flagLoadDataline, string strStartDate, string strEndDate)
        {
            //Model.IncomingCallsDetail objModel = new Model.IncomingCallsDetail();
            Model.IncomingCallsDetail objModel = (Model.IncomingCallsDetail)Session["ModelICD_Prepaid"];
            objModel.IdSession = idSession;
            objModel.NumberPhone = commonController.GetNumber(idSession, false, strTelephone);
            objModel.StrStartDate = strStartDate;
            objModel.StrEndDate = strEndDate;

            LoadReport(ref objModel, Constant.Action_Print);

            if (objModel.StatusCode.Equals(Constant.StatusCode_OK))
            {
                Session["ModelICD_Prepaid"] = objModel;
                 
                if (!flagPrint.Equals(Constant.Letter_T))
                {
                    commonController.SendMail(idSession, userAdmin, objModel.NumberPhone, clientAccount);
                }
                objModel.StatusCode = Constant.StatusCode_OK;
            }  
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPathExportExcel(string idSession, string idContact, string nameClient, string nameCACDAC,  
             string flagLoadDataline , string flagExport, string userAdmin, string clientAccount)
        {
            ExcelHelper oExcelHelper = new ExcelHelper();
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);

            Model.IncomingCallsDetail objModel = (Model.IncomingCallsDetail)Session["ModelICD_Prepaid"];
            objModel.StatusCode = "";
            try
            {
                objModel.TotalRecords = objModel.ListCallsDetail.Count.ToString();
                objModel.NameClient = nameClient;
                objModel.NameCACDAC = nameCACDAC;

                LoadReport(ref objModel, Constant.Action_Export);

                if (objModel.StatusCode.Equals(Constant.StatusCode_OK))
                {
                    if (!flagExport.Equals(Constant.Letter_T))
                    {
                        commonController.SendMail(idSession, userAdmin, objModel.NumberPhone, clientAccount);
                    } 
                }  
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            if (objModel.StatusCode.Equals(Constant.StatusCode_OK))
            {
                Session["ModelICD_Prepaid"] = objModel;

                objModel.StatusCode = Constant.StatusCode_OK;
                List<int> lstHelperPlan = Enumerable.Range(0, 10).ToList();
                objModel.PathExcel = oExcelHelper.ExportExcel(objModel, Claro.SIACU.Transac.Service.TemplateExcel.CONST_EXPORT_PREPAID_INCOMINGCALLDETAIL, lstHelperPlan);
            } 
            return Json(objModel);
        }
         
        public JsonResult Load(string idSession, string strPhone, string clientFullName, string flagGenerateOCC,
            string[] arrPermissions,string flagPlatform)
        {
            Claro.Web.Logging.Configure();
            Claro.Web.Logging.Info("Persquash", "EntrantesPrepago", "Entro a Load"); // Temporal
            Model.IncomingCallsDetail objModel = new Model.IncomingCallsDetail();
            objModel.IdSession = idSession;
            objModel.NumberPhone = commonController.GetNumber(idSession, false, strPhone);

            string strArrPermissions = string.Join(",", arrPermissions);
            if (String.IsNullOrEmpty(objModel.NumberPhone) || objModel.NumberPhone==Constant.strCero)
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Notes_IncomingCallsPrepaid.Message_NotLoadedDataLine;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
             
            LoadData(ref objModel, clientFullName, flagGenerateOCC, Constant.strCero);
            if (objModel.FlagLoadDataLine.Equals(Constant.strDos)) {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Notes_IncomingCallsPrepaid.Message_NotLoadedDataLine;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            } 
            EnablePermissions(ref  objModel, strArrPermissions);

            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
         
        public ActionResult PrepaidIncomingCallDetailPrint(string nameClient, string nameCACDAC)
        {
            Model.IncomingCallsDetail objModel = (Model.IncomingCallsDetail)Session["ModelICD_Prepaid"];
            objModel.NameClient = nameClient;
            objModel.NameCACDAC = nameCACDAC;
            return View(objModel);
        }

        public JsonResult Save(string idSession, HelperCommon.CustomerData client,
            string strNotes, string flagLoadDataline, string flagGenerateOCC, string idCACDAC,
            string nameCACDAC, string strStartDate, string strEndDate)
        {
            //Model.IncomingCallsDetail objModel = new Model.IncomingCallsDetail();
            Model.IncomingCallsDetail objModel = (Model.IncomingCallsDetail)Session["ModelICD_Prepaid"];
            objModel.IdSession = idSession;
            objModel.NumberPhone = commonController.GetNumber(idSession, false, client.Telephone);
            objModel.StrStartDate = strStartDate;
            objModel.StrEndDate = strEndDate;

            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);
             
            CommonService.ParameterDataResponseCommon objResponse = GetParameterDataResponse(KEY.AppSettings(Constant.Notes_IncomingCallsPrepaid.Key_AmountOCC), audit);
            double amount = objResponse.Parameter.Value_N;

            HelperController.Line objLine = LoadDataLinePrepaid(objModel, Constant.strUno);
            double mainBalance = Functions.CheckDbl(objLine.MainBalance);

            string strActionsDone = String.Empty, transactionId = String.Empty;
            string strRechargue = String.Empty; //Nunca se modifica esta variable para el sistema antiguo(SIAC - Prepago)
            if (objLine.IsTFI.Equals(Constant.Variable_SI))
                transactionId = KEY.AppSettings("gConstkeyTransaccionLLamadaEntranteTFIInfo");
            else
                transactionId = KEY.AppSettings("gConstkeyTransaccionLLamadaEntrantePrepInfo");

            string strTypeCode = String.Empty;
            if (objLine.IsTFI.Equals(Constant.Letter_T))
                strTypeCode = KEY.AppSettings("strTipoTelefonoTFI");
            else
                strTypeCode = KEY.AppSettings("strTipoTelefonoPrepago"); 
            HelperCommon.Typification typification = commonController.LoadTypification(objModel.IdSession, transactionId, strTypeCode);
            if (String.IsNullOrEmpty(typification.Type))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Message_TypificationNotRecognized;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            } 

            strActionsDone = GetActionsDone(flagGenerateOCC);  //hidAccionesRealizadas

            string idInteraction = "";//Revisar - No se llena hasta despues de insertar interaccion
            bool result = RegisterTransaction(ref objModel, client, transactionId, idInteraction,
             idCACDAC, amount, typification,
             strNotes, flagGenerateOCC, strActionsDone, strRechargue, objLine.IsTFI);
            RegisterAudit(objModel, Constant.Action_Save, amount.ToString(), objLine.IsTFI);

            //if ((mainBalance < amount) && strActionsDone.Equals(Constant.strUno) && (!String.IsNullOrEmpty(objModel.HidInteraction) ))
            //{
            //    UpdateNotes(audit, objModel.HidInteraction, Constant.Notes_IncomingCallsPrepaid.Message_InsufficientBalance, Constant.Letter_F);
            //}

            if (result) { 
                if (flagGenerateOCC.Equals(Constant.Letter_T)) objModel.HidFlagCharge = "T";
                else objModel.HidFlagCharge = "F"; 

                Session["ModelICD_Prepaid"] = objModel;

                objModel.MainBalance = mainBalance;
                objModel.Amount = amount;
                 
                GenerateContancy(ref objModel, client, nameCACDAC, strNotes);

                objModel.StatusCode = Constant.StatusCode_OK;  
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string idSession, string idContact, string strPhone, string flagLoadDataline,string flagPlatform ,string strNotes, string flagGenerateOCC,
            string strStartDate, string strEndDate)
        {
            Model.IncomingCallsDetail objModel = new Model.IncomingCallsDetail();
            objModel.IdSession = idSession;
            objModel.NumberPhone = commonController.GetNumber(idSession, false, strPhone);
            objModel.StrStartDate = strStartDate;
            objModel.StrEndDate = strEndDate;
            objModel.FlagPlatform = flagPlatform;
            
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);

            CommonService.ParameterDataResponseCommon objResponse = GetParameterDataResponse(KEY.AppSettings(Constant.Notes_IncomingCallsPrepaid.Key_AmountOCC), audit);
            double amount = objResponse.Parameter.Value_N;

            HelperController.Line objLine = LoadDataLinePrepaid(objModel, Constant.strUno);
            double mainBalance =Functions.CheckDbl(objLine.MainBalance);
 
            string transactionId = String.Empty;
            if (objLine.IsTFI.Equals(Constant.Variable_SI))
                transactionId = KEY.AppSettings("gConstkeyTransaccionLLamadaEntranteTFIInfo");
            else
                transactionId = KEY.AppSettings("gConstkeyTransaccionLLamadaEntrantePrepInfo");

            string interactionId = String.Empty, flagInsertion = String.Empty, msgText = String.Empty;
             
            string strTypeCode = String.Empty;
            if (objLine.IsTFI.Equals(Constant.Letter_T))
                strTypeCode = KEY.AppSettings("strTipoTelefonoTFI");
            else
                strTypeCode = KEY.AppSettings("strTipoTelefonoPrepago");
            HelperCommon.Typification typification = commonController.LoadTypification(objModel.IdSession, transactionId, strTypeCode);
            if (String.IsNullOrEmpty(typification.Type))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = Constant.Message_TypificationNotRecognized;
                return Json(objModel, JsonRequestBehavior.AllowGet);
            } 

            string strActionsDone = GetActionsDone(flagGenerateOCC);
            string strRechargue = String.Empty; //Nunca se modifica esta variable para el sistema antiguo(SIAC - Prepago)
            string strFinalNotes = String.Format(Constant.Notes_IncomingCallsPrepaid.NotesInteractionData, strNotes, objModel.StrStartDate, objModel.StrEndDate, strActionsDone, strRechargue);
            CommonService.Iteraction interaction = commonController.InteractionData(objModel.IdSession, idContact, objModel.NumberPhone, strFinalNotes, typification.Type, typification.Class, typification.SubClass, objLine.IsTFI);
            commonController.InsertBusinessInteraction2(interaction, objModel.IdSession, out interactionId, out flagInsertion, out msgText);
            transactionId = interactionId; //hidTransaccion.Value = strCodigoRetornoTransaccion
            if ((!String.IsNullOrEmpty(flagInsertion)) && !flagInsertion.Equals(Constant.Message_OK))
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_ErrorCreatingInteraction, msgText);
                return Json(objModel, JsonRequestBehavior.AllowGet);
            }
            List<PrepaidService.IncomingCallDetail> list = CreateListIncommingCall(ref  objModel);
            objModel.ListCallsDetail = MappingListCallDetail(list);

            string paramIN = String.Format(Constant.Notes_IncomingCallsPrepaid.ParametersIN_LogTrx, objModel.NumberPhone, objModel.StrStartDate, objModel.StrEndDate);
            string paramOUT = list.Count().ToString();

            commonController.RegisterLogTrx(objModel.IdSession, objModel.NumberPhone, Constant.strCero, Constant.strCero, paramIN, paramOUT,
                ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact"), Constant.Action_Search,
                ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamNoFact"), Constant.Notes_IncomingCallsPrepaid.NameModule);
            RegisterAudit(objModel, Constant.Action_Search, amount.ToString(), objLine.IsTFI); 


            Session["ModelICD_Prepaid"] = objModel;
            objModel.StatusCode = Constant.StatusCode_OK;
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
         
        #region Funciones Privadas

        /// <summary>
        /// ListarObjetos - Cumplimiento de Estandares
        /// </summary>
        /// <returns></returns>
        private string ConvertToObjectStandart(List<string> listDescription)
        {
            StringBuilder sbStandart = new StringBuilder();
            if (listDescription.Count > 0)
            {
                for (int i = 0; i < listDescription.Count; i++)
                {
                    if (sbStandart.Length == 0)
                        sbStandart.Append(listDescription[i]);
                    else
                        sbStandart.Append("," + listDescription[i]);
                }
            }
            return sbStandart.ToString();
        }

        private List<PrepaidService.IncomingCallDetail> CreateListIncommingCall(ref Model.IncomingCallsDetail objModel)
        {
            PrepaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(objModel.IdSession);

            string strStartDateFormatedd = objModel.StrStartDate.Substring(6, 4) + objModel.StrStartDate.Substring(3, 2) + objModel.StrStartDate.Substring(0, 2);
            string strEndDateFormatedd = objModel.StrEndDate.Substring(6, 4) + objModel.StrEndDate.Substring(3, 2) + objModel.StrEndDate.Substring(0, 2);
            if (!objModel.NumberPhone.Substring(0, 2).Equals(Constant.strCincuentaYUno)) objModel.NumberPhone = Constant.strCincuentaYUno + objModel.NumberPhone;
            objModel.RandomString = Functions.CadenaAleatoria();

            PrepaidService.IncomingCallDetailResponsePrepaid objResp = GetIncommingCallDetailResponse(objModel.NumberPhone, objModel.StrStartDate, objModel.StrEndDate, audit);

            return objResp.ListIncomingCallDetail.ToList();
        }

        private void EnablePermissions(ref Model.IncomingCallsDetail objModel, string strPermissions)
        {
            string strKeyExport = ConfigurationManager.AppSettings("gConstkeyExportarLLamadaEntranteAutorizada");
            string strKeyPrint = ConfigurationManager.AppSettings("gConstkeyImprimirLLamadaEntranteAutorizada");

            if (strPermissions.IndexOf(strKeyExport) >= 0)
                objModel.FlagAuthorization_Export = Constant.Letter_T;
            else
                objModel.FlagAuthorization_Export = Constant.Letter_F;

            if (strPermissions.IndexOf(strKeyPrint) >= 0)
                objModel.FlagAuthorization_Print = Constant.Letter_T;
            else
                objModel.FlagAuthorization_Print = Constant.Letter_F;
        }

        public bool GenerateContancy(ref Model.IncomingCallsDetail objModel,  HelperCommon.CustomerData client,
           string nameCACDAC, string strNotes)
        { 
            CommonService.ParametersGeneratePDF parameters = new CommonService.ParametersGeneratePDF()
            {
                StrCentroAtencionArea = nameCACDAC,
                StrTitularCliente = client.FirstName + ' ' + client.LastName,
                StrRepresLegal = client.LegalRepresentative,
                StrTipoDocIdentidad = client.DocumentType,
                StrNroDocIdentidad = client.NumberDocument,
                StrFechaTransaccionProgram = DateTime.Now.ToShortDateString(),
                StrCasoInter = objModel.HidInteraction,
                StrFecInicialReporte = objModel.StrStartDate,
                StrFecFinalReporte = objModel.StrEndDate,
                StrNroServicio = objModel.NumberPhone,
                StrMontoOCC = objModel.Amount.ToString("#.##"),
                strEnvioCorreo = Constant.Variable_NO,
                StrEmail = String.Empty,
                StrNotas = strNotes,
                StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
                    KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
                StrContenidoComercial2 = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                    KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
                StrCarpetaTransaccion = Constant.StrTipoLinea_PREPAGO + "\\" + ConfigurationManager.AppSettings("strDetalleLlamadasEntrantesTransac").ToString()+"\\",
                StrTipoTransaccion = "IncomingCallPrepaid",
                StrNombreArchivoTransaccion = KEY.AppSettings("strDetalleLlamadasEntrantesTransac"),
            };
            CommonService.GenerateConstancyResponseCommon response = commonController.GenerateContancyPDF(objModel.IdSession, parameters);

            if (!response.Generated)
            {
                objModel.StatusCode = Constant.StatusCode_Error;
                objModel.StatusMessage = String.Format(Constant.Message_ErrorGeneral, response.ErrorMessage);
                return false;
            }  
            objModel.FullPathPDF = response.FullPathPDF; 
            return true;
        }

        private string GetActionsDone(string flagGenerateOCC)
        {
            string actions = String.Empty;
            if (flagGenerateOCC.Equals(Constant.Letter_T))
                actions = Constant.strUno;
            else
                actions = String.Empty;
            return actions;
        }
        private void LoadData(ref Model.IncomingCallsDetail objModel, string clientFullName, string flagGenerateOCC, string flagLoadDataline)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);

            objModel.StrMinimumDate = DateTime.Now.AddMonths(-3).ToString(Constant.DateFormat_DateESP);
            objModel.StrMaximumDate = DateTime.Now.ToString(Constant.DateFormat_DateESP);
            objModel.StrStartDate = DateTime.Now.AddMonths(-1).ToString(Constant.DateFormat_DateESP);
            objModel.StrEndDate = DateTime.Now.ToString(Constant.DateFormat_DateESP);

            //CommonService.ParameterDataResponseCommon objResponse = GetParameterDataResponse(KEY.AppSettings(Constant.Notes_IncomingCallsPrepaid.Key_AmountOCC), audit);
            //double amount = objResponse.Parameter.Value_N;

            HelperController.Line objLinea = LoadDataLinePrepaid(objModel, flagLoadDataline);
            //double mainBalance = double.Parse(objLinea.MainBalance);

            objModel.FlagLoadDataLine = objLinea.FlagLoadDataLine; 
        }
         
        private HelperController.Line LoadDataLinePrepaid(Model.IncomingCallsDetail objModel, string flagLoadDataline)
        {
            HelperController.Line dataLine = null;

            if (flagLoadDataline.Equals(Constant.strUno))
            {
                if (Session["DataLinePrepaid"] != null)
                {
                    dataLine = (HelperController.Line)Session["DataLinePrepaid"];
                    if (dataLine.FlagLoadDataLine.Equals(Constant.strUno))
                        return dataLine;
                }
            }
            dataLine = new HelperController.Line();

            //Inicio Temporal 
            //dataLine.IsTFI = "No";
            //dataLine.MainBalance = "4.5";
            //dataLine.FlagLoadDataLine = "1";
            //Session["DataLinePrepaid"] = dataLine;
            //return dataLine;
            //Fin Temporal

            PrepaidService.ConsultLine info = new PrepaidService.ConsultLine();
            info.IdTransaction = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetTransactionID();
            info.Msisdn = objModel.NumberPhone;
            info.Traces = KEY.AppSettings("gTraceWSConsultationPrepaid");
            info.User = KEY.AppSettings("gUserSystemWSConsultationPrepaid");
            info.Application = KEY.AppSettings("gUserApplicationWSConsultationPrepaid");
            info.Password = KEY.AppSettings("gPasswordApplicationWSConsultationPrepaid");
            info.DateTransaction = DateTime.Now.ToString(Constant.DateFormat_DateENU2);

            PrepaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(objModel.IdSession);
            var objResponse = GetLineDataResponse(info, audit);

            if (String.IsNullOrEmpty(objResponse.Code))
            {
                //dataLine = MappingEntityLine(objResponse.LineItem);
                dataLine.NumberFailedRecharges =Functions.CheckInt (objResponse.LineItem.TriosChanguesFree);
                dataLine.TriosChanguesFree = objResponse.LineItem.TriosChanguesFree;
                dataLine.NumberTariffChangesMade = Functions.CheckInt(objResponse.LineItem.TariffChangesFree);
                dataLine.IdTriacionType = Functions.CheckInt(objResponse.LineItem.TriacionType);
                if (!String.IsNullOrEmpty(objResponse.LineItem.ActivationDate))
                {
                    DateTime dActivationDate;
                    if (DateTime.TryParse(objResponse.LineItem.ActivationDate, out dActivationDate))
                    {
                        dataLine.StrActivationDate = dActivationDate.ToString(Constant.DateFormat_DatetimeESP);
                    }
                }

                dataLine.StrNumIMSI = String.Empty;
                dataLine.StrNumICCID = String.Empty;
                dataLine.StatusIMSI = objResponse.LineItem.StatusIMSI;
                string statusLine = commonController.GetValueFromListValues(objModel.IdSession, objResponse.LineItem.LineStatus, Constant.NameListStateLine);
                dataLine.LineStatus = statusLine;
                dataLine.ProviderIdPlan = objResponse.LineItem.ProviderID;
                dataLine.TariffPlan = objResponse.LineItem.TariffPlan;
                dataLine.MainBalance = objResponse.LineItem.MainBalance;
                dataLine.MinutesBalance = objResponse.LineItem.MinuteBalance_Select;
                dataLine.ExpDate_Select = objResponse.LineItem.ExpDate_Line;
                dataLine.IsSelect = objResponse.LineItem.IsSelect;

                if (String.IsNullOrEmpty(objResponse.LineItem.SubscriberStatus))
                    dataLine.SubscriberStatus = Constant.Notes_IncomingCallsPrepaid.SubscriberStatusDefault;
                else
                    dataLine.SubscriberStatus = objResponse.LineItem.SubscriberStatus;

                dataLine.ListTrio = MappingListTrio(objResponse.ListTrio.ToList()); 
                dataLine.StandartGroupsFrequentNumbers = ConvertToObjectStandart(
                    objResponse.ListTrio.Where(x => x.Description.Length > 0).Select(x => x.Description).ToList());

                if (dataLine.IdTriacionType == 4)
                {
                    string flagSuscriberStatusOp4 = dataLine.SubscriberStatus.Substring(0, 1);
                    if ((!flagSuscriberStatusOp4.Equals(Constant.strCero)) &&
                        ((!String.IsNullOrEmpty(objResponse.LineItem.CNTNumber)) && (!objResponse.LineItem.CNTNumber.Equals(Constant.strCero))))
                    {
                        HelperController.Item entity = new HelperController.Item();
                        entity.Code = Constant.Notes_IncomingCallsPrepaid.Code_SMSIlimitado;
                        entity.Description = objResponse.LineItem.CNTNumber;
                        dataLine.ListTrio.Add(entity);
                        List<HelperController.Item> listNumberSMS = new List<HelperController.Item>();
                        listNumberSMS.Add(entity);
                        dataLine.StandartGroupsSMSNumbers = ConvertToObjectStandart(
                            listNumberSMS.Where(x => x.Description.Length > 0).Select(x => x.Description).ToList());
                    }
                }
                List<HelperController.Item> listTriadoAccounts = null;
                //PrepaidService.DataBagResponsePrepaid objDataBag = GetDataBagResponse(info.Msisdn, info.IdTransaction, KEY.AppSettings("strAPPWSDatosPrePost"), KEY.AppSettings("strUsrWSDatosPrePost"), audit);
                PrepaidService.DataBagResponsePrepaid objDataBag = new PrepaidService.DataBagResponsePrepaid(); //Temporal

                if (objDataBag != null && objDataBag.ListDataBag != null)
                {
                    listTriadoAccounts = new List<HelperController.Item>();
                    List<PrepaidService.Account> listTemp = objDataBag.ListDataBag.Where(x => (!String.IsNullOrEmpty(x.Name)) && !String.IsNullOrEmpty(x.Order)).ToList();
                    listTriadoAccounts = MappingListAccount(listTemp.ToList());
                    listTriadoAccounts = listTriadoAccounts.OrderBy(x => Int32.Parse(x.Order)).ToList();
                }

                dataLine.ListTriadoAccounts = listTriadoAccounts;
                dataLine.StrMainBalance = objDataBag.StrBalance;
                dataLine.StrMainDate = objDataBag.StrDate;

                dataLine.IsTFI = (dataLine.IsTFI == null) ? Constant.Variable_NO : objResponse.LineItem.IsTFI;
                if (objResponse.ListAccount.Count >= 8)
                {
                    dataLine.BalanceVoice1Promo = MappingItemAccount(objResponse.ListAccount[1]);
                    dataLine.BalanceVoice2Promo = MappingItemAccount(objResponse.ListAccount[2]);
                    dataLine.BalanceBonusPromo = MappingItemAccount(objResponse.ListAccount[5]);
                    dataLine.BalanceVoiceLoyalty = MappingItemAccount(objResponse.ListAccount[7]);
                    dataLine.BalanceBonus1Promo = MappingItemAccount(objResponse.ListAccount[8]);
                     
                    if (!objResponse.LineItem.IsTFI.Equals(Constant.Variable_SI))
                    {
                        dataLine.BalanceBonus2Promo = MappingItemAccount(objResponse.ListAccount[9]);
                    }
                }
                dataLine.FlagLoadDataLine = Constant.strUno;
                Session["DataLinePrepaid"] = dataLine;
            }
            else
            {
                dataLine.FlagLoadDataLine = Constant.strDos;
                Session["DataLinePrepaid"] = null;
            }

            return dataLine;
        }

        private void LoadReport(ref Model.IncomingCallsDetail objModel,string nameAction)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objModel.IdSession);
            CommonService.ParameterDataResponseCommon objResponse = GetParameterDataResponse(KEY.AppSettings(Constant.Notes_IncomingCallsPrepaid.Key_AmountOCC), audit);
           
            double amount = objResponse.Parameter.Value_N;

            HelperController.Line objLine = LoadDataLinePrepaid(objModel, Constant.strUno);
            double mainBalance = Functions.CheckDbl(objLine.MainBalance);

            objModel.StatusCode = Constant.StatusCode_OK;
            if (objModel.HidFlagCharge.Equals(Constant.Letter_T))
            {
                string bag = KEY.AppSettings("strOnPeakDetLlamEnt");
                string day = KEY.AppSettings("strDiasDetLlamEnt");
                string action = KEY.AppSettings("strAccionDetLlamEnt");
                string ticket = KEY.AppSettings("strTicketDetLlamEnt"); 
                //double factor = 0;
                //List<ItemGeneric> listItems = Functions.GetListValuesXML(Constant.NameListbags, Constant.strDos, Constant.SiacutDataPrepaidXML);
                //for (int i = 0; i < listItems.Count; i++)
                //{
                //    if (listItems[i].Code.Equals(bag))
                //    {
                //        factor = Functions.CheckDbl(listItems[i].Code2);
                //        break;
                //    }
                //}
                //if (factor == 0) factor = 1;
                //amount = factor * amount;
                string msgText = "0"; //Funcion en Mantenimiento
                if (msgText.Equals(Constant.strCero))
                {
                    objModel.HidFlagCharge = Constant.Letter_F;  
                    objModel.AlertMessage = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_DescriptionCharge, amount);
                    string message = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_TheChargeIsCompleted, nameAction);
                    UpdateNotes(audit, objModel.HidInteraction, message, Constant.Letter_F);
                }
                else
                {
                    objModel.StatusCode = Constant.StatusCode_Error;
                    objModel.StatusMessage = String.Format(Constant.Notes_IncomingCallsPrepaid.Message_ErrorGenerateRecharge, msgText);
                    UpdateNotes(audit, objModel.HidInteraction, Constant.Notes_IncomingCallsPrepaid.Message_UnableToRecharge, Constant.Letter_F);
                }
            }
            else
            {
                amount = 0;
                objModel.AlertMessage = String.Empty;
                UpdateNotes(audit, objModel.HidInteraction, nameAction, Constant.Letter_F);
            }

            string paramIN = String.Format(Constant.Notes_IncomingCallsPrepaid.ParametersIN_LogTrx, objModel.NumberPhone, objModel.StrStartDate, objModel.StrEndDate);
            string paramOUT = objModel.ListCallsDetail.Count().ToString();

            commonController.RegisterLogTrx(objModel.IdSession, objModel.NumberPhone, Constant.strCero, Constant.strCero, paramIN, paramOUT,
                ConfigurationManager.AppSettings("gConstOpcDetalleLlamadasNoFact"), nameAction,
                ConfigurationManager.AppSettings("gConstEvtConsultaDetLlamNoFact"), Constant.Notes_IncomingCallsPrepaid.NameModule);
            RegisterAudit(objModel, nameAction, amount.ToString(), objLine.IsTFI);
        }
         
        private bool RegisterAudit(Model.IncomingCallsDetail objModel, string typeAction, string strAmount, string isTFI)
        {
            if (objModel.FlagPlatform.Equals("Pers")) return true; //Temporal

            string transaction = String.Empty;
            string strText = String.Format(Constant.DescriptionX2, objModel.StrStartDate, objModel.StrEndDate);
            if (typeAction.Equals(Constant.Action_Search))
            {
                if (isTFI.Equals(Constant.Variable_SI))
                    transaction = KEY.AppSettings("strTransLlamadaEntranteConsTFI");
                else
                    transaction = KEY.AppSettings("strTransLlamadaEntranteCons");
            }
            else if (typeAction.Equals(Constant.Action_Save))
            {
                if (isTFI.Equals(Constant.Variable_SI))
                    transaction = KEY.AppSettings("strTransLlamadaEntranteGrabTFI");
                else
                    transaction = KEY.AppSettings("strTransLlamadaEntranteGrab");
            }
            else if (typeAction.Equals(Constant.Action_Export))
            {
                if (isTFI.Equals(Constant.Variable_SI))
                    transaction = KEY.AppSettings("strTransLlamadaEntranteExcelTFI");
                else
                    transaction = KEY.AppSettings("strTransLlamadaEntranteExcel");
            }
            else if (typeAction.Equals(Constant.Action_Print))
            {
                if (isTFI.Equals(Constant.Variable_SI))
                    transaction = KEY.AppSettings("strTransLlamadaEntranteImpTFI");
                else
                    transaction = KEY.AppSettings("strTransLlamadaEntranteImp");
            }
            string strService = KEY.AppSettings("strServicioLlamadaEntrante");

            commonController.RegisterAuditGeneral(objModel.IdSession, objModel.NumberPhone,
                strAmount, strText, strService, transaction);
            return true;
        }

        private bool RegisterTransaction(ref Model.IncomingCallsDetail objModel, HelperCommon.CustomerData client, string idTransaction, string idInteraction,
            string idCACDAC, double dblMonto, HelperCommon.Typification typification,
            string strNotes, string flagGenerateOCC, string strActionsDone, string strRechargue, string isTFI)
        {
            bool executeTransaction = true;

            string strFinalNotes = String.Format(Constant.Notes_IncomingCallsPrepaid.NotesInteractionData, strNotes, objModel.StrStartDate, objModel.StrEndDate, strActionsDone, strRechargue);
            CommonService.Iteraction interaction = commonController.InteractionData(objModel.IdSession, client.IdContact, objModel.NumberPhone, strFinalNotes, typification.Type, typification.Class, typification.SubClass, isTFI);
            CommonService.InsertTemplateInteraction interactionTemplateData = commonController.InteractionTemplateData(idInteraction, idTransaction, objModel.NumberPhone, Constant.Notes_IncomingCallsPrepaid.NameModule,
                client.FirstName, client.LastName, client.LegalRepresentative, client.DocumentType, client.NumberDocument,
                objModel.StrStartDate, objModel.StrEndDate, flagGenerateOCC, dblMonto, idCACDAC);

            CommonService.InsertGeneralResponse objResponse = GetInsertBusinessInteractionResponse(objModel.IdSession, objModel.NumberPhone,
                interaction, interactionTemplateData, executeTransaction);
            objModel.HidInteraction = objResponse.rInteraccionId;

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
            return true;
        }
        #endregion

        #region Funciones WS
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
                return oServiceCommon.GetinsertInteractionGeneral(request); ;
            });

            return objResponse;
        }

        private PrepaidService.DataBagResponsePrepaid GetDataBagResponse(string telephone, string strIdTransaction,
          string strNameApplication, string strUserApplication, PrepaidService.AuditRequest audit)
        {
            PrepaidService.DataBagRequestPrepaid objRequest = new PrepaidService.DataBagRequestPrepaid()
            {
                IdTransaction = strIdTransaction,
                Telephone = telephone,
                NameApplication = strNameApplication,
                UserApplication = strUserApplication,
                audit = audit
            };
            PrepaidService.DataBagResponsePrepaid objResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.DataBagResponsePrepaid>(() =>
            {
                return oServicePrepaid.GetDataBag(objRequest);
            });
            return objResponse;
        }

        private PrepaidService.IncomingCallDetailResponsePrepaid GetIncommingCallDetailResponse(string msisdn, string strStartDate, string strEndDate, PrepaidService.AuditRequest audit)
        {
            PrepaidService.IncomingCallDetailRequestPrepaid objRequest = new PrepaidService.IncomingCallDetailRequestPrepaid()
            {
                MSISDN = msisdn,
                StrStartDate = strStartDate,
                StrEndDate = strEndDate,
                audit = audit
            };
            PrepaidService.IncomingCallDetailResponsePrepaid objResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.IncomingCallDetailResponsePrepaid>(() =>
            {
                return oServicePrepaid.GetIncomingCallDetail(objRequest);
            });
            return objResponse;
        }

        private PrepaidService.LineDataResponsePrepaid GetLineDataResponse(PrepaidService.ConsultLine info, PrepaidService.AuditRequest audit)
        {
            PrepaidService.LineDataRequestPrepaid objRequest = new PrepaidService.LineDataRequestPrepaid()
            {
                Info = info,
                audit = audit
            };
            PrepaidService.LineDataResponsePrepaid objResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.LineDataResponsePrepaid>(() =>
            {
                return oServicePrepaid.GetLineData(objRequest);
            });
            return objResponse;
        }

        private CommonService.ParameterDataResponseCommon GetParameterDataResponse(string name, CommonService.AuditRequest audit)
        {
            CommonService.ParameterDataRequestCommon objRequest = new CommonService.ParameterDataRequestCommon()
            {
                Name = name,
                audit = audit
            };
            CommonService.ParameterDataResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.ParameterDataResponseCommon>(() =>
            {
                return oServiceCommon.GetParameterData(objRequest);
            });
            return objResponse;
        }
 
        private bool UpdateNotes(CommonService.AuditRequest audit, string strObjId, string strText, string strOrder)
        {
            CommonService.UpdateNotesRequestCommon objRequest = new CommonService.UpdateNotesRequestCommon()
            {
                StrObjId = strObjId,
                StrText = strText,
                StrOrder = strOrder,
                audit = audit
            };
            CommonService.UpdateNotesResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.UpdateNotesResponseCommon>(() =>
            {
                return oServiceCommon.UpdateNotes(objRequest);
            });
            return objResponse.Flag.Equals(Constant.Message_OK);
        }
        #endregion

        #region Funciones Mapping

        private HelperController.Item MappingItemAccount(PrepaidService.Account accountDB)
        {
            HelperController.Item entity = null;
            entity = new HelperController.Item()
            {
                Name = accountDB.Name,
                Balance = accountDB.Balance,
                ExpirationDate = accountDB.ExpirationDate,
                Order = accountDB.Order
            };
            return entity;
        }

        //private HelperController.Typification MappingTypification(CommonService.Typification typifDB)
        //{
        //    HelperController.Typification entity = null;
        //    entity = new HelperController.Typification()
        //    {
        //        IdClass = typifDB.CLASE_CODE,
        //        Class = typifDB.CLASE,
        //        IdSubClass = typifDB.SUBCLASE_CODE,
        //        SubClass = typifDB.SUBCLASE,
        //        Type = typifDB.TIPO
        //    };
        //    return entity;
        //}

        private List<HelperController.Item> MappingListAccount(List<PrepaidService.Account> listAccountDB)
        {
            List<HelperController.Item> list = new List<HelperController.Item>();
            HelperController.Item entity = null;
            foreach (var item in listAccountDB)
            {
                entity = new HelperController.Item()
                {
                    Name = item.Name,
                    Balance = item.Balance,
                    ExpirationDate = item.ExpirationDate,
                    Order = item.Order
                };
                list.Add(entity);
            }
            return list;
        }
        private List<HelperController.CallDetail> MappingListCallDetail(List<PrepaidService.IncomingCallDetail> listDB)
        {
            List<HelperController.CallDetail> list = new List<HelperController.CallDetail>();
            HelperController.CallDetail entity = null;
            foreach (var item in listDB)
            {
                entity = new HelperController.CallDetail()
                {
                    NroOrd = item.NroOrd,
                    Date = item.Date,
                    Duration = item.Duration,
                    NumberA = item.NumberA,
                    NumberB = item.NumberB,
                    StartHour = item.StartHour
                };
                list.Add(entity);
            }
            return list;
        }
        private List<HelperController.Item> MappingListTrio(List<PrepaidService.ListItem> listTrioDB)
        {
            List<HelperController.Item> list = new List<HelperController.Item>();
            HelperController.Item entity = null;
            foreach (var item in listTrioDB)
            {
                entity = new HelperController.Item()
                {
                    Code = item.Code,
                    Description = item.Description
                };
                list.Add(entity);
            }
            return list;
        }
        #endregion
    }
}