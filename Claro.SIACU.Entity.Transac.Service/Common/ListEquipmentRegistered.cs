using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ListEquipmentRegistered
    {
        [DataMember]
        [Data.DbColumn("REDEV_NUMERO_IMEI")]
        public string numeroImei { get; set; }

        [DataMember]
        [Data.DbColumn("REDEV_NUMEROLINEA")]
        public string numLinea { get; set; }

        [DataMember]
        [Data.DbColumn("REDEV_ESTADO")]
        public string estado { get; set; }

        [DataMember]
        [Data.DbColumn("REDEV_MARCA_MODELO")]
        public string marcaModelo { get; set; }

        [DataMember]
        [Data.DbColumn("REDED_FECHACREA")]
        public DateTime fechaRegistro { get; set; }

    }
}
