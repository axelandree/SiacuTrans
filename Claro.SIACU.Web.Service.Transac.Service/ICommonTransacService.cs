using Claro.SIACU.Entity.Transac.Service.Common;
using System.Collections.Generic;
using System.ServiceModel;
using Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial;
using Claro.SIACU.Entity.Transac.Service.Common.GetUpdateInter30;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Common.GetQuestionsAnswerSecurity;
using System;
using Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser;
using Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    [ServiceContract]
    public interface ICommonTransacService
    {
        
        [OperationContract]
        Entity.Transac.Service.Common.GetMotiveSot.MotiveSotResponse GetMotiveSot(Entity.Transac.Service.Common.GetMotiveSot.MotiveSotRequest objMotiveSotRequest);
        [OperationContract]
        bool ValidateSchedule(Claro.SIACU.Entity.Transac.Service.Common.GetSchedule.ScheduleRequest objScheduleRequest);

      

        [OperationContract]
        COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request);
        
        [OperationContract]
        COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request);
        [OperationContract]
        COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse GetListClientDocumentType(COMMON.GetListClientDocumentType.ListClientDocumentTypeRequest request);
        [OperationContract]
        COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request);
        [OperationContract]
        COMMON.GetUpdateNotes.UpdateNotesResponse UpdateNotes(COMMON.GetUpdateNotes.UpdateNotesRequest request);
    
        

        [OperationContract]
        Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesResponse GetBusinessRules(Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest);

        [OperationContract]
        Entity.Transac.Service.Common.GetRegion.RegionResponse GetRegions(Entity.Transac.Service.Common.GetRegion.RegionRequest objRegionRequest);

        [OperationContract]
        Entity.Transac.Service.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.Transac.Service.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest);
        
        [OperationContract]
        Entity.Transac.Service.Common.GetValueXml.ValueXmlResponse GetValueXml(Entity.Transac.Service.Common.GetValueXml.ValueXmlRequest objRequest);

        [OperationContract]
        Entity.Transac.Service.Common.GetListItem.ListItemResponse GetListValueXML(Entity.Transac.Service.Common.GetListItem.ListItemRequest objListItemRequest);

        

        [OperationContract]
        Entity.Transac.Service.Common.GetTypification.TypificationResponse GetTypification(Entity.Transac.Service.Common.GetTypification.TypificationRequest objTypificationRequest);

        [OperationContract]
        Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse GetReceipts(Entity.Transac.Service.Common.GetReceipts.ReceiptsRequest objRequest);

       

        #region interacciones 

        //obtener  interacciones de cliente
        [OperationContract]
        COMMON.GetInteractionClient.InteractionClientResponse GetInteractionClient(COMMON.GetInteractionClient.InteractionClientRequest objInteractionRequest);
      
        //registrar sub detalle de  interacciones del clliente
        [OperationContract]
        COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse InsertRecordSubClaseDetail(COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailRequest objrequest);
       
        
        //insertar interaccion
        //la funcion orginal es insertar
        [OperationContract]
        COMMON.GetInsertInt.InsertIntResponse InsertInt(COMMON.GetInsertInt.InsertIntRequest objrequest);

        //obterner cliente
        //la funcion original se llama obtenerCliente
        [OperationContract]
        COMMON.GetClient.ClientResponse GetObClient(COMMON.GetClient.ClientRequest objrequest);

        //registrar plantilla de interaccion
        [OperationContract]
        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest objresquest);

        //insertar plantilla de interaccion
        //similar ala de arriba ya que sus nonbres origianles son iguales 
        //usa difrentes variables y sp
        [OperationContract]
        COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest objrequest);

        //insertar interaction
        [OperationContract]
        COMMON.GetInsertInteract.InsertInteractResponse InsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objrequest);

        //insertarinteraccionenegocio2
        [OperationContract]
        COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetInsertBusinnesInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request obrequest);
        

        //insertainteraccionegocios
        //insertar general
        [OperationContract]
        COMMON.GetInsertGeneral.InsertGeneralResponse GetinsertInteractionGeneral(COMMON.GetInsertGeneral.InsertGeneralRequest request);
       
        //insertarplantillainteraccionnegocio
        //insertar plantilla general
        [OperationContract]
        COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetinsertInteractionTemplateGeneral(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest request);
          
        //insertarDetail
        //insertarDEtalle interaccion
        [OperationContract]
        COMMON.GetInsertDetail.InsertDetailResponse GetInsertDetail(COMMON.GetInsertDetail.InsertDetailRequest request);
        #endregion

        [OperationContract]
        COMMON.GetUpdateXInter29.UpdateXInter29Response UpdateXInter29(COMMON.GetUpdateXInter29.UpdateXInter29Request objRequest);

        
        [OperationContract]
        COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatus(COMMON.GetCivilStatus.CivilStatusRequest objRequest);

        [OperationContract]
        COMMON.GetOccupationClient.OccupationClientResponse GetOccupationClient(COMMON.GetOccupationClient.OccupationClientRequest objRequest);

        [OperationContract]
        COMMON.GetReasonRegistry.ReasonRegistryResponse GetReasonRegistry(COMMON.GetReasonRegistry.ReasonRegistryRequest objRequest);

        [OperationContract]
        COMMON.GetBrand.BrandResponse GetBrandList(COMMON.GetBrand.BrandRequest objRequest);

        [OperationContract]
        COMMON.GetConsultDepartment.ConsultDepartmentResponse GetConsultDepartment(COMMON.GetConsultDepartment.ConsultDepartmentRequest objRequest);

    

        [OperationContract]
        COMMON.GetBrandModel.BrandModelResponse GetBrandModel(COMMON.GetBrandModel.BrandModelRequest objRequest);

        [OperationContract]
        COMMON.GetConsultProvince.ConsultProvinceResponse GetConsultProvince(COMMON.GetConsultProvince.ConsultProvinceRequest objRequest);

        [OperationContract]
        COMMON.GetConsultDistrict.ConsultDistrictResponse GetConsultDistrict(COMMON.GetConsultDistrict.ConsultDistrictRequest objRequest);

        [OperationContract]
        COMMON.GetConsultNationality.ConsultNationalityResponse GetConsultNationality(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest);

        [OperationContract]
        COMMON.GetServicesVAS.ServicesVASResponse GetServicesVAS(COMMON.GetServicesVAS.ServicesVASRequest objRequest);

        [OperationContract]
        COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements(COMMON.GetMigratedElements.MigratedElementsRequest objRequest);

        [OperationContract]
        COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements2(COMMON.GetMigratedElements.MigratedElementsRequest objRequest);

        

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditResponse SaveAudit(Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditRequest objGrabarAuditReq);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMRequest objRegAuditReq);


        #region JCAA
        [OperationContract]
        COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest objPlansRequest);
        //[OperationContract]
        //COMMON.GetClient.ClientResponse GetClient(COMMON.GetClient.ClientRequest objRequest);

        #endregion
      
     
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC.InsertInteractHFCRequest objRegAuditReq);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteract.InsertInteractResponse GetInsertInteract(Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteract.InsertInteractRequest objRegAuditReq);

        #region -funciones para enviar email 
        //nombre orginnal EnviarEmail
        //envia email , con archivo
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest objrequest);
        //nombre original EnviarEmailAttach
        //envia email con archivo adjunto copia el archivo en el directorio del servidor
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailAttach(COMMON.GetSendEmail.SendEmailRequest objrequest);

        //EnviarCorreoAlt
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest objresuqest);


        //conseguir archivo por defecto impersonal
        [OperationContract]
        COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest objrequest);
        #endregion

        [OperationContract]
        COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest);
        [OperationContract]
        COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest);

        [OperationContract]
        COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest);

        [OperationContract]
        COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest);

        [OperationContract]
        COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest);

        [OperationContract]
        COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest);

        [OperationContract]
        string GetIdTrazabilidad(string strIdSession, string strTransaction, Int32 intCodGrupo);


        #region External/Internal Transfer

        [OperationContract]
        COMMON.GetMzBloEdiType.MzBloEdiTypeResponse GetMzBloEdiTypePVU(COMMON.GetMzBloEdiType.MzBloEdiTypeRequest objMzBloEdiTypeRequest);

        [OperationContract]
        COMMON.GetTipDptInt.TipDptIntResponse GetTipDptInt(COMMON.GetTipDptInt.TipDptIntRequest objTipDptIntRequest);

        [OperationContract]
        COMMON.GetDepartmentsPVU.DepartmentsPvuResponse GetDepartmentsPVU(COMMON.GetDepartmentsPVU.DepartmentsPvuRequest objDepartmentsRequest);

        [OperationContract]
        COMMON.GetProvincesPVU.ProvincesPvuResponse GetProvincesPVU(COMMON.GetProvincesPVU.ProvincesPvuRequest objProvincesRequest);

        [OperationContract]
        COMMON.GetDistrictsPVU.DistrictsPvuResponse GetDistrictsPVU(COMMON.GetDistrictsPVU.DistrictsPvuRequest objDistrictsRequest);

        [OperationContract]
        COMMON.GetCenterPopulatPVU.CenterPopulatPvuRespose GetCenterPopulatPVU(COMMON.GetCenterPopulatPVU.CenterPopulatPvuRequest objCenterPopulatRequest);

        [OperationContract]
        COMMON.GetZoneTypeCOBS.ZoneTypeCobsResponse GetZoneTypeCOBS(COMMON.GetZoneTypeCOBS.ZoneTypeCobsRequest objZoneTypeRequest);

        [OperationContract]
        COMMON.GetBuildingsPVU.BuildingsPvuResponse GetBuildingsPVU(COMMON.GetBuildingsPVU.BuildingsPvuRequest objBuildingsRequest);

        [OperationContract]
        COMMON.GetWorkType.WorkTypeResponse GetWorkType(COMMON.GetWorkType.WorkTypeRequest objWorkTypeRequest);

        [OperationContract]
        COMMON.GetWorkSubType.WorkSubTypeResponse GetWorkSubType(COMMON.GetWorkSubType.WorkSubTypeRequest objWorkSubTypeRequest);

        [OperationContract]
        List<ListItem> GetDocumentTypeCOBS(string strIdSession, string strTransaction, string strCodCargaDdl);

        #endregion 



        #region Redirect
        [OperationContract]
        Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateCommunicationRequest);
        [OperationContract]
        Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest);
         [OperationContract]
        Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest);
        #endregion

        [OperationContract]
        COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse GetPagOptionXuserVn(COMMON.GetPagOptionXuserNV.PagOptionXuserNVRequest objRequest);

        [OperationContract]
        COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest);

        [OperationContract]
        COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objRequest);

        [OperationContract]
        COMMON.GetPenaltyChangePlans.PenaltyChangePlanResponse GetPenaltyChangePlan(COMMON.GetPenaltyChangePlans.PenaltyChangePlanRequest objRequest);

        [OperationContract]
        COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest);

        [OperationContract]
        COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest);

        [OperationContract]
        ContractByPhoneNumberResponse GetContractByPhoneNumber(ContractByPhoneNumberRequest objRequest);

        [OperationContract]
        COMMON.GetSendEmailSB.SendEmailSBResponse GetSendEmailSB(COMMON.GetSendEmailSB.SendEmailSBRequest objRequest);

        [OperationContract]
        COMMON.GetNoteInteraction.NoteInteractionResponse GetNoteInteraction(COMMON.GetNoteInteraction.NoteInteractionRequest objRequest);
        [OperationContract]
        COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse GetDynamicCaseTemplateData(COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataRequest objRequest);

        [OperationContract]
        RegisterServiceCommercialResponse GetRegisterServiceCommercial(RegisterServiceCommercialRequest objRequest);

        [OperationContract]
        UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest);

        [OperationContract]
        COMMON.GetProgramTask.ProgramTaskResponse GetProgramTask(COMMON.GetProgramTask.ProgramTaskRequest objRequest);

        [OperationContract]
        COMMON.GetTypeTransaction.TypeTransactionResponse GetTypeTransaction(COMMON.GetTypeTransaction.TypeTransactionRequest objRequest);

        [OperationContract]
        COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest);

        [OperationContract]
        COMMON.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(COMMON.GetInsertEvidence.InsertEvidenceRequest objRequest);


        #region EMAIL-FIXED
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailFixed(COMMON.GetSendEmail.SendEmailRequest objrequest);
        #endregion
        [OperationContract]
        COMMON.GetConsultImei.ConsultImeiResponse GetConsultImei(COMMON.GetConsultImei.ConsultImeiRequest objRequest);
              


        [OperationContract]
        COMMON.GetSot.GetSotResponse GetSot(COMMON.GetSot.GetSotRequest objRequest);
        [OperationContract]
        COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityResponse GetQuestionsAnswerSecurity(COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityRequest objRequest);

        [OperationContract]
        COMMON.GetConsultMarkModel.ConsultMarkModelResponse GetConsultMarkModel(COMMON.GetConsultMarkModel.ConsultMarkModelRequest objRequest);
      
        [OperationContract]
        COMMON.GetEquipmentForeign.EquipmentForeignResponse GetEquipmentForeign(COMMON.GetEquipmentForeign.EquipmentForeignRequest objRequest);

        [OperationContract]
        COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse GetInsertEquipmentForeign(COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignRequest objrequest);
       
        [OperationContract]
        COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse GetListEquipmentRegistered(COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredRequest objRequest);

        [OperationContract]
        COMMON.GetConsultByGroupParameter.ConsultByGroupParameterResponse GetConsultByGroupParameter(COMMON.GetConsultByGroupParameter.ConsultByGroupParameterRequest objRequest);

        [OperationContract]
        COMMON.GetInteraction.InteractionResponse GetInteraction(COMMON.GetInteraction.InteractionRequest objRequest);

        [OperationContract]
        COMMON.GetBlackList.BlackListResponse GetBlackListOsiptel(Entity.Transac.Service.Common.GetBlackList.BlackListRequest objBlackListRequest);

        #region TOA
        [OperationContract]
        COMMON.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(COMMON.GetTransactionScheduled.TransactionScheduledRequest objRequest);
        [OperationContract]
        COMMON.GetETAAuditRequestCapacity.ETAAuditResponseCapacity GetETAAuditRequestCapacity(COMMON.GetETAAuditRequestCapacity.ETAAuditRequestCapacity objETAAuditRequestCapacity);
        [OperationContract]
        int RegisterEtaRequest(COMMON.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest);
        [OperationContract]
        string RegisterEtaResponse(COMMON.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse);
        [OperationContract]
        Entity.Transac.Service.Common.GetTimeZones.TimeZonesResponse GetTimeZones(Entity.Transac.Service.Common.GetTimeZones.TimeZonesRequest objTimeZonesRequest);
        #endregion

        [OperationContract]
        Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost GetPuntosAtencion(COMMON.GetPuntosAtencion.PuntosAtencionRequest objPuntosAtencionRequest);

        [OperationContract]
        COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest);

        [OperationContract]
        COMMON.GetConsultNationality.ConsultNationalityResponse GetNationalityList(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest);

        [OperationContract]
        COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatusList(COMMON.GetCivilStatus.CivilStatusRequest objRequest);

        #region  PROY-140245-IDEA140240

        [OperationContract]
        COMMON.GetValidateCollaborator.GetValidateCollaboratorResponse GetValidateCollaborator(Entity.Transac.Service.Common.GetValidateCollaborator.GetValidateCollaboratorRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetConsultCampaign.GetConsultCampaignResponse GetConsultCampaign(Entity.Transac.Service.Common.GetConsultCampaign.GetConsultCampaignRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetConsultServiceBono.GetConsultServiceBonoResponse GetConsultServiceBono(Entity.Transac.Service.Common.GetConsultServiceBono.GetConsultServiceBonoRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse GetRegisterBonoSpeed(Entity.Transac.Service.Common.GetRegisterBonoSpeed.GetRegisterBonoSpeedRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse PostValidateDeliveryBAV(Entity.Transac.Service.Common.PostValidateDeliveryBAV.PostValidateDeliveryBAVRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetRegisterCampaign.GetRegisterCampaignResponse GetRegisterCampaign(Entity.Transac.Service.Common.GetRegisterCampaign.GetRegisterCampaignRequest objRequest, string strIdSession);
        
        [OperationContract]
        GetReadDataUserResponse GetReadDataUser(GetReadDataUserRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse GetValidateQuantityCampaign(Entity.Transac.Service.Common.GetValidateQuantityCampaign.GetValidateQuantityCampaignRequest objRequest, string strIdSession);

        [OperationContract]
        COMMON.GetSubmitNotificationEmail.EmailSubmitResponse EmailSubmit(Entity.Transac.Service.Common.GetSubmitNotificationEmail.EmailSubmitRequest objRequest);
        
        #endregion
[OperationContract]
        COMMON.GetGenerateConstancy.GenerateConstancyResponse GenerateContancyWithXml(COMMON.GetGenerateConstancy.GenerateConstancyRequest request, string xml);

        [OperationContract]
        COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest);

        [OperationContract]
        Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse GetParamsBSCS(Entity.Transac.Service.Common.GetReceipts.ReceiptsRequest objRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Common.GetEmployeByUser.EmployeeResponse GetEmployeByUserwithDP(Claro.SIACU.Entity.Transac.Service.Common.GetEmployeByUser.EmployeeRequest objRequest);
        [OperationContract]
        COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUserwithDP(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objRequest);

        [OperationContract]
        COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyNamePDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request, string NombrePDF);

        //INI -RF-02 
        [OperationContract]
        COMMON.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse ConsultarClaroPuntos(COMMON.GetConsultarClaroPuntos.ConsultarClaroPuntosRequest objRequest);
        [OperationContract]
        COMMON.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(COMMON.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesRequest objRequest);
        [OperationContract]
        COMMON.GetComprarPaquetes.ComprarPaquetesBodyResponse ComprarPaquetes(COMMON.GetComprarPaquetes.ComprarPaquetesRequest objRequest);
        [OperationContract]
        COMMON.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFDegradacion(COMMON.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        [OperationContract]
         List<Entity.Transac.Service.Common.Client> GetDatosporNroDocumentos(string strIdSession, string strTransaction, string strTipDoc, string strDocumento, string strEstado);

        //CAYCHOJJ onBase
        [OperationContract]
        UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest);
        [OperationContract]
        COMMON.GetGenerateConstancy.GenerateConstancyResponse GetConstancyPDFWithOnbase(COMMON.GetGenerateConstancy.GenerateConstancyRequest request);

        [OperationContract]
        COMMON.GetPCRFConsultation.PCRFConnectorResponse ObtenerTelefonosClienteLTE(COMMON.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        
    }
}
