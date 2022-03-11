using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class DecoCustomer
    {
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string ContactObjId { get; set; }
        [DataMember]
        public string FlagReg { get; set; }
    }
}
