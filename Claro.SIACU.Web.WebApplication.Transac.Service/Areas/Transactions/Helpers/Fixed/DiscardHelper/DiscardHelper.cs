using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper
{
    public class DiscardHelper
    {

        public string id_descarte { get; set; }
        public string nombre_variable { get; set; }
        public string desc_descarte { get; set; }
        public string tipo_descarte { get; set; }
        public string flag_descarte { get; set; }
        public string orden_descarte { get; set; }
        public string fecha_reg { get; set; }
        public string id_grupo { get; set; }
        public string flag_Error { get; set; }
        public string flag_OK { get; set; }
        public string descarteValor { get; set; }
        public List<DiscardListValueHelper> descarteListaValor { get; set; }
        

    }
}