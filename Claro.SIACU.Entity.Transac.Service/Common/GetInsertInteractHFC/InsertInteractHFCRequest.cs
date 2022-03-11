using Claro.SIACU.Entity.Transac.Service.Fixed;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC
{
    public class InsertInteractHFCRequest : Claro.Entity.Request
    {
        [DataMember]
        public EntitiesFixed.Interaction Interaction { get; set; }

        public InsertInteractHFCRequest() {
            Interaction = new EntitiesFixed.Interaction();
        }
    }
}
