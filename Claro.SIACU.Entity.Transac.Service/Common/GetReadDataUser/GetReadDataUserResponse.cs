using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser
{
    [DataContract(Name = "leerDatosUsuarioResponse")]
    public class GetReadDataUserResponse
    {

        [DataMember]
        public AccessResponse accessResponse { get; set; }

        [DataMember]
        public AuditResponse auditResponse  { get; set; }
    }
}
