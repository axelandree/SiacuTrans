using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using System.Web.Mvc;
using Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Microsoft.VisualBasic;
using AuditRequest = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using ContractByPhoneNumber = Claro.SIACU.Entity.Transac.Service.Common.ContractByPhoneNumber;
using CONSTANTADDITIONALSERVICEPOSTPAID = Claro.SIACU.Transac.Service.Constants.ADDITIONALSERVICESPOSTPAID;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.Constants;
using User = Claro.SIACU.Entity.Transac.Service.Common.User;
using UserExistsBSCSRequest = Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS.UserExistsBSCSRequest;
using UserExistsBSCSResponse = Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS.UserExistsBSCSResponse;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class AdditionalServicesController : Controller
    {
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacService.CommonTransacServiceClient();
        private readonly PostTransacService.PostTransacServiceClient oServicePostpaid = new PostTransacService.PostTransacServiceClient();
        private static string strCodSubClass = "";
        private static readonly string strFlagContingencyHP = KEY.AppSettings("strFlagContingenciaHP");
        private static List<ServiceByContract> gListServiceByContractNotRepeat = null;
        private static List<ServiceByContract> gListServiceByContract = null;  


        private static string gNumberPhone { get; set; }

        public ActionResult PostPaidAdditionalServices()
        {
            return View("~/Areas/Transactions/Views/AdditionalServices/PostpaidAdditionalServices.cshtml");
        }
        [HttpPost]
        public JsonResult Page_Load(AdditionalServicesModel objAdditionalServices)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: Page_Load ", "Entra a Page_Load");

            if (objAdditionalServices.Transaction == CONSTANTADDITIONALSERVICEPOSTPAID.strTransactionACTDESSER)
            {
                objAdditionalServices.HidTransactionDTH = CONSTANTADDITIONALSERVICEPOSTPAID.strTransactionACTDESSER;
                objAdditionalServices.EnableTelephonyDTH = "Telef";
            }
            else
            {
                objAdditionalServices.HidTransactionDTH = CONSTANTADDITIONALSERVICEPOSTPAID.gstrTransactionDTHTACTDESSER;
                objAdditionalServices.EnableTelephonyDTH = "DTH";
            }

            Start(objAdditionalServices);
            GetTotalFixedCharge(objAdditionalServices, audit);
            //hidAcceso.Value = SIACPO_Session.AccesoPagina() se vaida en javascript ya que no tiene uso en el controlador

            objAdditionalServices.HidCodServ4G = KEY.AppSettings("strConstCodServ4G");
            objAdditionalServices.HidCodOptAuthorized = KEY.AppSettings("strConstkeyAct4G");
            objAdditionalServices.HidAccessWC = KEY.AppSettings("gConstAccesoProgFech");
            objAdditionalServices.HidAccessMCP = KEY.AppSettings("gConstkeyModCuoPer");
          
            if (!string.IsNullOrEmpty(objAdditionalServices.UserLogin))
            {
                ValidateUserBSCS(objAdditionalServices, audit);
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: Page_Load ", "sale de Page_Load");

            return Json(objAdditionalServices, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PostBack(AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: PostBack ", "entra a PostBack");

            //fin de datos de prueba
            if (Request.IsAjaxRequest()) {
                if (objAdditionalServices.HidEstGraMCP == "GCP")
                {
                    SaveInteractionMCP(objAdditionalServices);
                }
                else
                {
                    ProgrammingRoamming(objAdditionalServices);
                }
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: PostBack ", "sale de PostBack");

            return Json(objAdditionalServices, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProgramming(Models.Postpaid.AdditionalServicesModel oModel) {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: SaveProgramming ", "entra a SaveProgramming");

            if (Request.IsAjaxRequest()) {
                RecordProgramming(oModel);
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: SaveProgramming ", "sale de SaveProgramming");

            return Json(oModel);
        }
        [HttpPost]
        public JsonResult ProgActDesact(AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ProgActDesact ", "entra a ProgActDesact");

            if (Request.IsAjaxRequest())
            {
                ValidateProgDesact(objAdditionalServices);
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ProgActDesact ", "sale de ProgActDesact");

            return Json(objAdditionalServices,JsonRequestBehavior.AllowGet);
        }

        private void ValidateProgDesact(AdditionalServicesModel model)
        {
            Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: ValidateProgDesact ", "entra a ValidateProgDesact");

            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(model.IdSession);

            string strIdTransaction = string.Empty;
            string strIpAplication = string.Empty;
            string strApplication = string.Empty;
            string strMsgExist = string.Empty;
            string strCodActDesactService = string.Empty;
            string strResult = string.Empty;
            string strCodError = string.Empty;
            string strDesError = string.Empty;

            try
            {
                strIdTransaction = audit.transaction;
                strIpAplication = Functions.CheckStr(App_Code.Common.GetApplicationIp());
                strApplication = KEY.AppSettings("aplicacionVIP");
                if (model.StrStatus == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService)
                {
                    strMsgExist = KEY.AppSettings("strMsjExisteProgRoammingAct");
                }
                else if (model.StrStatus == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService)
                {
                    strMsgExist = KEY.AppSettings("strMsjExisteProgRoammingDesact");
                }
                else if (model.StrStatus == CONSTANTADDITIONALSERVICEPOSTPAID.gstrMaintainService)
                {
                    strMsgExist = KEY.AppSettings("strMsjExisteProgRoammingDesact");                    
                }


                strCodActDesactService = Functions.CheckStr(CONSTANTS.NumberFifteen);
                model.StrStatus = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;

                GetValidateActDesServProg(model, strIdTransaction, strIpAplication, strApplication, strCodActDesactService, out strCodError, out strDesError);

                if (string.Compare(strDesError.Trim(), CONSTANTADDITIONALSERVICEPOSTPAID.gstrExistProg) != CONSTANTS.NumberZero)
                {
                    strDesError = string.Empty;
                    strCodActDesactService = CONSTANTS.NumberFourteenString;
                    model.StrStatus = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                   
                    GetValidateActDesServProg(model, strIdTransaction, strIpAplication, strApplication, strCodActDesactService, out strCodError, out strDesError);

                }

                if (string.Compare(strDesError.Trim(), CONSTANTADDITIONALSERVICEPOSTPAID.gstrExistProg) == CONSTANTS.NumberZero)
                {
                    strDesError = strMsgExist;
                }

                strResult = strCodError + SIACU.Transac.Service.Constants.PresentationLayer.gstrVariablePipeline +
                            strDesError;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: ValidateProgDesact ", string.Format("Error ValidarProgramacionDesactivacion: {0}", ex.Message));
                strResult = ConstantsSiacpo.ConstMenosUno +
                            SIACU.Transac.Service.Constants.PresentationLayer.gstrVariablePipeline +
                            KEY.AppSettings("strMsjRoammingProgValidacion");
            }


            try
            {
                model.MessageCode = "OK";
                model.MessageLabel = strResult;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: ValidateProgDesact ", string.Format("Error ObtenerSOT: {0}", ex.Message));
            }
            Claro.Web.Logging.Info("Session: " + model.IdSession, "Transaction: ValidateProgDesact ", "sale de ValidateProgDesact");

        }

        private void MaintainActivation(AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: MaintainActivation ", "entra a MaintainActivation");

            AuditRequest auditC = App_Code.Common.CreateAuditRequest<AuditRequest>(objAdditionalServices.IdSession);


            string strCodRandom = KEY.AppSettings("gConstIdTransaccion") + Functions.CadenaAleatoria();
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: MaintainActivation ", string.Format("Id Transaccion: {0}", strCodRandom));
            if (objAdditionalServices.HidStateContract.Trim().ToUpper() != "ACTIVO")
            {
                objAdditionalServices.MessageCode = "A";
                objAdditionalServices.Message = "No se puede Activar un Servicio con Contrado Desactivo";
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: MaintainActivation ", string.Format("Estado Contrato : {0}", objAdditionalServices.HidStateContract));
                return;
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: MaintainActivation ", string.Format("Estado Contrato : {0}", objAdditionalServices.HidStateContract));

            if (objAdditionalServices.chkSendMail_IsCheched == "T" && objAdditionalServices.txtEmail == string.Empty)
            {
                objAdditionalServices.MessageCode = "A";
                objAdditionalServices.Message = "Ingrese Email";
                return;
            }

            if (!string.IsNullOrEmpty(objAdditionalServices.HidCodService))
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Codigo Servicio Comercial: {0}", objAdditionalServices.HidCodService));
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Nombre Servicio Comercial: {0}", objAdditionalServices.HidNameService));

                if (objAdditionalServices.HidBloqAct == "N")
                {
                    Iteraction objInteraction = DataInteraction(objAdditionalServices);
                    InsertGeneralResponse oSave = new InsertGeneralResponse();

                    bool blnExecuteTransaction = true;
                    string strUserSystem = objAdditionalServices.UserLogin;
                    string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                    string strUserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                    try
                    {
                        InsertTemplateInteraction objTemplateData = new InsertTemplateInteraction();
                        objTemplateData = DataTemplateInteraction(objAdditionalServices, SIACU.Transac.Service.Constants.strDos);

                        if (objAdditionalServices.blnValidate == false)
                        {
                            return;
                        }

                        InsertGeneralRequest objRequest = new InsertGeneralRequest()
                        {
                            Interaction = objInteraction,
                            InteractionTemplate = objTemplateData,
                            vNroTelefono = objAdditionalServices.lblPhoneNumber,
                            vUSUARIO_SISTEMA = strUserSystem,
                            vPASSWORD_USUARIO = strUserPass,
                            vUSUARIO_APLICACION = strUserApp,
                            vEjecutarTransaccion = blnExecuteTransaction,
                            audit = auditC
                        };
                        oSave = GetInsertInteractionBusiness(objRequest);
                        objAdditionalServices.HidCaseId = oSave.rInteraccionId;

                        if (oSave.rResult)
                        {
                            if (strFlagContingencyHP == CONSTANTS.NumberOneString)
                            {
                                bool blnSuccess = false;
                                blnSuccess = GeneratePDF(objTemplateData, objAdditionalServices);
                                if (!blnSuccess)
                                {
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                                }
                            }
                        }

                        if (objAdditionalServices.chkSendMail_IsCheched == "T")
                        {
                            SendEmail(objAdditionalServices, auditC);
                        }

                        string strCodEvent = KEY.AppSettings("gActDesactServiciosComerciales");
                        string strText = string.Format("Codigo Contrato: {0} /MSISDN: {1} /Codigo Servicio Comercial: {2} /Nombre Servicio Comercial: {3} /Accion: Mantener Activacion /CAC o DAC: {4}", objAdditionalServices.HidContract, objInteraction.TELEFONO, objAdditionalServices.HidCodService, objAdditionalServices.HidNameService, objAdditionalServices.cboCACDACValue);
                        InsertAudit(objAdditionalServices, strCodEvent, strText);


                        if ((oSave.rFlagInsercion != Constants.OK) & oSave.rFlagInsercion != string.Empty)
                        {
                            //limpiar() esta validacion se hara en el JS
                            objAdditionalServices.MessageCode = "A";
                            objAdditionalServices.Message = "No se pudo generar la interacción";
                            return;
                        }
                        else
                        {
                            //limpiar() esta validacion se hara en el JS
                            objAdditionalServices.MessageCode = "A";
                            objAdditionalServices.Message = KEY.AppSettings("strMsjRoammingProgMantenida");
                        }

                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Error MantenerActivacion(): {0}", ex.Message));
                    }

                }
                else
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "No se puede Mantener Activo un servicio No vigente";
                }
            }
            else
            {
                objAdditionalServices.MessageCode = "A";
                objAdditionalServices.Message = "Seleccione un servicio";
            }


            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: MaintainActivation ", "sale de MaintainActivation");

        }
        private void ProgrammingRoamming(AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ProgrammingRoamming ", "entra a ProgrammingRoamming");

            try
            {
                if (objAdditionalServices.HidProgramingRoamming.Trim() != string.Empty)
                {
                    if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveDetermined)
                    {
                        RecordProgramming(objAdditionalServices, CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveDetermined);
                    }
                    else if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDesactiveDetermined)
                    {
                        RecordProgramming(objAdditionalServices, CONSTANTADDITIONALSERVICEPOSTPAID.gstrDesactiveDetermined);
                    }
                    else if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveIndetermined)
                    {
                        RecordProgramming(objAdditionalServices, CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveIndetermined);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ProgrammingRoamming ", string.Format("Error: {0}", ex.Message));                
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ProgrammingRoamming ", "sale de ProgrammingRoamming");

        }

        private void RecordProgramming(AdditionalServicesModel objAdditionalServices, string strCombination = "")
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: RecordProgramming ", "entra a RecordProgramming");

            strCombination = string.Empty;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);
            
            try
            {
                Iteraction objInteraction = DataInteraction(objAdditionalServices);
                bool blnExecuteTransaction = true;
                string strDateActivation = objAdditionalServices.txtDateAct;
                string strDateDeactivation = objAdditionalServices.txtDateDeact;
                string strUserSystem = objAdditionalServices.UserLogin;
                string strUserProcess = KEY.AppSettings("USRProcesoSU");
                string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                string strUserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
                InsertTemplateInteraction objTemplateData = new InsertTemplateInteraction();
                objTemplateData = DataTemplateInteraction(objAdditionalServices);
                if (objAdditionalServices.blnValidate == false)
                {
                    return;
                }
                InsertGeneralRequest objRequest = new InsertGeneralRequest()
                {
                    Interaction = objInteraction,
                    InteractionTemplate = objTemplateData,
                    vNroTelefono = objAdditionalServices.lblPhoneNumber,
                    vUSUARIO_SISTEMA = strUserSystem,
                    vPASSWORD_USUARIO = strUserPass,
                    vUSUARIO_APLICACION = strUserApp,
                    vEjecutarTransaccion = blnExecuteTransaction,
                    audit = audit
                };
                InsertGeneralResponse oSave = GetInsertInteractionBusiness(objRequest);
                objAdditionalServices.HidCaseId = oSave.rInteraccionId;
                if (oSave.rResult)
                {
                    if (strFlagContingencyHP == SIACU.Transac.Service.Constants.strUno)
                    {
                        bool blnSuccess = false;

                        blnSuccess = GeneratePDF(objTemplateData, objAdditionalServices);
                        if (!blnSuccess)
                        {
                            objAdditionalServices.MessageCode = "A";
                            objAdditionalServices.Message =
                                "Ocurrió un error al tratar de generar la constancia en formato PDF";
                        }
                        
                    }
                }

                if (objAdditionalServices.chkSendMail_IsCheched == "T")
                {
                    SendEmail(objAdditionalServices, audit);
                }

                if (!string.IsNullOrEmpty(oSave.rInteraccionId))
                {
                    if (!string.IsNullOrEmpty(strCombination))
                    {
                        switch (strCombination)
                        {
                            case "AD":
                                objAdditionalServices.HidStatePrograming =
                                    CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                                if (Functions.CheckDate(strDateDeactivation).ToShortDateString() == DateTime.Now.ToShortDateString())
                                {
                                    TurnOff(objAdditionalServices, true);
                                }
                                else
                                {
                                    if (ActiveDesactiveProgramming(objAdditionalServices, strDateDeactivation, CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService, strUserProcess) == false)
                                    {
                                        break;
                                    }
                                }
                                break;
                            case "DD":
                                objAdditionalServices.HidStatePrograming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                                if (Functions.CheckDate(strDateActivation).ToShortDateString() == DateTime.Now.ToShortDateString())
                                {
                                    TurnOn(objAdditionalServices, true);
                                }
                                else
                                {
                                    if (ActiveDesactiveProgramming(objAdditionalServices, strDateActivation, CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService, strUserProcess) == false)
                                    {
                                        break;
                                    }
                                }

                                objAdditionalServices.HidStatePrograming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                                if (Functions.CheckDate(strDateDeactivation).ToShortDateString() == DateTime.Now.ToShortDateString())
                                {
                                    TurnOff(objAdditionalServices, true);
                                }
                                else
                                {
                                    if (ActiveDesactiveProgramming(objAdditionalServices, strDateDeactivation, CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService, strUserProcess) == false)
                                    {
                                        break;
                                    }
                                }
                                break;
                            case "AI":
                                objAdditionalServices.HidStatePrograming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                                if (Functions.CheckDate(strDateActivation).ToShortDateString() == DateTime.Now.ToShortDateString())
                                {
                                    TurnOn(objAdditionalServices, true);
                                }
                                else
                                {
                                    if (ActiveDesactiveProgramming(objAdditionalServices, strDateActivation, CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService, strUserProcess) == false)
                                    {
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        ActiveDesactiveProgramming(objAdditionalServices, string.Empty, string.Empty, strUserProcess);
                    }
                }
                else
                {
                    objAdditionalServices.MessageCode = "E_A";
                    objAdditionalServices.MessageLabel = oSave.rMsgTextTransaccion;
                    objAdditionalServices.Message =
                        "No se pudo generar tipificacion ni programación debido a que es posible que el número no se encuentra registrado en Clarify";
                    return;
                }

                string strCodEvent = KEY.AppSettings("gActDesactServiciosComerciales");
                if (objAdditionalServices.HidStatePrograming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService)
                {
                    string strText =
                        string.Format(
                            "Codigo Contrato: {0}/MSISDN: {1}/Codigo Servicio Comercial: {2}/Nombre Servicio Comercial: {3}/Accion: ProgActiv/CAC o DAC: {4}",
                            objAdditionalServices.HidContract, objInteraction.TELEFONO,
                            objAdditionalServices.HidCodService, objAdditionalServices.HidNameService,
                            objAdditionalServices.cboCACDACValue);
                    InsertAudit(objAdditionalServices, strCodEvent, strText);
                }
                else if (objAdditionalServices.HidStatePrograming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService)
                {
                    string strText =
                        string.Format(
                            "Codigo Contrato: {0}/MSISDN: {1}/Codigo Servicio Comercial: {2}/Nombre Servicio Comercial: {3}/Accion: ProgDesact/CAC o DAC: {4}",
                            objAdditionalServices.HidContract, objInteraction.TELEFONO,
                            objAdditionalServices.HidCodService, objAdditionalServices.HidNameService,
                            objAdditionalServices.cboCACDACValue);
                    InsertAudit(objAdditionalServices, strCodEvent, strText);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: RecordProgramming ", string.Format("Error GrabarProgramacion(): {0}", ex.Message));
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: RecordProgramming ", "sale de RecordProgramming");

        }

        [HttpPost]
        public JsonResult Active(AdditionalServicesModel oModel) {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Active ", "entra a Active");

            if (Request.IsAjaxRequest()) {

                oModel.TxtNote = HttpContext.Server.HtmlEncode(oModel.TxtNote);

                TurnOn(oModel);
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Active ", "sale de Active");

            return Json(oModel);
        }
        [HttpPost]
        public JsonResult Desactive(AdditionalServicesModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Desactive ", "entra a Desactive");

            if (Request.IsAjaxRequest())
            {
                oModel.TxtNote = HttpContext.Server.HtmlEncode(oModel.TxtNote);
                TurnOff(oModel);
            }
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Desactive ", "sale de Desactive");

            return Json(oModel);
        }
        [HttpPost]
        public JsonResult ProgRoamming(AdditionalServicesModel oModel)
        {
            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: ProgRoamming ", "entra a ProgRoamming");

            if (Request.IsAjaxRequest()) {
                oModel.TxtNote = HttpContext.Server.HtmlEncode(oModel.TxtNote);
                ProgrammingRoamming(oModel);
            }

            Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: ProgRoamming ", "sale de ProgRoamming");

            return Json(oModel);
        }

        private bool TurnOn(AdditionalServicesModel objAdditionalServices, bool blnNoTypi = false)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "entra a TurnOn");

            bool blnReturn = false;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);
            AuditRequest auditC = App_Code.Common.CreateAuditRequest<AuditRequest>(objAdditionalServices.IdSession);

            try
            {
                string strCodRandom = KEY.AppSettings("gConstIdTransaccion") + Functions.CadenaAleatoria();
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Id Transaccion: {0}", strCodRandom));

                if (string.IsNullOrEmpty(objAdditionalServices.HidStateContract)) {
                    objAdditionalServices.HidStateContract = string.Empty;
                }

                if (objAdditionalServices.HidStateContract.Trim().ToUpper() != "ACTIVO")
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "No se puede Activar un Servicio con Contrado Desactivo";
                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Estado Contrato : {0}", objAdditionalServices.HidStateContract));
                    return blnReturn;
                }
                
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Estado Contrato : {0}", objAdditionalServices.HidStateContract));
                if (objAdditionalServices.chkSendMail_IsCheched == "T" && objAdditionalServices.txtEmail == string.Empty)
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "Ingrese Email";
                    return blnReturn;
                }

                if (objAdditionalServices.HidState != "Activo")
                {
                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Estado Servicio Actual: Desactivo"));
                    if (!string.IsNullOrEmpty(objAdditionalServices.HidCodService))
                    {
                        Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Codigo Servicio Comercial: {0}", objAdditionalServices.HidCodService));
                        Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Nombre Servicio Comercial: {0}", objAdditionalServices.HidNameService));
                        if (objAdditionalServices.HidBloqAct == "N")
                        {
                            PostTransacService.ApprovalBusinessCreditLimitResponse objApprovalBusinessCreditLimit = new PostTransacService.ApprovalBusinessCreditLimitResponse();

                            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Servicio Vigente? : Si"));

                            objAdditionalServices.HidAction = "1";
                            //btnImprimir.Disabled = True se habilitara por medio de JS en el front
                            InsertGeneralResponse oSave = new InsertGeneralResponse();
                            Iteraction objInteraction = DataInteraction(objAdditionalServices);
                            bool blnExecuteTransaction = true;
                            string strUserSystem = objAdditionalServices.UserLogin;
                            string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                            string strUserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                            InsertTemplateInteraction objTemplateData = new InsertTemplateInteraction();
                            objTemplateData = DataTemplateInteraction(objAdditionalServices, SIACU.Transac.Service.Constants.strUno);
                            if (objAdditionalServices.blnValidate == false)
                            {
                                return blnReturn;
                            }

                            double dChargeMax = 0;
                            double dChargeNew = 0;
                            string strBusinessGroups = string.Empty;
                            string[]  arrBusinessGroups;
                            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "Inicio Validación de Limite de Credito");
                            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Entrada: Grupo de Servicio Comercial: {0},Codigo Servicio Comercial:{1},Nombre Servicio Comercial:{2}", objAdditionalServices.HidCodPackage, objAdditionalServices.HidCodService, objAdditionalServices.HidNameService));

                            strBusinessGroups =
                                KEY.AppSettings("strGruposServiciosComercialesExcepcionValidacionLimiteCredito");

                            if (strBusinessGroups.Equals(string.Empty))
                            {
                                string strAccount = objAdditionalServices.Account;
                                int intContract = Functions.CheckInt(objAdditionalServices.HidContract);
                                int intService = Functions.CheckInt(objAdditionalServices.HidCodService);
                                objApprovalBusinessCreditLimit = GetApprovalBusinessCreditLimit(objAdditionalServices, strAccount, intContract, intService, audit);

                                if (objApprovalBusinessCreditLimit.result)
                                {
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = "El CF del servicio que desea activar excede el LC asignado en la Cuenta";
                                    return blnReturn;
                                }
                            }
                            else
                            {
                                int intBusinessGroupExist = 0;
                                int intBusinessServiceExist = 0;
                                arrBusinessGroups = strBusinessGroups.Split('|');
                                for (int intGroup = 0; intGroup < arrBusinessGroups.Length - 1; intGroup++)
                                {
                                    if (arrBusinessGroups[intGroup] == string.Empty)
                                    {
                                        break;
                                    }
                                    if (Functions.CheckInt(arrBusinessGroups[intGroup]) == Functions.CheckInt(objAdditionalServices.HidCodPackage))
                                    {
                                        intBusinessGroupExist += 1;
                                    }
                                }

                                for (int intGroup = 0; intGroup < arrBusinessGroups.Length - 1; intGroup++)
                                {
                                    if (arrBusinessGroups[intGroup] == string.Empty)
                                    {
                                        break;
                                    }
                                    if (Functions.CheckInt(arrBusinessGroups[intGroup]) == Functions.CheckInt(objAdditionalServices.HidCodService))
                                    {
                                        intBusinessServiceExist += 1;
                                    }
                                }

                                if ((intBusinessGroupExist == CONSTANTS.NumberZero) & (intBusinessServiceExist == CONSTANTS.NumberZero))
                                {
                                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "Respuesta: Se realiza la validación del Limite de Crédito");
                                    string strAccount = objAdditionalServices.Account;
                                    int intContract = Functions.CheckInt(objAdditionalServices.HidContract);
                                    int intService = Functions.CheckInt(objAdditionalServices.HidCodService);
                                    objApprovalBusinessCreditLimit = GetApprovalBusinessCreditLimit(objAdditionalServices, strAccount, intContract, intService, audit);

                                    if (objApprovalBusinessCreditLimit.result)
                                    {
                                        objAdditionalServices.MessageCode = "A";
                                        objAdditionalServices.Message = "El CF del servicio que desea activar excede el LC asignado en la Cuenta";
                                        return blnReturn;
                                    }
                                }
                                else
                                {
                                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "Respuesta: Se excluye de la validación del Limite de Crédito");                                    
                                }
                            }
                            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "Fin Validación de Limite de Credito");
                            string strTypeActivation = SIACU.Transac.Service.Constants.strLetraA;
                            RegisterServiceCommercialResponse objBusiness = RegisterServiceCommercial(strUserApp, strCodRandom, strTypeActivation, objAdditionalServices, auditC);

                            if (objBusiness.StrResult == CONSTANTS.NumberZeroString)
                            {
                                if (blnNoTypi == false)
                                {

                                    InsertGeneralRequest objRequest = new InsertGeneralRequest()
                                    {
                                        Interaction = objInteraction,
                                        InteractionTemplate = objTemplateData,
                                        vNroTelefono = objAdditionalServices.lblPhoneNumber,
                                        vUSUARIO_SISTEMA = strUserSystem,
                                        vPASSWORD_USUARIO = strUserPass,
                                        vUSUARIO_APLICACION = strUserApp,
                                        vEjecutarTransaccion = blnExecuteTransaction,
                                        audit = auditC
                                    };
                                    oSave = GetInsertInteractionBusiness(objRequest);
                                    objAdditionalServices.HidCaseId = oSave.rInteraccionId;

                                    if (oSave.rResult)
                                    {
                                        if (strFlagContingencyHP == CONSTANTS.NumberOneString)
                                        {
                                            bool blnSuccess = false;
                                            blnSuccess = GeneratePDF(objTemplateData, objAdditionalServices);
                                            if (!blnSuccess)
                                            {
                                                objAdditionalServices.MessageCode = "A";
                                                objAdditionalServices.Message = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    oSave.rFlagInsercion = Constants.OK;
                                }


                                if (objAdditionalServices.chkSendMail_IsCheched == "T")
                                {
                                    SendEmail(objAdditionalServices, auditC);
                                }

                                string strCodEvent = KEY.AppSettings("gActDesactServiciosComerciales");
                                string strText = string.Format("Codigo Contrato: {0} /MSISDN: {1} /Codigo Servicio Comercial: {2} /Nombre Servicio Comercial: {3} /Accion: Activacion /CAC o DAC: {4}", objAdditionalServices.HidContract, objInteraction.TELEFONO, objAdditionalServices.HidCodService, objAdditionalServices.HidNameService, objAdditionalServices.cboCACDACValue);
                                InsertAudit(objAdditionalServices, strCodEvent, strText);

                                //limpiar() esta validacion se hara en el JS

                                if ((oSave.rFlagInsercion != Constants.OK) & oSave.rFlagInsercion != string.Empty)
                                {
                                    //limpiar() esta validacion se hara en el JS
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = "No se pudo generar la interacción";
                                    return blnReturn;
                                }
                                else
                                {
                                    if (objAdditionalServices.HidState.Trim() != string.Empty)
                                    {
                                        objAdditionalServices.HidState =
                                            CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                                    }
                                    blnReturn = true;
                                }

                                LoadServicesByContract(objAdditionalServices,audit);

                                if (objAdditionalServices.HidCodService != string.Empty && objAdditionalServices.HidCodService == KEY.AppSettings("strCodServicioRoamming"))
                                {
                                    objAdditionalServices.DisableSuccess = KEY.AppSettings("strMsjRoammingProgActivada");
                                }
                                else
                                {
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = "El servicio fue Activado correctamente";
                                }

                            }
                            else
                            {
                                if (objBusiness.StrResult == ConstantsSiacpo.ConstDieciseis)
                                {
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = KEY.AppSettings("strMsjErrorBolsas");
                                }
                                else
                                {
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = string.Format("No se puede activar el Servicio: {0} - {1}", objBusiness.StrResult, objBusiness.StrMessage);
                                }
                            }
                        }
                        else
                        {
                            objAdditionalServices.MessageCode = "A";
                            objAdditionalServices.Message = "No se puede Activar un servicio No vigente";
                        }
                    }
                    else
                    {
                        objAdditionalServices.MessageCode = "A";
                        objAdditionalServices.Message = "Seleccione un servicio";
                    }
                }
                else
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "El servicio ya se encuentra activo";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", string.Format("Error : {0}", ex.Message));
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOn ", "sale de TurnOn");

            return blnReturn; 
        }
        private void LoadServicesByContract(AdditionalServicesModel objAdditionalServices, PostTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadServicesByContract ", "entra a LoadServicesByContract");

            string strSystem = "SIACPO";

            try
            {
                //ServiceByContract item = new ServiceByContract();
                ServiceByContractRequest objRequest = new ServiceByContractRequest()
                {
                    StrCoid = objAdditionalServices.HidContract,
                    StrSystem = strSystem,
                    StrUser = objAdditionalServices.UserLogin,
                    audit = audit
                };
           
                ServiceByContractResponse obj = GetServiceByContract(objRequest);
                gListServiceByContract = obj.ListServiceByContract;



                if (gListServiceByContract.Count == CONSTANTS.NumberZero)
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "El contrato no cuenta con ningun servicio";
                    return;
                }
                else
                {
                    foreach (ServiceByContract item in gListServiceByContract)
                    {
                        if (NotRepeat(gListServiceByContractNotRepeat, item._pos_grupo))
                        {
                            gListServiceByContractNotRepeat.Add(item);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadServicesByContract ", string.Format("Error : {0}", ex.Message));                                    
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadServicesByContract ", "sale de LoadServicesByContract");

        }
        private bool NotRepeat(List<ServiceByContract> array, string codigo)
        {
            Claro.Web.Logging.Info("Session: 6546546546", "Transaction: NotRepeat ", "Entra a NotRepeat");

            bool bResult = true;

            foreach (ServiceByContract item in array)
            {
                if (item._pos_grupo == codigo)
                {
                    bResult = false;
                    break;
                    
                }
            }
            Claro.Web.Logging.Info("Session: 6546546546", "Transaction: NotRepeat ", "sale de NotRepeat");

            return bResult;
        }
        private bool GeneratePDF(InsertTemplateInteraction objTemplateData, AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GeneratePDF ", "Entra a GeneratePDF");

            ParametersGeneratePDF objParametersGeneratePdf = new ParametersGeneratePDF();
            string strDateTransaction = DateTime.Now.ToShortDateString();
            string strInteractionId = objAdditionalServices.HidCaseId;
            string strTermPDF = string.Empty;


            objAdditionalServices.HidRoutePdf = string.Empty;
            bool blnSuccess = false;


            try
            {
               
                objParametersGeneratePdf.StrNroServicio = objTemplateData._X_CLARO_NUMBER;
                objParametersGeneratePdf.StrTitularCliente = objAdditionalServices.FullName;
                objParametersGeneratePdf.StrTipoDocIdentidad = objAdditionalServices.TypeDocument;
                objParametersGeneratePdf.StrNroDocIdentidad = objTemplateData._X_DOCUMENT_NUMBER;
                objParametersGeneratePdf.StrTelfReferencia = objTemplateData._X_REFERENCE_PHONE;

                if (!string.IsNullOrEmpty(objAdditionalServices.HidProgramingRoamming))
                {
                    if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveIndetermined || objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDesactiveDetermined)
                    {
                        objParametersGeneratePdf.StrFlagPlantillaPlazo = CONSTANTS.NumberOneString;
                        objTemplateData._X_CLAROLOCAL6 = objAdditionalServices.txtDateAct;
                    }
                    if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveDetermined)
                    {
                        objTemplateData._X_CLAROLOCAL6 = objAdditionalServices.txtDateDeact;
                    }
                }

                if (objTemplateData._X_OPERATION_TYPE == "D")
                {
                    objParametersGeneratePdf.StrEscenarioServicioCom = "Desactivación";
                }
                else
                {
                    if (objTemplateData._X_OPERATION_TYPE == "A")
                    {
                        objParametersGeneratePdf.StrEscenarioServicioCom = "Activación";
                    }
                    else
                    {
                        objParametersGeneratePdf.StrEscenarioServicioCom = "Mantener Activación";
                        objParametersGeneratePdf.StrFlagPlantillaPlazo = CONSTANTS.NumberOneString;
                    }
                }
                objParametersGeneratePdf.StrDescripcionServicioCom = objTemplateData._X_INTER_29;
                objParametersGeneratePdf.StrCfServicioCom = objTemplateData._X_INTER_19;
                objParametersGeneratePdf.StrCfServicioModif = objTemplateData._X_INTER_20;
                objParametersGeneratePdf.StrPeriodoCuotaServicio = Functions.CheckStr(objTemplateData._X_INTER_25);
                objParametersGeneratePdf.StrEmail = objTemplateData._X_EMAIL;
                if (objTemplateData._X_EMAIL_CONFIRMATION.Trim() == CONSTANTS.NumberOneString)
                {
                    objParametersGeneratePdf.StrAplicaEmail = "SI";
                    objParametersGeneratePdf.StrEmail = objTemplateData._X_EMAIL;
                }
                else
                {
                    objParametersGeneratePdf.StrAplicaEmail = "NO";
                    objParametersGeneratePdf.StrEmail = string.Empty;
                }

                objParametersGeneratePdf.StrCentroAtencionArea = objTemplateData._X_INTER_15;
                objParametersGeneratePdf.StrPlanActual = objTemplateData._X_INTER_7;
                objParametersGeneratePdf.StrAplicaProgramacion = objTemplateData._X_CLAROLOCAL5;
                objParametersGeneratePdf.StrFechaEjecucion = objTemplateData._X_CLAROLOCAL6;
                objParametersGeneratePdf.StrPlazo = objTemplateData._X_REASON;
                objParametersGeneratePdf.StrCanalAtencion = objTemplateData._X_INTER_18;
                objParametersGeneratePdf.StrFechaPlazo = objTemplateData._X_INTER_17;
                objParametersGeneratePdf.StrRepresLegal = objAdditionalServices.LegalAgent;

                objParametersGeneratePdf.StrCasoInter = strInteractionId;
                objParametersGeneratePdf.StrFechaTransaccionProgram = strDateTransaction;

                objParametersGeneratePdf.StrNombreArchivoTransaccion = GetTypeTransaction(objAdditionalServices, string.Empty, objTemplateData._X_CLARO_NUMBER);
                objParametersGeneratePdf.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaActDesactServicios");
                objParametersGeneratePdf.StrTipoTransaccion = CONSTANTADDITIONALSERVICEPOSTPAID.NameTransactionActDesactServAdicionalesPostpago;
                    
                strTermPDF = KEY.AppSettings("strTerminacionPDF");
                CommonServicesController obj = new CommonServicesController();

                GenerateConstancyResponseCommon objGeneratePdf = obj.GenerateContancyPDF(objAdditionalServices.IdSession, objParametersGeneratePdf);
                string strError = objGeneratePdf.ErrorMessage;
                blnSuccess = objGeneratePdf.Generated;

                //objGeneratePdf.FullPathPDF = objGeneratePdf.FullPathPDF.Replace("\\", "//");
                objAdditionalServices.HidRoutePdf = objGeneratePdf.FullPathPDF;
                
                //if (blnSuccess)
                //{
                //    strDateTransaction = strDateTransaction.Replace("/", "-");
                //    string strNamePdf = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf",
                //        objParametersGeneratePdf.StrServidorLeerPDF,
                //        objParametersGeneratePdf.StrCarpetaPDFs, objParametersGeneratePdf.StrCarpetaTransaccion,
                //        strInteractionId,
                //        strDateTransaction, objParametersGeneratePdf.StrTipoTransaccion, strTermPDF);
                //    objAdditionalServices.HidRoutePdf = strNamePdf;

                //}

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GeneratePDF ", String.Format("Error GenerarPDF(): {0}", ex.Message));
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GeneratePDF ", "sale de GeneratePDF");

            return blnSuccess;
        }
        private string GetTypeTransaction(AdditionalServicesModel objAdditionalServices, string strRetention, string strIdentifier)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GetTypeTransaction ", "entra a GetTypeTransaction");

            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);

            Iteraction objIteraction = DataInteraction(objAdditionalServices);
            string strTransaction = KEY.AppSettings("strTransaccion");
            string strError = string.Empty;
            string strResult = string.Empty;
            try
            {
                TypeTransactionBRMSResponse objConsumeBRMS = ConsumeBRMS(strCodSubClass, strTransaction, strRetention, strIdentifier, out strError, audit);
                strResult = objConsumeBRMS.StrResult;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GetTypeTransaction ", "ErrorCargarSubMotivoxMotivo: " + ex.Message);
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GetTypeTransaction ", "sale de GetTypeTransaction");
            
            return strResult;
        }
        private bool ActiveDesactiveProgramming(AdditionalServicesModel objAdditionalServices, string strDate = "", string strState = "", string strUserProcess = "")
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ActiveDesactiveProgramming ", "entra a ActiveDesactiveProgramming");

            bool blnReturn = false;
            string strFlagOCCApadece ="";
            int intCodActDesService = 0;
            string strDateSend ="";
            

            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);


            try
            {
                if (!string.IsNullOrEmpty(strState))
                {
                    if (strState == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService)
                    {
                        strFlagOCCApadece = SIACU.Transac.Service.Constants.strCero;
                        intCodActDesService = CONSTANTS.NumberFourteen;
                    }
                    else if (strState == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService)
                    {
                        intCodActDesService = CONSTANTS.NumberFifteen;
                        strFlagOCCApadece = SIACU.Transac.Service.Constants.strCero;
                    }
                }
                else if (objAdditionalServices.HidStatePrograming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService)
                {
                    strState = objAdditionalServices.HidStatePrograming;
                    strFlagOCCApadece = SIACU.Transac.Service.Constants.strCero;
                    intCodActDesService = CONSTANTS.NumberFourteen;
                }
                else if (objAdditionalServices.HidStatePrograming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService)
                {
                    strState = objAdditionalServices.HidStatePrograming;
                    intCodActDesService = CONSTANTS.NumberFifteen;
                    strFlagOCCApadece = SIACU.Transac.Service.Constants.strCero;
                }

                if (!string.IsNullOrEmpty(strDate))
                {
                    strDateSend = strDate;
                }
                else
                {
                    strDateSend = objAdditionalServices.txtDateApp;
                }

                ActDesServProgRequest objRequest = new ActDesServProgRequest()
                {
                    StrTransactionId = audit.transaction,
                    StrIpApplication = Functions.CheckStr(App_Code.Common.GetApplicationIp()),
                    StrApplication = KEY.AppSettings("SIACPOST"),
                    StrMsisDn = objAdditionalServices.lblPhoneNumber.Trim(),
                    StrCoId = objAdditionalServices.HidCodId.Trim(),
                    StrCustomerId = objAdditionalServices.CustomerId.Trim(),
                    StrCoSer = objAdditionalServices.HidCodService,
                    StrFlagOccApadece = strFlagOCCApadece,
                    DAmountFidApadece = Functions.CheckDbl(0.0),
                    DNewCf = Functions.CheckDbl(objAdditionalServices.HidDiffCFixedTotWithCFixed),
                    StrTypeReg = strState,
                    ICycleFact = Functions.CheckInt(objAdditionalServices.lblCycleFact),
                    StrCodSer = Functions.CheckStr(intCodActDesService),
                    StrDesSer = objAdditionalServices.HidNameService,
                    StrNumberAccount = objAdditionalServices.Account,
                    StrUserApplication = strUserProcess,
                    StrUserSystem = objAdditionalServices.UserLogin,
                    StrDateProg = strDateSend,
                    StrIdInteract = objAdditionalServices.HidCaseId,
                    StrTypeSer = objAdditionalServices.HidType,
                    audit = audit
                };

                var vReturnWS = ActDesServProg(objRequest);

                if ((Functions.CheckInt(vReturnWS.StrCodError) == 0) && vReturnWS.BlnResposne)
                {
                    if (!string.IsNullOrEmpty(objAdditionalServices.HidCodService) && objAdditionalServices.HidCodService == KEY.AppSettings("strCodServicioRoamming"))
                    {
                        objAdditionalServices.DisableSuccess = KEY.AppSettings("strMsjRoammingProgTerminada");
                    }
                    else
                    {
                        objAdditionalServices.DisableSuccess = vReturnWS.StrDesResponse;
                    }
                    blnReturn = true;
                }
                else
                {

                    AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<AuditRequest>(objAdditionalServices.IdSession);
                    vReturnWS.StrDesResponse = "ERROR ActivarDesactivarProgramacion(): No se pudo realizar la ejecución porque ya existe una Programación Pendiente o exite algún error de integración.";
                    UpdatexInter30Request objRequestCaseData = new UpdatexInter30Request()
                    {
                        p_objid = objAdditionalServices.HidCaseId,
                        p_texto = vReturnWS.StrDesResponse,
                        audit = auditCommon
                    };
                    var objCaseData = GetUpdatexInter30(objRequestCaseData);

                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ActiveDesactiveProgramming ", string.Format("boolResultUpdate: {0},hidCasoId: {1},strDesError2: {2}", Functions.CheckStr(objCaseData.rResult), objAdditionalServices.HidCaseId, objCaseData.rMsgText));


                    if (vReturnWS.StrCodError == CONSTANTS.NumberZeroString)
                    {
                        objAdditionalServices.DisableError = KEY.AppSettings("strMsjErrorBolsas");
                    }
                    else
                    {
                        objAdditionalServices.DisableError = vReturnWS.StrDesResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ActiveDesactiveProgramming ", String.Format("Error ActivarDesactivarProgramacion(): {0}", ex.Message));                
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: ActiveDesactiveProgramming ", "sale de ActiveDesactiveProgramming");

            return blnReturn;
        }
        private bool TurnOff(AdditionalServicesModel objAdditionalServices, bool blnNoTypi = false)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOff ", "entra a TurnOff");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);
            InsertGeneralResponse oSave = new InsertGeneralResponse();
            bool blnReturn = false;
            try
            {
                
                string strCodRandom = KEY.AppSettings("gConstIdTransaccion") + Functions.CadenaAleatoria();
                if (objAdditionalServices.HidStateContract.Trim().ToUpper() != "ACTIVO")
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "No se puede Activar un Servicio con Contrado Desactivo";
                    return blnReturn;
                }
                if (objAdditionalServices.chkSendMail_IsCheched == "T" && objAdditionalServices.txtEmail == string.Empty)
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "Ingrese Email";
                    return blnReturn;
                }

                if (objAdditionalServices.HidState != "Desactivo")
                {
                    if (!string.IsNullOrEmpty(objAdditionalServices.HidCodService))
                    {
                        if (objAdditionalServices.HidBloqDes == "N")
                        {
                            objAdditionalServices.HidAction = "0";
                            //btnImprimir.Disabled = True se habilitara por medio de JS en el front

                            Iteraction objInteraction = DataInteraction(objAdditionalServices);
                            bool blnExecuteTransaction = true;
                            //string strDateActivation = objAdditionalServices.txtDateAct;
                            //string strDateDeactivation = objAdditionalServices.txtDateDeact;
                            string strUserSystem = objAdditionalServices.UserLogin;
                            //string strUserProcess = KEY.AppSettings("USRProcesoSU");
                            string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                            string strUserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
                            InsertTemplateInteraction objTemplateData = new InsertTemplateInteraction();
                            objTemplateData = DataTemplateInteraction(objAdditionalServices, SIACU.Transac.Service.Constants.strUno);
                            if (objAdditionalServices.blnValidate == false)
                            {
                                return blnReturn;
                            }
                            string strTypeActivation = SIACU.Transac.Service.Constants.strLetraD;
                            RegisterServiceCommercialResponse objBusiness = RegisterServiceCommercial(strUserApp,strCodRandom,strTypeActivation, objAdditionalServices, audit);

                            if (objBusiness.StrResult == "0")
                            {
                                if (blnNoTypi == false)
                                {
                                    bool blnSuccessInteraction = false;

                                    InsertGeneralRequest objRequest = new InsertGeneralRequest()
                                    {
                                        Interaction = objInteraction,
                                        InteractionTemplate = objTemplateData,
                                        vNroTelefono = objAdditionalServices.lblPhoneNumber,
                                        vUSUARIO_SISTEMA = strUserSystem,
                                        vPASSWORD_USUARIO = strUserPass,
                                        vUSUARIO_APLICACION = strUserApp,
                                        vEjecutarTransaccion = blnExecuteTransaction,
                                        audit = audit
                                    };
                                    oSave = GetInsertInteractionBusiness(objRequest);
                                    blnSuccessInteraction = oSave.rResult;

                                    objAdditionalServices.HidCaseId = oSave.rInteraccionId;
                                    if (strFlagContingencyHP == CONSTANTS.NumberOneString)
                                    {
                                        bool blnSuccess = false;
                                        blnSuccess = GeneratePDF(objTemplateData,objAdditionalServices);
                                        if (!blnSuccess)
                                        {
                                            objAdditionalServices.MessageCode = "A";
                                            objAdditionalServices.Message = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                                        }
                                    }
                                }
                                else
                                {
                                    oSave.rFlagInsercion = SIACU.Transac.Service.Constants.Message_OK;
                                }

                                if (objAdditionalServices.chkSendMail_IsCheched == "T")
                                {
                                    SendEmail(objAdditionalServices, audit);
                                }

                                string strCodEvent = KEY.AppSettings("gActDesactServiciosComerciales");
                                string strText = string.Format("Codigo Contrato: {0} /MSISDN: {1} /Codigo Servicio Comercial: {2} /Nombre Servicio Comercial: {3} /Accion: Desactivacion /CAC o DAC: {4}", objAdditionalServices.HidContract, objInteraction.TELEFONO, objAdditionalServices.HidCodService, objAdditionalServices.HidNameService, objAdditionalServices.cboCACDACValue);

                                InsertAudit(objAdditionalServices, strCodEvent, strText);

                                if (oSave.rFlagInsercion != "OK" && oSave.rFlagInsercion != "")
                                {
                                    //limpiar() esta validacion se hara en el JS
                                    objAdditionalServices.MessageCode = "A";
                                    objAdditionalServices.Message = "No se pudo generar la interacción";
                                    return blnReturn;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(objAdditionalServices.HidState.Trim()))
                                    {
                                        objAdditionalServices.HidState =
                                            CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                                    }
                                    blnReturn = true;
                                }

                                //limpiar() esta validacion se hara en el JS
                                if (objBusiness.StrResult == "0")
                                {
                                    if (objAdditionalServices.HidCodService != string.Empty && objAdditionalServices.HidCodService == KEY.AppSettings("strCodServicioRoamming"))
                                    {
                                        objAdditionalServices.DisableSuccess =
                                            KEY.AppSettings("strMsjRoammingProgDesactivada");
                                    }
                                    else
                                    {
                                        objAdditionalServices.MessageCode = "A";
                                        objAdditionalServices.Message = "El servicio se Desactivó con éxito.";
                                    }
                                }
                            }
                            else
                            {
                                objAdditionalServices.MessageCode = "A";
                                objAdditionalServices.Message = "No se puede Desactivar el Servicio: " + objBusiness.StrResult + "-" + objBusiness.StrMessage;
                            }

                        }
                        else
                        {
                            objAdditionalServices.MessageCode = "A";
                            objAdditionalServices.Message = "No se puede Desactivar un servicio Permanente";
                        }
                    }
                    else
                    {
                        objAdditionalServices.MessageCode = "A";
                        objAdditionalServices.Message = "Seleccione un servicio";
                    }
                }
                else
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "El servicio ya se encuentra Desactivo";
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOff ", string.Format("Error : {0}", ex.Message));
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: TurnOff ", "sale de TurnOff");

            return blnReturn;
        }
        private void InsertAudit(AdditionalServicesModel objAdditionalServices, string strCodEvent, string strText)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: InsertAudit ", "entra a InsertAudit");

            AuditRequest audit = App_Code.Common.CreateAuditRequest<AuditRequest>(objAdditionalServices.IdSession);

            string strTransaction = strCodEvent.Trim();
            string strService = KEY.AppSettings("gConstEvtServicio_ModCP");
            string strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strNameClient = objAdditionalServices.FullName;
            string strIpServer = App_Code.Common.GetApplicationIp();
            string strNameServer = App_Code.Common.GetApplicationName();
            string strAccuntUser = objAdditionalServices.UserLogin;
            string strAmount = CONSTANTS.NumberZeroString;
            string strTelephone = objAdditionalServices.lblPhoneNumber;
            //string strText = string.Format("Codigo Contrato: {0} /MSISDN: {1} /Codigo Servicio Comercial: {2} /Nombre Servicio Comercial: {3} /Accion: Desactivacion /CAC o DAC: {4}",
                //objAdditionalServices.ContractId, strTelephone, objAdditionalServices.HidCodService, objAdditionalServices.HidNameService, objAdditionalServices.cboCACDACValue);

            strText = string.Format("{0} /Ip Cliente: {1} /Usuario: {2} /Id Opcion: {3} /Fecha y Hora: {4:dd/MM/yyyy hh:mm:ss}", strText, strIpClient, strAccuntUser, KEY.AppSettings("strIdOpcionClaroProteccion"), DateTime.Now);

            SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
            {
                vCuentaUsuario = strAccuntUser,
                vIpCliente = strIpClient,
                vIpServidor = strIpServer,
                vMonto = strAmount,
                vNombreCliente = strNameClient,
                vNombreServidor = strNameServer,
                vServicio = strService,
                vTelefono = strTelephone,
                vTexto = strText,
                vTransaccion = strTransaction,
                audit = audit
            };
            try
            {
                SaveAuddit(objRequest);

            }
            catch (Exception ex)
            {
                objAdditionalServices.MessageCode = "E";
                objAdditionalServices.MessageLabel = "Error Auditoria(): " + ex.Message;
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: InsertAudit ", "sale de InsertAudit");

        }
        private bool SendEmail(AdditionalServicesModel objAdditionalServices, AuditRequest audit)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SendEmail ", "entra a SendEmail");

            bool blnResponseOK = true;
            try
            {
                Models.TemplateInteractionModel objDataTemplateInteraction = new TemplateInteractionModel();
                CommonServicesController objProcess = new CommonServicesController();
                try
                {
                    objDataTemplateInteraction = objProcess.GetInfoInteractionTemplate(objAdditionalServices.IdSession, objAdditionalServices.HidCaseId);

                }
                catch (Exception ex)
                {
                    objAdditionalServices.MessageCode = "E";
                    objAdditionalServices.MessageLabel = ex.Message;
                    return false;
                }

                string strAttached = "";
                string strSender = KEY.AppSettings("CorreoServicioAlCliente");
                string strDestination = objAdditionalServices.txtEmail;
                string strSubject = "";
                if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                {
                    strSubject = "Roaming Internacional";
                }else
	            {
	                strSubject = "Variación de Servicio";
	            }

                string strMessage = string.Empty;

                strMessage = "<html>";
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
                strMessage +=
                    objProcess.MailHeader("Solicitud de Variación de Servicio - Activación/Desactivación de Servicios",
                        objDataTemplateInteraction.X_INTER_15, DateTime.Now.ToShortDateString(), objAdditionalServices.FullName,
                        objAdditionalServices.HidCaseId, objAdditionalServices.LegalAgent, objDataTemplateInteraction.X_CLARO_NUMBER,
                        objAdditionalServices.TypeDocument, objAdditionalServices.DniRuc);
                strMessage += "</td></tr>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td class='Estilo1'>Los datos modificados son los siguientes:</td></tr>";
                strMessage += "<tr><td height='10'></td>";

                strMessage += "<tr>";
                strMessage += "<td align='center'>";
                strMessage += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";

                string strActionRoamming = string.Empty;
                if (!string.IsNullOrEmpty(objDataTemplateInteraction.X_REGISTRATION_REASON))
                {
                    strActionRoamming = objDataTemplateInteraction.X_REGISTRATION_REASON;
                }

                if (objDataTemplateInteraction.X_INTER_16 == KEY.AppSettings("strRoammingIndeterminado"))
                {
                    objDataTemplateInteraction.X_INTER_16 = CONSTANTADDITIONALSERVICEPOSTPAID.gstrTerm + CONSTANTS.FormatSpace + objDataTemplateInteraction.X_INTER_16;
                }

                if (objDataTemplateInteraction.X_INTER_17 == KEY.AppSettings("strRoammingIndeterminado"))
                {
                    objDataTemplateInteraction.X_INTER_17 = CONSTANTADDITIONALSERVICEPOSTPAID.gstrTerm + CONSTANTS.FormatSpace + objDataTemplateInteraction.X_INTER_17;
                }

                string strActionExecRoamming = string.Empty;

                if (objDataTemplateInteraction.X_OPERATION_TYPE == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService)
                {
                    if (!string.IsNullOrEmpty(strActionRoamming))
                    {
                        strActionExecRoamming = strActionRoamming;
                    }
                    else
                    {
                        strActionExecRoamming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveServiceConst;
                    }

                    if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                    {
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_29 +
                            "</td><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>" +
                            strActionExecRoamming + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Plazo:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_REASON +
                            "</td><td width='180' class='Estilo2' height='22'>Canal de Atención:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_18 + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Fecha de Activación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_16 +
                            "</td><td width='180' class='Estilo2' height='22'>Fecha de Desactivación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_17 + "</td></tr>";
                    }
                    else
                    {
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_29 + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>Activación</td></tr>";
                    }

                }
                else if (objDataTemplateInteraction.X_OPERATION_TYPE == CONSTANTADDITIONALSERVICEPOSTPAID.gstrMaintainService)
                {
                    if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                    {
                        if (!string.IsNullOrEmpty(strActionRoamming))
                        {
                            strActionExecRoamming = strActionRoamming;
                        }
                        else
                        {
                            strActionExecRoamming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrMantActivationServiceConst;
                        }

                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_29 +
                            "</td><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>" +
                            strActionExecRoamming + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Plazo:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_REASON +
                            "</td><td width='180' class='Estilo2' height='22'>Canal de Atención:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_18 + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Fecha de Activación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_16 +
                            "</td><td width='180' class='Estilo2' height='22'>Fecha de Desactivación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_17 + "</td></tr>";
                    }
                }
                else
                {
                    if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                    {
                        if (!string.IsNullOrEmpty(strActionRoamming))
                        {
                            strActionExecRoamming = strActionRoamming;
                        }
                        else
                        {
                            strActionExecRoamming = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDesactivactionServiceConst;
                        }

                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_29 +
                            "</td><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>" +
                            strActionExecRoamming + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Plazo:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_REASON +
                            "</td><td width='180' class='Estilo2' height='22'>Canal de Atención:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_18 + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Fecha de Activación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_16 +
                            "</td><td width='180' class='Estilo2' height='22'>Fecha de Desactivación:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_17 + "</td></tr>";
                    }
                    else
                    {
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                            objDataTemplateInteraction.X_INTER_29 + "</td></tr>";
                        strMessage +=
                            "<tr><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>Desactivación</td></tr>";
                    }
                }
            
                if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                {
                    strMessage += "<tr><td height='10'></td>";
                    strMessage += "<tr><td height='10'></td>";
                    strMessage +=
                        "<tr><td colspan='4' width='90%' class='Estilo2' height='22'>Por la presente doy fe que los datos personales antes consignados son verdaderos y autorizo a América Móvil Perú SAC, que en caso alguno de los datos proporcionados no sean válidos o no coincidan con los registros oficiales, se dé por inválida esta solicitud.</td></tr>";
                    strMessage += "<tr><td height='10'></td>";
                    strMessage += "<tr><td height='10'></td>";
                    strMessage +=
                        "<tr><td colspan='4' width='90%' class='Estilo2' height='22'>Declaro haber sido informado de las condiciones, restricciones y características del servicio Roaming Internacional se encuentran a disposición en el sitio Web www.claro.com.pe (Sección Personal/Móvil/Roaming) y haber recibido copia de la presente solicitud.</td></tr>";
                }


                strMessage += "</Table>";
                strMessage += "</td></tr>";

                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
                strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";

                if (Functions.CheckInt(objDataTemplateInteraction.X_INTER_21) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming")))
                {
                    strMessage +=
                        "<tr><td colspan='4' width='90%' class='Estilo2' height='22'>Para más información o consultas llama gratis desde tu celular Claro al 123 opción 3 - 6 o escríbenos a atencion.roaming@claro.com.pe</td></tr>";
                }
                else
                {
                    strMessage +=
                        "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";    
                }
                strMessage += "</table>";
                strMessage += "</body>";
                strMessage += "</html>";

                SendEmailResponseCommon objResponse = new SendEmailResponseCommon();
                SendEmailRequestCommon objRequest = new SendEmailRequestCommon()
                {
                    strTo = strDestination,
                    strSender = strSender,
                    strSubject = strSubject,
                    strMessage = strMessage,
                    audit = audit
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() =>
                {
                    return oServiceCommon.GetSendEmail(objRequest);
                });
                string strResponse = SIACU.Transac.Service.Constants.Message_OK;
                string strOK = KEY.AppSettings("gConstKeyStrResultInsTipUSOK");
                if (strResponse == strOK)
                {
                    objAdditionalServices.MessageCode = SIACU.Transac.Service.Constants.Message_OK;
                    objAdditionalServices.MessageLabel = objAdditionalServices.MessageLabel +
                                                         ". En breves minutos se estará enviando un correo de notificación.";
                    blnResponseOK = true;
                }
                else
                {
                    objAdditionalServices.MessageCode = SIACU.Transac.Service.Constants.Letter_E;
                    objAdditionalServices.MessageLabel = objAdditionalServices.MessageLabel +
                                                         ". Pero no se pudo procesar el envío del correo de notificación.";
                    blnResponseOK = false;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SendEmail ", string.Format("Error EnviarCorreo : {0}", ex.Message));
                throw new Exception("ERROR: EnviarCorreo(). " + ex.Message);
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SendEmail ", "sale de SendEmail");

            return blnResponseOK;
        }
        private InsertTemplateInteraction DataTemplateInteraction(AdditionalServicesModel objAdditionalServices, string strActDesac = "")
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataTemplateInteraction ", "entra a DataTemplateInteraction");

            objAdditionalServices.blnValidate = true;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);

            InsertTemplateInteraction objTemplateInteraction = new InsertTemplateInteraction();
            try
            {
                
                objTemplateInteraction._NOMBRE_TRANSACCION = objAdditionalServices.HidTransaction;
                objTemplateInteraction._X_CLARO_NUMBER = objAdditionalServices.lblPhoneNumber;
                objTemplateInteraction._X_FIRST_NAME = objAdditionalServices.FirstName;
                objTemplateInteraction._X_LAST_NAME = objAdditionalServices.LastName;
                objTemplateInteraction._X_DOCUMENT_NUMBER = objAdditionalServices.DniRuc;
                objTemplateInteraction._X_REFERENCE_PHONE = objAdditionalServices.PhoneReference;
                objTemplateInteraction._X_INTER_29 = objAdditionalServices.HidNameService;
                objTemplateInteraction._X_INTER_21 = objAdditionalServices.HidCodService;


                if (!string.IsNullOrEmpty(objAdditionalServices.HidFixedCharge))
                {
                    objTemplateInteraction._X_INTER_19 = objAdditionalServices.HidFixedCharge;
                }

                if (!string.IsNullOrEmpty(objAdditionalServices.HidFixedChargeM))
                {
                    objTemplateInteraction._X_INTER_20 = objAdditionalServices.HidFixedChargeM;
                }

                if (!string.IsNullOrEmpty(Functions.CheckStr(objAdditionalServices.HidNumberPeriod)))
                {
                    objTemplateInteraction._X_INTER_25 = objAdditionalServices.HidNumberPeriod;
                }

                if (objAdditionalServices.HidAction == SIACU.Transac.Service.Constants.strUno)
                {
                    objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                }
                else
                {
                    objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                }

                if (objAdditionalServices.chkSendMail_IsCheched == "T")
                {
                    objTemplateInteraction._X_EMAIL_CONFIRMATION = SIACU.Transac.Service.Constants.strUno;
                    objTemplateInteraction._X_EMAIL = objAdditionalServices.txtEmail;
                }
                else
                {
                    objTemplateInteraction._X_EMAIL_CONFIRMATION = SIACU.Transac.Service.Constants.strUno;
                }

                objTemplateInteraction._X_INTER_15 = objAdditionalServices.cboCACDACValue;
                objTemplateInteraction._X_INTER_7 = objAdditionalServices.Plan;

                if (objAdditionalServices.chkProgram_IsChecked == "T")
                {
                    objTemplateInteraction._X_CLAROLOCAL5 = SIACU.Transac.Service.Constants.PresentationLayer
                        .gstrVariableSI;
                    objTemplateInteraction._X_CLAROLOCAL6 = objAdditionalServices.txtDateApp;
                }
                else
                {
                    objTemplateInteraction._X_CLAROLOCAL5 = SIACU.Transac.Service.Constants.PresentationLayer
                        .gstrVariableNO;
                    objTemplateInteraction._X_CLAROLOCAL6 = DateTime.Now.ToShortDateString();
                }

                if (!string.IsNullOrEmpty(objAdditionalServices.HidCodService.Trim()) && string.IsNullOrEmpty(KEY.AppSettings("strCodServicioRoamming").Trim()))
                {
                    if (Functions.CheckInt(objAdditionalServices.HidCodService) == Functions.CheckInt(KEY.AppSettings("strCodServicioRoamming").Trim()))
                    {
                        string strDateFrom;
                        DateTime dtDateFrom;

                        if (!string.IsNullOrEmpty(objAdditionalServices.HidDateFrom))
                        {
                            strDateFrom =
                                objAdditionalServices.HidDateFrom.Replace(ConstantsSiacpo.ConstGuion,
                                    ConstantsSiacpo.ConstSlash);
                            dtDateFrom = Functions.CheckDate(strDateFrom);
                            strDateFrom = dtDateFrom.ToShortDateString();
                        }
                        else
                        {
                            strDateFrom = DateTime.Now.ToShortDateString();
                        }

                        if (!string.IsNullOrEmpty(strActDesac))
                        {
                            objTemplateInteraction._X_REASON = KEY.AppSettings("strRoammingIndeterminado");
                            if (strActDesac == Functions.CheckStr(SIACU.Transac.Service.Constants.zero))
                            {
                                objTemplateInteraction._X_INTER_16 = strDateFrom;
                                objTemplateInteraction._X_INTER_17 = DateTime.Now.ToShortDateString();
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                            }
                            else if (strActDesac == SIACU.Transac.Service.Constants.strUno)
                            {
                                objTemplateInteraction._X_INTER_16 = DateTime.Now.ToShortDateString();
                                objTemplateInteraction._X_INTER_17 = KEY.AppSettings("strRoammingIndeterminado");
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                            }
                            else if (strActDesac == SIACU.Transac.Service.Constants.strDos)
                            {
                                objTemplateInteraction._X_INTER_16 = strDateFrom;
                                objTemplateInteraction._X_INTER_17 = KEY.AppSettings("strRoammingIndeterminado");
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrMaintainService;
                            }
                        }

                        if (!string.IsNullOrEmpty(objAdditionalServices.HidProgramingRoamming))
                        {
                            if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveDetermined)
                            {
                                if (objAdditionalServices.rdbDetermined_IsChecked == "T")
                                {
                                    objTemplateInteraction._X_REASON = KEY.AppSettings("strRoammingDeterminado");
                                }
                                objTemplateInteraction._X_INTER_16 = strDateFrom;
                                objTemplateInteraction._X_INTER_17 = objAdditionalServices.txtDateDeact;
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrDeactivationService;
                            }
                            else if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrDesactiveDetermined)
                            {
                                objTemplateInteraction._X_REGISTRATION_REASON =
                                    CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveServiceConst;
                                if (objAdditionalServices.rdbDetermined_IsChecked == "T")
                                {
                                    objTemplateInteraction._X_REASON = KEY.AppSettings("strRoammingDeterminado");
                                }
                                objTemplateInteraction._X_INTER_16 = objAdditionalServices.txtDateAct;
                                objTemplateInteraction._X_INTER_17 = objAdditionalServices.txtDateDeact;
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                            }
                            else if (objAdditionalServices.HidProgramingRoamming == CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveIndetermined)
                            {
                                if (objAdditionalServices.rdbIndeterminate_IsChecked == "T")
                                {
                                    objTemplateInteraction._X_REASON = KEY.AppSettings("strRoammingIndeterminado");
                                }
                                objTemplateInteraction._X_INTER_16 = objAdditionalServices.txtDateAct;
                                objTemplateInteraction._X_INTER_17 = KEY.AppSettings("strRoammingIndeterminado");
                                objTemplateInteraction._X_INTER_18 = objAdditionalServices.HidTypeRequest;
                                objTemplateInteraction._X_OPERATION_TYPE = CONSTANTADDITIONALSERVICEPOSTPAID.gstrActiveService;
                            }
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataTemplateInteraction ", "sale de DataTemplateInteraction");
            
            return objTemplateInteraction;
        }
        private Iteraction DataInteraction(AdditionalServicesModel objAdditionalServices)
        {
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataInteraction ", "entra a DataInteraction");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);

            Iteraction obj = new Iteraction();
        
            try
            {
                obj.OBJID_CONTACTO = objAdditionalServices.ContactCode;
                obj.FECHA_CREACION = DateTime.Now.ToString("dd/MM/yyyy");
                obj.TELEFONO = objAdditionalServices.lblPhoneNumber;
                obj.TIPO = objAdditionalServices.HidType;
                obj.CLASE = objAdditionalServices.HidClassDes;
                obj.SUBCLASE = objAdditionalServices.HidSubClassDes;
                obj.SUBCLASE_CODIGO = objAdditionalServices.HidSubClassId;
                strCodSubClass = objAdditionalServices.HidSubClassId;
                obj.TIPO_INTER = KEY.AppSettings("AtencionDefault");
                obj.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
                obj.RESULTADO = KEY.AppSettings("Ninguno");
                obj.HECHO_EN_UNO = Functions.CheckStr(SIACU.Transac.Service.Constants.zero);
                obj.NOTAS =  objAdditionalServices.TxtNote;
                obj.FLAG_CASO = Functions.CheckStr(SIACU.Transac.Service.Constants.zero);
                obj.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
                obj.AGENTE = objAdditionalServices.UserLogin;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataInteraction ", string.Format("Error : {0}", ex.Message));
            }

            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataInteraction ", "sale  de DataInteraction");

            return obj;
        }


        public JsonResult AccesPAge()
        {
            ArrayList listAccesPage = new ArrayList();
            listAccesPage.Add(KEY.AppSettings("strOpcActivaCheckProgramarDTH"));
            return Json(listAccesPage, JsonRequestBehavior.AllowGet);
        }
        private void Start(AdditionalServicesModel objAdditionalServices)
        {

            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);

            CommonServicesController Convert2010 = new AdditionalPointsController();
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: Start ", "Ingresa a Start");

            try
            {
                objAdditionalServices.HidStateActiveCC = KEY.AppSettings("gConstkeyEstadoActivoCC");
                objAdditionalServices.strMaxQuota = KEY.AppSettings("gConstMontoMax_Cuota");
                objAdditionalServices.strMinQuota = KEY.AppSettings("gConstMontoMin_Cuota");
                objAdditionalServices.strMaxPeriod = KEY.AppSettings("gConstNumMax_Periodos");
                objAdditionalServices.strMinPeriod = KEY.AppSettings("gConstNumMin_Periodos");
                objAdditionalServices.strPeriod = KEY.AppSettings("gConstPeriodo_NoPer");
                objAdditionalServices.strModQuotaPer = KEY.AppSettings("gConstkeyModCuoPer");
                objAdditionalServices.strEnvioLog = KEY.AppSettings("gConstKeyStrEnvioLog");
                objAdditionalServices.strEstOk = KEY.AppSettings("gConstKeyStrResultValLogOK"); 
			    objAdditionalServices.strEstCancel =  KEY.AppSettings("gConstKeyStrResultValLogCANCEL");

                objAdditionalServices.gConstResultadoErrorBSCS = KEY.AppSettings("gConstResultadoErrorBSCS");

                gNumberPhone = Convert2010.GetNumber(objAdditionalServices.IdSession, false, objAdditionalServices.lblPhoneNumber);
                objAdditionalServices.lblPhoneNumber = gNumberPhone;
                objAdditionalServices.HidCodId = objAdditionalServices.ContractId;
                LoadTypification(objAdditionalServices);
                LoadContractByPhoneNumber(objAdditionalServices);

                objAdditionalServices.HidListTypeSolRoaming = GetTypeSolRoaming(objAdditionalServices);
                objAdditionalServices.HidCodServRoaming = KEY.AppSettings("strCodServicioRoamming");
                if (KEY.AppSettings("strMinFechaDesactivacion").Trim() != string.Empty || KEY.AppSettings("strMaxFechaDesactivacion").Trim() != string.Empty)
                {
                    if (Functions.CheckInt(KEY.AppSettings("strMinFechaDesactivacion")) > 0 || Functions.CheckInt(KEY.AppSettings("strMaxFechaDesactivacion")) > 0)
                    {
                        objAdditionalServices.HidMinDateDeactivation = KEY.AppSettings("strMinFechaDesactivacion");
                        objAdditionalServices.HidMaxDateDeactivacion = KEY.AppSettings("strMaxFechaDesactivacion");
                    }
                    else
                    {
                        objAdditionalServices.HidMinDateDeactivation = string.Empty;
                        objAdditionalServices.HidMaxDateDeactivacion = string.Empty;
                    }
                }
                else
                {
                    objAdditionalServices.HidMinDateDeactivation = string.Empty;
                    objAdditionalServices.HidMaxDateDeactivacion = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: Start ", "Error: " + ex.Message);
            }
        }
        private void GetTotalFixedCharge(AdditionalServicesModel objAdditionalServices, PostTransacService.AuditRequest audit)
        {
            bool blnResult;

            PostTransacService.ApprovalBusinessCreditLimitResponse objApprovalBusinessCreditLimit = new PostTransacService.ApprovalBusinessCreditLimitResponse();

            try
            {
                string strAccount = ConstantsSiacpo.ConstVacio;
                int intContract = Functions.CheckInt(objAdditionalServices.HidContract);
                int intService = SIACU.Transac.Service.Constants.zero;
                if (!string.IsNullOrEmpty(objAdditionalServices.ContractId.Trim()))
                {
                    objApprovalBusinessCreditLimit = GetApprovalBusinessCreditLimit(objAdditionalServices, strAccount, intContract, intService, audit);
                    blnResult = objApprovalBusinessCreditLimit.result;
                    Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GetTotalFixedCharge ", "RESULT: " + blnResult);

                    objAdditionalServices.HidTotalFixedCharge = Functions.CheckStr(objApprovalBusinessCreditLimit.NewCharge);
                }
                else
                {
                    objAdditionalServices.HidTotalFixedCharge = ConstantsSiacpo.ConstVacio;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: GetTotalFixedCharge ", "Error: " + ex.Message);
            }
        }
        private void LoadTypification(AdditionalServicesModel objAdditionalServices)
        {
            string strIdSession = objAdditionalServices.IdSession;
            string strContractId = objAdditionalServices.ContractId;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadTypification ", "Entra");

            string strTempTypePhone = string.Empty;


            bool result = false;

            if (objAdditionalServices.HidTransactionDTH == CONSTANTADDITIONALSERVICEPOSTPAID.strTransactionACTDESSER)
            {
                CommonServicesController objCommonService = new CommonServicesController();
                strTempTypePhone = objCommonService.ValidatePermissionPost(strIdSession, strContractId); //cambiar cuando se pueda conectar al WSDL
                //strTempTypePhone = "POSTPAGO";
            }
            else
            {
                strTempTypePhone = KEY.AppSettings("gConstKeyStrTipoDTHPOST");
            }

            objAdditionalServices.HidTransaction = objAdditionalServices.HidTransactionDTH;
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadTypification ", "strTempTypePhone: " + strTempTypePhone);

            try
            {
                var list = GetTypificationByTransaction(objAdditionalServices.HidTransaction.Trim().ToUpper(), audit).ListTypification;
                foreach (var item in list)
                {
                    if (item.TIPO.ToUpper().Equals(strTempTypePhone))
                    {
                        objAdditionalServices.HidClassId = item.CLASE_CODE;
                        objAdditionalServices.HidSubClassId = item.SUBCLASE_CODE;
                        objAdditionalServices.HidType = item.TIPO;
                        objAdditionalServices.HidClassDes = item.CLASE;
                        objAdditionalServices.HidSubClassDes = item.SUBCLASE;
                        result = true;

                    }
                }

                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadTypification ", "valores de : " + objAdditionalServices.HidClassId + "/" + objAdditionalServices.HidSubClassId + "/" + objAdditionalServices.HidType + "/" + objAdditionalServices.HidClassDes + "/" + objAdditionalServices.HidSubClassDes);

                if (result == false)
                {
                    objAdditionalServices.MessageLabel = CONSTANTADDITIONALSERVICEPOSTPAID.strNotTypification;
                    return;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadTypification ", "Error: " + ex.Message);
            }
        }
        private void LoadContractByPhoneNumber(AdditionalServicesModel objAdditionalServices)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);

            //List<CommonTransacService.ContractByPhoneNumber> list = new List<CommonTransacService.ContractByPhoneNumber>();
            //CommonTransacService.ContractByPhoneNumber obj = new CommonTransacService.ContractByPhoneNumber();
            //obj.CustCod = "7.469379825.00.00.100000";
            //obj.CodId = "78231455";
            //obj.Name = "DOCTOR SYSTEM PERU S.A.C";
            //obj.State = "Inactivo";
            //obj.Date = "2016-05-05 20:00:00";
            //obj.Reason = "Moroso";
            //list.Add(obj);

            //obj = new CommonTransacService.ContractByPhoneNumber();
            //obj.CustCod = "7.469379825";
            //obj.CodId = "78231457";
            //obj.Name = "SYSTEM PERU S.A.C";
            //obj.State = "Activo";
            //obj.Date = "2016-05-05 20:00:00";
            //obj.Reason = "Moroso";
            //list.Add(obj);

            //objAdditionalServices.ListContractByPhoneNumber = list;

            //return;

            var objResponse = GetContractByPhoneNumber(objAdditionalServices, audit);
            if (objResponse != null)
            {
                if (objResponse.ListContByPhone == null)
                {
                    objAdditionalServices.MessageCode = "E_A";
                    objAdditionalServices.Message = "La linea no cuenta con ningun contrato";
                    return;
                }
                else
                {
                    objAdditionalServices.ListContractByPhoneNumber = objResponse.ListContByPhone;
                }
            }
            else
            {
                objAdditionalServices.MessageCode = "E";
                objAdditionalServices.Message = "La linea no cuenta con ningun contrato";
                return;
            }
            Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: LoadContractByPhoneNumber ", "LoadContractByPhoneNumber: " + Functions.CheckStr(objAdditionalServices.ListContractByPhoneNumber.Count));

            return;
        }
        private string GetTypeSolRoaming(AdditionalServicesModel objAdditionalServices)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);

            string strString = string.Empty;
            try
            {
                objAdditionalServices.HidProfileSolRoaming = CONSTANTS.NumberZero;
                List<ItemGeneric> listGeneric = Functions.GetListValuesXML("ListaTipoSolicitudServRoaming", CONSTANTS.NumberTwoString, SIACU.Transac.Service.Constants.SiacutDataXML);
                foreach (ItemGeneric item in listGeneric)
                {
                    strString += item.Code + "," + item.Description + ";";
                }
                strString = strString.Substring(0, strString.Length - 1);

                int i;
                int j;
                int k;
                for (i = 0; i < Functions.CheckStr(objAdditionalServices.SessionProfile).Split(',').Length - 1; i++)
                {
                    for (j = 0; j < KEY.AppSettings("strOpcPerfilesPresenciales").Split(',').Length - 1; j++)
                    {
                        //var arrayTemp = objAdditionalServices.SessionProfile.Split(',');
                        //var arrayKey = KEY.AppSettings("strOpcPerfilesPresenciales").Split(',');
                        if (Functions.CheckInt(objAdditionalServices.SessionProfile.Split(',')[i]) == Functions.CheckInt(KEY.AppSettings("strOpcPerfilesPresenciales").Split(',')[j]))
                        {
                            objAdditionalServices.HidProfileSolRoaming = SIACU.Transac.Service.Constants.numeroUno;
                            break;
                        }
                        else
                        {
                            objAdditionalServices.HidProfileSolRoaming = SIACU.Transac.Service.Constants.numeroTres;
                        }
                    }
                    if (objAdditionalServices.HidProfileSolRoaming == SIACU.Transac.Service.Constants.numeroTres)
                    {
                        for (k = 0; k < KEY.AppSettings("strOpcPerfilesTelefonico").Split(',').Length - 1; k++)
                        {
                            if (Functions.CheckInt(Functions.CheckStr(objAdditionalServices.SessionProfile).Split(',')[i]) == Functions.CheckInt(KEY.AppSettings("strOpcPerfilesTelefonico").Split(',')[k]))
                            {
                                objAdditionalServices.HidProfileSolRoaming = SIACU.Transac.Service.Constants.numeroDos;
                                break;
                            }
                            else
                            {
                                objAdditionalServices.HidProfileSolRoaming = SIACU.Transac.Service.Constants.numeroTres;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }


            return strString;
        }
        private void SaveInteractionMCP(AdditionalServicesModel objAdditionalServices)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);

            P_CHargeCod_Typi_ModCuoPer(objAdditionalServices);

            Iteraction objInteraction = DataInteractionMCP(objAdditionalServices);

            InsertTemplateInteraction oTemplateData = new InsertTemplateInteraction();

            bool blnValidate;
            bool blnExecuteTransaction = true;
            string strUserSystem = objAdditionalServices.UserLogin;
            string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strPassUser = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            oTemplateData = DataTemplateInteractionMCP(objAdditionalServices);
            if (objAdditionalServices.blnValidate == false)
            {
                return;
            }
            InsertGeneralRequest objRequest = new InsertGeneralRequest()
            {
                Interaction = objInteraction,
                InteractionTemplate = oTemplateData,
                vNroTelefono = objAdditionalServices.lblPhoneNumber,
                vUSUARIO_SISTEMA = strUserSystem,
                vUSUARIO_APLICACION = strUserApp,
                vPASSWORD_USUARIO = strPassUser,
                vEjecutarTransaccion = blnExecuteTransaction,
                audit = audit
            };

            InsertGeneralResponse oSave = GetInsertInteractionBusiness(objRequest);

            objAdditionalServices.HidCaseId = oSave.rInteraccionId;

            //data de prueba por favor eliminar luego de colocar data real
            //objAdditionalServices.HidCaseId = "323";
            //objAdditionalServices.HidStateMod = "ADADAD";
            //Fin de data de preuba

            if (oSave.rInteraccionId != null)
            {
                if (oSave.rInteraccionId != string.Empty)
                {
                    SaveModCP(objAdditionalServices);
                    if (string.IsNullOrEmpty(objAdditionalServices.HidStateMod))
                    {
                        RegisterAuditModCP(objAdditionalServices, audit);
                        if (objAdditionalServices.chkSendMail_IsCheched == "T")
                        {
                            SendEmailModCP(objAdditionalServices, audit);
                        }

                        objAdditionalServices.HidStateMod = KEY.AppSettings("gConstKeyStrResult_ModCPOK");
                    }
                    else
                    {
                        
                        UpdateXInter29RequestCommon objRequestModfyNotes = new UpdateXInter29RequestCommon()
                        {
                            IdInteract = objAdditionalServices.HidCaseId,
                            Text = objAdditionalServices.HidStateMod,
                            Order = string.Empty,
                            audit = audit
                        };

                        ModifyNotes(objRequestModfyNotes);
                        objAdditionalServices.MessageCode = "A";
                        objAdditionalServices.Message =
                            "No se pudo generar la modificación del servicio o registro de auditoría. " +
                            objAdditionalServices.HidStateMod;
                        return;
                    }
                    objAdditionalServices.MessageCode = "E";
                    objAdditionalServices.MessageLabel = oSave.rMsgText + " " + oSave.rMsgTextTransaccion;
                    return;
                }
                objAdditionalServices.MessageCode = "E_A";
                objAdditionalServices.MessageLabel = oSave.rMsgTextTransaccion;
                objAdditionalServices.Message =
                    "No se pudo generar tipificacion debido a que es posible que el número no se encuentra registrado en Clarify";
            }



        }
        private void P_CHargeCod_Typi_ModCuoPer(AdditionalServicesModel objAdditionalServices)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);

            string strIdSession = objAdditionalServices.IdSession;
            string strContractId = objAdditionalServices.ContractId;
            try
            {
                string strTempTypePhone = string.Empty;
                bool result = false;
                if (objAdditionalServices.HidTransactionDTH == CONSTANTADDITIONALSERVICEPOSTPAID.strTransactionACTDESSER)
                {
                    CommonServicesController objCommonService = new CommonServicesController();
                    strTempTypePhone = objCommonService.ValidatePermissionPost(strIdSession, strContractId); //cambiar cuando se pueda conectar al WSDL
                    //strTempTypePhone = "POSTPAGO";
                }
                else
                {
                    strTempTypePhone = KEY.AppSettings("gConstKeyStrTipoDTHPOST");
                }

                objAdditionalServices.HidTransaction = objAdditionalServices.HidTransactionDTH;

                var trasaction = KEY.AppSettings("gConstTransTipi_ModCuoPer");

                var list = GetTypificationByTransaction(trasaction.Trim().ToUpper(), audit).ListTypification;
                foreach (var item in list)
                {
                    if (item.TIPO.ToUpper().Equals(strTempTypePhone))
                    {
                        objAdditionalServices.HidClassIdMCP = item.CLASE_CODE;
                        objAdditionalServices.HidSubClassIdMCP = item.SUBCLASE_CODE;
                        objAdditionalServices.HidTypeMCP = item.TIPO;
                        objAdditionalServices.HidClassDesMCP = item.CLASE;
                        objAdditionalServices.HidSubClassDesMCP = item.SUBCLASE;
                        result = true;

                    }
                }

                Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: P_CHargeCod_Typi_ModCuoPer ", "valores de : " + objAdditionalServices.HidClassIdMCP + "/" + objAdditionalServices.HidSubClassIdMCP + "/" + objAdditionalServices.HidTypeMCP + "/" + objAdditionalServices.HidClassDesMCP + "/" + objAdditionalServices.HidSubClassDesMCP);

                if (result == false)
                {
                    objAdditionalServices.MessageLabel = CONSTANTADDITIONALSERVICEPOSTPAID.strNotTypification;
                    return;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: P_CHargeCod_Typi_ModCuoPer ", "Error: " + ex.Message);
            }
        }
        private Iteraction DataInteractionMCP(AdditionalServicesModel objAdditionalServices)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);
            Iteraction objInteraction = new Iteraction();
            try
            {
                

                objInteraction.OBJID_CONTACTO = objAdditionalServices.ContractId;
                objInteraction.FECHA_CREACION = DateTime.Now.ToString("dd/MM/yyyy");
                objInteraction.TELEFONO = objAdditionalServices.lblPhoneNumber;
                objInteraction.TIPO = objAdditionalServices.HidTypeMCP;
                objInteraction.CLASE = objAdditionalServices.HidClassDesMCP;
                objInteraction.SUBCLASE = objAdditionalServices.HidSubClassDesMCP;
                objInteraction.TIPO_INTER = KEY.AppSettings("AtencionDefault");
                objInteraction.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
                objInteraction.RESULTADO = KEY.AppSettings("Ninguno");
                objInteraction.HECHO_EN_UNO = SIACU.Transac.Service.Constants.zero.ToString();
                objInteraction.NOTAS = objAdditionalServices.TxtNote;
                objInteraction.FLAG_CASO = SIACU.Transac.Service.Constants.zero.ToString();
                objInteraction.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
                objInteraction.AGENTE = objAdditionalServices.UserLogin;
                

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataInteractionMCP ", "Error: " + ex.Message);
            }
            return objInteraction;
        }
        private InsertTemplateInteraction DataTemplateInteractionMCP(AdditionalServicesModel objAdditionalServices)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objAdditionalServices.IdSession);
            InsertTemplateInteraction objDataTemplate = new InsertTemplateInteraction();
            try
            {
                objAdditionalServices.blnValidate = true;
                
                objDataTemplate._NOMBRE_TRANSACCION = KEY.AppSettings("gConstTransTipi_ModCuoPer");
                objDataTemplate._X_CLARO_NUMBER = objAdditionalServices.lblPhoneNumber;
                objDataTemplate._X_FIRST_NAME = objAdditionalServices.FirstName;
                objDataTemplate._X_LAST_NAME = objAdditionalServices.LastName;
                objDataTemplate._X_DOCUMENT_NUMBER = objAdditionalServices.DniRuc;
                objDataTemplate._X_REFERENCE_PHONE = objAdditionalServices.PhoneReference;
                objDataTemplate._X_ADDRESS5 = objAdditionalServices.HidNameService;
                objDataTemplate._X_INTER_21 = objAdditionalServices.HidCodService;
                objDataTemplate._X_LASTNAME_REP = objAdditionalServices.lblCustomerType;
                objDataTemplate._X_NAME_LEGAL_REP = objAdditionalServices.CustomerContact;

                if (!string.IsNullOrEmpty(objAdditionalServices.HidFixedCharge))
                {
                    objDataTemplate._X_INTER_19 = objAdditionalServices.HidFixedCharge;
                }
                if (!string.IsNullOrEmpty(objAdditionalServices.HidFixedChargeM))
                {
                    objDataTemplate._X_INTER_20 = objAdditionalServices.HidFixedChargeM;
                }
                if (!string.IsNullOrEmpty(Functions.CheckStr(objAdditionalServices.HidNumberPeriod)))
                {
                    objDataTemplate._X_INTER_25 = objAdditionalServices.HidNumberPeriod;
                }

                if (objAdditionalServices.chkSendMail_IsCheched == "T")
                {
                    objDataTemplate._X_EMAIL_CONFIRMATION = SIACU.Transac.Service.Constants.strUno;
                    objDataTemplate._X_EMAIL = objAdditionalServices.txtEmail.Trim();
                }
                else
                {
                    objDataTemplate._X_EMAIL_CONFIRMATION = SIACU.Transac.Service.Constants.zero.ToString();
                }

                objDataTemplate._X_INTER_15 = objAdditionalServices.cboCACDACValue;

                objDataTemplate._X_ADJUSTMENT_AMOUNT = Functions.CheckDbl(objAdditionalServices.HidQoutMod);
                objDataTemplate._X_CHARGE_AMOUNT = Functions.CheckDbl(objAdditionalServices.HidPeriodMod);
                if (!string.IsNullOrEmpty(objAdditionalServices.TxtNote))
                {
                    objDataTemplate._X_INTER_30 = objAdditionalServices.TxtNote.Trim();
                }
                objDataTemplate._X_INTER_10 = Functions.CheckDbl(objAdditionalServices.HidCarFixed);
                objDataTemplate._X_CLAROLOCAL6 = DateTime.Now.ToString("dd/MM/yyyy");
                objDataTemplate._X_INTER_2 = objAdditionalServices.HidPeriodAnt;
                objDataTemplate._X_INTER_16 = objAdditionalServices.HidQuotAnt;
                objDataTemplate._X_TYPE_DOCUMENT = objAdditionalServices.TypeDocument;

                


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataTemplateInteractionMCP ", "Error: " + ex.Message);
            }
            return objDataTemplate;
        }
        private void SaveModCP(AdditionalServicesModel objAdditionalServices)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);

            try
            {
                if (!DataService(objAdditionalServices))
                {
                    objAdditionalServices.MessageCode = "A";
                    objAdditionalServices.Message = "Hubo inconvenientes al solicitar el spcode y sncode.";
                    return;
                }
                GetModifyServiceQuotAmount(objAdditionalServices, audit);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SaveModCP ", "Error: " + ex.Message);
            }
            
        }
        private bool DataService(AdditionalServicesModel objAdditionalServices)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(objAdditionalServices.IdSession);
            
            
            //Solo como datos de prueba! se debe CAmbiar
            objAdditionalServices.HidCodId_Contract = "2257312";
            objAdditionalServices.HidCodService = "1099";
            //Fin de datos de prueba

            
            int intCodid = Functions.CheckInt(objAdditionalServices.HidCodId_Contract);
            int intCodServ = Functions.CheckInt(objAdditionalServices.HidCodService);
            bool blnObjState = true;

            try
            {
                ConsultServiceResponse CodError = GetConsultService(intCodid, intCodServ, audit);
                if (CodError.ErrorNum != SIACU.Transac.Service.Constants.zero.ToString())
                {
                    blnObjState = false;
                }
                else
                {
                    objAdditionalServices.HidSnCode = CodError.SnCode.ToString();
                    objAdditionalServices.HidSpCode = CodError.SpCode.ToString();
                }
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: DataService ", string.Format("Datos de SNCode,SPCode - Modificar Cuota/Periodo: {0}", ex.Message));
            }
            return blnObjState;
        }
        private bool RegisterAuditModCP(AdditionalServicesModel objAdditionalServices, AuditRequest audit)
        {
            string strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strIpServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationIp();
            string strAmount = SIACU.Transac.Service.Constants.zero.ToString();
            string strNameServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationName();
            string strService = KEY.AppSettings("gConstEvtServicio_ModCP");

            string strTransaction = KEY.AppSettings("gConstTransAudi_ModCuoPer");
            string strText = string.Format("Se generó la {0} del teléfono: {1}", strTransaction,
                objAdditionalServices.lblPhoneNumber);
            string strTransac = KEY.AppSettings("gConstEvtGeneraAudit_ModCP");

            bool blnResult = false;

            SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
            {
                vCuentaUsuario = objAdditionalServices.UserLogin,
                vIpCliente = strIpClient,
                vIpServidor = strIpServer,
                vMonto = strAmount,
                vNombreCliente = objAdditionalServices.FullName,
                vNombreServidor = strNameServer,
                vServicio = strService,
                vTelefono = objAdditionalServices.lblPhoneNumber,
                vTexto = strText,
                vTransaccion = strTransac,
                audit = audit
            };

            try
            {
                SaveAuditResponseCommon SaveAudit = SaveAuddit(objRequest);
                blnResult = SaveAudit.respuesta;
            }
            catch
            {
                bool blnResult2;
                string strMessage = string.Format("Problema con el WebServices. No se registró en el log de Auditoria, la generación de la {0} de Recibos del teléfono: {1}).", strTransaction, objAdditionalServices.lblPhoneNumber);
                objAdditionalServices.MessageCode = "A";
                objAdditionalServices.Message = strMessage;
                SaveAuditResponseCommon SaveAudit = SaveAuddit(objRequest);
                blnResult2 = SaveAudit.respuesta;
            }
            return blnResult;
        }
        private bool SendEmailModCP(AdditionalServicesModel objAdditionalServices, AuditRequest audit)
        {
            bool blnResponseOK = true;
            Models.TemplateInteractionModel oDataTemplateInteraction = new TemplateInteractionModel();
            CommonServicesController obj = new CommonServicesController();
            try
            {
                
                oDataTemplateInteraction = obj.GetInfoInteractionTemplate(objAdditionalServices.IdSession, objAdditionalServices.HidCaseId);
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SendEmailModCP ", "Error: " + ex.Message);

                objAdditionalServices.MessageCode = "E";
                objAdditionalServices.MessageLabel = ex.Message;
                return false;
            }

            string strAttached = "";
            string strSender = KEY.AppSettings("CorreoServicioAlCliente");
            string strDestination = objAdditionalServices.txtEmail;
            string strSubject = KEY.AppSettings("gConstAsuntoMail_ModCP");
            string strResponse;

            string strMessage;

            strMessage = "<html>";
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
            strMessage += obj.MailHeader("Solicitud Modificación de Cuotas de Servicios",
                oDataTemplateInteraction.X_INTER_15, DateTime.Now.ToShortDateString(), objAdditionalServices.FullName,
                objAdditionalServices.HidCaseId, objAdditionalServices.LegalAgent, oDataTemplateInteraction.X_CLARO_NUMBER,
                objAdditionalServices.TypeDocument,objAdditionalServices.DniRuc);
            strMessage += "</td></tr>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Los datos modificados son los siguientes:</td></tr>";
            strMessage += "<tr><td height='10'></td>";
            //INICIO : Acá va todo el cuerpo similar a los datos de la impresión de constancias;
            strMessage += "<tr>";
            strMessage += "<td align='center'>";
            strMessage += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strMessage +=
                "<tr><td width='180' class='Estilo2' height='22'>Servicio Comercial:</td><td class='Estilo1'>" +
                oDataTemplateInteraction.X_ADDRESS5 + "</td></tr>";
            strMessage += "<tr><td width='180' class='Estilo2' height='22'>Cargo Fijo S/.:</td><td class='Estilo1'>" +
                          oDataTemplateInteraction.X_INTER_16 + "</td></tr>";
            strMessage += "<tr><td width='180' class='Estilo2' height='22'>Número Periodos:</td><td class='Estilo1'>" +
                          oDataTemplateInteraction.X_INTER_2 + "</td></tr>";
            strMessage += "<tr><td width='180' class='Estilo2' height='22'></td><td class='Estilo1'></td></tr>";
            strMessage +=
                "<tr><td width='180' class='Estilo2' height='22'>Nuevo Cargo Fijo S/.:</td><td class='Estilo1'>" +
                oDataTemplateInteraction.X_ADJUSTMENT_AMOUNT + "</td></tr>";
            strMessage +=
                "<tr><td width='180' class='Estilo2' height='22'>Nuevo Número Periodos:</td><td class='Estilo1'>" +
                oDataTemplateInteraction.X_CHARGE_AMOUNT + "</td></tr>";
            //strMessage += "<tr><td width='180' class='Estilo2' height='22'>Acción Ejecutada:</td><td class='Estilo1'>Activación</td></tr>;

            strMessage += "</Table>";
            strMessage += "</td></tr>";
            //FIN: Acá va todo el cuerpo similar a los datos de la impresión de constancias
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage +=
                "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strMessage += "</table>";
            strMessage += "</body>";
            strMessage += "</html>";

            SendEmailResponseCommon objResponse = new SendEmailResponseCommon();
            try
            {
                
                SendEmailRequestCommon objRequest = new SendEmailRequestCommon()
                {
                    strTo = strDestination,
                    strSender = strSender,
                    strSubject = strSubject,
                    strMessage = strMessage,
                    audit = audit
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() =>
                {
                    return oServiceCommon.GetSendEmail(objRequest);
                });

                strResponse = SIACU.Transac.Service.Constants.Message_OK;



                string strOK = KEY.AppSettings("gConstKeyStrResultInsTipUSOK");
                if (strResponse == strOK)
                {
                    objAdditionalServices.MessageCode = SIACU.Transac.Service.Constants.Message_OK;
                    objAdditionalServices.MessageLabel = objAdditionalServices.MessageLabel +
                                                         ". En breves minutos se estará enviando un correo de notificación.";
                    blnResponseOK = true;
                }
                else
                {
                    objAdditionalServices.MessageCode = SIACU.Transac.Service.Constants.Letter_E;
                    objAdditionalServices.MessageLabel = objAdditionalServices.MessageLabel +
                                                         ". Pero no se pudo procesar el envío del correo de notificación.";
                    blnResponseOK = false;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + objAdditionalServices.IdSession, "Transaction: SendEmailModCP ", string.Format("Error EnviarCorreo : {0}", ex.Message));
            }
            
            return blnResponseOK;
        }


        private ValidateActDesServProgResponse GetValidateActDesServProg(AdditionalServicesModel model, string strIdTransaction, string strIpAplication, string strApplication, string strCodActDesactService, out string strCodError, out string strDesError)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(model.IdSession);

            ValidateActDesServProgResponse objResponse = new ValidateActDesServProgResponse();
            ValidateActDesServProgRequest objRequest = new ValidateActDesServProgRequest();

            objRequest.StrIdTransaction = strIdTransaction;
            objRequest.StrIpAplication = strIpAplication;
            objRequest.StrAplication = strApplication;
            objRequest.StrMsisdn = model.StrPhone;
            objRequest.StrCoId = model.StrCodId;
            objRequest.StrCoSer = model.StrCodSer;
            objRequest.StrTypeReg = model.StrStatus;
            objRequest.StrCodServ = strCodActDesactService;
            objRequest.audit = audit;

            objResponse = Claro.Web.Logging.ExecuteMethod<ValidateActDesServProgResponse>(() =>
                {
                    return oServicePostpaid.GetValidateActDesServProg(objRequest);
                });

            strCodError = objResponse.StrCodError;
            strDesError = objResponse.StrDesResponse;

            return objResponse;
        }
        private ServiceByContractResponse GetServiceByContract(ServiceByContractRequest objRequest)
        {
            ServiceByContractResponse objResponse = new ServiceByContractResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod<ServiceByContractResponse>(() =>
            {
                return oServicePostpaid.GetServiceByContract(objRequest);
            });
            return objResponse;
        }
        private UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest)
        {
            UpdatexInter30Response objResponse = new UpdatexInter30Response();
            objResponse = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() =>
            {
                return oServiceCommon.GetUpdatexInter30(objRequest);
            });
            return objResponse;
        }
        private ActDesServProgResponse ActDesServProg(ActDesServProgRequest objRequest)
        {
            ActDesServProgResponse objResponse = new ActDesServProgResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod<ActDesServProgResponse>(() =>
            {
                return oServicePostpaid.GetActDesServProg(objRequest);
            });
            return objResponse;
        }
        private TypeTransactionBRMSResponse ConsumeBRMS(string strCodSubClass, string strTransaction, string strRetention, string strIdentifier, out string strError,  PostTransacService.AuditRequest audit)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            TypeTransactionBRMSRequest objRequest = new TypeTransactionBRMSRequest()
            {
                StrIdentifier = strIdentifier,
                StrOperationCodSubClass = strCodSubClass,
                StrRetention = strRetention,
                StrTransactionM = strTransaction,
                audit = audit
            };

            objResponse = Claro.Web.Logging.ExecuteMethod<TypeTransactionBRMSResponse>(() =>
            {
                return oServicePostpaid.GetTypeTransactionBRMS(objRequest);
            });
            strError = objResponse.StrError;
            return objResponse;
        }
        private RegisterServiceCommercialResponse RegisterServiceCommercial(string strUserApp,string strCodRandom,string strTypeActivation, AdditionalServicesModel objAdditionalServices, AuditRequest audit)
        {
            RegisterServiceCommercialRequest objRequest = new RegisterServiceCommercialRequest()
            {
                StrCoId = objAdditionalServices.HidContract,
                StrCodServ = objAdditionalServices.HidCodService,
                StrIdTransaction = strCodRandom,
                StrSystem = strUserApp,
                StrTypeActivation = strTypeActivation,
                StrUser = objAdditionalServices.UserLogin,
                audit = audit
            };
            RegisterServiceCommercialResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<CommonTransacService.RegisterServiceCommercialResponse>(
                    () =>
                    {
                        return oServiceCommon.GetRegisterServiceCommercial(objRequest);
                    });
            return objResponse;
        }
        private UpdateXInter29ResponseCommon ModifyNotes(UpdateXInter29RequestCommon objRequest)
        {
            UpdateXInter29ResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<UpdateXInter29ResponseCommon>(
                () =>
                {
                    return oServiceCommon.UpdateXInter29(objRequest);
                });
            return objResponse;
        }
        private SaveAuditResponseCommon SaveAuddit(SaveAuditRequestCommon objRequest)
        {
            SaveAuditResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() =>
            {
                return oServiceCommon.SaveAudit(objRequest);
            });
            return objResponse;
        }
        private ModifyServiceQuotAmountResponse GetModifyServiceQuotAmount(AdditionalServicesModel objAdditionalServices, PostTransacService.AuditRequest audit)
        {
            ModifyServiceQuotAmountRequest objRequest = new ModifyServiceQuotAmountRequest()
            {
                IntCodId = Functions.CheckInt(objAdditionalServices.HidCodId_Contract),
                IntSnCode = Functions.CheckInt(objAdditionalServices.HidSnCode),
                IntSpCode = Functions.CheckInt(objAdditionalServices.HidSpCode),
                DCost = Functions.CheckDbl(objAdditionalServices.HidQoutMod),
                IntPeriod = Functions.CheckInt(objAdditionalServices.HidPeriodMod),
                audit = audit
            };
            ModifyServiceQuotAmountResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<ModifyServiceQuotAmountResponse>(
                    () =>
                    {
                        return oServicePostpaid.GetModifyServiceQuotAmount(objRequest);
                    });
            return objResponse;
        }
        private ConsultServiceResponse GetConsultService(int intCodid, int intCodServ, PostTransacService.AuditRequest audit)
        {
            ConsultServiceRequest objRequest = new ConsultServiceRequest()
            {
                CodId = intCodid,
                CodServ = intCodServ,
                audit = audit
            };
            ConsultServiceResponse objResponse = Claro.Web.Logging.ExecuteMethod<ConsultServiceResponse>(() =>
            {
                return oServicePostpaid.GetConsultService(objRequest);
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
        private void ValidateUserBSCS(AdditionalServicesModel objAdditionalServices, PostTransacService.AuditRequest audit)
        {
            int intResult;
            PostTransacService.UserExistsBSCSResponse obj = new PostTransacService.UserExistsBSCSResponse();
            try
            {
                obj = GetUserExistsBSCS(objAdditionalServices, audit);
                intResult = obj.Result;
                objAdditionalServices.HidStateUserBSCS = Functions.CheckStr(intResult);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalServices.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

        }
        private PostTransacService.UserExistsBSCSResponse GetUserExistsBSCS(AdditionalServicesModel objAdditionalServices, PostTransacService.AuditRequest audit)
        {
            PostTransacService.UserExistsBSCSRequest objRequest = new PostTransacService.UserExistsBSCSRequest()
            {
                Users = objAdditionalServices.UserLogin,
                audit = audit
            };
            PostTransacService.UserExistsBSCSResponse objResponse = Claro.Web.Logging
                .ExecuteMethod<PostTransacService.UserExistsBSCSResponse>(() =>
                {
                    return oServicePostpaid.GetUserExistsBSCS(objRequest);
                });
            return objResponse;
        }
        private PostTransacService.ApprovalBusinessCreditLimitResponse GetApprovalBusinessCreditLimit(AdditionalServicesModel objAdditionalServices, string strAccount,int intContract,int intService, PostTransacService.AuditRequest audit)
        {
            PostTransacService.ApprovalBusinessCreditLimitRequest objRequest = new PostTransacService.ApprovalBusinessCreditLimitRequest()
            {
                Account = strAccount,
                Contract = intContract,
                Service = intService,
                audit = audit
            };

            PostTransacService.ApprovalBusinessCreditLimitResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<PostTransacService.ApprovalBusinessCreditLimitResponse>(() =>
                {
                    return oServicePostpaid.GetApprovalBusinessCreditLimitBusinessAccount(objRequest);
                });
            return objResponse;
        }
        private ContractByPhoneNumberResponseCommon GetContractByPhoneNumber(AdditionalServicesModel objAdditionalServices, AuditRequest audit)
        {
            ContractByPhoneNumberRequestCommon objRequest = new ContractByPhoneNumberRequestCommon()
            {
                User = objAdditionalServices.UserLogin,
                PhoneNumber = objAdditionalServices.lblPhoneNumber,
                System = CONSTANTADDITIONALSERVICEPOSTPAID.strSystemSIACPO,
                audit = audit
            };
            ContractByPhoneNumberResponseCommon objResponse = Claro.Web.Logging
                .ExecuteMethod<CommonTransacService.ContractByPhoneNumberResponseCommon>(() =>
                {
                    return oServiceCommon.GetContractByPhoneNumber(objRequest);
                });
            return objResponse;
        }
        private TypificationResponse GetTypificationByTransaction(string strTransactionName, CommonTransacService.AuditRequest audit)
        {
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


        [HttpPost]
        public JsonResult GetServivesContract(Models.Postpaid.AdditionalServicesModel oModel) 
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(oModel.IdSession);

            try
            {
                //oModel.HidContract = "80";
                if (Request.IsAjaxRequest())
                {
                    LoadServicesByContract(oModel, audit);
                     
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return Json(new { data = gListServiceByContract });
        }
        public JsonResult GetConsumeBRMS(string IdSession , string strCodSubClass, string strTransaction, string strRetention, string strIdentifier)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(IdSession);
            TypeTransactionBRMSRequest objRequest = new TypeTransactionBRMSRequest()
            {
                StrIdentifier = strIdentifier,
                StrOperationCodSubClass = strCodSubClass,
                StrRetention = strRetention,
                StrTransactionM = strTransaction,
                audit = audit
            };

            TypeTransactionBRMSResponse objResponse = Claro.Web.Logging.ExecuteMethod<TypeTransactionBRMSResponse>(() =>
            {
                return oServicePostpaid.GetTypeTransactionBRMS(objRequest);
            });

            return Json(new { data = objResponse.StrResult});
        }
    }
}