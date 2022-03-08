using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityOptionalList")]
    public class ValidateIdentityOptionalList
    {
        [DataMember]
        public string Campo { get; set; }
        [DataMember]
        public string Valor { get; set; }
    }
}
