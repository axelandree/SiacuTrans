using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR
{
    public class HLRRequest : Claro.Entity.Request
    {
        [DataMember]
        public string PHONE_NUMBER { get; set; }

        [DataMember]
        public string RANGE_TYPE { get; set; }
    }
}
