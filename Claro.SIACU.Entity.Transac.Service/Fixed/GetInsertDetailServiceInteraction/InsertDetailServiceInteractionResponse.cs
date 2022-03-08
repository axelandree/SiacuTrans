using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction
{
    [DataContract]
    public class InsertDetailServiceInteractionResponse
    {
        [DataMember]
        public bool rResul { get; set; }
        [DataMember]
        public string resultado { get; set; }
        [DataMember]
        public string mensaje { get; set; }
    }
}
