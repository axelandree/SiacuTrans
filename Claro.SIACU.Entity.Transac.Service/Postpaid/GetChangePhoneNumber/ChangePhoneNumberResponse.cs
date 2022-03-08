using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber
{
    public class ChangePhoneNumberResponse
    {
        public List<Entity.Transac.Service.Postpaid.SimCardPhone> LstSimCardPhone { get; set; }

        public List<Entity.Transac.Service.Postpaid.ChangePhoneNumber> LstChangePhoneNumber { get; set; }

        public Entity.Transac.Service.Postpaid.ChangePhoneNumber objChangePhoneNumber { get; set; }

        [DataMember]
        public bool RESPONSE { get; set; }
        [DataMember]
        public string RESPONSE_MESSAGE { get; set; }
        [DataMember]
        public string RESPONSE_CODE { get; set; }
    }
}
