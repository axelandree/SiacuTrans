using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
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
        [DataMember(Name = "firstName")]
        public string Nombre { get; set; }
        [DataMember(Name = "lastName1")]
        public string Apellido { get; set; }
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
        [DataMember(Name = "searchUser")]
        public string UsuarioConsulta { get; set; }

    }
}