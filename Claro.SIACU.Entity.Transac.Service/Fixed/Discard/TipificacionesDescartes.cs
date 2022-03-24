using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    //INICIATIVA-986
    public class TipificacionesDescartes
    {
        public string ContactCode { get; set; }
        public string Linea { get; set; }
        public string TipoVenta { get; set; }
        public string TipoInconveniente { get; set; }
        public string Notas { get; set; }
        public string InteractionId { get; set; }
    }
}
