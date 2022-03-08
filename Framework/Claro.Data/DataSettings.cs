using Claro.Data.Configuration;
using System.Configuration;

namespace Claro.Data
{
    internal static class DataSettings
    {
        private static volatile DataSection m_dataSection = (DataSection)ConfigurationManager.GetSection("claro.data");

        public static ServiceConfigurationElementCollection Services
        {
            get
            {
                return m_dataSection.Services;
            }
        }

        public static WebServiceConfigurationElementCollection WebServices
        {
            get
            {
                return m_dataSection.WebServices;
            }
        }

        public static DbConnectionConfigurationElementCollection DataBases
        {
            get
            {
                return m_dataSection.Databases;
            }
        }

      
        public static DbProcedureConfigurationElementCollection Procedures
        {
            get
            {
                return m_dataSection.Procedures;
            }
        }

        public static NetworkConfigurationElementCollection Networks 
        {
            get
            {
                return m_dataSection.Networks;
            }
        }

        public static FtpConfigurationElementCollection Ftp 
        {
            get
            {
                return m_dataSection.Ftp; 
            }
        }

        public static RestServiceConfigurationElementCollection RestServices
        {
            get
            {
                return m_dataSection.RestServices;
            }
        }
    }
}
