using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial
{
    [DataContract(Name = "RegisterServiceCommercialResponse")]
    public class RegisterServiceCommercialResponse
    {
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMessage { get; set; }
    }
}
