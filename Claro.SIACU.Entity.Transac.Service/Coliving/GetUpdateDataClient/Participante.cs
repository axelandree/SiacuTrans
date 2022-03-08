using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    public class Participante
    {
        public string numeroDocumento { get; set; }
        public string codigoClienteUnico { get; set; }
        public int participanteId { get; set; }
        public string apellidoMaterno { get; set; }
        public string apellidoPaterno { get; set; }
        public string codigoAplicacion { get; set; }
        public string fechaCreacionProspecto { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombre { get; set; }
        public string tipoDocDescripcion { get; set; }
        public string tipoDocumento { get; set; }
        public string razonSocial { get; set; }
        public string sexo { get; set; }
        public string tipoPersona { get; set; }
        public string sistemaOrigen { get; set; }
        public string correoElectronico { get; set; }
        public string clasificacionDeMercado { get; set; }
        public string telefonoReferencia1 { get; set; }
        public string sectorComercial { get; set; }
        public string ubigeoDireccion { get; set; }
        public string estadoCivil { get; set; }
        public string estadoCivilDesc { get; set; }
        public string limiteCredito { get; set; }
        public string saldoLimiteCredito { get; set; }
        public string lugarNacimiento { get; set; }
        public string nombreComercial { get; set; }
        public string telefonoReferencia2 { get; set; }
        public string nacionalidadId { get; set; }
        public string nacionalidadDes { get; set; }
        public string estado { get; set; }
        public string segmentoComercial { get; set; }
        public string fechaCreacionCliente { get; set; }
        public string direccionLegal { get; set; }
        public string direccionReferenciaLegal { get; set; }
        public string paisLegal { get; set; }
        public string departamentoLegal { get; set; }
        public string provinciaLegal { get; set; }
        public string distritoLegal { get; set; }
        public string urbanizacionLegal { get; set; }
        public string codigoPostalLegal { get; set; }
        public object listaDetalleSegmento { get; set; }
    }
}
