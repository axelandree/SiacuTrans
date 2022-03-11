using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes 
{
//INI - RF-02
    public class RestComprarPaquetesRequest 
    {
        public RestComprarPaquetesMessageRequest MessageRequest { get; set; }

    }

    public class RestComprarPaquetesMessageRequest
    {
        public RestComprarPaquetesHeader Header { get; set; }

        public RestComprarPaquetesBodyRequest Body { get; set; }
    }

    public class RestComprarPaquetesHeader
    {
        public RestComprarPaquetesHeaderRequest HeaderRequest { get; set; }
    }

    public class RestComprarPaquetesHeaderRequest
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


    public class RestComprarPaquetesBodyRequest
    {
      public comprarPaquetesRequest comprarPaquetesRequest { get; set; }
      
    }

    public class comprarPaquetesRequest
    {
        public string msisdn { get; set; }
        public string monto { get; set; }
        public string paquete { get; set; }
        public string customerId { get; set; }
        public string planBase { get; set; }
        public string tipoProducto { get; set; }
        public string tipoCliente { get; set; }
        public string cicloFact { get; set; }
        public string fechaAct { get; set; }
        public string cargoFijo { get; set; }
        public string tipoPago { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public List<ComprarPaquetesListaOpcionalType> listaOpcionalType { get; set; }
      
    }
}
