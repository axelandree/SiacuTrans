using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial
{
    [DataContract(Name = "RegisterServiceCommercialRequest")]
    public class RegisterServiceCommercialRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrUser { get; set; }
        [DataMember]
        public string StrSystem { get; set; }
        [DataMember]
        public string StrIdTransaction { get; set; }
        [DataMember]
        public string StrCoId { get; set; }

        [DataMember]
        public string StrCodServ { get; set; }
        [DataMember]
        public string StrTypeActivation { get; set; }
    }
}
