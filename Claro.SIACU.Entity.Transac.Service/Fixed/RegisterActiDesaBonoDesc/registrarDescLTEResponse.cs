using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class registrarDescLTEResponse
    {
        [DataMember(Name = "auditResponse")]
        public auditResponse auditResponse { get; set; }
        [DataMember(Name = "listaResponseOpcional")]
        public listaResponseOpcional listaResponseOpcional { get; set; }
    }
}
