using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "Coser")]
    public class Coser
    {
        [DataMember]
        public string strSnCode { get; set; }
        [DataMember]
        public string strSpCode { get; set; }
        [DataMember]
        public string strTipoServicio { get; set; }
        [DataMember]
        public string strCargoFijo { get; set; }
        [DataMember]
        public string strPeriodos { get; set; }

    }
}
