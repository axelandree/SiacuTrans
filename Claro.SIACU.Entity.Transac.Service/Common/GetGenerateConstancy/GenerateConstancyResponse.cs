using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetGenerateConstancy
{
    [DataContract(Name = "GenerateConstancyResponseCommon")]
    public class GenerateConstancyResponse
    {
        [DataMember]
        public bool Generated { get; set; }
        /// <summary>
        /// Mensaje de texto codificago en byte
        /// </summary>
        [DataMember]
        public byte[] EngineMessage { get; set; } 
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string FullPathPDF { get; set; }
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Document { get; set; }
        [DataMember]
        public byte[] Drive { get; set; }
        [DataMember]
        public byte[] bytesConstancy { get; set; } 
    }
}
