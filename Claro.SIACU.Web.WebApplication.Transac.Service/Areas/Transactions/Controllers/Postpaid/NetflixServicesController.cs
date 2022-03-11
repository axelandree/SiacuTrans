using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class NetflixServicesController : Controller
    {
        //
        // GET: /Transactions/NetflixServices/
        public ActionResult PostpaidNetflixServices()
        {
            Claro.Web.Logging.Info("PostpaidLoad", "NetflixServices", "Inicio - Load");
            Models.Postpaid.NetflixServicesModel oNX = new Models.Postpaid.NetflixServicesModel();
            oNX.strEstadoContratoInactivo = KEY.AppSettings("strEstadoContratoInactivo");
            oNX.strEstadoContratoSuspendido = KEY.AppSettings("strEstadoContratoSuspendido");
            oNX.strEstadoContratoReservado = KEY.AppSettings("strEstadoContratoReservado");
            oNX.strMsjEstadoContratoInactivo = KEY.AppSettings("strMsjNotificacionNX");
            oNX.strMsjServicioContrato = KEY.AppSettings("strMsjServicioContrato");
            oNX.strMsjEstadoContratoReservado = KEY.AppSettings("strMsjEstadoContratoReservado");
            ViewBag.oNx = oNX;
            Claro.Web.Logging.Info("PostpaidLoad", "NetflixServices", "Fin - Load");
            return View(oNX);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public JsonResult PageLoad(string strIdSession)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.Typification oTipificacion = null;
            string strMensaje = "";
            try
            {
                oTipificacion = cargaTipificacion(audit, ref strMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { data = oTipificacion });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oAudit"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        public CommonTransacService.Typification cargaTipificacion(CommonTransacService.AuditRequest oAudit, ref string strMensaje)
        {
            CommonTransacService.CommonTransacServiceClient oCommonService = null;
            CommonTransacService.TypificationRequest objTypificationRequest = null;
            CommonTransacService.Typification oTypification = null;
            try
            {
                objTypificationRequest = new CommonTransacService.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("strTransaccionNetflixServicesPostpaid");
                objTypificationRequest.audit = oAudit;
                oCommonService = new CommonTransacService.CommonTransacServiceClient();
                CommonTransacService.TypificationResponse objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.TypificationResponse>(() => { return oCommonService.GetTypification(objTypificationRequest); });
                if (objResponse == null)
                {
                    strMensaje = "No se cargo las tipificaciones";
                }
                else { oTypification = objResponse.ListTypification.First(); }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAudit.Session, oAudit.transaction, ex.Message);
                strMensaje = "No se cargo las tipificaciones";
            }
            return oTypification;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public JsonResult saveTransactionNetflixServices(NetflixServicesModel oModel)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            ServicesNXResponse oResponseServicesNetflix = null;
            ServicesNXMessageResponse oResponseMessage = null;
            responseAudit oresponseAudit = null;
            bool bregistro = false;
            string strResultado = string.Empty;
            string strIdInteraccion = string.Empty;    
            int intFlag = -1;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : saveTransactionNetflixServices");
            try
            {
                oResponseServicesNetflix = saveNetflixServices(oModel);
                if ((oResponseServicesNetflix != null) && (oResponseServicesNetflix.MessageResponse != null))
                {
                    oresponseAudit = oResponseServicesNetflix.MessageResponse.Body.responseAudit;

                    if (oresponseAudit.codigoRespuesta.Equals(Constants.ZeroNumber))
                    {
                        bregistro = saveInteraccionNetflixServices(oModel, ref strIdInteraccion);
                        if (bregistro)
                        {
                            if ((oModel.notificaEmail.Equals(Constants.ConstUno)) && (oModel.notificaSMS.Equals(Constants.ConstUno)))
                            {
                                strResultado = string.Format(ConfigurationManager.AppSettings("strEnvioLineaEmailNX"), oModel.linea, oModel.email);
                            }
                            else if ((oModel.notificaEmail.Equals(Constants.ZeroNumber)) && (oModel.notificaSMS.Equals(Constants.ConstUno)))
                            {
                                strResultado = string.Format(ConfigurationManager.AppSettings("strEnvioLineaNX"), oModel.linea);
                            }
                            else if ((oModel.notificaEmail.Equals(Constants.ConstUno)) && (oModel.notificaSMS.Equals(Constants.ZeroNumber)))
                            {
                                strResultado = string.Format(ConfigurationManager.AppSettings("strEnvioCorreoNX"), oModel.email);
                            }
                            intFlag = 0;
                        }
                        else
                        {
                            intFlag = 1;
                            strResultado = ConfigurationManager.AppSettings("strEnvioErrorInterNX");
                        }
                    }
                    else
                    {
                        intFlag = 1;
                    }
                }
                else
                {
                    intFlag = 1;
                    strResultado = ConfigurationManager.AppSettings("strEnvioErrorLinkNX");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, oModel.strIdTransaccion, ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : saveTransactionNetflixServices");
            return Json(new { data = strResultado, flag = intFlag, id = strIdInteraccion });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public ServicesNXResponse saveNetflixServices(NetflixServicesModel oModel)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            PostTransacService.PostTransacServiceClient oServicio = null;
            PostTransacService.AuditRequest oAuditRequest = null;
            ServicesNXResponse oResponseServicesNetflix = null;
            ServicesNXRequest oRequestServicesNetflix = null;
            string strEnviaCorreo = string.Empty;
            string strEnviaSMS = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : saveNetflixServices");
            try
            {
                oServicio = new PostTransacService.PostTransacServiceClient();

                strEnviaCorreo = oModel.notificaEmail == "1" ? "true" : "false";
                strEnviaSMS = oModel.notificaSMS == "1" ? "true" : "false";

                oRequestServicesNetflix = new ServicesNXRequest()
                {
                    MessageRequest = new ServicesNXMessageRequest()
                    {
                        Header = new ServicesNXHeaderRequest()
                        {
                            HeaderRequest = getHeaderRequest("enviarurlactivacion")
                        },
                        Body = new ServicesNXBodyRequest()
                        {
                            usuarioId = oModel.linea,
                            tipoProducto = "1",
                            correo = oModel.email,
                            correlatorId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            tipoUsuario = "M",
                            usuarioReg = ConfigurationManager.AppSettings("USRProcesoSU"),
                            enviaCorreo = strEnviaCorreo,
                            enviaSMS = strEnviaSMS,
                            listaOpcional = new List<listaOpcional>() { new listaOpcional() { clave = "", valor = "" } }
                        }
                    }
                };
                
                oAuditRequest = new PostTransacService.AuditRequest()
                {
                    applicationName = audit.applicationName,
                    ipAddress = audit.ipAddress,
                    Session = audit.Session,
                    transaction = audit.transaction,
                    userName = audit.userName
                };

                oResponseServicesNetflix = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.PostTransacService.ServicesNXResponse>(oModel.strIdSession, oModel.strIdTransaccion, () =>
                {
                    return oServicio.envioNotificacionRegistroNX(oModel.strIdSession, oModel.strIdTransaccion, oRequestServicesNetflix, oAuditRequest);
                });
               
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : saveChangeServiceAddress");
            return oResponseServicesNetflix;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public WebApplication.Transac.Service.PostTransacService.HeaderRequest getHeaderRequest(string operation)
        {
            return new WebApplication.Transac.Service.PostTransacService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("modulo"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp()//"172.19.84.170"//
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intCoId"></param>
        /// <returns></returns>
        public JsonResult validaServicioContratado(string strIdSession, string strIdTransaccion, int intCoId, string strPlataforma, string strServicioOne, string strPhone)
        {
            CommonTransacService.AuditRequest oAudit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            PostTransacService.PostTransacServiceClient oServicio = null;
            PostTransacService.parametroMovil oparametroMovil = null;
            bool booServicioContratado = false;
            string strResultado = "NO OK";
            Claro.Web.Logging.Info(oAudit.Session, oAudit.transaction, "Inicio Método : validaServicioContratado");
            try
            {
                oServicio = new PostTransacServiceClient();
                oparametroMovil = new parametroMovil()
                {
                    strCodigoServicio = ConfigurationManager.AppSettings("strSnCodeNXM"),
                    intCodigoContrato = intCoId,
                    strCodigoServicioOne = strServicioOne == null ? "" : strServicioOne,
                    strPlataforma = strPlataforma == null ? Constants.ASIS : strPlataforma,
                    strTelefono = strPhone,
                    audit = new AuditRequest()
                    {
                        applicationName = oAudit.applicationName,
                        ipAddress = oAudit.ipAddress,
                        Session = oAudit.Session,
                        transaction = oAudit.transaction,
                        userName = oAudit.userName
                    }
                };
               
                booServicioContratado = oServicio.validarAccesoRegistroLinkMovil(strIdSession, strIdTransaccion, oparametroMovil);
                if (booServicioContratado)
                {
                    strResultado = "OK";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oAudit.Session, oAudit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(oAudit.Session, oAudit.transaction, "Fin Método : validaServicioContratado");
            return Json(new { data = strResultado });
        }

        #region "Registro de Interaccion"
        /// <summary>
        /// Método que registra la interacción al enviar el link de registro Netflix.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public bool saveInteraccionNetflixServices(NetflixServicesModel oModel, ref string strIdInteraccion)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            string strUSUARIO_SISTEMA = string.Empty;
            string strUSUARIO_APLICACION = string.Empty;
            string strPASSWORD_USUARIO = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : saveInteraccionNetflixServices");
            try
            {
                strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
                var oInteraction = interactionNetflixServices(oModel);
                if (oInteraction == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(oModel.claseDes))
                {
                    return false;
                }
                var oTemplateInteraction = templateInteractionNetflixServices(oModel);
                var resultInteraction = insertInteracNetflixServices(oInteraction, oTemplateInteraction,
                                                                          oModel.linea, strUSUARIO_SISTEMA,
                                                                          strUSUARIO_APLICACION, strPASSWORD_USUARIO,
                                                                          true, oModel.strIdTransaccion);
                strIdInteraccion = resultInteraction.rInteraccionId.ToString();
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Id Interacción : strInteraccionId: " + strIdInteraccion);
                var strFlagInsercion = resultInteraction.rFlagInsercion.ToString();
                var strFlagInsercionInteraccion = resultInteraction.rFlagInsercionInteraccion.ToString();
                if (strFlagInsercion.Trim().ToUpper() != Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercion != string.Empty)
                {
                    return false;
                }
                if (strFlagInsercionInteraccion.Trim().ToUpper() != Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercionInteraccion != string.Empty)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, oModel.strIdTransaccion, ex.Message.ToString());
                return false;
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : saveInteraccionNetflixServices");
            return true;
        }
        /// <summary>
        /// Método que obtiene los datos de la interacción.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public CommonTransacService.Iteraction interactionNetflixServices(NetflixServicesModel oModel)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            var responseModel = new CommonTransacService.Iteraction();
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : interactionNetflixServices");
            try
            {
                responseModel.OBJID_CONTACTO = oModel.strObjidContacto;
                responseModel.START_DATE = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.TELEFONO = oModel.linea;
                responseModel.TIPO = oModel.tipo;
                responseModel.CLASE = oModel.claseDes;
                responseModel.SUBCLASE = oModel.subClaseDes;
                responseModel.TIPO_CODIGO = oModel.tipoCode;
                responseModel.CLASE_CODIGO = oModel.claseCode;
                responseModel.SUBCLASE_CODIGO = oModel.subClaseCode;
                responseModel.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                responseModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                responseModel.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                responseModel.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                responseModel.NOTAS = oModel.strNote;
                responseModel.FLAG_CASO = Claro.Constants.NumberZeroString;
                responseModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                responseModel.AGENTE = oModel.currentUser;
                responseModel.APELLIDO_AGENTE = oModel.strApellidos;
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Interacción --> Parametros de entrada tipificación: " + json_object);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : interactionNetflixServices");
            return responseModel;
        }
        /// <summary>
        /// Método que obtiene los datos de la plantilla interacción.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public CommonTransacService.InsertTemplateInteraction templateInteractionNetflixServices(NetflixServicesModel oModel)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            var responseModel = new CommonTransacService.InsertTemplateInteraction();
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : templateInteractionNetflixServices");
            try
            {
                responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strTransaccionNetflixServicesPostpaid");
                responseModel._X_CLARO_NUMBER = oModel.linea;
                responseModel._X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                responseModel._X_FIRST_NAME = oModel.strNombres;
                responseModel._X_LAST_NAME = oModel.strApellidos;
                responseModel._X_OTHER_FIRST_NAME = oModel.strContactoCliente;
                responseModel._X_DISTRICT = oModel.distrito;
                responseModel._X_CITY = oModel.provincia;
                responseModel._X_DEPARTMENT = oModel.departamento;
                responseModel._X_NAME_LEGAL_REP = oModel.strfullNameUser;
                responseModel._X_EMAIL = oModel.email;
                responseModel._X_ADDRESS5 = oModel.email;
                responseModel._X_INTER_15 = oModel.strCacDac;
                responseModel._X_INTER_22 = oModel.notifica;
                responseModel._X_INTER_23 = Convert.ToInt(oModel.notificaSMS);
                responseModel._X_INTER_24 = Convert.ToInt(oModel.notificaEmail); 
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Plantilla Cambio de Dirección --> Parametros de entrada actualizacion: " + json_object);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : templateInteractionNetflixServices");
            return responseModel;
        }
        /// <summary>
        /// Método que inserta los datos enviados en la interacción.
        /// </summary>
        /// <param name="oInteractionModel"></param>
        /// <param name="oPlantillaDat"></param>
        /// <param name="strNroTelephone"></param>
        /// <param name="strUserSession"></param>
        /// <param name="strUserAplication"></param>
        /// <param name="strPassUser"></param>
        /// <param name="boolEjecutTransaction"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public CommonTransacService.InsertGeneralResponse insertInteracNetflixServices(CommonTransacService.Iteraction oInteractionModel, CommonTransacService.InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession)
        {
            CommonTransacService.InsertGeneralRequest objRequest;
            CommonTransacService.InsertGeneralResponse objResult = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.CommonTransacServiceClient oCommonService = null;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : insertInteracNetflixServices");
            try
            {
                oCommonService = new CommonTransacService.CommonTransacServiceClient();
                objRequest = new CommonTransacService.InsertGeneralRequest()
                {
                    Interaction = oInteractionModel,
                    InteractionTemplate = oPlantillaDat,
                    vNroTelefono = strNroTelephone,
                    vPASSWORD_USUARIO = strPassUser,
                    vUSUARIO_APLICACION = strUserSession,
                    vUSUARIO_SISTEMA = strUserAplication,
                    vEjecutarTransaccion = boolEjecutTransaction,
                    audit = audit,
                };
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Registro de Interacción --> Parametros de entrada actualizacion: " + json_object);

                objResult = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oCommonService.GetinsertInteractionGeneral(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : insertInteracNetflixServices");
            return objResult;
        }
        #endregion

        #region "Constancia"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdInteraccion"></param>
        /// <returns></returns>
        public ActionResult PostpaidNetflixServicesConst(string linea, string cac, string titular, string cliente, string interaccion, string tipo, string numero, string correo, string notifica, string email, string estado, string asesor)
        {
            ViewData["cac"] = cac;
            ViewData["fecha"] = String.Format("{0} de {1} del {2}", DateTime.Today.Day, DateTime.Today.ToString("MMMM"), DateTime.Today.Year);
            ViewData["linea"] = linea;
            ViewData["titular"] = titular;
            ViewData["cliente"] = cliente;
            ViewData["interaccion"] = interaccion;
            ViewData["tipo"] = tipo;
            ViewData["numero"] = numero;
            ViewData["correo"] = correo;
            ViewData["notifica"] = notifica;
            ViewData["email"] = email;
            ViewData["estado"] = estado;
            ViewData["asesor"] = asesor;
            return View();
        }
        #endregion
    }
}