using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.TargetOnBase
{
    [DataContract(Name = "OnBaseCargaResponse")]
    public class OnBaseCargaResponse
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "codeOnBase")]
        public string codeOnBase { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "codeResponse")]
        public string codeResponse { get; set; }

        [DataMember(Name = "descriptionResponse")]
        public string descriptionResponse { get; set; }

        [DataMember(Name = "date")]
        public DateTime date { get; set; }
    }
}
