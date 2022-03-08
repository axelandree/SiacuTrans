using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    public class DiscardListValue
    {
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }

        [DataMember(Name = "valor")]
        public string valor { get; set; }

        [DataMember(Name = "medida")]
        public string medida { get; set; }

        [DataMember(Name = "fechaVencimiento")]
        public string fechaVencimiento { get; set; }

        [DataMember(Name = "esCabecera")]
        public string esCabecera { get; set; }

    }
}
