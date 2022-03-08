using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using CSTS = Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Transac.Service;
using System.Collections;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class UnistallInstallationOfDecoderController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        public ActionResult UnistallInstallationOfDecoder()
        {
            Claro.Web.Logging.Configure();
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            int number = Convert.ToInt(ConfigurationManager.AppSettings("strIncrementDays", "0"));
            DateTime DateNowMoreSevenDay = DateTime.Now.AddDays(number);
            ViewData["strDateNew"] = DateNowMoreSevenDay.Year + "/" + DateNowMoreSevenDay.Month.ToString("00") + "/" + DateNowMoreSevenDay.Day.ToString("00");
            return PartialView();
        }
        public ActionResult AdditionalDecoder()
        {
            return PartialView();
        }
        public ActionResult UninstallDecoder()
        {
            return PartialView();
        }
        public JsonResult GetJobTypes(string strIdSession, string strInstDesins)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetJobTypes"); // Temporal
            JobTypesResponseHfc objJobTypeResponse;
            JobTypesRequestHfc objJobTypesRequest = new JobTypesRequestHfc()
            {
                audit = audit,
                p_tipo = 7
            };

            try
            {
                objJobTypeResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                    {
                        return _oServiceFixed.GetJobTypes(objJobTypesRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objUnistallInstallationOfDecoderModel = null;
            if (objJobTypeResponse != null && objJobTypeResponse.JobTypes != null)
            {
                objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
                List<Helpers.CommonServices.GenericItem> List = new List<Helpers.CommonServices.GenericItem>();

                for (int i = 0; i < objJobTypeResponse.JobTypes.Count; i++)
                {
                    List.Add(new Helpers.CommonServices.GenericItem()
                    {
                        Code = objJobTypeResponse.JobTypes[i].FLAG_FRANJA.Equals(CSTS.Constants.strUno) ? objJobTypeResponse.JobTypes[i].tiptra + "|" : objJobTypeResponse.JobTypes[i].tiptra, //Codigo
                        Code2 = objJobTypeResponse.JobTypes[i].FLAG_FRANJA, //Codigo2
                        Description = objJobTypeResponse.JobTypes[i].descripcion //Descripcion
                    });
                }
                Claro.Web.Logging.Info(strIdSession, audit.transaction, "GetJobTypes");
                objUnistallInstallationOfDecoderModel.ListJobTypes = List;
            }

            string strTipoTrabajo = string.Empty;
            if (strInstDesins == CSTS.Constants.strCero) //Instalacion
                strTipoTrabajo = ConfigurationManager.AppSettings("gTipoTrabajoDecoAdicional");
            else
                strTipoTrabajo = ConfigurationManager.AppSettings("gTipoTrabajoBajaDeco");//gTipoTrabajoBajaDeco
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetJobTypes"); // Temporal
            return Json(new { data = objUnistallInstallationOfDecoderModel.ListJobTypes, strTipoTrabajo });
        }
        public JsonResult GetJobSubType(string strIdSession, string strTipoTrabajo, string strContractID)
        {
            List<Helpers.CommonServices.GenericItem> List = null;
            Helpers.CommonServices.GenericItem item = null;
            string TipoTrabajo = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetJobSubType"); // Temporal

            var objOrderSubTypesResponse = new OrderSubTypesResponseHfc();
            var objOrderSubTypesRequest = new OrderSubTypesRequestHfc();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;
            if (CSTS.Functions.CheckStr(strTipoTrabajo.IndexOf("|")) == CSTS.Constants.strMenosUno)
                TipoTrabajo = strTipoTrabajo;
            else
                TipoTrabajo = CSTS.Functions.CheckStr(strTipoTrabajo.Split('|')[0]);


            try
            {
                objOrderSubTypesRequest.audit = audit;
                objOrderSubTypesRequest.av_cod_tipo_trabajo = TipoTrabajo;

                objOrderSubTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubTypesResponseHfc>(() => { return _oServiceFixed.GetOrderSubTypeWork(objOrderSubTypesRequest); });

                if (objOrderSubTypesResponse.OrderSubTypes != null)
                {
                    List = new List<Helpers.CommonServices.GenericItem>();
                    foreach (var aux in objOrderSubTypesResponse.OrderSubTypes)
                    {
                        item = new Helpers.CommonServices.GenericItem();
                        item.Code = aux.COD_SUBTIPO_ORDEN + "|" + aux.TIEMPO_MIN + "|" + aux.ID_SUBTIPO_ORDEN + "|" + aux.DECOS; //Codigo
                        item.Description = aux.DESCRIPCION; //Descripcion
                        item.Code2 = aux.TIPO_SERVICIO;
                        List.Add(item);
                    }
                }

                objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
                {
                    audit = audit,
                    av_cod_tipo_trabajo = TipoTrabajo,
                    av_cod_contrato = strContractID
                };
                objResponseValidate = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubType>(() => { return _oServiceFixed.GetValidationSubTypeWork(objResquest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objOrderSubTypesRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetJobSubType"); // Temporal
            return Json(new { data = List, typeValidate = objResponseValidate });
        }

        [HttpPost]
        public JsonResult GetCommertialPlan(string strIdSession, string strContratoID)
        {
            FixedTransacService.CommertialPlanResponse objCommercialPlanResponse;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetCommertialPlan"); // Temporal
            var objCommercialPlanRequest = new CommertialPlanRequest { audit = audit, StrCoId = strContratoID };

            try
            {
                objCommercialPlanResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommertialPlan(objCommercialPlanRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCommercialPlanRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetCommertialPlan"); // Temporal
            return Json(new { data = objCommercialPlanResponse });
        }
        public JsonResult ValidationETA(string strIdSession, string vstrTipTra, string vstrIdPLano)
        {
            string v_TipoOrden;

            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a ValidationETA -> Parametros de entrada => vstrTipTra: " + vstrTipTra + " vstrIdPLano: " + vstrIdPLano); // Temporal
            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc()
            {
                audit = audit,
                vIdtiptra = (vstrTipTra.IndexOf("|") == Claro.Constants.NumberOneNegative ? vstrTipTra : vstrTipTra.Split('|')[0])
            };

            OrderTypesResponseHfc objOrderTypesResponse;
            ETAFlowResponseHfc objETAFlowResponse;
            Helpers.CommonServices.GenericItem objGenericItem;

            try
            {
                objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderTypesResponseHfc>(() => { return _oServiceFixed.GetOrderType(objOrderTypesRequest); });

                if (objOrderTypesResponse.ordertypes.Count == 0)
                    v_TipoOrden = CSTS.Constants.strMenosUno;
                else
                    v_TipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;

                ETAFlowRequestHfc objETAFlowReques = new ETAFlowRequestHfc()
                {
                    audit = audit,
                    as_origen = ConfigurationManager.AppSettings("gConstHFCOrigen"),
                    av_idplano = vstrIdPLano,
                    av_ubigeo = string.Empty,
                    an_tiptra = CSTS.Functions.CheckInt(objOrderTypesRequest.vIdtiptra),
                    an_tipsrv = ConfigurationManager.AppSettings("gConstHFCTipoServicio")
                };

                objETAFlowResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ETAFlowResponseHfc>(() =>
                {
                    return _oServiceFixed.ETAFlowValidate(objETAFlowReques);
                });

                objGenericItem = new Helpers.CommonServices.GenericItem();
                //objGenericItem.Condition = objETAFlowResponse.rResult;
                objGenericItem.Description = string.Empty;
                objGenericItem.Code = CSTS.Functions.CheckStr(objETAFlowResponse.ETAFlow.an_indica);
                objGenericItem.Code2 = objETAFlowResponse.ETAFlow.as_codzona + "|" + vstrIdPLano + "|" + v_TipoOrden;

                switch (objETAFlowResponse.ETAFlow.an_indica)
                {
                    case -1:
                        objGenericItem.Description = CSTS.Functions.GetValueFromConfigFile("strMsgNoExistePlano", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -2:
                        objGenericItem.Description = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -3:
                        objGenericItem.Description = CSTS.Functions.GetValueFromConfigFile("strMsgNoExistePlanoUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -4:
                        objGenericItem.Description = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteTipoTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -5:
                        objGenericItem.Description = CSTS.Functions.GetValueFromConfigFile("strMsgNoExisteTipoServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case 1:
                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariable_OK; break;
                    case 0:
                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariableNO_OK; break;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a ValidationETA -> Parametros de salida => objGenericItem.Description: " + objGenericItem.Description + " objGenericItem.Code: " + objGenericItem.Code + " objGenericItem.Code2: " + objGenericItem.Code2); // Temporal
            return Json(new { data = objGenericItem });
        }
        public JsonResult GetProductDetail(string strIdSession, string strContratoID, string strCustomerID)
        {
            FixedTransacService.ProductDetailResponse objProductDetailResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetProductDetail"); // Temporal
            FixedTransacService.ProductDetailRequest objProductDetailRequest = new FixedTransacService.ProductDetailRequest()
            {
                audit = audit,
                vCoId = strContratoID,
                vCustomerId = strCustomerID,
                tipoBusqueda = CSTS.Constants.numeroDos,
            };

            try
            {
                objProductDetailResponse =
                Claro.Web.Logging.ExecuteMethod<FixedTransacService.ProductDetailResponse>(() =>
                {
                    return _oServiceFixed.GetProductDetail(objProductDetailRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProductDetailRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetProductDetail"); // Temporal
            return Json(new { data = objProductDetailResponse.listDecoder });
        }
        public JsonResult GetProductDown(string strIdSession, string strContratoID, string strCustomerID)
        {
            FixedTransacService.ProductDetailResponse objProductDetailResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetProductDown"); // Temporal
            FixedTransacService.ProductDetailRequest objProductDetailRequest = new FixedTransacService.ProductDetailRequest()
            {
                audit = audit,
                vCoId = strContratoID,
                vCustomerId = strCustomerID,
                tipoBusqueda = CSTS.Constants.numeroDos,
            };

            try
            {
                objProductDetailResponse =
                Claro.Web.Logging.ExecuteMethod<FixedTransacService.ProductDetailResponse>(() =>
                {
                    return _oServiceFixed.GetProductDown(objProductDetailRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProductDetailRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objUnistallInstallationOfDecoderModel = null;
            if (objProductDetailResponse != null && objProductDetailResponse.listDecoder != null)
            {
                objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
                List<Models.UnistallInstallationOfDecoder.Decoder> List = new List<Models.UnistallInstallationOfDecoder.Decoder>();

                foreach (var item in objProductDetailResponse.listDecoder)
                {
                    List.Add(new Models.UnistallInstallationOfDecoder.Decoder()
                    {
                        codigo_material = item.codigo_material,
                        codigo_sap = item.codigo_sap,
                        numero_serie = item.numero_serie,
                        id_producto = item.id_producto,
                        modelo = item.modelo,
                        descripcion_material = item.descripcion_material,
                        TIPODECO = item.TIPODECO,
                        tipo_equipo = item.tipo_equipo,
                        CARGO_FIJO = (item.CARGO_FIJO == "" ? "0" : item.CARGO_FIJO),
                        CARGO_FIJO_IGV = CSTS.Functions.CheckStr(CSTS.Functions.CheckDbl((item.CARGO_FIJO == "" ? "0" : item.CARGO_FIJO)) * (CSTS.Functions.CheckDbl(item.PORCENTAJE_IGV) * 0.01 + 1)),
                        tipoServicio = item.tipoServicio,
                        DesTipoServ = (item.tipoServicio == CSTS.Constants.strCero ? "ADICIONAL" : "INCLUIDO")
                    });
                }

                objUnistallInstallationOfDecoderModel.ListDecoder = List;
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetProductDown"); // Temporal
            return Json(new { data = objUnistallInstallationOfDecoderModel.ListDecoder });
        }
        public JsonResult GetAddtionalEquipment(string strIdSession, string idplan, string coid)
        {
            List<Models.UnistallInstallationOfDecoder.PlanService> listaFinal = null;
            FixedTransacService.AddtionalEquipmentResponse objAddtionalEquipmentResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetAddtionalEquipment"); // Temporal
            FixedTransacService.AddtionalEquipmentRequest objAddtionalEquipmentRequest = new FixedTransacService.AddtionalEquipmentRequest()
            {
                audit = audit,
                IdPlan = idplan,
                coid = coid,
                TypeProduct = ConfigurationManager.AppSettings("strProductoHFC")
            };

            try
            {
                objAddtionalEquipmentResponse =
                Claro.Web.Logging.ExecuteMethod<FixedTransacService.AddtionalEquipmentResponse>(() =>
                {
                    return _oServiceFixed.GetAddtionalEquipment(objAddtionalEquipmentRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAddtionalEquipmentRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objUnistallInstallationOfDecoderModel = null;
            if (objAddtionalEquipmentResponse != null && objAddtionalEquipmentResponse.LstPlanServices != null)
            {
                objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
                var lista = objAddtionalEquipmentResponse.LstPlanServices;

                for (int index1 = 0; index1 < lista.Count; index1++)
                {
                    for (int index2 = 0; index2 < lista.Count; index2++)
                    {
                        if (lista[index1].CodTipoServ.CompareTo(lista[index2].CodTipoServ) < 0)
                        {
                            var aux = lista[index1];
                            lista[index1] = lista[index2];
                            lista[index2] = aux;
                        }
                    }
                }

                listaFinal = new List<Models.UnistallInstallationOfDecoder.PlanService>();
                string CodServSisact = string.Empty;
                bool existe = false;
                for (int index3 = 0; index3 < lista.Count; index3++)
                {
                    CodServSisact = lista[index3].CodServSisact;
                    existe = false;
                    for (int index4 = 0; index4 < listaFinal.Count; index4++)
                    {
                        if (listaFinal[index4].CodServSisact.Equals(CodServSisact))
                            existe = true;
                    }

                    if (!existe)
                    {
                        Models.UnistallInstallationOfDecoder.PlanService PlanService = new Models.UnistallInstallationOfDecoder.PlanService()
                        {
                            CodigoPlan = lista[index3].CodigoPlan,
                            DescPlan = lista[index3].DescPlan,
                            TmCode = lista[index3].TmCode,
                            Solucion = lista[index3].Solucion,
                            CodServSisact = lista[index3].CodServSisact,
                            SNCode = lista[index3].SNCode,
                            SPCode = lista[index3].SPCode,
                            CodTipoServ = lista[index3].CodTipoServ,
                            TipoServ = lista[index3].TipoServ,
                            DesServSisact = lista[index3].DesServSisact,
                            CodGrupoServ = lista[index3].CodGrupoServ,
                            GrupoServ = lista[index3].GrupoServ,
                            CF = lista[index3].CF,
                            IdEquipo = lista[index3].IdEquipo,
                            Equipo = lista[index3].Equipo,
                            CantidadEquipo = lista[index3].CantidadEquipo,
                            //MatvIdSap = lista[index3].CodigoPlan,
                            MatvIdSap = lista[index3].MatvIdSap,
                            //MatvDesSap = lista[index3].CodigoPlan,
                            MatvDesSap = lista[index3].MatvDesSap,
                            TipoEquipo = lista[index3].TipoEquipo,
                            CodigoExterno = lista[index3].CodigoExterno,
                            DesCodigoExterno = lista[index3].DesCodigoExterno,
                            ServvUsuarioCrea = lista[index3].ServvUsuarioCrea,
                        };
                        listaFinal.Add(PlanService);
                    }
                }
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetAddtionalEquipment"); // Temporal
            return Json(new { data = listaFinal });
        }
        public JsonResult GetProcessingServices(string strIdSession, string strContratoID, string strCustomerID, string strCadena, Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ProcessingServicesRequest objProcessingServicesRequest = new FixedTransacService.ProcessingServicesRequest();
            FixedTransacService.ProcessingServicesResponse objProcessingServicesResponse = new ProcessingServicesResponse();
            try
            {
                InsertETASelectionResponse objInsertETASelectionResponse = null;
                InsertETASelectionRequest objInsertETASelectionRequest = null;
                if (model.ValidaEta == CSTS.Constants.strUno || model.ValidaEta == CSTS.Constants.strDos)
                {
                    if (Functions.CheckInt(model.CodigoRequestAct) > CSTS.Constants.numeroCero)
                    {
                        if (model.FechaProgramacion != null || model.FechaProgramacion != string.Empty)
                        {
                            if (model.FranjaHora != null)
                            {
                                objInsertETASelectionResponse = new InsertETASelectionResponse();
                                objInsertETASelectionRequest = new InsertETASelectionRequest()
                                {
                                    audit = audit,
                                    vidconsulta = CSTS.Functions.CheckInt(model.CodigoRequestAct),
                                    vidInteraccion = CSTS.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - model.CodigoRequestAct.Trim().Length) + model.CodigoRequestAct.Trim(),
                                    vfechaCompromiso = DateTime.Parse(model.FechaProgramacion),
                                    vfranja = model.FranjaHorariaFinal.Split('+')[0],
                                    vid_bucket = model.FranjaHorariaFinal.Split('+')[1]
                                };

                                objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<InsertETASelectionResponse>(() => { return _oServiceFixed.GetInsertETASelection(objInsertETASelectionRequest); });
                            }
                        }
                    }
                }

                
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetProcessingServices"); // Temporal
                objProcessingServicesRequest = new FixedTransacService.ProcessingServicesRequest()
            {
                audit = audit,
                vCoId = strContratoID,
                vCustomerId = strCustomerID,
                vCadena = strCadena.Substring(0, strCadena.Length - CSTS.Constants.numeroUno)
            };

                objProcessingServicesResponse =
                Claro.Web.Logging.ExecuteMethod<FixedTransacService.ProcessingServicesResponse>(() =>
                {
                    return _oServiceFixed.GetProcessingServices(objProcessingServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objProcessingServicesRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            //var data = CSTS.Functions.CheckStr(objProcessingServicesResponse.rResultado) + "|" + objProcessingServicesResponse.rMensaje + "|" + CSTS.Functions.CheckStr(objProcessingServicesResponse.rResult);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetProcessingServices"); // Temporal
            return Json(new { data = objProcessingServicesResponse });
        }
        public JsonResult GetLoyaltyAmount(string strIdSession, int iTipo)
        {
            Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objUnistallInstallationOfDecoderModel = null;
            FixedTransacService.LoyaltyAmountResponse objLoyaltyAmountResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetLoyaltyAmount"); // Temporal
            FixedTransacService.LoyaltyAmountRequest objLoyaltyAmountRequest = new FixedTransacService.LoyaltyAmountRequest()
            {
                audit = audit,
                iTipo = iTipo
            };

            try
            {
                objLoyaltyAmountResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.LoyaltyAmountResponse>(() =>
                {
                    return _oServiceFixed.GetLoyaltyAmount(objLoyaltyAmountRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objLoyaltyAmountRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            if (objLoyaltyAmountResponse != null && objLoyaltyAmountResponse.strMonto != null)
            {
                objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
                objUnistallInstallationOfDecoderModel.MontoFidelizacion = (objLoyaltyAmountResponse.strMonto.Trim() == "" ? CSTS.Constants.strCero : objLoyaltyAmountResponse.strMonto);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetLoyaltyAmount"); // Temporal
            return Json(new { data = objUnistallInstallationOfDecoderModel.MontoFidelizacion });
        }

        #region SaveTransaction
        public JsonResult SaveTransaction(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model)
        {
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "Begin a SaveTransaction"); // Temporal
            return Json(new { data = Transaction(model) });
        }
        public Dictionary<string, object> Transaction(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model)
        {
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "Begin a Transaction"); // Temporal
            var strMensaje = string.Empty;
            var listTrasaction = new Dictionary<string, object>();
            var objInteractionModel = new Model.InteractionModel();
            var oPlantillaData = new Model.TemplateInteractionModel();

            //Obtener Datos de Interaccion
            objInteractionModel = DataInteraction(model);

            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");


            //Obtener Datos de Plantilla de Interaccion
            oPlantillaData = DataTemplateInteraction(model);

            var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.oDatosCliente.CUSTOMER_ID;

            //Insertar Interacción
            var resultInteraction = InsertInteraction(objInteractionModel, oPlantillaData, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, model.IdSession, model.oDatosCliente.CUSTOMER_ID);
            string strInteraccionId = (string)resultInteraction["rInteraccionId"];

            if (strInteraccionId == string.Empty || strInteraccionId == null)
            {
                strMensaje = CSTS.Functions.GetValueFromConfigFile("gConstKeyGenTipificacionDeco", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                listTrasaction.Add("name", "Interaccion");
                listTrasaction.Add("message", strMensaje);
                return listTrasaction;
            }

            if (model.FlajInstDesins == CSTS.Constants.strCero)
            {
                //Insertar los Decos Adicionales
                var resultadoDecoAdicional = InsertDecoAdditional(model, strInteraccionId);
                if (resultadoDecoAdicional == CSTS.Constants.grstFalse)
                {
                    strMensaje = CSTS.Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    listTrasaction.Add("name", "Deco");
                    listTrasaction.Add("message", strMensaje);
                    listTrasaction.Add("btnConstancia", true);
                    return listTrasaction;
                }
            }
            else
            {
                //Insertar los Decos a dar de Baja
                var resultadoBajaDeco = InsertDownDeco(model, strInteraccionId);
                if (resultadoBajaDeco == CSTS.Constants.grstFalse)
                {
                    strMensaje = CSTS.Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    listTrasaction.Add("name", "Deco");
                    listTrasaction.Add("message", strMensaje);
                    listTrasaction.Add("btnConstancia", true);
                    return listTrasaction;
                }
            }

            //Generar SOT
            InsertTransactionResponse resultadoSOT = null;
            if (model.FlajInstDesins == CSTS.Constants.strCero)
            {
                resultadoSOT = InsertSOT(model, strInteraccionId);

                if (resultadoSOT.intNumSot == null || resultadoSOT.intNumSot == string.Empty || resultadoSOT.intNumSot.ToUpper() == "NULL")
                {
                    if (resultadoSOT.rintResCod.Equals(CSTS.Constants.strTres))
                    {
                        strMensaje = CSTS.Functions.GetValueFromConfigFile("strMsgSOTEnCursoMP", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    else
                    {
                        strMensaje = CSTS.Functions.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }


                    listTrasaction.Add("name", "SOT");
                    listTrasaction.Add("message", strMensaje);
                    listTrasaction.Add("btnConstancia", true);
                    return listTrasaction;
                }
            }

            //Insertar OCC
            string idSession = model.IdSession;
            int codSot = CSTS.Constants.numeroCero;

            if (model.FlajInstDesins == CSTS.Constants.strUno)
            { //Desinstalacion
                codSot = CSTS.Functions.CheckInt(model.SotDeBaja);
                AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);

                if (Double.Parse(model.SotDeBaja) > 1)
                {
                    FixedTransacService.UpdateInter29Response objUpdateInter29Response = new FixedTransacService.UpdateInter29Response();
                    FixedTransacService.UpdateInter29Request objUpdateInter29Request = new FixedTransacService.UpdateInter29Request()
                    {

                        audit = audit,
                        p_objid = strInteraccionId,
                        p_texto = model.SotDeBaja,
                        p_orden = Claro.SIACU.Transac.Service.Constants.strLetraI
                    };

                    objUpdateInter29Response = Claro.Web.Logging.ExecuteMethod<FixedTransacService.UpdateInter29Response>(() =>
                    {
                        return _oServiceFixed.GetUpdateInter29(objUpdateInter29Request);
                    });

                    Claro.Web.Logging.Info(model.IdSession, audit.transaction, string.Format("OUT GetUpdateInter29 -  rFlagInsercion: {0}", objUpdateInter29Response.rFlagInsercion == null ? "" : objUpdateInter29Response.rFlagInsercion));

                }
            }
            else //Instalacion
                codSot = CSTS.Functions.CheckInt(resultadoSOT.intNumSot);

            int customerId = CSTS.Functions.CheckInt(model.oDatosCliente.CUSTOMER_ID);
            var mes = DateTime.Now.Month + 1;
            var anio = DateTime.Now.Year;
            if (mes == 13) { mes = 1; anio = anio + 1; }
            DateTime fechaVig = CSTS.Functions.CheckDate(model.oDatosCliente.CICLO_FACTURACION.Trim() + "/" + mes.ToString().Trim() + "/" + anio.ToString().Trim());
            double monto = CSTS.Functions.CheckDbl(model.MontoFidelizacion);
            string comentario = ConfigurationManager.AppSettings("gConstComentarioIDD");
            int flag = CSTS.Functions.CheckInt(model.FlajFidelizacion);
            string aplicacion = ConfigurationManager.AppSettings("ApplicationName");
            string usuarioAct = model.oUsuarioAcceso.USRREGIS;
            var fechaAct = DateTime.Now;
            string codId = model.oDatosCliente.CONTRATO_ID;

            bool resulatdoFidelizacion = GetInsertLoyalty(idSession, model.oDatosCliente.CUSTOMER_ID, CSTS.Functions.CheckStr(codSot), usuarioAct);

            if (resulatdoFidelizacion)
            { bool resultadoOCC = GetSaveOCC(idSession, codSot, customerId, fechaVig, monto, comentario, flag, aplicacion, usuarioAct, fechaAct, codId); }

            //Generar constancia
            bool generadoPDF = false;
            string strRutaArchivo = string.Empty;
            Dictionary<string, object> oConstancyPDF = new Dictionary<string, object>();
            
            oConstancyPDF = GetConstancyPDF(model, strInteraccionId, model.FlajInstDesins);
            generadoPDF = (bool)(oConstancyPDF["respuesta"]);
            strRutaArchivo = CSTS.Functions.CheckStr(oConstancyPDF["ruta"]);

            model.rutaArchivo = strRutaArchivo;
            model.nombreArchivo = (string)oConstancyPDF["nombreArchivo"];

            //Enviar Correo
            if (model.FlajCorreo == CSTS.Constants.strUno)
            {
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string strDestinatario = model.Correo;
                string strAsunto = string.Empty;
                string strNombreArchivoPDF = string.Empty;

                if (model.FlajInstDesins == CSTS.Constants.strCero) // Instalacion
                {  
                    strAsunto = ConfigurationManager.AppSettings("strCorreoAsuntoInstalacion");
                    strNombreArchivoPDF = ConfigurationManager.AppSettings("strNombreArchivoInstalacion");
                }
                else
                { 
                    strAsunto = ConfigurationManager.AppSettings("strCorreoAsuntoDesinstalacion");
                    strNombreArchivoPDF = ConfigurationManager.AppSettings("strNombreArchivoDesinstalacion");
                }

                strMensaje = MailBody(model.IdSession, model.FlajInstDesins);

                //Obtener RutaLocal
                //strRutaArchivo = DownloandFilePDF(model.IdSession, strRutaArchivo, strNombreArchivoPDF);
                //Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "Transaction -> Ruta Archivo Local : strRutaArchivo: " + strRutaArchivo); // Temporal

                SendEmail(model.IdSession, strRemitente, strDestinatario, strMensaje, strNombreArchivoPDF, strAsunto, strRutaArchivo);
                //Eliminar archivo creado
                //if (strRutaArchivo != string.Empty)
                //    DeleteFilePDF(model.IdSession, strRutaArchivo);
            }

            //Auditoria
            string strTransacion = ConfigurationManager.AppSettings("gConstCodigoAuditoriaInstalacionDecodificadores");
            string strDesTransaccion = CSTS.Functions.GetValueFromConfigFile("DecoAdicionalAudit", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

            string strIdSession = model.IdSession;
            string strCuentaUsuario = CurrentUser(model.IdSession);
            string strIPServidor = model.IpServidor;
            string strMonto = CSTS.Constants.strCero;
            string strIpCliente = "";
            string strNombreCliente = model.oDatosCliente.NOMBRE_COMPLETO;
            string strNombreServidor = model.Sn;
            string strServicio = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");
            string strTelefono = model.oDatosCliente.CUSTOMER_ID;
            string strTexto = "/Ip Cliente: " + strIpCliente + "/Usuario: " + strCuentaUsuario + "/Opcion: " + strDesTransaccion + "/Fecha y Hora: " + DateTime.Now;
            SaveAudit(strIdSession, strCuentaUsuario, strIpCliente, strIPServidor, strMonto, strNombreCliente, strNombreServidor, strServicio, strTelefono, strTexto, strTransacion);

            strMensaje = CSTS.Functions.GetValueFromConfigFile("gConstMsgOVExito", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            listTrasaction.Add("name", "Exito");
            listTrasaction.Add("message", strMensaje);
            listTrasaction.Add("idInteraccion", strInteraccionId);
            listTrasaction.Add("rutaArchivo", model.rutaArchivo);
            listTrasaction.Add("nombreArchivo", model.nombreArchivo);
            listTrasaction.Add("codsot", CSTS.Functions.CheckStr(codSot));

            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "End a Transaction parametros de salida -> message: " + strMensaje + " idInteraccion: " + strInteraccionId + " rutaArchivo: " + model.rutaArchivo); // Temporal

            return listTrasaction;
        }
        //Interacción
        public Model.InteractionModel DataInteraction(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model)
        {
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "Begin a DataInteraction"); // Temporal
            string nombreTransaccion = string.Empty;
            var objInteractionModel = new Model.InteractionModel();

            if (model.FlajInstDesins == CSTS.Constants.strCero)// Instalacion
                nombreTransaccion = ConfigurationManager.AppSettings("gstrTRANSACCION_INSTALAC_DECO_ADICIONAL");
            else
                nombreTransaccion = ConfigurationManager.AppSettings("gstrTRANSACCION_DESINSTALAC_DECO_ADICIONAL");

            //Obtener Tipificacion
            var tipification = GetTypificationHFC(model.IdSession, nombreTransaccion);

            tipification.ToList().ForEach(x =>
            {
                objInteractionModel.Type = x.Type;
                objInteractionModel.Class = x.Class;
                objInteractionModel.SubClass = x.SubClass;
                objInteractionModel.InteractionCode = x.InteractionCode;
                objInteractionModel.TypeCode = x.TypeCode;
                objInteractionModel.ClassCode = x.ClassCode;
                objInteractionModel.SubClassCode = x.SubClassCode;
            });

            //ObtenerCliente
            string strFlgRegistrado = CSTS.Constants.strUno;
            var phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.oDatosCliente.CUSTOMER_ID;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = phone,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
            objInteractionModel.DateCreaction = DateTime.Now.ToString();
            objInteractionModel.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + model.oDatosCliente.CUSTOMER_ID;
            objInteractionModel.Type = objInteractionModel.Type;
            objInteractionModel.Class = objInteractionModel.Class;
            objInteractionModel.SubClass = objInteractionModel.SubClass;
            objInteractionModel.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
            objInteractionModel.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
            objInteractionModel.Result = ConfigurationManager.AppSettings("Ninguno");
            objInteractionModel.MadeOne = CSTS.Constants.strCero;
            objInteractionModel.Note = model.Nota;
            objInteractionModel.FlagCase = CSTS.Constants.strCero;
            objInteractionModel.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
            objInteractionModel.Contract = model.oDatosCliente.CONTRATO_ID;
            objInteractionModel.Plan = model.oDatosLinea.Plan;
            objInteractionModel.Agenth = model.oUsuarioAcceso.USRREGIS; // audit.userName; //"C12640";//por revisar
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: ", "End a DataInteraction"); // Temporal
            return objInteractionModel;
        }
        public Model.TemplateInteractionModel DataTemplateInteraction(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();

            oPlantillaCampoData.X_FIRST_NAME = model.oDatosCliente.NOMBRE_COMPLETO;
            oPlantillaCampoData.X_BASKET = model.oDatosLinea.Plan;
            oPlantillaCampoData.X_MARITAL_STATUS = DateTime.Now.Date.ToShortDateString();
            oPlantillaCampoData.X_INTER_1 = model.oDatosCliente.CICLO_FACTURACION;
            oPlantillaCampoData.X_CLARO_LDN1 = model.oDatosCliente.NRO_DOC;
            oPlantillaCampoData.X_INTER_3 = model.oDatosLinea.FecActivacion;
            oPlantillaCampoData.X_INTER_5 = model.oDatosLinea.StatusLinea;
            oPlantillaCampoData.X_INTER_7 = model.oDatosCliente.DIRECCION_DESPACHO;
            oPlantillaCampoData.X_INTER_15 = model.PuntoAtencion;

            oPlantillaCampoData.X_INTER_16 = model.oDatosCliente.DEPARTEMENTO_LEGAL;
            oPlantillaCampoData.X_INTER_17 = model.oDatosCliente.DISTRITO_LEGAL;
            oPlantillaCampoData.X_INTER_18 = model.oDatosCliente.PAIS_LEGAL;
            oPlantillaCampoData.X_INTER_19 = model.oDatosCliente.PROVINCIA_LEGAL;
            oPlantillaCampoData.X_INTER_20 = model.oDatosCliente.CODIGO_PLANO_INST;

            oPlantillaCampoData.X_INTER_21 = model.Cantidad;
            oPlantillaCampoData.X_INTER_22 = CSTS.Functions.CheckDbl(model.CargoFijoTotalPlanSIGV);
            oPlantillaCampoData.X_INTER_23 = CSTS.Functions.CheckDbl(model.CargoFijoTotalPlanCIGV);
            oPlantillaCampoData.X_INTER_24 = CSTS.Functions.CheckDbl(model.CargoFijoTotal);

            oPlantillaCampoData.X_CLARO_LDN2 = model.FlajCorreo; //Flaj Correo
            oPlantillaCampoData.X_EMAIL = model.Correo; // Correo
            oPlantillaCampoData.X_CLARO_LDN4 = model.FlajFidelizacion; // Flaj Fidelizacion 1 = SI y 0 = NO
            oPlantillaCampoData.X_CLAROLOCAL1 = model.MontoFidelizacion; // Monto Fidelizacion
            oPlantillaCampoData.X_CLAROLOCAL2 = model.FlajInstDesins; // Flaj Instalacion = 0 y Desinstlacion = 1
            oPlantillaCampoData.X_INTER_25 = model.IGV; // IGV Para el historial

            //Dim vExiste As Integer
            //Dim objItemTipTra = obtenerTipoTrabajo(vExiste)
            //If vExiste = CInt(NumeracionUNO) Then
            //    oPlantillaCampoData.X_INTER_29 = objItemTipTra.Descripcion
            //Else
            //    oPlantillaCampoData.X_INTER_29 = hdnTipoTrabajo.Value 'gstrGuion
            //End If
            //oPlantillaCampoData.X_INTER_29 = model.TipoTrabajo;

            oPlantillaCampoData.X_INTER_6 = CSTS.Constants.strCero; //intNroOST
            oPlantillaCampoData.X_DISTRICT = model.oDatosCliente.URBANIZACION_LEGAL;

            oPlantillaCampoData.X_INTER_30 = model.Nota;
            oPlantillaCampoData.X_FIRST_NAME = model.oDatosCliente.NOMBRES;
            oPlantillaCampoData.X_LAST_NAME = model.oDatosCliente.APELLIDOS;
            oPlantillaCampoData.X_DOCUMENT_NUMBER = model.oDatosCliente.NRO_DOC;

            if (model.FlajInstDesins == CSTS.Constants.strCero)// Instalacion
                oPlantillaCampoData.NOMBRE_TRANSACCION = ConfigurationManager.AppSettings("gstrTRANSACCION_INSTALAC_DECO_ADICIONAL");
            else
                oPlantillaCampoData.NOMBRE_TRANSACCION = ConfigurationManager.AppSettings("gstrTRANSACCION_DESINSTALAC_DECO_ADICIONAL");
            //oPlantillaCampoData.NOMBRE_TRANSACCION = model.TransaccionDTH; //Nombre de Trasacción Instalación o Desinstalación

            oPlantillaCampoData.X_REGISTRATION_REASON = model.oDatosCliente.CONTRATO_ID;

            oPlantillaCampoData.X_CLARO_NUMBER = model.oDatosCliente.CONTRATO_ID;
            oPlantillaCampoData.X_TYPE_DOCUMENT = model.oDatosCliente.TIPO_CLIENTE;
            oPlantillaCampoData.X_ADDRESS5 = model.oDatosCliente.DIRECCION_DESPACHO;
            oPlantillaCampoData.X_CITY = model.oDatosCliente.UBIGEO_INST;

            Claro.Web.Logging.Info("IdSession: ", " Transaccion: ", "Begin a DataTemplateInteraction CONTACTO_CLIENTE: " + model.oDatosCliente.CONTACTO_CLIENTE); // Temporal
            oPlantillaCampoData.X_REASON = model.oDatosCliente.RAZON_SOCIAL;
            oPlantillaCampoData.X_POSITION = model.FechaProgramacion;

            oPlantillaCampoData.X_OCCUPATION = DateTime.Now.ToShortDateString();

            return oPlantillaCampoData;
        }
        public Dictionary<string, object> InsertInteraction(Model.InteractionModel objInteractionModel,
                                                    Model.TemplateInteractionModel oPlantillaData,
                                                    string strNroTelephone,
                                                    string strUserSession,
                                                    string strUserAplication,
                                                    string strPassUser,
                                                    bool boolEjecutTransaction,
                                                    string strIdSession,
                                                    string strCustomerId)
        {
            string ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            string strTelefono;

            strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = CSTS.Constants.strUno;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a InsertInteraction"); // Temporal
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };

            objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            //Validacion de Contingencia
            var result = new Dictionary<string, string>();
            if (ContingenciaClarify != CSTS.Constants.blcasosVariableSI)
            {
                result = GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                result = GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }

            var model = new List<string>();
            foreach (KeyValuePair<string, string> par in result)
            {
                model.Add(par.Value);
            }

            var rInteraccionId = model[0];

            var dictionaryResponse = new Dictionary<string, object>();
            if (rInteraccionId != string.Empty && rInteraccionId != null)
            {
                if (oPlantillaData != null)
                {
                    dictionaryResponse = InsertPlantInteraction(oPlantillaData, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }

            dictionaryResponse.Add("rInteraccionId", rInteraccionId);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a InsertInteraction"); // Temporal
            return dictionaryResponse;
        }
        //Save ETA Service
        public FixedTransacService.GenerateSOTResponseFixed registrarEtaSot(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objGetRecordTransactionRequest)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objGetRecordTransactionRequest.IdSession);

            objRequestGenerateSOT.vCoID = objGetRecordTransactionRequest.CodSot;
            objRequestGenerateSOT.idSubTypeWork = objGetRecordTransactionRequest.strSubTypeWork.Split('|')[2];
            objRequestGenerateSOT.vFranja = objGetRecordTransactionRequest.FranjaHorariaFinal.Split('+')[0];
            objRequestGenerateSOT.idBucket = objGetRecordTransactionRequest.FranjaHorariaFinal.Split('+')[1];
            objRequestGenerateSOT.vFeProg = objGetRecordTransactionRequest.FechaProgramacion;
            try
            {
                Claro.Web.Logging.Info(objGetRecordTransactionRequest.IdSession, audit.transaction, "IN registrarEtaSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return _oServiceFixed.registraEtaSot(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(objGetRecordTransactionRequest.IdSession, audit.transaction, string.Format("OUT registrarEtaSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.IdSession, audit.transaction, ex.Message);
            }

            return objResponseGenerateSOT;
        }

        //Deco Adicional
        public string InsertDecoAdditional(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model, string strInteraccionId)
        {
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.IdSession);
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "Begin a InsertDecoAdditional"); // Temporal
            InsertDecoAdditionalResponse objInsertDecoAdditionalResponse = null;
            InsertDetailServiceInteractionResponse objInsertDetailServiceInteractionResponse = null;
            UpdatexInter30Response objUpdatexInter30Response = null;

            var listaDecoAdicional = model.ContenidoEquipo.Split(';');

            for (int i = 0; i < listaDecoAdicional.Length; i++)
            {
                var DecoAdicional = listaDecoAdicional[i].Split('|');
                var lista = GetDecoDetailByIdService(model, DecoAdicional[0].ToString());
                foreach (DetailInteractionService item in lista)
                {
                    if (item.IdServicio == null || item.IdServicio == string.Empty)
                    {
                        InsertDecoAdditionalRequest objInsertDecoAdditionalRequest = new InsertDecoAdditionalRequest()
                        {
                            audit = audit,
                            vInter = strInteraccionId,
                            vServ = item.IdServicio,
                            vGrupoPrincipal = item.GsrvcPrincipal,
                            vGrupo = item.GsrvcCodigo,
                            vCantidadInst = item.Cantidad,
                            vDscServ = item.Servicio,
                            vBandwid = item.Bandwid,
                            vFlagLc = item.FlagLc,
                            vCantIdLinea = item.CantidadIdLinea,
                            vTipoEquipo = item.IdEquipo,
                            vCodTipoEquipo = item.CodTipEqu,
                            vCantidad = item.CantEquipo,
                            vDscEquipo = item.Equipo,
                            vCodigoExt = item.CodigoExt,
                            vSnCode = DecoAdicional[1],
                            vSpCode = DecoAdicional[3],
                            vCargoFijo = DecoAdicional[11]
                        };

                        objInsertDecoAdditionalResponse = Claro.Web.Logging.ExecuteMethod<InsertDecoAdditionalResponse>(() => { return _oServiceFixed.GetInsertDecoAdditional(objInsertDecoAdditionalRequest); });

                        if (objInsertDecoAdditionalResponse.rResultado == false)
                            break;
                    }
                }

                InsertDetailServiceInteractionRequest objInsertDetailServiceInteractionRequest = new InsertDetailServiceInteractionRequest()
                {
                    audit = audit,
                    codinterac = strInteraccionId,
                    nombreserv = DecoAdicional[4],
                    tiposerv = DecoAdicional[5],
                    gruposerv = DecoAdicional[6],
                    cf = DecoAdicional[7],
                    equipo = DecoAdicional[8],
                    cantidad = DecoAdicional[9]
                };

                objInsertDetailServiceInteractionResponse = Claro.Web.Logging.ExecuteMethod<InsertDetailServiceInteractionResponse>(() => { return _oServiceFixed.GetInsertDetailServiceInteraction(objInsertDetailServiceInteractionRequest); });

                if (objInsertDecoAdditionalResponse.rResultado == false || objInsertDetailServiceInteractionResponse.rResul == false)
                    break;
            }

            if (objInsertDecoAdditionalResponse.rResultado == false || objInsertDetailServiceInteractionResponse.rResul == false)
            {
                UpdatexInter30Request objUpdatexInter30Request = new UpdatexInter30Request()
                {
                    audit = auditCommon,
                    p_objid = strInteraccionId,
                    p_texto = CSTS.Functions.GetValueFromConfigFile("strMensajeErrorparaNotasClfy", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
                };

                objUpdatexInter30Response = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return _oServiceCommon.GetUpdatexInter30(objUpdatexInter30Request); });

                return CSTS.Constants.grstFalse;
            }
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "End a InsertDecoAdditional"); // Temporal
            return CSTS.Constants.grstTrue;
        }
        //Baja Deco
        public string InsertDownDeco(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model, string strInteraccionId)
        {
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.IdSession);
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "Begin a InsertDownDeco"); // Temporal
            InsertDetailServiceInteractionResponse objInsertDetailServiceInteractionResponse = null;
            UpdatexInter30Response objUpdatexInter30Response = null;

            var listaBajaDeco = model.ContenidoEquipo.Split(';');

            for (int i = 0; i < listaBajaDeco.Length; i++)
            {
                var BajaDeco = listaBajaDeco[i].Split('|');
                InsertDetailServiceInteractionRequest objInsertDetailServiceInteractionRequest = new InsertDetailServiceInteractionRequest()
                {
                    audit = audit,
                    codinterac = strInteraccionId,
                    nombreserv = BajaDeco[4], //Nombre del Equipo
                    tiposerv = BajaDeco[5], //Tipo de Equipo (ADICINAL)
                    gruposerv = BajaDeco[6], //Grupo Servicio (SE ENVIA VACIO)
                    cf = BajaDeco[7], //Cargo Fijo Con IGV
                    equipo = BajaDeco[8], //VACIO
                    cantidad = BajaDeco[9] //VACIO
                };

                objInsertDetailServiceInteractionResponse = Claro.Web.Logging.ExecuteMethod<InsertDetailServiceInteractionResponse>(() => { return _oServiceFixed.GetInsertDetailServiceInteraction(objInsertDetailServiceInteractionRequest); });

                if (objInsertDetailServiceInteractionResponse.rResul == false)
                    break;
            }

            if (objInsertDetailServiceInteractionResponse.rResul == false)
            {
                UpdatexInter30Request objUpdatexInter30Request = new UpdatexInter30Request()
                {
                    audit = auditCommon,
                    p_objid = strInteraccionId,
                    p_texto = CSTS.Functions.GetValueFromConfigFile("strMensajeErrorparaNotasClfy", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
                };

                objUpdatexInter30Response = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return _oServiceCommon.GetUpdatexInter30(objUpdatexInter30Request); });

                return CSTS.Constants.grstFalse;
            }
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "End a InsertDownDeco"); // Temporal
            return CSTS.Constants.grstTrue;
        }
        public List<DetailInteractionService> GetDecoDetailByIdService(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model, string IdServicio)
        {
            List<DetailInteractionService> lista = null;
            DetailInteractionService item = null;
            FixedTransacService.DecoDetailByIdServiceResponse objDecoDetailByIdServiceResponse;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(model.IdSession);
            FixedTransacService.DecoDetailByIdServiceRequest objDecoDetailByIdServiceRequest = new FixedTransacService.DecoDetailByIdServiceRequest()
            {
                audit = audit,
                strIdServicio = IdServicio
            };

            try
            {
                objDecoDetailByIdServiceResponse =
                Claro.Web.Logging.ExecuteMethod<FixedTransacService.DecoDetailByIdServiceResponse>(() =>
                {
                    return _oServiceFixed.GetDecoDetailByIdService(objDecoDetailByIdServiceRequest);
                });

                if (objDecoDetailByIdServiceResponse != null && objDecoDetailByIdServiceResponse.listaServicio != null)
                {
                    var listaResultado = objDecoDetailByIdServiceResponse.listaServicio;
                    lista = new List<DetailInteractionService>();
                    for (int i = 0; i < listaResultado.Count; i++)
                    {
                        item = new DetailInteractionService();
                        item.IdServicio = listaResultado[i].IdServicio;
                        item.GsrvcPrincipal = listaResultado[i].GsrvcPrincipal;
                        item.GsrvcCodigo = listaResultado[i].GsrvcCodigo;
                        item.Cantidad = listaResultado[i].Cantidad;
                        item.Servicio = listaResultado[i].Servicio;
                        item.Bandwid = listaResultado[i].Bandwid;
                        item.FlagLc = listaResultado[i].FlagLc;
                        item.CantidadIdLinea = listaResultado[i].CantidadIdLinea;
                        item.IdEquipo = listaResultado[i].IdEquipo;
                        item.CodTipEqu = listaResultado[i].CodTipEqu;
                        item.CantEquipo = listaResultado[i].CantEquipo;
                        item.Equipo = listaResultado[i].Equipo;
                        item.CodigoExt = listaResultado[i].CodigoExt;

                        lista.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.IdSession, objDecoDetailByIdServiceRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return lista;
        }
        //SOT
        public InsertTransactionResponse InsertSOT(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model, string strInteraccionId)
        {
            string codigoSot = string.Empty;
            InsertETASelectionResponse objInsertETASelectionResponse = null;
            InsertETASelectionRequest objInsertETASelectionRequest = null;
            Transfer objTransfer = null;
            InsertTransactionRequest objInsertTransactionRequest = null;
            InsertTransactionResponse objInsertTransactionResponse = null;
            UpdatexInter30Response objUpdatexInter30Response = null;
            CommonTransacService.AuditRequest auditCommon = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.IdSession);
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(model.IdSession);
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "Begin a InsertSOT"); // Temporal

            if (model.ValidaEta == CSTS.Constants.strUno || model.ValidaEta == CSTS.Constants.strDos)
            {
                if (Functions.CheckInt(model.CodigoRequestAct) > CSTS.Constants.numeroCero)
            {
                if (model.FechaProgramacion != null || model.FechaProgramacion != string.Empty)
                {
                    if (model.FranjaHora != null)
            {
                objInsertETASelectionResponse = new InsertETASelectionResponse();
                objInsertETASelectionRequest = new InsertETASelectionRequest()
                {
                    audit = audit,
                    vidconsulta = CSTS.Functions.CheckInt(model.CodigoRequestAct),
                                vidInteraccion = CSTS.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 10 - strInteraccionId.Trim().Length) + strInteraccionId.Trim(),
                            vfechaCompromiso = DateTime.Parse(model.FechaProgramacion),
                            vfranja = model.FranjaHorariaFinal.Split('+')[0],
                            vid_bucket = model.FranjaHorariaFinal.Split('+')[1]
                };

                            objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<InsertETASelectionResponse>(() => { return _oServiceFixed.GetInsertETASelection(objInsertETASelectionRequest); });                    
                        }
            }
                }
            }

            objTransfer = new Transfer();
            objTransfer.ConID = model.oDatosCliente.CONTRATO_ID;
            objTransfer.CustomerID = model.oDatosCliente.CUSTOMER_ID;
            objTransfer.TransTipo = ConfigurationManager.AppSettings("gConstKeyTipoTranInstDeco");
            objTransfer.InterCasoID = strInteraccionId;
            objTransfer.FechaProgramada = CSTS.Functions.CheckDate(model.FechaProgramacion);
            if (model.FranjaHora == null)
            {
                objTransfer.FranjaHora = CSTS.Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            }
            else
            {
                objTransfer.FranjaHora = model.FranjaHora;
            }
            objTransfer.USRREGIS = model.oUsuarioAcceso.USRREGIS;
            objTransfer.Cargo = model.CargoFijoTotal;
            //Verifia Observacion

            model.Nota = model.Nota != null ? model.Nota.Replace('|', '-') : string.Empty;

            if (Functions.CheckInt(model.CodigoRequestAct) > CSTS.Constants.numeroCero)
            {
                if (model.FechaProgramacion != null || model.FechaProgramacion != string.Empty)
                {
                    if (model.FranjaHora != null)
                    {
                        objTransfer.Observacion = model.Nota + "|" + CSTS.Constants.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - model.CodigoRequestAct.Trim().Length) + model.CodigoRequestAct;// +"|";
                    }
                    else
                    {
            objTransfer.Observacion = model.Nota;
                    }
                }
                else
                {
                    objTransfer.Observacion = model.Nota;
                }
            }
            else
            {
                objTransfer.Observacion = model.Nota;
            }

            objInsertTransactionRequest = new InsertTransactionRequest()
            {
                audit = audit,
                oTransfer = objTransfer
            };

            objInsertTransactionResponse = new InsertTransactionResponse();
            objInsertTransactionResponse = Claro.Web.Logging.ExecuteMethod<InsertTransactionResponse>(() =>
            {
                return _oServiceFixed.GetInsertTransaction(objInsertTransactionRequest);
            });

            codigoSot = objInsertTransactionResponse.intNumSot;

            if (codigoSot == null || codigoSot == string.Empty || codigoSot.ToUpper() == "NULL")
            {
                if (objInsertTransactionResponse.rintResCod.Equals(CSTS.Constants.strTres))
                {
                    UpdatexInter30Request objUpdatexInter30Request = new UpdatexInter30Request()
                    {
                        audit = auditCommon,
                        p_objid = strInteraccionId,
                        p_texto = CSTS.Functions.GetValueFromConfigFile("strMsgSOTEnCursoMP", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
                    };

                    objUpdatexInter30Response = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return _oServiceCommon.GetUpdatexInter30(objUpdatexInter30Request); });

                    return objInsertTransactionResponse;//GetValueXmlMethod(model.IdSession, "strConstArchivoSIACUTHFCConfigMsg", "strMsgSOTEnCursoMP");
                }
                else
                {
                    UpdatexInter30Request objUpdatexInter30Request = new UpdatexInter30Request()
                    {
                        audit = auditCommon,
                        p_objid = strInteraccionId,
                        p_texto = CSTS.Functions.GetValueFromConfigFile("strMensajeErrorparaNotasClfy", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
                    };

                    objUpdatexInter30Response = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return _oServiceCommon.GetUpdatexInter30(objUpdatexInter30Request); });

                    return objInsertTransactionResponse;//GetValueXmlMethod(model.IdSession, "strConstArchivoSIACUTHFCConfigMsg", "strMensajeDeError");
                }
            }
            else
            {
                if (Double.Parse(codigoSot) > 1)
                {
                    FixedTransacService.UpdateInter29Response objUpdateInter29Response = new FixedTransacService.UpdateInter29Response();
                    FixedTransacService.UpdateInter29Request objUpdateInter29Request = new FixedTransacService.UpdateInter29Request()
                    {

                        audit = audit,
                        p_objid = strInteraccionId,
                        p_texto = codigoSot,
                        p_orden = Claro.SIACU.Transac.Service.Constants.strLetraI
                    };

                    objUpdateInter29Response = Claro.Web.Logging.ExecuteMethod<FixedTransacService.UpdateInter29Response>(() =>
                    {
                        return _oServiceFixed.GetUpdateInter29(objUpdateInter29Request);
                    });

                    Claro.Web.Logging.Info(model.IdSession, audit.transaction, string.Format("OUT GetUpdateInter29 -  rFlagInsercion: {0}", objUpdateInter29Response.rFlagInsercion == null ? "" : objUpdateInter29Response.rFlagInsercion));

                }
            }

            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + audit.transaction, "End a InsertSOT"); // Temporal
            return objInsertTransactionResponse;
        }
        //Insertar Fidelizacion
        public bool GetInsertLoyalty(string IdSession, string CustomerID, string CodSot, string User)
        {
            bool salida = false;

            //ObtenerCliente
            Customer oCustomer = new Customer();
            oCustomer.CustomerID = CustomerID;

            FixedTransacService.InsertLoyaltyResponse objInsertLoyaltyResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "Begin a GetInsertLoyalty"); // Temporal
            FixedTransacService.InsertLoyaltyRequest objInsertLoyaltyRequest = new FixedTransacService.InsertLoyaltyRequest()
            {
                audit = audit,
                oCustomer = oCustomer,
                vCodSoLot = CodSot,
                vFlagDirecFact = 0,
                vUser = User,
                vFechaReg = DateTime.Now
            };

            try
            {
                objInsertLoyaltyResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertLoyaltyResponse>(() =>
                {
                    return _oServiceFixed.GetInsertLoyalty(objInsertLoyaltyRequest);
                });

                salida = objInsertLoyaltyResponse.rSalida;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, objInsertLoyaltyRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "End a GetInsertLoyalty"); // Temporal
            return salida;
        }
        //Actualizar OCC
        public bool GetSaveOCC(string IdSession, int CodSot, int CustomerId, DateTime FechaVig, double Monto, string Comentario, int Flag, string Aplicacion, string UsuarioAct, DateTime FechaAct, string CodId)
        {
            bool salida = false;
            FixedTransacService.SaveOCCResponse objSaveOCCResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "Begin a GetSaveOCC"); // Temporal
            FixedTransacService.SaveOCCRequest objSaveOCCRequest = new FixedTransacService.SaveOCCRequest()
            {
                audit = audit,
                vCodSot = CodSot,
                vCustomerId = CustomerId,
                vFechaVig = FechaVig,
                vMonto = Monto,
                vComentario = Comentario,
                vflag = Flag,
                vAplicacion = Aplicacion,
                vUsuarioAct = UsuarioAct,
                vFechaAct = FechaAct,
                vCodId = CodId,
            };

            try
            {
                objSaveOCCResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.SaveOCCResponse>(() =>
                {
                    return _oServiceFixed.GetSaveOCC(objSaveOCCRequest);
                });

                salida = objSaveOCCResponse.rSalida;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, objSaveOCCRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "End a GetSaveOCC"); // Temporal
            return salida;
        }
        public string GetHourAgendaETA(string IdSession, string FranjaHorariaETA)
        {
            string strHora = string.Empty;

            strHora = CSTS.Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
            var listaHorariosAux = CSTS.Functions.GetListValuesXML("ListaFranjasHorariasETA", "", "HFCDatos.xml");
            foreach (var item in listaHorariosAux)
            {
                if (FranjaHorariaETA.Split('+')[0] == item.Code)
                {
                    strHora = item.Code2;
                    break;
                }
            }
            return strHora;
        }
        public string MailBody(string IdSession, string FlajInstDesins) {
            string bodyMail = string.Empty;
            int parameterID = 0;
            if (FlajInstDesins == CSTS.Constants.strCero) //Instalacion
                parameterID = CSTS.Functions.CheckInt(ConfigurationManager.AppSettings("gConstKeyBodyMailInstalacion"));
            else
                parameterID = CSTS.Functions.CheckInt(ConfigurationManager.AppSettings("gConstKeyBodyMailDesinstalacion"));

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "Begin a MailBody"); // Temporal
            ParameterTerminalTPIResponse objResponse = null;
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest() {
                audit = audit,
                ParameterID = parameterID
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ParameterTerminalTPIResponse>(() =>
                    {
                        return _oServiceCommon.GetParameterTerminalTPI(objRequest);
                    });
                if (objResponse.ListParameterTeminalTPI.Count != 0)
                    bodyMail = objResponse.ListParameterTeminalTPI[0].ValorL;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + IdSession, "Transaccion: " + audit.transaction, "End a MailBody"); // Temporal
            return bodyMail;
        }
        #endregion

        #region Obtener PDF
        public Dictionary<string, object> GetConstancyPDF(Model.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel model, string strIdInteraction, string strFlajInstDesins)
        {
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + strIdInteraction, "Begin a GetConstancyPDF"); // Temporal
            var listResponse = new Dictionary<string, object>();

            bool generado = false;
            string carpetaTransaccion = string.Empty;
            string nombreArcrivo = string.Empty;
            string nombrepath = string.Empty;

            if (strFlajInstDesins == CSTS.Constants.strCero) // Instalacion
            {
                carpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionInstHFC");
            }
            else
            {
                carpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionDesiHFC");
            }                

            Model.TemplateInteractionModel objTemplateInteractionModel = null;
            InteractionServiceResponseHfc objInteractionServiceResponse = null;
            InteractionServiceRequestHfc objInteractionServiceRequest = null;
            FixedTransacService.AuditRequest objAuditRequest = null;

            try
            {
                //Datos para la Planilla
                objTemplateInteractionModel = GetInfoInteractionTemplate(model.IdSession, strIdInteraction);

                //Lista de Servicios para la Plantilla
                objAuditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(model.IdSession);
                objInteractionServiceRequest = new InteractionServiceRequestHfc { audit = objAuditRequest, idInteraccion = strIdInteraction };
                Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + objAuditRequest.transaction, "Begin a GetConstancyPDF"); // Temporal
                objInteractionServiceResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetServicesByInteraction(objInteractionServiceRequest); });

                string Texto = string.Empty;
                string TituloInstalacion = string.Empty;
                string TituloDesinstalacion = string.Empty;

                nombreArcrivo = ConfigurationManager.AppSettings("strNombreArchivoInsDesHFC");
                TituloInstalacion = CSTS.Functions.GetValueFromConfigFile("strMsgInsDecoConstanciaTitulo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                TituloDesinstalacion = CSTS.Functions.GetValueFromConfigFile("strMsgDesDecoConstanciaTitulo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                Texto = CSTS.Functions.GetValueFromConfigFile("strMsgInsDesDecoConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
          
                //Obtener Constancia PDF -----------------------
                List<ServiceConstancy> list = new List<ServiceConstancy>();
                ServiceConstancy oServiceConstancy = null;

                foreach (var item in objInteractionServiceResponse.Services)
                {
                    oServiceConstancy = new ServiceConstancy();
                    oServiceConstancy.StrNombreEquipo = item.NOM_SERV;
                    oServiceConstancy.StrTipoServicio = item.TIP_SERV;
                    oServiceConstancy.StrCargoFijoSinIGV = item.CF; //Con IGV
                    list.Add(oServiceConstancy);
                }

                ParametersGeneratePDF parameters = new ParametersGeneratePDF()
                {
                    StrTituloInstalacion = (strFlajInstDesins == CSTS.Constants.strCero ? TituloInstalacion : TituloDesinstalacion),//TITULO_INSTALACION
                    StrCentroAtencionArea = objTemplateInteractionModel.X_INTER_15,//PUNTO_ANTENCIO
                    StrTitularCliente = objTemplateInteractionModel.X_FIRST_NAME + " " + objTemplateInteractionModel.X_LAST_NAME,//TITULAR
                    StrRepresLegal = model.oDatosCliente.REPRESENTANTE_LEGAL,// objTemplateInteractionModel.X_FIRST_NAME + " " + objTemplateInteractionModel.X_LAST_NAME,//objTemplateInteractionModel.X_NAME_LEGAL_REP,//REPRESENTANTE LEGAL
                    StrTipoDocIdentidad = objTemplateInteractionModel.X_TYPE_DOCUMENT,//TIPO DOC.IDENTIDAD
                    StrPlanActual = objTemplateInteractionModel.X_BASKET,//PLAN ACTUAL
                    StrFechaTransaccionProgram = objTemplateInteractionModel.X_OCCUPATION,//FECHA
                    StrCasoInter = strIdInteraction,//CASO/INTERACCION
                    strContrato = objTemplateInteractionModel.X_REGISTRATION_REASON,//NRO.CONTRATO
                    StrNroDocIdentidad = objTemplateInteractionModel.X_CLARO_LDN1, //NUMERO DOC.
                    StrCicloFacturacion = objTemplateInteractionModel.X_INTER_1,//CICLO DE FACTURACION

                    StrFlagTipoDeco = (strFlajInstDesins == CSTS.Constants.strCero ? CSTS.Constants.strUno : CSTS.Constants.strCero),//FLAG_TIPO_DECO(Dirección)

                    strDireccionClienteActual = objTemplateInteractionModel.X_INTER_7,//DIRECCION
                    StrNotasDireccion = objTemplateInteractionModel.X_INTER_7 + objTemplateInteractionModel.X_AMOUNT_UNIT,//NOTAS_DIRECCION
                    strDepClienteActual = objTemplateInteractionModel.X_INTER_16,//DEPARTAMENTO
                    strDistritoClienteActual = objTemplateInteractionModel.X_INTER_17,//DISTRITO
                    strPaisClienteActual = objTemplateInteractionModel.X_INTER_18,//PAIS
                    strProvClienteActual = objTemplateInteractionModel.X_INTER_19,//PROVINCIAS
                    StrCodigoPlano = objTemplateInteractionModel.X_INTER_20,//CODIGO_PLANO
                    StrFechaCompromiso = objTemplateInteractionModel.X_POSITION,//FECHA_COMPROMISO

                    ListDecoder = list,

                    StrCantidadDesinstalar = objTemplateInteractionModel.X_INTER_21,//CANTIDAD
                    StrCargoFijoConIGV = "S/ " + objTemplateInteractionModel.X_INTER_23.ToString("#####.00"),//CARGO_FIJO_CON_IGV
                    StrFidelizar = (objTemplateInteractionModel.X_CLARO_LDN4 == CSTS.Constants.strUno ? "SI" : "NO"),//FIDELIZAR
                    StrCostoInstalacion = "S/ " + objTemplateInteractionModel.X_CLAROLOCAL1,//COSTO

                    strEnvioCorreo = (objTemplateInteractionModel.X_CLARO_LDN2 == CSTS.Constants.strUno ? "SI" : "NO"),//ENVIO_CORREO
                    StrEmail = objTemplateInteractionModel.X_EMAIL,//EMAIL
                    StrContenidoComercial2 = Texto,//TEXTO

                    
                    StrCarpetaTransaccion = carpetaTransaccion,
                    StrTipoTransaccion = "UninstallInstallationOfDecos",
                    StrNombreArchivoTransaccion = nombreArcrivo,
                    strNroSot = objTemplateInteractionModel.X_INTER_29.Replace("-", "")
                };
                GenerateConstancyResponseCommon response = GenerateContancyPDF(model.IdSession, parameters);
                nombrepath = response.FullPathPDF;
                generado = response.Generated;
                Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + strIdInteraction, "Begin a GetConstancyPDF -> Ruta: " + nombrepath); // Temporal
                //----------------------------------------------

                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", nombreArcrivo);
                Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + objAuditRequest.transaction, "End a GetConstancyPDF"); // Temporal
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + model.IdSession, "Transaccion: " + strIdInteraction, "End a GetConstancyPDF"); // Temporal
            return listResponse;
        }
        #endregion

        #region Variable Of Config
        public JsonResult GetMessage()
        {
            var variables = new
            {
                strFechaActualServidor = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00"),
                gConstKeyPreguntaDeco = ConfigurationManager.AppSettings("gConstKeyPreguntaDeco"),
                strCodTipoTrabajoCodificador = ConfigurationManager.AppSettings("strCodTipoTrabajoCodificador"),
                gConstMensajeErrorEquiposAsociado = ConfigurationManager.AppSettings("gConstMensajeErrorEquiposAsociado"),
                gConstMensajeEquiposAsociado = ConfigurationManager.AppSettings("gConstMensajeEquiposAsociado"),

                //gstrTRANSACCION_INSTALACION_DECO_ADICIONAL = ConfigurationManager.AppSettings("gstrTRANSACCION_INSTALACION_DECO_ADICIONAL"),
                gCantidadLimiteDeEquipos = ConfigurationManager.AppSettings("gCantidadLimiteDeEquipos"),
                //gMontoFidelizacionInstalacion = ConfigurationManager.AppSettings("gMontoFidelizacionInstalacion"),
                //gMontoFidelizacionDesinstalacion = ConfigurationManager.AppSettings("gMontoFidelizacionDesinstalacion"),
                strMensajeCantidadLimiteDeEquipos = ConfigurationManager.AppSettings("strMensajeCantidadLimiteDeEquipos"),
                gConstMensajeNoTieneEquiposAdicionales = ConfigurationManager.AppSettings("gConstMensajeNoTieneEquiposAdicionales"),

                gConstKeyIngreseCorreo = CSTS.Functions.GetValueFromConfigFile("gConstKeyIngreseCorreo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                gConstKeyCorreoIncorrecto = CSTS.Functions.GetValueFromConfigFile("gConstKeyCorreoIncorrecto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                gConstMsgSelCacDac = CSTS.Functions.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                gConstMsgNSFranjaHor = CSTS.Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strMsgConsultaCustomerContratoVacio = CSTS.Functions.GetValueFromConfigFile("strMsgConsultaCustomerContratoVacio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strTextoDecoNoTieneCable = CSTS.Functions.GetValueFromConfigFile("strTextoDecoNoTieneCable", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strTextoEstadoInactivo = CSTS.Functions.GetValueFromConfigFile("strTextoEstadoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strMensajeErrValAge = CSTS.Functions.GetValueFromConfigFile("strMensajeErrValAge", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strMensajeProblemaLoad = CSTS.Functions.GetValueFromConfigFile("strMensajeProblemaLoad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strMensajeValidaPlanComercial = CSTS.Functions.GetValueFromConfigFile("strMensajeValidaPlanComercial", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),

                strDescActivo = CSTS.Functions.GetValueFromConfigFile("strDescActivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")),

                gAccesoInstalacionIDD = ConfigurationManager.AppSettings("gAccesoInstalacionIDD"),
                gAccesoDesinstalacionIDD = ConfigurationManager.AppSettings("gAccesoDesinstalacionIDD"),
                gAccesoFidelizaCostoIDD = ConfigurationManager.AppSettings("gAccesoFidelizaCostoIDD"),
                gAccesoGuardarIDD = ConfigurationManager.AppSettings("gAccesoGuardarIDD"),
                strMensajeErrorConsultaIGV = ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"),
                strMsgErrorTrasaccion = CSTS.Functions.GetValueFromConfigFile("strMsgErrorTrasaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strMessageETAValidation = ConfigurationManager.AppSettings("strMessageETAValidation"),
                gSubTipoTrabajoDecoAdicional = ConfigurationManager.AppSettings("gSubTipoTrabajoDecoAdicional"),
                gSubTipoTrabajoBajaDeco = ConfigurationManager.AppSettings("gSubTipoTrabajoBajaDeco"),

                strMensajeTransaccionFTTH = ConfigurationManager.AppSettings("strMensajeBackOfficeFTTH"), //RONALDRR - INICIO
                strPlanoFTTH = ConfigurationManager.AppSettings("strPlanoFTTH") //RONALDRR - FIN

            };

            return Json(new { data = variables });
        }
        #endregion

        #region Enviar Correo
        public void SendEmail(string idSession, string strSender, string strTo, string strMessage, string strNameFile, string strSubject, string strPathFile)
        {
            byte[] fileByte = null;
            CommonTransacService.SendEmailResponseCommon objGetSendEmailResponse = new CommonTransacService.SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            Claro.Web.Logging.Info("IdSession: " + idSession, "Transaccion: " + AuditRequest.transaction, "Begin a SendEmail -> Parametro de entrada strSender: " + strSender + " strTo: " + strTo + " strMessage: " + strMessage + " strNameFile: " + strNameFile + " strSubject: " + strSubject + " strRutaArchivo: " + strPathFile); // Temporal

            if (DisplayFileFromServerSharedFile(idSession, AuditRequest.transaction, strPathFile, out fileByte))
            {
                Claro.Web.Logging.Info("IdSession: " + idSession, "Transaccion: " + AuditRequest.transaction, "SendEmail -> generó archivo en byte: true"); // Temporal
                CommonTransacService.SendEmailRequestCommon objGetSendEmailRequest = new CommonTransacService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = strSender,
                    strTo = strTo,
                    strMessage = strMessage,
                    strAttached = strNameFile,
                    AttachedByte = fileByte,
                    strSubject = strSubject
                };
                try
                {
                    objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailResponseCommon>
                        (
                            () => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); }
                        );
                    Claro.Web.Logging.Info("IdSession: " + idSession, "Transaccion: " + AuditRequest.transaction, "End a SendEmail -> Parametro de salida Resultado: " + objGetSendEmailResponse.Exit); // Temporal
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
                }
            }
        }
        #endregion
    }
}