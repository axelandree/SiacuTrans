using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
   public class Trazability
    {
        public string idTransaccion { get; set; }
        public string idInteraccion { get; set; }
        public string tipoTransaccion { get; set; }
        public string tarea { get; set; }
        public string fechaRegistro { get; set; }
        public string tramaInput { get; set; }
        public string tramaOutput { get; set; }
        public string descripcion { get; set; }
    }
}
