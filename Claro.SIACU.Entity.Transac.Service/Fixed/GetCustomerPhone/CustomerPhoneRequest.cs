using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCustomerPhone
{
    [DataContract]
    public class CustomerPhoneRequest : Claro.Entity.Request
    {
        [DataMember]
        public int IdContract { get; set; }
    }
}
