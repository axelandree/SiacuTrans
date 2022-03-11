using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient
{
   [DataContract(Name = "Response")]
    public class HistoryClientResponse
    {
        [DataMember(Name = "listaOpcional")]
       public List<ListOptional> listaOpcional { get; set; }
        [DataMember(Name = "AuditResponse")]
        public AuditResponse responseAudit { get; set; }
    }
}
