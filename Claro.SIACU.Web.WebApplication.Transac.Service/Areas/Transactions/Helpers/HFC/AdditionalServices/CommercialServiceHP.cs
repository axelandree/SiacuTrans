namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices
{
    public class CommercialServiceHP
    {       
        public string DE_GRP { get; set; }        
        public string NO_GRP { get; set; }       
        public string CO_SER { get; set; }       
        public string DE_SER { get; set; }        
        public string NO_SER { get; set; }        
        public string CO_EXCL { get; set; }      
        public string DE_EXCL { get; set; }       
        public string ESTADO { get; set; }       
        public string VALIDO_DESDE { get; set; }       
        public string SUSCRIP { get; set; }       
        public string CARGOFIJO { get; set; }      
        public string CUOTA { get; set; }      
        public string PERIODOS { get; set; }     
        public string BLOQ_ACT { get; set; }     
        public string BLOQ_DES { get; set; }      
        public string SNCODE { get; set; }      
        public string SPCODE { get; set; }        
        public string VALORPVU { get; set; }       
        public string COSTOPVU { get; set; }        
        public string DESCOSER { get; set; }       
        public string TIPOSERVICIO { get; set; }       
        public string TIPO_SERVICIO { get; set; }       
        public decimal DESCUENTO { get; set; }     
        public string CODSERPVU { get; set; }

        //#region PROY-32650  II - Retención/Fidelización
        public string CODGRUPOSERV { get; set; }
        public string CANTEQUIPO { get; set; }
        public string IDEQUIPO { get; set; }
        public string CODTIPOEQUIPO { get; set; }
        public string TMCODE { get; set; }
    }
}