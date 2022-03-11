using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class SuscriberQuota
    {
        [DataMember]
        public string QTATIMESTAMP { get; set; }
        [DataMember]
        public string QTARSTDATETIME { get; set; }
        [DataMember]
        public string QTALASTRSTDATETIME { get; set; }
        [DataMember]
        public string QTASTATUS { get; set; }
        [DataMember]
        public string QTACONSUMPTION { get; set; }
        [DataMember]
        public string QTABALANCE { get; set; }
        [DataMember]
        public string QTAVALUE { get; set; }
        [DataMember]
        public string QTANAME { get; set; }
        [DataMember]
        public string SRVNAME { get; set; }
    }
}
