using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCommertialPlan
{
    [DataContract]
    public class CommertialPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrCoId { get; set; }
    }
}
