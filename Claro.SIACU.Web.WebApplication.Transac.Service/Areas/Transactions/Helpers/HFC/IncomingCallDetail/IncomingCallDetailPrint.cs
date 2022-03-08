using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail
{
    public class IncomingCallDetailPrint
    {
        public string phone { get; set; }
        public string cacdac { get; set; }
        public string fecStart { get; set; }
        public string fecEnd { get; set; }
        public string fullName { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.CallDetailSummary> LstPhoneCall { get; set; }
    }
}