using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetPenaltyExterns
{
    [DataContract]
    public class GetPenaltyExtResponse
    {

        [DataMember]
        public string NroTelephone { get; set; }
        [DataMember]
        public bool Result { get; set; }
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

    }
}
