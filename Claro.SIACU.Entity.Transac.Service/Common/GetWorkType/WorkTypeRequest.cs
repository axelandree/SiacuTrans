using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetWorkType
{
    [DataContract(Name = "WorkTypeRequestCommon")]
    public class WorkTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public int TransacType { get; set; }
    }
}
