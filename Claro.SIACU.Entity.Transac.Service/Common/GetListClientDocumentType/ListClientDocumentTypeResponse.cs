using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetListClientDocumentType
{ 
    [DataContract(Name = "ListClientDocumentTypeResponseCommon")]
    public class ListClientDocumentTypeResponse
    {
        [DataMember]
        public List<ListItem> ListClientDoctType{ get; set; }
    }
}
