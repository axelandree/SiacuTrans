using Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed
{
    public class ReplaceEquipmentSaveModel
    {
        public string strSessionId { get; set; }
        public string strContractId { get; set; }
        public string strCustomerType { get; set; }
        public string strCustomerID { get; set; }
        public string strFullName { get; set; }
        public string strCustomerContact { get; set; }
        public string strDocumentType { get; set; }
        public string strDocumentNumber { get; set; }
        public string strExecutionDate { get; set; }
        public bool blnCheckEmail { get; set; }
        public string strCheckEmail { get; set; }
        public string strCacDac { get; set; }
        public string strCacDacDescription { get; set; }
        public string strSOTNumber { get; set; }
        public string strSendEmail { get; set; }
        public string strAmmountToPay { get; set; }

        public string strBillingCycle { get; set; }
        public string strAddress { get; set; }
        public string strAddressNotes { get; set; }
        public string strDepartament { get; set; }
        public string strDistrict { get; set; }
        public string strCountry { get; set; }
        public string strProvince { get; set; }

        public string strResultMessage { get; set; }
        public string strResultCode { get; set; }
        public string strNumberTelephone { get; set; }
        public string strAccount { get; set; }

        public string strMessageErrorTransac { get; set; }
        public string strIdCase { get; set; }
        public bool bolErrorGenericCodSot { get; set; }
        public string strCodeUser { get; set; }
        public string strfullNameUser { get; set; }
        public string strCodPlan { get; set; }
        public List<Helpers.Fixed.ReplaceEquipment.CustomerEquipment> lstEquimentsAssociate { get; set; }
        public string strTypeWorkCode { get; set; }
        public string strTypeWork { get; set; }
        public string strMotiveSOTCode { get; set; }
        public string strMotiveSOT { get; set; }
        public string strTypeTransaction { get; set; }
        public string strNote { get; set; }
        public string strReferencia { get; set; }
        //Response Save

        public string ErrorInteraction { get; set; }
        public string SendEmail { get; set; }
        public string Error { get; set; }
        public string SaveSuccessfully { get; set; }
        public string strFilePathConstancy { get; set; }
        public bool bErrorTransac { get; set; }
        public Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.TypificationModel objTypification { get; set; }
    }
    public class ReplaceEquipmentMessageModel
    {
        public string strVariableEmpty { get; set; }
        public string strMsgVariableEmpty { get; set; }
        public string strStateLine { get; set; }
        public string strMsgStateLine { get; set; }
        public string strMsjValidacionCampoSlt { get; set; }
        public string strMsjValidacionCampoFormato { get; set; }
        public string strMsjValidacionCampo { get; set; }
        public string MessageInactiveContractStatus { get; set; }
        public string MessageDiscontinuedContractStatus { get; set; }
        public string MessageReserveContractStatus { get; set; }
        public string WithoutFixedTelephony { get; set; }
        public string ErrorInteraction { get; set; }
        public string WantToSave { get; set; }
        public string ValidationEmail { get; set; }
        public string SendEmail { get; set; }
        public string Error { get; set; }
        public string SaveSuccessfully { get; set; }
        public string FieldValidation { get; set; }
        public string FieldValidationSlt { get; set; }
        public string InactiveContractStatus { get; set; }
        public string DiscontinuedContractStatus { get; set; }
        public string ReserveContractStatus { get; set; }
        public string SendEmailDP { get; set; }
        public string FieldValidationQuantity { get; set; }
        public string NotRecognizeTyping { get; set; }
        public string ValidationChargueList { get; set; }
        public string strMsgCheckFullReplace { get; set; }
        public string strMsjValidacionCargadoListado { get; set; }
        public string strSaveSuccessfully { get; set; }
        public string strMessageSummary { get; set; }

        public string strConstancyError { get; set; }
        public string strEquipmentNotSelected { get; set; }
        public string strEquipmentBlockToSelected { get; set; }

        public string strValidateIncompleteFields { get; set; }
        public string strValidateEquipmentActive { get; set; }
        public string strValidateEquipmentNotFound { get; set; }
        public string strValidateEquipmentTechnicalErrors { get; set; }
        public string strValidateEquipmentContractInvalid { get; set; }
        public string strValidateEquipmentContractNotExist { get; set; }
        public string strValidateEquipmentIMSI { get; set; }
        public string strValidateEquipmentICCID { get; set; }
        public string strValidateEquipmentICCIDNotExist { get; set; }
        public string strValidateEquipmentMoreThanOneEquipment { get; set; }
        public string strValidateEquipmentNotReceivedNewData { get; set; }
        public string strValidateEquipmentResponseCodeInvalid { get; set; }
        public string strValidateSameDecoType { get; set; }
        public string strSeriesNumberNotEqual { get; set; }
        public string strNotSeriesInput { get; set; }
        public string strLimitEquipments { get; set; }
        public string strEqualEquipmentExistInOperation { get; set; }
        public string strSuccessModification { get; set; }
        public string strMsjErrorAlCargar { get; set; }
    }

    public class ReplaceEquipmentValidateModel
    {
        public string strSessionId { get; set; }
        public string strEquipmentTypeCode { get; set; }
        public string strContractId { get; set; }
        public string strResultMessage { get; set; }
        public string strEquipmentSeries { get; set; }
        public string strResultCode { get; set; }
        public string strCodInssrv { get; set; }
        public Helpers.Fixed.ReplaceEquipment.CustomerEquipment objEquipmentToAssociate { get; set; }
        public string strDecoType { get; set; }
        public string strOldEquipmentSeriesNumber { get; set; }
    }

    public class ReplaceEquipmentLoadModel
    {
        public string strIdSession { get; set; }
        public string strCodeUser { get; set; }
        public string strMessage { get; set; }
        public ReplaceEquipmentMessageModel objReplaceEquipmentMessageModel { get; set; }
        public string strUserCac { get; set; }
        public string strCustomerID { get; set; }
        public string strCustomerOBJID { get; set; }
        public string strCustomerContractID { get; set; }
        public string strFlagService { get; set; }
        public string strAmountToPay { get; set; }
        public string strCurrentEquipmentTypeSelected { get; set; }
        public string strCurrentEquipmentServiceTypeSelected { get; set; }
        public string strActiveFullReplaceEquipmentForFixed { get; set; }
        public List<ItemGeneric> lstClientSOTAssociate { get; set; }
        public List<ItemGeneric> lstSOTTypes { get; set; }
        public List<ItemGeneric> lstSOTReasons { get; set; }
        public List<Helpers.Fixed.ReplaceEquipment.CustomerEquipment> lstEquimentAssociate { get; set; }
        public List<ItemGeneric> lstCacDacTypes { get; set; }
        public List<ItemGeneric> lstBusinessRules { get; set; }
        public List<ItemGeneric> lstServiceTypes { get; set; }
        public List<ItemGeneric> lstMotiveSOTByTypeJob { get; set; }
        public List<Helpers.CommonServices.GenericItem> lstTypeWork { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ListItem> lstSOTListypes { get; set; }
        public Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.TypificationModel objTypification { get; set; }
        public string strEquipmentLimits { get; set; }
        public string strEquipmentAssociate { get; set; }
        public string strEquipmentAssociateUsed { get; set; }
        public string strServidorLeerPDF { get; set; }
        // Objetos a cambiar y agregar para el Método **ExecuteReplaceEquipment
        public string IDSESSION { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string NRO_TELEF { get; set; }
        public string POINT_ATTENTION { get; set; }
        public string CONTACT { get; set; }
        public string FULL_NAME { get; set; }

    }
}