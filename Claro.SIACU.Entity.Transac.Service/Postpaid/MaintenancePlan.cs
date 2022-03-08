using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    public class MaintenancePlan
    {
        [Data.DbColumn("DE_GRP")]
        public string DE_GRP { get; set; }
        [Data.DbColumn("CO_SER")]
        public int CO_SER { get; set; }
        [Data.DbColumn("DE_SER")]
        public string DE_SER { get; set; }
        [Data.DbColumn("CO_EXCL")]
        public float CO_EXCL { get; set; }
        [Data.DbColumn("DE_EXCL")]
        public string DE_EXCL { get; set; }
        [Data.DbColumn("TMCODE")]
        public string TMCODE { get; set; }
        [Data.DbColumn("SNCODE")]
        public string SNCODE { get; set; }
        [Data.DbColumn("SPCODE")]
        public string SPCODE { get; set; }
        [Data.DbColumn("CARGO_FIJO")]
        public double CARGO_FIJO { get; set; }
    }
}
