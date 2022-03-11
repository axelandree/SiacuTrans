using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class consultarDescartesRtiPrePostRequestGrupo
    {
        [DataMember(Name = "coId")]
        public string coId { get; set; }

        [DataMember(Name = "msisdn")]
        public string msisdn { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }

        [DataMember(Name = "grupoDescarte")]
        public string grupoDescarte { get; set; }
    }
}
