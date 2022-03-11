using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using KEY = Claro.SIACU.Transac.Service.Constants;
using System.Collections;
using EXCEL = Claro.Helpers.Transac.Service;
using System.Reflection;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.PrePaid
{
    public class BilledOutCallDetailsController : Controller
    {
        //
        // GET: /Transactions/CallOutDetails/
        private readonly PreTransacService.PreTransacServiceClient oPreTransacService = new PreTransacService.PreTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();

        public ActionResult PrepaidBilledOutCallDetail()
        {
            Claro.Web.Logging.Configure();
            return PartialView("~/Areas/Transactions/Views/BilledOutCallDetail/PrepaidBilledOutCallDetail.cshtml");
        }

        public JsonResult GetCallOutDetailsLoad(HELPERS.BilledCalltypification oBilledCalltipificacion)
        {
            Transactions.Controllers.CommonServicesController octlPostpaid = new Transactions.Controllers.CommonServicesController();
            string PhonfNroGener = octlPostpaid.GetNumber(oBilledCalltipificacion.strIdSession, true, oBilledCalltipificacion.Phone);

            PreTransacService.CallResponse objCallResponse = new PreTransacService.CallResponse();
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(oBilledCalltipificacion.strIdSession);
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, audit.transaction, "GetCallOutDetailsLoad");
            audit.userName = ConfigurationManager.AppSettings("strUsrWSDatosPrePost");
            audit.applicationName = ConfigurationManager.AppSettings("strAPPWSDatosPrePost");
            PreTransacService.CallRequest objCallRequest =
                new PreTransacService.CallRequest()
                {
                    audit = audit,
                    strfechaInicio = oBilledCalltipificacion.StartDate,
                    strfechaFin = oBilledCalltipificacion.EndDate,
                    linea = PhonfNroGener,
                    strTipoConsulta = ConfigurationManager.AppSettings("strTipoConsultaWSDatosPrePost"),
                    IsTFI = oBilledCalltipificacion.IsTFI,
                    tp = oBilledCalltipificacion.TrafType,
                };
            try
            {
                objCallResponse = Claro.Web.Logging.ExecuteMethod<PreTransacService.CallResponse>(() => { return oPreTransacService.GetCallOutDetailsLoad(objCallRequest); });
                InsertAudit(oBilledCalltipificacion, KEY.numeroUno);
            }
            catch (Exception ex)
            {
                InsertAudit(oBilledCalltipificacion, KEY.numeroCero);
                Claro.Web.Logging.Error(oBilledCalltipificacion.strIdSession, objCallRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Model.Prepaid.BilledOutCallDetailsViewModel objPrepServiceTransactionServices = new Models.Prepaid.BilledOutCallDetailsViewModel();
            List<HELPERS.BilledOutCallDetails> ListBilledOutCallDetails = new List<HELPERS.BilledOutCallDetails>();

            if (objCallResponse.lstCall != null)
            {
                int i = 0;
                foreach (PreTransacService.Call item in objCallResponse.lstCall)
                {
                    i++;
                    ListBilledOutCallDetails.Add(new HELPERS.BilledOutCallDetails()
                    {
                        NumOrden = i,
                        FechaHora = item.FECHA_HORA.ToString(),
                        TelefonoDestino = item.TELEFONO_DESTINO,
                        TipoTrafico = item.TIPO_DE_TRAFICO,
                        Duracion = item.DURACION,
                        Consumo = Convert.ToDecimal(item.CONSUMO),
                        CompradoRegalado = item.COMPRADO_REGALADO,
                        Saldo = item.SALDO,
                        Bolsa = item.BOLSA_ID,
                        Descripcion = item.DESCRIPCION,
                        Plan = item.PLAN,
                        Promoción = item.PROMOCION,
                        Destino = item.DESTINO,
                        Operador = item.OPERADOR,
                        GrupoCobro = item.GRUPO_DE_COBRO,
                        TipoRed = item.TIPO_DE_RED,
                        IMEI = item.IMEI,
                        Roaming = item.ROAMING,
                        ZonaTarifaria = item.ZONA_TARIFARIA,
                    });
                }


                objPrepServiceTransactionServices.ListBilledOutCallDetails = ListBilledOutCallDetails;
            }

            HELPERS.BilledCalltypification oloadTipificacion = new HELPERS.BilledCalltypification();

            var ID_TRANSACTION = string.Empty;
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, audit.transaction, oBilledCalltipificacion.IsTFI);
            if (oBilledCalltipificacion.IsTFI == Claro.SIACU.Constants.Yes)
                ID_TRANSACTION = ConfigurationManager.AppSettings("gConstkeyTransaccionLineaFijDetalleLLamada");
            else
                ID_TRANSACTION = ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaDetalleLLamada");

            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, audit.transaction, ID_TRANSACTION);

            oloadTipificacion = loadTipificacion(oBilledCalltipificacion.strIdSession, ID_TRANSACTION);

            oloadTipificacion.PhonfNroGener = PhonfNroGener;

            var msg = string.Format("{0} - {1}","N° Generado: ",oloadTipificacion.PhonfNroGener);
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, audit.transaction, msg);

            oloadTipificacion.columnvisib = ConfigurationManager.AppSettings("strGridVisualizaLlamada");

            msg = string.Format("{0} - {1}", "columnvisib: ",  ConfigurationManager.AppSettings("strGridVisualizaLlamada"));
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, audit.transaction, msg);

            objPrepServiceTransactionServices.BilledCalltipificacion = oloadTipificacion;
            return Json(new { data = objPrepServiceTransactionServices });
        }

        public JsonResult GetCallOutDetailsSearch(HELPERS.BilledCalltypification oBilledCallPerftipificacion)
        {
            PreTransacService.CallResponse objCallResponse;
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(oBilledCallPerftipificacion.strIdSession);
            var msg = string.Format("{0}", "GetCallOutDetailsSearch");
            Claro.Web.Logging.Info(oBilledCallPerftipificacion.strIdSession, audit.transaction, msg);

            audit.userName = ConfigurationManager.AppSettings("strUsrWSDatosPrePost");
            audit.applicationName = ConfigurationManager.AppSettings("strAPPWSDatosPrePost");
            PreTransacService.CallRequest objCallRequest =
                new PreTransacService.CallRequest()
                {
                    audit = audit,
                    strPerfilBuscar = oBilledCallPerftipificacion.PerfSearch,
                    strfechaInicio = oBilledCallPerftipificacion.StartDate,
                    strfechaFin = oBilledCallPerftipificacion.EndDate,
                    linea = oBilledCallPerftipificacion.PhonfNroGener,
                    strTipoConsulta = ConfigurationManager.AppSettings("strTipoConsultaWSDatosPrePost"),
                    tp = oBilledCallPerftipificacion.TrafType,
                };
            try
            {
                objCallResponse = Claro.Web.Logging.ExecuteMethod<PreTransacService.CallResponse>(
                    () =>
                    {
                        return oPreTransacService.GetCallOutDetailsLoad(objCallRequest);
                    });

                InsertInteractionBusiness2(oBilledCallPerftipificacion);

                msg = string.Format("Perfil:{0} - Email: {1}", oBilledCallPerftipificacion.PerfSearch, oBilledCallPerftipificacion.EmailUserValidator);
                Claro.Web.Logging.Info(oBilledCallPerftipificacion.strIdSession, audit.transaction, msg);

                if (oBilledCallPerftipificacion.PerfSearch.Equals(KEY.strUno))
                    if (oBilledCallPerftipificacion.EmailUserValidator != null)
                    {

                        GetSendEmail(oBilledCallPerftipificacion);
                    }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBilledCallPerftipificacion.strIdSession, objCallRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.Prepaid.BilledOutCallDetailsViewModel objPrepServiceTransactionServices = new Models.Prepaid.BilledOutCallDetailsViewModel();

            List<HELPERS.BilledOutCallDetails> ListBilledOutCallDetails = new List<HELPERS.BilledOutCallDetails>();

            if (objCallResponse != null && objCallResponse != null)
            {
                int i = 0;
                foreach (PreTransacService.Call item in objCallResponse.lstCall)
                {
                    i++;
                    ListBilledOutCallDetails.Add(new HELPERS.BilledOutCallDetails()
                    {
                        NumOrden = i,
                        FechaHora = item.FECHA_HORA.ToString(),
                        TelefonoDestino = item.TELEFONO_DESTINO,
                        TipoTrafico = item.TIPO_DE_TRAFICO,
                        Duracion = item.DURACION,
                        Consumo = Convert.ToDecimal(item.CONSUMO),
                        CompradoRegalado = item.COMPRADO_REGALADO,
                        Saldo = item.SALDO,
                        Bolsa = item.BOLSA_ID,
                        //Parametro_ID =item.,
                        Descripcion = item.DESCRIPCION,
                        Plan = item.PLAN,
                        Promoción = item.PROMOCION,
                        Destino = item.DESTINO,
                        Operador = item.OPERADOR,
                        GrupoCobro = item.GRUPO_DE_COBRO,
                        TipoRed = item.TIPO_DE_RED,
                        IMEI = item.IMEI,
                        Roaming = item.ROAMING,
                        ZonaTarifaria = item.ZONA_TARIFARIA,
                    });
                }


            objPrepServiceTransactionServices.ListBilledOutCallDetails = ListBilledOutCallDetails;
            }
            HELPERS.BilledCalltypification oBilledtipif = new HELPERS.BilledCalltypification()
            {
                columnvisib = ConfigurationManager.AppSettings("strGridVisualizaLlamada")
            };
            objPrepServiceTransactionServices.BilledCalltipificacion = oBilledtipif;
            return Json(new { data = objPrepServiceTransactionServices });

        }




        public JsonResult GetBilledOutCallExport(HELPERS.BilledCalltypification oBilledOutCallExport)
        {
            if (!oBilledOutCallExport.PerfExcel.Equals(KEY.strUno))
            {
                if (oBilledOutCallExport.EmailUserValidator != null)
                {
                    GetSendEmail(oBilledOutCallExport);
                }
            }
            EXCEL.ExcelHelper oExcelHelper = new EXCEL.ExcelHelper();
            string[] trafic;
            string desctrafic = string.Empty;
            if (oBilledOutCallExport.TrafType.IndexOf('|') > -1)
            {
                trafic = oBilledOutCallExport.TrafType.Split('|');
                oBilledOutCallExport.TrafType = trafic[0];
                desctrafic = trafic[1];
            }
            else
            {
                desctrafic = oBilledOutCallExport.TrafType;
            }

            var listCallDetails = ListCallDetailsCall(oBilledOutCallExport);

            Model.Prepaid.ExportExcelOutPrepaidModel objExportExcel = new Model.Prepaid.ExportExcelOutPrepaidModel();

            objExportExcel.PhoneNumber=oBilledOutCallExport.PhonfNroGener;
            objExportExcel.Trafic = desctrafic;

            objExportExcel.ListExportExcel = listCallDetails;
            List<string> lstRemove = new List<string>();
            List<string> lstParam = new List<string>();
            List<int> lstHelperPlan = ValidateExportExcel(objExportExcel.ListExportExcel, ref lstRemove);

            lstParam.Add(oBilledOutCallExport.Phone);
            lstParam.Add(oBilledOutCallExport.TrafType);
            string path = oExcelHelper.ExportExcelVisib(objExportExcel, ConfigurationManager.AppSettings("CONST_EXPORT_CALLDETAILS_PRE"), lstHelperPlan, lstRemove, lstParam);

            return Json(path);
        }

        public void GetSendEmail(HELPERS.BilledCalltypification oBilledOutCallExport)
        {
            var msg  = string.Format("Perfil:{0}","GetSendEmail"); 
            string strTemplateEmail = TemplateEmail(oBilledOutCallExport);

            string strDestinatarios = oBilledOutCallExport.EmailUserValidator;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oBilledOutCallExport.strIdSession);
             Claro.Web.Logging.Info(oBilledOutCallExport.strIdSession, AuditRequest.transaction, msg);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender =  strRemitente,
                    strTo = strDestinatarios,
                    strMessage = strTemplateEmail,
                    strSubject = ConfigurationManager.AppSettings("gConstAsuntoDetLlamadaSaliente")


                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oCommonTransacService.GetSendEmail(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBilledOutCallExport.strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }
                msg = string.Format("SendEmailResponse:{0} ", objGetSendEmailResponse.Exit);
                Claro.Web.Logging.Info(oBilledOutCallExport.strIdSession, AuditRequest.transaction, msg);

        }

        public void InsertInteractionBusiness2(HELPERS.BilledCalltypification oBilledCalltipificacion)
        {
            oBilledCalltipificacion.DataCreate = DateTime.Now.ToString();
            oBilledCalltipificacion.TypeInteraccion = ConfigurationManager.AppSettings("AtencionDefault");
            oBilledCalltipificacion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
            oBilledCalltipificacion.Result = ConfigurationManager.AppSettings("Ninguno");
            oBilledCalltipificacion.DoneinOne = Claro.Constants.NumberZero.ToString();
            oBilledCalltipificacion.Note = string.Format(" Información - Consulta Detalle de Llamadas Prepago desde {0} hasta {1}", oBilledCalltipificacion.StartDate, oBilledCalltipificacion.EndDate);
            oBilledCalltipificacion.FlagCase = Claro.Constants.NumberZero.ToString();
            oBilledCalltipificacion.UserProcess = ConfigurationManager.AppSettings("USRProcesoSU");
            
            CommonTransacService.BusinessInteraction2ResponseCommon objInteracResponse = new CommonTransacService.BusinessInteraction2ResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oBilledCalltipificacion.strIdSession);
            var msg = string.Format("{0} - {1} - StartDate: {2} - EndDate: {3}", "InsertInteractionBusiness2", oBilledCalltipificacion.UserProcess, oBilledCalltipificacion.StartDate, oBilledCalltipificacion.EndDate);
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, AuditRequest.transaction, msg);
            CommonTransacService.Iteraction Interaccion = new CommonTransacService.Iteraction()
            {
                OBJID_CONTACTO = oBilledCalltipificacion.ObjidContact,
                FECHA_CREACION = oBilledCalltipificacion.DataCreate,
                TELEFONO = oBilledCalltipificacion.PhonfNroGener,
                TIPO = oBilledCalltipificacion.TipoDes,
                CLASE = oBilledCalltipificacion.ClaseDes,
                SUBCLASE = oBilledCalltipificacion.SubClaseDes,
                TIPO_INTER = oBilledCalltipificacion.TypeInteraccion,
                METODO = oBilledCalltipificacion.Method,
                RESULTADO = oBilledCalltipificacion.Result,
                HECHO_EN_UNO = oBilledCalltipificacion.DoneinOne,
                NOTAS = oBilledCalltipificacion.Note,
                FLAG_CASO = oBilledCalltipificacion.FlagCase,
                USUARIO_PROCESO = oBilledCalltipificacion.UserProcess,
                AGENTE = oBilledCalltipificacion.CurrentUser,
                ES_TFI = oBilledCalltipificacion.IsTFI
            };

            CommonTransacService.BusinessInteraction2RequestCommon objInteracRequest =
                new CommonTransacService.BusinessInteraction2RequestCommon()
                {
                    audit = AuditRequest,
                    Item = Interaccion
                };
            try
            {
                objInteracResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.BusinessInteraction2ResponseCommon>(() => { return oCommonTransacService.GetInsertBusinnesInteraction2(objInteracRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBilledCalltipificacion.strIdSession, objInteracRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }

            if (objInteracResponse.InteractionId != null)
            {
                msg = string.Format("{0}", objInteracResponse.InteractionId);
            }
            else
            {
                msg = string.Format("{0}", "Interaccion null");
            }
            Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, AuditRequest.transaction, msg);
        }

        public void InsertAudit(HELPERS.BilledCalltypification oBilledCalltipificacion, int intResult)
        {
            var dblCodOpcion = Convert.ToDouble(ConfigurationManager.AppSettings("gConstOpcDetalleLlamadas"));
            var dblCodEvento = Convert.ToString(ConfigurationManager.AppSettings("gConstEvtVisualizaDetalleLLamadas"));
            var strLogin = oBilledCalltipificacion.CurrentUser;
            var strServicio = ConfigurationManager.AppSettings("strAuditoriaServicio");
            var strTransaccion = dblCodEvento;
            var strIpCliente = System.Web.HttpContext.Current.Request.UserHostAddress;
            var strCuentaUsuario = strLogin;
            var strTelefono = oBilledCalltipificacion.Phone;

            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oBilledCalltipificacion.strIdSession);
            CommonTransacService.SaveAuditRequestCommon objRegAuditRequest =
                new CommonTransacService.SaveAuditRequestCommon()
                {
                    audit = AuditRequest,
                    vCuentaUsuario = strCuentaUsuario,
                    vIpServidor =  AuditRequest.ipAddress,
                    vIpCliente = strIpCliente,
                    vServicio = strServicio,
                    vNombreCliente = oBilledCalltipificacion.Name + oBilledCalltipificacion.ApPat,
                    vTelefono = oBilledCalltipificacion.Phone

                };
            CommonTransacService.SaveAuditResponseCommon objRegAuditResponse = new CommonTransacService.SaveAuditResponseCommon();

            try
            {
                objRegAuditResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() => { return oCommonTransacService.SaveAudit(objRegAuditRequest); });
                string msg = string.Empty;

                msg = string.Format("Resultado: {0} - Estado: {1}", objRegAuditResponse.vResultado, objRegAuditResponse.vestado);
        
                Claro.Web.Logging.Info(oBilledCalltipificacion.strIdSession, AuditRequest.transaction, msg);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBilledCalltipificacion.strIdSession, AuditRequest.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }
        }

        public HELPERS.BilledCalltypification loadTipificacion(string strIdSession, string TransaccionValue)
        {

            PreTransacService.TipifCallOutPrepResponse objTipifResponse;
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "loadTipificacion");
            PreTransacService.TipifCallOutPrepRequest objTipifRequest =
                new PreTransacService.TipifCallOutPrepRequest()
                {
                    audit = audit,
                    vTransaccion = TransaccionValue
                };
            try
            {
                objTipifResponse = Claro.Web.Logging.ExecuteMethod<PreTransacService.TipifCallOutPrepResponse>(() => { return oPreTransacService.GetTipifCallOutPrep(objTipifRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTipifRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }


            HELPERS.BilledCalltypification oBilledCalltipificacion = new HELPERS.BilledCalltypification();

            if (objTipifResponse != null)
            {
                oBilledCalltipificacion.ClaseId = objTipifResponse.ClaseId;
                oBilledCalltipificacion.ClaseId = objTipifResponse.SubClaseId;
                oBilledCalltipificacion.TipoDes = objTipifResponse.TipoDes;
                oBilledCalltipificacion.ClaseDes = objTipifResponse.ClaseDes;
                oBilledCalltipificacion.SubClaseDes = objTipifResponse.SubClaseDes;

                var msg = string.Format("Tipificacion:  {0} {1} {2} {3} {4}", oBilledCalltipificacion.ClaseId, oBilledCalltipificacion.ClaseId, oBilledCalltipificacion.TipoDes, oBilledCalltipificacion.ClaseDes, oBilledCalltipificacion.SubClaseDes);
                Claro.Web.Logging.Info(strIdSession, audit.transaction, msg);
            }
            else
            {
                oBilledCalltipificacion.RespTipif = KEY.grstFalse;
                oBilledCalltipificacion.DescTipif = ConfigurationManager.AppSettings("gConstNoReconoceTipifiTransaccion");
                Claro.Web.Logging.Info(strIdSession, audit.transaction, oBilledCalltipificacion.DescTipif);
            }
            
            return oBilledCalltipificacion;

        }

        public string TemplateEmail(HELPERS.BilledCalltypification oBilledOutCallExport)
        {
            string strmessage = string.Empty;
            strmessage = "<html>";
            strmessage += "<head>";
            strmessage += "<style type='text/css'>";
            strmessage += "<!--";
            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strmessage += "-->";
            strmessage += "</style>";
            strmessage += "<body>";
            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Estimado(a) {0} </td>", oBilledOutCallExport.NamesUserValidator);// oBilledOutCallExport.Name, oBilledOutCallExport.ApPat, oBilledOutCallExport.ApMat);
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Se le informa que su Código y Password de Autorización ha sido utilizado para realizar una Transacción relacionada a Detalle de Llamadas desde las siguientes entradas</td></tr>";
            strmessage += "<tr><td height='10'></td>";

            strmessage += "<tr>";
            strmessage += "<td align='center'>";
            strmessage += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Nro. Telefónico :</td><td class='Estilo1'> {0} </td></tr>", oBilledOutCallExport.PhonfNroGener);
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Cuenta :</td><td class='Estilo1'>{0}</td></tr>", oBilledOutCallExport.Cuenta);
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Usuario Logueado:</td><td class='Estilo1'>{0}</td></tr>", oBilledOutCallExport.CurrentUser);
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Terminal o Computador :</td><td class='Estilo1'>{0}</td></tr>",System.Web.HttpContext.Current.Request.UserHostAddress);
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Fecha y Hora :</td><td class='Estilo1'>{0}</td></tr>", DateTime.Now.ToString());
            strmessage += "</Table>";
            strmessage += "</td></tr>";

            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Saludos Cordiales,</td></tr>";
            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";


            return strmessage;

        }

        public FileResult DownloadExcel(string strPath, string strNewfileName)
        {
            return File(strPath, "application/vnd.ms-excel", strNewfileName);
        }

        
        public List<HELPERS.ExportExcelPreCallDetails> ListCallDetailsCall(HELPERS.BilledCalltypification oBilledOutCallExport)
        {
            Transactions.Controllers.CommonServicesController octlPostpaid = new Transactions.Controllers.CommonServicesController();
            string PhonfNroGener = octlPostpaid.GetNumber(oBilledOutCallExport.strIdSession, true, oBilledOutCallExport.Phone);

            PreTransacService.CallResponse objCallResponse = new PreTransacService.CallResponse();
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(oBilledOutCallExport.strIdSession);
            Claro.Web.Logging.Info(oBilledOutCallExport.strIdSession, audit.transaction, "ListCallDetailsCall");
            audit.userName = ConfigurationManager.AppSettings("strUsrWSDatosPrePost");
            audit.applicationName = ConfigurationManager.AppSettings("strAPPWSDatosPrePost");
            PreTransacService.CallRequest objCallRequest =
                new PreTransacService.CallRequest()
                {
                    audit = audit,
                    strfechaInicio = oBilledOutCallExport.StartDate,
                    strfechaFin = oBilledOutCallExport.EndDate,
                    linea = PhonfNroGener,
                    strTipoConsulta = ConfigurationManager.AppSettings("strTipoConsultaWSDatosPrePost"),
                    IsTFI = oBilledOutCallExport.IsTFI,
                    tp = oBilledOutCallExport.TrafType,
                };
            try
            {
                objCallResponse = Claro.Web.Logging.ExecuteMethod<PreTransacService.CallResponse>(() => { return oPreTransacService.GetCallOutDetailsLoad(objCallRequest); });
                InsertAudit(oBilledOutCallExport, KEY.numeroUno);
            }
            catch (Exception ex)
            {
                InsertAudit(oBilledOutCallExport, KEY.numeroCero);
                Claro.Web.Logging.Error(oBilledOutCallExport.strIdSession, objCallRequest.audit.transaction, ex.Message);
            }
            List<HELPERS.ExportExcelPreCallDetails> list = new List<HELPERS.ExportExcelPreCallDetails>();
            int i = 0;
            foreach (PreTransacService.Call item in objCallResponse.lstCall)
                {
                    i++;
                    list.Add(new HELPERS.ExportExcelPreCallDetails {
                        Nro = i.ToString(),
                        FechaHora = item.FECHA_HORA.ToString(),
                        TelephoneDestin = item.TELEFONO_DESTINO,
                        TipoTrafico = item.TIPO_DE_TRAFICO,
                        Duracion = item.DURACION,
                        Consumo = item.CONSUMO,
                        CompradoRegalado = item.COMPRADO_REGALADO,
                        Saldo = item.SALDO,
                        BolsaId = item.BOLSA_ID,
                        Descripcion = item.DESCRIPCION,
                        Plan = item.PLAN,
                        Promocion = item.PROMOCION,
                        Destino = item.DESTINO,
                        Operador = item.OPERADOR,
                        GrupoCobro = item.GRUPO_DE_COBRO,
                        TipoRed = item.TIPO_DE_RED,
                        Imei = item.IMEI,
                        Roming = item.ROAMING,
                        ZoneTarifaria = item.ZONA_TARIFARIA,
                    });
                }
            return list;
        }


        public List<int> ValidateExportExcel(List<HELPERS.ExportExcelPreCallDetails> objRelationPlan, ref List<string> LstRemove)
        {
            HeaderAttribute headerAttribute;
            HELPERS.ExportExcelPreCallDetails oExportExcelPreCallDetails = new HELPERS.ExportExcelPreCallDetails();
            PropertyInfo[] properties = oExportExcelPreCallDetails.GetType().GetProperties();
            string columnvisibExcel = ConfigurationManager.AppSettings("strGridExcelLlamada");
            string[] arrcolumnvisibExcel = columnvisibExcel.Split(',');

            Type HeaderAttributeType = typeof(HeaderAttribute);
            var cont = 0;
            foreach (PropertyInfo prop in properties)
            {
                foreach (object attribute in prop.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == HeaderAttributeType)
                    {
                        headerAttribute = attribute as HeaderAttribute;
                        if (cont < arrcolumnvisibExcel.Length)
                            if (arrcolumnvisibExcel[cont] == KEY.grstFalse)
                            {
                                LstRemove.Add(headerAttribute.Title);
                            }
                        cont++;

                    }
                }
            }

            List<int> list = Enumerable.Range(0, cont - 1).ToList();

            return list;
        }


        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(ConfigurationManager.AppSettings("gConstkeyExportacionDetalleLLamada"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstkeyExportacionDetalleLLamadaAutorizada"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstkeyBuscarDetalleLLamadaAutorizada"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            Claro.Web.Logging.Info("1",App_Code.Common.GetTransactionID(),e.Message);
        }

    }
}