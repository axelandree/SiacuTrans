using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
    [DataContract]
    public class ValidateElegibilityResponse
    {
        [DataMember(Name = "MessageResponse")]
          public ValidateElegibilityMessageResponse MessageResponse { get; set; }
    }
}
