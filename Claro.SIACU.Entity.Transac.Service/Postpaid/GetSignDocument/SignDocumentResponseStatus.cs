using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
    public class SignDocumentResponseStatus
    {
        public int Estado { get; set; }
        public string CodigoRespuesta { get; set; }
        public string DescripcionRespuesta { get; set; }
        public string UbicacionError { get; set; }
        public DateTime Fecha { get; set; }
        public string Origen { get; set; }
    }
}
