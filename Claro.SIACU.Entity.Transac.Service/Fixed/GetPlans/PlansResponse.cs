using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlans
{
    [DataContract(Name = "PlansResponseHfc")]
    public class PlansResponse
    {
        [DataMember]
        public List<ProductPlan> listPlan { get; set; }
    }
}
