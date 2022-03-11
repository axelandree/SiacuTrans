using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBranchCustomer
{
    [DataContract]
    public class BranchCustomerResquest:Claro.Entity.Request
    {
        [DataMember]
        public int an_customer_id { get; set; }
    }
}
