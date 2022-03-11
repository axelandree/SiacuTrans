using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertTemplateInteraction
{
      [DataContract(Name = "InsertTemplateInteractionResponseCommon")]
    public class InsertTemplateInteractionResponse
    {
          [DataMember]
          public bool ProcesSucess { get; set; }

          [DataMember]
          public string FlagInsercion{ get; set; }

          [DataMember]
          public  string MsgText{ get; set; }


    }
}
