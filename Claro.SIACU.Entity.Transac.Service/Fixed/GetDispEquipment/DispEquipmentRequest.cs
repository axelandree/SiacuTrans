using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDispEquipment
{
    [DataContract(Name = "DispEquipmentRequest")]
    public class DispEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strNroserie { get; set; }
        [DataMember]
        public int intTipo { get; set; }
    }
}
