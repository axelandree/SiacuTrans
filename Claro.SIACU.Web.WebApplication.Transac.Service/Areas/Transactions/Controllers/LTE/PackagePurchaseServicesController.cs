using Claro.Entity;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.Controllers;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Common = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using KEY = Claro.ConfigurationManager;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers;
using System.Text;
using AutoMapper;
using Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase;
 

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class PackagePurchaseServicesController : CommonServicesController
    {
        public readonly CommonTransacService.CommonTransacServiceClient oCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        public readonly FixedTransacService.FixedTransacServiceClient oFixedTransacService = new FixedTransacService.FixedTransacServiceClient();



            public const string TIPO_TRANSACCION = ".Tipo transaccion";
            public const string NRO_CONTRATO = ".Nro de Contrato";
            public const string NRO_LINEA = ".Nro Linea";
            public const string CODIGO_OPERACION = ".Codigo De Operacion";
            public const string TIPO_OPERACION = ".Tipo de Operacion";
            public const string TIPO_PRODUCTO = ".Tipo de Producto";
            public const string CANAL_ATENCION = ".Canal de Atencion";
            public const string FECHA_ALTA = ".Fecha Alta";
            public const string FECHA_BAJA = ".Fecha Baja";
            public const string ESTADO_SERVICIO = ".Estado Servicio";
            public const string USUARIO = ".Username";
            public const string EXTENSION = ".Extension";
            public const string CODIGO_CLIENTE = ".Codigo de Cliente";

         

        public ActionResult LTEPackagePurchaseServices()
        {      
            ViewBag.strTipoVentaReinicia = ConfigurationManager.AppSettings("strTipoVentaReinicia");
            ViewBag.strMotivoDegradacion = ConfigurationManager.AppSettings("strMotivoDegradacion");
            ViewBag.strFacturada = ConfigurationManager.AppSettings("strFacturada");
            ViewBag.strClaroPuntos = ConfigurationManager.AppSettings("strClaroPuntos");
            ViewBag.strMsjClaroPuntos = ConfigurationManager.AppSettings("strMsjClaroPuntos");
            ViewBag.strCodTransPackagePurchaseFixedServLTE = ConfigurationManager.AppSettings("strCodTransPackagePurchaseFixedServLTE");
            ViewBag.strMsjNoPaquetesDisponibles = ConfigurationManager.AppSettings("strMsjNoPaquetesDisponibles");
            ViewBag.strNoPresentaVelocidadDegradada = ConfigurationManager.AppSettings("strNoPresentaVelocidadDegradadaLTE");
                 
            return View();
        }
        public JsonResult ConsultarPCRFDegradacion(string IdSession, string ContractIDSession)
        {            
            string DegradacionPlan = string.Empty;
            string DegradacionPaquetes = string.Empty;
            string Message = string.Empty;
            string strTelephone = string.Empty;
            CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarPCRFDegradacion");
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "IdSession: " + IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ContractIDSession: " + ContractIDSession);

            PCRFConnectorRequest objRequest2 = new PCRFConnectorRequest()
            {
                audit = oAuditRequest,
                strAccountId = ContractIDSession
            };
            PCRFConnectorResponse objResponse2 = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
            {
                return oCommonTransacService.ObtenerTelefonosClienteLTE(objRequest2);
            });

            strTelephone = objResponse2.strTelefonoLTE;
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "strTelephone: " + strTelephone);

            PCRFConnectorResponse objPCRFConnectorResponse = new PCRFConnectorResponse();
            try
            {

                PCRFConnectorRequest objPCRFConnectorRequest = new PCRFConnectorRequest()
                {
                    audit=oAuditRequest,
                    strLinea = strTelephone
                };

                objPCRFConnectorResponse = Claro.Web.Logging.ExecuteMethod<PCRFConnectorResponse>(() =>
                {
                    return oCommonTransacService.ConsultarPCRFDegradacion(objPCRFConnectorRequest); 
                });

                foreach (var item in objPCRFConnectorResponse.listSuscriberQuota)
                {
                         string strSRVNAME = item.SRVNAME.Replace("S_RTDD_","");
                         int temp;
                         if (int.TryParse(strSRVNAME, out temp))
                         {
                             DegradacionPlan = item.QTABALANCE == "0" ? "SI" : "NO";
                         }
                         else if (strSRVNAME.Equals("S_RTDD"))
                         {
                             DegradacionPlan = item.QTABALANCE == "0" ? "SI" : "NO";
                         }
                         else
                             DegradacionPaquetes = item.QTABALANCE == "0" ? "SI" : "NO";
                 }
            }

            catch (Exception ex)
            {
                objPCRFConnectorResponse.codRespuesta = "-1";
                Claro.Web.Logging.Error("IdSession : " + IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarPCRFDegradacion");
           return Json(new { codRespuesta = objPCRFConnectorResponse.codRespuesta ,DegradacionPlan = DegradacionPlan, DegradacionPaquetes= DegradacionPaquetes });
        }      
        public JsonResult ConsultarClaroPuntos(string IdSession, string tipoDocumento, string numeroDocumento)
        {
            string Message = string.Empty;
            CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarClaroPuntos");
            ConsultarClaroPuntosResponse objConsultarClaroPuntosResponse = new ConsultarClaroPuntosResponse();
            try
            {
                CommonTransacService.HeaderRequest oHeaderRequest = new CommonTransacService.HeaderRequest
                {
                    consumer = ConfigurationManager.AppSettings("consumer"),
                    country = ConfigurationManager.AppSettings("country"),
                    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                    language = ConfigurationManager.AppSettings("language"),
                    modulo = ConfigurationManager.AppSettings("modulo"),
                    msgType = ConfigurationManager.AppSettings("msgType"),
                    operation = ConfigurationManager.AppSettings("strOperatioConsultarClaroPuntos"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    wsIp = App_Code.Common.GetApplicationIp()
                };

                ConsultarClaroPuntosBodyRequest oBodyRequest = new ConsultarClaroPuntosBodyRequest()
                {
                    tipoConsulta = ConfigurationManager.AppSettings("ClaroPuntoTipoConsultaLTE"),                  
                    tipoDocumento =tipoDocumento,
                    numeroDocumento = numeroDocumento,
                    bolsa = ConfigurationManager.AppSettings("ClaroPuntosBolsaLTE"),
                    tipoPuntos = ConfigurationManager.AppSettings("ClaroPuntosTipoPuntosLTE"),
                };

                ConsultarClaroPuntosHeaderRequest oConsultarClaroPuntosHeader = new ConsultarClaroPuntosHeaderRequest()
                {
                    HeaderRequest = oHeaderRequest
                };

                ConsultarClaroPuntosMessageRequest oMessageRequest = new ConsultarClaroPuntosMessageRequest
                {
                    Header = oConsultarClaroPuntosHeader,
                    Body = oBodyRequest,
                };

                ConsultarClaroPuntosRequest objConsultarClaroPuntosRequest = new ConsultarClaroPuntosRequest()
                {
                    MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };

                  objConsultarClaroPuntosResponse = Claro.Web.Logging.ExecuteMethod<ConsultarClaroPuntosResponse>(() =>
                {
                    return oCommonTransacService.ConsultarClaroPuntos(objConsultarClaroPuntosRequest); 
                });

                  Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "objConsultarClaroPuntosResponse: " + objConsultarClaroPuntosResponse.MessageResponse.Body.codigoRespuesta);
                  Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "objConsultarClaroPuntosResponse: " + objConsultarClaroPuntosResponse.MessageResponse.Body.mensajeRespuesta);
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " + IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarClaroPuntos");
            return Json(new { data = objConsultarClaroPuntosResponse });
        }
        public JsonResult ConsultarPaqDisponibles(string IdSession, string idCategoria, string idContrato, string codigoCategoria, string prepagoCode, string tmCode)
        {
            string Message = string.Empty;
            CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarPaqDisponibles");
            ConsultarPaqDisponiblesResponse objConsultarPaqDisponiblesResponse = new ConsultarPaqDisponiblesResponse();

            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;

            try{
                CommonTransacService.HeaderRequest oHeaderRequest = new CommonTransacService.HeaderRequest
                {
                    consumer = ConfigurationManager.AppSettings("consumer"),
                    country = ConfigurationManager.AppSettings("country"),
                    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                    language = ConfigurationManager.AppSettings("language"),
                    modulo = ConfigurationManager.AppSettings("modulo"),
                    msgType = ConfigurationManager.AppSettings("msgType"),
                    operation = ConfigurationManager.AppSettings("strOperatioObtenerProductosPlan"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    wsIp =  App_Code.Common.GetApplicationIp()
                };

                ConsultarPaqDisponiblesBodyRequest oBodyRequest = new ConsultarPaqDisponiblesBodyRequest()
                {
                    idCategoria = idCategoria,
                    idContrato = idContrato,
                    codigoCategoria = codigoCategoria,
                    prepagoCode = prepagoCode,
                    tmCode = tmCode

                };

                ConsultarPaqDisponiblesHeaderRequest oConsultarPaqDisponiblesHeader = new ConsultarPaqDisponiblesHeaderRequest()
                {
                    HeaderRequest = oHeaderRequest
                };

                ConsultarPaqDisponiblesMessageRequest oMessageRequest = new ConsultarPaqDisponiblesMessageRequest
                {
                    Header = oConsultarPaqDisponiblesHeader,
                    Body = oBodyRequest,
                };

                ConsultarPaqDisponiblesRequest objConsultarPaqDisponiblesRequest = new ConsultarPaqDisponiblesRequest()
                {
                    MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };

                objConsultarPaqDisponiblesResponse = Claro.Web.Logging.ExecuteMethod<ConsultarPaqDisponiblesResponse>(() =>
                {
                    return oCommonTransacService.ConsultarPaqDisponibles(objConsultarPaqDisponiblesRequest);
                });
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "objConsultarPaqDisponiblesResponse: " + objConsultarPaqDisponiblesResponse.MessageResponse.Body.defaultServiceResponse.idRespuesta);
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "objConsultarPaqDisponiblesResponse: " + objConsultarPaqDisponiblesResponse.MessageResponse.Body.defaultServiceResponse.mensaje);

                day = DateTime.Now.Day.ToString();
                month = DateTime.Now.Month.ToString();
                year = DateTime.Now.Year.ToString();

            }

            catch (Exception ex)
            {
               Claro.Web.Logging.Error(IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarPaqDisponibles");
            return Json(new { data = objConsultarPaqDisponiblesResponse, day = day, month = month, year = year });
        }
        public JsonResult SaveTransaccionLTEPackagePurchase(Model.LTE.PackagePurchaseServicesModel oModel)
        {
             string rInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string vDesInteraction = string.Empty;
            string vDescCAC = string.Empty;
            string vresultado = string.Empty;
            string Mensaje = string.Empty;
            string strRutaArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            string strNumeroIntentos = string.Empty;
            byte[] byteArchivoSamba = null;
            var errorMessage = "";
            string strTelephone = string.Empty;
            oModel.fechaActual = DateTime.Now.ToShortDateString();
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var oInteraccion = new Model.InteractionModel();
            Claro.Web.Logging.Info(" Método : " + "SaveTransaccionLTEPackagePurchase", "INICIO", oModel.strIdSession);
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.strIdSession);
            CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);

            Claro.Web.Logging.Info(" Método : " + "SaveTransaccionLTEPackagePurchase", "oModel.strContrato: ", oModel.strContrato);
            Claro.Web.Logging.Info(" Método : " + "SaveTransaccionLTEPackagePurchase", "oModel.strTelefono: ", oModel.strTelefono);

            try
            {
                #region COMPRARPAQUETES

                PCRFConnectorRequest objRequest2 = new PCRFConnectorRequest()
                {
                    audit = oAuditRequest,
                    strAccountId = oModel.strContrato
                };
                PCRFConnectorResponse objResponse2 = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
                {
                    return oCommonTransacService.ObtenerTelefonosClienteLTE(objRequest2);
                });

                strTelephone = objResponse2.strTelefonoLTE;
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "SaveTransaccionLTEPackagePurchase strTelephone: " + strTelephone);

                oModel.strTelefono = strTelephone;
                oModel.strNumeroServicio = strTelephone;

                Claro.Web.Logging.Info(" Método : " + "SaveTransaccionLTEPackagePurchase", "oModel.strTelefono set: ", oModel.strTelefono);
                Claro.Web.Logging.Info(" Método : " + "SaveTransaccionLTEPackagePurchase", "oModel.strNumeroServicio set: ", oModel.strNumeroServicio);


                ComprarPaquetesBodyResponse objComprarPaquetesResponse = new ComprarPaquetesBodyResponse();
                    objComprarPaquetesResponse = ComprarPaquetes(oModel);
                     #endregion

                     if (objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.codigoRespuesta=="0")
                     {
                        #region INTERACCION
                        var result = new Dictionary<string, string>();
                        var lstaDatTemplate = new List<string>();
                        string idInteraccion = string.Empty;

                        string rFlagInsertion = string.Empty;
                        string rMsgText = string.Empty;

                        Dictionary<string, string> oInteraction;
                        oInteraction = InsertBusinessInteraction2(oModel);
                        var item = new List<string>();
                        foreach (KeyValuePair<string, string> par in oInteraction)
                        {
                            item.Add(par.Value);
                        }
                        rInteractionId = item[0];
                        rFlagInsertion = item[1];
                        rMsgText = item[2];
                        Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rInteractionId: ", rInteractionId);
                        Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rFlagInsertion: ", rFlagInsertion);
                        Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rMsgText: ", rMsgText);
                        oModel.strCasoInteraccion = rInteractionId;

                        #endregion 
               
                        if (!string.IsNullOrEmpty(rInteractionId))
                        {

                        if (rInteractionId != "null")
                        {

                            try
                            {
                                #region GENERAR CONSTANCIA PDF
                                oModel.intNumeroIntentos = Convert.ToInt(ConfigurationManager.AppSettings("strNumeroIntentosReinicia"));
                                GetConstancyPDF(oModel.strIdSession, rInteractionId, oModel);

                                #endregion

                                if (oModel.bGeneratedPDF)
                                {
                                    oModel.strDocument = string.IsNullOrEmpty(oModel.strFullPathPDF) ? string.Empty : oModel.strFullPathPDF.Substring(oModel.strFullPathPDF.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                                    #region OBTENER ARCHIVO SAMBA Y ENVIO DE CORREO

                                    if (DisplayFileFromServerSharedFile(oModel.strIdSession, rInteractionId, oModel.strFullPathPDF, out byteArchivoSamba))
                                    {
                                        if (byteArchivoSamba != null && oModel.ChkEmail)
                                        {
                                            MensajeEmail = sendCorreoSB(oModel, byteArchivoSamba);
                                        }
                                        else
                                        {
                                            MensajeEmail = "No se envio la constancia.";
                                        }
                                    }
                                    else
                                    {
                                        MensajeEmail = "No se pudo obtener la Constancia.";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    errorMessage = "Error al momento de generar la Constancia";
                                }
                            }
                            catch (Exception ex)
                            {
                                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                                errorMessage = ex.Message;
                            }
                        }
                        else {
                            errorMessage = "Error al momento de generar Interaccion.";
                        }
                            
                        }
                         else
                        {
                        errorMessage = "Error al momento de generar Interaccion.";
                        }

                     }
                     else
                     {
                        if (objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta != "")
                        {
                            errorMessage = objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta;
                        }
                        else
                        {
                            errorMessage = "Error al momento de generar la Comprar de Paquetes.";
                        }
                    
                     }
            }
            catch (Exception ex)
            {
                   Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                   errorMessage = "Error al momento de generar la Comprar de Paquetes.";
            }

             oModel.strMensajeEmail = MensajeEmail;
             oModel.strErrorMessage = errorMessage;
             oModel.btArchivoSamba  = byteArchivoSamba; 
            Claro.Web.Logging.Info(" Método : " + "SaveTransaccionIFIPackagePurchase", "FIN", oModel.strIdSession);
            return Json(new { data = oModel });
        }
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.LTE.PackagePurchaseServicesModel oModel)
        {
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;
            string strTypeTransaction = string.Empty;
            string documentName = string.Empty;
            string xml = string.Empty;
            bool generado = false;

            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            ParametersGeneratePDF parameters = null;

            strNombreArchivo = ConfigurationManager.AppSettings("consPackgePurchaseServiceLTE");
            Claro.Web.Logging.Info(" Método : " + "GetConstancyPDF", "strTypeTransaction: " + strTypeTransaction, "Inicio");
            string strBoolEnvioCorreo = oModel.ChkEmail ? "SI" : "NO";
           try
            {
                 

                parameters = new ParametersGeneratePDF();
                parameters.StrTitularCliente = oModel.strNombreCliente;
                parameters.StrTipoDocIdentidad = oModel.strTipoDocIdentidad;
                parameters.strRepLegNroDocumento = oModel.strNumeroDoc;
                parameters.StrTelfReferencia = oModel.strTelefono;
                
                parameters.StrRepresLegal = oModel.strRepresentanteLegal;
                parameters.strNroDoc = oModel.strNumeroDoc;
                parameters.StrNroDocIdentidad = oModel.strNumeroDoc;
                parameters.StrCentroAtencionArea = oModel.strPuntoAtencion;
                
                parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_CompraPaqueteFijaLTE");
                parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCompraPaquetesLTE");
                parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("consLTEPackgePurchaseService");
                parameters.StrCasoInter = strInteraccionId;

              
                parameters.strTipoCliente = oModel.strTipoCliente;
                parameters.strPaqueteVelDegradada = oModel.PqtVelocidadDegradacion;
                parameters.strVigenciaPaquete = oModel.strVigencia;
                parameters.strPrecioPaquete = "S/ " + oModel.strPrecioPaquete;
                parameters.strMBIncluidos = oModel.strMBIncluidos;
                parameters.strEnvioCorreo = oModel.ChkEmail ? "SI" : "NO";
                parameters.strCorreoCliente = oModel.strEmailCliente;
                parameters.StrCodigoAsesor = oModel.strCodigoAsesor;
                parameters.StrNombreAsesor = oModel.strNombreAsesor;
                parameters.strCodeTCRM = ConfigurationManager.AppSettings("strCodeTCRM");
                parameters.strCuenta = oModel.strNumeroCuenta;
                parameters.strNumeroServicio = oModel.strNumeroServicio;
                parameters.strTipoVenta = oModel.strTipoVenta;
                parameters.StrEmail = oModel.strEmailCliente;
                parameters.flagCargFijoServAdic = "1";

                string strCodigoOnBase = string.Empty;
                parameters.StrFechaTransaccionProgram = (DateTime.Now).ToString("dd/MM/yyyy");
                GenerateConstancyResponseCommon response = PackageGenerateContancyPDF(strIdSession, parameters, GetLstKeywordRequest(oModel), out strCodigoOnBase);

                oModel.byteArchivoSamba = response.bytesConstancy;

                generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                oModel.Document = response.Document;
                oModel.strCodOnBase = strCodigoOnBase;
                listResponse.Add("ruta", response.Document);
                listResponse.Add("respuesta", generado);
                listResponse.Add("nombreArchivo", strNombreArchivo);

 

            }
            catch (Exception ex)
            {
                oModel.bGeneratedPDF = false;
                Claro.Web.Logging.Error(strIdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo :  GetConstancyPDF - Fín ", "nombrepath : " + strNombreArchivo);

            return listResponse;
        }
       

        public string   sendCorreoSB(Model.LTE.PackagePurchaseServicesModel oModel, byte[] objFile)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);           
            string cuerpo = ConfigurationManager.AppSettings("strCuerpoCorreoLTEPackagePurchase");
            string strMsgEmailConsultaLlameGratis = ConfigurationManager.AppSettings("strMsgEmailConsultaLlameGratis");
            SendEmailSBModel objSendEmailSBModel = null;
            string strMessage = string.Empty;
            string strResul = string.Empty;

            #region "Body Email"

                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + cuerpo + "</td></tr>";
                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Cordialmente,</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>"+strMsgEmailConsultaLlameGratis+"</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

            var objRequest = new SendEmailSBRequest
            {
                audit = audit,
                SessionId = oModel.strIdSession,
                TransactionId = audit.transaction,
                strAppID = audit.ipAddress,
                strAppCode = audit.applicationName,
                strAppUser = audit.userName,
                strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente"),
                strDestinatario =  oModel.strEmailCliente,
                strAsunto =ConfigurationManager.AppSettings("strAsuntoLTEPackagePurchase"),
                strMensaje = strMessage,
                strHTMLFlag ="1",
                Archivo = objFile,
                strNomFile = oModel.strDocument
            };

            try
            {
                var objResponse = Claro.Web.Logging.ExecuteMethod(() => oCommonTransacService.GetSendEmailSB(objRequest));

                if (objResponse != null)
                {
                    objSendEmailSBModel = new SendEmailSBModel()
                    {
                        idTransaccion = objResponse.idTransaccion,
                        codigoRespuesta = objResponse.codigoRespuesta,
                        mensajeRespuesta = objResponse.mensajeRespuesta
                    };
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, objRequest.audit.transaction, ex.Message);
               
            }

            return strResul;
        }
       

        public ComprarPaquetesBodyResponse ComprarPaquetes(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            string Message = string.Empty;
            CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ComprarPaquetes");
            ComprarPaquetesBodyResponse objComprarPaquetesResponse = new ComprarPaquetesBodyResponse();

            string CadenaPaquetesDescripcionIFI = ConfigurationManager.AppSettings("PaquetesDescripcionIFI").ToString().ToUpper();
            string CadenaPaquetesSncodesIFI = ConfigurationManager.AppSettings("PaquetesSncodesIFI").ToString().ToUpper();

            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Paquetes " + CadenaPaquetesDescripcionIFI);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Sncodes " + CadenaPaquetesSncodesIFI);

            string[] PaquetesDescripcionIFI = CadenaPaquetesDescripcionIFI.Split(',');
            string[] PaquetesSncodesIFI = CadenaPaquetesSncodesIFI.Split(',');
            string paqueteSncode = string.Empty;

            for (int i = 0; i < PaquetesDescripcionIFI.Length; i++)
            {
                if (oModel.PqtVelocidadDegradacion.ToUpper() == PaquetesDescripcionIFI[i].ToString())
                {
                    paqueteSncode = PaquetesSncodesIFI[i].ToString();
                }
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Sncode " + paqueteSncode);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes FlagActivacionCampo " + ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionCampo"));
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes FlagActivacionValor " + ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionValor"));


            try
            {
                //string cargoFijo = obtenerCargoFijo(oModel.strIdSession, oModel.strCustomerID).ToString();

                oModel.strCargoFijo = GetDatosporNroDocumentos(oModel);

                CommonTransacService.HeaderRequest oHeaderRequest = new CommonTransacService.HeaderRequest
                {
                    consumer = ConfigurationManager.AppSettings("consumer"),
                    country = ConfigurationManager.AppSettings("country"),
                    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                    language = ConfigurationManager.AppSettings("language"),
                    modulo = ConfigurationManager.AppSettings("modulo"),
                    msgType = ConfigurationManager.AppSettings("msgType"),
                    operation = ConfigurationManager.AppSettings("strOperatioComprarPaquetes"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    wsIp = App_Code.Common.GetApplicationIp()
                };


                ComprarPaquetesBodyRequest oBodyRequest = new ComprarPaquetesBodyRequest()
                {
                    msisdn = oModel.strTelefono,
                    monto = oModel.strPrecioPaquete,
                    paquete = paqueteSncode,
                    customerId = oModel.strCustomerID,
                    planBase = oModel.strPlanBase,
                    tipoProducto = ConfigurationManager.AppSettings("ComprarPaquetesTipoProductoLTE"),               
                    tipoCliente = ConfigurationManager.AppSettings("ComprarPaquetesTipoClienteLTE"),
                    cicloFact = oModel.strCicloFacturacion,
                    fechaAct =oModel.fechaActual,
                    cargoFijo = oModel.strCargoFijo,
                    tipoPago = oModel.strTipoVenta,
                    departamento = oModel.strDepartamento,
                    provincia = oModel.strProvincia,
                    distrito = oModel.strDistrito,
                    listaOpcionalType = new List<ComprarPaquetesListaOpcionalType>()
                    {
                        new ComprarPaquetesListaOpcionalType()
                        {
                            campo = ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionCampo"),
                            valor = ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionValor")
                        }
                    }
                };

                ComprarPaquetesHeaderRequest oComprarPaquetesHeader = new ComprarPaquetesHeaderRequest()
                {
                    HeaderRequest = oHeaderRequest
                };

                ComprarPaquetesMessageRequest oMessageRequest = new ComprarPaquetesMessageRequest
                {
                    Header = oComprarPaquetesHeader,
                    Body = oBodyRequest,
                };

                ComprarPaquetesRequest objComprarPaquetesRequest = new ComprarPaquetesRequest()
                {
                    MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };

                objComprarPaquetesResponse = Claro.Web.Logging.ExecuteMethod<ComprarPaquetesBodyResponse>(() =>
                {
                    return oCommonTransacService.ComprarPaquetes(objComprarPaquetesRequest);
                });

                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "codigoRespuesta: " + objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.codigoRespuesta);
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "mensajeRespuesta: " + objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " + oModel.strIdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ComprarPaquetes");
            return objComprarPaquetesResponse;
        }
        public Dictionary<string, string> InsertBusinessInteraction2(Model.LTE.PackagePurchaseServicesModel oModel)
        {

            Dictionary<string, string> result = new   Dictionary<string, string>();
            var oInteraccion = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                oInteraccion.ObjidContacto = oModel.strObjidContacto;
                oInteraccion.StartDate = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = KEY.AppSettings("gConstKeyCustomerInteract") + oModel.strCustomerID;
                oInteraccion.Type = oModel.TIPO;
                oInteraccion.TypeCode = oModel.TIPO_CODE;
                oInteraccion.Class = oModel.CLASE;
                oInteraccion.ClassCode = oModel.CLASE_CODE;
                oInteraccion.SubClass = oModel.SUBCLASE;
                oInteraccion.SubClassCode = oModel.SUBCLASE_CODE;
                oInteraccion.Type_Interaction = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.Constants.NumberZeroString;
                oInteraccion.Note = oModel.strNotas;
                 
                oInteraccion.FlagCase = "0";
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;

                result = GetInsertInteractionCLFY(oInteraccion, oModel.strIdSession);


            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }
            return result;

        }
        public JsonResult GetInsertInteractionTemplate(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            var dictionaryResponse = new Dictionary<string, object>();
            var itemPlantilla = new List<object>();
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
            TemplateInteractionModel objInteractionTemplateModel = GetDataTemplateInteraction(oModel);
             var serviceModelInteraction = Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objInteractionTemplateModel);
            try
            {
              if (objInteractionTemplateModel != null)
              {
                   dictionaryResponse = InsertPlantInteraction(objInteractionTemplateModel, oModel.strCasoInteraccion, oModel.strTelefono, oModel.strCodigoAsesor, strUserAplication, strPassUser, true, oModel.strIdSession);

                  foreach (KeyValuePair<string, object> par in dictionaryResponse)
                   {
                       itemPlantilla.Add(par.Value);
                   }
                   Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "CodOnbase: ", oModel.strCodOnBase);
                   Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rInteractionId: ", oModel.strCasoInteraccion);
                   Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rFlagInsertion: ", itemPlantilla[1].ToString());
                   Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rMsgText: ", itemPlantilla[2].ToString());
              }  

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return Json(new { data = itemPlantilla });
        }
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.strIdSession);
            try
            {
                oPlantillaCampoData.X_INTER_16 =  oModel.strCodOnBase;
                oPlantillaCampoData.X_CLARO_NUMBER = oModel.strTelefono;
                oPlantillaCampoData.X_FIRST_NAME = oModel.strNombreCliente;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }
            return oPlantillaCampoData;
        }
   
        public JsonResult GetUploadDocumentOnBase(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Execute GetUploadDocumentOnBase");

            CommonTransacService.ParametersGeneratePDF parameters = new CommonTransacService.ParametersGeneratePDF();
            byte[] objFile = null;
            CommonTransacService.GenerateConstancyResponseCommon response = new GenerateConstancyResponseCommon();
            OnBaseCargaResponse objResponse = new OnBaseCargaResponse();
            try
            {
                parameters.StrCasoInter = oModel.Constancia.NRO_CASO_INTERACCION;
                parameters.StrCarpetaTransaccion = KEY.AppSettings("strCompraPaqueteFijaTransaccionLTE");
                parameters.StrNombreArchivoTransaccion = oModel.strTransactionFormat == null ? "CONSTANCIA_COMPRA_PAQUETE_FIJA" : oModel.strTransactionFormat;
                parameters.StrCarpetaPDFs = KEY.AppSettings("strCompraPaqueteFijaCarpetaPDFsLTE");
                parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");

                oModel.Constancia.FORMATO_TRANSACCION = oModel.strTransactionFormat;
                oModel.Constancia.TRANSACCION = KEY.AppSettings("strCompraPaqueteFijaTransaccionLTE") + oModel.strTransactionFormat;
                oModel.Constancia.FECHA_SOLICITUD = DateTime.Now.ToString("dd/MM/yyyy");

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                oCommonHandler.DisplayFileFromServerSharedFile(oModel.strIdSession, oModel.strTransactionFormat, oModel.strFullPathPDF, out objFile);


                string strExtencion = KEY.AppSettings("strCompraPaqueteFijaExtencionPDFLTE");

                if (objFile != null)
                {
                    oModel.strPath = (oModel.strDocument.ToUpper().Contains(strExtencion.ToUpper()) ? oModel.strDocument : oModel.strDocument + strExtencion);
                    oModel.strOperationType = KEY.AppSettings("strCompraPaqueteFijaTransaccionLTE") + oModel.strTransactionFormat;
                    oModel.strRegisterDate = DateTime.Now.ToString("o");

                    return Json(new { codeResponse = "0", OnBase = OnBaseTarget(oModel, objFile), Constancia = response });
                }
                else
                {
                    throw new Exception("GetUploadDocumentOnBase: No se generó la constancia " + audit.transaction);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }
        }

        public OnBaseCargaResponse OnBaseTarget(Model.LTE.PackagePurchaseServicesModel oModel, byte[] file)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Execute OnBaseTarget");
            Claro.Web.Logging.Info(audit.Session, audit.transaction,
            string.Format("Execute OnBaseTarget - Parámetros de entrada objRequest: [strConsultantCode] - [{0}] ; [strTransactionFormat] - [{1}] ; [strMetadatosName] - [{2}] ; [strMetadatosValue] - [{3}] ; [strMetadatosLength] - [{4}] ", oModel.strCodigoAsesor, oModel.strTransactionFormat, oModel.strKeyWorkName, oModel.strKeyWorkValue, oModel.strKeyWorkLeng));
            FixedTransacService.OnBaseCargaResponse objResponse = new OnBaseCargaResponse();

            try
            {
                if (oModel != null)
                {
                    string[] strMetadatosName = (oModel.strKeyWorkName.Length > 0 ? oModel.strKeyWorkName.Split(',') : null);
                    string[] strMetadatosValue = (oModel.strKeyWorkValue.Length > 0 ? oModel.strKeyWorkValue.Split(',') : null);
                    string[] strMetadatosLength = (oModel.strKeyWorkLeng.Length > 0 ? oModel.strKeyWorkLeng.Split(',') : null);

                    string valor;
                    List<FixedTransacService.metadatosOnBase> metaDatos = new List<FixedTransacService.metadatosOnBase>();

                    for (int i = 0; i < strMetadatosValue.Length; i++)
                    {
                        try
                        {
                            valor = (string)oModel.Constancia.GetType().GetProperty(strMetadatosValue[i]).GetValue(oModel.Constancia);

                            if (valor != null) if (!valor.Trim().Equals("")) if (strMetadatosLength[i] != "0")
                                        valor = valor.Substring(0, int.Parse(strMetadatosLength[i]));
                            metaDatos.Add(new FixedTransacService.metadatosOnBase()
                            {
                                attributeName = strMetadatosName[i],
                                attributeValue = valor
                            });
                        }
                        catch (Exception ex)
                        {
                            valor = "";
                        }
                    }

                    objResponse = oFixedTransacService.TargetDocumentoOnBase(new OnBaseCargaRequest()
                    {
                        audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(oModel.strIdSession),
                        HeaderDPService = App_Code.Common.GetHeaderDataPowerRequest<FixedTransacService.HeaderDPRequest>(oModel.strModule, "CargaOnBase"),
                        user = oModel.strCodigoAsesor,
                        metadatosOnBase = metaDatos,
                        SpecificationAttachmentOnBase = new FixedTransacService.SpecificationAttachmentOnBase()
                        {
                            name = oModel.strPath,
                            type = oModel.strTransactionFormat,
                            listEntitySpectAttach = new FixedTransacService.entitySpecAttachExtensionOnBase()
                            {
                                ID = oModel.strIdSession,
                                fileBase64 = System.Convert.ToBase64String(file)
                            }
                        }
                    });

                    Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("Execute objResponse - Parámetros de salida objRequest: [CodigoOnBase] - [{0}] ;", objResponse.codeOnBase));

                    return objResponse;
                }
                else
                {
                    throw new Exception("oModel es null");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "ERROR: OnBaseTarget" + ex.Message);
                return new FixedTransacService.OnBaseCargaResponse() { codeOnBase = "", codeResponse = "1", descriptionResponse = ex.Message };
            }
        }

        public JsonResult GetKeyConfig(string strIdSession, string strfilterKeyName)
        {
            string value = "";
            value = KEY.AppSettings(strfilterKeyName);
            JsonResult json = Json(new { data = value });
            return json;
        }

        public GenerateConstancyResponseCommon GenerateContancyPDF_WithXML(string idSession, ParametersGeneratePDF parameters)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            var strTerminacionPDF = ConfigurationManager.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession)
            };
            Logging.Info(idSession, "", "Begin GenerateContancyPDF");
            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return oCommonTransacService.GetGenerateContancyPDF(request);
            });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());


            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", string.Format("End GenerateContancyPDF_Reinicia result: {0}, FullPathPDF: {1}", objResponse.Generated.ToString(), objResponse.FullPathPDF));
            return objResponse;
        }

        public string GetDatosporNroDocumentos(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            string strCargofijo = "0.00";
            Claro.Web.Logging.Info(oModel.strIdSession, "INI - GetDatosporNroDocumentos","");

            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);

            string strTipoDocIdentidad = oModel.strTipoDocIdentidad == "DNI" ? "2" : "3";
            Claro.Web.Logging.Info(oModel.strIdSession, " strIdSession: ", oModel.strIdSession);
            Claro.Web.Logging.Info(oModel.strIdSession, " transaction: ", objAuditRequest.transaction);
            Claro.Web.Logging.Info(oModel.strIdSession, " strTipoDocIdentidad: ", strTipoDocIdentidad);
            Claro.Web.Logging.Info(oModel.strIdSession, " strNumeroDoc: ", oModel.strNumeroDoc);

            try
            {
             
               var objDatos = new List< Client>();
               objDatos = Claro.Web.Logging.ExecuteMethod<List<Client>>(() =>
                {
                    return new CommonTransacService.CommonTransacServiceClient().GetDatosporNroDocumentos(oModel.strIdSession,
                                                                                                         objAuditRequest.transaction,
                                                                                                          strTipoDocIdentidad,
                                                                                                          oModel.strNumeroDoc,
                                                                                                          "");
                });

               if (objDatos.Count>0)
                    strCargofijo = objDatos[0].CARGO;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                strCargofijo = "0.00";

            }
            Claro.Web.Logging.Info(oModel.strIdSession, "INI - GetDatosporNroDocumentos", "");
            return strCargofijo;
        }

        //CAYCHO
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest> GetLstKeywordRequest(Model.LTE.PackagePurchaseServicesModel oModel)
        {
            List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest> lstKeywordRequest = new List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest>();
            string Cadena = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameLTE").ToString();
            string[] KeywordRequest = Cadena.Split(',');

          

            try
            {
                string Campos = string.Empty;
                CommonTransacService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "GetLstKeywordRequest");

                int day_now = DateTime.Now.Day;
                int year_now = DateTime.Now.Year;
                int day =  (!string.IsNullOrEmpty(oModel.strCicloFacturacion)? Convert.ToInt(oModel.strCicloFacturacion) : DateTime.Now.Day );

                string DateBaja = string.Empty;
                if (day_now > day)
                {
                    int year_temp = DateTime.Now.AddMonths(1).Year;

                    if (year_temp == year_now)
                    {
                        DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2, '0') + "/" + year_now.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2, '0') + "/" + year_temp.ToString().PadLeft(4, '0');
                    }


                }
                else {
                    DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + year_now.ToString().PadLeft(4, '0');
                }

                string FORMATO_FECHA_ALTA = ConfigurationManager.AppSettings("FORMATO_FECHA_ALTA").ToString();
                string FORMATO_FECHA_BAJA = ConfigurationManager.AppSettings("FORMATO_FECHA_BAJA").ToString();
                for (int i = 0; i < KeywordRequest.Length; i++)
                {
                    Campos = string.Empty;
                    Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest objKeywordRequest = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest();
                    Campos = KeywordRequest[i].ToString() == null ? string.Empty : KeywordRequest[i].ToString();


                    if (TIPO_TRANSACCION == Campos) {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameLTE_TipoTransaccion").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                         
                    }

                    if (NRO_CONTRATO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strContrato;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (NRO_LINEA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strTelefono;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CODIGO_OPERACION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oAuditRequest.transaction;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (TIPO_OPERACION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameLTE_TIPO_OPERACION").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }
                    if (TIPO_PRODUCTO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("ComprarPaquetesTipoProductoLTE").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CANAL_ATENCION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strPuntoAtencion;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (FECHA_ALTA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = (DateTime.Now).ToString(FORMATO_FECHA_ALTA); // DateTime.Now.ToString("mm/dd/yyyy");  //  DateTime.Now.Month.ToString().PadLeft(2, '0')+ "/" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString().PadLeft(4, '0');

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (FECHA_BAJA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = Utils.CheckDate(DateBaja).ToString(FORMATO_FECHA_BAJA); ;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (ESTADO_SERVICIO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = "A";

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (USUARIO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.CurrentUser;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (EXTENSION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strExtensionOnBase").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CODIGO_CLIENTE == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strCustomerID;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : ", "Message Error : ", ex.Message);
            }
            return lstKeywordRequest;
        }

        public GenerateConstancyResponseCommon PackageGenerateContancyPDF(string idSession, ParametersGeneratePDF parameters, List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_KeywordRequest> MethodKeywords, out string strCodigoOnBase)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            var strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");
            strCodigoOnBase = string.Empty;

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(idSession)
            };

            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return oCommonTransacService.GetConstancyPDFWithOnbase(request);
            });

            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;

                objResponse.bytesConstancy = objResponse.bytesConstancy;

                if (MethodKeywords != null)
                {
                    Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UploadDocumentOnBaseResponse();
                    Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UploadDocumentOnBaseRequest()
                    {

                        listaDocumentos = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.E_DocumentRequest()
                        {
                            ListaMetadatos = MethodKeywords,
                            Seq = KEY.AppSettings("strConstSEQ"),
                            CodigoOnBase = KEY.AppSettings("strCodigoOnBase"),
                            CodigoTCRM = request.ParametersGeneratePDFGeneric.strCodeTCRM,
                            strExtension = KEY.AppSettings("strExtensionOnBase"),
                            TipoDocumentoOnBase = KEY.AppSettings("TipoDocumentoOnBaseReiniciaLTE"),
                            abytArchivo = System.Convert.ToBase64String(objResponse.bytesConstancy,0, objResponse.bytesConstancy.Length)
                            //abytArchivo =  objResponse.bytesConstancy.ToString()
                        },
                        audit = request.audit
                    };

                    try
                    {
                        objUploadDocumentOnBaseResponse = oCommonTransacService.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest);

                        if (objUploadDocumentOnBaseResponse != null)
                        {

                            if (objUploadDocumentOnBaseResponse.E_Document.Estado == "OK")
                            {
                                //objResponse.FullPathPDF = strNamePDF;
                                //objResponse.Path = strNamePath;
                                //objResponse.Document = strDocumentName;
                                strCodigoOnBase = objUploadDocumentOnBaseResponse.E_Document.CodigoOnBase; //Código OnBase
                            }
                            else
                            {
                                objResponse.Generated = false;
                                objResponse.FullPathPDF = string.Empty;
                                objResponse.Path = string.Empty;
                                objResponse.Document = string.Empty;
                                strCodigoOnBase = string.Empty; //Código OnBase
                                Logging.Error(idSession, request.audit.transaction, "Error al registrar Onbase." + objUploadDocumentOnBaseResponse.E_Document.Estado);
                            }
                        }
                        else
                        {
                            objResponse.FullPathPDF = string.Empty;
                            objResponse.Path = string.Empty;
                            objResponse.Document = string.Empty;
                            strCodigoOnBase = string.Empty; //Código OnBase
                        }
                    }
                    catch (Exception ex)
                    {
                        objResponse.FullPathPDF = string.Empty;
                        objResponse.Path = string.Empty;
                        objResponse.Document = string.Empty;
                        strCodigoOnBase = string.Empty; //Código OnBase
                        objResponse.Generated = false;
                        Logging.Error(idSession, request.audit.transaction, "Error al registrar Onbase." + ex.Message);
                    }
                   
                }

                Logging.Info(idSession, request.audit.transaction, " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.Generated = false;
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
                strCodigoOnBase = string.Empty; //Código OnBase
            }


            return objResponse;
        }

    }
}