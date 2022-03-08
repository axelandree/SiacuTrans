using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS
{
    [DataContract(Name = "UserExistsBSCSRequest")]
    public class UserExistsBSCSRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Users { get; set; }
    }
}
