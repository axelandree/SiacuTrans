using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail
{
    public class BilledOutCallDetails
    {
        public int NumOrden { get; set; }
        public string Parametro_ID { get; set; }
        public string FechaHora { get; set; }
        public string TelefonoDestino { get; set; }
        public string TipoTrafico { get; set; }
        public string Duracion { get; set; }
        public decimal Consumo { get; set; }
        public string CompradoRegalado { get; set; }

        public string Saldo { get; set; }
        public string Bolsa { get; set; }
        public string Descripcion { get; set; }

        public string Plan { get; set; }
        public string Promoción { get; set; }
        public string Destino { get; set; }
        public string Operador { get; set; }
        public string GrupoCobro { get; set; }
        public string TipoRed { get; set; }
        public string IMEI { get; set; }
        public string Roaming { get; set; }
        public string ZonaTarifaria { get; set; }
        public string PhonfNroGener { get; set; }

    }
}