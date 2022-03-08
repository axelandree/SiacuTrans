using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class SignDocument
    {
        public string CodigoPDV { get; set; }
        public string NombrePDV { get; set; }
        public string TipoFirma { get; set; }
        public string TipoArchivo { get; set; }
        public string Negocio { get; set; }
        public string TipoContrato { get; set; }
        public string DatFirma { get; set; }
        public string OrigenArchivo { get; set; }
        public string CodigoAplicacion { get; set; }
        public string PosFirma { get; set; }
        public string NombreArchivo { get; set; }
        public string IPAplicacion { get; set; }
        public string NumeroArchivo { get; set; }
        public string SegmentoOferta { get; set; }
        public string PlantillaBRMS { get; set; }
        public string TipoOperacion { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string ContenidoArchivo { get; set; }
        public string RutaArchivoDestino { get; set; }
        public string RutaArchivoOrigen { get; set; }
        public string CanalAtencion { get; set; }
    }
}
