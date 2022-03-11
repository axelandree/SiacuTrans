using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultIGV
{
    [DataContract]
    public class ConsultIGVRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public string AppId { get; set; }
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public string Username { get; set; }
    }
}
