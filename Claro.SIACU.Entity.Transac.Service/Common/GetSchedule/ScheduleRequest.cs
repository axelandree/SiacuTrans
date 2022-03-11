using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSchedule
{
    [DataContract]
    public class ScheduleRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vUbigeo { get; set; }
        [DataMember]
        public string vJobTypes { get; set; }
        [DataMember]
        public string vCommitmentDate { get; set; }
        [DataMember]
        public string vSubJobTypes { get; set; }
        [DataMember]
        public string vValidateETA { get; set; }
        [DataMember]
        public string vHistoryETA { get; set; }
        [DataMember]
        public string vTimeZona { get; set; }
    }
}
