using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class FtpConfigurationElement : ConfigurationElement
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

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
    }
}
