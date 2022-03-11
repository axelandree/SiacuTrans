using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS
{
    [DataContract(Name = "TypeTransactionBRMSResponse")]
    public class TypeTransactionBRMSResponse
    {
        [DataMember]
        public string StrError { get; set; }
        [DataMember]
        public string StrResult { get; set; }
    }
}
