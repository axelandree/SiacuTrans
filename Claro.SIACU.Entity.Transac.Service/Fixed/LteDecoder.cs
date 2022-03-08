using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class LteDecoder
    {
        [DataMember]
        public string CodeService { get; set; }
        [DataMember]
        public string SnCode { get; set; }
        [DataMember]
        public string Cf { get; set; }
        [DataMember]
        public string SpCode { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string ServiceGroup { get; set; }
        [DataMember]
        public string Equipment { get; set; }
        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string Associated { get; set; }
        [DataMember]
        public string TypeEquipmentCode { get; set; }
        [DataMember]
        public string SerialNumber { get; set; }

        [DataMember]
        public string CodeUser { get; set; }
        [DataMember]
        public string DateUser { get; set; }
        [DataMember]
        public string CodeInsSrv { get; set; }

        [DataMember]
        public string Flag { get; set; }
    }
}
