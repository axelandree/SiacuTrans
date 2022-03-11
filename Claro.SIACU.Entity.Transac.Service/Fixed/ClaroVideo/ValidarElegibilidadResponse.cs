using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "ValidarElegibilidadResponse")]
    public class ValidarElegibilidadResponse
    {
        [DataMember(Name = "codError")]
        public string codError { get; set; }

        [DataMember(Name = "msgError")]
        public string msgError { get; set; }

        [DataMember(Name = "medioPago")]
        public string medioPago { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }

        [DataMember(Name = "listadoServicios")]
        public List<ListServicesElegibility> listadoServicios { get; set; }
    }
}
