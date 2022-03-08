using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractHFC
{
    [DataContract]
    public class GetInsertInteractHFCRequest : Claro.Entity.Request
    {
        [DataMember]
        public Interaction Interaction { get; set; }
    }
}
