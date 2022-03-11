using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetMotiveSoft
{
     [DataContract(Name = "MotiveSoftLTEFixedResponse")]
    public class MotiveSoftResponse
    {

        [DataMember]
        public List<Entity.Transac.Service.Fixed.GenericItem> listMotiveSoft { get; set; }
    }
}
