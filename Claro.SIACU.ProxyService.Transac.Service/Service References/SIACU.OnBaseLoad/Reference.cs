﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="auditRequest", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class auditRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idTransaccionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ipAplicacionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nombreAplicacionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usuarioAplicacionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                if ((object.ReferenceEquals(this.idTransaccionField, value) != true)) {
                    this.idTransaccionField = value;
                    this.RaisePropertyChanged("idTransaccion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                if ((object.ReferenceEquals(this.ipAplicacionField, value) != true)) {
                    this.ipAplicacionField = value;
                    this.RaisePropertyChanged("ipAplicacion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                if ((object.ReferenceEquals(this.nombreAplicacionField, value) != true)) {
                    this.nombreAplicacionField = value;
                    this.RaisePropertyChanged("nombreAplicacion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                if ((object.ReferenceEquals(this.usuarioAplicacionField, value) != true)) {
                    this.usuarioAplicacionField = value;
                    this.RaisePropertyChanged("usuarioAplicacion");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="parametrosRequest", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class parametrosRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.ArrayOfObjetoRequestOpcional ListaRequestOpcionalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.ArrayOfObjetoRequestOpcional ListaRequestOpcional {
            get {
                return this.ListaRequestOpcionalField;
            }
            set {
                if ((object.ReferenceEquals(this.ListaRequestOpcionalField, value) != true)) {
                    this.ListaRequestOpcionalField = value;
                    this.RaisePropertyChanged("ListaRequestOpcional");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfObjetoRequestOpcional", Namespace="http://tempuri.org/", ItemName="objetoRequestOpcional")]
    [System.SerializableAttribute()]
    public class ArrayOfObjetoRequestOpcional : System.Collections.Generic.List<Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.objetoRequestOpcional> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="objetoRequestOpcional", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class objetoRequestOpcional : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string campoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string valorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string campo {
            get {
                return this.campoField;
            }
            set {
                if ((object.ReferenceEquals(this.campoField, value) != true)) {
                    this.campoField = value;
                    this.RaisePropertyChanged("campo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string valor {
            get {
                return this.valorField;
            }
            set {
                if ((object.ReferenceEquals(this.valorField, value) != true)) {
                    this.valorField = value;
                    this.RaisePropertyChanged("valor");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentosResponse", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class DocumentosResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idTransaccionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idInterfazTCRMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ipServerResponseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string fechaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string horaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string listaDocumentosField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                if ((object.ReferenceEquals(this.idTransaccionField, value) != true)) {
                    this.idTransaccionField = value;
                    this.RaisePropertyChanged("idTransaccion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string idInterfazTCRM {
            get {
                return this.idInterfazTCRMField;
            }
            set {
                if ((object.ReferenceEquals(this.idInterfazTCRMField, value) != true)) {
                    this.idInterfazTCRMField = value;
                    this.RaisePropertyChanged("idInterfazTCRM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ipServerResponse {
            get {
                return this.ipServerResponseField;
            }
            set {
                if ((object.ReferenceEquals(this.ipServerResponseField, value) != true)) {
                    this.ipServerResponseField = value;
                    this.RaisePropertyChanged("ipServerResponse");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string fecha {
            get {
                return this.fechaField;
            }
            set {
                if ((object.ReferenceEquals(this.fechaField, value) != true)) {
                    this.fechaField = value;
                    this.RaisePropertyChanged("fecha");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string hora {
            get {
                return this.horaField;
            }
            set {
                if ((object.ReferenceEquals(this.horaField, value) != true)) {
                    this.horaField = value;
                    this.RaisePropertyChanged("hora");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string listaDocumentos {
            get {
                return this.listaDocumentosField;
            }
            set {
                if ((object.ReferenceEquals(this.listaDocumentosField, value) != true)) {
                    this.listaDocumentosField = value;
                    this.RaisePropertyChanged("listaDocumentos");
                }
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap")]
    public interface svcOnBaseClaroCargaSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento auditRequest del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CargarDocumentoOnBase", ReplyAction="*")]
        Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse CargarDocumentoOnBase(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CargarDocumentoOnBase", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse> CargarDocumentoOnBaseAsync(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CargarDocumentoOnBaseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CargarDocumentoOnBase", Namespace="http://tempuri.org/", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequestBody Body;
        
        public CargarDocumentoOnBaseRequest() {
        }
        
        public CargarDocumentoOnBaseRequest(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CargarDocumentoOnBaseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.auditRequest auditRequest;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string idInterfazTCRM;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string fecha;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string hora;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string listaDocumentos;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.parametrosRequest parametrosRequest;
        
        public CargarDocumentoOnBaseRequestBody() {
        }
        
        public CargarDocumentoOnBaseRequestBody(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.auditRequest auditRequest, string idInterfazTCRM, string usuario, string fecha, string hora, string listaDocumentos, Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.parametrosRequest parametrosRequest) {
            this.auditRequest = auditRequest;
            this.idInterfazTCRM = idInterfazTCRM;
            this.usuario = usuario;
            this.fecha = fecha;
            this.hora = hora;
            this.listaDocumentos = listaDocumentos;
            this.parametrosRequest = parametrosRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CargarDocumentoOnBaseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CargarDocumentoOnBaseResponse", Namespace="http://tempuri.org/", Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponseBody Body;
        
        public CargarDocumentoOnBaseResponse() {
        }
        
        public CargarDocumentoOnBaseResponse(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CargarDocumentoOnBaseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.DocumentosResponse CargarDocumentoOnBaseResult;
        
        public CargarDocumentoOnBaseResponseBody() {
        }
        
        public CargarDocumentoOnBaseResponseBody(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.DocumentosResponse CargarDocumentoOnBaseResult) {
            this.CargarDocumentoOnBaseResult = CargarDocumentoOnBaseResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface svcOnBaseClaroCargaSoapChannel : Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class svcOnBaseClaroCargaSoapClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap>, Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap {
        
        public svcOnBaseClaroCargaSoapClient() {
        }
        
        public svcOnBaseClaroCargaSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public svcOnBaseClaroCargaSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public svcOnBaseClaroCargaSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public svcOnBaseClaroCargaSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap.CargarDocumentoOnBase(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest request) {
            return base.Channel.CargarDocumentoOnBase(request);
        }
        
        public Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.DocumentosResponse CargarDocumentoOnBase(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.auditRequest auditRequest, string idInterfazTCRM, string usuario, string fecha, string hora, string listaDocumentos, Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.parametrosRequest parametrosRequest) {
            Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest inValue = new Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest();
            inValue.Body = new Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequestBody();
            inValue.Body.auditRequest = auditRequest;
            inValue.Body.idInterfazTCRM = idInterfazTCRM;
            inValue.Body.usuario = usuario;
            inValue.Body.fecha = fecha;
            inValue.Body.hora = hora;
            inValue.Body.listaDocumentos = listaDocumentos;
            inValue.Body.parametrosRequest = parametrosRequest;
            Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse retVal = ((Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap)(this)).CargarDocumentoOnBase(inValue);
            return retVal.Body.CargarDocumentoOnBaseResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse> Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap.CargarDocumentoOnBaseAsync(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest request) {
            return base.Channel.CargarDocumentoOnBaseAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseResponse> CargarDocumentoOnBaseAsync(Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.auditRequest auditRequest, string idInterfazTCRM, string usuario, string fecha, string hora, string listaDocumentos, Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.parametrosRequest parametrosRequest) {
            Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest inValue = new Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequest();
            inValue.Body = new Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.CargarDocumentoOnBaseRequestBody();
            inValue.Body.auditRequest = auditRequest;
            inValue.Body.idInterfazTCRM = idInterfazTCRM;
            inValue.Body.usuario = usuario;
            inValue.Body.fecha = fecha;
            inValue.Body.hora = hora;
            inValue.Body.listaDocumentos = listaDocumentos;
            inValue.Body.parametrosRequest = parametrosRequest;
            return ((Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad.svcOnBaseClaroCargaSoap)(this)).CargarDocumentoOnBaseAsync(inValue);
        }
    }
}