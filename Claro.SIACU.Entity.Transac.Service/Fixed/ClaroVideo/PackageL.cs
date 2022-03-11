using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
      [DataContract(Name = "packageL")]
   public class PackageL
   {
       [DataMember(Name = "item")]
       public List<PackageLItem> item { get; set; }
    }
}
