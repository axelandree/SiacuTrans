using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetProvincesPVU
{
     [DataContract(Name = "ProvincesPvuResponseCommon")]
    public class ProvincesPvuResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListProvinces { get; set; }
    }
}
