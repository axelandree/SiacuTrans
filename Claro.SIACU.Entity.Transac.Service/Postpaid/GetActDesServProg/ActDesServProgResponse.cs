using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg
{
    [DataContract(Name = "ActDesServProgResponse")]
    public class ActDesServProgResponse
    {
        [DataMember]
        public string StrCodError { get; set; }
        [DataMember]
        public string StrDesResponse { get; set; }
        [DataMember]
        public bool BlnResposne { get; set; }
    }
}
