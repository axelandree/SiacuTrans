using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "HistorialServDispCVRequest")]
   public class HistorialServDispCVRequest
    {
        [DataMember(Name = "linea")]
        public string linea { get; set; }

        [DataMember(Name = "nombreServicio")]
        public string nombreServicio { get; set; }

        [DataMember(Name = "tmcod",IsRequired = false, EmitDefaultValue = false)]
        public string tmcod { get; set; }

        [DataMember(Name = "fechaInicio")]
        public string fechaInicio { get; set; }

        [DataMember(Name = "fechaFin")]
        public string fechaFin { get; set; }

        [DataMember(Name = "servicioName")]
        public string servicioName { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }
    }
}
