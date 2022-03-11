using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataLine
{
    [DataContract(Name = "DataLineRequestPostPaid")]
    public class DataLineRequest : Claro.Entity.Request
    {
        //[DataMember]
        //public int CustomerID { get; set; }
        [DataMember]
        public string ContractID { get; set; }
        //[DataMember]
        //public string Application { get; set; }
        //[DataMember]
        //public string Telephone { get; set; }
        //[DataMember]
        //public string IdTransaction { get; set; }
        //[DataMember]
        //public string IpApplication { get; set; }
        //[DataMember]
        //public string ApplicationName { get; set; }
        //[DataMember]
        //public string UserApplication { get; set; }
        //[DataMember]
        //public string CustomerType { get; set; }
        //[DataMember]
        //public string DocumentType { get; set; }
        //[DataMember]
        //public string DocumentNumber { get; set; }
        //[DataMember]
        //public string Modality { get; set; } 
    }
}
