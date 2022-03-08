using Claro.SIACU.Entity.Transac.Service.Fixed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class PlanMigrationLte
    {
        /// <summary>
        /// Obtiene la lista de planes
        /// </summary>
        /// <param name="objPlansRequest"></param>
        /// <returns>retorna una lista de planes</returns>
        public static Entity.Transac.Service.Fixed.GetPlans.PlansResponse GetPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest)
        {
            List<Entity.Transac.Service.Fixed.ProductPlan> listPlanResponse = null;
            List<Entity.Transac.Service.Fixed.ProductPlan> listPlan = null;
            List<Entity.Transac.Service.Fixed.ProductPlan> listPlanOrdered;
            try
            {
                listPlan = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ProductPlan>>(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.GetNewPlans(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, objPlansRequest.strPlano, objPlansRequest.strOferta, objPlansRequest.strTipoProducto);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, ex.Message);
            }

            if (listPlan != null)
            {
                listPlanResponse = new List<Entity.Transac.Service.Fixed.ProductPlan>();
                listPlanOrdered = listPlan.OrderBy(a => Convert.ToDate(a.FechaInicio)).ToList();
                listPlanResponse = listPlanOrdered;
            }
            Entity.Transac.Service.Fixed.GetPlans.PlansResponse objPlansResponse = new Entity.Transac.Service.Fixed.GetPlans.PlansResponse()
            {
                listPlan = listPlanResponse
            };
            return objPlansResponse;
        }

        public static Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse GetServicesByPlan(Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listService = null;
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.GetServicesByPlan(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, objServicesRequest.idplan, objServicesRequest.strTipoProducto);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.ServiceByPlan>();
                listServiceOrdered = listService.OrderBy(a => a.CodeExternal).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse objPlansResponse = new Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse()
            {
                listServicio = listServiceResponse
            };
            return objPlansResponse;
        }

        public static Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse GetCarrierList(Entity.Transac.Service.Fixed.GetCarrierList.CarrierRequest objCarrierRequest)
        {
            List<Entity.Transac.Service.Fixed.Carrier> listCarrierResponse = null;
            List<Entity.Transac.Service.Fixed.Carrier> listCarrier = null;
            List<Entity.Transac.Service.Fixed.Carrier> listCarrierOrdered;
            try
            {
                listCarrier = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.Carrier>>(objCarrierRequest.Audit.Session, objCarrierRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetCarrierList(objCarrierRequest.Audit.Session, objCarrierRequest.Audit.Transaction);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCarrierRequest.Audit.Session, objCarrierRequest.Audit.Transaction, ex.Message);
            }

            if (listCarrier != null)
            {
                listCarrierResponse = new List<Entity.Transac.Service.Fixed.Carrier>();
                listCarrierOrdered = listCarrier.OrderBy(a => a.IDCARRIER).ToList();
                listCarrierResponse = listCarrierOrdered;
            }
            Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse objCarrierResponse = new Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse()
            {
                carriers = listCarrierResponse
            };
            return objCarrierResponse;
        }

        public static Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse GetServicesByInteraction(Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceRequest objInteractionServiceRequest)
        {
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listService = null;
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ServiceByInteraction>>(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetServicesByInteraction(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, objInteractionServiceRequest.idInteraccion);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.ServiceByInteraction>();
                listServiceOrdered = listService.OrderBy(a => a.COD_INTERAC).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse objInteractionServiceResponse = new Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse()
            {
                Services = listServiceResponse
            };
            return objInteractionServiceResponse;
        }

        public static Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse GetTransactionRuleList(Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesRequest objTransactionRuleRequest)
        {
            List<Entity.Transac.Service.Fixed.TransactionRule> listTransactionRuleResponse = null;
            List<Entity.Transac.Service.Fixed.TransactionRule> listTransactionRule = null;
            List<Entity.Transac.Service.Fixed.TransactionRule> listTransactionRuleOrdered;

            try
            {
                listTransactionRule = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.TransactionRule>>(objTransactionRuleRequest.Audit.Session, objTransactionRuleRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetTransactionRuleList(objTransactionRuleRequest.Audit.Session, objTransactionRuleRequest.Audit.Transaction, objTransactionRuleRequest.SubClase);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTransactionRuleRequest.Audit.Session, objTransactionRuleRequest.Audit.Transaction, ex.Message);
            }

            if (listTransactionRule != null)
            {
                listTransactionRuleResponse = new List<Entity.Transac.Service.Fixed.TransactionRule>();
                listTransactionRuleOrdered = listTransactionRule.OrderBy(a => a.REATV_REGLA).ToList();
                listTransactionRuleResponse = listTransactionRuleOrdered;
            }
            Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse objTransactionRuleResponse = new Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse()
            {
                rules = listTransactionRuleResponse
            };
            return objTransactionRuleResponse;
        }

        public static Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            List<Entity.Transac.Service.Fixed.JobType> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.JobType> listService = null;
            List<Entity.Transac.Service.Fixed.JobType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.JobType>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetJobTypes(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, objJobTypesRequest.p_tipo);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.JobType>();
                listServiceOrdered = listService.OrderBy(a => a.descripcion).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse objJobTypesResponse = new Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse()
            {
                JobTypes = listServiceResponse
            };
            return objJobTypesResponse;
        }


        public static Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            Entity.Transac.Service.Fixed.ETAFlow listServiceResponse = null;
            Entity.Transac.Service.Fixed.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.ETAFlowValidate(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.as_origen, RequestParam.av_idplano, RequestParam.av_ubigeo, RequestParam.an_tiptra, RequestParam.an_tipsrv);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = listService;
            }
            Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse Resultado = new Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };
            return Resultado;
        }

        public static Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse GetOrderType(Entity.Transac.Service.Fixed.GetOrderType.OrderTypesRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.OrderType> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.OrderType> listService = null;
            List<Entity.Transac.Service.Fixed.OrderType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.OrderType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetOrderType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.vIdtiptra);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.OrderType>();
                listServiceOrdered = listService.OrderBy(a => a.VALOR).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse Resultado = new Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse()
            {
                ordertypes = listServiceResponse
            };
            return Resultado;
        }

        public static Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.OrderSubType> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.OrderSubType> listService = null;
            List<Entity.Transac.Service.Fixed.OrderSubType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.OrderSubType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetOrderSubType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.av_cod_tipo_trabajo);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.OrderSubType>();
                listServiceOrdered = listService.OrderBy(a => a.COD_SUBTIPO_ORDEN).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse Resultado = new Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse()
            {
                OrderSubTypes = listServiceResponse
            };
            return Resultado;
        }

        public static Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse GetGroupCapacity(Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityRequest RequestParam)
        {
            Entity.Transac.Service.Fixed.ETAAuditoriaCapacity listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.ETAAuditoriaCapacity>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetGroupCapacity(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.pAPP, RequestParam.pIP_APP, RequestParam.pUsuario, RequestParam.vFechas, RequestParam.vUbicacion, RequestParam.vCalcDur, RequestParam.vCalcDurEspec, RequestParam.vCalcTiempoViaje, RequestParam.vCalcTiempoViajeEspec, RequestParam.vCalcHabTrabajo, RequestParam.vCalcHabTrabajoEspec, RequestParam.vObtenerUbiZona, RequestParam.vObtenerUbiZonaEspec, RequestParam.vEspacioTiempo, RequestParam.vHabilidadTrabajo, RequestParam.vCampoActividad, RequestParam.vListaCapReqOpc);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
            }
            Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse Resultado = new Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse()
            {
                listPlan = listService
            };
            return Resultado;
        }


        public static Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse GetTimeZones(Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.TimeZone> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.TimeZone> listService = null;
            List<Entity.Transac.Service.Fixed.TimeZone> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.TimeZone>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetTimeZones(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.strAnUbigeo, RequestParam.strAnTiptra, RequestParam.strAdFecagenda);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.TimeZone>();
                listServiceOrdered = listService.OrderBy(a => a.CODCON).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse Resultado = new Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse()
            {
                TimeZones = listServiceResponse
            };
            return Resultado;
        }

        
        public static Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse ExecutePlanMigrationLte(Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.MigratedPlan> listServiceResponse = null;
            OsbLteEntity listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<OsbLteEntity>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.ExecutePlanMigrationLte(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.TransactionId, RequestParam.ServicesList, RequestParam.Tipification, RequestParam.ClientParameters, RequestParam.MainParameters, RequestParam.PlusParameters, RequestParam.EtaSelection, RequestParam.SotParameters, RequestParam.EtaParameters, RequestParam.Contract, RequestParam.ActualizarTipificacion, RequestParam.FlagContingencia, RequestParam.FlagCrearPlantilla, RequestParam.AuditRegister, RequestParam.ListCoser, RequestParam.FlagValidaEta, RequestParam.ParametrosConstancia, RequestParam.DestinatarioCorreo, RequestParam.Notes, RequestParam.strTipoPlan, RequestParam.strCodPlan, RequestParam.strTmCode, RequestParam.strTipoProducto, RequestParam.strCodServicioGeneralTope, RequestParam.dblMontoTopeConsumo, RequestParam.dblTopeConsumo, RequestParam.strComentTopeConsumo, RequestParam.dblLimiteCredito, RequestParam.strAnotacionToa);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(RequestParam.Audit.Session, RequestParam.Audit.Transaction, "PLANMIGRATIONLTE_EXCEPCION DE LLAMADA EN CAPA DATA: ");
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
                
            }

            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse Resultado = new Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse()
            {
                 result = listService
            };
            return Resultado;
        }

        
        public static Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse GetCurrentPlan(Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanRequest RequestParam)
        {
            Entity.Transac.Service.Fixed.CurrentPlan ServiceResponse = null;
            try
            {
                ServiceResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.CurrentPlan>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.GetCurrentPlan(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.CoId);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse Resultado = new Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse()
            {
                 Plan= ServiceResponse
            };
            return Resultado;
        }
        
        public static Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse SendNewPlanServices(Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesRequest RequestParam)
        {
            bool oServiceResponse = false;
            try
            {
                oServiceResponse = Claro.Web.Logging.ExecuteMethod<bool>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.SendNewPlanServices(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.Services);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse Resultado = new Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse()
            {
                 result = oServiceResponse
            };
            return Resultado;
        }
        public static Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.ServiceByCurrentPlan> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.ServiceByCurrentPlan> listService = null;
            List<Entity.Transac.Service.Fixed.ServiceByCurrentPlan> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ServiceByCurrentPlan>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetServicesByCurrentPlan(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.ContractId);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.ServiceByCurrentPlan>();
                listServiceOrdered = listService.OrderBy(a => a.DeSer).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse Resultado = new Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse()
            {
                ServicesByCurrentPlan = listServiceResponse
            };
            return Resultado;
        }

        public static Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse GetEquipmentByCurrentPlan(Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanRequest RequestParam)
        {
            List<Entity.Transac.Service.Fixed.EquipmentByCurrentPlan> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.EquipmentByCurrentPlan> listService = null;
            List<Entity.Transac.Service.Fixed.EquipmentByCurrentPlan> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.EquipmentByCurrentPlan>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationHfc.GetEquipmentByCurrentPlan(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.strIdContract);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.EquipmentByCurrentPlan>();
                listServiceOrdered = listService.OrderBy(a => a.Description).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse Resultado = new Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse()
            {
                Equipments = listServiceResponse
            };
            return Resultado;
        }
        
        public static Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse GetTechnicalVisitResult(Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultRequest RequestParam)
        {
            TechnicalVisit serviceResponse = new TechnicalVisit();
            try
            {
                serviceResponse = Claro.Web.Logging.ExecuteMethod<TechnicalVisit>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.GetTechnicalVisitResult(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.strCoId,RequestParam.strCustomerId,RequestParam.strTmCode,RequestParam.strCodPlanSisact,RequestParam.strTrama);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse Resultado = new Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse()
            {
                 Result = serviceResponse
            };
            return Resultado;
        }
        
        public static Entity.Transac.Service.Common.GetHubs.GetHubsResponse GetHubs(Entity.Transac.Service.Common.GetHubs.GetHubsRequest RequestParam)
        {
            List<Entity.Transac.Service.Common.BEHub> listServiceResponse = null;
            List<Entity.Transac.Service.Common.BEHub> listService = null;
            List<Entity.Transac.Service.Common.BEHub> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Common.BEHub>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetHubs(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.strCustomerId,RequestParam.strContrato);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Common.BEHub>();
                listServiceOrdered = listService.OrderBy(a => a.strHub).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Common.GetHubs.GetHubsResponse Resultado = new Entity.Transac.Service.Common.GetHubs.GetHubsResponse()
            {
                 listHub = listServiceResponse
            };
            return Resultado;
        }

        public static Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse ExecutePlanMigrationLTE(Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTERequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse objResponse = new Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.PlanMigrationLTE.ExecutePlanMigrationLTE(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "PLANMIGRATIONLTE_EXCEPCION DE LLAMADA EN CAPA DATA: ");
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);

            }
            return objResponse;
        }
    }
}
