using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class GetValidateQuantityCampaignKindProductResponse
    {
        [DataMember(Name = "codProducto")]
        public string CodProduct {get;set;}
        [DataMember(Name = "descProducto")]
        public string DescProduct {get;set;}
        [DataMember(Name = "cantMaxProducto")]
        public string CantMaxProduct { get; set; }
    }
}
