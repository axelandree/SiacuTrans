using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class CurrentDiscountFixedCharge
    {
        [DataMember]
        public string TOTAL_DESCUENTO { get; set; }

        [DataMember]
        public string FEC_INICIO { get; set; }

        [DataMember]
        public string FEC_FIN { get; set; }

        [DataMember]
        public string PERIODO { get; set; }

        [DataMember]
        public string FLAG { get; set; }

        [DataMember]
        public string PORCENTAJE { get; set; }
    }
}
