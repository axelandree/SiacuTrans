using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegisterTransaction
{
    [DataContract(Name = "RegisterTransactionLTEFixedRequest")]
    public class RegisterTransactionRequest: Claro.Entity.Request
    {
        [DataMember]
        public RegisterTransaction objRegisterTransaction { get; set; }
    }
}
