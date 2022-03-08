using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSot
{
    [DataContract(Name = "GetSotResponseCommon")]
    public class GetSotResponse
    {
        [DataMember]
        public List<ListItem> ListSot { get; set; }
    }
}
