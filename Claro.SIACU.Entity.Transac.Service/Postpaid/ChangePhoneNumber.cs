using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class ChangePhoneNumber
    {
        [DataMember]
        public bool RESPONSE { get; set; }
        [DataMember]
        public string RESPONSE_MESSAGE { get; set; }
        [DataMember]
        public string RESPONSE_CODE { get; set; }
    }
}
