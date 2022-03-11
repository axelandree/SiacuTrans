using Claro.Data.Configuration;
using Claro.Security.Cryptography;
using Microsoft.Win32;
using System.Configuration;
using System.Text;

namespace Claro.Data
{
    internal sealed class DataSection :
        ConfigurationSection
    {
        public static readonly DES Crypto = CreateCrypto();
        public const string TIMRootRegistry = @"SOFTWARE\TIM";

        #region CreateCryptographyObject

        public DataSection()
        {
            
        }

        #endregion

        #region CreateCrypto

        private static DES CreateCrypto()
        {
            string strSecretKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry).GetValue("key").ToString();

            DES crypto = new Security.Cryptography.DES(strSecretKey);

            return crypto;
        }

        #endregion

        #region RegistryReadValue

        public static string RegistryReadValue(string key, string value)
        {
            return  Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + key).GetValue(value, "").ToString();
        }

        #endregion

        #region RegistryReadDecryptValue

        public static string RegistryReadDecryptValue(string key, string value)
        {
            string strPassword = RegistryReadValue(key, value);

            byte[] ByteArray = Encoding.Default.GetBytes(strPassword);

            Crypto.DecryptByte(ref ByteArray);

            return Encoding.Default.GetString(ByteArray);
        }

        #endregion

        [ConfigurationProperty("services", IsRequired = false)]
        [ConfigurationCollection(typeof(ServiceConfigurationElement))]
        public ServiceConfigurationElementCollection Services
        {
            get
            {
                return (ServiceConfigurationElementCollection)this["services"];
            }
        }

        [ConfigurationProperty("webservices", IsRequired = false)]
        [ConfigurationCollection(typeof(WebServiceConfigurationElement))]
        public WebServiceConfigurationElementCollection WebServices
        {
            get
            {
                return (WebServiceConfigurationElementCollection)this["webservices"];
            }
        }

        [ConfigurationProperty("database.connections", IsRequired = false)]
        [ConfigurationCollection(typeof(DbConnectionConfigurationElement))]
        public DbConnectionConfigurationElementCollection Databases
        {
            get
            {
                return (DbConnectionConfigurationElementCollection)this["database.connections"];
            }
        }

        
        [ConfigurationProperty("database.procedures", IsRequired = false)]
        [ConfigurationCollection(typeof(DbProcedureConfigurationElement))]
        public DbProcedureConfigurationElementCollection Procedures
        {
            get
            {
                return (DbProcedureConfigurationElementCollection)this["database.procedures"];
            }
        }

        [ConfigurationProperty("networks", IsRequired = false)]
        [ConfigurationCollection(typeof(NetworkConfigurationElement))]
        public NetworkConfigurationElementCollection Networks 
        {
            get
            {
                return (NetworkConfigurationElementCollection)this["networks"];
            }
        }


        [ConfigurationProperty("ftp", IsRequired = false)]
        [ConfigurationCollection(typeof(FtpConfigurationElement))]
        public FtpConfigurationElementCollection Ftp 
        {
            get
            {
                return (FtpConfigurationElementCollection)this["ftp"];
            }
        }

        [ConfigurationProperty("restservices", IsRequired = false)]
        [ConfigurationCollection(typeof(RestServiceConfigurationElement))]
        public RestServiceConfigurationElementCollection RestServices
        {
            get
            {
                return (RestServiceConfigurationElementCollection)this["restservices"];
            }
        }
    }
}
