using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
    //INI - RF-02Evalenzs
    [DataContract(Name = "ConsultarClaroPuntosBodyRequest")]
    public class ConsultarClaroPuntosBodyRequest
    {
        [DataMember]
        public string tipoConsulta { get; set; }
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string bolsa { get; set; }
        [DataMember]
        public string tipoPuntos { get; set; }

    }
 

}
