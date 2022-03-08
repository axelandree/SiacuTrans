using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using FixedServiceClient = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class CheckDevicesController : CommonServicesController
    {
        private readonly FixedServiceClient.FixedTransacServiceClient _oServiceFixed = new FixedServiceClient.FixedTransacServiceClient();
        private string gstrUserHostName = string.Empty;
        private string gstrLocalAddr = string.Empty;
        private string gstrServerName = string.Empty;

        public ActionResult LteCheckDevices()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PageLoad_LTE(string strIdSession)
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


        [HttpPost]
        public JsonResult GetProductDetails(string strIdSession, string strCoid, string strCustomerId, string currentUser, string strFullName)
        {           
            string strDescripcion;
            gstrUserHostName = Request.UserHostName;
            gstrLocalAddr = Request.ServerVariables["LOCAL_ADDR"];
            gstrServerName = Request.ServerVariables["SERVER_NAME"];

            var objLstResult = GetServicesLte(strIdSession, strCoid, strCustomerId);
            if (objLstResult.Count > 0)
            {
                strDescripcion = Functions.GetValueFromConfigFile("strTextoRetornoRegistrosConsultaEquipos", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            else
            {
                strDescripcion = Functions.GetValueFromConfigFile("strTextoNoRetornoRegistrosConsultaEquipos", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }

            InsertAuditCheckDevices(strIdSession, strCustomerId, strCoid, strDescripcion, currentUser, strFullName);

            return new JsonResult
            {
                Data = objLstResult,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<CheckDevice> GetServicesLte(string strIdSession, string strCoid, string strCustomerId)
        {
            var objViewModelResponse = new List<CheckDevice>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objRequest = new FixedServiceClient.ServicesLteFixedRequest
            {
                audit = objAuditRequest,
                strCoid = strCoid,
                strCustomerId = strCustomerId
            };

            var objModelResponse = Claro.Web.Logging.ExecuteMethod(() => _oServiceFixed.GetServicesLte(objRequest));
            if (objModelResponse.ListServicesLte.Count > 0)
            {
                objViewModelResponse = Mapper.Map<List<CheckDevice>>(objModelResponse.ListServicesLte);
            }

            return objViewModelResponse;
        }

        public void InsertAuditCheckDevices(string strIdSession, string vstrCustomerId, string vstrContratoId, string vstrDescripcion, string currentUser, string strFullName)
        {
            var strCodigoAuditoria = string.Empty;
            var strServicio = string.Empty;
            var strIpCliente = string.Empty;

            var strNombreCliente = string.Empty;
            var strIPServidor = string.Empty;
            var strNombreServidor = string.Empty;
            var strCuentaUsuario = string.Empty;
            var strNombreUsuario = string.Empty;
            var strNroTelefono = vstrCustomerId;
            var strMonto = ConstantsHFC.PresentationLayer.NumeracionCERO;

            try
            {
                strCodigoAuditoria = ConfigurationManager.AppSettings("gConstCodigoAuditoriaConsultaEquiposLTE");
                strServicio = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
                strIpCliente = gstrUserHostName;
                strNombreCliente = strFullName;
                strIPServidor = gstrLocalAddr;
                strNombreServidor = gstrServerName;
                strCuentaUsuario = currentUser;

                var strTexto = string.Empty;
                strTexto = "/Customer ID: " + vstrCustomerId + "/Contrato ID: " + vstrContratoId + " " + vstrDescripcion;

                var resultado = SaveAudit(strIdSession, strCuentaUsuario, strIpCliente, strIPServidor, strMonto, strNombreCliente, strNombreServidor, strServicio, strNroTelefono, strTexto, strCodigoAuditoria);              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
	}
}