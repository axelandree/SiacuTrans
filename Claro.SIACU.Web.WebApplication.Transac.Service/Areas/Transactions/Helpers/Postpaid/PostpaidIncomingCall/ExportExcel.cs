
using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall
{
    public class ExportExcel : IExcel

    {
        [Header(Title = "N°")]
        public string Id { get; set; }
        [Header(Title = "  Número  ")]
        public string PhoneNumber { get; set; }
        [Header(Title = "  Fecha  ")]
        public string DateIncomingCall { get; set; }
        [Header(Title = "  Hora  ")]
        public string HourIncomingCall { get; set; }
        [Header(Title = "Numero Entrante")]
        public string IncomingPhoneNumber { get; set; }
        [Header(Title = "  Duración  ")]
        public string Duration { get; set; }


        public string NroTelefono { get; set; }

        public string Fecha_Ini { get; set; }

        public string Fecha_Fin { get; set; }

        public int TipoConsulta { get; set; }
    }
}