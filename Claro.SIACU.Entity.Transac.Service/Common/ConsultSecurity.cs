using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ConsultSecurity
    {
        [DataMember]
        public string Usuaccod { get; set; }
        [DataMember]
        public string Perfccod { get; set; }
        [DataMember]
        public string Usuaccodvensap { get; set; }
        [DataMember]
        public string Apliccod { get; set; }
        [DataMember]
        public string Opccodpad { get; set; }
        [DataMember]
        public string Opciccod { get; set; }
        [DataMember]
        public string Opcicnivpad { get; set; }
        [DataMember]
        public string Opcicniv { get; set; }
        [DataMember]
        public string Opcicdes { get; set; }
        [DataMember]
        public string Opcicabrev { get; set; }
        [DataMember]
        public string Opcicnompag { get; set; }
        [DataMember]
        public string Opcicnumord { get; set; }
        [DataMember]
        public string Opcicd1 { get; set; }
    }
}
