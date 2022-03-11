using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetChangeEquipment
{
    public class ChangeEquipmentResponse
    {
        [DataMember]
        public string idInteraccion { get; set; }

        [DataMember]
        public string numeroSOT { get; set; }

        [DataMember]
        public string rutaConstancia { get; set; } 

        [DataMember]
        public string codeResponse { get; set; }

        [DataMember]
        public DateTime date { get; set; }

        [DataMember]
        public string descriptionResponse { get; set; }

        [DataMember]
        public List<string> errorDetails { get; set; }

        [DataMember]
        public string errorLocation { get; set; }

        [DataMember]
        public string origin { get; set; }

        [DataMember]
        public int status { get; set; } 
    }
}
