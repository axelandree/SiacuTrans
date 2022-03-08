using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid
{
    /// <summary>
    /// 17/05/2017 - CAlarcon
    /// </summary>
    public class Call
    {
        public DateTime? FECHA_HORA { get; set; }
        public string TELEFONO_DESTINO { get; set; }
        public string TIPO_DE_TRAFICO { get; set; }
        public string DURACION { get; set; }
        public string CONSUMO { get; set; }
        public string COMPRADO_REGALADO { get; set; }
        public string SALDO { get; set; }
        public string BOLSA_ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string PLAN { get; set; }
        public string PROMOCION { get; set; }
        public string DESTINO { get; set; }
        public string OPERADOR { get; set; }
        public string GRUPO_DE_COBRO { get; set; }
        public string TIPO_DE_RED { get; set; }
        public string IMEI { get; set; }
        public string ROAMING { get; set; }
        public string ZONA_TARIFARIA { get; set; }
        public string TIPO_EVENTO { get; set; }
    }
}
