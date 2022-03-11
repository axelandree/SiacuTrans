using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
//INI - RF-04 - RF-05 Evalenzs
    public class RestConsultarClaroPuntosRequest 
    {
        public RestConsultarClaroPuntosMessageRequest MessageRequest { get; set; }

    }

    public class RestConsultarClaroPuntosMessageRequest
    {
        public RestConsultarClaroPuntosHeader Header { get; set; }

        public RestConsultarClaroPuntosBodyRequest Body { get; set; }
    }

    public class RestConsultarClaroPuntosHeader
    {
        public RestConsultarClaroPuntosHeaderRequest HeaderRequest { get; set; }
    }

    public class RestConsultarClaroPuntosHeaderRequest
    {
        public string consumer { get; set; }

        public string country { get; set; }

        public string dispositivo { get; set; }

        public string language { get; set; }

        public string modulo { get; set; }

        public string msgType { get; set; }
        public string operation { get; set; }

        public string pid { get; set; }

        public string system { get; set; }

        public string timestamp { get; set; }

        public string userId { get; set; }

        public string wsIp { get; set; }

    }

    public class RestConsultarClaroPuntosBodyRequest
    {
        public string tipoConsulta { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string bolsa { get; set; }
       public string tipoPuntos { get; set; }

    }

}
