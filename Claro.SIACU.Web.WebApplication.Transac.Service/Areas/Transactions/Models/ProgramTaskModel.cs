using System.Collections.Generic;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class ProgramTaskModel
    {
        [Header(Title = "ListExportExcel")]
        public List<ProgramTaskModelExcel> ListExportExcel { get; set; }

    }
    public class ProgramTaskModelExcel : IExcel
    {
        [Header(Title = "Contrato")]
        public string Contract { get; set; }
        [Header(Title = "Customer Id")]
        public string CustomerId { get; set; }
        [Header(Title = "Fecha Programacion")]
        public string ProgramationDate { get; set; }
        [Header(Title = "Fecha Registro")]
        public string RegisterDate { get; set; }
        [Header(Title = "Fecha Ejecucion")]
        public string EjectDate { get; set; }
        [Header(Title = "Estado")]
        public string State { get; set; }
        [Header(Title = "Descripcion Servicio")]
        public string ServiceDescription { get; set; }
        [Header(Title = "Cuenta")]
        public string Account { get; set; }
        [Header(Title = "Tipo Servicio")]
        public string ServiceType { get; set; }
        [Header(Title = "Usuario")]
        public string Users { get; set; }
    }
}