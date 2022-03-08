using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultImei
{
    [DataContract]
    public class ConsultImeiResponse
    {
        [DataMember]
        public List<ConsultImei> ListConsultImei { get; set; }
    }
}
