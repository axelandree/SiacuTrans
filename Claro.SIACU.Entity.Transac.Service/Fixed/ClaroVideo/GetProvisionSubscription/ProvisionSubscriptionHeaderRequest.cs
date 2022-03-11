using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    public class ProvisionSubscriptionHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
