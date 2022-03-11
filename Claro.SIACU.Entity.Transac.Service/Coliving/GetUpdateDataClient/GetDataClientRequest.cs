using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetDataClientRequest
    {
        [DataMember(Name = "estado")]
        public string estado { get; set; }
        [DataMember(Name = "tipoPersona")]
        public string tipoPersona { get; set; }
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }
        [DataMember(Name = "apellidoPaterno")]
        public string apellidoPaterno { get; set; }
        [DataMember(Name = "apellidoMaterno")]
        public string apellidoMaterno { get; set; }
        [DataMember(Name = "razonSocial")]
        public string razonSocial { get; set; }
        [DataMember(Name = "nombreComercial")]
        public string nombreComercial { get; set; }
        [DataMember(Name = "fechaNacimiento")]
        public string fechaNacimiento { get; set; }
        [DataMember(Name = "lugarNacimiento")]
        public string lugarNacimiento { get; set; }
        [DataMember(Name = "nacionalidad")]
        public string nacionalidad { get; set; }
        [DataMember(Name = "sexo")]
        public string sexo { get; set; }
        [DataMember(Name = "estadoCivil")]
        public string estadoCivil { get; set; }
        [DataMember(Name = "correoElectronico")]
        public string correoElectronico { get; set; }
        [DataMember(Name = "clasificacionDeMercado")]
        public string clasificacionDeMercado { get; set; }
        [DataMember(Name = "telefonoReferencia1")]
        public string telefonoReferencia1 { get; set; }
        [DataMember(Name = "telefonoReferencia2")]
        public string telefonoReferencia2 { get; set; }
        [DataMember(Name = "segmentoComercial")]
        public string segmentoComercial { get; set; }
        [DataMember(Name = "sectorComercial")]
        public string sectorComercial { get; set; }
        [DataMember(Name = "direccion")]
        public string direccion { get; set; }
        [DataMember(Name = "direccionReferencia")]
        public string direccionReferencia { get; set; }
        [DataMember(Name = "ubigeoDireccion")]
        public string ubigeoDireccion { get; set; }
        [DataMember(Name = "limiteCredito")]
        public string limiteCredito { get; set; }
        [DataMember(Name = "motivo")]
        public string motivo { get; set; }
        [DataMember(Name = "fecReg")]
        public string fecReg { get; set; }

        [DataMember(Name = "direccionLegal")]
        public string direccionLegal { get; set; }
        [DataMember(Name = "direccionReferenciaLegal")]
        public string direccionReferenciaLegal { get; set; }
        [DataMember(Name = "paisLegal")]
        public string paisLegal { get; set; }
        [DataMember(Name = "departamentoLegal")]
        public string departamentoLegal { get; set; }
        [DataMember(Name = "provinciaLegal")]
        public string provinciaLegal { get; set; }
        [DataMember(Name = "distritoLegal")]
        public string distritoLegal { get; set; }
        [DataMember(Name = "urbanizacionLegal")]
        public string urbanizacionLegal { get; set; }
        [DataMember(Name = "codigoPostalLegal")]
        public string codigoPostalLegal { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
    }
}
