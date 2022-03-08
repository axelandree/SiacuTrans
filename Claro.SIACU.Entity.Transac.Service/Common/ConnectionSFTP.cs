using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ConnectionSFTP
    {

        public string name { get; set; }
        public string server { get; set; }
        public string path_Destination { get; set; }
        public string registryKey { get; set; }
        public string port { get; set; }
        public string KeyUser { get; set; }
        public string KeyPassword { get; set; } 
        
    }
}
