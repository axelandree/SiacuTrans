using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoDetailByIdService
{
    [DataContract]
    public class DecoDetailByIdServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdServicio { get; set; }
    }
}
