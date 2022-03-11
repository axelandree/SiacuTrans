using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
      
    public class ConsultDiscardRTIBodyRequest
    {
        [DataMember(Name = "consultarDescartesRtiPrePostRequest")]
        public consultarDescartesRtiPrePostRequest consultarDescartesRtiPrePostRequest { get; set; }
    }
}
