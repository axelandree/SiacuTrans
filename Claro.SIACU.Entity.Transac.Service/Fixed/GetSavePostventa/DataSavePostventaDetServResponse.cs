using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetSavePostventa
{
   
        [DataContract]
        public class DataSavePostventaDetServResponse
        {
            [DataMember]
            public string strErrmsg_out { get; set; }

            [DataMember]
            public int intResultado_out { get; set; }
        }
    
}
