using Claro.Helpers.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail
{
    public class BilledOutCallDetailsPost : IExcel
    {
        [Header(Title = "Nro")]
        public int Nro { get; set; } //numero
        [Header(Title = "Fecha")]
        public string StrDate { get; set; } //fecha
        [Header(Title = "Hora")]
        public string StrHour { get; set; } //hora
        [Header(Title = "Teléfono Destino")]
        public string DestinationPhone { get; set; } //telefono destino
        [Header(Title = "Consumo")]
        public string Consumption { get; set; }  //consumo
        [Header(Title = "Consumo S/.")]
        public string OriginalCharge { get; set; } //costo
        //public decimal Plan { get; set; }
        [Header(Title = "Tipo")]
        public string CallType { get; set; } //tipo
        [Header(Title = "Destino")]
        public string Destiny { get; set; } //destino
        [Header(Title = "Operador")]
        public string Operator { get; set; } //operador
        //public string Timetable { get; set; } //horario

        //public string Balance { get; set; } //saldo
        //public string Promoción { get; set; }
        //public string Destino { get; set; }
        //public string Operador { get; set; }
        //public string GrupoCobro { get; set; }
        //public string TipoRed { get; set; }
        //public string IMEI { get; set; }
        //public string Roaming { get; set; }
        //public string ZonaTarifaria { get; set; }
        //public string PhonfNroGener { get; set; }
    }
}