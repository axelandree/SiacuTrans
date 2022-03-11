using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    [DataContract(Name = "CancelSubscriptionSNBodyRequest")]
    public class CancelSubscriptionSNBodyRequest
    {
        [DataMember(Name = "cancelarSuscripcionSNRequest")]
        public CancelarSuscripcionSNRequest cancelarSuscripcionSNRequest { get; set; }
    }

}
