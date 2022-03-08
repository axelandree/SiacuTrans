using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class RegisterTraceability
    {
        public string IdPadre { get; set; }
        public string CodOperacion { get; set; }
        public string Sistema { get; set; }
        public string CodCanal { get; set; }
        public string CodPdv { get; set; }
        public string CodModalVenta { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Lineas { get; set; }
        public string VeprnId { get; set; }
        public string DNIAutorizado { get; set; }
        public string UsuarioCtaRed { get; set; }
        public string IdHijo { get; set; }
        public string PadreAnt { get; set; }
        public string DNIConsultado { get; set; }
        public string WSOrigen { get; set; }
        public string TipoValidacion { get; set; }
        public string OrigenTipo { get; set; }
        public string CodigoRptaExitoError { get; set; }
        public string MensajeProceso { get; set; }
        public string Estado { get; set; }
        public string Flag { get; set; }
    }
}
