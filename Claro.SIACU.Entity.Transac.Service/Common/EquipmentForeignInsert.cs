using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    //se registra interaccion subclase detalle
    public class EquipmentForeignInsert
    {
        public string REDEV_NUMERO_IMEI { get; set; }
        public string REDEV_NUMERO_IMEI_FISICO { get; set; }
        public string REDEV_NUMEROLINEA { get; set; }
        public string REDEV_ESTADO { get; set; }
        public string REDEV_MARCA_MODELO { get; set; }
        public string REDEV_USUARIOCREA { get; set; }
        public string REDEV_USUARIOMODI { get; set; }
        public DateTime REDED_FECHAMODI { get; set; }
        public Int64 REDEN_CUSTOMERID { get; set; }
        public int MAXIMO { get; set; }

    }
}
