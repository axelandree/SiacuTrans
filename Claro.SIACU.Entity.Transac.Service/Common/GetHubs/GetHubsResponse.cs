using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetHubs
{
    [DataContract(Name = "GetHubsResponse")]
    public class GetHubsResponse
    {
        [DataMember]
        public List<BEHub> listHub { get; set; }
    }
}
