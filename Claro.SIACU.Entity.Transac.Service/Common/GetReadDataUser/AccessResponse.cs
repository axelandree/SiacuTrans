using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser
{
     [DataContract(Name = "AccesoResponse")]
    public class AccessResponse
    {
        [DataMember]
         public Auditoria auditoria { get; set; }
        [DataMember]
        public Employee employee { get; set; }
    }
}
