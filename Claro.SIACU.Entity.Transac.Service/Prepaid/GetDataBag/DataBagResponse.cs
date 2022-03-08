using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetDataBag
{
    [DataContract(Name = "DataBagResponsePrepaid")]
    public class DataBagResponse
    {
        [DataMember]
        public List<Account> ListDataBag { get; set; }
        [DataMember]
        public string StrDate{ get; set; }
        [DataMember]
        public string StrBalance { get; set; }
    }
}
