using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class RegistrarEta
    {
        [DataMember]
        public string IdPoblado { get; set; }
        [DataMember]
        public string DniTecnico { get; set; }
        [DataMember]
        public string Franja { get; set; }
        [DataMember]
        public string Idbucket { get; set; }
        [DataMember]
        public string IpCreacion { get; set; }
        [DataMember]
        public string SubTipoOrden { get; set; }
        [DataMember]
        public string UsrCrea { get; set; }
        [DataMember]
        public string FechaProg { get; set; }
        [DataMember]
        public string FechaCrea { get; set; }
    }
}
