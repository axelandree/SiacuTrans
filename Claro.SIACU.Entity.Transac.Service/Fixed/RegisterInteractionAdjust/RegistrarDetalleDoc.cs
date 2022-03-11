using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarDetalleDoc
    {
        #region "Constructor"
        public RegistrarDetalleDoc()
        {
            ListaRegistroDetDocum = new List<RegistroDetDocum>();
            ListaDetDocAdicional = new List<DetDocAdicional>();
        }
        #endregion "Constructor"
        

        [DataMember]
        public string piNumDocAjuste { get; set; }
        [DataMember]
        public List<RegistroDetDocum> ListaRegistroDetDocum { get; set; }
        [DataMember]
        public List<DetDocAdicional> ListaDetDocAdicional { get; set; }
    }
}
