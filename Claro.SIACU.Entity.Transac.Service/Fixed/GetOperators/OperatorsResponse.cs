using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetOperators
{
    [DataContract(Name = "OperatorsResponseHfc")]
    public class OperatorsResponse
    {
        [DataMember]
        public List<Operator> Operators { get; set; }
    }
}
