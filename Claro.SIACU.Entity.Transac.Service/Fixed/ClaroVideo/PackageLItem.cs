using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "PackageLItem")]
    public class PackageLItem
    {
        //consult lista paquetes cliente
        [DataMember(Name = "key")]
        public string key { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }

        [DataMember(Name = "price")]
        public string price { get; set; }

        //consult lista paquetes 

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
