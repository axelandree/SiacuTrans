using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE
{
    public class PackagePurchaseServicesModel
    {
        public string strIdSession { get; set; }

        //************  Datoscampañas  *****************//
        public string fechaActual { get; set; }
        public string CodeTipification { get; set; }
        public int idCampana { get; set; }
        public string CurrentUser { get; set; }
        public string hidSupJef { get; set; }
        public int intNumeroIntentos { get; set; }
        //******************* DATOS CLIENTE ***********//
        public string strContacto { get; set; }
        public string strCustomerID { get; set; }
        public string strNombreCliente { get; set; }
        public string strTipoCliente { get; set; }
        public string strContrato { get; set; }
        public string strNumeroDoc { get; set; }
        public string strRepresentanteLegal { get; set; }
        public string strPlan { get; set; }
        public string strFechaActivacion { get; set; }
        public string strCicloFacturacion { get; set; }
        public string strLimiteCrediticio { get; set; }

        //********** Direccíón de Facturación ***********//
        public string strDireccion { get; set; }
        public string strNotasDireccion { get; set; }
        public string strPais { get; set; }
        public string strDepartamento { get; set; }
        public string strProvincia { get; set; }
        public string strDistrito { get; set; }
        public string strCodUbigeo { get; set; }

        //************* Datos para Constancia ************ //
        public string strTipoDocIdentidad { get; set; }
        public string strCasoInteraccion { get; set; }
        public string strNumeroCuenta { get; set; }
        public string strNumeroServicio { get; set; }
        public string strCodigoAsesor { get; set; }
        public string strNombreAsesor { get; set; }
        public string strFullPathPDF { get; set; }
        public string strNamePDF { get; set; }
        //JFG
        public string strPath { get; set; }
        public string strTransactionFormat { get; set; }
        public string strDocument { get; set; }
        public string strModule { get; set; }
        public ConstanciaPackagePurchaseServicesModel Constancia { get; set; }
        public string strOperationType { get; set; }
        public string strRegisterDate { get; set; }
        public string strConsultantCode { get; set; }

        #region EstructuraConstancia
        public string strKeyWorkName { get; set; }
        public string strKeyWorkValue { get; set; }
        public string strKeyWorkLeng { get; set; }
        #endregion

        //**************** Datos que cambian *************//
        public string PlanVeloacidadDegrada { get; set; }
        public string MotivoDegradacion { get; set; }
        public string PqtVelocidadDegradacion { get; set; }
        public string strTipoVenta { get; set; }
        public string strSaldoPuntos { get; set;}
        public string strVigencia { get; set; }
        public string strPrecioPaquete { get; set; }
        public string strMBIncluidos { get; set; }
        public string strEmailCliente { get; set; }
        public string strPuntoAtencion { get; set; }
        public string strNotas { get; set; }
        public string FlagEmail { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool ChkEmail { get; set; }

        /*Tipificacion*/

        public string CLASE { get; set; }
        public string CLASE_CODE { get; set; }
        public string SUBCLASE { get; set; }
        public string SUBCLASE_CODE { get; set; }
        public string TIPO { get; set; }
        public string TIPO_CODE { get; set; }
        public string strObjidContacto { get; set; }
        public string strTelefono { get; set; }  
        /***/
        public string strCodOnBase { get; set; }
        public string strPlanBase { get; set; }

        public byte[] byteArchivoSamba { get; set; }
        public string Document { get; set; }
         

        public string strCargoFijo { get; set; }
        public string strMensajeEmail { get; set; }
        public string strErrorMessage { get; set; }
        public byte[] btArchivoSamba { get; set; }


    }

    [XmlRoot(ElementName = "PLANTILLA")]
    public class ConstanciaPackagePurchaseServicesModel
    {
        [XmlElement(ElementName = "FORMATO_TRANSACCION")]
        public string FORMATO_TRANSACCION { get; set; }

        [XmlElement(ElementName = "CANAL_ATENCION")]
        public string CANAL_ATENCION { get; set; }

        [XmlElement(ElementName = "TITULAR_CLIENTE")]
        public string TITULAR_CLIENTE { get; set; }

        [XmlElement(ElementName = "TIPO_DOC_IDENTIDAD")]
        public string TIPO_DOC_IDENTIDAD { get; set; }

        [XmlElement(ElementName = "FECHA_SOLICITUD")]
        public string FECHA_SOLICITUD { get; set; }

        [XmlElement(ElementName = "CASO_INTER")]
        public string CASO_INTER { get; set; }

        [XmlElement(ElementName = "CONTACTO_CLIENTE")]
        public string CONTACTO_CLIENTE { get; set; }

        [XmlElement(ElementName = "NRO_DOC_IDENTIDAD")]
        public string NRO_DOC_IDENTIDAD { get; set; }

        [XmlElement(ElementName = "TRANSACCION")]
        public string TRANSACCION { get; set; }

        [XmlElement(ElementName = "NRO_CLARO")]
        public string NRO_CLARO { get; set; }

        [XmlElement(ElementName = "HABILITAR_SERVICIO")]
        public string HABILITAR_SERVICIO { get; set; }

        [XmlElement(ElementName = "RESTRINGIR_SERVICIO")]
        public string RESTRINGIR_SERVICIO { get; set; }

        [XmlElement(ElementName = "MEDIO_PERMITIDO")]
        public string MEDIO_PERMITIDO { get; set; }

        [XmlElement(ElementName = "MEDIO_NO_PERMITIDO")]
        public string MEDIO_NO_PERMITIDO { get; set; }

        [XmlElement(ElementName = "ENVIO_CORREO")]
        public string ENVIO_CORREO { get; set; }

        [XmlElement(ElementName = "CORREO_SOLICITUD")]
        public string CORREO_SOLICITUD { get; set; }

        [XmlElement(ElementName = "COD_AGENTE")]
        public string COD_AGENTE { get; set; }

        [XmlElement(ElementName = "PUNTO_ATENCION")]
        public string PuntoAtencion { get; set; }

        [XmlElement(ElementName = "TITULAR")]
        public string Titular { get; set; }

        [XmlElement(ElementName = "REPRESENTANTE_LEGAL")]
        public string RepresentanteLegal { get; set; }

        //[XmlElement(ElementName = "TIPO_DOC")]
        //public string TipoDocumento { get; set; }

        [XmlElement(ElementName = "TIPO_CLIENTE")]
        public string TipoCliente { get; set; }

        [XmlElement(ElementName = "PLAN_ACTUAL")]
        public string PlanActual { get; set; }

        [XmlElement(ElementName = "CICLO_FACTURACION")]
        public string CicloFacturacion { get; set; }

        [XmlElement(ElementName = "EMAIL")]
        public string Email { get; set; }

        [XmlElement(ElementName = "FECHA_ACTUALIZACION")]
        public string FechaActualizacion { get; set; }

        [XmlElement(ElementName = "NRO_CASO_INTERACCION")]
        public string NRO_CASO_INTERACCION { get; set; }

        [XmlElement(ElementName = "NRO_CASO")]
        public string NumeroCaso { get; set; }

        [XmlElement(ElementName = "NRO_SERVICIO")]
        public string NumeroServicio { get; set; }

        [XmlElement(ElementName = "NRO_DOC")]
        public string NumeroDocumento { get; set; }

        [XmlElement(ElementName = "TIPO_BENEFICIO")]
        public string TipoBeneficio { get; set; }

        [XmlElement(ElementName = "LINEAS_ASOCIADAS")]
        public List<string> ListaLineas { get; set; }

        [XmlElement(ElementName = "CODIGO_ASESOR")]
        public string CodigoAsesor { get; set; }

        [XmlElement(ElementName = "NOMBRE_ASESOR")]
        public string NombreAsesor { get; set; }

        [XmlElement(ElementName = "VALIDADOR_FIRMA_DIGITAL")]
        public string FlagFimaDigital { get; set; }

        [XmlElement(ElementName = "TIPO_OPERACION")]
        public string FormatoTransaccion { get; set; }

        [XmlElement(ElementName = "NRO_LINEA")]
        public string NumeroLinea { get; set; }

        [XmlElement(ElementName = "COD_CLIENTE")]
        public string CodigoCliente { get; set; }

    }
}