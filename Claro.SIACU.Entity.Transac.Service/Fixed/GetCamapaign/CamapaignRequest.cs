using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCamapaign
{
    [DataContract]
    public class CamapaignRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Coid { get; set; }
        [DataMember]
        public string Sncode { get; set; }

        [DataMember]
        public int? Active { get; set; }
    }
}
