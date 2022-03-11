using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    [DataContract]
    public class ProvisionSubscriptionRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ProvisionSubscriptionMessageRequest MessageRequest { get; set; } 
    }
}
