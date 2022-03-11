using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge
{
    public class RechargeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string START_DATE { get; set; }

        [DataMember]
        public string END_DATE { get; set; }

        [DataMember]
        public string FLAG { get; set; }

        [DataMember]
        public int NUMBER_REG { get; set; }
    }
}
