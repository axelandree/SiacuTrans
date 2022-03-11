using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Transac.Service;
using SIACPreServiceCont = Claro.SIACU.ProxyService.Transac.Service.SIACPre.ContingencyService;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Common;
using SIACPreService = Claro.SIACU.ProxyService.Transac.Service.SIACPre.Service;
using ConsultDataPrePostWS = Claro.SIACU.ProxyService.Transac.Service.SIACPre.ConsultPrePostData;
using KEY = Claro.ConfigurationManager;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;
using Claro.Data;
using System.Data;
using ItemTrans = Claro.SIACU.Transac.Service.ItemGeneric;
using Constant = Claro.SIACU.Transac.Service.Constants;


namespace Claro.SIACU.Data.Transac.Service.Prepaid
{
    public class CallsDetail
    {
        #region Llamadas Entrantes
        /// <summary>
        /// Segio Aguilar
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vMSISDN"></param>
        /// <param name="vStartDate"></param>
        /// <param name="vEndDate"></param>
        /// <param name="vInteraccion"></param>
        /// <param name="vResultado"></param>
        /// <returns></returns>
        public static List<PREPAID.IncomingCallDetail> GetIncomingCallDetail(string strIdSession, string strTransaction, string vMSISDN, string vStartDate, string vEndDate, ref string vInteraccion, ref string vResultado)
        {

            List<PREPAID.IncomingCallDetail> listIncomingcallDetail = new List<PREPAID.IncomingCallDetail>();
            DbParameter[] parameters = {
                new DbParameter("V_MSISDN", DbType.String,255, ParameterDirection.Input, vMSISDN),
                new DbParameter("V_INI", DbType.String,255, ParameterDirection.Input, vStartDate),
                new DbParameter("V_FIN", DbType.String,255, ParameterDirection.Input, vEndDate),
                new DbParameter("P_CURSOR_SALIDA", DbType.Object, ParameterDirection.Output),
                new DbParameter("V_FLAG", DbType.String,255, ParameterDirection.Output)

            };

            int count = 0;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIACU_DM, DbCommandConfiguration.SIACU_SP_DET_LLAM_ENTRANTES, parameters, (IDataReader reader) =>
            {
                PREPAID.IncomingCallDetail entity = null; 
                while (reader.Read())
                {
                    count++;
                    entity = new PREPAID.IncomingCallDetail();
                    entity.NroOrd = count.ToString();
                    entity.NumberA = Functions.CheckStr(reader["NUMEROA"]);
                    entity.Date = Functions.CheckStr(reader["FECHA"]);
                    entity.StartHour = Functions.CheckStr(reader["HORA_INICIO"]);
                    entity.NumberB = Functions.CheckStr(reader["NUMEROB"]);
                    entity.Duration = Functions.CheckStr(reader["DURACION"]);
                    listIncomingcallDetail.Add(entity);
                } 
            });

            //PREPAID.IncomingCallDetail entity = null;
            //for (int i = 1; i < 11; i++) {
            //    entity = new PREPAID.IncomingCallDetail();
            //    entity.NroOrd = (i).ToString();
            //    entity.NumberA = "98820154" + i;
            //    entity.Date = "20/0" + i+"/2016";
            //    entity.StartHour = "06:"+(i*6-1);
            //    entity.NumberB = i+"7782124";
            //    entity.Duration = (i*3)+5+"";
            //    listIncomingcallDetail.Add(entity);
            //}

            vResultado = Functions.CheckStr(parameters[4]); 
            return listIncomingcallDetail;
        }

