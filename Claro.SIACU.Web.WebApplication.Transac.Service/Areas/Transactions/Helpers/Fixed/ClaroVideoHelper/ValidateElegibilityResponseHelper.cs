using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class ValidateElegibilityResponseHelper
    {       
        public string codError { get; set; }      
        public string msgError { get; set; }        
        public string medioPago { get; set; }      
        public string tipoLinea { get; set; }
        public List<ListServicesHelper> listadoServicios { get; set; }
    }
}