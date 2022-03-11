using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostReconexionLte
{
    [DataContract]
    public class ReconexionLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public ReconexionLte ReconexionLte { get; set; }
    }
}
