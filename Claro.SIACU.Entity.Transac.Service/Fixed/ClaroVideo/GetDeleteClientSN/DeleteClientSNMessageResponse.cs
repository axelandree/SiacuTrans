using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetDeleteClientSN
{
    [DataContract]
    public class DeleteClientSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public DeleteClientSNHeaderResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public DeleteClientSNBodyResponse Body { get; set; }
    }
}
