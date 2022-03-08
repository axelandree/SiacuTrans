using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser
{
    [DataContract(Name = "RequestOpcional")]
    public class RequestOpcional
    {
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
}
