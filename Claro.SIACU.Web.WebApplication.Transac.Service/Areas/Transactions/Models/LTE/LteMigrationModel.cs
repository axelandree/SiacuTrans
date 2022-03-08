using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.Fixed;
using System;
using System.Collections.Generic;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE
{
    public class LteMigrationModel
    {
        public string Rules { get; set; }

        public List<FixedTransacService.ProductPlanHfc> plans { get; set; }

        public IEnumerable<string> Campaigns { get; set; }

        public IEnumerable<string> Solutions { get; set; }

        public List<FixedTransacService.ServiceByPlan> ServicesByPlan { get; set; }

        public List<FixedTransacService.JobType> JobTypes { get; set; }

        public List<OrderSubTypeViewModel> OrderSubTypes { get; set; }

        public string ServerDate { get; set; }
        public List<FixedTransacService.Carrier> Carriers { get; set; }

        public string IdServicio { get; set; }


        public List<FixedTransacService.ServiceByCurrentPlan> ServicesByCurrentPlan { get; set; }
        public Helpers.LTE.MigrationPlan.PlanCharges ServicesByCurrentPlanCharges { get; set; }
        public List<FixedTransacService.ServiceByPlan> AditionalServices { get; set; }
        public string[] SelectedServices { get; set; }
        public string strServerPathPDF { get; set; }

        public List<FixedTransacService.ServiceByPlan> CoreServices { get; set; }

        public Helpers.LTE.MigrationPlan.ConfigurationData ConfigurationData { get; set; }
    }

    public class LteMigrationPlanSaveModel
    {
        public string strIdSession { get; set; }
        public string strIdContract { get; set; }
        public string strIdCustomer { get; set; }
        public string strTypeNumber { get; set; }
        public bool bolSendEmail { get; set; }
        public string strSendEmail { get; set; }
        public string strEmail { get; set; }
        public string strCustomerContact { get; set; }
        public string strTelephone { get; set; }
        public string strCacDac { get; set; }
        public string strNotes { get; set; }
        public string strCustomerType { get; set; }
        public string strFullName { get; set; }
        public string strDescCacDac { get; set; }
        public string strMessageErrorTransac { get; set; }
        public string strflgResult { get; set; }
        public string strMSError { get; set; }
        public string strLogin { get; set; }
        public string strModality { get; set; }
        public string strCodeTipification { get; set; }
        public bool bErrorTransac { get; set; }
        public string strPostalCode { get; set; }
        public string strLegalRepresent { get; set; }
        public string strIdCase { get; set; }
        public bool bolErrorGenericCodSot { get; set; }
        public string strAccount { get; set; }
        public string strNumberTelephone { get; set; }
        public string strFirstName { get; set; }
        public Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.TypificationModel objTypification { get; set; }
        public string strFilePathConstancy { get; set; }
        public string strPlanName { get; set; }
        public string strPhoneReference { get; set; }

        public string strName { get; set; }
        public string strLastname { get; set; }
        public string strBusinessName { get; set; }
        public string strDocumentType { get; set; }
        public string strDocumentNumber { get; set; }
        public string strAddress { get; set; }
        public string strDistrict { get; set; }
        public string strDepartament { get; set; }
        public string strProvince { get; set; }

        public TypificationItem objItemTypification { get; set; }
        public string strCodPlan { get; set; }
        public string strBillingCycle { get; set; }
        public string strActivationDate { get; set; }
        public string strTermContract { get; set; }
        public string strStateLine { get; set; }
        public string strExpirationDate { get; set; }
        public string strOfficeAddress { get; set; }
        public string strLegalAddress { get; set; }
        public string strLegalDistrict { get; set; }
        public string strLegalCountry { get; set; }
        public string strLegalProvince { get; set; }
        public string strPlaneCodeInstallation { get; set; }
        public string strLegalUrbanization { get; set; }
        public string strLegalAgent { get; set; }
        public string strPlan { get; set; }
        public string strPresuscritoStatus { get; set; }
        public string strNoLetter { get; set; }
        public string strDdlOperator { get; set; }
        public string strPublishFinalStatus { get; set; }
        public string strRefound { get; set; }
        public string strLoyalityAmount { get; set; }
        public string strTotalPenalty { get; set; }
        public string strFinalLoyalityStatus { get; set; }
        public string strOCCFinalStatus { get; set; }
        public string strValidateETAStatus { get; set; }
        public string spnNewTotalFixedChargeCIGV { get; set; }
        public string strExistCorePhoneService { get; set; }
        public string strFProgrammming { get; set; }
        public string strDdlOperatorStatus { get; set; }
        public string strHdnListFTMCode { get; set; }
        public string strCodMoTot { get; set; }
        public string strTmCode { get; set; }
        public string strHdnCodPlan { get; set; }
        public string strProductType { get; set; }
        public string strConstanceXml { get; set; }
        public List<string> lstPDFConstancyParameters { get; set; }

        public List<ServiceByPlan> lstServices { get; set; }
        public List<ServiceByPlan> lstEquipmentTotalCore { get; set; }
        public List<ServiceByPlan> lstAdditionalEquipmentQuantity { get; set; }
        public List<ServiceByPlan> lstEquipmentCableAllAdditional { get; set; }
        public double dblCreditLimit { get; set; }
        public double dblConsumeCapAmount { get; set; }
        public double dblConsumeCap { get; set; }
        public string strConsumeCapComment { get; set; }
        public int intFlagConsumeCap { get; set; }
        public string strAnotation { get; set; }

        //Typification
        public string strNewPlan { get; set; }
        public string strNewSolution { get; set; }
        public string strAddressReference { get; set; }
    }
    public class LteMigrationPlanMessageModel
    {
        public string strErrorInteraction { get; set; }
        public string strWantToSave { get; set; }
        public string strValidationFormat { get; set; }
        public string strSendEmail { get; set; }
        public string strError { get; set; }
        public string strSaveSuccessfully { get; set; }
        public string strFieldValidation { get; set; }
        public string strFieldValidationSlt { get; set; }
        public string strFieldValidationQuantity { get; set; }
        public string strValidationChargueList { get; set; }
        public string strCurrentCablePlan { get; set; }
        public string strCurrentInternetPlan { get; set; }
        public string strCurrentPhonePlan { get; set; }
        public string strLTEGroupCable { get; set; }
        public string strCanLoyaltyActive { get; set; }
        public string strCantLoyaltyActive { get; set; }
        public string strAuthorizeLoyaltyActive { get; set; }
        public string strCanEnterActive { get; set; }
        public string strAuthorizeEnterActive { get; set; }
        public string strLTEGroupInternet { get; set; }
        public string strLTEGroupTelephony { get; set; }
        public string strCantEnterActive { get; set; }
        public string strErrorMessageIgv { get; set; }
        public string strYesTechnicalVisit { get; set; }
        public string strNotTechnicalVisit { get; set; }
        public string strElementWasAdded { get; set; }
        public string strNotHaveAuthorization { get; set; }
        public string strServerPDF { get; set; }
        public string strValidationLine { get; set; }
        public string strErrorRequestAjax { get; set; }
        public string strSelectToRemove { get; set; }
        public string strSelectMaximumEquipment { get; set; }
        public string strSelectToRent { get; set; }
        public string strErrorLoading { get; set; }
        public string strErrorValidating { get; set; }
        public string strThereAreNoRecords { get; set; }
        public string strDontLoadTipification { get; set; }
        public string strThereAreNoRecordsIn { get; set; }
        public string strThereAreNoPhone { get; set; }
        public string strWantChargueCore { get; set; }
        public string strMessageValidationQuantityDecos { get; set; }
        public string strCodeHD { get; set; }
        public string strCodeSD { get; set; }
        public string strCodeDVR { get; set; }
        public string strMessageValidateVelPlan { get; set; }
        public string strDayInstallation { get; set; }
        public string strTittleConstancy { get; set; }
        public string strTextConstancy { get; set; }
        public string strActionDefaultTopeConsumo { get; set; }
        public string strValueTopeConsumo { get; set; }
        public string strValueTopeConsumoOCC { get; set; }
        public string strNameFormatLte { get; set; }
        public string strCodeTechnicalVisit { get; set; }
    }

    public class LteMigrationPlanLoadModel
    {
        public string strIdSession { get; set; }
        public List<FixedTransacService.Carrier> lstCarriers { get; set; }
        public List<FixedTransacService.ServiceByCurrentPlan> lstServicesByCurrentPlan { get; set; }
        public Helpers.LTE.MigrationPlan.PlanCharges objServicesByCurrentPlanCharges { get; set; }
        public string strServerDate { get; set; }
        public string strPhone { get; set; }
        public double dblIgv { get; set; }
        public double dblIgvView { get; set; }
        public string strMessage { get; set; }
        public LteMigrationPlanMessageModel objLteMigrationPlanMessage { get; set; }
        public string strUserCac { get; set; }
        public Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.User objDataUser { get; set; }
        public List<DetEquipmentService> lstEquipmentByCurrenPlan { get; set; }
        public List<JobType> lstJobTypes { get; set; }
        public List<ItemGeneric> lstCacDacTypes { get; set; }
        public List<ItemGeneric> lstBusinessRules { get; set; }
        public TypificationItem objTypification { get; set; }
        public int intLTEValidateVel { get; set; }
        public string strIdIdentifyTypeWork { get; set; }
        public string strIdTypeWork { get; set; }
        public List<ItemGeneric> lstActions { get; set; }
        public Office objOffice { get; set; }

        public List<ItemGeneric> lstOptions { get; set; }
        
    }

    public class LteChoosePlanLoadModel
    {
        public List<FixedTransacService.ProductPlanHfc> lstPlans { get; set; }
        public List<FixedTransacService.ProductPlanHfc> lstCampaniasAndSolutions { get; set; }
        //public List<FixedTransacService.Campaign> lstCampaigns { get; set; }
        public IEnumerable<string> lstSolutions { get; set; }
        public IEnumerable<string> lstCampaigns { get; set; }
        public List<ItemGeneric> lstSearchOptions { get; set; }
        public bool bolPermition { get; set; } 
    }
    public class LteChooseCoreServicesByPlanLoadModel
    {
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanCable { get; set; }
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanInternet { get; set; }
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanTelephone { get; set; }
        public string strLTEServicesType { get; set; }
        public string strLTEGroupCable { get; set; }
        public string strLTEGroupInternet { get; set; }
        public string strLTEGroupTelephony { get; set; }
        public string[] arrLTEGroupCable { get; set; }
        public string[] arrLTEGroupInternet { get; set; }
        public string[] arrLTEServicesType { get; set; }
        public string[] arrLTEGroupTelephony { get; set; }
        public LteChooseCoreServicesByPlanLoadMessageModel objLteChooseCoreServicesByPlanLoadMessage { get; set; }
    }
    public class LteChooseCoreServicesByPlanLoadMessageModel
    {
        public string strMessageValidateVelPlan { get; set; }
    }
    public class LteChooseServicesByPlanLoadModel
    {
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanCable { get; set; }
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanInternet { get; set; }
        public List<FixedTransacService.ServiceByPlan> lstServicesByPlanTelephone { get; set; }
        public string strLTEServicesType { get; set; }
        public string strLTEGroupCable { get; set; }
        public string strLTEGroupInternet { get; set; }
        public string strLTEGroupTelephony { get; set; }
        public string[] arrLTEGroupCable { get; set; }
        public string[] arrLTEGroupInternet { get; set; }
        public string[] arrLTEServicesType { get; set; }
        public string[] arrLTEGroupTelephony { get; set; }
    }
    public class LteChooseEquipmentByPlanLoadModel
    {
        public List<FixedTransacService.ServiceByPlan> lstEquipmentByPlan { get; set; }
        public DecoMatrizResponse objDecoMatriz { get; set; }
        public string strLTEEquipmentType { get; set; }
        public string[] arrLTEEquipmentType { get; set; }
        public LteChooseEquipmentByPlanLoadMessageModel objMessage { get; set; }

    }
    public class LteChooseEquipmentByPlanLoadMessageModel
    {
        public int intQuantityMaxPoint { get; set; }
        public int intQuantityDefaultHD { get; set; }
        public int intQuantityDefaultSD { get; set; }
        public int intQuantityDefaultDVR { get; set; }
        public string strMessageQuantityEquipment { get; set; }
        public string strMessageErrorValidationEquipment { get; set; }
        public string strMessageQuantityXEquipment { get; set; }
    }
    public class LteMigrationPlanRequest: RequestDataModel
    {
        public string strIdPlan { get; set; }
        public string strPermitions { get; set; }
        public string strTmCode { get; set; }
        public string strCodPlanSisact { get; set; }
        public string strUbigeo { get; set; }
        public string strIdCable { get; set; }
        public string strIdInternet { get; set; }
        public string strIdPhone { get; set; }
        public string strOffice { get; set; }
        public string strIgv { get; set; }
        public string strPlaneCodeInst { get; set; } 
        public List<ServiceByPlan> lstEquipmentVisitCore { get; set; }
        public List<ServiceByPlan> lstAdditionalEquipmentQuantity { get; set; }
        public List<ServiceByPlan> lstAdditionalServicesCableSelected { get; set; }
        public List<ServiceByPlan> lstAdditionalServicesInternetSelected { get; set; }
        public List<ServiceByPlan> lstAdditionalServicesPhoneSelected { get; set; }
        
    }
    public class  EquipmentCoreAndCoreAdditional{
        public List<ServiceByPlan> lstEquipmentDecoCableAdditional { get; set; }
        public List<ServiceByPlan> lstServicesByPlanCableCoreAddi { get; set; }
        public List<ServiceByPlan> lstServicesByPlanInternetCoreAddi { get; set; }
        public List<ServiceByPlan> lstServicesByPlanTelephoneCoreAddi { get; set; }
        public List<ServiceByPlan> lstEquipmentInternetAndPhone { get; set; }
        public List<ServiceByPlan> lstEquipmentDecoCableCore { get; set; }
        public List<ServiceByPlan> lstEquipmentTotalCore { get; set; }
        public List<ServiceByPlan> lstEquipmentCableNotDecos { get; set; }
        
        public List<ServiceByPlan> lstEquipmentVisitCore { get; set; }
        public List<ServiceByPlan> lstEquipmentCableAllAdditional { get; set; }
        
        
    }
}