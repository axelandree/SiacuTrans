using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBuildingsPVU
{

     [DataContract(Name = "BuildingsResponsePvuCommon")]
    public class BuildingsPvuResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListBuildings { get; set; }
    }
}
