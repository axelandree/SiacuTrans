using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
    [DataContract(Name = "ValidateElegibilityBodyRequest")]
    public class ValidateElegibilityBodyRequest
    {
        [DataMember(Name = "validarElegibilidadRequest")]
        public ValidarElegibilidadRequest validarElegibilidadRequest { get; set; }

    }
}
