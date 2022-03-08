using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using FixedContract = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using TEMPLATE = Claro.SIACU.Transac.Service.TemplateExcel;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using System.Text;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.Helpers.Transac.Service;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;



namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class HfcIncomingCallDetailController : HfcIncomingCallDetailBaseController
    {
        public static string rutaConstancy = string.Empty;


        public ActionResult HFCIncomingCallDetail()
        {
            return PartialView("~/Areas/Transactions/Views/IncomingCallDetail/HfcIncomingCallDetail.cshtml");
        }

        public JsonResult GetLoad(IncomingCall objIncomingCallRequest)
        {            
            string stxtFecActivacion = string.Empty;
            string idMonto = string.Empty;
            string strTempTypePhone = String.Empty;
            strTempTypePhone = objIncomingCallRequest.typeProduct;
            string statusMessage = string.Empty;
            string idPerfilExportar = string.Empty;
            string idPerfilImprimir = string.Empty;            


            string[] tipoLineaActual = KEY.AppSettings("gConstTipoLineaActualFijo").Split('|');
            string optionI = strTempTypePhone.Equals(tipoLineaActual[0]) ? ConfigurationManager.AppSettings("strLlamadasEntImprimirHFC").Trim() : ConfigurationManager.AppSettings("strLlamadasEntImprimirLTE").Trim();
            string optionE = strTempTypePhone.Equals(tipoLineaActual[0]) ? ConfigurationManager.AppSettings("strLlamadasEntExportarHFC").Trim() : ConfigurationManager.AppSettings("strLlamadasEntExportarLTE").Trim();

            var audit = App_Code.Common.CreateAuditRequest<FixedContract.AuditRequest>(objIncomingCallRequest.strIdSession);


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "ENTRANDO AL LOAD");

            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.ConsultationServiceByContractResponse oConsultationServiceByContractResponse = null;


            try
            {
                if (!(strTempTypePhone.Equals(tipoLineaActual[0]) || strTempTypePhone.Equals(tipoLineaActual[1])))
                {
                    statusMessage = KEY.AppSettings("strMsgTransNoHabilPlanDetLLamEnt");
                }



                if (objIncomingCallRequest.flagPlataforma == CONSTANTS.strLetraC)
                {
                    stxtFecActivacion = objIncomingCallRequest.fechaActivacionPrepago;
                }
                else if (objIncomingCallRequest.flagPlataforma == CONSTANTS.strLetraP)
                {
                    stxtFecActivacion = objIncomingCallRequest.fecActivacion;
                }

                AmountIncomingCallResponse oAmountIncomingCallResponse = null;
                AmountIncomingCallRequest objRequest = new AmountIncomingCallRequest
                {
                    Name = strTempTypePhone.Equals(tipoLineaActual[0]) ? KEY.AppSettings("MontoDetLlamEntHFC") : KEY.AppSettings("MontoDetLlamEntLTE"),
                    audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.AuditRequest>(objIncomingCallRequest.strIdSession)
                };

                oAmountIncomingCallResponse = 
                    Claro.Web.Logging.ExecuteMethod<AmountIncomingCallResponse>(
                    () => { return oServicePostPaid.GetAmountIncomingCall(objRequest); });



                idMonto = oAmountIncomingCallResponse.ListAmountIncomingCall.LastOrDefault().ValorN.ToString("0.00");



                if (objIncomingCallRequest.pageAccess.IndexOf(optionE) >= 0)
                {
                    idPerfilExportar = CONSTANTS.strUno;
                }
                else
                {
                    idPerfilExportar = CONSTANTS.strCero;
                }


                if (objIncomingCallRequest.pageAccess.IndexOf(optionI) >= 0)
                {
                    idPerfilImprimir = CONSTANTS.strUno;
                }
                else
                {
                    idPerfilImprimir = CONSTANTS.strCero;
                }
                

                Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest>(objIncomingCallRequest.strIdSession);


                var ConsultationServiceByContractRequest = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.ConsultationServiceByContractRequest()
                {
                    audit = objAuditRequest,
                    strCodContrato = objIncomingCallRequest.contratoId,
                    typeProduct = strTempTypePhone
                };


                oConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oFixedService.GetCustomerLineNumber(ConsultationServiceByContractRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error al Iniciar : " + ex.Message);
                throw;
            }
                        
            
            var objResponse = new
            {
                idMonto = idMonto,
                FecActivacion = stxtFecActivacion,
                idPerfilExportar = idPerfilExportar,
                idPerfilImprimir = idPerfilImprimir,
                FechaFin = DateTime.Now.ToShortDateString(),
                FechaInicio = DateTime.Now.AddMonths(-1).ToShortDateString(),
                statusMessage = statusMessage,
                strTipificacionConsulta = strTempTypePhone.Equals(tipoLineaActual[0]) ? ConfigurationManager.AppSettings("strTipificacionConsultaHFC") : ConfigurationManager.AppSettings("strTipificacionConsultaLTE"),
                strTipificacionGuardar = strTempTypePhone.Equals(tipoLineaActual[0]) ? ConfigurationManager.AppSettings("strTipificacionGuardarHFC") : ConfigurationManager.AppSettings("strTipificacionGuardarLTE"),
                msisdn = oConsultationServiceByContractResponse.msisdn,
                msgIgvError = ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"),
                startDateConfig = DateTime.Now.AddMonths(-1 * 3).ToShortDateString()
            };


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Response : " + objResponse.ToString());

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadTypificationFixed(IncomingCall objIncomingCallRequest)
        {
            Claro.Web.Logging.Configure();
            
            string strTempTypePhone = objIncomingCallRequest.typeProduct;

            CommonService.Typification oTypification = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objIncomingCallRequest.strIdSession);

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Iniciando el Metodo LoadTypification");


            try
            {
                CommonService.TypificationRequest objTypificationRequest = new CommonTransacService.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = objIncomingCallRequest.transactionName;
                objTypificationRequest.audit = audit;

                CommonService.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<CommonService.TypificationResponse>(
                    () => { return oCommonService.GetTypification(objTypificationRequest); });



                oTypification = objResponse.ListTypification.SingleOrDefault(x => x.TIPO == strTempTypePhone);
            }
            catch (Exception ex)
            {                
                Claro.Web.Logging.Error(objIncomingCallRequest.strIdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return Json(new { oTypification = oTypification, success = oTypification != null ? true : false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcessTransactionsIterations(IncomingCall objIncomingCallRequest)
        {
            
            Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, "1", "INICIANDO ProcessTransactionsIterations");

            string message = string.Empty;
            string messageOCC = string.Empty;
            object response = null;
            string strInteraccionId = string.Empty;
            string ptexto = string.Empty;
            string porden = CONSTANTS.strLetraF;
            string paramIN = string.Format("TelfConsulta={0}; CoId={1}; FechaINI={2};FechaFin={3}", objIncomingCallRequest.phone, objIncomingCallRequest.contratoId, objIncomingCallRequest.fecStart, objIncomingCallRequest.fecEnd);
            string paramOUT = string.Empty;
            CommonServicesController convert2010 = new CommonServicesController();


            FixedContract.InsertInteractionBusinessRequest oInsertInteractionBusinessRequest = new FixedContract.InsertInteractionBusinessRequest();
            oInsertInteractionBusinessRequest.Interaction = GetDataInteraction(objIncomingCallRequest);
            oInsertInteractionBusinessRequest.InteractionTemplate = GetDataIteracionTemplate(objIncomingCallRequest);
            oInsertInteractionBusinessRequest.audit = App_Code.Common.CreateAuditRequest<FixedContract.AuditRequest>(objIncomingCallRequest.strIdSession);
            oInsertInteractionBusinessRequest.Phone = convert2010.GetNumber(objIncomingCallRequest.strIdSession, false, objIncomingCallRequest.phone);
            oInsertInteractionBusinessRequest.UserSystem = KEY.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            oInsertInteractionBusinessRequest.UserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            oInsertInteractionBusinessRequest.UserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            oInsertInteractionBusinessRequest.ExecuteTransactation = true;

            if (ValidateProcessTransactionsIterations(oInsertInteractionBusinessRequest, ref message, ref strInteraccionId))
            {
                response = new { message = message, estateButton = "Guardar", strInteraccionId = strInteraccionId, idAuditoria = message };
            }
            else
            {
                try
                {
                    UpdateInteraction(strInteraccionId, ptexto, porden, objIncomingCallRequest.strIdSession);

                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, "ERROR LoadTypification");
                    message = string.Format("Error al actualizar la Interaccion : {0}", ex.Message);
                    throw ex;
                }

                response = new
                {
                    message = message,
                    estateButton = string.Empty,
                    strInteraccionId = strInteraccionId,
                    idAuditoria = message
                };
            }

            
            //Validar Si se genero interaccion
            if (!string.IsNullOrEmpty(strInteraccionId))
            {
                //GENERAR CONSTANCIA
                objIncomingCallRequest.constancy = new Constancy
                {
                    puntoAtencion = objIncomingCallRequest.cboCACDAC,
                    titularCliente = string.Format("{0} {1}", objIncomingCallRequest.firstName, objIncomingCallRequest.lastName),
                    representanteLegal = objIncomingCallRequest.LegalAgent,
                    tipoDocumento = objIncomingCallRequest.tipoDocumento,
                    nroDocumento = objIncomingCallRequest.documentNumber,
                    fecTransProgram = DateTime.Now.ToShortDateString(),
                    casoInteracion = strInteraccionId,
                    fechaInicio = objIncomingCallRequest.fecStart,
                    fechaFin = objIncomingCallRequest.fecEnd,
                    nroServicio = objIncomingCallRequest.phone,
                    montoOcc = objIncomingCallRequest.chkGeneraOCC ? objIncomingCallRequest.idMontoConIGV.ToString("0.00") : CONSTANTS.PresentationLayer.NumeracionCERODECIMAL2,//CAMBIO
                    enviarCorreo = objIncomingCallRequest.chkSendMail ? CONSTANTS.Variable_SI : CONSTANTS.Variable_NO,
                    correo = objIncomingCallRequest.chkSendMail ? objIncomingCallRequest.tipificacion.destinatario : string.Empty,
                    notas = string.Empty,

                };


                ParametersGeneratePDF objConstancy = new ParametersGeneratePDF
                {
                    StrCentroAtencionArea = objIncomingCallRequest.constancy.puntoAtencion,
                    StrTitularCliente = objIncomingCallRequest.constancy.titularCliente,
                    StrRepresLegal = objIncomingCallRequest.constancy.representanteLegal,
                    StrTipoDocIdentidad = objIncomingCallRequest.constancy.tipoDocumento,
                    StrNroDocIdentidad = objIncomingCallRequest.constancy.nroDocumento,
                    StrFechaTransaccionProgram = objIncomingCallRequest.constancy.fecTransProgram,
                    StrCasoInter = objIncomingCallRequest.constancy.casoInteracion,
                    StrFecInicialReporte = objIncomingCallRequest.constancy.fechaInicio,
                    StrFecFinalReporte = objIncomingCallRequest.constancy.fechaFin,
                    StrNroServicio = objIncomingCallRequest.constancy.nroServicio,
                    StrMontoOCC = string.Format("{0} {1}", "S/.", objIncomingCallRequest.constancy.montoOcc),
                    strEnvioCorreo = objIncomingCallRequest.constancy.enviarCorreo,
                    StrEmail = objIncomingCallRequest.constancy.correo,
                    StrNotas = objIncomingCallRequest.constancy.notas
                };

                Claro.Web.Logging.Info("123123123", "1", "Antes de ejecutar metodo GenerateConstancyFixed - Controller");

                GenerateConstancyResponseCommon oGeneratePDFDataResponseHfc = Claro.Web.Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
                {
                    return GenerateConstancyFixed(objConstancy, objIncomingCallRequest.type, objIncomingCallRequest.strIdSession);
                });
                if (oGeneratePDFDataResponseHfc.Generated)
                    rutaConstancy = oGeneratePDFDataResponseHfc.FullPathPDF;

                Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, "RUTA HPXTREAM : " + oGeneratePDFDataResponseHfc.FullPathPDF);
                //FIN CONSTANCIA


                //INSERTAR EVIDENCIA
                InsertEvidence(objIncomingCallRequest.strIdSession, objIncomingCallRequest.contratoId, objIncomingCallRequest.customerId, objIncomingCallRequest.subClaseDes, objIncomingCallRequest.SubClaseDesCode, strInteraccionId, KEY.AppSettings("strDetalleLlamadasEntrantesTransac"), rutaConstancy, oGeneratePDFDataResponseHfc.Document, objIncomingCallRequest.currentUser);



                //ENVIAR CORREO
                if (objIncomingCallRequest.chkSendMail)
                {
                    byte[] attachFile = null;
                    //Nombre del archivo
                    string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                    if(DisplayFileFromServerSharedFile(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, rutaConstancy, out attachFile))
                        GetSendEmail(objIncomingCallRequest.tipificacion, objIncomingCallRequest.strIdSession, strAdjunto , attachFile);
                }

                Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, "RESPONSE PROCESSTRANSACTIONS : " + response.ToString());



                //GENERAR OCC                
                string messageValidateOcc = string.Empty;
                string idAuditoria = string.Empty;
                string idCobro = string.Empty;
                bool idCobroOCC = false;
                if (objIncomingCallRequest.chkGeneraOCC)
                {
                    if (GenerateOCC(objIncomingCallRequest.phone, strInteraccionId, objIncomingCallRequest.strIdSession, objIncomingCallRequest.idMontoSinIGV, objIncomingCallRequest.customerId, ref messageValidateOcc, ref idAuditoria, ref idCobro, ref idCobroOCC) == 0)
                    {
                        messageOCC = "Se realizó el cobro por OCC";
                    }
                }


            }

            
            //Para auditoria
            paramOUT = message;

            InsertAudit(objIncomingCallRequest, objIncomingCallRequest.tipificacion.titular, "gConstEvtLlamadasEntrantesGuardar", "Accion: Guardar Desde: " + objIncomingCallRequest.fecStart + " Hasta: " + objIncomingCallRequest.fecEnd +
                " Respuesta Interaccion: " + message, convert2010.GetNumber(objIncomingCallRequest.strIdSession, false, objIncomingCallRequest.phone));


            Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, "PASO AUDITORIA");

            RegisterLogTrx("Grabar", objIncomingCallRequest.phone, CONSTANTS.strCero, CONSTANTS.strCero, objIncomingCallRequest.codOpcion, "gConstEvtLlamadasEntrantesGuardar", paramIN, paramOUT, App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objIncomingCallRequest.strIdSession));

            Claro.Web.Logging.Info(objIncomingCallRequest.strIdSession, oInsertInteractionBusinessRequest.audit.transaction, "PASO RegisterLogTrx");

            return Json(new { data = response , messageOCC = messageOCC }, JsonRequestBehavior.AllowGet);
        }

        public Boolean ValidateProcessTransactionsIterations(FixedContract.InsertInteractionBusinessRequest oInsertInteractionBusinessRequest, ref string message, ref string strInteraccionId)
        {
            try
            {
                Claro.Web.Logging.Configure();
                Claro.Web.Logging.Info(oInsertInteractionBusinessRequest.audit.Session, oInsertInteractionBusinessRequest.audit.transaction, "INICIANDO ValidateProcessTransactionsIterations");


                FixedContract.InsertInteractionBusinessResponse oInsertInteractionBusinessResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedContract.InsertInteractionBusinessResponse>(
                    () => { return oFixedService.GetInsertInteractionBusiness(oInsertInteractionBusinessRequest); });

                strInteraccionId = oInsertInteractionBusinessResponse.InteractionId;
                int n;

                if (int.TryParse(oInsertInteractionBusinessResponse.InteractionId, out n))
                {
                    oInsertInteractionBusinessResponse.CodReturnTransaction = CONSTANTS.strCero;
                }

                if (oInsertInteractionBusinessResponse.FlagInsercion != CONSTANTS.PresentationLayer.gstrVariableOK && oInsertInteractionBusinessResponse.FlagInsercion != string.Empty)
                {
                    message = string.Format("Al crear interacción en clarify: {0}", oInsertInteractionBusinessResponse.MsgText);
                    return false;
                }
                else if (oInsertInteractionBusinessResponse.FlagInsercionInteraction != CONSTANTS.PresentationLayer.gstrVariableOK && oInsertInteractionBusinessResponse.FlagInsercion != string.Empty)
                {
                    message = string.Format("No se pudo ejecutar la transacción, se debre proceder en forma manual. Por el siguiente error : {0} El nro de interacción es :{1}", oInsertInteractionBusinessResponse.MsgTextInteraction, oInsertInteractionBusinessResponse.InteractionId);
                    return false;
                }
                else if (oInsertInteractionBusinessResponse.FlagInsercionInteraction == CONSTANTS.PresentationLayer.gstrVariableOK && oInsertInteractionBusinessResponse.FlagInsercion == CONSTANTS.PresentationLayer.gstrVariableOK)
                {
                    message = ConfigurationManager.AppSettings("strMsgTranGrabSatis");
                }

                Claro.Web.Logging.Info(oInsertInteractionBusinessRequest.audit.Session, oInsertInteractionBusinessRequest.audit.transaction, "INICIANDO ValidateProcessTransactionsIterations result : " + message);

                return true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oInsertInteractionBusinessRequest.audit.Session, oInsertInteractionBusinessRequest.audit.transaction, "ERROR ValidateProcessTransactionsIterations");
                Claro.Web.Logging.Error(oInsertInteractionBusinessRequest.audit.Session, oInsertInteractionBusinessRequest.audit.transaction, ex.Message);
                message = string.Format("Error al registrar la Interaccion : {0}", ex.Message);
                throw ex;
            }
        }

        bool UpdateInteraction(string strInteraccionId,
                               string ptexto,
                               string porden,
                               string strIdSession)
        {

            CommonService.UpdateNotesResponseCommon objResponse = null;
            var audit= App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest>(strIdSession);
            try
            {
                Claro.Web.Logging.Configure();
                Claro.Web.Logging.Info(audit.Session, audit.Session, "INICIANDO UpdateInteraction");                

                CommonService.UpdateNotesRequestCommon objRequest = new CommonService.UpdateNotesRequestCommon()
                {
                    StrObjId = strInteraccionId,
                    StrText = ptexto,
                    StrOrder = porden,
                    audit = audit
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.UpdateNotesResponseCommon>(() =>
                {
                    return oCommonService.UpdateNotes(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "ERROR UpdateInteraction");
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw ex;
            }
            
            return objResponse.Flag.Equals("OK");
        }

        public FixedContract.Iteraction GetDataInteraction(IncomingCall objIncomingCallRequest)
        {
            FixedContract.Iteraction oInteraction = new FixedContract.Iteraction();

            CommonServicesController convert2010 = new CommonServicesController();

            
            if (objIncomingCallRequest.flagTfi == Constants.Yes)
            {
                objIncomingCallRequest.type = string.Format("{0}{1}", KEY.AppSettings("gConstProductoTFIPOSTPAGO"), objIncomingCallRequest.type);
            }
            oInteraction.OBJID_CONTACTO = string.Empty;
            oInteraction.FECHA_CREACION = DateTime.Now.ToShortDateString();
            oInteraction.TELEFONO = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + objIncomingCallRequest.customerId;
            oInteraction.TIPO = objIncomingCallRequest.type;
            oInteraction.CLASE = objIncomingCallRequest.claseDes;
            oInteraction.SUBCLASE = objIncomingCallRequest.subClaseDes;
            oInteraction.AGENTE = objIncomingCallRequest.currentUser; 
            oInteraction.TIPO_INTER = KEY.AppSettings("AtencionDefault"); 
            oInteraction.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
            oInteraction.RESULTADO = KEY.AppSettings("Ninguno");
            oInteraction.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
            oInteraction.FLAG_CASO = Constants.ZeroNumber;

            if ((objIncomingCallRequest.note == null ? 0 : objIncomingCallRequest.note.Length) > 0)
            {
                oInteraction.NOTAS = string.Format("{0} . Detalle Llamadas Entrantes - Desde: {1} Hasta: {2}", objIncomingCallRequest.note, objIncomingCallRequest.fecStart, objIncomingCallRequest.fecEnd);
            }
            else
            {
                oInteraction.NOTAS = string.Format("Detalle Llamadas Entrantes - Desde: {0} Hasta: {1}", objIncomingCallRequest.fecStart, objIncomingCallRequest.fecEnd);
            }

            return oInteraction;
        }

        public FixedContract.InsertTemplateInteraction GetDataIteracionTemplate(IncomingCall objIncomingCallRequest)
        {
            
            CommonServicesController convert2010 = new CommonServicesController();

            FixedContract.InsertTemplateInteraction oInteractionTemplate = new FixedContract.InsertTemplateInteraction();
            oInteractionTemplate._NOMBRE_TRANSACCION = KEY.AppSettings("strDetalleLlamadasEntrantesTransac");//objIncomingCallRequest.transactionName;
            oInteractionTemplate._X_CLARO_NUMBER = convert2010.GetNumber(objIncomingCallRequest.strIdSession, false, objIncomingCallRequest.phone);//objIncomingCallRequest.phone;
            oInteractionTemplate._P_USUARIO_ID = objIncomingCallRequest.currentUser;
            oInteractionTemplate._X_INTER_15 = objIncomingCallRequest.cboCACDAC;
            oInteractionTemplate._X_FIRST_NAME = objIncomingCallRequest.firstName;
            oInteractionTemplate._X_LAST_NAME = objIncomingCallRequest.lastName;
            oInteractionTemplate._X_DOCUMENT_NUMBER = objIncomingCallRequest.documentNumber;
            oInteractionTemplate._X_REFERENCE_PHONE = objIncomingCallRequest.referencePhone;
            oInteractionTemplate._X_INTER_20 = objIncomingCallRequest.fecStart;
            oInteractionTemplate._X_INTER_21 = objIncomingCallRequest.fecEnd;

            if ((objIncomingCallRequest.note == null ? 0 : objIncomingCallRequest.note.Length) > 0)
            {
                oInteractionTemplate._X_INTER_30 = string.Format("{0}.|Detalle Llamadas Entrantes - Desde: {1} Hasta: {2}", objIncomingCallRequest.note, objIncomingCallRequest.fecStart, objIncomingCallRequest.fecEnd);
            }
            else
            {
                oInteractionTemplate._X_INTER_30 = string.Format("Detalle Llamadas Entrantes - Desde: {0} Hasta: {1}", objIncomingCallRequest.fecStart, objIncomingCallRequest.fecEnd);
            }

            if (objIncomingCallRequest.chkGeneraOCC)
            {
                oInteractionTemplate._X_FLAG_REGISTERED = CONSTANTS.strCero;
                oInteractionTemplate._X_ADJUSTMENT_AMOUNT = Convert.ToDouble(objIncomingCallRequest.idMonto);
            }
            else
            {
                oInteractionTemplate._X_FLAG_REGISTERED = CONSTANTS.strUno;
                oInteractionTemplate._X_ADJUSTMENT_AMOUNT = 0.00;
            }

            oInteractionTemplate._X_POSITION = objIncomingCallRequest.currentUser;
                       
            oInteractionTemplate._X_EMAIL = objIncomingCallRequest.chkSendMail ? objIncomingCallRequest.tipificacion.destinatario : string.Empty;
            oInteractionTemplate._X_INTER_5 = objIncomingCallRequest.chkSendMail ? CONSTANTS.strUno : Constants.ZeroNumber;
            return oInteractionTemplate;
        }

        public JsonResult GetIncomingCallExportExcel(string idsession, string phone, string fecStart, string fecEnd, string fullName, string cacdac, string typeProduct, List<FixedContract.CallDetailSummary> LstPhoneCall)
        {
            Claro.Web.Logging.Configure();
            Claro.Web.Logging.Info(idsession, "666", "ENTRANDO AL GetIncomingCallExportExcel");

            ExcelHelper oExcelHelper = new ExcelHelper();
            ExportExcelModel objExportExcel = new ExportExcelModel();
            List<ExportExcel> list = null;
            try
            {
                list = IncomingCallExportExcel(LstPhoneCall);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            Claro.Web.Logging.Info(idsession, "666", "ENTRANDO AL GetIncomingCallExportExcel LIST :" + list.Count.ToString());

            objExportExcel.ListExportExcel = list;
            objExportExcel.Phone = phone;
            objExportExcel.dateStart = fecStart;
            objExportExcel.dateEnd = fecEnd;
            objExportExcel.date = string.Format("{0} - {1}", objExportExcel.dateStart, objExportExcel.dateEnd);
            objExportExcel.cacdac = cacdac;
            objExportExcel.fullName = fullName;
            List<int> lstHelperPlan = ValidateExportExcel(objExportExcel.ListExportExcel);

            string[] tipoLineaActual = KEY.AppSettings("gConstTipoLineaActualFijo").Split('|');

            string path = oExcelHelper.ExportExcel(objExportExcel, typeProduct.Equals(tipoLineaActual[0]) ? TEMPLATE.CONST_EXPORT_HFCINCOMINGCALL : TEMPLATE.CONST_EXPORT_LTEINCOMINGCALL, lstHelperPlan);

            Claro.Web.Logging.Info(idsession, "666", "ENTRANDO AL GetIncomingCallExportExcel PATH :" + path);

            return Json(path);
        }

        private List<ExportExcel> IncomingCallExportExcel(List<FixedContract.CallDetailSummary> LstPhoneCall)
        {
            List<ExportExcel> list = new List<ExportExcel>();
            ExportExcel entity = null;

            foreach (var item in LstPhoneCall)
            {
                entity = new ExportExcel()
                {
                    Id = item.NroOrd,
                    PhoneNumber = item.MSISDN,
                    IncomingPhoneNumber = item.CallNumber,
                    DateIncomingCall = item.CallDate,
                    HourIncomingCall = item.CallTime,
                    Duration = item.CallDuration
                };
                list.Add(entity);
            }

            return list;
        }

        public int GenerateOCC(string idTelefono,
                               string idCasoId,
                               string idSession,
                               string idMonto,
                               string customerId,
                               ref string message,
                               ref string idAuditoria,
                               ref string idCobro,
                               ref bool idCobroOCC)
        {

            Claro.Web.Logging.Configure();
            Int64 scustomerId = Convert.ToInt64(customerId);
            string sCodOCC = KEY.AppSettings("sLlamadasEntrantes");
            string sPeriodo = KEY.AppSettings("strPeriodoOCC");
            string sComentario = string.Format("{0}C{1}", idTelefono, idCasoId);

            FixedContract.GenerateOCCRequest objRequest = new FixedContract.GenerateOCCRequest();
            FixedContract.GenerateOCCResponse objResponse = null;
            var audit = App_Code.Common.CreateAuditRequest<FixedContract.AuditRequest>(idSession);


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "INICIANDO GenerateOCC");      

            try
            {
                objRequest.customerId = scustomerId;
                objRequest.recDate = DateTime.Now;
                objRequest.remark = sComentario;
                objRequest.codigoOcc = Convert.ToDecimal(sCodOCC);
                objRequest.montoOcc = float.Parse(idMonto);
                objRequest.txId = sPeriodo;
                objRequest.codigoOccSpecified = true;
                objRequest.customerIdSpecified = true;
                objRequest.montoOccSpecified = true;
                objRequest.nroCuotasSpecified = true;
                objRequest.recDateSpecified = true;
                objRequest.audit = audit;


                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedContract.GenerateOCCResponse>(
                    () => { return oFixedService.GenerateOCC(objRequest); });


                if (objResponse.errorCode != "0")
                {
                    message = string.Format("Error en la transacción : No se pudo realizar el Ajuste al {0}", "Guardar");
                    idAuditoria = message;
                }
                else
                {
                    idCobro = string.Format("El cobro se realizó al : {0} .", "Guardar");
                    idAuditoria = string.Format("{0} - El cobro se realizó al : {1}.", objResponse.errorMsg, "Guardar");
                    idCobroOCC = false;
                }
            }
            catch (Exception ex)
            {
                message = string.Format("Error al generar el Cobro al {0} : {1}", "Guardar", ex.Message);
                idAuditoria = string.Format("{0} Error al generar el Cobro al {1}.", objResponse.errorMsg, ex.Message);
                Claro.Web.Logging.Info(idSession, objRequest.audit.transaction, "ERROR GenerateOCC");
                Claro.Web.Logging.Error(idSession, objRequest.audit.transaction, ex.Message);  

                throw ex;
            }

            return Convert.ToInt(objResponse.errorCode);


        }

        public JsonResult GetSearch(IncomingCall objIncomingCallRequest)
        {

            Claro.Web.Logging.Configure();

            Claro.Web.Logging.Info("123456789", "123456", "ENTRANDO AL SEARCH");
            FixedContract.CallDetailInputFixedResponse oCallDetailResponse = null;
            var audit = App_Code.Common.CreateAuditRequest<FixedContract.AuditRequest>(objIncomingCallRequest.strIdSession);          

            try
            {
                var headerRequest = new FixedContract.HeaderRequestTypeBpel
                {
                    Canal = "WEB",
                    IdAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                    UsuarioAplicacion = objIncomingCallRequest.currentUser,
                    UsuarioSesion = objIncomingCallRequest.currentUser,
                    IdTransaccionEsb = string.Empty,
                    IdTransaccionNegocio = audit.transaction,
                    NodoAdicional = CONSTANTS.strUno
                };

                var contactUser = new FixedContract.ContactUserBpel
                {
                    Usuario = objIncomingCallRequest.currentUser,
                    Nombres = objIncomingCallRequest.firstName,
                    Apellidos = objIncomingCallRequest.lastName,
                    RazonSocial = objIncomingCallRequest.razonSocial,
                    TipoDoc = objIncomingCallRequest.tipoDocumento,
                    NumDoc = objIncomingCallRequest.documentNumber,
                    Domicilio = objIncomingCallRequest.domicilio,
                    Distrito = objIncomingCallRequest.provincia,
                    Departamento = objIncomingCallRequest.departamento,
                    Provincia = objIncomingCallRequest.provincia,
                    Modalidad = ConfigurationManager.AppSettings("gConstKeyStrModalidad")
                };

                var customerClfy = new FixedContract.CustomerClfyBpel()
                {
                    Account = string.Empty,
                    ContactObjId = string.Empty,
                    FlagReg = CONSTANTS.strUno
                };


                
                FixedContract.Iteraction oIteraction = GetDataInteraction(objIncomingCallRequest);               


                var interact = new FixedContract.InteractionBpel
                {
                    Contactobjid = oIteraction.OBJID_CONTACTO,
                    Phone = oIteraction.TELEFONO,
                    Tipo = oIteraction.TIPO,
                    Clase = oIteraction.CLASE,
                    Subclase = oIteraction.SUBCLASE,
                    Agente = oIteraction.AGENTE,
                    TipoInter = oIteraction.TIPO_INTER,
                    MetodoContacto = oIteraction.METODO,
                    Resultado = oIteraction.RESULTADO,
                    UsrProceso = oIteraction.USUARIO_PROCESO,
                    FlagCaso = oIteraction.FLAG_CASO,
                    Notas = oIteraction.NOTAS
                };

                FixedContract.InsertTemplateInteraction oInsertTemplateInteraction = GetDataIteracionTemplate(objIncomingCallRequest);

                var interactPlus = new FixedContract.InteractionPlusBpel
                {
                    ClaroNumber = oInsertTemplateInteraction._X_CLARO_NUMBER,
                    Inter15 = oInsertTemplateInteraction._X_INTER_15,
                    FirstName = oInsertTemplateInteraction._X_FIRST_NAME,
                    LastName = oInsertTemplateInteraction._X_LAST_NAME ?? string.Empty,
                    DocumentNumber = oInsertTemplateInteraction._X_DOCUMENT_NUMBER,
                    ReferencePhone = oInsertTemplateInteraction._X_REFERENCE_PHONE,
                    Inter20 = Convert.ToDate(oInsertTemplateInteraction._X_INTER_20).ToString("dd/MM/yyyy"),
                    Inter21 = Convert.ToDate(oInsertTemplateInteraction._X_INTER_21).ToString("dd/MM/yyyy"),
                    Inter30 = oInsertTemplateInteraction._X_INTER_30.Replace("|", " ").ToString(),
                    FlagRegistered = oInsertTemplateInteraction._X_FLAG_REGISTERED,
                    AdjustmentAmount = oInsertTemplateInteraction._X_ADJUSTMENT_AMOUNT.ToString(),
                    Position = oInsertTemplateInteraction._X_POSITION,
                    Birthday = DateTime.Now.ToString("ddMMyyyy"),
                    ExpireDate = DateTime.Now.ToString("ddMMyyyy"),
                    Email = oInsertTemplateInteraction._X_EMAIL,
                    Inter5 = oInsertTemplateInteraction._X_INTER_5
                };


                Model.BpelCallDetailModel oBpelCallDetailModel = new Model.BpelCallDetailModel
                {
                    DetailCallRequestBpelModel = new Model.DetailCallRequestBpelModel
                    {
                        TipoConsulta = CONSTANTS.Letter_E,
                        Msisdn = objIncomingCallRequest.phone,
                        FechaInicio = Convert.ToDate(objIncomingCallRequest.fecStart).ToString("ddMMyyyy"),
                        FechaFin = Convert.ToDate(objIncomingCallRequest.fecEnd).ToString("ddMMyyyy"),
                        FlagConstancia = CONSTANTS.strCero,
                        IpCliente = audit.ipAddress,
                        TipoConsultaContrato = CONSTANTS.Letter_T,
                        ValorContrato = objIncomingCallRequest.phone,
                        FlagContingencia = KEY.AppSettings("gConstContingenciaClarify_SIACU"),
                        CodigoCliente = objIncomingCallRequest.customerId,
                        FlagEnvioCorreo = string.Empty,
                        FlagGenerarOcc = string.Empty,
                        InvoiceNumber = string.Empty,
                        Periodo = string.Empty,
                        TipoProducto = objIncomingCallRequest.typeProduct
                    }
                };

                var bodyRequest = new FixedContract.DetalleLlamadasRequestBpel
                {
                    TipoConsulta = oBpelCallDetailModel.DetailCallRequestBpelModel.TipoConsulta,
                    Msisdn = oBpelCallDetailModel.DetailCallRequestBpelModel.Msisdn,
                    FechaInicio = oBpelCallDetailModel.DetailCallRequestBpelModel.FechaInicio,
                    FechaFin = oBpelCallDetailModel.DetailCallRequestBpelModel.FechaFin,
                    ContactUserBpel = contactUser,
                    CustomerClfyBpel = customerClfy,
                    InteractionBpel = interact,
                    InteractionPlusBpel = interactPlus,
                    FlagConstancia = oBpelCallDetailModel.DetailCallRequestBpelModel.FlagConstancia,
                    IpCliente = oBpelCallDetailModel.DetailCallRequestBpelModel.IpCliente,
                    TipoConsultaContrato = oBpelCallDetailModel.DetailCallRequestBpelModel.TipoConsultaContrato,
                    ValorContrato = oBpelCallDetailModel.DetailCallRequestBpelModel.ValorContrato,
                    FlagContingencia = oBpelCallDetailModel.DetailCallRequestBpelModel.FlagContingencia,
                    CodigoCliente = oBpelCallDetailModel.DetailCallRequestBpelModel.CodigoCliente,
                    FlagEnvioCorreo = oBpelCallDetailModel.DetailCallRequestBpelModel.FlagEnvioCorreo,
                    FlagGenerarOcc = oBpelCallDetailModel.DetailCallRequestBpelModel.FlagGenerarOcc,
                    InvoiceNumber = oBpelCallDetailModel.DetailCallRequestBpelModel.InvoiceNumber,
                    Periodo = oBpelCallDetailModel.DetailCallRequestBpelModel.Periodo,
                    TipoProducto = oBpelCallDetailModel.DetailCallRequestBpelModel.TipoProducto
                };

                var objRequest = new FixedContract.BpelCallDetailRequest()
                {
                    audit = audit,
                    DetalleLlamadasRequestBpel = bodyRequest,
                    HeaderRequestTypeBpel = headerRequest
                };

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Parametros de Entrada para el BPEL : " + objRequest.ToString());                


                oCallDetailResponse = Claro.Web.Logging.ExecuteMethod<FixedContract.CallDetailInputFixedResponse>(() =>
                {
                    return oFixedService.GetCallDetailInputFixed(objRequest);
                });

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "RESPONSE SEARCH " + oCallDetailResponse.ListCallDetailSummary.Count);

                
                //Session["ListCallsDetailHFCEntrantes"] = oCallDetailResponse.ListCallDetailSummary;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error al Buscar BPEL : " + ex.Message);                
            }

            return Json(new { data = oCallDetailResponse.ListCallDetailSummary, message = oCallDetailResponse.ListCallDetailSummary == null ? "0" : oCallDetailResponse.ListCallDetailSummary.Count.ToString(), codigoRespuesta = oCallDetailResponse.codigoRespuesta, descripcionRespuesta = oCallDetailResponse.descripcionRespuesta }, JsonRequestBehavior.AllowGet);

        }       

        private bool RegisterLogTrx(string strAccion, string strPhone, string strInteraction, string strTypification,
           string strOpcionCode, string strCodAccionEvento, string strParamIN, string strParamOUT, CommonService.AuditRequest audit)
        {
            DateTime dNow = DateTime.Now;
            string strCliente = string.Empty ;
            string strIpCliente = HttpContext.Request.UserHostAddress;
            try
            {
                strCliente = System.Net.Dns.GetHostByAddress(strIpCliente).HostName;
            }
            catch
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format("Llamada_Telefono {0} \n Error en el RegistroLogTrx. IpCliente: {1} /Hostname: {2}",
                    dNow.ToString("yyyyMMdd"), strIpCliente, strCliente));
                strCliente = strIpCliente;
            }

            string strMsg = string.Empty;
            bool salida = false;
            string strAccionEvento = KEY.AppSettings(strCodAccionEvento);

            try
            {
                strAccionEvento = strAccion + " - " + strAccionEvento;
                CommonService.InsertLogTrxRequestCommon request = new CommonService.InsertLogTrxRequestCommon()
                {
                    Accion = strAccionEvento,
                    Aplicacion = "SIACUNICO",
                    audit = audit,
                    IdInteraction = strInteraction,
                    IdTypification = strTypification,
                    InputParameters = strParamIN,
                    OutpuParameters = strParamOUT,
                    IPPCClient = strIpCliente,
                    PCClient = strCliente,
                    IPServer = audit.ipAddress,
                    NameServer = audit.applicationName,
                    Opcion = strOpcionCode,
                    Phone = strPhone,
                    Transaccion = "DETALLE_LLAMADA_ENTRANTE",
                    User = audit.userName
                };
                string flagInsertion = string.Empty;
                InsertLogTrx(request, out flagInsertion);
                if (flagInsertion.Equals("OK")) salida = true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format("Error REGISTERLOG: {0}", ex.Message));
                salida = false;
                strMsg = ex.Message;
            }
            return salida;
        }
        private void InsertLogTrx(CommonService.InsertLogTrxRequestCommon request, out string flagInsertion)
        {
            flagInsertion = string.Empty;

            try
            {
                CommonService.InsertLogTrxResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.InsertLogTrxResponseCommon>(() =>
                {
                    return oCommonService.InsertLogTrx(request);
                });
                flagInsertion = objResponse.FlagInsertion;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, String.Format("Error InsertLogTrx: {0}", ex.Message));                
            }

            
        }
        public ActionResult IncomingCallDetailPrint(string phone, string cacdac, string fecStart, string fecEnd, string fullName)
        {
            Claro.Web.Logging.Info("123456789", "123456", "IncomingCallDetailPrint : " + phone);

            IncomingCallDetailPrint oIncomingCallDetailPrint = new IncomingCallDetailPrint()
            {
                phone = phone,
                cacdac = cacdac,
                fecStart = fecStart,
                fecEnd = fecEnd,
                fullName = fullName
            };

            
            //LstPhoneCall = Session["ListCallsDetailHFCEntrantes"] as  List<FixedContract.CallDetailSummary>

            return PartialView("~/Areas/Transactions/Views/IncomingCallDetail/HfcIncomingCallDetailPrint.cshtml", oIncomingCallDetailPrint);
        }

        public JsonResult GetIncomingCallConstancy()
        {            
            return Json(new { strutaConstancy = rutaConstancy }, JsonRequestBehavior.AllowGet);
        }

        private void InsertAudit(IncomingCall objIncomingCallRequest, string strNameUserLoging, string strCodEvent, string strText, string strPhoneOpd)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(objIncomingCallRequest.strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "HfcIncomingCallDetailController", "InsertAudit", "strWebServiceSeguridad");
            Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Message" + msg);
            string strTransac = KEY.AppSettings(strCodEvent.Trim());
            string strService = KEY.AppSettings("gConstEvtServicio_ModCP");
            string strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strNameClient = strNameUserLoging;
            string strIpServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationIp();
            string strNameServer = Claro.SIACU.Web.WebApplication.Transac.Service.App_Code.Common.GetApplicationName();
            string strAccuntUser = objIncomingCallRequest.currentUser;
            string strAmount = Functions.CheckStr(objIncomingCallRequest.idMonto);
            string strPhone = strPhoneOpd;
            bool result = false;



            try
            {
                SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strAccuntUser,
                    vIpCliente = strIpClient,
                    vIpServidor = strIpServer,
                    vMonto = strAmount,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNameServer,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = strText,
                    vTransaccion = strTransac,
                    audit = audit
                };

                Claro.Web.Logging.Info("Session: " + audit.Session, "Transaction: " + audit.transaction, "Iniciando SAVE AUDIT");
                SaveAuditResponseCommon SaveAudit = SaveResponse(objRequest);
                result = SaveAudit.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objIncomingCallRequest.strIdSession, audit.transaction, "ERROR SAVE AUDIT :" + ex.Message);               
            }

        }
    }
}