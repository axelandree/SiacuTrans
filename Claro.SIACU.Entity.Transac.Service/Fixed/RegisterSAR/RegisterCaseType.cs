using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterCaseType
    {
        [DataMember]
        public string piContactObjId1 { get; set; }
        [DataMember]
        public string piSiteObjId1 { get; set; }
        [DataMember]
        public string piAccount { get; set; }
        [DataMember]
        public string piPhone { get; set; }
        [DataMember]
        public string piTipo { get; set; }
        [DataMember]
        public string piClase { get; set; }
        [DataMember]
        public string piSubClase { get; set; }
        [DataMember]
        public string piMetodoContacto { get; set; }
        [DataMember]
        public string piPrioridad { get; set; }
        [DataMember]
        public string piSeveridad { get; set; }
        [DataMember]
        public string piCola { get; set; }
        [DataMember]
        public string piFlagInteract { get; set; }
        [DataMember]
        public string piUsrProceso { get; set; }
        [DataMember]
        public string piUsuario { get; set; }
        [DataMember]
        public string piTipoInter { get; set; }
        [DataMember]
        public string piNotas { get; set; }
        [DataMember]
        public string piServAfect { get; set; }
        [DataMember]
        public string piInconven { get; set; }
        [DataMember]
        public string piServAfectCode { get; set; }
        [DataMember]
        public string piInconvenCode { get; set; }
        [DataMember]
        public string piCoId { get; set; }
        [DataMember]
        public string piCodPlano { get; set; }
        [DataMember]
        public string piValor1 { get; set; }
        [DataMember]
        public string piValor2 { get; set; }
        [DataMember]
        public string piDummyId { get; set; }
        [DataMember]
        public string piCaseFather { get; set; }

    }
}
