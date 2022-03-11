using System;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices
{
    [Serializable]
    public class ListItemVM
    {
        public string Code { get; set; }
        public string Code2 { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public string MotiveId { get; set; }
        public int CodeTypeService { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public string Condition { get; set; }    
        public ListItemVM()
        {

        }
        public ListItemVM(string vCodigo, string vDescripcion)
        {
            Code = vCodigo;
            Description = vDescripcion;
        }
        public ListItemVM(string vCodigo, string vCodigo2, string vDescripcion)
        {
            Code = vCodigo;
            Code2 = vCodigo2;
            Description = vDescripcion;
        }
    }
}