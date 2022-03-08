using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetOccupationClient
{
    [DataContract(Name = "OccupationClientResponsePrepaid")]
    public class OccupationClientResponse
    {
        [DataMember]
        public List<ListItem> ListOccupationClient { get; set; }
    }
}
