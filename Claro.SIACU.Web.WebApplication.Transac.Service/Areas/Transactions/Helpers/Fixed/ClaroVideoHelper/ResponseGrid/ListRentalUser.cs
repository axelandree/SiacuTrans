using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid
{
    public class ListRentalUser
    {
        public string descripcion { get; set; }
        public string ipUsuario { get; set; }
        public string ultimaVisualizacion { get; set; }
        public string tiempoMaximoVisualizacion { get; set; }
        public string fechaAlta { get; set; }
        public string fechaExpiracion { get; set; }
        public string precio { get; set; }
        public string medioPago { get; set; }
        public string idRenta { get; set; }
        public string idRefRenta { get; set; }
        public string moneda { get; set; }
    }
}