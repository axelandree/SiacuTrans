using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetFacturePDI
{
    [DataContract]
    public class FacturePDIResponse
    {
        [DataMember]
        public List<GenericItem> LstGenericItem { get; set; }
    }
}
