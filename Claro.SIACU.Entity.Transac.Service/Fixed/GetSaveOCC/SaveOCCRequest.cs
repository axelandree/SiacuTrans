using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetSaveOCC
{
    [DataContract]
    public class SaveOCCRequest : Claro.Entity.Request
    {
        [DataMember]
        public int vCodSot { get; set; }
        [DataMember]
        public int vCustomerId { get; set; }
        [DataMember]
        public DateTime vFechaVig { get; set; }
        [DataMember]
        public double vMonto { get; set; }
        [DataMember]
        public string vComentario { get; set; }
        [DataMember]
        public int vflag { get; set; }
        [DataMember]
        public string vAplicacion { get; set; }
        [DataMember]
        public string vUsuarioAct { get; set; }
        [DataMember]
        public DateTime vFechaAct { get; set; }
        [DataMember]
        public string vCodId { get; set; }
    }
}
