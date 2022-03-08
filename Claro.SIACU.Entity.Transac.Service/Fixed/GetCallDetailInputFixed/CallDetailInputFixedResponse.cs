using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailInputFixed
{
    [DataContract]
    public class CallDetailInputFixedResponse
    {
        [DataMember]
        public string codigoRespuesta { get; set; }

        [DataMember]
        public string descripcionRespuesta { get; set; }

        [DataMember]
        public List<Claro.SIACU.Entity.Transac.Service.Postpaid.CallDetailSummary> ListCallDetailSummary { get; set; }
    }
}
