using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListScheduledTransactions
{
    [DataContract(Name = "ListScheduledTransactionsRequest")]
    public class ListScheduledTransactionsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string ApplicationCode { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string UserApp;
        [DataMember]

        public string ServiCoId { get; set; }
        [DataMember]

        public string StrStartDate { get; set; }
        [DataMember]
        public string StrEndDate { get; set; }
        [DataMember]

        public string ServiceState { get; set; }
        [DataMember]
        public string Advisor { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public string CodeInteraction { get; set; }
        [DataMember]
        public string NameCACDAC { get; set; } 
    }
}
