using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVBodyRequest
    {
        [DataMember(Name = "coId")]
        public string CoId { get; set; }
        [DataMember(Name = "meses")]
        public string Meses { get; set; }
        [DataMember(Name = "codSubClase")]
        public string CodSubClase { get; set; }
    }
}
