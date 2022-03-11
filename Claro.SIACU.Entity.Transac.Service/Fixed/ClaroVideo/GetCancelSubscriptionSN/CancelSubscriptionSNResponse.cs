using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    [DataContract]
    public class CancelSubscriptionSNResponse
    {
        [DataMember(Name = "MessageResponse")]
        public CancelSubscriptionSNMessageResponse MessageResponse { get; set; }
    }
}
