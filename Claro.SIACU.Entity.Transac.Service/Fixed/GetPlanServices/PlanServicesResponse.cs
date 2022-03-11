using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlanServices
{
    [DataContract]
    public class PlanServicesResponse
    {
        [DataMember]
        public List<PlanService> LstPlanServices { get; set; }
    }
}
