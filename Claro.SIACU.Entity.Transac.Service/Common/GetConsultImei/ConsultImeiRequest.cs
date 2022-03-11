using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultImei
{
    [DataContract]
    public class ConsultImeiRequest : Claro.Entity.Request
    {

        [DataMember]
        public string Nro_phone { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
    }
}
