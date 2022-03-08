using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultByGroupParameter
    {
        [DataMember]
        [Data.DbColumn("PARAN_CODIGO")]
        public Int64 ID_PARAMETRO { get; set; }

        [DataMember]
        [Data.DbColumn("PARAV_DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [DataMember]
        [Data.DbColumn("PARAV_VALOR")]
        public string VALOR { get; set; }

        [DataMember]
        [Data.DbColumn("PARAV_VALOR1")]
        public string VALOR1 { get; set; }

    }
}
