﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Claro.SIACU.ProxyService.Transac.Service.SIACPre.BondTFI {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ebsOperacionesINPortTypeSOAP11Binding", Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ebsOperacionesINService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback consultarLineaINOperationCompleted;
        
        private System.Threading.SendOrPostCallback ejecutarOperacionINOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ebsOperacionesINService() {
            this.Url = global::Claro.SIACU.ProxyService.Transac.Service.Properties.Settings.Default.Claro_SIACU_ProxyService_Transac_Service_SIACPre_BondTFI_ebsOperacionesINService;
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
        public event consultarLineaINCompletedEventHandler consultarLineaINCompleted;
        
        /// <remarks/>
        public event ejecutarOperacionINCompletedEventHandler ejecutarOperacionINCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://claro.com.pe/eai/esb/services/postventa/operacionesin/consultarLineaIN", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("consultarLineaINResponse", Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
        public consultarLineaINResponse consultarLineaIN([System.Xml.Serialization.XmlElementAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")] consultarLineaINRequest consultarLineaINRequest) {
            object[] results = this.Invoke("consultarLineaIN", new object[] {
                        consultarLineaINRequest});
            return ((consultarLineaINResponse)(results[0]));
        }
        
        /// <remarks/>
        public void consultarLineaINAsync(consultarLineaINRequest consultarLineaINRequest) {
            this.consultarLineaINAsync(consultarLineaINRequest, null);
        }
        
        /// <remarks/>
        public void consultarLineaINAsync(consultarLineaINRequest consultarLineaINRequest, object userState) {
            if ((this.consultarLineaINOperationCompleted == null)) {
                this.consultarLineaINOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconsultarLineaINOperationCompleted);
            }
            this.InvokeAsync("consultarLineaIN", new object[] {
                        consultarLineaINRequest}, this.consultarLineaINOperationCompleted, userState);
        }
        
        private void OnconsultarLineaINOperationCompleted(object arg) {
            if ((this.consultarLineaINCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.consultarLineaINCompleted(this, new consultarLineaINCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://claro.com.pe/eai/esb/services/postventa/operacionesin/ejecutarOperacionIN", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ejecutarOperacionINResponse", Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
        public ejecutarOperacionINResponse ejecutarOperacionIN([System.Xml.Serialization.XmlElementAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")] ejecutarOperacionINRequest ejecutarOperacionINRequest) {
            object[] results = this.Invoke("ejecutarOperacionIN", new object[] {
                        ejecutarOperacionINRequest});
            return ((ejecutarOperacionINResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ejecutarOperacionINAsync(ejecutarOperacionINRequest ejecutarOperacionINRequest) {
            this.ejecutarOperacionINAsync(ejecutarOperacionINRequest, null);
        }
        
        /// <remarks/>
        public void ejecutarOperacionINAsync(ejecutarOperacionINRequest ejecutarOperacionINRequest, object userState) {
            if ((this.ejecutarOperacionINOperationCompleted == null)) {
                this.ejecutarOperacionINOperationCompleted = new System.Threading.SendOrPostCallback(this.OnejecutarOperacionINOperationCompleted);
            }
            this.InvokeAsync("ejecutarOperacionIN", new object[] {
                        ejecutarOperacionINRequest}, this.ejecutarOperacionINOperationCompleted, userState);
        }
        
        private void OnejecutarOperacionINOperationCompleted(object arg) {
            if ((this.ejecutarOperacionINCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ejecutarOperacionINCompleted(this, new ejecutarOperacionINCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class consultarLineaINRequest {
        
        private string idTransaccionField;
        
        private string nombreAplicacionField;
        
        private string ipAplicacionField;
        
        private string msisdnField;
        
        private string inField;
        
        private string[] listaParametrosRequestField;
        
        /// <remarks/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <remarks/>
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                this.nombreAplicacionField = value;
            }
        }
        
        /// <remarks/>
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
            }
        }
        
        /// <remarks/>
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
            }
        }
        
        /// <remarks/>
        public string @in {
            get {
                return this.inField;
            }
            set {
                this.inField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("parametro", IsNullable=false)]
        public string[] listaParametrosRequest {
            get {
                return this.listaParametrosRequestField;
            }
            set {
                this.listaParametrosRequestField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ParametrosObjectType {
        
        private string parametroField;
        
        private string valorField;
        
        /// <remarks/>
        public string parametro {
            get {
                return this.parametroField;
            }
            set {
                this.parametroField = value;
            }
        }
        
        /// <remarks/>
        public string valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class consultarLineaINResponse {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        private string customeridField;
        
        private ParametrosObjectType[] listaParametrosResponseField;
        
        /// <remarks/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <remarks/>
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
            }
        }
        
        /// <remarks/>
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
            }
        }
        
        /// <remarks/>
        public string customerid {
            get {
                return this.customeridField;
            }
            set {
                this.customeridField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosResponse {
            get {
                return this.listaParametrosResponseField;
            }
            set {
                this.listaParametrosResponseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ejecutarOperacionINRequest {
        
        private string idTransaccionField;
        
        private string nombreAplicacionField;
        
        private string ipAplicacionField;
        
        private string msisdnField;
        
        private string operacionField;
        
        private string inField;
        
        private ParametrosObjectType[] listaParametrosRequestField;
        
        /// <remarks/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <remarks/>
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                this.nombreAplicacionField = value;
            }
        }
        
        /// <remarks/>
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
            }
        }
        
        /// <remarks/>
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
            }
        }
        
        /// <remarks/>
        public string operacion {
            get {
                return this.operacionField;
            }
            set {
                this.operacionField = value;
            }
        }
        
        /// <remarks/>
        public string @in {
            get {
                return this.inField;
            }
            set {
                this.inField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosRequest {
            get {
                return this.listaParametrosRequestField;
            }
            set {
                this.listaParametrosRequestField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ejecutarOperacionINResponse {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        private ParametrosObjectType[] listaParametrosResponseField;
        
        /// <remarks/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <remarks/>
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
            }
        }
        
        /// <remarks/>
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosResponse {
            get {
                return this.listaParametrosResponseField;
            }
            set {
                this.listaParametrosResponseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void consultarLineaINCompletedEventHandler(object sender, consultarLineaINCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultarLineaINCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal consultarLineaINCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public consultarLineaINResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((consultarLineaINResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void ejecutarOperacionINCompletedEventHandler(object sender, ejecutarOperacionINCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ejecutarOperacionINCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ejecutarOperacionINCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ejecutarOperacionINResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ejecutarOperacionINResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591