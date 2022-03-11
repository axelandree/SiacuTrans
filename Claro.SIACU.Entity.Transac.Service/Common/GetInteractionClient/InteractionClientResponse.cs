using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInteractionClient
{

     [DataContract(Name = "GetInteractionClientResponseCommon")]
   public  class InteractionClientResponse
    {

         [DataMember]
         public List<Iteraction> ListInteractionClient { get; set; }

         
         [DataMember]
         public string  Flagcreation { get; set; }

         
         [DataMember]
         public string Msgtext  { get; set; }
    }
}
