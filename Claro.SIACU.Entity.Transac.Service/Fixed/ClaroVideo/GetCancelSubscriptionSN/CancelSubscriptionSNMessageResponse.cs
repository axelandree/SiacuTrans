using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    public class CancelSubscriptionSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public CancelSubscriptionSNHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public CancelSubscriptionSNBodyResponse Body { get; set; }
    }
}
