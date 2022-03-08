using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetStatusAccountAOC
{
    [DataContract(Name = "StatusAccountAOCPostPaid")]
    public class StatusAccountAOC
    {
        [DataMember]
        public int CorrelativoId { get; set; }


        [Data.DbColumn("TIPO")]
        [DataMember]
        public string Tipo { get; set; }


        [Data.DbColumn("DOCUMENTO")]
        [DataMember]
        public string Documento { get; set; }


        [Data.DbColumn("DESCRIPCION_PAGO")]
        [DataMember]
        public string DescripcionPago { get; set; }


        [Data.DbColumn("FECHA_REGISTRO")]
        [DataMember]
        public string FechaRegistro { get; set; }


        [Data.DbColumn("FECHA_EMISION")]
        [DataMember]
        public string FechaEmision { get; set; }


        [Data.DbColumn("FECHA_VENCIMIENTO")]
        [DataMember]
        public string FechaVencimiento { get; set; }


        [Data.DbColumn("CARGO")]
        [DataMember]
        public string Cargo { get; set; }



        [Data.DbColumn("ABONO")]
        [DataMember]
        public string Abono { get; set; }



        [Data.DbColumn("IMPORTE_PENDIENTE")]
        [DataMember]
        public string ImportePendiente { get; set; }



        [Data.DbColumn("USUARIO")]
        [DataMember]
        public string Usuario { get; set; }


        [Data.DbColumn("MONTO_RECLAMO")]
        [DataMember]
        public string MontoReclamo { get; set; }

        [Data.DbColumn("DESCRIPCION_DETALLE")]
        [DataMember]
        public string DescripcionDetalle { get; set; }



        [Data.DbColumn("FECHA")]
        [DataMember]
        public string Fecha { get; set; }

        [DataMember]
        public string Saldo { get; set; }


    }
}
