using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.ReadOptionsByUser
{
    [DataContract(Name = "ReadOptionsByUserRequest")]
    public class ReadOptionsByUserRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdSession { set; get; }
        [DataMember]
        public string Transaction { set; get; }
        [DataMember]
        public int IdAplication { set; get; }
        [DataMember]
        public int IdUser { set; get; }
        [DataMember]
        public string IdAplicacion { set; get; }
    }
}
