using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultIGV
    {
        [DataMember]
        public double igv { get; set; }
        [DataMember]
        public double igvD { get; set; }
        [DataMember]
        public string impudFecFinVigencia { get; set; }
        [DataMember]
        public string impudFecIniVigencia { get; set; }
        [DataMember]
        public string impudFecRegistro { get; set; }
        [DataMember]
        public string impunTipDoc { get; set; }
        [DataMember]
        public string imputId { get; set; }
        [DataMember]
        public string impuvDes { get; set; }
    }
}
