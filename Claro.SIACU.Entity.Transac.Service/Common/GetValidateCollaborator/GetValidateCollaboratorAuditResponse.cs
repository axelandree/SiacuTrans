using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator
{
    [DataContract]
    public class GetValidateCollaboratorAuditResponse
    {
        [DataMember(Name = "idTransaccion")]
        public string IdTransaction { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string CodeResponse { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string MessageResponse { get; set; }
    }
}
