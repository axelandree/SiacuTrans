using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultMarkModel
{
    [DataContract]
    public class ConsultMarkModelResponse
    {
        [DataMember]
        public string result { get; set; }
    }
}
