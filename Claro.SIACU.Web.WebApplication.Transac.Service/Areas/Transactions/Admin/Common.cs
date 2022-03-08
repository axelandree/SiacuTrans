using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Admin
{
    public static class Common
    {
        public static string GetTransactionID()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return DateTime.Now.ToString("yyyyMMddHHMMss") + rd.Next(100000, 999999).ToString();
        }

        public static string GetApplicationIp()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]);
        }

        public static string GetApplicationName()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
        }

        public static string GetApplicationCode()
        {
            return KEY.AppSettings("ApplicationCode");
        }

        [Browsable(false)]
        public static string CurrentUser
        {
            get
            {
                string strDomainUser = HttpContext.Current.Request.ServerVariables["LOGON_USER"];
                string strUser = KEY.AppSettings("TestUser", "");

                if (string.IsNullOrEmpty(strUser))
                {
                    strUser = strDomainUser.Substring(strDomainUser.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                }

                return strUser.ToUpperInvariant();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static string GetPhoneNumber(string strPhone)
        {
            return ((strPhone.Substring(0, 2) == KEY.AppSettings("ConstKeyCodigoInternacional") ? strPhone : KEY.AppSettings("ConstKeyCodigoInternacional") + strPhone.Trim()));
        }

        /// <summary>
        /// Metodo que hace una invocacion al registro de auditoria
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strClientIP"></param>
        /// <param name="strClientName"></param>
        /// <param name="strText"></param>
        /// <param name="objAuditRequest"></param>
        /// <returns></returns>
        public static string InsertAudit(SecurityAudit.AuditRequest audit, string strPhone, string strTransaction, string strText)
        {
            string strResponse = "";

            string strUserName = CurrentUser;
            string strService = KEY.AppSettings("gConstEvtServicio_ModCP");
            string strServerIP = GetApplicationIp();
            string strServerName = GetApplicationName();

            string strClientIP = GetClientIP();
            string strClientName = GetClientName();

            SecurityAudit.AuditClient objAuditClient = new SecurityAudit.AuditClient();
            SecurityAudit.RegisterRequest objRegisterRequest = new SecurityAudit.RegisterRequest()
            {
                audit = audit,
                userName = strUserName,
                service = strService,
                phone = strPhone,
                text = strText,
                transaction = strTransaction,
                clientIP = strClientIP,
                clientName = strClientName,
                serverIP = strServerIP,
                serverName = strServerName
            };

            try
            {
                objRegisterRequest.audit = audit;   
            }
            catch (Exception ex)
            {
                strResponse = ex.Message.ToString();
                Claro.Web.Logging.Error(objRegisterRequest.audit.Session, objRegisterRequest.audit.transaction, ex.Message);
            }

            return strResponse;
        }

        /// <summary>
        /// Metodo que hace una invocacion al registro de auditoria
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strClientIP"></param>
        /// <param name="strClientName"></param>
        /// <param name="strText"></param>
        /// <param name="objAuditRequest"></param>
        /// <returns></returns>
        public static string InsertAudit(SecurityAudit.AuditRequest audit, string strPhone, string strTransaction, string strText, string strService)
        {
            string strResponse = "";

            string strUserName = CurrentUser;
            string strServerIP = GetApplicationIp();
            string strServerName = GetApplicationName();

            string strClientIP = GetClientIP();
            string strClientName = GetClientName();

            SecurityAudit.AuditClient objAuditClient = new SecurityAudit.AuditClient();
            SecurityAudit.RegisterRequest objRegisterRequest = new SecurityAudit.RegisterRequest()
            {
                audit = audit,
                userName = strUserName,
                service = strService,
                phone = strPhone,
                text = strText,
                transaction = strTransaction,
                clientIP = strClientIP,
                clientName = strClientName,
                serverIP = strServerIP,
                serverName = strServerName
            };

            try
            {
                objRegisterRequest.audit = audit;
                Claro.Web.Logging.ExecuteMethod(
                    objRegisterRequest.audit.Session,
                    objRegisterRequest.audit.transaction,
                    () => { objAuditClient.Register(objRegisterRequest); });
                strResponse = Claro.SIACU.Constants.RegisterAuditFor + strPhone;
            }
            catch (Exception ex)
            {
                strResponse = ex.Message.ToString();
                Claro.Web.Logging.Error(objRegisterRequest.audit.Session, objRegisterRequest.audit.transaction, ex.Message);
            }

            return strResponse;
        }

        public static TAudit CreateAuditRequest<TAudit>(string strIdSession)
        {
            TAudit audit = Activator.CreateInstance<TAudit>();
            foreach (PropertyInfo propertyInfo in audit.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.Transaction)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetTransactionID());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.ApplicationName)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetApplicationName());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.IpAddress)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetApplicationIp());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.UserName)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.CurrentUser);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.Session)
                {
                    propertyInfo.SetValue(audit, strIdSession);
                }
            }
            return audit;
        }

        public static string GetClientIP()
        {
            string strIpClient = Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(strIpClient))
            {
                strIpClient = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
            }
            return strIpClient;
        }

        public static string GetClientName()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
        }

        public static string GetApplicationRoute()
        {
            return AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        /// <summary>
        /// CAlarcon - 18/05/2017 
        /// Get the data in xml configuration file
        /// </summary>
        /// <param name="inVariable"></param>
        /// <returns>An Arraylist with the required data</returns>
        public static ArrayList GetXMLList(string inVariable)
        {

            string archive = string.Format("{0}\\{1}", Claro.SIACU.Transac.Service.Constants.FileSiacutData, "SiacutData.xml");
            string rute = GetApplicationRoute();
            rute += archive;
            if (!File.Exists(rute))
            {
                return new ArrayList();
            }
            else
            {

                ArrayList salida = new ArrayList();
                XmlDocument doc = new XmlDocument();
                doc.Load(rute);
                XmlNodeList nodeList = doc.SelectNodes("descendant::" + inVariable + "/item");
                for (int i = 0; i < nodeList.Count; i++)
                {
                    GenericItem item = new GenericItem()
                    {
                        Codigo = nodeList[i].ChildNodes[0].InnerText,
                        Descripcion = nodeList[i].ChildNodes[1].InnerText
                    };

                    salida.Add(item);
                }


                return salida;
            }

        }

       
    }
}