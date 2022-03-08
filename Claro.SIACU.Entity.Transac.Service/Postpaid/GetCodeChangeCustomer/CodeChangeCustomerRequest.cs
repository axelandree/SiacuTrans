using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer
{

    [DataContract(Name = "CodeChangeCustomerRequest")]
    public class CodeChangeCustomerRequest : Claro.Entity.Request
    {
        public string strName { get; set; }
    }
}
