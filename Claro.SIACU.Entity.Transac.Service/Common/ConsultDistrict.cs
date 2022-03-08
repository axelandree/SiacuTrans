using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultDistrict
    {
        [DataMember]
        [Data.DbColumn("ID_DISTRITO")]
        public string Id_District { get; set; }
        [DataMember]
        [Data.DbColumn("DIS_DESC")]
        public string Dis_Desc { get; set; }
        [DataMember]
        [Data.DbColumn("DIS_ESTADO")]
        public string Dis_State { get; set; }
        [DataMember]
        [Data.DbColumn("ID_PROVINCIA")]
        public string Id_Province { get; set; }
    }
}
