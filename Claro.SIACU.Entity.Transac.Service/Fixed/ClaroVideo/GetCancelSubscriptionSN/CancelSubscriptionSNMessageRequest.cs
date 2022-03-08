using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    public class CancelSubscriptionSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public CancelSubscriptionSNHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public CancelSubscriptionSNBodyRequest Body { get; set; }
    }
}
