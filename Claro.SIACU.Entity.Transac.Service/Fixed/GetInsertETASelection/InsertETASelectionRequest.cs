using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertETASelection
{
    [DataContract]
    public class InsertETASelectionRequest : Claro.Entity.Request
    {
        [DataMember]
        public int vidconsulta { get; set; }
        [DataMember]
        public string vidInteraccion { get; set; }
        [DataMember]
        public DateTime vfechaCompromiso { get; set; }
        [DataMember]
        public string vfranja { get; set; }
        [DataMember]
        public string vid_bucket { get; set; }
    }
}
