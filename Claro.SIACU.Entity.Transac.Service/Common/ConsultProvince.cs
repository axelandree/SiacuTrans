using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultProvince
    {
        [DataMember]
        [Data.DbColumn("ID_PROVINCIA")]
        public string IdProvince { get; set; }
        [DataMember]
        [Data.DbColumn("PRO_DESC")]
        public string ProDesc { get; set; }
        [DataMember]
        [Data.DbColumn("PRO_ESTADO")]
        public string ProState { get; set; }
        [DataMember]
        [Data.DbColumn("ID_DEPARTAMENTO")]
        public string IdDepartment { get; set; }
    }
}
