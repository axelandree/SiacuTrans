using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetParameterData
{
    [DataContract(Name = "ParameterDataRequestCommon")] 
    public class ParameterDataRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Name{get;set;}
    }
}
