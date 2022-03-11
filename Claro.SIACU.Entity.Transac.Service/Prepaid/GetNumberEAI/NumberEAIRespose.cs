using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetNumberEAI
{
        [DataContract(Name = "NumberEAIResponse")]
    public class NumberEAIResponse  
     { 
        [DataMember]
       public string vNumber { get; set; }
    }

}
