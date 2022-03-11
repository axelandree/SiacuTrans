﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi", ConfigurationName="SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType")]
    public interface CambioPlanPrepagoTFIPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación ejecutarCambioPlanPrepago no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi/ejecutarCambioPlanPrepa" +
            "go", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse1 ejecutarCambioPlanPrepago(Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi/types")]
    public partial class ejecutarCambioPlanPrepagoRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string telefonoField;
        
        private string offerField;
        
        private string subscriberStatusField;
        
        private parametrosTypeObjetoOpcional[] listaRequestOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditRequestType auditRequest {
            get {
                return this.auditRequestField;
            }
            set {
                this.auditRequestField = value;
                this.RaisePropertyChanged("auditRequest");
            }
        }
        
        /// <comentarios/>
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
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string offer {
            get {
                return this.offerField;
            }
            set {
                this.offerField = value;
                this.RaisePropertyChanged("offer");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string subscriberStatus {
            get {
                return this.subscriberStatusField;
            }
            set {
                this.subscriberStatusField = value;
                this.RaisePropertyChanged("subscriberStatus");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public parametrosTypeObjetoOpcional[] listaRequestOpcional {
            get {
                return this.listaRequestOpcionalField;
            }
            set {
                this.listaRequestOpcionalField = value;
                this.RaisePropertyChanged("listaRequestOpcional");
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string nombreAplicacionField;
        
        private string usuarioAplicacionField;
        
        /// <comentarios/>
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
        
        /// <comentarios/>
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
        
        /// <comentarios/>
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
        
        /// <comentarios/>
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class DefaultServiceResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idRespuesta {
            get {
                return this.idRespuestaField;
            }
            set {
                this.idRespuestaField = value;
                this.RaisePropertyChanged("idRespuesta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <comentarios/>
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
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
                this.RaisePropertyChanged("codigoRespuesta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class parametrosTypeObjetoOpcional : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string campoField;
        
        private string valorField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string campo {
            get {
                return this.campoField;
            }
            set {
                this.campoField = value;
                this.RaisePropertyChanged("campo");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi/types")]
    public partial class ejecutarCambioPlanPrepagoResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private string offerAntiguoField;
        
        private string offerNuevoField;
        
        private DefaultServiceResponse defaultServiceResponseField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditResponseType auditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
                this.RaisePropertyChanged("auditResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string offerAntiguo {
            get {
                return this.offerAntiguoField;
            }
            set {
                this.offerAntiguoField = value;
                this.RaisePropertyChanged("offerAntiguo");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string offerNuevo {
            get {
                return this.offerNuevoField;
            }
            set {
                this.offerNuevoField = value;
                this.RaisePropertyChanged("offerNuevo");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public DefaultServiceResponse defaultServiceResponse {
            get {
                return this.defaultServiceResponseField;
            }
            set {
                this.defaultServiceResponseField = value;
                this.RaisePropertyChanged("defaultServiceResponse");
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
    public partial class ejecutarCambioPlanPrepagoRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi/types", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest ejecutarCambioPlanPrepagoRequest;
        
        public ejecutarCambioPlanPrepagoRequest1() {
        }
        
        public ejecutarCambioPlanPrepagoRequest1(Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest ejecutarCambioPlanPrepagoRequest) {
            this.ejecutarCambioPlanPrepagoRequest = ejecutarCambioPlanPrepagoRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ejecutarCambioPlanPrepagoResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/cambioplanprepagotfi/types", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse ejecutarCambioPlanPrepagoResponse;
        
        public ejecutarCambioPlanPrepagoResponse1() {
        }
        
        public ejecutarCambioPlanPrepagoResponse1(Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse ejecutarCambioPlanPrepagoResponse) {
            this.ejecutarCambioPlanPrepagoResponse = ejecutarCambioPlanPrepagoResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CambioPlanPrepagoTFIPortTypeChannel : Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CambioPlanPrepagoTFIPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType>, Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType {
        
        public CambioPlanPrepagoTFIPortTypeClient() {
        }
        
        public CambioPlanPrepagoTFIPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CambioPlanPrepagoTFIPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CambioPlanPrepagoTFIPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CambioPlanPrepagoTFIPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse1 Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType.ejecutarCambioPlanPrepago(Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest1 request) {
            return base.Channel.ejecutarCambioPlanPrepago(request);
        }
        
        public Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse ejecutarCambioPlanPrepago(Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest ejecutarCambioPlanPrepagoRequest) {
            Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest1 inValue = new Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoRequest1();
            inValue.ejecutarCambioPlanPrepagoRequest = ejecutarCambioPlanPrepagoRequest;
            Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.ejecutarCambioPlanPrepagoResponse1 retVal = ((Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI.CambioPlanPrepagoTFIPortType)(this)).ejecutarCambioPlanPrepago(inValue);
            return retVal.ejecutarCambioPlanPrepagoResponse;
        }
    }
}
