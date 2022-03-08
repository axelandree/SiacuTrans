using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertTemplateGeneral
{
    [DataContract]
    public class InsertTemplateGeneralResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public string rCodigoRetornoTransaccion { get; set; }
        [DataMember]
        public string rMensajeErrorTransaccion { get; set; }
    }
}
