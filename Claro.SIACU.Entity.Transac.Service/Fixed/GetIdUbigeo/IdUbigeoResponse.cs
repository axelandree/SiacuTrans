using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetIdUbigeo
{
    [DataContract(Name="IdUbigeoFixedResponse")]
   public  class IdUbigeoResponse
    {
        [DataMember]
        public string strIdUbigeo { get; set; }
    }
}
