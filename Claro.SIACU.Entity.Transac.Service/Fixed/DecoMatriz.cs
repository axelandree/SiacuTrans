using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class DecoMatriz
    {
        [DataMember]
        public string TipoDeco { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Valor { get; set; }
    }
}

