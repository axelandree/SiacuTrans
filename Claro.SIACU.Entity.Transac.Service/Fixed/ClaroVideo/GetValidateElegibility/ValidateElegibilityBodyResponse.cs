using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
      [DataContract(Name = "ValidateElegibilityBodyResponse")]
    public class ValidateElegibilityBodyResponse
    {
          [DataMember(Name = "validarElegibilidadResponse")]
          public ValidarElegibilidadResponse validarElegibilidadResponse { get; set; }

    }
}
