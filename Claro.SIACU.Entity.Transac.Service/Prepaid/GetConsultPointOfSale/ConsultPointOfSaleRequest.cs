using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetConsultPointOfSale
{
    [DataContract]
    public class ConsultPointOfSaleRequest: Claro.Entity.Request
    {
        [DataMember]
        public string CodigoCAC { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
    }
}
