using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBlackList
{
    [DataContract(Name = "BlackListRequest")]
    public class BlackListRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public BlackListMessageRequest MessageRequest { get; set; }



    }
}
