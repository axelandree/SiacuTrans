using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlanCommercial
{
    [DataContract]
    public class PlanCommercialResponse
    {
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMessage { get; set; }   
    }
}
