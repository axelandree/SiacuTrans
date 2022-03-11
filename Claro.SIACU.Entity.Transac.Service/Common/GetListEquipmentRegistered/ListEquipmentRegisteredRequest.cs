using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetListEquipmentRegistered
{
    public class ListEquipmentRegisteredRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public Int64 CustomerId { get; set; }
        [DataMember]
        public string Imei { get; set; }
        [DataMember]
        public string EstadoId { get; set; }
        [DataMember]
        public int NumMaximo { get; set; }
    }
}
