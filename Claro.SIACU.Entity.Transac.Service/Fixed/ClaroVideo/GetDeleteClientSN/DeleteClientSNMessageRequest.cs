using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetDeleteClientSN
{
    [DataContract]
    public class DeleteClientSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public DeleteClientSNHeaderRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public DeleteClientSNBodyRequest Body { get; set; }
    }
}
