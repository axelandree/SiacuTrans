using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "subscriptions")]
    public class Subscriptions
    {
        [DataMember(Name = "item")]
        public List<SubscriptionsItem> item { get; set; }
    }
}
