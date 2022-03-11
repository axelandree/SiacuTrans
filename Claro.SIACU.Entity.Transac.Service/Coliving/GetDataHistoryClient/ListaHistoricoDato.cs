using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient
{
    public class ListaHistoricoDato
    {
        public string customerId { get; set; }
        public string dniRuc { get; set; }
        public string dniRucRepLegal { get; set; }
        public string repreLegal { get; set; }
        public string tipoDocRepLegal { get; set; }
        public string razonSocial { get; set; }
        public string nombres { get; set; }
        public string apellido { get; set; }
        public string cargo { get; set; }
        public string numTelefono { get; set; }
        public string numCelular { get; set; }
        public string numFax { get; set; }
        public string email { get; set; }
        public string nombreComercial { get; set; }
        public string contactoCliente { get; set; }
        public string fechaNacimiento { get; set; }
        public string nacionalidad { get; set; }
        public string sexo { get; set; }
        public string estadoCivil { get; set; }
        public string direccionFact { get; set; }
        public string notasDireccionFact { get; set; }
        public string distritoFact { get; set; }
        public string provinciaFact { get; set; }
        public string departamentoFact { get; set; }
        public string codigoPostalFact { get; set; }
        public string direccionLeg { get; set; }
        public string notasDireccionLeg { get; set; }
        public string distritoLeg { get; set; }
        public string provinciaLeg { get; set; }
        public string departamentoLeg { get; set; }
        public string codigoPostalLeg { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string motivo { get; set; }
        public string usuario { get; set; }
        public string fechaRegistro { get; set; }
        public string updCliente { get; set; }
        public string updDataMinor { get; set; }
        public string updDirLegal { get; set; }
        public string updDirFac { get; set; }
        public string updRepLegal { get; set; }
        public string updContact { get; set; }
    }
}
