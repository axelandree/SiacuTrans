using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.InsertRedirectCommunication
{
    [DataContract(Name = "InsertRedirectComRequestDashboard")]
    public class InsertRedirectComRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Option { get; set; }
        [DataMember]
        public string AppDest { get; set; }
        [DataMember]
        public string IpClient { get; set; }
        [DataMember]
        public string IpServer { get; set; }
        [DataMember]
        public string JsonParameters { get; set; }
        [DataMember]
        public string Sequence { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}
