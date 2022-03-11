using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient
{
    [DataContract]
    public class HistoryClientRequest
    {

        [DataMember]
        public HistoryClient HistoryClient { get; set; }
    } 
}
