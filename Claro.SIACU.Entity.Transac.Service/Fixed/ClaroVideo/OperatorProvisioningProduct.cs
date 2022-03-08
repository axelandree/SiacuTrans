using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "operatorProvisioningProductRequest")]
    public class OperatorProvisioningProduct
    {
        [DataMember(Name = "partnerID")]
        public string partnerID { get; set; }

        [DataMember(Name = "productID")]
        public string productID { get; set; }

        [DataMember(Name = "level")]
        public string level { get; set; }

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
