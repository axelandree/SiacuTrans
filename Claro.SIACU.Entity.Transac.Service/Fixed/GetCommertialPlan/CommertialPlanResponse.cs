using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCommertialPlan
{
    [DataContract]
    public class CommertialPlanResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rCodigoPlan { get; set; }
        [DataMember]
        public int rintCodigoError { get; set; }
        [DataMember]
        public string rstrDescripcionError { get; set; }
    }
}
