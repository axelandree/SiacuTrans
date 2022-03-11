using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Redirect
{
    public class BEPortabilidad
    {
        public BEPortabilidad() { }
        public string numeroSolicitud { get; set; }
        public string tipoPortabilidad { get; set; }
        public string estadoProceso { get; set; }
        public string descripcionEstadoProc { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaEjecucion { get; set; }
        public string codigoOperadorCedente { get; set; }
        public string codigoOperadorReceptor { get; set; }
        public string usuarioCambioEstado { get; set; }
        public string usuarioCrea { get; set; }
        public string observaciones { get; set; }
        public string IdCorrelativo { get; set; }
        public string NombreAnexo { get; set; }
        public string CodEstadoProc { get; set; }
        public string InicioRango { get; set; }
        public string Cac { get; set; }
        public string Fecha_Ejecución { get; set; }
    }
}
