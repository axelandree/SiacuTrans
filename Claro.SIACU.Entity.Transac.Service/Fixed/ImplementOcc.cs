using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class ImplementOcc
    {
        [DataMember]
        public string CustomerId;
        [DataMember]
        public string Fecvig;
        [DataMember]
        public string Monto;
        [DataMember]
        public string Observacion;
        [DataMember]
        public string FlagCobroOcc;
        [DataMember]
        public string Aplicacion;
        [DataMember]
        public string UsuarioAct;
        [DataMember]
        public string FechaAct;
    }
}
