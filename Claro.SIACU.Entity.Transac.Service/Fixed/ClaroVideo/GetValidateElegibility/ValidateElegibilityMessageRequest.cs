using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
    public class ValidateElegibilityMessageRequest
    {
        [DataMember(Name = "Header")]
        public ValidateElegibilityHeaderRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public ValidateElegibilityBodyRequest Body { get; set; }
    }

}
