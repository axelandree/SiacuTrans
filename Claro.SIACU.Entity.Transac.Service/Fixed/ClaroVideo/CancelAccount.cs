using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "cancelAccountRequest")]
    public class CancelAccount
    {
        [DataMember(Name = "partnerID")]
        public int partnerID { get; set; }

        [DataMember(Name = "productID")]
        public int productID { get; set; }

        [DataMember(Name = "level")]
        public int level { get; set; }

        [DataMember(Name = "operatorUser")]
        public OperatorUser operatorUser { get; set; }

        [DataMember(Name = "ChildUser")]
        public ChildUser ChildUser { get; set; }

        [DataMember(Name = "countryID")]
        public string countryID { get; set; }

        [DataMember(Name = "extensionInfo")]
        public List<ExtensionInfo> extensionInfo { get; set; }

    }
}
