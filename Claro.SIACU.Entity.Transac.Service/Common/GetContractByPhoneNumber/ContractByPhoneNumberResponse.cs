using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber
{
    [DataContract(Name = "ContractByPhoneNumberResponseCommon")]
    public class ContractByPhoneNumberResponse
    {
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public List<ContractByPhoneNumber> ListContByPhone { get; set; }

    }
}
