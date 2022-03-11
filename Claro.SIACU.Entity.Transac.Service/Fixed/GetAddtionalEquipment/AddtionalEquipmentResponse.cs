using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAddtionalEquipment
{
    [DataContract]
    public class AddtionalEquipmentResponse
    {
        [DataMember]
        public List<PlanService> LstPlanServices { get; set; }
    }
}
