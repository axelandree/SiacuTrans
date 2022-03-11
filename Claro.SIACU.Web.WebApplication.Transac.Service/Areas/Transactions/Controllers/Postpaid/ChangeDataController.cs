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

using Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService;

using Claro.SIACU.ProxyService.Transac.Service.CambioDatosSiacWS;
using System.Reflection;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class ChangeDataController : CommonServicesController
    {
        private readonly Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.PostTransacServiceClient oServicePostpaid = new Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oCommonService = new CommonTransacService.CommonTransacServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.FixedTransacServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.FixedTransacServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.ColivingTransacServiceClient oServiceColiving = new Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.ColivingTransacServiceClient();
        //
        // GET: /Transactions/ChangeData/
        public ActionResult ChangeData()
        {
            ViewBag.LengthPhone = KEY.AppSettings("gConstkeyLengthPhone");
            return View();
        }
        CommonTransacService.AuditRequest audit = new Common.AuditRequest();
        public JsonResult PageLoad(string strIdSession)
        {
            audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
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

        public JsonResult GetParticipante(string strTipoDocumento, string strNumDocumento, string strIdSession)
        {
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "GetParticipante entro!!!!!!!!!!!!");
            bool existe = false;
            RequestCUParticipante objRequest = new RequestCUParticipante()
            {
                numeroDocumento = strNumDocumento,
                tipoDocumento = strTipoDocumento
            };

            Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.HeaderToBe objHeader = new Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.HeaderToBe()
            {
                IdTransaccion = audit.transaction,
                IpAplicacion = audit.ipAddress,
                UserId = audit.userName,
                MsgId = audit.userName
            };
            Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("objRequest=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest)));
            ResponseCUParticipante objResponse = Claro.Web.Logging.ExecuteMethod(() => { return oServiceColiving.GetCuParticipante(objRequest, objHeader); });

            Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("objPresponse=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objResponse)));
            if (objResponse.participante != null)
            {
                existe = true;
            }
            return Json(new { data = objResponse, Existente = existe });
        }
        public JsonResult RegistarInteracion(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string plataformaAT, string flagconvivencia, Model.Postpaid.ChangeDataModel oModelIni)
        {
            Claro.Web.Logging.Info("RegistarInteracion", "RegistarInteracion", string.Format("RegistarInteracion=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(oModel)));
            string vDesInteraction = string.Empty;
            string strRutaArchivo = string.Empty;
            string vInteractionId = string.Empty;
            string strNombreArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(oModel.strIdSession);
            string _strResult = "0";
            string strResultTransCustomer = string.Empty;
            //cmendere init
            if (plataformaAT == "TOBE")
            {
                if (DataOld.Cliente.EMAIL == null) DataOld.Cliente.EMAIL = string.Empty;
                if (DataOld.Cliente.TELEF_REFERENCIA == null) DataOld.Cliente.TELEF_REFERENCIA = string.Empty;
                if (DataOld.Cliente.TELEFONO_CONTACTO == null) DataOld.Cliente.TELEFONO_CONTACTO = string.Empty;
                repLegAntiguo = DataOld.Cliente.REPRESENTANTE_LEGAL;

                List<Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal> olistaReprLegal = null;
                List<Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal> oldListRepLegal = null;
                if (oModel.listaRL != null)
                {
                    olistaReprLegal = JsonConvert.DeserializeObject<List<Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal>>(oModel.listaRL);
                    oldListRepLegal = JsonConvert.DeserializeObject<List<Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal>>(oModel.listaRLOLD);

                    var repCurrentUpdated = olistaReprLegal.LastOrDefault(rl => rl.cuRepAction == "M");                    
                    var TipDocRepCurrent = string.Empty;
                    if (repCurrentUpdated != null)
                    {

                        switch (repCurrentUpdated.cuReptipdoc) { 
                            case "4":
                                TipDocRepCurrent = "Carnet de Extranjería";
                                break;
                            case "6":
                                TipDocRepCurrent = "RUC";
                                break;
                            case "1":
                                TipDocRepCurrent = "Pasaporte";
                                break;
                            case "2":
                                TipDocRepCurrent = "DNI";
                                break;
                            case "10":
                                TipDocRepCurrent = "CTM";
                                break;
                            case "9":
                                TipDocRepCurrent = "CPP";
                                break;
                            case "8":
                                TipDocRepCurrent = "CIRE";
                                break;
                            case "7":
                                TipDocRepCurrent = "CIE";
                                break;
                        }

                        oModel.RepresentLegal = repCurrentUpdated.cuRepnombres + " " + repCurrentUpdated.cuRepapepat + " " + repCurrentUpdated.cuRepapemat;
                        oModel.strTxtTipoDocumentoRL = TipDocRepCurrent;
                        oModel.strDNI = repCurrentUpdated.cuRepnumdoc;
                        
                        var repOld = oldListRepLegal.LastOrDefault(rl => rl.idCurep == repCurrentUpdated.idCurep);
                        var TipDocRepOld = string.Empty;
                        if (TipDocRepOld != null)
                        {
                            switch (repOld.cuReptipdoc)
                            {
                                case "4":
                                    TipDocRepOld = "Carnet de Extranjería";
                                    break;
                                case "6":
                                    TipDocRepOld = "RUC";
                                    break;
                                case "1":
                                    TipDocRepOld = "Pasaporte";
                                    break;
                                case "2":
                                    TipDocRepOld = "DNI";
                                    break;
                                case "10":
                                    TipDocRepOld = "CTM";
                                    break;
                                case "9":
                                    TipDocRepOld = "CPP";
                                    break;
                                case "8":
                                    TipDocRepOld = "CIRE";
                                    break;
                                case "7":
                                    TipDocRepOld = "CIE";
                                    break;
                            }
                            DataOld.Cliente.REPRESENTANTE_LEGAL = repOld.cuRepnombres + " " + repOld.cuRepapepat + " " + repOld.cuRepapemat;
                            DataOld.Cliente.TIPO_DOC_RL = TipDocRepOld;
                            DataOld.Cliente.NRO_DOC = repOld.cuRepnumdoc;
                        }
                    }                         
                oModel.listaRepresentanteLegal = olistaReprLegal;
                }
                
                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "UpdateDatosClienteMigrado ");
                _strResult = UpdateDatosClienteMigrado(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, oModel.strIdSession, flagconvivencia, oModelIni);
                
                #region registrarInteraccion
                try
                {
                    if (_strResult == "0")
                    {
                        strResultTransCustomer = "";
                        Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : RegistrarInteracion");
                        DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
                        DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();

                        List<string> strInteractionId = SaveInteraction(ref oModel, oModelD, oModelF, DataOld, FlagD, FlagF);
                        vInteractionId = strInteractionId[3].ToString();
                        if (strInteractionId[0] == Claro.SIACU.Constants.OK)
                        {
                            try
                            {
                                vDesInteraction = "La transacción se realizó con éxito.";
                                #region CONSTANCY PDF

                                Dictionary<string, object> oConstancyPDF = new Dictionary<string, object>();
                                Claro.Web.Logging.Info(oModel.strIdSession, "Yulino GetConstancyPDF", "ERROR: ");

                                oConstancyPDF = GetConstancyPDF(oModel.strIdSession, vInteractionId, oModel, oModelD, oModelF, DataOld, FlagD, FlagF);
                                strRutaArchivo = oModel.strFullPathPDF;
                                #endregion

                                if (oModel.Flag_Email)
                                {

                                    #region SEND EMAIL
                                    byte[] attachFile = null;
                                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                                    if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile))
                                        MensajeEmail = GetSendEmail(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
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
                    else if (_strResult == "-3")
                    {
                        strResultTransCustomer = "El documento " + oModel.DNI_RUC + " Ya se encuentra registrado";
                        return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo, strResultTransCustomer });
                    }
                    else
                    {
                        strResultTransCustomer = "Ocurrió un error al tratar de actualizar la información";
                        return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo, strResultTransCustomer });
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                }

                #endregion


            }//cmendere end
            else
            {
                #region asis
            try
            {
                if (oModel.FLAG_CUSTOMER ==  "CUSTOMER")
	            {
                    Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Inicio invocación método UpdateNameCustomer");
		             _strResult = UpdateNameCustomer(oModel.strIdSession,oModel,DataOld.Cliente.TIPO_DOC,DataOld.Cliente.DNI_RUC);
                     Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "UpdateNameCustomer _strResult: " + _strResult);
                     Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Fin invocación método UpdateNameCustomer");
	            }

                if (_strResult == "0")
	            {
		             if (oModel.FLAG_DATA_MINOR == "DATA_MINOR")
	                    {
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Inicio invocación método UpdateDataMinorCustomer");
                            _strResult = UpdateDataMinorCustomer(oModel.strIdSession, oModel, DataOld.Cliente.TIPO_DOC, oModel.DNI_RUC);
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "UpdateDataMinorCustomer _strResult: " + _strResult);
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Fin invocación método UpdateDataMinorCustomer");
	                    }
	            }
                if (_strResult == "0")
	            {
		             if (oModelD.FLAG_LEGAL == "LEGAL")
	                {
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Inicio invocación método UpdateAddressCustomer para legal");
                        _strResult = UpdateAddressCustomer(oModelD, DataOld.Cliente.TIPO_DOC, oModel.DNI_RUC);
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "UpdateAddressCustomer Legal _strResult: " + _strResult);
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Fin invocación método UpdateAddressCustomer para legal");
	                }
	            }

                if (_strResult == "0")
	            {
		             if (oModelF.FLAG_FACTURACION == "FACTURACION")
	                {
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Inicio invocación método UpdateAddressCustomer para facturacion");
                        _strResult = UpdateAddressCustomer(oModelF, DataOld.Cliente.TIPO_DOC, DataOld.Cliente.DNI_RUC);
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "UpdateAddressCustomer Facturacion _rESULTADO: " + _strResult);
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "Fin invocación método UpdateAddressCustomer para facturacion");
	                }
	            }

                if (_strResult == "0")
                {
                    strResultTransCustomer = "";
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : RegistrarInteracion");
                    DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
                    DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();

                    List<string> strInteractionId = SaveInteraction(ref oModel, oModelD, oModelF, DataOld, FlagD, FlagF);
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
                                    MensajeEmail = GetSendEmail(vInteractionId, strAdjunto, oModel, strNombreArchivo, attachFile, strAdjunto);
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
                else if (_strResult == "-3")
                {
                    strResultTransCustomer = "El documento " + oModel.DNI_RUC + " Ya se encuentra registrado";
                    return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo, strResultTransCustomer });
                }
                else 
                {
                    strResultTransCustomer = "Ocurrió un error al tratar de actualizar la información";
                    return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo, strResultTransCustomer });
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
                #endregion
            }
            return Json(new { data = oModel, vInteractionId, vDesInteraction, strRutaArchivo, strResultTransCustomer });
        }

        private string UpdateDatosClienteMigrado(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession, string flagconvivencia, Model.Postpaid.ChangeDataModel oModelIni)
        {
            string oResponse = "-1";
            try
            {
            string srtUser = string.Empty;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            DataClientRequest oClientrequest = new DataClientRequest();
            oClientrequest.IdTransaccion = audit.transaction;
            oClientrequest.IpAplicacion = ConfigurationManager.AppSettings("ApplicationName");
            oClientrequest.MsgId = oModel.strPhone;
            oClientrequest.TimesTamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            oClientrequest.UserId = oModelD.userId;
            oClientrequest.Channel = ConfigurationManager.AppSettings("ApplicationName");

            oModel.FECHA_ACT = Convert.ToString(DateTime.Now.ToString("s"));
            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "UpdateDatosClienteMigrado ");

            ColivingTransacService.AuditRequest obj = new ColivingTransacService.AuditRequest();
            obj.idTransaccion = oClientrequest.IdTransaccion;
            obj.idAplicacion = oClientrequest.IpAplicacion;
            obj.nombreAplicacion = oClientrequest.IpAplicacion;
            obj.usuarioAplicacion = oClientrequest.UserId;

            oClientrequest.GetDataClientRequest = null;
            oClientrequest.HistoryClientRequest = null;
            oClientrequest.HistoryClientDataRequest = null;
            oClientrequest.ListRepresentanteLegal = null;

            //vtorremo - INICIO
            //CUSTOMER
            string customerId = oModel.strCustomerId;
            Model.Postpaid.ChangeDataModel oModels = new Model.Postpaid.ChangeDataModel();
            Model.Postpaid.AddressCustomerModel oModelsF = new Model.Postpaid.AddressCustomerModel();
            Model.Postpaid.AddressCustomerModel oModelsD = new Model.Postpaid.AddressCustomerModel();
            oModels.strCustomerId = customerId;
            oModels.FLAG_CUSTOMER = "CUSTOMER";
            oModels.strMotivo = "Registro";
            oModels.strRazonSocial = DataOld.Cliente.RAZON_SOCIAL;
            oModels.strNombres = DataOld.Cliente.NOMBRE_COMERCIAL;
            oModels.strApellidos = DataOld.Cliente.APELLIDOS;
            oModels.strTipoDocumento = DataOld.Cliente.TIPO_DOC;
            oModels.DNI_RUC = DataOld.Cliente.NRO_DOC;
            oModels.FECHA_ACT = DataOld.Cliente.FECHA_ACT.ToString("s");
            oModelIni.FECHA_ACT = DataOld.Cliente.FECHA_ACT.ToString("s");
            oModelIni.strMotivo = oModels.strMotivo;
            oClientrequest.HistoryClientRequest = HistoryClientInicial(oModelIni, DataOld.Cliente.DNI_RUC);
            oClientrequest.HistoryClientRequest.HistoryClient.auditRequest = obj;

            //DATA_MINOR
            oModels.strCargo = DataOld.Cliente.CARGO;
            oModels.strMovil = DataOld.Cliente.TELEFONO_CONTACTO;   //phone contact (1)
                if (string.IsNullOrEmpty(DataOld.Cliente.FECHA_NAC))
                {
                    DataOld.Cliente.FECHA_NAC = DateTime.MinValue.ToShortTimeString();
                }
            oModels.strFax = DataOld.Cliente.FAX;
            oModels.strMail = DataOld.Cliente.EMAIL;
            oModels.strNombreComercial = DataOld.Cliente.NOMBRE_COMERCIAL;
            oModels.strContactoCliente = DataOld.Cliente.CONTACTO_CLIENTE;
            oModels.dateFechaNacimiento = DateTime.Parse(DataOld.Cliente.FECHA_NAC);
            oModels.strNacionalidad = string.IsNullOrEmpty(DataOld.Cliente.LUGAR_NACIMIENTO_DES) ? "" : DataOld.Cliente.LUGAR_NACIMIENTO_DES;
            oModels.strSexo = DataOld.Cliente.SEXO;
            if (DataOld.Cliente.REPRESENTANTE_LEGAL != null && DataOld.Cliente.REPRESENTANTE_LEGAL.Length > 20)
            {
                oModels.RepresentLegal = DataOld.Cliente.REPRESENTANTE_LEGAL.Substring(0, 20);
            }
            else
            {
                oModels.RepresentLegal = DataOld.Cliente.REPRESENTANTE_LEGAL;
            }
            oModels.strDNI = DataOld.Cliente.DNI_RUC;
            oModels.strTelefono = DataOld.Cliente.TELEFONO_REFERENCIA_1; //phonereference (2)
            oModels.FLAG_CUSTOMER = "DATA_MINOR";
            oClientrequest.HistoryClientDataRequest = HistoryClientData(oModels, oModelsD, oModelsF, DataOld, FlagD, FlagF, strIdSession, DataOld.Cliente.DNI_RUC,"Registro");
            oClientrequest.HistoryClientDataRequest.HistoryClient.auditRequest = obj;


            //FACTURACION


            oModelsF.strCustomerId = customerId;
            oModelsF.strCodPostal = DataOld.Cliente.ZIPCODE;
            oModelsF.strPais = DataOld.Cliente.PAIS_FAC;
            oModelsF.strDireccion = DataOld.Cliente.CALLE_FAC;
            oModelsF.strReferencia = DataOld.Cliente.URBANIZACION_FAC;
            oModelsF.strDistrito = DataOld.Cliente.DISTRITO_FAC;
            oModelsF.strProvincia = DataOld.Cliente.PROVINCIA_FAC;
            oModelsF.strDepartamento = DataOld.Cliente.DEPARTEMENTO_FAC;
            oModels.FLAG_CUSTOMER = "FACTURACION";
            oClientrequest.HistoryClientFactRequest = HistoryClientFact(oModels, oModelsD, oModelsF, DataOld, FlagD, FlagF, strIdSession, DataOld.Cliente.DNI_RUC, "Registro");
            oClientrequest.HistoryClientFactRequest.HistoryClient.auditRequest = obj;


            //LEGAL

            oModelsD.strCustomerId = customerId;
            oModelsD.strPais = string.IsNullOrEmpty(DataOld.Cliente.PAIS_LEGAL) ? "" : DataOld.Cliente.PAIS_LEGAL;
            oModelsD.strDepartamento = DataOld.Cliente.DEPARTEMENTO_LEGAL;
            oModelsD.strProvincia = DataOld.Cliente.PROVINCIA_LEGAL;
            oModelsD.strDistrito = DataOld.Cliente.DISTRITO_LEGAL;
            oModelsD.strStreet = DataOld.Cliente.DOMICILIO;
            oModelsD.strReferencia = DataOld.Cliente.REFERENCIA;
            oModelsD.strDireccion = DataOld.Cliente.CALLE_LEGAL;
            oModels.FLAG_CUSTOMER = "LEGAL";
            oModels.direccionReferenciaLegal = DataOld.Cliente.URBANIZACION_LEGAL;
            oClientrequest.HistoryClientLegalRequest = HistoryClientLegal(oModels, oModelsD, oModelsF, DataOld, FlagD, FlagF, strIdSession, DataOld.Cliente.DNI_RUC, "Registro");
            oClientrequest.HistoryClientLegalRequest.HistoryClient.auditRequest = obj;

            string NumDocCustomerReq = DataOld.Cliente.DNI_RUC != oModel.DNIRUCNew ? oModel.DNIRUCNew : DataOld.Cliente.DNI_RUC;
            string NumDocCustomer = oModel.strCustomerId + "|" + NumDocCustomerReq;
            cantHist = ValidateHistoryDataTobe(strIdSession, NumDocCustomer, flagconvivencia);

            if ((oModel.FLAG_CUSTOMER != "" || oModel.FLAG_DATA_MINOR != "" || oModelF.FLAG_FACTURACION != "" || oModelD.FLAG_LEGAL != ""))
            {
                if (DataOld.Cliente.DNI_RUC == oModel.DNIRUCNew) //ULTIMO_MODIFICADO
                {
                    //int historicoCount = ValidateHistoryDataTobe(strIdSession, NumDocCustomer, flagconvivencia);
                    if (cantHist == 0)
                    {
                        oServiceColiving.PostDataHistoryClientResponse(oClientrequest, oClientrequest.HistoryClientRequest.HistoryClient, strIdSession, audit.transaction);
                        Claro.Web.Logging.Info(strIdSession, audit.transaction, "Registro Primer histórico cliente, datos del cliente");
                        oServiceColiving.PostDataHistoryClientResponse(oClientrequest, oClientrequest.HistoryClientDataRequest.HistoryClient, strIdSession, audit.transaction);
                        Claro.Web.Logging.Info(strIdSession, audit.transaction, "Registro Primer histórico cliente, datos de contacto");
                        oServiceColiving.PostDataHistoryClientResponse(oClientrequest, oClientrequest.HistoryClientFactRequest.HistoryClient, strIdSession, audit.transaction);
                        Claro.Web.Logging.Info(strIdSession, audit.transaction, "Registro Primer histórico cliente, dirección de facturación");
                        oServiceColiving.PostDataHistoryClientResponse(oClientrequest, oClientrequest.HistoryClientLegalRequest.HistoryClient, strIdSession, audit.transaction);
                        Claro.Web.Logging.Info(strIdSession, audit.transaction, "Registro Primer histórico cliente, dirección legal ");

                        //REPRESENTANTE LEGAL
                        if ((oModel.DNI_RUC.Length == 11 && oModel.DNI_RUC.Substring(0, 2) == "20"))
                        {
                            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Inicio registro histórico de RRLL");
                            if (oModel.rrllHistorico != null)
                            {
                                var listRRLL = HistoryClientDataRRLL(oModel, obj, DataOld.Cliente.FECHA_ACT.ToString("s"), DataOld.Cliente.DNI_RUC);
                                listRRLL.ForEach((value) =>
                                {
                                    oServiceColiving.PostDataHistoryClientResponse(oClientrequest, value.HistoryClient, strIdSession, audit.transaction);
                                });
                                Claro.Web.Logging.Info(strIdSession, audit.transaction, "Fin registro histórico de RRLL");                                            
                            }
                        }
                    }                
                }                
            }

            //vtorremo - FIN

            oClientrequest = new DataClientRequest();
            Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("FLAG_CUSTOMER: {0} - FLAG_DATA_MINOR: {1} - FLAG_FACTURACION: {2} - FLAG_LEGAL: {3}", oModel.FLAG_CUSTOMER, oModel.FLAG_DATA_MINOR, oModelF.FLAG_FACTURACION, oModelD.FLAG_LEGAL));
            cantHist = ValidateHistoryDataTobe(strIdSession, NumDocCustomer, flagconvivencia);
            var motivoreg = cantHist > 0 ? oModel.strMotivo : "Registro";

            if (oModel.FLAG_CUSTOMER == "CUSTOMER" || oModel.FLAG_DATA_MINOR == "DATA_MINOR")
            {
                oClientrequest.GetDataClientRequest = DataClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);
                if (oModel.FLAG_CUSTOMER == "CUSTOMER")
                {
                    oClientrequest.HistoryClientRequest = HistoryClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession, motivoreg);
                    oClientrequest.HistoryClientRequest.HistoryClient.auditRequest = obj;
                }

                if (oModel.FLAG_DATA_MINOR == "DATA_MINOR")
                {
                    oClientrequest.HistoryClientDataRequest = HistoryClientData(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession, oModel.DNIRUCNew, motivoreg);
                    oClientrequest.HistoryClientDataRequest.HistoryClient.auditRequest = obj;
                }

            }

            if (oModelF.FLAG_FACTURACION == "FACTURACION")
            {
                Claro.Web.Logging.Info("Dir.facturacion", "Dir.facturacion", "inicio");
                oClientrequest.BillingAddressRequest = BillingAddress(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);
                oClientrequest.HistoryClientFactRequest = HistoryClientFact(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession, oModel.DNIRUCNew, motivoreg);
                oClientrequest.HistoryClientFactRequest.HistoryClient.auditRequest = obj;

            }
            else
            {
                oClientrequest.BillingAddressRequest = null;
                oClientrequest.HistoryClientFactRequest = null;
            }

            if (oModelD.FLAG_LEGAL == "LEGAL")
            {
                oClientrequest.GetDataClientRequest = DataClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);
                oClientrequest.HistoryClientLegalRequest = HistoryClientLegal(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession, oModel.DNIRUCNew, motivoreg);
                oClientrequest.HistoryClientLegalRequest.HistoryClient.auditRequest = obj;
            }
            else
            {
                oClientrequest.HistoryClientLegalRequest = null;
            }


            List<GetListRepresentanteLegal> olistaRL = null;
            if (oModel.listaRepresentanteLegal != null)
            {
                Claro.Web.Logging.Info("oModel.listaRepresentanteLegal", "oModel.listaRepresentanteLegal", Newtonsoft.Json.JsonConvert.SerializeObject(oModel.listaRepresentanteLegal));
                olistaRL = new List<GetListRepresentanteLegal>();
                var cLista = oModel.listaRepresentanteLegal;
                cLista.ForEach(x =>
                    {
                        if (x.cuRepAction == "M" || x.cuRepstatus == "0")
                        {
                            olistaRL.Add(new GetListRepresentanteLegal()
                            {
                                cuRepapemat = x.cuRepapemat,
                                cuRepapepat = x.cuRepapepat,
                                cuRepnombres = x.cuRepnombres,
                                cuReptipdoc = x.cuReptipdoc,
                                cuRepnumdoc = x.cuRepnumdoc,
                                //idCurep = x.idCurep,
                                cuRepstatus = x.cuRepstatus
                            });                            
                        }
                    });

                if (oClientrequest.GetDataClientRequest == null)
                {
                    oClientrequest.GetDataClientRequest = DataClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);                
                }
                oClientrequest.GetDataClientRequest.actualizarDatosClienteRequest.listaRepresentanteLegal = olistaRL;
                Claro.Web.Logging.Info("oClientrequest.ListRepresentanteLegal", "oClientrequest.ListRepresentanteLegal", Newtonsoft.Json.JsonConvert.SerializeObject(oClientrequest.GetDataClientRequest.actualizarDatosClienteRequest.listaRepresentanteLegal));

                oClientrequest.ListRepresentanteLegal = new List<HistoryClientRequest>();
                var existmodif = oModel.listaRepresentanteLegal.Exists(x => x.cuRepAction == "M");
                if (cantHist == 0)
                {
                      oModel.listaRepresentanteLegal.ForEach(delegate(Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal legalAgent){
                if (legalAgent.cuRepAction == "M" || legalAgent.cuRepstatus == "1")
                {
                    HistoryClientRequest lstHistRRLL = new HistoryClientRequest();
                    lstHistRRLL.HistoryClient = new HistoryClient() { 
                        auditRequest = new ColivingTransacService.AuditRequest() { 
                            idTransaccion = audit.transaction,
                            idAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                            nombreAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                            usuarioAplicacion = oModelD.userId                          
                        },
                        customerId = oModel.strCustomerId,
                        legalRep = legalAgent.cuRepnombres + " " + legalAgent.cuRepapepat + " " + legalAgent.cuRepapemat,
                        docRep = legalAgent.cuRepnumdoc,
                        docTypePerep = legalAgent.cuReptipdoc,
                        changeMot = motivoreg,//ULTIMA_MODIFICACION
                        fecReg = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss"),
                        //fecReg = DateTime.UtcNow.AddHours(5).ToString("yyyy-MM-ddTHH\\:mm\\:ssZ"),
                        updateGrupo = "5",//Modificado valor 2 por 5
                        ccNameNew = "",
                        firstName = "",
                        lastName = "",
                        businessName = "",
                        doctype = "",
                        doctypeDesc = "",
                        nroDoc = oModel.DNIRUCNew,
                        csCompRegNoNew = "",
                        passPortNoNewc = "",
                        birthdate = "",
                        jobDesc = "",
                        telf = "",
                        movil = "",
                        fax = "",
                        email = "",
                        csEmployerNew = "",
                        tradeName = "",
                        contact = "",
                        nationality = "",
                        nationalityDesc = "",
                        sex = "",
                        maritalStatus = "",
                        addressLegal = "",
                        addressNoteLegal = "",
                        ccAddrL2New = "",
                        districtLegal = "",
                        provinceLegal = "",
                        ccStreetLNew = "",
                        departmentLegal = "",
                        countryLegal = "",
                        countryLNew = "",
                        zipLegal = "",
                        addressFact = "",
                        addressNoteFact = "",
                        districtFact = "",
                        provinceFact = "",
                        departmentFact = "",
                        ccStreetNew = "",
                        countryFact = "",
                        zipFact = "",
                        usuario = ""                        
                    };
                    
                    oClientrequest.ListRepresentanteLegal.Add(lstHistRRLL);
                }
                });
                }

                if (cantHist > 0 && existmodif)
                {
                     oModel.listaRepresentanteLegal.ForEach(delegate(Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal legalAgent){
                if (legalAgent.cuRepAction == "M" || legalAgent.cuRepstatus == "1")
                {
                    HistoryClientRequest lstHistRRLL = new HistoryClientRequest();
                    lstHistRRLL.HistoryClient = new HistoryClient() { 
                        auditRequest = new ColivingTransacService.AuditRequest() { 
                            idTransaccion = audit.transaction,
                            idAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                            nombreAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                            usuarioAplicacion = oModelD.userId                          
                        },
                        customerId = oModel.strCustomerId,
                        legalRep = legalAgent.cuRepnombres + " " + legalAgent.cuRepapepat + " " + legalAgent.cuRepapemat,
                        docRep = legalAgent.cuRepnumdoc,
                        docTypePerep = legalAgent.cuReptipdoc,
                        changeMot = motivoreg,//ULTIMA_MODIFICACION
                        fecReg = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss"),
                        //fecReg = DateTime.UtcNow.AddHours(5).ToString("yyyy-MM-ddTHH\\:mm\\:ssZ"),
                        updateGrupo = "5",//Modificado valor 2 por 5
                        ccNameNew = "",
                        firstName = "",
                        lastName = "",
                        businessName = "",
                        doctype = "",
                        doctypeDesc = "",
                        nroDoc = oModel.DNIRUCNew,
                        csCompRegNoNew = "",
                        passPortNoNewc = "",
                        birthdate = "",
                        jobDesc = "",
                        telf = "",
                        movil = "",
                        fax = "",
                        email = "",
                        csEmployerNew = "",
                        tradeName = "",
                        contact = "",
                        nationality = "",
                        nationalityDesc = "",
                        sex = "",
                        maritalStatus = "",
                        addressLegal = "",
                        addressNoteLegal = "",
                        ccAddrL2New = "",
                        districtLegal = "",
                        provinceLegal = "",
                        ccStreetLNew = "",
                        departmentLegal = "",
                        countryLegal = "",
                        countryLNew = "",
                        zipLegal = "",
                        addressFact = "",
                        addressNoteFact = "",
                        districtFact = "",
                        provinceFact = "",
                        departmentFact = "",
                        ccStreetNew = "",
                        countryFact = "",
                        zipFact = "",
                        usuario = ""                        
                    };
                    
                    oClientrequest.ListRepresentanteLegal.Add(lstHistRRLL);
                }
                });
                }
               
            }

            if (oModel.DNI_RUC.Length != 11 || (oModel.DNI_RUC.Length == 11 && oModel.DNI_RUC.Substring(0, 2) == "10"))
            {
                oClientrequest.ListRepresentanteLegal = null;
                if (oClientrequest.GetDataClientRequest == null)
                {
                    oClientrequest.GetDataClientRequest = DataClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);                
                }
                oClientrequest.GetDataClientRequest.actualizarDatosClienteRequest.listaRepresentanteLegal = null;
            }
            else {
                //La lógica solo aplica cuando RRLL, no ha sido modificado
                if (olistaRL == null || olistaRL.Count() == 0)
                {
                    oModel.RepresentLegal = "";
                    DataOld.Cliente.REPRESENTANTE_LEGAL = "";
                    oModel.strTxtTipoDocumentoRL = "";
                    oModel.strDNI = "";
                    DataOld.Cliente.TIPO_DOC_RL = "";
                    DataOld.Cliente.NRO_DOC = "";
                    oClientrequest.ListRepresentanteLegal = null;

                    if (oClientrequest.GetDataClientRequest == null)
                    {
                        oClientrequest.GetDataClientRequest = DataClient(oModel, oModelD, oModelF, DataOld, FlagD, FlagF, strIdSession);
                    }
                    oClientrequest.GetDataClientRequest.actualizarDatosClienteRequest.listaRepresentanteLegal = null;
                }
            }

            oClientrequest.IdTransaccion = audit.transaction;
            oClientrequest.IpAplicacion = ConfigurationManager.AppSettings("ApplicationName");
            oClientrequest.MsgId = oModel.strPhone;
            oClientrequest.TimesTamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            oClientrequest.UserId = oModelD.userId;
            oClientrequest.Channel = ConfigurationManager.AppSettings("ApplicationName");

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Participante Existe?: " + oModel.strParticipante);

            oClientrequest.GetDataClientRequest.actualizarDatosClienteRequest.listaOpcional = new List<ListOptional>(){
                new ListOptional(){
                     clave="EXISTECLIENTE",
                     //valor=oModel.strParticipante==true?"1":"0"
                     valor=oModel.strParticipante==true?"0":"1"
                }
            };
                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Llamado :oServiceColiving.UpdateDatosClienteMigrado");
                oResponse = oServiceColiving.UpdateDatosClienteMigrado(oClientrequest, strIdSession, audit.transaction);
                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Fin :oServiceColiving.UpdateDatosClienteMigrado");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return oResponse;
        }

        //vtorremo

        private int ValidateHistoryDataTobe(string strIdSession, string strCustomerID, string flagconvivencia)
        {
            ColivingTransacService.AuditRequest1 audit = App_Code.Common.CreateAuditRequest<ColivingTransacService.AuditRequest1>(strIdSession);

            List<ColivingTransacService.DataHistorical> lista = new List<ColivingTransacService.DataHistorical>();

            int Response = 0;

            try
            {
                lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServiceColiving.HistoryDataClientTobe(audit, strIdSession, audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, strCustomerID, flagconvivencia);
                });
                Response = lista.Count;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Response;
        }

        //vtorremo


        private BillingAddressRequest BillingAddress(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession)
        {
            Claro.Web.Logging.Info("Dir.facturacion", "Dir.facturacion", "inicio BillingAddress");
            BillingAddressRequest oBillingAddressRequest = new BillingAddressRequest();
            UpdateBillingAddress oUpdateBillingAddress = new UpdateBillingAddress();
            UpdateBillingAddressRequest oUpdateBillingAddressRequest = new UpdateBillingAddressRequest();

            oUpdateBillingAddressRequest.adrSeq = oModelF.adrSeq;
            oUpdateBillingAddressRequest.idTypeCode = null;
            oUpdateBillingAddressRequest.adrIdno = oModel.DNI_RUC;
            oUpdateBillingAddressRequest.adrStreet = oModelF.adrStreet;
            oUpdateBillingAddressRequest.adrNote1 = oModelF.adrNote1;
            oUpdateBillingAddressRequest.adrNote2 = oModelF.adrNote2;
            oUpdateBillingAddressRequest.adrNote3 = oModelF.adrNote3;
            oUpdateBillingAddressRequest.adrCounty = oModelF.strDistrito;
            oUpdateBillingAddressRequest.adrCity = oModelF.strProvincia;
            oUpdateBillingAddressRequest.adrzIp = oModelF.strCodPostal;
            oUpdateBillingAddressRequest.adrState = oModelF.strDepartamento;
            oUpdateBillingAddressRequest.countryId = null;
            oUpdateBillingAddressRequest.countryIdPub = "PER";
            oUpdateBillingAddressRequest.adrEmail = oModel.strMail;
            oUpdateBillingAddressRequest.csId = oModel.strCustomerId;
            oUpdateBillingAddressRequest.bmId = "";
            oUpdateBillingAddressRequest.csIdPub = oModelD.csIdPub;
            oUpdateBillingAddressRequest.billingAccountId = "";
            oUpdateBillingAddressRequest.adrStreetNo = oModelF.adrStreetNo;
            oUpdateBillingAddressRequest.adrJbdes = oModelF.adrJbdes;
            //referenciaFact
            oUpdateBillingAddress.actualizarDireccionFacturacionRequest = oUpdateBillingAddressRequest;
            oBillingAddressRequest.BillingAddress = oUpdateBillingAddress;
            Claro.Web.Logging.Info("Dir.facturacion", "Dir.facturacion", "fin BillingAddress");
            return oBillingAddressRequest;

        }

        private HistoryClientRequest HistoryClient(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession, string Motivo)
        {

            HistoryClientRequest oHistoryRequest = new HistoryClientRequest();
            HistoryClient oHistoryClient = new HistoryClient();
            oHistoryClient.auditRequest = new ColivingTransacService.AuditRequest() { };
            oHistoryClient.customerId = oModel.strCustomerId;
            oHistoryClient.businessName = oModel.strRazonSocialNew;
            //oHistoryClient.businessName = oModel.strRazonSocial;
            oHistoryClient.firstName = oModel.strNombres;
            oHistoryClient.ccNameNew = oModel.strNombres;
            oHistoryClient.lastName = oModel.strApellidos;
            oHistoryClient.doctype = oModel.strTipoDocumento == Claro.Constants.NumberZeroString ? Claro.Constants.NumberTwoString : oModel.strTipoDocumento;//New
            oHistoryClient.nationality = oModel.strNacionalidad;
            oHistoryClient.nationalityDesc = oModel.lugarNacimiento;
            //oHistoryClient.nationality = string.Empty;
            oHistoryClient.maritalStatus = oModel.strEstadoCivilId;
            //oHistoryClient.maritalStatus = string.Empty;
            oHistoryClient.countryLNew = string.Empty;
            oHistoryClient.tradeName = string.Empty;
            oHistoryClient.updateGrupo = "1";

            oHistoryClient.nroDoc = oModel.DNIRUCNew;
            oHistoryClient.changeMot = Motivo;//ULTIMA_MODIFICACION
            oHistoryClient.fecReg = oModel.FECHA_ACT;
            oHistoryClient.birthdate = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
            oHistoryClient.sex = oModel.strSexo;
            oHistoryClient.docTypePerep = "";
            oHistoryRequest.HistoryClient = oHistoryClient;

            return oHistoryRequest;

        }

        private HistoryClientRequest HistoryClientInicial(Model.Postpaid.ChangeDataModel oModel, string DNIRUC)
        {
            Claro.Web.Logging.Info("", "", "History Client Ini");
            Claro.Web.Logging.Info("", "", "Representante Legal: " + oModel.RepresentLegal);
            HistoryClientRequest oHistoryRequest = new HistoryClientRequest();
            HistoryClient oHistoryClient = new HistoryClient();
            oHistoryClient.auditRequest = new ColivingTransacService.AuditRequest() { };
            oHistoryClient.customerId = oModel.strCustomerId;
            oHistoryClient.businessName = oModel.strRazonSocial;
            oHistoryClient.firstName = oModel.strNombres;
            oHistoryClient.ccNameNew = oModel.strNombres;
            oHistoryClient.lastName = oModel.strApellidos;
            oHistoryClient.doctype = oModel.strTipoDocumento == Claro.Constants.NumberZeroString ? Claro.Constants.NumberTwoString : oModel.strTipoDocumento;//New
            oHistoryClient.nationality = string.Empty; //Claro.SIACU.Transac.Service.Functions.CheckStr(oModel.strNacionalidad);
            oHistoryClient.nationalityDesc = oModel.lugarNacimiento;
            oHistoryClient.maritalStatus = oModel.strEstadoCivilId;
            oHistoryClient.countryLNew = string.Empty;
            oHistoryClient.tradeName = string.Empty;
            oHistoryClient.updateGrupo = "1";
            oHistoryClient.nroDoc = DNIRUC;
            oHistoryClient.changeMot = oModel.strMotivo;
            oHistoryClient.fecReg = oModel.FECHA_ACT;
            oHistoryClient.birthdate = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
            oHistoryClient.sex = oModel.strSexo;
            oHistoryClient.docTypePerep = "";
            oHistoryRequest.HistoryClient = oHistoryClient;

            return oHistoryRequest;
        }

        private HistoryClientRequest HistoryClientData(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession, string DNIRUC, string Motivo)
        {
            HistoryClientRequest oHistoryRequest = new HistoryClientRequest();
            HistoryClient oHistoryClient = new HistoryClient();

            oHistoryClient.customerId = oModel.strCustomerId;
            oHistoryClient.businessName = oModel.strRazonSocial;
            oHistoryClient.ccNameNew = oModel.strNombres;
            oHistoryClient.lastName = oModel.strApellidos;
            oHistoryClient.nroDoc = DNIRUC;
            oHistoryClient.jobDesc = oModel.strCargo;
            oHistoryClient.movil = (!string.IsNullOrEmpty(oModel.strMovil) && oModel.strMovil.EndsWith("|")) ? string.Empty : oModel.strMovil;
            oHistoryClient.fax = oModel.strFax;
            oHistoryClient.email = (!string.IsNullOrEmpty(oModel.strMail) && oModel.strMail.EndsWith("|")) ? string.Empty : oModel.strMail;
            oHistoryClient.businessName = oModel.strNombreComercial;
            oHistoryClient.contact = oModel.strContactoCliente;
            oHistoryClient.birthdate = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
            oHistoryClient.nationality = oModel.strNacionalidad;
            oHistoryClient.sex = oModel.strSexo;
            oHistoryClient.legalRep = oModel.RepresentLegal;
            //oHistoryClient.nroDoc = oModel.strDNI;
            oHistoryClient.telf = (!string.IsNullOrEmpty(oModel.strTelefono) && oModel.strTelefono.EndsWith("|")) ? string.Empty : oModel.strTelefono;
            oHistoryClient.changeMot = Motivo;//ULTIMA_MODIFICACION
            oHistoryClient.fecReg = oModel.FECHA_ACT;
            oHistoryClient.maritalStatus = "";
            oHistoryClient.countryLNew = "";
            oHistoryClient.tradeName = "";

            oHistoryClient.doctype = "0";
            oHistoryClient.updateGrupo = "2";
            oHistoryClient.docTypePerep = "";

            PrintPropertyValues(oHistoryClient);
            oHistoryRequest.HistoryClient = oHistoryClient;
            return oHistoryRequest;

        }

        #region Historico de RRLL
        private List<HistoryClientRequest> HistoryClientDataRRLL(Model.Postpaid.ChangeDataModel oModel, ColivingTransacService.AuditRequest obj, string FECHACTI, string DNIRUC)
        {
            List<HistoryClientRequest> lHistoryRequest = new List<HistoryClientRequest>();

            var replegOld = JsonConvert.DeserializeObject<List<Transac.Service.Areas.Transactions.Models.Postpaid.listaRepresentanteLegal>>(oModel.rrllHistorico);

            replegOld.ForEach((value) =>
            {
                HistoryClientRequest oHistoryRequest = new HistoryClientRequest()
                {
                    HistoryClient = new HistoryClient()
                    {
                        auditRequest = obj,
                        customerId = oModel.strCustomerId,
                        legalRep = value.cuRepnombres + " " + value.cuRepapepat + " " + value.cuRepapemat,
                        docRep = value.cuRepnumdoc,
                        docTypePerep = value.cuReptipdoc,
                        changeMot = "Registro",
                        fecReg = FECHACTI,
                        updateGrupo = "5",ccNameNew = "",firstName = "",lastName = "",
                        businessName = "",doctype = "",doctypeDesc = "",
                        nroDoc = DNIRUC,csCompRegNoNew = "",passPortNoNewc = "",birthdate = "",jobDesc = "",
                        telf = "",movil = "",fax = "",email = "",csEmployerNew = "",
                        tradeName = "",contact = "",nationality = "",nationalityDesc = "",
                        sex = "",maritalStatus = "",addressLegal = "",addressNoteLegal = "",
                        ccAddrL2New = "",districtLegal = "",provinceLegal = "",ccStreetLNew = "",
                        departmentLegal = "",countryLegal = "",countryLNew = "",zipLegal = "",
                        addressFact = "",addressNoteFact = "",districtFact = "",provinceFact = "",
                        departmentFact = "",ccStreetNew = "",countryFact = "",zipFact = "",usuario = ""
                    }
                };

                lHistoryRequest.Add(oHistoryRequest);
            });

            return lHistoryRequest;

        }
        #endregion
        private void PrintPropertyValues(Object obj)
        {
            Type t = obj.GetType();

            string Session = App_Code.Common.GetTransactionID();
            Claro.Web.Logging.Info(Session, Session, string.Format("Type is: {0}", t.Name));
            PropertyInfo[] props = t.GetProperties();
            Claro.Web.Logging.Info(Session, Session, string.Format("Properties (N = {0}):", props.Length));

            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                {
                    Claro.Web.Logging.Info(Session, Session, string.Format("{0} ({1}): {2}", prop.Name,
                                    prop.PropertyType.Name,
                                    prop.GetValue(obj)));
                }

                else
                {
                    Claro.Web.Logging.Info(Session, Session, string.Format("{0} ({1}): <Indexed>", prop.Name,
                                      prop.PropertyType.Name));
                }

        }


        private HistoryClientRequest HistoryClientLegal(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession, string DNIRUC, string Motivo)
        {

            HistoryClientRequest oHistoryRequest = new HistoryClientRequest();
            HistoryClient oHistoryClient = new HistoryClient();
            oHistoryClient.customerId = oModelD.strCustomerId;
            oHistoryClient.zipLegal = oModelD.strCodPostal;
            oHistoryClient.departmentLegal = oModelD.strDepartamento;
            //oHistoryClient.ccStreetLNew = oModelD.strDireccion;
            oHistoryClient.ccStreetLNew = "";
            oHistoryClient.districtLegal = oModelD.strDistrito;
            oHistoryClient.countryLegal = oModelF.strPais;
            oHistoryClient.provinceLegal = oModelD.strProvincia;
            oHistoryClient.doctype = "0";
            oHistoryClient.nroDoc = DNIRUC;
            oHistoryClient.updateGrupo = "3";
            oHistoryClient.changeMot = Motivo;//ULTIMA_MODIFICACION
            oHistoryClient.fecReg = oModel.FECHA_ACT;
            oHistoryClient.nationality = "";
            oHistoryClient.maritalStatus = "";
            //oHistoryClient.countryLNew = string.IsNullOrEmpty(oModelF.strPais) ? "" : oModelF.strPais;
            oHistoryClient.countryLNew = "";
            oHistoryClient.tradeName = "";
            oHistoryClient.addressLegal = oModelD.strDireccion; //strStreet
            oHistoryClient.addressNoteLegal = oModel.direccionReferenciaLegal;
            oHistoryClient.docTypePerep = "";
            oHistoryRequest.HistoryClient = oHistoryClient;

            return oHistoryRequest;
        }

        private HistoryClientRequest HistoryClientFact(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession, string DNIRUC, string Motivo)
        {
            Claro.Web.Logging.Info("Dir.facturacion", "Dir.facturacion", "inicio HistoryClientFact");
            HistoryClientRequest oHistoryRequest = new HistoryClientRequest();
            HistoryClient oHistoryClient = new HistoryClient();

            oHistoryClient.customerId = oModelF.strCustomerId;
            oHistoryClient.zipLegal = oModelF.strCodPostal;
            oHistoryClient.ccStreetLNew = oModelD.strDireccion;
            oHistoryClient.doctype = "0";
            oHistoryClient.nroDoc = DNIRUC;
            oHistoryClient.updateGrupo = "4";
            oHistoryClient.nationality = "";
            oHistoryClient.maritalStatus = "";
            oHistoryClient.changeMot = Motivo;//ULTIMA_MODIFICACION
            oHistoryClient.fecReg = oModel.FECHA_ACT;
            oHistoryClient.countryLNew = string.IsNullOrEmpty(oModelD.strPais) ? "" : oModelD.strPais;
            oHistoryClient.tradeName = "";
            oHistoryClient.countryFact = oModelD.strPais;
            oHistoryClient.addressFact = oModelF.strDireccion;
            oHistoryClient.addressNoteFact = oModelF.strReferencia;
            oHistoryClient.districtFact = oModelF.strDistrito;
            oHistoryClient.provinceFact = oModelF.strProvincia;
            oHistoryClient.departmentFact = oModelF.strDepartamento;
            oHistoryClient.docTypePerep = "";
            oHistoryRequest.HistoryClient = oHistoryClient;
            Claro.Web.Logging.Info("Dir.facturacion", "Dir.facturacion", "fin HistoryClientFact");
            return oHistoryRequest;
        }


        private GetUpdateDataClientRequest DataClient(Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF, string strIdSession)
        {
            //LHV: Actualizar datos del cliente
            GetUpdateDataClientRequest oUpdateDataClientRequest = new GetUpdateDataClientRequest();
            GetUpdateClientRequest oDataClientRequest = new GetUpdateClientRequest()
            {
                clienteID = string.Empty, //"2-40947143",
                tipoDocumento = oModel.strTipoDocumentoAnt, //"2",
                numeroDocumento = oModel.strNroDocAnt, //"40947143",
                codAplicacion = ConfigurationManager.AppSettings("strCodAplicativo"), //oModel.strCodigoAplicativo, //"1",
                sistemaOrigen = ConfigurationManager.AppSettings("ApplicationName"),   //"SIACUNICO",
                usuario = oModel.strCodAgente,
                motivo = oModelD.strMotivo,
                dirSecuencial = string.Empty,
                csId = oModelD.strCustomerId, //"43007109",
                csIdPub = oModelD.csIdPub, //"CUST0000006549",
                datosCliente = new GetDataClientRequest()
                {
                    estado = string.Empty,
                    tipoPersona = string.Empty,
                    nombre = oModel.strNombres,
                    apellidoPaterno = oModel.strApellidosPat,
                    apellidoMaterno = oModel.strApellidosMat,
                    razonSocial = oModel.strRazonSocialNew,
                    nombreComercial = oModel.strNombreComercialNew,
                    fechaNacimiento = System.Convert.ToString(oModel.dateFechaNacimiento),
                    lugarNacimiento = oModel.lugarNacimiento,
                    nacionalidad = oModel.strNacionalidad,
                    sexo = oModel.strSexo,
                    estadoCivil = oModel.strEstadoCivilId,
                    correoElectronico = (!string.IsNullOrEmpty(oModel.strMail) && oModel.strMail.EndsWith("|")) ? string.Empty : oModel.strMail,
                    clasificacionDeMercado = string.Empty,
                    telefonoReferencia1 = (!string.IsNullOrEmpty(oModel.strMovil) && oModel.strMovil.EndsWith("|")) ? string.Empty : oModel.strMovil,
                    telefonoReferencia2 = (!string.IsNullOrEmpty(oModel.strTelefono) && oModel.strTelefono.EndsWith("|")) ? string.Empty : oModel.strTelefono,
                    motivo = oModel.strMotivo,
                    segmentoComercial = string.Empty,
                    sectorComercial = string.Empty,
                    direccionLegal = oModel.direccionLegal,
                    direccionReferenciaLegal = oModel.direccionReferenciaLegal,
                    paisLegal = oModel.paisLegal,
                    departamentoLegal = oModel.departamentoLegal,
                    provinciaLegal = oModel.provinciaLegal,
                    distritoLegal = oModel.distritoLegal,
                    urbanizacionLegal = oModel.urbanizacionLegal,
                    codigoPostalLegal = oModel.codigoPostalLegal,
                    ubigeoDireccion = string.Empty,
                    limiteCredito = string.Empty,
                    numeroDocumento = oModel.DNIRUCNew
                },
                listaCuentaFacturacion = new List<GetAccountBillingRequest>(){
                    new GetAccountBillingRequest(){
                        IdCuenta = string.Empty,
                        codCuenta= string.Empty,
                        nombreCuenta= string.Empty,
                        idMedioFacturacion = string.Empty,
                        idCliente = string.Empty,
                        idClientePub = string.Empty,
                        saldoCuentaFacturacion = string.Empty,
                        flagReclamo = string.Empty,
                        tipoFactura = string.Empty,
                        idTipoFactura = string.Empty,
                        idTipoFacturaPublico = string.Empty,
                        indicadorUso = string.Empty,
                        ultimaFechaFacturacion = string.Empty,
                        saldoAnterior = string.Empty,
                        moneda = string.Empty,
                        flagPrimaria = string.Empty,
                        flagFacturacionDividida = string.Empty,
                        estado = string.Empty
                }},
                listaDatosCuentaFacturacion = new List<GetListDataAccountBillingRequest>()
                {
                  new GetListDataAccountBillingRequest(){
                            cuentaCambioSaldo = string.Empty,
                            codCuenta= string.Empty,
                            nombreCuenta= string.Empty,
                            flagDetalleLLamadas= string.Empty,
                            secuencialDirFactura= string.Empty,
                            secuencialDirFacturaTemp= string.Empty,
                            saldoActualCuentaFact= string.Empty,
                            flagReclamo= string.Empty,
                            nivelCambioMoneda= string.Empty,
                            cuentaTipo= string.Empty,
                            idTipoFacturaVinculada= string.Empty,     
                  }                
                },
                listaDirecciones = new List<GetListAddressRequest>()
                {
                  new GetListAddressRequest(){                         
                        dirFechaNacimiento = System.Convert.ToString(oModel.dateFechaNacimiento),
                        dirCiudad = string.Empty,
                        dirRegEmpresa = oModel.DNIRUCNew,
                        dirDistrito = string.Empty,
                        dirTipoCliente = string.Empty,
                        dirBorrado = string.Empty,
                        dirLicencia = string.Empty,
                        dirCorreo= (!string.IsNullOrEmpty(oModel.strMail) && oModel.strMail.EndsWith("|")) ? string.Empty : oModel.strMail,
                        dirEmpleado = string.Empty,
                        dirEmpleador = string.Empty,
                        dirFax1 = string.Empty,
                        dirFax1Area = string.Empty,
                        dirFaxEmpresa = string.Empty,
                        dirComercial = string.Empty,
                        dirIdentidad = string.Empty,
                        dirCiudadPostal = string.Empty,
                        dirLabor = string.Empty,
                        dirUsoDif = string.Empty,
                        dirApellido = string.Empty, //oModel.strApellidosPat + " " + oModel.strApellidosMat,
                        dirAdic1 = string.Empty,
                        dirAdic2 = string.Empty,
                        dirNombre2 = string.Empty, //oModel.strNombres,
                        dirEmpresa = string.Empty,
                        adrNacinalidad = oModel.strNacionalidad,
                        adrNacinalidadPublico = string.Empty,
                        dirNote1 = string.Empty,
                        dirNote2 = string.Empty,
                        dirNote3 = string.Empty,
                        dirTlf1 = string.Empty,
                        dirAreaTlf1 = string.Empty,
                        dirTlf2 = string.Empty,
                        dirAreaTlf2 = string.Empty,
                        dirSugerencia = string.Empty,
                        dirRoles = string.Empty,
                        dirSecuencial = Constants.ConstUno,
                        dirSexo = oModel.strSexo,
                        dirMensCorto = string.Empty,
                        dirSocial = string.Empty,
                        dirEstado = string.Empty,
                        dirCalle = string.Empty,
                        dirCalleNoPostal = string.Empty,
                        dirNroReg = string.Empty,
                        dirTemp = string.Empty,
                        dirUrgente = string.Empty,
                        dirFechaValidacion = string.Empty,  
                        dirAnios = string.Empty,
                        dirCodPostal = string.Empty,
                        idPais = string.Empty,
                        idPaisPub = string.Empty,
                        idTipoDoc = oModel.strTipoDocumento,
                        codIdioma = string.Empty,
                        codIdiomaPub = string.Empty,
                        estadoCivil = oModel.strEstadoCivilId,
                        estadoCivilPub = oModel.strEstadoCivilId,
                        idTitulo = string.Empty,
                        idTituloPub = string.Empty,
                  }
                },
                listaOpcional = new List<ListOptional>(){
                 new ListOptional(){
                    clave = "",
                    valor = ""
                 }
                },
                listaRepresentanteLegal = new List<GetListRepresentanteLegal>() { 
                    new GetListRepresentanteLegal()
                    {
                        idCurep = 0,
                        cuRepapepat = string.Empty,
                        cuRepapemat = string.Empty,
                        cuRepnombres = string.Empty,
                        cuRepnumdoc = string.Empty,
                        cuReptipdoc = string.Empty,
                        cuRepstatus = string.Empty
                    }
                }

            };

            oUpdateDataClientRequest.actualizarDatosClienteRequest = oDataClientRequest;

            return oUpdateDataClientRequest;
        }





        #region CONSTANCY PDF - SEND EMAIL
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            Claro.Web.Logging.Info("GetConstancyPDF :", strIdInteraction, "inicio de GetConstancyPDF");
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strTerminacionPdf = ConfigurationManager.AppSettings("strTerminacionPDF").ToString();
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;
            string xml = string.Empty;

            Claro.Web.Logging.Info("GetConstancyPDF :", strIdInteraction, "despues de declaracion de variables");

            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            CommonTransacService.ParametersGeneratePDF parameters = new CommonTransacService.ParametersGeneratePDF();

            Claro.Web.Logging.Info("GetConstancyPDF :", strIdInteraction, "despues de la invocacion al request");
            try
            {
                parameters.StrCasoInter = strIdInteraction;
                parameters.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaCambioDatosPost");
                parameters.StrNombreArchivoTransaccion = KEY.AppSettings("strCambioDatosFormatoTransac");
                parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");
                parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
        //        strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgDatosMenoresConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                Claro.Web.Logging.Info("GetGenerateConstancyXml :", strIdInteraction, "antes de GetGenerateConstancyXml");

                xml = GetGenerateConstancyXml(strIdInteraction, oModel, oModelD, oModelF, DataOld, FlagD, FlagF);

                Claro.Web.Logging.Info("GetGenerateConstancyXml :", strIdInteraction, "despues de GetGenerateConstancyXml");


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


        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.Postpaid.ChangeDataModel model, string strNombreArchivoPDF, byte[] attachFile,string strNomFile)
        {
            string strResul = string.Empty;
            Common.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(model.strIdSession);
            CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strAsunto = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strAsuntoEmailMinorChanges",
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
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + " </td></tr>";
                strMessage += "<tr><td width='180' height='22'>&nbsp;</td></tr>";
                if (model.Accion == Claro.Constants.LetterR)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgEmailMinorChanges", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                }



                strMessage += "<tr><td class='Estilo1'>&nbsp;</td></tr>";

                strMessage += "         <tr><td class='Estilo1'>" + Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td height='22'></td></tr> ";

                strMessage += "         <tr><td class='Estilo1'>" + Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                CommonTransacService.SendEmailSBRequest objSendEmailSBReq = 
                    new CommonTransacService.SendEmailSBRequest() { 
                        audit = AuditRequest,
                        TransactionId = model.strTransaccion,
                        SessionId = model.strIdSession,
                        strRemitente = strRemitente,
                        strDestinatario = model.strEmailSend,
                        strMensaje = strMessage,
                        strHTMLFlag = "1",
                        strAsunto = strAsunto,
                        Archivo = attachFile,
                        strNomFile = strNomFile
                };
                CommonTransacService.SendEmailSBResponse objSendEmailSBRes = new CommonTransacService.SendEmailSBResponse();
                objSendEmailSBRes = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailSBResponse>(() => { return oCommonService.GetSendEmailSB(objSendEmailSBReq); });              

                if (objSendEmailSBRes.codigoRespuesta == "0")
                {
                    strResul = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                }
                else
                {
                    strResul = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                }
                Claro.Web.Logging.Info(model.strIdSession, "strInteraccionId: " + strInteraccionId, "Fín Método : GetSendEmail - strResul : " + strResul);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.strIdSession, AuditRequest.transaction, ex.Message);
                Claro.Web.Logging.Info(model.strIdSession, AuditRequest.transaction, "Cambio_Datos  ERROR - GetSendEmail");
                strResul = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
            }
            return strResul;
        }
        #endregion

