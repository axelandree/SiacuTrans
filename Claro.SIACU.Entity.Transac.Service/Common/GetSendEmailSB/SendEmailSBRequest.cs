using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSendEmailSB
{
    [DataContract]
    public class SendEmailSBRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public string strAppID { get; set; }
        [DataMember]
        public string strAppCode { get; set; }
        [DataMember]
        public string strAppUser { get; set; }
        [DataMember]
        public string strRemitente { get; set; }
        [DataMember]
        public string strDestinatario { get; set; }
        [DataMember]
        public string strAsunto { get; set; }
        [DataMember]
        public string strMensaje { get; set; }
        [DataMember]
        public string strHTMLFlag { get; set; }
        [DataMember]
        public byte[] Archivo { get; set; }
        [DataMember]
        public string strNomFile { get; set; }
    }
}
