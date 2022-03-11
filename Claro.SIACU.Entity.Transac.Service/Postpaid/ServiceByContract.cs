using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ServiceByContract")]
    public class ServiceByContract
    {
        [DataMember]
        public string _cod_grupo { get; set; }
        [DataMember]
        public string _des_grupo { get; set; }
        [DataMember]
        public string _pos_grupo { get; set; }
        [DataMember]
        public string _cod_serv { get; set; }
        [DataMember]
        public string _des_serv { get; set; }
        [DataMember]
        public string _pos_serv { get; set; }
        [DataMember]
        public string _cod_excluyente { get; set; }
        [DataMember]
        public string _des_excluyente { get; set; }
        [DataMember]
        public string _estado { get; set; }
        [DataMember]
        public string _fecha_validez { get; set; }
        [DataMember]
        public string _monto_cargo_sus { get; set; }
        [DataMember]
        public string _monto_cargo_fijo { get; set; }
        [DataMember]
        public string _cuota_modif { get; set; }
        [DataMember]
        public string _monto_final { get; set; }
        [DataMember]
        public string _periodos_validos { get; set; }
        [DataMember]
        public string _bloqueo_desact { get; set; }
        [DataMember]
        public string _bloqueo_act { get; set; }
    }
}
