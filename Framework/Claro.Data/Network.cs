using Claro.Data.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Claro.Data
{
    public static class Network
    {
        public static string Connect(string strIdSession, string transaction, INetworkConfiguration objIDbConnectionConfiguration, string path)
        {
            NetworkConfigurationElement name = DataSettings.Networks[objIDbConnectionConfiguration.Name];
            string user = "TIM" + "\\" + DataSection.RegistryReadDecryptValue(name.RegistryKey, "User");

            string password;
            password = DataSection.RegistryReadDecryptValue(name.RegistryKey, "Password");


            StringBuilder sbResponse = new StringBuilder(), sbParameters = new StringBuilder("use");

            sbParameters.Append(" ");
            sbParameters.Append(path);
            sbParameters.Append(" ");
            sbParameters.Append(password);
            sbParameters.Append(" ");
            sbParameters.Append("/user:");
            sbParameters.Append(user);
            Process objCommand = new Process();
            objCommand.StartInfo = new ProcessStartInfo("net", sbParameters.ToString())
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            objCommand.Start();
            StreamReader srOutput = objCommand.StandardOutput;
            sbResponse.Append(srOutput.ReadLine());
            Claro.Web.Logging.Info(strIdSession, transaction, "respuesta del servidor al conectar: " + sbResponse.ToString());
            if (sbResponse.ToString() != "The command completed successfully." && sbResponse.ToString().IndexOf("Se ha completado el comando correctamente", System.StringComparison.InvariantCulture) == -1)
            {

                while (!srOutput.EndOfStream)
                {
                    sbResponse.Append(srOutput.ReadLine());
                }
                throw new ArgumentException("No se pudo conectar al servidor de facturas electrónicas");
            }

            objCommand.Close();

            return path;
        }



    }
}
