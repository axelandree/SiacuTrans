using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "ValidarElegibilidadRequest")]
    public class ValidarElegibilidadRequest
    {
        [DataMember(Name = "medioPago")]
        public string medioPago { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }

        [DataMember(Name = "producto")]
        public string producto { get; set; }

        [DataMember(Name = "productoId")]
        public string productoId { get; set; }

    }
}
