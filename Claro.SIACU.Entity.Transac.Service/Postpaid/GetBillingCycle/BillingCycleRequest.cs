using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle
{
    [DataContract(Name = "BillingCycleRequest")]
    public class BillingCycleRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTypeCustomer { get; set; }
    }
}
