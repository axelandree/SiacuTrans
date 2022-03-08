using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.BilledOutCallDetail
{
    public class BilledOutCallDetailsPostList
    {
        public List<PostTransacService.CallDetailGeneric> lista1 { get; set; }
        public List<PostTransacService.ListCallDetailTransactions> lista2 { get; set; }
        public List<PostTransacService.ListCallDetail_PDITransactions> lista3 { get; set; }

        public BilledOutCallDetailsPostList()
        {
            lista1 = new List<PostTransacService.CallDetailGeneric>();
            lista2 = new List<PostTransacService.ListCallDetailTransactions>();
            lista3 = new List<PostTransacService.ListCallDetail_PDITransactions>();
        }
    }
}