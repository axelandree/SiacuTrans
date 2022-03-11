using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ListItem
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Code2 { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string CodeTypeService { get; set; }
        [DataMember]
        public string IdMotive { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Group { get; set; }
        [DataMember]
        public string Condition { get; set; }
        [DataMember]
        public string ParameterId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ValueC { get; set; }
        public ListItem()
        {
        }
        public ListItem(string vCodigo, string vDescripcion)
        {
            Code = vCodigo;
            Description = vDescripcion;
        }
        public ListItem(string vCodigo, string vCodigo2, string vDescripcion)
        {
            Code = vCodigo;
            Code2 = vCodigo2;
            Description = vDescripcion;
        }
    }

}
