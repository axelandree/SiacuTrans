using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.SendNewPlanServices
{
    [DataContract(Name = "NewPlanServicesRequest")]
    public class NewPlanServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public List<ServiceByPlan> Services { get; set; }
    }
}
