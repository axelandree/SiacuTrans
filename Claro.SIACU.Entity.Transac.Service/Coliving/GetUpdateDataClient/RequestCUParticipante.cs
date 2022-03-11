using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
   public class RequestCUParticipante
    {
        public object participanteId { get; set; }
        public object codigoClienteUnico { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public object nombres { get; set; }
        public object apellidoMaterno { get; set; }
        public object apellidoPaterno { get; set; }
        public object razonSocial { get; set; }
    }
}
