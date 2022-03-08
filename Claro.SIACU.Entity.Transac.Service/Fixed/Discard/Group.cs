using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    [DataContract(Name = "Group")]
    public class Group
    {
        [DataMember(Name = "degriIdGrupo")]
        public string degriIdGrupo { get; set; }

        [DataMember(Name = "degrvDescripcion")]
        public string degrvDescripcion { get; set; }

        [DataMember(Name = "degrvTipo")]
        public string degrvTipo { get; set; }

        [DataMember(Name = "degriFlagVisual")]
        public string degriFlagVisual { get; set; }

        [DataMember(Name = "degriOrden")]
        public string degriOrden { get; set; }

        [DataMember(Name = "degrvUsuReg")]
        public string degrvUsuReg { get; set; }

        [DataMember(Name = "degrdFecReg")]
        public string degrdFecReg { get; set; }

        [DataMember(Name = "degrvUsuMod")]
        public string degrvUsuMod { get; set; }

        [DataMember(Name = "degrdFecMod")]
        public string degrdFecMod { get; set; }

        [DataMember(Name = "degriEstadoReg")]
        public string degriEstadoReg { get; set; }


    }
}
