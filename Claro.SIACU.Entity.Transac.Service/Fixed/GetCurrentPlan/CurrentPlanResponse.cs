using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCurrentPlan

{
    [DataContract(Name = "CurrentPlanResponse")]
    public class CurrentPlanResponse
    {
        [DataMember]
        public CurrentPlan Plan { get; set; }
    }
}
           