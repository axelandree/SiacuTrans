using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRedirectSession
{
    [DataContract(Name = "RedirectSessionRequestDashboard")]
    public class RedirectSessionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string Option { get; set; }

    }
}
