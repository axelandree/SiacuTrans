using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetGenerateConstancy
{
    [DataContract(Name = "GenerateConstancyRequestCommon")]
    public class GenerateConstancyRequest : Claro.Entity.Request
    {
        [DataMember]
        public ParametersGeneratePDF ParametersGeneratePDFGeneric { get; set; } 
    }
}
