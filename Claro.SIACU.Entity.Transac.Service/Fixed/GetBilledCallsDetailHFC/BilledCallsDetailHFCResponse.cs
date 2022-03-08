using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBilledCallsDetailHFC
{
    [DataContract]
    public class BilledCallsDetailHFCResponse
    {
        [DataMember]
        public List<BilledCallsDetailHFC> ListBilledCallsDetailHFC { get; set; }
    }
}
