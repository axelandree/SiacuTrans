using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPenaltyChangePlans
{
    [DataContract]
    public class PenaltyChangePlanResponse
    {
        [DataMember]
        public double NroFacture { get; set; }
        [DataMember]
        public double CargCurrentFixed { get; set; }
        [DataMember]
        public double CargFixedNewPlan { get; set; }
        [DataMember]
        public double AgreementIdExit { get; set; }
        [DataMember]
        public double DayPendient { get; set; }
        [DataMember]
        public double CargFiexdDiar { get; set; }
        [DataMember]
        public double PrecList { get; set; }
        [DataMember]
        public double PrecVent { get; set; }
        [DataMember]
        public double PenaltyPcs { get; set; }
        [DataMember]
        public double PenaltyApadece { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }
}
