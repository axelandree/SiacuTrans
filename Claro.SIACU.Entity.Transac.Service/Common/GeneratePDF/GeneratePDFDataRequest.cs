using Claro.SIACU.Entity.Transac.Service.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GeneratePDF
{
    [DataContract(Name = "GeneratePDFDataRequestHfc")]
    public class GeneratePDFDataRequest : Claro.Entity.Request
    {

        private List<ServiceArmaPlan> arrListaInfo;
        //private List<Producto> arrListaInfo2;
        //private List<DocumAutDet> arrListaInfo3;
        //private List<DocumAutDet> arrListaInfo4;
        private string strNroServicio;
        private string strTitularCliente;
        private string strContactoCliente;
        private string strPlanActual;
        private string strCentroAtencionArea;
        private string strTipoDocIdentidad;
        private string strNroDocIdentidad;
        private string strFechaTransaccionProgram;
        private string strCasoInter;
        private string strRepresLegal;
        private string strNroDocRepLegal;
        private string strCustomerId;
        private string strCuentaPostpago;
        private string strNotas;
        private string strTelfReferencia;
        private string strCicloFacturacion;
        private string strFechaEjecucion;
        private string strCodUsuario;
        private string strNombreAgenteUsuario;
        private string strAplicaEmail;
        private string strEmail;
        private string strAplicaOtroContacto;
        private string strContactoOtro;
        private string strNroDocContactoOtro;
        private string strMotivoPariente;
        private string strTelfOtroContacto;
        private string strCanalAtencion;
        private string strFlagPlantillaPlazo;
        private string strEscenarioServicioCom;
        private string strAplicaProgramacion;
        private string strCfServicioCom;
        private string strFechaPlazo;
        private string strPlazo;
        private string strCfServicioModif;
        private string strDescripcionServicioCom;
        private string strPeriodoCuotaServicio;
        private string strBolsaSolesAdicionales;
        private string strImei;
        private string strMarcaEquipo;
        private string strModeloEquipo;
        private string strTransaccionBloqueo;
        private string strMotivoTipoBloqueo;
        private string strTopeConsumo;
        private string strFechaEjecTopeCons;
        private string strFlagGrilaAtp;
        private string strEscenarioMigracion;
        private string strNuevoPlan;
        private string strCfTotalNuevo;
        private string strMontoApadece;
        private string strMontoPcs;
        private string strMotivoCancelacion;
        private string strEscenarioRetencion;
        private string strAccionRetencion;
        private string strModalidad;
        private string strProductos;
        private string strSecuencialOrden;
        private string strIdProductoCc;
        private string strDescProductoCc;
        private string strPuntosCc;
        private string strCantidadCc;
        private string strDescuentoCc;
        private string strTipoPremioCc;
        private string strMontoRecargaCc;
        private string strPuntosCcAntesTrans;
        private string strCantidadTotalCanjeDev;
        private string strTipoDocFact;
        private string strNroDocFact;
        private string strDireccionPostal;
        private string strDistritoPostal;
        private string strProvinciaPostal;
        private string strDepartamentoPostal;
        private string strFechaEmisionDocFact;
        private string strFechaVencDocFact;
        private string strFlagPosicionIgv;
        private string strCategoriaFact;
        private string strSubcategoriaFact;
        private string strSubcategoriaFactSinIgv;
        private string strNroServicio2;
        private string strFechaDesdeAjuste;
        private string strFechaHastaAjuste;
        private string strImporteConceptoAjusteSinIgv;
        private string strSubtotalAjusteSinIgv;
        private string strIgvTax;
        private string strTotalConIgv;
        private string strTotalAjuste;
        private string strMotivoCambioSim;
        private string strNuevoSim;
        private string strCostoTransaccion;
        private string strFlag4G;
        private string strSim4GLte;
        private string strEstadoServicio4G;
        private string strNroDocIdentidadRef;
        private string strFechaEmisionDocRef;
        private string strCodDesbloqueo;

        // Variables para guardar opciones del servidor
        private string strServidorGenerarPDF;
        private string strServidorLeerPDF;
        private string strCarpetaPDFs;
        private string strCarpetaTransaccion;
        private string strNombreArchivoTransaccion;

        private string strDescripTransaccion;
        private string strAccion;
        private string strPaisA;
        private string strCodigoLocalA;
        private string strReferenciaActual;
        private string strReferenciaDestino;
        private string strPaisB;
        private string strDepartamentoLocalB;
        private string strProvinciaLocalB;
        private string strDistrtitoLocalB;
        private string strCodigoLocalB;
        private string strDireccionPostalC;
        private string strAplicaCambioDireccion;
        private string strAplicaCambioNombre;
        private string strCentroPobladoActual;
        private string strCentroPobladoDestino;
        private string strFechaSuspension;
        private string strFechaActivacion;
        private string strCostoReactivacion;
        private string strFlagExterInter;
        private string strMontSolicitad;
        [DataMember]
        public string StrFlagExterInter { get { return strFlagExterInter; } set { strFlagExterInter = value; } }
        [DataMember]
        public string StrDescripTransaccion { get { return strDescripTransaccion; } set { strDescripTransaccion = value; } }
        [DataMember]
        public string StrPaisA { get { return strPaisA; } set { strPaisA = value; } }
        [DataMember]
        public string StrCentroPobladoActual { get { return strCentroPobladoActual; } set { strCentroPobladoActual = value; } }
        [DataMember]
        public string StrCentroPobladoDestino { get { return strCentroPobladoDestino; } set { strCentroPobladoDestino = value; } }
        [DataMember]
        public string StrAccion { get { return strAccion; } set { strAccion = value; } }

        [DataMember]
        public string StrFechaSuspension { get { return strFechaSuspension; } set { strFechaSuspension = value; } }
        [DataMember]
        public string StrFechaActivacion { get { return strFechaActivacion; } set { strFechaActivacion = value; } }
        [DataMember]
        public string StrCostoReactivacion { get { return strCostoReactivacion; } set { strCostoReactivacion = value; } }




        [DataMember]
        public string StrCodigoLocalA { get { return strCodigoLocalA; } set { strCodigoLocalA = value; } }
        [DataMember]
        public string StrReferenciaActual { get { return strReferenciaActual; } set { strReferenciaActual = value; } }
        [DataMember]
        public string StrReferenciaDestino { get { return strReferenciaDestino; } set { strReferenciaDestino = value; } }
        [DataMember]
        public string StrPaisB { get { return strPaisB; } set { strPaisB = value; } }
        [DataMember]
        public string StrDepartamentoLocalB { get { return strDepartamentoLocalB; } set { strDepartamentoLocalB = value; } }
        [DataMember]
        public string StrProvinciaLocalB { get { return strProvinciaLocalB; } set { strProvinciaLocalB = value; } }
        [DataMember]
        public string StrDistrtitoLocalB { get { return strDistrtitoLocalB; } set { strDistrtitoLocalB = value; } }
        [DataMember]
        public string StrCodigoLocalB { get { return strCodigoLocalB; } set { strCodigoLocalB = value; } }
        [DataMember]
        public string StrDireccionPostalC { get { return strDireccionPostalC; } set { strDireccionPostalC = value; } }
        [DataMember]
        public string StrAplicaCambioDireccion { get { return strAplicaCambioDireccion; } set { strAplicaCambioDireccion = value; } }
        [DataMember]
        public string StrAplicaCambioNombre { get { return strAplicaCambioNombre; } set { strAplicaCambioNombre = value; } }
        [DataMember]
        public List<ServiceArmaPlan> ArrListaInfo { get { return arrListaInfo; } set { arrListaInfo = value; } }
        //[DataMember]
        //public List<Producto> ArrListaInfo2 { get { return arrListaInfo2; } set { arrListaInfo2 = value; } }
        //[DataMember]
        //public List<DocumAutDet> ArrListaInfo3 { get { return arrListaInfo3; } set { arrListaInfo3 = value; } }
        //[DataMember]
        //public List<DocumAutDet> ArrListaInfo4 { get { return arrListaInfo4; } set { arrListaInfo4 = value; } }
        [DataMember]
        public string StrNroServicio { get { return strNroServicio; } set { strNroServicio = value; } }
        [DataMember]
        public string StrTitularCliente { get { return strTitularCliente; } set { strTitularCliente = value; } }
        [DataMember]
        public string StrContactoCliente { get { return strContactoCliente; } set { strContactoCliente = value; } }
        [DataMember]
        public string StrPlanActual { get { return strPlanActual; } set { strPlanActual = value; } }
        [DataMember]
        public string StrCentroAtencionArea { get { return strCentroAtencionArea; } set { strCentroAtencionArea = value; } }
        [DataMember]
        public string StrTipoDocIdentidad { get { return strTipoDocIdentidad; } set { strTipoDocIdentidad = value; } }
        [DataMember]
        public string StrNroDocIdentidad { get { return strNroDocIdentidad; } set { strNroDocIdentidad = value; } }
        [DataMember]
        public string StrFechaTransaccionProgram { get { return strFechaTransaccionProgram; } set { strFechaTransaccionProgram = value; } }
        [DataMember]
        public string StrCasoInter { get { return strCasoInter; } set { strCasoInter = value; } }
        [DataMember]
        public string StrRepresLegal { get { return strRepresLegal; } set { strRepresLegal = value; } }
        [DataMember]
        public string StrNroDocRepLegal { get { return strNroDocRepLegal; } set { strNroDocRepLegal = value; } }
        [DataMember]
        public string StrCustomerId { get { return strCustomerId; } set { strCustomerId = value; } }
        [DataMember]
        public string StrCuentaPostpago { get { return strCuentaPostpago; } set { strCuentaPostpago = value; } }
        [DataMember]
        public string StrNotas { get { return strNotas; } set { strNotas = value; } }
        [DataMember]
        public string StrTelfReferencia { get { return strTelfReferencia; } set { strTelfReferencia = value; } }
        [DataMember]
        public string StrCicloFacturacion { get { return strCicloFacturacion; } set { strCicloFacturacion = value; } }
        [DataMember]
        public string StrFechaEjecucion { get { return strFechaEjecucion; } set { strFechaEjecucion = value; } }
        [DataMember]
        public string StrCodUsuario { get { return strCodUsuario; } set { strCodUsuario = value; } }
        [DataMember]
        public string StrNombreAgenteUsuario { get { return strNombreAgenteUsuario; } set { strNombreAgenteUsuario = value; } }
        [DataMember]
        public string StrAplicaEmail { get { return strAplicaEmail; } set { strAplicaEmail = value; } }
        [DataMember]
        public string StrEmail { get { return strEmail; } set { strEmail = value; } }
        [DataMember]
        public string StrAplicaOtroContacto { get { return strAplicaOtroContacto; } set { strAplicaOtroContacto = value; } }
        [DataMember]
        public string StrContactoOtro { get { return strContactoOtro; } set { strContactoOtro = value; } }
        [DataMember]
        public string StrNroDocContactoOtro { get { return strNroDocContactoOtro; } set { strNroDocContactoOtro = value; } }
        [DataMember]
        public string StrMotivoPariente { get { return strMotivoPariente; } set { strMotivoPariente = value; } }
        [DataMember]
        public string StrTelfOtroContacto { get { return strTelfOtroContacto; } set { strTelfOtroContacto = value; } }
        [DataMember]
        public string StrCanalAtencion { get { return strCanalAtencion; } set { strCanalAtencion = value; } }
        [DataMember]
        public string StrFlagPlantillaPlazo { get { return strFlagPlantillaPlazo; } set { strFlagPlantillaPlazo = value; } }
        [DataMember]
        public string StrEscenarioServicioCom { get { return strEscenarioServicioCom; } set { strEscenarioServicioCom = value; } }
        [DataMember]
        public string StrAplicaProgramacion { get { return strAplicaProgramacion; } set { strAplicaProgramacion = value; } }
        [DataMember]
        public string StrCfServicioCom { get { return strCfServicioCom; } set { strCfServicioCom = value; } }
        [DataMember]
        public string StrFechaPlazo { get { return strFechaPlazo; } set { strFechaPlazo = value; } }
        [DataMember]
        public string StrPlazo { get { return strPlazo; } set { strPlazo = value; } }
        [DataMember]
        public string StrCfServicioModif { get { return strCfServicioModif; } set { strCfServicioModif = value; } }
        [DataMember]
        public string StrDescripcionServicioCom { get { return strDescripcionServicioCom; } set { strDescripcionServicioCom = value; } }
        [DataMember]
        public string StrPeriodoCuotaServicio { get { return strPeriodoCuotaServicio; } set { strPeriodoCuotaServicio = value; } }
        [DataMember]
        public string StrBolsaSolesAdicionales { get { return strBolsaSolesAdicionales; } set { strBolsaSolesAdicionales = value; } }
        [DataMember]
        public string StrImei { get { return strImei; } set { strImei = value; } }
        [DataMember]
        public string StrMarcaEquipo { get { return strMarcaEquipo; } set { strMarcaEquipo = value; } }
        [DataMember]
        public string StrModeloEquipo { get { return strModeloEquipo; } set { strModeloEquipo = value; } }
        [DataMember]
        public string StrTransaccionBloqueo { get { return strTransaccionBloqueo; } set { strTransaccionBloqueo = value; } }
        [DataMember]
        public string StrMotivoTipoBloqueo { get { return strMotivoTipoBloqueo; } set { strMotivoTipoBloqueo = value; } }
        [DataMember]
        public string StrTopeConsumo { get { return strTopeConsumo; } set { strTopeConsumo = value; } }
        [DataMember]
        public string StrFechaEjecTopeCons { get { return strFechaEjecTopeCons; } set { strFechaEjecTopeCons = value; } }
        [DataMember]
        public string StrFlagGrilaAtp { get { return strFlagGrilaAtp; } set { strFlagGrilaAtp = value; } }
        [DataMember]
        public string StrEscenarioMigracion { get { return strEscenarioMigracion; } set { strEscenarioMigracion = value; } }
        [DataMember]
        public string StrNuevoPlan { get { return strNuevoPlan; } set { strNuevoPlan = value; } }
        [DataMember]
        public string StrCfTotalNuevo { get { return strCfTotalNuevo; } set { strCfTotalNuevo = value; } }
        [DataMember]
        public string StrMontoApadece { get { return strMontoApadece; } set { strMontoApadece = value; } }
        [DataMember]
        public string StrMontoPcs { get { return strMontoPcs; } set { strMontoPcs = value; } }
        [DataMember]
        public string StrMotivoCancelacion { get { return strMotivoCancelacion; } set { strMotivoCancelacion = value; } }
        [DataMember]
        public string StrEscenarioRetencion { get { return strEscenarioRetencion; } set { strEscenarioRetencion = value; } }
        [DataMember]
        public string StrAccionRetencion { get { return strAccionRetencion; } set { strAccionRetencion = value; } }
        [DataMember]
        public string StrModalidad { get { return strModalidad; } set { strModalidad = value; } }
        [DataMember]
        public string StrProductos { get { return strProductos; } set { strProductos = value; } }
        [DataMember]
        public string StrSecuencialOrden { get { return strSecuencialOrden; } set { strSecuencialOrden = value; } }
        [DataMember]
        public string StrIdProductoCc { get { return strIdProductoCc; } set { strIdProductoCc = value; } }
        [DataMember]
        public string StrDescProductoCc { get { return strDescProductoCc; } set { strDescProductoCc = value; } }
        [DataMember]
        public string StrPuntosCc { get { return strPuntosCc; } set { strPuntosCc = value; } }
        [DataMember]
        public string StrCantidadCc { get { return strCantidadCc; } set { strCantidadCc = value; } }
        [DataMember]
        public string StrDescuentoCc { get { return strDescuentoCc; } set { strDescuentoCc = value; } }
        [DataMember]
        public string StrTipoPremioCc { get { return strTipoPremioCc; } set { strTipoPremioCc = value; } }
        [DataMember]
        public string StrMontoRecargaCc { get { return strMontoRecargaCc; } set { strMontoRecargaCc = value; } }
        [DataMember]
        public string StrPuntosCcAntesTrans { get { return strPuntosCcAntesTrans; } set { strPuntosCcAntesTrans = value; } }
        [DataMember]
        public string StrCantidadTotalCanjeDev { get { return strCantidadTotalCanjeDev; } set { strCantidadTotalCanjeDev = value; } }
        [DataMember]
        public string StrTipoDocFact { get { return strTipoDocFact; } set { strTipoDocFact = value; } }
        [DataMember]
        public string StrNroDocFact { get { return strNroDocFact; } set { strNroDocFact = value; } }
        [DataMember]
        public string StrDireccionPostal { get { return strDireccionPostal; } set { strDireccionPostal = value; } }
        [DataMember]
        public string StrDistritoPostal { get { return strDistritoPostal; } set { strDistritoPostal = value; } }
        [DataMember]
        public string StrProvinciaPostal { get { return strProvinciaPostal; } set { strProvinciaPostal = value; } }
        [DataMember]
        public string StrDepartamentoPostal { get { return strDepartamentoPostal; } set { strDepartamentoPostal = value; } }
        [DataMember]
        public string StrFechaEmisionDocFact { get { return strFechaEmisionDocFact; } set { strFechaEmisionDocFact = value; } }
        [DataMember]
        public string StrFechaVencDocFact { get { return strFechaVencDocFact; } set { strFechaVencDocFact = value; } }
        [DataMember]
        public string StrFlagPosicionIgv { get { return strFlagPosicionIgv; } set { strFlagPosicionIgv = value; } }
        [DataMember]
        public string StrCategoriaFact { get { return strCategoriaFact; } set { strCategoriaFact = value; } }
        [DataMember]
        public string StrSubcategoriaFact { get { return strSubcategoriaFact; } set { strSubcategoriaFact = value; } }
        [DataMember]
        public string StrSubcategoriaFactSinIgv { get { return strSubcategoriaFactSinIgv; } set { strSubcategoriaFactSinIgv = value; } }
        [DataMember]
        public string StrNroServicio2 { get { return strNroServicio2; } set { strNroServicio2 = value; } }
        [DataMember]
        public string StrFechaDesdeAjuste { get { return strFechaDesdeAjuste; } set { strFechaDesdeAjuste = value; } }
        [DataMember]
        public string StrFechaHastaAjuste { get { return strFechaHastaAjuste; } set { strFechaHastaAjuste = value; } }
        [DataMember]
        public string StrImporteConceptoAjusteSinIgv { get { return strImporteConceptoAjusteSinIgv; } set { strImporteConceptoAjusteSinIgv = value; } }
        [DataMember]
        public string StrSubtotalAjusteSinIgv { get { return strSubtotalAjusteSinIgv; } set { strSubtotalAjusteSinIgv = value; } }
        [DataMember]
        public string StrIgvTax { get { return strIgvTax; } set { strIgvTax = value; } }
        [DataMember]
        public string StrTotalConIgv { get { return strTotalConIgv; } set { strTotalConIgv = value; } }
        [DataMember]
        public string StrTotalAjuste { get { return strTotalAjuste; } set { strTotalAjuste = value; } }
        [DataMember]
        public string StrMotivoCambioSim { get { return strMotivoCambioSim; } set { strMotivoCambioSim = value; } }
        [DataMember]
        public string StrNuevoSim { get { return strNuevoSim; } set { strNuevoSim = value; } }
        [DataMember]
        public string StrCostoTransaccion { get { return strCostoTransaccion; } set { strCostoTransaccion = value; } }
        [DataMember]
        public string StrFlag4G { get { return strFlag4G; } set { strFlag4G = value; } }
        [DataMember]
        public string StrSim4GLte { get { return strSim4GLte; } set { strSim4GLte = value; } }
        [DataMember]
        public string StrEstadoServicio4G { get { return strEstadoServicio4G; } set { strEstadoServicio4G = value; } }
        [DataMember]
        public string StrNroDocIdentidadRef { get { return strNroDocIdentidadRef; } set { strNroDocIdentidadRef = value; } }
        [DataMember]
        public string StrFechaEmisionDocRef { get { return strFechaEmisionDocRef; } set { strFechaEmisionDocRef = value; } }
        [DataMember]
        public string StrCodDesbloqueo { get { return strCodDesbloqueo; } set { strCodDesbloqueo = value; } }

        //Set y Get de variables para guardar opciones del servidor
        [DataMember]
        public string StrServidorGenerarPDF { get { return strServidorGenerarPDF; } set { strServidorGenerarPDF = value; } }
        [DataMember]
        public string StrServidorLeerPDF { get { return strServidorLeerPDF; } set { strServidorLeerPDF = value; } }
        [DataMember]
        public string StrCarpetaPDFs { get { return strCarpetaPDFs; } set { strCarpetaPDFs = value; } }
        [DataMember]
        public string StrCarpetaTransaccion { get { return strCarpetaTransaccion; } set { strCarpetaTransaccion = value; } }
        [DataMember]
        public string StrNombreArchivoTransaccion { get { return strNombreArchivoTransaccion; } set { strNombreArchivoTransaccion = value; } }
        [DataMember]
        public string StrMontSolicitad { get { return strMontSolicitad; } set { strMontSolicitad = value; } }
    }
}
