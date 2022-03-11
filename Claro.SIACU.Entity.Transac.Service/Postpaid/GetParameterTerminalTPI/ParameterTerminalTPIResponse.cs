using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterTerminalTPI
{
    [DataContract(Name = "ParameterTerminalTPIResponse")]
    public class ParameterTerminalTPIResponse
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public List<ParameterTerminalTPI> ListParameterTeminalTPI { get; set; }
    }
}
