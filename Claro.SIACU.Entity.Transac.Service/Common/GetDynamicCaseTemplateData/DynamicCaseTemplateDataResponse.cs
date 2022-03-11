using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDynamicCaseTemplateData
{
    [DataContract]
    public class DynamicCaseTemplateDataResponse
    {
        [DataMember]
        public CaseTemplate oCaseTemplate { get; set; }
        [DataMember]
        public string vFLAG_CONSULTA { get; set; }
        [DataMember]
        public string vMSG_TEXT { get; set; }
    }
}
