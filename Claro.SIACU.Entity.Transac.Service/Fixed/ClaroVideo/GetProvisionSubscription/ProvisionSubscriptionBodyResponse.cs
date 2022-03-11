using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    [DataContract(Name = "ProvisionSubscriptionBodyResponse")]
    public class ProvisionSubscriptionBodyResponse
    {
        [DataMember(Name = "operatorProvisioningProductResponse")]
        public OperatorProvisioningProductResponse operatorProvisioningProductResponse { get; set; }

    }
}
