using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Balance { get; set; }
        [DataMember]
        public string ExpirationDate { get; set; }
        [DataMember]
        public string Order { get; set; }
    }
}
