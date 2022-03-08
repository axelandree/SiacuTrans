using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetUpdateStateLineEmail
{
    public class UpdateStateLineEmailBodyRequest
    {
        [DataMember(Name = "tipoOperacion")]
        public int tipoOperacion { get; set; }

        [DataMember(Name = "listaServicios")]
        public List<ListServices> listaServicios { get; set; }

        [DataMember(Name = "origenFuente")]
        public string origenFuente { get; set; }

        [DataMember(Name = "usuario")]
        public string usuario { get; set; }

        [DataMember(Name = "fechaRegistro")]
        public string fechaRegistro { get; set; }
    }
}
