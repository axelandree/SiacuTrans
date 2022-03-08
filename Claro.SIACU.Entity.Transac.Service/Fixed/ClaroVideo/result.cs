using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "Result")]
    public class Result
    {
        [DataMember(Name = "resultCode")]
        public string resultCode { get; set; }

        [DataMember(Name = "resultMessage")]
        public string resultMessage { get; set; }
    }
}
