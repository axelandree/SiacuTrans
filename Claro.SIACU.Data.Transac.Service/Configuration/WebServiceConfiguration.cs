using Claro.Data;
using Claro.SIACU.ProxyService.Transac.Service.ActivaDesactivaServicioComercialWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.SimCard;
using Claro.SIACU.ProxyService.Transac.Service.AuditoriaWS;
using Claro.SIACU.ProxyService.Transac.Service.ConsultaOpcionesAuditoriaWS;
using Claro.SIACU.ProxyService.Transac.Service.RegistroAuditoriaWS;
using Claro.SIACU.ProxyService.Transac.Service.ServiciosPostpagoWS;
using SIACPreServiceCont = Claro.SIACU.ProxyService.Transac.Service.SIACPre.ContingencyService;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.ValidationQueryBscsEai;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.GestionAcuerdoWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.MigracionControlPostpagoWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.MigracionPlanPostpagoWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.BondTFI;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.Service;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.ConsultPrePostData;
using Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CrewManagement;
using Claro.SIACU.ProxyService.Transac.Service.WSClienteHFC;
using Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS;
//using ADMCUAD = Claro.SIACU.ProxyService.Transac.Service.WSAdministracionCuadrillas;
using Claro.SIACU.ProxyService.Transac.Service.TransaccionOCC;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.EnvioCorreoSB;
using Claro.SIACU.ProxyService.Transac.Service.WSBRMS;
using Claro.SIACU.ProxyService.Transac.Service.WSValidaIdentidad;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.WSDigitalSignature;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.InboundToaWS;
using Claro.SIACU.ProxyService.Transac.Service.CambioDatosSiacWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.StateAccount;//Proy-32650

namespace Claro.SIACU.Data.Transac.Service.Configuration
{
    internal struct WebServiceConfiguration
    {
        public static readonly ebsSimcards PREPAID_SIMCARD = WebService.Create<ebsSimcards>("strWSZsansTransaladoTelf");
        public static readonly ConsultaDatosPrePostWSService DataPrePostWS = WebService.Create<ConsultaDatosPrePostWSService>("strWSDataPrePostDataWSService");
        public static readonly EbsDatosPrepagoService PrepaidService = WebService.Create<EbsDatosPrepagoService>("SIACPrepagoConsultasWS");
        public static readonly SIACPreServiceCont.EbsDatosPrepagoService PrepaidServiceContingency = WebService.Create<SIACPreServiceCont.EbsDatosPrepagoService>("SIACPrepagoConsultasWSContingencia");
        public static readonly EbsConsultaValidacionBscsTimEaiService ValidateQueryBscsEAi = WebService.Create<EbsConsultaValidacionBscsTimEaiService>("SIACConsultaValidacionBscsTimEaiWS");
        public static readonly GestionAcuerdoWSService GestionAcuerdoWS = WebService.Create<GestionAcuerdoWSService>("SIACGestionAcuerdoWSService");
        public static readonly ebsOperacionesINService PrepaidOperations = WebService.Create<ebsOperacionesINService>("SIACPrepagoOperacionesIN");
        public static readonly ebsMigracionControlPostpagoService MigracionControlPostpagoService = WebService.Create<ebsMigracionControlPostpagoService>("SIACMigracionControlPostpagoWS");
        public static readonly ebsMigracionPlanPostpago_ep MigracionPlanPostpago = WebService.Create<ebsMigracionPlanPostpago_ep>("SIACMigracionPlanPostpagoWS");
        public static readonly EbsAuditoriaService GRABARAUDIT = WebService.Create<EbsAuditoriaService>("strWebServiceSeguridad");
        public static readonly RegistroAuditoriaService REGISTRARAUDIT = WebService.Create<RegistroAuditoriaService>("RegistroAuditoriaWS");
        public static readonly ConsultaOpcionesAuditoriaService OpcionesAuditoria = WebService.Create<ConsultaOpcionesAuditoriaService>("ConsultaOpcionesAuditoriaWS");
        public static readonly clienteHFCService ClienteHFC = WebService.Create<clienteHFCService>("SIACPostClienteHFCWS");
        public static readonly TransaccionOCC TransaccionOCC = WebService.Create<TransaccionOCC>("TransaccionOCC");
        public static readonly ServiciosPostPagoWSService ActDesactServiciosComerciales = WebService.Create<ServiciosPostPagoWSService>("ActDesactServiciosComerciales");
        public static readonly ebsADMCUAD_CapacityService ADMCUAD_CapacityService = WebService.Create<ebsADMCUAD_CapacityService>("SIACAdministracionCuadrillasHFCWS");

        public static readonly REGLASAUTOMATIZACIONDEDOCUMENTOSDecisionService objWSBRMSService = WebService.Create<REGLASAUTOMATIZACIONDEDOCUMENTOSDecisionService>("strServidorBRMS");

        public static readonly EbsActivaDesactivaServicioComercial objEbsActivaDesactivaServicio = WebService.Create<EbsActivaDesactivaServicioComercial>("gConstWSValidActDesServ");

        public static readonly EnvioCorreoSBPortTypeSOAP11BindingQSService SIACUEnvioCorreoSB = WebService.Create<EnvioCorreoSBPortTypeSOAP11BindingQSService>("WSURLEnvioCorreoSB");

        public static readonly SIACPostpagoTxWSService SIACPostpagoTxWS = WebService.Create<SIACPostpagoTxWSService>("strWebServiceTriacion");
        public static readonly PRS_FirmaDigital_WS SIACUDigitalSignature = WebService.Create<PRS_FirmaDigital_WS>("strDigitalSignatureUrl");

        public static readonly PRS_ValidaIdentidad_WS objWSValidaIdentidad = WebService.Create<PRS_ValidaIdentidad_WS>("strWSValidaIdentidad");
        public static readonly ebsADMCUAD_InboundSchemaService ADMCUAD_InboundService = WebService.Create<ebsADMCUAD_InboundSchemaService>("SIACInboundTOAHFCWS");
        public static readonly ConsultaEstadoCuenta FIXED_STATE = WebService.Create<ConsultaEstadoCuenta>("SIACConsultaEstadoCuentaWS");//Proy-32650
        

        //Cambio de Datos
        public static readonly CambioDatosSIACWSService CambioDatosWS = WebService.Create<CambioDatosSIACWSService>("strWebServiceCambioDatos");

        public static readonly Claro.SIACU.ProxyService.Transac.Service.CargaDocumentOnBaseWS.cargaDocumentOnBase CargaDocumentoOnBase = WebService.Create<Claro.SIACU.ProxyService.Transac.Service.CargaDocumentOnBaseWS.cargaDocumentOnBase>("CargaDocumentoOnBaseWS");

    }
}
