using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive
{
    [DataContract(Name = "SubMotiveRequest")]
    public class SubMotiveRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdArea { get; set; }
        [DataMember]
        public string strIdMotive { get; set; }
    }
}
