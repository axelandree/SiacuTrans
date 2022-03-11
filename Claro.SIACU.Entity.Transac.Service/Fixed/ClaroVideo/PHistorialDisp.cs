using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "pHistorialDisp")]
    public class PHistorialDisp
    {
        [DataMember(Name = "linea")]
        public string linea { get; set; }

        [DataMember(Name = "dispositivoId")]
        public string dispositivoId { get; set; }

        [DataMember(Name = "nombreDisp")]
        public string nombreDisp { get; set; }

        [DataMember(Name = "tipoDisp")]
        public string tipoDisp { get; set; }

        [DataMember(Name = "fechaAct")]
        public string fechaAct { get; set; }

        [DataMember(Name = "fehaExp")]
        public string fehaExp { get; set; }
    }
}
