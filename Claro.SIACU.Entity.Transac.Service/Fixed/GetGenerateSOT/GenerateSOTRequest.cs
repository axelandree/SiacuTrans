using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateSOT
{
    [DataContract(Name = "GenerateSOTRequestFixed")]
    public class GenerateSOTRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCusID { get; set; }
        [DataMember]
        public string vCoID { get; set; }
        [DataMember]
        public int vTipTra { get; set; }
        [DataMember]
        public string vFeProg { get; set; }
        [DataMember]
        public string vFranja { get; set; }
        [DataMember]
        public string vCodMotivo { get; set; }

        [DataMember]
        public string vObserv { get; set; }
        [DataMember]
        public string vPlano { get; set; }
        [DataMember]
        public string vUser { get; set; }

        [DataMember]
        public string idTipoServ { get; set; }
  
        //--------------------------------------------------------------

        [DataMember]
        public object Servicios { get; set; }
        [DataMember]
        public string ConID { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string TransTipo { get; set; }
        [DataMember]
        public string InterCasoID { get; set; }
        [DataMember]
        public int MotivoID { get; set; }
        [DataMember]
        public string TrabajoID { get; set; }
        [DataMember]
        public string TipoVia { get; set; }
        [DataMember]
        public string NomVia { get; set; }
        [DataMember]
        public int NroVia { get; set; }
        [DataMember]
        public string TipMz { get; set; }
        [DataMember]
        public string NumMZ { get; set; }
        [DataMember]
        public string NumLote { get; set; }
        [DataMember]
        public string TipoUrb { get; set; }
        [DataMember]
        public string NomUrb { get; set; }
        [DataMember]
        public string Ubigeo { get; set; }
        [DataMember]
        public string ZonaID { get; set; }
        [DataMember]
        public string PlanoID { get; set; }
        [DataMember]
        public string EdificioID { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public string FranjaHora { get; set; }
        [DataMember]
        public DateTime FechaProgramada { get; set; }
        [DataMember]
        public string NumCarta { get; set; }
        [DataMember]
        public string Operador { get; set; }
        [DataMember]
        public string Presuscrito { get; set; }
        [DataMember]
        public string Publicar { get; set; }
        [DataMember]
        public string ListaEquipos { get; set; }
        [DataMember]
        public string TmCode { get; set; }
        [DataMember]
        public string ListaCoSer { get; set; }
        [DataMember]
        public string ListaSNCode { get; set; }
        [DataMember]
        public string ListaSPCode { get; set; }
        [DataMember]
        public string USRREGIS { get; set; }
        [DataMember]
        public string Cargo { get; set; }


        //-----------
        [DataMember]
        public string strIdSession { get; set; }
        [DataMember]
        public bool chkUseChangeBillingChecked { get; set; }
        [DataMember]
        public bool chkEmailChecked { get; set; }

        [DataMember]
        public string strtypetransaction { get; set; }

        [DataMember]
        public bool chkSN { get; set; }

        [DataMember]
        public bool txtNumDir { get; set; }

        [DataMember]
        public string txtNotText { get; set; }

        [DataMember]
        public string hdnCodigoRequestAct { get; set; }

        [DataMember]
        public string idBucket { get; set; }
        [DataMember]
        public string idSubTypeWork { get; set; }
    }
}
