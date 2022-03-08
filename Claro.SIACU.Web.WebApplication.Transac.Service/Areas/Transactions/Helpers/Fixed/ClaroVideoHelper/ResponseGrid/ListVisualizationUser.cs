using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid
{
    public class ListVisualizationUser
    {
        public string idContenido { get; set; }
        public string titulo { get; set; }
        public string ipUsuario { get; set; }
        public string fechaTiempoVisualizacion { get; set; }
        public string ultimoTiempoVisualizacion { get; set; }
        public string fechaMaximoVisualizacion { get; set; }
        public string tiempoMaximoVisualizacion { get; set; }
    }
}