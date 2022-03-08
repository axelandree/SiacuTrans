using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Redirect
{
    [DataContract]
    public class BEUsuarioSession
    {
        [DataMember(Name = "userId")]
        public string CodigoUsuario { get; set; }

        [DataMember(Name = "sapVendorId")]
        public string CodigoVendedorSap { get; set; }

        [DataMember(Name = "login")]
        public string CodigoUsuarioRed { get; set; }

        [DataMember(Name = "accessStatus")]
        public string EstadoAcceso { get; set; }

        [DataMember(Name = "fullName")]
        public string NombreCompleto { get; set; }

        [DataMember(Name = "profileId")]
        public string CodigoPerfil { get; set; }

        [DataMember(Name = "profiles")]
        public string CadenaPerfil { get; set; }

        [DataMember(Name = "optionPermissions")]
        public string CadenaOpciones { get; set; }

        [DataMember(Name = "areaId")]
        public string AreaId { get; set; }

        [DataMember(Name = "areaName")]
        public string AreaDes { get; set; }
        public string Host { get; set; }
        public string OficinaVenta { get; set; }
        public string OficinaVentaDescripcion { get; set; }
        public string CanalVenta { get; set; }
        public string CanalVentaDescripcion { get; set; }
        public string Terminar { get; set; }
        public string UsuarioConsulta { get; set; }
        public string AccesoPorFiltro { get; set; }
        public string AccesoPorFiltroFlag { get; set; }
        public string AccesoCargaContrato { get; set; }

    }
}
