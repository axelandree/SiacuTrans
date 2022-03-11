using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class ReservaToa
    {
        public string strNroOrden { get; set; }
        public int strIdConsulta { get; set; }
        public string strFranja { get; set; }
        public DateTime strDiaReserva { get; set; }
        public string strIdBucket { get; set; }
        public string strCodZona { get; set; }
        public string strCodPlano { get; set; }
        public string strTipoOrden { get; set; }
        public string strSubTipoOrden { get; set; }
        public string strValor { get; set; }
        public int strTipoTransaccion { get; set; }
    }
}