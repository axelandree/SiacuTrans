using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "PhoneContractPostPaid")]
    public class PhoneContract
    {
        [DataMember]
        public string CUSTCODE { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public string COID { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string FECHA { get; set; }
        [DataMember]
        public string RAZON { get; set; }

    }
}
