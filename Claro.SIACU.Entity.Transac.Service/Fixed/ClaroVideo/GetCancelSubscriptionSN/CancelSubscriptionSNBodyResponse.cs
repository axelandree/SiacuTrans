using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetCancelSubscriptionSN
{
    [DataContract(Name = "CancelSubscriptionSNBodyResponse")]
    public class CancelSubscriptionSNBodyResponse
    {
        [DataMember(Name = "cancelAccountResponse")]
        public CancelAccountResponse cancelAccountResponse { get; set; }

    }
}
