using System;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion
{
    [DataContract]
    public class Interaccion : Claro.Entity.Request
    {
        [DataMember]
        public string clase { get; set; }
        [DataMember]
        public string codigoEmpleado { get; set; }
        [DataMember]
        public string codigoSistema { get; set; }
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public string flagCaso { get; set; }
        [DataMember]
        public string hechoEnUno { get; set; }
        [DataMember]
        public string metodoContacto { get; set; }
        [DataMember]
        public string notas { get; set; }
        [DataMember]
        public string objId { get; set; }
        [DataMember]
        public string siteObjId { get; set; }
        [DataMember]
        public string subClase { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string textResultado { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string tipoInteraccion { get; set; }

        /*PROY-32650*/
        [DataMember]
        public string servAfect { get; set; }
        [DataMember]
        public string inconv { get; set; }
        [DataMember]
        public string servAfectCode { get; set; }
        [DataMember]
        public string inconvenCode { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string codPlano { get; set; }
        [DataMember]
        public string valor1 { get; set; }
        [DataMember]
        public string valor2 { get; set; }
    }
}
