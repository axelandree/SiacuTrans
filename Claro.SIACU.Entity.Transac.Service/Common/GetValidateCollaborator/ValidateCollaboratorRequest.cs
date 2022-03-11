using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator
{
    [DataContract]
    public class ValidateCollaboratorRequest
    {
        [DataMember(Name = "auditRequest")]
        public GetValidateCollaboratorAuditRequest ValidateCollaboratorAuditRequest { get; set; }
        [DataMember(Name = "tipoDocumento")]
        public string KindDocument { get; set; }
        [DataMember(Name = "numeroDocumento")]
        public string NumberDocument { get; set; }
        [DataMember(Name = "casoEspecial")]
        public string CaseSpecial { get; set; }
    }
}
