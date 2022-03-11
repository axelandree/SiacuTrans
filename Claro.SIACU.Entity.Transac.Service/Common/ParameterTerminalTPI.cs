using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "ParameterTerminalTPI")]
    public class ParameterTerminalTPI
    {
        [DataMember]
        public string ParameterID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string ValorC { get; set; }
        [DataMember]
        public double ValorN { get; set; }
        [DataMember]
        public string ValorL { get; set; }

    }
}
