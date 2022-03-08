using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTransactionScheduled
{
    [DataContract]
    public class TransactionScheduledResponse
    {
        [DataMember]
        public List<TransactionScheduled> ListTransactionScheduled { get; set; }
    }
}
