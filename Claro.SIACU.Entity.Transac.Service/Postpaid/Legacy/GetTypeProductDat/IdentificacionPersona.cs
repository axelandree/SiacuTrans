using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class IdentificacionPersona
    {
        [DataMember(Name = "numeroDocumento")]
        public object numeroDocumento { get; set; }
        [DataMember(Name = "tipoDocumento")]
        public object tipoDocumento { get; set; }
        [DataMember(Name = "genero")]
        public object genero { get; set; }
        [DataMember(Name = "fechaNacimiento")]
        public object fechaNacimiento { get; set; }
        [DataMember(Name = "telefonoContacto")]
        public object telefonoContacto { get; set; }
        [DataMember(Name = "email")]
        public object email { get; set; }
        [DataMember(Name = "nombreCompleto")]
        public string nombreCompleto { get; set; }
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }
        [DataMember(Name = "apellidoCompleto")]
        public string apellidoCompleto { get; set; }
    }
}
