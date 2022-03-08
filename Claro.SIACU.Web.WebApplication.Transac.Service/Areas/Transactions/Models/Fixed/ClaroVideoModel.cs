using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed
{
    public class ClaroVideoModel
    {

    }
    public class ParametersGenerateContancy
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
        public string StrNombreArchivoPDF { get; set; }           
        public List<ClaroVideoServiceConstancyModel> ListService { get; set; }
        public List<ClaroVideoDeviceConstancyModel> ListDevice { get; set; }
        public List<ClaroVideoSubscriptionConstancyModel> ListSuscriptcion { get; set; }
        public List<ClaroVideoSubscriptionConstancyModel> ListSuscriptcionAdicionales { get; set; }
        
    }
    public class ClaroVideoServiceConstancyModel
    {
        public string strBajaServicios { get; set; }

    }
    public class ClaroVideoDeviceConstancyModel
    {
        public string strDispotisitivoID { get; set; }
        public string strDispotisitivoNom { get; set; }
        public string strFechaDesac { get; set; }

    }
    public class ClaroVideoSubscriptionConstancyModel{

        public string idSubscription { get; set; }
        public string strSuscTitulo { get; set; }
        public string strSuscEstado { get; set; }
        public string strSuscPeriodo { get; set; }
        public string strSuscServicio { get; set; }
        public string strSuscPrecio { get; set; }
        public string strSuscFechReg { get; set; }
    }

    public class ConsultSN
    {
        public HELPER_ITEM.QueryOttResponseHelper queryOttResponse { get; set; }
    }

    public class UpdateClientSN
    {
        public HELPER_ITEM.UpdateUserOttResponseHelper updateUserOttResponse { get; set; }

    }
    public class CancelSubscriptionSN
    {
        public HELPER_ITEM.CancelSubscriptionSNHelper cancelSubscriptionSN { get; set; }
    }
    public class ProvisionSubscription
    {
        public HELPER_ITEM.ProvisionSubscriptionHelper provisionSubscription { get; set; }
    }

    public class HistoryDevice
    {
        public HELPER_ITEM.HistorialServDispCVResponseHelper historialServDispCVResponse { get; set; }
    }

    public class RegisterClientSN
    {
        public HELPER_ITEM.CreateUserOttResponseHelper createUserOttResponse { get; set; }
    }

    public class DeleteClientSN
    {
        public HELPER_ITEM.DeleteUserOttResponseHelper deleteUserOttResponse { get; set; }
    }

    public class ConsultClientSN
    {
        public HELPER_ITEM.QueryUserOttResponseHelper QueryUserOttResponse { get; set; }
        // LISTAS PARA LAS GRID
       
    }
    public class ValidateElegibility
    {
        public HELPER_ITEM.ValidateElegibilityResponseHelper validateElegibilityResponse { get; set; }
    }

    public class RegisterControles
    {
        public HELPER_ITEM.RegistrarcontrolescvResponseHelper registrarcontrolescvresponse { get; set; }
    }

    public class ContractedBusinessServicesModel
    {
        public List<HELPER_ITEM.PhoneContractHelper> PhoneContracts { get; set; }
        public List<HELPER_ITEM.ContractServicesHelper> ContractServices { get; set; }
        public List<HELPER_ITEM.BSCSServiceHelper> BSCSServices { get; set; }
    }

    public class OnBaseTargetModel
    {
        public string IdSession { get; set; }
        public string CodigoAsesor { get; set; }
        public string Path { get; set; }
        public string FormatoTransaccion { get; set; }
        public string Modulo { get; set; }
        public string FullPathPDF { get; set; }
        public string Document { get; set; }

        public ConstanciaClaroVideoModel Constancia { get; set; }

        #region MetaDatos

        public string NombreCliente { get; set; }
        public string Canal { get; set; }
        public string NumeroLinea { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string TipoCliente { get; set; }
        public string TipoOperacion { get; set; }


        #endregion
        #region EstructuraConstancia
        public string strKeyWorkName { get; set; }
        public string strKeyWorkValue { get; set; }
        public string strKeyWorkLeng { get; set; }
        #endregion

    }
    #region ConstanciaClaroVideo
    [XmlRoot(ElementName = "PLANTILLA")]
    public class ConstanciaClaroVideoModel
    {
        [XmlElement(ElementName = "FORMATO_TRANSACCION")]
        public string FORMATO_TRANSACCION { get; set; }
        [XmlElement(ElementName = "CANAL_ATENCION")]
        public string CANAL_ATENCION { get; set; }
        [XmlElement(ElementName = "TITULAR_CLIENTE")]
        public string TITULAR_CLIENTE { get; set; }
        //[XmlElement(ElementName = "NRO_SERVICIO")]
        //public string NRO_SERVICIO { get; set; }
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
        //[XmlElement(ElementName = "TIPO_CLIENTE")]
        //public string TIPO_CLIENTE { get; set; }
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
        //[XmlElement(ElementName = "EMAIL")]
        //public string EMAIL { get; set; }
        [XmlElement(ElementName = "CORREO_SOLICITUD")]
        public string CORREO_SOLICITUD { get; set; }
        [XmlElement(ElementName = "COD_AGENTE")]
        public string COD_AGENTE { get; set; }
        //[XmlElement(ElementName = "NOMBRE_ASESOR")]
        //public string NOMBRE_ASESOR { get; set; }
        //[XmlElement(ElementName = "LINEAS_ASOCIADAS")]
        //public List<string> LINEAS_ASOCIADAS { get; set; }

        [XmlElement(ElementName = "PUNTO_ATENCION")]
        public string PuntoAtencion { get; set; }

        [XmlElement(ElementName = "TITULAR")]
        public string Titular { get; set; }

        [XmlElement(ElementName = "REPRESENTANTE_LEGAL")]
        public string RepresentanteLegal { get; set; }

        [XmlElement(ElementName = "TIPO_DOC")]
        public string TipoDocumento { get; set; }

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
    #endregion

    public class PersonalizaMensajeOTTModel
    {
        public HELPER_ITEM.PersonalizarMensajeResponseHelper PersonalizarMensajeResponse { get; set; }
    }
}