using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetReconeService
{
    [DataContract]
    public class ReconeServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public Reconection GetReconection { get; set; }
    }
}
