using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetBusinessInteraction2
{
    [DataContract(Name = "BusinessInteraction2ResponseCommon")]
  public   class BusinessInteraction2Response
    {

       [DataMember]
       public bool ProcessOK { get; set; }

       [DataMember]
       public string MsgText { get; set; }

       [DataMember]
       public string InteractionId { get; set; }

       [DataMember]
       public string FlagInsertion { get; set; }
    }
}
