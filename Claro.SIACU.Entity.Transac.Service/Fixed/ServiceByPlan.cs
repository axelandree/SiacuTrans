using System;
using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "ServiceByPlan")]
    public class ServiceByPlan
    {
        [DbColumn("COD_PLAN_SISACT")]
        [DataMember]
        public string CodPlanSisact { get; set; }
        [DbColumn("DES_PLAN_SISACT")]
        [DataMember]
        public string DesPlanSisact { get; set; }
        [DbColumn("TMCODE")]
        [DataMember]
        public string Tmcode { get; set; }
        [DbColumn("SOLUCION")]
        [DataMember]
        public string Solution { get; set; }
        [DbColumn("COD_SERV_SISACT")]
        [DataMember]
        public string CodServSisact { get; set; }
        [DbColumn("SNCODE")]
        [DataMember]
        public string Sncode { get; set; }
        [DbColumn("SPCODE")]
        [DataMember]
        public string Spcode { get; set; }
        [DbColumn("COD_TIPO_SERVICIO")]
        [DataMember]
        public string CodServiceType { get; set; }
        [DbColumn("TIPO_SERVICIO")]
        [DataMember]
        public string ServiceType { get; set; }
        [DbColumn("DES_SERV_SISACT")]
        [DataMember]
        public string DesServSisact { get; set; }
        [DbColumn("COD_GRUPO_SERV")]
        [DataMember]
        public string CodGroupServ { get; set; }
        [DbColumn("GRUPO_SERV")]
        [DataMember]
        public string GroupServ { get; set; }
        [DbColumn("CF")]
        [DataMember]
        public string CF { get; set; }
        [DbColumn("IDEQUIPO")]
        [DataMember]
        public string IDEquipment { get; set; }
        [DbColumn("EQUIPO")]
        [DataMember]
        public string Equipment { get; set; }
        [DbColumn("CANT_EQUIPO")]
        [DataMember]
        public string CantEquipment { get; set; }
        //[DbColumn("MATV_ID_SAP")]
        //[DataMember]
        //public string MATV_ID_SAP { get; set; }
        [DbColumn("CODTIPEQU")]
        [DataMember]
        public string Codtipequ { get; set; }
        //[DbColumn("MATV_DES_SAP")]
        //[DataMember]
        //public string MATV_DES_SAP { get; set; }
        [DbColumn("DSCEQU")]
        [DataMember]
        public string Dscequ { get; set; }
        [DbColumn("TIPEQU")]
        [DataMember]
        public string Tipequ { get; set; }
        [DbColumn("COD_EXTERNO")]
        [DataMember]
        public string CodeExternal { get; set; }
        [DbColumn("DES_COD_EXTERNO")]
        [DataMember]
        public string DesCodeExternal { get; set; }
        [DbColumn("SERVV_USUARIO_CREA")]
        [DataMember]
        public string ServvUserCrea { get; set; }

        //ADD COLUMN CF WITH IGV 02-02-2018
        [DataMember]
        public string CfWithIgv { get; set; }
        [DataMember]
        public string CodPrincipalGroup { get; set; }
        [DataMember]
        public string IdLineQuantity { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string CodEquipmentGroup { get; set; }
        
    }
}
