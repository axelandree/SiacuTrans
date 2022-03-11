using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTechnicalVisitResult
{
    [DataContract(Name = "TechnicalVisitResultResponse")]
    public class TechnicalVisitResultResponse
    {
        [DataMember]
        public TechnicalVisit Result { get; set; }
    }
}
