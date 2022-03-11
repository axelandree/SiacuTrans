using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertLogTrx
{
    [DataContract(Name = "InsertLogTrxResponseCommon")]
    public class InsertLogTrxResponse
    {
        [DataMember]
        public string FlagInsertion { get; set; }

        [DataMember]
        public bool Exito { get; set; }
    }
}
