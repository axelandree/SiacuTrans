using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetMigratedElements
{
    [DataContract(Name = "MigratedElementsResponse")]
    public class MigratedElementsResponse
    {
        [DataMember]
        public string Flag_Consult { get; set; }
        [DataMember]
        public string  Message { get; set; }
        [DataMember]
        public List<ListItem> ListMigratedElements { get; set; }
    }
}
