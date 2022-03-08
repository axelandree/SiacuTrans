using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
    //INI - RF-02 Evalenzs
    [DataContract(Name = "ConsultarClaroPuntosBodyResponse")]
    public class ConsultarClaroPuntosBodyResponse
    {
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeError")]
        public string mensajeError { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "saldo")]
        public string saldo { get; set; }
    }

}
