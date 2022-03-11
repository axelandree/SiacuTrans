using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.Web;
using AutoMapper;
using Constant = Claro.SIACU.Transac.Service.Constants;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.PreTransacService;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Prepaid
{
    public class TFIPlanMigrationController : CommonServicesController
    {
        private readonly CommonTransacServiceClient ServiceCommon = new CommonTransacServiceClient();
        private readonly PreTransacServiceClient ServicePrepaid = new PreTransacServiceClient();

        public ActionResult TFIPlanMigration()
        {
            Claro.Web.Logging.Configure();
            return PartialView("~/Areas/Transactions/Views/PlanMigration/PrepaidTFIPlanMigration.cshtml");
        }

        public JsonResult GetValidateState(string strIdSession, string strSessionState)
        {
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(strIdSession);
            Model.Prepaid.TFIPlanMigrationModel oModel = new Model.Prepaid.TFIPlanMigrationModel();
            oModel.bValidateState = false;
            oModel.strMessageValidateState = KEY.AppSettings("strMessageValidateState");
            try
            {
                string strState = KEY.AppSettings("strValidateState");
                var s = strState.Split('|');
                for (int i = 0; i < s.Length; i++)
                {
                    if (strSessionState == s[i])
                    {
                        oModel.bValidateState = true;
                    }
                }
                oModel.strStateLine = GetStateLine(strIdSession, strSessionState);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = oModel });
        }

        public string GetStateLine(string strIdSession, string strStateLine)
        {
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(strIdSession);
            string stateLine;
            try
            {
                stateLine = GetValueFromListValues(strIdSession, strStateLine, Constant.NameListStateLine);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction + strStateLine, ex.Message);
                throw new Exception(ex.Message);
            }

            return stateLine;
        }

        public JsonResult GetPlanTFI(string strIdSession, string strSuscriber, string strProveedor, string strTarifa)
        {
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(strIdSession);
            Logging.Info(audit.Session, audit.transaction, "Entra a GetPlanTFI");
            responseDataObtenerTabConsultaPlanTFIPost objPlanesTFIResponse;
            
            PlanesTFIRequest objPlanesTFIRequest = new PlanesTFIRequest()
            {
                audit = audit,

                Header = new PreTransacService.HeaderRequest()
                {
                    country = KEY.AppSettings("country"),
                    language = KEY.AppSettings("language"),
                    consumer = KEY.AppSettings("consumer"),
                    system = KEY.AppSettings("system"),
                    msgType = KEY.AppSettings("msgType"),
                },
                obtenerTabCambioPlanTFI = new obtenerTabCambioPlanTFIPostRequest()
                {
                    suscriptor = strSuscriber,
                    proveedor = strProveedor,
                    tarifa = strTarifa,
                },

            };
            try
            {
                objPlanesTFIResponse = Logging.ExecuteMethod<responseDataObtenerTabConsultaPlanTFIPost>(() =>
                {
                    return ServicePrepaid.GetPlanesTFI(objPlanesTFIRequest);
                });
            }

            catch (Exception ex)
            {
                Logging.Error(strIdSession, objPlanesTFIRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            List<Helpers.CommonServices.GenericItem> listaPlanesTFI = new List<Helpers.CommonServices.GenericItem>();

            if (objPlanesTFIResponse.listaTabConsultaPlanTFIPost != null && objPlanesTFIResponse.listaTabConsultaPlanTFIPost.Length > 0)
            {
                objPlanesTFIResponse.listaTabConsultaPlanTFIPost.ToList().ForEach(item =>
                    {
                        var objItem = new Helpers.CommonServices.GenericItem
                        {
                            Description = item.desc_plan,
                            Code = String.Concat(item.provider,"|",item.tariff,"|",item.suscriber),
                        };
                        listaPlanesTFI.Add(objItem);
                    });

            };
            Logging.Info(audit.Session, audit.transaction, "Sale de GetPlanTFI");
            return Json(listaPlanesTFI, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePlanMigrationTFI(Model.Prepaid.TFIPlanMigrationModel oModel)
        {
           
            AuditRequestCommon audit = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(oModel.IdSession);
            Logging.Info(audit.Session, audit.transaction, "Entra a SavePlanMigrationTFI");
            bool bConstancyPDF = false;
            bool bSaveChange = false;
            bool bSaveInteraction = false;
            string strRutaArchivo = string.Empty;
            string strMensajeEmail = string.Empty;
            try
            {
                bSaveChange = SaveChangePLan(oModel);
                if (bSaveChange)
                {
                    bSaveInteraction = SaveInseraction(oModel);
                    if (bSaveInteraction)
                    {
                        bConstancyPDF = GeneratePDF(oModel);
                        if (bConstancyPDF)
                        {
                            GetInsertEvidence(oModel);
                            if (oModel.bSendMail)
                            {
                                byte[] attachFile = null;
                                CommonServicesController objCommonServices = new CommonServicesController();
                                string strAdjunto = string.IsNullOrEmpty(oModel.strRoutePDF) ? string.Empty : oModel.strRoutePDF.Substring(oModel.strRoutePDF.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                                
                                var ResultMAil = string.Empty;
                                if (objCommonServices.DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, oModel.strRoutePDF, out attachFile))
                                {
                                    strMensajeEmail = SendCorreo(oModel, strAdjunto, attachFile);
                                }
                            }
                        }
                    }
                    SaveAudit(oModel);

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return Json(new { data = oModel });
        }

        public bool SaveChangePLan(Model.Prepaid.TFIPlanMigrationModel oModel)
        {
            PreTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PreTransacService.AuditRequest>(oModel.IdSession);
            Logging.Info(audit.Session, audit.transaction, "Entra a SavePlanMigrationTFI");
            CambioPlanTFIResponse objCambioPlanTFIResponse = new CambioPlanTFIResponse();
            oModel.strOfferDescription = String.Concat("Claro",oModel.strProvider,"_"+oModel.strTariff);
            CambioPlanTFIRequest objCambioPlanTFIRequest = new CambioPlanTFIRequest()
            {
                audit = audit,
                telefono = oModel.strTelephone,
                offer = oModel.strOfferDescription,
                subscriberStatus = oModel.strSuscriber,
            };
            try
            {
                objCambioPlanTFIResponse = Claro.Web.Logging.ExecuteMethod<CambioPlanTFIResponse>(() =>
                    { 
                        return ServicePrepaid.GetCambioPlanTFI(objCambioPlanTFIRequest); 
                    });
            }
           
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

            if (objCambioPlanTFIResponse != null && objCambioPlanTFIResponse.idRespuesta == Constant.strCero)
            {
                oModel.bErrorTransac = true;
                oModel.strNewOfferDescription = objCambioPlanTFIResponse.offerNuevo;
                oModel.strMessageError = objCambioPlanTFIResponse.mensajeRespuesta;
            }
            else
            {
                oModel.bErrorTransac = false;
                oModel.strMessageError = objCambioPlanTFIResponse.mensajeRespuesta;
            }
                 
            return oModel.bErrorTransac;
        }

        public bool SaveInseraction(Model.Prepaid.TFIPlanMigrationModel oModel)
        {
            var objInteractionModel = new Iteraction();
            var oPlantillaDat = new InsertTemplateInteraction();
            AuditRequestCommon audit = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(oModel.IdSession);
            oModel.bErrorInteract = false;
            oModel.strDateTransaction = DateTime.Now.ToShortDateString();
            oModel.strAction = KEY.AppSettings("strCambioPlanAction");
            try
            {
                oModel.strCodeTipification = KEY.AppSettings("strKeyTransaccionCambioPlanTFI");
                objInteractionModel = DatInteraction(oModel);
                oPlantillaDat = GetDatTemplateInteraction(oModel);

                InsertGeneralRequest request = new InsertGeneralRequest()
                {
                    audit = audit,
                    Interaction = objInteractionModel,
                    InteractionTemplate = oPlantillaDat,
                    vEjecutarTransaccion = true,
                    vNroTelefono = oModel.strTelephone,
                    vUSUARIO_APLICACION = KEY.AppSettings("gUserSystemWSConsultationPrepaid"),
                    vUSUARIO_SISTEMA = CurrentUser(oModel.IdSession),
                    vPASSWORD_USUARIO = KEY.AppSettings("gPasswordApplicationWSConsultationPrepaid")
                };

                InsertGeneralResponse objResponse = Claro.Web.Logging.ExecuteMethod<InsertGeneralResponse>(() =>
                    {
                        return ServiceCommon.GetinsertInteractionGeneral(request); ;
                    });

                oModel.strSubClaseCode = objInteractionModel.SUBCLASE_CODIGO;
                oModel.strSubClaseDescription = objInteractionModel.SUBCLASE;
                oModel.strIdTipification = objResponse.rInteraccionId;
                oModel.bErrorInteract = true;
                if (objResponse.rFlagInsercion.ToUpper() != Constant.CriterioMensajeOK && objResponse.rFlagInsercion != string.Empty)
                {
                    oModel.strMessageErrorTransac = "Error al crear Interacción: " + objResponse.rMsgText;
                    oModel.bErrorInteract = false;
                }
                if (objResponse.rFlagInsercionInteraccion.ToUpper() != Constant.CriterioMensajeOK && objResponse.rFlagInsercionInteraccion != string.Empty)
                {
                    oModel.strMessageErrorTransac = "Se creó la interaccion pero existe error en la transacción, el numero insertado es: " + objResponse.rInteraccionId + " por el siguiente error: " + objResponse.rMsgTextInteraccion + objResponse.rMsgText;
                    oModel.bErrorInteract = false;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return oModel.bErrorInteract;
        }

        public Iteraction DatInteraction(Model.Prepaid.TFIPlanMigrationModel model)
        {
            AuditRequestCommon audit = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(model.IdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, " entra a DatInteraction");
            var objInteractionModel = new Iteraction();

            try
            {
                var tipification = GetTypification(audit.Session, model.strCodeTipification);
                tipification.ToList().ForEach(x =>
                {
                    objInteractionModel.TIPO = x.Type;
                    objInteractionModel.CLASE = x.Class;
                    objInteractionModel.SUBCLASE = x.SubClass;
                    objInteractionModel.TIPO_CODIGO = x.TypeCode;
                    objInteractionModel.CLASE_CODIGO = x.ClassCode;
                    objInteractionModel.SUBCLASE_CODIGO = x.SubClassCode;
                });
                objInteractionModel.OBJID_CONTACTO = model.strObjidContacto;
                objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
                objInteractionModel.TELEFONO = model.strTelephone;
                objInteractionModel.TIPO_INTER = KEY.AppSettings("AtencionDefault");
                objInteractionModel.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
                objInteractionModel.RESULTADO = KEY.AppSettings("Ninguno");
                objInteractionModel.HECHO_EN_UNO = Constant.strCero;
                objInteractionModel.NOTAS = model.strNote;
                objInteractionModel.FLAG_CASO = Constant.strCero;
                objInteractionModel.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
                objInteractionModel.AGENTE = model.strCurrentUser;
                objInteractionModel.ES_TFI = model.strisTFI;

            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return objInteractionModel;
        }

        public InsertTemplateInteraction GetDatTemplateInteraction(Model.Prepaid.TFIPlanMigrationModel model)
        {
            AuditRequestCommon audit = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(model.IdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "entra a GetDatTemplateInteraction");
            var oPlantCampDat = new InsertTemplateInteraction();
            try
            {
                oPlantCampDat._NOMBRE_TRANSACCION = model.strCodeTipification;
                oPlantCampDat._X_CLARO_NUMBER = model.strTelephone;
                oPlantCampDat._X_INTER_19 = model.strTelephone;
                oPlantCampDat._X_DOCUMENT_NUMBER = model.strDocument;
                oPlantCampDat._X_OPERATION_TYPE = model.strNewOfferDescription;
                oPlantCampDat._X_FIRST_NAME = model.strFullName;
                oPlantCampDat._X_FLAG_REGISTERED = Constant.strUno;
                oPlantCampDat._X_NAME_LEGAL_REP = model.strLegalAgent;
                oPlantCampDat._X_INTER_5 = Constant.strCero;
                oPlantCampDat._X_INTER_3 = Constant.strMenosUno;
                oPlantCampDat._X_INTER_15 = model.strPlanDescription;
                oPlantCampDat._X_INTER_17 = model.strPuntoAtencion;
                oPlantCampDat._X_INTER_18 = model.strDocumentType;
                oPlantCampDat._X_OTHER_FIRST_NAME = model.strNameUser;
                oPlantCampDat._X_INTER_20 = model.strCurrentUser;
                oPlantCampDat._X_INTER_16 = model.strNewPlanDescription;
                oPlantCampDat._X_INTER_29 = model.strDateTransaction;
                oPlantCampDat._X_INTER_21 = model.strStateLine;
                oPlantCampDat._X_INTER_6 = model.strDateActivation;
                oPlantCampDat._X_INTER_7 = model.strAction;
                if (model.bSendMail)
                {
                    if (!string.IsNullOrEmpty(model.strEmail))
                    {
                        oPlantCampDat._X_REGISTRATION_REASON = model.strEmail;
                        oPlantCampDat._X_INTER_5 = Constant.strUno;
                    }
                    else
                    {
                        oPlantCampDat._X_REGISTRATION_REASON = string.Empty;
                        oPlantCampDat._X_INTER_5 = Constant.strCero;
                    }
                }

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "sale de GetDatTemplateInteraction");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return oPlantCampDat;
        }

        public List<TypificationModel> GetTypification(string strIdSession, string strTransactionName)
        {
            var response = new List<TypificationModel>();
            TypificationResponse objTypificationResponse = null;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest = new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "entra a GetTypification");
            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return ServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "Error GetTypification : " + ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objTypificationResponse.ListTypification;

            if (objTypificationResponse.ListTypification != null)
                Claro.Web.Logging.Info(audit.Session, audit.transaction, objTypificationResponse.ListTypification.Count().ToString());
            else
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "0 o null");

            response = Mapper.Map<List<TypificationModel>>(tempLst);
            return response;
        }

        private bool GeneratePDF(Model.Prepaid.TFIPlanMigrationModel oModel)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Entra a GeneratePDF");
            ParametersGeneratePDF objParametersGeneratePdf = new ParametersGeneratePDF();
            
            string strInteractionId = oModel.strIdTipification;
            string strTermPDF = string.Empty;

            oModel.strRoutePDF = string.Empty;
            oModel.bGeneratedPDF = false;

            try
            {

                objParametersGeneratePdf.StrNroServicio = oModel.strTelephone;
                objParametersGeneratePdf.StrTitularCliente = oModel.strFullName;
                objParametersGeneratePdf.StrTipoDocIdentidad = oModel.strDocumentType;
                objParametersGeneratePdf.StrNroDocIdentidad = oModel.strDocument;

                if (oModel.bSendMail)
                {
                    objParametersGeneratePdf.StrAplicaEmail = "SI";
                    objParametersGeneratePdf.StrEmail = oModel.strEmail;
                }
               
                objParametersGeneratePdf.StrCentroAtencionArea = oModel.strPuntoAtencion;
                objParametersGeneratePdf.StrNuevoPlan = oModel.strNewPlanDescription;
                objParametersGeneratePdf.StrPlanAnterior = oModel.strPlanDescription;

                objParametersGeneratePdf.StrRepresLegal = oModel.strLegalAgent;

                objParametersGeneratePdf.StrCasoInter = strInteractionId;
                objParametersGeneratePdf.StrFechaTransaccionProgram = oModel.strDateTransaction;
                objParametersGeneratePdf.StrFechaEjecucion = oModel.strDateTransaction;
                objParametersGeneratePdf.strAccionEjecutar = oModel.strAction;

                objParametersGeneratePdf.StrNombreArchivoTransaccion = KEY.AppSettings("strNombreArchivoTransaccionCambioPlanTFI");
                objParametersGeneratePdf.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaCambioPlan");
                objParametersGeneratePdf.StrTipoTransaccion = KEY.AppSettings("strSolicitudConstancia");
                objParametersGeneratePdf.StrCodigoAsesor = oModel.strNameUser;
                objParametersGeneratePdf.StrNombreAsesor = oModel.strCurrentUser;
            
                CommonServicesController objResponse = new CommonServicesController();

                GenerateConstancyResponseCommon objGeneratePdf = objResponse.GenerateContancyPDF(oModel.IdSession, objParametersGeneratePdf);
                string strError = objGeneratePdf.ErrorMessage;
                oModel.bGeneratedPDF = objGeneratePdf.Generated;

                oModel.strRoutePDF = objGeneratePdf.FullPathPDF;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, String.Format("Error GenerarPDF(): {0}", ex.Message));
            }

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "sale de GeneratePDF");

            return oModel.bGeneratedPDF;
        }

        public void GetInsertEvidence(Model.Prepaid.TFIPlanMigrationModel oModel) 
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Entra a GetInsertEvidence");
            string strDateTransaction = oModel.strDateTransaction.Replace("/", "_");
            InsertEvidenceRequest oInsertEvidenceRequest;
           
            try
            {
                oInsertEvidenceRequest = new InsertEvidenceRequest()
                {
                    audit = audit,
                    Evidence = new Evidence()
                    {
                        StrTransactionType = KEY.AppSettings("strTransactionType"),
                        StrTransactionCode = oModel.strIdTipification,
                        StrCustomerCode = oModel.strCustomerCode,
                        StrPhoneNumber = oModel.strTelephone,
                        StrTypificationCode = oModel.strSubClaseCode,
                        StrTypificationDesc = oModel.strSubClaseDescription,
                        StrCommercialDesc = oModel.strSubClaseDescription,
                        StrProductType = string.Empty,
                        StrServiceChannel = oModel.strPuntoAtencion,
                        StrTransactionDate = oModel.strDateTransaction,
                        StrActivationDate = oModel.strDateActivation,
                        StrSuspensionDate = null,
                        StrServiceStatus = oModel.strStateLine,

                        StrDocumentPath = String.Format(KEY.AppSettings("strServidorLeerPDF"), KEY.AppSettings("strCarpetaPDFs"), KEY.AppSettings("strCarpetaCambioPlan")),
                        StrUserName = oModel.strCurrentUser,
                        StrDocumentType = KEY.AppSettings("strNombreArchivoTransaccionCambioPlanTFI"),
                        StrDocumentName = String.Format(oModel.strIdTipification, strDateTransaction, KEY.AppSettings("strNombreArchivoTransaccionCambioPlanTFI"), KEY.AppSettings("strTerminacionPDF")),
                        StrDocumentExtension = KEY.AppSettings("strDocumentoExtension"),
                    }
                };
                InsertEvidenceResponse oInsertEvidenceResponse = Claro.Web.Logging.ExecuteMethod<InsertEvidenceResponse>(() =>
                {
                    return ServiceCommon.GetInsertEvidence(oInsertEvidenceRequest); 
                });

                if(oInsertEvidenceResponse.BoolResult)
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, String.Format("Error GetInsertEvidence(): {0}", KEY.AppSettings("strRegistroOK")));
                }
                else 
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format("Error GetInsertEvidence(): {0}", KEY.AppSettings("strRegistroNOOK")));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format("Error GetInsertEvidence(): {0}", ex.Message));
            }
        }

        private string SendCorreo(Model.Prepaid.TFIPlanMigrationModel oModel, string strAdjunto, byte[] attachFile)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
            Claro.Web.Logging.Info(audit.Session,audit.transaction, "Entra a SendCorreo");
            string strResul = string.Empty;

            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strTemplateEmail = TemplateEmail();
                string strTIEMailAsunto = oModel.strAction;


                string strDestinatarios = oModel.strEmail;
                string strRemitente = KEY.AppSettings("CorreoServicioAlCliente");
                CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
                objGetSendEmailRequest = new CommonTransacService.SendEmailRequestCommon()
                    {
                        audit = audit,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strTemplateEmail,
                        strAttached = strAdjunto,
                        strSubject = strTIEMailAsunto,
                        AttachedByte = attachFile
                    };

                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(
                    () =>
                    {
                        return ServiceCommon.GetSendEmail(objGetSendEmailRequest);
                    });

                if (objGetSendEmailResponse.Exit == Constant.CriterioMensajeOK)
                {
                    strResul = Functions.GetValueFromConfigFile("strMensajeEnvioOK", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                else
                {
                    strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, String.Format("Error SendCorreo(): {0}", ex.Message));
            }
            return strResul;
        }

        private string TemplateEmail()
        {
            string strmessage = string.Empty;
            var strHtml = new System.Text.StringBuilder();

            strHtml.Append("<html>");
            strHtml.Append("<head>");
            strHtml.Append("<style type='text/css'>");
            strHtml.Append("<!--");
            strHtml.Append(".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}");
            strHtml.Append(".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}");
            strHtml.Append("-->");
            strHtml.Append("</style>");
            strHtml.Append("<body>");
            strHtml.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>");

            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>Por la presente queremos informarle que su solicitud de Cambio de Plan TFI Prepago fue atendida.</td></tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td align='center'>");
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>&nbsp;</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>Cordialmente</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>Atención al Cliente</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>");
            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }

        public bool SaveAudit(Model.Prepaid.TFIPlanMigrationModel oModel)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.IdSession);
            bool FlatResultado = false;
            string strCodigoAuditoria = KEY.AppSettings("gConstCodigoAuditoriaCambioPlanTFI");
            string strService = KEY.AppSettings("gConstEvtServicio");

            Claro.Web.Logging.Info(oModel.IdSession, "Transaccion: " + audit.transaction, "Inicio Método : SaveAudit");


            string strIpCliente = Claro.SIACU.Transac.Service.Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strIPServidor = App_Code.Common.GetApplicationIp();
            string strNombreServidor = App_Code.Common.GetApplicationName();
            string strCuentaUsuario = oModel.strCurrentUser;
            string strPhone = oModel.strTelephone;
            string strNameClient = oModel.strNameUser;
            string strTransaccion = "Transaccion: " + KEY.AppSettings("strKeyTransaccionCambioPlanTFI");
            string strPuntoAtencion = " PuntoAtencion: " + oModel.strPuntoAtencion;
            CommonTransacService.SaveAuditResponseCommon objResponse = null;

            try
            {
                CommonTransacService.SaveAuditRequestCommon objRequest = new CommonTransacService.SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strCuentaUsuario,
                    vIpCliente = strIpCliente,
                    vIpServidor = strIPServidor,
                    vMonto = Constant.strCero,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNombreServidor,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = strTransaccion + strPuntoAtencion,
                    vTransaccion = strCodigoAuditoria,
                    audit = audit
                };

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Iniciando SAVE AUDIT");
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() =>
                {
                    return ServiceCommon.SaveAudit(objRequest);
                });
                FlatResultado = objResponse.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "ERROR SAVE AUDIT :" + ex.Message);
            }

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : Auditoria");
            return FlatResultado;
        }

    }
}