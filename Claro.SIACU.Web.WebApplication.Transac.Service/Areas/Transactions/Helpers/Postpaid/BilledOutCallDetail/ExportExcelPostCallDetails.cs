using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail
{
    public class ExportExcelPostCallDetails
    {
        [Header(Title = "NRO")]
        public int Nro { get; set; }
        [Header(Title = "FECHAHORA ")]
        public string FechaHora { get; set; }
        [Header(Title = "FECHA ")]
        public string Fecha { get; set; }
        [Header(Title = "TELEFONO DESTINO")]
        public string TelephoneDestin { get; set; }
        [Header(Title = "CONSUMO")]
        public string Consumo { get; set; }
        [Header(Title = "COSTO (S/.)")]
        public string Costo { get; set; }
        [Header(Title = "PLAN")]
        public string Plan { get; set; }
        [Header(Title = "TIPO DE TRAFICO")]
        public string TipoTrafico { get; set; }
        [Header(Title = "DESTINO")]
        public string Destino { get; set; }
        [Header(Title = "OPERADOR")]
        public string Operador { get; set; }
        [Header(Title = "HORA")]
        public string Hora { get; set; }
        [Header(Title = "SALDO (S/.)")]
        public string Saldo { get; set; }
        [Header(Title = "HORARIO")]
        public string Horario { get; set; }
    }
}