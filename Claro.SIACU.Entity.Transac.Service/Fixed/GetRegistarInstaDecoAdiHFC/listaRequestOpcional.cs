using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC
{
    [DataContract]
    public class listaRequestOpcional
    {
        [DataMember(Name = "campo")]
        public string campo { get; set; }
        [DataMember(Name = "valor")]
        public string valor { get; set; }
    }
}
