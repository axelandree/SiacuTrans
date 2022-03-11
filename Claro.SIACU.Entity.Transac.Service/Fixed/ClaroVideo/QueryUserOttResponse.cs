using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "queryUserOttResponse")]
    public class QueryUserOttResponse
    {
        [DataMember(Name = "resultCode")]
        public string resultCode { get; set; }

        [DataMember(Name = "resultMessage")]
        public string resultMessage { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "userData")]
        public UserData userData { get; set; }

        [DataMember(Name = "subscriptionList")]
        public SubscriptionList subscriptionList { get; set; }

        [DataMember(Name = "countryId")]
        public string countryId { get; set; }

        [DataMember(Name = "serviceName")]
        public string serviceName { get; set; }

        [DataMember(Name = "providerId")]
        public string providerId { get; set; }

        [DataMember(Name = "extensionInfo")]
        public List<ExtensionInfo> extensionInfo { get; set; }
    }
}
