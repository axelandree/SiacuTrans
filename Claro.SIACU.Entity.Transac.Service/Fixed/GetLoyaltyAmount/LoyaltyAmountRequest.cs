using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetLoyaltyAmount
{
    [DataContract]
    public class LoyaltyAmountRequest : Claro.Entity.Request
    {
        [DataMember]
        public int iTipo { get; set; }
    }
}
