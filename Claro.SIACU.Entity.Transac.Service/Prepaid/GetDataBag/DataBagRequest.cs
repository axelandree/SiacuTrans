using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetDataBag
{
    [DataContract(Name = "DataBagRequestPrepaid")]
    public class DataBagRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string UserApplication { get; set; }
        [DataMember]
        public string NameApplication { get; set; }

        [DataMember]
        public string IdTransaction { get; set; }
        
    }
}
