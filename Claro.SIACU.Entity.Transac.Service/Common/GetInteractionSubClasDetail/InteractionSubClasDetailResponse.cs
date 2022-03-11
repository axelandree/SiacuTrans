using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInteractionSubClasDetail
{
    [DataContract(Name = "InteractionSubClasDetailResponseCommon")]
   public  class InteractionSubClasDetailResponse
    {
        [DataMember]
        public bool ProcesSucess { get; set; }

        [DataMember]
        public int CodeError { get; set; }

        [DataMember]
        public string MsgError { get; set; }


    }
}
