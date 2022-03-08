using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "ContractByPhoneNumber")]
    public class ContractByPhoneNumber
    {
        [DataMember]
        public string CustCod { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string CodId { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Reason { get; set; }
    }
}
