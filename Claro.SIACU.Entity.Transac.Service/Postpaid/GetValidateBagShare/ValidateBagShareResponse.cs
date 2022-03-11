using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateBagShare
{
    [DataContract(Name = "ValidateBagShareResponseTransactions")]
    public class ValidateBagShareResponse
    {
        [DataMember]
        public List<ListItem> lstListItem { get; set; }
        [DataMember]
        public string RPT { get; set; }
        
        [DataMember]
        public string NroCuenta { get; set; }
         
        [DataMember]
        public string CustCode { get; set; }
         
        [DataMember]
        public int Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; } 
    }
}
