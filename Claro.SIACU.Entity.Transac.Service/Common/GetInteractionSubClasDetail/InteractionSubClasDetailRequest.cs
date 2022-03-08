using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInteractionSubClasDetail
{
    [DataContract(Name = "InteractionSubClasDetailRequestCommon")]
    public class InteractionSubClasDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public InteractionSubClasDetail item  { get; set; }

    }
}
