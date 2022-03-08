using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator
{
    [DataContract]
    public class ValidateCollaboratorResponse
    {
        [DataMember(Name = "auditResponse")]
        public GetValidateCollaboratorAuditResponse ValidateCollaboratorAuditResponse { get; set; }
        [DataMember(Name = "pExiste")]
        public string pExists { get; set; }
        [DataMember(Name = "pCfMax")]
        public string pCfMax { get; set; }
    }
}
