using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class SFTPFileURLRequest
    {
        [DataMember(Name = "SFTPDomain")]
        public string SFTPDomain { get; set; }
        [DataMember(Name = "SFTPUsername")]
        public string SFTPUsername { get; set; }
        [DataMember(Name = "SFTPPassword")]
        public string SFTPPassword { get; set; }
        [DataMember(Name = "SFTPPath")]
        public string SFTPPath { get; set; }
    }
}
