using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertGeneral
{
    [DataContract]
   public  class InsertGeneralResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rInteraccionId { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public string rFlagInsercionInteraccion { get; set; }
        [DataMember]
        public string rMsgTextInteraccion { get; set; }
        [DataMember]
        public string rCodigoRetornoTransaccion { get; set; }
        [DataMember]
        public string rMsgTextTransaccion { get; set; }
    }
}
