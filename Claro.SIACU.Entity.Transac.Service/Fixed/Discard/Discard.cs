using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    [DataContract(Name = "Discard")]
    public class Discard
    {
        [DataMember(Name = "id_descarte")]
        public string id_descarte { get; set; }

        [DataMember(Name = "nombre_variable")]
        public string nombre_variable { get; set; }

        [DataMember(Name = "desc_descarte")]
        public string desc_descarte { get; set; }

        [DataMember(Name = "tipo_descarte")]
        public string tipo_descarte { get; set; }

        [DataMember(Name = "flag_descarte")]
        public string flag_descarte { get; set; }

        [DataMember(Name = "orden_descarte")]
        public string orden_descarte { get; set; }

        [DataMember(Name = "fecha_reg")]
        public string fecha_reg { get; set; }

        [DataMember(Name = "id_grupo")]
        public string id_grupo { get; set; }

        [DataMember(Name = "flag_Error")]
        public string flag_Error { get; set; }

        [DataMember(Name = "flag_OK")]
        public string flag_OK { get; set; }

        [DataMember(Name = "descarteValor")]
        public string descarteValor { get; set; }

        [DataMember(Name = "descarteListaValor")]
        public List<DiscardListValue> descarteListaValor { get; set; }

    }
}
