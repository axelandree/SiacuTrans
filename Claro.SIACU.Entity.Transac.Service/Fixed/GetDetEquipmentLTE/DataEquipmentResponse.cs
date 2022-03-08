using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDetEquipmentLTE
{
    [DataContract(Name = "DataEquipmentResponse")]
    public class DataEquipmentResponse
    {
        [DataMember]
        public List<DetEquipmentService> Data_k_cod_id { get; set; }
    }
}