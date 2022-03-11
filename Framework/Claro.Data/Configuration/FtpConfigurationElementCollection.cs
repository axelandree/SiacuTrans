using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class FtpConfigurationElementCollection :
        ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new FtpConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FtpConfigurationElement)element).Name;
        }

        new public FtpConfigurationElement this[string Name]
        {
            get { return (FtpConfigurationElement)BaseGet(Name); }
        }
    }
}
