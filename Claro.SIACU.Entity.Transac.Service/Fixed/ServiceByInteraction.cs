using System.Runtime.Serialization;
using Claro.Data;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class ServiceByInteraction
    {
        [DbColumn("COD_INTERAC")]
        [DataMember]
        public string COD_INTERAC { get; set; }
        [DbColumn("NOM_SERV")]
        [DataMember]
        public string NOM_SERV { get; set; }
        [DbColumn("TIP_SERV")]
        [DataMember]
        public string TIP_SERV { get; set; }
        [DbColumn("GRUP_SERV")]
        [DataMember]
        public string GRUP_SERV { get; set; }
        [DbColumn("CF")]
        [DataMember]
        public string CF { get; set; }
        [DbColumn("EQUIPO")]
        [DataMember]
        public string EQUIPO { get; set; }
        [DbColumn("CANTIDAD")]
        [DataMember]
        public string CANTIDAD { get; set; }
    }
}
