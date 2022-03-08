using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPenaltyChangePlans
{
    [DataContract]
    public class PenaltyChangePlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string NroTelephone { get; set; }
        [DataMember]
        public double CodeNewPlan { get; set; }
        [DataMember]
        public DateTime DatePenalty { get; set; }
        [DataMember]
        public double DayXMonth { get; set; }
    }
}
