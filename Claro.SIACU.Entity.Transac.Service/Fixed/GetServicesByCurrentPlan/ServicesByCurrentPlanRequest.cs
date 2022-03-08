using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesByCurrentPlan
{
    [DataContract(Name = "ServicesByCurrentPlanRequest")]
    public class ServicesByCurrentPlanRequest : Claro.Entity.Request
    {

        [DataMember]
        public string ContractId { get; set; }
    }
}
