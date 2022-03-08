using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class InteractionHFC : Claro.Entity.Request
    {
        public InteractionHFC()
        {
            
        }
        [DataMember]
        public string ObjidContacto { get; set; }
         [DataMember]
        public string ObjidSite { get; set; }
                [DataMember]
        public string Cuenta { get; set; }
                [DataMember]
        public string IdInteractio { get; set; }
                [DataMember]
        public string DateCreaction { get; set; }
                [DataMember]
        public string StartDate { get; set; }
                [DataMember]
        public string Telephone { get; set; }
                [DataMember]
        public string Type { get; set; }
                [DataMember]
                public string Class { get; set; }
                [DataMember]
                public string SubClass { get; set; }
                [DataMember]
                public string Tipification { get; set; }
                [DataMember]
                public string TypeCode { get; set; }
                [DataMember]
                public string ClassCode { get; set; }
                [DataMember]
                public string SubClassCode { get; set; }
                [DataMember]
                public string InsertPor { get; set; }
                [DataMember]
                public string TypeInter { get; set; }
                [DataMember]
                public string Method { get; set; }
                [DataMember]
                public string Result { get; set; }
                [DataMember]
                public string MadeOne { get; set; }
                [DataMember]
                public string Agenth { get; set; }
                [DataMember]
                public string NameAgenth { get; set; }
                [DataMember]
                public string ApellidoAgenth { get; set; }
                [DataMember]
                public string IdCase { get; set; }
                [DataMember]
                public string Note { get; set; }
                [DataMember]
                public string FlagCase { get; set; }
                [DataMember]
                public string UserProces { get; set; }
                [DataMember]
                public string Service { get; set; }
                [DataMember]
                public string Inconveniente { get; set; }
                [DataMember]
                public string ServiceCode { get; set; }
                [DataMember]
                public string InconvenienteCode { get; set; }
                [DataMember]
                public string Contract { get; set; }
                [DataMember]
                public string Plan { get; set; }
                [DataMember]
                public string Value1 { get; set; }
                [DataMember]
                public string Value2 { get; set; }
                [DataMember]
        public string InteractionCode { get; set; }
                public string Cola { get; set; }
                [DataMember]
                public string Prioridad { get; set; }
                [DataMember]
                public string Severity { get; set; }
                [DataMember]
                public string Method_Contact { get; set; }
                [DataMember]
                public string Type_Interaction { get; set; }
                [DataMember]
                public string User_Id { get; set; }
                [DataMember]
        public string FlatInteraccion { get; set; }       


    }
}
