using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class PackageLItemHelper
    {
        // lista paquetes cliente
        public string key { get; set; }
        public string value { get; set; }

        // lista paquetes 
        public string price { get; set; }
        public string packageId { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string acronym { get; set; }
        public string FechaRegistro { get; set; }
        public string Estado { get; set; }
        public string Periodo { get; set; }
        public string Servicio { get; set; }
    }
}