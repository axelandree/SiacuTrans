using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
    public class SignDocumentDatosFirmarDocumentoResponse
    {
        public string RutaArchivo { get; set; }
        public string IdTransaccion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string CodigoRespuesta { get; set; }
        public string MensajeRespuesta { get; set; }
        public string DescripcionRespuesta { get; set; }
    }
}
