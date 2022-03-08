using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetFileDefaultImpersonation
{
    [DataContract(Name="FileDefaultImpersonationRequestCommon")]
    public class GetFileDefaultImpersonationRequest:Claro.Entity.Request

    {
        [DataMember]
        public string strPath { get; set; }
    }
}
