using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetListItem
{
    [DataContract(Name = "ListItemResponse")]
    public class ListItemResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> lstListItem { get; set; }
    
    
    }

}
