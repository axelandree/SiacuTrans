using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class SimCardPhone
    {
        [DataMember]
        public bool RESPONSE { get; set; }

        [DataMember]
        public string RESPONSE_SERVICE { get; set; }

        [DataMember]
        public string RESPONSE_MESSAGE { get; set; }

        [DataMember]
        public string NRO_TELEF { get; set; }

        [DataMember]
        public string CODIG_HLR { get; set; }

        [DataMember]
        public string DESC_CLRED { get; set; }

        [DataMember]
        public string DESC_REGIO { get; set; }

        [DataMember]
        public string DESC_TPCLI { get; set; }

        [DataMember]
        public string DESC_TPTLF { get; set; }
    }
}
