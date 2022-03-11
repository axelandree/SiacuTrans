using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetClient
{
    [DataContract(Name = "GetClientRequestCommon")]
    public class ClientRequest:Claro.Entity.Request
    {
        [DataMember]
        public  string strphone {get;set;}
        [DataMember]
        public string  straccount {get;set;}
        [DataMember]
        public string strContactobjid {get;set;}
        [DataMember]
        public string strflagreg { get; set; }
    }
}
