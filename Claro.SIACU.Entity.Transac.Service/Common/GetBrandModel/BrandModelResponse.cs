using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBrandModel
{
    [DataContract(Name = "BrandModelResponse")]
    public class BrandModelResponse
    {
        [DataMember]
        public List<ListItem> ListBrandModel { get; set; }
    }
}
