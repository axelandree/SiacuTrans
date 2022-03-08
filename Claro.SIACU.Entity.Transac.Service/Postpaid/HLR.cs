using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class HLR
    {
        [DataMember]
        [Data.DbColumn("HLR2C_INICIO")]
        public long HLR2C_INICIO { get; set; }

        [DataMember]
        [Data.DbColumn("HLR2C_FIN")]
        public long HLR2C_FIN { get; set; }

        [DataMember]
        [Data.DbColumn("HLR2C_NROHLR")]
        public int HLR2C_NROHLR { get; set; }
    }
}
