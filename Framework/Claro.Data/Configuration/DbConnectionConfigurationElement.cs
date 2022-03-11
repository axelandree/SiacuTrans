using System;
using System.Configuration;
using System.Text;

namespace Claro.Data.Configuration
{
    internal sealed class DbConnectionConfigurationElement :
        ConfigurationElement
    {
        #region [ Fields ]

        private DbProvider m_provider;

        #endregion

        #region [ Protected Methods ]

        #region PostDeserialize

        protected override void PostDeserialize()
        {
            string strKey = this.RegistryKey,
                strUser = DataSection.RegistryReadDecryptValue(strKey, "User"),
                strPassword = DataSection.RegistryReadDecryptValue(strKey, "Password"),
                strServer = DataSection.RegistryReadValue(strKey, "Server"),
                strDatabase = DataSection.RegistryReadValue(strKey, "BD_Activa"),
                strProvider = DataSection.RegistryReadValue(strKey, "Provider").ToUpper();
            int indexProvider = strProvider.IndexOf("ORA",StringComparison.InvariantCulture);

            if (string.IsNullOrEmpty(strProvider) || indexProvider >= 0)
            {
                this.m_provider = DbProvider.Oracle;
            }
            else
            {
                this.m_provider = DbProvider.MSSQL;
            }

            this.User = strUser;
            this.Password = strPassword;
            this.Server = strServer;
            this.Database = strDatabase;

            base.PostDeserialize();
        }

        #endregion

        protected override bool IsModified()
        {
            this.m_connectionString = string.Empty;
            return base.IsModified();
        }

        #endregion

        #region [ Public Properties]
        private string m_connectionString;
        #region ConnectionString

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_connectionString))
                {
                    StringBuilder connectionString = new StringBuilder();

                    switch (this.Provider)
                    {
                        case DbProvider.MSSQL:
                            connectionString.AppendFormat("Server={0};", this.Server);
                            connectionString.AppendFormat("Database={0};", this.Database);
                            connectionString.AppendFormat("User={0};", this.User);
                            connectionString.AppendFormat("Password={0};", this.Password);

                            break;
                        case DbProvider.Oracle:
                            connectionString.AppendFormat("Data Source={0};", this.Database);
                            connectionString.AppendFormat("User Id={0};", this.User);
                            connectionString.AppendFormat("Password={0};", this.Password);

                            break;
                    }

                    connectionString.AppendFormat("Pooling={0};", this.Pooling);
                    connectionString.AppendFormat("Max Pool Size={0};", this.MaxPoolSize);
                    connectionString.AppendFormat("Min Pool Size={0};", this.MinPoolSize);
                    connectionString.AppendFormat("Connection Timeout={0};", this.Timeout);
                    connectionString.AppendFormat("Connection Lifetime={0};", this.LifeTime);

                    this.m_connectionString = connectionString.ToString();
                }

                return this.m_connectionString;
            }
        }

        #endregion

        #region Database

        [ConfigurationProperty("database", IsRequired = false)]
        public string Database
        {
            get
            {
                return this.GetDatabase();
            }
            set
            {
                this["database"] = value;
            }
        }

        private string GetDatabase()
        {
            string value = (string)this["database"];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Database not must be empty or null.");
            }

            return value;
        }

        #endregion

        #region LifeTime

        [ConfigurationProperty("lifeTime", DefaultValue = "0", IsRequired = false)]
        [IntegerValidator(ExcludeRange = true, MaxValue = -1)]
        public int LifeTime
        {
            get { return (int)this["lifeTime"]; }
            set { this["lifeTime"] = value; }
        }


        #endregion

        #region MaxPoolSize

        [ConfigurationProperty("maxPoolSize", DefaultValue = "100", IsRequired = false)]
        public int MaxPoolSize
        {
            get { return (int)this["maxPoolSize"]; }
            set { this["maxPoolSize"] = value; }
        }

        #endregion

        #region MinPoolSize

        [ConfigurationProperty("minPoolSize", DefaultValue = "1", IsRequired = false)]
        public int MinPoolSize
        {
            get { return (int)this["minPoolSize"]; }
            set { this["minPoolSize"] = value; }
        }

        #endregion

        #region Name

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        #endregion

        #region Password

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password
        {
            get
            {
                return this.GetPassword();
            }
            set
            {
                this["password"] = value;
            }
        }

        private string GetPassword()
        {
            string value = (string)this["password"];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Password not must be empty or null.");
            }

            return value;
        }

        #endregion

        #region Pooling

        [ConfigurationProperty("pooling", DefaultValue = "true", IsRequired = false)]
        public bool Pooling
        {
            get { return (bool)this["pooling"]; }
            set { this["pooling"] = value; }
        }

        #endregion

        #region Provider

        public DbProvider Provider
        {
            get
            {
                return this.m_provider;
            }
        }

        #endregion

        #region RegistryKey

        [ConfigurationProperty("registryKey", IsRequired = true)]
        public string RegistryKey
        {
            get { return (string)this["registryKey"]; }
            set
            {
                this["registryKey"] = value;
            }
        }

        #endregion

        #region Server

        [ConfigurationProperty("server", IsRequired = false)]
        public string Server
        {
            get
            {
                return this.GetServer();
            }
            set
            {
                this["server"] = value;
            }
        }

        private string GetServer()
        {
            string value = (string)this["server"];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Server not must be empty or null.");
            }

            return value;
        }

        #endregion

        #region Timeout

        [ConfigurationProperty("timeout", DefaultValue = "15", IsRequired = false)]
        [IntegerValidator(ExcludeRange = true, MaxValue = -1)]
        public int Timeout
        {
            get { return (int)this["timeout"]; }
            set { this["timeout"] = value; }
        }

        #endregion

        #region User

        [ConfigurationProperty("user", IsRequired = false)]
        public string User
        {
            get
            {
                return this.GetUser();
            }
            set
            {
                this["user"] = value;
            }
        }

        private string GetUser()
        {
            string value = (string)this["user"];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("User not must be empty or null.");
            }

            return value;
        }

        #endregion

        #endregion

    }
}
