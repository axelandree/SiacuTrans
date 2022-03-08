using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.IncomingCallsDetail
{
    public class CallDetail : IExcel
    {
        [Header(Title = "Nro")]
        public string NroOrd { get; set; }

        [Header(Title = "NumberA")]
        public string NumberA { get; set; }

        [Header(Title = "Date")]
        public string Date { get; set; }
        [Header(Title = "StartHour")]
        public string StartHour { get; set; }
        [Header(Title = "NumberB")]
        public string NumberB { get; set; }
        [Header(Title = "Duration")]
        public string Duration { get; set; }
    }
}