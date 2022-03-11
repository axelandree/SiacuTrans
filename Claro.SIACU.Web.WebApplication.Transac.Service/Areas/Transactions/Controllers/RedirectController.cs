using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect;
using Newtonsoft;
using KEY = Claro.ConfigurationManager;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class RedirectController : Controller
    {
        //
        // GET: /Transactions/Redirect/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BritgeRedirect(string secuencia)
        {

            ViewBag.sequence = secuencia;

            return View();

        }

        public ActionResult ChangeNumber()  
        {
            return PartialView();
        }
        public JsonResult GetSessionsParams(string strIdSession, string strOption, string strApplication)
        {
            CommonTransacService.RedirectSessionResponseDashboard objRedirectSessionResponseDashboard;
            CommonTransacService.RedirectSessionRequestDashboard objRedirectSessionRequestDashboard = new CommonTransacService.RedirectSessionRequestDashboard()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession),
                Application = strApplication,
                Option = strOption
            };
            try
            {
                objRedirectSessionResponseDashboard = Claro.Web.Logging.ExecuteMethod<CommonTransacService.RedirectSessionResponseDashboard>(
                    objRedirectSessionRequestDashboard.audit.Session,
                    objRedirectSessionRequestDashboard.audit.transaction,
                    () => { return new CommonTransacService.CommonTransacServiceClient().GetRedirectSession(objRedirectSessionRequestDashboard); });
            }
            catch (Exception ex)
            {
                objRedirectSessionResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, ex.Message);
            }
            if (objRedirectSessionResponseDashboard != null)
            {
                if (objRedirectSessionResponseDashboard.CodeError == "0")
                {
                    if (objRedirectSessionResponseDashboard.listRedirect.Count == 0)
                    {
                        Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, objRedirectSessionResponseDashboard.ErrorMsg);
                        throw new Claro.MessageException(objRedirectSessionResponseDashboard.ErrorMsg);
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, objRedirectSessionResponseDashboard.ErrorMsg);
                    throw new Claro.MessageException(objRedirectSessionResponseDashboard.ErrorMsg);
                }
            }
            return Json(objRedirectSessionResponseDashboard.listRedirect);
        }

        public JsonResult RedirectApp(string strIdSession, string strAppDest, string strOption, string strData)
        {
            string[] response = new string[2];
            CommonTransacService.InsertRedirectComResponseDashboard objInsertRedirectComResponseDashboard;
            CommonTransacService.InsertRedirectComRequestDashboard objInsertRedirectComRequestDashboard =new CommonTransacService.InsertRedirectComRequestDashboard()
            {

                audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession),
                Option = strOption,
                IpClient = App_Code.Common.GetClientIP(),
                IpServer = App_Code.Common.GetApplicationIp(),
                JsonParameters = strData,
                AppDest = strAppDest
            };
            objInsertRedirectComRequestDashboard.audit.userName = "C12640";
            try
            {
                objInsertRedirectComResponseDashboard = new CommonTransacService.CommonTransacServiceClient().InsertRedirectCommunication(objInsertRedirectComRequestDashboard);
            }
            catch (Exception ex)
            {
                objInsertRedirectComResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, ex.Message);
            }
            if (objInsertRedirectComResponseDashboard.ResultRegCommunication == "ok")
            {
                Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL: " + objInsertRedirectComResponseDashboard.Url);
                if (KEY.AppSettings("strFlagPiloto") == Claro.Constants.NumberOneString)
                {
                    string urlRedirect = objInsertRedirectComResponseDashboard.Url;
                    string serverPiloto = KEY.AppSettings("strNodoPiloto") + urlRedirect.Substring(urlRedirect.IndexOf("/", urlRedirect.IndexOf("/", urlRedirect.IndexOf("/") + Claro.Constants.NumberOne) + Claro.Constants.NumberOne) + Claro.Constants.NumberOne);
                    response[0] = serverPiloto;
                    response[1] = objInsertRedirectComResponseDashboard.Sequence;
                    Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL Piloto: " + serverPiloto);
                }
                else
                {
                    response[0] = objInsertRedirectComResponseDashboard.Url;
                    response[1] = objInsertRedirectComResponseDashboard.Sequence;
                }
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, objInsertRedirectComResponseDashboard.ResultRegCommunication);
                throw new Claro.MessageException(objInsertRedirectComResponseDashboard.ResultRegCommunication);
            }
            return Json(response);
        }
        public JsonResult GetRedirect(string strIdSession, string sequence) 
        { 
            String url = String.Empty;
            String strScript = String.Empty;
            String jsonString = String.Empty;
            String strJsonSesiones = String.Empty;
            String responseMsg = String.Empty;
            String ipAplication = String.Empty;
            String availability = String.Empty;
            BERedireccion DatosRedirect = new BERedireccion();
            string strServerName = System.Web.HttpContext.Current.Server.MachineName;
            string strNroNodo = string.Empty;

            CommonTransacService.ValidateCommunicationRequest obj = new CommonTransacService.ValidateCommunicationRequest();


            string[] response = new string[4];
            CommonTransacService.ValidateCommunicationResponse objValidateRedirectComResponseDashboard;
            CommonTransacService.ValidateCommunicationRequest objValidateRedirectComRequestDashboard = new CommonTransacService.ValidateCommunicationRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession),
                Sequence = sequence,
                Server = App_Code.Common.GetApplicationIp()
            };

            try
            {
                objValidateRedirectComResponseDashboard = new CommonTransacService.ValidateCommunicationResponse();

                objValidateRedirectComResponseDashboard = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ValidateCommunicationResponse>(() =>
                { 
                     return new CommonTransacService.CommonTransacServiceClient().ValidateRedirectCommunication(objValidateRedirectComRequestDashboard);
                });
                
            }
            catch (Exception ex)
            {
                objValidateRedirectComResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, ex.Message);
            }

            try
            {
                RegisterLog(strIdSession, objValidateRedirectComResponseDashboard.ResultValCommunication.ToString());
                RegisterLog(strIdSession, "URL =>" + objValidateRedirectComResponseDashboard.url);
                RegisterLog(strIdSession, "Availability =>" + objValidateRedirectComResponseDashboard.Availability);
                RegisterLog(strIdSession, "JsonString =>" + objValidateRedirectComResponseDashboard.JsonString);
                RegisterLog(strIdSession, "ErrorMessage =>" + objValidateRedirectComResponseDashboard.ErrorMessage);
            }
            catch (Exception ex)
            {
                RegisterLog(strIdSession, "ERROR => " + ex.Message);
            }
           
            if (strServerName.Length > 1)
            {
                strNroNodo = strServerName.Substring((strServerName.Length - 2), 2);
            }

            if (objValidateRedirectComResponseDashboard.ResultValCommunication)
            {
                response[0] = objValidateRedirectComResponseDashboard.url;
                response[1] = objValidateRedirectComResponseDashboard.Availability;
                response[2] = objValidateRedirectComResponseDashboard.JsonString;
                response[3] = strNroNodo;
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, objValidateRedirectComResponseDashboard.ErrorMessage);
                throw new Exception(objValidateRedirectComResponseDashboard.ErrorMessage);
            }


            return Json(response);

        }


        private void RegisterLog(string IdSession, string Message)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "RedirectController", "GetRedirect: " + Message);

        }
	}
}