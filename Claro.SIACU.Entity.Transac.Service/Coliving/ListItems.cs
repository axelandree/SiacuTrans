using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving
{
    [DataContract(Name = "ListItems")]
   public class ListItems
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Code2 { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int number { get; set; }  
    }
}
