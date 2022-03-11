using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class HistorialServDispCVResponseHelper
    {
        public string codError { get; set; }
        public string messageError { get; set; }
        public List<PHistorialServHelper> pHistorialServ { get; set; }
        public List<PHistorialDispHelper> pHistorialDisp { get; set; }
        public List<PEstadoPagoServHelper> pEstadoPagoServ { get; set; }
    }
}