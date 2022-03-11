using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsTemplateInteraction
{

    [DataContract(Name = "InsTemplateInteractionResponseCommon")]
    public class InsTemplateInteractionResponse
    {
        [DataMember]
        public bool ProcessSucess { get; set; }

        [DataMember]
        public string FlagInsercion { get; set; }

        [DataMember]
        public string MsgText { get; set; }
    }
}
