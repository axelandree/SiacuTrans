using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetLineData
{
    [DataContract(Name = "LineDataResponsePrepaid")]
    public class LineDataResponse
    {
        [DataMember]
        public List<Account> ListAccount {get;set;}
        [DataMember]
        public List<ListItem> ListTrio {get;set;}
        [DataMember]
        public Line LineItem{get;set;}
        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string Code { get; set; }

    }
}
