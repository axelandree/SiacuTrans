using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "EtaSelection")]
    public class EtaSelection
    {
        [DataMember]
        public string strIdConsulta { get; set; }
        [DataMember]
        public string strFechaCompromiso { get; set; }
        [DataMember]
        public string strFranja { get; set; }
        [DataMember]
        public string strIdBucket { get; set; }

    }
}
