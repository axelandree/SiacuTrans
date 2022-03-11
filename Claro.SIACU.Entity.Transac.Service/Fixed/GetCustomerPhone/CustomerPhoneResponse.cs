using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCustomerPhone
{
    [DataContract]
    public class CustomerPhoneResponse
    {
        [DataMember]
        public List<GenericItem> LstCustomerPhone { get; set; }
    }
}
