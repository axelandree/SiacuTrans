﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws", ConfigurationName="SIACFixed.ValidarLineasCliente.validarLineasClientePortType")]
    public interface validarLineasClientePortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación contarLineas no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ValidarLineasCliente/ws/contarLineas", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1 contarLineas(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ValidarLineasCliente/ws/contarLineas", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1> contarLineasAsync(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class contarLineasRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private AuditRequest auditRequestField;
        
        private string numeroDocumentoField;
        
        private ListaCamposAdicionalesTypeCampoAdicional[] listaCamposAdicionalesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public AuditRequest auditRequest {
            get {
                return this.auditRequestField;
            }
            set {
                this.auditRequestField = value;
                this.RaisePropertyChanged("auditRequest");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string numeroDocumento {
            get {
                return this.numeroDocumentoField;
            }
            set {
                this.numeroDocumentoField = value;
                this.RaisePropertyChanged("numeroDocumento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("campoAdicional", IsNullable=false)]
        public ListaCamposAdicionalesTypeCampoAdicional[] listaCamposAdicionales {
            get {
                return this.listaCamposAdicionalesField;
            }
            set {
                this.listaCamposAdicionalesField = value;
                this.RaisePropertyChanged("listaCamposAdicionales");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class AuditRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string nombreAplicacionField;
        
        private string usuarioAplicacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
                this.RaisePropertyChanged("ipAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                this.nombreAplicacionField = value;
                this.RaisePropertyChanged("nombreAplicacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                this.usuarioAplicacionField = value;
                this.RaisePropertyChanged("usuarioAplicacion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class AuditResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codRespuestaField;
        
        private string msjRespuestaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codRespuesta {
            get {
                return this.codRespuestaField;
            }
            set {
                this.codRespuestaField = value;
                this.RaisePropertyChanged("codRespuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string msjRespuesta {
            get {
                return this.msjRespuestaField;
            }
            set {
                this.msjRespuestaField = value;
                this.RaisePropertyChanged("msjRespuesta");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class ListaCamposAdicionalesTypeCampoAdicional : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string nombreCampoField;
        
        private string valorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string nombreCampo {
            get {
                return this.nombreCampoField;
            }
            set {
                this.nombreCampoField = value;
                this.RaisePropertyChanged("nombreCampo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
                this.RaisePropertyChanged("valor");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class contarLineasResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private AuditResponse auditResponseField;
        
        private ListaCamposAdicionalesTypeCampoAdicional[] listaCamposAdicionalesField;
        
        private string cantidadLineasActivasField;
        
        private string cantidadLineasActivasPorDiaField;
        
        private ListaLineasConsolidadasTypeLineaConsolidada[] listaLineasConsolidadasTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public AuditResponse auditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
                this.RaisePropertyChanged("auditResponse");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("campoAdicional", IsNullable=false)]
        public ListaCamposAdicionalesTypeCampoAdicional[] listaCamposAdicionales {
            get {
                return this.listaCamposAdicionalesField;
            }
            set {
                this.listaCamposAdicionalesField = value;
                this.RaisePropertyChanged("listaCamposAdicionales");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string cantidadLineasActivas {
            get {
                return this.cantidadLineasActivasField;
            }
            set {
                this.cantidadLineasActivasField = value;
                this.RaisePropertyChanged("cantidadLineasActivas");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string cantidadLineasActivasPorDia {
            get {
                return this.cantidadLineasActivasPorDiaField;
            }
            set {
                this.cantidadLineasActivasPorDiaField = value;
                this.RaisePropertyChanged("cantidadLineasActivasPorDia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("lineaConsolidada", IsNullable=false)]
        public ListaLineasConsolidadasTypeLineaConsolidada[] listaLineasConsolidadasType {
            get {
                return this.listaLineasConsolidadasTypeField;
            }
            set {
                this.listaLineasConsolidadasTypeField = value;
                this.RaisePropertyChanged("listaLineasConsolidadasType");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2558.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types")]
    public partial class ListaLineasConsolidadasTypeLineaConsolidada : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msisdnField;
        
        private string segmentoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string segmento {
            get {
                return this.segmentoField;
            }
            set {
                this.segmentoField = value;
                this.RaisePropertyChanged("segmento");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class contarLineasRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest contarLineasRequest;
        
        public contarLineasRequest1() {
        }
        
        public contarLineasRequest1(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest contarLineasRequest) {
            this.contarLineasRequest = contarLineasRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class contarLineasResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ValidarLineasCliente/ws/types", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse contarLineasResponse;
        
        public contarLineasResponse1() {
        }
        
        public contarLineasResponse1(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse contarLineasResponse) {
            this.contarLineasResponse = contarLineasResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface validarLineasClientePortTypeChannel : Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class validarLineasClientePortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType>, Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType {
        
        public validarLineasClientePortTypeClient() {
        }
        
        public validarLineasClientePortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public validarLineasClientePortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public validarLineasClientePortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public validarLineasClientePortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1 Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType.contarLineas(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 request) {
            return base.Channel.contarLineas(request);
        }
        
        public Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse contarLineas(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest contarLineasRequest) {
            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 inValue = new Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1();
            inValue.contarLineasRequest = contarLineasRequest;
            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1 retVal = ((Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType)(this)).contarLineas(inValue);
            return retVal.contarLineasResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1> Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType.contarLineasAsync(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 request) {
            return base.Channel.contarLineasAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse1> contarLineasAsync(Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest contarLineasRequest) {
            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1 inValue = new Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest1();
            inValue.contarLineasRequest = contarLineasRequest;
            return ((Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.validarLineasClientePortType)(this)).contarLineasAsync(inValue);
        }
    }
}