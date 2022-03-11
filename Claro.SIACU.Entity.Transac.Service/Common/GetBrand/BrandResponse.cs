using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBrand
{
    [DataContract(Name = "BrandResponsePrepaid")]
    public class BrandResponse
    {
        [DataMember]
        public List<ListItem> ListBrand { get; set; }
    }
}
