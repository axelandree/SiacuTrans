using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultIGV
{
    [DataContract]
    public class ConsultIGVResponse
    {
        [DataMember]
        public List<ConsultIGV> ListConsultIGV { get; set; }
    }
}
