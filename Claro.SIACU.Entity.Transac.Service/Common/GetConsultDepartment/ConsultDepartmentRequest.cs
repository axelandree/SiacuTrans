using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultDepartment
{
    [DataContract(Name = "ConsultDepartmentRequestPrepaid")]
    public class ConsultDepartmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public int CodRegion { get; set; }
        [DataMember]
        public int CodDepartment { get; set; }
        [DataMember]
        public int CodState { get; set; }
    }
}
