using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract]
    public class LineDetail
    {
        [DataMember]
        public string NroOrd { get; set; }
        [DataMember]
        public string CodId { get; set; }
        [DataMember]
        public string DnNum { get; set; }
        [DataMember]
        public string StrDtIni { get; set; }
        [DataMember]
        public string StrDtFin { get; set; }

        [DataMember]
        public DateTime DtIni { get; set; }
        [DataMember]
        public DateTime DtFin { get; set; }
    }
}
