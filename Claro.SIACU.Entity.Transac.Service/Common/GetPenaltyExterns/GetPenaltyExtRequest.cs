using System;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetPenaltyExterns
{
    [DataContract]
    public class GetPenaltyExtRequest : Claro.Entity.Request
    {
        [DataMember]
        public string NroTelephone { get; set; }
        [DataMember]
        public DateTime DatePenalty { get; set; }
        [DataMember]
        public double NroFacture { get; set; }
        [DataMember]
        public double CargFixedCurrent { get; set; }
        [DataMember]
        public double CargFixedNewPlan { get; set; }
        [DataMember]
        public double DayXMonth { get; set; }
        [DataMember]
        public double CodeNewPlan { get; set; }
    }
}
