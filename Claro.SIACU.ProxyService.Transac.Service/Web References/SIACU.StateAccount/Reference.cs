﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Claro.SIACU.ProxyService.Transac.Service.SIACU.StateAccount {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ConsultaEstadoCuentaSOAP", Namespace="http://claro.com.pe/eai/oac/consultaestadocuenta/")]
    public partial class ConsultaEstadoCuenta : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback consultaEstadoCuentaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ConsultaEstadoCuenta() {
            this.Url = global::Claro.SIACU.ProxyService.Transac.Service.Properties.Settings.Default.Claro_SIACU_ProxyService_Transac_Service_SIACU_StateAccount_ConsultaEstadoCuenta;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event consultaEstadoCuentaCompletedEventHandler consultaEstadoCuentaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://claro.com.pe/eai/oac/consultaestadocuenta/consultaEstadoCuenta", RequestNamespace="http://claro.com.pe/eai/oac/consultaestadocuenta/", ResponseNamespace="http://claro.com.pe/eai/oac/consultaestadocuenta/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("audit")]
        public AuditType consultaEstadoCuenta(string txId, string pCodAplicacion, string pUsuarioAplic, string pTipoConsulta, string pTipoServicio, string pCliNroCuenta, string pNroTelefono, string pFlagSoloSaldo, string pFlagSoloDisputa, string pFechaDesde, string pFechaHasta, decimal pTamanoPagina, decimal pNroPagina, [System.Xml.Serialization.XmlArrayItemAttribute("xDetalleEstadoCuentaCab", IsNullable=false)] out DetalleEstadoCuentaCabType[] xEstadoCuenta) {
            object[] results = this.Invoke("consultaEstadoCuenta", new object[] {
                        txId,
                        pCodAplicacion,
                        pUsuarioAplic,
                        pTipoConsulta,
                        pTipoServicio,
                        pCliNroCuenta,
                        pNroTelefono,
                        pFlagSoloSaldo,
                        pFlagSoloDisputa,
                        pFechaDesde,
                        pFechaHasta,
                        pTamanoPagina,
                        pNroPagina});
            xEstadoCuenta = ((DetalleEstadoCuentaCabType[])(results[1]));
            return ((AuditType)(results[0]));
        }
        
        /// <remarks/>
        public void consultaEstadoCuentaAsync(string txId, string pCodAplicacion, string pUsuarioAplic, string pTipoConsulta, string pTipoServicio, string pCliNroCuenta, string pNroTelefono, string pFlagSoloSaldo, string pFlagSoloDisputa, string pFechaDesde, string pFechaHasta, decimal pTamanoPagina, decimal pNroPagina) {
            this.consultaEstadoCuentaAsync(txId, pCodAplicacion, pUsuarioAplic, pTipoConsulta, pTipoServicio, pCliNroCuenta, pNroTelefono, pFlagSoloSaldo, pFlagSoloDisputa, pFechaDesde, pFechaHasta, pTamanoPagina, pNroPagina, null);
        }
        
        /// <remarks/>
        public void consultaEstadoCuentaAsync(string txId, string pCodAplicacion, string pUsuarioAplic, string pTipoConsulta, string pTipoServicio, string pCliNroCuenta, string pNroTelefono, string pFlagSoloSaldo, string pFlagSoloDisputa, string pFechaDesde, string pFechaHasta, decimal pTamanoPagina, decimal pNroPagina, object userState) {
            if ((this.consultaEstadoCuentaOperationCompleted == null)) {
                this.consultaEstadoCuentaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconsultaEstadoCuentaOperationCompleted);
            }
            this.InvokeAsync("consultaEstadoCuenta", new object[] {
                        txId,
                        pCodAplicacion,
                        pUsuarioAplic,
                        pTipoConsulta,
                        pTipoServicio,
                        pCliNroCuenta,
                        pNroTelefono,
                        pFlagSoloSaldo,
                        pFlagSoloDisputa,
                        pFechaDesde,
                        pFechaHasta,
                        pTamanoPagina,
                        pNroPagina}, this.consultaEstadoCuentaOperationCompleted, userState);
        }
        
        private void OnconsultaEstadoCuentaOperationCompleted(object arg) {
            if ((this.consultaEstadoCuentaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.consultaEstadoCuentaCompleted(this, new consultaEstadoCuentaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/servicecommons/")]
    public partial class AuditType {
        
        private string txIdField;
        
        private string errorCodeField;
        
        private string errorMsgField;
        
        /// <comentarios/>
        public string txId {
            get {
                return this.txIdField;
            }
            set {
                this.txIdField = value;
            }
        }
        
        /// <comentarios/>
        public string errorCode {
            get {
                return this.errorCodeField;
            }
            set {
                this.errorCodeField = value;
            }
        }
        
        /// <comentarios/>
        public string errorMsg {
            get {
                return this.errorMsgField;
            }
            set {
                this.errorMsgField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/oac/consultaestadocuenta/")]
    public partial class DetalleEstadoCtaType {
        
        private string xTipoDocumentoField;
        
        private string xNroDocumentoField;
        
        private string xDescripDocumentoField;
        
        private string xEstadoDocumentoField;
        
        private string xFechaRegistroField;
        
        private string xFechaEmisionField;
        
        private string xFechaVencimientoField;
        
        private string xTipoMonedaField;
        
        private System.Nullable<decimal> xMontoDocumentoField;
        
        private System.Nullable<decimal> xMontoFcoField;
        
        private System.Nullable<decimal> xMontoFinanField;
        
        private System.Nullable<decimal> xSaldoDocumentoField;
        
        private System.Nullable<decimal> xSaldoFcoField;
        
        private System.Nullable<decimal> xSaldoFinanField;
        
        private System.Nullable<decimal> xMontoSolesField;
        
        private System.Nullable<decimal> xMontoDolaresField;
        
        private System.Nullable<decimal> xCargoField;
        
        private System.Nullable<decimal> xAbonoField;
        
        private System.Nullable<decimal> xSaldoCuentaField;
        
        private string xNroOperacionPagoField;
        
        private string xFechaPagoField;
        
        private string xFormaPagoField;
        
        private System.Nullable<decimal> xDocAnioField;
        
        private System.Nullable<decimal> xDocMesField;
        
        private System.Nullable<decimal> xDocAnioVencField;
        
        private System.Nullable<decimal> xDocMesVencField;
        
        private string xFlagCargoCtaField;
        
        private string xNroTicketField;
        
        private System.Nullable<decimal> xMontoReclamadoField;
        
        private string xTelefonoField;
        
        private string xUsuarioField;
        
        private string xIdDocOrigenField;
        
        private string xDescripExtendField;
        
        private decimal xIdDocOACField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xTipoDocumento {
            get {
                return this.xTipoDocumentoField;
            }
            set {
                this.xTipoDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xNroDocumento {
            get {
                return this.xNroDocumentoField;
            }
            set {
                this.xNroDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xDescripDocumento {
            get {
                return this.xDescripDocumentoField;
            }
            set {
                this.xDescripDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xEstadoDocumento {
            get {
                return this.xEstadoDocumentoField;
            }
            set {
                this.xEstadoDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaRegistro {
            get {
                return this.xFechaRegistroField;
            }
            set {
                this.xFechaRegistroField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaEmision {
            get {
                return this.xFechaEmisionField;
            }
            set {
                this.xFechaEmisionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaVencimiento {
            get {
                return this.xFechaVencimientoField;
            }
            set {
                this.xFechaVencimientoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xTipoMoneda {
            get {
                return this.xTipoMonedaField;
            }
            set {
                this.xTipoMonedaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoDocumento {
            get {
                return this.xMontoDocumentoField;
            }
            set {
                this.xMontoDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoFco {
            get {
                return this.xMontoFcoField;
            }
            set {
                this.xMontoFcoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoFinan {
            get {
                return this.xMontoFinanField;
            }
            set {
                this.xMontoFinanField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xSaldoDocumento {
            get {
                return this.xSaldoDocumentoField;
            }
            set {
                this.xSaldoDocumentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xSaldoFco {
            get {
                return this.xSaldoFcoField;
            }
            set {
                this.xSaldoFcoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xSaldoFinan {
            get {
                return this.xSaldoFinanField;
            }
            set {
                this.xSaldoFinanField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoSoles {
            get {
                return this.xMontoSolesField;
            }
            set {
                this.xMontoSolesField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoDolares {
            get {
                return this.xMontoDolaresField;
            }
            set {
                this.xMontoDolaresField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xCargo {
            get {
                return this.xCargoField;
            }
            set {
                this.xCargoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xAbono {
            get {
                return this.xAbonoField;
            }
            set {
                this.xAbonoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xSaldoCuenta {
            get {
                return this.xSaldoCuentaField;
            }
            set {
                this.xSaldoCuentaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xNroOperacionPago {
            get {
                return this.xNroOperacionPagoField;
            }
            set {
                this.xNroOperacionPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaPago {
            get {
                return this.xFechaPagoField;
            }
            set {
                this.xFechaPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFormaPago {
            get {
                return this.xFormaPagoField;
            }
            set {
                this.xFormaPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDocAnio {
            get {
                return this.xDocAnioField;
            }
            set {
                this.xDocAnioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDocMes {
            get {
                return this.xDocMesField;
            }
            set {
                this.xDocMesField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDocAnioVenc {
            get {
                return this.xDocAnioVencField;
            }
            set {
                this.xDocAnioVencField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDocMesVenc {
            get {
                return this.xDocMesVencField;
            }
            set {
                this.xDocMesVencField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFlagCargoCta {
            get {
                return this.xFlagCargoCtaField;
            }
            set {
                this.xFlagCargoCtaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xNroTicket {
            get {
                return this.xNroTicketField;
            }
            set {
                this.xNroTicketField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xMontoReclamado {
            get {
                return this.xMontoReclamadoField;
            }
            set {
                this.xMontoReclamadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xTelefono {
            get {
                return this.xTelefonoField;
            }
            set {
                this.xTelefonoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xUsuario {
            get {
                return this.xUsuarioField;
            }
            set {
                this.xUsuarioField = value;
            }
        }
        
        /// <comentarios/>
        public string xIdDocOrigen {
            get {
                return this.xIdDocOrigenField;
            }
            set {
                this.xIdDocOrigenField = value;
            }
        }
        
        /// <comentarios/>
        public string xDescripExtend {
            get {
                return this.xDescripExtendField;
            }
            set {
                this.xDescripExtendField = value;
            }
        }
        
        /// <comentarios/>
        public decimal xIdDocOAC {
            get {
                return this.xIdDocOACField;
            }
            set {
                this.xIdDocOACField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3163.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/oac/consultaestadocuenta/")]
    public partial class DetalleEstadoCuentaCabType {
        
        private string xNombreClienteField;
        
        private System.Nullable<decimal> xDeudaActualField;
        
        private System.Nullable<decimal> xDeudaVencidaField;
        
        private System.Nullable<decimal> xTotalMontoDisputaField;
        
        private string xFechaUltFacturaField;
        
        private string xFechaUtlPagoField;
        
        private string xCodCuentaField;
        
        private string xCodCuentaAlternaField;
        
        private string xDescUbigeoField;
        
        private string xTipoClienteField;
        
        private string xEstadoCuentaField;
        
        private string xFechaActivacionField;
        
        private string xCicloFacturacionField;
        
        private System.Nullable<decimal> xLimiteCreditoField;
        
        private string xCreditScoreField;
        
        private string xTipoPagoField;
        
        private DetalleEstadoCtaType[] xDetalleTrxField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xNombreCliente {
            get {
                return this.xNombreClienteField;
            }
            set {
                this.xNombreClienteField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDeudaActual {
            get {
                return this.xDeudaActualField;
            }
            set {
                this.xDeudaActualField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xDeudaVencida {
            get {
                return this.xDeudaVencidaField;
            }
            set {
                this.xDeudaVencidaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xTotalMontoDisputa {
            get {
                return this.xTotalMontoDisputaField;
            }
            set {
                this.xTotalMontoDisputaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaUltFactura {
            get {
                return this.xFechaUltFacturaField;
            }
            set {
                this.xFechaUltFacturaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaUtlPago {
            get {
                return this.xFechaUtlPagoField;
            }
            set {
                this.xFechaUtlPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xCodCuenta {
            get {
                return this.xCodCuentaField;
            }
            set {
                this.xCodCuentaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xCodCuentaAlterna {
            get {
                return this.xCodCuentaAlternaField;
            }
            set {
                this.xCodCuentaAlternaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xDescUbigeo {
            get {
                return this.xDescUbigeoField;
            }
            set {
                this.xDescUbigeoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xTipoCliente {
            get {
                return this.xTipoClienteField;
            }
            set {
                this.xTipoClienteField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xEstadoCuenta {
            get {
                return this.xEstadoCuentaField;
            }
            set {
                this.xEstadoCuentaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xFechaActivacion {
            get {
                return this.xFechaActivacionField;
            }
            set {
                this.xFechaActivacionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xCicloFacturacion {
            get {
                return this.xCicloFacturacionField;
            }
            set {
                this.xCicloFacturacionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> xLimiteCredito {
            get {
                return this.xLimiteCreditoField;
            }
            set {
                this.xLimiteCreditoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xCreditScore {
            get {
                return this.xCreditScoreField;
            }
            set {
                this.xCreditScoreField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string xTipoPago {
            get {
                return this.xTipoPagoField;
            }
            set {
                this.xTipoPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("xDetalleEstadoCuenta", IsNullable=false)]
        public DetalleEstadoCtaType[] xDetalleTrx {
            get {
                return this.xDetalleTrxField;
            }
            set {
                this.xDetalleTrxField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    public delegate void consultaEstadoCuentaCompletedEventHandler(object sender, consultaEstadoCuentaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultaEstadoCuentaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal consultaEstadoCuentaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AuditType Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AuditType)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public DetalleEstadoCuentaCabType[] xEstadoCuenta {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DetalleEstadoCuentaCabType[])(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591