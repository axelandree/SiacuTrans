using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBpelCallDetail
{
    [DataContract]
    public class BpelCallDetailResponse
    {
        [DataMember]
        public string StrResponseCode { get; set; }
        [DataMember]
        public string StrResponseMessage { get; set; }
        [DataMember]
        public string FechaCicloIni { get; set; }
        [DataMember]
        public string FechaCicloFin { get; set; }
        [DataMember]
        public List<BilledCallsDetailHFC> ListBilledCallsDetailHfc { get; set; }
        //public List<UnbilledCallDetailHFC> ListUnbilledCallDetailHfc { get; set; }   
    }
}
