using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateSOT
{
    [DataContract(Name = "GenerateSOTResponseFixed")]
    public class GenerateSOTResponse
    {

        [DataMember]
        public string IdGenerateSOT { get; set; }
        [DataMember]
        public string DsescTransfer { get; set; }
        [DataMember]
        public string CodMessaTransfer { get; set; }
        [DataMember]
        public string DescMessaTransfer { get; set; }
    }
}
