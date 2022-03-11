using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "TechnicalVisit")]
    public class TechnicalVisit
    {
        [DataMember]
        public string Flag { get; set; }
        [DataMember]

        public string Codmot { get; set; }
        [DataMember]

        public string Anerror { get; set; }
        [DataMember]

        public string Averror { get; set; }

        [DataMember]
        public string Anotaciones { get; set; }

        [DataMember]
        public string Subtipo { get; set; }
    }
}
