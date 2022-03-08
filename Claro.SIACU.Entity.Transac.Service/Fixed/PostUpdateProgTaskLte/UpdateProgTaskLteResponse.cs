using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostUpdateProgTaskLte
{
    [DataContract]
    public class UpdateProgTaskLteResponse
    {
        [DataMember]
        public bool ResultStatus { get; set; }
    }
}
