using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using MODELS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using ConstantsFixed = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using FIXED = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper;
using AutoMapper;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Linq;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;


namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Fixed
{

    public class RestricSellInfoPromotionController : Controller
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        public ActionResult RestricSellInfoPromotion()
        {
            return PartialView();
        }

        public ActionResult UploadInfoProm(string strNumeroDocumento,string strTelefono)
        {

            ViewBag.numeroDocumento = strNumeroDocumento;
            ViewBag.Telefono = strTelefono;
            
            return PartialView();
        }


        public JsonResult SearchStateLineEmail(string strIdSession, string strNroTelefono, string strCorreo)
        {
            SearchStateLineEmailResponse objSearchStateLineEmailResponse = new SearchStateLineEmailResponse();
            SearchStateLineEmailRequest objSearchStateLineEmailRequest = new SearchStateLineEmailRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.SearchStateLineEmailMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.SearchStateLineEmailHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("consultaEstadoLineaCorreo")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.SearchStateLineEmailBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.SearchStateLineEmailBodyRequest()
            {
                nroTelefono = strNroTelefono,
                correo = strCorreo
            };

            objSearchStateLineEmailRequest.MessageRequest.Body = objBodyRequest;
            objSearchStateLineEmailRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objSearchStateLineEmailResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.SearchStateLineEmailResponse>(() =>
                {
                    return _oServiceFixed.SearchStateLineEmail(objSearchStateLineEmailRequest);
                });
                //objSearchStateLineEmailResponse = _oServiceFixed.SearchStateLineEmail(objSearchStateLineEmailRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objSearchStateLineEmailResponse = null;
                Claro.Web.Logging.Error(strIdSession, objSearchStateLineEmailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSearchStateLineEmailRequest.audit.transaction);
            }
            Areas.Transactions.Models.Fixed.InfoPromotionModel objInfoPromotionModel = new Models.Fixed.InfoPromotionModel();

            if (objSearchStateLineEmailResponse.MessageResponse != null)
            {
                if (objSearchStateLineEmailResponse.MessageResponse.Body != null)
                {
                    if (objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente != null)
                    {
                        //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Generacion de Modelo SearchDiscardMatricesModel");
                        //  List<HELPER_ITEM.DataClientHelper> lstDataClient = new List<HELPER_ITEM.DataClientHelper>();
                        HELPER_ITEM.DataClientHelper objDataClient = new HELPER_ITEM.DataClientHelper();


                        objDataClient.cliId = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.cliId;
                        objDataClient.tipoDoc = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.tipoDoc;
                        objDataClient.tipoDocDesc = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.tipoDocDesc;
                        objDataClient.nroDocumento = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.nroDocumento;
                        objDataClient.nombresYApellidos = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.nombresYApellidos;
                        objDataClient.email = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.email;
                        objDataClient.tipoCliente = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.tipoCliente;
                        objDataClient.origen = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.origen;
                        objDataClient.usuarioCrea = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.usuarioCrea;
                        objDataClient.fechaCrea = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.fechaCrea;
                        objDataClient.usuarioModi = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.usuarioModi;
                        objDataClient.fechaModi = objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.fechaModi;

                        if (objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.contactos != null)
                        {
                            List<HELPER_ITEM.ContactHelper> lstContact = new List<HELPER_ITEM.ContactHelper>();

                            foreach (WebApplication.Transac.Service.FixedTransacService.contactos itemE in objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.contactos)
                            {
                                lstContact.Add(new HELPER_ITEM.ContactHelper()
                                {
                                    cliId = itemE.cliId,
                                    contId = itemE.contId,
                                    tipoDocContact = itemE.tipoDocContact,
                                    tipoDocDescContact = itemE.tipoDocDescContact,
                                    nroDocContact = itemE.nroDocContact,
                                    nombresContact = itemE.nombresContact,
                                    tipoContact = itemE.tipoContact

                                });
                                objDataClient.contactos = lstContact;
                            }
                        }
                        //lstDataClient.Add(objDataClient);


                        objInfoPromotionModel.datosCliente = objDataClient;


                        if (objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.detalle != null)
                        {
                            // Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Generacion de Modelo SearchDiscardMatricesModel");
                            List<HELPER_ITEM.DetailsHelper> lstDetails = new List<HELPER_ITEM.DetailsHelper>();
                            foreach (WebApplication.Transac.Service.FixedTransacService.detalle itemD in objSearchStateLineEmailResponse.MessageResponse.Body.datosCliente.detalle)
                            {
                                lstDetails.Add(new HELPER_ITEM.DetailsHelper()
                                {
                                    listId = itemD.listId,
                                    cliId = itemD.cliId,
                                    msisdn = itemD.msisdn,
                                    servId = itemD.servId,
                                    contacto = itemD.contacto,
                                    desContactCanal = itemD.desContactCanal,
                                    contactAplic = itemD.contactAplic,
                                    tipoMedioAprob = itemD.tipoMedioAprob,
                                    interacId = itemD.interacId,
                                    fechaRespuesta = itemD.fechaRespuesta,
                                    estadoInfo = itemD.estadoInfo,
                                    tipoLinea = itemD.tipoLinea
                                });
                            }

                            objInfoPromotionModel.datosCliente.detalle = lstDetails;
                        }
                    }

                }
            }
            JsonResult json = Json(new { data = objInfoPromotionModel });

            return json;

        }

        public JsonResult UpdateStateLineEmail(string strIdSession, WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailRequest objNodeRequest)
        {
            UpdateStateLineEmailResponse objUpdateStateLineEmailResponse = new UpdateStateLineEmailResponse();
            UpdateStateLineEmailRequest objUpdateStateLineEmailRequest = new UpdateStateLineEmailRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("actualizarEstadoLineaCorreo")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailBodyRequest()
            {
                tipoOperacion = objNodeRequest.MessageRequest.Body.tipoOperacion,
                listaServicios = (objNodeRequest.MessageRequest.Body.listaServicios == null ? (new List<WebApplication.Transac.Service.FixedTransacService.ListServices>()) : objNodeRequest.MessageRequest.Body.listaServicios),
                origenFuente = (objNodeRequest.MessageRequest.Body.origenFuente == null ? "" : objNodeRequest.MessageRequest.Body.origenFuente),
                usuario = (objNodeRequest.MessageRequest.Body.usuario == null ? "" : objNodeRequest.MessageRequest.Body.usuario),
                fechaRegistro = Functions.CheckStr(DateTime.Now)
            };

            objUpdateStateLineEmailRequest.MessageRequest.Body = objBodyRequest;
            objUpdateStateLineEmailRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objUpdateStateLineEmailResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailResponse>(() =>
                {
                    return _oServiceFixed.UpdateStateLineEmail(objUpdateStateLineEmailRequest);
                });
                // objUpdateStateLineEmailResponse = _oServiceFixed.UpdateStateLineEmail(objUpdateStateLineEmailRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objUpdateStateLineEmailResponse = null;
                Claro.Web.Logging.Error(strIdSession, objUpdateStateLineEmailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objUpdateStateLineEmailRequest.audit.transaction);
            }

            return Json(new { data = objUpdateStateLineEmailResponse.MessageResponse.Body });
        }
        public WebApplication.Transac.Service.FixedTransacService.HeaderRequest getHeaderRequest(string operation)
        {
            return new WebApplication.Transac.Service.FixedTransacService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloLeyPromo"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp() //"172.19.91.216" 
            };
        }

        public JsonResult GetKeyConfig(string strIdSession, string strfilterKeyName)
        {
            string value = "";
            value = KEY.AppSettings(strfilterKeyName);
            JsonResult json = Json(new { data = value });
            return json;
        }

        [HttpPost]
        public JsonResult ConsultarDatosUsuarioClarify(string srtIdSession, string strTransaction, string strTelefono, string strCuenta, string strcontactobjid_1, string strflag_registrado)
        {
            Areas.Transactions.Models.Fixed.ListConsultarDatosUsuarioModel objListConsultarDatosUsuarioModel = new Models.Fixed.ListConsultarDatosUsuarioModel();

            GetClientRequestCommon oClienteRequest = new GetClientRequestCommon()
            {
                strflagreg = strflag_registrado,
                strContactobjid = strcontactobjid_1,
                straccount = strCuenta,
                strphone = strTelefono,
                audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(srtIdSession)
            };

            GetClientResponseComon oClienteResponse = Claro.Web.Logging.ExecuteMethod<GetClientResponseComon>(() =>
            {
                return _oServiceCommon.GetObClient(oClienteRequest);
            });

            JsonResult json = Json(new { data = oClienteResponse });

            return json;
        }

        public JsonResult GrabarTxt()
        {

            string xxtipo = "C";
            string mensaje = "Se proceso correctamente";
            List<string> oListaNewLine = new List<string>();
            bool proceso = true;

            string strDNI = Request.Form["strDNI"];
            string strTelefono = Request.Form["strTelefono"];
            string strCheck = Request.Form["strCheck"];

            if (strCheck == "1") //Todos
            {
                if (strDNI == "")
                {
                    xxtipo = "I";
                    mensaje = "El dni no puede estar vacio";
                    proceso = false;
                }
                else if (strTelefono == "")
                {
                    xxtipo = "I";
                    mensaje = "El telefono no puede estar vacio";
                    proceso = false;
                }
                if (proceso)
                {

                    string codigoRespuesta = string.Empty;
                    string mensajeRespuesta = string.Empty;
                    List<HELPER_ITEM.lineaConsolidadaHelper> oListaDni =
                    new List<HELPER_ITEM.lineaConsolidadaHelper>();
                    oListaDni = ObtenerLineaPorDNI(strDNI,
                                                   strTelefono,
                                                   out codigoRespuesta,
                                                   out mensajeRespuesta);
                    if (codigoRespuesta == "2")
                    {
                        xxtipo = "I";
                        mensaje = KEY.AppSettings("strLeyPromoErrorLineasWS2");
                    }
                    else if (codigoRespuesta == "3")
                    {
                        xxtipo = "I";
                        mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                    }
                    else if (codigoRespuesta == "-1")
                    {
                        xxtipo = "I";
                        mensaje = KEY.AppSettings("strLeyPromoconstMensajeRespuesta03");
                    }
                    else if (codigoRespuesta == "-2")
                    {
                        xxtipo = "I";
                        mensaje = KEY.AppSettings("strLeyPromoiframeTimeoutMsgError");
                    }
                    else if (codigoRespuesta == "-3")
                    {
                        xxtipo = "I";
                        mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                    }
                    else
                    {
                        if (codigoRespuesta == "0")
                        {
                            foreach (var item in oListaDni)
                            {
                                //if (item.msisdn.Length > 9) { item.msisdn = item.msisdn.Substring((item.msisdn.Length - 9) - 1); }
                                oListaNewLine.Add(item.msisdn);
                            }
                            xxtipo = "C";
                            mensaje = "Se proceso correctamente";
                        }
                        else
                        {
                            xxtipo = "I";
                            mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                        }
                    }
                }
            }
            else if (strCheck == "2") //Algunas
            {

                string strProcessAnyLine = Request.Form["strProcessAnyLine"];
                if (strProcessAnyLine == "0")
                {
                    xxtipo = "I";
                    mensaje = KEY.AppSettings("strLeyPromoErrorNoProcesoTxt");
                }

            }
            else
            {
                strCheck = "0";
                oListaNewLine.Clear();
                xxtipo = "I";
                mensaje = "No seleccionó ninguna opción";
            }

            JsonResult json = Json(new { data = ENMessage.GetMessage(xxtipo, mensaje), listaLineas = oListaNewLine, TipoCargaMasiva = strCheck });
            return json;
        }

        public JsonResult CargarTxt()
        {

            string xxtipo = "C";
            string mensaje = "Se proceso correctamente";
            List<string> oListaNewLine = new List<string>();
            bool proceso = true;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase upload = Request.Files[0];
                int maxsize = int.Parse((KEY.AppSettings("strLeyPromoSizeMaximoMBCargaVAS")));

                string fileName = Path.GetFileName(upload.FileName);
                string ruta = Server.MapPath("~/" + KEY.AppSettings("strLeyPromoRutaTemporal"));
                string rutaFinal = Path.Combine(ruta, fileName);
                int size = upload.ContentLength;

                string strDNI = Request.Form["strDNI"];
                string strTelefono = Request.Form["strTelefono"];

                if (size > maxsize)
                {
                    xxtipo = "I";
                    mensaje = "El tamaño del archivo excede el permitido";
                    proceso = false;
                }
                else if (Path.GetExtension(rutaFinal) == "txt")
                {
                    xxtipo = "I";
                    mensaje = "El formato del archivo es incorrecto";
                    proceso = false;
                }
                else if (strDNI == "")
                {
                    xxtipo = "I";
                    mensaje = "El dni no puede estar vacio";
                    proceso = false;
                }
                else if (strTelefono == "")
                {
                    xxtipo = "I";
                    mensaje = "El telefono no puede estar vacio";
                    proceso = false;
                }

                if (proceso)
                {

                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }

                    if (System.IO.File.Exists(rutaFinal))
                    {
                        System.IO.File.Delete(rutaFinal);
                    }
                    upload.SaveAs(rutaFinal);

                    //Exista el Archivo
                    if (rutaFinal != "")
                    {
                        if (System.IO.File.Exists(rutaFinal))
                        {
                            List<string> oListaArchivo = new List<string>();
                            oListaArchivo = CargarDatosArchivo(rutaFinal);
                            if (oListaArchivo != null)
                            {
                                if (oListaArchivo.Count > 0)
                                {
                                    string codigoRespuesta = string.Empty;
                                    string mensajeRespuesta = string.Empty;
                                    List<HELPER_ITEM.lineaConsolidadaHelper> oListaDni =
                                    new List<HELPER_ITEM.lineaConsolidadaHelper>();
                                    oListaDni = ObtenerLineaPorDNI(strDNI,
                                                                   strTelefono,
                                                                   out codigoRespuesta,
                                                                   out mensajeRespuesta);

                                    if (System.IO.File.Exists(rutaFinal))
                                    {
                                        System.IO.File.Delete(rutaFinal);
                                    }

                                    if (codigoRespuesta == "2")
                                    {
                                        xxtipo = "I";
                                        mensaje = KEY.AppSettings("strLeyPromoErrorLineasWS2");
                                    }
                                    else if (codigoRespuesta == "3")
                                    {
                                        xxtipo = "I";
                                        mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                                    }
                                    else if (codigoRespuesta == "-1")
                                    {
                                        xxtipo = "I";
                                        mensaje = KEY.AppSettings("strLeyPromoconstMensajeRespuesta03");
                                    }
                                    else if (codigoRespuesta == "-2")
                                    {
                                        xxtipo = "I";
                                        mensaje = KEY.AppSettings("strLeyPromoiframeTimeoutMsgError");
                                    }
                                    else if (codigoRespuesta == "-3")
                                    {
                                        xxtipo = "I";
                                        mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                                    }
                                    else
                                    {
                                        if (codigoRespuesta == "0")
                                        {

                                            if (oListaDni != null)
                                            {
                                                foreach (var itemArchivo in oListaArchivo)
                                                {
                                                    if (itemArchivo.Length == 11)
                                                    {
                                                        Int64 oLinea = 0;
                                                        if (Int64.TryParse(itemArchivo, out oLinea))
                                                        {
                                                            string strZIPCodePeru = KEY.AppSettings("strLeyPromoZIPCodePeru");
                                                            if (itemArchivo.Substring(0, 2) == strZIPCodePeru)
                                                            {
                                                                foreach (var itemLinea in oListaDni)
                                                                {
                                                                    if (itemArchivo == itemLinea.msisdn)
                                                                    {
                                                                        oListaNewLine.Add(itemArchivo);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                xxtipo = "I";
                                                                mensaje = KEY.AppSettings("strLeyPromoMsgErrorValidacion847");
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            xxtipo = "I";
                                                            mensaje = KEY.AppSettings("strLeyPromoMsgErrorValidacion847");
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        xxtipo = "I";
                                                        mensaje = KEY.AppSettings("strLeyPromoMsgErrorValidacion847");
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            xxtipo = "I";
                                            mensaje = KEY.AppSettings("strLeyPromoMsjeErrorConsulta");
                                        }
                                    }
                                }
                                else
                                {
                                    xxtipo = "I";
                                    mensaje = "El archivo txt esta vacio";
                                }
                            }
                            else
                            {
                                xxtipo = "I";
                                mensaje = "Ocurrio un error al cargar el Archivo";
                            }
                        }
                    }
                    else
                    {
                        xxtipo = "I";
                        mensaje = "Ocurrio un error al cargar el Archivo";
                    }

                }

            }
            else
            {
                xxtipo = "I";
                mensaje = "No se ha cargado ningun archivo";
            }

            if (xxtipo == "I")
            {
                oListaNewLine.Clear();
            }
            else if (xxtipo == "C")
            {
                mensaje = oListaNewLine.Count.ToString();
            }

            JsonResult json = Json(new { data = ENMessage.GetMessage(xxtipo, mensaje), listaLineas = oListaNewLine });
            return json;
        }

        private ListlineaConsolidadaModel ListValidateLine(string strIdSession,
                                                           string strDNI,
                                                           string straplicativo,
                                                           string StrTipoDoc,
                                                           string strnombreCampo,
                                                           out string codigoRespuesta,
                                                           out string mensajeRespuesta)
        {
            contarLineasResponse objResponse = new contarLineasResponse();
            contarLineasRequest objRequest = new contarLineasRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.AuditRequest>(strIdSession),
                numeroDocumento = strDNI,
                valor = StrTipoDoc,
                straplicativo = straplicativo,
                nombreCampo = strnombreCampo
            };


            try
            {
                objResponse = _oServiceFixed.GetListValidateLine(objRequest);
                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                codigoRespuesta = objResponse.codRespuesta;
                mensajeRespuesta = objResponse.msjRespuesta;
            }
            catch (Exception ex)
            {
                objResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }
            Areas.Transactions.Models.Fixed.ListlineaConsolidadaModel objListlineaConsolidadaModel = new Models.Fixed.ListlineaConsolidadaModel();

            if (objResponse.listaLineasConsolidadasType != null)
            {
                if (objResponse.listaLineasConsolidadasType.lineaConsolidada != null)
                {
                    if (objResponse.listaLineasConsolidadasType.lineaConsolidada.Count > 0)
                    {
                        int cantidad = objResponse.listaLineasConsolidadasType.lineaConsolidada.Count;

                        List<HELPER_ITEM.lineaConsolidadaHelper> ArrListaLineas = new List<HELPER_ITEM.lineaConsolidadaHelper>();
                        for (int i = 0; i < cantidad; i++)
                        {

                            if ((objResponse.listaLineasConsolidadasType.lineaConsolidada[i].segmento.ToUpper().Contains(straplicativo)) == true)
                            {
                                HELPER_ITEM.lineaConsolidadaHelper obj = new HELPER_ITEM.lineaConsolidadaHelper();
                                obj.msisdn = objResponse.listaLineasConsolidadasType.lineaConsolidada[i].msisdn;
                                obj.segmento = objResponse.listaLineasConsolidadasType.lineaConsolidada[i].segmento;
                                ArrListaLineas.Add(obj);
                                // log.Info(string.Format("Datos de Salida: msisdn:{0},segmento:{1}", obj.msisdn, obj.segmento));
                            }
                        }
                        objListlineaConsolidadaModel.listaLineasConsolidadasType.lineaConsolidada = ArrListaLineas;
                    }
                }

            }
            return objListlineaConsolidadaModel;
        }



        private List<HELPER_ITEM.lineaConsolidadaHelper> ObtenerLineaPorDNI(string strDNI,
                                                                            string strTelefono,
                                                                            out string codigoRespuesta,
                                                                            out string mensajeRespuesta)
        {
            string strIdSession = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string strDNI = "32403057";
            string straplicativo = KEY.AppSettings("strLeyPromoModalidadPostpago");
            string StrTipoDoc = string.Empty;
            string strnombreCampo = KEY.AppSettings("strLeyPromoTIPO_DOC_BL");

            if (strDNI.Length == 8)
                StrTipoDoc = "DNI";
            else if (strDNI.Length == 11)
                StrTipoDoc = "RUC";

            List<HELPER_ITEM.lineaConsolidadaHelper> oLista = new List<HELPER_ITEM.lineaConsolidadaHelper>();
            List<HELPER_ITEM.lineaConsolidadaHelper> oListaNew = new List<HELPER_ITEM.lineaConsolidadaHelper>();
            oLista = ListValidateLine(strIdSession,
                                      strDNI,
                                      straplicativo,
                                      StrTipoDoc,
                                      strnombreCampo,
                                      out codigoRespuesta,
                                      out mensajeRespuesta).listaLineasConsolidadasType.lineaConsolidada;

            LineaQuitar(oLista, strTelefono, out oListaNew);
            return oListaNew;
        }

        private List<string> CargarDatosArchivo(string rutaArchivo)
        {
            List<string> oLista = new List<string>();
            if (rutaArchivo != string.Empty)
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    using (StreamReader reader = new StreamReader(rutaArchivo, Encoding.UTF8))
                    {
                        string linea = string.Empty;
                        while ((linea = reader.ReadLine()) != null)
                        {
                            oLista.Add(linea);
                        }
                    }
                }
            }
            return oLista;
        }

        private void LineaQuitar(List<HELPER_ITEM.lineaConsolidadaHelper> oLista,
                                 string strTelefono,
                                 out List<HELPER_ITEM.lineaConsolidadaHelper> oListaNew)
        {
            oListaNew = new List<HELPER_ITEM.lineaConsolidadaHelper>();
            string msisdn = strTelefono;
            if (msisdn.Length == 9)
            {
                msisdn = KEY.AppSettings("strLeyPromoZIPCodePeru") + msisdn;
            }
            foreach (var item in oLista)
            {
                if (item.msisdn != msisdn)
                {
                    oListaNew.Add(item);
                }
            }
        }

        public string RegisterNuevaInteraccion(string idSession, MODELS.InteractionModel oInteraccion, string tipo)
        {
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(idSession);
            var interaccion = new Interaccion()
            {
                clase = oInteraccion.Class,
                subClase = oInteraccion.SubClass,
                objId = oInteraccion.ObjidContacto,
                siteObjId = string.IsNullOrEmpty(oInteraccion.ObjidSite) ? "" : oInteraccion.ObjidSite,
                codigoSistema = ConfigurationManager.AppSettings("USRProcesoSU"),
                notas = (string.IsNullOrEmpty(oInteraccion.Note) ? "" : oInteraccion.Note),
                codigoEmpleado = oInteraccion.Agenth,
                flagCaso = oInteraccion.FlagCase,
                hechoEnUno = oInteraccion.MadeOne,
                metodoContacto = oInteraccion.Method,
                telefono = oInteraccion.Telephone,
                textResultado = oInteraccion.Result,
                tipo = oInteraccion.Type,
                tipoInteraccion = oInteraccion.TypeInter,
                cuenta = ""
            };
            var request = new RegisterNuevaInteraccionRequest()
            {
                interaccion = interaccion,
                txId = audit.transaction,
                audit = audit
            };
            Claro.Web.Logging.Info(idSession, audit.transaction, "Begin RegisterNuevaInteraccion " + tipo);
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return _oServiceFixed.RegisterNuevaInteraccion(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, request.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(idSession, audit.transaction, string.Format("End RegisterNuevaInteraccion {0} - result: {1}", tipo, result));
            return result;
        }

        public JsonResult GenerarArchivoCsv(string strIdSession, WebApplication.Transac.Service.FixedTransacService.UpdateStateLineEmailRequest objNodeRequest)
        {
            Boolean Result = false;


            CommonTransacService.AuditRequest audit =
                 Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            try
            {

                string strLeyPromoColumnsCSV = KEY.AppSettings("strLeyPromoColumnsCSV");
                var ListColumnsCSV = strLeyPromoColumnsCSV.Split('|');


                StringBuilder csvExport = new StringBuilder();

                csvExport.AppendLine(string.Format(
                       "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                        ListColumnsCSV[0], ListColumnsCSV[1], ListColumnsCSV[2], ListColumnsCSV[3], ListColumnsCSV[4], ListColumnsCSV[5], ListColumnsCSV[6], ListColumnsCSV[7], ListColumnsCSV[8], ListColumnsCSV[9], ListColumnsCSV[10], ListColumnsCSV[11], ListColumnsCSV[12], ListColumnsCSV[13], ListColumnsCSV[14], ListColumnsCSV[15], ListColumnsCSV[16], ListColumnsCSV[17], ListColumnsCSV[18]));

                foreach (var row in objNodeRequest.MessageRequest.Body.listaServicios)
                {
                    csvExport.AppendLine(
                        string.Format(
                        "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                        row.msisdn, row.tipoDoc, row.tipoDocDes, row.nroDoc, row.nombApell, row.email, row.tipoCliente, row.origen, row.tipoLinea, row.tipoDocContact, row.tipoDocDescContact, row.nroDocContact, row.nombresContact, row.tipoContact, row.servId, row.contact, row.desContactCanal, Functions.CheckStr(DateTime.Now).ToString(), row.estadoInfo));
                }

                string Ruta = Server.MapPath("~") + "\\" + KEY.AppSettings("strLeyPromoRutaTemporal");

                String NombreArchivo = KEY.AppSettings("strLeyPromoNombreArchCSV");
                String FormatHora = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                String FormatoArchivo = (((NombreArchivo + "_" + FormatHora).Replace("/", "").Replace(":", "")).Replace("-", "")).Replace(" ", "");
                String ExtensionArchivo = KEY.AppSettings("strLeyPromoExtensionCSV");

                System.IO.File.WriteAllText(Ruta + "\\" + FormatoArchivo + "." + ExtensionArchivo, csvExport.ToString());

                ConnectionSFTP objRequest = new ConnectionSFTP();
                objRequest.KeyUser = KEY.AppSettings("strUserSFTPLeyPromoSFTP");
                objRequest.KeyPassword = KEY.AppSettings("strPassSFTPLeyPromoSFTP");
                objRequest.port = KEY.AppSettings("strLeyPromoPuertoSFTP");
                objRequest.path_Destination = @KEY.AppSettings("strLeyPromoPathSFTP");
                objRequest.server = KEY.AppSettings("strLeyPromoServidorSFTP");

                Result = UploadSFTP(strIdSession, objRequest, Ruta, FormatoArchivo + "." + ExtensionArchivo);

            }

            catch (Exception ex)
            {
                Result = false;
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }

            return Json(new { ResultResponse = (Result == true ? "0" : "1") });

        }


        public JsonResult RegistrarNoCliente(MODELS.Fixed.RegistroNoCliente oModel)
        {
            bool resultado = false;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.idSession);

            try
            {

                resultado = new FixedTransacServiceClient().GetInsertNoCliente(new Customer()
                {
                    audit = audit,
                    Telephone = oModel.Telefono,
                    User= oModel.Usuario,
                    Name = oModel.Nombre,
                    LastName = oModel.Apellidos,
                    BusinessName = oModel.RazonSocial,
                    DocumentType = oModel.DocumentoTipo,
                    DocumentNumber = oModel.DocumentoNumero,
                    Address = oModel.Direccion,
                    District = oModel.Distrito,
                    Departament = oModel.Departamento,
                    Modality = oModel.Modalidad
                });
                

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.idSession, audit.transaction, ex.Message);
                return Json(new { codigoRespuesta = "2", mensajeRespuesta = ex.ToString() });
            }

            if (!resultado)
                return Json(new { codigoRespuesta = "1", mensajeRespuesta = "No se registro al NO cliente" });
            else
                return Json(new { codigoRespuesta = "0", mensajeRespuesta = "" });
        }


        public JsonResult GenerarTipificacion(MODELS.Fixed.ParametersInteraccion oModel)
        {
            String idInteraction = "";
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);

            try
            {
                var oInteraccion = DatosInteraccion(oModel);
                 idInteraction = RegisterNuevaInteraccion(oModel.strIdSession, oInteraccion, "LEY DE PROMOCIONES");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);

            }

            if (idInteraction == "")
                return Json(new { codeResponse = "1", idInteraction = "" });
            else
                return Json(new { codeResponse = "0", idInteraction = idInteraction });
        }

        public MODELS.InteractionModel DatosInteraccion(MODELS.Fixed.ParametersInteraccion oModel)
        {
            var oInteraccion = new MODELS.InteractionModel();
            var objInteraction = new MODELS.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Begin DatosInteraccion");
            try
            {
                // Get Datos de la Tipificacion
                //objInteraction = CargarTificacion(oModel.IdSession, codeTipification);
                //var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;

                oInteraccion.ObjidContacto = oModel.objIdContacto; // GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.CustomerId; //ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + ;
                oInteraccion.Type = oModel.Type;
                oInteraccion.Class = oModel.Class;
                oInteraccion.SubClass = oModel.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.SIACU.Transac.Service.Constants.strCeroUno;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.FlagCase = Claro.SIACU.Transac.Service.Constants.strCero;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
                oInteraccion.ObjidSite = oModel.objIdSite;
                oInteraccion.Cuenta = oModel.Cuenta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "End DatosInteraccion");
            return oInteraccion;
        }

        public JsonResult GenerarTipificacionPlus(string strIdSession, CommonTransacService.InsertTemplateInteraction template) { 
            string result = "";
            result = InsertarTipificacionPlus(strIdSession, template);
            return Json(new { codeResponse = "0", mensajeResponse = "", interaccionPlusId = result });
        }

        public string InsertarTipificacionPlus(string strIdSession, CommonTransacService.InsertTemplateInteraction template)
        {
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var txId = audit.transaction;
            var dateBegin = new DateTime(1, 1, 1);
            var interaccionPlus = new InteraccionPlus()
            {
                p_nro_interaccion = template._ID_INTERACCION,
                p_inter_1 = template._X_INTER_1,
                p_inter_2 = template._X_INTER_2,
                p_inter_3 = template._X_INTER_3,
                p_inter_4 = template._X_INTER_4,
                p_inter_5 = template._X_INTER_5,
                p_inter_6 = template._X_INTER_6,
                p_inter_7 = template._X_INTER_7,
                p_inter_8 = template._X_INTER_8.ToString(),
                p_inter_9 = template._X_INTER_9.ToString(),
                p_inter_10 = template._X_INTER_10.ToString(),
                p_inter_11 = template._X_INTER_11.ToString(),
                p_inter_12 = template._X_INTER_12.ToString(),
                p_inter_13 = template._X_INTER_13.ToString(),
                p_inter_14 = template._X_INTER_14.ToString(),
                p_inter_15 = template._X_INTER_15,
                p_inter_16 = template._X_INTER_16,
                p_inter_17 = template._X_INTER_17,
                p_inter_18 = template._X_INTER_18,
                p_inter_19 = template._X_INTER_19,
                p_inter_20 = template._X_INTER_20,
                p_inter_21 = template._X_INTER_21,
                p_inter_22 = template._X_INTER_22.ToString(),
                p_inter_23 = template._X_INTER_23.ToString(),
                p_inter_24 = template._X_INTER_24.ToString(),
                p_inter_25 = template._X_INTER_25.ToString(),
                p_inter_26 = template._X_INTER_26.ToString(),
                p_inter_27 = template._X_INTER_27.ToString(),
                p_inter_28 = template._X_INTER_28.ToString(),
                p_inter_29 = template._X_INTER_29,
                p_inter_30 = template._X_INTER_30,
                p_plus_inter2interact = template._X_PLUS_INTER2INTERACT.ToString(),
                p_adjustment_amount = template._X_ADJUSTMENT_AMOUNT.ToString(),
                p_adjustment_reason = template._X_ADJUSTMENT_REASON,
                p_address = template._X_ADDRESS,
                p_amount_unit = template._X_AMOUNT_UNIT,
                p_birthday = template._X_BIRTHDAY != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_BIRTHDAY)) : "",
                p_clarify_interaction = template._X_CLARIFY_INTERACTION,
                p_claro_ldn1 = template._X_CLARO_LDN1,
                p_claro_ldn2 = template._X_CLARO_LDN2,
                p_claro_ldn3 = template._X_CLARO_LDN3,
                p_claro_ldn4 = template._X_CLARO_LDN4,
                p_clarolocal1 = template._X_CLAROLOCAL1,
                p_clarolocal2 = template._X_CLAROLOCAL2,
                p_clarolocal3 = template._X_CLAROLOCAL3,
                p_clarolocal4 = template._X_CLAROLOCAL4,
                p_clarolocal5 = template._X_CLAROLOCAL5,
                p_clarolocal6 = template._X_CLAROLOCAL6,
                p_contact_phone = template._X_CONTACT_PHONE,
                p_dni_legal_rep = template._X_DNI_LEGAL_REP,
                p_document_number = template._X_DOCUMENT_NUMBER,
                p_email = template._X_EMAIL,
                p_first_name = template._X_FIRST_NAME,
                p_fixed_number = template._X_FIXED_NUMBER,
                p_flag_change_user = template._X_FLAG_CHANGE_USER,
                p_flag_legal_rep = template._X_FLAG_CHANGE_USER,
                p_flag_other = template._X_FLAG_OTHER,
                p_flag_titular = template._X_FLAG_TITULAR,
                p_imei = template._X_IMEI,
                p_last_name = template._X_LAST_NAME,
                p_lastname_rep = template._X_LASTNAME_REP,
                p_ldi_number = template._X_LDI_NUMBER,
                p_name_legal_rep = template._X_NAME_LEGAL_REP,
                p_old_claro_ldn1 = template._X_OLD_CLARO_LDN1,
                p_old_claro_ldn2 = template._X_OLD_CLARO_LDN2,
                p_old_claro_ldn3 = template._X_OLD_CLARO_LDN3,
                p_old_claro_ldn4 = template._X_OLD_CLARO_LDN4,
                p_old_clarolocal1 = template._X_OLD_CLAROLOCAL1,
                p_old_clarolocal2 = template._X_OLD_CLAROLOCAL2,
                p_old_clarolocal3 = template._X_OLD_CLAROLOCAL3,
                p_old_clarolocal4 = template._X_OLD_CLAROLOCAL4,
                p_old_clarolocal5 = template._X_OLD_CLAROLOCAL5,
                p_old_clarolocal6 = template._X_OLD_CLAROLOCAL6,
                p_old_doc_number = template._X_OLD_DOC_NUMBER,
                p_old_first_name = template._X_OLD_FIRST_NAME,
                p_old_fixed_phone = template._X_OLD_FIXED_PHONE,
                p_old_last_name = template._X_OLD_LAST_NAME,
                p_old_ldi_number = template._X_OLD_LDI_NUMBER,
                p_old_fixed_number = template._X_OLD_FIXED_NUMBER,
                p_operation_type = template._X_OPERATION_TYPE,
                p_other_doc_number = template._X_OTHER_DOC_NUMBER,
                p_other_first_name = template._X_OTHER_FIRST_NAME,
                p_other_last_name = template._X_OTHER_LAST_NAME,
                p_other_phone = template._X_OTHER_PHONE,
                p_phone_legal_rep = template._X_PHONE_LEGAL_REP,
                p_reference_phone = template._X_REFERENCE_PHONE,
                p_reason = template._X_REASON,
                p_model = template._X_MODEL,
                p_lot_code = template._X_LOT_CODE,
                p_flag_registered = template._X_FLAG_REGISTERED,
                p_registration_reason = template._X_REGISTRATION_REASON,
                p_claro_number = template._X_CLARO_NUMBER,
                p_month = template._X_MONTH,
                p_ost_number = template._X_OST_NUMBER,
                p_basket = template._X_BASKET,
                p_expire_date = template._X_EXPIRE_DATE != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_EXPIRE_DATE)) : "",
                p_ADDRESS5 = template._X_ADDRESS5,
                p_CHARGE_AMOUNT = template._X_CHARGE_AMOUNT.ToString(),
                p_CITY = template._X_CITY,
                p_CONTACT_SEX = template._X_CONTACT_SEX,
                p_DEPARTMENT = template._X_DEPARTMENT,
                p_DISTRICT = template._X_DISTRICT,
                p_EMAIL_CONFIRMATION = template._X_EMAIL_CONFIRMATION,
                p_FAX = template._X_FAX,
                p_FLAG_CHARGE = template._X_FLAG_CHARGE,
                p_MARITAL_STATUS = template._X_MARITAL_STATUS,
                p_OCCUPATION = template._X_OCCUPATION,
                p_POSITION = template._X_POSITION,
                p_REFERENCE_ADDRESS = template._X_REFERENCE_ADDRESS,
                p_TYPE_DOCUMENT = template._X_TYPE_DOCUMENT,
                p_ZIPCODE = template._X_ZIPCODE,
                p_iccid = template._X_ICCID,
            };
            var request = new RegisterNuevaInteraccionPlusRequest()
            {
                interaccionPlus = interaccionPlus,
                txId = txId,
                audit = audit,
            };
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin RegisterNuevaInteraccionPlus");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return _oServiceFixed.RegisterNuevaInteraccionPlus(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, request.audit.transaction, ex.Message);
                return "";
            }
            Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("End RegisterNuevaInteraccionPlus result: {0}", result));
            return result;
        }


        #region Constancia y OnBase

        public FileContentResult DownloadFileServer(string strIdSession, string strPath)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO: DownloadFileServer");
            byte[] objFile = null;
            Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
            oCommonHandler.DisplayFileFromServerSharedFile(strIdSession, strIdSession, strPath, out objFile);
            Claro.Web.Logging.Info(strIdSession, strIdSession, "FIN: DownloadFileServer ");
            return File(objFile, Tools.Utils.Functions.f_obtieneContentType(Path.GetExtension(strPath)));

        }

        public JsonResult GenerarConstanciaLeyPromo(OnBaseCargaModel objRequest)
        {
            CommonTransacService.ParametersGeneratePDF parameters = new CommonTransacService.ParametersGeneratePDF();

            parameters.StrCasoInter = objRequest.Constancia.CASO_INTER;
            parameters.StrCarpetaTransaccion = KEY.AppSettings("strLeyPromoCarpetaTransaccion");
            parameters.StrNombreArchivoTransaccion = KEY.AppSettings("strLeyPromoNombreFormatoHP");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strLeyPromoCarpetaPDFs");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");

            objRequest.Constancia.FORMATO_TRANSACCION = KEY.AppSettings("strLeyPromoNombreFormatoHP");
            objRequest.Constancia.TRANSACCION = KEY.AppSettings("strLeyPromoConstanciaTransaccion");
            objRequest.Constancia.FECHA_SOLICITUD = DateTime.Now.ToString("dd/MM/yyyy");

            Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
            CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyWithXml(objRequest.IdSession, App_Code.Common.ObjectToSerializeXML<ConstanciaLeyPromoModel>(objRequest.Constancia), parameters);

            byte[] objFile = null;

            oCommonHandler.DisplayFileFromServerSharedFile(objRequest.IdSession, objRequest.IdSession, response.FullPathPDF, out objFile);

            if (objFile != null)
            {

                objRequest.Path = response.Document;
                objRequest.TipoOperacion = KEY.AppSettings("strLeyPromoTypeOnBase");
                objRequest.FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy");

                return Json(new { codeResponse = "0", OnBase = CargarOnBase(objRequest, objFile), Constancia = response });
            }
            else
                throw new Exception("No se genero la constancia");

        }

        public OnBaseCargaResponse CargarOnBase(OnBaseCargaModel objResquest, byte[] file)
        {
            try
            {
                string[] strMetadatosName = KEY.AppSettings("strLeyPromoMetadatosName").Split(',');
                string[] strMetadatosValue = KEY.AppSettings("strLeyPromoMetadatosValue").Split(',');
                string[] strMetadatosLength = KEY.AppSettings("strLeyPromoMetadatosLength").Split(',');
                string valor;
                List<FixedTransacService.metadatosOnBase> metaDatos = new List<FixedTransacService.metadatosOnBase>();

                objResquest.FormatoTransaccion = KEY.AppSettings("strLeyPromoNombreFormatoHP");

                for (int i = 0; i < strMetadatosValue.Length; i++)
                {
                    valor = (string)objResquest.GetType().GetProperty(strMetadatosValue[i]).GetValue(objResquest);

                    if (valor != null) if (!valor.Trim().Equals("")) if (strMetadatosLength[i] != "0")
                                valor = valor.Substring(0, int.Parse(strMetadatosLength[i]));

                    metaDatos.Add(new FixedTransacService.metadatosOnBase()
                    {
                        attributeName = strMetadatosName[i],
                        attributeValue = valor
                    });
                }

                FixedTransacService.OnBaseCargaResponse objResponse = _oServiceFixed.TargetDocumentoOnBase(new OnBaseCargaRequest()
                {

                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(objResquest.IdSession),
                    HeaderDPService = App_Code.Common.GetHeaderDataPowerRequest<FixedTransacService.HeaderDPRequest>(KEY.AppSettings("moduloLeyPromo"), "CargaOnBase"),
                    user = objResquest.CodigoAsesor,
                    metadatosOnBase = metaDatos,
                    SpecificationAttachmentOnBase = new FixedTransacService.SpecificationAttachmentOnBase()
                    {
                        name = objResquest.Path + ".pdf",
                        type = KEY.AppSettings("strLeyPromoTypeOnBase"),
                        listEntitySpectAttach = new FixedTransacService.entitySpecAttachExtensionOnBase()
                        {
                            ID = objResquest.IdSession,
                            fileBase64 = System.Convert.ToBase64String(file)
                        }
                    }

                });
                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objResquest.IdSession, objResquest.IdSession, "ERROR: CargarOnBase" + ex.Message);
                return new FixedTransacService.OnBaseCargaResponse() { codeOnBase = "", codeResponse = "1", descriptionResponse = ex.Message };
            }
        }


        #endregion OnBase

        public JsonResult GetSendEmailSBLeyPromociones(MODELS.Fixed.ParametersEnvioEmail oModel)
        {

            string strRutaArchivo = string.Empty;             

            SendEmailSBModel objSendEmailSBModel = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.srtIdSession);
            
            byte[] objFile = null;
            Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
            oCommonHandler.DisplayFileFromServerSharedFile(oModel.srtIdSession, oModel.srtIdSession, oModel.strFullPathPDF, out objFile);

            strRutaArchivo = oModel.strFullPathPDF;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
            
            var objRequest = new SendEmailSBRequest
            {
                audit = audit,
                SessionId = oModel.srtIdSession,
                TransactionId = audit.transaction,
                strAppID = audit.ipAddress,
                strAppCode = audit.applicationName,
                strAppUser = audit.userName,
                strRemitente = oModel.strRemitente,
                strDestinatario = oModel.strDestinatario,
                strAsunto = oModel.strAsunto,
                strMensaje = oModel.strMensaje,
                strHTMLFlag = oModel.strHTMLFlag,
                Archivo = objFile,
                strNomFile = strAdjunto,
            };

            try
            {
                var objResponse = Claro.Web.Logging.ExecuteMethod(() => _oServiceCommon.GetSendEmailSB(objRequest));

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
                Claro.Web.Logging.Error(oModel.srtIdSession, objRequest.audit.transaction, ex.Message);
                return Json(new { codigoRespuesta = '1', mensajeRespuesta = ex.Message });
            }

            return Json(new { codigoRespuesta = objSendEmailSBModel.codigoRespuesta, mensajeRespuesta = objSendEmailSBModel.mensajeRespuesta });
        }

        public JsonResult InsertAuditory(string strIdSession, string strTelephone, string strUsuarioNombre , string strUsuarioLogin)
        {
            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Ley Promo InsertAuditory strTelephone: {0}", strTelephone));

                var strTransaction = ConfigurationManager.AppSettings("gConstEvtLeyPromoOptGrabar");
                var strDesTranssation = ConfigurationManager.AppSettings("gConstEvtLeyPromoOptGrabarMsj");
                var strService = ConfigurationManager.AppSettings("gConstEvtLeyPromoOptGrabarServicio");
                var strDate = DateTime.Now;

                string strIpCliente = Request.UserHostName;
                string strIpServidor = Request.ServerVariables["LOCAL_ADDR"];
                string strNameServidor = Request.ServerVariables["SERVER_NAME"];

                string sbText = string.Format("Ip Cliente: {0},/Usuario: {1}, /Opcion: {2}, /Fecha y Hora: {3}", strIpCliente, strUsuarioLogin, strDesTranssation, strDate);


                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                oCommonHandler.SaveAuditM(strTransaction, strService, sbText, strTelephone, strUsuarioNombre, strIdSession, strNameServidor, strIpServidor);

                return Json(new { codigoRespuesta = "0", mensajeRespuesta = "" });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Ley Promo InsertAuditory ex: {0}", ex.ToString()));
                return Json(new { codigoRespuesta = "1", mensajeRespuesta = ex.ToString() });
            }

        }

        public Boolean UploadSFTP(string strIdSession, ConnectionSFTP objConnectionSFTP, string filePath, string fileName)
        {
            FIXED.AuditRequest1 audit =
            App_Code.Common.CreateAuditRequest<FIXED.AuditRequest1>(strIdSession);

            Boolean UploadResult = false;
            ConnectionSFTP objRequest = new ConnectionSFTP()
            {
                KeyUser = objConnectionSFTP.KeyUser,
                KeyPassword = objConnectionSFTP.KeyPassword,
                path_Destination = objConnectionSFTP.path_Destination,
                port = objConnectionSFTP.port,
                server = objConnectionSFTP.server,
                registryKey = objConnectionSFTP.registryKey
            };

            try
            {
                byte[] objFile = System.IO.File.ReadAllBytes(string.Concat(filePath, "\\", fileName));
                UploadResult = _oServiceFixed.UploadSftp(audit, objRequest, fileName, objFile);

            }
            catch (Exception ex)
            {
                UploadResult = false;
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return UploadResult;
        }
    }

}



