using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetSavePostventa
{
    
        [DataContract]
        public class DataSavePostventaDetServRequest : Claro.Entity.Request
        {

            [DataMember]
            public string striInteractionId { get; set; }
            [DataMember]
            public string strServiceId { get; set; }
            [DataMember]
            public string strIGroupPrincipalId { get; set; }
            [DataMember]
            public string strIdGroupId { get; set; }
            [DataMember]
            public string strQuantity_Intance_id { get; set; } 
            [DataMember]
            public string strDscsrv_Id { get; set; }
            [DataMember]
            public string strBandwid_Id { get; set; }
            [DataMember]
            public string strFlag_lc_Id { get; set; }
            [DataMember]
            public string strQuantity_idline_Id { get; set; }
            [DataMember]
            public string strTipequ_Id { get; set; } 
            [DataMember]
            public string strCodigoTipequ_id { get; set; }
            [DataMember]
            public string strQuantity_Id { get; set; }
            [DataMember]
            public string strDscequ_Id { get; set; }
            [DataMember]
            public string strCodigo_ext_Id { get; set; }
            [DataMember]
            public string strSncode_Id { get; set; } 
            [DataMember]
            public string strSpcode_Id { get; set; }
            [DataMember]
            public string strFlag_Id { get; set; }
        }
    
}


