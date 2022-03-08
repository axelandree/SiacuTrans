using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetContractedBusinessServices
{
    [DataContract(Name = "ContractedBusinessServicesRequestPostPaid")]
    public class ContractedBusinessServicesRequest : Tools.Entity.Request
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string System { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string ContractId { get; set; }
        [DataMember]
        public string DesPlan { get; set; }
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string Application { get; set; }
    }
}
