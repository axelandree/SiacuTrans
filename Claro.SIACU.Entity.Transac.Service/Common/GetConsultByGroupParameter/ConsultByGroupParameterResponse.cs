using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultByGroupParameter
{
    [DataContract]
    public class ConsultByGroupParameterResponse
    {
        [DataMember]
        public List<ConsultByGroupParameter> ListConsultByGroupParameter { get; set; }

        
    }
}
