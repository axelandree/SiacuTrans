using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "subscription")]
    public class Subscription
    {
        [DataMember(Name = "item")]
        public List<SubscriptionItem> item { get; set; }
    }
}
