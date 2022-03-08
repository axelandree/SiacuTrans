using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRecordTransExtInt
{
    [DataContract(Name = "RecordTranferExtIntResponseFixed")]
    public class RecordTranferExtIntResponse
    {
        [DataMember]
        public string IdTransfer { get; set; }

        [DataMember]
        public string DescTransfer { get; set; }
        [DataMember]
        public string CodMessaTransfer { get; set; }
        [DataMember]
        public string DescMessaTransfer { get; set; }

    }
}
