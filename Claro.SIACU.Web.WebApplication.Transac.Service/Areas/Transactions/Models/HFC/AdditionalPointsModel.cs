using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class AdditionalPointsModel
    {
        public string strMensajeErrorConsultaIGV { get; set; }
        public string strCicloFact { get; set; }
        public string strIGV { get; set; }
        public string strPostalCode { get; set; }
        public bool bGeneratedPDF {get; set;} 
        public string strFullPathPDF {get; set;} 
        public string strlogin { get; set; }
        public string strJobTypes { get; set; }
        public string strInternetValue { get; set; }
        public string strCellPhoneValue { get; set; }
        public string strCableValue { get; set; }
        public string strCodePlanInst { get; set; }
        public string strCustomerId { get; set; }
        public string strContractId { get; set; }

        #region GetParameters

        public string strServerName { get; set; }
        public string strLocalAddress { get; set; }
        public string strHostName { get; set; }
        public string strTitlePageAdditionalPoints {get; set;}
        public string strMessageConfirmAdditionsPoints {get; set;}
        public string strMessageEnterMail {get; set;} 
        public string strMessageValiateMail {get;set;}
        public string strMessageValidatePointCare {get; set;}
        public string strMessageValidatePhone {get; set;}
        public string strMessageValidateTimeZone {get; set;}
        public string strMessageValidateSchedule {get; set;}

        public string strDateNew { get; set; }
        public string strDateServer {get; set;}
        public string strCustomerRequestId { get; set; }
        public string strJobTypeComplementarySalesHFC { get; set; }
            
        public string strMessageConsultationDisabilityNotAvailable {get; set;}
        public string strMessageOK {get; set;}
        public string strRouteSiteInitial {get; set;}

        public string strJobTypeDefault {get; set; }
        public string strJobTypeLoyalty {get; set;}
        public string strJobTypeMaintenance {get; set;}
        public string strJobTypeMaintenance_Bs {get; set;}
        public string strJobTypeRetention {get; set;}
        public string strJobTypePoints {get; set;}
        public string strMessageMaxProgDay { get; set; }
        public string strMessageDateAppNotLowerNow { get; set; }
        public string strMessageGenericBackOffice { get; set; }
        public string strMessageGenericBackOfficeBucked { get; set; }
        public string strMessageNotServiceCableInternet { get; set; }
        public string strMessageCustomerContractEmpty { get; set; }
        public string strHourServer { get; set; }
        public string strMessageForcesSSTTETA { get; set; }
        public string strMessageNotTimeZoneHourETA { get; set; }
        public string strMessageValidationETA { get; set; }

        public string strAdditionaPointHFCCost { get; set; }
        #endregion

        public string strTelephone { get; set; }
        public string strCurrentUser  { get; set; }
        public string strFullName  { get; set; }
        public string strLastName  { get; set; }
        public string strBusinessName  { get; set; }
        public string strDocumentType  { get; set; }
        public string strDocumentNumber { get; set; }
        public string strAddress { get; set; }
        public string strDistrict { get; set; }
        public string strDepartament { get; set; }
        public string strProvince { get; set; }
        public string strModality { get; set; }
        public string strValidateETA { get; set; }
        public string strRequestActId { get; set; }
        public string strDateProgramming { get; set; }
        public string strSchedule { get; set; }
        public string strMotiveSot { get; set; }
        public string strServicesType { get; set; }
        public string strAttachedQuantity { get; set; }
        public string strCodSOT { get; set; }
        public string strMonthEmision { get; set; }
        public string strYearEmision { get; set; }
        public string strTransaction { get; set; }
        //Response
        public bool bErrorTransac { get; set; }
        public bool bErrorGenericCodSot { get; set; }
        public string strMessageErrorTransac { get; set; }
        public string strCaseID { get; set; }
        public string strCodeTipification { get; set; }
        public string strNote { get; set; }
        public string strPlan { get; set; }
        public string strEmail { get; set; }
      
        
        public string Sn { get; set; }
        public string IpServidor { get; set; }
        public string IdSession { get; set; }

        public string strFirstName { get; set; }
        
        public string strLegalRepresent { get; set; }
        public string strLegalDepartament { get; set; }
        public string strLegalProvince { get; set; }
        public string strLegalDistrict { get; set; }
        public string strLegalBuilding { get; set; }
        public string strDescCacDac { get; set; }
        public string strDescJobType { get; set; }
        public string strDescMotive { get; set; }
        public string strDescServicesType { get; set; }
        public string strAddressInst { get; set; }
        public string strUbigeoInst { get; set; }
        public string strAmount { get; set; }
        public string strCacDac { get; set; }
        public string strReference { get; set; }

        public bool bSendMail { get; set; }

        public int iFidelidad { get; set; }
        public string strFidelidad { get; set; }
        public string strCountry { get; set; }
        public string strtypeCliente { get; set; }
        public string strScheduleValue { get; set; }
        public string strScheduleGet { get; set; }
        public string strSubTypeWork { get; set; }
        public string strMensajeTransaccionFTTH { get; set; } //RONALDRR - PUNTOS ADICIONALES
        public string strPlanoFTTH { get; set; } //RONALDRR - PUNTOS ADICIONALES
    }
}