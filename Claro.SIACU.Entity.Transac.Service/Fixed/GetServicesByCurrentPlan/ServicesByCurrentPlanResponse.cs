using System.Runtime.Serialization;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesByCurrentPlan
{
    [DataContract(Name = "ServicesByCurrentPlanResponse")]
    public class ServicesByCurrentPlanResponse
    {
        [DataMember]
        public List<ServiceByCurrentPlan> ServicesByCurrentPlan { get; set; }
    }
}
