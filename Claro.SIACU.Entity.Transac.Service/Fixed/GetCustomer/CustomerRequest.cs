using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCustomer
{
    [DataContract]
    public class GetCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vPhone { get; set; }
        [DataMember]
        public string vAccount { get; set; }
        [DataMember]
        public string vContactobjid1 { get; set; }
        [DataMember]
        public string vFlagReg { get; set; }
    }
}
