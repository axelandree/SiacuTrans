using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "HistorialServDispCVResponse")]
    public class HistorialServDispCVResponse
    {
        [DataMember(Name = "codError")]
        public string codError { get; set; }

        [DataMember(Name = "msgError")]
        public string messageError { get; set; }

        [DataMember(Name = "pHistorialServ")]
        public List<PHistorialServ> pHistorialServ { get; set; }

        [DataMember(Name = "pHistorialDisp")]
        public List<PHistorialDisp> pHistorialDisp { get; set; }

        [DataMember(Name = "pEstadoPagoServ")]
        public List<PEstadoPagoServ> pEstadoPagoServ { get; set; }
    }
}
