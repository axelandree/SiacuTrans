using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TIME")]
    [DataContract(Name = "CallDetailPostPaid")]
    public class CallDetail
    {
        public string NROORD { get; set; }

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string CALLBEFORE { get; set; }

        [DataMember]
        public string CALLAFTER { get; set; }

        [DataMember]
        public string CALLDESTINATION { get; set; }


        [Data.DbColumn("CALLNUMBER")]
        [DataMember]
        public string CALLNUMBER { get; set; }


        [Data.DbColumn("CALLDATE")]
        [DataMember]
        public string CALLDATE { get; set; }


        [Data.DbColumn("CALLTIME")]
        [DataMember]
        public string CALLTIME { get; set; }


        [Data.DbColumn("CALLDURATION")]
        [DataMember]
        public string CALLDURATION { get; set; }


        [Data.DbColumn("CALLTOTAL")]
        [DataMember]
        public decimal CALLTOTAL { get; set; }


        [Data.DbColumn("CALLORIGIN")]
        [DataMember]
        public string CALLORIGIN { get; set; }


        [Data.DbColumn("TIPOLLAMADA")]
        [DataMember]
        public string TIPOLLAMADA { get; set; }
    }
}
