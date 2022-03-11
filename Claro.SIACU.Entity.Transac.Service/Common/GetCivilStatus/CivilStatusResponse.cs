using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetCivilStatus
{
    [DataContract(Name = "CivilStatusResponsePrepaid")]
    public class CivilStatusResponse
    {
        [DataMember]
        public List<ListItem> ListCivilStatus { get; set; }
    }
}
