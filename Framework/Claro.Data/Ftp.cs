using Claro.Data.Configuration;
using System;
using System.Net;

namespace Claro.Data
{
    public static class Ftp
    {
        public static byte[] ConnectFtp(string transaction, IFtpConfiguration objIDbConnectionConfiguration, string filePath, string fileName)
        {

            FtpConfigurationElement name = DataSettings.Ftp[objIDbConnectionConfiguration.Name];

            string user = DataSection.RegistryReadDecryptValue(name.RegistryKey, "User");
            string password = DataSection.RegistryReadDecryptValue(name.RegistryKey, "Password");
            byte[] newFileData = null;

            WebClient request = new WebClient();

            request.Credentials = new NetworkCredential(user, password);

            try
            {
                newFileData = request.DownloadData(new Uri(filePath) + fileName);
                request.Dispose();
            }
            catch (WebException ex)
            {
                Claro.Web.Logging.Error("1", transaction, "error ConnectFtp: " + ex);
                throw new ArgumentException("No se pudo conectar al servidor FTP de facturas electrónicas -" + ex);
            }

            return newFileData;
        }
    }
}
