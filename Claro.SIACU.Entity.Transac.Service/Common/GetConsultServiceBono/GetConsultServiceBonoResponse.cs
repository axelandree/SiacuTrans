using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoResponse
    {
        [DataMember(Name= "MessageResponse")]
        public GetConsultServiceBonoMessageResponse ConsultServiceBonoMessageResponse { get; set; } 
    }
}
