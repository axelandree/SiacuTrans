using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "pHistorialServ")]
    public class PHistorialServ
    {
        [DataMember(Name = "servicioID")]
        public string servicioID { get; set; }

        [DataMember(Name = "nombreServicio")]
        public string nombreServicio { get; set; }

        [DataMember(Name = "precio")]
        public string precio { get; set; }

        [DataMember(Name = "fechaActivacion")]
        public string fechaActivacion { get; set; }

        [DataMember(Name = "fechaExpiracion")]
        public string fechaExpiracion { get; set; }

        [DataMember(Name = "fechaCancelacion")]
        public string fechaCancelacion { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }

        [DataMember(Name = "servicio")]
        public string servicio { get; set; }

        [DataMember(Name = "estado")]
        public string estado { get; set; }

        [DataMember(Name = "operacion")]
        public string operacion { get; set; }

    }
}
