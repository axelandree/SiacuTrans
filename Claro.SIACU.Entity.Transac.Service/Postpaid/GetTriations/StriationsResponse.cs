using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTriations
{
    public class StriationsResponse
    {
        [DataMember]
        public List<Striations> Striations { get; set; }
    }
}
