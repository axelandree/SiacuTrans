using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "ClientParameters")]
    public class ClientParameters
    {
        [DataMember]
        public string strmsisdn { get; set; }
        [DataMember]
        public string strflagReg { get; set; }
        [DataMember]
        public string strcontactObjId { get; set; }
        [DataMember]
        public string straccount { get; set; }
    }
}
