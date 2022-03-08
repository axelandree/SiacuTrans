using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class Region
    {
        [DataMember]
        [Data.DbColumn("ID_REGION")]
        public string ID_REGION { get; set; }

        [DataMember]
        [Data.DbColumn("REGION_DESC")]
        public string DESCRIPCION { get; set; }
    }
}
