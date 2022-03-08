using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class DetEquipmentService
    {
        [DataMember]
        public string strDscequ { get; set; }

        [DataMember]
        public string strTipsrv { get; set; }

        [DataMember]
        public string strCodsrv { get; set; }

        [DataMember]
        public string strTipo_srv { get; set; }

        [DataMember]
        public string intCargo_Fijo { get; set; }

        [DataMember]
        public string intCantidad { get; set; }

        [DataMember]
        public string strTipo { get; set; }

    }
}