using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.BannerDesc
{
    [DataContract]
    public class BannerDescartesRequest
    {
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string grupo { get; set; }
        [DataMember]
        public string accion { get; set; }
    }
}
