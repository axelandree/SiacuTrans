using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY = Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Transac.Service;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
//using SIACPOSTPAGOTxWS = Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Func = Claro.SIACU.Transac.Service;
namespace Claro.SIACU.Data.Transac.Service.Fixed
{
   public class ExternalInternalTransfer
    {

       public static Claro.SIACU.Entity.Transac.Service.Common.ListItem GetGenerateSOT(string strIdSession, string strTransaction, string vCusID, string vCoID, int vTipTra, string vFeProg, string vFranja, string vCodMotivo,
                                         string vObserv, string vPlano, string vUser, string idTipoServ, string cargo)
        {
            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("as_customer_id",DbType.String,22,ParameterDirection.Input,vCusID),                                                                                                                                                
                    new DbParameter("ad_cod_id",DbType.String,22,ParameterDirection.Input,vCoID),
                    new DbParameter("an_tiptra",DbType.Int64,300,ParameterDirection.Input,vTipTra),
                    new DbParameter("ad_fecprog",DbType.Date,ParameterDirection.Input,DateTime.Parse(vFeProg)),
					new DbParameter("as_franja",DbType.String,300,ParameterDirection.Input,vFranja),
                    new DbParameter("an_codmotot",DbType.String,22,ParameterDirection.Input,vCodMotivo),
                    new DbParameter("as_observacion",DbType.String,300,ParameterDirection.Input,vObserv),
                    new DbParameter("as_plano",DbType.String,300,ParameterDirection.Input,vPlano),
                    new DbParameter("as_tiposervico",DbType.Int64,ParameterDirection.Input),                                                                                             
					new DbParameter("as_usuarioreg",DbType.String,30,ParameterDirection.Input,vUser),
                    new DbParameter("as_cargo",DbType.Double,ParameterDirection.Input),
                    new DbParameter("o_codsolot",DbType.Int64,300,ParameterDirection.Output),
                    new DbParameter("o_res_cod",DbType.Int64,300,ParameterDirection.Output),
					new DbParameter("o_res_desc",DbType.String,1000,ParameterDirection.Output)};


