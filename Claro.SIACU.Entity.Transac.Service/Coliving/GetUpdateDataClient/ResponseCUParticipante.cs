using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
   public class ResponseCUParticipante
    {
        public int codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
        public object mensajeError { get; set; }
        public IList<Participante> participante { get; set; }
        public object claroFault { get; set; }
    }
}
