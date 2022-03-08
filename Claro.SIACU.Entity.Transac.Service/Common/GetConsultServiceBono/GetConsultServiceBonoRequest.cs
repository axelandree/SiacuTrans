using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoRequest : Claro.Entity.Request
    {
        [DataMember(Name = "coId")]
        public string CoId { get; set; }

        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

    }
}
