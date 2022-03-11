using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class DbProcedureConfigurationElementCollection :
        ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DbProcedureConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DbProcedureConfigurationElement)element).Name;
        }

        new public DbProcedureConfigurationElement this[string Name]
        {
            get { return (DbProcedureConfigurationElement)BaseGet(Name); }
        }
    }
}
