using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt
{
    [DataContract]
    public class AuditType
    {
        [DataMember]
        public string txtId { get; set; }
        [DataMember]
        public string errorCode { get; set; }
        [DataMember]
        public string errorMsg { get; set; }
    }
}
