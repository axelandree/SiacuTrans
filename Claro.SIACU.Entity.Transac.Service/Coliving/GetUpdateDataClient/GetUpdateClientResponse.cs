using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetUpdateClientResponse
    {
        [DataMember(Name = "responseAudit")]
        public AuditResponse AuditResponse { get; set; }
        [DataMember(Name = "adr_seq")]
        public string adr_seq { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<ListOptional> ListOptional { get; set; }
    }
}
