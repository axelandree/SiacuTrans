using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetFactureDBTO
{
    [DataContract]
    public class FactureDBTOResponse
    {
        [DataMember]
        public List<GenericItem> LstGenericItem { get; set; }
    }
}
