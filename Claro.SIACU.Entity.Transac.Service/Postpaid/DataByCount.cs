using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("DataByCount")]
    public class DataByCount
    {
        [Data.DbColumn("CODIGO_AREA")]
        public string CODIGO_AREA { get; set; }
        [Data.DbColumn("CENTRO_COSTO")]
        public string CENTRO_COSTO { get; set; }
        [Data.DbColumn("CICLO")]
        public string CICLO { get; set; }
        [Data.DbColumn("LIMITE_CREDITO")]
        public string LIMITE_CREDITO { get; set; }
        [Data.DbColumn("CUENTA")]
        public string CUENTA { get; set; }
        [Data.DbColumn("CUENTA_PADRE")]
        public string CUENTA_PADRE { get; set; }
        [Data.DbColumn("CUSTOMER_PADRE")]
        public string CUSTOMER_PADRE { get; set; }
        [Data.DbColumn("NIVEL_CUENTA")]
        public string NIVEL_CUENTA { get; set; }
        [Data.DbColumn("REP_LEGAL")]
        public string REP_LEGAL { get; set; }
        [Data.DbColumn("RESP_PAGO")]
        public string RESP_PAGO { get; set; }
        [Data.DbColumn("ESTADO_CUENTA")]
        public string ESTADO_CUENTA { get; set; }
        [Data.DbColumn("TIPO_CLIENTE")]
        public string TIPO_CLIENTE { get; set; }
        [Data.DbColumn("PLAN_CUENTA")]
        public string PLAN_CUENTA { get; set; }
        [Data.DbColumn("COD_RAZON")]
        public string COD_RAZON { get; set; }
        [Data.DbColumn("FREC_FACT")]
        public string FREC_FACT { get; set; }
        [Data.DbColumn("COD_PROF")]
        public string COD_PROF { get; set; }
        [Data.DbColumn("TITULO")]
        public string TITULO { get; set; }
        [Data.DbColumn("APELLIDOS")]
        public string APELLIDOS { get; set; }
        [Data.DbColumn("NOMBRES")]
        public string NOMBRES { get; set; }
        [Data.DbColumn("ABREVIACION")]
        public string ABREVIACION { get; set; }
        [Data.DbColumn("RAZON_SOCIAL")]
        public string RAZON_SOCIAL { get; set; }
        [Data.DbColumn("FEC_NACIMIENTO")]
        public string FEC_NACIMIENTO { get; set; }
        [Data.DbColumn("DEPARTAMENTO")]
        public string DEPARTAMENTO { get; set; }
        [Data.DbColumn("PROVINCIA")]
        public string PROVINCIA { get; set; }
        [Data.DbColumn("COD_POSTAL")]
        public string COD_POSTAL { get; set; }
        [Data.DbColumn("DIRECCION")]
        public string DIRECCION { get; set; }
        [Data.DbColumn("REFERENCIA")]
        public string REFERENCIA { get; set; }
        [Data.DbColumn("DISTRITO")]
        public string DISTRITO { get; set; }
        [Data.DbColumn("TELF_CONTRACTO")]
        public string TELF_CONTRACTO { get; set; }
        [Data.DbColumn("TELF_CONT_2")]
        public string TELF_CONT_2 { get; set; }
        [Data.DbColumn("MAIL")]
        public string MAIL { get; set; }
        [Data.DbColumn("TIPO_DOC")]
        public string TIPO_DOC { get; set; }
        [Data.DbColumn("NRO_DOC")]
        public string NRO_DOC { get; set; }
        [Data.DbColumn("RUC")]
        public string RUC { get; set; }
        [Data.DbColumn("SEXO")]
        public string SEXO { get; set; }
        [Data.DbColumn("VAL_DIRECION")]
        public string VAL_DIRECION { get; set; }
        [Data.DbColumn("PAIS")]
        public string PAIS { get; set; }
        [Data.DbColumn("SEGMENTO")]
        public string SEGMENTO { get; set; }
        [Data.DbColumn("NACIONALIDAD")]
        public string NACIONALIDAD { get; set; }
        [Data.DbColumn("ROL")]
        public string ROL { get; set; }
        [Data.DbColumn("NICHO")]
        public string NICHO { get; set; }
        [Data.DbColumn("COD_UBIGEO")]
        public string COD_UBIGEO { get; set; }
        [Data.DbColumn("COD_PLANO")]
        public string COD_PLANO { get; set; }
        [Data.DbColumn("TIPO_INFO")]
        public string TIPO_INFO { get; set; }
    }
}
