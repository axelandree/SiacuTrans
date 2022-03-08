using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
    public class SignDocumentHeaderRequest
    {
        public string Canal { get; set; }
        public string IdAplicacion { get; set; }
        public string UsuarioAplicacion { get; set; }
        public string UsuarioSesion { get; set; }
        public string IdTransaccionESB { get; set; }
        public string IdTransaccionNegocio { get; set; }
        public DateTime FechaInicio { get; set; }
        public string NodoAdicional { get; set; }
    }
}
