using Claro.SIACU.Transac.Service;

using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail;
using System.IO;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System.Text;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class HfcIncomingCallDetailBaseController : CommonServicesController
    {
        public readonly CommonTransacService.CommonTransacServiceClient oCommonService = new CommonTransacService.CommonTransacServiceClient();
        public readonly FixedTransacService.FixedTransacServiceClient oFixedService = new FixedTransacService.FixedTransacServiceClient();
        public readonly PostTransacService.PostTransacServiceClient oServicePostPaid = new PostTransacService.PostTransacServiceClient();

    public List<int> ValidateExportExcel(List<ExportExcel> ListExportExcel)
        {
            List<int> list = Enumerable.Range(7, 6).ToList();

            foreach (ExportExcel item in ListExportExcel)
            {
                if (item.Id == Claro.Constants.NumberOneString) { list.Remove(7); }
                if (item.PhoneNumber == Claro.Constants.NumberOneString) { list.Remove(8); }
                if (item.DateIncomingCall == Claro.Constants.NumberOneString) { list.Remove(9); }
                if (item.HourIncomingCall == Claro.Constants.NumberOneString) { list.Remove(10); }
                if (item.IncomingPhoneNumber == Claro.Constants.NumberOneString) { list.Remove(11); }
                if (item.Duration == Claro.Constants.NumberOneString) { list.Remove(12); }
                break;
            }
            return list;
        }
        
        public void GetSendEmail(CallTipificacion tipificacion, string idSession, string strAdjunto, byte[] attachFile)
        {

            string strTemplateEmail = TemplateEmail(tipificacion);
            string strDestinatarios = tipificacion.destinatario;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
            


            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = strRemitente,
                    strTo = strDestinatarios, 
                    strMessage = strTemplateEmail,
                    strAttached = strAdjunto,
                    strSubject = ConfigurationManager.AppSettings("gConstAsuntoDetLlamadaSaliente"),
                    AttachedByte = attachFile
                };
                        
            try
            {

                string user = string.Empty;
                string pass = string.Empty;
                string domain = string.Empty;
               
                
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>
                (
                    () => { return oCommonService.GetSendEmailFixed(objGetSendEmailRequest); }
                );
                

                Claro.Web.Logging.Info("666","666", "Error EMAIL CONTROLLER ENTRANTES : " + objGetSendEmailResponse.Exit);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
            }
        }

        public string TemplateEmail(CallTipificacion objCallTipificacion)
        {
            string strmessage = string.Empty;
            strmessage = "<html>";
            strmessage += "<head>";
            strmessage += "<style type='text/css'>";
            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strmessage += "</style>";
            strmessage += "</head>";
            strmessage += "<body>";
            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += "<tr><td width='180' class='Estilo1' height='22'>Estimado Cliente: </td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td width='180' class='Estilo1' height='22'>Por la Presente queremos informarle que su solicitud de Detalle de Llamadas Entrantes fue atendida.</td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td height='10'></td></tr>";
            strmessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";

            return strmessage;
        }
                
        public SaveAuditResponseCommon SaveResponse(SaveAuditRequestCommon objRequest)
        {
            Claro.Web.Logging.Configure();
            Claro.Web.Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, "INICIANDO AUDITORIA");
            SaveAuditResponseCommon objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() =>
                {
                    return oCommonService.SaveAudit(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, "Error Auditoria : " + ex.Message);                
            }
            
            return objResponse;
        }

        public GenerateConstancyResponseCommon GenerateConstancyFixed(ParametersGeneratePDF objConstancy, string typeFixed, string strSession)
        {

            GenerateConstancyResponseCommon objGeneratePdf = null;

            try
            {
                objConstancy.StrContenidoComercial = Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial",
               KEY.AppSettings("strConstArchivoSIACPOConfigMsg"));
                objConstancy.StrContenidoComercial2 =
                    Functions.GetValueFromConfigFile("IncomingCallDetailContentCommercial2",
                        KEY.AppSettings("strConstArchivoSIACPOConfigMsg"));
                objConstancy.StrCarpetaTransaccion = string.Format(@"{0}\{1}\", typeFixed , ConfigurationManager.AppSettings("strDetalleLlamadasEntrantesTransac"));
                objConstancy.StrNombreArchivoTransaccion = KEY.AppSettings("strDetalleLlamadasEntrantesTransac");

                CommonServicesController obj = new CommonServicesController();
                objGeneratePdf = Claro.Web.Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
                {
                    return obj.GenerateContancyPDF(strSession, objConstancy);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strSession, "666", "Error al Generar el HPEXTREAM ENTRANTES : " + ex.Message);
            }

            return objGeneratePdf;
        }


	}
}