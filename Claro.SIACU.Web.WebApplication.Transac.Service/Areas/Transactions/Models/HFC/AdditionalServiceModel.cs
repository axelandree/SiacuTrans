using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class AdditionalServiceModel
    {
        public string IdSession { get; set; }
        public string strUser { get; set; }
        public string strContractId { get; set; }
        public string strCustomerId { get; set; }
        public string strAmount { get; set; }
        public string strCboCampaign { get; set; }
        public string strDateProgramation { get; set; }
        public string strBillingCycle { get; set; }
        public string strTxtNote { get; set; }
        public string strCodPlaneInst { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public string strNumberDocument { get; set; }
        public string strReferencePhone { get; set; }
        public string strReasonSocial { get; set; }
        public string strContactClient { get; set; }
      
        public string strPlan { get; set; }
        public string strCacDacDescription { get; set; }
        public string strFlagChkSendEmail { get; set; }
        public string strTxtSendEmail { get; set; }
        public string strFlagChkProgramming { get; set; }
        public string strPhone { get; set; }
        public string strTransaction { get; set; }
        public string strNameClient { get; set; }
        public string strAccountUser { get; set; }
        public string strLegalRepresent { get; set; }
        public string strDocumentType { get; set; }
        public string strFullNameCustomer { get; set; }

        public string strHdnReady { get; set; }
        public string strHdnCodService { get; set; }
        public string strHdnTipoTransaccion { get; set; }
        public string strHdnTipiService { get; set; }
        public string strHdnDesCoSerSel { get; set; }
        public string strHdnValuePVUMatch { get; set; }
        public string strHdnCoSerSel { get; set; }
      
        public string strHdnCostoPVUSel { get; set; }
        public string strHdnCostoBSCS { get; set; }
        public string strHdnCargoFijoSel { get; set; }



      
        public string strHdnClass { get; set; }
        public string strHdnSubClass { get; set; }
        public string strHdnCaseId { get; set; }
        public string strHdnType { get; set; }
        public string strHdnInteractionCode { get; set; }

        public bool bErrorTransac { get; set; }
        public bool bShowLabelMessage { get; set; }
        public string strShowLabelMessage { get; set; }

        #region Message
         public string strMessageErrorTransac { get; set; }
		 public string strMessageEnterMail {get; set;}
         public string strMessageValiateMail {get; set;}


        public string strLabelMessage { get; set; }
	    #endregion

        public string strDniRuc { get; set; }
        public string strAddress { get; set; }
        public string strDistrict { get; set; }
        public string strDepartment { get; set; }
        public string strProvince { get; set; }

        public string strAccountNumber { get; set; }
        public string strUsernameApp { get; set; }

        public string strCustomerType { get; set; }
    }
}