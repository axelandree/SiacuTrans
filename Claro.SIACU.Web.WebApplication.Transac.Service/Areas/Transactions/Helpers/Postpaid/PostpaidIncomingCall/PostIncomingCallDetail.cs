using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall
{
    public class PostIncomingCallDetail
    {
        public string idSession { get; set; }
        public string contractID { get; set; }
        public string flagPlatform { get; set; }
        public string strTypeClient { get; set; }

        public string txtStartDate { get; set; }
        public string txttaskNote { get; set; }
        public string codOption { get; set; }
        public string telephone { get; set; }
        public string NameUserLoging { get; set; }
        public double Amount { get; set; }
        public string StatusMessage { get; set; }
        public string StatusCode { get; set; }
        public string txtEndDate { get; set; }

        public string flagTFI { get; set; }
        public string ObjId_Contact { get; set; }

        public string strNombres { get; set; }
        public string strApellidos { get; set; }
        public string strDocumentNumber { get; set; }

        public string strReferencePhone { get; set; }

        public string chkGenerateOCC_IsChecked { get; set; }


        public string ProfileAuthorized { get; set; }

        public string ddlCACDACSelected { get; set; }

        public string CaseId { get; set; }

        public string AuditHidden { get; set; }
        public List<Helpers.Postpaid.PostpaidIncomingCall.QueryAssociatedLines> lstQueryAssociatedLines { get; set; }
        public string DescriptionTotalRecords { get; set; }
        public string Payment { get; set; }

        public string profileExp { get; set; }
        public string profilePrint { get; set; }
        public string EmailProfileAuthorized { get; set; }
        public bool PaymentOCC { get; set; }
        public string CustomerId { get; set; }

        public string NameEmp { get; set; }
        public string SecondNameEmp { get; set; }
        public string Account { get; set; }
        public string Path { get; set; }
        public string dateIni { get; set; }
        public string dateEnd { get; set; }
        public string NameClient { get; set; }
        public string DateInConsult { get; set; }
        public string UserLogin { get; set; }
        public string LegalAgent { get; set; }
        public string TypeDocument { get; set; }

        public string RoutePdf { get; set; }
    }
}