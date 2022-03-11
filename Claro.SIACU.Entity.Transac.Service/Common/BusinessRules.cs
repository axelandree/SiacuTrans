using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class BusinessRules
    {
        [DataMember]
        [Data.DbColumn("REATV_SUBCLASE")]
        public string SUB_CLASE { get; set; }

        [DataMember]
        [Data.DbColumn("REATV_REGLA")]
        public string REGLA { get; set; }

        [DataMember]
        [Data.DbColumn("REATV_NOTA")]
        public string NOTA { get; set; }
    }
}
