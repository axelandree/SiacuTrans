using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetNumberEAI
{
    [DataContract(Name = "NumberEAIRequest")]
    public class NumberEAIRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
    }
}
