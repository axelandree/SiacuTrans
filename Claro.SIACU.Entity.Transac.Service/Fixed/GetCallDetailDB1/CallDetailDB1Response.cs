using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailDB1
{
    [DataContract]
    public class CallDetailDB1Response
    {
        [DataMember]
        public string vTOTAL { get; set; }
        [DataMember]
        public string MSGERROR { get; set; }
        [DataMember]
        public List<PhoneCall> LstPhoneCall { get; set; }
    }
}
