using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertLoyalty
{
    [DataContract]
    public class InsertLoyaltyRequest : Claro.Entity.Request
    {
        [DataMember]
        public Customer oCustomer { get; set; }
        [DataMember]
        public string vCodSoLot { get; set; }
        [DataMember]
        public int vFlagDirecFact { get; set; }
        [DataMember]
        public string vUser { get; set; }
        [DataMember]
        public DateTime vFechaReg { get; set; }
    }
}
