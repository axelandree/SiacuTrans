using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser
{
    [DataContract(Name = "AccesoRequest")]
    public class AccessRequest
    {
        [DataMember]
        public int IntUser { get; set; }
        [DataMember]
        public int IntAplicationCode { get; set; }
    }
}
