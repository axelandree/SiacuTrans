using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetMzBloEdiType
{
    [DataContract(Name = "MzBloEdiTypeRequestCommon")]
    public class MzBloEdiTypeRequest : Claro.Entity.Request
    {

       [DataMember]
       public string CodMzBloEdiType { get; set; }
    }
}

