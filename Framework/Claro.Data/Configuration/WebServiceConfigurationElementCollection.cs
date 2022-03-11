using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class WebServiceConfigurationElementCollection :
        ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WebServiceConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebServiceConfigurationElement)element).Name;
        }

        new public WebServiceConfigurationElement this[string Name]
        {
            get { return (WebServiceConfigurationElement)BaseGet(Name); }
        }
    }
}
