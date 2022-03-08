using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract]
    public class CallDetailSummary
    {
        [DataMember]
        public string Co_ID { get; set; }

        [DataMember]
        public string NroOrd { get; set; }


        [DataMember]
        public string MSISDN { get; set; }



        [DataMember]
        public string CallNumber { get; set; }



        [DataMember]
        public string CallDate { get; set; }



        [DataMember]
        public string CallTime { get; set; }



        [DataMember]
        public string CallDuration { get; set; }
    }
}
