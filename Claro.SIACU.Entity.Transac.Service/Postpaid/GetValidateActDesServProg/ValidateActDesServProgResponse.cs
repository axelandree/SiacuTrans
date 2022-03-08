using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateActDesServProg
{
    [DataContract(Name = "ValidateActDesServProgResponse")]
    public class ValidateActDesServProgResponse
    {
        [DataMember]
        public string StrCodError { get; set; }
        [DataMember]
        public string StrDesResponse { get; set; }
    }
}
