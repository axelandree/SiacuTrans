using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Transac.Service;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    /// <summary>
    /// Creado por LCochachi
    /// </summary>
    public class BilledOutCallDetailModel
    {
        public string IdSession { get; set; }
        public string CurrentId { get; set; }
        public string strinteractionId { get; set; }
        public string strFinalNotes { get; set; }
        [Header(Title = "Telephone")]
        public string Telephone { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin{ get; set; } 

        public string StatusCode { get; set; }
        public string StatusMessage { get; set; } 

        public string HidCurrentDate { get; set; }
        public string HidTempTypePhone { get; set; }
        public string HidInvoiceNumberWithDates { get; set; }
        public string HidInvoiceNumber { get; set; } 
        public string HidTypePD { get; set; }
        public string HidClassPD { get; set; }
        public string HidSubClassPD { get; set; }
        public string HidIdCase { get; set; }
         
        public string StrDayEmission { get; set; }
        public string StrOptionCode { get; set; }
        public string StrDescription { get; set; }
        public string RutaPdf { get; set; }
        
        public List<ItemGeneric> ListYears { get; set; }
        public List<ItemGeneric> ListMonths { get; set; }
        public List<ItemGeneric> ListCACDAC { get; set; }

        public string FlagSecurity { get; set; }
        public string FlagExport{ get; set; }
        public string FlagPrint { get; set; }
        public string FlagSearch { get; set; }
        public string FlagEmail { get; set; }
        public string FlagPlatform { get; set; } 
       

        public string LitFinalCharge { get; set; }

        public List<String> ListCallsDetail2 { get; set; }

        // == Variable para la exportacion
        [Header(Title = "ListCallsDetail")]
        public List<HELPER_ITEM.BilledOutCallDetailsPost> ListCallsDetail { get; set; }
        public string PathExcel { get; set; }
        [Header(Title = "LblTypeCustomer")]
        public string LblTypeCustomer { get; set; }
        [Header(Title = "Account")]
        public string AccountCustomer { get; set; }

        public string StrDateRange { get; set; } 

        [Header(Title = "FullName")]
        public string FullNameCustomer { get; set; }
        [Header(Title = "Plan")]
        public string Plan { get; set; }

        [Header(Title = "StrTotal")]
        public string StrTotal { get; set; }
        [Header(Title = "StrTotalSMS")]
        public string StrTotalSMS { get; set; }
        [Header(Title = "StrTotalMMS")]
        public string StrTotalMMS { get; set; }
        [Header(Title = "StrTotalGPRS")]
        public string StrTotalGPRS { get; set; }
        [Header(Title = "StrTotalRegistration")]
        public string StrTotalRegistration { get; set; }
        [Header(Title = "StrFinalCharge")]
        public string StrFinalCharge { get; set; }
    }
}