using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    //INICIATIVA-871
    public class AplicarRetirarContingencia
    {
        public string NeType { get; set; }
        public string Priority { get; set; }
        public string ReqUser { get; set; }
        public string ActionId { get; set; }
        public string Imsi { get; set; }
        public string Linea { get; set; }
        public string NetworkService { get; set; }
        public string ServicioVolte { get; set; }
        public string TipoPlan { get; set; }
        public string ClienteCbio { get; set; }
        public string TipoCliente { get; set; }
        public string ContactCode { get; set; }
        public string Escenario { get; set; }
        public string Accion { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombresCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string PlataformaActivacion { get; set; }
    }
}
