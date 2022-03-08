using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDepartmentsPVU
{

  [DataContract(Name = "DepartmentsPvuResponseCommon")]
    public class DepartmentsPvuResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListDepartments { get; set; }
    }
}
