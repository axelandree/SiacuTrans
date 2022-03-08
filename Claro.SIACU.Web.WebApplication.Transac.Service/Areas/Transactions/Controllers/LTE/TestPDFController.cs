//using Claro.Entity;
//using Claro.SIACU.Web.WebApplication.Transac.Service.DataThroughWebServicesService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class TestPDFController : Controller
    {
        //
        // GET: /Transactions/TestPDF/
        public ActionResult Index()
        {
         
            return View();

        }
        public ActionResult GenerarPdf()
        {
            string strIdSession = "1";

            CommonTransacServiceClient oGenerarPDF = new CommonTransacServiceClient();

            GeneratePDFDataRequestHfc oParametrosGenerarPDF = new GeneratePDFDataRequestHfc()
            {
                StrNroServicio = "123456789",
                StrCasoInter = "strCaso",
                StrTitularCliente = "RAZON_SOCIAL",
                StrContactoCliente = "NOMBRE_COMPLETO",
                StrNroDocIdentidad = "DNI_RUC",
                StrDireccionPostal = "CALLE_FAC ",
                StrDistritoPostal = "DISTRITO_FAC ",
                StrProvinciaPostal = "PROVINCIA_FAC",
                StrDepartamentoPostal = "DEPARTEMENTO_FAC",
                StrServidorGenerarPDF = ConfigurationManager.AppSettings("strServidorGenerarPDF").ToString(),
                StrFechaTransaccionProgram = DateTime.Now.ToShortDateString(),
                StrServidorLeerPDF = ConfigurationManager.AppSettings("strServidorLeerPDF").ToString(),
                StrCarpetaPDFs = ConfigurationManager.AppSettings("strCarpetaPDFs").ToString(),
                StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaRegistroAjustes").ToString(),
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };
            string strTerminacionPDF = ConfigurationManager.AppSettings("strTerminacionPDF").ToString();
            GeneratePDFDataResponseHfc oGeneratePDFDataResponseHfc = new GeneratePDFDataResponseHfc();
            
            try
            {
                oGeneratePDFDataResponseHfc = Claro.Web.Logging.ExecuteMethod<GeneratePDFDataResponseHfc>(() =>
                {
                    return new CommonTransacServiceClient().GeneratePDF(oParametrosGenerarPDF);
                });
                string strInteraccionId = "123345";
                string strFechaTransaccion = DateTime.Now.ToShortDateString();
                if (oGeneratePDFDataResponseHfc.Generated && oGeneratePDFDataResponseHfc.FilePath != null)
                {
                    string nombrePDF = String.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", oParametrosGenerarPDF.StrServidorLeerPDF,
                                         oParametrosGenerarPDF.StrCarpetaPDFs, oParametrosGenerarPDF.StrCarpetaTransaccion,
                                         strInteraccionId, strFechaTransaccion, oParametrosGenerarPDF.StrNombreArchivoTransaccion,
                                         strTerminacionPDF);

                    string nombrepath = String.Format("{0}{1}{2}", oParametrosGenerarPDF.StrServidorLeerPDF,
                                                            oParametrosGenerarPDF.StrCarpetaPDFs, oParametrosGenerarPDF.StrCarpetaTransaccion);

                    string documentName = String.Format("{0}_{1}_{2}_{3}",
                                                             strInteraccionId, strFechaTransaccion, oParametrosGenerarPDF.StrNombreArchivoTransaccion,
                                                             strTerminacionPDF);

                }
                else
                {
                    //usar la plantilla html y el windows print
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View("~/Areas/Transactions/Views/TestPDF/Index.cshtml", oGeneratePDFDataResponseHfc);

        }
    }
}