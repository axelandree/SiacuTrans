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
    public class DataClientResponse
    {
        [DataMember]
        public GetUpdateDataClientResponse GetDataClientResponse { get; set; }
        [DataMember]
        public BillingAddressResponse BillingAddressResponse { get; set; }
        [DataMember]
        public HistoryClientResponse HistoryClientResponse { get; set; }
        [DataMember]
        public HistoryClientResponse HistoryClientDataResponse { get; set; }
        [DataMember]
        public HistoryClientResponse HistoryClientLegalResponse { get; set; } 
        [DataMember]
        public HistoryClientResponse HistoryClientFactResponse { get; set; }
    }
}
