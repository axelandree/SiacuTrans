using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail
{
    public class CallTipificacion
    {
        public string cacdac { get; set; }
        public string fecha { get; set; }
        public string titular { get; set; }
        public string interaccion { get; set; }
        public string representanteLegal { get; set; }
        public string telefono { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public string destinatario { get; set; }
        public string customerId { get; set; }
        public CallTipificacion tipificacion { get; set; }
    }
}