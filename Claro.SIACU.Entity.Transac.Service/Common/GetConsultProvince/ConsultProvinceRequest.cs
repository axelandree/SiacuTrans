using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultProvince
{
    [DataContract(Name = "ConsultProvinceRequest")]
    public class ConsultProvinceRequest : Claro.Entity.Request
    {
        [DataMember]
        public int CodDepartment { get; set; }
        [DataMember]
        public int CodProvince { get; set; }
        [DataMember]
        public int CodState { get; set; }
    }
}
