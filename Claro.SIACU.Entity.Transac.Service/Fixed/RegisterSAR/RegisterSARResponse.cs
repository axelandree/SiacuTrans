using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterSARResponse:Claro.Entity.Response
    {
        [DataMember]
        public AuditResponse auditResponse { get; set; }
        [DataMember]
        public string nroCaso { get; set; }
        [DataMember]
        public string idDocAut { get; set; }
        [DataMember]
        public string farenNroSar { get; set; }
        [DataMember]
        public List<ListResponseOptional> listaResponseOpcional { get; set; }
    }
}
