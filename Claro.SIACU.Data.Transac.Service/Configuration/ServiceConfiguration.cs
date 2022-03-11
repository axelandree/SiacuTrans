using Claro.SIACU.ProxyService.Transac.Service.SIACPost.Actions;
using Claro.SIACU.ProxyService.Transac.Service.SIACPost.Customer;
using Claro.SIACU.ProxyService.Transac.Service.SIACUConsultaSeguridad;
using Claro.SIACU.ProxyService.Transac.Service.RedirectWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerHFC;
using Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerLTE;
using AdditionalServicesHFC = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddHFC;
using Claro.SIACU.ProxyService.Transac.Service.SIACUBPEL.ChangeNumber;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultIGV;
using Claro.SIACU.ProxyService.Transac.Service.WSDwh;
using ActivateDesactivateLTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddLTE;
using ServiceRefenceBPEL = Claro.SIACU.ProxyService.Transac.Service.SIACFixedBpelCallDetail;
using Claro.SIACU.ProxyService.Transac.Service.CambioPlanFija;
using ConfigServices = Claro.SIACU.ProxyService.Transac.Service.ConfigurationServices;
using UninstallInstallDecos = Claro.SIACU.ProxyService.Transac.Service.Fixed.UninstallInstallDecosLTE;
using Claro.SIACU.ProxyService.Transac.Service.EbsSWSAP;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.WSRegisterTraceability;
using Claro.SIACU.ProxyService.Transac.Service.CambioEquipoWS;
using Claro.SIACU.ProxyService.Transac.Service.CambioPlanLTEWSService;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.PuntosAtencion;
using Claro.SIACU.ProxyService.Transac.Service.SIAC.PlanesTFI;
using Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI;
using Claro.Transversal.ProxyService.ConsultaClaves;
using Claro.SIACU.ProxyService.Transac.Service.SIACUEnviarNotificacion;
using Claro.SIACU.ProxyService.Transac.Service.AuditoriaWS;
using Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions;
using Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE;
using ValidarLineasCliente = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente;
using Claro.SIACU.ProxyService.Transac.Service.SIACFixed.PCRFConnectorLTE;
using Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad; 
using Claro.SIACU.ProxyService.SIACPre.ConsultarDatosSIM; //INICIATIVA-871

namespace Claro.SIACU.Data.Transac.Service.Configuration
{
    internal struct ServiceConfiguration
    {
        public static readonly ValidarCredencialesSUWSPortTypeClient AUDIT_CREDENTIALS = Claro.Data.Service.Create<ValidarCredencialesSUWSPortTypeClient>("RedirectWS");
        public static readonly EbsAccionesPostPagoWSClient CHANGE_NUMBER = Claro.Data.Service.Create<EbsAccionesPostPagoWSClient>("SIACPostActions");
        public static readonly SIACPostpagoConsultasWSClient POSTPAID_CONSULTCLIENT = Claro.Data.Service.Create<SIACPostpagoConsultasWSClient>("SIACPostpagoConsultas");
        public static readonly ConsultaSeguridadClient SIACU_ConsultaSeguridad = Claro.Data.Service.Create<ConsultaSeguridadClient>("SIACU_ConsultaSeguridad");
        //public static readonly Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoWS.SIACPostpagoConsultasWSClient POSTPAID_CONSULTDATALINE = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoWS.SIACPostpagoConsultasWSClient>("SIACPostpagoConsultasDataLine");
        public static readonly EbsAuditoriaClient SECURITY_PERMISSIONS = Claro.Data.Service.Create<EbsAuditoriaClient>("strWebServiceSecurityPermissions");
        public static readonly clienteHFCPortTypeClient FIXED_CUSTOMER_HFC = Claro.Data.Service.Create<clienteHFCPortTypeClient>("WSURLClienteHFC");
        public static readonly clienteLTEPortTypeClient FIXED_CUSTOMER_LTE = Claro.Data.Service.Create<clienteLTEPortTypeClient>("WSURLClienteLTE");
        public static readonly cambioNumeroFijaPortClient FIXED_CHANGE_NUMBER = Claro.Data.Service.Create<cambioNumeroFijaPortClient>("WSURLBPELChangeNumber");

        public static readonly ConsultaIGVWSPortTypeClient SIACUConsultaIGV = Claro.Data.Service.Create<ConsultaIGVWSPortTypeClient>("WSURLConsultIGV");
        public static readonly AdditionalServicesHFC.ebsServiciosHFCSB11Client FIXED_ADDSERV_HFC = Claro.Data.Service.Create<AdditionalServicesHFC.ebsServiciosHFCSB11Client>("FixedAddServHFC");

        public static readonly ServiceRefenceBPEL.detalleLlamadasPortClient FIXED_BPEL_CALLDETAIL = Claro.Data.Service.Create<ServiceRefenceBPEL.detalleLlamadasPortClient>("FixedBPELCallDetail");

        //Activacion y Desactivacion de servivios adicionales
        public static readonly ActivateDesactivateLTE.ServiciosLTEPortTypeClient SiacFixedActivationDesactivacionLte = Claro.Data.Service.Create<ActivateDesactivateLTE.ServiciosLTEPortTypeClient>("FixedAddServLTE");
        public static readonly AdditionalServicesHFC.ebsServiciosHFCSB11Client SiacFixedActivationDesactivacionHfc = Claro.Data.Service.Create<AdditionalServicesHFC.ebsServiciosHFCSB11Client>("FixedAddServHFC");
        //FIN Activacion y Desactivacion de servivios adicionales

        public static readonly EsbDwhClient SIACUConsultaIMEI = Claro.Data.Service.Create<EsbDwhClient>("strWebServiceConsultaIMEI");
        public static readonly EbsSWSAPClient SIACUConsultMarkModel = Claro.Data.Service.Create<EbsSWSAPClient>("strWebServiceDatosSAP");

