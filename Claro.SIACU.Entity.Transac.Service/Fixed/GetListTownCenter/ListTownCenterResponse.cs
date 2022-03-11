using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListTownCenter
{
    [DataContract(Name="ListTownCenterFixedResponse")]
    public class ListTownCenterResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Fixed.GenericItem> ListItem { get; set; }

    }
}
