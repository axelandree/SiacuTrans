using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using KEY = Claro.ConfigurationManager;
using System.Collections;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Claro.SIACU.Transac.Service;
using Claro.Web;
using Helper = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using System.Xml.Xsl;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class ConfigurationIPController : CommonServicesController
    {
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private static string intNroOST = string.Empty;
        //
        // GET: /Transactions/ConfigurationIP/
        public ActionResult ConfigurationIP()
        {
            var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "ConfigurationIP", "ConfigurationIP - HFC", "ConfigurationIP");
            Claro.Web.Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
            return PartialView();
        }

        [HttpPost]
        public JsonResult ConfigurationIPSave(Model.HFC.ConfigurationIPModel oModel)
        {
            
            bool bResult = false;
 
            oModel.strFlagContingencia = KEY.AppSettings("gConstContingenciaClarify_SIACU", "0");

            FixedTransacService.ValidateCustomerIdResponse oValidateCustomerIdResponse = new FixedTransacService.ValidateCustomerIdResponse();
            FixedTransacService.CustomerResponse oCustomerResponse = new FixedTransacService.CustomerResponse();
 
            List<string> strInteractionId = new List<string>();
            FixedTransacService.ConfigurationIPResponse oConfigurationIPResponse = new ConfigurationIPResponse();


            try
            {

                oModel.CodeTipification = KEY.AppSettings("TRANSACCION_CONFIGURACION_IP", "");
                oModel.strTransaction = KEY.AppSettings("CONFIGURACION_SERVICIOS","");
                if (oModel.strServices == "-1") oModel.strServices = null;
                //oModel.strTipoTransaccion = KEY.AppSettings("gstrTransaccionHFCConfigIP", "TRANSACCION_GEN_ORD_VISITA_MANT_HFC");

                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "CodeTipification: " + oModel.CodeTipification);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strTransaction: " + oModel.strTransaction);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strServices: " + oModel.strServices);

                string strMessageExito = Functions.GetValueFromConfigFile("gConstMsgOVExito", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg", ""));

                oConfigurationIPResponse = Transaction(oModel);
                oConfigurationIPResponse.strDescripcionRespuesta = strMessageExito;
                

                
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strCodigoRespuesta: " + oConfigurationIPResponse.strCodigoRespuesta);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strDescripcionRespuesta: " + oConfigurationIPResponse.strDescripcionRespuesta);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strIdInteraccion: " + oConfigurationIPResponse.strIdInteraccion);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strNroSOT: " + oConfigurationIPResponse.strNroSOT);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strRutaConstancia: " + oConfigurationIPResponse.strRutaConstacy);

                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strCodigoRespuesta: " + oConfigurationIPResponse.oResponseStatus.strCodigoRespuesta);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strDescripcionRespuesta: " + oConfigurationIPResponse.oResponseStatus.strDescripcionRespuesta);
                RegisterLog(oModel.IdSession, "ConfigurationIPSave", "strUbicacionError: " + oConfigurationIPResponse.oResponseStatus.strUbicacionError);
                 
 
                //if (oConfigurationIPResponse.strNroSOT!=null)
                //{
                //    if (oConfigurationIPResponse.strNroSOT.Length > 0)
                //    {
                //        oConfigurationIPResponse.oResponseStatus.strUbicacionError = strMessageExito;
                //    }
                //    else {
                //        oConfigurationIPResponse.oResponseStatus.strUbicacionError = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                //    }
                //}
                //else {
                //    oConfigurationIPResponse.oResponseStatus.strUbicacionError = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                //}
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + oModel.IdSession, "Transaction: Generacion de Constancia  Configuracion IP", "RUTA : " + ex.Message);
                 
            }


            //if (oConfigurationIPResponse.oResponseStatus != null)
            //{
            //    if (oConfigurationIPResponse.strCodigoRespuesta == ConstantsHFC.numeroCuatro.ToString())
            //    {
            //        if (!oConfigurationIPResponse.oResponseStatus.strUbicacionError.ToUpper().Contains("EXISTE"))
            //        {
            //            oConfigurationIPResponse.oResponseStatus.strUbicacionError = oConfigurationIPResponse.oResponseStatus.strDescripcionRespuesta;
            //        }
            //    }
            //    else if(oConfigurationIPResponse.oResponseStatus.strCodigoRespuesta != "-2")
            //    {
            //        oConfigurationIPResponse.oResponseStatus.strUbicacionError = oConfigurationIPResponse.oResponseStatus.strDescripcionRespuesta;
            //    }
            //}
             
             

            return Json(oConfigurationIPResponse);
        }

        [HttpPost]
        public JsonResult LoadVariables(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            try
            {
                lstMessage.Add(Request.ServerVariables["SERVER_NAME"]);
                lstMessage.Add(Request.ServerVariables["LOCAL_ADDR"]);
                lstMessage.Add(Request.UserHostName);
                lstMessage.Add(KEY.AppSettings("strConstCodigoASolicitudDelCliente"));

                lstMessage.Add(KEY.AppSettings("gConstKeyTituloTranGenVisTecMan")); // Titulo de la Pagina
                lstMessage.Add(KEY.AppSettings("gConstKeyPreguntaGenVisTecMan")); //Mensaje de Confirmacion

                lstMessage.Add(Functions.GetValueFromConfigFile("gConstKeyIngreseCorreo",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstKeyCorreoIncorrecto",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstMsgSelCacDac",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstMsgTlfDSNum",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstMsgNVAgendamiento",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                lstMessage.Add(Functions.GetValueFromConfigFile("gConstMsgOVExito",KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg") ));

                lstMessage.Add(KEY.AppSettings("gConstKeyPreguntaGenConfIP_IPFIJA")); //Mensaje de Confirmacion
                lstMessage.Add(KEY.AppSettings("gConstKeyPreguntaGenConfIP_PUERTO25")); //Mensaje de Confirmacion
                lstMessage.Add(KEY.AppSettings("IPFIJA")); //Mensaje de Confirmacion
                lstMessage.Add(KEY.AppSettings("PUERTO25")); //Mensaje de Confirmacion
                lstMessage.Add(KEY.AppSettings("strServidorLeerPDF"));

                lstMessage.Add(Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                       ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")));
                lstMessage.Add(KEY.AppSettings("strMontoInstalacionConfiguracionIP"));
                
                lstMessage.Add(KEY.AppSettings("Planes_EVO"));
                lstMessage.Add(KEY.AppSettings("Planes_Full_HD_Digital_30MB"));
                lstMessage.Add(KEY.AppSettings("gConstKeyPlanActualCliente"));
                
                lstMessage.Add(KEY.AppSettings("strEstadoContratoInactivo"));
                lstMessage.Add(KEY.AppSettings("strEstadoContratoSuspendido"));
                lstMessage.Add(KEY.AppSettings("strEstadoContratoReservado"));

                lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoInactivo"));
                lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoSuspendido"));
                lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoReservado"));
                 lstMessage.Add(KEY.AppSettings("strMensajeNoAplicaFTTH")); //RONALDRR. - INICIO
                lstMessage.Add(KEY.AppSettings("strPlanoFTTH")); //RONALDRR. - FIN                   

                var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "ConfigurationIP", "LoadVariables", "Cargar de Variables");
                Claro.Web.Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);


                
            }
            catch (Exception ex)
            {
                var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "ConfigurationIP", "LoadVariables", ex.Message);
                Claro.Web.Logging.Error("IdSession: " + "", "Transaccion: " + "", msg1);
                 
            }
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductDetailt(string strIdSession, string vCustomerId, string vContratId)
        {

            var msg1 = string.Format("Controller: {0},Metodo: {1}, Pametros: {2}", "ConfigurationIP", "GetProductDetailt", "Session: " + strIdSession + "| CustomerId: " + vCustomerId + "|ContratoId" + vContratId);
            Claro.Web.Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            FixedTransacService.BEDecoServicesResponseFixed objBEDecoServicesResponseFixed = new BEDecoServicesResponseFixed();
            FixedTransacService.BEDecoServicesRequestFixed objBEDecoServicesRequestFixed = new BEDecoServicesRequestFixed();

            try
            {
                objBEDecoServicesRequestFixed.audit = _audit;
                objBEDecoServicesRequestFixed.vCusID = vCustomerId;
                objBEDecoServicesRequestFixed.vCoID = vContratId;


                objBEDecoServicesResponseFixed = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEDecoServicesResponseFixed>(() =>
                {
                    return _oServiceFixed.GetServicesDTH(objBEDecoServicesRequestFixed);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
                msg1 = string.Format("Controller: {0},Metodo: {1}, Error: {2}", "ConfigurationIP", "GetProductDetailt", ex.Message);
                Claro.Web.Logging.Error("IdSession: " + "", "Transaccion: " + "", msg1);
              //  throw new Exception(ex.Message);
            }
            return Json(new { data = objBEDecoServicesResponseFixed.ListDecoServices });
        }

        [HttpPost]
        public JsonResult GetJobTypeConfIp(string strIdSession)
        {
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.JobTypesConfigIPRequest objJobTypesRequestHfc = new FixedTransacService.JobTypesConfigIPRequest();
            List<FixedTransacService.JobTypesConfigIPResponse> objJobTypesResponseHfc = new List<FixedTransacService.JobTypesConfigIPResponse>();
            try
            {
                objJobTypesRequestHfc.audit = _audit;
                objJobTypesResponseHfc = Claro.Web.Logging.ExecuteMethod<List<FixedTransacService.JobTypesConfigIPResponse>>(() =>
                {
                    return _oServiceFixed.GetJobTypesConfigIP(objJobTypesRequestHfc);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
             //   throw new Exception(ex.Message);
            }
            return Json(new { data = objJobTypesResponseHfc });
        }

        [HttpPost]
        public JsonResult GetTypeConfIp(string strIdSession, string strJobType)
        {
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.TypeConfigIpRequest objTypeConfigIpRequest = new FixedTransacService.TypeConfigIpRequest();
            List<FixedTransacService.TypeConfigIpResponse> LstTypeConfigIpResponse = new List<FixedTransacService.TypeConfigIpResponse>();

            Claro.Web.Logging.Info("Session:" + strIdSession, "Configuracion IP -  Transaction: GetTypeConfIp ", "an_tiptrabajo : " + strJobType);

            try
            {
                objTypeConfigIpRequest.audit = _audit;
                objTypeConfigIpRequest.an_tiptrabajo = Convert.ToInt(strJobType);
                LstTypeConfigIpResponse = Claro.Web.Logging.ExecuteMethod<List<FixedTransacService.TypeConfigIpResponse>>(() =>
                {
                    return _oServiceFixed.GetTypeConfIP(objTypeConfigIpRequest);
                });

                if (LstTypeConfigIpResponse != null)
                {
                    Claro.Web.Logging.Info("Session:" + strIdSession, "Configuracion IP -  Transaction: GetTypeConfIp ", "Listado : " + LstTypeConfigIpResponse.Count);
                }
                else {
                    Claro.Web.Logging.Info("Session:" + strIdSession, "Configuracion IP -  Transaction: GetTypeConfIp ", "Listado : NULL");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
              //  throw new Exception(ex.Message);
            }
            return Json(new { data = LstTypeConfigIpResponse });
        }
        
        [HttpPost]
        public JsonResult GetBranchCustomer(string strIdSession, string strCustomerId)
        {
            Claro.Web.Logging.Info("Session:" + strIdSession, "Transaction: GetBranchCustomer ", "an_customer_id: " + strCustomerId);
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            List<FixedTransacService.BranchCustomerResponse> LstBranchCustomerResponse = new List<FixedTransacService.BranchCustomerResponse>();
            FixedTransacService.BranchCustomerResquest objBranchCustomerResquest = new FixedTransacService.BranchCustomerResquest();
            try
            {
                objBranchCustomerResquest.audit = _audit;
                objBranchCustomerResquest.an_customer_id = Convert.ToInt(strCustomerId);
                LstBranchCustomerResponse = Claro.Web.Logging.ExecuteMethod<List<FixedTransacService.BranchCustomerResponse>>(() =>
                {
                    return _oServiceFixed.GetBranchCustomer(objBranchCustomerResquest);
                });

                Claro.Web.Logging.Info("Session:" + strIdSession, "Transaction: GetBranchCustomer ", "Listado : " + LstBranchCustomerResponse.Count);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
             //   throw new Exception(ex.Message);
            }
            return Json(new { data = LstBranchCustomerResponse });
        }

        [HttpPost]
        public JsonResult GetDataLine(string strIdSession, string vContratId)
        {

            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ConfigurationIPRequest objTypeConfigIpRequest = new FixedTransacService.ConfigurationIPRequest();
            FixedTransacService.ConfigurationIPResponse objTypeConfigIpResponse = new FixedTransacService.ConfigurationIPResponse();

            Claro.Web.Logging.Info(strIdSession, _audit.transaction, "GetConfigurationIPMegas HFC - IN" );

            try
            {
                objTypeConfigIpRequest.audit = _audit;
                objTypeConfigIpRequest.strId = vContratId;
                objTypeConfigIpResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetConfigurationIPMegas(objTypeConfigIpRequest);
                });

                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strflag : " + objTypeConfigIpResponse.strflag);
                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strCodigoRespuesta : " + objTypeConfigIpResponse.strCodigoRespuesta);
                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strDescripcionRespuesta : " + objTypeConfigIpResponse.strDescripcionRespuesta);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(strIdSession, _audit.transaction, "GetConfigurationIPMegas HFC - OUT");
            
            return Json(new { data = objTypeConfigIpResponse });
        }

        

        private FixedTransacService.ConfigurationIPResponse Transaction(Model.HFC.ConfigurationIPModel model)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(model.IdSession);
            var objInteractionModel = new Model.InteractionModel();
            var oPlantillaDat = new Model.TemplateInteractionModel();
           
            var listInteraction = new List<string>();
            FixedTransacService.GenericSotResponse oGenericSotResponse = new FixedTransacService.GenericSotResponse();
            FixedTransacService.ConfigurationIPRequest oConfigurationIPRequest = new FixedTransacService.ConfigurationIPRequest();
            FixedTransacService.ConfigurationIPResponse oConfigurationIPResponse = new FixedTransacService.ConfigurationIPResponse();
            FixedTransacService.Interaction oInteraction = new FixedTransacService.Interaction();
            FixedTransacService.InsertInteractHFCRequest oInsertInteractHFCRequest = new FixedTransacService.InsertInteractHFCRequest();
          
            try
            {
                var blnEjectTransaction = true;
                var strUserSession = string.Empty;
                var strUserAplication = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                var strPassUser = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
               // oPlantillaDat = GetDatTemplateInteractionConfigurationIP(model);
 
                oInteraction = DatInteraction(model);
               


                oInsertInteractHFCRequest.Interaction = oInteraction;
                oConfigurationIPRequest.oInsertTemplateInteraction = GetDatTemplateInteractionConfigurationIP(model);
                oConfigurationIPRequest.oInsertInteractHFCRequest = oInsertInteractHFCRequest;
                oConfigurationIPRequest.strMsisdn = model.strMsisdn; // KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId; //model.strMsisdn; //"84606060";
                oConfigurationIPRequest.strFlagContingencia = model.strFlagContingencia; //"1";

                oConfigurationIPRequest.strTipoTransaccion = model.strTipoTransaccion; // "ConfiguracionIP"; 
                oConfigurationIPRequest.strCorreoDestinatario = model.strEmail; //"valdezkj@globalhitss.com";
                oConfigurationIPRequest.strFormatoConstancia =   string.Empty;
                oConfigurationIPRequest.strId = "0";
                oConfigurationIPRequest.strTipoVia = string.Empty;
                oConfigurationIPRequest.strNombreVia = string.Empty;
                oConfigurationIPRequest.strNumeroVia = string.Empty;
                 

                FixedTransacService.Customer oCustomer = new FixedTransacService.Customer();
                oCustomer.Account = model.strAccount;
                oCustomer.FLAG_REGISTRADO = model.strFLAG_REGISTRADO;
                oCustomer.ContactCode = oInteraction.OBJID_CONTACTO;
                oCustomer.User = model.strUser;
                oCustomer.Name = model.strName;
                oCustomer.LastName = model.strLastName;
                oCustomer.BusinessName = model.strBusinessName; 
                oCustomer.DocumentType = model.strDocumentType;
                oCustomer.DocumentNumber = model.strDocumentNumber;
                oCustomer.Address = model.strAddress;
                oCustomer.District = model.strDistrict;
                oCustomer.Departament = model.strDepartament;
                oCustomer.Province = model.strProvince;
                oCustomer.Modality = model.strModality;
                oCustomer.ContractID = model.strContractId; //"666";
                oCustomer.CustomerID = model.strCustomerId; //"666";


                oConfigurationIPRequest.strJobType = model.strJobTypes; //Tipo Trabajo
                oConfigurationIPRequest.strCodSolot = model.strCodSolot; // "19553545"; //?
                oConfigurationIPRequest.strTypeServices = model.strServices; //Tipo Servicio
                oConfigurationIPRequest.strCodinssrv = model.strCodinssrv;// "277783

                oConfigurationIPRequest.oCustomer = oCustomer;
                var strNroTelephone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId;


                oConfigurationIPRequest.strIpCliente = Request.ServerVariables["LOCAL_ADDR"];
                oConfigurationIPRequest.strApplicationName = App_Code.Common.GetApplicationName();
                oConfigurationIPRequest.strWeb = KEY.AppSettings("strWEB");
                oConfigurationIPRequest.audit = audit;
                oConfigurationIPRequest.strFormatoConstancia = model.strFormatConstancy;

                oConfigurationIPResponse = _oServiceFixed.ConfigurationServicesSave(oConfigurationIPRequest);
               

            }
            catch (Exception ex)
            {
                Logging.Info("Session: 123456789", "Transaction: Page_Load ", "Error: " + ex.Message);
            }
            return oConfigurationIPResponse;
        }

        private FixedTransacService.Interaction DatInteraction(Model.HFC.ConfigurationIPModel model)
        {
            var objInteractionModel = new FixedTransacService.Interaction();
            //tipificacion
            var tipification = GetTypificationHFC(model.IdSession, model.CodeTipification);

            tipification.ToList().ForEach(x =>
            {
                objInteractionModel.TIPO = x.Type;
                objInteractionModel.CLASE = x.Class;
                objInteractionModel.SUBCLASE = x.SubClass;
                objInteractionModel.ID_INTERACCION = x.InteractionCode;
                objInteractionModel.TIPO_CODIGO = x.TypeCode;
                objInteractionModel.CLASE_CODIGO = x.ClassCode;
                objInteractionModel.SUBCLASE_CODIGO = x.SubClassCode;
            });

            string strFlgRegistrado = ConstantsHFC.strUno;
            //ObtenerCliente
            var phone = KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId;
            CustomerResponse objCustomerResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(model.IdSession);
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

            objInteractionModel.OBJID_CONTACTO = objCustomerResponse.Customer.ContactCode;
            objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
            objInteractionModel.TELEFONO = KEY.AppSettings("gConstKeyCustomerInteract") + "" + model.strCustomerId;
            objInteractionModel.TIPO = objInteractionModel.TIPO;
            objInteractionModel.CLASE = objInteractionModel.CLASE;
            objInteractionModel.SUBCLASE = objInteractionModel.SUBCLASE;
            objInteractionModel.TIPO_INTER = KEY.AppSettings("AtencionDefault");
            objInteractionModel.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
            objInteractionModel.RESULTADO = KEY.AppSettings("Ninguno");
            objInteractionModel.HECHO_EN_UNO = ConstantsHFC.strCero;
            objInteractionModel.NOTAS = model.strNote;
            objInteractionModel.FLAG_CASO = ConstantsHFC.strCero;
            objInteractionModel.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
            objInteractionModel.CONTRATO = model.strContractId;
            objInteractionModel.PLANO = model.strPlan;
            objInteractionModel.AGENTE =  model.strUser;
            objInteractionModel.CUENTA = model.strAccount;
            objInteractionModel.OBJID_SITE = model.strSiteCode;
            return objInteractionModel;
        }
        private FixedTransacService.InsertTemplateInteraction GetDatTemplateInteractionConfigurationIP(Model.HFC.ConfigurationIPModel oModel)
        {
            var oPlantCampDat = new FixedTransacService.InsertTemplateInteraction();

  

            oPlantCampDat._X_MARITAL_STATUS = DateTime.Now.ToShortDateString();
            oPlantCampDat._X_INTER_1 = ConstantsHFC.strCero;
            oPlantCampDat._X_INTER_7 = oModel.strNameComplete;
            oPlantCampDat._X_INTER_2 = oModel.strDepartament;
            oPlantCampDat._X_INTER_4 = oModel.strProvince;
            oPlantCampDat._X_INTER_6 = oModel.strDistrict;
            oPlantCampDat._X_INTER_15 = oModel.strDescCacDac;
            oPlantCampDat._X_INTER_16 = oModel.strNroSot;
            oPlantCampDat._X_INTER_29 = oModel.strNroSot;
            oPlantCampDat._X_INTER_18 = oModel.strTelephone;
            oPlantCampDat._X_DISTRICT = oModel.strLegalBuilding;
            oPlantCampDat._X_INTER_17 = (string.IsNullOrEmpty(oModel.strJobTypes) || oModel.strJobTypes == "-1") ? "" : oModel.strDescJobType;
            oPlantCampDat._X_INTER_19 = (string.IsNullOrEmpty(oModel.strMotiveSot) || oModel.strMotiveSot == "-1") ? "" : oModel.strDescMotiveSot;
            oPlantCampDat._X_INTER_21 = (string.IsNullOrEmpty(oModel.strServices) || oModel.strServices == "-1") ? "" : oModel.strDescServices;
            oPlantCampDat._X_INTER_20 = DateTime.Now.ToShortDateString();
            oPlantCampDat._X_BIRTHDAY = Convert.ToDate(oModel.strBIRTHDAY);
            oPlantCampDat._X_INTER_30 = oModel.strNote;
            oPlantCampDat._X_FIRST_NAME = oModel.strFirtName;
            oPlantCampDat._X_LAST_NAME = oModel.strLastName;
            oPlantCampDat._X_DOCUMENT_NUMBER = oModel.strDocumentNumber;
       
            if (oModel.bSendMail)
            {
                if (!string.IsNullOrEmpty(oModel.strEmail))
                {
                    oPlantCampDat._X_REGISTRATION_REASON = oModel.strEmail;
                    oPlantCampDat._X_INTER_5 = ConstantsHFC.strUno; //Flag Email
                }
                else
                {
                    oPlantCampDat._X_REGISTRATION_REASON = string.Empty;
                    oPlantCampDat._X_INTER_5 = ConstantsHFC.strCero; //Flag Email
                }
            }

            oPlantCampDat._X_CLAROLOCAL6 = oPlantCampDat._X_INTER_17;//oModel.strTransaction + " - " + oPlantCampDat._X_INTER_17;
            oPlantCampDat._X_CLARO_NUMBER = oModel.strContractId;
            oPlantCampDat._X_TYPE_DOCUMENT = oModel.strDocumentType;
            oPlantCampDat._X_ADDRESS5 = oModel.strAddress;
            oPlantCampDat._X_CITY = oModel.strUbigeoInst;

            return oPlantCampDat;
        }

        [HttpPost]
        public JsonResult GetCommercialService(string strCoId)
        {
            var objLstCommercialService = new List<Helper.CommercialServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");

            var objCommercialServicesRequest = new CommercialServicesRequest
            {
                audit = objAuditRequest,
                StrCoId = strCoId
            };
            try
            {
                var objCommercialServicesResponse = Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommercialService(objCommercialServicesRequest); });
                if (objCommercialServicesResponse.LstCommercialServices.Count > 0)
                {
                    var lstTemp = objCommercialServicesResponse.LstCommercialServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Helper.CommercialServiceHP
                        {
                            DE_SER = item.DE_SER ?? "",
                            DE_EXCL = item.DE_EXCL ?? "",
                            BLOQ_ACT = item.BLOQ_ACT ?? "",
                            BLOQ_DES = item.BLOQ_DES ?? "",
                            CARGOFIJO = item.CARGOFIJO ?? "",
                            CODSERPVU = item.CODSERPVU ?? "",
                            COSTOPVU = item.COSTOPVU ?? "",
                            CO_EXCL = item.CO_EXCL ?? "",
                            CO_SER = item.CO_SER ?? "",
                            CUOTA = item.CUOTA ?? "",
                            DESCOSER = item.DESCOSER ?? "",
                            DESCUENTO = item.DESCUENTO,
                            DE_GRP = item.DE_GRP ?? "",
                            ESTADO = item.ESTADO ?? "",
                            NO_GRP = item.NO_GRP ?? "",
                            NO_SER = item.NO_SER ?? "",
                            PERIODOS = item.PERIODOS ?? "",
                            SNCODE = item.SNCODE ?? "",
                            SPCODE = item.SPCODE ?? "",
                            SUSCRIP = item.SUSCRIP ?? "",
                            TIPOSERVICIO = item.TIPOSERVICIO ?? "",
                            TIPO_SERVICIO = item.TIPO_SERVICIO ?? "",
                            VALIDO_DESDE = item.VALIDO_DESDE ?? "",
                            VALORPVU = item.VALORPVU ?? ""
                        };

                        objLstCommercialService.Add(objTemp);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialServicesRequest.audit.transaction, ex.Message);
            }

            return Json(objLstCommercialService,JsonRequestBehavior.AllowGet);
        }

        private void RegisterLog(string IdSession, string Method, string Message)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "ConfigurationIP", Method + ": " + Message);

        }

        [HttpPost]
        public JsonResult GetConstanceTemplateLabels()
        {
            List<string> Labels = new List<string>();
            if (Request.IsAjaxRequest())
            {
                Labels = CommonServicesController.GetXmlToString(Common.GetApplicationRoute() + "/DataTransac/HFCConfigurationIPConstance.xml");
                
            }
            return Json(new { data = Labels });
        }
        [HttpPost]
        public JsonResult CreateConstanceXmlString(Dictionary<string, string> dict)
        {
            string resultado = String.Empty;
            try
            {
                if (Request.IsAjaxRequest()) {
                    XsltArgumentList xslArg = new XsltArgumentList();
                    foreach (var item in dict)
                    {
                        xslArg.AddParam(item.Key, "", item.Value);
                    }
                    StringBuilder xml = new StringBuilder();
                    XmlWriter xmlWriter = XmlWriter.Create(xml);
                    string filename = Common.GetApplicationRoute() + "/DataTransac/HFCConfigurationIPConstance.xml";
                    string stylesheet = Common.GetApplicationRoute() + "/DataTransac/HFCConfigurationIPConstance.xslt";
                    XPathDocument doc = new XPathDocument(filename);

                    //XslTransform xslt = new XslTransform();
                    XslCompiledTransform xslCT = new XslCompiledTransform();
                    xslCT.Load(stylesheet);
                    xslCT.Transform(doc, xslArg, xmlWriter, null);
                    xmlWriter.Close();
                    string myString = xml.ToString();
                    byte[] myByteArray = System.Text.Encoding.UTF8.GetBytes(myString);
                    MemoryStream ms = new MemoryStream(myByteArray);
                    StreamReader sr = new StreamReader(ms);
                    var reader = XmlReader.Create(sr);
                    reader.MoveToContent();
                    var inputXml = XDocument.ReadFrom(reader);
                    resultado = inputXml.ToString();
                }
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("HFC", "CreateConstanceXmlString- ConfigurationIP", ex.Message);
            }
            return Json(new { data = resultado });
        }

      
        public JsonResult GetConfigurationIPMegas(string strIdSession, string Co_Id)
        {
            FixedTransacService.AuditRequest _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ConfigurationIPRequest objTypeConfigIpRequest = new FixedTransacService.ConfigurationIPRequest();
            FixedTransacService.ConfigurationIPResponse objTypeConfigIpResponse = new FixedTransacService.ConfigurationIPResponse();

            Claro.Web.Logging.Info(strIdSession, _audit.transaction, "GetConfigurationIPMegas HFC - IN" );

            try
            {
                objTypeConfigIpRequest.audit = _audit;
                objTypeConfigIpRequest.strId = Co_Id;
                objTypeConfigIpResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetConfigurationIPMegas(objTypeConfigIpRequest);
                });

                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strflag : " + objTypeConfigIpResponse.strflag);
                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strCodigoRespuesta : " + objTypeConfigIpResponse.strCodigoRespuesta);
                Claro.Web.Logging.Info(strIdSession, _audit.transaction, "strDescripcionRespuesta : " + objTypeConfigIpResponse.strDescripcionRespuesta);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(strIdSession, _audit.transaction, "GetConfigurationIPMegas HFC - OUT");
            return Json(new { data = objTypeConfigIpResponse });
        }
	}
}