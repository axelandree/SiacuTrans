using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class RentItemHelper
    {
        public string key { get; set; }
        public string value { get; set; }

        // CAMPOS DE LA GRILLA DE RENTAS 
        public string title { get; set; }
        public string IpAddress { get; set; }
        public string UltimeTimeVisualization { get; set; }
        public string TimeMaxVisualization { get; set; }
        public string DischargeDate { get; set; }
        public string ExpirationDate{ get; set; }
        public string Price { get; set; }
        public string PaymentMethod { get; set; } 
        public string IdRenta { get; set; }
         
    }
}