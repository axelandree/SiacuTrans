using System;
using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class ServiceConfigurationElement :
        ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("bindingName", IsRequired = true)]
        public string BindingName
        {
            get { return (string)this["bindingName"]; }
            set { this["bindingName"] = value; }
        }

        [ConfigurationProperty("maxBufferPoolSize", DefaultValue = "524288", IsRequired = false)]
        [LongValidator(ExcludeRange = true, MaxValue = 0)]
        public long MaxBufferPoolSize
        {
            get { return (long)this["maxBufferPoolSize"]; }
            set { this["maxBufferPoolSize"] = value; }
        }

        [ConfigurationProperty("maxBufferSize", DefaultValue = "65536", IsRequired = false)]
        [IntegerValidator(ExcludeRange = true, MaxValue = 0)]
        public int MaxBufferSize
        {
            get { return (int)this["maxBufferSize"]; }
            set { this["maxBufferSize"] = value; }
        }

        [ConfigurationProperty("maxReceivedMessageSize", DefaultValue = "65536", IsRequired = false)]
        [LongValidator(ExcludeRange = true, MaxValue = 0)]
        public long MaxReceivedMessageSize
        {
            get { return (long)this["maxReceivedMessageSize"]; }
            set { this["maxReceivedMessageSize"] = value; }
        }


        [ConfigurationProperty("closeTimeout", DefaultValue = "00:00:30", IsRequired = false)]
        public TimeSpan CloseTimeout
        {
            get { return (TimeSpan)this["closeTimeout"]; }
            set { this["closeTimeout"] = value; }
        }

        [ConfigurationProperty("openTimeout", DefaultValue = "00:00:30", IsRequired = false)]
        public TimeSpan OpenTimeout
        {
            get { return (TimeSpan)this["openTimeout"]; }
            set { this["openTimeout"] = value; }
        }

        [ConfigurationProperty("receiveTimeout", DefaultValue = "00:00:30", IsRequired = false)]
        public TimeSpan ReceiveTimeout
        {
            get { return (TimeSpan)this["receiveTimeout"]; }
            set { this["receiveTimeout"] = value; }
        } 
        [ConfigurationProperty("sendTimeout", DefaultValue = "00:00:30", IsRequired = false)]
        public TimeSpan SendTimeout
        {
            get { return (TimeSpan)this["sendTimeout"]; }
            set { this["sendTimeout"] = value; }
        } 
        [ConfigurationProperty("clientCredentialType", DefaultValue = "", IsRequired = false)]
        public string ClientCredentialType
        {
            get { return (string)this["clientCredentialType"]; }
            set { this["clientCredentialType"] = value; }
        } 
    }
}
