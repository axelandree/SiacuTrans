using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "QueryOttResponse")]
    public class QueryOttResponse
    {        
        [DataMember(Name = "resultCode")]
        public string resultCode { get; set; }

        [DataMember(Name = "resultMessage")]
        public string resultMessage { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "packages")]
        public Package packages { get; set; }

        [DataMember(Name = "rentList")]
        public RentList rentList { get; set; }

        [DataMember(Name = "visualizationsList")]
        public VisualizationsList visualizationsList { get; set; }

        [DataMember(Name = "deviceList")]
        public DeviceList deviceList { get; set; }

        [DataMember(Name = "packagesList")]
        public PackagesList packagesList { get; set; }

        [DataMember(Name = "paymentMethodList")]
        public PaymentMethodList paymentMethodList { get; set; }

        [DataMember(Name = "subscriptions")]
        public Subscriptions subscriptions { get; set; }

        [DataMember(Name = "eventList")]
        public EventList eventList { get; set; }

        [DataMember(Name = "countryId")]
        public string countryId { get; set; }
        [DataMember(Name = "serviceName")]
        public string serviceName { get; set; }

        [DataMember(Name = "providerId")]
        public string providerId { get; set; }

        [DataMember(Name = "extensionInfo")]
        public List<ExtensionInfo> extensionInfo { get; set; }

    }
}
