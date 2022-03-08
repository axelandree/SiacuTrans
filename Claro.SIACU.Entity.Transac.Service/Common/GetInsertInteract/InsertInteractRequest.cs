using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteract
{
    [DataContract(Name = "InsertInteractRequestCommon")]
    public class InsertInteractRequest: Claro.Entity.Request
    {
        [DataMember]
        //public InsertInteract item { get; set; }
        public Iteraction item { get; set; }
    }
}
