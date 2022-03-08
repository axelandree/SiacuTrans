using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInteraction
{
    [DataContract]
    public class InteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public Typification objTypification { get; set; }
    }
}
