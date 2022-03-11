using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractionBusiness
{
    [DataContract]
    public class InsertInteractionBusinessRequest : Claro.Entity.Request
    {
        [DataMember]
        public Claro.SIACU.Entity.Transac.Service.Common.Iteraction Interaction { get; set; }

        [DataMember]
        public Claro.SIACU.Entity.Transac.Service.Common.InsertTemplateInteraction InteractionTemplate { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string UserSystem { get; set; }

        [DataMember]
        public string UserApp { get; set; }

        [DataMember]
        public string UserPass { get; set; }

        [DataMember]
        public bool ExecuteTransactation { get; set; }
    }
}
