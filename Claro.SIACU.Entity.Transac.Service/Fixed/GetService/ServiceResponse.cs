using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetService
{
    public class ServiceResponse
    {
        [DataMember]
        public Entity.Transac.Service.Fixed.Service ObjService { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Fixed.Service> ListService { get; set; } 
    }
}
