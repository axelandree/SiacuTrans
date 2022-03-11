using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator
{
    [DataContract]
    public class GetValidateCollaboratorRequest
    {
        [DataMember(Name = "MessageRequest")]
        public GetValidateCollaboratorMessageRequest ValidateCollaboratorMessageRequest { get; set; }
    }
}
