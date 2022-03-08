﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.Transac.Service.WSDwh {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://pe/com/claro/esb/services/dwh/ws", ConfigurationName="WSDwh.EsbDwh")]
    public interface EsbDwh {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el espacio de nombres de partes de mensaje (http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd) no coincide con el valor predeterminado (http://pe/com/claro/esb/services/dwh/ws).
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ImeiResponse")]
        Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse buscarIMEIs(Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse> buscarIMEIsAsync(Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
    public partial class ImeiRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroTelefonoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string numeroTelefono {
            get {
                return this.numeroTelefonoField;
            }
            set {
                this.numeroTelefonoField = value;
                this.RaisePropertyChanged("numeroTelefono");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
    public partial class ImeiType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string imeiField;
        
        private string telefonoField;
        
        private string fechaInicioField;
        
        private string fechaFinField;
        
        private string descripcionField;
        
        private string modeloField;
        
        private string marcaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string imei {
            get {
                return this.imeiField;
            }
            set {
                this.imeiField = value;
                this.RaisePropertyChanged("imei");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
                this.RaisePropertyChanged("telefono");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string fechaInicio {
            get {
                return this.fechaInicioField;
            }
            set {
                this.fechaInicioField = value;
                this.RaisePropertyChanged("fechaInicio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string fechaFin {
            get {
                return this.fechaFinField;
            }
            set {
                this.fechaFinField = value;
                this.RaisePropertyChanged("fechaFin");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
                this.RaisePropertyChanged("descripcion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string modelo {
            get {
                return this.modeloField;
            }
            set {
                this.modeloField = value;
                this.RaisePropertyChanged("modelo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string marca {
            get {
                return this.marcaField;
            }
            set {
                this.marcaField = value;
                this.RaisePropertyChanged("marca");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
    public partial class ImeiResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string respuestaField;
        
        private string mensajeField;
        
        private ImeiResponseImeiLista imeiListaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string respuesta {
            get {
                return this.respuestaField;
            }
            set {
                this.respuestaField = value;
                this.RaisePropertyChanged("respuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string mensaje {
            get {
                return this.mensajeField;
            }
            set {
                this.mensajeField = value;
                this.RaisePropertyChanged("mensaje");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public ImeiResponseImeiLista imeiLista {
            get {
                return this.imeiListaField;
            }
            set {
                this.imeiListaField = value;
                this.RaisePropertyChanged("imeiLista");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
    public partial class ImeiResponseImeiLista : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ImeiType[] imeiItemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("imeiItem", Order=0)]
        public ImeiType[] imeiItem {
            get {
                return this.imeiItemField;
            }
            set {
                this.imeiItemField = value;
                this.RaisePropertyChanged("imeiItem");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName="buscarIMEIs", WrapperNamespace="http://pe/com/claro/esb/services/dwh/ws", IsWrapped=true)]
    public partial class buscarIMEIsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
        public Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiRequest ImeiRequest;
        
        public buscarIMEIsRequest() {
        }
        
        public buscarIMEIsRequest(Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiRequest ImeiRequest) {
            this.ImeiRequest = ImeiRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="buscarIMEIsResponse", WrapperNamespace="http://pe/com/claro/esb/services/dwh/ws", IsWrapped=true)]
    public partial class buscarIMEIsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://pe/com/claro/esb/services/dwh/schemas/imei/DwhImei.xsd")]
        public Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiResponse ImeiResponse;
        
        public buscarIMEIsResponse() {
        }
        
        public buscarIMEIsResponse(Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiResponse ImeiResponse) {
            this.ImeiResponse = ImeiResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface EsbDwhChannel : Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EsbDwhClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh>, Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh {
        
        public EsbDwhClient() {
        }
        
        public EsbDwhClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EsbDwhClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EsbDwhClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EsbDwhClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh.buscarIMEIs(Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest request) {
            return base.Channel.buscarIMEIs(request);
        }
        
        public Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiResponse buscarIMEIs(Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiRequest ImeiRequest) {
            Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest inValue = new Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest();
            inValue.ImeiRequest = ImeiRequest;
            Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse retVal = ((Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh)(this)).buscarIMEIs(inValue);
            return retVal.ImeiResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse> Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh.buscarIMEIsAsync(Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest request) {
            return base.Channel.buscarIMEIsAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsResponse> buscarIMEIsAsync(Claro.SIACU.ProxyService.Transac.Service.WSDwh.ImeiRequest ImeiRequest) {
            Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest inValue = new Claro.SIACU.ProxyService.Transac.Service.WSDwh.buscarIMEIsRequest();
            inValue.ImeiRequest = ImeiRequest;
            return ((Claro.SIACU.ProxyService.Transac.Service.WSDwh.EsbDwh)(this)).buscarIMEIsAsync(inValue);
        }
    }
}
