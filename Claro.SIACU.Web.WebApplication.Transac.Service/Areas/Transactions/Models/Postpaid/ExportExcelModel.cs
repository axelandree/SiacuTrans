using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class ExportExcelModel : IExcel
    {
        [Header(Title = "ID")]
        public string Id { get; set; }

        [Header(Title = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Header(Title = "NameClient")]
        public string NameClient { get; set; }

        [Header(Title = "DateInConsult")]
        public string DateInConsult { get; set; }

        [Header(Title = "CacDac")]
        public string CacDac { get; set; }

        [Header(Title = "ListExportExcel")]
        public List<Helpers.Postpaid.PostpaidIncomingCall.ExportExcel> ListExportExcel { get; set; }
    }
}