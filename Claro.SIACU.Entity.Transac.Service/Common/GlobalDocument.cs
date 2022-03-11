using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
 
    public class GlobalDocument
    {
        public string strCodeError { get; set; }
        public string  strMesaggeError { get; set; }
        public byte[] Document { get; set; }
    }
}
