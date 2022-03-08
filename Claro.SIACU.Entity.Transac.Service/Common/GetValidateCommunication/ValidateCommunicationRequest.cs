using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCommunication
{
    public class ValidateCommunicationRequest : Claro.Entity.Request
    {

        [DataMember]
        public string Sequence { get; set; }

        [DataMember]
        public string Server { get; set; }


    }
}
