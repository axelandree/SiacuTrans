using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetActivationDesactivation
{
    [DataContract(Name = "ActivationDesactivationRequest")]
    public class ActivationDesactivationRequest : Claro.Entity.Request
    {
        [DataMember]
        public AddServiceAditional AddServiceAditional { get; set; } 

    }  
}      
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       