using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid
{
    public class ListUserSubscription
    {
        public string descripcion { get; set; }
        public string origen { get; set; }
        public string ipUsuario { get; set; }
        public string idSuscripcion { get; set; }
        public string idRefSuscripcion { get; set; }
        public string fechaAlta { get; set; }
        public string fechaExpiracion { get; set; }
        public string fechaCancelacion { get; set; }
        public string precio { get; set; }
        public string medioPago { get; set; }
        public string estadoPago { get; set; }
        public string detalleAccion { get; set; }
        public string activo { get; set; }
        public string moneda { get; set; }
        public string productId { get; set; }
        public string detallePago { get; set; }       
        
    }
}