using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "packagesList")]
    public class PackagesList
    {
        [DataMember(Name = "package")]
        public List<PackageL> package { get; set; }

   

    }
}
