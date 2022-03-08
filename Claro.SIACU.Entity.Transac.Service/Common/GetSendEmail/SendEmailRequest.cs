using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetSendEmail
{
    [DataContract(Name = "SendEmailRequestCommon")]
  public   class SendEmailRequest:Claro.Entity.Request
    {
        [DataMember]
        public string strSender { get; set; }
        [DataMember]
        public string strTo { get; set; }
        [DataMember]
        public string strCC{get; set;}
        [DataMember]
        public string  strBCC{get;set;}
        [DataMember]
        public string  strSubject {get;set;}
        [DataMember]
        public string strMessage{get;set;}
        [DataMember]
        public string strAttached{get; set;}
        [DataMember]
        public List<string> lsAttached{get; set;}
        [DataMember]
        public string strJoinfile { get; set; }

        [DataMember]
        public byte[] AttachedByte { get; set; }
    }
}
