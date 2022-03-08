using System.Diagnostics;
using System.IO;
using System.Text;

namespace Claro.Data
{
   public class MapDriveHelper
    {
       public bool Connect(string path, string username, string password, ref string response)
       {
           bool blnSuccess = false;

           StringBuilder sbResponse = new StringBuilder()
               , sbParameters = new StringBuilder("use");

           sbParameters.Append(" ");
           sbParameters.Append(path);
           sbParameters.Append(" ");
           sbParameters.Append(password);
           sbParameters.Append(" ");
           sbParameters.Append("/user:");
           sbParameters.Append(username);

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

           if (sbResponse.ToString() == "The command completed successfully.")
           {
               blnSuccess = true;
           }
           else
           {
               while (!srOutput.EndOfStream)
               {
                   sbResponse.Append(srOutput.ReadLine());
               }
           }

           objCommand.Close();

           response = sbResponse.ToString();

           return blnSuccess;
       }
    }
}
