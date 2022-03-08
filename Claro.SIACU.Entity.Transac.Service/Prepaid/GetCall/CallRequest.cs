using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetCall
{
    /// <summary>
    /// CAlarcon - 18/05/2017
    /// </summary>
    public class CallRequest : Claro.Entity.Request
    {
        [DataMember]
        public string linea { get; set; }

        [DataMember]
        public string strfechaInicio { get; set; }

        [DataMember]
        public string strfechaFin { get; set; }

        [DataMember]
        public string strTipoConsulta { get; set; }

        [DataMember]
        public string tp { get; set; }
        [DataMember]

        public string strflag { get; set; }
        [DataMember]
        public string strPerfil { get; set; }
        [DataMember]
        public string strEmail { get; set; }
        [DataMember]
        public string IsTFI { get; set; }
        [DataMember]
        public string strPerfilBuscar { get; set; }
        [DataMember]
        public string strPerfilExportar { get; set; }
    }
}
