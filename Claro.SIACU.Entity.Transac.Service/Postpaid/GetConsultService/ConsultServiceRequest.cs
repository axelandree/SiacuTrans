using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsultService
{
    [DataContract(Name = "ConsultServiceRequest")]
    public class ConsultServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public int CodId { get; set; }
        [DataMember]
        public int CodServ { get; set; }
    }
}
