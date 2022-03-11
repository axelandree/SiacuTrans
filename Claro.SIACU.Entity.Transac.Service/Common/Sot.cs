using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class Sot
    {
        [DataMember]
        [Data.DbColumn("codsolot")]
        public string COD_SOT { get; set; }
    }
}
