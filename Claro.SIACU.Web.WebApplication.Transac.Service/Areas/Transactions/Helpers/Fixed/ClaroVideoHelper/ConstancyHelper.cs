using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//INI ADD Luis D AMCO
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class ConstancyClaroVideoHelper
    {        
        public string strTipoConstanciaAMCO { get; set; }        
        public string StrNombreArchivoTransaccion { get; set; }         
        public string strPuntoAtencion { get; set; }        
        public string strTitular { get; set; }        
        public string strRepresentante { get; set; }        
        public string strTipoDoc { get; set; }        
        public string strFechaAct { get; set; }        
        public string strNroCaso { get; set; }        
        public string strNroServicio { get; set; }         
        public string strNroDoc { get; set; }        
        public string strEmail { get; set; }      

        public List<ClaroVideoServiceConstancyHelper> ListService { get; set; }        
        public List<ClaroVideoDeviceConstancyHelper> ListDevice { get; set; }        
        public List<ClaroVideoSubscriptionConstancyHelper> ListSuscriptcion { get; set; }
    }
    public class ClaroVideoServiceConstancyHelper
    {
        public string strBajaServicios { get; set; }

    }
    public class ClaroVideoDeviceConstancyHelper
    {
        public string strDispotisitivoID { get; set; }
        public string strDispotisitivoNom { get; set; }
        public string strFechaDesac { get; set; }

    }
    public class ClaroVideoSubscriptionConstancyHelper
    {
        public string strSuscTitulo { get; set; }
        public string strSuscEstado { get; set; }
        public string strSuscPeriodo { get; set; }
        public string strSuscServicio { get; set; }
        public string strSuscPrecio { get; set; }
        public string strSuscFechReg { get; set; }
    }
}