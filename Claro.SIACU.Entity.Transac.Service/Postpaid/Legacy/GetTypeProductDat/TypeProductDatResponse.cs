using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    public class TypeProductDatResponse
    {
        [DataMember(Name = "responseStatus")]
        public ResponseStatus responseStatus { get; set; }
        [DataMember(Name = "responseData")]
        public ResponseData responseData { get; set; }
    }
}
