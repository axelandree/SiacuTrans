using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarAreaImputable
    {
        #region "Constructor"
        public RegistrarAreaImputable()
        {
            ListadoAreaImputable = new List<AreaImputable>();
        }
        #endregion "Constructor"
        
        [DataMember]
        public List<AreaImputable> ListadoAreaImputable { get; set; }
    }
}
