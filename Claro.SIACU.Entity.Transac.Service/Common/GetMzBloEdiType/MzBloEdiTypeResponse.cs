using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetMzBloEdiType
{
    [DataContract(Name = "MzBloEdiTypeResponseCommon")]
    public class MzBloEdiTypeResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> ListMzBloEdiType { get; set; }
    }


}
