using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
//INI - RF-02 Evalenzs
    [DataContract(Name = "ConsultarClaroPuntosRequest")]
    public class ConsultarClaroPuntosRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ConsultarClaroPuntosMessageRequest MessageRequest { get; set; }      

        [DataMember(Name = "tipoConsulta")]
        public string tipoConsulta { get; set; }
        [DataMember(Name = "tipoDocumento")]
        public string tipoDocumento { get; set; }
        [DataMember(Name = "numeroDocumento")]
        public string numeroDocumento { get; set; }
        [DataMember(Name = "bolsa")]
        public string bolsa { get; set; }
        [DataMember(Name = "tipoPuntos")]
        public string tipoPuntos { get; set; }

    }
}
