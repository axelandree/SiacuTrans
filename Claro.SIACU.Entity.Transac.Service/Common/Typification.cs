using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class Typification
    {
        [DataMember]
        [Data.DbColumn("TIPO")]
        public string TIPO { get; set; }

        [DataMember]
        [Data.DbColumn("CLASE")]
        public string CLASE { get; set; }

        [DataMember]
        [Data.DbColumn("SUBCLASE")]
        public string SUBCLASE { get; set; }

        [DataMember]
        [Data.DbColumn("INTERACCION_CODE")]
        public string INTERACCION_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("TIPO_CODE")]
        public string TIPO_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("CLASE_CODE")]
        public string CLASE_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("SUBCLASE_CODE")]
        public string SUBCLASE_CODE { get; set; }

        [DataMember]
        public string CODIGOEMPLEADO { get; set; }

        [DataMember]
        public string CODIGOSISTEMA { get; set; }

        [DataMember]
        public string FLAGCASO { get; set; }

        [DataMember]
        public string HECHOENUNO { get; set; }

        [DataMember]
        public string METODOCONTACTO { get; set; }

        [DataMember]
        public string NOTAS { get; set; }

        [DataMember]
        public string TELEFONO { get; set; }

        [DataMember]
        public string TEXTRESULTADO { get; set; }

        [DataMember]
        public string TIPOINTERACCION { get; set; }

        [DataMember]
        public string CUENTA { get; set; }
    }
}
