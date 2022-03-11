using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class RegistrarEtaSeleccion
    {
        [DataMember]
        public string IdConsulta { get; set; }
        [DataMember]
        public string IdInteraccion { get; set; }
        [DataMember]
        public string FechaCompromiso { get; set; }
        [DataMember]
        public string Franja { get; set; }
        [DataMember]
        public string Idbucket { get; set; }
    }
}