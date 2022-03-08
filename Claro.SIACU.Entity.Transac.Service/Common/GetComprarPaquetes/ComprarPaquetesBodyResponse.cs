using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes 
{
    //INI - RF-02 Evalenzs
    [DataContract(Name = "ComprarPaquetesBodyResponse")]
    public class ComprarPaquetesBodyResponse
    {
       

    [DataMember(Name = "comprarPaqueteResponseType")]
        public comprarPaqueteResponseType comprarPaqueteResponseType { get; set; }
  
    }
    public class comprarPaqueteResponseType {

        public responseStatus responseStatus { get; set; }
    }
    public class responseStatus
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }
    }
}
