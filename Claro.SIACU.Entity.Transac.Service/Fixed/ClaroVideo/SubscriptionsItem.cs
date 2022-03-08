using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "SubscriptionsItem")]
    public class SubscriptionsItem
    {
        [DataMember(Name = "price")]
        public string price { get; set; }

        [DataMember(Name = "offerId")]
        public string offerId { get; set; }

        [DataMember(Name = "currency")]
        public string currency { get; set; }

        [DataMember(Name = "idSubscription")]
        public string idSubscription { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "productId")]
        public string productId { get; set; }
        

    }
}
