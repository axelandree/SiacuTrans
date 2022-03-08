using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProductTracDeacServ
{
    [DataContract]
    public class ProductTracDeacServRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vstrIdentificador { get; set; }
        [DataMember]
        public string vstrCoId { get; set; }
        [DataMember]
        public bool vod { get; set; }
        [DataMember]
        public bool match { get; set; }
    }
}
