using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa
{
    public class EtaInboundToaResponse
    {
        public string idAgenda { get; set; }
        public string idETA { get; set; }
        public string resultadoOperacion { get; set; }
        public string tipoMensaje { get; set; }
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
    }
}
