using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetServicesVAS
{
    [DataContract(Name = "ServicesVASResponse")]
    public class ServicesVASResponse
    {
        [DataMember]
        public List<ListItem> ListServicesVAS { get; set; }
    }
}
