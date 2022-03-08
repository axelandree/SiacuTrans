using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBuildingsPVU
{

    [DataContract(Name = "BuildingsPvuRequestCommon")]
    public class BuildingsPvuRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodFlant { get; set; }
        [DataMember]
        public string CodBuildings { get; set; }
    }
}
