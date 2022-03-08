using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListPlans
{
    [DataContract(Name="ListPlansFixedResponse")]
    public class ListPlansResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Fixed.GenericItem> ListPlans { get; set; }
    }
}