public string GetGenerateConstancyXml(string strInteraccion, Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
        {
            string xmlConstancyPDF = string.Empty;
            try
            {
                string pathFileXml = KEY.AppSettings("strRutaXmlConstanciaCambioDatos");
                string vacio = string.Empty;
                string FechAct = DateTime.Now.ToShortDateString();

                var listParamConstancyPdf = new List<string>();

                listParamConstancyPdf.Add(KEY.AppSettings("strCambioDatosFormatoTransac")); //FORMATO_TRANSACCION
                listParamConstancyPdf.Add(KEY.AppSettings("strNombreTransaccionCambioDatos")); //TITULO_TRANSACCION

                listParamConstancyPdf.Add(oModel.oChangeDataTemplate.X_INTER_17.ToString()); //CENTRO_ATENCION_AREA
                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3) != string.Empty)
                {
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3) == "RUC")
                    {
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1) != string.Empty)
                        {
                            //listParamConstancyPdf.Add(oModel.oChangeDataTemplate.X_CLARO_LDN1.ToString()); //TITULAR_CLIENTE
                            listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                        }
                        else
                        {
                            listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                        }
                    }
                   else
                    {
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) != string.Empty && Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME) != string.Empty)
                        {
                            //listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) + " " + Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME)); //TITULAR_CLIENTE
                            if (DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                            }
                            else { 
                                listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.NOMBRES) + " " + Claro.Convert.ToString(DataOld.Cliente.APELLIDOS)); //TITULAR_CLIENTE                            
                            }
                        }
                        else
                        {
                            if (DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                            }
                            else { 
                                listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.NOMBRES) + " " + Claro.Convert.ToString(DataOld.Cliente.APELLIDOS)); //TITULAR_CLIENTE                            
                            }
                        }
                    }

                    //listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3)); //TIPO_DOC_IDENTIDAD
                    listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.TIPO_DOC)); //TIPO_DOC_IDENTIDAD                    
                }
                else
                {
                    if (DataOld.Cliente.TIPO_DOC.ToString() == "RUC")
                    {
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1) != string.Empty)
                        {
                            if (DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                            }
                            else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1)); //TITULAR_CLIENTE
                        }
                        }
                        else
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.RAZON_SOCIAL)); //TITULAR_CLIENTE
                        }
                    }
                    else
                    {
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) != string.Empty && Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME) != string.Empty)
                        {
                            if (DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                            }
                            else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) + " " + Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME)); //TITULAR_CLIENTE
                        }
                        }
                        else
                        {
                            if (DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                            {
                                listParamConstancyPdf.Add(DataOld.Cliente.RAZON_SOCIAL.ToString()); //TITULAR_CLIENTE
                            }
                            else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.NOMBRES) + " " + Claro.Convert.ToString(DataOld.Cliente.APELLIDOS)); //TITULAR_CLIENTE
                        }
                    }
                    }

                    listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.TIPO_DOC)); //TIPO_DOC_IDENTIDAD
                }

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DOCUMENT_NUMBER) != string.Empty)
                {
                    //listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DOCUMENT_NUMBER)); //NRO_DOC_IDENTIDAD
                    listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.DNI_RUC)); //NRO_DOC_IDENTIDAD
                }
                else
                {
                    listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.DNI_RUC)); //NRO_DOC_IDENTIDAD
                }

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_NAME_LEGAL_REP) != string.Empty)
                {
                    //listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_NAME_LEGAL_REP)); //REPRE_LEGAL
                    if ((oModel.strFlagPlataforma == "TOBE" || oModel.strFlagPlataforma == "CBIO") && DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                    {
                        if (oModel.DNIRUCNew.Length != 11)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(repLegAntiguo)); //REPRE_LEGAL
                            Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo nodruc1", "ok" + repLegAntiguo);
                        }
                        else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.NameRepLegalCurrent)); //REPRE_LEGAL
                            Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo NameRepLegalCurrent1", "ok" + oModel.NameRepLegalCurrent);
                        }
                    }
                    else {
                        Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo 1", "ok" + repLegAntiguo);
                        listParamConstancyPdf.Add(Claro.Convert.ToString(repLegAntiguo)); //REPRE_LEGAL
                    }
                }
                else
                {
                    if ((oModel.strFlagPlataforma == "TOBE" || oModel.strFlagPlataforma == "CBIO") && DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                    {
                        if (oModel.DNIRUCNew.Length != 11)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(repLegAntiguo)); //REPRE_LEGAL
                            Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo nodruc2", "ok" + repLegAntiguo);
                        }
                        else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.NameRepLegalCurrent)); //REPRE_LEGAL  
                            Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo NameRepLegalCurrent2", "ok" + oModel.NameRepLegalCurrent);                  
                        }
                    }
                    else
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(repLegAntiguo)); //REPRE_LEGAL
                        Claro.Web.Logging.Info(oModel.strIdSession, "rep leg antiguo 2", "ok" + repLegAntiguo);
                    }
                }

                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.strPhone)); //NRO_TELF
                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_18)); //FECHA_TRANSACCION_PROGRAM
                listParamConstancyPdf.Add(Claro.Convert.ToString(strInteraccion)); //CASO_INTER
                listParamConstancyPdf.Add(Claro.Convert.ToString(DataOld.Cliente.CONTRATO_ID));//CONTRATO

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DOCUMENT_NUMBER) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN2) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME) == string.Empty)
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_OIPER
                    listParamConstancyPdf.Add(string.Empty); //TIT_NIPER
                    listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOMB_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //APELL_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //APELL_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //RSOCIAL_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //RSOCIAL_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_NUEVO
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3) != string.Empty)
                    {

                        if (oModel.oChangeDataTemplate.X_OLD_CLARO_LDN3 == oModel.oChangeDataTemplate.X_CLARO_LDN3)
                        {

                            listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_NUEVO
                        }
                        else
                        {

                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN3)); //TIPO_DOC_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3)); //TIPO_DOC_NUEVO
                        }
                    }
                    else
                    {
                    listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_NUEVO
                    }
                    listParamConstancyPdf.Add(string.Empty); //NRO_DOC_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NRO_DOC_NUEVO
                    if (oModel.strEstadoCivilId != DataOld.Cliente.ESTADO_CIVIL_ID)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.ESTADO_CIVIL); //EST_CIVIL_ACTUAL
                        listParamConstancyPdf.Add(oModel.strEstadoCivil); //EST_CIVIL_NUEVO
                    }
                    else {
                        listParamConstancyPdf.Add(string.Empty); //EST_CIVIL_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //EST_CIVIL_NUEVO
                    }
                    
                }
                else
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle1")); //TIT_OIPER
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle2")); //TIT_NIPER

                    if (oModel.strTxtTipoDocumento == "RUC")
                    {
                        if (oModel.DNI_RUC.Length == 11)
                        {
                            if (oModel.DNI_RUC.Substring(0,2) == "10")
	                        {
		                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) != string.Empty)
                                {
                                    listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_FIRST_NAME)); //NOMB_ACTUAL
                                    listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME)); //NOMB_ACTUAL
                                }
                                else
                                {
                                    listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                                    listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                                }
                                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME) != string.Empty)
                                {
                                    listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_LAST_NAME)); //APELL_ACTUAL
                                    listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME)); //APELL_NUEVO
                                }
                                else
                                {
                                    listParamConstancyPdf.Add(string.Empty); //APELL_ACTUAL
                                    listParamConstancyPdf.Add(string.Empty); //APELL_NUEVO
                                }
	                        }else{
                                    listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                                    listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                                    listParamConstancyPdf.Add(string.Empty); //APELL_ACTUAL
                                    listParamConstancyPdf.Add(string.Empty); //APELL_NUEVO
                            }
                        }
                        
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN1)); //RSOCIAL_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN1)); //RSOCIAL_NUEVO
                        }
                        else
                        {
                            listParamConstancyPdf.Add(string.Empty); //RSOCIAL_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //RSOCIAL_NUEVO
                        }
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN2) != string.Empty)
                        {
                            if ((oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2 == null || oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2 == "") && (oModel.oChangeDataTemplate.X_CLARO_LDN2 != null || oModel.oChangeDataTemplate.X_CLARO_LDN2 != ""))
                            {
                                listParamConstancyPdf.Add(Claro.Convert.ToString("NINGUNO")); //NOMB_COMERCIAL_ACTUAL
                                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN2)); //NOMB_COMERCIAL_NUEVO
                            }
                            else if ((oModel.oChangeDataTemplate.X_CLARO_LDN2 == null || oModel.oChangeDataTemplate.X_CLARO_LDN2 == "") && (oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2 != null || oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2 != ""))
                            {
                                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2)); //NOMB_COMERCIAL_ACTUAL
                                listParamConstancyPdf.Add(Claro.Convert.ToString("ELIMINADO")); //NOMB_COMERCIAL_NUEVO
                            }
                            else { 
                                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2)); //NOMB_COMERCIAL_ACTUAL
                                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN2)); //NOMB_COMERCIAL_NUEVO
                            }                           
                        }
                        else
                        {
                            if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2) != string.Empty)
                            {
                                listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN2)); //NOMB_COMERCIAL_ACTUAL
                                listParamConstancyPdf.Add(Claro.Convert.ToString("ELIMINADO")); //NOMB_COMERCIAL_NUEVO
                            }
                            else { 
                            listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_NUEVO
                            }                           
                        }                        
                    }
                    else
                    {
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_FIRST_NAME)); //NOMB_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_FIRST_NAME)); //NOMB_ACTUAL
                        }
                        else
                        {
                            listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //NOMB_ACTUAL
                        }
                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_LAST_NAME)); //APELL_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LAST_NAME)); //APELL_NUEVO
                        }
                        else
                        {
                            listParamConstancyPdf.Add(string.Empty); //APELL_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //APELL_NUEVO
                        }
                        listParamConstancyPdf.Add(string.Empty); //RSOCIAL_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //RSOCIAL_NUEVO
                        listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NOMB_COMERCIAL_NUEVO
                    }

                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3) != string.Empty)
                    {
                        
                        if (oModel.oChangeDataTemplate.X_OLD_CLARO_LDN3 == oModel.oChangeDataTemplate.X_CLARO_LDN3)
                        {
                            listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_NUEVO
                        }
                        else { 
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN3)); //TIPO_DOC_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_LDN3)); //TIPO_DOC_NUEVO
                        }
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //TIPO_DOC_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DOCUMENT_NUMBER) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_DOC_NUMBER)); //NRO_DOC_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DOCUMENT_NUMBER)); //NRO_DOC_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //NRO_DOC_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NRO_DOC_NUEVO
                    }
                    if (oModel.strEstadoCivilId != DataOld.Cliente.ESTADO_CIVIL_ID)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.ESTADO_CIVIL); //EST_CIVIL_ACTUAL
                        listParamConstancyPdf.Add(oModel.strEstadoCivil); //EST_CIVIL_NUEVO
                    }
                    else { 
                        listParamConstancyPdf.Add(string.Empty); //EST_CIVIL_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //EST_CIVIL_NUEVO
                    }
                    
                }

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL1) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_NUMBER) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL2) == string.Empty)
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_OCDC
                    listParamConstancyPdf.Add(string.Empty); //TIT_NCDC
                    listParamConstancyPdf.Add(string.Empty); //TELF_REF1_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //TELF_REF1_NUEVO
                    if (oModel.strTelefono != DataOld.Cliente.TELEF_REFERENCIA)
                    {
                        listParamConstancyPdf.Add(DataOld.Cliente.TELEF_REFERENCIA); //TELF_REF2_ACTUAL
                        listParamConstancyPdf.Add(oModel.strTelefono); //TELF_REF2_NUEVO
                    }
                    else {
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF2_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF2_NUEVO
                    }
                    
                    listParamConstancyPdf.Add(string.Empty); //EMAIL_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //EMAIL_NUEVO
                }
                else
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle3")); //TIT_OCDC
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle4")); //TIT_NCDC

                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL1) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLAROLOCAL1)); //TELF_REF1_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL1)); //TELF_REF1_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF1_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF1_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_NUMBER) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_REFERENCE_PHONE)); //TELF_REF2_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLARO_NUMBER)); //TELF_REF2_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF2_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //TELF_REF2_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL2) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_LOT_CODE)); //EMAIL_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL2)); //EMAIL_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //EMAIL_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //EMAIL_NUEVO
                    }
                }

                if (oModel.strTxtTipoDocumento == "RUC")
                {
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_NAME_LEGAL_REP) == string.Empty &&
                        Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DNI_LEGAL_REP) == string.Empty)
                    {
                        listParamConstancyPdf.Add(string.Empty); //TIT_OIRL
                        listParamConstancyPdf.Add(string.Empty); //TIT_NIRL
                        listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_NUEVO
                        listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_NUEVO
                        listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle5")); //TIT_OIRL
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle6")); //TIT_NIRL

                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_NAME_LEGAL_REP) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_6)); //NOM_REPRES_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_NAME_LEGAL_REP)); //NOM_REPRES_NUEVO
                        }
                        else
                        {
                            listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_NUEVO
                        }


                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DNI_LEGAL_REP) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_7)); //NRODOC_REP_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DNI_LEGAL_REP)); //NRODOC_REP_NUEVO
                        }
                        else
                        {
                            listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_NUEVO
                        }

                        if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OST_NUMBER) != string.Empty)
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_POSITION)); //TIPODOC_REP_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OST_NUMBER)); //TIPODOC_REP_NUEVO
                        }
                        else {
                            listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_ACTUAL
                            listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_NUEVO
                        }                        
                    }
                }
                else
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_OIRL
                    listParamConstancyPdf.Add(string.Empty); //TIT_NIRL
                    listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOM_REPRES_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NRODOC_REP_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //TIPODOC_REP_NUEVO
                }

                if (oModel.strTxtTipoDocumento == "RUC")
                {
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_3) != string.Empty)
                    {
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle7")); //TIT_OCC
                        listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle8")); //TIT_NCC
                        if ((oModel.oChangeDataTemplate.X_INTER_2 == null || oModel.oChangeDataTemplate.X_INTER_2 == "") && (oModel.oChangeDataTemplate.X_INTER_3 != null || oModel.oChangeDataTemplate.X_INTER_3 != ""))
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString("NINGUNO")); //NOM_CONT_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_3)); //NOM_CONT_NUEVO
                        }
                        else if ((oModel.oChangeDataTemplate.X_INTER_3 == null || oModel.oChangeDataTemplate.X_INTER_3 == "") && (oModel.oChangeDataTemplate.X_INTER_2 != null || oModel.oChangeDataTemplate.X_INTER_2 != ""))
                        {
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_2)); //NOM_CONT_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString("ELIMINADO")); //NOM_CONT_NUEVO
                        }
                        else { 
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_2)); //NOM_CONT_ACTUAL
                            listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_3)); //NOM_CONT_NUEVO
                        }                       
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //TIT_OCC
                        listParamConstancyPdf.Add(string.Empty); //TIT_NCC
                        listParamConstancyPdf.Add(string.Empty); //NOM_CONT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NOM_CONT_NUEVO
                    }
                }
                else
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_OCC
                    listParamConstancyPdf.Add(string.Empty); //TIT_NCC
                    listParamConstancyPdf.Add(string.Empty); //NOM_CONT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOM_CONT_NUEVO
                }

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_ADDRESS) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_REFERENCE_ADDRESS) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DEPARTMENT) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_5) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DISTRICT) == string.Empty)
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_ODD
                    listParamConstancyPdf.Add(string.Empty); //TIT_NDD
                    listParamConstancyPdf.Add(string.Empty); //DIRECCION_CLIENTE_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DIRECCION_CLIENTE_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //NOTAS_DIRECCION_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOTAS_DIRECCION_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_CLIENTE_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_CLIENTE_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //PROVINCIA_CLIENTE_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //PROVINCIA_CLIENTE_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //DISTRITO_CLIENTE_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DISTRITO_CLIENTE_NUEVO
                }
                else
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle9")); //TIT_ODD
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle10")); //TIT_NDD

                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_ADDRESS) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_ADDRESS5)); //DIRECCION_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_ADDRESS)); //DIRECCION_CLIENTE_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DIRECCION_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DIRECCION_CLIENTE_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_REFERENCE_ADDRESS) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_15)); //NOTAS_DIRECCION_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_REFERENCE_ADDRESS)); //NOTAS_DIRECCION_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //NOTAS_DIRECCION_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NOTAS_DIRECCION_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DEPARTMENT) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_21)); //DEPARTAMENTO_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DEPARTMENT)); //DEPARTAMENTO_CLIENTE_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_CLIENTE_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_5) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_4)); //PROVINCIA_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_5)); //PROVINCIA_CLIENTE_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //PROVINCIA_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //PROVINCIA_CLIENTE_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DISTRICT) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_29)); //DISTRITO_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_DISTRICT)); //DISTRITO_CLIENTE_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DISTRITO_CLIENTE_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DISTRITO_CLIENTE_NUEVO
                    }
                }

                if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL3) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL4) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL5) ==string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL6) == string.Empty &&
                    Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_1) == string.Empty)
                {
                    listParamConstancyPdf.Add(string.Empty); //TIT_ODF
                    listParamConstancyPdf.Add(string.Empty); //TIT_NDF
                    listParamConstancyPdf.Add(string.Empty); //DIRECCION_FACT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DIRECCION_FACT_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //NOTAS_FACT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //NOTAS_FACT_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_FACT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_FACT_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //PROVINCIA_FACT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //PROVINCIA_FACT_NUEVO
                    listParamConstancyPdf.Add(string.Empty); //DISTRITO_FACT_ACTUAL
                    listParamConstancyPdf.Add(string.Empty); //DISTRITO_FACT_NUEVO
                }
                else
                {
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle11")); //TIT_ODF
                    listParamConstancyPdf.Add(KEY.AppSettings("strCHDTitle12")); //TIT_NDF

                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL3) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLAROLOCAL3)); //DIRECCION_FACT_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL3)); //DIRECCION_FACT_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DIRECCION_FACT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DIRECCION_FACT_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL4) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLAROLOCAL4)); //NOTAS_FACT_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL4)); //NOTAS_FACT_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //NOTAS_FACT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //NOTAS_FACT_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL5) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLAROLOCAL5)); //DEPARTAMENTO_FACT_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL5)); //DEPARTAMENTO_FACT_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_FACT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DEPARTAMENTO_FACT_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL6) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLAROLOCAL6)); //PROVINCIA_FACT_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_CLAROLOCAL6)); //PROVINCIA_FACT_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //PROVINCIA_FACT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //PROVINCIA_FACT_NUEVO
                    }
                    if (Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_1) != string.Empty)
                    {
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_OLD_CLARO_LDN4)); //DISTRITO_FACT_ACTUAL
                        listParamConstancyPdf.Add(Claro.Convert.ToString(oModel.oChangeDataTemplate.X_INTER_1)); //DISTRITO_FACT_NUEVO
                    }
                    else
                    {
                        listParamConstancyPdf.Add(string.Empty); //DISTRITO_FACT_ACTUAL
                        listParamConstancyPdf.Add(string.Empty); //DISTRITO_FACT_NUEVO
                    }
                }


                if (oModel.Flag_Email == true)
                {
                    listParamConstancyPdf.Add("SI"); //ENVIO_CORREO
                    listParamConstancyPdf.Add(oModel.strEmailSend); //EMAIL
                }
                else
                {
                    listParamConstancyPdf.Add("NO"); //ENVIO_CORREO
                    listParamConstancyPdf.Add(vacio); //EMAIL
                }
                listParamConstancyPdf.Add(Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("CambioDatosContentCommercial", KEY.AppSettings("strConstArchivoSIACPOConfigMsg")));//CONTENIDO_COMERCIAL
                listParamConstancyPdf.Add(oModel.strCodAgente);//COD_AGENTE
                listParamConstancyPdf.Add(oModel.strNombAgente);//NOM_AGENTE

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

            if (oModel.strTipoDocumento == "0")
            {
                if (oModel.DNI_RUC.Substring(0, 2) == "20")
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
            if (oModel.strMovil == null) oModel.strMovil = "";
            if (oModel.strMovil != DataOld.Cliente.TELEFONO_CONTACTO)
            {
                respuesta = true;
            }
            if (oModel.strTelefono == null) oModel.strTelefono = "";
            if (oModel.strTelefono != DataOld.Cliente.TELEF_REFERENCIA)
            {
                respuesta = true;
            }
            if (oModel.strMail == null) oModel.strMail = "";
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
            if (oModel.strTxtTipoDocumentoRL != DataOld.Cliente.TIPO_DOC_RL)
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
                    strcustomerid = oModel.strCustomerId
                };

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetDataCustomer(objRequestCustomer,1);
                });
                                
                if (objResponse.Cliente.FECHA_NAC != null)
                {
                    cambiarfecha = objResponse.Cliente.FECHA_NAC;
                }

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

        public JsonResult GetTypeDocumentTOBE(string strIdSession)
        {
            List<ListItems> lista = new List<ListItems>();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            //Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetMotivoCambio");
            string strParametro = ConfigurationManager.AppSettings("ConstKeyListaDocumentos");
            string mensaje = "";
            try
            {
                lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServiceColiving.GetDocumentTypeTOBE(strIdSession, audit.transaction, strParametro);
                });

            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                }

            }

            return Json(new { objLista = lista });
        }

        public JsonResult GetCivilStatus(string strIdSession, string strTransaccion)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Common.CivilStatusRequestPrepaid objRequest = new Common.CivilStatusRequestPrepaid() {
                audit = audit
            };
            

            Common.CivilStatusResponsePrepaid objResponse = new Common.CivilStatusResponsePrepaid();
            List<ListItem> lista = new List<ListItem>();
           
            try{
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

        public JsonResult GetCivilStatusToBe(string strIdSession, string strTransaccion)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            ColivingTransacService.GetParameterResponse objResponse = new ColivingTransacService.GetParameterResponse();
            string strName = ConfigurationManager.AppSettings("strParameterEstatuscivil"); ;
            List<ListItems> lista = new List<ListItems>();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServiceColiving.GetParameter(strName, strIdSession, strTransaccion);
                });

                if (objResponse.LstParameter.Count > 0)
                {
                    foreach (var temp in objResponse.LstParameter)
                    {
                        ListItems item = new ListItems();
                        item.Code = temp.VALOR_C;
                        item.Description = temp.DESCRIPCION;
                        Claro.Web.Logging.Error(audit.Session, audit.transaction, "GetCivilStatusToBe- item.Code: " + item.Code);
                        if (item.Code != "-1")
                        {
                        lista.Add(item);
                    }
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
            Common.BrandRequestPrepaid1 objRequest = new Common.BrandRequestPrepaid1() {
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
                        codigo  = (item.Description.Substring(0,3)).Trim();
                    }else{
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
            return Json(new {data = strcodePostal});


        }
        public string GetPostalCode(string strSession, string strTransaction, string strIdDistrito)
        {
            string strCode = string.Empty;
          // List<HELPERS.CommonServices.GenericItem> listPostal = new List<HELPERS.CommonServices.GenericItem>();
         

       
            List<Claro.SIACU.Transac.Service.ItemGeneric> listaItem = Claro.SIACU.Transac.Service.Functions.GetListValuesXML("ListaCodigoPostalCambioDatos", "0", "HFCDatos.xml");

           Claro.SIACU.Transac.Service.ItemGeneric item = listaItem.Where(x => x.Code.Equals(strIdDistrito)).FirstOrDefault();
            
            if (item != null)
            {
                strCode = item.Description;
            }

            return strCode;
        }




        public JsonResult GetAditionalData(string strSession, string strCustomer)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strSession);
            Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.Client oCliente = new Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.Client();
            try
            {
                var ipCliente = App_Code.Common.GetClientIP();
                var ipServidor = App_Code.Common.GetClientIP();
                //var nomServClient = App_Code.Common.GetClientName();
                var ctaUsuClient = App_Code.Common.CurrentUser;
                var ApplicationName = ConfigurationManager.AppSettings("ApplicationName");

                oCliente = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetAditionalData(audit.transaction, ipServidor, ApplicationName, ctaUsuClient, strCustomer);
                });
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            return Json(new { data = oCliente });
        }




        public string ActualizarDatosMenores(Model.Postpaid.ChangeDataModel oModel)
        {
            string strResultado = "";
            bool flagResult = false;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strIdSession);
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
                

                strResultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateChangeData(objRequest, audit.Session, audit.transaction);
                });

                if (strResultado == "")
                {
                    flagResult = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            return strResultado;
        }

