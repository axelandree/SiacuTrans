using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Helper = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.IncomingCallsDetail;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Prepaid
{
    public class IncomingCallsDetail
    {
        public string IdSession { get; set; } 
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        [Header(Title = "Telephone")]
        [DisplayName("Teléfono")]
        public string NumberPhone { get; set; }
        public string RandomString { get; set; }
        public string HidInteraction { get; set; }
        public string HidFlagCharge { get; set; }
        
        public double Amount { get; set; } 
        public double MainBalance { get; set; }
        public string PathExcel { get; set; }
        public string AlertMessage { get; set; }
        public string FullPathPDF { get; set; }
        

        public string FlagPlatform { get; set; }
        public string FlagAuthorization_Export { get; set; }
        public string FlagAuthorization_Print { get; set; }
        public string FlagLoadDataLine { get; set; } 
        public string StrMinimumDate { get; set; }
        public string StrMaximumDate { get; set; }
        [Header(Title = "StartDate")]
        public string StrStartDate { get; set; }
        [Header(Title = "EndDate")]
        public string StrEndDate { get; set; }

        [Header(Title = "ListCallsDetail")]
        public List<Helper.CallDetail> ListCallsDetail { get; set; }

        [Header(Title = "Name")]
        [DisplayName("Nombre")]
        public string NameClient { get; set; }
        [Header(Title = "CACDAC")]
        [DisplayName("Punto de Control")]
        public string NameCACDAC { get; set; }
        [Header(Title = "TotalRecords")]
        public string TotalRecords { get; set; }
    }
}