using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa
{
    [DataContract(Name = "EtaInboundToaRequest")]
    public class EtaInboundToaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string pIdTrasaccion { get; set; }
        [DataMember]
        public string pIP_APP { get; set; }
        [DataMember]
        public string pAPP { get; set; }
        [DataMember]
        public string pUsuario { get; set; }
        [DataMember]
        public InboundEtaComandos comando { get; set; }
        [DataMember]
        public InboundEtaPropiedades propiedades { get; set; }
        [DataMember]
        public InboundEtaOrdenTrabajo ordenTrabajo { get; set; }
    }
}
