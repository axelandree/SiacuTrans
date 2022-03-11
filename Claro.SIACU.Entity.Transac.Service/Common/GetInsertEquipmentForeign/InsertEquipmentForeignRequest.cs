using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertEquipmentForeign
{
    public class InsertEquipmentForeignRequest : Claro.Entity.Request
    {
        public EquipmentForeignInsert item  { get; set; }
        public string nameTransaction { get; set; }
        public string nameCac { get; set; }
        public string typeCac { get; set; }
        public string userAccesslogin { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string documentType { get; set; }
        public string documentNumber { get; set; }
        public string referencePhone { get; set; }
        public string parient { get; set; }
        public string imei { get; set; }
        public string imeiFisico { get; set; }
        public string markModel { get; set; }
        public string area { get; set; }
        public string notes { get; set; }
        public string relationshipOwner { get; set; }
        public string codeadviser { get; set; }
        public string adviser { get; set; }
        public string numberclaro { get; set; }
        public string replegal { get; set; }
        public string idSession { get; set; }
        public Int64 customerId { get; set; }
        public string customerTelephone { get; set; }
        public string documentTypeText { get; set; }
        public string customerFullName { get; set; }
        public string customerName { get; set; }
        public string customerLastName { get; set; }
        public string customerNumberDocument { get; set; }
        public string flagFirmaDigital { get; set; }
        public string tipoPersona { get; set; }
        public string customerLegalAgent { get; set; }
        public string customerNumberDocumentRRLL { get; set; }
        public string listLegalAgent { get; set; }
        public string strHuellaMinucia { get; set; }
        public string strHuellaEncode { get; set; }        
        public string strStatusLinea { get; set; }
        
    }
}
