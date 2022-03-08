using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.Data;
using System.Data;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Common;
using FUNCTIONS = Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
   public class PostBilledOutCallDetails
    {
       public static List<CallDetailGeneric> ListarLlamadasMesPrepagoControl(string strIdSession, string strTransaction, string vMSISDN, string vFECHA_INI, string vFECHA_FIN, string vFLAG_NACIONAL, string vFLAG_SMS_MMS, string vFLAG_GPRS, string vFLAG_INTERNACIONA, string vFLAG_777, string vFLAG_VAS, string vFLAG_DETALLE, string vFLAG_TIPO_VISUAL, string vFlag, string vSeguridad)
        {
            List<CallDetailGeneric> GetBillPostDetail = new List<CallDetailGeneric>();
            
            DbParameter[] parameters = new DbParameter[] {                                       
                new DbParameter("P_MSISDN", DbType.String,20,ParameterDirection.Input,vMSISDN), 
				new DbParameter("P_FECHA_INI", DbType.String,20,ParameterDirection.Input,vFECHA_INI),
				new DbParameter("P_FECHA_FIN", DbType.String,20,ParameterDirection.Input,vFECHA_FIN),
				new DbParameter("P_FLAG_NACIONAL", DbType.String,20,ParameterDirection.Input,vFLAG_NACIONAL),
				new DbParameter("P_FLAG_SMS_MMS", DbType.String,20,ParameterDirection.Input,vFLAG_SMS_MMS),
				new DbParameter("P_FLAG_GPRS", DbType.String,20,ParameterDirection.Input,vFLAG_GPRS),
				new DbParameter("P_FLAG_INTERNACIONA", DbType.String,20,ParameterDirection.Input,vFLAG_INTERNACIONA),
				new DbParameter("P_FLAG_777", DbType.String,20,ParameterDirection.Input,vFLAG_777),
				new DbParameter("P_FLAG_VAS", DbType.String,20,ParameterDirection.Input,vFLAG_VAS),
				new DbParameter("P_FLAG_DETALLE", DbType.String,20,ParameterDirection.Input,vFLAG_DETALLE),
				new DbParameter("P_FLAG_TIPO_VISUAL", DbType.String,20,ParameterDirection.Input,vFLAG_TIPO_VISUAL),
				new DbParameter("p_cursor_salida", DbType.Object, ParameterDirection.Output)
			  };


            
            try
            {
                GetBillPostDetail = DbFactory.ExecuteReader<List<CallDetailGeneric>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_ODSPRE_REC, DbCommandConfiguration.SIACU_POST_SP_DETALLE_LLAMADAS, parameters);
            
            }
            catch (Exception ex)
            {
                GetBillPostDetail.Clear();
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            
            return GetBillPostDetail;
        }

        public static List<OPostBillOutCallDetail.Invoice_PDI> ListarFacturas_PDI(string strIdSession, string strTransaction, string vCODCLIENTE)
       {
           Claro.Web.Logging.Info("993760253", "SalientesFacturado", "ingresa a ListarFacturas_PDI - GetListInvoicePDI");
            List<OPostBillOutCallDetail.Invoice_PDI> GetListInvoicePDI = new List<OPostBillOutCallDetail.Invoice_PDI>();
            DbParameter[] parameters = new DbParameter[] {                                       
            new DbParameter("K_CODIGOCLIENTE", DbType.String,24,ParameterDirection.Input,vCODCLIENTE),
			new DbParameter("K_ERRMSG", DbType.String,ParameterDirection.Output),												   
			new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
											   };

           
            try
            {
                Claro.Web.Logging.Info("993760253", "SalientesFacturado", "ingresa al try");
                GetListInvoicePDI = DbFactory.ExecuteReader<List<OPostBillOutCallDetail.Invoice_PDI>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters);
                
                Claro.Web.Logging.Info("993760253", "SalientesFacturado", String.Format("item|| GetListInvoicePDI:{0}",
                        GetListInvoicePDI));
            }
            catch (Exception e)
            {
                GetListInvoicePDI.Clear();
                Web.Logging.Error("993760253", "GetListInvoicePDI-ListarFacturas_PDI", "entro al catch");
                Web.Logging.Error(strIdSession, strTransaction, e.Message);
                Web.Logging.Error("993760253", "GetListInvoicePDI-ListarFacturas_PDI", e.Message);
                throw new Exception(FUNCTIONS.Functions.GetExceptionMessage(e));
                
            }
            

            return GetListInvoicePDI;

        }

        public static List<OPostBillOutCallDetail.Invoice> ListarFacturas(string strIdSession, string strTransaction, string vCODCLIENTE)
        {
            List<OPostBillOutCallDetail.Invoice> GetListInvoice = new List<OPostBillOutCallDetail.Invoice>();
            //int contador;
            DbParameter[] parameters = new DbParameter[] {  
            new DbParameter("K_CODIGOCLIENTE", DbType.String,24,ParameterDirection.Input,vCODCLIENTE),
			new DbParameter("K_ERRMSG", DbType.String,ParameterDirection.Output),												   
			new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
											   };

            
            try
            {
                GetListInvoice = DbFactory.ExecuteReader<List<OPostBillOutCallDetail.Invoice>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters);

            }
            catch (Exception e)
            {
                Web.Logging.Error(strIdSession, strTransaction, e.Message);
                GetListInvoice.Clear();
            }
            

            return GetListInvoice;

        }

        public static List<OPostBillOutCallDetail.ListCallDetail> Listar_TR_Detalle_Llamada(string strIdSession, string strTransaction, string vINVOICENUMBER, string vSeguridad, string vTELEFONO)
        {
            List<OPostBillOutCallDetail.ListCallDetail> GetListCallDetail = new List<OPostBillOutCallDetail.ListCallDetail>();
            //string sNumero = "";


            DbParameter[] parameters = new DbParameter[] {  
            new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
			new DbParameter("P_INVOICENUMBER", DbType.String,30,ParameterDirection.Input,vINVOICENUMBER),
			new DbParameter("P_TELEFONO", DbType.String,20,ParameterDirection.Input,vTELEFONO)  
											   };

           
            try
            {
                GetListCallDetail = DbFactory.ExecuteReader<List<OPostBillOutCallDetail.ListCallDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_DBTO_SP_DETALLE_LLAMADAS, parameters);
 
            }
            catch (Exception e)
            {
                Web.Logging.Error(strIdSession, strTransaction, e.Message);
                GetListCallDetail.Clear();
            }
            
            return GetListCallDetail;

        }

        public static List<OPostBillOutCallDetail.ListCallDetail_PDI> Listar_TR_Detalle_Llamada_PDI(string strIdSession, string strTransaction, string vINVOICENUMBER, string vSeguridad, string vTELEFONO)
        {
            List<OPostBillOutCallDetail.ListCallDetail_PDI> GetListCallDetailPDI = new List<OPostBillOutCallDetail.ListCallDetail_PDI>();
            //string sNumero = "";


            DbParameter[] parameters = new DbParameter[] { 
            new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
			new DbParameter("P_INVOICENUMBER", DbType.String,30,ParameterDirection.Input,vINVOICENUMBER),
			new DbParameter("P_TELEFONO", DbType.String,20,ParameterDirection.Input,vTELEFONO)												   
											   };

            
            try
            {
                GetListCallDetailPDI = DbFactory.ExecuteReader<List<OPostBillOutCallDetail.ListCallDetail_PDI>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_POST_SP_DETALLE_LLAMADAS_PDI, parameters);
 
            }
            catch (Exception e)
            {
                Web.Logging.Error(strIdSession, strTransaction, e.Message);
                GetListCallDetailPDI.Clear();
            }
            
            return GetListCallDetailPDI;
        }

        public static List<OPostBillOutCallDetail.RechargeList> SP_RechargeList(string strIdSession, string strTransaction, string vMSISDN, string vFECHINI, string vFECHFIN, string vFlag, int vNroRegistros)
        {
            List<OPostBillOutCallDetail.RechargeList> GetRechargeList = new List<OPostBillOutCallDetail.RechargeList>();
            
            DbParameter[] parameters = new DbParameter[] { 
            new DbParameter("P_MSISDN", DbType.String,20,ParameterDirection.Input,vMSISDN),
			new DbParameter("P_FECHINI", DbType.String,20,ParameterDirection.Input,vFECHINI),
			new DbParameter("P_FECHFIN", DbType.String,20,ParameterDirection.Input,vFECHFIN),												   
			new DbParameter("P_CURSOR_SALIDA", DbType.Object,ParameterDirection.Output)
											   };

            
            try
            {
                GetRechargeList = DbFactory.ExecuteReader<List<OPostBillOutCallDetail.RechargeList>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_ODSPRE_REC, DbCommandConfiguration.SIACU_POST_SP_Recharge_List_DWO, parameters);
 
            }
            catch (Exception e)
            {
                Web.Logging.Error(strIdSession, strTransaction, e.Message);
                GetRechargeList.Clear();
            }
            
            return GetRechargeList;
        }
	

    }
}
