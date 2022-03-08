using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetWorkSubType
{

    [DataContract(Name = "WorkSubTypeRequestCommon")]
    public class WorkSubTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodTypeWork { get; set; }


    }
}
