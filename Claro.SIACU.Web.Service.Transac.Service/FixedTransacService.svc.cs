using Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert;
using System;
using Claro.Entity;
using System.ServiceModel;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using EntityCommon = Claro.SIACU.Entity.Transac.Service.Common;
using ConsultClienteHFCWS = Claro.SIACU.ProxyService.Transac.Service.WSClienteHFC;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH;
using FUNCTIONS = Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta;
using POSTPREDATA = Claro.SIACU.ProxyService.Transac.Service.SIAC.Post.DatosPrePost_V2;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard; //INICIATIVA-871
using Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard.ProcesarContinue;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FixedTransacService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FixedTransacService.svc or FixedTransacService.svc.cs at the Solution Explorer and start debugging.
    public class FixedTransacService : IFixedTransacService 
    {
        
        public EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request)
        {
            EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ProgramTask.GetListScheduledTransactions(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
       

        public EntitiesFixed.GetCommercialServices.CommercialServicesResponse GetCommercialService(EntitiesFixed.GetCommercialServices.CommercialServicesRequest objRequest)
        {
            EntitiesFixed.GetCommercialServices.CommercialServicesResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.AdditionalServices
                                    .GetCommercialServices(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetCommertialPlan.CommertialPlanResponse GetCommertialPlan(EntitiesFixed.GetCommertialPlan.CommertialPlanRequest objRequest)
        {
            EntitiesFixed.GetCommertialPlan.CommertialPlanResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.AdditionalServices.GetCommertialPlan(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProductTracDeacServ(
            EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest)
        {
            EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        () =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalServices
                                .GetProductTracDeacServ(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCamapaign.CamapaignResponse GetCamapaign(
            EntitiesFixed.GetCamapaign.CamapaignRequest objRequest)
        {
            EntitiesFixed.GetCamapaign.CamapaignResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.AdditionalServices.GetCamapaign(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(
            EntitiesFixed.GetInsertInteractHFC.GetInsertInteractHFCRequest objRequest)
        {
            EntitiesFixed.GetInsertInteractHFC.InsertInteractHFCResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.AdditionalServices
                                    .GetInsertInteractHFC(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServices(
            EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            EntitiesFixed.GetPlanServices.PlanServicesResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.AdditionalServices
                                    .GetPlanServices(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateResponse GetInfoInteractionTemplate(
            EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateRequest objRequest)
        {
            EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateResponse objResponse = null;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalServices
                                .GetInfoInteractionTemplate(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCustomer.CustomerResponse GetCustomer(
            EntitiesFixed.GetCustomer.GetCustomerRequest objRequest)
        {
            EntitiesFixed.GetCustomer.CustomerResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.Fixed.GetCustomer(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedResponse GetInsertInteractionMixed(
            EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedRequest objRequest)
        {
            EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalServices
                                .GetInsertInteractionMixed(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCustomerPhone.CustomerPhoneResponse GetCustomerPhone(
            EntitiesFixed.GetCustomerPhone.CustomerPhoneRequest objRequest)
        {
            EntitiesFixed.GetCustomerPhone.CustomerPhoneResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.CallsDetail.GetCustomerPhone(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCallDetailDB1.CallDetailDB1Response GetCallDetailDB1(
            EntitiesFixed.GetCallDetailDB1.CallDetailDB1Request objRequest)
        {
            EntitiesFixed.GetCallDetailDB1.CallDetailDB1Response objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.CallsDetail.GetCallDetailDB1(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCallDetail.CallDetailResponse GetCallDetail(
            EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {
            EntitiesFixed.GetCallDetail.CallDetailResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.CallsDetail.GetCallDetail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetFacturePDI.FacturePDIResponse GetFacturePDI(
            EntitiesFixed.GetFacturePDI.FacturePDIRequest objRequest)
        {
            EntitiesFixed.GetFacturePDI.FacturePDIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.CallsDetail.GetFacturePDI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetFactureDBTO.FactureDBTOResponse GetFactureDBTO(
            EntitiesFixed.GetFactureDBTO.FactureDBTORequest objRequest)
        {
            EntitiesFixed.GetFactureDBTO.FactureDBTOResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.CallsDetail.GetFactureDBTO(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCallDetail.CallDetailResponse GetBilledCallsDetailDB1_BSCS(
            EntitiesFixed.GetCallDetail.CallDetailRequest objRequest)
        {
            EntitiesFixed.GetCallDetail.CallDetailResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.CallsDetail.GetBilledCallsDetailDB1_BSCS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region JCAA

        public Entity.Transac.Service.Fixed.GetPlans.PlansResponse LteGetPlans(
            Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.GetPlans.PlansResponse objPlansResponse = null;
            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod(
                    objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte.GetPlans(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }

            return objPlansResponse;
        }
        public Entity.Transac.Service.Fixed.GetPlans.PlansResponse HfcGetPlans(
            Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.GetPlans.PlansResponse objPlansResponse = null;
            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod(
                    objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                    {
                        return Business.Transac.Service.Fixed.PlanMigrationHfc.GetPlans(objPlansRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }

            return objPlansResponse;
        }

        public Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse LteGetServicesByPlan(
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse objServicesResponse = null;
            try
            {
                objServicesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte
                        .GetServicesByPlan(objServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objServicesResponse;
        }
        public Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse HfcGetServicesByPlan(
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse objServicesResponse = null;
            try
            {
                objServicesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.PlanMigrationHfc
                                .GetServicesByPlan(objServicesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objServicesResponse;
        }

        public Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse GetCarrierList(
            Entity.Transac.Service.Fixed.GetCarrierList.CarrierRequest objCarriersRequest)
        {
            Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse objCarriersResponse = null;
            try
            {
                objCarriersResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objCarriersRequest.Audit.Session, objCarriersRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetCarrierList(objCarriersRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCarriersRequest.Audit.Session, objCarriersRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objCarriersResponse;
        }

        public Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse
            GetServicesByInteraction(
                Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceRequest
                    objInteractionServiceRequest)
        {
            Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse
                objInteractionServicesResponse = null;
            try
            {
                objInteractionServicesResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.Transac.Service.Fixed.Fixed.GetServicesByInteraction(
                                    objInteractionServiceRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionServiceRequest.Audit.Session,
                    objInteractionServiceRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objInteractionServicesResponse;
        }

        public Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse GetTransactionRuleList(
            Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesRequest objTransactionRulesRequest)
        {
            Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse objTransactionRulesResponse =
                null;
            try
            {
                objTransactionRulesResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objTransactionRulesRequest.Audit.Session, objTransactionRulesRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.Transac.Service.Fixed.PlanMigrationHfc.GetTransactionRuleList(
                                    objTransactionRulesRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTransactionRulesRequest.Audit.Session,
                    objTransactionRulesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objTransactionRulesResponse;
        }

        //public Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        //{
        //    Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse objJobTypesResponse = null;
        //    try
        //    {
        //        objJobTypesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
        //        {
        //            return Business.Transac.Service.Fixed.PlanMigrationHfc.GetJobTypes(objJobTypesRequest);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

        //    }
        //    return objJobTypesResponse;
        //}


        //public Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest)
        //{
        //    Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse objPlansResponse = null;
        //    try
        //    {
        //        objPlansResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse>(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
        //        {
        //            return Business.Transac.Service.Fixed.PlanMigrationHfc.ETAFlowValidate(objPlansRequest);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

        //    }
        //    return objPlansResponse;
        //}

        //public Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse GetOrderType(Entity.Transac.Service.Fixed.GetOrderType.OrderTypesRequest objOrderTypesRequest)
        //{
        //    Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse objOrderTypesResponse = null;
        //    try
        //    {
        //        objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse>(objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction, () =>
        //        {
        //            return Business.Transac.Service.Fixed.PlanMigrationHfc.GetOrderType(objOrderTypesRequest);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

        //    }
        //    return objOrderTypesResponse;
        //}


        //public Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest)
        //{
        //    Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse objSubTypesResponse = null;
        //    try
        //    {
        //        objSubTypesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse>(objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction, () =>
        //        {
        //            return Business.Transac.Service.Fixed.PlanMigrationHfc.GetOrderSubType(objSubTypesRequest);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

        //    }
        //    return objSubTypesResponse;
        //}

        public Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse GetGroupCapacity(
            Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityRequest objETAAuditoriaCapacityRequest)
        {
            Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse objETAAuditoriaCapacityResponse =
                null;
            try
            {
                objETAAuditoriaCapacityResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objETAAuditoriaCapacityRequest.Audit.Session,
                            objETAAuditoriaCapacityRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetGroupCapacity(
                        objETAAuditoriaCapacityRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objETAAuditoriaCapacityRequest.Audit.Session,
                    objETAAuditoriaCapacityRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objETAAuditoriaCapacityResponse;
        }


        public Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse GetTimeZones(
            Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasRequest objFranjasHorariasRequest)
        {
            Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse objFranjasHorariasResponse = null;
            try
            {
                objFranjasHorariasResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objFranjasHorariasRequest.Audit.Session, objFranjasHorariasRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetTimeZones(
                        objFranjasHorariasRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFranjasHorariasRequest.Audit.Session,
                    objFranjasHorariasRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objFranjasHorariasResponse;
        }

        public Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypesVisitOrder(
            Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse objJobTypesResponse = null;
            try
            {
                objJobTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetJobTypes(objJobTypesRequest);
                });

                foreach (var item in objJobTypesResponse.JobTypes)
                {
                    if (item.FLAG_FRANJA.Equals(Convert.ToString(1)))
                    {
                        item.tiptra = item.tiptra + ".|";
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objJobTypesResponse;
        }


        public Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse ExecutePlanMigrationLte(
            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanRequest objMigratedPlansRequest)
        {
            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse objMigratedPlansResponse = null;
            try
            {
                objMigratedPlansResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objMigratedPlansRequest.Audit.Session, objMigratedPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte.ExecutePlanMigrationLte(
                        objMigratedPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMigratedPlansRequest.Audit.Session,
                    objMigratedPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objMigratedPlansResponse;
        }

        public Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse ExecutePlanMigrationHfc(
            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanRequest objMigratedPlansRequest)
        {
            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse objMigratedPlansResponse = null;
            try
            {
                objMigratedPlansResponse =
                    Claro.Web.Logging
                        .ExecuteMethod<Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse>(
                            objMigratedPlansRequest.Audit.Session, objMigratedPlansRequest.Audit.Transaction, () =>
                            {
                                return Business.Transac.Service.Fixed.PlanMigrationHfc.ExecutePlanMigrationHfc(
                                    objMigratedPlansRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMigratedPlansRequest.Audit.Session,
                    objMigratedPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objMigratedPlansResponse;
        }

        public Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse GetCurrentPlan(
            Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte.GetCurrentPlan(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }


        public Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse
            GetServicesByCurrentPlan(
                Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc
                        .GetServicesByCurrentPlan(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }


        public Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse SendNewPlanServices(
            Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesRequest objNewPlanServicesRequest)
        {
            Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objNewPlanServicesRequest.Audit.Session, objNewPlanServicesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte.SendNewPlanServices(
                        objNewPlanServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objNewPlanServicesRequest.Audit.Session,
                    objNewPlanServicesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }


        public Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse
            GetEquipmentByCurrentPlan(
                Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanRequest
                    objEquipmentsByCurrentPlanRequest)
        {
            Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse
                objEquipmentByCurrentPlanResponse = null;
            try
            {
                objEquipmentByCurrentPlanResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objEquipmentsByCurrentPlanRequest.Audit.Session,
                            objEquipmentsByCurrentPlanRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetEquipmentByCurrentPlan(
                        objEquipmentsByCurrentPlanRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEquipmentsByCurrentPlanRequest.Audit.Session,
                    objEquipmentsByCurrentPlanRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objEquipmentByCurrentPlanResponse;
        }


        public Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse
            GetTechnicalVisitResult(
                Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc
                        .GetTechnicalVisitResult(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }

        
        public Entity.Transac.Service.Common.GetHubs.GetHubsResponse GetHubsHfc(Entity.Transac.Service.Common.GetHubs.GetHubsRequest objPlansRequest)
        {
            Entity.Transac.Service.Common.GetHubs.GetHubsResponse objPlansResponse = null;
            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetHubs.GetHubsResponse>(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationHfc.GetHubs(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);

            }
            return objPlansResponse;
        }
                public Entity.Transac.Service.Common.GetHubs.GetHubsResponse GetHubsLte(Entity.Transac.Service.Common.GetHubs.GetHubsRequest objPlansRequest)
        {
            Entity.Transac.Service.Common.GetHubs.GetHubsResponse objPlansResponse = null;
            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetHubs.GetHubsResponse>(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.PlanMigrationLte.GetHubs(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);

            }
            return objPlansResponse;
        }
        public Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse ExecutePlanMigrationLTE(
            Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTERequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse objResponse = new Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse();
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                            {
                                return Business.Transac.Service.Fixed.PlanMigrationLte.ExecutePlanMigrationLTE(
                                    objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objResponse;
        }


        #endregion

        #region RetenciónCancelación

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetListarAccionesRC(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetListarAcciones(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotCancelacion(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetMotCancelacion(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vEstado.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSubMotiveCancel(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetSubMotiveCancel(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vIdMotive.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetTypeWork(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetTypeWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSubTypeWork(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse =
                new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetSubTypeWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotiveSOT(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetMotiveSOT(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.AddDayWorkResponse GetAddDayWork(
            EntitiesFixed.GetCaseInsert.AddDayWorkRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.AddDayWorkResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetAddDayWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ
                        .GetObtainParameterTerminalTPI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSoloTFIPostpago(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetSoloTFIPostpago(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainPenalidadExt(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetObtainPenalidadExt(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse ObtenerDatosBSCSExt(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.ObtenerDatosBSCSExt(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(
            EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest)
        {
            EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetValidateCustomerId(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(
            Entity.Transac.Service.Fixed.Customer objRequest)
        {
            EntitiesFixed.GetCustomer.CustomerResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetRegisterCustomerId(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public string GetRegisterEtaSelection(
            Entity.Transac.Service.Fixed.GetRegisterEtaSelection.RegisterEtaSelectionRequest objRequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetRegisterEtaSelection(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return resultado;
        }


        public EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCaseInsert(
            EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            CaseInsertResponse oResponse = new CaseInsertResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetCaseInsert(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return oResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetApadeceCancelRet(
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetApadeceCancelRet(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }






        public Entity.Transac.Service.Fixed.GetGenericSot.GenericSotResponse GetGenericSOT(
            Entity.Transac.Service.Fixed.GetGenericSot.GenericSotRequest objGenericSotRequest)
        {
            Entity.Transac.Service.Fixed.GetGenericSot.GenericSotResponse objGenericSotResponse;
            try
            {
                objGenericSotResponse = Claro.Web.Logging
                    .ExecuteMethod(
                    objGenericSotRequest.Audit.Session,
                    objGenericSotRequest.Audit.Transaction,
                        () =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalPoints.GetGenericSot(objGenericSotRequest);
                        }
                );
            }
            catch (Exception)
            {

                throw;
            }
            return objGenericSotResponse;
        }

        public EntitiesFixed.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(
            EntitiesFixed.GetUpdateInter29.UpdateInter29Request objRequest)
        {
            EntitiesFixed.GetUpdateInter29.UpdateInter29Response objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.AdditionalPoints.GetUpdateInter29(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public bool GetDesactivatedContract(Customer objRequestCliente)
        {
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ
                            .GetDesactivatedContract(objRequestCliente);
                    }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;

        }

        public bool GetDesactivatedContract_LTE(Customer objRequest)
        {
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetDesactivatedContract_LTE(objRequest);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;

        }

        #endregion

        public EntitiesFixed.GetService.ServiceResponse GetTelephoneByContractCode(
            EntitiesFixed.GetService.ServiceRequest objRequest)
        {
            EntitiesFixed.GetService.ServiceResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetTelephoneByContractCode(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetPhoneNumber.PhoneNumberResponse GetExecuteChangeNumber(
            EntitiesFixed.GetPhoneNumber.PhoneNumberRequest objRequest)
        {
            EntitiesFixed.GetPhoneNumber.PhoneNumberResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ChangePhoneNumber.GetExecuteChangeNumber(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region External/ Internal Transfer

        public EntitiesFixed.GetRecordTransExtInt.RecordTranferExtIntResponse GetRecordTransaction(
            EntitiesFixed.GetRecordTransExtInt.RecordTranferExtIntRequest objGetRecordTransactionRequest)
        {
            EntitiesFixed.GetRecordTransExtInt.RecordTranferExtIntResponse ResponseobjGetRecordTransactionResponse;

            try
            {
                ResponseobjGetRecordTransactionResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        () =>
                        {
                            return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetRecordTransaction(
                                objGetRecordTransactionRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRecordTransactionRequest.Audit.Session,
                    objGetRecordTransactionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return ResponseobjGetRecordTransactionResponse;
        }

        public EntitiesFixed.GetGenerateSOT.GenerateSOTResponse GetGenerateSOT(
            EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            EntitiesFixed.GetGenerateSOT.GenerateSOTResponse objGenerateSOTResponse;

            try
            {
                objGenerateSOTResponse =
                    Claro.Web.Logging.ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetGenerateSOT(
                            objGetGenerateSOTRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetGenerateSOTRequest.Audit.Session,
                    objGetGenerateSOTRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objGenerateSOTResponse;
        }

        #endregion


        #region Punto Adicional

        public EntitiesFixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity
            GetETAAuditoriaRequestCapacity(
                EntitiesFixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity
                    objBEETAAuditoriaRequestCapacity)
        {

            EntitiesFixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetETAAuditoriaRequestCapacity(
                        objBEETAAuditoriaRequestCapacity);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBEETAAuditoriaRequestCapacity.Audit.Session,
                    objBEETAAuditoriaRequestCapacity.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public int registraEtaRequest(EntitiesFixed.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {
            var objResponse = 0;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session,
                    objRegisterEtaRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.AdditionalPoints
                        .registraEtaRequest(objRegisterEtaRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public string registraEtaResponse(EntitiesFixed.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            string vidreturn = string.Empty;
            try
            {
                vidreturn = Claro.Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session,
                    objRegisterEtaResponse.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.AdditionalPoints
                        .RegisterEtaResponse(objRegisterEtaResponse);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return vidreturn;
        }

        public EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse REGISTRA_COSTO_PA(
            EntitiesFixed.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {
            EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse =
                new EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Claro.Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session,
                    objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.AdditionalPoints.REGISTRA_COSTO_PA(
                        objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailTransExtraRequest.Audit.Session,
                    objDetailTransExtraRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objDetailTransExtraResponse;
        }

        #endregion


        public EntitiesFixed.GetGenerateOCC.GenerateOCCResponse GenerateOCC(
            EntitiesFixed.GetGenerateOCC.GenerateOCCRequest objRequest)
        {
            EntitiesFixed.GetGenerateOCC.GenerateOCCResponse objResponse =
                new EntitiesFixed.GetGenerateOCC.GenerateOCCResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GenerateOCC(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse ACTUALIZAR_COSTO_PA(
            EntitiesFixed.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {
            EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse =
                new EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Claro.Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session,
                    objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.AdditionalPoints.ACTUALIZAR_COSTO_PA(
                        objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailTransExtraRequest.Audit.Session,
                    objDetailTransExtraRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objDetailTransExtraResponse;
        }



        #region "Inst/Desinst Decodificadores"

        public Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse GetProductDetail(
            Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailRequest objProductDetailRequest)
        {
            Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse objProductDetailResponse;

            try
            {
                objProductDetailResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetProductDetail(
                                objProductDetailRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objProductDetailRequest.Audit.Session,
                    objProductDetailRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objProductDetailResponse;
        }

        public Entity.Transac.Service.Fixed.GetAddtionalEquipment.AddtionalEquipmentResponse GetAddtionalEquipment(
            Entity.Transac.Service.Fixed.GetAddtionalEquipment.AddtionalEquipmentRequest objAddtionalEquipmentRequest)
        {
            Entity.Transac.Service.Fixed.GetAddtionalEquipment.AddtionalEquipmentResponse objAddtionalEquipmentResponse;

            try
            {
                objAddtionalEquipmentResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                    .GetAddtionalEquipment(objAddtionalEquipmentRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAddtionalEquipmentRequest.Audit.Session,
                    objAddtionalEquipmentRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objAddtionalEquipmentResponse;
        }

        public Entity.Transac.Service.Fixed.GetProcessingServices.ProcessingServicesResponse GetProcessingServices(
            Entity.Transac.Service.Fixed.GetProcessingServices.ProcessingServicesRequest objProcessingServicesRequest)
        {
            Entity.Transac.Service.Fixed.GetProcessingServices.ProcessingServicesResponse objProcessingServicesResponse;

            try
            {
                objProcessingServicesResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                    .GetProcessingServices(objProcessingServicesRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objProcessingServicesRequest.Audit.Session,
                    objProcessingServicesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objProcessingServicesResponse;
        }

        public Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(
            Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse objJobTypesResponse = null;
            try
            {
                objJobTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetJobTypes(objJobTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objJobTypesResponse;
        }

        public Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(
            Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.ETAFlowValidate(objPlansRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }

        public Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse GetOrderType(
            Entity.Transac.Service.Fixed.GetOrderType.OrderTypesRequest objOrderTypesRequest)
        {
            Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse objOrderTypesResponse = null;
            try
            {
                objOrderTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetOrderType(objOrderTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objOrderTypesResponse;
        }

        public Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(
            Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest)
        {
            Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse objSubTypesResponse = null;
            try
            {
                objSubTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetOrderSubType(objSubTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objSubTypesResponse;
        }

        public Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubTypeWork(
            Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest)
        {
            Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse objSubTypesResponse = null;
            try
            {
                objSubTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.Fixed.GetOrderSubTypeWork(objSubTypesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objSubTypesResponse;
        }

        public Entity.Transac.Service.Fixed.GetDecoDetailByIdService.DecoDetailByIdServiceResponse
            GetDecoDetailByIdService(
                Entity.Transac.Service.Fixed.GetDecoDetailByIdService.DecoDetailByIdServiceRequest
                    objDecoDetailByIdServiceRequest)
        {
            Entity.Transac.Service.Fixed.GetDecoDetailByIdService.DecoDetailByIdServiceResponse
                objDecoDetailByIdServiceResponse;

            try
            {
                objDecoDetailByIdServiceResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                .GetDecoDetailByIdService(objDecoDetailByIdServiceRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDecoDetailByIdServiceRequest.Audit.Session,
                    objDecoDetailByIdServiceRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objDecoDetailByIdServiceResponse;
        }

        public Entity.Transac.Service.Fixed.GetInsertDecoAdditional.InsertDecoAdditionalResponse
            GetInsertDecoAdditional(
                Entity.Transac.Service.Fixed.GetInsertDecoAdditional.InsertDecoAdditionalRequest
                    objInsertDecoAdditionalRequest)
        {
            Entity.Transac.Service.Fixed.GetInsertDecoAdditional.InsertDecoAdditionalResponse
                objInsertDecoAdditionalResponse = null;
            try
            {
                objInsertDecoAdditionalResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(objInsertDecoAdditionalRequest.Audit.Session,
                            objInsertDecoAdditionalRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertDecoAdditional(
                        objInsertDecoAdditionalRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertDecoAdditionalRequest.Audit.Session,
                    objInsertDecoAdditionalRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objInsertDecoAdditionalResponse;
        }

        public Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse
            GetInsertDetailServiceInteraction(
                Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest
                    objInsertDetailServiceInteractionRequest)
        {
            Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse
                objInsertDetailServiceInteractionResponse = null;
            try
            {
                objInsertDetailServiceInteractionResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objInsertDetailServiceInteractionRequest.Audit.Session,
                            objInsertDetailServiceInteractionRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertDetailServiceInteraction(
                        objInsertDetailServiceInteractionRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertDetailServiceInteractionRequest.Audit.Session,
                    objInsertDetailServiceInteractionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objInsertDetailServiceInteractionResponse;
        }

        public Entity.Transac.Service.Fixed.GetInsertETASelection.InsertETASelectionResponse GetInsertETASelection(
            Entity.Transac.Service.Fixed.GetInsertETASelection.InsertETASelectionRequest objInsertETASelectionRequest)
        {
            Entity.Transac.Service.Fixed.GetInsertETASelection.InsertETASelectionResponse
                objInsertETASelectionResponse = null;
            try
            {
                objInsertETASelectionResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objInsertETASelectionRequest.Audit.Session, objInsertETASelectionRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.Transac.Service.Fixed.Fixed.GetInsertETASelection(
                                    objInsertETASelectionRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertETASelectionRequest.Audit.Session,
                    objInsertETASelectionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objInsertETASelectionResponse;
        }

        public Entity.Transac.Service.Fixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(
            Entity.Transac.Service.Fixed.GetInsertTransaction.InsertTransactionRequest objInsertTransactionRequest)
        {
            Entity.Transac.Service.Fixed.GetInsertTransaction.InsertTransactionResponse objInsertTransactionResponse =
                null;
            try
            {
                objInsertTransactionResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objInsertTransactionRequest.Audit.Session, objInsertTransactionRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.Transac.Service.Fixed.Fixed.GetInsertTransaction(
                                    objInsertTransactionRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertTransactionRequest.Audit.Session,
                    objInsertTransactionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objInsertTransactionResponse;
        }

        public Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse GetProductDown(
            Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailRequest objProductDetailRequest)
        {
            Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse objProductDetailResponse;

            try
            {
                objProductDetailResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetProductDown(
                                objProductDetailRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objProductDetailRequest.Audit.Session,
                    objProductDetailRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objProductDetailResponse;
        }

        public Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmount(
    Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest)
        {
            Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse objLoyaltyAmountResponse;

            try
            {
                objLoyaltyAmountResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetLoyaltyAmount(
                                objLoyaltyAmountRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLoyaltyAmountRequest.Audit.Session,
                    objLoyaltyAmountRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objLoyaltyAmountResponse;
        }

        #endregion

        public EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse
            GetInsertInteractionBusiness(
                EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest)
        {
            EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertInteractionBusiness(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        //obtener lista de centro poblado
        public EntitiesFixed.GetListTownCenter.ListTownCenterResponse GetListTownCenter(
            EntitiesFixed.GetListTownCenter.ListTownCenterRequest objRequest)
        {
            EntitiesFixed.GetListTownCenter.ListTownCenterResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetListTownCenter(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        //obtener lista ubigeo desde ddp

        public EntitiesFixed.GetIdUbigeo.IdUbigeoResponse GetIdUbigeo(
            EntitiesFixed.GetIdUbigeo.IdUbigeoRequest objRequest)
        {

            EntitiesFixed.GetIdUbigeo.IdUbigeoResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetIdUbigeo(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesFixed.GetDecoServices.BEDecoServicesResponse GetServicesDTH(
            EntitiesFixed.GetDecoServices.BEDecoServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetDecoServices.BEDecoServicesResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetServicesDTH(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServicesLte(
            EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            EntitiesFixed.GetPlanServices.PlanServicesResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.Transac.Service.Fixed.AdditionalServices
                                    .GetPlanServicesLte(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        //obtner lista de planos
        public EntitiesFixed.GetListPlans.ListPlansResponse GetListPlans(
            EntitiesFixed.GetListPlans.ListPlansRequest objRequest)
        {
            EntitiesFixed.GetListPlans.ListPlansResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetListPlans(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        //obtener lista de edificaciones

        public EntitiesFixed.GetListEbuildings.ListEbuildingsResponse GetListEBuildings(
            EntitiesFixed.GetListEbuildings.ListEbuildingsRequest objRequest)
        {
            EntitiesFixed.GetListEbuildings.ListEbuildingsResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetListEBuildings(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        //obtener cobertura

        public EntitiesFixed.GetCoverage.CoverageResponse GetCoverage(
            EntitiesFixed.GetCoverage.CoverageRequest objRequest)
        {
            EntitiesFixed.GetCoverage.CoverageResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                    {

                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetCoverage(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;



        }

        public EntitiesFixed.GetAddressUpdate.AddressUpdateResponse GetUpdateAddress(
            EntitiesFixed.GetAddressUpdate.AddressUpdateRequest objRequest)
        {
            bool Result = false;
            EntitiesFixed.GetAddressUpdate.AddressUpdateResponse objAddressUpdateResponse =
                new EntitiesFixed.GetAddressUpdate.AddressUpdateResponse();
            try
            {
                objAddressUpdateResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {

                        return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetUpdateAddress(objRequest);
                    });
                objAddressUpdateResponse.blnResult = Result;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    ex.InnerException.Message);

            }
            return objAddressUpdateResponse;

        }

        public EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(
            EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {

            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse =
                new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
                oCustomerResponse = Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session,
                    oGetCustomerRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetValidateCustomer(oGetCustomerRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oCustomerResponse;
        }


        #region ConfigurationIP

        public List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> GetJobTypesConfigIP(
            EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPRequest objJobTypesRequest)
        {
            List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> objJobTypesResponse = null;
            try
            {
                objJobTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP.GetJobTypesConfIP(objJobTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objJobTypesResponse;
        }

        public List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse> GetTypeConfIP(
            EntitiesFixed.GetTypeConfip.TypeConfigIpRequest objTypeConfigIpRequest)
        {
            List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse> LstTypeConfigIpResponse =
                new List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse>();
            try
            {
                LstTypeConfigIpResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP.GetTypeConfIP(objTypeConfigIpRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return LstTypeConfigIpResponse;
        }

        public List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse> GetBranchCustomer(
            EntitiesFixed.GetBranchCustomer.BranchCustomerResquest objBranchCustomerResquest)
        {
            List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse> LstBranchCustomerResponse =
                new List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse>();
            try
            {
                LstBranchCustomerResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP.GetBranchCustomer(
                        objBranchCustomerResquest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBranchCustomerResquest.Audit.Session,
                    objBranchCustomerResquest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return LstBranchCustomerResponse;
        }

        public EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse ConfigurationServicesSave(
            EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest oConfigurationIPRequest)
        {
            EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPResponse =
                new EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse();
            try
            {
                oConfigurationIPResponse = Claro.Web.Logging.ExecuteMethod(oConfigurationIPRequest.Audit.Session,
                    oConfigurationIPRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP
                        .ConfigurationServicesSave(oConfigurationIPRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oConfigurationIPRequest.Audit.Session,
                    oConfigurationIPRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return oConfigurationIPResponse;
        }

        public   EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse GetConfigurationIPMegas(
            EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest  oConfigurationIPMegasRequest) 
        {
         EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPResponse =
                new EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse();
            try
            {
                oConfigurationIPResponse = Claro.Web.Logging.ExecuteMethod(oConfigurationIPMegasRequest.Audit.Session,
                    oConfigurationIPMegasRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP
                        .GetConfigurationIPMegas(oConfigurationIPMegasRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oConfigurationIPMegasRequest.Audit.Session,
                    oConfigurationIPMegasRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return oConfigurationIPResponse;
        }
        
        #endregion

        #region external / internal -LTE

        public EntitiesFixed.GetMotiveSoft.MotiveSoftResponse GetMotiveSoftLte(
            EntitiesFixed.GetMotiveSoft.MotiveSoftRequest objRequest
            )
        {
            EntitiesFixed.GetMotiveSoft.MotiveSoftResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetMotiveSoftLte(objRequest);

                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }


        public EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypeLte(
           EntitiesFixed.GetJobTypes.JobTypesRequest objRequest
            )
        {
            EntitiesFixed.GetJobTypes.JobTypesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetJobTypeLte(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetServicesLte.ServicesLteResponse GetServicesLte(
            EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            EntitiesFixed.GetServicesLte.ServicesLteResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {

                    return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetServicesLte(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse RegisterTransactionLTE(
            EntitiesFixed.GetRegisterTransaction.RegisterTransactionRequest objRequest
            )
        {
            EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.ExternalInternalTransfer
                            .RegisterTransactionLTE(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        #endregion


        public EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse GetActivationDesactivation(
            EntitiesFixed.GetActivationDesactivation.ActivationDesactivationRequest objRequest)
        {
            EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse objActivationDesactivation = null;

            try
            {
                objActivationDesactivation = Claro.Web.Logging
                    .ExecuteMethod(
                        () =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalServices
                                .GetActivationDesactivation(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objActivationDesactivation;
        }


        public EntitiesFixed.Interaction GetCreateCase(EntitiesFixed.Interaction oRequest)
        {

            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.Interaction GetInsertCase(
            EntitiesFixed.Interaction oItem)
        {

            Interaction oResponse = new Interaction();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.CaseTemplate GetInsertTemplateCase(
            EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertTemplateCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(
            EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertTemplateCaseContingent(oItem);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(
    EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ActualizaPlantillaCaso(oItem);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse GetBilledCallsDetailHfC(
            EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {
            EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () =>
                    {
                        return Business.Transac.Service.Fixed.CallsDetail.GetBilledCallsDetailHfC(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }



        public EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse
            GetConsultationServiceByContract(
                EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractRequest
                    oConsultationServiceByContractRequest)
        {
            EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse
                oConsultationServiceByContractResponse;

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetConsultationServiceByContract(
                        oConsultationServiceByContractRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oConsultationServiceByContractRequest.Audit.Session,
                    oConsultationServiceByContractRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return oConsultationServiceByContractResponse;
        }

        public EntitiesFixed.GetCallDetailInputFixed.CallDetailInputFixedResponse GetCallDetailInputFixed(EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {
            EntitiesFixed.GetCallDetailInputFixed.CallDetailInputFixedResponse oCallDetailInputFixedResponse = null;

            try
            {
                oCallDetailInputFixedResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetCallDetailInputFixed(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oCallDetailInputFixedResponse;
        }

        public EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest)
        {
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse oResponse =
                new EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ.GetMotiveSOTByTypeJob(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse GetValidateActDesService(
            EntitiesFixed.GetValidateActDesService.ValidateActDesServiceRequest objRequest)
        {
            EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(
                        () =>
                        {
                            return Business.Transac.Service.Fixed.AdditionalServices
                                .GetValidateActDesService(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse oResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetTransactionScheduled(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }


       
        public ServiceDTHResponse GetServiceDTH(ServiceDTHRequest objRequest)
        {
            ServiceDTHResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {

                    return Business.Transac.Service.Fixed.Fixed.GetServiceDTH(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetPlanCommercial.PlanCommercialResponse GetPlanCommercial(EntitiesFixed.GetPlanCommercial.PlanCommercialRequest objRequest)
        {
            EntitiesFixed.GetPlanCommercial.PlanCommercialResponse oCallDetailInputFixedResponse = null;

            try
            {
                oCallDetailInputFixedResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetPlanCommercial.PlanCommercialResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.AdditionalServices.GetPlanCommercial(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oCallDetailInputFixedResponse;
        }

        public EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse DeleteProgramTask(EntitiesFixed.DeleteProgramTask.DeleteProgramTaskRequest request)
        {
            EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse objResponse = new EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ProgramTask.DeleteProgramTask(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse DeleteProgramTaskHfc(EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcRequest request)
        {
            EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse objResponse = new EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ProgramTask.DeleteProgramTaskHfc(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceActivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest)
        {

            EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse objResponse = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.AdditionalServices.GetComServiceActivation(objrequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }


        public EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceDesactivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest)
        {

            EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse objResponse = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.AdditionalServices.GetComServiceDesactivation(objrequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProdIdTraDesacServ(EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest)
        {

            EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse oProductTracDeacServResponse = new EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse();
            try
            {
                oProductTracDeacServResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.AdditionalServices.GetProdIdTraDesacServ(objRequest);

                    });

                return oProductTracDeacServResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oProductTracDeacServResponse;
        }

        public EntitiesFixed.GetSaveOCC.SaveOCCResponse GetSaveOCC(EntitiesFixed.GetSaveOCC.SaveOCCRequest objRequest)
        {

            EntitiesFixed.GetSaveOCC.SaveOCCResponse objResponse = new EntitiesFixed.GetSaveOCC.SaveOCCResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.GetSaveOCC(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse GetInsertLoyalty(EntitiesFixed.GetInsertLoyalty.InsertLoyaltyRequest objRequest)
        {

            EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse objResponse = new EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.GetInsertLoyalty(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.SuspensionService.EjecutaSuspensionDeServicioCodRes(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest)
        {

            var objResponse = new EntitiesFixed.GetReconeService.ReconeServiceResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.SuspensionService.GetReconectionService(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse GetCustomerLineNumber(EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractRequest objRequest)
        {
            EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetCustomerLineNumber(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return model;
        }

        public EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.CaseInsertResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetInteractIDforCaseID(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return model;
        }

        public EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest objRequest)
        {
            EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.ProgramTask.UpdateProgTaskLte(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return model;
        }

        public EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.SuspensionService.EjecutaSuspensionDeServicioLte(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.Transac.Service.Fixed.SuspensionService.EjecutaReconexionDeServicioLte(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse LTERegisterTransaction(
            EntitiesFixed.GetRegisterTransaction.RegisterTransactionRequest objRequest
            )
        {
            EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.AdditionalPoints.LTERegisterTransaction(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }



        public List<EntitiesFixed.GetDataLine.DataLineResponse> GetDataLine(EntitiesFixed.GetDataLine.DataLineRequest oBE)
        {
            List<EntitiesFixed.GetDataLine.DataLineResponse> LstDataLine = new List<EntitiesFixed.GetDataLine.DataLineResponse>();

            try
            {
                LstDataLine = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GetDataLine.DataLineResponse>>(oBE.Audit.Session, oBE.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.ConfigurationIP.GetDataLine(oBE);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBE.Audit.Session, oBE.Audit.Transaction, ex.Message);
            }
            return LstDataLine;
        }

        public EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse oResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetSchedulingRule(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        public Entity.Transac.Service.Fixed.OrderSubType GetValidationSubTypeWork(EntitiesFixed.GetOrderSubType.OrderSubTypesRequest objRequest)
        {
            Entity.Transac.Service.Fixed.OrderSubType model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetValidationSubTypeWork(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return model;
        }

        public EntitiesFixed.GetGenerateSOT.GenerateSOTResponse registraEtaSot(
            EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            EntitiesFixed.GetGenerateSOT.GenerateSOTResponse objGenerateSOTResponse;

            try
            {
                objGenerateSOTResponse =
                    Claro.Web.Logging.ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.registraEta(
                            objGetGenerateSOTRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetGenerateSOTRequest.Audit.Session,
                    objGetGenerateSOTRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objGenerateSOTResponse;
        }

        public EntitiesFixed.GetGenerateSOT.GenerateSOTResponse UpdateEta(
            EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            EntitiesFixed.GetGenerateSOT.GenerateSOTResponse objGenerateSOTResponse;

            try
            {
                objGenerateSOTResponse =
                    Claro.Web.Logging.ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.UpdateEta(
                            objGetGenerateSOTRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetGenerateSOTRequest.Audit.Session,
                    objGetGenerateSOTRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objGenerateSOTResponse;
        }

public EntitiesFixed.GetServicesLte.ServicesLteResponse GetCustomerEquipments(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            EntitiesFixed.GetServicesLte.ServicesLteResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ChangeEquipment.GetCustomerEquipments(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

public Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse GetServicesByPlanLTE(
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse objServicesResponse = null;
            try
            {
                objServicesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                .GetServicesByPlan(objServicesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objServicesResponse;
        }

public EntitiesFixed.GetDispEquipment.DispEquipmentResponse GetValidateEquipment(EntitiesFixed.GetDispEquipment.DispEquipmentRequest objDispEquipmentRequest)
        {
            Entity.Transac.Service.Fixed.GetDispEquipment.DispEquipmentResponse objDispEquipmentResponse;

            try
            {
                objDispEquipmentResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.ChangeEquipment.GetValidateEquipment(objDispEquipmentRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDispEquipmentRequest.Audit.Session, objDispEquipmentRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objDispEquipmentResponse;
        }

        public EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardSANS(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest)
        {
            Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse objAvailabilitySimcardResponse;

            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.ChangeEquipment.GetAvailabilitySimcardSANS(objAvailabilitySimcardRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAvailabilitySimcardRequest.Audit.Session, objAvailabilitySimcardRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objAvailabilitySimcardResponse;
        }

        public EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardBSCS(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest)
        {
            Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse objAvailabilitySimcardResponse;

            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.ChangeEquipment.GetAvailabilitySimcardBSCS(objAvailabilitySimcardRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAvailabilitySimcardRequest.Audit.Session, objAvailabilitySimcardRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objAvailabilitySimcardResponse;
        }

        public EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetValidateSimcardBSCS_HLCODE(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest)
        {
            Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse objAvailabilitySimcardResponse;

            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.ChangeEquipment.GetValidateSimcardBSCS_HLCODE(objAvailabilitySimcardRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAvailabilitySimcardRequest.Audit.Session, objAvailabilitySimcardRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objAvailabilitySimcardResponse;
        }

        public EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipment(EntitiesFixed.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse objResponse; 

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ChangeEquipment.GetExecuteChangeEquipment(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }
public Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse GetDecoMatriz(
            Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizRequest objDecoMatrizRequest)
        {
            Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse objDecoMatrizResponse = null;
            try
            {
                objDecoMatrizResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                .GetDecoMatriz(objDecoMatrizRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objDecoMatrizResponse;
        }


        public Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse GetDecoType(
            Entity.Transac.Service.Fixed.GetDecoType.DecoTypeRequest objDecoTypeRequest)
        {
            Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse objDecoTypeResponse = null;
            try
            {
                objDecoTypeResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder
                                .GetDecoType(objDecoTypeRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction,
                    FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objDecoTypeResponse;
        }

public EntitiesFixed.PostExecuteDecosLte.DecosLteResponse PostExecuteDecosLte(EntitiesFixed.PostExecuteDecosLte.DecosLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostExecuteDecosLte.DecosLteResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder.PostExecuteDecosLte(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmountLte(
            Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest)
        {
            Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse objLoyaltyAmountResponse;

            try
            {
                objLoyaltyAmountResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetLoyaltyAmountLte(
                                objLoyaltyAmountRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLoyaltyAmountRequest.Audit.Session,
                    objLoyaltyAmountRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objLoyaltyAmountResponse;
        }
        public Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse GetDetEquipo_LTE(EntitiesFixed.GetDetEquipmentLTE.DataEquipmentRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetDetEquipmentLTE.DataEquipmentResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetDetEquipo_LTE(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }



        public EntitiesFixed.GetCamapaign.CamapaignResponse GetCampaign(EntitiesFixed.GetCamapaign.CamapaignRequest objRequest)
        {
            EntitiesFixed.GetCamapaign.CamapaignResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.GetCampaign(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.GetPlans.PlansResponse GetNewPlans(EntitiesFixed.GetPlans.PlansRequest objRequest)
        {
            EntitiesFixed.GetPlans.PlansResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.GetNewPlans(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.GetValidateDepVelLte.ValidateDepVelLteResponse ValidateDepVelLTE(EntitiesFixed.GetValidateDepVelLte.ValidateDepVelLteRequest objRequest)
        {
            EntitiesFixed.GetValidateDepVelLte.ValidateDepVelLteResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ValidateDepVelLTE(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }


        public Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse GetDataServById(EntitiesFixed.GetDataServ.DataServByIdRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetDataServ.DataServByIdResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetDataServById(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse SavePostventaDetServ(EntitiesFixed.GetSavePostventa.DataSavePostventaDetServRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetSavePostventa.DataSavePostventaDetServResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.SavePostventaDetServ(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarOrdenesToa(EntitiesFixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.PostGestionarOrdenesToa(objRequestInboundEta); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequestInboundEta.Audit.Session,
                    objRequestInboundEta.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarCancelaToa(EntitiesFixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.PostGestionarCancelaToa(objRequestInboundEta); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequestInboundEta.Audit.Session,
                    objRequestInboundEta.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetHistoryToa.HistoryToaResponse GetHistoryToa(EntitiesFixed.GetHistoryToa.HistoryToaRequest objRequest)
        {

            EntitiesFixed.GetHistoryToa.HistoryToaResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetHistoryToa(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public EntitiesFixed.GetHistoryToa.HistoryToaResponse GetUpdateHistoryToa(EntitiesFixed.GetHistoryToa.HistoryToaRequest objRequest)
        {

            EntitiesFixed.GetHistoryToa.HistoryToaResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.GetUpdateHistoryToa(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidateReservation(
            Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest)
        {
            Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                        {
                            return Business.Transac.Service.Fixed.Fixed.ETAFlowValidateReservation(objPlansRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }

#region Proy-32650
        public RetentionCancelServicesResponse GetMonths(RetentionCancelServicesRequest objRequest)
        {
            RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ.GetMonths(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public RetentionCancelServicesResponse GetListDiscount(RetentionCancelServicesRequest objRequest)
        {
            RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ.GetListDiscount(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetPlanServices.PlanServicesResponse GetRetentionRate(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            EntitiesFixed.GetPlanServices.PlanServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ.GetRetentionRate(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.IdPlan.ToString(), objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetCurrentDiscountFixedCharge(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.PlanMigrationHfc.GetCurrentDiscountFixedCharge(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse InsertDiscountBondExchangePlan(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.PlanMigrationHfc.InsertDiscountBondExchangePlan(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public bool RegisterBonoDiscount(EntitiesFixed.RegisterBonoDiscount.RegisterBonoDiscountRequest request)
        {
            bool result = false;
            try
            {
                result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegisterBonoDiscount(request);
                }
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return result;
        }

        public bool ActualizarDatosMenores(Entity.Transac.Service.Fixed.Customer request)
        {
            bool result = false;
            try
            {
                result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.ActualizarDatosMenores(request);
                }
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return result;
        }

        public bool ActualizarDatosClarify(Entity.Transac.Service.Fixed.Customer request)
        {
            bool result = false;
            try
            {
                result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.ActualizarDatosClarify(request);
                }
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return result;
        }

        public string RegisterNuevaInteraccion(EntitiesFixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionRequest request)
        {
            string result = "";
            try
            {
                result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegisterNuevaInteraccion(request);
                }
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return result;
        }

        public string RegisterNuevaInteraccionPlus(EntitiesFixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionPlusRequest request)
        {
            string result = "";
            try
            {
                result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegisterNuevaInteraccionPlus(request);
                }
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return result;
        }

        public RetentionCancelServicesResponse GetTotalInversion(RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.RetentionCancelServ.GetTotalInversion(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusResponse GetStatusAccountFixedAOC(Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusRequest objAccountStatusRequest)
        {
            var objAccountStatusResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusResponse();
            try
            {
                objAccountStatusResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusResponse>(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetStatusAccountFixedAOC(objAccountStatusRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccountStatusRequest.Audit.Session, objAccountStatusRequest.Audit.Transaction, ex.Message);

            }
            return objAccountStatusResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtResponse GetDebtQuery(Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtRequest objDebtQueryRequest)
        {
            var objDebtQueryResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtResponse();
            try
            {
                objDebtQueryResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtResponse>(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.GetDebtQuery(objDebtQueryRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDebtQueryRequest.Audit.Session, objDebtQueryRequest.Audit.Transaction, ex.Message);

            }
            return objDebtQueryResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustResponse GetRegisterInteractionAdjust(Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustRequest objRegisterInteractionAdjustRequest)
        {
            var objRegisterInteractionAdjustResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustResponse();
            try
            {
                objRegisterInteractionAdjustResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustResponse>(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegisterInteractionAdjust(objRegisterInteractionAdjustRequest.Audit.Session, objRegisterInteractionAdjustRequest.Audit.Transaction, objRegisterInteractionAdjustRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterInteractionAdjustRequest.Audit.Session, objRegisterInteractionAdjustRequest.Audit.Transaction, ex.Message);

            }
            return objRegisterInteractionAdjustResponse;
        }

        public EntitiesFixed.GetRegistarInstaDecoAdiHFC.RegistarInstaDecoAdiHFCResponse RegistarInstaDecoAdiHFC(EntitiesFixed.GetRegistarInstaDecoAdiHFC.RegistarInstaDecoAdiHFCRequest pRegistarInstaDecoAdiHFCRequest)
        {
            var registarInstaDecoAdiHFCResponse = new EntitiesFixed.GetRegistarInstaDecoAdiHFC.RegistarInstaDecoAdiHFCResponse();
            try
            {
                registarInstaDecoAdiHFCResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegistarInstaDecoAdiHFC(pRegistarInstaDecoAdiHFCRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(pRegistarInstaDecoAdiHFCRequest.Audit.Session, pRegistarInstaDecoAdiHFCRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return registarInstaDecoAdiHFCResponse;
        }

        public Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse RegisterActiDesaBonoDescHFC(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request Request)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescHFCResponse = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse();
            try
            {
                registrarDescHFCResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.RegisterActiDesaBonoDescHFC(Request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return registrarDescHFCResponse;
        }

        public Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTE(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request Request)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTEResponse = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse();
            try
            {
                registrarDescLTEResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.RetentionCancelServ.registrarDescLTE(Request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return registrarDescLTEResponse;
        }

        #endregion


        public EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse SearchStateLineEmail(EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailRequest objRequest)
        {
            EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse objResponse = new EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.SearchStateLineEmail(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse UpdateStateLineEmail(EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailRequest objRequest)
        {
            EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse objResponse = new EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.UpdateStateLineEmail(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }


        public EntitiesFixed.GetValidateLine.ValidateLineResponse GetListValidateLine(EntitiesFixed.GetValidateLine.ValidateLineRequest request)
        {
            EntitiesFixed.GetValidateLine.ValidateLineResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetListValidateLine(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        #region OnBase
        public Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaResponse TargetDocumentoOnBase(Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaRequest objRequest)
        {
            Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaResponse objResponse = null;
            try
            {
                objResponse = Business.Transac.Service.Common.TargetDocumentoOnBase(objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objResponse;
        }

        #endregion

        public Boolean UploadSftp(Tools.Entity.AuditRequest objAuditRequest, Entity.Transac.Service.Common.ConnectionSFTP objConnectionSFTP, string fileName, byte[] objFile)
        {
            Boolean objResponse = false;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                     () => Business.Transac.Service.Common.UploadSftp(objAuditRequest, objConnectionSFTP, fileName, objFile)
                );
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public bool GetInsertNoCliente(EntitiesFixed.Customer request)
        {
            try
            {
                return Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.Fixed.GetInsertNoCliente(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                return false;
            }
        }

        //PROY140315 - Inicio
        public EntitiesFixed.GetServicesLte.ServicesLteResponse GetCustomerEquipmentsDTH(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            EntitiesFixed.GetServicesLte.ServicesLteResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ChangeEquipment.GetCustomerEquipmentsDTH(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypeDTH(
           EntitiesFixed.GetJobTypes.JobTypesRequest objRequest
            )
        {
            EntitiesFixed.GetJobTypes.JobTypesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ExternalInternalTransfer.GetJobTypeDTH(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipmentDTH(EntitiesFixed.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Fixed.ChangeEquipment.GetExecuteChangeEquipmentDTH(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        //PROY140315 - Fin

        public EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequest objRequest)
        {
            EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse objResponse = new EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultDiscardRTI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
		
        public EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardGrupoRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequestGrupo objRequest)
        {
            EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse objResponse = new EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultDiscardGrupoRTI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBe(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequest objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse objResponse = new EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultDiscardRTIToBe(objRequest, audirRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
		
        public EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBeGrupo(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequestGrupo objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse objResponse = new EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultDiscardRTIToBeGrupo(objRequest, audirRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

		  public EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse ConsultClientSN(EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultClientSN(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse ConsultSN(EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ConsultSN(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse ProvisionSubscription(EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse objResponse = new EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ProvisionSubscription(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse CancelSubscriptionSN(EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.CancelSubscriptionSN(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
        public EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse UpdateClientSN(EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.UpdateClientSN(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse HistoryDevice(EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse objResponse = new EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.HistoryDevice(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
        public EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse RegisterClientSN(EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.RegisterClientSN(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(EntitiesFixed.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest)
        {
            EntitiesFixed.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = null;

            try
            {
                objContractedBusinessServicesResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.GetContractServices(objContractedBusinessServicesRequest);
                    });

               
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContractedBusinessServicesResponse;
        }

        public EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse RegistrarControles(EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse objResponse = new EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.RegistrarControles(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse ValidateElegibility(EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse objResponse = new EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.ValidateElegibility(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
      
        public EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse PersonalizaMensajeOTT(EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse objResponse = new EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse();
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.Transac.Service.Fixed.Fixed.PersonalizaMensajeOTT(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public EntitiesFixed.getConsultaLineaCuenta.ConsultaLineaResponse ConsultarLineaCuenta(EntitiesFixed.getConsultaLineaCuenta.ConsultaLineaRequest ConsultaLineaRequest)
        {
            EntitiesFixed.getConsultaLineaCuenta.ConsultaLineaResponse objSearchCustomerResponse =  null;
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<ConsultaLineaResponse>(() => { return Business.Transac.Service.Fixed.Fixed.ConsultarLineaCuenta(ConsultaLineaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ConsultaLineaRequest.Audit.Session, ConsultaLineaRequest.Audit.Transaction, ex.Message);

            }
            return objSearchCustomerResponse;
        }
        #region PROY-140510 - AMCO - Modulo de consulta y eliminar cuenta de Claro Video
        public EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse DeleteClientSN(EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.DeleteClientSN(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }

        public POSTPREDATA.consultarDatosLineaResponse GetConsultaDatosLinea(Tools.Entity.AuditRequest objaudit, string strTelephone)
            
        {
            POSTPREDATA.consultarDatosLineaResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarDatosLineaResponse>(() => 
                {
                    return Business.Transac.Service.Fixed.Fixed.GetConsultaDatosLinea(objaudit, strTelephone);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objaudit.Session, objaudit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
            
        }

        public EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse ConsultarContrato(string strIdSession, string strIdTransaction, EntitiesFixed.GetTypeProductDat.GetTypeProductDatRequest objRequest)
        {
            EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse>(strIdSession, strIdTransaction, () =>
                {
                    return Business.Transac.Service.Fixed.Fixed.ConsultarContrato(strIdSession, strIdTransaction, objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }
            return objResponse;
        }
        #endregion

        //INICIATIVA-794
        public Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse ConsultarServicioIPTV(Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV.ConsultIPTVRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse>(() => { return Business.Transac.Service.Fixed.Fixed.ConsultarServicioIPTV(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse ValidarServicioIPTV(Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV.ValidateIPTVRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse>(() => { return Business.Transac.Service.Fixed.Fixed.ValidarServicioIPTV(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        //INI: INICIATIVA-871
        public DatosSIMPrepago ObtenerDatosSIMPrepago(string strIdSession, string strTransactionID, string strApplicationUser, string strPhoneNumber)
        {
            DatosSIMPrepago objResponse = new DatosSIMPrepago();

            objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.ObtenerDatosSIMPrepago(strIdSession, strTransactionID, strApplicationUser, strPhoneNumber); });

            return objResponse;
        }
        //FIN: INICIATIVA-871

        #region "NetflixServices ServicesMethods"
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia HFC.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intContrato"></param>
        /// <param name="strCodigoServicio"></param>
        /// <returns></returns>
        public bool validarAccesoRegistroLinkHFC(string strIdSession, string strIdTransaccion, int intContrato, string strCodigoServicio)
        {
            bool booAccedeLink = false;
            booAccedeLink = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strIdTransaccion, () =>
            {
                return Business.Transac.Service.Fixed.NetflixServices.validarAccesoRegistroLinkHFC(strIdSession, strIdTransaccion, intContrato, strCodigoServicio);
            });
            return booAccedeLink;
        }
        /// <summary>
        /// Permite realizar el envio de link para la suscripción al servicio Netflix.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        public ServicesNXResponse envioNotificacionRegistroNX(string strIdSession, string strIdTransaccion, ServicesNXRequest oRequest, AuditRequest oAuditRequest)
        {
            ServicesNXResponse oResponse = null;
            try
            {
                oResponse = new ServicesNXResponse();
                oResponse = Claro.Web.Logging.ExecuteMethod<ServicesNXResponse>(strIdSession, strIdTransaccion, () =>
                {
                    return Business.Transac.Service.Fixed.NetflixServices.envioNotificacionRegistroNX(strIdSession, strIdTransaccion, oRequest, oAuditRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return oResponse;
        }
        #endregion

        //INI: INICIATIVA-986
        public Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response ActivarDesactivarContinueWS(string strIdSession, string strTransaccion, AplicarRetirarContingencia objRequestContinue)
        {
            var objResponse = new Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response();

            objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response>(strIdSession, strTransaccion, () =>
            {
                return Business.Transac.Service.Fixed.Fixed.ActivarDesactivarContinueWS(strIdSession, strTransaccion, objRequestContinue);
            });

            return objResponse;
        }

        public MessageResponseRegistrarProcesoContinue RegistrarActualizarContingencia(MessageRequestRegistrarProcesoContinue objRequest, Tools.Entity.AuditRequest objAuditRequest)
        {
            MessageResponseRegistrarProcesoContinue objResponse = new MessageResponseRegistrarProcesoContinue();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Fixed.Fixed.RegistrarActualizarContingencia(objRequest, objAuditRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objResponse;
        }
        //FIN: INICIATIVA-986
    }
}

