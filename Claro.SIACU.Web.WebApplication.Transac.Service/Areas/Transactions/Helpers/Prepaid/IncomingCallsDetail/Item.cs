using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.IncomingCallsDetail
{
    public class Item
    {
        public string Name { get; set; }
        public string Balance { get; set; }
        public string ExpirationDate { get; set; }
        public string Order { get; set; }

         
        public string Code { get; set; } 
        public string Code2 { get; set; } 
        public string Description { get; set; } 
        public int Number { get; set; }
    }
}