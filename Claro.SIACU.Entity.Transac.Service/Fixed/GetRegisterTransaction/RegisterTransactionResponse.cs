using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegisterTransaction
{
    [DataContract(Name = "RegisterTransactionLTEFixedResponse")]
   public  class RegisterTransactionResponse
    {
        [DataMember]
        public string intNumSot { get; set; }
        [DataMember]
        public int intResCod { get; set; }
        [DataMember]
        public string strResDes { get; set; }
    }
}
