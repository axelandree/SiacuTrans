using System;
using System.Collections.Generic;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail;
using System.Linq;
using System.Web;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Prepaid
{
    public class ExportExcelOutPrepaidModel
    {
        [Header(Title = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Header(Title = "Trafic")]
        public string Trafic { get; set; }

        [Header(Title = "ListExportExcel")]
        public List<ExportExcelPreCallDetails> ListExportExcel { get; set; }
    }
}