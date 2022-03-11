using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    public class SearchStateLineEmailBodyResponse
    {
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "datosCliente")]
        public DataClient datosCliente { get; set; }

    }
}
