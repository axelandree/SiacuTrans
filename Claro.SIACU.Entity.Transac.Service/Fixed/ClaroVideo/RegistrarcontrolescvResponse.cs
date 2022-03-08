using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
   public class RegistrarcontrolescvResponse
    {
       [DataMember(Name = "codRpta")]
       public string codRpta { get; set; }

       [DataMember(Name = "msjRpta")]
       public string msjRpta { get; set; }
    }
}
