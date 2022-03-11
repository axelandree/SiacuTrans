using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class EquipmentForeign
    {
        [DataMember]
        [Data.DbColumn("REDEV_NUMEROLINEA")]
        public string redevNumeroLinea { get; set; }

        [DataMember]
        [Data.DbColumn("REDED_FECHACREA")]
        public string rededFechaCrea { get; set; }

        [DataMember]
        [Data.DbColumn("REDEV_NUMERO_IMEI")]
        public string redevNumeroImei { get; set; }

        [DataMember]
        [Data.DbColumn("REDEV_MARCA_MODELO")]
        public string redevMarcaModelo { get; set; }

        [DataMember]
        [Data.DbColumn("ESTADO_EQUIPO")]
        public string estadoEquipo { get; set; }

        [DataMember]
        [Data.DbColumn("TIPO_MOTIVO_BLOQUEO")]
        public string tipoMotivoBloqueo { get; set; }

        [DataMember]
        [Data.DbColumn("FECHA_REGISTRO_BLOQUEO")]
        public string fechaRegistroBloqueo { get; set; }
       
    }
}
