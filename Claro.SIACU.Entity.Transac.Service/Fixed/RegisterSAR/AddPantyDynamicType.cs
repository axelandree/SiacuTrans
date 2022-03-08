using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class AddPantyDynamicType
    {
        [DataMember]
        public string piTipo { get; set; }
        [DataMember]
        public string piValor1 { get; set; }
        [DataMember]
        public string piValor2 { get; set; }
        [DataMember]
        public string piValor3 { get; set; }
        [DataMember]
        public string piValor4 { get; set; }
        [DataMember]
        public string piValor5 { get; set; }
        [DataMember]
        public string piValor6 { get; set; }
        [DataMember]
        public string piValor7 { get; set; }
        [DataMember]
        public string piValor8 { get; set; }
        [DataMember]
        public string piValor9 { get; set; }
        [DataMember]
        public string piValor10 { get; set; }
        [DataMember]
        public string piFechaRegistro { get; set; }
        [DataMember]
        public string piUsuarioRegistro { get; set; }
    }
}
