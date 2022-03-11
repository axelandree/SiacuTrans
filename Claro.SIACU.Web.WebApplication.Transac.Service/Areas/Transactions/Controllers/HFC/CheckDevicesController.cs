using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC;
using Claro.Web;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class CheckDevicesController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private string gstrUserHostName = string.Empty;
        private string gstrLocalAddr = string.Empty;
        private string gstrServerName = string.Empty;
        //
        // GET: /Transactions/CheckDevices/
        public ActionResult HfcCheckDevices()
        {
            return View();
        }

        public JsonResult PageLoad_HFC(string strIdSession)
        {
            var dictionaryPageLoad = new Dictionary<string, object>
            {
                {"hdnSiteUrl", ConfigurationManager.AppSettings("strRutaSiteInicio")},
                {"hdnTituloPagina", Functions.GetValueFromConfigFile( "strMsgTituloConEquipoLinea",ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                {"gstrUserHostName", Request.UserHostName},
                {"gstrLocalAddr", Request.ServerVariables["LOCAL_ADDR"]},
                {"gstrServerName", Request.ServerVariables["SERVER_NAME"]}
            };

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductDetails(string strIdSession, string strCoid, string strCustomerId,
            string currentUser, string strFullName)
        {
            string strDescription = string.Empty;
            gstrUserHostName = Request.UserHostName;
            gstrLocalAddr = Request.ServerVariables["LOCAL_ADDR"];
            gstrServerName = Request.ServerVariables["SERVER_NAME"];
            List<CheckDeviceHFC> listLine = GetServicesDTH(strIdSession, strCoid, strCustomerId);

            if (listLine.Count > 0)
            {
                strDescription = Functions.GetValueFromConfigFile("strTextoRetornoRegistrosConsultaEquipos", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            else
            {
                strDescription = Functions.GetValueFromConfigFile("strTextoNoRetornoRegistrosConsultaEquipos", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));   
            }

            InsertAudit(strIdSession, strCustomerId, strCoid, strDescription, currentUser, strFullName);

            return new JsonResult
            {
                Data = listLine,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void InsertAudit(string strIdSession, string strCustomerId, string strCoid, string strDescription,string currentUser, string strFullName)
        {
            string strCodAudit = string.Empty;
            string strService = string.Empty;
            string strIpClient = string.Empty;
            string strNameUser = string.Empty;
            string strIpServer = string.Empty;
            string strServerName = string.Empty;
            string strUserAccount = string.Empty;
            string strAmount = Claro.Constants.NumberZeroString;
            try
            {
                strCodAudit = ConfigurationManager.AppSettings("gConstCodigoAuditoriaConsultaEquipos");
                strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
                strIpClient = gstrUserHostName;
                strNameUser = strFullName;
                strIpServer = gstrLocalAddr;
                strServerName = gstrServerName;
                strUserAccount = currentUser;
                string strText = string.Format("/Customer ID: {0} /Contrato ID: {1} ", strCustomerId, strCoid);

                var result = SaveAudit(strIdSession,
                    strUserAccount, 
                    strIpClient, 
                    strIpServer, 
                    strAmount, 
                    strNameUser,
                    strServerName,
                    strService,
                    strCustomerId,
                    strText + " " + strDescription,
                    strCodAudit
                    );


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CheckDeviceHFC> GetServicesDTH(string strIdSession, string strCoid, string strCustomerId)
        {
            List<CheckDeviceHFC> listCheckDeviceHFC = new List<CheckDeviceHFC>();

            try
            {
                AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

                FixedTransacService.ServiceDTHRequest objRequest = new FixedTransacService.ServiceDTHRequest()
                {
                    strCoid = strCoid,
                    strCustomerId = strCustomerId,
                    audit = audit
                };
                FixedTransacService.ServiceDTHResponse objResponse =
                    Claro.Web.Logging.ExecuteMethod(() => _oServiceFixed.GetServiceDTH(objRequest));

                if (objResponse.ListServicesDTH.Count > 0)
                {
                    listCheckDeviceHFC = Mapper.Map<List<CheckDeviceHFC>>(objResponse.ListServicesDTH);
                }
            }
            catch (Exception e)
            {
                LogException(strIdSession, strIdSession, "ERROR CONSULTAR  EQUIPOS: ", e);
            }

            return listCheckDeviceHFC;
        }

	}
}