public string UpdateNameCustomer(string strIdSession, Model.Postpaid.ChangeDataModel oModel,string TipDocOld,string DniRucOld)
        {
            
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string strResult =string.Empty;
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();
            try{
                 Client objRequest = new Client();
                
                objRequest.CUSTOMER_ID = oModel.strCustomerId;
                objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
                objRequest.NOMBRES = oModel.strNombres;
                objRequest.APELLIDOS = oModel.strApellidos;
                objRequest.TIPO_DOC = oModel.strTipoDocumento == Claro.Constants.NumberZeroString ? Claro.Constants.NumberTwoString : oModel.strTipoDocumento;//New
                if (oModel.DNI_RUC == DniRucOld)
                {
                    objRequest.DNI_RUC = null;//New
                }
                else {
                    objRequest.DNI_RUC = oModel.DNI_RUC;//New
                }
                objRequest.MOTIVO_REGISTRO = oModel.strMotivo;
                
                //Nuevos Requests
                if (TipDocOld == "RUC")
                {
                    objRequest.TIPO_DOC_RL = "2";//Old
                }
                else if (TipDocOld == "Carnet de Extranjería") {
                    objRequest.TIPO_DOC_RL = "4";//Old
                }
                else if (TipDocOld == "CIE")
                {
                    objRequest.TIPO_DOC_RL = "12";//Old
                }
                else if (TipDocOld == "CIRE")
                {
                    objRequest.TIPO_DOC_RL = "11";//Old
                }
                else if (TipDocOld == "CPP")
                {
                    objRequest.TIPO_DOC_RL = "13";//Old
                }
                else if (TipDocOld == "CTM")
                {
                    objRequest.TIPO_DOC_RL = "14";//Old
                }
                else if (TipDocOld == "DNI")
                {
                    objRequest.TIPO_DOC_RL = "2";//Old
                }
                else if (TipDocOld == "Pasaporte")
                {
                    objRequest.TIPO_DOC_RL = "1";//Old
                }
                objRequest.NRO_DOC = DniRucOld;//Old

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateNameCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
                });

                if (objResponse != null)
                {
                    strResult = objResponse.ResultCode;

                    if (strResult == "0")
                    {
                        strResult = UpdateDataCustomerPClub(strIdSession, oModel, DniRucOld);
                    }
                }
                
            }catch(Exception ex){
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                strResult = "-1";
            }

            return strResult;

        }

