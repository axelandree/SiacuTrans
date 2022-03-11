using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "ServiceBSCSPostPaid")]
    public class ServiceBSCS
    {
        [DataMember]
        public string SERVICIO { get; set; }
        [DataMember]
        public string PAQUETE { get; set; }
        [DataMember]
        public string ESTADO { get; set; }

    }
}
