using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertDetailServiceInteraction
{
    [DataContract]
    public class InsertDetailServiceInteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string codinterac { get; set; }
        [DataMember]
        public string nombreserv { get; set; }
        [DataMember]
        public string tiposerv { get; set; }
        [DataMember]
        public string gruposerv { get; set; }
        [DataMember]
        public string cf { get; set; }
        [DataMember]
        public string equipo { get; set; }
        [DataMember]
        public string cantidad { get; set; }
    }
}
