using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS
{
    [DataContract(Name = "TypeTransactionBRMSRequest")]
    public class TypeTransactionBRMSRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrOperationCodSubClass { get; set; }
        [DataMember]
        public string StrTransactionM { get; set; }
        [DataMember]
        public string StrRetention { get; set; }
        [DataMember]
        public string StrIdentifier { get; set; }
    }
}
