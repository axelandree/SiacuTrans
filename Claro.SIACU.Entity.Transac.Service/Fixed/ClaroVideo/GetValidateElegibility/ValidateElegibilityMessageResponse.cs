using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateElegibility
{
    public class ValidateElegibilityMessageResponse
    {
        [DataMember(Name = "Header")]
        public ValidateElegibilityHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ValidateElegibilityBodyResponse Body { get; set; }
    }
}
