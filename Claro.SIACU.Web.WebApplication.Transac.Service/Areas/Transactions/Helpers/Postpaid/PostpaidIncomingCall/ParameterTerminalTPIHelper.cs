using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall
{
    public class ParameterTerminalTPIHelper
    {
        public string ParameterID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ValorC { get; set; }
        public double ValorN { get; set; }
        public string ValorL { get; set; }
    }
}