using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlanServices
{
    [DataContract]
    public class PlanServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdPlan { get; set; }
        [DataMember]
        public string CodeProduct { get; set; }
        [DataMember]
        public string TypeProduct { get; set; }
        [DataMember]
        public string ArrServices { get; set; }
    }
}
