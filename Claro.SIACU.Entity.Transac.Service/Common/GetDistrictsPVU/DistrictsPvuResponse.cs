using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDistrictsPVU
{

    [DataContract(Name = "DistrictsPvuResponseCommon")]
    public class DistrictsPvuResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListDistricts { get; set; }
    }
}
