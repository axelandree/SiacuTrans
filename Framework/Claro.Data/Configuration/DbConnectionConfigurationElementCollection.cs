using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class DbConnectionConfigurationElementCollection :
        ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DbConnectionConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DbConnectionConfigurationElement)element).Name;
        }

        new public DbConnectionConfigurationElement this[string Name]
        {
            get { return (DbConnectionConfigurationElement)BaseGet(Name); }
        }
    }
}
