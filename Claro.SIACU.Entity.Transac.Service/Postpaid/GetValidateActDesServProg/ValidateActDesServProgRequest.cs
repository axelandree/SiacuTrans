using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateActDesServProg
{
    [DataContract(Name = "ValidateActDesServProgRequest")]
    public class ValidateActDesServProgRequest:Claro.Entity.Request
    {
        [DataMember]
        public string StrIdTransaction { get; set; }
        [DataMember]
        public string StrIpAplication { get; set; }
        [DataMember]
        public string StrAplication { get; set; }
        [DataMember]
        public string StrMsisdn { get; set; }
        [DataMember]
        public string StrCoId { get; set; }
        [DataMember]
        public string StrCoSer { get; set; }
        [DataMember]
        public string StrTypeReg { get; set; }
        [DataMember]
        public string StrCodServ { get; set; }
    }
}
