using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class JobType
    {
        [DbColumn("tiptra")]
        [DataMember]
        public string tiptra { get; set; }
        [DbColumn("descripcion")]
        [DataMember]
        public string descripcion { get; set; }
        [DbColumn("FLAG_FRANJA")]
        [DataMember]
        public string FLAG_FRANJA { get; set; }

    }
}
