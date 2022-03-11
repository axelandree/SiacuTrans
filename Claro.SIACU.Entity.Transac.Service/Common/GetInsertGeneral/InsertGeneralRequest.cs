using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertGeneral
{
    [DataContract]
    public class InsertGeneralRequest:Claro.Entity.Request
    {
        [DataMember]
        public Iteraction Interaction { get; set; }
        [DataMember]
        public InsertTemplateInteraction InteractionTemplate { get; set; }
        [DataMember]
        public string vNroTelefono { get; set; }
        [DataMember]
        public string vUSUARIO_SISTEMA { get; set; }
        [DataMember]
        public string vUSUARIO_APLICACION { get; set; }
        [DataMember]
        public string vPASSWORD_USUARIO { get; set; }
        [DataMember]
        public bool vEjecutarTransaccion { get; set; }
    }
}
