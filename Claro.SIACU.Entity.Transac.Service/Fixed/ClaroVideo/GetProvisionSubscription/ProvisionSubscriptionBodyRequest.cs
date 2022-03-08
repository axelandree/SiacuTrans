using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetProvisionSubscription
{
    [DataContract(Name = "ProvisionSubscriptionBodyRequest")]
    public class ProvisionSubscriptionBodyRequest
    {
        [DataMember(Name = "provisionarSuscripcionSNRequest")]
        public ProvisionarSuscripcionSNRequest provisionarSuscripcionSNRequest { get; set; }
        
    }
}
