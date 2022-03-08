using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarClaroPuntosResponse")]
    public class ConsultarClaroPuntosResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ConsultarClaroPuntosMessageResponse MessageResponse { get; set; }
    }
}
