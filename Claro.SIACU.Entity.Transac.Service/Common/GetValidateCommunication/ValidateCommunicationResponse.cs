using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateCommunication
{
    public class ValidateCommunicationResponse
    {

        [DataMember]
        public Boolean ResultValCommunication { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string Availability { get; set; }

        [DataMember]
        public string JsonString { get; set; }

        [DataMember]
        public string CodError { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }



    }
}
