using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertInt
{
    [DataContract(Name = "InsertIntRequestCommon")]
   public  class InsertIntRequest:Claro.Entity.Request
    {
       [DataMember]
       //public InsertInteract item { get; set; }
       public Iteraction item { get; set; }
    }
}
