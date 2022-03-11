using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetHubs
{
    [DataContract(Name = "GetHubsRequest")]
    public class GetHubsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCustomerId { get; set; }
        [DataMember]
        public string strContrato { get; set; }
    }
}
