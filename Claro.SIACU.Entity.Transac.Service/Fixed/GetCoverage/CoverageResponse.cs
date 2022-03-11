using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCoverage
{
    [DataContract(Name="CoverageFixedResponse")]
   public  class CoverageResponse
    {
       [DataMember]
       public string strCoverage { get; set; }
    }
}
