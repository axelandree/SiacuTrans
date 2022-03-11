using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class InboundEtaPropiedades
    {
        [DataMember]
        public string modoCargaPropiedades { get; set; }
        [DataMember]
        public string modoProcesamiento { get; set; }
        [DataMember]
        public string tipoCarga { get; set; }
        [DataMember]
        public DateTime fechaTransaccion { get; set; }
        [DataMember]
        public string[] configuracionSOT { get; set; }
        [DataMember]
        public string[] configuracionInventario { get; set; }
    }
}
