using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegion
{
    public class RegionResponse
    {
        [DataMember]
        public Entity.Transac.Service.Common.Region ObjRegion { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Common.Region> ListRegion { get; set; }
    }
}
