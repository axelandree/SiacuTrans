using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAddressUpdate
{
    [DataContract(Name = "AddressUpdateFixedResponse")]
    public class AddressUpdateResponse 
    {
        [DataMember]
        public bool blnResult { get; set; }

    }
}
