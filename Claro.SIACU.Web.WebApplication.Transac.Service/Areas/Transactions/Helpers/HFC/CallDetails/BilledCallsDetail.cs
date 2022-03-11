using System;

using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails
{
    public class BilledCallsDetail : IExcel
    {

        [Header(Title = "NRO")]
        public string CurrentNumber { get; set; }
        [Header(Title = "FECHA")]
        public string StrDate { get; set; }
        [Header(Title = "HORA")]
        public string StrHour { get; set; }
        [Header(Title = "TELEFONO DESTINO")]
        public string DestinationPhone { get; set; }
        [Header(Title = "NRO CLIENTE")]
        public string NroCustomer { get; set; }
        [Header(Title = "CONSUMOS")]
        public string Consumption { get; set; }
        [Header(Title = "COSTO SOLES")]
        public string CostSoles { get; set; }
        [Header(Title = "TIPO")]
        public string Type { get; set; }
        [Header(Title = "DESTINO")]
        public string Destination { get; set; }
        [Header(Title = "OPERADOR")]
        public string Operator { get; set; }

        public string TypeCalls { get; set; }
        public string CargOriginal { get; set; }
        public string TelephoneDestExport { get; set; }
    }
}