using System.Collections.Generic;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDispEquipment
{
    [DataContract(Name = "DispEquipmentResponse")]
    public class DispEquipmentResponse : Claro.Entity.Response
    {
        [DataMember]
        public string ResultCode { get; set; }

        [DataMember] 
        public string ResultMessage { get; set; }

        [DataMember]
        public List<EntitiesFixed.BEDeco> lstEquipments { get; set; }
    }
}
