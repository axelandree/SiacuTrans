using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetail
{
    [DataContract]
    public class CallDetailResponse
    {
        [DataMember]
        public string VTotal { get; set; }
        [DataMember]
        public string StrResponseCode { get; set; }
        [DataMember]
        public string StrResponseMessage { get; set; }
        [DataMember]
        public List<PhoneCall> LstPhoneCall { get; set; }
    }
}
