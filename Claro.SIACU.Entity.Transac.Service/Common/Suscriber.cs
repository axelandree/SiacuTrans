using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class Suscriber
    {
        [DataMember]
        public string USRIMSI { get; set; }
        [DataMember]
        public string USRHOMESRVZONE { get; set; }
        [DataMember]
        public string USRBILLCYCLEDATE { get; set; }
        [DataMember]
        public string USRSTATION { get; set; }
        [DataMember]
        public string USRCATEGORY { get; set; }
        [DataMember]
        public string USRPAIDTYPE { get; set; }
        [DataMember]
        public string USRSTATE { get; set; }
        [DataMember]
        public string USRMSISDN { get; set; }
        [DataMember]
        public string USRIDENTIFIER { get; set; }
    }
}
