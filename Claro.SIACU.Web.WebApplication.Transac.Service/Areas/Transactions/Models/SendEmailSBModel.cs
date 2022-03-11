using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class SendEmailSBModel
    {
        public string idTransaccion { get; set; }
        public string codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
    }
}