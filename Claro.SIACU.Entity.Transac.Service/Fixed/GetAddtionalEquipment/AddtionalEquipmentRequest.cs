using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAddtionalEquipment
{
    [DataContract]
    public class AddtionalEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdPlan { get; set; }
        [DataMember]
        public string coid { get; set; }
        [DataMember]
        public string TypeProduct { get; set; }
    }
}
