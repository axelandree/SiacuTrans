using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "packages")]
    public class Package
    {
        [DataMember(Name = "item")]
        public List<PackageItem> item { get; set; }
    }
}
