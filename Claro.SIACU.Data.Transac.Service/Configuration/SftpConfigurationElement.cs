using System.Configuration;

namespace Claro.SIACU.Data.Transac.Service.Configuration
{
    internal sealed class SftpConfigurationElement : 
         ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("registryKey", IsRequired = true)]
        public string RegistryKey
        {
            get { return (string)this["registryKey"]; }
            set { this["registryKey"] = value; }
        }

        [ConfigurationProperty("server", IsRequired = false)]
        public string server
        {
            get { return (string)this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("path_Destination", IsRequired = false)]
        public string path_Destination
        {
            get { return (string)this["path_Destination"]; }
            set { this["path_Destination"] = value; }
        }


        [ConfigurationProperty("port", IsRequired = true)]
        public string port
        {
            get { return (string)this["port"]; }
            set { this["port"] = value; }
        }

    }
}
