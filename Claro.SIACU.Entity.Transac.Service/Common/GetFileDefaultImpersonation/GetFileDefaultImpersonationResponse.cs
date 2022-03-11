using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetFileDefaultImpersonation
{
    [DataContract(Name="FileDefaultImpersonationResponseCommon")]
    public class GetFileDefaultImpersonationResponse
    {
        [DataMember]
        public GlobalDocument objGlobalDocument { get; set; }
    }
}
