using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa
{
    [DataContract(Name = "HistoryToaResponse")]
    public class HistoryToaResponse
    {
        [DataMember]
        public string strCodResult { get; set; }
        [DataMember]
        public string strMsjResult { get; set; }
        [DataMember]
        public string strNroOrden { get; set; }
    }
}
