using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class objetoOpcional
    {
        [DataMember(Name = "campo")]
        public string campo { get; set; }
        [DataMember(Name = "valor")]
        public string valor { get; set; }

    }
}