public string UpdateDataMinorCustomer(string strIdSession, Model.Postpaid.ChangeDataModel oModel, string TipDocOld, string DniRucOld)
        {            
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string strResult =string.Empty; 
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();

            try{
                 Client objRequest = new Client();
                
                objRequest.CUSTOMER_ID = oModel.strCustomerId;
                objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
                objRequest.NOMBRES = oModel.strNombres;
                objRequest.APELLIDOS = oModel.strApellidos;
                objRequest.DNI_RUC = DniRucOld;//Old
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

                //Nuevos Requests
                objRequest.TIPO_DOC = "0";                                             //Old
                objRequest.FLAG_EMAIL =  oModel.MessageSelectDate;                     //Data minor
                objRequest.FLAG_REGISTRADO = Int32.Parse(oModel.MessageSelectJobTypes);//Representante Legal
                objRequest.P_FLAG_CONSULTA = oModel.MessageValidate;                   //Contacto
                objRequest.TIPO_CLIENTE = oModel.strTipoDocumentoRL;

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateDataMinorCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest, oModel.intSeqIn);
                });

                 if (objResponse != null)
                 {
                     strResult = objResponse.ResultCode;

                     if (strResult == "0")
                     {
                         strResult = UpdateDataCustomerCLF(strIdSession, objRequest);
                     }
                 }
                      
            }catch(Exception ex){
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                strResult = "-1";
            }

            return strResult;
        }

        public string UpdateDataCustomerCLF(string strIdSession, Client objRequest){
            
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado =""; 
            try{

                string strFlgRegistrado = "1";
                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit2= App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                {
                    audit = audit2,
                    vPhone = objRequest.TELEFONO_REFERENCIA_1,
                    vAccount = string.Empty,
                    vContactobjid1 = string.Empty,
                    vFlagReg = strFlgRegistrado
                };
                objCustomerResponse =  Claro.Web.Logging.ExecuteMethod(() =>
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
                                
                
            }catch(Exception ex){
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
            }
            return resultado;
        }

public string UpdateDataCustomerPClub(string strIdSession, Model.Postpaid.ChangeDataModel oModel, string DniRucOld)
        {
            
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string resultado =""; 
            int intTipoCliente = 0;
            //bool retorno = false;
            try{
                 Client objRequest = new Client();
                
                if(oModel.strFlagPlataforma == "C"){
                    intTipoCliente = 1;
                }else{
                    if (oModel.strTipoCliente.ToUpper() == "Consumer")
                    {
                        intTipoCliente = 2;
                    }                    
                }
                
                objRequest.TIPO_CLIENTE = intTipoCliente.ToString();
                objRequest.CUSTOMER_ID = oModel.strCuenta;
                objRequest.CUENTA = oModel.strCuenta;
                objRequest.DNI_RUC = DniRucOld;
                objRequest.TIPO_DOC = oModel.strTipoDocumento;

                if (DniRucOld.Length == 11)
                {
                    var valor = DniRucOld.Substring(0, 2);
                    if(valor == "20"){
                        objRequest.NOMBRES = oModel.strRazonSocial;
                        objRequest.APELLIDOS = oModel.strRazonSocial;
                    }
                    if(valor =="10"){
                        objRequest.NOMBRES = oModel.strNombres;
                        objRequest.APELLIDOS = oModel.strApellidos;
                    }
                }else{
                     objRequest.NOMBRES = oModel.strNombres;
                     objRequest.APELLIDOS = oModel.strApellidos;
                }
                objRequest.USUARIO = audit.userName;

                

                 resultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.UpdateDataCustomerPClub(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
                });

                
                                
                
            }catch(Exception ex){
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                resultado = "-1";
            }
            return resultado;
        }


        //public int registrarTransaccionSiga(string strIdSession, string strDireccion, Model.Postpaid.ChangeDataModel oModel){
            
        //    CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
        //    int resultado = 0; 
        //    try{
        //         TransactionSiga objRequest = new TransactionSiga();
                
        //        objRequest.MSISDN = oModel.strTelefono;
        //        objRequest.MOTIVO_APADECE = Convert.ToInt(ConfigurationManager.AppSettings("strMotivoApadeceNS"));
        //        objRequest.FLAG_EQUIPO = Claro.Constants.NumberOneNegative;
        //        objRequest.ESTADO_ACUERDO = Claro.Constants.NumberOneNegative;
        //        objRequest.ESTADO_APADECE = Claro.Constants.NumberOneNegative;
        //        objRequest.MONTO_FIDELIZA = Claro.Constants.NumberOneNegative;
        //        objRequest.COD_TIPO_OPERACION = ConfigurationManager.AppSettings("strNSTipoOpeCT");
        //        objRequest.DIRECCION_CLIENTE = strDireccion;
        //        objRequest.FUENTE_ACTUALIZACI = ConfigurationManager.AppSettings("strSIACPONS");
        //        objRequest.NOMBRE_CLIENTE = oModel.strNombres.Trim() + " " + oModel.strApellidos.Trim();
        //        objRequest.NRO_DOC_CLIENTE = oModel.DNI_RUC;
        //        objRequest.RAZON_SOCIAL = oModel.strRazonSocial;
        //        objRequest.NRO_DOC_PAGO = null;
                

        //         resultado = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
        //        {
        //            return oServicePostpaid.registrarTransaccionSiga(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, objRequest);
        //        });
                                
                
        //    }catch(Exception ex){
        //        Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
        //        resultado = -1;
        //    }
        //    return resultado;
        //}

