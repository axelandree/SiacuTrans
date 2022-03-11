using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetLineData
{
    [DataContract(Name = "LineDataRequestPrepaid")]
    public class LineDataRequest : Claro.Entity.Request
    { 
        [DataMember]
        public ConsultLine Info { get; set; }
    }
}
