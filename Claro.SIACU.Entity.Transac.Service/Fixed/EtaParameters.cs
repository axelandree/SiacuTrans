using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "EtaParameters")]
    public class EtaParameters
    {
        [DataMember]
        public string strPlano { get; set; }
        [DataMember]
        public string strIdPoblado { get; set; }
        [DataMember]
        public string strSubtipo { get; set; }
        [DataMember]
        public string strIdFranja { get; set; }
        [DataMember]
        public string strIdBucket { get; set; }
        [DataMember]
        public string strDniTecnico { get; set; }
        [DataMember]
        public string strIpCreacion { get; set; }
        [DataMember]
        public string strFechaCreacion { get; set; }
        [DataMember]
        public string strUsrCreacion { get; set; }

    }
}
