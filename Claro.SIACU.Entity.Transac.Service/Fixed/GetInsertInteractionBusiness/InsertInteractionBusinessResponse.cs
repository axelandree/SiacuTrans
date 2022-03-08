using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractionBusiness
{
    [DataContract]
    public class InsertInteractionBusinessResponse
    {
        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string InteractionId { get; set; }

        [DataMember]
        public string FlagInsercion { get; set; }

        [DataMember]
        public string MsgText { get; set; }

        [DataMember]
        public string FlagInsercionInteraction { get; set; }

        [DataMember]
        public string MsgTextInteraction { get; set; }

        [DataMember]
        public string CodReturnTransaction { get; set; }

        [DataMember]
        public string MsgTextTransaccion { get; set; }
    }
}