public string UpdateAddressCustomer(Model.Postpaid.AddressCustomerModel oModel,string TIPDOCOLD,string DNIRUCOLD)
        {
            string strResult = string.Empty;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.strSessionId);
            PostTransacService.UpdateChangeDataResponse objResponse = new PostTransacService.UpdateChangeDataResponse();

            try{
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
                 //Nuevos Requests
                 oCliente.TIPO_DOC = "0";      //Old
                 oCliente.DNI_RUC = DNIRUCOLD; //Old

                 objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                 {
                    return oServicePostpaid.UpdateAddressCustomer(audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, oCliente, oModel.strTipo, oModel.intSeqIn);
                 });

                 if (objResponse != null)
                 {
                     strResult = objResponse.ResultCode;
                 }                
                
            }catch(Exception ex){
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
                strResult = "-1";
            }

            return strResult;
        }
        
public List<string> SaveInteraction(ref Model.Postpaid.ChangeDataModel oModel, Model.Postpaid.AddressCustomerModel oModelD, Model.Postpaid.AddressCustomerModel oModelF, DataCustomerResponsePostPaid DataOld, int FlagD, int FlagF)
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

                oModel.oChangeDataTemplate = oPlantillaDat;

                //Datos Antiguos del Cliente                
                if (DataOld.Cliente.DNI_RUC != "")
                {
                    if (DataOld.Cliente.DNI_RUC.Length == 11)
                    {
                        if (DataOld.Cliente.DNI_RUC.Substring(0, 2) != "10")
                        {
                            oPlantillaDat.X_ADJUSTMENT_REASON = DataOld.Cliente.RAZON_SOCIAL;
                            oPlantillaDat.X_OPERATION_TYPE = DataOld.Cliente.CONTACTO_CLIENTE;
                        }
                        else {
                            oPlantillaDat.X_ADJUSTMENT_REASON = DataOld.Cliente.NOMBRES + " " + DataOld.Cliente.APELLIDOS;
                            oPlantillaDat.X_OPERATION_TYPE = DataOld.Cliente.NOMBRES + " " + DataOld.Cliente.APELLIDOS;
                        }
                    }
                    else { 
                        oPlantillaDat.X_ADJUSTMENT_REASON = DataOld.Cliente.NOMBRES + " " + DataOld.Cliente.APELLIDOS;
                        oPlantillaDat.X_OPERATION_TYPE = DataOld.Cliente.NOMBRES + " " + DataOld.Cliente.APELLIDOS;
                    }
                }                
                oPlantillaDat.X_TYPE_DOCUMENT = DataOld.Cliente.TIPO_DOC;
                if ((oModel.strFlagPlataforma == "TOBE" || oModel.strFlagPlataforma == "CBIO") && DataOld.Cliente.DNI_RUC.Length == 11 && DataOld.Cliente.DNI_RUC.Substring(0, 2) == "20")
                {
                    try
                    {
                        request objRequest = new request()
                        {
                            obtenerDatosClienteRequest = new obtenerDatosClienteRequestType()
                            {
                                custCode = "",
                                dnNum = oModel.strPhone
                            }
                        };

                        ColivingTransacService.AuditRequest1 auditt = App_Code.Common.CreateAuditRequest<ColivingTransacService.AuditRequest1>(oModel.strIdSession);

                        ResponseCBIO objResponse = new ResponseCBIO();

                        objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                        {
                            return oServiceColiving.getDataCustomerCBIO(auditt, objRequest);
                        });
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "objResponse.replegal: " + objResponse.replegal);
                        if (oModel.DNIRUCNew.Length != 11)
                        {
                            oPlantillaDat.X_MODEL = repLegAntiguo;
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "repleginteraccion.replegal: " + oPlantillaDat.X_MODEL);
                        }
                        else {
                            oPlantillaDat.X_MODEL = objResponse.replegal;
                            Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "RepreLegalGet: " + objResponse.replegal);
                        }
                        oModel.NameRepLegalCurrent = objResponse.replegal;
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, ex.Message);
                    }
                }
                else {
                    oPlantillaDat.X_MODEL = repLegAntiguo; //DataOld.Cliente.REPRESENTANTE_LEGAL;
                    Claro.Web.Logging.Info(oModel.strIdSession, oModel.strIdSession, "repleginteraccionelse.replegal: " + oPlantillaDat.X_MODEL);
                }
                oPlantillaDat.X_OLD_FIXED_PHONE = DataOld.Cliente.NRO_DOC;
                oPlantillaDat.X_ZIPCODE = DataOld.Cliente.DNI_RUC;
                oPlantillaDat.X_OLD_LDI_NUMBER = oModel.DNI_RUC;//num doc old/new
                oPlantillaDat.X_ICCID = oModel.strTxtTipoDocumento;//tip doc old/new

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
                //objTemplateInteraction.X_CLARO_NUMBER = oModel.strPhone;
                objTemplateInteraction.X_BASKET = DataOld.Cliente.CONTRATO_ID;
                objTemplateInteraction.TIENE_DATOS = "S";

                if (ValidateChangeIdPer(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_8 = 1;

                    if (oModel.strTxtTipoDocumento == "RUC")
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
                    else {
                        objTemplateInteraction.X_OLD_CLARO_LDN3 = DataOld.Cliente.TIPO_DOC;
                        objTemplateInteraction.X_CLARO_LDN3 = DataOld.Cliente.TIPO_DOC;
                    }

                    if (oModel.DNI_RUC != DataOld.Cliente.DNI_RUC)
                    {
                        objTemplateInteraction.X_OLD_DOC_NUMBER = DataOld.Cliente.DNI_RUC;
                        objTemplateInteraction.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                    }

                }
                else { objTemplateInteraction.X_INTER_8 = 0; }


                if (ValidateChangeContC(oModel, DataOld))
                {
                    objTemplateInteraction.X_INTER_9 = 1;
                    if (!string.IsNullOrEmpty(oModel.strMovil) && oModel.strMovil.EndsWith("|")) oModel.strMovil = oModel.strMovil.Substring(0, oModel.strMovil.Length - 1);

                    if (oModel.strMovil != DataOld.Cliente.TELEFONO_CONTACTO)
                    {                        

                        //if (oModel.strMovil != "vacio")
                        //{
                        if ((oModel.strMovil == "" || oModel.strMovil == null) && (DataOld.Cliente.TELEFONO_CONTACTO != "" || DataOld.Cliente.TELEFONO_CONTACTO != null))
                        {
                            objTemplateInteraction.X_OLD_CLAROLOCAL1 = DataOld.Cliente.TELEFONO_CONTACTO;
                            objTemplateInteraction.X_CLAROLOCAL1 = "ELIMINADO";
                        }
                        else if ((DataOld.Cliente.TELEFONO_CONTACTO == "" || DataOld.Cliente.TELEFONO_CONTACTO == null) && (oModel.strMovil != "" || oModel.strMovil != null))
                        {
                            objTemplateInteraction.X_OLD_CLAROLOCAL1 = "NINGUNO";
                            objTemplateInteraction.X_CLAROLOCAL1 = oModel.strMovil;

                        }
                        else
                        {
                            objTemplateInteraction.X_OLD_CLAROLOCAL1 = DataOld.Cliente.TELEFONO_CONTACTO;
                            objTemplateInteraction.X_CLAROLOCAL1 = oModel.strMovil;
                        }

                        //  }
                    }
                    if (!string.IsNullOrEmpty(oModel.strTelefono) && oModel.strTelefono.EndsWith("|")) oModel.strTelefono = oModel.strTelefono.Substring(0, oModel.strTelefono.Length - 1);

                    //Nuevo Cambio RRLL
                    if (oModel.strTelefono == null) oModel.strTelefono = "";
                    if (DataOld.Cliente.TELEF_REFERENCIA == null) DataOld.Cliente.TELEF_REFERENCIA = "";
                    if (oModel.strTelefono != DataOld.Cliente.TELEF_REFERENCIA)
                    {
                        if ((oModel.strTelefono == "" || oModel.strTelefono == null)  && (DataOld.Cliente.TELEF_REFERENCIA != "" || DataOld.Cliente.TELEF_REFERENCIA != null))
                        {
                            objTemplateInteraction.X_REFERENCE_PHONE = DataOld.Cliente.TELEF_REFERENCIA;
                            objTemplateInteraction.X_CLARO_NUMBER = "ELIMINADO";
                        }
                        else if ((DataOld.Cliente.TELEF_REFERENCIA == "" || DataOld.Cliente.TELEF_REFERENCIA == null)  && (oModel.strTelefono != "" || oModel.strTelefono != null))
                        {
                            objTemplateInteraction.X_REFERENCE_PHONE = "NINGUNO";
                            objTemplateInteraction.X_CLARO_NUMBER = oModel.strTelefono;
                        }
                        else { 
                            objTemplateInteraction.X_REFERENCE_PHONE = DataOld.Cliente.TELEF_REFERENCIA;
                            objTemplateInteraction.X_CLARO_NUMBER = oModel.strTelefono;
                        }                        
                    }
                    if (!string.IsNullOrEmpty(oModel.strMail) && oModel.strMail.EndsWith("|")) oModel.strMail = oModel.strMail.Substring(0, oModel.strMail.Length - 1);


                    if (oModel.strMail != DataOld.Cliente.EMAIL)
                    {                        
                        if ((oModel.strMail == "" || oModel.strMail == null) && (DataOld.Cliente.EMAIL != "" || DataOld.Cliente.EMAIL != null))
	                    {
		                    objTemplateInteraction.X_LOT_CODE = DataOld.Cliente.EMAIL;
                            objTemplateInteraction.X_CLAROLOCAL2 = "ELIMINADO";
	                    }else if((DataOld.Cliente.EMAIL == "" || DataOld.Cliente.EMAIL == null) && (oModel.strMail != "" || oModel.strMail != null)){
                            objTemplateInteraction.X_LOT_CODE = "NINGUNO";
                            objTemplateInteraction.X_CLAROLOCAL2 = oModel.strMail;
                        }else{
                            objTemplateInteraction.X_LOT_CODE = DataOld.Cliente.EMAIL;
                            objTemplateInteraction.X_CLAROLOCAL2 = oModel.strMail;
                        }
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

                    if (oModel.strTxtTipoDocumentoRL != DataOld.Cliente.TIPO_DOC_RL)
                    {
                        objTemplateInteraction.X_POSITION = DataOld.Cliente.TIPO_DOC_RL;// tipo doc rep legal antiguo
                        objTemplateInteraction.X_OST_NUMBER = oModel.strTxtTipoDocumentoRL;// tipo doc rep legal nuevo
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

        public JsonResult GetDataBilling(ColivingTransacService.GetDataBillingRequest objRequest, string strIdSession, string strUser)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : GetDataBilling");

            ColivingTransacService.GetDataBillingResponse objresponse = null;
            ColivingTransacService.DataBillingRequest oRequest = new ColivingTransacService.DataBillingRequest();

            objRequest.obtenerDatosFacturacionRequestType.indicador = ConfigurationManager.AppSettings("strIndicadorBilling");
            objRequest.obtenerDatosFacturacionRequestType.modo = ConfigurationManager.AppSettings("strModoBilling");
            objRequest.obtenerDatosFacturacionRequestType.listaOpcional = new List<ListOptional>(){
                       new ListOptional(){
                            clave = string.Empty,
                            valor = string.Empty
                       }
                    };
            oRequest.oDataBillingRequest = new GetDataBillingRequest();
            oRequest.IdTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss");
            oRequest.MsgId = strUser;
            oRequest.TimesTamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            oRequest.UserId = strUser;
            oRequest.oDataBillingRequest = objRequest;

            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServiceColiving.GetDataBilling(oRequest, strIdSession, audit.transaction);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { data = objresponse });
        }

        public JsonResult DataRepreLegal(string strNumDocumento, string strTipoDocumento, string strIdSession)
        {           
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "GetParticipante entro!!!!!!!!!!!!");
            RequestCUParticipante objRequest = new RequestCUParticipante()
            {
                numeroDocumento = strNumDocumento,
                tipoDocumento = strTipoDocumento
            };

            Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.HeaderToBe objHeader = new Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.HeaderToBe()
            {
                IdTransaccion = audit.transaction,
                IpAplicacion = audit.ipAddress,
                UserId = audit.userName,
                MsgId = audit.userName
            };
            Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("objRequest=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest)));
            ResponseCUParticipante objResponse = Claro.Web.Logging.ExecuteMethod(() => { return oServiceColiving.GetCuParticipante(objRequest, objHeader); });

            Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("objPresponse=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objResponse)));

            var listareprel = new List<Claro.SIACU.Web.WebApplication.Transac.Service.ColivingTransacService.listaRepresentanteLegal>();
            if ((objResponse.participante != null) && (objResponse.participante[0].listaRepresentanteLegal != null))
            {
                objResponse.participante[0].listaRepresentanteLegal = objResponse.participante[0].listaRepresentanteLegal.FindAll(e => e.cuRepstatus == "1");
                listareprel = objResponse.participante[0].listaRepresentanteLegal.Skip(Math.Max(0, objResponse.participante[0].listaRepresentanteLegal.Count - 5))
                                                                                 .Select(x=> new listaRepresentanteLegal {cuReptipdoc=x.cuReptipdoc, cuRepnumdoc = x.cuRepnumdoc, cuRepnombres = x.cuRepnombres, cuRepapepat=x.cuRepapepat, cuRepapemat=x.cuRepapemat, cuRepstatus=x.cuRepstatus })
                                                                                 .ToList();

            }
            return Json(new { data = listareprel});
        }
        public int cantHist { get; set; }
        public string repLegAntiguo { get; set; }
    }
}