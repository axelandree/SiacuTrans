using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVBodyResponse
    {
        [DataMember(Name = "mensajeError")]
        public string MensajeError { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string CodigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string MensajeRespuesta { get; set; }
        [DataMember(Name = "flagAplica")]
        public int FlagAplica { get; set; }
    }
}
