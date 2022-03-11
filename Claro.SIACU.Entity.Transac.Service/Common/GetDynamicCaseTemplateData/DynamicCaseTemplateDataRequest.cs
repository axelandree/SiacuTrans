using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDynamicCaseTemplateData
{
    [DataContract]
    public class DynamicCaseTemplateDataRequest:Claro.Entity.Request
    {
        [DataMember]
        public string vCasoID { get; set; }
    }
}
