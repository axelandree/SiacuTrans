using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultDepartment
{
    [DataContract(Name = "ConsultDepartmentResponsePrepaid")]
    public class ConsultDepartmentResponse
    {
        [DataMember]
        public List<ConsultDepartment> ListConsultDepartment { get; set; }
    }
}
