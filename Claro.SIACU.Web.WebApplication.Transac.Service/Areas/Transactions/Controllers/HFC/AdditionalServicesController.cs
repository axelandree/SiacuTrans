using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.Web;
using Org.BouncyCastle.Asn1;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using Helper = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using AutoMapper;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class AdditionalServicesController : CommonServicesController
    {
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private static string _strNumberPhone = string.Empty;
        
        public ActionResult HfcAdditionalServices()
        {
            var msg = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "HFCAdditionalServices", "Iniciando Servicios Adicionales");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg);
            return View();
        }

        public ActionResult HfcGetDevices()
        {
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult HfcGetCommercialSercices(string strIdSession, string strCoId)
        {
            var lstFinal = new List<Helper.CommercialServiceHP>();
            try
            {
                var lstCommertialServices = HfcGetAdditionalServices(strCoId);
                var lstPlanServices = HfcGetPlanServices(strCoId);
                var valueXmlIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                var valueXmlConstArcjivo = FunctionsSIACU.GetValueFromConfigFile("strNomServVODADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                var valueXmlFiltroPvu = FunctionsSIACU.GetValueFromConfigFile("strFiltroPVUTRADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")); 
                
                var valueIgv = valueXmlIgv;

                for (int i = 0; i < lstCommertialServices.Count; i++)
                {
                    for (int j = 0; j < lstPlanServices.Count; j++)
                    {
                        if (lstCommertialServices[i].SNCODE.Equals(lstPlanServices[j].SNCode) && lstCommertialServices[i].SPCODE.Equals(lstPlanServices[j].SPCode))
                        {
                            lstCommertialServices[i].COSTOPVU = String.Format("{0:0.00}", Double.Parse(lstPlanServices[j].CF));
                            lstCommertialServices[i].VALORPVU = lstPlanServices[j].DesCodigoExterno;
                            lstCommertialServices[i].DESCOSER = lstPlanServices[j].DesServSisact;
                            lstCommertialServices[i].TIPO_SERVICIO = lstPlanServices[j].TipoServ;
                        }

                        if (lstPlanServices[j].DesServSisact.Equals(valueXmlConstArcjivo))
                        {
                            lstCommertialServices[i].TIPOSERVICIO = "VOD";
                        }
                        else
                        {
                            lstCommertialServices[i].TIPOSERVICIO = "CANAL";
                        }
                    }
                }

                if (valueXmlFiltroPvu.Equals(ConstantsHFC.PresentationLayer.NumeracionUNO))
                {
                    for (int i = 0; i < lstCommertialServices.Count; i++)
                    {
                        if (!lstCommertialServices[i].COSTOPVU.Equals(ConstantsHFC.PresentationLayer.gstrNoInfo))
                        {
                            lstFinal.Add(lstCommertialServices[i]);
                        }
                    }
                }
                else
                {
                    return new JsonResult
                    {
                        Data = lstCommertialServices,
                        ContentType = "application/json",
                        ContentEncoding = Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strIdSession, e.Message);
            }            

            return new JsonResult
            {
                Data = lstFinal,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        public string HfcGetCommertialPlan(string strCoId)
        {
            var planCode = string.Empty;
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var objCommercialPlanRequest = new CommertialPlanRequest
            {
                audit = objAuditRequest,
                StrCoId = strCoId
            };
            try
            {
                var objCommercialPlanResponse = Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommertialPlan(objCommercialPlanRequest); });
                if (!string.IsNullOrEmpty(objCommercialPlanResponse.rCodigoPlan))
                {
                    planCode = objCommercialPlanResponse.rCodigoPlan;
                }
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialPlanRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return planCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        public List<Helper.PlanServiceHP> HfcGetPlanServices(string strCoId)
        {
            var objLstPlanService = new List<Helper.PlanServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var planCode = HfcGetCommertialPlan(strCoId);

            var valueFilterDecoCode = ConfigurationManager.AppSettings("strFilterDecoCodeHFC");

            var objPlanServiceRequest = new PlanServicesRequest()
            {
                audit = objAuditRequest,
                IdPlan = planCode,
                TypeProduct = ConfigurationManager.AppSettings("strProductoHFC")
            };
            try
            {
                var objPlanServiceResponse = Logging.ExecuteMethod(() => { return _oServiceFixed.GetPlanServices(objPlanServiceRequest); });

                if (objPlanServiceResponse.LstPlanServices.Count > 0)
                {
                    var lstTemp = objPlanServiceResponse.LstPlanServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Helper.PlanServiceHP
                        {
                            CF = item.CF ?? "",
                            CantidadEquipo = item.CantidadEquipo ?? "",
                            CodGrupoServ = item.CodGrupoServ ?? "",
                            CodServSisact = item.CodServSisact ?? "",
                            CodTipoServ = item.CodTipoServ ?? "",
                            CodigoExterno = item.CodigoExterno ?? "",
                            CodigoPlan = item.CodigoPlan ?? "",
                            DesCodigoExterno = item.DesCodigoExterno ?? "",
                            DesServSisact =  item.DesServSisact ?? "",
                            DescPlan = item.DescPlan ?? "",
                            Equipo = item.Equipo ?? "",
                            GrupoServ = item.GrupoServ ?? "",
                            IdEquipo = item.IdEquipo ?? "",
                            MatvDesSap = item.MatvDesSap ?? "",
                            MatvIdSap = item.MatvIdSap ?? "",
                            SNCode = item.SNCode ?? "",
                            SPCode = item.SPCode ?? "",
                            ServvUsuarioCrea = item.ServvUsuarioCrea ?? "",
                            Solucion = item.Solucion ?? "",
                            TipoEquipo = item.TipoEquipo ?? "",
                            TipoServ = item.TipoServ ?? "",
                            TmCode = item.TmCode ?? ""
                        };

                        if (string.IsNullOrEmpty(valueFilterDecoCode))
                        {
                            objLstPlanService.Add(objTemp);
                        }
                        else
                        {
                            if (!valueFilterDecoCode.Contains("|" + objTemp.CodGrupoServ + "|"))
                            {
                        objLstPlanService.Add(objTemp);
                    }
                }
            }
                }
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objPlanServiceRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objLstPlanService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        public List<Helper.CommercialServiceHP> HfcGetAdditionalServices(string strCoId)
        {
            var objLstCommercialService = new List<Helper.CommercialServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");

            var objCommercialServicesRequest = new CommercialServicesRequest
            {
                audit = objAuditRequest,
                StrCoId = strCoId
            };
            try
            {
                var objCommercialServicesResponse = Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommercialService(objCommercialServicesRequest); });
                if (objCommercialServicesResponse.LstCommercialServices.Count > 0)
                {
                    var lstTemp = objCommercialServicesResponse.LstCommercialServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Helper.CommercialServiceHP
                        {
                            DE_SER = item.DE_SER ?? "",
                            DE_EXCL = item.DE_EXCL ?? "",
                            BLOQ_ACT = item.BLOQ_ACT ?? "",
                            BLOQ_DES = item.BLOQ_DES ?? "",
                            CARGOFIJO = item.CARGOFIJO ?? "",
                            CODSERPVU = item.CODSERPVU ?? "",
                            COSTOPVU = item.COSTOPVU ?? "",
                            CO_EXCL = item.CO_EXCL ?? "",
                            CO_SER = item.CO_SER ?? "",
                            CUOTA = item.CUOTA ?? "",
                            DESCOSER = item.DESCOSER ?? "",
                            DESCUENTO = item.DESCUENTO,
                            DE_GRP = item.DE_GRP ?? "",
                            ESTADO = item.ESTADO ?? "",
                            NO_GRP = item.NO_GRP ?? "",
                            NO_SER = item.NO_SER ?? "",
                            PERIODOS = item.PERIODOS ?? "",
                            SNCODE = item.SNCODE ?? "",
                            SPCODE = item.SPCODE ?? "",
                            SUSCRIP = item.SUSCRIP ?? "",
                            TIPOSERVICIO = item.TIPOSERVICIO ?? "",
                            TIPO_SERVICIO = item.TIPO_SERVICIO ?? "",
                            VALIDO_DESDE = item.VALIDO_DESDE ?? "",
                            VALORPVU = item.VALORPVU ?? ""
                        };

                        objLstCommercialService.Add(objTemp);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objLstCommercialService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="nroCelular"></param>
        /// <param name="cadenaOpciones"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StartPage_HFC(string strIdSession, string nroCelular, string cadenaOpciones)
        {
            var hidTelefono = GetNumber(strIdSession, false, nroCelular);
            _strNumberPhone = hidTelefono;
            var dictionaryStartPageHfc = new Dictionary<string, object>
            {
                { "hdnTelefono", hidTelefono }
               
            };

            dictionaryStartPageHfc.Add("chkCampana",
                cadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strCodSegHabilitaCheckCampana_HFC"),
                    StringComparison.OrdinalIgnoreCase) + 1 <= 0);

            dictionaryStartPageHfc.Add("gstrTransaccionDTHTACTDESSER", ConstantsHFC.ADDITIONALSERVICESHFC.gstrTransaccionDTHTACTDESSER);
            dictionaryStartPageHfc.Add("gConstTipoHFC", ConfigurationManager.AppSettings("gConstTipoHFC"));

            return new JsonResult
            {
                Data = dictionaryStartPageHfc,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="estadoLinea"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PageLoad_HFC(string strIdSession, string estadoLinea)
        {
            var dictionaryPageLoad = new Dictionary<string, object>();
            try
            {            
                if (Request.IsAjaxRequest())
                {

                    dictionaryPageLoad.Add("EstadoLinea", StatusLineValidate(strIdSession, 5, estadoLinea));
                    dictionaryPageLoad.Add("hdnValorIGV", GetCommonConsultIgv(strIdSession).igvD.ToString(CultureInfo.InvariantCulture));
                    dictionaryPageLoad.Add("hdnSiteUrl", ConfigurationManager.AppSettings("strRutaSiteInicio"));
                    dictionaryPageLoad.Add("hdnTituloPagina", FunctionsSIACU.GetValueFromConfigFile("strMsgTituloActDesacServComer", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    dictionaryPageLoad.Add("gstrTransaccionDTHTACTDESSER", ConstantsHFC.ADDITIONALSERVICESHFC.gstrTransaccionDTHTACTDESSER);
                    dictionaryPageLoad.Add("strMensajeTransaccionFTTH", KEY.AppSettings("strMensajeBackOfficeFTTH")); //EVALENZS - INICIO
                    dictionaryPageLoad.Add("strPlanoFTTH", KEY.AppSettings("strPlanoFTTH")); //EVALENZS - FIN 
                }

                Logging.Info(strIdSession, strIdSession, "EstadoLinea : " + dictionaryPageLoad["EstadoLinea"]);
                Logging.Info(strIdSession, strIdSession, "hdnValorIGV : " + dictionaryPageLoad["hdnValorIGV"]);
                Logging.Info(strIdSession, strIdSession, "hdnSiteUrl : " + dictionaryPageLoad["hdnSiteUrl"]);
                Logging.Info(strIdSession, strIdSession, "hdnTituloPagina : " + dictionaryPageLoad["hdnTituloPagina"]);
                Logging.Info(strIdSession, strIdSession, "gstrTransaccionDTHTACTDESSER : " + dictionaryPageLoad["gstrTransaccionDTHTACTDESSER"]);               
            }
            catch (Exception ex)
            {
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strIdSession, ex.Message);
            }

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadTypification_HFC(string strIdSession)
        {
            Models.HFC.AdditionalServiceModel oAdditionalServiceModel = new Models.HFC.AdditionalServiceModel();

            var loadTypification = new Dictionary<string, object>();
            try
            {
                var objServiceTypification = GetTypificationHFC(strIdSession, "TRANSACCION_ACT_DES_SERVICIOS_HFC");
                var tipo = ConfigurationManager.AppSettings("gConstTipoHFC");
                var objTypificationModel = objServiceTypification.Where(x => x.Type.Equals(tipo)).ToList().FirstOrDefault();

                if (objTypificationModel != null && !string.IsNullOrEmpty(objTypificationModel.Class))
                {
                    loadTypification.Add("hidClaseId", objTypificationModel.ClassCode);
                    loadTypification.Add("hidSubClaseId", objTypificationModel.SubClassCode);
                    loadTypification.Add("hidTipo", objTypificationModel.Type);
                    loadTypification.Add("hidClaseDes", objTypificationModel.SubClass);
                    loadTypification.Add("hidClass", objTypificationModel.Class);
                    loadTypification.Add("lblMensajeTxt", "");
                    loadTypification.Add("lblMensajeVis", false);
                    loadTypification.Add("btnGuardar", false);
                    loadTypification.Add("btnConstancia", false);
                }
                else
                {
                    loadTypification.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    loadTypification.Add("lblMensajeVis", true);
                    loadTypification.Add("btnGuardar", true);
                    loadTypification.Add("btnConstancia", true);
                }

                Logging.Info(strIdSession, strIdSession, "hidClaseId : " + loadTypification["hidClaseId"]);
                Logging.Info(strIdSession, strIdSession, "hidSubClaseId : " + loadTypification["hidSubClaseId"]);
                Logging.Info(strIdSession, strIdSession, "hidTipo : " + loadTypification["hidTipo"]);
                Logging.Info(strIdSession, strIdSession, "hidClaseDes : " + loadTypification["hidClaseDes"]);
                Logging.Info(strIdSession, strIdSession, "hidClass : " + loadTypification["hidClass"]);

            }
            catch (Exception e)
            {
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strIdSession, e.Message);
            }

            return new JsonResult
            {
                Data = loadTypification,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaction"></param>
        /// <param name="cargoSinIgv"></param>
        /// <param name="cargoConIgv"></param>
        [HttpPost]
        public void LogTotalFixedCharge(string strIdSession, string strIdTransaction, string cargoSinIgv, string cargoConIgv)
        {
            Logging.Info(strIdSession, strIdTransaction, "Cargo Fijo Total Sin IGV: S/. " + cargoSinIgv + ", Cargo Fijo Total Con IGV: S/. " + cargoConIgv);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveTransaction(Models.HFC.AdditionalServiceModel oModel)
        {
            bool blnResult = false;
            string strCodResult = string.Empty;
            
            Models.HFC.AdditionalServiceModel oAdditionalServicesModel = new Models.HFC.AdditionalServiceModel();
            oModel.strHdnReady = ConstantsHFC.strUno;
            bool bTipification = SaveInteraction(ref oModel);

            try
            {
                if (oModel.strHdnTipoTransaccion.Length < 1)
                {
                    oAdditionalServicesModel.bErrorTransac = true;
                    oAdditionalServicesModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMsgADSCNoSelTran", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    return Json(oAdditionalServicesModel, JsonRequestBehavior.AllowGet);
                }

                if (Functions.GetValueFromConfigFile("strEjecutaTransaccionADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")).Equals(ConstantsHFC.strUno))
                {
                    if (oModel.strHdnTipoTransaccion.Equals(ConstantsHFC.strLetraA))
                    {
                        var responseActivate = ActiveService(oModel);
                        blnResult = responseActivate.BlValues;
                        strCodResult = responseActivate.StrResult ?? string.Empty;
                    }

                    if (oModel.strHdnTipoTransaccion.Equals(ConstantsHFC.strLetraD))
                    {
                        var responseDesactive = DesactiveService(oModel);
                        blnResult = responseDesactive.BlValues;
                        strCodResult = responseDesactive.StrResult ?? string.Empty;
                    }
                }
              
                if (blnResult)
                {

                    if (oModel.strHdnTipoTransaccion.Equals(ConstantsHFC.strLetraA))
                    {
                        Logging.Info(oModel.IdSession, oModel.strTransaction, "ACTIVACION INICIANDO LA AUDITORIA");
                        InsertAudit(true,oModel);
                    }
                    else
                    {
                        Logging.Info(oModel.IdSession, oModel.strTransaction, "DESACTIVACION SE ESTA INICIANDO LA AUDITORIA");
                        InsertAudit(false,oModel);
                    }
                    oAdditionalServicesModel.bErrorTransac = false;
                    oAdditionalServicesModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMsgTranGrabSatis", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    oAdditionalServicesModel.strLabelMessage = Functions.GetValueFromConfigFile("strMsgTranGrabSatis", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    oAdditionalServicesModel.strHdnCaseId = oModel.strHdnCaseId;
                }
                else
                {
                    if (oModel.strHdnCaseId.Length > 1)
                    {
                        UpdatexInter30(oModel.IdSession, oModel.strHdnCaseId,
                            Functions.GetValueFromConfigFile("strMensajeErrorparaNotasClfy",
                                KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg")) + oModel.strTxtNote);
                    }


                    if (strCodResult.Equals(ConstantsHFC.PresentationLayer.NumeracionMenosNueve))
                    {
                        oAdditionalServicesModel.bErrorTransac = false;
                        oAdditionalServicesModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("gConstMsgNPCTPEOTP", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        
                    }
                    else
                    {
                        oAdditionalServicesModel.bErrorTransac = true;
                        oAdditionalServicesModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }

                    oAdditionalServicesModel.strLabelMessage = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
            }
            catch (Exception ex)
            {
                oAdditionalServicesModel.bErrorTransac = true;
                oAdditionalServicesModel.strLabelMessage = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                Logging.Info("Session: " + oModel.IdSession, "Transaction: SaveTransaction AdditionalServices HFC", string.Format("Error: {0}", ex.Message));
                
            }

            return Json(oAdditionalServicesModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="blnActive"></param>
        /// <param name="oModel"></param>
        private void InsertAudit(bool blnActive, Models.HFC.AdditionalServiceModel oModel)
        {
            string strTransactionAudit = ConfigurationManager.AppSettings("gActDesactServiciosComerciales");
            string strService = ConfigurationManager.AppSettings("gConstEvtServicio");
            string strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress);
            string strIpServer = App_Code.Common.GetApplicationIp();
            string strNameServer = App_Code.Common.GetApplicationName();
            string strAmount = Claro.Constants.NumberZeroString;
            string strText = string.Empty;
            string strTextSecondary = string.Empty;
            
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            if (blnActive)
            {
                strTextSecondary = string.Format(
                    "Codigo Contrato: {0}/MSISDN: {1}/Codigo Servicio Comercial: {2}/Nombre Servicio Comercial: {3}/Accion: ProgActiv/CAC o DAC: {4}",
                    oModel.strContractId, oModel.strPhone, oModel.strHdnCoSerSel, oModel.strHdnDesCoSerSel, oModel.strCacDacDescription);
            }
            else
            {
                strTextSecondary = string.Format(
                    "Codigo Contrato: {0}/MSISDN: {1}/Codigo Servicio Comercial: {2}/Nombre Servicio Comercial: {3}/Accion: ProgActiv/CAC o DAC: {4}",
                    oModel.strContractId, oModel.strPhone, oModel.strHdnCoSerSel, oModel.strHdnDesCoSerSel, oModel.strCacDacDescription);
            }
            strText = string.Format("{0}/Ip Cliente: {1}/Usuario: {2}/Id Opcion: {3}/Fecha y Hora: {4}", strTextSecondary, strIpClient, oModel.strAccountUser, ConfigurationManager.AppSettings("strIdOpcionClaroProteccion"), DateTime.Now.ToShortDateString());
            try
            {
                RegisterAudit(audit.Session, audit.transaction, strTransactionAudit, strService, strIpClient, oModel.strFullNameCustomer,
                    strIpServer, strNameServer, oModel.strAccountUser, oModel.strCustomerId, strAmount, strText);
            }
            catch (Exception ex)
            {
                Logging.Info("Session: " + oModel.IdSession, "Transaction: InsertAudit AdditionalServices HFC", string.Format("Error: {0}", ex.Message));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <param name="strAdjunto"></param>
        /// <param name="attachFile"></param>
        /// <param name="strIdSession"></param>
        /// <param name="interactionId"></param>
        private void SendEmail(Models.HFC.AdditionalServiceModel oModel, string strAdjunto, byte[] attachFile, string strIdSession, string interactionId, string strAccion)
        { 
            string strMessage = string.Empty;

            try
            {
                var objDataTemplateInteraction = GetInfoInteractionTemplate(oModel.IdSession, interactionId);
                string strAttached = string.Empty;
                string strSender = Functions.GetValueFromConfigFile("CorreoServicioAlCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                string strTo = oModel.strTxtSendEmail;
                string strSubject = "Variación de Servicio";

                if (strAccion == "Activación")
                {
                    strAccion = "Activación de Servicios";
                }
                else
                {
                    strAccion = "Desactivación de Servicios";
                }

                strMessage = "<html>";
                strMessage += "<head>";
                strMessage += "<style type='text/css'>";
                strMessage += "<!--";
                strMessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "-->";
                strMessage += "</style>";
                strMessage += "<body>";
                strMessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "<tr><td>";
                strMessage += MailHeader(strAccion,
                    objDataTemplateInteraction.X_INTER_15, 
                    DateTime.Now.ToShortDateString(), 
                    oModel.strFullNameCustomer,
                    oModel.strHdnCaseId,
                    oModel.strLegalRepresent, 
                    oModel.strCustomerId, 
                    oModel.strDocumentType, 
                    oModel.strNumberDocument);
                strMessage += "</td></tr>";

                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
                strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "<tr><td height='10'></td>";
                strMessage += "<tr><td height='10'></td>";
                strMessage +=
                    "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
                strMessage += "</table>";
                strMessage += "</body>";
                strMessage += "</html>";

                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

                SendEmailResponseCommon objGetSendEmailResponse = new SendEmailResponseCommon();
                CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                SendEmailRequestCommon objGetSendEmailRequest =
                    new SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strTo,
                        strMessage = strMessage,
                        strAttached = strAdjunto,
                        strSubject = strSubject,
                        AttachedByte = attachFile
                    };
                try
                {
                    objGetSendEmailResponse = Logging.ExecuteMethod<SendEmailResponseCommon>
                    (
                        () => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); }
                    );

                    Logging.Info("666", "666", "INFO EMAIL CONTROLLER ACTIVACION DESACTIVACION : " + objGetSendEmailResponse.Exit);
                }
                catch (Exception ex)
                {
                    Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Logging.Info("Session: " + oModel.IdSession, "Transaction: SendEmail AdditionalServices HFC", string.Format("Error: {0}", ex.Message));
            }
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private bool SaveInteraction(ref Models.HFC.AdditionalServiceModel oModel)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            string strInteractionId = string.Empty;
            string strFlagInsert = string.Empty;
            string strMsgText = string.Empty;
            string strFlagInsertInteraction = string.Empty;
            string strMsgTextInteraction = string.Empty;
            string strCodReturntransaction = string.Empty;
            string strMessageErrorTransaction = string.Empty;

            var saveInteraction = new Dictionary<string, object>();
            try
            {
                string strCustomerId = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.strCustomerId;
                if (!GetValidateCustomerIdCustom(audit.Session, audit.transaction, strCustomerId, oModel))
                {
                    saveInteraction.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("gConstKeyNoValidaCustomerID", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    saveInteraction.Add("lblMensajeVis", true);
                }

                Helper.BEInteraction objInteraction = DataInteraction(audit.Session, audit.transaction,oModel);
                bool blnExecuteTransaction = true;
                string strUserSystem = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                string strUserApp = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                string strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                bool blnValidate;
                Helper.BETemplateInteraction objTemplateInteraction = DataTemplateInteraction(audit.Session, audit.transaction,oModel, out blnValidate);

                if (blnValidate == false)
                {
                    saveInteraction.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("strNoTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    saveInteraction.Add("lblMensajeVis", true);
                    return false;
                }

                Insert(audit.Session, audit.transaction, objInteraction, objTemplateInteraction, strCustomerId, strUserSystem, strUserApp, strPassUser, blnExecuteTransaction,
                    out strInteractionId,
                    out strFlagInsert,
                    out strMsgText,
                    out strFlagInsertInteraction,
                    out strMsgTextInteraction,
                    out strCodReturntransaction,
                    out strMessageErrorTransaction);

                oModel.strHdnCaseId = strInteractionId;

                if ((strFlagInsert != ConstantsHFC.Message_OK) & strFlagInsert != ConstantsSiacpo.ConstVacio)
                {
                    saveInteraction.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    saveInteraction.Add("lblMensajeVis", true);
                    return false;
                }

                if ((strFlagInsertInteraction != ConstantsHFC.Message_OK) & strFlagInsertInteraction != ConstantsSiacpo.ConstVacio)
                {
                    saveInteraction.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    saveInteraction.Add("lblMensajeVis", true);                  
                    return false;
                }
            }
            catch (Exception ex)
            {
                saveInteraction.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                saveInteraction.Add("lblMensajeVis", true);
                Logging.Error(oModel.IdSession, oModel.strTransaction, ex.Message);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objInteraction"></param>
        /// <param name="objTemplateInteraction"></param>
        /// <param name="strPhoneNumber"></param>
        /// <param name="strUserSystem"></param>
        /// <param name="strUserApp"></param>
        /// <param name="strPassUser"></param>
        /// <param name="blnExecuteTransaction"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strFlagInsert"></param>
        /// <param name="strMsgText"></param>
        /// <param name="strFlagInsertInteraction"></param>
        /// <param name="strMsgTextInteraction"></param>
        /// <param name="strCodReturntransaction"></param>
        /// <param name="strMessageErrorTransaction"></param>
        /// <returns></returns>
        private bool Insert(string strIdSession, 
            string strTransaction, 
            Helper.BEInteraction objInteraction, 
            Helper.BETemplateInteraction objTemplateInteraction,
            string strPhoneNumber, 
            string strUserSystem, string strUserApp, 
            string strPassUser, bool blnExecuteTransaction,
            out string strInteractionId, 
            out string strFlagInsert, 
            out string strMsgText,
            out string strFlagInsertInteraction,
            out string strMsgTextInteraction,
            out string strCodReturntransaction, 
            out string strMessageErrorTransaction)
        {
            strFlagInsertInteraction = string.Empty;
            strMsgTextInteraction = string.Empty;
            strCodReturntransaction = string.Empty;
            strMessageErrorTransaction = string.Empty;
            strInteractionId = string.Empty;
            strFlagInsert = string.Empty;
            strMsgText = string.Empty;

            bool blnResult = false;

            try
            {
                string contingencyClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
                string strTelephone;
                if (strPhoneNumber == objInteraction.TELEFONO)
                {
                    strTelephone = strPhoneNumber;
                }
                else
                {
                    strTelephone = objInteraction.TELEFONO;
                }

                CustomerResponse objConsult = new CustomerResponse();
                objConsult = GetCustomer(strIdSession, strTransaction, strTelephone, objInteraction.OBJID_CONTACTO);

                if (objConsult != null)
                {
                    objInteraction.OBJID_CONTACTO = objConsult.Customer.ContactCode;
                    objInteraction.OBJID_SITE = objConsult.Customer.SiteCode;
                }

                CommonTransacService.InsertInteractHFCResponse objInsert = new CommonTransacService.InsertInteractHFCResponse();
                InsertInteractResponseCommon objInsertInteraction = new InsertInteractResponseCommon();

                if (contingencyClarify != ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableSI)
                {
                    objInsert = GetInsert(strIdSession, strTransaction, objInteraction, out strInteractionId, out strFlagInsert, out strMsgText);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strInteractionId: " + strInteractionId);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strFlagInsert: " + strFlagInsert);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strMsgText: " + strMsgText);
                    blnResult = objInsert.rResult;
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION rResult: " + objInsert.rResult);
                }
                else
                {
                    objInsertInteraction = GetInsertInteraction(strIdSession, strTransaction, objInteraction, out strInteractionId, out strFlagInsert, out strMsgText);
                    blnResult = objInsertInteraction.ProcesSucess;
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strInteractionId: " + strInteractionId);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strFlagInsert: " + strFlagInsert);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strMsgText: " + strMsgText);
                    Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION rResult: " + blnResult);
                }

                if (strInteractionId != string.Empty)
                {
                    if (objTemplateInteraction != null)
                    {
                        InsertTemplateInteraction(strIdSession, strTransaction, objTemplateInteraction, strInteractionId, strPhoneNumber,
                            strUserSystem, strUserApp, strPassUser, blnExecuteTransaction, out strFlagInsertInteraction, out strMsgTextInteraction, out strCodReturntransaction, out strMessageErrorTransaction);
                        Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strFlagInsertInteraction: " + strFlagInsertInteraction);
                        Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strMsgTextInteraction: " + strMsgTextInteraction);
                        Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strCodReturntransaction: " + strCodReturntransaction);
                        Logging.Info(strIdSession, strTransaction, "ADSA ACTIVACION DESACTIVACION strMessageErrorTransaction: " + strMessageErrorTransaction);
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransaction, e.Message);
            }

            return blnResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objTemplateInteraction"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strPhoneNumber"></param>
        /// <param name="strUserSystem"></param>
        /// <param name="strUserApp"></param>
        /// <param name="strPassUser"></param>
        /// <param name="blnExecuteTransaction"></param>
        /// <param name="strFlagInsert"></param>
        /// <param name="strMsgText"></param>
        /// <param name="strCodReturntransaction"></param>
        /// <param name="strMessageErrorTransaction"></param>
        private void InsertTemplateInteraction(string strIdSession, 
            string strTransaction, 
            Helper.BETemplateInteraction objTemplateInteraction, 
            string strInteractionId, 
            string strPhoneNumber, 
            string strUserSystem, 
            string strUserApp, 
            string strPassUser, 
            bool blnExecuteTransaction,
            out string strFlagInsert, 
            out string strMsgText, 
            out string strCodReturntransaction, 
            out string strMessageErrorTransaction)
        {
            string strTransactionOp = FunctionsSIACU.CheckStr(objTemplateInteraction.NOMBRE_TRANSACCION);
            string contingencyClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
           
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            strCodReturntransaction = ConstantsHFC.strCero;
            strMessageErrorTransaction = string.Empty;
            strFlagInsert = string.Empty;
            strMsgText = string.Empty;
            try
            {
                if (contingencyClarify != ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableSI)
                {
                    var serviceModelInteraction = Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objTemplateInteraction);
             
                    InsertTemplateInteractionResponseCommon objResponse = new InsertTemplateInteractionResponseCommon();
                    InsertTemplateInteractionRequestCommon objRequest = new InsertTemplateInteractionRequestCommon()
                    {
                        item = serviceModelInteraction,
                        IdInteraction = strInteractionId,
                        audit = audit
                    };

                    objResponse = Logging
                        .ExecuteMethod(strIdSession,
                            strTransaction,
                            () =>
                            {
                                return _oServiceCommon.GetInsertInteractionTemplate(objRequest);
                            });
                    strFlagInsert = objResponse.FlagInsercion;
                    strMsgText = objResponse.MsgText;
                    Logging.Info(strIdSession, strIdSession, "ADSA ACTIVACION DESACTIVACION  strFlagInsert: " + strFlagInsert);
                    Logging.Info(strIdSession, strIdSession, "ADSA ACTIVACION DESACTIVACION  strMsgText: " + strMsgText);
                }
                else
                {
                    var serviceModelInteraction =
                        Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objTemplateInteraction);
                    InsTemplateInteractionResponseCommon objResponse = null;
                     InsTemplateInteractionRequestCommon objRequest =
                        new InsTemplateInteractionRequestCommon()
                        {
                            item = serviceModelInteraction,
                            IdInteraction = strInteractionId,
                            audit = audit
                        };

                    objResponse = Logging.ExecuteMethod(
                        strIdSession,
                        strTransaction, () =>
                        {
                            return _oServiceCommon.GetInsInteractionTemplate(objRequest);
                        });
                    strFlagInsert = objResponse.FlagInsercion;
                    strMsgText = objResponse.MsgText;
                }

                objTemplateInteraction.ID_INTERACCION = strInteractionId;
                if (!(strTransactionOp != string.Empty && blnExecuteTransaction))
                {
                    strCodReturntransaction = ConstantsHFC.strCero;
                    strMessageErrorTransaction = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);               
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="oModel"></param>
        /// <param name="blnValidate"></param>
        /// <returns></returns>
        private Helper.BETemplateInteraction DataTemplateInteraction(string strIdSession, string strTransaction, Models.HFC.AdditionalServiceModel oModel, out bool blnValidate)
        {
            Helper.BETemplateInteraction objTemplateFieldData = new Helper.BETemplateInteraction();
            blnValidate = true;
            try
            {
                var igvCurrent = GetCommonConsultIgv(strIdSession);
                var igvWithUnit = 0.0;
                if (igvCurrent != null)
                {
                    igvWithUnit = igvCurrent.igvD + 1;
                }

                objTemplateFieldData.NOMBRE_TRANSACCION = ConstantsHFC.ADDITIONALSERVICESHFC.gstrTransaccionDTHTACTDESSER;
                objTemplateFieldData.X_CLARO_NUMBER = _strNumberPhone;
                objTemplateFieldData.X_FIRST_NAME = oModel.strFirstName ?? string.Empty;
                objTemplateFieldData.X_LAST_NAME = oModel.strLastName ?? string.Empty;
                objTemplateFieldData.X_DOCUMENT_NUMBER = oModel.strNumberDocument ?? string.Empty;
                objTemplateFieldData.X_REFERENCE_PHONE = oModel.strReferencePhone ?? string.Empty;
                objTemplateFieldData.X_REASON = oModel.strReasonSocial ?? string.Empty;
                objTemplateFieldData.X_INTER_1 = oModel.strContactClient ?? string.Empty;
                objTemplateFieldData.X_INTER_2 = oModel.strNumberDocument ?? string.Empty;
                objTemplateFieldData.X_INTER_3 = oModel.strHdnTipoTransaccion == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA ? (string.IsNullOrEmpty(oModel.strHdnCostoPVUSel) ? string.Empty : Math.Round(Convert.ToDouble(oModel.strHdnCostoPVUSel) * igvWithUnit, 2).ToString(CultureInfo.InvariantCulture)) : SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;//oModel.strHdnCostoPVUSel ?? string.Empty;
                objTemplateFieldData.X_INTER_4 = oModel.strHdnDesCoSerSel ?? string.Empty;
                objTemplateFieldData.X_INTER_5 = oModel.strHdnCostoBSCS ?? string.Empty;
                objTemplateFieldData.X_INTER_6 = oModel.strBillingCycle ?? string.Empty;
                objTemplateFieldData.X_IMEI = oModel.strHdnTipoTransaccion ?? string.Empty;
                objTemplateFieldData.X_INTER_7 = oModel.strPlan ?? string.Empty;
                objTemplateFieldData.X_INTER_15 = oModel.strCacDacDescription ?? string.Empty;
                objTemplateFieldData.X_INTER_19 = oModel.strHdnTipoTransaccion == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA ?
                    (oModel.strHdnCostoPVUSel ?? string.Empty) : SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
                objTemplateFieldData.X_INTER_20 = oModel.strHdnCargoFijoSel ?? string.Empty;
                objTemplateFieldData.X_INTER_21 = oModel.strHdnCoSerSel ?? string.Empty;
                objTemplateFieldData.X_INTER_29 = oModel.strHdnDesCoSerSel ?? string.Empty;
                objTemplateFieldData.X_INTER_30 = oModel.strTxtNote ?? string.Empty;
                objTemplateFieldData.X_INTER_25 = FunctionsSIACU.CheckDbl(oModel.strBillingCycle);
                objTemplateFieldData.X_OPERATION_TYPE = oModel.strHdnTipoTransaccion ?? string.Empty;
                objTemplateFieldData.X_ADJUSTMENT_REASON = oModel.strContractId ?? string.Empty;
                objTemplateFieldData.X_INTER_8 = Convert.ToDouble(oModel.strCustomerId);
                objTemplateFieldData.X_TYPE_DOCUMENT = oModel.strCustomerType;
                objTemplateFieldData.X_LASTNAME_REP = oModel.strDocumentType.ToUpper();

                if (oModel.strCustomerType == ConstantsHFC.PresentationLayer.gstrConsumer)
                {
                    objTemplateFieldData.X_INTER_16 = oModel.strFullNameCustomer;
                    objTemplateFieldData.X_NAME_LEGAL_REP = oModel.strFullNameCustomer;
                    objTemplateFieldData.X_OLD_LAST_NAME = oModel.strNumberDocument;
                }
                else
                {
                    objTemplateFieldData.X_INTER_16 = oModel.strNameClient;
                    objTemplateFieldData.X_NAME_LEGAL_REP = string.IsNullOrEmpty(oModel.strLegalRepresent) ? oModel.strFullNameCustomer : oModel.strLegalRepresent;
                    objTemplateFieldData.X_OLD_LAST_NAME = oModel.strDniRuc;
                }

                if (oModel.strFlagChkSendEmail == "T")
                {
                    objTemplateFieldData.X_CLAROLOCAL4 = ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableSI;
                    objTemplateFieldData.X_EMAIL = oModel.strTxtSendEmail ?? string.Empty;
                }
                else
                {
                    objTemplateFieldData.X_CLAROLOCAL4 = ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableNO;
                }

                if (oModel.strFlagChkProgramming == "T")
                {
                    objTemplateFieldData.X_CLAROLOCAL5 = ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableSI;
                    objTemplateFieldData.X_CLAROLOCAL6 = oModel.strDateProgramation;
                }
                else
                {
                    objTemplateFieldData.X_CLAROLOCAL5 = ConstantsHFC.ADDITIONALSERVICESHFC.gstrVariableNO;
                    objTemplateFieldData.X_CLAROLOCAL6 = DateTime.Now.ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);               
            }

            return objTemplateFieldData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private Helper.BEInteraction DataInteraction(string strIdSession, string strTransaction, Models.HFC.AdditionalServiceModel oModel)
        {
            Helper.BEInteraction objInteraction = new Helper.BEInteraction();
            string strHidType = ConfigurationManager.AppSettings("gConstTipoHFC");
            string strCustomerId = KEY.AppSettings("gConstKeyCustomerInteract","") + oModel.strCustomerId;
            try
            {
                objInteraction.OBJID_CONTACTO = GetObjid(strIdSession, strTransaction, strCustomerId);
                objInteraction.FECHA_CREACION = DateTime.UtcNow.ToString("dd/MM/yyyy");
                objInteraction.TELEFONO = strCustomerId;
                objInteraction.TIPO = strHidType;
                objInteraction.CLASE = oModel.strHdnClass;
                objInteraction.SUBCLASE = oModel.strHdnSubClass;
                objInteraction.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                objInteraction.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                objInteraction.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                objInteraction.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                objInteraction.NOTAS = oModel.strTxtNote;
                objInteraction.FLAG_CASO = Claro.Constants.NumberZeroString;
                objInteraction.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                objInteraction.AGENTE = oModel.strUsernameApp;
                objInteraction.CONTRATO = oModel.strContractId;
                objInteraction.PLANO = oModel.strCodPlaneInst ?? string.Empty;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return objInteraction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        private string GetObjid(string strIdSession, string strTransaction, string strCustomerId)
        {
            string strObjId = string.Empty;
            string strObjIdContact = string.Empty;
            try
            {
                CustomerResponse objConsult = GetCustomer(strIdSession, strTransaction, strCustomerId, strObjIdContact);
                strObjId = objConsult.contactobjid;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            return strObjId;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strPhone"></param>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private bool GetValidateCustomerIdCustom(string strIdSession, string strTransaction, string strPhone, Models.HFC.AdditionalServiceModel oModel)
        {
            ValidateCustomerIdResponse objBusiness = GetValidateCustomer(strIdSession, strTransaction, strPhone);
            try
            {
                if (objBusiness.FlgResult != Claro.Constants.NumberZeroString)
                {
                    Helper.BEClienteCustomerID objBeClienteCustomerId = new Helper.BEClienteCustomerID();
                    objBeClienteCustomerId = LoadDataCustomerId(oModel);

                    CustomerResponse objRegisterClientCustomerId = GetRegisterCustomerId(strIdSession, strTransaction, objBeClienteCustomerId);
                    if (objRegisterClientCustomerId.Resultado)
                    {
                        if (objRegisterClientCustomerId.vFlagInsert.Trim() == ConfigurationManager.AppSettings("gConstKeyStrResultInsertCusID"))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransaction, e.Message);
                return false;
            }
            
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private Helper.BEClienteCustomerID LoadDataCustomerId(Models.HFC.AdditionalServiceModel oModel)
        {
            string strCustomerId = oModel.strCustomerId; 
            string strCurrentUser = oModel.strAccountUser;
            string strFisrtName = oModel.strFirstName;
            string strLastName = oModel.strLastName;
            string strRazonSocial = oModel.strReasonSocial;
            string strDniRuc = oModel.strNumberDocument;
            string strNumberDoc = oModel.strNumberDocument;
            string strAddress = oModel.strAddress;
            string strDistrict = oModel.strDistrict;
            string strDepartment = oModel.strDepartment;
            string strProvince = oModel.strProvince;

            Helper.BEClienteCustomerID objCustomerId = new Helper.BEClienteCustomerID()
            {
                P_PHONE = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + strCustomerId,
                P_USUARIO = strCurrentUser,
                P_NOMBRES = strFisrtName,
                P_APELLIDOS = strLastName,
                P_RAZONSOCIAL = strRazonSocial,
                P_TIPO_DOC = strDniRuc,
                P_NUM_DOC = strNumberDoc,
                P_DOMICILIO = strAddress,
                P_DISTRITO = strDistrict,
                P_DEPARTAMENTO = strDepartment,
                P_PROVINCIA = strProvince,
                P_MODALIDAD = ConfigurationManager.AppSettings("gConstKeyStrModalidad")
            };

            return objCustomerId;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strHidCaseId"></param>
        /// <param name="strText"></param>
        private void UpdatexInter30(string strIdSession, string strHidCaseId, string strText)
        {
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            try
            {
                UpdatexInter30Request objRequest = new UpdatexInter30Request()
                {
                    p_objid = strHidCaseId,
                    p_texto = strText,
                    audit = audit
                };

                Logging.ExecuteMethod(() =>
                {
                    return _oServiceCommon.GetUpdatexInter30(objRequest);
                });
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strIdSession, e.Message);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTransactionAudit"></param>
        /// <param name="strService"></param>
        /// <param name="strIpClient"></param>
        /// <param name="strNameClient"></param>
        /// <param name="strIpServer"></param>
        /// <param name="strNameServer"></param>
        /// <param name="strAccountUser"></param>
        /// <param name="strPhoneNumber"></param>
        /// <param name="strAmount"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        private bool RegisterAudit(string strIdSession, string strTransaction, string strTransactionAudit,
            string strService, string strIpClient, string strNameClient, string strIpServer, string strNameServer,
            string strAccountUser, string strPhoneNumber, string strAmount, string strText)
        {
            bool blnRespose = false;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SaveAuditResponseCommon saveAuditResponse = new SaveAuditResponseCommon();

            try
            {
                SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
                {
                    vTransaccion = strTransactionAudit,
                    vServicio = strService,
                    vIpCliente = strIpClient,
                    vNombreCliente = strNameClient,
                    vIpServidor = strIpServer,
                    vNombreServidor = strNameServer,
                    vCuentaUsuario = strAccountUser,
                    vTelefono = strPhoneNumber,
                    vMonto = strAmount,
                    vTexto = strText,
                    audit = audit
                };

                saveAuditResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceCommon.SaveAudit(objRequest);
                });

                if (saveAuditResponse.vestado == Claro.Constants.NumberZeroString)
                {
                    blnRespose = false;
                }
                else
                {
                    blnRespose = true;
                }
                
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
                Logging.Info("Session: " + strIdSession, "Transaction: RegisterAudit AdditionalServices HFC", string.Format("Error: {0}", ex.Message));
            }

            return blnRespose;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objInteraction"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strFlagInsert"></param>
        /// <param name="strMsgText"></param>
        /// <returns></returns>
        private CommonTransacService.InsertInteractHFCResponse GetInsert(string strIdSession, string strTransaction,
            Helper.BEInteraction objInteraction, out string strInteractionId, out string strFlagInsert, out string  strMsgText)
        {

            var serviceModelInteraction = Mapper.Map<CommonTransacService.Interaction>(objInteraction);
            CommonTransacService.InsertInteractHFCResponse objResponse = new CommonTransacService.InsertInteractHFCResponse();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            try
            {
                CommonTransacService.InsertInteractHFCRequest objRequest = new CommonTransacService.InsertInteractHFCRequest()
                {
                    Interaction = serviceModelInteraction,
                    audit = audit
                };
                objResponse = Logging.ExecuteMethod(
                    strIdSession, strTransaction,
                    () =>
                    {
                        return _oServiceCommon.GetInsertInteractHFC(objRequest);
                    });

            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            strInteractionId = objResponse.rInteraccionId;
            strFlagInsert = objResponse.rFlagInsercion;
            strMsgText = objResponse.rMsgText;
            return objResponse;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objInteraction"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strFlagInsert"></param>
        /// <param name="strMsgText"></param>
        /// <returns></returns>
        private InsertInteractResponseCommon GetInsertInteraction(string strIdSession, string strTransaction, Helper.BEInteraction objInteraction,
            out string strInteractionId, out string strFlagInsert, out string strMsgText)
        {
            var serviceModelInteraction = Mapper.Map<CommonTransacService.Iteraction>(objInteraction);
            InsertInteractResponseCommon objResponse = new InsertInteractResponseCommon();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            try
            {
                InsertInteractRequestCommon objRequest = new InsertInteractRequestCommon()
                {
                    item = serviceModelInteraction,
                    audit = audit
                };

                objResponse = Logging.ExecuteMethod(
                    strIdSession, strTransaction,
                    () =>
                    {
                        return _oServiceCommon.GetInsertInteract(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            strInteractionId = objResponse.Interactionid;
            strFlagInsert = objResponse.FlagInsercion;
            strMsgText = objResponse.MsgText;
            return objResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerId"></param>
        /// <param name="strObjIdContact"></param>
        /// <returns></returns>
        private CustomerResponse GetCustomer(string strIdSession, string strTransaction, string strCustomerId, string strObjIdContact)
        {
            CustomerResponse objResponse = new CustomerResponse();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            try
            {
                GetCustomerRequest objRequest = new GetCustomerRequest()
                {
                    vPhone = strCustomerId,
                    vAccount = string.Empty,
                    vContactobjid1 = strObjIdContact,
                    vFlagReg = Claro.Constants.NumberOneString,
                    audit = audit
                };

                objResponse = Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    return _oServiceFixed.GetCustomer(objRequest);
                });

            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            return objResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objBeClienteCustomerId"></param>
        /// <returns></returns>
        private CustomerResponse GetRegisterCustomerId(string strIdSession, string strTransaction, Helper.BEClienteCustomerID objBeClienteCustomerId)
        {
            CustomerResponse objResponse = new CustomerResponse();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            try
            {
                Customer objRequest = new Customer()
                {
                    PhoneContact = objBeClienteCustomerId.P_PHONE,
                    User = objBeClienteCustomerId.P_USUARIO,
                    Name = objBeClienteCustomerId.P_NOMBRES,
                    LastName = objBeClienteCustomerId.P_APELLIDOS,
                    BusinessName = objBeClienteCustomerId.P_RAZONSOCIAL,
                    DocumentType = objBeClienteCustomerId.P_TIPO_DOC,
                    DocumentNumber = objBeClienteCustomerId.P_NUM_DOC,
                    Address = objBeClienteCustomerId.P_DOMICILIO,
                    District = objBeClienteCustomerId.P_DISTRITO,
                    Departament = objBeClienteCustomerId.P_DEPARTAMENTO,
                    Province = objBeClienteCustomerId.P_PROVINCIA,
                    Modality = objBeClienteCustomerId.P_MODALIDAD,
                    audit = audit
                };

                objResponse = Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    return _oServiceFixed.GetRegisterCustomerId(objRequest);
                });

            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);                
            }

            return objResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strPhone"></param>
        /// <returns></returns>
        private ValidateCustomerIdResponse GetValidateCustomer(string strIdSession,string strTransaction, string strPhone)
        {
            ValidateCustomerIdResponse objResponse = new ValidateCustomerIdResponse();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            try
            {
                ValidateCustomerIdRequest objRequest = new ValidateCustomerIdRequest()
                {
                    Phone = strPhone,
                    audit = audit
                };

                objResponse = Logging.ExecuteMethod(strIdSession, strTransaction,
                () =>
                {
                    return _oServiceFixed.GetValidateCustomerId(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            return objResponse;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private ComServiceActivationResponse ActiveService(Models.HFC.AdditionalServiceModel oModel)
        { 
            bool bResult = false;
            ComServiceActivationRequest oComServiceActivationRequest = new ComServiceActivationRequest();
            ComServiceActivationResponse oComServiceActivationResponse = new ComServiceActivationResponse();

            ProductTracDeacServResponse oProductTracDeacServResponse = new ProductTracDeacServResponse();
            ProductTracDeacServRequest oProductTracDeacServRequest = new ProductTracDeacServRequest();

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            string strUser = string.Empty;
            try 
	        {

                if (oModel.strHdnTipiService.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServCANAL))
                {

                    if (string.IsNullOrEmpty(oModel.strDateProgramation))
                        oComServiceActivationRequest.StrDateProgramation = DateTime.Now;
                    else oComServiceActivationRequest.StrDateProgramation = Convert.ToDate(oModel.strDateProgramation);

                    oProductTracDeacServRequest.vod = false;
                    oProductTracDeacServRequest.match = false;
                    oProductTracDeacServRequest.vstrCoId = oModel.strContractId;
                    oProductTracDeacServRequest.vstrIdentificador = oModel.strHdnValuePVUMatch;
                    oProductTracDeacServRequest.audit = audit;
                   
                    oProductTracDeacServResponse =  Logging.ExecuteMethod(audit.Session,audit.transaction,()=>{
                            return _oServiceFixed.GetProdIdTraDesacServ(oProductTracDeacServRequest);
                    });

                    if (Functions.GetValueFromConfigFile("strUsaUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")) == ConstantsHFC.strUno)
                    {
                        strUser = Functions.GetValueFromConfigFile("strUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    }
                    else 
                    {
                        strUser = App_Code.Common.CurrentUser;
                    }

                    oComServiceActivationRequest.audit = audit;
                    oComServiceActivationRequest.StrIdTransaction = App_Code.Common.GetTransactionID();
                    oComServiceActivationRequest.StrCodeAplication = Functions.GetValueFromConfigFile("strCodigoApliTRADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    oComServiceActivationRequest.StrIpAplication = App_Code.Common.GetApplicationIp();
                    oComServiceActivationRequest.StrDateRegistre = DateTime.Now;
                    oComServiceActivationRequest.StrFlagSearch = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.StrCoId = oModel.strContractId;
                    oComServiceActivationRequest.StrCodeCustomer = oModel.strCustomerId;
                    oComServiceActivationRequest.StrProIds = oProductTracDeacServResponse.IdProductoMayor;
                    oComServiceActivationRequest.StrDatRegistry = oModel.strHdnValuePVUMatch;
                    oComServiceActivationRequest.StrUser = strUser;
                    oComServiceActivationRequest.StrTelephone = string.Empty;
                    oComServiceActivationRequest.StrTypeService = KEY.AppSettings("gConstTipoHFC");
                    oComServiceActivationRequest.StrCoSer = oModel.strHdnCoSerSel;
                    oComServiceActivationRequest.StrTypeRegistry = ConstantsHFC.strLetraA;
                    oComServiceActivationRequest.StrUserSystem = strUser;
                    oComServiceActivationRequest.StrUserApp = oModel.strUsernameApp;
                    oComServiceActivationRequest.StrEmailUserApp = string.Empty;
                    oComServiceActivationRequest.StrDesCoSer = oModel.strHdnDesCoSerSel;
                    oComServiceActivationRequest.StrCodeInteraction = string.Empty;
                    oComServiceActivationRequest.StrNroAccount = oModel.strAccountNumber;
                    oComServiceActivationRequest.StrCost = oModel.strHdnCostoPVUSel;
                    if (oModel.strCboCampaign == null)
                        oComServiceActivationRequest.StrCodeBell = (oModel.strCboCampaign == "-1") ? "" : oModel.strCboCampaign;
                    else
                        oComServiceActivationRequest.StrCodeBell = string.Empty;

                    oComServiceActivationResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetComServiceActivation(oComServiceActivationRequest);
                    });

                    bResult = oComServiceActivationResponse.BlValues;


                }
                else if (oModel.strHdnTipiService.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServVOD))
                {
                    if (string.IsNullOrEmpty(oModel.strDateProgramation))
                        oComServiceActivationRequest.StrDateProgramation = DateTime.Now;
                    else oComServiceActivationRequest.StrDateProgramation = Convert.ToDate(oModel.strDateProgramation);

                    oProductTracDeacServRequest.vod = true;
                    oProductTracDeacServRequest.match = false;
                    oProductTracDeacServRequest.vstrCoId = oModel.strContractId;
                    oProductTracDeacServRequest.vstrIdentificador = oModel.strHdnValuePVUMatch;
                    oProductTracDeacServRequest.audit = audit;
                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA strHdnValuePVUMatch: " + oModel.strHdnValuePVUMatch);

                    oProductTracDeacServResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetProdIdTraDesacServ(oProductTracDeacServRequest);
                    });
                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA StrProIds: " + oProductTracDeacServResponse.IdProductoMayor);
                    if (Functions.GetValueFromConfigFile("strUsaUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")) == ConstantsHFC.strUno)
                    {
                        strUser = Functions.GetValueFromConfigFile("strUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    }
                    else
                    {
                        strUser = App_Code.Common.CurrentUser;
                    }

                    oComServiceActivationRequest.audit = audit;
                    oComServiceActivationRequest.StrIdTransaction = App_Code.Common.GetTransactionID();
                    oComServiceActivationRequest.StrCodeAplication = Functions.GetValueFromConfigFile("strCodigoApliTRADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    oComServiceActivationRequest.StrIpAplication = App_Code.Common.GetApplicationIp();
                    oComServiceActivationRequest.StrDateRegistre = DateTime.Now;
                    oComServiceActivationRequest.StrFlagSearch = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.StrCoId = oModel.strContractId;
                    oComServiceActivationRequest.StrCodeCustomer = oModel.strCustomerId;
                    oComServiceActivationRequest.StrProIds = oProductTracDeacServResponse.IdProductoMayor;
                    oComServiceActivationRequest.StrDatRegistry = KEY.AppSettings("gConstDatosRegActVOD");
                    oComServiceActivationRequest.StrUser = strUser;
                    oComServiceActivationRequest.StrTelephone = string.Empty;
                    oComServiceActivationRequest.StrTypeService = KEY.AppSettings("gConstTipoHFC");
                    oComServiceActivationRequest.StrCoSer = oModel.strHdnCoSerSel;
                    oComServiceActivationRequest.StrTypeRegistry = ConstantsHFC.strLetraA;
                    oComServiceActivationRequest.StrUserSystem = strUser;
                    oComServiceActivationRequest.StrUserApp = oModel.strUsernameApp;
                    oComServiceActivationRequest.StrEmailUserApp = string.Empty;
                    oComServiceActivationRequest.StrDesCoSer = oModel.strHdnDesCoSerSel;
                    oComServiceActivationRequest.StrCodeInteraction = string.Empty;
                    oComServiceActivationRequest.StrNroAccount = oModel.strAccountNumber;
                    oComServiceActivationRequest.StrCost = oModel.strHdnCostoPVUSel;
                  // oComServiceActivationRequest.StrCodeBell = oModel.strCboCampaign;

                    if (oModel.strCboCampaign == null)
                        oComServiceActivationRequest.StrCodeBell = (oModel.strCboCampaign == "-1") ? "" : oModel.strCboCampaign;
                    else
                        oComServiceActivationRequest.StrCodeBell = string.Empty;


                    oComServiceActivationResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetComServiceActivation(oComServiceActivationRequest);
                    });

                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA ACTIVACION DESACTIVACION: " + oComServiceActivationResponse.StrMessage);
                    bResult = oComServiceActivationResponse.BlValues;
                }
	        }
	        catch (Exception ex)
	        {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
	        }
            return oComServiceActivationResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        private ComServiceActivationResponse DesactiveService(Models.HFC.AdditionalServiceModel oModel)
        {
            bool bResult = false;

            ComServiceActivationRequest oComServiceActivationRequest = new ComServiceActivationRequest();
            ComServiceActivationResponse oComServiceActivationResponse = new ComServiceActivationResponse();

            ProductTracDeacServResponse oProductTracDeacServResponse = new ProductTracDeacServResponse();
            ProductTracDeacServRequest oProductTracDeacServRequest = new ProductTracDeacServRequest();

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            string strUser = string.Empty;

            try
            {
                if (oModel.strHdnTipiService.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServCANAL))
                {

                    if (string.IsNullOrEmpty(oModel.strDateProgramation))
                        oComServiceActivationRequest.StrDateProgramation = DateTime.Now;
                    else oComServiceActivationRequest.StrDateProgramation = Convert.ToDate(oModel.strDateProgramation);

                    oProductTracDeacServRequest.vod = false;
                    oProductTracDeacServRequest.match = true;
                    oProductTracDeacServRequest.vstrCoId = oModel.strContractId;
                    oProductTracDeacServRequest.vstrIdentificador = oModel.strHdnValuePVUMatch;
                    oProductTracDeacServRequest.audit = audit;
                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA strHdnValuePVUMatch: " + oModel.strHdnValuePVUMatch);

                    oProductTracDeacServResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetProdIdTraDesacServ(oProductTracDeacServRequest);
                    });

                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA StrProIds: " + oProductTracDeacServResponse.IdProductoMayor);

                    if (Functions.GetValueFromConfigFile("strUsaUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")) == ConstantsHFC.strUno)
                    {
                        strUser = Functions.GetValueFromConfigFile("strUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    }
                    else
                    {
                        strUser = App_Code.Common.CurrentUser;
                    }

                    oComServiceActivationRequest.audit = audit;
                    oComServiceActivationRequest.StrIdTransaction = App_Code.Common.GetTransactionID();
                    oComServiceActivationRequest.StrCodeAplication = Functions.GetValueFromConfigFile("strCodigoApliTRADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    oComServiceActivationRequest.StrIpAplication = App_Code.Common.GetApplicationIp();
                    oComServiceActivationRequest.StrDateRegistre = DateTime.Now;
                    oComServiceActivationRequest.StrFlagSearch = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.StrFlagOccPenalty = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strPenalty = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strAmountFIdPenalty = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strNewCF = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strBillingCycle = oModel.strBillingCycle;
                    oComServiceActivationRequest.strTicklerCode = string.Empty;
                    oComServiceActivationRequest.StrCoId = oModel.strContractId;
                    oComServiceActivationRequest.StrCodeCustomer = oModel.strCustomerId;
                    oComServiceActivationRequest.StrProIds = oProductTracDeacServResponse.IdProductoMayor;
                    oComServiceActivationRequest.StrDatRegistry = oModel.strHdnValuePVUMatch;
                    oComServiceActivationRequest.StrUser = strUser;
                    oComServiceActivationRequest.strInteraction = string.Empty;

                    oComServiceActivationRequest.StrTelephone = string.Empty;
                    oComServiceActivationRequest.StrTypeService = KEY.AppSettings("gConstTipoHFC");
                    oComServiceActivationRequest.StrCoSer = oModel.strHdnCoSerSel;
                    oComServiceActivationRequest.StrTypeRegistry = ConstantsHFC.strLetraD;
                    oComServiceActivationRequest.StrUserSystem = strUser;
                    oComServiceActivationRequest.StrUserApp = oModel.strUsernameApp;
                    oComServiceActivationRequest.StrEmailUserApp = string.Empty;
                    oComServiceActivationRequest.StrDesCoSer = oModel.strHdnDesCoSerSel;
                    oComServiceActivationRequest.StrCodeInteraction = string.Empty;
                    oComServiceActivationRequest.StrNroAccount = oModel.strAccountNumber;

                    oComServiceActivationResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetComServiceDesactivation(oComServiceActivationRequest);
                    });

                    bResult = oComServiceActivationResponse.BlValues;
                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA ACTIVACION DESACTIVACION: " + oComServiceActivationResponse.StrMessage);


                }
                else if (oModel.strHdnTipiService.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServVOD))
                {
                    if (string.IsNullOrEmpty(oModel.strDateProgramation))
                        oComServiceActivationRequest.StrDateProgramation = DateTime.Now;
                    else oComServiceActivationRequest.StrDateProgramation = Convert.ToDate(oModel.strDateProgramation);

                    oProductTracDeacServRequest.vod = true;
                    oProductTracDeacServRequest.match = true;
                    oProductTracDeacServRequest.vstrCoId = oModel.strContractId;
                    oProductTracDeacServRequest.vstrIdentificador = oModel.strHdnValuePVUMatch;
                    oProductTracDeacServRequest.audit = audit;

                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA strHdnValuePVUMatch: " + oModel.strHdnValuePVUMatch);

                    oProductTracDeacServResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetProdIdTraDesacServ(oProductTracDeacServRequest);
                    });

                    Logging.Info(oModel.IdSession, oModel.strTransaction, "ADSA StrProIds: " + oProductTracDeacServResponse.IdProductoMayor);

                    if (Functions.GetValueFromConfigFile("strUsaUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")) == ConstantsHFC.strUno)
                    {
                        strUser = Functions.GetValueFromConfigFile("strUserConfTADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    }
                    else
                    {
                        strUser = App_Code.Common.CurrentUser;
                    }

                    oComServiceActivationRequest.audit = audit;
                    oComServiceActivationRequest.StrIdTransaction = App_Code.Common.GetTransactionID();
                    oComServiceActivationRequest.StrCodeAplication = Functions.GetValueFromConfigFile("strCodigoApliTRADSC", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    oComServiceActivationRequest.StrIpAplication = App_Code.Common.GetApplicationIp();
                    oComServiceActivationRequest.StrDateRegistre = DateTime.Now;
                    oComServiceActivationRequest.StrFlagSearch = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.StrCoId = oModel.strContractId;
                    oComServiceActivationRequest.StrCodeCustomer = oModel.strCustomerId;
                    oComServiceActivationRequest.StrProIds = oProductTracDeacServResponse.IdProductoMayor;
                    oComServiceActivationRequest.StrDatRegistry = KEY.AppSettings("gConstDatosRegActVOD");
                    oComServiceActivationRequest.StrUser = strUser;
                    oComServiceActivationRequest.StrTelephone = string.Empty;
                    oComServiceActivationRequest.StrTypeService = KEY.AppSettings("gConstTipoHFC");
                    oComServiceActivationRequest.StrCoSer = oModel.strHdnCoSerSel;
                    oComServiceActivationRequest.StrTypeRegistry = ConstantsHFC.strLetraD;
                    oComServiceActivationRequest.StrUserSystem = strUser;
                    oComServiceActivationRequest.StrUserApp = oModel.strUsernameApp;
                    oComServiceActivationRequest.StrEmailUserApp = string.Empty;
                    oComServiceActivationRequest.StrDesCoSer = oModel.strHdnDesCoSerSel;
                    oComServiceActivationRequest.StrCodeInteraction = string.Empty;
                    oComServiceActivationRequest.StrNroAccount = oModel.strAccountUser;
                    oComServiceActivationRequest.StrCost = oModel.strHdnCostoPVUSel;
                    //oComServiceActivationRequest.StrCodeBell = oModel.strCboCampaign;

                    if (oModel.strCboCampaign == null)
                        oComServiceActivationRequest.StrCodeBell = (oModel.strCboCampaign == "-1") ? "" : oModel.strCboCampaign;
                    else
                        oComServiceActivationRequest.StrCodeBell = string.Empty;


                    oComServiceActivationRequest.StrFlagOccPenalty = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strPenalty = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strAmountFIdPenalty =  ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strNewCF = ConstantsHFC.numeroCero;
                    oComServiceActivationRequest.strBillingCycle = oModel.strBillingCycle;
                    oComServiceActivationRequest.strTicklerCode = string.Empty;
                    oComServiceActivationRequest.strInteraction = string.Empty;


                    oComServiceActivationResponse = Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                    {
                        return _oServiceFixed.GetComServiceDesactivation(oComServiceActivationRequest);
                    });

                    bResult = oComServiceActivationResponse.BlValues;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return oComServiceActivationResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strContractId"></param>
        /// <param name="strSncode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCamapaign(string strIdSession, string strContractId, string strSncode)
        {
            CamapaignResponse objCamapaignResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            if (Request.IsAjaxRequest()) {
                CamapaignRequest objCamapaignRequest = new CamapaignRequest();
                objCamapaignRequest.audit = audit;
                objCamapaignRequest.Coid = strContractId;
                objCamapaignRequest.Sncode = strSncode;

                try
                {
                    objCamapaignResponse = Logging.ExecuteMethod(() =>
                    {
                        return _oServiceFixed.GetCamapaign(objCamapaignRequest);
                    });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                }
            }
         
            return Json(objCamapaignResponse.LstCampaign, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strTypeTransaction"></param>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GenerateContancy(string strIdSession, string strInteraccionId, string strTypeTransaction, Models.HFC.AdditionalServiceModel oModel)
        {
            GenerateConstancyResponseCommon response = null;
            try
            {

                if (Request.IsAjaxRequest()) {

                    if (strInteraccionId.Trim().Length == 0) {
                        throw new Exception("No se ha encontrado el codigo de transacción");
                    }
                    var getDatTemplateInteraction = GetInfoInteractionTemplate(strIdSession, strInteraccionId);

                    ParametersGeneratePDF parameters = new ParametersGeneratePDF();
                    parameters.StrCentroAtencionArea = getDatTemplateInteraction.X_INTER_15;
                    parameters.StrTitularCliente = string.Format("{0} {1}", getDatTemplateInteraction.X_FIRST_NAME, getDatTemplateInteraction.X_LAST_NAME);
                    parameters.StrRepresLegal = getDatTemplateInteraction.X_NAME_LEGAL_REP;
                    parameters.StrTipoDocIdentidad = getDatTemplateInteraction.X_LASTNAME_REP;
                    parameters.StrNroDocIdentidad = getDatTemplateInteraction.X_OLD_LAST_NAME;
                    parameters.StrFechaTransaccionProgram = string.IsNullOrEmpty(oModel.strDateProgramation) ? DateTime.Today.ToShortDateString() : oModel.strDateProgramation;
                    parameters.strFechaTransaccion = DateTime.Today.ToShortDateString();
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrNroServicio = getDatTemplateInteraction.X_INTER_4;
                    parameters.StrCargoFijo = getDatTemplateInteraction.X_INTER_19;

                    parameters.strServComercial = oModel.strHdnDesCoSerSel;
                    parameters.StrCargoFijoConIGV = getDatTemplateInteraction.X_INTER_3;
                    //CONTRATO
                    parameters.strContrato = oModel.strContractId;
                    parameters.StrContenidoComercial = Functions.GetValueFromConfigFile("AdditionalServicesContentCommercial", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                    parameters.StrContenidoComercial2 = Functions.GetValueFromConfigFile("AdditionalServicesContentCommercial2", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));

                    if (getDatTemplateInteraction.X_IMEI == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
                    {
                        parameters.StrAccionRetencion = "Activación";
                        parameters.StrCostoTransaccion = getDatTemplateInteraction.X_INTER_19;
                        parameters.strAccionEjecutiva = "Activación";
                    }
                    else
                    {
                        parameters.StrAccionRetencion = "Desactivación";
                        parameters.StrCostoTransaccion = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
                        parameters.strAccionEjecutiva = "Desactivación";
                    }

                    parameters.strEnvioCorreo = oModel.strFlagChkSendEmail == ConstantsHFC.Letter_T ? ConstantsHFC.Variable_SI : ConstantsHFC.Variable_NO;

                    parameters.StrEmail = getDatTemplateInteraction.X_EMAIL;
                    parameters.strProgramado = oModel.strFlagChkProgramming ==ConstantsHFC.Letter_T ? ConstantsHFC.Variable_SI : ConstantsHFC.Variable_NO;
                    parameters.StrAplicaProgramacion = getDatTemplateInteraction.X_CLAROLOCAL5;
                    parameters.StrIgvTax = getDatTemplateInteraction.X_INTER_5;
                    parameters.StrAplicaEmail = getDatTemplateInteraction.X_CLAROLOCAL4;

                    if (parameters.StrAplicaProgramacion == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableSI)
                    {
                        parameters.StrFechaEjecucion = getDatTemplateInteraction.X_CLAROLOCAL6;
                    }

                    parameters.StrTipoTransaccion = Claro.SIACU.Transac.Service.Constants.PresentationLayer.TipoProduco.Fixed;

                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaActivacionDesactivacionHfc");
                    parameters.strAccionEjecutar = ConfigurationManager.AppSettings("strServiciosAdicionalesAccionEjecutar");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strServiciosAdicionalesTransac");

                    response = GenerateContancyPDF(strIdSession, parameters);
                    if (response.Generated)
                    {
                        if (oModel.strFlagChkSendEmail == "T")
                        {
                            var rutaConstancy = response.FullPathPDF;
                            byte[] attachFile = null;
                            string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                            if (DisplayFileFromServerSharedFile(strIdSession, strIdSession, rutaConstancy, out attachFile))
                                SendEmail(oModel, strAdjunto, attachFile, strIdSession, strInteraccionId, parameters.strAccionEjecutiva);
                        }
                    }

                    Logging.Info("Persquash", "GenerateContancy",
                        string.Format("Result={0}, fullPathPDF={1} ", response.Generated, response.FullPathPDF));
                    if (!response.Generated)
                    {
                        Logging.Info("Persquash", "GenerateContancy", string.Format("ADSA Error={0} ", response.ErrorMessage));
                    }
                }                
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdSession, ex.Message);
            }

            return Json(response);
        }
	}
}