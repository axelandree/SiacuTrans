namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class TypificationItem
    {
        public TypificationItem()
        {
            
        }
        public string TIPO { get; set; }
        public string CLASE { get; set; }
        public string SUBCLASE { get; set; }
        public string INTERACCION_CODE { get; set; }
        public string TIPO_CODE { get; set; }
        public string CLASE_CODE { get; set; }
        public string SUBCLASE_CODE { get; set; }
        public string CASO { get; set; }
        public string TRANSACCION { get; set; }
        public string PAGINA_LECTURA { get; set; }
        public string PAGINA_CASO { get; set; }

        public TypificationItem(string vTIPO, string vCLASE, string vSUBCLASE, string vINTERACCION_CODE, string vTIPO_CODE, string vCLASE_CODE, string vSUBCLASE_CODE, string vCASO)
        {
            TIPO = vTIPO;
            CLASE = vCLASE;
            SUBCLASE = vSUBCLASE;
            INTERACCION_CODE = vINTERACCION_CODE;
            TIPO_CODE = vTIPO_CODE;
            CLASE_CODE = vCLASE_CODE;
            SUBCLASE_CODE = vSUBCLASE_CODE;
            CASO = vCASO;
        }
    }

}
