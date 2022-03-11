using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAvailabilitySimcard
{
    [DataContract]
    public class AvailabilitySimcardRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SimcardSeries { get; set; }

        [DataMember]
        public string ContractId { get; set; }

        [DataMember]
        public string SimcardSeriesOld { get; set; }
    }
}
