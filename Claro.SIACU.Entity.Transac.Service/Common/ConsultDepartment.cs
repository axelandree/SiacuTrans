using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultDepartment
    {
        [DataMember]
        [Data.DbColumn("ID_DEPARTAMENTO")]
        public string IdDepartment { get; set; }
        [DataMember]
        [Data.DbColumn("DEP_DESC")]
        public string DepDesc { get; set; }
        [DataMember]
        [Data.DbColumn("DEP_ESTADO")]
        public string DepState { get; set; }
        [DataMember]
        [Data.DbColumn("ID_REGION")]
        public string IdRegion { get; set; }
    }
}
