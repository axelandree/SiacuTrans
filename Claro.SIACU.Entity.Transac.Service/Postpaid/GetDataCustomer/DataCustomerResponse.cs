using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomer
{
    [DataContract(Name = "DataCustomerResponsePostPaid")]
    public class DataCustomerResponse
    {
        [DataMember]
        public string StrResponse { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public Client Cliente { get; set; }

    }
}
