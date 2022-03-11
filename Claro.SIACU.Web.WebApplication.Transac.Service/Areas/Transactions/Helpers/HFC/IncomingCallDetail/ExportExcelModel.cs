using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail
{
    public class ExportExcelModel
    {
        [Header(Title = "Phone")]
        public string Phone { get; set; }

        [Header(Title = "fullName")]
        public string fullName { get; set; }
        public string dateStart { get; set; }

        [Header(Title = "date")]
        public string date { get; set; }
        public string dateEnd { get; set; }

        [Header(Title = "cacdac")]
        public string cacdac { get; set; }

        [Header(Title = "ListExportExcel")]
        public List<ExportExcel> ListExportExcel { get; set; }
    }
}