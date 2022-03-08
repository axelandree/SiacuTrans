using Claro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.OutgoingCallsDetailNB
{
    public class CallDetailExcel : IExcel
    {
        [Header(Title = "Nro")]
        public string StrOrder { get; set; }
        [Header(Title = "Fecha")]
        public string StrDate { get; set; }
        [Header(Title = "Hora")]
        public string StrHour { get; set; }

        [Header(Title = "Teléfono Destino")]
        public string DestinationPhone { get; set; }
        [Header(Title = "Consumo")]
        public string StrQuantity { get; set; }
        [Header(Title = "Costo (S/.)")]
        public Double OriginalAmount { get; set; }
        [Header(Title = "Plan")] 
        public string TariffPlan { get; set; }
        [Header(Title = "Tarifa")]
        public string Tariff { get; set; }
        [Header(Title = "Tipo")]
        public string Type { get; set; }
        [Header(Title = "Destino")]
        public string Tariff_Zone { get; set; }
        [Header(Title = "Operador")]
        public string Operator { get; set; }
        [Header(Title = "Horario")]
        public string Horary { get; set; }
        [Header(Title = "Tipo Llamada")]
        public string CallType { get; set; }
        [Header(Title = "Consumo S/.")]
        public Double FinalAmount { get; set; }
         
    }
}