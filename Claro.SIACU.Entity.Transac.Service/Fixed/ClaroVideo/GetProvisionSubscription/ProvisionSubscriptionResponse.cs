using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
     [DataContract]
    public class ProvisionSubscriptionResponse
    {
         [DataMember(Name = "MessageResponse")]
         public ProvisionSubscriptionMessageResponse MessageResponse { get; set; }
    }
}
