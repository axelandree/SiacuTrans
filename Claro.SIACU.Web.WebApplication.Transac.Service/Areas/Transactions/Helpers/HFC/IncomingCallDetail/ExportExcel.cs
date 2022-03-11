using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail
{
    public class ExportExcel : IExcel
    {
        [Header(Title = "N°")]
        public string Id { get; set; }

        [Header(Title = "NÚMERO")]
        public string PhoneNumber { get; set; }

        [Header(Title = "FECHA")]
        public string DateIncomingCall { get; set; }

        [Header(Title = "HORA")]
        public string HourIncomingCall { get; set; }

        [Header(Title = "NÚMERO ENTRANTE")]
        public string IncomingPhoneNumber { get; set; }

        [Header(Title = "DURACIÓN")]
        public string Duration { get; set; }
    }
}