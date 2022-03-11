using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
     [DataContract(Name = "PackageItem")]
    public class PackageItem
    {
        [DataMember(Name = "price")]
        public string  price { get; set; }

        [DataMember(Name = "packageId")]
        public string packageId { get; set; }

        [DataMember(Name = "currency")]
        public string currency { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "acronym")]
        public string acronym { get; set; }

    }
}
