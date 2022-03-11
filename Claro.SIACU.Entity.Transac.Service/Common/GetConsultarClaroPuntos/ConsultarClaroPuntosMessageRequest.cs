using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarClaroPuntosMessageRequest")]
    public class ConsultarClaroPuntosMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultarClaroPuntosHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultarClaroPuntosBodyRequest Body { get; set; }
    }          
}
