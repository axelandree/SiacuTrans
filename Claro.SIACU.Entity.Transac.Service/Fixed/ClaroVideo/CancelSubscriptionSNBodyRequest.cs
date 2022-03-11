using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    public class CancelSubscriptionSNBodyRequest
    {
        [DataMember(Name = "cancelAccountRequest")]
        public CancelAccountRequest cancelAccountRequest { get; set; }
    }
}
