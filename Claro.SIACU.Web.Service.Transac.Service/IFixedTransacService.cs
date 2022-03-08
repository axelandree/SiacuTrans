using Claro.SIACU.Entity.Transac.Service.Fixed;
using System.ServiceModel;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConsultClienteHFCWS = Claro.SIACU.ProxyService.Transac.Service.WSClienteHFC;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateOCC;
using System.Collections.Generic;
using EntityCommon = Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetConsultationServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailInputFixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH;
using POSTPREDATA = Claro.SIACU.ProxyService.Transac.Service.SIAC.Post.DatosPrePost_V2;
using System;

using Claro.SIACU.Entity.Transac.Service.Fixed.Discard;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard.ProcesarContinue;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFixedTransacService" in both code and config file together.
    [ServiceContract]
    public interface IFixedTransacService
    {
        [OperationContract]
        EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request);
        

        [OperationContract]
        EntitiesFixed.GetCommercialServices.CommercialServicesResponse GetCommercialService(EntitiesFixed.GetCommercialServices.CommercialServicesRequest objCommercialServicesRequest);

        [OperationContract]
        EntitiesFixed.GetCommertialPlan.CommertialPlanResponse GetCommertialPlan(EntitiesFixed.GetCommertialPlan.CommertialPlanRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProductTracDeacServ(EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCamapaign.CamapaignResponse GetCamapaign(EntitiesFixed.GetCamapaign.CamapaignRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(EntitiesFixed.GetInsertInteractHFC.GetInsertInteractHFCRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServices(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateResponse GetInfoInteractionTemplate(EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedResponse GetInsertInteractionMixed(EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCustomerPhone.CustomerPhoneResponse GetCustomerPhone(EntitiesFixed.GetCustomerPhone.CustomerPhoneRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCallDetailDB1.CallDetailDB1Response GetCallDetailDB1(EntitiesFixed.GetCallDetailDB1.CallDetailDB1Request objRequest);

        [OperationContract]
        EntitiesFixed.GetCallDetail.CallDetailResponse GetCallDetail(EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetFacturePDI.FacturePDIResponse GetFacturePDI(EntitiesFixed.GetFacturePDI.FacturePDIRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetFactureDBTO.FactureDBTOResponse GetFactureDBTO(EntitiesFixed.GetFactureDBTO.FactureDBTORequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCallDetail.CallDetailResponse GetBilledCallsDetailDB1_BSCS(EntitiesFixed.GetCallDetail.CallDetailRequest objRequest);

        #region JCAA
                [OperationContract]
        Entity.Transac.Service.Fixed.GetPlans.PlansResponse LteGetPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest);
        [OperationContract]
                Entity.Transac.Service.Fixed.GetPlans.PlansResponse HfcGetPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse LteGetServicesByPlan(Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse HfcGetServicesByPlan(Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetCarrierList.CarrierResponse GetCarrierList(Entity.Transac.Service.Fixed.GetCarrierList.CarrierRequest objCarriersRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse GetServicesByInteraction(Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceRequest objInteractionServiceRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesResponse GetTransactionRuleList(Entity.Transac.Service.Fixed.GetTransactionRuleList.TransactionRulesRequest objTransactionRulesRequest)
;
        [OperationContract]
        Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetOrderType.OrderTypesResponse GetOrderType(Entity.Transac.Service.Fixed.GetOrderType.OrderTypesRequest objOrderTypesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityResponse GetGroupCapacity(Entity.Transac.Service.Fixed.GetGroupCapacity.ETAAuditoriaCapacityRequest objETAAuditoriaCapacityRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasResponse GetTimeZones(Entity.Transac.Service.Fixed.GetTimeZones.FranjasHorariasRequest objFranjasHorariasRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse ExecutePlanMigrationLte(Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanRequest objMigratedPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanResponse ExecutePlanMigrationHfc(
            Entity.Transac.Service.Fixed.ExecutePlanMigrationLte.MigratedPlanRequest objMigratedPlansRequest);
        [OperationContract ]
        Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanResponse GetCurrentPlan(Entity.Transac.Service.Fixed.GetCurrentPlan.CurrentPlanRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(Entity.Transac.Service.Fixed.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesResponse SendNewPlanServices(Entity.Transac.Service.Fixed.SendNewPlanServices.NewPlanServicesRequest objNewPlanServicesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanResponse GetEquipmentByCurrentPlan(Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan.EquipmentsByCurrentPlanRequest objEquipmentsByCurrentPlanRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultResponse GetTechnicalVisitResult(Entity.Transac.Service.Fixed.GetTechnicalVisitResult.TechnicalVisitResultRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Common.GetHubs.GetHubsResponse GetHubsHfc(Entity.Transac.Service.Common.GetHubs.GetHubsRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Common.GetHubs.GetHubsResponse GetHubsLte(Entity.Transac.Service.Common.GetHubs.GetHubsRequest objPlansRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse ExecutePlanMigrationLTE(Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTERequest objRequest);
        #endregion

        #region VisorOrder Fase03
        [OperationContract]
        Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypesVisitOrder(Entity.Transac.Service.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetGenericSot.GenericSotResponse GetGenericSOT(Entity.Transac.Service.Fixed.GetGenericSot.GenericSotRequest objGenericSotRequest);
        [OperationContract]
        EntitiesFixed.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(EntitiesFixed.GetUpdateInter29.UpdateInter29Request objRequest);
        #endregion

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetListarAccionesRC(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotCancelacion(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSubMotiveCancel(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetTypeWork(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSubTypeWork(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotiveSOT(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.AddDayWorkResponse GetAddDayWork(EntitiesFixed.GetCaseInsert.AddDayWorkRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetSoloTFIPostpago(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainPenalidadExt(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse ObtenerDatosBSCSExt(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(Customer objRequest);

        [OperationContract]
        string GetRegisterEtaSelection(Entity.Transac.Service.Fixed.GetRegisterEtaSelection.RegisterEtaSelectionRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCaseInsert(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetApadeceCancelRet(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        bool GetDesactivatedContract(Customer objRequestCliente);

        [OperationContract]
        EntitiesFixed.GetService.ServiceResponse GetTelephoneByContractCode(EntitiesFixed.GetService.ServiceRequest objRequest);
        #region external/Internal Transfer
        [OperationContract]
        EntitiesFixed.GetRecordTransExtInt.RecordTranferExtIntResponse GetRecordTransaction(EntitiesFixed.GetRecordTransExtInt.RecordTranferExtIntRequest objGetRecordTransactionRequest);


        [OperationContract]
        EntitiesFixed.GetPhoneNumber.PhoneNumberResponse GetExecuteChangeNumber(EntitiesFixed.GetPhoneNumber.PhoneNumberRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetGenerateSOT.GenerateSOTResponse GetGenerateSOT(EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest);
        #endregion

        #region Puntos Adicionales
        [OperationContract]
        EntitiesFixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity GetETAAuditoriaRequestCapacity(EntitiesFixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity objBEETAAuditoriaRequestCapacity);
        [OperationContract]
        int registraEtaRequest(EntitiesFixed.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest);
        [OperationContract]
        string registraEtaResponse(EntitiesFixed.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse);
        [OperationContract]
        EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse REGISTRA_COSTO_PA(EntitiesFixed.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest);
        [OperationContract]
        EntitiesFixed.GetDetailTransExtra.DetailTransExtraResponse ACTUALIZAR_COSTO_PA(EntitiesFixed.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest);
        #endregion

        #region "Inst/Desinst Decodificadores"
        [OperationContract]
        Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse GetProductDetail(Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailRequest objProductDetailRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetAddtionalEquipment.AddtionalEquipmentResponse GetAddtionalEquipment(Entity.Transac.Service.Fixed.GetAddtionalEquipment.AddtionalEquipmentRequest objAddtionalEquipmentRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetProcessingServices.ProcessingServicesResponse GetProcessingServices(Entity.Transac.Service.Fixed.GetProcessingServices.ProcessingServicesRequest objProcessingServicesRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetDecoDetailByIdService.DecoDetailByIdServiceResponse GetDecoDetailByIdService(Entity.Transac.Service.Fixed.GetDecoDetailByIdService.DecoDetailByIdServiceRequest objDecoDetailByIdServiceRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetInsertDecoAdditional.InsertDecoAdditionalResponse GetInsertDecoAdditional(Entity.Transac.Service.Fixed.GetInsertDecoAdditional.InsertDecoAdditionalRequest objInsertDecoAdditionalRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse GetInsertDetailServiceInteraction(Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest objInsertDetailServiceInteractionRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetInsertETASelection.InsertETASelectionResponse GetInsertETASelection(Entity.Transac.Service.Fixed.GetInsertETASelection.InsertETASelectionRequest objInsertETASelectionRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(Entity.Transac.Service.Fixed.GetInsertTransaction.InsertTransactionRequest objInsertTransactionRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailResponse GetProductDown(Entity.Transac.Service.Fixed.GetProductDetail.ProductDetailRequest objProductDetailRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmount(Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest);
        [OperationContract]
        Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse GetServicesByPlanLTE(Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest);
        
        #endregion

        [OperationContract]
        EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse GetInsertInteractionBusiness(EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest);
        [OperationContract]
        GenerateOCCResponse GenerateOCC(GenerateOCCRequest objRequest);
          
        [OperationContract]
        EntitiesFixed.GetDecoServices.BEDecoServicesResponse GetServicesDTH(EntitiesFixed.GetDecoServices.BEDecoServicesRequest objRequest);

        
        [OperationContract]
        EntitiesFixed.GetListTownCenter.ListTownCenterResponse GetListTownCenter(EntitiesFixed.GetListTownCenter.ListTownCenterRequest objRequest);
        
        
        [OperationContract]
        EntitiesFixed.GetIdUbigeo.IdUbigeoResponse GetIdUbigeo(EntitiesFixed.GetIdUbigeo.IdUbigeoRequest objRequest);
    

        [OperationContract]
        EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServicesLte(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest);
        
       
        [OperationContract]
        EntitiesFixed.GetListPlans.ListPlansResponse GetListPlans(EntitiesFixed.GetListPlans.ListPlansRequest objRequest);

        
        [OperationContract]
        EntitiesFixed.GetListEbuildings.ListEbuildingsResponse GetListEBuildings(EntitiesFixed.GetListEbuildings.ListEbuildingsRequest objRequest);

       
        [OperationContract]
        EntitiesFixed.GetCoverage.CoverageResponse GetCoverage(EntitiesFixed.GetCoverage.CoverageRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetAddressUpdate.AddressUpdateResponse GetUpdateAddress(EntitiesFixed.GetAddressUpdate.AddressUpdateRequest objRequest);
  
        #region ConfigurationIP
        [OperationContract]
        List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> GetJobTypesConfigIP(EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPRequest objJobTypesRequest);
        [OperationContract]
        List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse> GetTypeConfIP(EntitiesFixed.GetTypeConfip.TypeConfigIpRequest objTypeConfigIpRequest);
        [OperationContract]
        List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse> GetBranchCustomer(EntitiesFixed.GetBranchCustomer.BranchCustomerResquest objBranchCustomerResquest);
        [OperationContract]
        EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse ConfigurationServicesSave(EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest oConfigurationIPRequest);
        [OperationContract]
        EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse GetConfigurationIPMegas(EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest oConfigurationIPMegasRequest);

        #endregion

        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest);
        #region external/internal LTE

        [OperationContract]
        EntitiesFixed.GetMotiveSoft.MotiveSoftResponse GetMotiveSoftLte(EntitiesFixed.GetMotiveSoft.MotiveSoftRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypeLte(EntitiesFixed.GetJobTypes.JobTypesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetServicesLte.ServicesLteResponse GetServicesLte(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse RegisterTransactionLTE(EntitiesFixed.GetRegisterTransaction.RegisterTransactionRequest objResquest);
        #endregion

        [OperationContract]
        EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse GetActivationDesactivation(
            EntitiesFixed.GetActivationDesactivation.ActivationDesactivationRequest objRequest);

        [OperationContract]
        Interaction GetCreateCase(Interaction oRequest);
        [OperationContract]
        Interaction GetInsertCase(Interaction oItem);
        [OperationContract]
        CaseTemplate GetInsertTemplateCase(CaseTemplate oItem);
        [OperationContract]
        CaseTemplate GetInsertTemplateCaseContingent(CaseTemplate oItem);
        [OperationContract]
        CaseTemplate ActualizaPlantillaCaso(CaseTemplate oItem);
        [OperationContract]
        EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse GetBilledCallsDetailHfC(EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest);

        [OperationContract]
        ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest);

        //[OperationContract]
        //QueryAssociatedLinesResponse GetCallDetailInputFixed(Entity.Transac.Service.Fixed.GetBpelCallDetail.BpelCallDetailRequest objRequest);

        [OperationContract]
        CallDetailInputFixedResponse GetCallDetailInputFixed(Entity.Transac.Service.Fixed.GetBpelCallDetail.BpelCallDetailRequest objRequest);


        [OperationContract]
        EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse GetValidateActDesService(
            EntitiesFixed.GetValidateActDesService.ValidateActDesServiceRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest);

        [OperationContract]
        ServiceDTHResponse GetServiceDTH(ServiceDTHRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetPlanCommercial.PlanCommercialResponse GetPlanCommercial(EntitiesFixed.GetPlanCommercial.PlanCommercialRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceActivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest);

        [OperationContract]
        EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceDesactivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest);
        [OperationContract]
        EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProdIdTraDesacServ(EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest);

        [OperationContract]
        EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse DeleteProgramTask(EntitiesFixed.DeleteProgramTask.DeleteProgramTaskRequest request);

        [OperationContract]
        bool GetDesactivatedContract_LTE(Customer objRequest);

        [OperationContract]
        EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse DeleteProgramTaskHfc(EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcRequest request);

        [OperationContract]
        EntitiesFixed.GetSaveOCC.SaveOCCResponse GetSaveOCC(EntitiesFixed.GetSaveOCC.SaveOCCRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse GetInsertLoyalty(EntitiesFixed.GetInsertLoyalty.InsertLoyaltyRequest objRequest);

        [OperationContract]
        EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest);

        [OperationContract]
        ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest);

        [OperationContract]
        EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse LTERegisterTransaction(EntitiesFixed.GetRegisterTransaction.RegisterTransactionRequest objRequest);

        [OperationContract]
        List<EntitiesFixed.GetDataLine.DataLineResponse> GetDataLine(EntitiesFixed.GetDataLine.DataLineRequest oBE);

        [OperationContract]
        EntitiesFixed.GetServicesLte.ServicesLteResponse GetCustomerEquipments(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCamapaign.CamapaignResponse GetCampaign(EntitiesFixed.GetCamapaign.CamapaignRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetPlans.PlansResponse GetNewPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objPlansRequest);

 [OperationContract]
        EntitiesFixed.GetDispEquipment.DispEquipmentResponse GetValidateEquipment(EntitiesFixed.GetDispEquipment.DispEquipmentRequest objDispEquipmentRequest);

        [OperationContract]
        EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardSANS(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest);

        [OperationContract]
        EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardBSCS(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest);

        [OperationContract]
        EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetValidateSimcardBSCS_HLCODE(EntitiesFixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objAvailabilitySimcardRequest);

        [OperationContract] 
        EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipment(EntitiesFixed.GetChangeEquipment.ChangeEquipmentRequest objRequest);

 [OperationContract]
        Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse GetDecoMatriz(
            Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizRequest objDecoMatrizRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse GetDecoType(
            Entity.Transac.Service.Fixed.GetDecoType.DecoTypeRequest objDecoTypeRequest);

 [OperationContract]
        Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteResponse ValidateDepVelLTE(Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.PostExecuteDecosLte.DecosLteResponse PostExecuteDecosLte(EntitiesFixed.PostExecuteDecosLte.DecosLteRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmountLte(
            Entity.Transac.Service.Fixed.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest);
        
        [OperationContract]
        Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubTypeWork(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.OrderSubType GetValidationSubTypeWork(EntitiesFixed.GetOrderSubType.OrderSubTypesRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse GetDataServById(EntitiesFixed.GetDataServ.DataServByIdRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse SavePostventaDetServ(EntitiesFixed.GetSavePostventa.DataSavePostventaDetServRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse GetDetEquipo_LTE(EntitiesFixed.GetDetEquipmentLTE.DataEquipmentRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetGenerateSOT.GenerateSOTResponse registraEtaSot(EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest);

        [OperationContract]
        EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetGenerateSOT.GenerateSOTResponse UpdateEta( EntitiesFixed.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest);

        [OperationContract]
        EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarOrdenesToa(EntitiesFixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta);

        [OperationContract]
        EntitiesFixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarCancelaToa(EntitiesFixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta);

        [OperationContract]
        EntitiesFixed.GetHistoryToa.HistoryToaResponse GetHistoryToa(EntitiesFixed.GetHistoryToa.HistoryToaRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetHistoryToa.HistoryToaResponse GetUpdateHistoryToa(EntitiesFixed.GetHistoryToa.HistoryToaRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidateReservation(Entity.Transac.Service.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest);

#region Proy-32650
        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMonths(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetPlanServices.PlanServicesResponse GetRetentionRate(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetCurrentDiscountFixedCharge(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse InsertDiscountBondExchangePlan(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetListDiscount(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        bool RegisterBonoDiscount(EntitiesFixed.RegisterBonoDiscount.RegisterBonoDiscountRequest request);

        [OperationContract]
        bool ActualizarDatosMenores(Entity.Transac.Service.Fixed.Customer request);

        [OperationContract]
        bool ActualizarDatosClarify(Entity.Transac.Service.Fixed.Customer request);

        [OperationContract]
        EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetTotalInversion(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest);

        [OperationContract]
        string RegisterNuevaInteraccion(EntitiesFixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionRequest request);

        [OperationContract]
        string RegisterNuevaInteraccionPlus(EntitiesFixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionPlusRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusResponse GetStatusAccountFixedAOC(Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus.AccountStatusRequest objAccountStatusRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtResponse GetDebtQuery(Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt.QueryDebtRequest objDebtQueryRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustResponse GetRegisterInteractionAdjust(Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustRequest objRegisterInteractionAdjustRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC.RegistarInstaDecoAdiHFCResponse RegistarInstaDecoAdiHFC(Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC.RegistarInstaDecoAdiHFCRequest pRegistarInstaDecoAdiHFCRequest);

        [OperationContract]
        Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse RegisterActiDesaBonoDescHFC(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request request);


        [OperationContract]
        Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTE(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request request);
        #endregion

        [OperationContract]
        EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse SearchStateLineEmail(EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailRequest objRequest);

        [OperationContract]
        EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse UpdateStateLineEmail(EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailRequest objRequest);
               

        [OperationContract]
        EntitiesFixed.GetValidateLine.ValidateLineResponse GetListValidateLine(EntitiesFixed.GetValidateLine.ValidateLineRequest request);


        #region OnBase

        [OperationContract]
        Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaResponse TargetDocumentoOnBase(Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaRequest objRequest);

        #endregion

        [OperationContract]
        Boolean UploadSftp(Tools.Entity.AuditRequest objAuditRequest, Entity.Transac.Service.Common.ConnectionSFTP objConnectionSFTP, string fileName, byte[] objFile);

        [OperationContract]
        bool GetInsertNoCliente(EntitiesFixed.Customer request);

        //PROY140315 - Inicio
        [OperationContract]
        EntitiesFixed.GetServicesLte.ServicesLteResponse GetCustomerEquipmentsDTH(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypeDTH(EntitiesFixed.GetJobTypes.JobTypesRequest objRequest);

        [OperationContract]
        EntitiesFixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipmentDTH(EntitiesFixed.GetChangeEquipment.ChangeEquipmentRequest objRequest);
        //PROY140315 - Fin

        [OperationContract]
        EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequest objRequest);

        [OperationContract]
        EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardGrupoRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequestGrupo objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse ConsultClientSN(EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse ConsultSN(EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse ProvisionSubscription(EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse CancelSubscriptionSN(EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse UpdateClientSN(EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse HistoryDevice(EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceRequest objRequest);
       
	    [OperationContract]
        EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse RegisterClientSN(EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(EntitiesFixed.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest);
        [OperationContract]
        EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse RegistrarControles(EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse ValidateElegibility(EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityRequest objRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse PersonalizaMensajeOTT(EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTRequest objRequest);
        
        [OperationContract]
        EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBe(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequest objRequest, Tools.Entity.AuditRequest audirRequest);
        
        [OperationContract]
        EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBeGrupo(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequestGrupo objRequest, Tools.Entity.AuditRequest audirRequest);
        
        [OperationContract]
        EntitiesFixed.getConsultaLineaCuenta.ConsultaLineaResponse ConsultarLineaCuenta(EntitiesFixed.getConsultaLineaCuenta.ConsultaLineaRequest ConsultaLineaRequest);

        [OperationContract]
        EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse DeleteClientSN(EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNRequest objRequest);
        
        [OperationContract]
        POSTPREDATA.consultarDatosLineaResponse GetConsultaDatosLinea(Tools.Entity.AuditRequest objaudit, string strTelephone);
       
        [OperationContract]
        EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse ConsultarContrato(string strIdSession, string strIdTransaction, EntitiesFixed.GetTypeProductDat.GetTypeProductDatRequest objRequest);

        //INICIATIVA-794
        [OperationContract]
        EntitiesFixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse ConsultarServicioIPTV(Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV.ConsultIPTVRequest objRequest);
        [OperationContract]
        EntitiesFixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse ValidarServicioIPTV(Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV.ValidateIPTVRequest objRequest);

        //INI: INICIATIVA-871
        [OperationContract]
        EntitiesFixed.Discard.DatosSIMPrepago ObtenerDatosSIMPrepago(string strIdSession, string strTransactionID, string strApplicationUser, string strPhoneNumber);
        //FIN: INICIATIVA-871 

        //INI: INICIATIVA-986 
        [OperationContract]
        Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response ActivarDesactivarContinueWS(string strIdSession, string strTransaccion, AplicarRetirarContingencia objRequestContinue);

        [OperationContract]
        MessageResponseRegistrarProcesoContinue RegistrarActualizarContingencia(MessageRequestRegistrarProcesoContinue objRequest, Tools.Entity.AuditRequest objAuditRequest);
        //FIN: INICIATIVA-986 
    }

}
