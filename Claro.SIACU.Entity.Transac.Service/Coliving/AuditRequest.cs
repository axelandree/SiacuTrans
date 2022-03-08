using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving
{
    [DataContract]
    public class AuditRequest
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string idAplicacion { get; set; }
        [DataMember]
        public string nombreAplicacion { get; set; }
        [DataMember]
        public string usuarioAplicacion { get; set; }
    }
}
