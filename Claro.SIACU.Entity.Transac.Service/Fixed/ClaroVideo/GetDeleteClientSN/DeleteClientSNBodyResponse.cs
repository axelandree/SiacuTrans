using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetDeleteClientSN
{
    [DataContract]
    public class DeleteClientSNBodyResponse
    {
        [DataMember(Name = "deleteUserOttResponse")]
        public DeleteUserOttResponse deleteUserOttResponse { get; set; }
    }
}
