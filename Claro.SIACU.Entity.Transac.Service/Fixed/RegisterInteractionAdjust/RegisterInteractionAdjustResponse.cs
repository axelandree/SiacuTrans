using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegisterInteractionAdjustResponse : Claro.Entity.Response
    {
        #region "Constructor"
        public RegisterInteractionAdjustResponse()
        {
            auditResponse = new auditResponseType();
            listaResponseOpcional = new List<parametrosType>();
        }
        #endregion "Constructor"
        
        [DataMember]
        public auditResponseType auditResponse { get; set; }
        [DataMember]
        public string idInteract { get; set; }
        [DataMember]
        public string idDocAut { get; set; }
        [DataMember]
        public string farenNroSar { get; set; }
        [DataMember]
        public List<parametrosType> listaResponseOpcional { get; set; }

    }
}
