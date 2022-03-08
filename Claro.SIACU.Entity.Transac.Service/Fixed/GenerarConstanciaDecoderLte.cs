using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class GenerarConstanciaDecoderLte
    {
        [DataMember]
        public string Directory { get; set; }
        [DataMember]
        public string Driver { get; set; }
        [DataMember]
        public string FileName { get; set; }
    }
}
