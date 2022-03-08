using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Helper = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.Web;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using AutoMapper;
using Claro.SIACU.Transac.Service;
using Constant = Claro.SIACU.Transac.Service.Constants.Notes_AdditionalServicesLte;
using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using System.IO;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class AdditionalServicesController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly COMMON.CommonTransacServiceClient _oCommonService = new COMMON.CommonTransacServiceClient();

        public ActionResult LTEAdditionalServices()
        {
            var msg = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "LTEAdditionalServices", "Iniciando Servicios Adicionales");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg);
            return View();
        }

        public JsonResult Page_Load(string strIdSession, string strStateLine)
        {
            Claro.Web.Logging.Configure();//Temporal
            Logging.Info("NCHO", "AditionalServicesLTE", "Page_Load - Entro");//Temporal
             
            var result = new Dictionary<string, string>
            {
                {"hdnStateLine", StatusLineValidate(strIdSession, 5, strStateLine)},
                {"hdnRouteSiteStart", ConfigurationManager.AppSettings("strRutaSiteInicio")},
                {"hdnValorIGV", GetCommonConsultIgv(strIdSession).igvD.ToString(CultureInfo.InvariantCulture)},
                {"hdnTitleActandDesact", FunctionsSIACU.GetValueFromConfigFile("strMsgTituloActDesacServComer", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                {"hdnSelectCacDac", FunctionsSIACU.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                {"hdnLocalAddr", Request.ServerVariables["LOCAL_ADDR"]},
                {"hdnServerName", Request.ServerVariables["SERVER_NAME"]},
                {"hdnProblemLoad", FunctionsSIACU.GetValueFromConfigFile("strMensajeProblemaLoad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                {"hdnCodSegHabiliCheckCampLte",ConfigurationManager.AppSettings("strCodSegHabilitaCheckCampanaLTE")},
                {"hdnTranstionActDesacServLte", SIACU.Transac.Service.Constants.Notes_AdditionalServicesLte.StrTranstionActDesacServLte},
                {"hdnConstTypeLte", ConfigurationManager.AppSettings("gConstTipoLTE")},
                {"hdnAjustNrRecon", FunctionsSIACU.GetValueFromConfigFile("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                {"hdnProductoLTE", ConfigurationManager.AppSettings("strProductoLTE")},
                {"hdnUSRProceso", ConfigurationManager.AppSettings("USRProcesoSU")},
                {"hdnUsaUserConfTaADSLTE", FunctionsSIACU.GetValueFromConfigFile("strUsaUserConfTADSCLTE", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                {"hdnUserConfTaADSLTE", FunctionsSIACU.GetValueFromConfigFile("strUserConfTADSCLTE", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                {"strCodeApliTRADSC", FunctionsSIACU.GetValueFromConfigFile("strCodigoApliTRADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))}
            };

            //var msg = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "Page_Load", "Traendo las Llaves del WebConfig");
            //Logging.Info("IdSession: " + "", "Transaccion: " + "", msg);
            Logging.Info("NCHO", "AditionalServicesLTE", "Traendo las Llaves del WebConfig");//Temporal
            return new JsonResult
            {
                Data = result,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #region Listar Servicios
        public JsonResult LteGetCommercialSercices(string strIdSession, string strContractId, string strProductoLte)
        {

            Logging.Info(strIdSession, "AditionalServicesLTE", "LteGetCommercialSercices - Entro");//Temporal

            var lstServiceXclable = new List<Helper.CommercialServiceHP>();
            var lstSalidaFinal = new List<Helper.CommercialServiceHP>();

            var lstCommertialServices = LteGetAdditionalServices(strIdSession, strContractId); //ListarServiciosComerciales
            Logging.Info(strIdSession, "AditionalServicesLTE", "lstCommertialServices.Count: " + lstCommertialServices.Count);
            var lstPlanServices = LteGetPlanServices(strIdSession, strContractId, strProductoLte); //ListarServiciosDesdePVU
            Logging.Info(strIdSession, "AditionalServicesLTE", "[lstPlanServices.Count]: " + lstPlanServices.Count);
            var valueXmlIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
            var valueXmlFiltroPvu = FunctionsSIACU.GetValueFromConfigFile("strFiltroPVUTRADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")); //  strFiltroPVUTRADSCLTE
            var valueXmlConstArcjivo = FunctionsSIACU.GetValueFromConfigFile("strNomServVODADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));//strNomServVODADSCLTE
            var valueIgv = valueXmlIgv;

            Logging.Info(strIdSession, "AditionalServicesLTE", String.Format("valueXmlIgv:{0}, valueXmlFiltroPvu:{1}, valueXmlConstArcjivo:{2}, valueIgv:{3}",
                valueXmlIgv, valueXmlFiltroPvu, valueXmlConstArcjivo, valueIgv));

            Logging.Info(strIdSession, "AditionalServicesLTE", string.Format("lstCommertialServices.Count: {0}; lstPlanServices.Count:{1}", lstCommertialServices.Count, lstPlanServices.Count));//Temporal

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
                        lstCommertialServices[i].CODSERPVU = lstPlanServices[j].CodServSisact;
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

           //Inicio Filtro solo Servicios adicionale por cable
            //var strService = ConfigurationManager.AppSettings("ServicioAdicionalesCable").Split('|');
            //Logging.Info(strIdSession, "AditionalServicesLTE", "strService.Length: " + strService.Length);
            //for (int y = 0; y < strService.Length; y++)
            //{
            //    for (int x = 0; x < lstCommertialServices.Count; x++)
            //    {
            //        if (lstCommertialServices[x].DE_GRP.ToUpper() == strService[y].ToUpper())
            //        {
            //            lstServiceXclable.Add(lstCommertialServices[x]);
            //        }
            //    }
            //}

            Logging.Info(strIdSession, "AditionalServicesLTE", "P10");
            //Filtro por PVU
            if (valueXmlFiltroPvu.Equals(ConstantsHFC.PresentationLayer.NumeracionUNO))
            {
                Logging.Info(strIdSession, "AditionalServicesLTE", "P20");
                Logging.Info(strIdSession, "AditionalServicesLTE", "lstServiceXclable.Count:" + lstCommertialServices.Count);
                for (int i = 0; i < lstCommertialServices.Count; i++)
                {
                    if (!lstCommertialServices[i].COSTOPVU.Equals(ConstantsHFC.PresentationLayer.gstrNoInfo))
                    {
                        lstSalidaFinal.Add(lstCommertialServices[i]);
                    }
                }
            }
            else
            {
                Logging.Info(strIdSession, "AditionalServicesLTE", "P30");
                return new JsonResult
                {
                    Data = lstCommertialServices,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = lstSalidaFinal,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<Helper.CommercialServiceHP> LteGetAdditionalServices(string strIdSession, string strContractId)
        {
            Logging.Info("NCHO", "AditionalServicesLTE", "Funcion - LteGetAdditionalServices");//Temporal
            var objLstCommercialService = new List<Helper.CommercialServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            var objCommercialServicesRequest = new CommercialServicesRequest
            {
                audit = objAuditRequest,
                StrCoId = strContractId
            };
            try
            {
                var objCommercialServicesResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCommercialService(objCommercialServicesRequest);
                });
                Logging.Info("NCHO", "AditionalServicesLTE", "LstCommercialServices.Count: " + objCommercialServicesResponse.LstCommercialServices.Count);//Temporal
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
                Logging.Error(strIdSession, objCommercialServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objLstCommercialService;
        }

        public List<Helper.PlanServiceHP> LteGetPlanServices(string strIdSession, string strContractId, string strProductoLte)
        {
            Logging.Info("NCHO", "AditionalServicesLTE", "Entro LteGetPlanServices");//Temporal
            var objLstPlanService = new List<Helper.PlanServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var planCode = LteGetCommertialPlan(strIdSession, strContractId); //ObtenerPlanComercial

            var valueFilterDecoCode = ConfigurationManager.AppSettings("strFilterDecoCodeLTE");

            Logging.Info("NCHO", "AditionalServicesLTE", "[planCode]: " + planCode);//Temporal
            var objPlanServiceRequest = new PlanServicesRequest()
            {
                audit = objAuditRequest,
                IdPlan = planCode,
                CodeProduct = strProductoLte
            };
            try
            {
                var objPlanServiceResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetPlanServicesLte(objPlanServiceRequest); //ObtenerServiciosPorPlanLTE
                });
                Logging.Info("NCHO", "AditionalServicesLTE", "LstPlanServices.Count:" + objPlanServiceResponse.LstPlanServices.Count);//Temporal
                if (objPlanServiceResponse.LstPlanServices.Count > 0)
                {
                    var lstTemp = objPlanServiceResponse.LstPlanServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Helper.PlanServiceHP
                        {
                            CodigoPlan = item.CodigoPlan ?? "",
                            DescPlan = item.DescPlan ?? "",
                            TmCode = item.TmCode ?? "",
                            Solucion = item.Solucion ?? "",
                            CodServSisact = item.CodServSisact ?? "",
                            SNCode = item.SNCode ?? "",
                            SPCode = item.SPCode ?? "",
                            CodTipoServ = item.CodTipoServ ?? "",
                            TipoServ = item.TipoServ ?? "",
                            DesServSisact = item.DesServSisact ?? "",
                            CodGrupoServ = item.CodGrupoServ ?? "",
                            GrupoServ = item.GrupoServ ?? "",
                            CF = item.CF ?? "",
                            IdEquipo = item.IdEquipo ?? "",
                            Equipo = item.Equipo ?? "",
                            CantidadEquipo = item.CantidadEquipo ?? "",
                            MatvIdSap = item.MatvIdSap ?? "",
                            MatvDesSap = item.MatvDesSap ?? "",
                            TipoEquipo = item.TipoEquipo ?? "",
                            CodigoExterno = item.CodigoExterno ?? "",
                            DesCodigoExterno = item.DesCodigoExterno ?? "",
                            ServvUsuarioCrea = item.ServvUsuarioCrea ?? ""
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
                Logging.Error(strIdSession, objPlanServiceRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objLstPlanService;
        }

        public string LteGetCommertialPlan(string strIdSession, string strContractId)
        {
            var planCode = string.Empty;
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objCommercialPlanRequest = new CommertialPlanRequest
            {
                audit = objAuditRequest,
                StrCoId = strContractId
            };
            try
            {
                Logging.Info("NCHO", "AditionalServicesLTE",
                    String.Format("LteGetCommertialPlan=> strIdSession:{0};strContractId:{1}", strIdSession, strContractId));//Temporal
                var objCommercialPlanResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCommertialPlan(objCommercialPlanRequest);
                });
                if (!string.IsNullOrEmpty(objCommercialPlanResponse.rCodigoPlan))
                {
                    planCode = objCommercialPlanResponse.rCodigoPlan;
                    Logging.Info("NCHO", "AditionalServicesLTE", "planCode:" + planCode);//Temporal
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCommercialPlanRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return planCode;
        }
        #endregion
        public JsonResult LogTotalFixedCharge(string strIdSession, string strIdTransaction, string cargoSinIgv, string cargoConIgv)
        {
            Logging.Info(strIdSession, strIdTransaction, "Cargo Fijo Total Sin IGV: S/. " + cargoSinIgv + ", Cargo Fijo Total Con IGV: S/. " + cargoConIgv);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #region Guardar Transaccion Activar y Desactivar
        public JsonResult SaveTransaction(Model.LTE.AddtionalServiceModel model)
        {
            Logging.Info("NCHO", "AditionalServicesLTE", "SaveTransaction - Entro");

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                Model.LTE.AddtionalServiceModel resultServiceAditional = new Model.LTE.AddtionalServiceModel();
                model.oServerModel.StrAccountUser = App_Code.Common.CurrentUser;
                Logging.Info("", "AditionalServicesLTE", "model.oServerModel.StrAccountUser:" + model.oServerModel.StrAccountUser);
                //Grabar Interaccion
                var booleanTipi = SaveInteraction(model);
                Logging.Info("", "AditionalServicesLTE", "Finalizo SaveInteraction");
                var interactioId = booleanTipi.SingleOrDefault(y => y.Key.Equals("hdnCaseId")).Value;
                model.StrInteractionId = interactioId != null ? interactioId.ToString() : string.Empty;
                Logging.Info("", "AditionalServicesLTE", "StrInteractionId: " + model.StrInteractionId);
                dictionary["strInteractionId"] = model.StrInteractionId;
                model.StrCaseId = !string.IsNullOrEmpty(model.StrInteractionId) ? Convert.ToInt(model.StrInteractionId) : 0;
                Logging.Info("", "AditionalServicesLTE", "StrCaseId: " + model.StrCaseId);
                var strMsgAdsNoSelTran = FunctionsSIACU.GetValueFromConfigFile("strMsgADSCNoSelTran", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                var strValues = FunctionsSIACU.GetValueFromConfigFile("strEjecutaTransaccionADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                Logging.Info("", "AditionalServicesLTE", "strValues: " + strValues);
                if (model.TypeTransaction.Length < 1)
                {
                    dictionary["strMsgScript"] = strMsgAdsNoSelTran;
                    dictionary["lblMessage"] = strMsgAdsNoSelTran;
                }
                Logging.Info("", "AditionalServicesLTE", "TypeTransaction.Length: " + model.TypeTransaction.Length);
                if (strValues == SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO)
                {
                    if (model.TypeTransaction == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA || model.TypeTransaction == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableD)
                    {
                        Logging.Info("", "AditionalServicesLTE", "model.TypeTransaction: " + model.TypeTransaction);

                        model.DblNewCf = Functions.CheckDbl(SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2);
                        if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA )
                        {
                            var montoNuevoCFIgv = booleanTipi.SingleOrDefault(y => y.Key.Equals("montoNuevoCFIgv")).Value;
                            model.DblNewCf = Functions.CheckDbl(montoNuevoCFIgv);
                        }

                        resultServiceAditional = ActivationDesactivationServices(model);
                    }
                }
                dictionary["FullPathPDF"] = ""; 
                if (resultServiceAditional.BlValues)
                {
                    Logging.Info("", "AditionalServicesLTE", "BlValues: true" );
                    
                    Logging.Info("", "AditionalServicesLTE", "model.TypeTransaction: " + model.TypeTransaction);
                    if (model.TypeTransaction == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
                    {
                        InsertAudit(true, model);
                    }
                    else
                    {
                        InsertAudit(false, model);
                    }
                    var hdnMsgTransGrabSatis = FunctionsSIACU.GetValueFromConfigFile("strMsgTranGrabSatis", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    Logging.Info("", "AditionalServicesLTE", "hdnMsgTransGrabSatis: " + hdnMsgTransGrabSatis);//Temporal


                    COMMON.GenerateConstancyResponseCommon objGC = GenerateContancy(model);
                    dictionary["btnConstancy"] = objGC.Generated;
                    if (objGC.Generated)
                    {
                        var rutaConstancy = objGC.FullPathPDF;
                        //Envio de Correo y Envio de Constancia
                        if (model.ChkEnviarPorEmail)
                        {
                            byte[] attachFile;
                            //Nombre del archivo
                            string strAdjunto = string.IsNullOrEmpty(rutaConstancy) ? string.Empty : rutaConstancy.Substring(rutaConstancy.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                            if (DisplayFileFromServerSharedFile(model.oServerModel.StrIdSession, model.oServerModel.StrIdSession, rutaConstancy,
                                out attachFile))
                            {

                                string StrAccionRetencion= string.Empty;

                                if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
                                {
                                    StrAccionRetencion = "Activación";

                                }
                                else
                                {
                                    StrAccionRetencion = "Desactivación";
                                }

                                Logging.Info("", "AditionalServicesLTE", "ChkEnviarPorEmail: true");
                                SendEmailAdditionalService(model.oHiddenModel.TxtEnviarporEmail,// model.oCustomersDataModel.StrEmail,
                                    model.StrInteractionId, model.oCustomersDataModel.StrName,
                                    model.oCustomersDataModel.StrLegalRepresent, model.oCustomersDataModel.StrCustomerId,
                                    model.oCustomersDataModel.StrDocumentType, model.oCustomersDataModel.StrDocumentNumber,
                                    model.TypeTransaction, model.oServerModel.StrIdSession, strAdjunto, attachFile, StrAccionRetencion);
                                var lblMsgEmail = "En breves minutos se estará enviando un correo de notificación.";
                                dictionary["strRsptaEmail"] = true;
                                dictionary["lblMsgEmail"] = lblMsgEmail;
                                Logging.Info("", "AditionalServicesLTE", String.Format("strRsptaEmail: {0}, lblMsgEmail:{1}", true, lblMsgEmail));
                            }
                        }

                        dictionary["FullPathPDF"] = (objGC.Generated) ? objGC.FullPathPDF : ""; 
                        Logging.Info("", "AditionalServicesLTE", "objGC.FullPathPDF: " + dictionary["FullPathPDF"]);

                        dictionary["hdnMsgTransGrabSatis"] = hdnMsgTransGrabSatis;
                        dictionary["lblMessage"] = hdnMsgTransGrabSatis;
                        dictionary["lblMessageVisible"] = true;
                    }
                    else {
                        objGC.ErrorMessage = (objGC.ErrorMessage!=null) ? objGC.ErrorMessage:String.Empty;
                        Logging.Info("", "AditionalServicesLTE", "No se genero la Consancia: " + objGC.ErrorMessage);//Temporal
                        dictionary["lblMessage"] = "Se grabo el proceso pero no se pudo generar la constancia";
                        dictionary["lblMessageVisible"] = true;
                    } 
                }
                else
                {
                    Logging.Info("", "AditionalServicesLTE", "BlValues: false");//Temporal
                    if (model.StrCaseId > 1)
                    {
                        Logging.Info("", "AditionalServicesLTE", "model.StrCaseId: " + model.StrCaseId);//Temporal
                        var pText = FunctionsSIACU.GetValueFromConfigFile("strMensajeErrorparaNotasClfy", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        pText = string.Format("{0}{1}", pText, model.StrNote);
                        ExecuteUpdateInter30(model.oServerModel.StrIdSession, model.StrTransaccionId, model.StrInteractionId, pText);
                        Logging.Info("", "AditionalServicesLTE", "Fin ejecucion ExecuteUpdateInter30()");//Temporal
                    }
                    dictionary["btnConstancy"] = false; 
                    var isNumeric = Functions.IsNumeric(resultServiceAditional.StrCodeResult);
                    if (Convert.ToInt(isNumeric) > 0)
                    {
                        dictionary["strMsgScript"] = resultServiceAditional.StrMessage;
                    }
                    else
                    {
                        dictionary["strMsgScript"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    Logging.Info("", "AditionalServicesLTE", "dictionary[strMsgScript]: " + dictionary["strMsgScript"].ToString());//Temporal
                    dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    dictionary["lblMessageVisible"] = true;
                }
            }
            catch (Exception ex)
            {
                dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                dictionary["lblMessageVisible"] = true;
                Logging.Error(model.oServerModel.StrIdSession, "strIdTransaction", ex.Message);
            }

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }
        public Dictionary<string, string> GetPlanCommercial(Model.LTE.AddtionalServiceModel model)
        {
            var dictionary = new Dictionary<string, string>();
            try
            {
                AuditRequest audit = App_Code.Common.CreateAuditRequest<AuditRequest>(model.oServerModel.StrIdSession);
                PlanCommercialResponse objResponse = new PlanCommercialResponse();
                PlanCommercialRequest objRequest = new PlanCommercialRequest();

                objRequest.audit = audit;
                objRequest.StrContractId = Convert.ToInt(model.StrCoId);
                objRequest.StrCodService = Convert.ToInt(model.oHiddenModel.HdnCoSerSel);
                objRequest.StrState = model.StrStateService;
                objRequest.StrTypeProduct = model.oHiddenModel.StrTypeProduct;

                objResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetPlanCommercial(objRequest);
                });
                Logging.Info("NCHO", "AditionalServicesLTE", String.Format("StrResult: {0}, StrMessage: {1}", objResponse.StrResult, objResponse.StrMessage));//Temporal
                dictionary["StrResult"] = objResponse.StrResult;
                dictionary["StrMessage"] = objResponse.StrMessage;

            }
            catch (Exception ex)
            {
                Logging.Error(model.oServerModel.StrIdSession, "strIdTransaction", ex.Message);
            }
            return dictionary;
        }
        public Model.LTE.AddtionalServiceModel ActivationDesactivationServices(Model.LTE.AddtionalServiceModel model)
        {

            Logging.Info("", "AditionalServicesLTE", "ActivationDesactivationServices(Model) - Entro");
            if (model.StrDateProgramation != null)
            {
                model.StrDateProgramation = DateTime.ParseExact(model.StrDateProgramation, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            }

            Logging.Info("", "AditionalServicesLTE", "StrTypeRegistry:  " + model.StrTypeRegistry);

            if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
            {
                model.StrCodeSer = ConstantsHFC.numeroCatorce.ToString();
            }
            else if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableD)
            {
                model.StrCodeSer = ConstantsHFC.numeroQuince.ToString();
            }
            Logging.Info("", "AditionalServicesLTE", "model.StrCodeSer:  " + model.StrCodeSer);
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.oServerModel.StrIdSession);
            objAuditRequest.applicationName = ConfigurationManager.AppSettings("ApplicationName");  

            if (model.StrDateProgramation == null)
            {
                model.StrDateProgramation = DateTime.Now.ToString("dd/MM/yyyy");
            }

            Logging.Info("", "AditionalServicesLTE", "model.StrDateProgramation: " + model.StrDateProgramation);
            var modelAddService = new AddServiceAditional();
            modelAddService.StrMsisdn = model.oLineDataModel.StrMsisdn;
            modelAddService.StrCoId = model.oCustomersDataModel.StrContractId;
            modelAddService.StrCustomerId = model.oCustomersDataModel.StrCustomerId;
            modelAddService.StrCoSer = model.oHiddenModel.HdnCoSerSel;
            modelAddService.IntFlagOccPenalty = ConstantsHFC.numeroCero;
            modelAddService.DblPenaltyAmount = Functions.CheckDbl(0.0);
            modelAddService.DblNewCf =model.DblNewCf;
            Logging.Info("", "AditionalServicesLTE", "modelAddService.DblNewCf: " + modelAddService.DblNewCf);
            modelAddService.StrTypeRegistry = model.StrTypeRegistry;
            modelAddService.StrCycleFacturation = model.oCustomersDataModel.StrBillingCycle;
            modelAddService.StrCodeSer = model.StrCodeSer;
            modelAddService.StrDescriptioCoSer = model.oHiddenModel.HdnDesCoSerSel;
            modelAddService.StrNroAccoutnt = model.oCustomersDataModel.StrAccount;
            modelAddService.StrDateProgramation = model.StrDateProgramation;
            modelAddService.StrInteractionId = model.StrInteractionId;
            modelAddService.StrTypeSerivice = model.oHiddenModel.HdnType;
            modelAddService.StrUserSystem = Functions.GetValueFromConfigFile("strUserConfTADSC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

            var objRequest = new ActivationDesactivationRequest
            {
                audit = objAuditRequest,

                AddServiceAditional = modelAddService
            };
            var result = new Model.LTE.AddtionalServiceModel();

            Logging.Info("", "AditionalServicesLTE", "_oServiceFixed.GetActivationDesactivation: ");
            
            var objResponse = _oServiceFixed.GetActivationDesactivation(objRequest);
            
            Logging.Info("", "AditionalServicesLTE", String.Format("StrResult: {0}, StrMessage: {1}, BlValues: {2}",
                objResponse.StrResult, objResponse.StrMessage, objResponse.BlValues));
            result.StrCodeResult = objResponse.StrResult;
            result.StrMessage = objResponse.StrMessage;
            result.BlValues = objResponse.BlValues;

            return result;
        }
        //Insertar Auditoria
        public void InsertAudit(bool blActivate, Model.LTE.AddtionalServiceModel model)
        {
            string strTransaction = ConfigurationManager.AppSettings("gActDesactServiciosComerciales");
            string strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            string strUrl = null;

            var action = (blActivate == true) ? "ProgActiv" : "ProgDesac";
            Logging.Info("", "AditionalServicesLTE", "action:" + action);//Temporal
            strUrl = "Codigo Contrato: " + model.oCustomersDataModel.StrContractId + "/MSISDN: " + model.oLineDataModel.StrCellPhone + "/Codigo Servicio Comercial: " + model.StrCoSer + "/Nombre Servicio Comercial: " + model.StrDescriptioCoSer + "/Accion:" + action + "/CAC o DAC: " + model.StrCacDac;
            string strTexto = strUrl + "/Ip Cliente: " + model.StrIpCustomer + "/Usuario: " + model.oServerModel.StrAccountUser + "/Id Opcion: " + ConfigurationManager.AppSettings("strIdOpcionClaroProteccion") + "/Fecha y Hora: " + DateTime.Now;

            try
            {
                SaveAuditM(
                    strTransaction,
                    strService,
                    strTexto,
                    model.oCustomersDataModel.StrTelephone,
                    model.oCustomersDataModel.StrFullName,
                    model.oServerModel.StrIdSession,
                    model.oServerModel.StrNameServer,
                    model.oServerModel.StrIpServer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Logging.Info("NCHO", "AditionalServicesLTE", "Fin del Insertar Auditoria");//Temporal
        }
        //Insertar Interaccion
        public Dictionary<string, object> SaveInteraction(Model.LTE.AddtionalServiceModel model)
        {
            Logging.Info("", "AditionalServicesLTE", "Dictionary_SaveTransaction - Entro");//Temporal
            var nextProcess = true;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Model.TemplateInteractionModel objemplateInteraction = new Model.TemplateInteractionModel();

            objemplateInteraction.BlValues = false;
            Logging.Info("", "AditionalServicesLTE", "Dictionary_SaveTransaction - A01");
            //Valida cliente por Id
            var validateCustomerId = GetValidateCustomerId(model.oCustomersDataModel, string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), model.oCustomersDataModel.StrCustomerId), model.oServerModel.StrIdSession);
            Logging.Info("", "AditionalServicesLTE", "validateCustomerId: " + validateCustomerId);
            if (!validateCustomerId)
            {
                dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyNoValidaCustomerID", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                dictionary["lblMessageVisible"] = false;
                nextProcess = false;
            }
            var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "SaveInteraction", "Valida cliente por Id " + validateCustomerId);
            Logging.Info("IdSession: " + model.oServerModel.StrIdSession, "Transaccion: " + "", msg1);

            if (nextProcess)
            {
                Logging.Info("", "AditionalServicesLTE", "IF nextProcess== False");
                var strPhone = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), model.oCustomersDataModel.StrCustomerId);
                var idContact = GetCustomer(strPhone, model.oServerModel.StrIdSession);
                Logging.Info("", "AditionalServicesLTE", "idContact: " + idContact);
                string isTfi = string.Empty;

                //Obtener Datos de Interaccion
                var objInteractionData = InteractionData(model.oServerModel.StrIdSession, idContact, strPhone, model.StrNote, "", "", "", isTfi);
                Logging.Info("NCHO", "AditionalServicesLTE", "Dictionary_SaveTransaction=> Se creo objInteractionData");
                var objInteractionModel = new Model.InteractionModel();
                #region Map InteractionModel
                objInteractionModel.ObjidContacto = objInteractionData.OBJID_CONTACTO;
                objInteractionModel.DateCreaction = objInteractionData.FECHA_CREACION;
                objInteractionModel.Telephone = objInteractionData.TELEFONO;

                objInteractionModel.Type = model.oHiddenModel.HdnType;
                objInteractionModel.Class = model.oHiddenModel.HdnClase;
                objInteractionModel.SubClass = model.oHiddenModel.HdnSubClass;
                objInteractionModel.TypeCode = model.oHiddenModel.HdnTypeCode;
                objInteractionModel.ClassCode = model.oHiddenModel.HdnClaseCode;
                objInteractionModel.SubClassCode = model.oHiddenModel.HdnSubClassCode;
                objInteractionModel.InteractionCode = model.oHiddenModel.HdnInteractionCode;

                objInteractionModel.TypeInter = objInteractionData.TIPO_INTER;
                objInteractionModel.Method = objInteractionData.METODO;
                objInteractionModel.Result = objInteractionData.RESULTADO;
                objInteractionModel.MadeOne = objInteractionData.HECHO_EN_UNO;
                objInteractionModel.Note = objInteractionData.NOTAS;
                objInteractionModel.FlagCase = objInteractionData.FLAG_CASO;
                objInteractionModel.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                objInteractionModel.Contract = model.oCustomersDataModel.StrContractId;
                objInteractionModel.Plan = model.oLineDataModel.StrPlan;
                objInteractionModel.Agenth = model.oCustomersDataModel.StrUser;
                #endregion
                Logging.Info("", "AditionalServicesLTE", "Fin Map_InteractionModel");

                var strUserSystem = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                var strPassWord = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                Logging.Info("", "AditionalServicesLTE", String.Format("strUserSystem: {0}, strUserAplication: {1}, strPassWord: {2}",
                    strUserSystem, strUserAplication, strPassWord));
                //Obtener Datos de Plantilla de Interaccion
                string montoNuevoCFIgv= string.Empty;
                objemplateInteraction = DatTemplateInteraction(model);
                montoNuevoCFIgv= objemplateInteraction.X_INTER_3; 
                dictionary["montoNuevoCFIgv"] = montoNuevoCFIgv; 

                nextProcess = objemplateInteraction.BlValues;
                if (nextProcess == false)
                {
                    dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("strNoTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    dictionary["lblMessageVisible"] = true;
                    nextProcess = false;
                }
                if (nextProcess)
                {
                    var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + model.oCustomersDataModel.StrCustomerId;
                    Logging.Info("", "AditionalServicesLTE", "strNroTelephone: " + strNroTelephone);
                    //Insertar Interaccion
                    var resultInteraction = InsertInteraction(objInteractionModel, objemplateInteraction, strNroTelephone, strUserSystem, strUserAplication, strPassWord, true, model.oServerModel.StrIdSession, model.oCustomersDataModel.StrCustomerId);
                    Logging.Info("", "AditionalServicesLTE", "Finalizo InsertInteraction");
                    dictionary["hdnCaseId"] = resultInteraction.SingleOrDefault(y => y.Key.Equals("rInteraccionId")).Value;
                    var strFlagInsertion = resultInteraction.SingleOrDefault(y => y.Key.Equals("strFlagInsertion")).Value.ToString();
                    var strFlagInsertionInteraction = resultInteraction.SingleOrDefault(y => y.Key.Equals("strFlagInsertionInteraction")).Value.ToString();

                    if (strFlagInsertion != ConstantsHFC.CriterioMensajeOK &&
                        strFlagInsertion != SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableEmpty)
                    {
                        dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        dictionary["lblMessageVisible"] = true;
                        nextProcess = false;
                    }
                    Logging.Info("", "AditionalServicesLTE", "strFlagInsertion: " + (strFlagInsertion ?? ""));
                    if (nextProcess)
                    {
                        if ((!String.IsNullOrEmpty(strFlagInsertionInteraction)) && (!strFlagInsertionInteraction.Equals(ConstantsHFC.CriterioMensajeOK))
                        ) 
                        {
                            dictionary["lblMessage"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                            dictionary["lblMessageVisible"] = true;
                            nextProcess = false;
                        }
                    }
                    Logging.Info("", "AditionalServicesLTE", "strFlagInsertionInteraction: " + (strFlagInsertionInteraction ?? ""));
                }
            }
            return dictionary;
        }
        //Datos de Plantilla de Interaccion
        public Model.TemplateInteractionModel DatTemplateInteraction(Model.LTE.AddtionalServiceModel objAddServiceAditional)
        {
            Logging.Info("", "AditionalServicesLTE", "DatTemplateInteraction - Entro");//Temporal
            Model.TemplateInteractionModel model = new Model.TemplateInteractionModel();

            var igvCurrent = GetCommonConsultIgv(objAddServiceAditional.oServerModel.StrIdSession);
            var igvWithUnit = 0.0;
            if (igvCurrent != null)
            {
                igvWithUnit = igvCurrent.igvD + 1;
            }

            model.NOMBRE_TRANSACCION = Constant.StrTranstionActDesacServLte;
            model.X_CLARO_NUMBER = objAddServiceAditional.oCustomersDataModel.StrTelephone;
            model.X_FIRST_NAME = objAddServiceAditional.oCustomersDataModel.StrFirtName;
            model.X_LAST_NAME = objAddServiceAditional.oCustomersDataModel.StrLastName;
            model.X_DOCUMENT_NUMBER = objAddServiceAditional.oCustomersDataModel.StrDocumentNumber;
            model.X_REFERENCE_PHONE = objAddServiceAditional.oCustomersDataModel.StrPhoneReference;

            model.X_REASON = objAddServiceAditional.oCustomersDataModel.StrBusinessName;

            model.X_INTER_1 = objAddServiceAditional.oCustomersDataModel.StrCustomerContact;
            model.X_INTER_2 = objAddServiceAditional.oCustomersDataModel.StrDocumentNumber;
            model.X_INTER_3 = string.IsNullOrEmpty(objAddServiceAditional.oHiddenModel.HdnCostoPvuSel) ? string.Empty : Math.Round(Convert.ToDouble(objAddServiceAditional.oHiddenModel.HdnCostoPvuSel) * igvWithUnit, 2).ToString(CultureInfo.InvariantCulture); //objAddServiceAditional.oHiddenModel.HdnCostoPvuSel;
            if (objAddServiceAditional.StrTypeRegistry != SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
            {
                model.X_INTER_3 = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
            }


            model.X_INTER_4 = objAddServiceAditional.oHiddenModel.HdnDesCoSerSel;
            model.X_INTER_5 = objAddServiceAditional.oHiddenModel.HdnCostoBscs;
            model.X_IMEI = objAddServiceAditional.StrTypeRegistry;

            model.X_INTER_7 = objAddServiceAditional.oHiddenModel.Plan;
            model.X_INTER_15 = objAddServiceAditional.oCustomersDataModel.StrCacDac;
            model.X_INTER_19 = objAddServiceAditional.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA ?
                                                                                    objAddServiceAditional.oHiddenModel.HdnCostoPvuSel :
                                                                                    SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
            model.X_INTER_20 = objAddServiceAditional.oHiddenModel.HdnCargoFijoSel;
            model.X_INTER_21 = objAddServiceAditional.oHiddenModel.HdnCoSerSel;
            model.X_INTER_29 = objAddServiceAditional.oHiddenModel.HdnDesCoSerSel;
            model.X_INTER_30 = objAddServiceAditional.oHiddenModel.TxtNota;
            model.X_INTER_25 = Convert.ToDouble(objAddServiceAditional.oCustomersDataModel.StrBillingCycle);
            model.X_CLAROLOCAL3 = DateTime.Now.ToShortDateString();

            model.X_OPERATION_TYPE = objAddServiceAditional.oHiddenModel.HdnTipoTransaccion;

            model.X_ADJUSTMENT_REASON = objAddServiceAditional.oCustomersDataModel.StrContractId;
            model.X_TYPE_DOCUMENT = objAddServiceAditional.oCustomersDataModel.StrCustomerType;
            model.X_INTER_8 = Convert.ToDouble(objAddServiceAditional.oCustomersDataModel.StrCustomerId);
            model.X_LASTNAME_REP = objAddServiceAditional.oCustomersDataModel.StrDocumentType.ToUpper();

            if (objAddServiceAditional.oCustomersDataModel.StrCustomerType == ConstantsHFC.PresentationLayer.gstrConsumer)
            {
                model.X_INTER_16 = objAddServiceAditional.oCustomersDataModel.StrFullName;
                model.X_NAME_LEGAL_REP = objAddServiceAditional.oCustomersDataModel.StrFullName;
                model.X_OLD_LAST_NAME = objAddServiceAditional.oCustomersDataModel.StrDocumentNumber;
            }
            else
            {
                model.X_INTER_16 = objAddServiceAditional.oCustomersDataModel.StrBusinessName;
                model.X_NAME_LEGAL_REP = string.IsNullOrEmpty(objAddServiceAditional.oCustomersDataModel.StrBusinessName) ? objAddServiceAditional.oCustomersDataModel.StrFullName : objAddServiceAditional.oCustomersDataModel.StrBusinessName;
                model.X_OLD_LAST_NAME = objAddServiceAditional.oCustomersDataModel.StrDniruc;
            }

            if (objAddServiceAditional.oHiddenModel.ChkEnviarPorEmail)
            {
                model.X_CLAROLOCAL4 = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableSI;
                model.X_EMAIL = objAddServiceAditional.oHiddenModel.TxtEnviarporEmail;
            }
            else
            {
                model.X_CLAROLOCAL4 = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableNO;
            }

            if (objAddServiceAditional.oHiddenModel.ChkProgramar)
            {
                model.X_CLAROLOCAL5 = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableSI;
                model.X_CLAROLOCAL6 = objAddServiceAditional.oHiddenModel.HdnFechaProg;
            }
            else
            {
                model.X_CLAROLOCAL5 = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableNO;
                model.X_CLAROLOCAL6 = DateTime.Now.ToShortDateString();
            }
            model.BlValues = true;
            return model;
        }
        #endregion
        public string GetIdTransaction()
        {
            var idTransaction = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
            return idTransaction;
        }
        public JsonResult GetCamapaign(string strIdSession, string strContractId, string strSncode)
        {
            var msg1 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "GetCamapaign", "Iniciando GetCamapaign");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + "", msg1);

            CamapaignResponse objCamapaignResponse = null;
            AuditRequest audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
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
                throw new Exception(ex.Message);
            }
            var msg2 = string.Format("Controller: {0},Metodo: {1}, RESULTADO: {2}", "AdditionalServices", "GetCamapaign", "Fin GetCamapaign");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + "", msg2);

            return Json(objCamapaignResponse.LstCampaign, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidateActDesactService(string strCoSer, string strState, string strMsIsdn, string strIdSession, string strContractId)
        {

            Logging.Info("", "AditionalServicesLTE", "Iniciando la validacion de Activacion");

            var typeReg = string.Empty;
            var intCodActDesService = string.Empty;
            var strUser = string.Empty;
            if (strState == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
            {
                intCodActDesService = ConfigurationManager.AppSettings("strConsActivaDesactivaQuince");
                typeReg = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableD;
            }
            else if (strState == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableD)
            {
                intCodActDesService = ConfigurationManager.AppSettings("strConsActivaDesactivaCatorce");
                typeReg = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA;

            }
            Logging.Info("", "AditionalServicesLTE", String.Format("intCodActDesService: {0}, typeReg: {1}",
                intCodActDesService,typeReg));
            var strUsaUserConf = FunctionsSIACU.GetValueFromConfigFile("strUsaUserConfTADSCLTE", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            if (strUsaUserConf == SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO)
            {
                strUser = FunctionsSIACU.GetValueFromConfigFile("strUserConfTADSCLTE", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            Logging.Info("", "AditionalServicesLTE", "strUser: " + strUser);
            ValidateActDesServiceResponse objResponse = null;
            AuditRequest audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            audit.userName = strUser;
            ValidateActDesServiceRequest obRequest = new ValidateActDesServiceRequest
            {
                audit = audit,
                StrMsisdn = strMsIsdn,
                StrCodId = strContractId,
                StrCoSer = strCoSer,
                StrTypeRegistre = typeReg,
                StrCodSer = intCodActDesService,
                StrStateService = ConstantsHFC.numeroUno
            };
            try
            {
                objResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetValidateActDesService(obRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdSession, ex.Message);
                throw;
            }

            var result = objResponse.StrResult + "|" + objResponse.StrMsg;
            Logging.Info("", "AditionalServicesLTE", "resultado de la validacion : " + result);//Temporal

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomerPhone(string strIdSession, string intIdContract, string strTypeProduct)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objRequest = new ConsultationServiceByContractRequest()
            {
                audit = objAuditRequest,
                strCodContrato = intIdContract,
                typeProduct = strTypeProduct
            };

            var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomerLineNumber(objRequest);
            });
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Metodo: " + "GetCustomerPhone", objCustomerPhoneResponse.msisdn);
            var phone = objCustomerPhoneResponse.msisdn;
            return Json(phone, JsonRequestBehavior.AllowGet);
        }

        #region Generar Constanacia
        private COMMON.GenerateConstancyResponseCommon GenerateContancy(Model.LTE.AddtionalServiceModel model)
        { 
            Logging.Info("", "AditionalServicesLTE", "GenerateContancy Entro");
            COMMON.GenerateConstancyResponseCommon response = new COMMON.GenerateConstancyResponseCommon();
            try
            {
                var getDatTemplateInteraction = GetInfoInteractionTemplate(model.oServerModel.StrIdSession, model.StrInteractionId);

                COMMON.ParametersGeneratePDF parameters = new COMMON.ParametersGeneratePDF();
                parameters.StrCentroAtencionArea = model.oCustomersDataModel.StrCacDac;
                parameters.StrTitularCliente = string.Format("{0} {1}", model.oCustomersDataModel.StrFirtName , model.oCustomersDataModel.StrLastName);
                parameters.StrRepresLegal = getDatTemplateInteraction.X_NAME_LEGAL_REP;
                parameters.StrTipoDocIdentidad =model.oCustomersDataModel.StrDocumentType  ;
                parameters.StrNroDocIdentidad = model.oCustomersDataModel.StrDocumentNumber;
                parameters.StrFechaTransaccionProgram = string.IsNullOrEmpty(model.StrDateProgramation) ? DateTime.Today.ToShortDateString() : model.StrDateProgramation;
                parameters.strFechaTransaccion = DateTime.Today.ToShortDateString();
                parameters.StrCasoInter = model.StrInteractionId;

                parameters.StrNroServicio = model.oHiddenModel.HdnDesCoSerSel;
                parameters.strServComercial = model.oHiddenModel.HdnDesCoSerSel;
                

                var contrato = getDatTemplateInteraction.X_ADJUSTMENT_REASON;
                parameters.strContrato = contrato;
                parameters.StrContenidoComercial = Functions.GetValueFromConfigFile("AdditionalServicesContentCommercial", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
                parameters.StrContenidoComercial2 = Functions.GetValueFromConfigFile("AdditionalServicesContentCommercial2", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));

                if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableA)
                {
                    parameters.StrAccionRetencion = "Activación";
                    parameters.StrCostoTransaccion = getDatTemplateInteraction.X_INTER_19;
                    parameters.StrCargoFijo = getDatTemplateInteraction.X_INTER_19;
                    parameters.strAccionEjecutiva = "Activación";
                    parameters.StrCargoFijoConIGV = getDatTemplateInteraction.X_INTER_3;
                }
                if (model.StrTypeRegistry == SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableD)
                {
                    parameters.StrAccionRetencion = "Desactivación";
                    parameters.StrCostoTransaccion = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
                    parameters.StrCargoFijo = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
                    parameters.strAccionEjecutiva = "Desactivación";
                    parameters.StrCargoFijoConIGV = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERODECIMAL2;
                }



                parameters.strEnvioCorreo = model.ChkEnviarPorEmail ? ConstantsHFC.Variable_SI : ConstantsHFC.Variable_NO;
                parameters.StrEmail = model.oHiddenModel.TxtEnviarporEmail;
                parameters.strProgramado = model.oHiddenModel.ChkProgramar ? ConstantsHFC.Variable_SI : ConstantsHFC.Variable_NO;

                parameters.StrIgvTax = model.oHiddenModel.HdnCostoBscs; //cargo fijo sin igv
                parameters.StrAplicaEmail = (model.oHiddenModel.ChkEnviarPorEmail ? ConstantsHFC.Variable_SI : ConstantsHFC.Variable_NO); 

                if (parameters.strProgramado == SIACU.Transac.Service.Constants.PresentationLayer
                        .gstrVariableSI)
                {
                    parameters.StrFechaEjecucion = model.oHiddenModel.HdnFechaProg;
                }

                parameters.StrTipoTransaccion = SIACU.Transac.Service.Constants.PresentationLayer.TipoProduco.Fixed;
                parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaActivacionDesactivacionLte");
                parameters.strAccionEjecutar = ConfigurationManager.AppSettings("strServiciosAdicionalesAccionEjecutar");
                parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strServiciosAdicionalesTransac");

                response = GenerateContancyPDF(model.oServerModel.StrIdSession, parameters); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logging.Info(model.oServerModel.StrIdSession, "AditionalServicesLTE", ex.Message); 
            }
            return response;
        }

        #endregion

    }
}