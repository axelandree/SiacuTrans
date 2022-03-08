using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetCall
{
    /// <summary>
    /// CAlarcon - 18/05/2017
    /// </summary>
    public class CallResponse
    {
        [DataMember]
        public Call objCall { get; set; }

        [DataMember]
        public List<Call> lstCall { get; set; }

        [DataMember]
        public string idTransaction { get; set; }
        [DataMember]
        public string PhonfNroGener { get; set; }
        [DataMember]
        public string PhonFnlNroGener { get; set; }
    }
}
