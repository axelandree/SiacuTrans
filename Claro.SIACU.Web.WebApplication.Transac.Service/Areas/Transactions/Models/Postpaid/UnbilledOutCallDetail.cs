using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.UnbilledOutCallDetail;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class UnbilledOutCallDetail
    {
        //public string Phone { get; set; }

        //public string ContractID { get; set; }
        public string IdSession { get; set; }
        public string FlagPlatform { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string FlagSecurity { get; set; }
        //public string StatusDescription { get; set; }
        //public bool ShowButtonExport { get; set; }

        //public bool ShowPnlLlamadaPostpago { get; set; }

        //public bool ShowTrPrepago { get; set; }
        //public bool ShowTrPostpago { get; set; }
        //public bool ShowPnlBolsa { get; set; }

        //public bool ReqAuthorization_ExportDocumentExcel { get; set; }

        //public bool ReqAuthorization_Search { get; set; }

        [Header(Title = "Telephone")]
        public string Telephone { get; set; }

        public string FlagShow_Export { get; set; }
        public string FlagAuthorization_Export { get; set; }
        public string FlagAuthorization_Search { get; set; }

        [Header(Title = "StartDate")]
        public string StrStartDate { get; set; }
        [Header(Title = "EndDate")]
        public string StrEndDate { get; set; }
        public string StrMinimumDate { get; set; }
        public string StrMaximumDate { get; set; }

        [Header(Title = "ListCallsDetail")]
        public List<Helper.CallDetail> ListCallsDetail { get; set; }

        [Header(Title = "TotalMinutes")]
        public string StrTotal { get; set; }
        [Header(Title = "TotalSMS")]
        public string StrTotalSMS { get; set; }

        [Header(Title = "TotalRegistration")]
        public string StrTotalRegistration { get; set; }
        //public string StrTotalRegistration { get; set; }
    }
}