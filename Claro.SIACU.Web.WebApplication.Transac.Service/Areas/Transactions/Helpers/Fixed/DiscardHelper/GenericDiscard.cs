using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper
{
    //INICIATIVA-871
    public class GenericDiscard
    {
        public string EstadoPortabilidad { get; set; }
        public List<DiscardListValueHelper> ListaDescartesValor { get; set; }
    }
}