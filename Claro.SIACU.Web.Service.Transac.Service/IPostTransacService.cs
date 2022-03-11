using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Claro.Entity;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsultService;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyServiceQuotAmount;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.PostUpdateChangeData;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetNetflixServices;
using Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPostTransacService" in both code and config file together.
    [ServiceContract]
    public interface IPostTransacService
    {
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt.ReceiptResponse GetDataInvoice(ReceiptRequest request);
        
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit.ConsumeLimitResponse GETConsumeLimit(ConsumeLimitRequest objConsumeLimitRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeResponse ConsultFixedCharge(Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeRequest objRequest);
        
        [OperationContract]
        SimCardPhone GetStatusPhone(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber);

        [OperationContract]
        HLRResponse GetHLRLocation(HLRRequest objHLRRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleResponse GetBillingCycle(Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesResponse GetPlanModel(Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanResponse GetNewPlan(Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanResponse GetTarifePlan(Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationResponse GetElementMigration(Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerResponse GetCodeChangeCustomer(Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea.AreaResponse GetArea(Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea.AreaRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaResponse GetMotiveByArea(Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveResponse GetSubMotive(Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopResponse GetConsumptionStop(Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopRequest objRequest);


        [OperationContract]
        ChangePhoneNumber ValidateChangeNumberTransaction(string strIdSession, string strTransactionID, string strContract, string strFlagFidelize);

        [OperationContract]
        ChangePhoneNumberResponse GetAvailableLines(ChangePhoneNumberRequest objChangePhoneNumberRequest);

        [OperationContract]
        bool ValidateChangeNumberBSCS(string strIdSession, string strTransactionID, string strSerialNum, string strDnNum, int intEstado);

        [OperationContract]
        ChangePhoneNumberResponse ExecuteChangeNumber(ChangePhoneNumberRequest objChangePhoneNumberRequest);

        [OperationContract]
        ChangePhoneNumberResponse RollbackChangeNumber(ChangePhoneNumberRequest objChangePhoneNumberRequest);

        [OperationContract]
        string DeleteUserHistory(string strIdSession, string strTransactionID, string strPhone, string strMotive);

        [OperationContract]
        ChangePhoneNumberResponse UpdatePhoneNumber(ChangePhoneNumberRequest objChangePhoneNumberRequest);

        [OperationContract]
        Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse GetPortability(Entity.Transac.Service.Postpaid.GetPortability.PortabilityRequest objPortabilityRequest);

        [OperationContract]
        Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse GetTriaciones(Entity.Transac.Service.Postpaid.GetTriations.StriationsRequest objTriacionRequest);

        //[OperationContract]
        //Entity.Transac.Service.Postpaid.GetSiacParameter.SiacParameterResponse GetSiacParameterTFIPostpago(Entity.Transac.Service.Postpaid.GetSiacParameter.SiacParameterRequest request);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanResponse GetPlansMigrations(Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanRequest objRequest);


        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanResponse GetFixedCostBasePlan(Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanRequest objRequest);


        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesResponse GetListQueryAssociatedLines(Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesRequest objQueryAssociatedLinesRequest);

        [OperationContract] 
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationProgDeudaBloqSuspResponse
            (Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationMigration
            (Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest);

        [OperationContract]
        POSTPAID.GetBillData.BillDataResponse GetListBillSummary(POSTPAID.GetBillData.BillDataRequest request);

        //[OperationContract]
        //POSTPAID.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(POSTPAID.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest);

        [OperationContract]
        POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest);
        [OperationContract]
        POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest);

        [OperationContract]
        List<ListItem> GetDocumentType(string strIdSession, string strTransaction, string strCodCargaDdl);

        [OperationContract]
        POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response GetCallDetailNB_DB1(POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Request request);

        [OperationContract]
        POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse GetCallDetailNB_DB1_BSCS(POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSRequest request);
        [OperationContract]
        POSTPAID.GetAgreement.AgreementResponse GetReinstatementEquipment(POSTPAID.GetAgreement.AgreementResquest objRequest);
        [OperationContract]
        POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductResponse GetServByTransCodeProductResponse(POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductRequest objRequest);
     
     
        [OperationContract]
        POSTPAID.GetMaintenancePlan.MaintenancePlanResponse GetPlansServices(POSTPAID.GetMaintenancePlan.MaintenancePlanRequest objRequest);

        [OperationContract]
        POSTPAID.GetValidateBagShare.ValidateBagShareResponse GetValidateBagShare(POSTPAID.GetValidateBagShare.ValidateBagShareRequest objRequest);


        [OperationContract]
        POSTPAID.GetValidateProgByProduct.ValidateProgByProductResponse GetValidateProgByProduct(POSTPAID.GetValidateProgByProduct.ValidateProgByProductRequest objRequest);

        [OperationContract]
        POSTPAID.GetRegisterPlanService.RegisterPlanResponse RegisterPlanService(POSTPAID.GetRegisterPlanService.RegisterPlanRequest objRequest);

        [OperationContract]
        POSTPAID.GetProgramerMigration.ProgramerMigrationResponse ProgramerMigrationControlPostPago(POSTPAID.GetProgramerMigration.ProgramerMigrationRequest objRequest);

        [OperationContract]
        POSTPAID.GetDataByContract.DataByContractResponse GetDataByContract(POSTPAID.GetDataByContract.DataByContractRequest objRequest);

        [OperationContract]
        POSTPAID.GetDataByCount.DataByCountResponse GetDataByCount(POSTPAID.GetDataByCount.DataByCountRequest objRequest);


        [OperationContract]
        bool AlignTransactionService(string strIdSession, string strTransaction, string strContractID);

        [OperationContract]
        POSTPAID.GetUpdateInteraction.UpdateInteractionResponse GetUpdateInteraction(POSTPAID.GetUpdateInteraction.UpdateInteractionRequest objRequest);
        [OperationContract]
        POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse GetAdjustForClaims(POSTPAID.GetAdjustForClaims.AdjustForClaimsRequest objRequest);
        [OperationContract]
        bool AlignCodID(string strIdSession, string strTransaction, string strContractID);
        [OperationContract]
        POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse MigrationPlans(POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanRequest objRequest);

        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailResponse GetBillPostDetail(Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailRequest objBillPostRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIResponse GetListInvoicePDI(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIRequest objListInvoicePdiRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceResponse GetListInvoice(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceRequest objListInvoiceRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailResponse GetListCallDetail(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailRequest objListCallDetailRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIResponse GetListCallDetailPDI(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIRequest objListCallDetailPDIRequest);
        [OperationContract]
        Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListResponse GetRechargeList(Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListRequest objRechargeListRequest);
        [OperationContract]
        ApprovalBusinessCreditLimitResponse GetApprovalBusinessCreditLimitBusinessAccount(
            ApprovalBusinessCreditLimitRequest objRequest);

        [OperationContract]
        UserExistsBSCSResponse GetUserExistsBSCS(UserExistsBSCSRequest objRequest);

        [OperationContract]
        ConsultServiceResponse GetConsultService(ConsultServiceRequest objRequest);

        [OperationContract]
        ModifyServiceQuotAmountResponse GetModifyServiceQuotAmount(ModifyServiceQuotAmountRequest objRequest);

        [OperationContract]
        TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest);

        [OperationContract]
        ActDesServProgResponse GetActDesServProg(ActDesServProgRequest objRequest);

        [OperationContract]
        ServiceByContractResponse GetServiceByContract(ServiceByContractRequest objRequest);

        [OperationContract]
        POSTPAID.GetServicesDTH.ServicesDTHResponse GetServicesDTH(POSTPAID.GetServicesDTH.ServicesDTHRequest objRequest);

        [OperationContract]
        POSTPAID.GetValidateActDesServProg.ValidateActDesServProgResponse GetValidateActDesServProg(
            POSTPAID.GetValidateActDesServProg.ValidateActDesServProgRequest objRequest);

        [OperationContract]
        POSTPAID.GetServiceBSCS.ServiceBSCSResponse GetServiceBSCS(
            POSTPAID.GetServiceBSCS.ServiceBSCSRequest objRequest);
        #region RetenciónCancelación

        [OperationContract]
        RetentionCancel GetDataAccord(RetentionCancel oRequest);

        [OperationContract]
        RetentionCancel GetLoadStaidTotal(RetentionCancel oRequest);
        #endregion
        
        [OperationContract]
        InsertTraceabilityResponse GetInsertTraceability(InsertTraceabilityRequest objRequest);

        [OperationContract]
        BiometricConfigurationResponse GetBiometricConfiguration(BiometricConfigurationRequest objRequest);

        [OperationContract]
        SignDocumentResponse GetSignDocument(SignDocumentRequest objRequest);

        [OperationContract]
        POSTPAID.GetDataCustomer.DataCustomerResponse GetDataCustomer(POSTPAID.GetDataCustomer.DataCustomerRequest objRequest, int flag);

        [OperationContract]
        List<ListItem> GetMotivoCambio(string strIdSession, string strTransaction, string pid_parametro, string rMsgText);              

        [OperationContract]
        string ValidateEnvioxMail(string strIdSession, string strTransaction, string strCustomerID);

        [OperationContract]
        UpdateChangeDataResponse UpdateNameCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente);

        [OperationContract]
        UpdateChangeDataResponse UpdateDataMinorCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, int intSeq_in);

        [OperationContract]
        string UpdateDataCustomerCLF(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente);
        
        [OperationContract]
        UpdateChangeDataResponse UpdateAddressCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, string tipoDireccion, int intSeq_in);

        [OperationContract]
        string UpdateDataCustomerPClub(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente);

        [OperationContract]
        int registrarTransaccionSiga(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Fixed.TransactionSiga oTransaction);
        
        #region "NetflixServices ServicesMethods"
        [OperationContract]
        bool validarAccesoRegistroLinkMovil(string strIdSession, string strIdTransaccion, parametroMovil oparametroMovil);
        
        [OperationContract]
        ServicesNXResponse envioNotificacionRegistroNX(string strIdSession, string strIdTransaccion, ServicesNXRequest oRequest, AuditRequest oAuditRequest);
        #endregion
    }
}
