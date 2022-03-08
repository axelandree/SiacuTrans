using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetListAddressRequest
    {
        [DataMember(Name = "dirFechaNacimiento")]
        public string dirFechaNacimiento { get; set; }
        [DataMember(Name = "dirCiudad")]
        public string dirCiudad { get; set; }
        [DataMember(Name = "dirRegEmpresa")]
        public string dirRegEmpresa { get; set; }
        [DataMember(Name = "dirDistrito")]
        public string dirDistrito { get; set; }
        [DataMember(Name = "dirTipoCliente")]
        public string dirTipoCliente { get; set; }
        [DataMember(Name = "dirBorrado")]
        public string dirBorrado { get; set; }
        [DataMember(Name = "dirLicencia")]
        public string dirLicencia { get; set; }
        [DataMember(Name = "dirCorreo")]
        public string dirCorreo { get; set; }
        [DataMember(Name = "dirEmpleado")]
        public string dirEmpleado { get; set; }
        [DataMember(Name = "dirEmpleador")]
        public string dirEmpleador { get; set; }
        [DataMember(Name = "dirFax1")]
        public string dirFax1 { get; set; }
        [DataMember(Name = "dirFax1Area")]
        public string dirFax1Area { get; set; }
        [DataMember(Name = "dirFaxEmpresa")]
        public string dirFaxEmpresa { get; set; }
        [DataMember(Name = "dirComercial")]
        public string dirComercial { get; set; }
        [DataMember(Name = "dirIdentidad")]
        public string dirIdentidad { get; set; }
        [DataMember(Name = "dirCiudadPostal")]
        public string dirCiudadPostal { get; set; }
        [DataMember(Name = "dirLabor")]
        public string dirLabor { get; set; }
        [DataMember(Name = "dirUsoDif")]
        public string dirUsoDif { get; set; }
        [DataMember(Name = "dirApellido")]
        public string dirApellido { get; set; }
        [DataMember(Name = "dirAdic1")]
        public string dirAdic1 { get; set; }
        [DataMember(Name = "dirAdic2")]
        public string dirAdic2 { get; set; }
        [DataMember(Name = "dirNombre2")]
        public string dirNombre2 { get; set; }
        [DataMember(Name = "dirEmpresa")]
        public string dirEmpresa { get; set; }
        [DataMember(Name = "adrNacinalidad")]
        public string adrNacinalidad { get; set; }
        [DataMember(Name = "adrNacinalidadPublico")]
        public string adrNacinalidadPublico { get; set; }
        [DataMember(Name = "dirNote1")]
        public string dirNote1 { get; set; }
        [DataMember(Name = "dirNote2")]
        public string dirNote2 { get; set; }
        [DataMember(Name = "dirNote3")]
        public string dirNote3 { get; set; }
        [DataMember(Name = "dirTlf1")]
        public string dirTlf1 { get; set; }
        [DataMember(Name = "dirAreaTlf1")]
        public string dirAreaTlf1 { get; set; }
        [DataMember(Name = "dirTlf2")]
        public string dirTlf2 { get; set; }
        [DataMember(Name = "dirAreaTlf2")]
        public string dirAreaTlf2 { get; set; }
        [DataMember(Name = "dirSugerencia")]
        public string dirSugerencia { get; set; }
        [DataMember(Name = "dirRoles")]
        public string dirRoles { get; set; }
        [DataMember(Name = "dirSecuencial")]
        public string dirSecuencial { get; set; }
        [DataMember(Name = "dirSexo")]
        public string dirSexo { get; set; }
        [DataMember(Name = "dirMensCorto")]
        public string dirMensCorto { get; set; }
        [DataMember(Name = "dirSocial")]
        public string dirSocial { get; set; }
        [DataMember(Name = "dirEstado")]
        public string dirEstado { get; set; }
        [DataMember(Name = "dirCalle")]
        public string dirCalle { get; set; }
        [DataMember(Name = "dirCalleNoPostal")]
        public string dirCalleNoPostal { get; set; }
        [DataMember(Name = "dirNroReg")]
        public string dirNroReg { get; set; }
        [DataMember(Name = "dirTemp")]
        public string dirTemp { get; set; }
        [DataMember(Name = "dirUrgente")]
        public string cuentaCambioSaldo { get; set; }
        [DataMember(Name = "dirFechaValidacion")]
        public string dirFechaValidacion { get; set; }      
        [DataMember(Name = "dirAnios")]
        public string dirAnios { get; set; }
        [DataMember(Name = "dirCodPostal")]
        public string dirCodPostal { get; set; }
        [DataMember(Name = "idPais")]
        public string idPais { get; set; }
        [DataMember(Name = "idPaisPub")]
        public string idPaisPub { get; set; }
        [DataMember(Name = "idTipoDoc")]
        public string idTipoDoc { get; set; }
        [DataMember(Name = "codIdioma")]
        public string codIdioma { get; set; }
        [DataMember(Name = "codIdiomaPub")]
        public string codIdiomaPub { get; set; }
        [DataMember(Name = "estadoCivil")]
        public string estadoCivil { get; set; }
        [DataMember(Name = "estadoCivilPub")]
        public string estadoCivilPub { get; set; }
        [DataMember(Name = "idTitulo")]
        public string idTitulo { get; set; }
        [DataMember(Name = "idTituloPub")]
        public string idTituloPub { get; set; }
    }
}
