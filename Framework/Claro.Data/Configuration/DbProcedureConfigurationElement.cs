using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class DbProcedureConfigurationElement :
        ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("schemaName", DefaultValue = "", IsRequired = false)]
        public string SchemaName
        {
            get { return (string)this["schemaName"]; }
            set { this["schemaName"] = value; }
        }

        [ConfigurationProperty("packageName", DefaultValue = "", IsRequired = false)]
        public string PackageName
        {
            get { return (string)this["packageName"]; }
            set { this["packageName"] = value; }
        }

        [ConfigurationProperty("procedureName", IsRequired = true)]
        public string ProcedureName
        {
            get { return (string)this["procedureName"]; }
            set { this["procedureName"] = value; }
        }

        [ConfigurationProperty("commandTimeout", DefaultValue = "30", IsRequired = false)]
        [IntegerValidator(ExcludeRange = true, MaxValue = -1)]
        public int CommandTimeout
        {
            get { return (int)this["commandTimeout"]; }
            set { this["commandTimeout"] = value; }
        }
    }
}
