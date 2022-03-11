using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetQuestionsAnswerSecurity
{
    [DataContract]
    public class QuestionsAnswerSecurityRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Typeclient { get; set; }
        [DataMember]
        public string Groupclient { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
    }
}

