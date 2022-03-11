using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterTerminalTPI
{
    [DataContract(Name = "ParameterTerminalTPIRequest")]
    public class ParameterTerminalTPIRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ParameterID { get; set; }

        //[DataMember]
        //public string Message { get; set; }
    }
}
