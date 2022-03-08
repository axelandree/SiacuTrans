using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListEbuildings
{
    [DataContract(Name = "ListEbuildingsFixedRequest")]
  public   class ListEbuildingsRequest:Claro.Entity.Request
    {
        [DataMember]
        public string strCodePlan { get; set; }
    }
}
