using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    public class ProvisionSubscriptionMessageRequest
    {
        [DataMember(Name = "Header")]
        public ProvisionSubscriptionHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ProvisionSubscriptionBodyRequest Body { get; set; }
    }
}
