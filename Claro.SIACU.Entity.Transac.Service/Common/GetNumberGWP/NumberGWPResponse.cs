using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetNumberGWP
{
    [DataContract(Name = "NumberGWPResponse")]
    public class NumberGWPResponse
    {
        [DataMember]
        public string Number { get; set; }
    }
}
