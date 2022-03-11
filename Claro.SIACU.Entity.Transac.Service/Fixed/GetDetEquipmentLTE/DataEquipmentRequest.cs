using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDetEquipmentLTE
{
    public class DataEquipmentRequest : Claro.Entity.Request
    {
        [DbColumn("KRTK_COD_ID")]
        [DataMember]
        public string strK_cod_id { get; set; }
    }
}