using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEmployerDate
{
     [DataContract(Name = "GetEmployerDateRequestCommon")]
    public class GetEmployerDateRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCurrentUser { get; set; }

    }
}
