using Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PutDataClient
{
    [DataContract]
    public class DataClientRequest : HeaderToBe
    {
        [DataMember]
        public GetUpdateDataClientRequest GetDataClientRequest { get; set; } 
        [DataMember]
        public BillingAddressRequest BillingAddressRequest { get; set; }
        [DataMember]
        public HistoryClientRequest HistoryClientRequest { get; set; }
        [DataMember]
        public HistoryClientRequest HistoryClientDataRequest { get; set; }
        [DataMember]
        public HistoryClientRequest HistoryClientLegalRequest { get; set; }
        [DataMember]
        public HistoryClientRequest HistoryClientFactRequest { get; set; }
        [DataMember]
        public List<HistoryClientRequest> ListRepresentanteLegal { get; set; }
    } 
}
