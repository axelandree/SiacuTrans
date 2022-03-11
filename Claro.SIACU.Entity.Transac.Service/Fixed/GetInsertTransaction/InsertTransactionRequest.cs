using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertTransaction
{
    [DataContract]
    public class InsertTransactionRequest : Claro.Entity.Request
    {
        [DataMember]
        public Transfer oTransfer { get; set; }
    }
}
