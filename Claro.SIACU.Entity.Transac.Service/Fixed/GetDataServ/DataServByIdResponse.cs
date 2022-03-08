using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDataServ
{
    [DataContract(Name = "DataServByIdResponseLTE")]
    public class DataServByIdResponse
    {
        [DataMember]
        public List<DetailInteractionService> DataServById { get; set; }
    }
}
