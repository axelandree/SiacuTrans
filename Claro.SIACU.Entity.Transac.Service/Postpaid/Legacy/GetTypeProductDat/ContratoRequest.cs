using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class ContratoRequest
    {
        [DataMember(Name = "idContrato")]
        public string idContrato { get; set; }
        [DataMember(Name = "ofertaProducto")]
        public OfertaProducto ofertaProducto { get; set; }
        [DataMember(Name = "caracteristicaAdicional")]
        public CaracteristicaAdicional caracteristicaAdicional { get; set; }
    }
}
