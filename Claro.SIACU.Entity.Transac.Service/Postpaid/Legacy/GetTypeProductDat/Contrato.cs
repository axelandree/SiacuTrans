using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class Contrato
    {
        [DataMember(Name = "idContrato")]
        public string idContrato { get; set; }
        [DataMember(Name = "fechaActivacion")]
        public string fechaActivacion { get; set; }
        [DataMember(Name = "idPublicoContrato")]
        public string idPublicoContrato { get; set; }
        [DataMember(Name = "estadoContrato")]
        public string estadoContrato { get; set; }
        [DataMember(Name = "ofertaProducto")]
        public List<OfertaProducto> ofertaProducto { get; set; }
        [DataMember(Name = "cuentaFacturacion")]
        public CuentaFacturacion cuentaFacturacion { get; set; }
        [DataMember(Name = "caracteristicaAdicional")]
        public List<CaracteristicaAdicional> caracteristicaAdicional { get; set; }
    }
}
