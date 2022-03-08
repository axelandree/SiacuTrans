using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall
{
    public class QueryAssociatedLines
    {
        public string NroOrd { get; set; }

        public string MSISDN { get; set; }
        public string CallNumber { get; set; }

        public string CallDate { get; set; }
        public string CallTime { get; set; }
        public string CallDuration { get; set; }
    }
}