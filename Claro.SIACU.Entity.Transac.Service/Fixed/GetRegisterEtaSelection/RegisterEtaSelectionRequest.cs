using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegisterEtaSelection
{
    [DataContract]
    public class RegisterEtaSelectionRequest : Claro.Entity.Request
    {
        [DataMember]
        public int IdConsulta { get; set; }
        [DataMember]
        public string IdInteraccion { get; set; }
        [DataMember]
        public string FechaCompromiso { get; set; }
        [DataMember]
        public string Franja { get; set; }
        [DataMember]
        public string Id_Bucket { get; set; }


    }
}
