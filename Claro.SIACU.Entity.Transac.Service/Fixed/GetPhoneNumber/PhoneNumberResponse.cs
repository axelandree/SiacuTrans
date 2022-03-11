using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPhoneNumber
{
    public class PhoneNumberResponse
    {
        [DataMember]
        public string NEW_PHONE { get; set; }

        [DataMember]
        public string RESPONSE_CODE { get; set; }

        [DataMember]
        public string RESPONSE_MESSAGE { get; set; }

        [DataMember]
        public string ROUTE_PDF { get; set; }
    }
}
