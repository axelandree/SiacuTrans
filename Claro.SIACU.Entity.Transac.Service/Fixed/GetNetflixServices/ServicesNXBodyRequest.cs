using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices
{
    [DataContract]
    public class ServicesNXBodyRequest
    {
        [DataMember(Name = "usuarioId")]
        public string usuarioId { get; set; }

        [DataMember(Name = "tipoProducto")]
        public string tipoProducto { get; set; }

        [DataMember(Name = "correo")]
        public string correo { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "tipoUsuario")]
        public string tipoUsuario { get; set; }

        [DataMember(Name = "usuarioReg")]
        public string usuarioReg { get; set; }

        [DataMember(Name = "enviaCorreo")]
        public string enviaCorreo { get; set; }

        [DataMember(Name = "enviaSMS")]
        public string enviaSMS { get; set; }

        [DataMember(Name = "listaOpcional")]
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
