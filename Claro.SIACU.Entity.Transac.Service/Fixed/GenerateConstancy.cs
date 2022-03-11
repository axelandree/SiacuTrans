using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class GenerateConstancy
    {
        [DataMember]
        public string Driver { get; set; }

        [DataMember]
        public string Directory { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string ServerReadPdf { get; set; }

        [DataMember]
        public string FolderPdf { get; set; }
    }
}

