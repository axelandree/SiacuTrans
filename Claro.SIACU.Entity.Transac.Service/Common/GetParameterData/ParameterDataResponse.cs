using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetParameterData
{
    [DataContract(Name = "ParameterDataResponseCommon")]
    public class ParameterDataResponse
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public ParameterData Parameter { get; set; }
    }
}
