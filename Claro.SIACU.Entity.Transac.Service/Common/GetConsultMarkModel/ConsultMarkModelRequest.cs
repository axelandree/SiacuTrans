using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultMarkModel
{
    [DataContract]
    public class ConsultMarkModelRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Nro_imei { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
    }
}
