using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityHeaderRequest")]
    public class ValidateIdentityHeaderRequest
    {
        [DataMember]
        public string canal { get; set; }
        [DataMember]
        public string idAplicacion { get; set; }
        [DataMember]
        public string usuarioAplicacion { get; set; }
        [DataMember]
        public string usuarioSesion { get; set; }
        [DataMember]
        public string idTransaccionESB { get; set; }
        [DataMember]
        public string idTransaccionNegocio { get; set; }
        [DataMember]
        public DateTime fechaInicio { get; set; }
        [DataMember]
        public string nodoAdicional { get; set; }
    }
}