            if (idTipoServ.Length > 0)
            {
                parameters[8].Value = int.Parse(idTipoServ);
            }
            if (cargo != null)
            {
                if (cargo.Length > 0)
                {
                    parameters[10].Value = double.Parse(cargo);
                }
            }
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SGA_P_GENERA_SOT, parameters);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }



            string rCodSot;
            string rCodRes;
            string rDescRes;

            rCodSot = Convert.ToString(parameters[11].Value.ToString());
            rCodRes = Convert.ToString(parameters[12].Value.ToString());
            rDescRes = Convert.ToString(parameters[13].Value.ToString());

            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("RecordSotTrasnsferInternal - out  Res:{0} Des{1}", rCodRes == null ? "" : rCodRes, rDescRes == null ? "" : rDescRes));
            ListItem Item = new ListItem()
            {
                Code = rCodSot,
                Code2 = Convert.ToString(rCodRes),
                Description = rDescRes
            };
            return Item;

        }


        public static ENTITY.GetRecordTransExtInt.RecordTranferExtIntResponse GetRecordTransaction(string strIdSession, string strTransaction, ENTITY.GetRecordTransExtInt.RecordTranferExtIntRequest objGetRecordTransactionRequest)
        {

            string intNumSot = string.Empty;

            if (objGetRecordTransactionRequest.InterCasoID == null)
            {
                objGetRecordTransactionRequest.InterCasoID = SIACU.Transac.Service.Constants.strCero;
            }
            if (objGetRecordTransactionRequest.InterCasoID == string.Empty)
            {
                objGetRecordTransactionRequest.InterCasoID = SIACU.Transac.Service.Constants.strCero;
            }
            if (objGetRecordTransactionRequest.NumCarta == null)
            {
                objGetRecordTransactionRequest.NumCarta = SIACU.Transac.Service.Constants.strCero;
            }
            if (objGetRecordTransactionRequest.NumCarta == string.Empty)
            {
                objGetRecordTransactionRequest.NumCarta = SIACU.Transac.Service.Constants.strCero;
            }

            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("p_id", DbType.String, ParameterDirection.Input,Convert.ToString(objGetRecordTransactionRequest.InterCasoID)),
                    new DbParameter("v_cod_id", DbType.String, ParameterDirection.Input,Convert.ToString(objGetRecordTransactionRequest.ConID)),
                    new DbParameter("v_customer_id", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.CustomerID)),
                    new DbParameter("v_tipotrans", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.TransTipo)),
                    new DbParameter("v_codintercaso", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.InterCasoID)),
                    new DbParameter("v_tipovia", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.TipoVia)),
                    new DbParameter("v_nombrevia", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.NomVia)),
                    new DbParameter("v_numerovia", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.NroVia)),
                    new DbParameter("v_tipourbanizacion", DbType.Int32, ParameterDirection.Input, Convert.ToInt(objGetRecordTransactionRequest.TipoUrb)),
                    new DbParameter("v_nombreurbanizacion", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.NomUrb)),
                    new DbParameter("v_manzana", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.NumMZ)),
                    new DbParameter("v_lote", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.NumLote)),
                    new DbParameter("v_codubigeo", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.Ubigeo)),
                    new DbParameter("v_codzona", DbType.Int32, ParameterDirection.Input, Convert.ToInt(objGetRecordTransactionRequest.ZonaID)),
                    new DbParameter("v_idplano", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.PlanoID)),
                    new DbParameter("v_codedif", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.EdificioID)),
                    new DbParameter("v_referencia", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.Referencia)),
                    new DbParameter("v_observacion", DbType.String,4000, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.Observacion)),
                    new DbParameter("v_fec_prog", DbType.Date, ParameterDirection.Input,Convert.ToDate( objGetRecordTransactionRequest.FechaProgramada)),
                    new DbParameter("v_franja_horaria", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.FranjaHora)),                                       
                    new DbParameter("v_numcarta", DbType.Double, ParameterDirection.Input, Convert.ToInt(objGetRecordTransactionRequest.NumCarta)),
                    new DbParameter("v_operador", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.Operador)),
                    new DbParameter("v_presuscrito", DbType.Int32, ParameterDirection.Input, Convert.ToInt(objGetRecordTransactionRequest.Presuscrito)),
                    new DbParameter("v_publicar", DbType.Double, ParameterDirection.Input, Convert.ToDouble(objGetRecordTransactionRequest.Publicar)),
                    new DbParameter("v_ad_tmcode", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.TmCode)),
                    new DbParameter("v_lista_coser", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.ListaCoSer)),
                    new DbParameter("v_lista_spcode", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.ListaSPCode)),
                    new DbParameter("v_usuarioreg", DbType.String, ParameterDirection.Input, Convert.ToString(objGetRecordTransactionRequest.USRREGIS)),
                    new DbParameter("v_cargo", DbType.Double,  ParameterDirection.Input),
                    new DbParameter("v_codsolot", DbType.Int64,ParameterDirection.Output),
                    new DbParameter("p_error_code", DbType.Int64, ParameterDirection.Output),
                    new DbParameter("p_error_msg", DbType.String,4000, ParameterDirection.Output)
                };



            int rintResCod;
            string rstrResDes;
            ENTITY.GetRecordTransExtInt.RecordTranferExtIntResponse Item = new ENTITY.GetRecordTransExtInt.RecordTranferExtIntResponse();
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SGA_P_GENERA_TRANSACCION, parameters);
           
                intNumSot = parameters[29].Value.ToString();
                rintResCod = Convert.ToInt(parameters[30].Value.ToString());
                rstrResDes = Convert.ToString(parameters[31].Value.ToString());
        
               Item.IdTransfer = intNumSot;
               Item.CodMessaTransfer = Convert.ToString(rintResCod);
               Item.DescMessaTransfer = rstrResDes;
            

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                Item.IdTransfer =string.Empty;
                Item.CodMessaTransfer = Claro.SIACU.Transac.Service.Constants.strMenosUno; 
                Item.DescMessaTransfer = ex.Message;
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }


            return Item;
        }




        public static List<ENTITY.BEDeco> GetServicesDTH(string strIdSession, string strTransaction, string vCusID, string vCoID)
        {
            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("av_customer_id",DbType.String,22,ParameterDirection.Input,vCusID),                                                                                                                                                
                    new DbParameter("av_cod_id",DbType.String,22,ParameterDirection.Input,vCoID),
                    new DbParameter("ac_equ_cur",DbType.Object,ParameterDirection.Output),
                    new DbParameter("an_resultado",DbType.Int64,ParameterDirection.Output),
                    new DbParameter("av_mensaje",DbType.String,250,ParameterDirection.Output)
					};
            List<ENTITY.BEDeco> listItem = null;
            try
            {

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_CONSULTA_EQU, parameters, (IDataReader reader) =>
              {
                  listItem = new List<ENTITY.BEDeco>();
                while (reader.Read())
                {
                    listItem.Add(new ENTITY.BEDeco()
                    {
                        idtransaccion = (reader["idtransaccion"]).ToString(),
                        codigo_material = (reader["codigo_material"]).ToString(),
                        codigo_sap = (reader["codigo_sap"]).ToString(),
                        numero_serie = (reader["numero_serie"]).ToString(),
                        macadress = (reader["macaddress"]).ToString(),
                        descripcion_material = (reader["descripcion_material"]).ToString(),
                        abrev_material = (reader["abrev_material"]).ToString(),
                        estado_material = (reader["estado_material"]).ToString(),
                        precio_almacen = (reader["precio_almacen"]).ToString(),
                        codigo_cuenta = (reader["codigo_cuenta"]).ToString(),
                        componente = (reader["componente"]).ToString(),
                        //item.codsap_mig = (reader["codsap_mig"]).ToString(),
                        centro = (reader["centro"]).ToString(),
                        idalm = (reader["idalm"]).ToString(),
                        almacen = (reader["almacen"]).ToString(),
                        tipo_equipo = (reader["tipo_equipo"]).ToString(),
                        id_producto = (reader["idproducto"]).ToString(),
                        //item.id_interfase = (reader["id_interfase"]).ToString(),
                        id_cliente = (reader["id_cliente"]).ToString(),
                        //item.id_activacion = (reader["id_activacion"]).ToString(),
                        modelo = (reader["modelo"]).ToString(),
                        //item.codsolot = "11111".ToString(), //(reader["codsolot"]).ToString(),
                        fecusu = (reader["fecusu"]).ToString(),
                        codusu = (reader["codusu"]).ToString(),
                        //item.objetoiw = (reader["objetoiw"]).ToString(),
                        //item.error = (reader["error"]).ToString(),
                        //item.id_sec = (reader["id_seq"]).ToString(),
                        //item.evento = (reader["evento"]).ToString(),
                        //item.estado_itw = (reader["estado_itw"]).ToString(),
                        //item.respuestaxml = (reader["respuestaxml"]).ToString(),
                        //item.mtamodel = (reader["mtamodel"]).ToString(),
                        //item.unitadreaderess = (reader["unitadreaderess"]).ToString(),
                        convertertype = (reader["convertertype"]).ToString(),
                        servicio_principal = (reader["servicio_principal"]).ToString(),
                        headend = (reader["headend"]).ToString(),
                        ephomeexchange = (reader["ephomeexchange"]).ToString(),
                        numero = (reader["numero"]).ToString()
                    });
                }
            });
            
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }

             return listItem;

        }


       
        public static List<ENTITY.GenericItem> GetListTownCenter(string strIdSession, string strTransaction, string strUbigeo)
        {
            DbParameter[] parameters = new DbParameter[] { 
            
            new DbParameter("ac_ubigeo", DbType.String,100,ParameterDirection.Input,strUbigeo),
            new DbParameter("ao_cursor", DbType.Object,ParameterDirection.Output),
            new DbParameter("an_codigo_error", DbType.Int64,ParameterDirection.Output), 
			new DbParameter("ac_mensaje_error", DbType.String,200,ParameterDirection.Output)
         
			
            
            };
            List<ENTITY.GenericItem> Listitem = null;
            int intCount = 0;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_TOWN_CENTER, parameters, (IDataReader reader) =>
                {
                    Listitem = new List<ENTITY.GenericItem>();
                    while (reader.Read())
                    {
                        intCount++;
                        string strId = intCount.ToString();
                        Listitem.Add(new ENTITY.GenericItem()
                            {
                                Id_motivo = strId,
                                Numero = strId,
                                Codigo = Functions.CheckStr(reader["IDPOBLADO"]),
                                Descripcion = Functions.CheckStr(reader["NOMBRE"]),

                            });

                    }


                });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }
         
           return Listitem;
        
        }

       
        public static string GetUbigeoId(string strSession, string strTransaction, string strDepartment, string strState, string strProvince, string strDistrict)
        {
            DbParameter[] parameters = new DbParameter[]{
        			   new DbParameter("K_COD_DISTRITO", DbType.String,100,ParameterDirection.Input,strDistrict),
                       new DbParameter("K_COD_PROVINCIA", DbType.String,100,ParameterDirection.Input,strDepartment), 
                       new DbParameter("K_COD_DEPARTAMENTO", DbType.String,100,ParameterDirection.Input,strProvince), 
					   new DbParameter("K_ESTADO", DbType.String,100,ParameterDirection.Input,strState),
					   new DbParameter("K_CUR_SALIDA", DbType.Object,ParameterDirection.Output)
         
         };
            string strIdUbigeo = string.Empty;
            int intCount = 0;
            try
            {
                DbFactory.ExecuteReader(strSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SECSS_CON_DISTRITO, parameters, (IDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            strIdUbigeo = Functions.CheckStr(reader["UBIGEO"]);
                            intCount++;

                        }

                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strSession, strTransaction, ex.InnerException.Message);
                }
             }
            
            return strIdUbigeo;
        
        }


        
        public static List<ENTITY.GenericItem> GetListPlans(string strIdSession, string strTransaction, string strUbigeo)
        {
            DbParameter[] parameters = new DbParameter[]
           {
             new DbParameter("p_ubigeo", DbType.String,ParameterDirection.Input,strUbigeo), 
			  new DbParameter("cur_salida_o", DbType.Object,ParameterDirection.Output),
             new DbParameter("an_coderror_o", DbType.Int64,ParameterDirection.Output),
			 new DbParameter("ac_mensaje_o", DbType.String,75,ParameterDirection.Output)
			
           
           };
            List<ENTITY.GenericItem> Listitem = null;
            int intCount=0;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_CONSULT_CENTER, parameters, (IDataReader reader) =>
                    {
                        Listitem = new List<ENTITY.GenericItem>();
                        while (reader.Read())
                        {
                            intCount++;
                            string strId = intCount.ToString();
                            Listitem.Add(new ENTITY.GenericItem()
                                {
                                    Id_motivo = Functions.CheckStr(reader["idplano"]),
                                    Numero =(reader["idplano"]).ToString(),
                                    Codigo = Functions.CheckStr(reader["descripcion"]),
                                    Codigo2 = Functions.CheckStr(reader["distrito_Desc"]),
                                    Codigo3 = Functions.CheckStr(reader["centro_poblado"]),
                                    Fecha = Functions.CheckStr(reader["idplano"])

                                });
                        }

                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return Listitem;
          
        
        }

       
        public static List<ENTITY.GenericItem> GetListEBuildings(string strIdSession, string strTransaction, string strCodePlan)
        {
            DbParameter[] parameters = new DbParameter[]
          {
            new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output),
            new DbParameter("P_COD_PLANO", DbType.String,100,ParameterDirection.Input,strCodePlan), 
		    new DbParameter("P_COD_EDIFICIO", DbType.String,100,ParameterDirection.Input,string.Empty)
          };
            List<ENTITY.GenericItem> Listitem = null;
            int intCount = 0;
            try
            {
                DbFactory.ExecuteReader(strIdSession,strTransaction,DbConnectionConfiguration.SIAC_POST_PVU,DbCommandConfiguration.PVU_MANTSS_LISTA_EDIFICIOHFC,parameters,(IDataReader reader)=>
                    {
                     Listitem= new List<ENTITY.GenericItem>();
                        while(reader.Read())
                        {
                            intCount++;
                            string strId = intCount.ToString();
                            Listitem.Add(new ENTITY.GenericItem()
                                {
                                
                                Id_motivo=strId.ToString(),
                                Numero=strId.ToString(),
                                Codigo=Functions.CheckStr(reader["DEPAV_DESCRIPCION"]),
                                Codigo2=Functions.CheckStr(reader["PROVV_DESCRIPCION"]),
                                Codigo3=Functions.CheckStr(reader["DISTV_DESCRIPCION"]),
                                Descripcion=Functions.CheckStr(reader["EDIFV_DIRECCION"]),
                                Descripcion2=Functions.CheckStr(reader["EDIFV_DIRECCION"]),
                                Tipo=Functions.CheckStr(reader["EDIFV_DESCRIPCION"]),
                                Fecha=Functions.CheckStr(reader["EDIFV_CODIGO"])

                                });
                        
                        
                        }
                    
                    });

            }
            catch (Exception ex)
            {
                 Web.Logging.Error(strIdSession, strTransaction,ex.Message);
                 if (ex.InnerException.Message != null)
                 {
                     Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                 }
               
            }
            return Listitem;
        
        }

        
        public static string GetCoverage(string strIdSession, string strTransaction, string strIdCob)
        { 
           DbParameter[] parameters= new DbParameter[]{
                new DbParameter("an_idpoblado",DbType.String,20,ParameterDirection.Input,strIdCob),
				new DbParameter("an_valido",DbType.Int32,10,ParameterDirection.Output),
				new DbParameter("an_codigo_error",DbType.Int32,10,ParameterDirection.Output),
				new DbParameter("ac_mensaje_error",DbType.String,250,ParameterDirection.Output)};

           string strCoverage = string.Empty;
       
           try
           {
               DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_COBERTURA, parameters);
           }
           catch (Exception ex)
           {
               Web.Logging.Error(strIdSession, strTransaction, ex.Message);
               if (ex.InnerException.Message != null)
               {
                   Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
               }
              
           }
           strCoverage = Functions.CheckStr(parameters[1].Value);
           return strCoverage;
        }

        public static bool GetUpdateAddress(string strIdSession, string strTransaction, ENTITY.GetAddressUpdate.AddressUpdateRequest RequestUpdateAddress) 
        {
            bool Item = false;
            try
            {
               Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS.bscs_cambioDireccionPostal_request Req = new  Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS.bscs_cambioDireccionPostal_request();
               Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS.bscs_cambioDireccionPostal_response Resp = new Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS.bscs_cambioDireccionPostal_response();

                if (RequestUpdateAddress.strIdCustomer != null)
                    Req.p_CustomerID = Functions.CheckInt(RequestUpdateAddress.strIdCustomer);

                if (RequestUpdateAddress.strDomicile != null)
                   Req.p_Direccion = RequestUpdateAddress.strDomicile;

                if (RequestUpdateAddress.strReference != null)
                   Req.p_NotasDireccion = RequestUpdateAddress.strReference;

                if (RequestUpdateAddress.strDistrict != null)
                   Req.p_Distrito = RequestUpdateAddress.strDistrict;

                if (RequestUpdateAddress.strProvince != null)
                    Req.p_Provincia = RequestUpdateAddress.strProvince;

                if (RequestUpdateAddress.strCodPostal != null)
                   Req.p_CodigoPostal = RequestUpdateAddress.strCodPostal;

                if (RequestUpdateAddress.StrDepartament != null)
                    Req.p_Departamento = RequestUpdateAddress.StrDepartament;

                 if (RequestUpdateAddress.strCountryLegal != null) { 
                    Req.p_Pais = RequestUpdateAddress.strCountryLegal;
                    }


                 Resp = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.ProxyService.Transac.Service.SIACPostpagoTxWS.bscs_cambioDireccionPostal_response>(strIdSession, strTransaction,
                   () =>
                   {
                       return Configuration.WebServiceConfiguration.SIACPostpagoTxWS.cambioDireccionPostal(Req); 
                   });


                 if (Resp.p_result == Claro.SIACU.Transac.Service.Constants.numeroUno)
                 {
                     Item = true;
                 }
                 else
                 {
                     Item = false;
                 }
             }
            catch (Exception ex)
            {
             Web.Logging.Info(strIdSession, strTransaction, ex.Message);
             Item = false;
            }
            return Item;
        }

        #region Internal /External -LTE
        
        public static List<ENTITY.GenericItem> GetMotiveSoftLte(string strIdSession, string strTransaction)
        {
            List<ENTITY.GenericItem> listMotiveSoft = null;
            DbParameter[] parameters = new DbParameter[]
            {
              new DbParameter("srv_cur", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                int count = 0;
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LTE_P_CONSULT_MOTIVE, parameters, (IDataReader reader) =>
                {
                    listMotiveSoft = new List<ENTITY.GenericItem>();
                    while (reader.Read())
                    {

                            listMotiveSoft.Add(new ENTITY.GenericItem()
                            {

                                Codigo = reader["CODMOTOT"].ToString(),
                                Descripcion = reader["MOTIVO"].ToString()

                            });
                        }

                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);
            }
            return listMotiveSoft;
        }
  
        public static List<EntitiesFixed.JobType> GetJobTypeLte(string strIdSession, string strTransaction, int intTypeTransaction)
        {
            DbParameter[] parameters = new DbParameter[]
          {
           new DbParameter("p_tipo", DbType.Int32,22,ParameterDirection.Input,intTypeTransaction),
           new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
          
          };
            List<EntitiesFixed.JobType> listJobType = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LTE_P_CONSULT_TIPTRA, parameters, (IDataReader reader) =>
                {
                    listJobType = new List<EntitiesFixed.JobType>();

                    EntitiesFixed.JobType GenericItem = null;
                    while (reader.Read())
                    {
                        GenericItem = new EntitiesFixed.JobType();
                        GenericItem.tiptra = Functions.CheckStr(reader["tiptra"]);
                        GenericItem.descripcion = Functions.CheckStr(reader["descripcion"]);
                        GenericItem.FLAG_FRANJA = Functions.CheckStr(reader["FLAG_FRANJA"]);
                        GenericItem.FLAG_FRANJA = (GenericItem.FLAG_FRANJA == ConstantsHFC.strUno ? GenericItem.tiptra + ".|" : GenericItem.tiptra);

                        listJobType.Add(GenericItem);

                    }

                });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);
            }
            return listJobType;

        }
    
        public static List<ENTITY.BEDeco> GetServicesLte(string strIdSession, string strTransaction, string strCustomerId, string strCoid)
        {
            List<ENTITY.BEDeco> listServicesLte = new List<BEDeco>();
            DbParameter[] parameters = 
            { 
              new  DbParameter("av_customer_id", DbType.Int32, ParameterDirection.Input,strCustomerId), 
			  new  DbParameter("av_cod_id", DbType.String, ParameterDirection.Input,strCoid),
              new  DbParameter("ac_equ_cur", DbType.Object,ParameterDirection.Output),
			  new  DbParameter("an_resultado", DbType.Int32,ParameterDirection.Output),
			  new  DbParameter("av_mensaje", DbType.String,250,ParameterDirection.Output)             
            };

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LTE_P_CONSULT_EQU, parameters, (IDataReader reader) =>
                {
                    listServicesLte = new List<EntitiesFixed.BEDeco>();
                    while (reader.Read())
                    {
                        listServicesLte.Add(new ENTITY.BEDeco()
                        {
                            codigo_material = Functions.CheckStr(reader["codigo_material"]),
                            codigo_sap = Functions.CheckStr(reader["codigo_sap"]),
                            numero_serie = Functions.CheckStr(reader["numero_serie"]),
                            macadress = Functions.CheckStr(reader["macaddress"]),
                            descripcion_material = Functions.CheckStr(reader["descripcion_material"]),
                            tipo_equipo = Functions.CheckStr(reader["tipo_equipo"]),
                            centro = Functions.CheckStr(reader["centro"]),
                            Tipo = Functions.CheckStr(reader["tipo"]),
                            sim_card = Functions.CheckStr(reader["sim_card"]),
                            tipo_servicio = Functions.CheckStr(reader["tipo_servicio"]),
                            Estado = Functions.CheckStr(reader["estado"]),
                            numero = Functions.CheckStr(reader["numero"]),
                            modelo = Functions.CheckStr(reader["modelo"]),
                            convertertype = Functions.CheckStr(reader["tipodth"])
                        });

                    }

                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);

            }

            return listServicesLte;

        }

        public static string RegisterTransactionLTE(string strIdSession, string strTransaction,RegisterTransaction objRegisterTransaction, out int intResCod,out string strResDes)
        {
            
            int intNumSot = 0;
            //if (objRegisterTransaction.InterCasoID == null)
            //    objRegisterTransaction.InterCasoID = Func.Constants.strCero;
            //if (objRegisterTransaction.InterCasoID == string.Empty)
            //    objRegisterTransaction.InterCasoID = Func.Constants.strCero;
            if (objRegisterTransaction==null)
            {
                objRegisterTransaction = new RegisterTransaction();
            }
            if(string.IsNullOrEmpty(objRegisterTransaction.InterCasoID))
            { 
              objRegisterTransaction.InterCasoID=Func.Constants.strCero;
            }
            
            if (objRegisterTransaction.CodOCC == null)
            {
                objRegisterTransaction.CodOCC = string.Empty;
            }
            DateTime FechaProgramada = Claro.Convert.ToDate(Claro.Convert.ToDate(objRegisterTransaction.FechaProgramada).ToShortDateString());// Functions.CheckDate(objRegisterTransaction.FechaProgramada);


            DbParameter[] parameters = new DbParameter[] {
              
                    new  DbParameter("p_id", DbType.Int64,  ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.InterCasoID)),
                    new  DbParameter("a_cargo", DbType.Double,  ParameterDirection.Input,Func.Functions.CheckDbl(objRegisterTransaction.Cargo)),
                    new  DbParameter("a_codigo_occ", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.CodOCC)),
                    new  DbParameter("a_cod_caso", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.CodCaso)),
                    new  DbParameter("a_cod_id", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.ConID)),
                    new  DbParameter("a_cod_intercaso", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.InterCasoID)),
                    new  DbParameter("a_codedif", DbType.Int64,  ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.EdificioID)),
                    new  DbParameter("a_codmotot", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.MotivoID)),
                    new  DbParameter("a_codusu", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.USRREGIS)),
                    new  DbParameter("a_codzona", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.ZonaID)),
                    new  DbParameter("a_customer_id", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.CustomerID)),
                    new  DbParameter("a_fecprog", DbType.Date , ParameterDirection.Input, FechaProgramada),  
                    new  DbParameter("a_franja", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.FranjaHoraID)),
                    new  DbParameter("a_franja_hor", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.FranjaHora)),
                    new  DbParameter("a_lote", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.NumLote)),
                    new  DbParameter("a_manzana", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.NumMZ)),
                    new  DbParameter("a_nom_via", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.NomVia)),
                    new  DbParameter("a_nomurb", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.NomUrb)),
                    new  DbParameter("a_num_via", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.NomVia)),
                    new  DbParameter("a_observacion", DbType.String,4000, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.Observacion)),
                    new  DbParameter("a_centro_poblado", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.CentroPobladoID)),
                    new  DbParameter("a_referencia", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.Referencia)),
                    new  DbParameter("a_tip_urb", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.TipoUrb)),
                    new  DbParameter("a_tipo_trans", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.TransTipo)),
                    new  DbParameter("a_tipo_via", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.TipoVia)),
                    new  DbParameter("a_tiposervico", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.ServicioID)),
                    new  DbParameter("a_tiptra", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.TrabajoID)),
                    new  DbParameter("a_ubigeo", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.Ubigeo)),
                    new  DbParameter("a_reclamo_caso", DbType.Int64,ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.CodReclamo)),
                    new  DbParameter("a_flag_act_dir_fact", DbType.String ,ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.FlagActDirFact)),
                    new  DbParameter("a_numcarta", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.NumCarta)),
                    new  DbParameter("a_operador", DbType.String, ParameterDirection.Input , Func.Functions.CheckStr(objRegisterTransaction.Operador)),
                    new  DbParameter("a_presuscrito", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.Presuscrito)),
                    new  DbParameter("a_publicar", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.Publicar)),
                    new  DbParameter("a_ad_tmcode", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.TmCode)),
                    new  DbParameter("a_lista_coser", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.ListaCoser)),
                    new  DbParameter("a_lista_spcode", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.ListaSpCode)),
                    new  DbParameter("a_cantidad", DbType.Int64, ParameterDirection.Input,Func.Functions.CheckInt64(objRegisterTransaction.Cantidad)),
                    new  DbParameter("o_codsolot", DbType.Int64,ParameterDirection.Output),
                    new  DbParameter("errmsg_out", DbType.String,1000, ParameterDirection.Output),
                    new  DbParameter("resultado_out", DbType.Int64, ParameterDirection.Output)
                 //   new DbParameter("a_codigo_occ", DbType.String, ParameterDirection.Input,Func.Functions.CheckStr(objRegisterTransaction.CodOCC))
            };

            if (objRegisterTransaction.Cargo != null)
            {
                if (objRegisterTransaction.Cargo.Length > 0)
                {
                    parameters[1].Value = double.Parse(objRegisterTransaction.Cargo);
                }else
                {
                    parameters[1].Value = DBNull.Value;
                }
        }

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LTE_P_GENERA_TRANSACCION ,parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Info(strIdSession, strTransaction, ex.Message);

                if (ex.InnerException.Message!=null)
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
            }
            finally
            {
                //var varNumber = parameters[38].Value.ToString() == "null" ? SIACU.Transac.Service.Constants.numeroCero : Functions.CheckInt(parameters[38].Value);
                //intNumSot = Convert.ToInt(varNumber);
                //strResDes = (parameters[39].Value.ToString() == null ? string.Empty : parameters[39].Value).ToString();
                //intResCod = Convert.ToInt(parameters[40].Value.ToString() == null ? string.Empty : parameters[40].Value.ToString());

               // var varNumber = parameters[parameters.Length - 4].Value.ToString() == string.Empty ? SIACU.Transac.Service.Constants.numeroCero : Functions.CheckInt(parameters[parameters.Length - 4].Value);
                intNumSot = Convert.ToInt(parameters[parameters.Length - 4].Value.ToString() == string.Empty ? SIACU.Transac.Service.Constants.numeroCero : Functions.CheckInt(parameters[parameters.Length - 4].Value));
                intResCod = Convert.ToInt(parameters[parameters.Length - 3].Value.ToString() == string.Empty ? string.Empty : parameters[parameters.Length - 3].Value.ToString());
                strResDes = (parameters[parameters.Length - 2].Value.ToString() == string.Empty ? string.Empty : parameters[parameters.Length - 2].Value).ToString();
                
       
        }

            return Func.Functions.CheckStr(intNumSot);
        
        }
        #endregion
        //PROY140315 - Inicio
        public static List<JobType> GetJobTypeDTH(string strIdSession, string strTransaction, int intTypeTransaction)
        {
            DbParameter[] parameters = new DbParameter[]
          {
           new DbParameter("p_tipo", DbType.Int32,22,ParameterDirection.Input,intTypeTransaction),
           new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
          
          };
            List<EntitiesFixed.JobType> listJobType = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_DTH_P_CONSULT_TIPTRA, parameters, (IDataReader reader) =>
                {
                    listJobType = new List<EntitiesFixed.JobType>();

                    EntitiesFixed.JobType GenericItem = null;
                    while (reader.Read())
                    {
                        GenericItem = new EntitiesFixed.JobType();
                        GenericItem.tiptra = Functions.CheckStr(reader["tiptra"]);
                        GenericItem.descripcion = Functions.CheckStr(reader["descripcion"]);
                        GenericItem.FLAG_FRANJA = Functions.CheckStr(reader["FLAG_FRANJA"]);
                        GenericItem.FLAG_FRANJA = (GenericItem.FLAG_FRANJA == ConstantsHFC.strUno ? GenericItem.tiptra + ".|" : GenericItem.tiptra);

                        listJobType.Add(GenericItem);

                    }

                });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);
            }
            return listJobType;
        }
        //PROY140315 - Fin
    }
}
