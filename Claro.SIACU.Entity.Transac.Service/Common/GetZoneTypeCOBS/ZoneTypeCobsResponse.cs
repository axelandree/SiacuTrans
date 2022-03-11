using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetZoneTypeCOBS
{
     [DataContract(Name = "ZoneTypeCobsResponseCommon")]
        public class ZoneTypeCobsResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListZoneType { get; set; }
    }
}
