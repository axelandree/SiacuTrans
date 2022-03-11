using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListEbuildings
{
     [DataContract(Name = "ListEbuildingsFixedResponse")]
  public   class ListEbuildingsResponse
    {
         [DataMember]
         public List<Entity.Transac.Service.Fixed.GenericItem> ListEbuildings { get; set; }
    }
}
