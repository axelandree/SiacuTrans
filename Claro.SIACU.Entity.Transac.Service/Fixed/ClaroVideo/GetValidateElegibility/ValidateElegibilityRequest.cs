using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
    [DataContract]
    public class ValidateElegibilityRequest : Tools.Entity.Request
    {
         [DataMember(Name = "MessageRequest")]
         public ValidateElegibilityMessageRequest MessageRequest { get; set; }
    }
}
