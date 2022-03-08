using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge
{
    [DataContract(Name = "FixedChargeResponseTransactions")]
    public class FixedChargeResponse
    {
        [DataMember]
        public string CargoFijo { get; set; }
        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public string DescError { get; set; }
    }
}
