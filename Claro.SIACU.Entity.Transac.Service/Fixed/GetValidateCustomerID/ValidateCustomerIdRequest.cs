using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateCustomerID
{
    [DataContract]
    public class ValidateCustomerIdRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Phone { get; set; }

    }
}
