using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle
{
    [DataContract(Name = "BillingCycleResponse")]
    public class BillingCycleResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.BillingCycle> LstBillingCycleResponse { get; set; }
    }
}
