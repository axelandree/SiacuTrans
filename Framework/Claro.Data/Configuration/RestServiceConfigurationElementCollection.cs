using System.Configuration;

namespace Claro.Data.Configuration
{
     internal sealed class RestServiceConfigurationElementCollection:
      ConfigurationElementCollection
     {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RestServiceConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RestServiceConfigurationElement)element).Name;
        }

        new public RestServiceConfigurationElement this[string Name]
        {
            get { return (RestServiceConfigurationElement)BaseGet(Name); }
        }
    }
}
