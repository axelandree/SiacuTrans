using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetService
{
    public class ServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractID { get; set; }

        [DataMember]
        public string ProductType { get; set; }
    }
}