        public static readonly registrarTrazabilidadPortTypeClient SIACURegistrarTrazabilidad = Claro.Data.Service.Create<registrarTrazabilidadPortTypeClient>("strRegistrarTrazabilidad");
        

        public static readonly relacionPuntosAtencionPortClient SIACU_PUNTOSATENCION = Claro.Data.Service.Create<relacionPuntosAtencionPortClient>("SiacuPuntosAtencion");
        public static readonly relacionConsultaPlanTFIPortClient PREPAID_PLANESTFI = Claro.Data.Service.Create<relacionConsultaPlanTFIPortClient>("SIACPlanesTFI");
        public static readonly CambioPlanPrepagoTFIPortTypeClient PREPAID_CAMBIOPLANTFI = Claro.Data.Service.Create<CambioPlanPrepagoTFIPortTypeClient>("SIACCambioPlanTFI");
       
        //consulta claves
        public static readonly ebsConsultaClavesPortTypeClient CONSULTA_CLAVES = Claro.Data.Service.Create<ebsConsultaClavesPortTypeClient>("strConsultaClavesWS");
        #region JCAA
        public static readonly cambioPlanFijaPortClient SiacFixedPlanMigration = Claro.Data.Service.Create<cambioPlanFijaPortClient>("SiacFixedPlanMigration");

        #endregion

        #region ConfigurationServices
        public static readonly ConfigServices.BPEL_ConfiguracionIPPortClient FIXED_CONFIGURATION_SERVICES = Claro.Data.Service.Create<ConfigServices.BPEL_ConfiguracionIPPortClient>("FixedConfigurationServicesHFC");
         
        #endregion

        //Instalacion Desinstalacion de Decos
        public static readonly UninstallInstallDecos.InstalacionDesinstalacionDecosWSPortTypeClient FixedUninstallinstallDecosLte = Claro.Data.Service.Create<UninstallInstallDecos.InstalacionDesinstalacionDecosWSPortTypeClient>("WSUninstallInstallDecosLTE");
        #region Cambio de Plan LTE
        public static readonly CambioPlanLTEWSPortTypeClient SiacFixedPlanMigrationLTE = Claro.Data.Service.Create<CambioPlanLTEWSPortTypeClient>("SiacFixedPlanMigrationLTE");
        #endregion
       public static readonly CambioEquipoWSPortTypeClient FixedChangeEquipmentWS = Claro.Data.Service.Create<CambioEquipoWSPortTypeClient>("WSURLChangeEquipment");

        #region  PROY-140245-IDEA140240
       public static readonly EmailServiceClient SIACU_SubmitNotificationEmail = Claro.Data.Service.Create<EmailServiceClient>("strEnviarNotificacionEmail");
        #endregion
     
        #region Proy-32650
        public static readonly Claro.SIACU.ProxyService.Transac.Service.SIAC.ServAdiDescuentoWS.gestionServAdiDescuentoWSPortTypeClient SIACUServAdiDescuento = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIAC.ServAdiDescuentoWS.gestionServAdiDescuentoWSPortTypeClient>("WSServAdiDescuento");
        public static readonly Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.TransaccionInteraccionesClient TransaccionInteraccion = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.TransaccionInteraccionesClient>("WSTrasaccionInteraccion");
        public static readonly Claro.SIACU.ProxyService.Transac.Service.EBSRegistroAjusteSAR.EbsRegistroAjusteSARPortTypeClient REGISTRAR_SAR = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.EBSRegistroAjusteSAR.EbsRegistroAjusteSARPortTypeClient>("strRegistrarSAR");
        public static readonly Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultationPayments.ConsultaPagosClient CONSULTA_PAGOS = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultationPayments.ConsultaPagosClient>("WSConsultationPayments");

        public static readonly Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE.ConsultaOpcionesAuditoriaPortTypeClient LDE = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE.ConsultaOpcionesAuditoriaPortTypeClient>("WSConsultaOpcionesAuditoriaLDE");

        public static readonly Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LPOPU.ConsultaOpcionesAuditoriaPortTypeClient SECURITY_PERMISSIONSDP = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LPOPU.ConsultaOpcionesAuditoriaPortTypeClient>("strWebServiceSecurityPermissionsDP"); 
        #endregion

        public static readonly ValidarLineasCliente.validarLineasClientePortTypeClient ValidateLine = Claro.Data.Service.Create<ValidarLineasCliente.validarLineasClientePortTypeClient>("FixedValidaLineaCliente");
        public static readonly PCRFWSPortTypeSOAP11Client SiacFixedPCRFConnector = Claro.Data.Service.Create<PCRFWSPortTypeSOAP11Client>("WSPCRFConnector");

        /*
         CAYCHOJJ
         */
        public static readonly svcOnBaseClaroCargaSoapClient onBaseLoad = Claro.Data.Service.Create<svcOnBaseClaroCargaSoapClient>("strOnbaseLoad");

        //<AMCO> 
        public static readonly Claro.SIACU.ProxyService.Transac.Service.SIAC.ConsultaLineaCuenta.ConsultaLineaCuentaWSPortTypeClient CONSULTALINEACUENTA = Claro.Data.Service.Create<Claro.SIACU.ProxyService.Transac.Service.SIAC.ConsultaLineaCuenta.ConsultaLineaCuentaWSPortTypeClient>("strConsultaLineaCliente");
        //<AMCO> 

        public static readonly BSS_ConsultaDatosSIMPortClient CONSULTA_DATOS_SIM = Claro.Data.Service.Create<BSS_ConsultaDatosSIMPortClient>("strConsultarDatosSIMWS"); //INICIATIVA-871
    }
}
