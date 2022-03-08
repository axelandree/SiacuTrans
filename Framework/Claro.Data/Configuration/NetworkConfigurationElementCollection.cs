using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class NetworkConfigurationElementCollection :
        ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new NetworkConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NetworkConfigurationElement)element).Name;
        }

        new public NetworkConfigurationElement this[string Name]
        {
            get { return (NetworkConfigurationElement)BaseGet(Name); }
        }
    }
}
