using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using KEY = Claro.ConfigurationManager;
using Newtonsoft.Json;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Common = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using CommonE = Claro.SIACU.Entity.Transac.Service.Common;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using Claro.SIACU.Entity.Transac.Service.Postpaid.PostUpdateChangeData;
//using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.ProxyService.Transac.Service.CambioDatosSiacWS;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class ChangeDataController : CommonServicesController
    {
        private readonly Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.PostTransacServiceClient oServicePostpaid = new Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonService = new CommonTransacService.CommonTransacServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.FixedTransacServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.FixedTransacServiceClient();

        //
        // GET: /Transactions/ChangeData/
        public ActionResult HFCChangeData()
        {
            return PartialView();
        }

        public JsonResult PageLoad(string strIdSession)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : PageLoad");
            Common.Typification typificationLoad = null;
            string lblMensaje = "";
            try
            {
                typificationLoad = LoadTypification(audit, ref lblMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            return Json(new { data = typificationLoad });

        }


        public Common.Typification LoadTypification(CommonTransacService.AuditRequest audit,
                                                           ref string lblMensaje)
        {
            Common.Typification oTypification = null;


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypificationChangeData");


            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("TransaccionChangeData");
                objTypificationRequest.audit = audit;

                Common.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod(() => { return oCommonService.GetTypification(objTypificationRequest); });

                oTypification = objResponse.ListTypification.First();

                if (oTypification == null)
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification ingreso validacion nula");
                    lblMensaje = "No se cargo las tipificaciones";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error LoadTypifications --> " + ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }


        public JsonResult RegistarInteracion(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            string vDesInteraction = string.Empty;
            string strRutaArchivo = string.Empty;
            string vInteractionId = string.Empty;
            string strNombreArchivo = string.Empty;
            string MensajeEmail = string.Empty;

            var audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(oModel.strIdSession);
            //bool blnRetorno = false;
            // oModel.blnInteract = false;
            //string strFlgRegistrado = Claro.Constants.NumberOneString;

            try
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : RegistrarInteracion");
                DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
                DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();

                string strFlgRegistrado = Claro.Constants.NumberOneString;
                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit2 = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                {
                    audit = audit2,
                    vPhone = oModel.strPhone,
                    vAccount = string.Empty,
                    vContactobjid1 = string.Empty,
                    vFlagReg = strFlgRegistrado
                };
                objCustomerResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCustomer(objGetCustomerRequest);
                });

                if (objCustomerResponse.Customer != null)
                {
                    oModel.strObjidContacto = objCustomerResponse.Customer.ContactCode;
                }


                List<string> strInteractionId = SaveInteraction(oModel, oModelD, oModelF, DataOld, FlagD, FlagF);
                vInteractionId = strInteractionId[3].ToString();
                if (strInteractionId[0] == Claro.SIACU.Constants.OK)
                {
                    try
                    {
                        vDesInteraction = "La transacción se realizó con éxito.";
                        #region CONSTANCY PDF
                        //vDesInteraction = ConfigurationManager.AppSettings("strMsgTranGrabSatis");

                        Dictionary<string, object> oConstancyPDF = new Dictionary<string, object>();

                        oConstancyPDF = GetConstancyPDF(oModel.strIdSession, vInteractionId, oModel, oModelD, oModelF, DataOld, FlagD, FlagF);
                        strRutaArchivo = oModel.strFullPathPDF;
                        #endregion

                        if (oModel.Flag_Email)
                        {

                            #region SEND EMAIL
                            byte[] attachFile = null;
                            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                            if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile))
                                MensajeEmail = GetSendEmail(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile);
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Info(oModel.strIdSession, "GetConstancyPDF-GetSendEmail", "ERROR: " + ex.Message);
                    }

                }
                else
                {
                    vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo });
        }

        #region CONSTANCY PDF - SEND EMAIL
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {

            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strTerminacionPdf = ConfigurationManager.AppSettings("strTerminacionPDF").ToString();
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;
            string xml = string.Empty;

            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            CommonTransacService.ParametersGeneratePDF parameters = new CommonTransacService.ParametersGeneratePDF();

            try
            {
                parameters.StrCasoInter = strIdInteraction;
                parameters.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaCambioDatosPost");
                parameters.StrNombreArchivoTransaccion = KEY.AppSettings("strCambioDatosFormatoTransac");
                parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");
                parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
                //        strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgDatosMenoresConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                xml = GetGenerateConstancyXml(strIdInteraction, oModel, oModelD, oModelF, DataOld, FlagD, FlagF);



                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyWithXml(strIdSession, parameters, xml);

                nombrepath = response.FullPathPDF;
                var generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);

                Claro.Web.Logging.Info("nombrepath: " + nombrepath, "generado: " + generado, "strNombreArchivo : " + strNombreArchivo);
                Claro.Web.Logging.Info(strIdSession, "Metodo :  GetConstancyPDF - Fín ", "nombrepath : " + nombrepath);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oModel.strIdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }


        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.Postpaid.ChangeDataModel model, string strNombreArchivoPDF, byte[] attachFile)
        {
            string strResul = string.Empty;
            Common.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(model.strIdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailMinorChanges",
                    ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string OpcionRetenido = string.Empty;
                Claro.Web.Logging.Info(model.strIdSession, "strInteraccionId: " + strInteraccionId, "Inicio Método : GetSendEmail");

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
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + " </td></tr>";
                if (model.Accion == Claro.Constants.LetterR)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailMinorChanges", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                }



                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
                objGetSendEmailRequest =
                    new CommonTransacService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = model.strEmailSend,
                        strMessage = strMessage,
                        strAttached = strAdjunto,
                        strSubject = strAsunto,
                        AttachedByte = attachFile
                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailFixed(objGetSendEmailRequest); });

                if (objGetSendEmailResponse.Exit == Claro.SIACU.Constants.OK)
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                }
                else
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                }
                Claro.Web.Logging.Info(model.strIdSession, "strInteraccionId: " + strInteraccionId, "Fín Método : GetSendEmail - strResul : " + strResul);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.strIdSession, AuditRequest.transaction, ex.Message);
                Claro.Web.Logging.Info(model.strIdSession, AuditRequest.transaction, "Cambio_Datos  ERROR - GetSendEmail");
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
            }
            return strResul;
        }

        public string GetGenerateConstancyXml(string strInteraccion, Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            string xmlConstancyPDF = string.Empty;
            try
            {
                string pathFileXml = KEY.AppSettings("strRutaXmlConstanciaCambioDatos");
                string vacio = string.Empty;
                string FechAct = DateTime.Now.ToString("dd/MM/yyyy");

                var listParamConstancyPdf = new List<string>();

                listParamConstancyPdf.Add(KEY.AppSettings("strCambioDatosFormatoTransac"));
                listParamConstancyPdf.Add(KEY.AppSettings("strNombreTransaccionCambioDatos"));

                listParamConstancyPdf.Add(oModel.strCacDac);
                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL);
                listParamConstancyPdf.Add(DataOld.Cliente.TIPO_DOC);

                if (DataOld.Cliente.TIPO_DOC == "RUC")
                {
                    listParamConstancyPdf.Add(DataOld.Cliente.REPRESENTANTE_LEGAL);
                    listParamConstancyPdf.Add(vacio);
                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(oModel.strPhone);
                }
                listParamConstancyPdf.Add(FechAct);
                listParamConstancyPdf.Add(strInteraccion);
                listParamConstancyPdf.Add(DataOld.Cliente.DNI_RUC);
                listParamConstancyPdf.Add(vacio);//contrato

                if (ValidateChangeIdPer(oModel, DataOld))
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle1"));
                    int n1 = 0;
                    int n2 = 0;
                    int n3 = 0;
                    int n4 = 0;
                    int n5 = 0;
                    int n6 = 0;
                    int n7 = 0;

                    if (oModel.strTipoDocumento == "RUC")
                    {
                        if (oModel.DNI_RUC.Substring(0, 1) == "2")
                        {
                            listParamConstancyPdf.Add(vacio);//NombreActual
                            listParamConstancyPdf.Add(vacio);//ApellidoActual

                            if (oModel.strRazonSocial != DataOld.Cliente.RAZON_SOCIAL)
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL);
                                n3 = 1;
                            }
                            else
                            {
                                listParamConstancyPdf.Add(vacio);
                            }
                            if (oModel.strNombreComercial != DataOld.Cliente.NOMBRE_COMERCIAL)
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.NOMBRE_COMERCIAL);
                                n4 = 1;
                            }
                            else
                            {
                                listParamConstancyPdf.Add(vacio);
                            }
                        }
                        else
                        {
                            if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.NOMBRES);
                                n1 = 1;
                            }
                            else
                            {
                                listParamConstancyPdf.Add(vacio);
                            }
                            if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.APELLIDOS);
                                n2 = 1;
                            }
                            else
                            {
                                listParamConstancyPdf.Add(vacio);
                            }

                            listParamConstancyPdf.Add(vacio);//RazonSocialActual
                            listParamConstancyPdf.Add(vacio);//NombreComercialActual
                        }

                    }
                    else
                    {
                        if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                        {
                            listParamConstancyPdf.Add(DataOld.Cliente.NOMBRES);
                            n1 = 1;
                        }
                        else
                        {
                            listParamConstancyPdf.Add(vacio);
                        }
                        if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                        {
                            listParamConstancyPdf.Add(DataOld.Cliente.APELLIDOS);
                            n2 = 1;
                        }
                        else
                        {
                            listParamConstancyPdf.Add(vacio);
                        }

                        listParamConstancyPdf.Add(vacio);//RazonSocialActual
                        listParamConstancyPdf.Add(vacio);//NombreComercialActual
                    }

                    if (oModel.strTxtTipoDocumento != DataOld.Cliente.TIPO_DOC)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.TIPO_DOC);
                        n5 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (oModel.DNI_RUC != DataOld.Cliente.NRO_DOC)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.NRO_DOC);
                        n6 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (oModel.strEstadoCivil != DataOld.Cliente.ESTADO_CIVIL)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.ESTADO_CIVIL);
                        n7 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }



                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle2"));
                    if (n1 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strNombres);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n2 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strApellidos);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n3 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strRazonSocial);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n4 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strNombreComercial);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n5 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strTxtTipoDocumento);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n6 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.DNI_RUC);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n7 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strEstadoCivil);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }

                if (ValidateChangeContC(oModel, DataOld))
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle3"));
                    int n8 = 0;
                    int n9 = 0;
                    if (oModel.strMovil != DataOld.Cliente.TELEFONO_CONTACTO)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.TELEFONO_CONTACTO);
                        n8 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (oModel.strMail != DataOld.Cliente.EMAIL)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.EMAIL);
                        n9 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle4"));
                    if (n8 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strMovil);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n9 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strMail);
                    }
                    else { listParamConstancyPdf.Add(vacio); }
                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }
                if (ValidateChangeRL(oModel, DataOld))
                {
                    int n10 = 0;
                    int n11 = 0;
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle5"));
                    if (oModel.RepresentLegal != DataOld.Cliente.REPRESENTANTE_LEGAL)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.REPRESENTANTE_LEGAL);
                        n10 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (oModel.strDNI != DataOld.Cliente.NRO_DOC)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.NRO_DOC);
                        n11 = 1;
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle6"));

                    if (n10 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.RepresentLegal);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                    if (n11 == 1)
                    {
                        listParamConstancyPdf.Add(oModel.strDNI);
                    }
                    else { listParamConstancyPdf.Add(vacio); }

                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }

                if (ValidateChangeCC(oModel, DataOld))
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle7"));
                    listParamConstancyPdf.Add(DataOld.Cliente.CONTACTO_CLIENTE);
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle8"));
                    listParamConstancyPdf.Add(oModel.strContactoCliente);
                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }

                if (FlagD == 1)
                {
                    if (ValidateChangeAddressLegal(oModelD, DataOld))
                    {
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle9"));
                        listParamConstancyPdf.Add(DataOld.Cliente.CALLE_LEGAL);
                        listParamConstancyPdf.Add(DataOld.Cliente.URBANIZACION_LEGAL);
                        listParamConstancyPdf.Add(DataOld.Cliente.DEPARTEMENTO_LEGAL);
                        listParamConstancyPdf.Add(DataOld.Cliente.PROVINCIA_LEGAL);
                        listParamConstancyPdf.Add(DataOld.Cliente.DISTRITO_LEGAL);
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle10"));
                        listParamConstancyPdf.Add(oModelD.strDireccion);
                        listParamConstancyPdf.Add(oModelD.strReferencia);
                        listParamConstancyPdf.Add(oModelD.strDepartamento);
                        listParamConstancyPdf.Add(oModelD.strProvincia);
                        listParamConstancyPdf.Add(oModelD.strDistrito);
                    }
                    else
                    {
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                    }
                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }

                if (FlagF == 1)
                {
                    if (ValidateChangeAddressFac(oModelF, DataOld))
                    {
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle11"));
                        listParamConstancyPdf.Add(DataOld.Cliente.CALLE_FAC);
                        listParamConstancyPdf.Add(DataOld.Cliente.URBANIZACION_FAC);
                        listParamConstancyPdf.Add(DataOld.Cliente.DEPARTEMENTO_FAC);
                        listParamConstancyPdf.Add(DataOld.Cliente.PROVINCIA_FAC);
                        listParamConstancyPdf.Add(DataOld.Cliente.DISTRITO_FAC);
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle12"));
                        listParamConstancyPdf.Add(oModelF.strDireccion);
                        listParamConstancyPdf.Add(oModelF.strReferencia);
                        listParamConstancyPdf.Add(oModelF.strDepartamento);
                        listParamConstancyPdf.Add(oModelF.strProvincia);
                        listParamConstancyPdf.Add(oModelF.strDistrito);
                    }
                    else
                    {
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                        listParamConstancyPdf.Add(vacio);
                    }
                }
                else
                {
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                    listParamConstancyPdf.Add(vacio);
                }

                if (oModel.Flag_Email == true)
                {
                    listParamConstancyPdf.Add("SI");
                    listParamConstancyPdf.Add(oModel.strEmailSend);
                }
                else
                {
                    listParamConstancyPdf.Add("NO");
                    listParamConstancyPdf.Add(vacio);
                }
                listParamConstancyPdf.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("CambioDatosContentCommercial", KEY.AppSettings("strConstArchivoSIACPOConfigMsg")));//Contenido
                listParamConstancyPdf.Add(oModel.strCodAgente);
                listParamConstancyPdf.Add(oModel.strNombAgente);


                var listLabels = GetXmlToString(App_Code.Common.GetApplicationRoute() + pathFileXml);
                var count = 0;
                var xmlGenerated = new System.Text.StringBuilder();
                foreach (string key in listLabels)
                {
                    xmlGenerated.Append(string.Format("<{0}>{1}</{2}>\r\n", key, listParamConstancyPdf[count], key));
                    count++;
                }


                xmlConstancyPDF = String.Format("<?xml version='1.0' encoding='utf-8'?><PLANTILLA>{0}</PLANTILLA>", xmlGenerated);
                Claro.Web.Logging.Info(oModel.strIdSession, "GetGenerateConstancyXml()", "xmlConstancyPDF:    " + xmlConstancyPDF);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oModel.strIdSession, "GetGenerateConstancyXml()", "ERROR:    " + ex.InnerException);
            }

            return xmlConstancyPDF.ToString();
        }


        public bool ValidateChangeIdPer(Model.Postpaid.ChangeDataModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.strTipoDocumento == "RUC")
            {
                if (oModel.DNI_RUC.Substring(0, 1) == "2")
                {
                    if (oModel.strRazonSocial != DataOld.Cliente.RAZON_SOCIAL)
                    {
                        respuesta = true;
                    }
                    if (oModel.strNombreComercial != DataOld.Cliente.NOMBRE_COMERCIAL)
                    {
                        respuesta = true;
                    }
                }
                else
                {
                    if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                    {
                        respuesta = true;
                    }
                    if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                    {
                        respuesta = true;
                    }
                }

            }
            else
            {
                if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                {
                    respuesta = true;
                }
                if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                {
                    respuesta = true;
                }
            }

            if (oModel.strTxtTipoDocumento != DataOld.Cliente.TIPO_DOC)
            {
                respuesta = true;
            }
            if (oModel.DNI_RUC != DataOld.Cliente.DNI_RUC)
            {
                respuesta = true;
            }
            if (oModel.strEstadoCivil != DataOld.Cliente.ESTADO_CIVIL)
            {
                respuesta = true;
            }

            return respuesta;
        }
        public bool ValidateChangeContC(Model.Postpaid.ChangeDataModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.strMovil != DataOld.Cliente.TELEFONO_CONTACTO)
            {
                respuesta = true;
            }
            if (oModel.strMail != DataOld.Cliente.EMAIL)
            {
                respuesta = true;
            }
            return respuesta;
        }
        public bool ValidateChangeRL(Model.Postpaid.ChangeDataModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.RepresentLegal != DataOld.Cliente.REPRESENTANTE_LEGAL)
            {
                respuesta = true;
            }
            if (oModel.strDNI != DataOld.Cliente.NRO_DOC)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public bool ValidateChangeCC(Model.Postpaid.ChangeDataModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.strContactoCliente != DataOld.Cliente.CONTACTO_CLIENTE)
            {
                respuesta = true;
            }
            return respuesta;
        }
        public bool ValidateChangeAddressLegal(Model.Postpaid.AddressCustomerModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.strDireccion != DataOld.Cliente.CALLE_LEGAL)
            {
                respuesta = true;
            }
            if (oModel.strReferencia != DataOld.Cliente.URBANIZACION_LEGAL)
            {
                respuesta = true;
            }
            if (oModel.strDepartamento != DataOld.Cliente.DEPARTEMENTO_LEGAL)
            {
                respuesta = true;
            }
            if (oModel.strProvincia != DataOld.Cliente.PROVINCIA_LEGAL)
            {
                respuesta = true;
            }
            if (oModel.strDistrito != DataOld.Cliente.DISTRITO_LEGAL)
            {
                respuesta = true;
            }


            return respuesta;
        }

        public bool ValidateChangeAddressFac(Model.Postpaid.AddressCustomerModel oModel, DataCustomerResponsePostPaid DataOld)
        {
            bool respuesta = false;

            if (oModel.strDireccion != DataOld.Cliente.CALLE_FAC)
            {
                respuesta = true;
            }
            if (oModel.strReferencia != DataOld.Cliente.URBANIZACION_FAC)
            {
                respuesta = true;
            }
            if (oModel.strDepartamento != DataOld.Cliente.DEPARTEMENTO_FAC)
            {
                respuesta = true;
            }
            if (oModel.strProvincia != DataOld.Cliente.PROVINCIA_FAC)
            {
                respuesta = true;
            }
            if (oModel.strDistrito != DataOld.Cliente.DISTRITO_FAC)
            {
                respuesta = true;
            }


            return respuesta;
        }

        #endregion

        public JsonResult GetCustomerChangeData(Model.Postpaid.ChangeDataModel oModel)
        {

            string cambiarfecha = "";

            string strPermiso = ConfigurationManager.AppSettings("gConstkeyMotivError");
            string opcAct = ConfigurationManager.AppSettings("strMotivActualizacion");
            string opcError = ConfigurationManager.AppSettings("strMotivError");
            var audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetCustomerChangeData");

            DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
            DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();
            try
            {
                objRequestCustomer = new DataCustomerRequestPostPaid()
                {
                    audit = audit,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    strtelefono = oModel.strTelefono,
                    strcustomerid = oModel.account//INC000002608479
                };

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetDataCustomer(objRequestCustomer,1);
                });

                if (objResponse.Cliente.FECHA_NAC == null || objResponse.Cliente.FECHA_NAC == "")
                {
                    cambiarfecha = "";
                }else{ cambiarfecha = objResponse.Cliente.FECHA_NAC;}

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objCus = objResponse.Cliente, objResponse, strPermiso, opcAct, opcError });
        }

        public JsonResult GetValidateEnvioxMail(string strIdSession, string strCustomerID)
        {
            List<ListItem> lista = new List<ListItem>();
            string strRespuesta = string.Empty;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            //Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetMotivoCambio");

            try
            {
                strRespuesta = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.ValidateEnvioxMail(strIdSession, audit.transaction, strCustomerID);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { data = strRespuesta });
        }

        public JsonResult GetMotivoCambio(string strIdSession)
        {
            List<ListItem> lista = new List<ListItem>();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            //Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetMotivoCambio");
            string strParametro = ConfigurationManager.AppSettings("strMotivoCambio_CD");
            string mensaje = "";
            try
            {
                lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetMotivoCambio(strIdSession, audit.transaction, strParametro, mensaje);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objLista = lista });
        }

        public JsonResult GetTypeDocument(string strIdSession)
        {
            List<ListItem> lista = new List<ListItem>();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            //Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetMotivoCambio");
            string strParametro = ConfigurationManager.AppSettings("ConstKeyListaDocumentos");
            string mensaje = "";
            try
            {
                lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetDocumentType(strIdSession, audit.transaction, strParametro);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objLista = lista });
        }

        public JsonResult GetCivilStatus(string strIdSession, string strTransaccion)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Common.CivilStatusRequestPrepaid objRequest = new Common.CivilStatusRequestPrepaid()
            {
                audit = audit
            };


            Common.CivilStatusResponsePrepaid objResponse = new Common.CivilStatusResponsePrepaid();
            List<ListItem> lista = new List<ListItem>();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oCommonService.GetCivilStatusList(objRequest);
                });

                if (objResponse.ListCivilStatus.Count > 0)
                {
                    foreach (var temp in objResponse.ListCivilStatus)
                    {
                        ListItem item = new ListItem();
                        item.Code = temp.Code;
                        item.Description = temp.Description;
                        lista.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objLista = lista });
        }

        public JsonResult GetNacType(string strIdSession)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            List<ListItem> lista = new List<ListItem>();

            Common.BrandResponsePrepaid1 oResponse = new Common.BrandResponsePrepaid1();
            Common.BrandRequestPrepaid1 objRequest = new Common.BrandRequestPrepaid1()
            {
                audit = audit
            };

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oCommonService.GetNationalityList(objRequest);
                });

                if (oResponse.ListConsultNationality.Count > 0)
                {
                    foreach (var item in oResponse.ListConsultNationality)
                    {
                        ListItem it = new ListItem();
                        it.Code = item.Code;
                        it.Description = item.Description;
                        lista.Add(it);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objLista = lista });
        }

        public JsonResult GetDepartments(string strIdSession)
        {
            List<HELPERS.CommonServices.GenericItem> listDepartments = new List<HELPERS.CommonServices.GenericItem>();
            var msg = string.Format("In GetDepartments - Postpago");
            Claro.Web.Logging.Info(strIdSession, strIdSession, msg);
            CommonTransacService.DepartmentsPvuResponseCommon objDepartmentsResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.DepartmentsPvuRequestCommon objDepartmentsRequestCommon = new CommonTransacService.DepartmentsPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objDepartmentsResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.DepartmentsPvuResponseCommon>(() => { return oCommonService.GetDepartmentsPVU(objDepartmentsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDepartmentsRequestCommon.audit.transaction, ex.Message);
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Info(strIdSession, strIdSession, ex.InnerException.Message);
            }

            if (objDepartmentsResponseCommon != null && objDepartmentsResponseCommon != null)
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, "objDepartmentsResponseCommon");

                foreach (CommonTransacService.ListItem item in objDepartmentsResponseCommon.ListDepartments)
                {
                    listDepartments.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return Json(new { data = listDepartments });
        }
        public JsonResult GetProvinces(string strIdSession, string strDepartments)
        {
            List<HELPERS.CommonServices.GenericItem> listProvinces = new List<HELPERS.CommonServices.GenericItem>();
            CommonTransacService.ProvincesPvuResponseCommon objProvincesResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.ProvincesPvuRequestCommon objProvincesRequestCommon = new CommonTransacService.ProvincesPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objProvincesRequestCommon.CodDep = strDepartments;
                objProvincesResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ProvincesPvuResponseCommon>(() => { return oCommonService.GetProvincesPVU(objProvincesRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProvincesRequestCommon.audit.transaction, ex.Message);

            }

            if (objProvincesResponseCommon != null && objProvincesResponseCommon != null)
            {


                foreach (CommonTransacService.ListItem item in objProvincesResponseCommon.ListProvinces)
                {
                    listProvinces.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return Json(new { data = listProvinces });
        }
        public JsonResult GetDistricts(string strIdSession, string strDepartments, string strProvinces)
        {
            List<HELPERS.CommonServices.GenericItem> listDistricts = new List<HELPERS.CommonServices.GenericItem>();
            CommonTransacService.DistrictsPvuResponseCommon objDistrictsResponseCommon = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.DistrictsPvuRequestCommon objDistrictsRequestCommon = new CommonTransacService.DistrictsPvuRequestCommon()
            {
                audit = audit
            };

            try
            {
                objDistrictsRequestCommon.CodDepart = strDepartments;
                objDistrictsRequestCommon.CodProv = strProvinces;
                objDistrictsResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonTransacService.DistrictsPvuResponseCommon>(
                    () =>
                    { return oCommonService.GetDistrictsPVU(objDistrictsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDistrictsRequestCommon.audit.transaction, ex.Message);
            }

            if (objDistrictsResponseCommon != null && objDistrictsResponseCommon != null)
            {


                foreach (CommonTransacService.ListItem item in objDistrictsResponseCommon.ListDistricts)
                {
                    listDistricts.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

            }

            return Json(new { data = listDistricts });
        }

        public JsonResult GetStateType(string strIdSession, string idList)
        {
            List<HELPERS.CommonServices.GenericItem> listStateType = new List<HELPERS.CommonServices.GenericItem>();
            List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem> GetDocumentType = null;

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            GetDocumentType = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem>>(() =>
            {
                return oCommonService.GetDocumentTypeCOBS(strIdSession, audit.transaction, idList);
            });

            if (GetDocumentType != null)
            {
                foreach (CommonTransacService.ListItem item in GetDocumentType)
                {
                    var codigo = "";
                    if (item.Description.Length > 3)
                    {
                        codigo = (item.Description.Substring(0, 3)).Trim();
                    }
                    else
                    {
                        codigo = item.Description;
                    }


                    listStateType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = codigo,// item.Code,
                        Description = item.Description
                    });
                }
            }
            return Json(new { data = listStateType });
        }

        public JsonResult GetZoneTypes(string strIdSession)
        {
            List<HELPERS.CommonServices.GenericItem> listZoneType = new List<HELPERS.CommonServices.GenericItem>();

            CommonTransacService.ZoneTypeCobsResponseCommon objZoneTypeResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.ZoneTypeCobsRequestCommon objZoneTypeCobsRequestCommon = new CommonTransacService.ZoneTypeCobsRequestCommon()
            {
                audit = audit
            };

            try
            {
                objZoneTypeResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ZoneTypeCobsResponseCommon>(() => { return oCommonService.GetZoneTypeCOBS(objZoneTypeCobsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objZoneTypeCobsRequestCommon.audit.transaction, ex.Message);
            }

            if (objZoneTypeResponse != null && objZoneTypeResponse != null)
            {


                foreach (CommonTransacService.ListItem item in objZoneTypeResponse.ListZoneType)
                {
                    listZoneType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return Json(new { data = listZoneType });
        }
        public JsonResult GetMzBloEdiType(string strIdSession)
        {
            List<HELPERS.CommonServices.GenericItem> ListMzBloEdiType = new List<HELPERS.CommonServices.GenericItem>();

            CommonTransacService.MzBloEdiTypeResponseCommon objMzBloEdiTypeResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.MzBloEdiTypeRequestCommon objMzBloEdiTypeRequest = new CommonTransacService.MzBloEdiTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objMzBloEdiTypeResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.MzBloEdiTypeResponseCommon>(() => { return oCommonService.GetMzBloEdiTypePVU(objMzBloEdiTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objMzBloEdiTypeRequest.audit.transaction, ex.Message);
            }

            if (objMzBloEdiTypeResponse != null && objMzBloEdiTypeResponse != null)
            {
                foreach (CommonTransacService.ListItem item in objMzBloEdiTypeResponse.ListMzBloEdiType)
                {
                    ListMzBloEdiType.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return Json(new { data = ListMzBloEdiType });
        }
        public JsonResult GetTipDptInt(string strIdSession)
        {
            List<HELPERS.CommonServices.GenericItem> ListTipDptInt = new List<HELPERS.CommonServices.GenericItem>();

            CommonTransacService.TipDptIntResponseCommon objTipDptIntResponse = null;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CommonTransacService.TipDptIntRequestCommon objTipDptIntRequest = new CommonTransacService.TipDptIntRequestCommon()
            {
                audit = audit
            };

            try
            {
                objTipDptIntResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.TipDptIntResponseCommon>(() => { return oCommonService.GetTipDptInt(objTipDptIntRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTipDptIntRequest.audit.transaction, ex.Message);
            }

            if (objTipDptIntResponse != null && objTipDptIntResponse != null)
            {
                foreach (CommonTransacService.ListItem item in objTipDptIntResponse.LisTipDptIntType)
                {
                    ListTipDptInt.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return Json(new { data = ListTipDptInt });
        }

        public JsonResult ObtenerCodigoPostal(string strIdSession, string vstrDisID)
        {

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var msg = string.Format("In ObtenerCodigoPostal - PostPaid - vstrDisID : {0}", vstrDisID);

            Claro.Web.Logging.Info(strIdSession, audit.transaction, msg);
            ;
            string strcodePostal = GetPostalCode(strIdSession, audit.transaction, vstrDisID);

            msg = string.Format("Out ObtenerCodigoPostal - PostPaid - strcodePostal : {0}", strcodePostal);
            Claro.Web.Logging.Info(strIdSession, audit.transaction, msg);
            return Json(new { data = strcodePostal });


        }
        public string GetPostalCode(string strSession, string strTransaction, string strIdDistrito)
        {
            string strCode = string.Empty;
            // List<HELPERS.CommonServices.GenericItem> listPostal = new List<HELPERS.CommonServices.GenericItem>();



            List<Claro.SIACU.Transac.Service.ItemGeneric> listaItem = Claro.SIACU.Transac.Service.Functions.GetListValuesXML("ListaCodigoPostal", "0", "HFCDatos.xml");

            Claro.SIACU.Transac.Service.ItemGeneric item = listaItem.Where(x => x.Code.Equals(strIdDistrito)).FirstOrDefault();

            if (item != null)
            {
                strCode = item.Description;
            }

            return strCode;
        }




        //public JsonResult GetAditionalData(string strSession, string strCustomer)
        //{
        //    CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strSession);
        //    Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.Client oCliente = new Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.Client();
        //    try
        //    {
        //        var ipCliente = App_Code.Common.GetClientIP();
        //        var ipServidor = App_Code.Common.GetClientIP();
        //        //var nomServClient = App_Code.Common.GetClientName();
        //        var ctaUsuClient = App_Code.Common.CurrentUser;
        //        var ApplicationName = ConfigurationManager.AppSettings("ApplicationName");

        //        oCliente = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
        //        {
        //            return oServicePostpaid.GetAditionalData(audit.transaction, ipServidor, ApplicationName, ctaUsuClient, strCustomer);
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
        //    }
        //    return Json(new { data = oCliente });
        //}




        //public string ActualizarDatosMenores(Model.Postpaid.ChangeDataModel oModel)
        //{
        //    string strResultado = "";
        //    bool flagResult = false;
        //    CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
        //    try
        //    {
        //        Client objRequest = new Client();

        //        objRequest.CUSTOMER_ID = oModel.strCustomerId;
        //        objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
        //        objRequest.NOMBRES = oModel.strNombres;
        //        objRequest.APELLIDOS = oModel.strApellidos;
        //        objRequest.DNI_RUC = oModel.DNI_RUC;
        //        objRequest.CARGO = oModel.strCargo;
        //        objRequest.TELEFONO = oModel.strTelefono;
        //        objRequest.TELEFONO_CONTACTO = oModel.strMovil;
        //        objRequest.FAX = oModel.strFax;
        //        objRequest.EMAIL = oModel.strMail;
        //        objRequest.NOMBRE_COMERCIAL = oModel.strNombreComercial;
        //        objRequest.CONTACTO_CLIENTE = oModel.strContactoCliente;
        //        objRequest.FECHA_NAC = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
        //        objRequest.NACIONALIDAD = Convert.ToInt64(oModel.strNacionalidad);
        //        objRequest.SEXO = oModel.strSexo;
        //        objRequest.ESTADO_CIVIL_ID = oModel.strEstadoCivilId;
        //        objRequest.REPRESENTANTE_LEGAL = oModel.RepresentLegal;
        //        objRequest.NRO_DOC = oModel.strDNI;


        //        strResultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
        //        {
        //            return oServicePostpaid.UpdateChangeData(objRequest, audit.Session, audit.transaction);
        //        });

        //        if (strResultado == "")
        //        {
        //            flagResult = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
        //    }
        //    return strResultado;
        //}

        public JsonResult UpdateNameCustomer(string strIdSession, Model.Postpaid.ChangeDataModel oModel)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado = "";
            int secuencia = 0;
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();
            try
            {
                Client objRequest = new Client();

                objRequest.CUSTOMER_ID = oModel.strCustomerId;
                objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
                objRequest.NOMBRES = oModel.strNombres;
                objRequest.APELLIDOS = oModel.strApellidos;
                objRequest.TIPO_DOC = oModel.strTipoDocumento == Claro.Constants.NumberZeroString ? Claro.Constants.NumberOneString : oModel.strTipoDocumento;
                objRequest.DNI_RUC = oModel.DNI_RUC;
                objRequest.MOTIVO_REGISTRO = oModel.strMotivo;


                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateNameCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
                });

                if (objResponse != null)
                {
                    resultado = objResponse.ResultCode;
                    secuencia = objResponse.SequenceCode;

                    if (resultado == "0")
                    {
                        resultado = UpdateDataCustomerPClub(strIdSession, oModel);
                    }
                }



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
                secuencia = 0;
            }
            return Json(new { result = resultado, seq = secuencia });
        }

        public JsonResult UpdateDataMinorCustomer(string strIdSession, Model.Postpaid.ChangeDataModel oModel)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado = "";
            bool retorno = false;
            int secuencia = 0;
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();

            try
            {
                Client objRequest = new Client();

                objRequest.CUSTOMER_ID = oModel.strCustomerId;
                objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
                objRequest.NOMBRES = oModel.strNombres;
                objRequest.APELLIDOS = oModel.strApellidos;
                objRequest.DNI_RUC = oModel.DNI_RUC;
                objRequest.CARGO = oModel.strCargo;
                objRequest.TELEFONO = oModel.strTelefono;
                objRequest.TELEFONO_CONTACTO = oModel.strMovil;
                objRequest.FAX = oModel.strFax;
                objRequest.EMAIL = oModel.strMail;
                objRequest.NOMBRE_COMERCIAL = oModel.strNombreComercial;
                objRequest.CONTACTO_CLIENTE = oModel.strContactoCliente;
                objRequest.FECHA_NAC = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
                objRequest.NACIONALIDAD = Convert.ToInt64(oModel.strNacionalidad);
                objRequest.SEXO = oModel.strSexo;
                objRequest.ESTADO_CIVIL_ID = oModel.strEstadoCivilId;
                objRequest.REPRESENTANTE_LEGAL = oModel.RepresentLegal;
                objRequest.NRO_DOC = oModel.strDNI;
                objRequest.TELEFONO_REFERENCIA_1 = oModel.strPhone;
                objRequest.MOTIVO_REGISTRO = oModel.strMotivo;


                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateDataMinorCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest, oModel.intSeqIn);
                });


                if (objResponse != null)
                {
                    resultado = objResponse.ResultCode;
                    secuencia = objResponse.SequenceCode;

                    if (resultado == "0")
                    {
                        resultado = UpdateDataCustomerCLF(strIdSession, objRequest);
                    }
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
                secuencia = 0;
            }
            return Json(new { result = resultado, seq = secuencia });
        }


        public string UpdateDataCustomerCLF(string strIdSession, Client objRequest)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado = "";
            try
            {

                string strFlgRegistrado = "1";
                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit2 = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                {
                    audit = audit2,
                    vPhone = objRequest.TELEFONO_REFERENCIA_1,
                    vAccount = string.Empty,
                    vContactobjid1 = string.Empty,
                    vFlagReg = strFlgRegistrado
                };
                objCustomerResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCustomer(objGetCustomerRequest);
                });

                if (objCustomerResponse.Customer != null)
                {
                    objRequest.OBJID_CONTACTO = objCustomerResponse.Customer.ContactCode;
                }

                resultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateDataCustomerCLF(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
            }
            return resultado;
        }

        public string UpdateDataCustomerPClub(string strIdSession, Model.Postpaid.ChangeDataModel oModel)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado = "";
            int intTipoCliente = 0;
            //bool retorno = false;
            try
            {
                Client objRequest = new Client();

                if (oModel.strFlagPlataforma == "C")
                {
                    intTipoCliente = 1;
                }
                else
                {
                    if (oModel.strTipoCliente.ToUpper() == "CUSTOMER")
                    {
                        intTipoCliente = 2;
                    }

                }

                objRequest.TIPO_CLIENTE = intTipoCliente.ToString();
                objRequest.CUENTA = oModel.strCuenta;
                objRequest.NRO_DOC = oModel.DNI_RUC;
                objRequest.TIPO_DOC = oModel.strTipoDocumento;

                if (oModel.DNI_RUC.Length == 11)
                {
                    var valor = oModel.DNI_RUC.Substring(0, 2);
                    if (valor == "20")
                    {
                        objRequest.NOMBRES = oModel.strRazonSocial;
                        objRequest.APELLIDOS = oModel.strRazonSocial;
                    }
                    if (valor == "10")
                    {
                        objRequest.NOMBRES = oModel.strNombres;
                        objRequest.APELLIDOS = oModel.strApellidos;
                    }
                }
                else
                {
                    objRequest.NOMBRES = oModel.strNombres;
                    objRequest.APELLIDOS = oModel.strApellidos;
                }
                objRequest.USUARIO = audit.userName;



                resultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateDataCustomerPClub(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
                });




            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
            }
            return resultado;
        }

        public JsonResult UpdateAddressCustomer(Model.Postpaid.AddressCustomerModel oModel)
        {

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strSessionId);
            string resultado = "";
            int secuencia = 0;
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();

            try
            {
                Client oCliente = new Client();
                oCliente.CUSTOMER_ID = oModel.strCustomerId;
                oCliente.POSTAL_LEGAL = oModel.strCodPostal;
                oCliente.DEPARTEMENTO_LEGAL = oModel.strDepartamento;
                oCliente.CALLE_LEGAL = oModel.strDireccion;
                oCliente.DISTRITO_LEGAL = oModel.strDistrito;
                oCliente.REFERENCIA = oModel.strReferencia;
                oCliente.PAIS_LEGAL = oModel.strPais;
                oCliente.PROVINCIA_LEGAL = oModel.strProvincia;
                oCliente.MOTIVO_REGISTRO = oModel.strMotivo;

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateAddressCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, oCliente, oModel.strTipo, oModel.intSeqIn);
                });

                if (objResponse != null)
                {
                    resultado = objResponse.ResultCode;
                    secuencia = objResponse.SequenceCode;

                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
            }
            return Json(new { result = resultado, seq = secuencia });
        }

        public List<string> SaveInteraction(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.strPhone;
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = new Model.InteractionModel();

            Common.AuditRequest audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(oModel.strIdSession);
            Common.InsertGeneralResponse oSave = new Common.InsertGeneralResponse();

            bool blnExecuteTransaction = true;
            string strUserSystem = oModel.strCodAgente;
            string strUserApp = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strUserPass = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            try
            {
                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : GrabaInteraccion");

                oInteraccion = DatosInteraccion(oModel);

                oPlantillaDat = GetDataTemplateInteraction(oModel, oModelD, oModelF, DataOld, FlagD, FlagF);


                var resultInteraction = InsertInteraction(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.strIdSession, oModel.strCustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {
                    Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                    throw new Exception(Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Fín Método : GrabaInteraccion FlagMessage : " + lstaDatTemplate[0]);
            return lstaDatTemplate;
        }

        public Model.InteractionModel DatosInteraccion(Model.Postpaid.ChangeDataModel oModel)
        {
            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                oInteraccion.ObjidContacto = oModel.strObjidContacto;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.strPhone;
                oInteraccion.Type = oModel.tipo;
                oInteraccion.TypeCode = oModel.tipoCode;
                oInteraccion.Class = oModel.claseDes;
                oInteraccion.ClassCode = oModel.claseCode;
                oInteraccion.SubClass = oModel.subClaseDes;
                oInteraccion.SubClassCode = oModel.subClaseCode;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.Constants.NumberZeroString;
                oInteraccion.Note = oModel.strNote;
                oInteraccion.FlagCase = "0";
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.strCodAgente;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Fín Método : DatosInteraccion");

            return oInteraccion;

        }

        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            var objTemplateInteraction = new Model.TemplateInteractionModel();

            Common.AuditRequest audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(oModel.strIdSession);

            try
            {
                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : GetDataTemplateInteraction");

                objTemplateInteraction.X_CITY = ConfigurationManager.AppSettings("strNombreTransaccionCambioDatos"); //Nombre de la transacción
                objTemplateInteraction.X_INTER_18 = DateTime.Now.ToShortDateString(); //Fecha de atención
                objTemplateInteraction.X_REASON = oModel.strMotivo; //Motivo de Cambio
                objTemplateInteraction.X_CLARO_NUMBER = oModel.strPhone;
                objTemplateInteraction.X_BASKET = DataOld.Cliente.CONTRATO_ID;
                objTemplateInteraction.TIENE_DATOS = "S";

                if (ValidateChangeIdPer(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_8 = 1;

                    if (oModel.strTipoDocumento == "RUC")
                    {
                        if (oModel.DNI_RUC.Substring(0, 1) == "2")
                        {
                            if (oModel.strRazonSocial != DataOld.Cliente.RAZON_SOCIAL)
                            {
                                objTemplateInteraction.X_OLD_CLARO_LDN1 = DataOld.Cliente.RAZON_SOCIAL;
                                objTemplateInteraction.X_CLARO_LDN1 = oModel.strRazonSocial;
                            }
                            if (oModel.strNombreComercial != DataOld.Cliente.NOMBRE_COMERCIAL)
                            {
                                objTemplateInteraction.X_OLD_CLARO_LDN2 = DataOld.Cliente.NOMBRE_COMERCIAL;
                                objTemplateInteraction.X_CLARO_LDN2 = oModel.strNombreComercial;
                            }
                        }
                        else
                        {
                            if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                            {
                                objTemplateInteraction.X_OLD_FIRST_NAME = DataOld.Cliente.NOMBRES;
                                objTemplateInteraction.X_FIRST_NAME = oModel.strNombres;
                            }
                            if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                            {
                                objTemplateInteraction.X_OLD_LAST_NAME = DataOld.Cliente.APELLIDOS;
                                objTemplateInteraction.X_LAST_NAME = oModel.strApellidos;
                            }
                        }

                    }
                    else
                    {
                        if (oModel.strNombres != DataOld.Cliente.NOMBRES)
                        {
                            objTemplateInteraction.X_OLD_FIRST_NAME = DataOld.Cliente.NOMBRES;
                            objTemplateInteraction.X_FIRST_NAME = oModel.strNombres;
                        }
                        if (oModel.strApellidos != DataOld.Cliente.APELLIDOS)
                        {
                            objTemplateInteraction.X_OLD_LAST_NAME = DataOld.Cliente.APELLIDOS;
                            objTemplateInteraction.X_LAST_NAME = oModel.strApellidos;
                        }
                    }

                    if (oModel.strTxtTipoDocumento != DataOld.Cliente.TIPO_DOC)
                    {
                        objTemplateInteraction.X_OLD_CLARO_LDN3 = DataOld.Cliente.TIPO_DOC;
                        objTemplateInteraction.X_CLARO_LDN3 = oModel.strTxtTipoDocumento;
                    }

                    if (oModel.DNI_RUC != DataOld.Cliente.NRO_DOC)
                    {
                        objTemplateInteraction.X_OLD_DOC_NUMBER = DataOld.Cliente.NRO_DOC;
                        objTemplateInteraction.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                    }

                }
                else { objTemplateInteraction.X_INTER_8 = 0; }


                if (ValidateChangeContC(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_9 = 1;

                    if (oModel.strMovil != DataOld.Cliente.TELEFONO_CONTACTO)
                    {
                        objTemplateInteraction.X_OLD_CLAROLOCAL1 = DataOld.Cliente.TELEFONO_CONTACTO;
                        objTemplateInteraction.X_CLAROLOCAL1 = oModel.strMovil;
                    }
                    if (oModel.strTelefono != DataOld.Cliente.TELEF_REFERENCIA)
                    {
                        objTemplateInteraction.X_REFERENCE_PHONE = DataOld.Cliente.TELEF_REFERENCIA;
                        objTemplateInteraction.X_CLARO_NUMBER = oModel.strTelefono;
                    }
                    if (oModel.strMail != DataOld.Cliente.EMAIL)
                    {
                        objTemplateInteraction.X_OLD_CLAROLOCAL2 = DataOld.Cliente.EMAIL;
                        objTemplateInteraction.X_CLAROLOCAL2 = oModel.strMail;
                    }
                }
                else { objTemplateInteraction.X_INTER_9 = 0; }

                if (ValidateChangeRL(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_10 = 1;

                    if (oModel.RepresentLegal != DataOld.Cliente.REPRESENTANTE_LEGAL)
                    {
                        objTemplateInteraction.X_INTER_6 = DataOld.Cliente.REPRESENTANTE_LEGAL;
                        objTemplateInteraction.X_NAME_LEGAL_REP = oModel.RepresentLegal;
                    }
                    if (oModel.strDNI != DataOld.Cliente.NRO_DOC)
                    {
                        objTemplateInteraction.X_INTER_7 = DataOld.Cliente.NRO_DOC;
                        objTemplateInteraction.X_DNI_LEGAL_REP = oModel.strDNI;
                    }
                }
                else { objTemplateInteraction.X_INTER_10 = 0; }

                if (ValidateChangeCC(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_11 = 1;
                    objTemplateInteraction.X_INTER_2 = DataOld.Cliente.CONTACTO_CLIENTE;
                    objTemplateInteraction.X_INTER_3 = oModel.strContactoCliente;
                }
                else { objTemplateInteraction.X_INTER_11 = 0; }

                if (FlagD == 1)
                {
                    if (ValidateChangeAddressLegal(oModelD, DataOld))
                    {

                        objTemplateInteraction.X_INTER_12 = 1;

                        objTemplateInteraction.X_ADDRESS5 = DataOld.Cliente.CALLE_LEGAL;
                        objTemplateInteraction.X_ADDRESS = oModelD.strDireccion;

                        objTemplateInteraction.X_INTER_15 = DataOld.Cliente.URBANIZACION_LEGAL;
                        objTemplateInteraction.X_REFERENCE_ADDRESS = oModelD.strReferencia;

                        objTemplateInteraction.X_INTER_21 = DataOld.Cliente.DEPARTEMENTO_LEGAL;
                        objTemplateInteraction.X_DEPARTMENT = oModelD.strDepartamento;

                        objTemplateInteraction.X_INTER_4 = DataOld.Cliente.PROVINCIA_LEGAL;
                        objTemplateInteraction.X_INTER_5 = oModelD.strProvincia;

                        objTemplateInteraction.X_INTER_29 = DataOld.Cliente.DISTRITO_LEGAL;
                        objTemplateInteraction.X_DISTRICT = oModelD.strDistrito;

                    }
                    else
                    {
                        objTemplateInteraction.X_INTER_12 = 0;
                    }
                }
                else { objTemplateInteraction.X_INTER_12 = 0; }


                if (FlagF == 1)
                {
                    if (ValidateChangeAddressFac(oModelF, DataOld))
                    {
                        objTemplateInteraction.X_INTER_13 = 1;

                        objTemplateInteraction.X_OLD_CLAROLOCAL3 = DataOld.Cliente.CALLE_FAC;
                        objTemplateInteraction.X_CLAROLOCAL3 = oModelF.strDireccion;

                        objTemplateInteraction.X_OLD_CLAROLOCAL4 = DataOld.Cliente.URBANIZACION_FAC;
                        objTemplateInteraction.X_CLAROLOCAL4 = oModelF.strReferencia;

                        objTemplateInteraction.X_OLD_CLAROLOCAL5 = DataOld.Cliente.DEPARTEMENTO_FAC;
                        objTemplateInteraction.X_CLAROLOCAL5 = oModelF.strDepartamento;

                        objTemplateInteraction.X_OLD_CLAROLOCAL6 = DataOld.Cliente.PROVINCIA_FAC;
                        objTemplateInteraction.X_CLAROLOCAL6 = oModelF.strProvincia;

                        objTemplateInteraction.X_OLD_CLARO_LDN4 = DataOld.Cliente.DISTRITO_FAC;
                        objTemplateInteraction.X_INTER_1 = oModelF.strDistrito;

                    }
                    else
                    {
                        objTemplateInteraction.X_INTER_13 = 0;
                    }
                }
                else
                {
                    objTemplateInteraction.X_INTER_13 = 0;
                }

                if (oModel.Flag_Email == true)
                {
                    objTemplateInteraction.X_FLAG_OTHER = Claro.Constants.LetterS;
                    objTemplateInteraction.X_EMAIL = oModel.strEmailSend;
                }
                else
                {
                    objTemplateInteraction.X_FLAG_OTHER = Claro.Constants.LetterN;
                }


                objTemplateInteraction.X_INTER_17 = oModel.strCacDac;


                objTemplateInteraction.X_INTER_19 = oModel.strCodAgente;
                objTemplateInteraction.X_INTER_20 = oModel.strNombAgente;
                objTemplateInteraction.X_INTER_30 = oModel.strNote;


                objTemplateInteraction.X_FLAG_REGISTERED = "1";
                objTemplateInteraction.X_CHARGE_AMOUNT = 0;
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Fín Método : GetDataTemplateInteraction");
            return objTemplateInteraction;

        }



    }
}