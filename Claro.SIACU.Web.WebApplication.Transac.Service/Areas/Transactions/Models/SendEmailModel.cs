using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class SendEmailModel
    {
        public string strSender { get; set; }
        public string strTo { get; set; }
        public string strCC { get; set; }
        public string strBCC { get; set; }
        public string strSubject { get; set; }
        public string strMessage { get; set; }
        public string strAttached { get; set; }
        public byte[] byteAttached { get; set; }
        public string strIdSession { get; set; }
        public string strConstFile { get; set; }
        public string strSubjectEmail { get; set; }
        public string strMsgEmailCall { get; set; }
        public List<string> lsAttached { get; set; }
    }
}