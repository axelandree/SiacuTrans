using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    [DataContract]
    public class CancelSubscriptionSNRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public CancelSubscriptionSNMessageRequest MessageRequest { get; set; } 
    }
}
