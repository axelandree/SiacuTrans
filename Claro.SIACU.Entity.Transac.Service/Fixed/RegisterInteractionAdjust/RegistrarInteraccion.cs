using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarInteraccion
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
        public string piTipoInter { get; set; }
        [DataMember]
        public string piAgente { get; set; }
        [DataMember]
        public string piUsrProceso { get; set; }
        [DataMember]
        public string piHechoEnUno { get; set; }
        [DataMember]
        public string piNotas { get; set; }
        [DataMember]
        public string piFlagCaso { get; set; }
        [DataMember]
        public string piResultado { get; set; }
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

    }
}
