using Claro.SIACU.Transac.Service;
using Claro.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class CommonController : Controller
    {
        CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        PreTransacService.PreTransacServiceClient objPreTransacService = new PreTransacService.PreTransacServiceClient();
        
        public ActionResult SearchQuestionsAnswer()
        {
            return View();
        }

        public JsonResult SearchQuestionsAnswerSecurityJson(string idSession, string tipoCliente, string grupoCliente)
        {
            string codTipoCliente = getCodeTypeClient(tipoCliente.ToUpper());
            string codGrupoCliente = getCodeGroupClient(grupoCliente.ToUpper());

            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.QuestionsAnswerSecurityRequest objQuestionsAnswerSecurityRequest = new CommonTransacService.QuestionsAnswerSecurityRequest()
            {
                audit = objAuditRequest,
                SessionId = objAuditRequest.Session,
                TransactionId = objAuditRequest.transaction,
                Typeclient = codTipoCliente,
                Groupclient = codGrupoCliente

            };
            CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();

            CommonTransacService.QuestionsAnswerSecurityResponse objQuestionsAnswerSecurityResponse = objCommonTransacService.GetQuestionsAnswerSecurity(objQuestionsAnswerSecurityRequest);


            return Json(new { data = objQuestionsAnswerSecurityResponse });
        }
        public string getCodeTypeClient(string tipoCliente)
        {
            string codTipoCliente = String.Empty;
            if (tipoCliente == KEY.AppSettings("ConstBusiness")) {
                codTipoCliente = KEY.AppSettings("ConstCodBusiness");
            }
            if (tipoCliente == KEY.AppSettings("ConstConsumer"))
            {
                codTipoCliente = KEY.AppSettings("ConstCodConsumer");
            }
            if (tipoCliente == KEY.AppSettings("ConstB2E"))
            {
                codTipoCliente = KEY.AppSettings("ConstCodB2E");
            }
            if (tipoCliente == KEY.AppSettings("ConstEmpleadoClaro"))
            {
                codTipoCliente = KEY.AppSettings("ConstCodEmpleadoClaro");
            }
            if (tipoCliente == KEY.AppSettings("ConstPrepago"))
            {
                codTipoCliente = KEY.AppSettings("ConstCodPrepago");
            }
  
            return codTipoCliente;
        }
        public string getCodeGroupClient(string grupoCliente)
        {
            string codGrupoCliente = String.Empty;
            if (grupoCliente == KEY.AppSettings("ConstCorporativo"))
            {
                codGrupoCliente = KEY.AppSettings("ConstCodCorporativo");
            }
            if (grupoCliente == KEY.AppSettings("ConstMasivo"))
            {
                codGrupoCliente = KEY.AppSettings("ConstCodMasivo");
            }

            return codGrupoCliente;
        }

        public ActionResult SearchImei()
       {
            var number = Request["number"];
            ViewData["Number"] = number;
            return View();
       }
        public ActionResult QuestionsAnswerSecurity()
        {

            return View();
        }
        public JsonResult SearchListIMEI(string strIdSession, string number){

            Logging.Info(strIdSession, "Transaction: ", "IN CommonController SearchListIMEI()");
            ArrayList arraylist = new ArrayList();
            String strfecaux = String.Empty;
            CommonServicesController convert2010 = new CommonServicesController();      

            Logging.Info(strIdSession, "Transaction: ", "IN CommonController SearchListIMEI() > convert2010.GetNumber()");
            number = convert2010.GetNumber(strIdSession, true, number);

            CommonTransacService.ConsultImeiResponse objImeiResponse = new CommonTransacService.ConsultImeiResponse();
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.ConsultImeiRequest objImeiRequest = new CommonTransacService.ConsultImeiRequest
            {
                audit = objAuditRequest,
                Nro_phone = number,
                SessionId = objAuditRequest.Session,
                TransactionId = objAuditRequest.transaction

            };
            CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
            try
            {
                Logging.Info(objImeiRequest.SessionId, objImeiRequest.TransactionId, "IN CommonController SearchListIMEI() > GetConsultImei()");
                objImeiResponse = objCommonTransacService.GetConsultImei(objImeiRequest);

                if (objImeiResponse.ListConsultImei != null)
                {
                    foreach (var list in objImeiResponse.ListConsultImei)
                    {
                        if (list.mark != String.Empty && list.model != String.Empty)
                        {
                            list.mark_model = list.model + " - " + list.mark;
                        }

                        strfecaux = Functions.CheckStr(list.Date_hour_start);

                        if (strfecaux != String.Empty)
                        {
                            list.Date_hour_start = strfecaux.Substring(6, 2) + "/" + strfecaux.Substring(4, 2) + "/" + strfecaux.Substring(0, 4) + " " + strfecaux.Substring(8, 2) + ":" + strfecaux.Substring(10, 2) + ":" + strfecaux.Substring(12, 2);
                        }
                        else
                        {
                            list.Date_hour_start = String.Empty;
                        }
                        strfecaux = Functions.CheckStr(list.Date_hour_end);

                        if (strfecaux != String.Empty)
                        {
                            list.Date_hour_end = strfecaux.Substring(6, 2) + "/" + strfecaux.Substring(4, 2) + "/" + strfecaux.Substring(0, 4) + " " + strfecaux.Substring(8, 2) + ":" + strfecaux.Substring(10, 2) + ":" + strfecaux.Substring(12, 2);
                        }
                        else
                        {
                            list.Date_hour_end = String.Empty;
                        }
                    }
                }
                Logging.Info(objImeiRequest.SessionId, objImeiRequest.TransactionId, "OUT CommonController SearchListIMEI() > GetConsultImei()");
            }
            catch(Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction,"ERROR: CommonController SearchListIMEI()" + ex.Message);
            }           

            return Json(new { data = objImeiResponse.ListConsultImei });
        }
        public JsonResult GetConsultMarkModel(string IMEI, string strIdSession)
        {
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.ConsultMarkModelRequest objImeiRequest = new CommonTransacService.ConsultMarkModelRequest
            {
                audit = objAuditRequest,
                Nro_imei = IMEI,
                SessionId = objAuditRequest.Session,
                TransactionId = objAuditRequest.transaction

            };
            CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
            CommonTransacService.ConsultMarkModelResponse objImeiResponse = objCommonTransacService.GetConsultMarkModel(objImeiRequest);

            return Json(new { data = objImeiResponse.result });
        }
        public ActionResult OptionApplicant()
        {
            return View();
        }
        public ActionResult TypeApplicant()
        {
            string TypeDocument = ConfigurationManager.AppSettings("strCodigoTipoDocumentoDNIValidacionBiometrica").ToString();
            Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.TypeApplicantModel objEnt = new Models.TypeApplicantModel()
            {
                TypeDocumentValidation = TypeDocument
            };
            return View(objEnt);
        }

        public ActionResult CantApplicant()
        {
            Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.CantApplicantModel objCantApplicantModel = new Models.CantApplicantModel()
            {
                CantMax = KEY.AppSettings("strCantMaxRRLL")
            };
            return View(objCantApplicantModel);
        }

        public ActionResult ValidationBiometria()
        {
            //Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.ValidationBiometria objEnt = new Models.ValidationBiometria()
            //{
            //    strUrl = str
            //};
            return View();
        }
        public ActionResult ValidationBiometriaPrepaid()
        {

            return View();
        }
        public ActionResult ValidationNoBiometria()
        {
            return View();
        }
        public ActionResult ValidationNoBiometriaPrepaId()
        {
            return View();
        }
        public JsonResult getMsjBiometria()
        {

            Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.BiometryKeys BioKeys = new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.BiometryKeys()
            {
                strTipoDocumentos = KEY.AppSettings("strTipoDocumentos"),
                strCodigoTipoDocumentoDNIValidacionBiometrica = KEY.AppSettings("strCodigoTipoDocumentoDNIValidacionBiometrica"),
                strMensajeValidacionBiometrica1 = KEY.AppSettings("strMensajeValidacionBiometrica1"),
                strMensajeValidacionBiometrica2 = KEY.AppSettings("strMensajeValidacionBiometrica2"),
                strMensajeValidacionBiometrica3 = KEY.AppSettings("strMensajeValidacionBiometrica3"),
                strMensajeValidacionBiometrica4 = KEY.AppSettings("strMensajeValidacionBiometrica4"),
                strMensajeValidacionBiometricaMenos2 = KEY.AppSettings("strMensajeValidacionBiometricaMenos2"),
                strMensajeValidacionBiometricaMenos4 = KEY.AppSettings("strMensajeValidacionBiometricaMenos4"),
                strMensajeValidacionBiometricaMenos5 = KEY.AppSettings("strMensajeValidacionBiometricaMenos5"),
                strMensajeValidacionBiometrica0 = KEY.AppSettings("strMensajeValidacionBiometrica0"),
                strMensajeValidacionBiometricaOtros = KEY.AppSettings("strMensajeValidacionBiometricaOtros"),
                strKeyTransaccionDesbloqueoLinea = KEY.AppSettings("strKeyTransaccionDesbloqueoLinea"),
                strDomainBiometria = KEY.AppSettings("strDominioBiometria"),
                Url = KEY.AppSettings("UrlObtenerConfiguracionBiometrica")
            };

            return Json(new { data = BioKeys }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getConfigBiometria(string strIdSession, string strCodUsuario, string strCodTipoCAC, string strCodCAC, string strIdPadre, string strNumDoc, string strTipoDoc, string strCanalPermitido)
        {
            PostTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            string idPadre= new CommonTransacService.CommonTransacServiceClient().GetIdTrazabilidad(objAuditRequest.Session, objAuditRequest.transaction, 1);
            string idHijo = new CommonTransacService.CommonTransacServiceClient().GetIdTrazabilidad(objAuditRequest.Session, objAuditRequest.transaction, 2);
            
            PostTransacService.PostTransacServiceClient obj = new PostTransacService.PostTransacServiceClient();
            PostTransacService.BiometricConfigurationRequest request = new PostTransacService.BiometricConfigurationRequest()
            {
                audit = objAuditRequest,
                head = new PostTransacService.ValidateIdentityHeaderRequest()
                {
                    canal = ConfigurationManager.AppSettings("strConfigBiometricaSistema"),
                    idAplicacion = App_Code.Common.GetApplicationIp(),
                    usuarioAplicacion = strCodUsuario,
                    usuarioSesion = strCodUsuario,
                    idTransaccionESB = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    fechaInicio = DateTime.Now,
                    idTransaccionNegocio = idPadre,
                    nodoAdicional = ConfigurationManager.AppSettings("strNodoAdicional"),

                },
                codCanal = strCodTipoCAC,
                codigoPDV = strCodCAC,
                codOperacion = ConfigurationManager.AppSettings("strConfigBioCodOperacion"),
                codModalVenta = ConfigurationManager.AppSettings("strConfigBioModalidadVenta"),
                estado = ConfigurationManager.AppSettings("strConfigBioEstado"),
                idPadre = idPadre,
                idHijo = idHijo,                
                numeroDocumento = strNumDoc,
                sistema = ConfigurationManager.AppSettings("strConfigBiometricaSistema"),
                tipoDocumento = strTipoDoc,
                usuarioCtaRed = strCodUsuario
            };

            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: Inicio Parametros HeaderRequest");
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: canal: " + strCanalPermitido);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: idAplicacion: " + App_Code.Common.GetApplicationIp());
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: usuarioAplicacion: " + strCodUsuario);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: usuarioSesion: " + strCodUsuario);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: idTransaccionESB: " + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: idTransaccionNegocio: " + idPadre);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: fechaInicio: " + DateTime.Now);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: nodoAdicional: " + ConfigurationManager.AppSettings("strNodoAdicional"));         
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: codCanal: " + strCodTipoCAC);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: codigoPDV: " + strCodCAC);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: codOperacion: " + ConfigurationManager.AppSettings("strConfigBioCodOperacion"));
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: codModalVenta: " + ConfigurationManager.AppSettings("strConfigBioModalidadVenta"));
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: estado: " + ConfigurationManager.AppSettings("strConfigBioEstado"));
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: idHijo: " + idHijo);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: idPadre: " + idPadre);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: numeroDocumento: " + strNumDoc);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: sistema: " + ConfigurationManager.AppSettings("strConfigBiometricaSistema"));
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: tipoDocumento: " + strTipoDoc);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: usuarioCtaRed: " + strCodUsuario);
            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "[getConfigBiometria]: Fin Parametros HeaderRequest");


            PostTransacService.BiometricConfigurationResponse response = new PostTransacService.BiometricConfigurationResponse()
            {
                status = new PostTransacService.ValidateIdentityStatusResponse(),
                data = new List<PostTransacService.BiometricDataConfiguration>()
            };
            response = obj.GetBiometricConfiguration(request);


            return Json(new { data = response }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult DeclaracionJuradaNoBiometria(string phone, string fullName, string rp1, string rp2, string rp3, string motivo, string dni)
        {
            ViewData["chkPerNatu"] = 1;
            ViewData["customerTelephone"] = phone;
            ViewData["customerFullName"] = fullName;
            ViewData["respuesta1"] = rp1;
            ViewData["respuesta2"] = rp2;
            ViewData["respuesta3"] = rp3;
            ViewData["motivo"] = motivo;
            ViewData["dni"] = dni;
            ViewData["lblFecha"] = String.Format("{0} de {1} del {2}", DateTime.Today.Day, DateTime.Today.ToString("MMMM"), DateTime.Today.Year);
            return View();
        }
        public JsonResult GetConsultPointOfSale(string idSession, string CustomerId, string codeCAC)
        {
           
            PreTransacService.ConsultPointOfSaleResponse objConsultPointOfSaleResponse = new PreTransacService.ConsultPointOfSaleResponse();
            PreTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(idSession);
            PreTransacService.ConsultPointOfSaleRequest objConsultPointOfSaleRequest = new PreTransacService.ConsultPointOfSaleRequest
            {
                audit = objAuditRequest,
                CodigoCAC = codeCAC,
                SessionId = idSession,
                TransactionId = objAuditRequest.transaction
            };

            try
            {
                Logging.Info(objConsultPointOfSaleRequest.SessionId, objConsultPointOfSaleRequest.TransactionId, "IN Front GetConsultPointOfSale >  Entrando a GetConsultPointOfSale()");
                objConsultPointOfSaleResponse = objPreTransacService.GetConsultPointOfSale(objConsultPointOfSaleRequest);
            }
            catch (Exception ex)
            {
                Logging.Info(objConsultPointOfSaleRequest.SessionId, objConsultPointOfSaleRequest.TransactionId, "ERROR Front GetConsultPointOfSale >  GetConsultPointOfSale()");
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "GetConsultPointOfSale() Error: " + ex);
                throw new Exception(ex.Message);
            }
            return Json(objConsultPointOfSaleResponse.flag_biometria, JsonRequestBehavior.AllowGet);
        }
    }
}
