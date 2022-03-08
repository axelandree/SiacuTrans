using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class OrderSubType
    {
        [DbColumn("COD_SUBTIPO_ORDEN")]
        [DataMember]
        public string COD_SUBTIPO_ORDEN { get; set; }
        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DbColumn("TIEMPO_MIN")]
        [DataMember]
        public string TIEMPO_MIN { get; set; }
        [DbColumn("ID_SUBTIPO_ORDEN")]
        [DataMember]
        public string ID_SUBTIPO_ORDEN { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string COD_SP { get; set; }
        public string MSJ_SP { get; set; }
        [DbColumn("DECOS")]
        [DataMember]
        public string DECOS { get; set; }
    }
}
