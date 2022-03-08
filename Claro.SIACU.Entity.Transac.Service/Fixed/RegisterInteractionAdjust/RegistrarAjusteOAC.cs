using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarAjusteOAC
    {
        #region "Constructor"
        public RegistrarAjusteOAC() { 
            this.docsRef = new List<DetalleDocs>(); 
        }
        #endregion "Constructor"
        
        [DataMember]
        public string piCodAplicacionOAC {get;set;}
        [DataMember]
        public string piTipoServicio{get;set;}
        [DataMember]
        public string piCodCuenta{get;set;}
        [DataMember]
        public string piTipoOperacion{get;set;}
        [DataMember]
        public string piTipoAjuste{get;set;}
        [DataMember]
        public string piEstado{get;set;}
        [DataMember]
        public List<DetalleDocs> docsRef { get; set; }
        [DataMember]
        public string piIdReclamoOrigen{get;set;}
        [DataMember]
        public string piMonedaOrigen{get;set;}
        [DataMember]
        public string piNSaldoAjuste{get;set;}
        [DataMember]
        public string piCodMotivoAjuste{get;set;}
        [DataMember]
        public string piFechaAjuste{get;set;}
        [DataMember]
        public string piFechaCancelacion{get;set;}
    }
}
