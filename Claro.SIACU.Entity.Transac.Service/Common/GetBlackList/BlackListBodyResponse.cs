using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBlackList
{
    [DataContract]
    public class BlackListBodyResponse
    {
        [DataMember(Name = "responseStatus")]
        public Common.GetDataPower.ResponseStatus responseStatus { get; set; }
        [DataMember(Name = "detalleValidaEstadoIMEI")]
        public BlackListDetalleValidaEstadoIMEIResponse responseDetalleValidaEstadoIMEI { get; set; }
    }
}
