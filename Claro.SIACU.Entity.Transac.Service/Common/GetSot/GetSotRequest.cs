using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSot
{
       [DataContract(Name = "GetSotRequest")]
    public class GetSotRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        [DataMember]
        public string COD_ID { get; set; }
    }
}
