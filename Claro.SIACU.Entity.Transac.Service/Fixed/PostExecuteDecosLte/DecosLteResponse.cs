using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostExecuteDecosLte
{
    [DataContract]
    public class DecosLteResponse
    {
        [DataMember]
        public string SotNumber { get; set; }
        [DataMember]
        public string CodeInteraction { get; set; }
        [DataMember]
        public string UrlConstancy { get; set; }
        [DataMember]
        public string ResponseCode { get; set; }
        [DataMember]
        public string ResponseMessage { get; set; }
    }
}
