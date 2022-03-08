using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPlanMigration = Claro.SIACU.Entity.Transac.Service.Postpaid;
using oTransacServ = Claro.SIACU.Transac.Service;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class MigrationPlan
    { /// <summary>
        /// 
        /// </summary>
        /// <param name="objReceiptRequest"></param>
        /// <returns></returns>
        public static OPlanMigration.GetReceipt.ReceiptResponse GetDataInvoice(OPlanMigration.GetReceipt.ReceiptRequest objReceiptRequest)
        {
            string strValidaCorreo = "";

            OPlanMigration.GetReceipt.ReceiptResponse objReceiptResponse = new OPlanMigration.GetReceipt.ReceiptResponse()
            {
                ObjReceipt = Claro.Web.Logging.ExecuteMethod<OPlanMigration.Receipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetDataInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptRequest.CustomerCode); })

            };


            if (objReceiptResponse.ObjReceipt != null)
            {

                if (!String.IsNullOrEmpty(objReceiptRequest.InvoiceNumber)) objReceiptResponse.ObjReceipt.NUMERO_RECIBO = objReceiptRequest.InvoiceNumber;

                objReceiptResponse.ObjReceipt.RECIBO_DETALLADO = Claro.Web.Logging.ExecuteMethod<OPlanMigration.DetailReceipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetDetailInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO); });

                if (objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Length > 0)
                {
                    objReceiptResponse.ObjReceipt.INVOICENUMBER = Helper.GetNumberReceipt(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO, objReceiptResponse.ObjReceipt.FECHA_EMISION);
                    objReceiptResponse.ObjReceipt.FECHA_VENCIMIENTO = objReceiptResponse.ObjReceipt.FECHA_VENCIMIENTO.Substring(0, 10);
                }
                try
                {
                    strValidaCorreo = Claro.Web.Logging.ExecuteMethod<string>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.Postpaid.ValidateMail(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptRequest.CustomerCode); });
                    objReceiptResponse.ObjReceipt.ENVIO_CORREO = strValidaCorreo.Equals(Claro.Constants.LetterA) ? true : false;
                }
                catch (Exception ex)
                {
                    objReceiptResponse.ObjReceipt.ENVIO_CORREO = false;
                    Claro.Web.Logging.Error(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, ex.Message);
                }
            }
            return objReceiptResponse;
        }

        public static OPlanMigration.GetConsumeLimit.ConsumeLimitResponse GetConsumeLimit(OPlanMigration.GetConsumeLimit.ConsumeLimitRequest objConsumeLimitRequest)
        {
            OPlanMigration.GetConsumeLimit.ConsumeLimitResponse ObjConsumeLimitResponse = null;
            List<OPlanMigration.ConsumeLimit> lstComsumeLimit = null;
            try
            {
                lstComsumeLimit = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.ConsumeLimit>>(objConsumeLimitRequest.Audit.Session, objConsumeLimitRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetConsumeLimit(objConsumeLimitRequest.Audit.Session, objConsumeLimitRequest.Audit.Transaction, objConsumeLimitRequest.Telefono, int.Parse(objConsumeLimitRequest.IdContrato)); });
                ObjConsumeLimitResponse.LstConsumeLimit = lstComsumeLimit;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLimitRequest.Audit.Session, objConsumeLimitRequest.Audit.Transaction, ex.Message);
            }
            return ObjConsumeLimitResponse;
        }

        public static OPlanMigration.GetFixedCharge.FixedChargeResponse ConsultFixedCharge(OPlanMigration.GetFixedCharge.FixedChargeRequest objRequest)
        {
            OPlanMigration.GetFixedCharge.FixedChargeResponse objFixedChargeResponse = new OPlanMigration.GetFixedCharge.FixedChargeResponse();
            string vError = string.Empty, vDescError = string.Empty;
            try
            {
                objFixedChargeResponse = new OPlanMigration.GetFixedCharge.FixedChargeResponse()
                {
                    CargoFijo = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.ConsultFixedCharge(objRequest.Audit.Session, objRequest.Audit.Transaction, int.Parse(objRequest.Flag), objRequest.Valor, out vError, out vDescError); }),
                    Error = vError,
                    DescError = vDescError,
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);

            }
            return objFixedChargeResponse;
        }

        public static OPlanMigration.GetMigrationPlans.MigrationPlanResponse GetPlansMigrations(OPlanMigration.GetMigrationPlans.MigrationPlanRequest objMigrationPlanRequest)
        {
            OPlanMigration.GetMigrationPlans.MigrationPlanResponse objMigrationPlanResponse = null;
            List<OPlanMigration.NewPlan> lstNewPlan = new List<OPlanMigration.NewPlan>();
            List<OPlanMigration.RatePlan> lstRatePlan = new List<OPlanMigration.RatePlan>();

            if (objMigrationPlanRequest.Modalidad!=""  && objMigrationPlanRequest.Familia != "")
            {

                lstNewPlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.NewPlan>>(objMigrationPlanRequest.Audit.Session, objMigrationPlanRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetNewPlans(objMigrationPlanRequest.Audit.Session, objMigrationPlanRequest.Audit.Transaction, objMigrationPlanRequest.ValorTipoProducto, objMigrationPlanRequest.CategoriaProducto, ""); });
                lstRatePlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.RatePlan>>(objMigrationPlanRequest.Audit.Session, objMigrationPlanRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetRatePlan(objMigrationPlanRequest.Audit.Session, objMigrationPlanRequest.Audit.Transaction, oTransacServ.Constants.strCeroUno, oTransacServ.Constants.strCeroUno, objMigrationPlanRequest.Modalidad, objMigrationPlanRequest.Familia); });
                if (lstNewPlan != null)
                {
                    foreach (OPlanMigration.NewPlan itemNew in lstNewPlan)
                    {
                        if (lstRatePlan != null)
                        {
                            foreach (OPlanMigration.RatePlan itemRate in lstRatePlan)
                            {
                                if (itemNew.TMCODE == itemRate.PLNV_CODIGO_BSCS)
                                {
                                    if (objMigrationPlanRequest.MigracionPlan == oTransacServ.Constants.strUno)
                                    {
                                        if (objMigrationPlanRequest.PlanActual.ToUpper().Trim() != itemNew.DESC_PLAN.ToUpper().Trim())
                                        {
                                            objMigrationPlanResponse.lstNewPlan.Add(itemNew);
                                        }
                                    }
                                    else
                                    {
                                        objMigrationPlanResponse.lstNewPlan.Add(itemNew);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return objMigrationPlanResponse;
        }

        public static OPlanMigration.GetNewPlan.NewPlanResponse GetNewPlan(OPlanMigration.GetNewPlan.NewPlanRequest objNewPlanRequest)
        {
            OPlanMigration.GetNewPlan.NewPlanResponse objNewPlanResponse = new OPlanMigration.GetNewPlan.NewPlanResponse();
            List<OPlanMigration.NewPlan> lstNewPlan = new List<OPlanMigration.NewPlan>();
            List<OPlanMigration.NewPlan> listaNewPlan = new List<OPlanMigration.NewPlan>();
            string[] datos = ConfigurationManager.AppSettings("CodPlanesTarifarioBAM").ToString().ToUpper().Trim().Split('|');

            lstNewPlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.NewPlan>>(objNewPlanRequest.Audit.Session, objNewPlanRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetNewPlans(objNewPlanRequest.Audit.Session, objNewPlanRequest.Audit.Transaction, objNewPlanRequest.ValorTipoProducto, objNewPlanRequest.CategoriaProducto, ""); });
            if (esPlanBAM(objNewPlanRequest.CodPlanTarifario.ToString().Trim()))
            {

                if (lstNewPlan != null)
                {

                    foreach (OPlanMigration.NewPlan itemNew in lstNewPlan)
                    {
                        foreach (string dato in datos)
                        {

                            if (itemNew.TMCODE == dato)
                            {
                                if (objNewPlanRequest.MigracionPlan == oTransacServ.Constants.strUno)
                                {
                                    if (objNewPlanRequest.PlanActual.ToUpper().Trim() != itemNew.DESC_PLAN.ToUpper().Trim())
                                    {
                                        listaNewPlan.Add(itemNew);
                                    }
                                }
                                else
                                {
                                    listaNewPlan.Add(itemNew);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                listaNewPlan = lstNewPlan;
            }
            if (listaNewPlan.Count > 0)
            {
                objNewPlanResponse.lstNewPlan = listaNewPlan;
            }
            return objNewPlanResponse;
        }

        public static OPlanMigration.GetFixedCostBasePlan.FixedCostBasePlanResponse GetFixedCostBasePlan(OPlanMigration.GetFixedCostBasePlan.FixedCostBasePlanRequest objRequest)
        {
            OPlanMigration.GetFixedCostBasePlan.FixedCostBasePlanResponse objResponse = new OPlanMigration.GetFixedCostBasePlan.FixedCostBasePlanResponse();
            List<OPlanMigration.NewPlan> lstNewPlan = new List<OPlanMigration.NewPlan>();
            List<OPlanMigration.TopConsumption> lstNewPlanMantenimiento = new List<OPlanMigration.TopConsumption>();
            string DescPlanOriginal="";
            double CFPlanBase = 0;
            string strResultado="";
            lstNewPlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.NewPlan>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetNewPlans(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdProduct, objRequest.CategoriaProducto, objRequest.DescriptionPlan); });
            if (lstNewPlan != null && lstNewPlan.Count>0)
            {
                DescPlanOriginal = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.GetRatePlanBSCS(objRequest.Audit.Session, objRequest.Audit.Transaction, Convert.ToInt(lstNewPlan[0].TMCODE)); }); 
            }
            lstNewPlanMantenimiento = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.SearchMaintenancePlan(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodigoProduct); });
            if (lstNewPlanMantenimiento != null && lstNewPlanMantenimiento.Count > 0)
            {
                foreach (OPlanMigration.TopConsumption item in lstNewPlanMantenimiento)
                {
                    if (item.ESTADO.Trim().ToUpper().Equals("P"))
                    {
                        CFPlanBase = CFPlanBase + Convert.ToDouble(item.CARGO_FIJO);
                        strResultado = strResultado + CFPlanBase.ToString();
                    }
                }
            }
            strResultado = strResultado + "|" + DescPlanOriginal;
            objResponse.DescriptionOrigenPlan= strResultado;

            return objResponse;
        }

        public static OPlanMigration.GetServByTransCodeProduct.ServByTransCodeProductResponse GetServByTransCodeProductResponse(OPlanMigration.GetServByTransCodeProduct.ServByTransCodeProductRequest objRequest)
        {
            OPlanMigration.GetServByTransCodeProduct.ServByTransCodeProductResponse objResponse = new OPlanMigration.GetServByTransCodeProduct.ServByTransCodeProductResponse();
            List<OPlanMigration.TopConsumption> lstNewPlanMantenimiento = new List<OPlanMigration.TopConsumption>();
            List<OPlanMigration.TopConsumption> lstNewPlanMantenimientoResponse = new List<OPlanMigration.TopConsumption>();
            double CFPlanBase = 0;
            double dblTotCF = 0;
            int count = 0;

            lstNewPlanMantenimiento = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.MigrationPlan.SearchMaintenancePlan(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodProducto); });
            if (lstNewPlanMantenimiento != null && lstNewPlanMantenimiento.Count > 0)
            {
                foreach (OPlanMigration.TopConsumption item in lstNewPlanMantenimiento)
                {
                   if (!(item.ESTADO.Trim().ToUpper().Equals("P")))
                        {
                            lstNewPlanMantenimientoResponse.Add(item);
                            dblTotCF = Convert.ToDouble(dblTotCF) + Convert.ToDouble(item.CARGO_FIJO);
                         }
                   else
                   {
                            dblTotCF = Convert.ToDouble(dblTotCF) + Convert.ToDouble(item.CARGO_FIJO);
                            CFPlanBase = CFPlanBase + Convert.ToDouble(item.CARGO_FIJO);
                         }
                   count += 1;
                    }
                }
            objResponse.CargoFijoPorPlan = CFPlanBase;
            objResponse.NroRegistro = count;
            objResponse.TotalCargoFijo = dblTotCF;
            objResponse.lstTopConsumption = lstNewPlanMantenimientoResponse;
            return objResponse;
        }
        private static bool esPlanBAM(string strCodPlanTarifario)
        {
            string[] datos = ConfigurationManager.AppSettings("CodPlanesTarifarioBAM").ToString().ToUpper().Trim().Split('|');
            bool blExiste = false;
            foreach (string dato in datos)
            {
                if (strCodPlanTarifario == dato)
                {
                    blExiste = true;
                    break;
                }
            }
            return blExiste;
        }

        public static OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationProgDeudaBloqSusp(OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest)
        {
            OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse objResponse= null;
            string[] CodEstados = KEY.AppSettings("gBloqueo_CodEstadoProgramacion").Split('|');
            List<OPlanMigration.TransactionsDetail> objTransactionsDetail = null;
            OPlanMigration.DebtPayment objDebtPayment = null;
            OPlanMigration.Suspension objSuspension = null;
            List<OPlanMigration.Block> objBlock = null;
            string mensajeError = string.Empty, strResultmensajeError = string.Empty;
            int iResult = 0;
            foreach (string codEstado in CodEstados)
            {
                if (codEstado!="")
                {
                    objTransactionsDetail = Claro.Web.Logging.ExecuteMethod < List<OPlanMigration.TransactionsDetail>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => 
                                            { 
                                            return Data.Transac.Service.Postpaid.MigrationPlan.GetTransactionDetail(objRequest.Audit.Session, objRequest.Audit.Transaction, 
                                            objRequest.Audit.ApplicationName, objRequest.Audit.IPAddress, objRequest.Audit.UserName,
                                            objRequest.Telefono, string.Empty, string.Empty, codEstado, string.Empty, string.Empty, string.Empty, string.Empty, out mensajeError);
                    });

                    if (objTransactionsDetail.Count>0)
                    {
                        iResult = 1;
                        strResultmensajeError = "Existen Programaciones Pendientes";
                        break;
                    }
                }
            }


            if (iResult==0)
            {
                objDebtPayment =  Claro.Web.Logging.ExecuteMethod<OPlanMigration.DebtPayment>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Postpaid.MigrationPlan.GetDebtPayment(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                            objRequest.Audit.Transaction, objRequest.Contrato);
                    });
                if (objDebtPayment.RESULTADO=="0")
                {
                    iResult = 1;
                    strResultmensajeError = "Existe Deuda de Pago";
                }
                else if (objDebtPayment.RESULTADO == "1")
                {
                    iResult = 0;
                    strResultmensajeError = "No existe el número telefónico";
                }
            }


            if (iResult==0)
            {
                objSuspension=Claro.Web.Logging.ExecuteMethod<OPlanMigration.Suspension>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Postpaid.MigrationPlan.ValidateLockSuspension(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Contrato);
                    });
                if (objSuspension.CODIGO=="0")
                {
                    strResultmensajeError = objSuspension.DESCRIPCION;
                    iResult = Convert.ToInt(objSuspension.CODIGO);
                }
            }


            if (iResult==2)
            {
                bool blResult= false;
                string strBlock=string.Empty;
                objBlock = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.Block>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Postpaid.MigrationPlan.GetBlock(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Contrato);
                    });
                foreach (OPlanMigration.Block item in objBlock)
                {
                    if (!(item.DESCRIPCION.ToUpper().Equals(KEY.AppSettings("strBloqCob"))))
                    {
                        blResult= true;
                        strBlock=item.DESCRIPCION.ToUpper();
                        break;
                    }
                }
           string varBloqPermitido= KEY.AppSettings("gConstBloqueos_PermitidosCTC");
           string varSusPermitido = KEY.AppSettings("gConstSuspensiones_PermitidasCTC");

               foreach (OPlanMigration.Block item2 in objBlock)
               {
                   if ((varBloqPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno) || 
                       (varSusPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno))
                   {
                       blResult= false;
                   }
                   else
	                {
                      blResult= true;
                        break;
	                }
               }

               if (blResult==true)
               {
                   strResultmensajeError = KEY.AppSettings("strBloqueSuspensionContrato")+";" + strBlock;
                   iResult = 2;
               }
               else
               {
                   strResultmensajeError = string.Empty;
                   iResult = 0;
               }

            }

            objResponse.RespuestaValidacion = iResult.ToString() + "|" + strResultmensajeError;

            return objResponse;
        }

        public static OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationMigration(OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest)
        {
            OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse objResponse = new OPlanMigration.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse();
            string[] CodEstados = KEY.AppSettings("gBloqueo_CodEstadoProgramacion").Split('|');
            List<OPlanMigration.TransactionsDetail> objTransactionsDetail = null;
            OPlanMigration.DebtPayment objDebtPayment = null;
            OPlanMigration.Suspension objSuspension = null;
            List<OPlanMigration.Block> objBlock = null;
            string mensajeError = string.Empty, strResultmensajeError = string.Empty;
            int iResult = 0;
            foreach (string codEstado in CodEstados)
            {
                if (codEstado != "")
                {
                    objTransactionsDetail = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.TransactionsDetail>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Postpaid.MigrationPlan.GetTransactionDetail(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.Audit.ApplicationName, objRequest.Audit.IPAddress, objRequest.Audit.UserName,
                        objRequest.Telefono, string.Empty, string.Empty, codEstado, string.Empty, string.Empty, string.Empty, string.Empty, out mensajeError);
                    });

                    if (objTransactionsDetail.Count > 0)
                    {
                        iResult = 1;
                        strResultmensajeError = "Existen Programaciones Pendientes";
                        break;
                    }
                }
            }


            if (iResult == 0)
            {
                objDebtPayment = Claro.Web.Logging.ExecuteMethod<OPlanMigration.DebtPayment>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.GetDebtPayment(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                        objRequest.Audit.Transaction, objRequest.Contrato);
                });
                if (objDebtPayment!=null)
                {
                    if (objDebtPayment.RESULTADO == "0")
                    {
                        iResult = 1;
                        strResultmensajeError = "Existe Deuda de Pago";
                    }
                    else if (objDebtPayment.RESULTADO == "1")
                    {
                        iResult = 0;
                        strResultmensajeError = "No existe el número telefónico";
                    }
                }
            }


            if (iResult == 0)
            {
                objSuspension = Claro.Web.Logging.ExecuteMethod<OPlanMigration.Suspension>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.ValidateLockSuspension(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Contrato);
                });
                if (objSuspension!=null)
                {
                    if (objSuspension.CODIGO == "0")
                    {
                        strResultmensajeError = objSuspension.DESCRIPCION;
                        iResult = Convert.ToInt(objSuspension.CODIGO);
                    }
                }
            }


            if (iResult == 2)
            {
                bool blResult = false;
                objBlock = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.Block>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.GetBlock(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Contrato);
                });
                foreach (OPlanMigration.Block item in objBlock)
                {
                    if (!(item.DESCRIPCION.ToUpper().Equals(KEY.AppSettings("strBloqCob"))))
                    {
                        blResult = true;
                        break;
                    }
                }
                if (objRequest.PRY=="CTC")
                {
                    string varBloqPermitido = KEY.AppSettings("gConstBloqueos_PermitidosCTC");
                    string varSusPermitido = KEY.AppSettings("gConstSuspensiones_PermitidasCTC");
                    foreach (OPlanMigration.Block item2 in objBlock)
                    {
                        if ((varBloqPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno) ||
                            (varSusPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno))
                        {
                            blResult = false;
                        }
                        else
                        {
                            blResult = true;
                            break;
                        }
                    }
                }
                if (objRequest.PRY=="MKV")
                {
                    string varBloqPermitido = KEY.AppSettings("gConstBloqueos_Permitidos");
                    string varSusPermitido = KEY.AppSettings("gConstSuspensiones_Permitidas");
                    foreach (OPlanMigration.Block item2 in objBlock)
                    {
                        if ((varBloqPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno) ||
                            (varSusPermitido.IndexOf(item2.DESCRIPCION.ToUpper()) != oTransacServ.Constants.PresentationLayer.kitracVariableMenosUno))
                        {
                            blResult = false;
                        }
                        else
                        {
                            blResult = true;
                            break;
                        }
                    }
                }


                if (blResult == true)
                {
                    strResultmensajeError = "Existe Bloque/Suspensión en el Contrato";
                    iResult = 1;
                }
                else
                {
                    strResultmensajeError = string.Empty;
                    iResult = 0;
                }

            }

            objResponse.RespuestaValidacion = iResult.ToString() + ";" + strResultmensajeError;

            return objResponse;
        }

        public static OPlanMigration.GetAgreement.AgreementResponse GetReinstatementEquipment(OPlanMigration.GetAgreement.AgreementResquest objRequest)
        {
            OPlanMigration.GetAgreement.AgreementResponse objResponse = new OPlanMigration.GetAgreement.AgreementResponse();
            OPlanMigration.Agreement objAgreement;
            string strCodigoRespuesta="",strMensajeRespuesta="";
            bool blRespuesta;
                 objAgreement = Claro.Web.Logging.ExecuteMethod<OPlanMigration.Agreement>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => 
                                            {
                                                return Data.Transac.Service.Postpaid.MigrationPlan.ReinstatementEquipment(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                objRequest.Audit.ApplicationName, objRequest.Audit.IPAddress, objRequest.Audit.UserName,
                                                objRequest.msisdn, objRequest.CoId, objRequest.FechaTransaccion, objRequest.CargoFijoNuevo, objRequest.MontoApadece, objRequest.FlagEquipo,
                                                out strCodigoRespuesta, out strMensajeRespuesta, out blRespuesta);
                                            });
                 if (objAgreement!= null)
                 {
                     
                     objResponse = new OPlanMigration.GetAgreement.AgreementResponse() {
                         ACUERDO_CADUCADO = objAgreement.ACUERDO_CADUCADO,
                         ACUERDO_ESTADO = objAgreement.ACUERDO_ESTADO,
                         ACUERDO_FECHA_FIN = objAgreement.ACUERDO_FECHA_FIN,
                         ACUERDO_FECHA_INICIO = objAgreement.ACUERDO_FECHA_INICIO,
                         ACUERDO_ID = objAgreement.ACUERDO_ID,
                         ACUERDO_MONTO_APADECE_TOTAL = objAgreement.ACUERDO_MONTO_APADECE_TOTAL,
                         ACUERDO_ORIGEN = objAgreement.ACUERDO_ORIGEN,
                         ACUERDO_VIGENCIA_MES = objAgreement.ACUERDO_VIGENCIA_MES,
                         CARGO_FIJO_DIARIO = objAgreement.CARGO_FIJO_DIARIO,
                         CO_ID = objAgreement.CO_ID,
                         CODIGO_PLAZO_ACUERDO = objAgreement.CODIGO_PLAZO_ACUERDO,
                         CUSTOMER_ID = objAgreement.CUSTOMER_ID,
                         DESCRIPCION_ESTADO_ACUERDO = objAgreement.DESCRIPCION_ESTADO_ACUERDO,
                         DESCRIPCION_PLAZO_ACUERDO = objAgreement.DESCRIPCION_PLAZO_ACUERDO,
                         DIAS_BLOQUEO = objAgreement.DIAS_BLOQUEO,
                         DIAS_PENDIENTES = objAgreement.DIAS_PENDIENTES,
                         DIAS_VIGENCIA = objAgreement.DIAS_VIGENCIA,
                         FIN_VIGENCIA_REAL = objAgreement.FIN_VIGENCIA_REAL,
                         MESES_ANTIGUEDAD = objAgreement.MESES_ANTIGUEDAD,
                         MESES_PENDIENTES = objAgreement.MESES_PENDIENTES,
                         MONTO_APADECE = objAgreement.MONTO_APADECE,
                         PENALIDAD = objAgreement.PENALIDAD,
                         PRECIO_LISTA = objAgreement.PENALIDAD,
                         PRECIO_VENTA = objAgreement.PRECIO_VENTA,
                         MesajeRespuesta=strMensajeRespuesta,
                         CodRespuesta=strCodigoRespuesta,
                     };
                 }

            return objResponse;
        }
        public static OPlanMigration.GetMaintenancePlan.MaintenancePlanResponse GetPlansServices(OPlanMigration.GetMaintenancePlan.MaintenancePlanRequest objRequest)
        {
            OPlanMigration.GetMaintenancePlan.MaintenancePlanResponse objResponse = new OPlanMigration.GetMaintenancePlan.MaintenancePlanResponse();
            List<OPlanMigration.MaintenancePlan> objMaintenancePlan;
            objMaintenancePlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.MaintenancePlan>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.MigrationPlan.GetPlansServices(objRequest.Audit.Session, objRequest.Audit.Transaction,objRequest.Tmcode);
            });
            objResponse.FlgTopeAutomatico = 0;
            objResponse.FlgCincoSoles = 0;
            objResponse.FlgAdicionales = 0;
            if (objMaintenancePlan != null)
            {
                if (objMaintenancePlan.Count>0)
	            {
		        foreach (OPlanMigration.MaintenancePlan item in objMaintenancePlan)
	                {
                        if (KEY.AppSettings("OpcTopeConsumoAutomatico") == item.CO_SER.ToString())
                         {
                             objResponse.FlgTopeAutomatico = 1;
                         }
                        if (KEY.AppSettings("OpcTopeConsumo5soles") == item.CO_SER.ToString())
                        {
                            objResponse.FlgCincoSoles = 1;
                        }
                        if (KEY.AppSettings("OpcTopeConsumoAdicional") == item.CO_SER.ToString())
                        {
                            objResponse.FlgAdicionales = 1;
                        } 
	                }
                     
	            }
               
            }

            return objResponse;
        }

        public static OPlanMigration.GetValidateBagShare.ValidateBagShareResponse GetValidateBagShare(OPlanMigration.GetValidateBagShare.ValidateBagShareRequest objRequest) 
        {
            OPlanMigration.GetValidateBagShare.ValidateBagShareResponse objResponse = new OPlanMigration.GetValidateBagShare.ValidateBagShareResponse();
            List<OPlanMigration.ListItem> objListItem;
            string strCustCode = string.Empty, strRPT = string.Empty, strMSGERR = string.Empty, strNroCuenta = string.Empty;
            int intResultado=0;
            objListItem = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.MigrationPlan.GetValidateBagShare(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Contrato, out strCustCode, out strRPT, out intResultado, out  strMSGERR, out  strNroCuenta);
            });
            objResponse.CustCode = strCustCode;
            objResponse.NroCuenta = strNroCuenta;
            objResponse.Resultado = intResultado;
            objResponse.Mensaje = strMSGERR;
            objResponse.RPT = strRPT;
            if (objListItem != null)
            {
                if (objListItem.Count > 0)
                {
                    objResponse.lstListItem = objListItem;
                }

            }

            return objResponse;
        }

        public static OPlanMigration.GetValidateProgByProduct.ValidateProgByProductResponse GetValidateProgByProduct(OPlanMigration.GetValidateProgByProduct.ValidateProgByProductRequest objRequest)
        {
            OPlanMigration.GetValidateProgByProduct.ValidateProgByProductResponse objResponse = new OPlanMigration.GetValidateProgByProduct.ValidateProgByProductResponse();
            string strESTADO = Claro.SIACU.Transac.Service.Constants.strUno;
            string strServicio=KEY.AppSettings("ParamPROGRAMACIONSERV");
            string strERRORCODE= string.Empty, strERRORMSG= string.Empty;
            bool res=Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.MigrationPlan.GetValidateProgByProduct(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Cuenta, string.Empty, objRequest.ArrayCodProd, strServicio, strESTADO, out  strERRORCODE, out strERRORMSG);
            });

            if (strERRORCODE.Trim()== Claro.SIACU.Transac.Service.Constants.strCero)
            {
                objResponse.Result = false;
                objResponse.ErrorMensagge = strERRORMSG;
                objResponse.ErrorCode = strERRORCODE;
            }
            else if (strERRORCODE.Trim()== Claro.SIACU.Transac.Service.Constants.strUno)
            {
                objResponse.Result = true;
                objResponse.ErrorCode = strERRORCODE;
                objResponse.ErrorMensagge = strERRORMSG;      
            }

            return objResponse;
        }

        public static OPlanMigration.GetRegisterPlanService.RegisterPlanResponse RegisterPlanService(OPlanMigration.GetRegisterPlanService.RegisterPlanRequest objRequest)
        {
            string strMensaje;
            OPlanMigration.GetRegisterPlanService.RegisterPlanResponse objResponse = new OPlanMigration.GetRegisterPlanService.RegisterPlanResponse()
            {
                CodRegServicioPlan = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.RegisterPlanService(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ID_INTERACCION, objRequest.COD_SERVICIO, objRequest.DES_SERVICIO, objRequest.MOTIVO_EXCLUYE, objRequest.CARGO_FIJO, objRequest.PERIODO,
                        objRequest.USUARIO, out strMensaje);
                })
            };
               
            return objResponse;
            }

        public static OPlanMigration.GetProgramerMigration.ProgramerMigrationResponse ProgramerMigrationControlPostPago(OPlanMigration.GetProgramerMigration.ProgramerMigrationRequest objRequest)
        {
            string strMensaje=string.Empty;
            string FlagResult=string.Empty;
            int iResult=0;
            bool respuesta = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.ProgramerMigrationControlPostPago(objRequest,out FlagResult,out strMensaje,out iResult);
                });

            OPlanMigration.GetProgramerMigration.ProgramerMigrationResponse objResponse = new OPlanMigration.GetProgramerMigration.ProgramerMigrationResponse()
            {
                CodResult=FlagResult,
                MenssageResult=strMensaje
            };
            objResponse.IResultado = iResult;
            return objResponse;
            }
        
        public static OPlanMigration.GetDataByContract.DataByContractResponse GetDataByContract(OPlanMigration.GetDataByContract.DataByContractRequest objRequest)
        {
            OPlanMigration.GetDataByContract.DataByContractResponse objResponse = new OPlanMigration.GetDataByContract.DataByContractResponse();
            string FlagResult =string.Empty;
            string strMensaje = string.Empty;
            List<OPlanMigration.DataByContract> lstDataContract=new List<OPlanMigration.DataByContract>();
            List<OPlanMigration.DataByContractInfo> lstDataContractInfo=new List<OPlanMigration.DataByContractInfo>();
            bool respuesta = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.GetDataByContract(objRequest.Audit.Session,objRequest.Audit.Transaction,
                        Convert.ToInt(objRequest.CustomerId), Convert.ToInt(objRequest.CoId), out lstDataContract,out lstDataContractInfo, out FlagResult, out strMensaje);
                });

            objResponse.lstDataContract = lstDataContract;
            objResponse.lstDataContractInfo = lstDataContractInfo;
            return objResponse;
        }
        public static OPlanMigration.GetDataByCount.DataByCountResponse GetDataByCount(OPlanMigration.GetDataByCount.DataByCountRequest objRequest)
        { 
            OPlanMigration.GetDataByCount.DataByCountResponse objResponse=new OPlanMigration.GetDataByCount.DataByCountResponse();
            List<OPlanMigration.DataByCount> lstDataCount = new List<OPlanMigration.DataByCount>();
            List<OPlanMigration.DataByContractInfo> lstDataContractInfo= new List<OPlanMigration.DataByContractInfo>();
            int respuesta = Claro.Web.Logging.ExecuteMethod<int>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.MigrationPlan.GetDataByCount(objRequest.Audit.Session,objRequest.Audit.Transaction,
                        Convert.ToInt(objRequest.CustomerId),out lstDataCount,out lstDataContractInfo);
                });

            objResponse.lstDataCount = lstDataCount;
            objResponse.lstDataContractInfo = lstDataContractInfo;
            return objResponse;
            }

        public static OPlanMigration.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse MigrationPlans(OPlanMigration.GetExecuteMigrationPlan.ExecuteMigrationPlanRequest objRequest)
        {
            OPlanMigration.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse objResponse = new OPlanMigration.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse();
            string CodRespuestaReturn= string.Empty;
            string Mensaje = string.Empty;
            bool respuesta = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.MigrationPlan.MigrationPlans(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    objRequest.Audit.IPAddress,objRequest.Audit.ApplicationName,objRequest.Msisdn,objRequest.CoId,
                    objRequest.CustomerId,objRequest.Cuenta,objRequest.Escenario,objRequest.TipoProducto,objRequest.ServiciosAdicionales,objRequest.CodigoProducto,
                    objRequest.CodPlanBase,objRequest.MontoApadece,objRequest.MontoFidelizar,objRequest.FlagValidaApadece,objRequest.FlagAplicaApadece,
                    objRequest.TopeConsumo,objRequest.TipoTope,objRequest.DescripcionTipoTpe,objRequest.TipoRegistroTope,objRequest.TopeControlConsumo,
                    objRequest.FechaProgramacionTope,objRequest.CAC,objRequest.Asesor,objRequest.CodigoInteraccion,objRequest.MontoPCS,objRequest.AreaPCS,objRequest.MotivoPCS,
                    objRequest.SubMotivoPCS, objRequest.CicloFacturacion, objRequest.IdTipoCliente, objRequest.NumeroDocumento,objRequest.FlagServicioOnTop,
                    objRequest.FechaProgramacion,objRequest.FlagLimiteCredito,objRequest.TipoClarify, objRequest.NumeroCuentaPadre,objRequest.UsuarioAplicacion,
                    objRequest.UsuarioSistema, out CodRespuestaReturn, out Mensaje);
            });
            objResponse.CodResult=CodRespuestaReturn;
            objResponse.MenssageResult = Mensaje;

            return objResponse;
        }
        
        }
    }
    

