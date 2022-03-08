using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "subscriptionList")]
    public class SubscriptionList
    {
        [DataMember(Name = "subscription")]
        public List<Subscription> subscription { get; set; }
    }
}
