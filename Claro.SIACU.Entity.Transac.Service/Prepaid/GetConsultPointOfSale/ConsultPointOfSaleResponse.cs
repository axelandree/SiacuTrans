using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetConsultPointOfSale
{   
    [DataContract]
    public class ConsultPointOfSaleResponse
    {
        [DataMember]
        public string flag_biometria{ get; set; }
    }
}
