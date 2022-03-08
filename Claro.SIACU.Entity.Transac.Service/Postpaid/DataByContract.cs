using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("DataByContract")]
    public class DataByContract
    {
        [Data.DbColumn("customer_id")]
        public string customer_id {get;set;}
        [Data.DbColumn("tmcode")]
        public string tmcode { get; set; }
        [Data.DbColumn("strPLNVCode")]
        public string sub_mercado { get; set; }
        [Data.DbColumn("mercado")]
        public string mercado {get;set;}
        [Data.DbColumn("red")]
        public string red {get;set;}
        [Data.DbColumn("estado_umbral")]
        public string estado_umbral {get;set;}
        [Data.DbColumn("cantidad_umbral")]
        public string cantidad_umbral {get;set;}
        [Data.DbColumn("ARCH_LLAMADAS")]
        public string ARCH_LLAMADAS {get;set;}
        [Data.DbColumn("co_id")]
        public string co_id {get;set;}
        [Data.DbColumn("estado")]
        public string estado {get;set;}
        [Data.DbColumn("razon")]
        public string razon {get;set;}
        [Data.DbColumn("rs_desc")]
        public string rs_desc {get;set;}
        [Data.DbColumn("sm_id")]
        public string sm_id { get; set; }
    }
}
