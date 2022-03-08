using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomer
{
    [DataContract(Name = "DataCustomerRequestPostPaid")]
    public class DataCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strcustomerid { get; set; }

        [DataMember]
        public string strtelefono { get; set; }

        [DataMember]
        public string strIdSession { get; set; }

        [DataMember]
        public string strTransaccion { get; set; }

    }
}
