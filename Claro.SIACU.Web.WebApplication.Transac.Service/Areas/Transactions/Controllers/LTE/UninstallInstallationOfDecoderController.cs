using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using Claro.SIACU.Transac.Service;
using System.Text;
using AutoMapper;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using HelpersCommon = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using EntitiesFixedService = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Common = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class UninstallInstallationOfDecoderController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed;

        public UninstallInstallationOfDecoderController()
        {
            _oServiceFixed = new FixedTransacServiceClient();
        }
        
        public ActionResult UninstallInstallationOfDecoderLte()
        {
            Claro.Web.Logging.Configure();
            ViewData["strDateServer"] = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00");
            int number = Convert.ToInt(ConfigurationManager.AppSettings("strIncrementDays", "0"));
            DateTime DateNowMoreSevenDay = DateTime.Now.AddDays(number);
            ViewData["strDateNew"] = DateNowMoreSevenDay.Year + "/" + DateNowMoreSevenDay.Month.ToString("00") + "/" + DateNowMoreSevenDay.Day.ToString("00");
            return PartialView();
        }
        [HttpPost]
        public JsonResult GetCommertialPlan(string strIdSession, string strContratoId)
        {
            CommertialPlanResponse objCommercialPlanResponse;
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetCommertialPlan");
            var objCommercialPlanRequest = new CommertialPlanRequest { audit = audit, StrCoId = strContratoId };

            try
            {
                objCommercialPlanResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommertialPlan(objCommercialPlanRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCommercialPlanRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetCommertialPlan");

            return new JsonResult
            {
                Data = objCommercialPlanResponse,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetListEquipment(string strIdSession, string idplan, string coid)
        {
            List<Models.UnistallInstallationOfDecoder.PlanService> listaFinal = null;
            PlanServiceResponse objPlanServiceResponse;
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetListEquipment");
            var objPlanServiceRequest = new PlanServiceRequest()
            {
                audit = audit,
                idplan = idplan,
                strTipoProducto = ConfigurationManager.AppSettings("strProductoLTE")
            };
            var strGrupos = ConfigurationManager.AppSettings("strGrupoEquiposLTE");

            var listGrupos = strGrupos.Split(';');

            try
            {
                objPlanServiceResponse =
                    Claro.Web.Logging.ExecuteMethod(() => _oServiceFixed.GetServicesByPlanLTE(objPlanServiceRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPlanServiceRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel objUnistallInstallationOfDecoderModel = null;

            var filterOnlyDecos = ConfigurationManager.AppSettings("gConstFilterOnlyDecos");
            if (objPlanServiceResponse != null && objPlanServiceResponse.listServicio != null)
            {
                objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
                var lista = objPlanServiceResponse.listServicio;

                for (var index1 = 0; index1 < lista.Count; index1++)
                {
                    for (var index2 = 0; index2 < lista.Count; index2++)
                    {
                        if (String.Compare(lista[index1].CodServiceType, lista[index2].CodServiceType, StringComparison.Ordinal) < 0)
                        {
                            var aux = lista[index1];
                            lista[index1] = lista[index2];
                            lista[index2] = aux;
                        }
                    }
                }

                listaFinal = new List<Models.UnistallInstallationOfDecoder.PlanService>();
                var codServSisact = string.Empty;


                var existe = false;

                for (var z = 0; z < listGrupos.Length; z++)
                {

                    for (var index3 = 0; index3 < lista.Count; index3++)
                    {
                        codServSisact = lista[index3].CodServSisact;
                        existe = false;

                        if (lista[index3].CodGroupServ == listGrupos[z])
                        {
                            for (var index4 = 0; index4 < listaFinal.Count; index4++)
                            {
                                if (listaFinal[index4].CodServSisact.Equals(codServSisact))
                                    existe = true;
                            }

                            var tipoEquTemp = "|" + lista[index3].Tipequ.Replace(" ", string.Empty) + "|";

                            if (!existe && !filterOnlyDecos.Contains(tipoEquTemp))
                            {
                                string tipoAux = string.Empty;
                                bool resultAux = false;

                                var objDecoTypeRequest = new DecoTypeRequest
                                {
                                    audit = audit,
                                    strTipoEquipo = lista[index3].Tipequ
                                };

                                var decoType = string.Empty;
                                var objDecoTypeResponse = Claro.Web.Logging.ExecuteMethod(() => _oServiceFixed.GetDecoType(objDecoTypeRequest));

                                if (objDecoTypeResponse != null)
                                {
                                    if (!string.IsNullOrEmpty(objDecoTypeResponse.TipoDeco))
                                    {
                                        decoType = objDecoTypeResponse.TipoDeco;
                                    }
                                }

                                //decoType = !string.IsNullOrEmpty(objDecoTypeResponse.TipoDeco) || objDecoTypeResponse.TipoDeco != "null" ? objDecoTypeResponse.TipoDeco : "HD";

                                decoType = decoType == "REGULAR" ? "SD" : decoType;

                                if (decoType != "null" && decoType != string.Empty)
                                {
                                    var planService = new Models.UnistallInstallationOfDecoder.PlanService
                                    {
                                        CodigoPlan = lista[index3].CodPlanSisact,
                                        DescPlan = lista[index3].DesPlanSisact,
                                        TmCode = lista[index3].Tmcode,
                                        Solucion = lista[index3].Solution,
                                        CodServSisact = lista[index3].CodServSisact,
                                        SNCode = lista[index3].Sncode,
                                        SPCode = lista[index3].Spcode,
                                        CodTipoServ = lista[index3].CodServiceType,
                                        TipoServ = lista[index3].ServiceType,
                                        DesServSisact = lista[index3].DesServSisact,
                                        CodGrupoServ = lista[index3].CodGroupServ,
                                        GrupoServ = lista[index3].GroupServ,
                                        CF = lista[index3].CF,
                                        IdEquipo = lista[index3].IDEquipment,
                                        Equipo = lista[index3].Equipment,
                                        CantidadEquipo = lista[index3].CantEquipment,

                                        TipoEquipo = decoType,

                                        CodigoExterno = lista[index3].CodeExternal,
                                        DesCodigoExterno = lista[index3].DesCodeExternal,
                                        ServvUsuarioCrea = lista[index3].ServvUserCrea,
                                        MatvDesSap = lista[index3].Dscequ
                                    };

                                    listaFinal.Add(planService);
                                }
                            }
                        }
                    }
                }
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetListEquipment");

            return new JsonResult
            {
                Data = listaFinal,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetListDataProducts(string strIdSession, string strContratoId, string strCustomerId)
        {
            Claro.Web.Logging.Info(strIdSession, "UninstallInstallationOfDecoder", string.Format("ExecuteGetListDataProducts() - CustomerID: {0} - ContractID: {1}", strCustomerId, strContratoId));

            ServicesLteFixedResponse objServicesLteFixedResponse = null;
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            var objServicesLteFixedRequest = new ServicesLteFixedRequest
            {
                audit = audit,
                strCoid = strContratoId,
                strCustomerId = strCustomerId
            };

            try
            {
                objServicesLteFixedResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return _oServiceFixed.GetCustomerEquipments(objServicesLteFixedRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesLteFixedRequest.audit.transaction, Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("UninstallInstallationOfDecoder - EndGetListDataProducts() - CustomerID: {0} - ContractID: {1}", strCustomerId, strContratoId));
            objServicesLteFixedResponse.ListServicesLte = objServicesLteFixedResponse.ListServicesLte.OrderByDescending(x => x.oc_equipo).ToList();

            foreach (var item in objServicesLteFixedResponse.ListServicesLte)
            {
                item.Codigo = Guid.NewGuid().ToString();
            }

            return new JsonResult
            {
                Data = objServicesLteFixedResponse.ListServicesLte,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetDefaultVariables(string strIdSession)
        {
            var response = new object();

            try
            {
                response = new
            {
                hdnTituloPagina = ConfigurationManager.AppSettings("gConstKeyTituloTranDecoAdicional"),
                hdnmensajeConfirmacion = ConfigurationManager.AppSettings("gConstKeyPreguntaDeco"),
                hdnMensaje1 = Functions.GetValueFromConfigFile("gConstKeyIngreseCorreo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnMensaje2 = Functions.GetValueFromConfigFile("gConstKeyCorreoIncorrecto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnMensaje8 = Functions.GetValueFromConfigFile("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnMensaje9 = Functions.GetValueFromConfigFile("gConstMsgTlfDSNum", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnMensaje10 = Functions.GetValueFromConfigFile("gConstMsgNSFranjaHor", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnMensaje11 = Functions.GetValueFromConfigFile("gConstMsgNVAgendamiento", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnFechaActualServidor = DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00"),
                hdnTipoTrabajo = ConfigurationManager.AppSettings("strCodTipoTrabajoCodificadorLTE"),
                hdnErrValidarAge = Functions.GetValueFromConfigFile("strMensajeErrValAge", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                hdnListaGrupoCableLTE = ConfigurationManager.AppSettings("strGrupoCableLTE"),
                hdnListaGrupoEquiposLTE = ConfigurationManager.AppSettings("strGrupoEquiposLTE"),
                strMensajeValidaPlanComercialLTE = ConfigurationManager.AppSettings("strMensajeValidaPlanComercialLTE"),
                strMsgNoExisteDeco = ConfigurationManager.AppSettings("strMsgNoExisteDecoInst"),
                strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE = ConfigurationManager.AppSettings("gstrTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE"),
                strMensajeNoExistenReglasDeNegocio = ConfigurationManager.AppSettings("strMensajeNoExistenReglasDeNegocio"),
                strMensajeErrorConsultaIGV = ConfigurationManager.AppSettings("strMensajeErrorConsultaIGV"),
                strMensajeValidationETA = ConfigurationManager.AppSettings("strMensajeValidationETA"),
                strMensajeConfirmacionDeco = ConfigurationManager.AppSettings("gConstKeyPreguntaDeco"),
                strMsgLimiteSdHd = ConfigurationManager.AppSettings("strMsgLimiteSdHd"),
                strMsgLimiteDVR = ConfigurationManager.AppSettings("strMsgLimiteDVR"),
                intTypeLoyalty = ConfigurationManager.AppSettings("intTypeLoyaltyLTE"),
                strCodigoMotivoSot = ConfigurationManager.AppSettings("strCodigoMotivoSot"),
                strCodTipServLte = ConfigurationManager.AppSettings("CodTipServLte")
            };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, Functions.GetExceptionMessage(ex));
            }


            return new JsonResult
                {
                    Data = response,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
        }
        [HttpPost]
        public JsonResult GetValidationMessages(string strIdSession)
        {
            var response = new object();
            try
            {
                response = new
            {
                strMsgConsultaCustomerContratoVacio = Functions.GetValueFromConfigFile("strMsgConsultaCustomerContratoVacio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strTextoDecoNoTieneCable = Functions.GetValueFromConfigFile("strTextoDecoNoTieneCable", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                strDescActivo = Functions.GetValueFromConfigFile("strDescActivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")),
                strTextoEstadoInactivo = Functions.GetValueFromConfigFile("strTextoEstadoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
            };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, Functions.GetExceptionMessage(ex));
            }


            return new JsonResult
            {
                Data = response,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetTypificationTransaction(string strIdSession, string strTransactionName)
        {
            var strProductType = ConfigurationManager.AppSettings("gConstTipoLTE");
            var objLstTypification = GetTypificationHFC(strIdSession, strTransactionName);
            var objInteractionModel = new Model.InteractionModel();

            try
            {
                if (objLstTypification != null)
                {
                    objLstTypification = objLstTypification.Where(s => s.Type.Equals(strProductType)).ToList();

                    objLstTypification.ToList().ForEach(x =>
                    {
                        objInteractionModel.Type = x.Type;
                        objInteractionModel.Class = x.Class;
                        objInteractionModel.SubClass = x.SubClass;
                        objInteractionModel.InteractionCode = x.InteractionCode;
                        objInteractionModel.TypeCode = x.TypeCode;
                        objInteractionModel.ClassCode = x.ClassCode;
                        objInteractionModel.SubClassCode = x.SubClassCode;
                    });
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, Functions.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strIdSession, "End a GetTypificationTransaction");

            return new JsonResult
                {
                    Data = objInteractionModel,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
        }
        [HttpPost]
        public JsonResult GetWorkType(string strIdSession, string strTransacType)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "In GetWorkType - LTE");
            JobTypesResponseHfc objWorkTypeResponseCommon = null;
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            var objWorkTypeRequestCommon = new JobTypesRequestHfc
            {
                audit = audit,
                p_tipo = Convert.ToInt(ConfigurationManager.AppSettings("strTypeJobInUnDecoLte"))
            };

            try
            {
                objWorkTypeResponseCommon = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetJobTypeLte(objWorkTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objWorkTypeRequestCommon.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            var objFinalResponse = new List<HelpersCommon.GenericItem>();
            if (objWorkTypeResponseCommon != null && objWorkTypeResponseCommon.JobTypes != null)
            {
                foreach (var item in objWorkTypeResponseCommon.JobTypes)
                {
                    objFinalResponse.Add(new HelpersCommon.GenericItem
                    {
                        Code = item.tiptra,
                        Description = item.descripcion
                    });
                }
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, "Out GetWorkType - LTE");

            var strTypeJobInst = ConfigurationManager.AppSettings("strTypeJobInstLte");
            var strTypeJobDesinst = ConfigurationManager.AppSettings("strTypeJobDesinstLte");
            var strTypeJobMix = ConfigurationManager.AppSettings("strTypeJobMixtaLte");
            return new JsonResult
            {
                Data = new { objFinalResponse, strTypeJobInst, strTypeJobDesinst, strTypeJobMix },
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetJobSubType(string strIdSession, string strTipoTrabajo)
        {
            List<Helpers.CommonServices.GenericItem> List = null;
            Helpers.CommonServices.GenericItem item = null;
            string TipoTrabajo = string.Empty;
            string strTipoOrdEta = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetJobSubType"); // Temporal
            var objOrderTypesResponse = new OrderTypesResponseHfc();
            var objOrderTypesRequest = new OrderTypesRequestHfc();
            var objOrderSubTypesResponse = new OrderSubTypesResponseHfc();
            var objOrderSubTypesRequest = new OrderSubTypesRequestHfc();

            if (Functions.CheckStr(strTipoTrabajo.IndexOf("|")) == SIACU.Transac.Service.Constants.strMenosUno)
                TipoTrabajo = strTipoTrabajo;
            else
                TipoTrabajo = Functions.CheckStr(strTipoTrabajo.Split('|')[0]);
            
            try
            {

                objOrderSubTypesRequest.audit = audit;
                objOrderSubTypesRequest.av_cod_tipo_trabajo = strTipoTrabajo;

                objOrderSubTypesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubTypesResponseHfc>(() => { return _oServiceFixed.GetOrderSubTypeWork(objOrderSubTypesRequest); });

                if (objOrderSubTypesResponse.OrderSubTypes != null)
                {
                    List = new List<Helpers.CommonServices.GenericItem>();
                    foreach (var aux in objOrderSubTypesResponse.OrderSubTypes)
                    {
                        item = new Helpers.CommonServices.GenericItem();
                        item.Code = aux.COD_SUBTIPO_ORDEN + "|" + aux.TIEMPO_MIN; //Codigo
                        item.Description = aux.DESCRIPCION; //Descripcion
                        List.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objOrderSubTypesRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetJobSubType"); // Temporal
            return Json(new { data = List });
        }

        [HttpPost]
        public JsonResult GetDecoMatriz(string strIdSession)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "In GetDecoMatriz - LTE");
            DecoMatrizResponse objDecoMatrizResponse = null;
            List<DecoMatriz> listDecos = new List<DecoMatriz>();
            string numDecosMax = "0";

            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            var objDecoMatrizRequest = new DecoMatrizRequest
            {
                audit = audit,
                strTipoProducto = "08"
            };

            try
            {
                objDecoMatrizResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetDecoMatriz(objDecoMatrizRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDecoMatrizRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            if (objDecoMatrizResponse != null)
            {
                listDecos = objDecoMatrizResponse.ListaMatrizDecos;
                numDecosMax = objDecoMatrizResponse.CantidadMaxima;
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, "Out GetDecoMatriz - LTE");

            return new JsonResult
            {
                Data = new { ListDecos = listDecos, numDecosMax },
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        [HttpPost]
        public JsonResult ExecuteTransaction(InstallUninstallDecoderModel objViewModel)
        {
            Claro.Web.Logging.Info("IdSession: " + objViewModel.StrIdSession, "ExecuteTransaction: ", "Begin a Transaction");
            var objResponseViewModel = new object();
            var ipCliente = App_Code.Common.GetClientIP();
            var ipServidor = App_Code.Common.GetClientIP();
            var nomServClient = App_Code.Common.GetClientName();
            var ctaUsuClient = App_Code.Common.CurrentUser;
            var amountIgv = Convert.ToDecimal(objViewModel.InsInteractionPlusModel.Inter25);
            ServicesLteFixedResponse objServicesLteFixedResponse = null;
            var listDecoAddDelete = new List<Model.DecoModel>();
            var listDecoAddConstancy = new List<Model.DecoModel>();
            var listDecoAdd = new List<Model.DecoModel>();
            var listTemp = new List<Model.DecoModel>();
            Decimal costoTotalSIgv = 0;
            Decimal costoTotalCIgv = 0;

            listTemp = objViewModel.Decos;
            objViewModel.Decos = new List<Model.DecoModel>(); 

            string flagconting = "1";

            var registrarEtaSeleccion = new RegistrarEtaSeleccion();
            var registrarEta = new RegistrarEta();

            if (ConfigurationManager.AppSettings("gConstContingenciaClarify") == "NO")
            {
                flagconting = "0";
            }
            var intThereDesinstalation =(from x in listTemp where x.Flag=="B"
                                select x).Count();
            if (intThereDesinstalation>0)
                    objServicesLteFixedResponse = GetDecoAll(objViewModel.StrIdSession, objViewModel.StrContractId, objViewModel.StrCustomerId);

            foreach (var item in listTemp)
            {
                item.Quantity = SIACU.Transac.Service.Constants.strUno;
                item.Equipment = string.Empty;
                item.DateUser = DateTime.UtcNow.ToString("dd/MM/yyyy");
                item.CodeUser = ctaUsuClient;
                if (item.Flag == "B")
                {
                    if (objServicesLteFixedResponse != null)
                    {
                        var lstServiceChoose= (from x in objServicesLteFixedResponse.ListServicesLte
                                               where x.tipoServicio.ToUpper() == "TV SATELITAL" && x.oc_equipo.ToUpper() == "ADICIONAL" && x.asociado == item.Associated
                                      select x).ToList();

                        foreach (var itemEquipment in lstServiceChoose)
                        {
                                    var tempDeco = new Model.DecoModel
                                    {
                                Associated = itemEquipment.asociado,
                                TypeEquipmentCode = itemEquipment.codigo_tipo_equipo,
                                SerialNumber = itemEquipment.numero_serie,
                                        CodeUser = ctaUsuClient,
                                        DateUser = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                                CodeInsSrv = itemEquipment.codinssrv,
                                        Flag = "B",
                                        Quantity = SIACU.Transac.Service.Constants.strUno,
                                SnCode = itemEquipment.codtipequ,
                                SpCode = itemEquipment.tipequ,
                                ServiceName = itemEquipment.descripcion_material,
                                        Cf = "0.00",
                                ServiceType = (itemEquipment.tipo_equipo == "DECO")?itemEquipment.tipo_deco:"-",//itemEquipment.tipoServicio.ToUpper(),
                                        Equipment = string.Empty,
                                        CodeService = string.Empty,
                                        ServiceGroup = string.Empty
                                    };

                                    listDecoAddDelete.Add(tempDeco);
                                    if (itemEquipment.tipo_equipo == "DECO")
                                    {
                                        var tempDeco1 = new Model.DecoModel
                                        {
                                            Associated = itemEquipment.asociado,
                                            TypeEquipmentCode = itemEquipment.codigo_tipo_equipo,
                                            SerialNumber = itemEquipment.numero_serie,
                                            CodeUser = ctaUsuClient,
                                            DateUser = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                                            CodeInsSrv = itemEquipment.codinssrv,
                                            Flag = "B",
                                            Quantity = SIACU.Transac.Service.Constants.strUno,
                                            SnCode = itemEquipment.codtipequ,
                                            SpCode = itemEquipment.tipequ,
                                            ServiceName = itemEquipment.descripcion_material,
                                            Cf = "0.00",
                                            ServiceType = itemEquipment.tipo_deco, // itemEquipment.tipoServicio.ToUpper(),//itemEquipment.tipo_equipo + " " + itemEquipment.tipo_deco,
                                            Equipment = string.Empty,
                                            CodeService = string.Empty,
                                            ServiceGroup = string.Empty,
                                            tipodeco = itemEquipment.tipo_deco
                                        };
                                        listDecoAddConstancy.Add(tempDeco1);

                                    }
                    }

                    }
                }
                if (item.Flag == "A")
                {
                    costoTotalSIgv = decimal.Round(costoTotalSIgv + Convert.ToDecimal(item.Cf), 2);
                    costoTotalCIgv = decimal.Round(costoTotalCIgv + Convert.ToDecimal(item.Cf) * amountIgv, 2);
                    item.ServiceType = item.tipodeco;
                    listDecoAdd.Add(item);
                }
            }

            var quantityDecoders = listTemp.Count;
            var quantityDecordersUninstall = 0;
            var quantityDecordersInstall = 0;
            if (listDecoAddDelete.Count > 0)
            {
                quantityDecordersUninstall = listDecoAddConstancy.Count;
                objViewModel.Decos = listDecoAddDelete;
            }
            if (listDecoAdd.Count > 0)
            {
                quantityDecordersInstall = listDecoAdd.Count;
                listDecoAddConstancy.AddRange(listDecoAdd);
                objViewModel.Decos.AddRange(listDecoAdd);
            }
            objViewModel.InsInteractionPlusModel.Inter22 = string.Format("{0:0.00}", costoTotalSIgv);
            objViewModel.InsInteractionPlusModel.Inter23 = string.Format("{0:0.00}", costoTotalCIgv);

            objViewModel.InteractionModel.Siteobjid = string.Empty;
            objViewModel.InteractionModel.Account = string.Empty;
            objViewModel.InteractionModel.Phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + objViewModel.StrCustomerId;
            objViewModel.InteractionModel.MetodoContacto = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
            objViewModel.InteractionModel.TipoInter = ConfigurationManager.AppSettings("AtencionDefault");
            objViewModel.InteractionModel.UsrProceso = ConfigurationManager.AppSettings("USRProcesoSU");
            objViewModel.InteractionModel.FlagCaso = SIACU.Transac.Service.Constants.strCero;
            objViewModel.InteractionModel.Resultado = ConfigurationManager.AppSettings("Ninguno");
            objViewModel.InteractionModel.HechoEnUno = SIACU.Transac.Service.Constants.strCero;

            objViewModel.AuditRegister.strIdTransaccion = ConfigurationManager.AppSettings("gConstCodigoAuditoriaInstalacionDecodificadores");
            objViewModel.AuditRegister.strServicio = ConfigurationManager.AppSettings("gConstEvtServicio");
            objViewModel.AuditRegister.strIpCliente = ipCliente;
            objViewModel.AuditRegister.strIpServidor = ipServidor;
            objViewModel.AuditRegister.strNombreServidor = nomServClient;
            objViewModel.AuditRegister.strMonto = SIACU.Transac.Service.Constants.strCero;
            objViewModel.AuditRegister.strCuentaUsuario = ctaUsuClient;
            objViewModel.AuditRegister.strTexto = string.Empty;

            objViewModel.DecoCustomerModel = new Model.DecoCustomerModel
            {
                Phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + objViewModel.StrCustomerId,
                Account = string.Empty,
                ContactObjId = string.Empty,
                FlagReg = SIACU.Transac.Service.Constants.strUno
            };

            objViewModel.RegistrarProcesoPostventa.PiTipoTrans = ConfigurationManager.AppSettings("gConstKeyTipoTranInstDecoLTE");
            objViewModel.RegistrarProcesoPostventa.PiTmcode = "";
            objViewModel.RegistrarProcesoPostventa.PiUsureg = ctaUsuClient;
            objViewModel.RegistrarProcesoPostventa.PiFecreg = DateTime.Now.ToString("dd/MM/yyyy");
            objViewModel.RegistrarProcesoPostventa.PiCargo = objViewModel.ImplementOcc.Monto;//ConfigurationManager.AppSettings("cargoOCCIDADDLTE");

            objViewModel.RegistrarProcesoPostventa.PiTramaCab = BuildStringPostSale(objViewModel.RegistrarProcesoPostventa);
            
            DecosLteResponse objResponse = null;
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(objViewModel.StrIdSession);

            var oConstancyPdf = GetConstancyPdf(objViewModel.StrIdSession, audit.transaction, objViewModel, objViewModel.InsInteractionPlusModel.ClaroLocal2, objViewModel.InsInteractionPlusModel.Inter25, listDecoAddConstancy, quantityDecordersUninstall, quantityDecordersInstall);
            var strCarpeta = ConfigurationManager.AppSettings(objViewModel.InsInteractionPlusModel.ClaroLocal2 == "0" ? "strCarpetaTransaccionDesiLTE" : "strCarpetaTransaccionInstLTE");

            objViewModel.GenerateConstancy = new GenerateConstancy
            {
                Driver = oConstancyPdf,
                Directory = strCarpeta,
                FileName = "INSTALACION_DESINSTALACION_DECOS_ADICIONALES",//ConfigurationManager.AppSettings("strNombreArchivoInsDesLTE"),
                FolderPdf = ConfigurationManager.AppSettings("strCarpetaPDFs"),
                ServerReadPdf = ConfigurationManager.AppSettings("strServidorLeerPDF")
            };

            objViewModel.ImplementLoyalty.UsuarioReg = ctaUsuClient;
            objViewModel.ImplementLoyalty.FlagDireccFact = "0";
            objViewModel.ImplementLoyalty.FechaReg = DateTime.Now.ToString("dd/MM/yyyy");

            var mes = DateTime.Now.Month + 1;
            var anio = DateTime.Now.Year;
            if (mes == 13) { mes = 1; anio = anio + 1; }
            DateTime fechaVig = Functions.CheckDate(objViewModel.InsInteractionPlusModel.Inter1.Trim() + "/" + mes.ToString().Trim() + "/" + anio.ToString().Trim());
            
            objViewModel.ImplementOcc.Observacion = ConfigurationManager.AppSettings("gConstComentarioIDD");
            objViewModel.ImplementOcc.Aplicacion = ConfigurationManager.AppSettings("ApplicationName"); 
            objViewModel.ImplementOcc.Fecvig = fechaVig.ToString("dd/MM/yyyy");
            objViewModel.ImplementOcc.UsuarioAct = ctaUsuClient;
            objViewModel.ImplementOcc.FechaAct = DateTime.Now.ToString("dd/MM/yyyy");

            if (!objViewModel.EtaValidation.Equals(SIACU.Transac.Service.Constants.strCero))
            {
                registrarEtaSeleccion.Idbucket = objViewModel.RegistrarEtaSeleccion.Idbucket ?? string.Empty;
                registrarEtaSeleccion.FechaCompromiso = objViewModel.RegistrarEtaSeleccion.FechaCompromiso ?? string.Empty;
                registrarEtaSeleccion.Franja = objViewModel.RegistrarEtaSeleccion.Franja ?? string.Empty;
                registrarEtaSeleccion.IdConsulta = objViewModel.RegistrarEtaSeleccion.IdConsulta ?? string.Empty;
                registrarEtaSeleccion.IdInteraccion = objViewModel.RegistrarEtaSeleccion.IdInteraccion ?? string.Empty;

                registrarEta.DniTecnico = objViewModel.RegistrarEta.DniTecnico ?? string.Empty;
                registrarEta.Franja = objViewModel.RegistrarEta.Franja ?? string.Empty;
                registrarEta.IdPoblado = objViewModel.RegistrarEta.IdPoblado ?? string.Empty;
                registrarEta.Idbucket = objViewModel.RegistrarEta.Idbucket ?? string.Empty;
                registrarEta.IpCreacion = objViewModel.RegistrarEta.IpCreacion ?? string.Empty;
                registrarEta.SubTipoOrden = objViewModel.RegistrarEta.SubTipoOrden ?? string.Empty;
                registrarEta.UsrCrea = objViewModel.RegistrarEta.UsrCrea ?? string.Empty;
                registrarEta.FechaProg = objViewModel.RegistrarEta.FechaProg ?? string.Empty;
                registrarEta.FechaCrea = DateTime.UtcNow.ToString("dd/MM/yyyy");                
            }

            var objRequest = new DecosLteRequest
            {
                audit = audit,
                Interaction = Mapper.Map<InteractionBpel>(objViewModel.InteractionModel),
                InsInteractionPlus = Mapper.Map<InteractionPlusBpel>(objViewModel.InsInteractionPlusModel),
                StrIdSession = objViewModel.StrIdSession,
                StrCustomerId = objViewModel.StrCustomerId,
                DecoCustomer = Mapper.Map<DecoCustomer>(objViewModel.DecoCustomerModel),
                AuditRegister = objViewModel.AuditRegister,
                SotPending = objViewModel.SotPending,
                FlagConting = flagconting,
                StrContractId = objViewModel.StrContractId,
                LstDecoders = Mapper.Map<List<LteDecoder>>(objViewModel.Decos),
                RegistrarProcesoPostVenta = Mapper.Map<RegistrarProcesoPostVentaLte>(objViewModel.RegistrarProcesoPostventa),
                GenerateConstancy = objViewModel.GenerateConstancy,
                RealizarFidelizacion = objViewModel.ImplementLoyalty,
                RealizarOcc = objViewModel.ImplementOcc,
                EtaValidation = objViewModel.EtaValidation,
                RegistrarEtaSeleccion = registrarEtaSeleccion,
                RegistrarEta = registrarEta
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.PostExecuteDecosLte(objRequest); });

                if (objResponse != null)
                {
                    objResponseViewModel = new
                    {
                        objResponse.SotNumber,
                        objResponse.CodeInteraction,
                        objResponse.UrlConstancy,
                        objResponse.ResponseCode,
                        objResponse.ResponseMessage
                    };
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objViewModel.StrIdSession, objRequest.audit.transaction, Functions.GetExceptionMessage(ex));
            }


            return new JsonResult
            {
                Data = objResponseViewModel,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult GetLoyaltyAmountLte(string strIdSession, int iTipo)
        {
            var objUnistallInstallationOfDecoderModel = new Models.UnistallInstallationOfDecoder.UnistallInstallationOfDecoderModel();
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Begin a GetLoyaltyAmount");
            var objLoyaltyAmountRequest = new LoyaltyAmountRequest
            {
                audit = audit,
                iTipo = iTipo
            };

            try
            {
                var objLoyaltyAmountResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetLoyaltyAmountLte(objLoyaltyAmountRequest);
                });

                if (objLoyaltyAmountResponse != null)
                {
                    objUnistallInstallationOfDecoderModel.MontoFidelizacion = objLoyaltyAmountResponse.strMonto;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objLoyaltyAmountRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "End a GetLoyaltyAmountLte");


            return new JsonResult
            {
                Data = objUnistallInstallationOfDecoderModel.MontoFidelizacion,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ServicesLteFixedResponse GetDecoAll(string strIdSession, string strContratoId, string strCustomerId)
        {
            Claro.Web.Logging.Info(strIdSession, "UninstallInstallationOfDecoder", string.Format("GetDecoAll() - CustomerID: {0} - ContractID: {1}", strCustomerId, strContratoId));

            var objServicesLteFixedResponse = new ServicesLteFixedResponse();
            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            var objServicesLteFixedRequest = new ServicesLteFixedRequest
            {
                audit = audit,
                strCoid = strContratoId,
                strCustomerId = strCustomerId
            };

            try
            {
                objServicesLteFixedResponse = Claro.Web.Logging.ExecuteMethod(
                () =>
                {
                    return _oServiceFixed.GetCustomerEquipments(objServicesLteFixedRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesLteFixedRequest.audit.transaction, Functions.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("UninstallInstallationOfDecoder - GetDecoAll() - CustomerID: {0} - ContractID: {1}", strCustomerId, strContratoId));

            if (objServicesLteFixedResponse != null)
            {
                objServicesLteFixedResponse.ListServicesLte = objServicesLteFixedResponse.ListServicesLte.OrderByDescending(x => x.oc_equipo).ToList();
            }

            return objServicesLteFixedResponse;
        }

        public string GetConstancyPdf(string strIdSession, string strIdTransaccion, InstallUninstallDecoderModel model, string strFlajInstDesins,string amountIgv, List<Model.DecoModel> lstDecos, int quantityDecordersUninstall, int quantityDecordersInstall)
        {
            /* strFlajInstDesins => 0 = Desinstalacion , 1 = Instalacion y 2 = Actualizacion(Inst + Desint) */
            var xmlConstancyPdf = string.Empty;
            try
            {
                var datos = new List<string>();
                var tituloTransaccion = string.Empty;
                var tituloAlta = ConfigurationManager.AppSettings("strTituloAltaDecosLte");
                var tituloBaja = ConfigurationManager.AppSettings("strTituloBajaDecosLte");
                var tituloActualizacion = ConfigurationManager.AppSettings("strTituloActualizacionDecosLte");

                if (strFlajInstDesins.Equals(SIACU.Transac.Service.Constants.strCero))
                {
                    tituloTransaccion = tituloBaja;
                }
                else if (strFlajInstDesins.Equals(SIACU.Transac.Service.Constants.strUno))
                {
                    tituloTransaccion = tituloAlta;
                }
                else if (strFlajInstDesins.Equals(SIACU.Transac.Service.Constants.strDos))
                {
                    tituloTransaccion = tituloActualizacion;
                }

                datos.Add(tituloTransaccion);//TITULO_TRANSACCION
                datos.Add(model.InsInteractionPlusModel.Inter15);//PUNTO_ATENCION
                datos.Add(model.InsInteractionPlusModel.FirstName + " " + model.InsInteractionPlusModel.LastName);//TITULAR
                datos.Add(model.InsInteractionPlusModel.NameLegalRep);// objTemplateInteractionModel.X_FIRST_NAME + " " + objTemplateInteractionModel.X_LAST_NAME,//objTemplateInteractionModel.X_NAME_LEGAL_REP,//REPRESENTANTE LEGAL
                datos.Add(model.InsInteractionPlusModel.TypeDocument);//TIPO DOC.IDENTIDAD
                datos.Add(model.InsInteractionPlusModel.Basket);//PLAN ACTUAL
                datos.Add(DateTime.UtcNow.ToString("dd/MM/yyyy"));//FECHA
                datos.Add("$CodigoInteraccion");//INTERACCION
                datos.Add(model.StrContractId);//NRO.CONTRATO
                datos.Add(model.InsInteractionPlusModel.ClaroLdn1); //NUMERO DOC.
                datos.Add(model.InsInteractionPlusModel.Inter1);//CICLO DE FACTURACION
                datos.Add(strFlajInstDesins);//FLAG_TIPO_DECO
                datos.Add(model.InsInteractionPlusModel.Inter7);//DIRECCION
                datos.Add(model.InsInteractionPlusModel.Address);//NOTAS_DIRECCION
                datos.Add(model.InsInteractionPlusModel.Inter16);//DEPARTAMENTO
                datos.Add(model.InsInteractionPlusModel.Inter17);//DISTRITO
                datos.Add(model.InsInteractionPlusModel.Inter18);//PAIS
                datos.Add(model.InsInteractionPlusModel.Inter19);//PROVINCIAS
                datos.Add(model.InsInteractionPlusModel.Inter20);//CODIGO_PLANO
                datos.Add(model.InsInteractionPlusModel.Position);//FECHA_COMPROMISO
                datos.Add(quantityDecordersInstall.ToString());//CANTIDAD 
                datos.Add("S/ " + String.Format("{0:0.00}", Convert.ToDecimal(model.InsInteractionPlusModel.Inter23)));//CARGO_FIJO_CON_IGV
                datos.Add("S/ " + String.Format("{0:0.00}", Convert.ToDecimal(model.InsInteractionPlusModel.Inter22)));//CARGO_FIJO_SIN_IGV
                datos.Add(model.InsInteractionPlusModel.ClaroLdn4 == SIACU.Transac.Service.Constants.strUno ? "SI" : "NO");//FIDELIZAR
                datos.Add("S/ " + String.Format("{0:0.00}", Convert.ToDecimal(model.InsInteractionPlusModel.ClaroLocal1)));//COSTO INSTALACION
                datos.Add(model.InsInteractionPlusModel.ClaroLdn2 == SIACU.Transac.Service.Constants.strUno ? "SI" : "NO");//ENVIO DE CORREO
                datos.Add(model.InsInteractionPlusModel.Email);//CORREO
                datos.Add("$CodigoSot");
                datos.Add(Functions.GetValueFromConfigFile("strMsgInsDesDecoConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));

                datos.Add(ConfigurationManager.AppSettings("strTipoTransaccionInslacionDesinstalacionDecosLte"));
                datos.Add(ConfigurationManager.AppSettings("strNombreArchivoInsDesLTE"));
                datos.Add(ConfigurationManager.AppSettings("strUnistallInstallDecoderAccionEjecutar"));
                datos.Add(tituloTransaccion.Substring(tituloTransaccion.IndexOf("-") + 1));
                datos.Add("S/ " + String.Format("{0:0.00}", Convert.ToDecimal(Constants.ZeroNumber)));
                datos.Add(quantityDecordersUninstall.ToString());
                datos.Add(model.InteractionModel.Agente);
                datos.Add(model.InteractionModel.AgenteName);

                xmlConstancyPdf = GetGenerateConstancyXmlDecos(datos, ConfigurationManager.AppSettings("strRutaXmlInstDesintDecos"), lstDecos, strFlajInstDesins, amountIgv);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, Functions.GetExceptionMessage(ex));
            }
            
            return xmlConstancyPdf;
        }
        [HttpPost]
        public JsonResult GetAmountCurrentPlan(string strIdSession, string strContractId)
        {
            var responseFinal = new PlanCharges();
            var objServicesByCurrentPlanRequest = new ServicesByCurrentPlanRequest
            {
                ContractId = strContractId,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };

            try
            {
                var objServicesByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetServicesByCurrentPlan(objServicesByCurrentPlanRequest);
                });

                if(objServicesByCurrentPlanResponse.ServicesByCurrentPlan != null)
                {
                    responseFinal = CalculatePlanCharges.CalculateCharges(objServicesByCurrentPlanResponse.ServicesByCurrentPlan);
                }

                Claro.Web.Logging.Info("Session:" + strIdSession + ", Contrato:" + strContractId, "LTE-ID-DECOS", "GetServicesByCurrentPlan-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + strIdSession + ", Contrato:" + strContractId, "LTE-ID-DECOS GetServicesByCurrentPlan", Functions.GetExceptionMessage(ex));
            }

            return new JsonResult
            {
                Data = new
                {
                    responseFinal.CantidadServicios,
                    MontoActualBase = string.Format("{0:0.00}", responseFinal.MontoActualBase),
                    MontoActualAdicional = string.Format("{0:0.00}", responseFinal.MontoActualAdicional)
                },
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string GetGenerateConstancyXmlDecos(List<string> listParamConstancyPdf, string pathFileXml, List<Model.DecoModel> listDecos, string flagDecos, string amountIgv)
        {
            var listLabels = GetXmlToString(App_Code.Common.GetApplicationRoute() + pathFileXml);
            var count = 0;
            var xmlGenerated = new StringBuilder();
            xmlGenerated.Append("<PLANTILLA>\r\n");
            foreach (var key in listLabels)
            {
                xmlGenerated.Append(string.Format("<{0}>{1}</{2}>\r\n", key, listParamConstancyPdf[count], key));
                count++;
            }

            if (flagDecos.Equals(SIACU.Transac.Service.Constants.strCero))
            {
                foreach (var item in listDecos)
                {
                    xmlGenerated.Append(string.Format("<NOMBRE_EQUIPO_GRILLA>{0}</NOMBRE_EQUIPO_GRILLA>\r\n", item.ServiceName));
                    xmlGenerated.Append(string.Format("<TIPO_SERVICIO_GRILLA>{0}</TIPO_SERVICIO_GRILLA>\r\n", item.tipodeco));
                    xmlGenerated.Append(string.Format("<CARGO_FIJO_SIN_IGV_GRILLA>{0}</CARGO_FIJO_SIN_IGV_GRILLA>\r\n", "0.00"));
                }
            }
            else
            {
                foreach (var item in listDecos)
                {
                    var amountWithIgv = String.Format("{0:0.00}", Convert.ToDecimal(item.Cf) * Convert.ToDecimal(amountIgv));
                    xmlGenerated.Append(string.Format("<NOMBRE_EQUIPO_GRILLA>{0}</NOMBRE_EQUIPO_GRILLA>\r\n", item.ServiceName));
                    xmlGenerated.Append(string.Format("<TIPO_SERVICIO_GRILLA>{0}</TIPO_SERVICIO_GRILLA>\r\n", item.tipodeco));
                    xmlGenerated.Append(string.Format("<CARGO_FIJO_SIN_IGV_GRILLA>{0}</CARGO_FIJO_SIN_IGV_GRILLA>\r\n", amountWithIgv));
                }
            }

            xmlGenerated.Append("</PLANTILLA>");
            return xmlGenerated.ToString();
        }

        public static string BuildStringPostSale(Model.PostSaleProcessModel objViewModel)
        {
            string strTrama = string.Empty;

            strTrama = strTrama + objViewModel.PiCodId;
            strTrama = strTrama + '|' + objViewModel.PiCustomerId;
            strTrama = strTrama + '|' + "$IdInteracion";
            strTrama = strTrama + '|' + objViewModel.PiTipoTrans;
            strTrama = strTrama + '|' + objViewModel.PiTiptra;
            strTrama = strTrama + '|' + objViewModel.PiCodIntercaso;
            strTrama = strTrama + '|' + objViewModel.PiCodmotot;
            strTrama = strTrama + '|' + objViewModel.PiTipoVia;
            strTrama = strTrama + '|' + objViewModel.PiNomVia;
            strTrama = strTrama + '|' + objViewModel.PiNumVia;
            strTrama = strTrama + '|' + objViewModel.PiTipUrb;
            strTrama = strTrama + '|' + objViewModel.PiNomurb;
            strTrama = strTrama + '|' + objViewModel.PiManzana;
            strTrama = strTrama + '|' + objViewModel.PiLote;
            strTrama = strTrama + '|' + objViewModel.PiUbigeo;
            strTrama = strTrama + '|' + objViewModel.PiCodzona;
            strTrama = strTrama + '|' + objViewModel.PiCodplano;
            strTrama = strTrama + '|' + objViewModel.PiCodedif;
            strTrama = strTrama + '|' + objViewModel.PiReferencia;
            strTrama = strTrama + '|' + objViewModel.PiFecProg;
            strTrama = strTrama + '|' + objViewModel.PiFranjaHor;
            strTrama = strTrama + '|' + objViewModel.PiNumCarta;
            strTrama = strTrama + '|' + objViewModel.PiOperador;
            strTrama = strTrama + '|' + objViewModel.PiPresuscrito;
            strTrama = strTrama + '|' + objViewModel.PiPublicar;
            strTrama = strTrama + '|' + objViewModel.PiTmcode;
            strTrama = strTrama + '|' + objViewModel.PiUsureg;
            strTrama = strTrama + '|' + objViewModel.PiCargo;
            strTrama = strTrama + '|' + objViewModel.PiCodCaso;
            strTrama = strTrama + '|' + objViewModel.PiTiposervicio;
            strTrama = strTrama + '|' + objViewModel.PiFlagActDirFact;
            strTrama = strTrama + '|' + objViewModel.PiTipoProducto;
            strTrama = strTrama + '|' + ConfigurationManager.AppSettings("codOCCIDADDLTE");
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + objViewModel.PiObservacion;
            strTrama = strTrama + '|' + string.Empty;
            strTrama = strTrama + '|' + string.Empty;
            return strTrama;
        }
    }
}