using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class parametrosType
    {

        #region "Constructor"
        public parametrosType()
        {
            ParametrosType = new ObjetoOpcional();
        }
        #endregion "Constructor"
       

        [DataMember]
        public ObjetoOpcional ParametrosType { get; set; }

    }
}
