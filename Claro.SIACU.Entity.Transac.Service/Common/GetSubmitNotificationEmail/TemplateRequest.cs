using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class TemplateRequest
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "DataFileURL")]
        public string DataFileURL { get; set; }
        [DataMember(Name = "SFTPFileURL")]
        public SFTPFileURLRequest SFTPFileURLRequest { get; set; }
    }
}
