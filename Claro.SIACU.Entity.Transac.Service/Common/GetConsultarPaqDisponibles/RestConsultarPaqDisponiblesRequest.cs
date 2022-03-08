using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
//INI - RF-02
    public class RestConsultarPaqDisponiblesRequest 
    {
        public RestConsultarPaqDisponiblesMessageRequest MessageRequest { get; set; }

    }

    public class RestConsultarPaqDisponiblesMessageRequest
    {
        public RestConsultarPaqDisponiblesHeader Header { get; set; }

        public RestConsultarPaqDisponiblesBodyRequest Body { get; set; }
    }

    public class RestConsultarPaqDisponiblesHeader
    {
        public RestConsultarPaqDisponiblesHeaderRequest HeaderRequest { get; set; }
    }

    public class RestConsultarPaqDisponiblesHeaderRequest
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

    public class RestConsultarPaqDisponiblesBodyRequest
    {
        public string idCategoria { get; set; }

        public string idContrato { get; set; }

        public string codigoCategoria { get; set; }
        public string prepagoCode { get; set; }

        public string tmCode { get; set; }

    }


}
