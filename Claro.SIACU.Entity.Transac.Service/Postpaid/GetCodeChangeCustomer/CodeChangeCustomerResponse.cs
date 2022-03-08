using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer
{
    [DataContract(Name = "CodeChangeCustomerResponse")]
    public class CodeChangeCustomerResponse
    {
        [DataMember]
        public string strCodeChangeCustomer { get; set; }
    }
}
