using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability
{
    [DataContract]
    public class InsertTraceabilityRequest : Claro.Entity.Request
    {

        [DataMember]
        public RegisterTraceability item { get; set; }
        [DataMember]
        public List<RegisterTraceabilityOptionalList> lstRegisterTraceabilityOptionalList { get; set; }


    }
}
