using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability
{
    [DataContract]
    public class InsertTraceabilityResponse
    {
        [DataMember]
        public string IdTransaccion { get; set; }
        [DataMember]
        public string CodRespuesta { get; set; }
        [DataMember]
        public string MsjRespuesta { get; set; }
    }
}
