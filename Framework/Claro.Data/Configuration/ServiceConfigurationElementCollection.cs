using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class ServiceConfigurationElementCollection :
        ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceConfigurationElement)element).Name;
        }

        new public ServiceConfigurationElement this[string Name]
        {
            get { return (ServiceConfigurationElement)BaseGet(Name); }
        }
    }
}
