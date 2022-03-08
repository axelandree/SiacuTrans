using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetSendEmail
{
    [DataContract(Name = "SendEmailResponseCommon")]
public  class SendEmailResponse
    {
        [DataMember]
        public string Exit { get; set; }
    }
}
