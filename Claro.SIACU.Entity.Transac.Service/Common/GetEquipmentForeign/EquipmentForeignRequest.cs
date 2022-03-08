using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEquipmentForeign
{
    [DataContract]
    public class EquipmentForeignRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public Int64 CustomerId { get; set; }
        [DataMember]
        public string Estado { get; set; }
    }
}
