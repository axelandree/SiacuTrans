using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail
{
    public class ExportExcelPreCallDetails
    {
        //Header
        [Header(Title = "NRO")]
        public string Nro { get; set; }
        [Header(Title = "FECHA HORA")]
        public string FechaHora { get; set; }
        [Header(Title = "TELEFONO DESTINO")]
        public string TelephoneDestin { get; set; }
        [Header(Title = "TIPO DE TRAFICO")]
        public string TipoTrafico { get; set; }
        [Header(Title = "DURACION")]
        public string Duracion { get; set; }
        [Header(Title = "CONSUMO")]
        public string Consumo { get; set; }
        [Header(Title = "COMPRADO/REGALADO")]
        public string CompradoRegalado { get; set; }
        [Header(Title = "SALDO")]
        public string Saldo { get; set; }

        [Header(Title = "BOLSA ID")]
        public string BolsaId { get; set; }

        [Header(Title = "DESCRIPCION")]
        public string Descripcion { get; set; }
        [Header(Title = "PLAN")]
        public string Plan { get; set; }
        [Header(Title = "PROMOCION")]
        public string Promocion { get; set; }
        [Header(Title = "DESTINO")]
        public string Destino { get; set; }
        [Header(Title = "OPERADOR")]
        public string Operador { get; set; }
        [Header(Title = "GRUPO DE COBRO")]
        public string GrupoCobro { get; set; }
        [Header(Title = "TIPO DE RED")]
        public string TipoRed { get; set; }
        [Header(Title = "IMEI")]
        public string Imei { get; set; }
        [Header(Title = "ROAMING")]
        public string Roming { get; set; }
        [Header(Title = "ZONA TARIFARIA")]
        public string ZoneTarifaria { get; set; }

    }
}