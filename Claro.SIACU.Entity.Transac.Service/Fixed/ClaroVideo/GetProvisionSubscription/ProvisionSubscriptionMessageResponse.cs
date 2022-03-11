using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    public class ProvisionSubscriptionMessageResponse
    {
        [DataMember(Name = "Header")]
        public ProvisionSubscriptionHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ProvisionSubscriptionBodyResponse Body { get; set; }
    }
}
