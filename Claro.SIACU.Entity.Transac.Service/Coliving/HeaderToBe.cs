using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving
{
    [System.Serializable]
    [DataContract]
    public class HeaderToBe
    {
        [DataMember]
        public string IdTransaccion { get; set; }

        [DataMember]
        public string MsgId { get; set; }

        [DataMember]
        public string TimesTamp { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Accept { get; set; }

        [DataMember]
        public string Channel { get; set; }

        [DataMember]
        public string IpAplicacion { get; set; }
    }
}
