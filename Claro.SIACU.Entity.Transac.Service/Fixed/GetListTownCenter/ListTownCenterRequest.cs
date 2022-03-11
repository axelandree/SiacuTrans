using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListTownCenter
{
    [DataContract(Name="ListTownCenterFixedRequest")]
    public class ListTownCenterRequest:Claro.Entity.Request
    {
        [DataMember]
        public string strUbigeo { get; set; }
    }
}
