using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "MainParameters")]
    public class MainParameters
    {
        [DataMember]
        public string strType { get; set; }
        [DataMember]
        public string strClass { get; set; }
        [DataMember]
        public string strSubClass { get; set; }
        [DataMember]
        public string strContactMethod { get; set; }
        [DataMember]
        public string strInterType { get; set; }
        [DataMember]
        public string strAgent { get; set; }
        [DataMember]
        public string strUserProcess { get; set; }
        [DataMember]
        public string strMadeInOne { get; set; }
        [DataMember]
        public string strNotes { get; set; }
        [DataMember]
        public string strFlagCase { get; set; }
        [DataMember]
        public string strResult { get; set; }
        [DataMember]
        public string strServAfect { get; set; }
        [DataMember]
        public string strInconven { get; set; }
        [DataMember]
        public string strServAfectCode { get; set; }
        [DataMember]
        public string strInconvenCode { get; set; }
        [DataMember]
        public string strCoId { get; set; }
        [DataMember]
        public string strCodPlan { get; set; }
        [DataMember]
        public string strValueOne { get; set; }
        [DataMember]
        public string trValueTwo { get; set; }
    }
}