        /// <summary>
        /// DatosLineaPrepago
        /// </summary>
        /// <param name="info"></param>
        /// <param name="listAccount"></param>
        /// <param name="listTrio"></param>
        /// <param name="LineItem"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static String GetLineData(string idSession, string transaction,
            ConsultLine info, ref List<Account> listAccount, ref List<ListItem> listTrio, ref Line LineItem, ref string errorMessage)
        {
            string codigo = "";
            string mensaje = "", mensajeError = "";

            int intTimeOut = (int)Claro.SIACU.Data.Transac.Service.Common.GetParameterData(KEY.AppSettings("gParamWebServiceDatosPregagoTimeOut"), ref mensaje).Value_N;
            string flagNDP = KEY.AppSettings("FlagNuevoDatosPrepago");

            SIACPreService.EbsDatosPrepagoService preService = new SIACPreService.EbsDatosPrepagoService();
            try
            {
                if (flagNDP.Equals(Constant.strUno))
                {
                    SIACPreService.INDatosPrepagoRequest obPrepagoReq = new SIACPreService.INDatosPrepagoRequest();
                    obPrepagoReq.telefono = info.Msisdn;
                    SIACPreService.EbsDatosPrepagoService oINWS = new SIACPreService.EbsDatosPrepagoService();
                    oINWS.Url = KEY.AppSettings("gConstWebServiceDatosPregagoNuevo");
                    oINWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    oINWS.Timeout = intTimeOut;

                    SIACPreService.INDatosPrepagoResponse objPrepagoResp = new SIACPreService.INDatosPrepagoResponse();
                    objPrepagoResp = Claro.Web.Logging.ExecuteMethod<SIACPreService.INDatosPrepagoResponse>(idSession, transaction, () =>
                    {
                        return oINWS.leerDatosPrepago(obPrepagoReq);
                    });
                    SIACPreService.DatosPrepago objPrepagoResultado = null;

                    if (objPrepagoResp.resultado.Trim() == Constant.strCero)
                    {
                        codigo = "";
                        mensajeError = "";
                        objPrepagoResultado = objPrepagoResp.datosPrePago;
                        LineItem = new Line();

                        LineItem.PhoneNumber = info.Msisdn;
                        LineItem.MainBalance = Functions.ConvertSoles(objPrepagoResultado.onPeakAccountIDBalance);
                        LineItem.ExpirationDateBalance = Functions.CheckStr(objPrepagoResultado.expiryDate);
                        LineItem.LineStatus = Functions.CheckStr(objPrepagoResultado.subscriberLifeCycleStatus);
                        LineItem.TriosChanguesFree = Functions.CheckStr(objPrepagoResultado.voucherRchFraudCounter);
                        LineItem.TariffChangesFree = KEY.AppSettings("strCounterChangeTariffForFree");
                        LineItem.TariffPlan = Functions.CheckStr(objPrepagoResultado.tariffModelNumber);
                        LineItem.ProviderID = Functions.CheckStr(objPrepagoResultado.providerID);

                        string providerTFI = KEY.AppSettings("strProviderTFI");
                        string providerDTH = KEY.AppSettings("strProviderDTH");
                        LineItem.MinuteBalance_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54Balance);
                        LineItem.ExpDate_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                        LineItem.IsSelect = Functions.CheckStr(objPrepagoResultado.isSelect);

                        if (LineItem.ProviderID == providerTFI)
                            LineItem.IsTFI = Constant.Variable_SI;
                        else
                            LineItem.IsTFI = Constant.Variable_NO;
                        if (LineItem.ProviderID == providerDTH)
                            LineItem.IsDTH = Constant.Variable_SI;
                        else
                            LineItem.IsDTH = Constant.Variable_NO;
                         
                        LineItem.ActivationDate = Functions.CheckStr(objPrepagoResultado.firstCallDate);
                        LineItem.ExpDate_Line = Functions.CheckStr(objPrepagoResultado.deletionDate);
                        LineItem.DolDate = String.Empty;
                        LineItem.SubscriberStatus = Functions.CheckStr(objPrepagoResultado.subscriberStatus);
                        string vAuxCNTNumber = String.Empty;

                        if (Functions.CheckStr(objPrepagoResultado.cNTNumber.Trim()) != String.Empty)
                        {
                            vAuxCNTNumber = Functions.CheckStr(objPrepagoResultado.cNTNumber);
                            if (vAuxCNTNumber == Constant.strCero) { vAuxCNTNumber = String.Empty; }
                        }
                        LineItem.CNTNumber = vAuxCNTNumber;
                        LineItem.IsCNTPossible = Functions.CheckStr(objPrepagoResultado.isCNTPossible);
                        LineItem.NumIMSI = Functions.CheckStr(objPrepagoResultado.imsi);
                        LineItem.StatusIMSI = Functions.CheckStr(objPrepagoResultado.isLocked);

                        while (LineItem.SubscriberStatus.Length < 10)
                        {
                            LineItem.SubscriberStatus = LineItem.SubscriberStatus + Constant.strCero;
                        }
                        if (LineItem.StatusIMSI.ToUpper() == Constant.Variable_True)
                        {
                            LineItem.StatusIMSI = ConstantsSiacpo.ConstBloqueado;
                        }
                        else
                        {
                            if (LineItem.StatusIMSI.ToUpper() == Constant.Variable_False)
                                LineItem.StatusIMSI = ConstantsSiacpo.ConstDesbloqueado;
                            else
                                LineItem.StatusIMSI = String.Empty;
                        } 
                        List<ItemTrans> listaValoresXML = Functions.GetListValuesXML(Constant.NameListbags, Constant.strDos, Constant.SiacutDataPrepaidWSXML);
                        double factor_division = 1;
                        double saldo = 0;

                        Account item = new Account();
                        item.Name = Constant.Balance_SMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.sMSPromoAccountIDBalance);
                        factor_division = GetFactorBag("SMSPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSPromoAccountIDExpiryDate);
                        listAccount.Add(item);   

                        item = new Account();
                        item.Name = Constant.Voice1_PromoAccount;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voice1PromoAccountIDBalance);
                        factor_division = GetFactorBag("Voice1PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice1PromoAccountIDExpiryDate);
                        listAccount.Add(item);
                         
                        item = new Account();
                        item.Name = Constant.Voice2_PromoAccount;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voice2PromoAccountIDBalance);
                        factor_division = GetFactorBag("Voice2PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice2PromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        List<Account> lstBono5Tfi = null;
                        if (LineItem.IsTFI == Constant.Variable_SI)
                        {
                            string strNumberIn = KEY.AppSettings("strNumInTFIB");
                            lstBono5Tfi = BolsaTFIBono5.GetBono5TFI_Prepago(info.IdTransaction, info.IPApplication, info.Application, info.Msisdn, strNumberIn);
                            if (lstBono5Tfi != null)
                            {
                                if (lstBono5Tfi.Count == 2)
                                {
                                    listAccount.Add(lstBono5Tfi[1]);
                                }
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(mensajeError))
                                    mensajeError = KEY.AppSettings("strErrorTFIB");
                                else
                                    mensajeError = mensajeError + " - " + KEY.AppSettings("strErrorTFIB");
                            } 
                        }
                         
                        if (LineItem.IsTFI != Constant.Variable_SI)
                        {
                            item = new Account();
                            item.Name = Constant.Balance_MMS;
                            saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                            factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                            item.Balance = Functions.CheckStr(saldo / factor_division);
                            item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSPromoAccountIDExpiryDate);
                            listAccount.Add(item);
                        }
                        else
                        {
                            saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                            factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                            LineItem.OutstandingBalance = Functions.CheckStr(saldo / factor_division);
                        }

                        item = new Account();
                        item.Name = Constant.Balance_PromoSoles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusPromoAccountIDBalance);
                        factor_division = GetFactorBag("BonusPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusPromoAccountIDExpiryDate);
                        listAccount.Add(item);
                        
                        item = new Account();
                        item.Name = Constant.Balance_LoyaltySMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.sMSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("SMSLoyaltyAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item); 

                        item = new Account();
                        item.Name = Constant.Balance_LoyaltyVoice;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voiceLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("VoiceLoyaltyAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voiceLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Promo1_Soles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.gPRSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("Bonus1PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.gPRSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item); 
                       
                        item = new Account();
                        item.Name = Constant.Promo2_Soles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.mMSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("Bonus2PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);
                         
                        item = new Account();
                        item.Name = Constant.Bag_Multicast;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account54Balance);
                        factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                        listAccount.Add(item);
                         
                        item = new Account();
                        if (LineItem.IsTFI == Constant.Variable_SI)
                        {
                            item.Name = Constant.Bag_NumbersFrequentTFI;
                            factor_division = GetFactorBag("BonusCounter_Account52", listaValoresXML);
                        }
                        else
                        {
                            item.Name = Constant.Bag_FreeMinutes;
                            factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                        }
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account52Balance);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account52ExpiryDate);
                        listAccount.Add(item);
 
                        if (LineItem.IsTFI == Constant.Variable_SI)
                        {
                            if (lstBono5Tfi != null)
                            {
                                if (lstBono5Tfi.Count == 2)
                                {
                                    listAccount.Add(lstBono5Tfi[0]);
                                }
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(mensajeError))
                                    mensajeError = KEY.AppSettings("strErrorTFIB");
                                else
                                    mensajeError = mensajeError + " - " + KEY.AppSettings("strErrorTFIB");
                            }
                        }

                        item = new Account();
                        item.Name = Constant.FrequentlyNumbers_SMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account57Balance);
                        factor_division = GetFactorBag("BonusCounterAccount57", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account57ExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.FrequentlyNumbers_SEG;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account58Balance);
                        factor_division = GetFactorBag("BonusCounterAccount58", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account58ExpiryDate);
                        listAccount.Add(item);
                           
                        item = new Account();
                        item.Name = Constant.GPRS_KB;
                        saldo = Functions.CheckDbl(objPrepagoResultado.gPRSPromoAccountIDBalance);
                        factor_division = GetFactorBag("GPRSPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.gPRSPromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        ListItem itemGen;
                        List<string> fnfNunmber = MappingFnfNumber(objPrepagoResultado);

                        if (fnfNunmber.Count > 0)
                        {
                            for (int j = 0; j < fnfNunmber.Count; j++)
                            {
                                itemGen = new ListItem();
                                itemGen.Code = String.Format(Constant.CodeNumberTriado, (j + 1).ToString());
                                itemGen.Description = Functions.CheckStr(fnfNunmber[j]);
                                listTrio.Add(itemGen);
                            }
                        }

                        LineItem.TriacionType = Functions.CheckStr(objPrepagoResultado.activeFnFOption);
                        LineItem.NumFamFriends = listTrio.Count.ToString();
                    }
                    else
                    {
                        codigo = objPrepagoResp.resultado.Trim();
                        if (String.IsNullOrEmpty(mensajeError))
                            mensajeError = objPrepagoResp.descripcion.Trim();
                        else
                            mensajeError = mensajeError + " - " + objPrepagoResp.descripcion.Trim(); 
                    }
                }
                else if (flagNDP.Equals(Constant.strDos))
                {
                    SIACPreService.INDatosPrepagoRequest obPrepagoReq = new SIACPreService.INDatosPrepagoRequest();
                    obPrepagoReq.telefono = info.Msisdn;


                    preService.Url = WebServiceConfiguration.PrepaidService.Url;
                    //preService.Url = "http://172.19.74.202:8909/ConsultaDatosPrepagoWS/EbsDatosPrepago?WSDL"; 
                    preService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    preService.Timeout = intTimeOut;

                    SIACPreService.INDatosPrepagoResponse objPrepagoResp = new SIACPreService.INDatosPrepagoResponse();

                    objPrepagoResp = Claro.Web.Logging.ExecuteMethod<SIACPreService.INDatosPrepagoResponse>("S", transaction, () =>
                    {
                        return preService.leerDatosPrepago(obPrepagoReq);
                    });
                    SIACPreService.DatosPrepago objPrepagoResultado = null;

                    if (objPrepagoResp.resultado.Trim() == Constant.strCero)
                    {
                        codigo = "";
                        mensajeError = "";
                        objPrepagoResultado = objPrepagoResp.datosPrePago;
                        LineItem = new Line();

                        LineItem.PhoneNumber = info.Msisdn;
                        LineItem.MainBalance = Functions.ConvertSoles(objPrepagoResultado.onPeakAccountIDBalance);
                        LineItem.ExpirationDateBalance = Functions.CheckStr(objPrepagoResultado.expiryDate);
                        LineItem.LineStatus = Functions.CheckStr(objPrepagoResultado.subscriberLifeCycleStatus);
                        LineItem.TriosChanguesFree = Functions.CheckStr(objPrepagoResultado.voucherRchFraudCounter);
                        LineItem.TariffChangesFree = KEY.AppSettings("strCounterChangeTariffForFree");
                        LineItem.TariffPlan = Functions.CheckStr(objPrepagoResultado.tariffModelNumber);
                        LineItem.ProviderID = Functions.CheckStr(objPrepagoResultado.providerID);

                        string providerTFI = KEY.AppSettings("strProviderTFI");
                        string providerDTH = KEY.AppSettings("strProviderDTH");
                        LineItem.MinuteBalance_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54Balance);
                        LineItem.ExpDate_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                        LineItem.IsSelect = Functions.CheckStr(objPrepagoResultado.isSelect);

                        if (LineItem.ProviderID == providerTFI)
                            LineItem.IsTFI = Constant.Variable_SI;
                        else
                            LineItem.IsTFI = Constant.Variable_NO;
                        if (LineItem.ProviderID == providerDTH)
                            LineItem.IsDTH = Constant.Variable_SI;
                        else
                            LineItem.IsDTH = Constant.Variable_NO;

                        LineItem.ActivationDate = Functions.CheckStr(objPrepagoResultado.firstCallDate);
                        LineItem.ExpDate_Line = Functions.CheckStr(objPrepagoResultado.deletionDate);
                        LineItem.DolDate = String.Empty;
                        LineItem.SubscriberStatus = Functions.CheckStr(objPrepagoResultado.subscriberStatus);
                        string vAuxCNTNumber = String.Empty;
                        if (Functions.CheckStr(objPrepagoResultado.cNTNumber.Trim()) != String.Empty)
                        {
                            vAuxCNTNumber = Functions.CheckStr(objPrepagoResultado.cNTNumber);
                            if (vAuxCNTNumber == Constant.strCero) { vAuxCNTNumber = String.Empty; }
                        }
                        LineItem.CNTNumber = vAuxCNTNumber;
                        LineItem.IsCNTPossible = Functions.CheckStr(objPrepagoResultado.isCNTPossible);
                        LineItem.NumIMSI = Functions.CheckStr(objPrepagoResultado.imsi);
                        LineItem.StatusIMSI = Functions.CheckStr(objPrepagoResultado.isLocked);
                        while (LineItem.SubscriberStatus.Length < 10)
                        {
                            LineItem.SubscriberStatus = LineItem.SubscriberStatus + Constant.strCero;
                        }
                        if (LineItem.StatusIMSI.ToUpper() == Constant.Variable_True)
                        {
                            LineItem.StatusIMSI = ConstantsSiacpo.ConstBloqueado;
                        }
                        else
                        {
                            if (LineItem.StatusIMSI.ToUpper() == Constant.Variable_False)
                                LineItem.StatusIMSI = ConstantsSiacpo.ConstDesbloqueado;
                            else
                                LineItem.StatusIMSI = String.Empty;
                        }
                        List<ItemTrans> listaValoresXML = Functions.GetListValuesXML(Constant.NameListbags, Constant.strDos, Constant.SiacutDataPrepaidWSXML);
                        double factor_division = 1;
                        double saldo = 0;

                        Account item = new Account();
                        item.Name = Constant.Balance_SMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.sMSPromoAccountIDBalance);
                        factor_division = GetFactorBag("SMSPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSPromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Voice1_PromoAccount;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voice1PromoAccountIDBalance);
                        factor_division = GetFactorBag("Voice1PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice1PromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Voice2_PromoAccount;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voice2PromoAccountIDBalance);
                        factor_division = GetFactorBag("Voice2PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice2PromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        List<Account> lstBono5Tfi = null;
                        if (LineItem.IsTFI == Constant.Variable_SI)
                        {
                            string strNumberIn = KEY.AppSettings("strNumInTFIB");
                            lstBono5Tfi = BolsaTFIBono5.GetBono5TFI_Prepago(info.IdTransaction, info.IPApplication, info.Application, info.Msisdn, strNumberIn);
                            if (lstBono5Tfi != null)
                            {
                                if (lstBono5Tfi.Count == 2)
                                {
                                    listAccount.Add(lstBono5Tfi[1]);
                                }
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(mensajeError))
                                    mensajeError = KEY.AppSettings("strErrorTFIB");
                                else
                                    mensajeError = mensajeError + " - " + KEY.AppSettings("strErrorTFIB");
                            }
                        }

                        if (LineItem.IsTFI != Constant.Variable_SI)
                        {
                            item = new Account();
                            item.Name = Constant.Balance_MMS;
                            saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                            factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                            item.Balance = Functions.CheckStr(saldo / factor_division);
                            item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSPromoAccountIDExpiryDate);
                            listAccount.Add(item);
                        }
                        else
                        {
                            saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                            factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                            LineItem.OutstandingBalance = Functions.CheckStr(saldo / factor_division);
                        }

                        item = new Account();
                        item.Name = Constant.Balance_PromoSoles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusPromoAccountIDBalance);
                        factor_division = GetFactorBag("BonusPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusPromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Balance_LoyaltySMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.sMSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("SMSLoyaltyAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Balance_LoyaltyVoice;
                        saldo = Functions.CheckDbl(objPrepagoResultado.voiceLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("VoiceLoyaltyAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voiceLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name =Constant.Promo1_Soles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.gPRSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("Bonus1PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.gPRSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Promo2_Soles;
                        saldo = Functions.CheckDbl(objPrepagoResultado.mMSLoyaltyAccountIDBalance);
                        factor_division = GetFactorBag("Bonus2PromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSLoyaltyAccountIDExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.Bag_Multicast;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account54Balance);
                        factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        if (LineItem.IsTFI == Constant.Variable_SI)
                        {
                            item.Name = Constant.Bag_NumbersFrequentTFI;
                            factor_division = GetFactorBag("BonusCounter_Account52", listaValoresXML);
                        }
                        else
                        {
                            item.Name = Constant.Bag_FreeMinutes;
                            factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                        }
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account52Balance);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account52ExpiryDate);
                        listAccount.Add(item);

                        if (LineItem.IsTFI ==Constant.Variable_SI)
                        {
                            if (lstBono5Tfi != null)
                            {
                                if (lstBono5Tfi.Count == 2)
                                {
                                    listAccount.Add(lstBono5Tfi[0]);
                                }
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(mensajeError))
                                    mensajeError = KEY.AppSettings("strErrorTFIB");
                                else
                                    mensajeError = mensajeError + " - " + KEY.AppSettings("strErrorTFIB");
                            }
                        }

                        item = new Account();
                        item.Name = Constant.FrequentlyNumbers_SMS;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account57Balance);
                        factor_division = GetFactorBag("BonusCounterAccount57", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account57ExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.FrequentlyNumbers_SEG;
                        saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account58Balance);
                        factor_division = GetFactorBag("BonusCounterAccount58", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account58ExpiryDate);
                        listAccount.Add(item);

                        item = new Account();
                        item.Name = Constant.GPRS_KB ;
                        saldo = Functions.CheckDbl(objPrepagoResultado.gPRSPromoAccountIDBalance);
                        factor_division = GetFactorBag("GPRSPromoAccountID", listaValoresXML);
                        item.Balance = Functions.CheckStr(saldo / factor_division);
                        item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.gPRSPromoAccountIDExpiryDate);
                        listAccount.Add(item);

                        ListItem itemGen;
                        List<string> fnfNunmber = MappingFnfNumber(objPrepagoResultado);

                        if (fnfNunmber.Count > 0)
                        {
                            for (int j = 0; j < fnfNunmber.Count; j++)
                            {
                                itemGen = new ListItem();
                                itemGen.Code = String.Format(Constant.CodeNumberTriado,(j + 1).ToString());
                                itemGen.Description = Functions.CheckStr(fnfNunmber[j]);
                                listTrio.Add(itemGen);
                            }
                        }

                        LineItem.TriacionType = Functions.CheckStr(objPrepagoResultado.activeFnFOption);
                        LineItem.NumFamFriends = listTrio.Count.ToString();
                    }
                    else
                    {
                        codigo = objPrepagoResp.resultado.Trim();
                        if (String.IsNullOrEmpty(mensajeError))
                            mensajeError = objPrepagoResp.descripcion.Trim();
                        else
                            mensajeError = mensajeError + " - " + objPrepagoResp.descripcion.Trim();
                    }
                }
                // Opcion 0
                else
                {

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(idSession, transaction, String.Format("Problemas con DatosPrepagoWS, WebServiceStatus: {0}", ex.Message));
                if (ex.InnerException != null)
                {
                    Claro.Web.Logging.Info(idSession, transaction, String.Format("Problemas con DatosPrepagoWS, ex.InnerException: {0}", ex.InnerException));
                }
               
                codigo = GetLineDataContingency(info, ref listAccount, ref listTrio, ref LineItem, ref mensajeError);
            }
            finally {
                preService.Dispose();
                preService = null;
            }
            return codigo;
        }

        /// <summary>
        /// ConsultaDatosBolsasPrepago
        /// </summary>
        /// <param name="strTelephone"></param>
        /// <param name="strIdTransaction"></param>
        /// <param name="strIpServer"></param>
        /// <param name="nameApplication"></param>
        /// <param name="userApplication"></param>
        /// <param name="strDate"></param>
        /// <param name="strBalance"></param>
        /// <returns></returns>
        public static List<Account> GetListDataBag(string strTelephone, string strIdTransaction, string strIpServer, string nameApplication,
            string userApplication, ref string strDate, ref string strBalance)
        {
            List<Account> list = new List<Account>();

            ConsultDataPrePostWS.ConsultaDatosPrePostWSService service = new ConsultDataPrePostWS.ConsultaDatosPrePostWSService();

            service.Url = WebServiceConfiguration.DataPrePostWS.Url;
            service.Credentials = System.Net.CredentialCache.DefaultCredentials;
            service.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

            ConsultDataPrePostWS.auditRequestType audit = new ConsultDataPrePostWS.auditRequestType();
            audit.idTransaccion = strIdTransaction;
            audit.ipAplicacion = strIpServer;
            audit.nombreAplicacion = nameApplication;
            audit.usuarioAplicacion = userApplication;

            ConsultDataPrePostWS.consultarDatosPrepagoRequest request = new ConsultDataPrePostWS.consultarDatosPrepagoRequest();
            request.msisdn = strTelephone;
            request.tipoConsulta = KEY.AppSettings("gTypeConsultDataPrePostWS");
            request.auditRequest = audit;
             
            ConsultDataPrePostWS.operacionesType[] op = new ConsultDataPrePostWS.operacionesType[1];
            op[0] = new ConsultDataPrePostWS.operacionesType();
            op[0].codigoOperacion = KEY.AppSettings("gOperationCodeDataPrePostWS");
            op[0].listaModificador = GenerateMod();

            request.listaOperacionesConsulta = op;

            ConsultDataPrePostWS.consultarDatosPrepagoResponse response = Claro.Web.Logging.ExecuteMethod<ConsultDataPrePostWS.consultarDatosPrepagoResponse>("S", "T", () =>
            {
                return service.consultarDatosPrepagoSiac(request);
            });

            if (response.auditResponse.codigoRespuesta.Equals("0"))
            {
                for (int i = 0; i < response.listaOperacionesRespuesta[0].listaModificador.Length; i++)
                {
                    ConsultDataPrePostWS.modificadorType modificador = new ConsultDataPrePostWS.modificadorType();
                    modificador = response.listaOperacionesRespuesta[0].listaModificador[i];

                    if (modificador.nombreModificador.Equals("ROP"))
                    {
                        ConsultDataPrePostWS.parametroType[] paramentros;
                        ConsultDataPrePostWS.parametroListaType[] paramentrosListaM;

                        paramentros = modificador.parametro;
                        paramentrosListaM = modificador.parametroLista;

                        for (int e = 0; e < paramentros.Length; e++)
                        {
                            if (paramentros[e].nombre.Equals("ActiveEndDate"))
                            {
                                strDate = paramentros[e].valor;
                                break;
                            }
                        }

                        for (int j = 0; j < paramentrosListaM.Length; j++)
                        {

                            if (paramentrosListaM[j].nombre.Equals("OnPeakAccountID_FU"))
                            {
                                ConsultDataPrePostWS.parametroType[] paramentrosM1;
                                paramentrosM1 = paramentrosListaM[j].parametro;
                                for (int u = 0; u < paramentrosM1.Length; u++)
                                {
                                    if (paramentrosM1[u].nombre.Equals("Balance"))
                                    {
                                        strBalance = paramentrosM1[u].valor;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else if (modificador.nombreModificador.Equals("RPP"))
                    {
                        ConsultDataPrePostWS.parametroType[] paramentros;
                        ConsultDataPrePostWS.parametroListaType[] paramentrosListaM;

                        paramentros = modificador.parametro;
                        paramentrosListaM = modificador.parametroLista;

                        Account entity = new Account();
                        for (int e = 0; e < paramentros.Length; e++)
                        {
                            if (paramentros[e].nombre.Equals("NombreComercial"))
                                entity.Name = paramentros[e].valor;
                            else if (paramentros[e].nombre.Equals("Orden"))
                                entity.Order = paramentros[e].valor; 
                            else if (paramentros[e].nombre.Equals("FechaVigenciaBolsa")) 
                                entity.ExpirationDate = paramentros[e].valor; 
                        }

                        for (int j = 0; j < paramentrosListaM.Length; j++)
                        {
                            if (paramentrosListaM[j].nombre.Equals("s_PeriodicBonus_FU"))
                            {
                                ConsultDataPrePostWS.parametroType[] paramentrosM1;
                                paramentrosM1 = paramentrosListaM[j].parametro;
                                for (int u = 0; u < paramentrosM1.Length; u++)
                                {
                                    if (paramentrosM1[u].nombre.Equals("Balance"))
                                    {
                                        entity.Balance = paramentrosM1[u].valor;
                                        break;
                                    }
                                }
                                break;
                            }
                        }

                        list.Add(entity);
                    }
                }
            }

            return list;
        } 
        #endregion

        #region Funciones Privadas
        private static ConsultDataPrePostWS.modificadorType[] GenerateMod() {
            ConsultDataPrePostWS.modificadorType[] mod = new ConsultDataPrePostWS.modificadorType[3];

            ConsultDataPrePostWS.parametroType[] param1 = new ConsultDataPrePostWS.parametroType[4];
            param1[0] = new ConsultDataPrePostWS.parametroType();
            param1[0].nombre = "billCycleId";
            param1[1] = new ConsultDataPrePostWS.parametroType();
            param1[1].nombre = "billCycleIdAfterSwitch";
            param1[2] = new ConsultDataPrePostWS.parametroType();
            param1[2].nombre = "billCycleSwitch";
            param1[3] = new ConsultDataPrePostWS.parametroType();
            param1[3].nombre = "category";

            mod[0] = new ConsultDataPrePostWS.modificadorType();
            mod[0].nombreModificador = "Customer";
            mod[0].parametro = param1;

            ConsultDataPrePostWS.parametroType[] param2 = new ConsultDataPrePostWS.parametroType[6];
            param2[0] = new ConsultDataPrePostWS.parametroType();
            param2[0].nombre = "ActiveEndDate";
            param2[1] = new ConsultDataPrePostWS.parametroType();
            param2[1].nombre = "GraceEndDate";
            param2[2] = new ConsultDataPrePostWS.parametroType();
            param2[2].nombre = "IsMTCLockUsed";
            param2[3] = new ConsultDataPrePostWS.parametroType();
            param2[3].nombre = "s_CRMTitle";
            param2[4] = new ConsultDataPrePostWS.parametroType();
            param2[4].nombre = "s_OfferId";
            param2[5] = new ConsultDataPrePostWS.parametroType();
            param2[5].nombre = "OnPeakAccountID_FU";

            mod[1] = new ConsultDataPrePostWS.modificadorType();
            mod[1].nombreModificador = "ROP";
            mod[1].parametro = param2;


            ConsultDataPrePostWS.parametroType[] paramLista1_param1 = new ConsultDataPrePostWS.parametroType[1];
            paramLista1_param1[0] = new ConsultDataPrePostWS.parametroType();
            paramLista1_param1[0].nombre = "ExpiryDate";

            ConsultDataPrePostWS.parametroType[] paramLista1_param2 = new ConsultDataPrePostWS.parametroType[1];
            paramLista1_param2[0] = new ConsultDataPrePostWS.parametroType();
            paramLista1_param2[0].nombre = "Balance";

            ConsultDataPrePostWS.parametroListaType[] paramLista1 = new ConsultDataPrePostWS.parametroListaType[2];
            paramLista1[0] = new ConsultDataPrePostWS.parametroListaType();
            paramLista1[0].nombre = "s_PeriodicBonus";
            paramLista1[0].parametro = paramLista1_param1;

            paramLista1[1] = new ConsultDataPrePostWS.parametroListaType();
            paramLista1[1].nombre = "s_PeriodicBonus_FU";
            paramLista1[1].parametro = paramLista1_param2;

            ConsultDataPrePostWS.parametroType[] param3 = new ConsultDataPrePostWS.parametroType[2];
            param3[0] = new ConsultDataPrePostWS.parametroType();
            param3[0].nombre = "s_ActivationEndTime";
            param3[1] = new ConsultDataPrePostWS.parametroType();
            param3[1].nombre = "s_PackageId";

            mod[2] = new ConsultDataPrePostWS.modificadorType();
            mod[2].nombreModificador = "RPP";
            mod[2].parametroLista = paramLista1;
            mod[2].parametro = param3;

            return mod;
        }

        private static String GetLineDataContingency(ConsultLine info, ref List<Account> listAccount, ref List<ListItem> listTrio, ref Line LineItem, ref string errorMessage)
        {
            string contingencyCode = "";
            int i = 1;

            SIACPreServiceCont.INDatosPrepagoRequest request = new SIACPreServiceCont.INDatosPrepagoRequest();
            request.telefono = info.Msisdn;

            SIACPreServiceCont.INDatosPrepagoResponse response = Claro.Web.Logging.ExecuteMethod<SIACPreServiceCont.INDatosPrepagoResponse>("S", "T", Configuration.WebServiceConfiguration.PrepaidServiceContingency, () =>
            {
                return WebServiceConfiguration.PrepaidServiceContingency.leerDatosPrepago(request);
            });
            SIACPreServiceCont.DatosPrepago objPrepagoResultado = null;

            if (response.resultado.Trim() == "0")
            {
                errorMessage = "";
                objPrepagoResultado = new SIACPreServiceCont.DatosPrepago();
                LineItem.PhoneNumber = info.Msisdn;
                LineItem.MainBalance = Functions.ConvertSoles(objPrepagoResultado.onPeakAccountIDBalance);
                LineItem.ExpirationDateBalance = Functions.CheckStr(objPrepagoResultado.expiryDate);
                LineItem.LineStatus = Functions.CheckStr(objPrepagoResultado.subscriberLifeCycleStatus);
                LineItem.TriosChanguesFree = Functions.CheckStr(objPrepagoResultado.voucherRchFraudCounter);
                Claro.Web.Logging.Info("S", "T", "objPrepagoResultado.counterChangeTariffForFree: " + objPrepagoResultado.counterChangeTariffForFree);
                LineItem.TariffChangesFree = KEY.AppSettings("strCounterChangeTariffForFree");
                Claro.Web.Logging.Info("S", "T", "LineItem.CambiosTarifaGratis: " + LineItem.TariffChangesFree);
                LineItem.TariffPlan = Functions.CheckStr(objPrepagoResultado.tariffModelNumber);
                LineItem.ProviderID = Functions.CheckStr(objPrepagoResultado.providerID);

                string providerTFI = KEY.AppSettings("strProviderTFI");
                LineItem.MinuteBalance_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54Balance);
                LineItem.ExpDate_Select = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                LineItem.IsSelect = Functions.CheckStr(objPrepagoResultado.isSelect);
                if (LineItem.ProviderID == providerTFI)
                    LineItem.IsTFI = "SI";
                else
                    LineItem.IsTFI = "NO";

                LineItem.ActivationDate = Functions.CheckStr(objPrepagoResultado.firstCallDate);
                LineItem.ExpDate_Line = Functions.CheckStr(objPrepagoResultado.deletionDate);
                LineItem.DolDate = " ";
                LineItem.SubscriberStatus = Functions.CheckStr(objPrepagoResultado.subscriberStatus);
                string vAuxCNTNumber = "";
                if (Functions.CheckStr(objPrepagoResultado.cNTNumber.Trim()) != "")
                {
                    vAuxCNTNumber = Functions.CheckStr(objPrepagoResultado.cNTNumber);
                    if (vAuxCNTNumber == "0") { vAuxCNTNumber = ""; }
                }
                LineItem.CNTNumber = vAuxCNTNumber;
                LineItem.IsCNTPossible = Functions.CheckStr(objPrepagoResultado.isCNTPossible);
                LineItem.NumIMSI = Functions.CheckStr(objPrepagoResultado.imsi);
                LineItem.StatusIMSI = Functions.CheckStr(objPrepagoResultado.isLocked);

                while (LineItem.SubscriberStatus.Length < 10)
                {
                    LineItem.SubscriberStatus = LineItem.SubscriberStatus + "0";
                }
                if (LineItem.StatusIMSI.ToUpper() == "TRUE")
                {
                    LineItem.StatusIMSI = "Bloqueado";
                }
                else
                {
                    if (LineItem.StatusIMSI.ToUpper() == "FALSE")
                        LineItem.StatusIMSI = "Desbloqueado";
                    else
                        LineItem.StatusIMSI = "";
                }
                List<ItemTrans> listaValoresXML = Functions.GetListValuesXML("ListaBolsa", "2", Constant.SiacutDataPrepaidWSXML);
                double factor_division = 1;
                double saldo = 0;
                Account item = new Account();

                item.Name = "Saldo SMS";
                saldo = Functions.CheckDbl(objPrepagoResultado.sMSPromoAccountIDBalance);
                factor_division = GetFactorBag("SMSPromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSPromoAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Voice1 Promo. account";
                saldo = Functions.CheckDbl(objPrepagoResultado.voice1PromoAccountIDBalance);
                factor_division = GetFactorBag("Voice1PromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice1PromoAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Voice2 Promo. account";
                saldo = Functions.CheckDbl(objPrepagoResultado.voice2PromoAccountIDBalance);
                factor_division = GetFactorBag("Voice2PromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voice2PromoAccountIDExpiryDate);
                listAccount.Add(item);

                if (LineItem.IsTFI != "SI")
                {
                    item = new Account();
                    item.Name = "Saldo MMS";
                    saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                    factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                    item.Balance = Functions.CheckStr(saldo / factor_division);
                    item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSPromoAccountIDExpiryDate);
                    listAccount.Add(item);
                }
                else
                {
                    saldo = Functions.CheckDbl(objPrepagoResultado.mMSPromoAccountIDBalance);
                    factor_division = GetFactorBag("MMSPromoAccountID", listaValoresXML);
                    LineItem.OutstandingBalance = Functions.CheckStr(saldo / factor_division);
                }

                item = new Account();
                item.Name = "Saldo S/. Promo";
                saldo = Functions.CheckDbl(objPrepagoResultado.bonusPromoAccountIDBalance);
                factor_division = GetFactorBag("BonusPromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusPromoAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Saldo SMS Fidelizacion";
                saldo = Functions.CheckDbl(objPrepagoResultado.sMSLoyaltyAccountIDBalance);
                factor_division = GetFactorBag("SMSLoyaltyAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.sMSLoyaltyAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Saldo Voz Fidelizacion";
                saldo = Functions.CheckDbl(objPrepagoResultado.voiceLoyaltyAccountIDBalance);
                factor_division = GetFactorBag("VoiceLoyaltyAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.voiceLoyaltyAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Promo 1 (Soles)";
                saldo = Functions.CheckDbl(objPrepagoResultado.gPRSLoyaltyAccountIDBalance);
                factor_division = GetFactorBag("Bonus1PromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.gPRSLoyaltyAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Promo 2 (Soles)";
                saldo = Functions.CheckDbl(objPrepagoResultado.mMSLoyaltyAccountIDBalance);
                factor_division = GetFactorBag("Bonus2PromoAccountID", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.mMSLoyaltyAccountIDExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Bolsa Multidestino";
                saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account54Balance);
                factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account54ExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "Bolsa Min Gratis";
                saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account52Balance);
                factor_division = GetFactorBag("bonusCounterAccount54", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account52ExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "SMS Num. Frec.";
                saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account57Balance);
                factor_division = GetFactorBag("BonusCounterAccount57", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account57ExpiryDate);
                listAccount.Add(item);

                item = new Account();
                item.Name = "SEG Num. Frec.";
                saldo = Functions.CheckDbl(objPrepagoResultado.bonusCounter_Account58Balance);
                factor_division = GetFactorBag("BonusCounterAccount58", listaValoresXML);
                item.Balance = Functions.CheckStr(saldo / factor_division);
                item.ExpirationDate = Functions.CheckStr(objPrepagoResultado.bonusCounter_Account58ExpiryDate);
                listAccount.Add(item);

                ListItem itemGen;
                if (objPrepagoResultado.fnFNumber0.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber0);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber1.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber1);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber2.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber2);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber3.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber3);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber4.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber4);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber5.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber5);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber6.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber6);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber7.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber7);
                    listTrio.Add(itemGen);
                    i++;
                }
                if (objPrepagoResultado.fnFNumber8.Trim() != "")
                {
                    itemGen = new ListItem();
                    itemGen.Code = "Nro Triado " + i.ToString();
                    itemGen.Description = Functions.CheckStr(objPrepagoResultado.fnFNumber8);
                    listTrio.Add(itemGen);
                    i++;
                }
                LineItem.TriacionType = Functions.CheckStr(objPrepagoResultado.activeFnFOption);
                LineItem.NumFamFriends = listTrio.Count.ToString();
            }
            else
            {
                contingencyCode = response.resultado.Trim();
                errorMessage = response.descripcion.Trim();
            }
            Claro.Web.Logging.Info("S", "T", "Se invoco correctamente DatosPrepagoWSProxy (Contingencia), WebServiceStatus: OK");
            return contingencyCode;
        }
        private static double GetFactorBag(string bag, List<ItemTrans> listaValoresXML)
        {
            double factor = 1;
            for (int i = 0; i < listaValoresXML.Count; i++)
            {
                ItemTrans item = listaValoresXML[i];
                if (item.Code == bag)
                {
                    factor = Functions.CheckDbl(item.Code2);
                    break;
                }
            }
            return factor;
        }

        private static List<string> MappingFnfNumber(SIACPreService.DatosPrepago obj)
        {
            List<string> fnfNunmber = new List<string>();
            if (!String.IsNullOrEmpty(obj.fnFNumber0)) fnfNunmber.Add(obj.fnFNumber0);
            if (!String.IsNullOrEmpty(obj.fnFNumber1)) fnfNunmber.Add(obj.fnFNumber1);
            if (!String.IsNullOrEmpty(obj.fnFNumber2)) fnfNunmber.Add(obj.fnFNumber2);
            if (!String.IsNullOrEmpty(obj.fnFNumber3)) fnfNunmber.Add(obj.fnFNumber3);
            if (!String.IsNullOrEmpty(obj.fnFNumber4)) fnfNunmber.Add(obj.fnFNumber4);
            if (!String.IsNullOrEmpty(obj.fnFNumber5)) fnfNunmber.Add(obj.fnFNumber5);
            if (!String.IsNullOrEmpty(obj.fnFNumber6)) fnfNunmber.Add(obj.fnFNumber6);
            if (!String.IsNullOrEmpty(obj.fnFNumber7)) fnfNunmber.Add(obj.fnFNumber7);
            if (!String.IsNullOrEmpty(obj.fnFNumber8)) fnfNunmber.Add(obj.fnFNumber8);
            return fnfNunmber;
        } 
        #endregion
    }
}
