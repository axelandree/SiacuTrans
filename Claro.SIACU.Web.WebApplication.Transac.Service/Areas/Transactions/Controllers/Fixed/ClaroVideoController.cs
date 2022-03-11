using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using MODELS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using ConstantsFixed = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using FIXED = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper;
using HELPER_ITEM_GRID = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid;
using AutoMapper;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Linq;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
//Luis D
using Claro.Web;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper;
//Luis D
using PrepaidService = Claro.SIACU.Web.WebApplication.Transac.Service.PreTransacService;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta;
using serviceAmco = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Fixed
{
    public class ClaroVideoController : Controller
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        //
        // GET: /Transactions/ClaroVideo/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SuscripcionClaroVideo()
        {           
            //INICIATIVA-794
            ViewBag.LstValidacionIPTV = KEY.AppSettings("LstValidacionIPTV");

            return PartialView();
        }
        public ActionResult ViewServiceAdditional()
        {
            return PartialView();
        }

        public ActionResult HistoryActivateService(string strDescripcion, string strSession, string strMobile)
        {
            ViewBag.descripcion = strDescripcion;
            ViewBag.session = strSession;
            ViewBag.mobile = strMobile;
            return PartialView();
        }
        public ActionResult HistoryRentalUser(string strCustomerID)
        {
            ViewBag.CustomerID = strCustomerID;
            return PartialView();
        }
        public ActionResult HistoryVisualizationClient(string stridRefSuscripcionHistory,string strCustomerID)
        {
            ViewBag.idRefSuscripcionHistory = stridRefSuscripcionHistory;
            ViewBag.CustomerID = strCustomerID;
            return PartialView();
        }
        public ActionResult ViewHistoryDevice(string strIdSession, string strMobile, string strType)
        {
            ViewBag.Session = strIdSession;
            ViewBag.Telefono = strMobile;
            ViewBag.Type = strType;

            return PartialView();
        }
        public JsonResult ConsultSN(string strIdSession, WebApplication.Transac.Service.FixedTransacService.ConsultSNRequest objqueryOttRequest)
        {
            ConsultSNResponse objConsultSNResponse = new ConsultSNResponse();
            ConsultSNRequest objConsultSNRequest = new ConsultSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultSNMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.ConsultSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("consultarSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.ConsultSNBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultSNBodyRequest()
            {
                queryOttRequest = new QueryOttRequest()
                {
                    invokeMethod = objqueryOttRequest.MessageRequest.Body.queryOttRequest.invokeMethod,
                    correlatorId = objqueryOttRequest.MessageRequest.Body.queryOttRequest.correlatorId,
                    countryId = objqueryOttRequest.MessageRequest.Body.queryOttRequest.countryId,
                    employeeId = objqueryOttRequest.MessageRequest.Body.queryOttRequest.employeeId,
                    origin = objqueryOttRequest.MessageRequest.Body.queryOttRequest.origin,
                    serviceName = objqueryOttRequest.MessageRequest.Body.queryOttRequest.serviceName,
                    providerId = objqueryOttRequest.MessageRequest.Body.queryOttRequest.providerId,
                    startDate = objqueryOttRequest.MessageRequest.Body.queryOttRequest.startDate,
                    endDate = objqueryOttRequest.MessageRequest.Body.queryOttRequest.endDate,
                    iccidManager = objqueryOttRequest.MessageRequest.Body.queryOttRequest.iccidManager,
                    extensionInfo = (objqueryOttRequest.MessageRequest.Body.queryOttRequest.extensionInfo == null ? (new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>()) : objqueryOttRequest.MessageRequest.Body.queryOttRequest.extensionInfo)
                    // extraData = objqueryOttRequest.MessageRequest.Body.queryOttRequest.extraData

                }
            };

            objConsultSNRequest.MessageRequest.Body = objBodyRequest;
            objConsultSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                objConsultSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ConsultSNResponse>(() =>
                {
                    return _oServiceFixed.ConsultSN(objConsultSNRequest);
                });

            }
            catch (Exception ex)
            {
                objConsultSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objConsultSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objConsultSNRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.ConsultSN objConsultSN = new Models.Fixed.ConsultSN();


            if (objConsultSNResponse.MessageResponse != null)
            {
                if (objConsultSNResponse.MessageResponse.Body != null)
                {
                    if (objConsultSNResponse.MessageResponse.Body.queryOttResponse != null)
                    {
                        objConsultSN.queryOttResponse = new HELPER_ITEM.QueryOttResponseHelper();
                        objConsultSN.queryOttResponse.resultCode = objConsultSNResponse.MessageResponse.Body.queryOttResponse.resultCode;
                        objConsultSN.queryOttResponse.resultMessage = objConsultSNResponse.MessageResponse.Body.queryOttResponse.resultMessage;
                        objConsultSN.queryOttResponse.correlatorId = objConsultSNResponse.MessageResponse.Body.queryOttResponse.correlatorId;


                        HELPER_ITEM.PackageHelper oPackageHelper = new HELPER_ITEM.PackageHelper();
                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.packages != null)
                        {

                            List<HELPER_ITEM.PackageItemHelper> lstPackageItemHelper = new List<HELPER_ITEM.PackageItemHelper>();
                            HELPER_ITEM.PackageItemHelper obPackageItemHelper = new HELPER_ITEM.PackageItemHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.PackageItem itemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.packages.item)
                            {
                                lstPackageItemHelper.Add(new HELPER_ITEM.PackageItemHelper()
                                {
                                    price = itemE.price,
                                    packageId = itemE.packageId,
                                    currency = itemE.currency,
                                    name = itemE.name,
                                    acronym = itemE.acronym,
                                    FechaRegistro = "",
                                    Estado = "Desactivado",
                                    Periodo = "",
                                    Servicio = ""
                                });
                            }
                            oPackageHelper.item = lstPackageItemHelper;
                        }
                        objConsultSN.queryOttResponse.packages = new HELPER_ITEM.PackageHelper();
                        objConsultSN.queryOttResponse.packages = oPackageHelper;



                        //lista rentas
                        List<HELPER_ITEM.RentHelper> oListRentHelper = new List<HELPER_ITEM.RentHelper>();
                        HELPER_ITEM.RentHelper oRentHelper = new HELPER_ITEM.RentHelper();
                        List<HELPER_ITEM_GRID.ListRentalUser> oListRentalUser = new List<HELPER_ITEM_GRID.ListRentalUser>();

                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.rentList != null)
                        {
                            if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.rentList.rent != null)
                            {
                                foreach (WebApplication.Transac.Service.FixedTransacService.rent RentitemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.rentList.rent)
                                {

                                    List<HELPER_ITEM.RentItemHelper> lstRentItemHelper = new List<HELPER_ITEM.RentItemHelper>();
                                    HELPER_ITEM.RentItemHelper oRentItemHelper = new HELPER_ITEM.RentItemHelper();

                                    oRentHelper = new HELPER_ITEM.RentHelper();
                                    if (RentitemE.item != null)
                                    {
                                        HELPER_ITEM_GRID.ListRentalUser objListRentalUser = new HELPER_ITEM_GRID.ListRentalUser();

                                        foreach (WebApplication.Transac.Service.FixedTransacService.RentItem itemE in RentitemE.item)
                                        {
                                            objListRentalUser.GetType().GetProperty(itemE.key).SetValue(objListRentalUser, itemE.value, null);

                                            lstRentItemHelper.Add(new HELPER_ITEM.RentItemHelper()
                                            {
                                                key = itemE.key,
                                                value = itemE.value
                                            });
                                        }
                                        oListRentalUser.Add(objListRentalUser);

                                        oRentHelper.item = lstRentItemHelper;
                                        oListRentHelper.Add(oRentHelper);
                                    }


                                }
                            }
                        }

                        objConsultSN.queryOttResponse.rentList = new HELPER_ITEM.RentListHelper();
                        objConsultSN.queryOttResponse.rentList.rent = oListRentHelper;
                        objConsultSN.queryOttResponse.rentList.ListRentalUser = oListRentalUser;

                        //lista visualizaciones
                        List<HELPER_ITEM.VisualizationHelper> oListVisualizationHelper = new List<HELPER_ITEM.VisualizationHelper>();
                        HELPER_ITEM.VisualizationHelper oVisualizationHelper = new HELPER_ITEM.VisualizationHelper();
                        List<HELPER_ITEM_GRID.ListVisualizationUser> oListVisualizationUser = new List<HELPER_ITEM_GRID.ListVisualizationUser>();

                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.visualizationsList != null)
                        {
                            if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.visualizationsList.visualization != null)
                            {
                                foreach (WebApplication.Transac.Service.FixedTransacService.visualizations visualizationitemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.visualizationsList.visualization)
                                {
                                    List<HELPER_ITEM.VisualizationItemHelper> lstVisualizationItemHelper = new List<HELPER_ITEM.VisualizationItemHelper>();
                                    HELPER_ITEM.VisualizationItemHelper oVisualizationItemHelper = new HELPER_ITEM.VisualizationItemHelper();

                                    oVisualizationHelper = new HELPER_ITEM.VisualizationHelper();
                                    if (visualizationitemE.item != null)
                                    {
                                        HELPER_ITEM_GRID.ListVisualizationUser objListVisualizationUser = new HELPER_ITEM_GRID.ListVisualizationUser();

                                        foreach (WebApplication.Transac.Service.FixedTransacService.VisualizationItem itemE in visualizationitemE.item)
                                        {
                                            objListVisualizationUser.GetType().GetProperty(itemE.key).SetValue(objListVisualizationUser, itemE.value);
                                            lstVisualizationItemHelper.Add(new HELPER_ITEM.VisualizationItemHelper()
                                            {
                                                key = itemE.key,
                                                value = itemE.value
                                            });
                                        }
                                        oListVisualizationUser.Add(objListVisualizationUser);

                                        oVisualizationHelper.item = lstVisualizationItemHelper;
                                        oListVisualizationHelper.Add(oVisualizationHelper);
                                    }

                                }

                            }
                        }

                        objConsultSN.queryOttResponse.visualizationsList = new HELPER_ITEM.VisualizationsListHelper();
                        objConsultSN.queryOttResponse.visualizationsList.visualization = oListVisualizationHelper;
                        objConsultSN.queryOttResponse.visualizationsList.ListVisualizationUser = oListVisualizationUser;


                        //lista dispositivos
                        List<HELPER_ITEM.DeviceHelper> oListDeviceHelper = new List<HELPER_ITEM.DeviceHelper>();
                        HELPER_ITEM.DeviceHelper oDeviceHelper = new HELPER_ITEM.DeviceHelper();
                        List<HELPER_ITEM_GRID.ListDeviceUser> oListDeviceUser = new List<HELPER_ITEM_GRID.ListDeviceUser>();

                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.deviceList != null)
                        {
                            if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.deviceList.device != null)
                            {
                                foreach (WebApplication.Transac.Service.FixedTransacService.device deviceitemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.deviceList.device)
                                {
                                    List<HELPER_ITEM.DeviceItemHelper> lstDeviceItemHelper = new List<HELPER_ITEM.DeviceItemHelper>();
                                    HELPER_ITEM.DeviceItemHelper oDeviceItemHelper = new HELPER_ITEM.DeviceItemHelper();
                                    oDeviceHelper = new HELPER_ITEM.DeviceHelper();


                                    HELPER_ITEM_GRID.ListDeviceUser objListDeviceUser = new HELPER_ITEM_GRID.ListDeviceUser();
                                    if (deviceitemE.item != null)
                                    {
                                        foreach (WebApplication.Transac.Service.FixedTransacService.DeviceItem itemE in deviceitemE.item)
                                        {
                                            objListDeviceUser.GetType().GetProperty(itemE.key).SetValue(objListDeviceUser, itemE.value, null);

                                            lstDeviceItemHelper.Add(new HELPER_ITEM.DeviceItemHelper()
                                            {
                                                key = itemE.key,
                                                value = itemE.value
                                            });
                                        }
                                        oListDeviceUser.Add(objListDeviceUser);
                                        oDeviceHelper.item = lstDeviceItemHelper;
                                        oListDeviceHelper.Add(oDeviceHelper);
                                    }
                                }

                            }
                        }

                        objConsultSN.queryOttResponse.deviceList = new HELPER_ITEM.DeviceListHelper();
                        objConsultSN.queryOttResponse.deviceList.device = oListDeviceHelper;
                        objConsultSN.queryOttResponse.deviceList.ListDeviceUser = oListDeviceUser;


                        //lista paquetes
                        List<HELPER_ITEM.PackageLHelper> oListPackageLHelper = new List<HELPER_ITEM.PackageLHelper>();
                        HELPER_ITEM.PackageLHelper oPackageLHelper = new HELPER_ITEM.PackageLHelper();

                        List<HELPER_ITEM_GRID.ListPackageClient> oListPackageClient = new List<HELPER_ITEM_GRID.ListPackageClient>();

                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.packagesList != null)
                        {
                            if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.packagesList.package != null)
                            {
                                foreach (WebApplication.Transac.Service.FixedTransacService.packageL packagesitemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.packagesList.package)
                                {
                                    List<HELPER_ITEM.PackageLItemHelper> lstPackageLItemHelper = new List<HELPER_ITEM.PackageLItemHelper>();
                                    HELPER_ITEM.PackageLItemHelper oPackageLItemHelper = new HELPER_ITEM.PackageLItemHelper();
                                    oPackageLHelper = new HELPER_ITEM.PackageLHelper();

                                    if (packagesitemE.item != null)
                                    {
                                        HELPER_ITEM_GRID.ListPackageClient objListPackageClient = new HELPER_ITEM_GRID.ListPackageClient();
                                        foreach (WebApplication.Transac.Service.FixedTransacService.PackageLItem itemE in packagesitemE.item)
                                        {

                                            objListPackageClient.GetType().GetProperty(itemE.key).SetValue(objListPackageClient, itemE.value, null);

                                            lstPackageLItemHelper.Add(new HELPER_ITEM.PackageLItemHelper()
                                            {
                                                price = itemE.price,
                                                packageId = itemE.packageId,
                                                currency = itemE.currency,
                                                name = itemE.name,
                                                acronym = itemE.acronym,
                                                //FechaRegistro = "",
                                                //Estado = "Desactivado",
                                                //Periodo = "",
                                                //Servicio = "",
                                                key = itemE.key,
                                                value = itemE.value
                                            });
                                        }
                                        oListPackageClient.Add(objListPackageClient);

                                        oPackageLHelper.item = lstPackageLItemHelper;
                                        oListPackageLHelper.Add(oPackageLHelper);
                                    }
                                }

                            }
                        }

                        objConsultSN.queryOttResponse.packagesList = new HELPER_ITEM.PackagesListHelper();
                        objConsultSN.queryOttResponse.packagesList.package = oListPackageLHelper;
                        objConsultSN.queryOttResponse.packagesList.ListPackageClient = oListPackageClient;


                        // lista de pagos
                        HELPER_ITEM.PaymentMethodListHelper oPaymentMethodListHelper = new HELPER_ITEM.PaymentMethodListHelper();
                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.paymentMethodList != null)
                        {

                            List<HELPER_ITEM.PaymentMethodHelper> lstPaymentMethodHelper = new List<HELPER_ITEM.PaymentMethodHelper>();
                            HELPER_ITEM.PaymentMethodHelper oPaymentMethodHelper = new HELPER_ITEM.PaymentMethodHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.paymentMethod itemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.paymentMethodList.paymentMethod)
                            {
                                lstPaymentMethodHelper.Add(new HELPER_ITEM.PaymentMethodHelper()
                                {
                                    id = itemE.id,
                                    description = itemE.description

                                });
                            }
                            oPaymentMethodListHelper.paymentMethod = lstPaymentMethodHelper;
                        }

                        objConsultSN.queryOttResponse.paymentMethodList = new HELPER_ITEM.PaymentMethodListHelper();
                        objConsultSN.queryOttResponse.paymentMethodList = oPaymentMethodListHelper;



                        // lista de sucripciones
                        HELPER_ITEM.SubscriptionsHelper oSubscriptionsHelper = new HELPER_ITEM.SubscriptionsHelper();
                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.subscriptions != null)
                        {

                            List<HELPER_ITEM.SubscriptionsItemHelper> lstSubscriptionsItemHelper = new List<HELPER_ITEM.SubscriptionsItemHelper>();
                            HELPER_ITEM.SubscriptionsItemHelper oSubscriptionsItemHelper = new HELPER_ITEM.SubscriptionsItemHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.SubscriptionsItem itemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.subscriptions.item)
                            {
                                lstSubscriptionsItemHelper.Add(new HELPER_ITEM.SubscriptionsItemHelper()
                                {
                                    price = itemE.price,
                                    offerId = itemE.offerId,
                                    currency = itemE.currency,
                                    idSubscription = itemE.idSubscription,
                                    name = HttpUtility.HtmlDecode(itemE.name),
                                    productId = itemE.productId


                                });
                            }
                            oSubscriptionsHelper.item = lstSubscriptionsItemHelper;
                        }

                        objConsultSN.queryOttResponse.subscriptions = new HELPER_ITEM.SubscriptionsHelper();
                        objConsultSN.queryOttResponse.subscriptions = oSubscriptionsHelper;




                        //lista DE Eventos
                        List<HELPER_ITEM.EventHelper> oEventHelper = new List<HELPER_ITEM.EventHelper>();

                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.eventList != null)
                        {
                            if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.eventList.@event != null)
                            {

                                List<HELPER_ITEM.EventItemHelper> lstEventItemHelper = new List<HELPER_ITEM.EventItemHelper>();
                                HELPER_ITEM.EventItemHelper oEventItemHelper = new HELPER_ITEM.EventItemHelper();

                                foreach (WebApplication.Transac.Service.FixedTransacService.Event itemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.eventList.@event)
                                {
                                    lstEventItemHelper = new List<HELPER_ITEM.EventItemHelper>();
                                    foreach (WebApplication.Transac.Service.FixedTransacService.EventItem item in itemE.item)
                                    {
                                        lstEventItemHelper.Add(new HELPER_ITEM.EventItemHelper()
                                        {

                                            key = item.key,
                                            value = item.value
                                        });
                                    }

                                    oEventHelper.Add(new HELPER_ITEM.EventHelper()
                                     {

                                         item = lstEventItemHelper
                                     });
                                }

                            }
                        }

                        objConsultSN.queryOttResponse.eventList = new HELPER_ITEM.EventListHelper();
                        objConsultSN.queryOttResponse.eventList.Event = oEventHelper;

                        objConsultSN.queryOttResponse.countryId = objConsultSNResponse.MessageResponse.Body.queryOttResponse.countryId;
                        objConsultSN.queryOttResponse.serviceName = objConsultSNResponse.MessageResponse.Body.queryOttResponse.serviceName;
                        objConsultSN.queryOttResponse.providerId = objConsultSNResponse.MessageResponse.Body.queryOttResponse.providerId;


                        List<HELPER_ITEM.ExtensionInfoHelper> lstExtensionInfoHelper = new List<HELPER_ITEM.ExtensionInfoHelper>();
                        if (objConsultSNResponse.MessageResponse.Body.queryOttResponse.extensionInfo != null)
                        {

                            HELPER_ITEM.ExtensionInfoHelper oExtensionInfoHelper = new HELPER_ITEM.ExtensionInfoHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.extensionInfo itemE in objConsultSNResponse.MessageResponse.Body.queryOttResponse.extensionInfo)
                            {
                                lstExtensionInfoHelper.Add(new HELPER_ITEM.ExtensionInfoHelper()
                                {
                                    key = itemE.key,
                                    value = itemE.value

                                });
                            }
                        }

                        objConsultSN.queryOttResponse.extensionInfo = new List<HELPER_ITEM.ExtensionInfoHelper>();
                        objConsultSN.queryOttResponse.extensionInfo = lstExtensionInfoHelper;

                    }
                }
            }

            JsonResult json = Json(new { data = objConsultSN });

            return json;

        }
        public JsonResult ConsultClientSN(string strIdSession, WebApplication.Transac.Service.FixedTransacService.ConsultClientSNRequest objqueryUserOttRequest)
        {
            ConsultClientSNResponse objConsultClientSNResponse = new ConsultClientSNResponse();
            ConsultClientSNRequest objConsultClientSNRequest = new ConsultClientSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultClientSNMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.ConsultClientSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("consultarClienteSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.ConsultClientSNBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultClientSNBodyRequest()
            {
                queryUserOttRequest = new QueryUserOttRequest()
                {
                    invokeMethod = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.invokeMethod,
                    correlatorId = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.correlatorId,
                    countryId = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.countryId,
                    startDate = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.startDate,
                    endDate = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.endDate,
                    employeeId = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.employeeId,
                    origin = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.origin,
                    extraData = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.extraData,
                    serviceName = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.serviceName,
                    providerId = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.providerId,
                    iccidManager = objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.iccidManager
                    //extensionInfo = (objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.extensionInfo == null ? (new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>()) : objqueryUserOttRequest.MessageRequest.Body.queryUserOttRequest.extensionInfo),

                }

            };

            objConsultClientSNRequest.MessageRequest.Body = objBodyRequest;
            objConsultClientSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                objConsultClientSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ConsultClientSNResponse>(() =>
                {
                    return _oServiceFixed.ConsultClientSN(objConsultClientSNRequest);
                });

            }
            catch (Exception ex)
            {
                objConsultClientSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objConsultClientSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objConsultClientSNRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.ConsultClientSN objConsultClientSN = new Models.Fixed.ConsultClientSN();


            if (objConsultClientSNResponse.MessageResponse != null)
            {
                if (objConsultClientSNResponse.MessageResponse.Body != null)
                {
                    if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse != null)
                    {
                        objConsultClientSN.QueryUserOttResponse = new HELPER_ITEM.QueryUserOttResponseHelper();
                        objConsultClientSN.QueryUserOttResponse.resultCode = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.resultCode;
                        objConsultClientSN.QueryUserOttResponse.resultMessage = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.resultMessage;
                        objConsultClientSN.QueryUserOttResponse.correlatorId = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.correlatorId;


                        HELPER_ITEM.UserDataHelper oUserDataHelper = new HELPER_ITEM.UserDataHelper();

                        List<HELPER_ITEM_GRID.ListUserData> oListUserData = new List<HELPER_ITEM_GRID.ListUserData>();

                        if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.userData != null)
                        {

                            List<HELPER_ITEM.UserDataItemHelper> lstUserDataItemHelper = new List<HELPER_ITEM.UserDataItemHelper>();
                            HELPER_ITEM.UserDataItemHelper obPackageItemHelper = new HELPER_ITEM.UserDataItemHelper();

                            if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.userData.item != null)
                            {
                                HELPER_ITEM_GRID.ListUserData objListUserData = new HELPER_ITEM_GRID.ListUserData();
                                foreach (WebApplication.Transac.Service.FixedTransacService.UserDataitem itemE in objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.userData.item)
                                {
                                    objListUserData.GetType().GetProperty(itemE.key.Trim()).SetValue(objListUserData, itemE.value.Trim(), null);
                                    lstUserDataItemHelper.Add(new HELPER_ITEM.UserDataItemHelper()
                                    {
                                        key = itemE.key,
                                        value = itemE.value
                                    });
                                }
                                oListUserData.Add(objListUserData);
                                oUserDataHelper.item = lstUserDataItemHelper;
                            }
                        }
                        objConsultClientSN.QueryUserOttResponse.userData = new HELPER_ITEM.UserDataHelper();
                        objConsultClientSN.QueryUserOttResponse.userData = oUserDataHelper;
                        objConsultClientSN.QueryUserOttResponse.userData.ListUserData = oListUserData;



                        List<HELPER_ITEM.SubscriptionHelper> oListSubscriptionListHelper = new List<HELPER_ITEM.SubscriptionHelper>();


                        List<HELPER_ITEM_GRID.ListUserSubscription> oListUserSubscription = new List<HELPER_ITEM_GRID.ListUserSubscription>();

                        if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.subscriptionList != null)
                        {
                            if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.subscriptionList.subscription != null)
                            {

                            foreach (WebApplication.Transac.Service.FixedTransacService.subscription subscriptionListE in objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.subscriptionList.subscription)
                            {
                                List<HELPER_ITEM.SubscriptionItemHelper> lstSubscriptionHelper = new List<HELPER_ITEM.SubscriptionItemHelper>();
                                HELPER_ITEM_GRID.ListUserSubscription objListUserSubscription = new HELPER_ITEM_GRID.ListUserSubscription();

                                HELPER_ITEM.SubscriptionHelper oSubscriptionListHelper = new HELPER_ITEM.SubscriptionHelper();

                                foreach (WebApplication.Transac.Service.FixedTransacService.SubscriptionItem itemE in subscriptionListE.item)
                                {
                                    objListUserSubscription.GetType().GetProperty(itemE.key.Trim()).SetValue(objListUserSubscription, itemE.value.Trim(), null);
                                    lstSubscriptionHelper.Add(new HELPER_ITEM.SubscriptionItemHelper()
                                    {
                                        key = itemE.key,
                                        value = itemE.value
                                    });
                                }

                                oSubscriptionListHelper.Item = lstSubscriptionHelper;
                                oListSubscriptionListHelper.Add(oSubscriptionListHelper);
                                oListUserSubscription.Add(objListUserSubscription);
                                }
                            }
                        }

                        objConsultClientSN.QueryUserOttResponse.subscriptionList = new HELPER_ITEM.SubscriptionListHelper();
                        objConsultClientSN.QueryUserOttResponse.subscriptionList.subscription = oListSubscriptionListHelper;
                        objConsultClientSN.QueryUserOttResponse.ListUserSubscription = oListUserSubscription;



                        objConsultClientSN.QueryUserOttResponse.countryId = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.countryId;
                        objConsultClientSN.QueryUserOttResponse.providerId = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.providerId;
                        objConsultClientSN.QueryUserOttResponse.serviceName = objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.serviceName;

                        string CustomerID = string.Empty;

                        List<HELPER_ITEM.ExtensionInfoHelper> lstExtensionInfoHelper = new List<HELPER_ITEM.ExtensionInfoHelper>();
                        if (objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.extensionInfo != null)
                        {

                            HELPER_ITEM.ExtensionInfoHelper oExtensionInfoHelper = new HELPER_ITEM.ExtensionInfoHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.extensionInfo itemE in objConsultClientSNResponse.MessageResponse.Body.queryUserOttResponse.extensionInfo)
                            {
                                String CustomerIDkey = ConfigurationManager.AppSettings("KeyCustomer_ConsultarUsuarioSN");
                                if (itemE.key.ToUpper().Equals(CustomerIDkey.ToUpper()))
                                {

                                    CustomerID = itemE.value;
                                }

                                lstExtensionInfoHelper.Add(new HELPER_ITEM.ExtensionInfoHelper()
                                {
                                    key = itemE.key,
                                    value = itemE.value

                                });
                            }
                        }

                        objConsultClientSN.QueryUserOttResponse.CUSTOMERID = CustomerID;

                        objConsultClientSN.QueryUserOttResponse.extensionInfo = new List<HELPER_ITEM.ExtensionInfoHelper>();
                        objConsultClientSN.QueryUserOttResponse.extensionInfo = lstExtensionInfoHelper;
                    }

                }
            }
            JsonResult json = Json(new { data = objConsultClientSN });

            return json;

        }

        public JsonResult ProvisionSubscription(string strIdSession, WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionRequest objProvisionSubscripRequest)
        {
            ProvisionSubscriptionResponse objProvisionSubscriptionResponse = new ProvisionSubscriptionResponse();
            ProvisionSubscriptionRequest objProvisionSubscriptionRequest = new ProvisionSubscriptionRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("provisionarSuscripcionSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionBodyRequest()
            {
                provisionarSuscripcionSNRequest = new provisionarSuscripcionSNRequest()
                {
                    operatorProvisioningProductRequest = new operatorProvisioningProductRequest()
                    {
                        partnerID = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.partnerID,
                        productID = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.productID,
                        level = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.level,
                        operatorUser = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.operatorUser,
                        //ChildUser = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.ChildUser,
                        countryID = objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.countryID,
                        extensionInfo = (objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.extensionInfo == null ? new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>() : objProvisionSubscripRequest.MessageRequest.Body.provisionarSuscripcionSNRequest.operatorProvisioningProductRequest.extensionInfo)

                    }

                }
                //tipoOperacion = objNodeRequest.MessageRequest.Body.tipoOperacion,
                //listaServicios = (objNodeRequest.MessageRequest.Body.listaServicios == null ? (new List<WebApplication.Transac.Service.FixedTransacService.ListServices>()) : objNodeRequest.MessageRequest.Body.listaServicios),
                //origenFuente = (objNodeRequest.MessageRequest.Body.origenFuente == null ? "" : objNodeRequest.MessageRequest.Body.origenFuente),
                //usuario = (objNodeRequest.MessageRequest.Body.usuario == null ? "" : objNodeRequest.MessageRequest.Body.usuario),
                //fechaRegistro = Functions.CheckStr(DateTime.Now)
            };

            objProvisionSubscriptionRequest.MessageRequest.Body = objBodyRequest;
            objProvisionSubscriptionRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objProvisionSubscriptionResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ProvisionSubscriptionResponse>(() =>
                {
                    return _oServiceFixed.ProvisionSubscription(objProvisionSubscriptionRequest);
                });
                // objProvisionSubscriptionResponse = _oServiceFixed.ProvisionSubscription(objProvisionSubscriptionRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objProvisionSubscriptionResponse = null;
                Claro.Web.Logging.Error(strIdSession, objProvisionSubscriptionRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objProvisionSubscriptionRequest.audit.transaction);
            }

            return Json(new { data = objProvisionSubscriptionResponse.MessageResponse.Body, Transaccion = objProvisionSubscriptionRequest.audit.transaction });
        }
        public JsonResult CancelSubscriptionSN(string strIdSession, WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNRequest oCancelSubscriptionSNRequest)
        {
            CancelSubscriptionSNResponse objCancelSubscriptionSNResponse = new CancelSubscriptionSNResponse();
            CancelSubscriptionSNRequest objCancelSubscriptionSNRequest = new CancelSubscriptionSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("cancelarSuscripcionSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNBodyRequest()
            {
                cancelarSuscripcionSNRequest = new CancelarSuscripcionSNRequest
                {
                    cancelAccountRequest = new CancelAccountRequest()
                    {
                        partnerID = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.partnerID,
                        productID = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.productID,
                        level = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.level,
                        operatorUser = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.operatorUser,
                        //childUser = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.childUser,
                        countryID = oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.countryID,
                        extensionInfo = (oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.extensionInfo == null ? new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>() : oCancelSubscriptionSNRequest.MessageRequest.Body.cancelarSuscripcionSNRequest.cancelAccountRequest.extensionInfo)

                    }
                }
                //tipoOperacion = objNodeRequest.MessageRequest.Body.tipoOperacion,
                //listaServicios = (objNodeRequest.MessageRequest.Body.listaServicios == null ? (new List<WebApplication.Transac.Service.FixedTransacService.ListServices>()) : objNodeRequest.MessageRequest.Body.listaServicios),
                //origenFuente = (objNodeRequest.MessageRequest.Body.origenFuente == null ? "" : objNodeRequest.MessageRequest.Body.origenFuente),
                //usuario = (objNodeRequest.MessageRequest.Body.usuario == null ? "" : objNodeRequest.MessageRequest.Body.usuario),
                //fechaRegistro = Functions.CheckStr(DateTime.Now)
            };

            objCancelSubscriptionSNRequest.MessageRequest.Body = objBodyRequest;
            objCancelSubscriptionSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objCancelSubscriptionSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.CancelSubscriptionSNResponse>(() =>
                {
                    return _oServiceFixed.CancelSubscriptionSN(objCancelSubscriptionSNRequest);
                });
                // objCancelSubscriptionSNResponse = _oServiceFixed.CancelSubscriptionSN(objCancelSubscriptionSNRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objCancelSubscriptionSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objCancelSubscriptionSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCancelSubscriptionSNRequest.audit.transaction);
            }

            return Json(new { data = objCancelSubscriptionSNResponse.MessageResponse.Body, Transaccion = objCancelSubscriptionSNRequest.audit.transaction });
        }

        public JsonResult UpdateClientSN(string strIdSession, string strEntityAddEmail, string strEntityAdddeviceId, WebApplication.Transac.Service.FixedTransacService.UpdateClientSNRequest objNodeRequest)
        {
            UpdateClientSNResponse objUpdateClientSNResponse = new UpdateClientSNResponse();
            UpdateClientSNRequest objUpdateClientSNRequest = new UpdateClientSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.UpdateClientSNMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.UpdateClientSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("actualizarClienteSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.UpdateClientSNBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.UpdateClientSNBodyRequest()
                {
                    updateUserOttRequest = new UpdateUserOttRequest()
                    {
                        invokeMethod = objNodeRequest.MessageRequest.Body.updateUserOttRequest.invokeMethod,
                        correlatorId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.correlatorId,
                        countryId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.countryId,
                        userId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.userId,
                        employeeId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.employeeId,
                        origin = objNodeRequest.MessageRequest.Body.updateUserOttRequest.origin,
                        serviceName = objNodeRequest.MessageRequest.Body.updateUserOttRequest.serviceName,
                        providerId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.providerId,
                        iccidManager = objNodeRequest.MessageRequest.Body.updateUserOttRequest.iccidManager,
                        extensionInfo = (objNodeRequest.MessageRequest.Body.updateUserOttRequest.extensionInfo == null ? new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>() : objNodeRequest.MessageRequest.Body.updateUserOttRequest.extensionInfo)

                    }
                };

            if (strEntityAddEmail.Equals("1"))
            {
                objBodyRequest.updateUserOttRequest.newEmail = objNodeRequest.MessageRequest.Body.updateUserOttRequest.newEmail;
            }

            if (strEntityAdddeviceId.Equals("1"))
            {
                objBodyRequest.updateUserOttRequest.deviceId = objNodeRequest.MessageRequest.Body.updateUserOttRequest.deviceId;
            }


            objUpdateClientSNRequest.MessageRequest.Body = objBodyRequest;
            objUpdateClientSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objUpdateClientSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.UpdateClientSNResponse>(() =>
                {
                    return _oServiceFixed.UpdateClientSN(objUpdateClientSNRequest);
                });
                // objUpdateClientSNResponse = _oServiceFixed.UpdateClientSN(objUpdateClientSNRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objUpdateClientSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objUpdateClientSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objUpdateClientSNRequest.audit.transaction);
            }

            return Json(new { data = objUpdateClientSNResponse.MessageResponse.Body });
        }
        public JsonResult RegisterClientSN(string strIdSession, WebApplication.Transac.Service.FixedTransacService.RegisterClientSNRequest objcreateUserOttRequest)
        {
            RegisterClientSNResponse objRegisterClientSNResponse = new RegisterClientSNResponse();
            RegisterClientSNRequest objRegisterClientSNRequest = new RegisterClientSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.RegisterClientSNMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.RegisterClientSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("registrarClienteSN")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.RegisterClientSNBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.RegisterClientSNBodyRequest()
            {
                createUserOttRequest = new CreateUserOttRequest()
                {
                    invokeMethod = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.invokeMethod,
                    countryId = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.countryId,
                    employeeId = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.employeeId,
                    correlatorId = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.correlatorId,
                    origin = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.origin,
                    name = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.name,
                    lastName = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.lastName,
                    email = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.email,
                    motherLastName = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.motherLastName,
                    serviceName = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.serviceName,
                    providerId = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.providerId,
                    iccidManager = objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.iccidManager,
                    extensionInfo = (objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.extensionInfo == null ? new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>() : objcreateUserOttRequest.MessageRequest.Body.createUserOttRequest.extensionInfo)

                }
            };

            objRegisterClientSNRequest.MessageRequest.Body = objBodyRequest;
            objRegisterClientSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            Areas.Transactions.Models.Fixed.RegisterClientSN objRegisterClientSN = new Models.Fixed.RegisterClientSN();

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objRegisterClientSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.RegisterClientSNResponse>(() =>
                {
                    return _oServiceFixed.RegisterClientSN(objRegisterClientSNRequest);
                });
                // objRegisterClientSNResponse = _oServiceFixed.RegisterClientSN(objRegisterClientSNRequest);



                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                if (objRegisterClientSNResponse.MessageResponse != null)
                {
                    if (objRegisterClientSNResponse.MessageResponse.Body != null)
                    {
                        if (objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse != null)
                        {
                            objRegisterClientSN.createUserOttResponse = new HELPER_ITEM.CreateUserOttResponseHelper();
                            objRegisterClientSN.createUserOttResponse.resultCode = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.resultCode;
                            objRegisterClientSN.createUserOttResponse.resultMessage = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.resultMessage;
                            objRegisterClientSN.createUserOttResponse.correlatorId = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.correlatorId;
                            objRegisterClientSN.createUserOttResponse.userId = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.userId;
                            objRegisterClientSN.createUserOttResponse.countryId = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.countryId;
                            objRegisterClientSN.createUserOttResponse.serviceName = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.serviceName;
                            objRegisterClientSN.createUserOttResponse.providerId = objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.providerId;


                            string CustomerID = string.Empty;

                            List<HELPER_ITEM.ExtensionInfoHelper> lstExtensionInfoHelper = new List<HELPER_ITEM.ExtensionInfoHelper>();
                            if (objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.extensionInfo != null)
                            {

                                HELPER_ITEM.ExtensionInfoHelper oExtensionInfoHelper = new HELPER_ITEM.ExtensionInfoHelper();

                                foreach (WebApplication.Transac.Service.FixedTransacService.extensionInfo itemE in objRegisterClientSNResponse.MessageResponse.Body.createUserOttResponse.extensionInfo)
                                {
                                    String CustomerIDkey = ConfigurationManager.AppSettings("KeyCustomer_RegistrarClienteSN");
                                    if (itemE.key.ToUpper().Equals(CustomerIDkey.ToUpper()))
                                    {

                                        CustomerID = itemE.value;
                                    }

                                    lstExtensionInfoHelper.Add(new HELPER_ITEM.ExtensionInfoHelper()
                                    {
                                        key = itemE.key,
                                        value = itemE.value

                                    });
                                }
                            }

                            objRegisterClientSN.createUserOttResponse.CUSTOMERID = CustomerID;

                            objRegisterClientSN.createUserOttResponse.extensionInfo = new List<HELPER_ITEM.ExtensionInfoHelper>();
                            objRegisterClientSN.createUserOttResponse.extensionInfo = lstExtensionInfoHelper;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                objRegisterClientSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRegisterClientSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRegisterClientSNRequest.audit.transaction);
            }

            return Json(new { data = objRegisterClientSN });
        }
        public JsonResult GenerarTipificacion(MODELS.Fixed.ParametersInteraccion oModel)
        {
            String idInteraction = "";
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);

            try
            {
                var oInteraccion = DatosInteraccion(oModel);
                idInteraction = RegisterNuevaInteraccion(oModel.strIdSession, oInteraccion, "CLARO VIDEO");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);

            }

            if (idInteraction == "")
                return Json(new { codeResponse = "1", idInteraction = "" });
            else
                return Json(new { codeResponse = "0", idInteraction = idInteraction });
        }
        public JsonResult GetSendEmailSBClaroVideo(MODELS.Fixed.ParametersEnvioEmail oModel)
        {

            string strRutaArchivo = string.Empty;

            SendEmailSBModel objSendEmailSBModel = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oModel.srtIdSession);

            byte[] objFile = null;
            Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
            oCommonHandler.DisplayFileFromServerSharedFile(oModel.srtIdSession, oModel.srtIdSession, oModel.strFullPathPDF, out objFile);

            strRutaArchivo = oModel.strFullPathPDF;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            var objRequest = new SendEmailSBRequest
            {
                audit = audit,
                SessionId = oModel.srtIdSession,
                TransactionId = audit.transaction,
                strAppID = audit.ipAddress,
                strAppCode = audit.applicationName,
                strAppUser = audit.userName,
                strRemitente = oModel.strRemitente,
                strDestinatario = oModel.strDestinatario,
                strAsunto = oModel.strAsunto,
                strMensaje = oModel.strMensaje,
                strHTMLFlag = oModel.strHTMLFlag,
                Archivo = objFile,
                strNomFile = strAdjunto,
            };

            try
            {
                var objResponse = Claro.Web.Logging.ExecuteMethod(() => _oServiceCommon.GetSendEmailSB(objRequest));

                if (objResponse != null)
                {
                    objSendEmailSBModel = new SendEmailSBModel()
                    {
                        idTransaccion = objResponse.idTransaccion,
                        codigoRespuesta = objResponse.codigoRespuesta,
                        mensajeRespuesta = objResponse.mensajeRespuesta
                    };
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.srtIdSession, objRequest.audit.transaction, ex.Message);
                return Json(new { codigoRespuesta = '1', mensajeRespuesta = ex.Message });
            }

            return Json(new { codigoRespuesta = objSendEmailSBModel.codigoRespuesta, mensajeRespuesta = objSendEmailSBModel.mensajeRespuesta });
        }

        public string RegisterNuevaInteraccion(string idSession, MODELS.InteractionModel oInteraccion, string tipo)
        {
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(idSession);
            var interaccion = new Interaccion()
            {
                clase = oInteraccion.Class,
                subClase = oInteraccion.SubClass,
                objId = oInteraccion.ObjidContacto,
                siteObjId = string.IsNullOrEmpty(oInteraccion.ObjidSite) ? "" : oInteraccion.ObjidSite,
                codigoSistema = ConfigurationManager.AppSettings("USRProcesoSU"),
                notas = (string.IsNullOrEmpty(oInteraccion.Note) ? "" : oInteraccion.Note),
                codigoEmpleado = oInteraccion.Agenth,
                flagCaso = oInteraccion.FlagCase,
                hechoEnUno = oInteraccion.MadeOne,
                metodoContacto = oInteraccion.Method,
                telefono = oInteraccion.Telephone,
                textResultado = oInteraccion.Result,
                tipo = oInteraccion.Type,
                tipoInteraccion = oInteraccion.TypeInter,
                cuenta = ""
            };
            var request = new RegisterNuevaInteraccionRequest()
            {
                interaccion = interaccion,
                txId = audit.transaction,
                audit = audit
            };
            Claro.Web.Logging.Info(idSession, audit.transaction, "Begin RegisterNuevaInteraccion " + tipo);
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return _oServiceFixed.RegisterNuevaInteraccion(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, request.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(idSession, audit.transaction, string.Format("End RegisterNuevaInteraccion {0} - result: {1}", tipo, result));
            return result;
        }

        public MODELS.InteractionModel DatosInteraccion(MODELS.Fixed.ParametersInteraccion oModel)
        {
            var oInteraccion = new MODELS.InteractionModel();
            var objInteraction = new MODELS.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Begin DatosInteraccion");
            try
            {
                // Get Datos de la Tipificacion
                //objInteraction = CargarTificacion(oModel.IdSession, codeTipification);
                //var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;

                oInteraccion.ObjidContacto = oModel.objIdContacto; // GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.CustomerId; //ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + ;
                oInteraccion.Type = oModel.Type;
                oInteraccion.Class = oModel.Class;
                oInteraccion.SubClass = oModel.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.SIACU.Transac.Service.Constants.strCeroUno;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.FlagCase = Claro.SIACU.Transac.Service.Constants.strCero;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
                oInteraccion.ObjidSite = oModel.objIdSite;
                oInteraccion.Cuenta = oModel.Cuenta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "End DatosInteraccion");
            return oInteraccion;
        }

        public JsonResult GenerarTipificacionPlus(string strIdSession, CommonTransacService.InsertTemplateInteraction template)
        {
            string result = "";
            result = InsertarTipificacionPlus(strIdSession, template);
            return Json(new { codeResponse = "0", mensajeResponse = "", interaccionPlusId = result });
        }

        public string InsertarTipificacionPlus(string strIdSession, CommonTransacService.InsertTemplateInteraction template)
        {
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var txId = audit.transaction;
            var dateBegin = new DateTime(1, 1, 1);
            var interaccionPlus = new InteraccionPlus()
            {
                p_nro_interaccion = template._ID_INTERACCION,
                p_inter_1 = template._X_INTER_1,
                p_inter_2 = template._X_INTER_2,
                p_inter_3 = template._X_INTER_3,
                p_inter_4 = template._X_INTER_4,
                p_inter_5 = template._X_INTER_5,
                p_inter_6 = template._X_INTER_6,
                p_inter_7 = template._X_INTER_7,
                p_inter_8 = template._X_INTER_8.ToString(),
                p_inter_9 = template._X_INTER_9.ToString(),
                p_inter_10 = template._X_INTER_10.ToString(),
                p_inter_11 = template._X_INTER_11.ToString(),
                p_inter_12 = template._X_INTER_12.ToString(),
                p_inter_13 = template._X_INTER_13.ToString(),
                p_inter_14 = template._X_INTER_14.ToString(),
                p_inter_15 = template._X_INTER_15,
                p_inter_16 = template._X_INTER_16,
                p_inter_17 = template._X_INTER_17,
                p_inter_18 = template._X_INTER_18,
                p_inter_19 = template._X_INTER_19,
                p_inter_20 = template._X_INTER_20,
                p_inter_21 = template._X_INTER_21,
                p_inter_22 = template._X_INTER_22.ToString(),
                p_inter_23 = template._X_INTER_23.ToString(),
                p_inter_24 = template._X_INTER_24.ToString(),
                p_inter_25 = template._X_INTER_25.ToString(),
                p_inter_26 = template._X_INTER_26.ToString(),
                p_inter_27 = template._X_INTER_27.ToString(),
                p_inter_28 = template._X_INTER_28.ToString(),
                p_inter_29 = template._X_INTER_29,
                p_inter_30 = template._X_INTER_30,
                p_plus_inter2interact = template._X_PLUS_INTER2INTERACT.ToString(),
                p_adjustment_amount = template._X_ADJUSTMENT_AMOUNT.ToString(),
                p_adjustment_reason = template._X_ADJUSTMENT_REASON,
                p_address = template._X_ADDRESS,
                p_amount_unit = template._X_AMOUNT_UNIT,
                p_birthday = template._X_BIRTHDAY != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_BIRTHDAY)) : "",
                p_clarify_interaction = template._X_CLARIFY_INTERACTION,
                p_claro_ldn1 = template._X_CLARO_LDN1,
                p_claro_ldn2 = template._X_CLARO_LDN2,
                p_claro_ldn3 = template._X_CLARO_LDN3,
                p_claro_ldn4 = template._X_CLARO_LDN4,
                p_clarolocal1 = template._X_CLAROLOCAL1,
                p_clarolocal2 = template._X_CLAROLOCAL2,
                p_clarolocal3 = template._X_CLAROLOCAL3,
                p_clarolocal4 = template._X_CLAROLOCAL4,
                p_clarolocal5 = template._X_CLAROLOCAL5,
                p_clarolocal6 = template._X_CLAROLOCAL6,
                p_contact_phone = template._X_CONTACT_PHONE,
                p_dni_legal_rep = template._X_DNI_LEGAL_REP,
                p_document_number = template._X_DOCUMENT_NUMBER,
                p_email = template._X_EMAIL,
                p_first_name = template._X_FIRST_NAME,
                p_fixed_number = template._X_FIXED_NUMBER,
                p_flag_change_user = template._X_FLAG_CHANGE_USER,
                p_flag_legal_rep = template._X_FLAG_CHANGE_USER,
                p_flag_other = template._X_FLAG_OTHER,
                p_flag_titular = template._X_FLAG_TITULAR,
                p_imei = template._X_IMEI,
                p_last_name = template._X_LAST_NAME,
                p_lastname_rep = template._X_LASTNAME_REP,
                p_ldi_number = template._X_LDI_NUMBER,
                p_name_legal_rep = template._X_NAME_LEGAL_REP,
                p_old_claro_ldn1 = template._X_OLD_CLARO_LDN1,
                p_old_claro_ldn2 = template._X_OLD_CLARO_LDN2,
                p_old_claro_ldn3 = template._X_OLD_CLARO_LDN3,
                p_old_claro_ldn4 = template._X_OLD_CLARO_LDN4,
                p_old_clarolocal1 = template._X_OLD_CLAROLOCAL1,
                p_old_clarolocal2 = template._X_OLD_CLAROLOCAL2,
                p_old_clarolocal3 = template._X_OLD_CLAROLOCAL3,
                p_old_clarolocal4 = template._X_OLD_CLAROLOCAL4,
                p_old_clarolocal5 = template._X_OLD_CLAROLOCAL5,
                p_old_clarolocal6 = template._X_OLD_CLAROLOCAL6,
                p_old_doc_number = template._X_OLD_DOC_NUMBER,
                p_old_first_name = template._X_OLD_FIRST_NAME,
                p_old_fixed_phone = template._X_OLD_FIXED_PHONE,
                p_old_last_name = template._X_OLD_LAST_NAME,
                p_old_ldi_number = template._X_OLD_LDI_NUMBER,
                p_old_fixed_number = template._X_OLD_FIXED_NUMBER,
                p_operation_type = template._X_OPERATION_TYPE,
                p_other_doc_number = template._X_OTHER_DOC_NUMBER,
                p_other_first_name = template._X_OTHER_FIRST_NAME,
                p_other_last_name = template._X_OTHER_LAST_NAME,
                p_other_phone = template._X_OTHER_PHONE,
                p_phone_legal_rep = template._X_PHONE_LEGAL_REP,
                p_reference_phone = template._X_REFERENCE_PHONE,
                p_reason = template._X_REASON,
                p_model = template._X_MODEL,
                p_lot_code = template._X_LOT_CODE,
                p_flag_registered = template._X_FLAG_REGISTERED,
                p_registration_reason = template._X_REGISTRATION_REASON,
                p_claro_number = template._X_CLARO_NUMBER,
                p_month = template._X_MONTH,
                p_ost_number = template._X_OST_NUMBER,
                p_basket = template._X_BASKET,
                p_expire_date = template._X_EXPIRE_DATE != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_EXPIRE_DATE)) : "",
                p_ADDRESS5 = template._X_ADDRESS5,
                p_CHARGE_AMOUNT = template._X_CHARGE_AMOUNT.ToString(),
                p_CITY = template._X_CITY,
                p_CONTACT_SEX = template._X_CONTACT_SEX,
                p_DEPARTMENT = template._X_DEPARTMENT,
                p_DISTRICT = template._X_DISTRICT,
                p_EMAIL_CONFIRMATION = template._X_EMAIL_CONFIRMATION,
                p_FAX = template._X_FAX,
                p_FLAG_CHARGE = template._X_FLAG_CHARGE,
                p_MARITAL_STATUS = template._X_MARITAL_STATUS,
                p_OCCUPATION = template._X_OCCUPATION,
                p_POSITION = template._X_POSITION,
                p_REFERENCE_ADDRESS = template._X_REFERENCE_ADDRESS,
                p_TYPE_DOCUMENT = template._X_TYPE_DOCUMENT,
                p_ZIPCODE = template._X_ZIPCODE,
                p_iccid = template._X_ICCID,
            };
            var request = new RegisterNuevaInteraccionPlusRequest()
            {
                interaccionPlus = interaccionPlus,
                txId = txId,
                audit = audit,
            };
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin RegisterNuevaInteraccionPlus");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return _oServiceFixed.RegisterNuevaInteraccionPlus(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, request.audit.transaction, ex.Message);
                return "";
            }
            Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("End RegisterNuevaInteraccionPlus result: {0}", result));
            return result;
        }
        public JsonResult GetKeyConfig(string strIdSession, string strfilterKeyName)
        {
            string value = "";
            value = KEY.AppSettings(strfilterKeyName);
            JsonResult json = Json(new { data = value });
            return json;
        }
        public WebApplication.Transac.Service.FixedTransacService.HeaderRequest getHeaderRequest(string operation)
        {
            return new WebApplication.Transac.Service.FixedTransacService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloClaroVideo"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp()
            };
        }

        public JsonResult GetContractServices(string strIdSession, WebApplication.Transac.Service.FixedTransacService.ContractedBusinessServicesRequestPostPaid objContractedBusinessServicesRequestPostPaid)
        {
            WebApplication.Transac.Service.FixedTransacService.ContractedBusinessServicesResponsePostPaid objContractedBusinessServicesResponsePostPaid = null;
            string strText = "";
            try
            {
                objContractedBusinessServicesRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
                objContractedBusinessServicesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ContractedBusinessServicesResponsePostPaid>(() => { return _oServiceFixed.GetContractServices(objContractedBusinessServicesRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objContractedBusinessServicesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objContractedBusinessServicesRequestPostPaid.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.ContractedBusinessServicesModel objContractedBusinessServicesModel = new Areas.Transactions.Models.Fixed.ContractedBusinessServicesModel();

            if (objContractedBusinessServicesResponsePostPaid.ContractServices != null)
            {
                List<HELPER_ITEM.ContractServicesHelper> listContractServices = new List<HELPER_ITEM.ContractServicesHelper>();

                foreach (WebApplication.Transac.Service.FixedTransacService.ContractServicesPostPaid objContractServicesPostPaid in objContractedBusinessServicesResponsePostPaid.ContractServices)
                {
                    listContractServices.Add(new HELPER_ITEM.ContractServicesHelper()
                    {
                        GroupCode = objContractServicesPostPaid.COD_GRUPO,
                        GroupDescription = objContractServicesPostPaid.DES_GRUPO,
                        GroupPos = objContractServicesPostPaid.POS_GRUPO,
                        ServiceCode = objContractServicesPostPaid.COD_SERV,
                        ServiceDescription = objContractServicesPostPaid.DES_SERV,
                        ServicePos = objContractServicesPostPaid.POS_SERV,
                        ExclusiveCode = objContractServicesPostPaid.COD_EXCLUYENTE,
                        ExclusiveDescription = objContractServicesPostPaid.DES_EXCLUYENTE,
                        State = objContractServicesPostPaid.ESTADO,
                        ValidityDate = objContractServicesPostPaid.FECHA_VALIDEZ,
                        SubscriptionFeeAmount = objContractServicesPostPaid.MONTO_CARGO_SUS,
                        AmountFixedCharge = objContractServicesPostPaid.MONTO_CARGO_FIJO,
                        ModifiedShare = objContractServicesPostPaid.CUOTA_MODIF,
                        FinalAmount = objContractServicesPostPaid.MONTO_FINAL,
                        ValidPeriods = objContractServicesPostPaid.PERIODOS_VALIDOS,
                        DisableLock = objContractServicesPostPaid.BLOQUEO_DESACT,
                        ActiveBlocking = objContractServicesPostPaid.BLOQUEO_ACT,
                    });
                }
                objContractedBusinessServicesModel.ContractServices = listContractServices;
                strText = Claro.SIACU.Constants.ReturnContractService;
            }
            else
            {
                strText = Claro.SIACU.Constants.ReturnContractServiceNull;
            }


            if (!objContractedBusinessServicesRequestPostPaid.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
            {

                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    strText = strText + objContractedBusinessServicesRequestPostPaid.Telephone + Claro.SIACU.Constants.ContractCode + objContractedBusinessServicesRequestPostPaid.ContractId;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, objContractedBusinessServicesRequestPostPaid.Telephone, KEY.AppSettings("strAudiTraCodConsultaServComercial"), strText); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            }

            return Json(new { data = objContractedBusinessServicesModel });
        }

        public JsonResult HistoryDeviceService(string strIdSession, string flagIsTMCOD, WebApplication.Transac.Service.FixedTransacService.HistoryDeviceRequest objNodeRequest)
        {
            HistoryDeviceResponse objResponse = new HistoryDeviceResponse();
            HistoryDeviceRequest objRequest = new HistoryDeviceRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.HistoryDeviceMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.HistoryDeviceHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("HistoriaServDispoCv")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.HistoryDeviceBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.HistoryDeviceBodyRequest()
            {
                historialServDispCVRequest = new HistorialServDispCVRequest()
                {
                    linea = objNodeRequest.MessageRequest.Body.historialServDispCVRequest.linea,
                    nombreServicio = (objNodeRequest.MessageRequest.Body.historialServDispCVRequest.nombreServicio == null ? " " : objNodeRequest.MessageRequest.Body.historialServDispCVRequest.nombreServicio),
                    fechaInicio = (objNodeRequest.MessageRequest.Body.historialServDispCVRequest.fechaInicio == null ? DateTime.Now.ToShortDateString() : objNodeRequest.MessageRequest.Body.historialServDispCVRequest.fechaInicio),
                    fechaFin = (objNodeRequest.MessageRequest.Body.historialServDispCVRequest.fechaFin == null ? DateTime.Now.ToShortDateString() : objNodeRequest.MessageRequest.Body.historialServDispCVRequest.fechaFin),
                    servicioName = objNodeRequest.MessageRequest.Body.historialServDispCVRequest.servicioName,
                    tipoLinea = objNodeRequest.MessageRequest.Body.historialServDispCVRequest.tipoLinea
                }

            };

            if (flagIsTMCOD.Equals("1"))
            {
                objBodyRequest.historialServDispCVRequest.tmcod = (objNodeRequest.MessageRequest.Body.historialServDispCVRequest.tmcod == null ? " " : objNodeRequest.MessageRequest.Body.historialServDispCVRequest.tmcod);

            }

            objRequest.MessageRequest.Body = objBodyRequest;
            objRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.HistoryDeviceResponse>(() =>
                {
                    return _oServiceFixed.HistoryDevice(objRequest);
                });
            }
            catch (Exception ex)
            {
                objResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.HistoryDevice objHistoryDeviceService = new Models.Fixed.HistoryDevice();

            if (objResponse.MessageResponse != null)
            {
                if (objResponse.MessageResponse.Body != null)
                {
                    if (objResponse.MessageResponse.Body.historialServDispCVResponse != null)
                    {
                        objHistoryDeviceService.historialServDispCVResponse = new HELPER_ITEM.HistorialServDispCVResponseHelper();
                        objHistoryDeviceService.historialServDispCVResponse.codError = objResponse.MessageResponse.Body.historialServDispCVResponse.codError;
                        objHistoryDeviceService.historialServDispCVResponse.messageError = objResponse.MessageResponse.Body.historialServDispCVResponse.msgError;

                        HELPER_ITEM.HistorialServDispCVResponseHelper objDevServ = new HELPER_ITEM.HistorialServDispCVResponseHelper();

                        //Historial de dispositivos
                        if (objResponse.MessageResponse.Body.historialServDispCVResponse.pHistorialDisp != null)
                        {
                            List<HELPER_ITEM.PHistorialDispHelper> lstDevice = new List<HELPER_ITEM.PHistorialDispHelper>();


                            foreach (WebApplication.Transac.Service.FixedTransacService.pHistorialDisp device in objResponse.MessageResponse.Body.historialServDispCVResponse.pHistorialDisp)
                            {
                                lstDevice.Add(new HELPER_ITEM.PHistorialDispHelper()
                                {
                                    tipoDisp = device.tipoDisp,
                                    nombreDisp = device.nombreDisp,
                                    dispositivoId = device.dispositivoId,
                                    fechaAct = device.fechaAct,
                                    fehaExp = device.fehaExp
                                });
                            }

                            objDevServ.pHistorialDisp = lstDevice;
                            objHistoryDeviceService.historialServDispCVResponse.pHistorialDisp = objDevServ.pHistorialDisp;
                        }

                        //Historial de servicios
                        if (objResponse.MessageResponse.Body.historialServDispCVResponse.pHistorialServ != null)
                        {
                            List<HELPER_ITEM.PHistorialServHelper> lstService = new List<HELPER_ITEM.PHistorialServHelper>();

                            foreach (WebApplication.Transac.Service.FixedTransacService.pHistorialServ service in objResponse.MessageResponse.Body.historialServDispCVResponse.pHistorialServ)
                            {
                                lstService.Add(new HELPER_ITEM.PHistorialServHelper()
                                {
                                    nombreServicio = service.nombreServicio,
                                    estado = service.estado,
                                    fechaActivacion = service.fechaActivacion,
                                    fechaExpiracion = service.fechaExpiracion,
                                    precio = service.precio,
                                    servicio = service.servicio
                                });
                            }
                            objDevServ.pHistorialServ = lstService;
                            objHistoryDeviceService.historialServDispCVResponse.pHistorialServ = objDevServ.pHistorialServ;

                        }

                        //Historial de Estado de pago
                        if (objResponse.MessageResponse.Body.historialServDispCVResponse.pEstadoPagoServ != null)
                        {
                            List<HELPER_ITEM.PEstadoPagoServHelper> lstState = new List<PEstadoPagoServHelper>();

                            foreach (WebApplication.Transac.Service.FixedTransacService.pEstadoPagoServ state in objResponse.MessageResponse.Body.historialServDispCVResponse.pEstadoPagoServ)
                            {
                                lstState.Add(new HELPER_ITEM.PEstadoPagoServHelper()
                                {
                                    nombreServicio = state.nombreServicio,
                                    descripcionServicio = state.descripcionServicio,
                                    fechaAct = state.fechaAct,
                                    fehaExp = state.fehaExp,
                                    diasPromo = state.diasPromo,
                                    servicioPrecio = state.servicioPrecio,
                                    estadoPago = state.estadoPago
                                });

                            }
                            objDevServ.pEstadoPagoServ = lstState;
                            objHistoryDeviceService.historialServDispCVResponse.pEstadoPagoServ = objDevServ.pEstadoPagoServ;

                        }

                    }
                }
            }

            JsonResult json = Json(new { data = objHistoryDeviceService });

            return json;
        }

        public JsonResult ValidateElegibility(string strIdSession, string flagIsTMCOD, WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityRequest objValidateElegibilityRequest)
        {
            ValidateElegibilityResponse objResponse = new ValidateElegibilityResponse();
            ValidateElegibilityRequest objRequest = new ValidateElegibilityRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("validarElegibilidad")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityBodyRequest()
            {
                validarElegibilidadRequest = new ValidarElegibilidadRequest()
                {
                    medioPago = objValidateElegibilityRequest.MessageRequest.Body.validarElegibilidadRequest.medioPago,
                    tipoLinea = objValidateElegibilityRequest.MessageRequest.Body.validarElegibilidadRequest.tipoLinea,
                    producto = objValidateElegibilityRequest.MessageRequest.Body.validarElegibilidadRequest.producto,
                    productoId = objValidateElegibilityRequest.MessageRequest.Body.validarElegibilidadRequest.productoId                   

                }

            };
           

            objRequest.MessageRequest.Body = objBodyRequest;
            objRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ValidateElegibilityResponse>(() =>
                {
                    return _oServiceFixed.ValidateElegibility(objRequest);
                });
            }
            catch (Exception ex)
            {
                objResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.ValidateElegibility objValidateElegibility = new Models.Fixed.ValidateElegibility();

            if (objResponse.MessageResponse != null)
            {
                if (objResponse.MessageResponse.Body != null)
                {
                    if (objResponse.MessageResponse.Body.validarElegibilidadResponse != null)
                    {
                        objValidateElegibility.validateElegibilityResponse = new HELPER_ITEM.ValidateElegibilityResponseHelper();
                        objValidateElegibility.validateElegibilityResponse.codError = objResponse.MessageResponse.Body.validarElegibilidadResponse.codError;
                        objValidateElegibility.validateElegibilityResponse.msgError = objResponse.MessageResponse.Body.validarElegibilidadResponse.msgError;
                        objValidateElegibility.validateElegibilityResponse.medioPago = objResponse.MessageResponse.Body.validarElegibilidadResponse.medioPago;
                        objValidateElegibility.validateElegibilityResponse.tipoLinea = objResponse.MessageResponse.Body.validarElegibilidadResponse.tipoLinea;

                        List<HELPER_ITEM.ListServicesHelper> lstServicesHelper = new List<HELPER_ITEM.ListServicesHelper>();
                        if (objResponse.MessageResponse.Body.validarElegibilidadResponse.listadoServicios != null)
                        {
                            HELPER_ITEM.ListServicesHelper oExtensionInfoHelper = new HELPER_ITEM.ListServicesHelper();

                            foreach (WebApplication.Transac.Service.FixedTransacService.ListServicesElegibility itemE in objResponse.MessageResponse.Body.validarElegibilidadResponse.listadoServicios)
                            {
                                lstServicesHelper.Add(new HELPER_ITEM.ListServicesHelper()
                                {
                                    nombre = itemE.nombre,
                                    productID = itemE.productID

                                });
                            }

                            objValidateElegibility.validateElegibilityResponse.listadoServicios = lstServicesHelper;
                        }

                    }
                }
            }

            JsonResult json = Json(new { data = objValidateElegibility });

            return json;
        }

        public JsonResult RegisterControlesCV(string strIdSession, WebApplication.Transac.Service.FixedTransacService.RegistrarControlesRequest objRegistrarControlesRequest)
        {
            RegistrarControlesResponse objRegistrarcontrolescvResponse = new RegistrarControlesResponse();
            RegistrarControlesRequest objRegistrarControlesCvRequest = new RegistrarControlesRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                //AuditService = App_Code.Common.CreateAuditRequest<WebApplication.Transac.Service.FixedTransacService.Audit>("", Claro.SICES.Constants.strGetDetailsFormulary + "" + Claro.SICES.Constants.strKey + "" + Claro.SICES.Constants.strHora + DateTime.Now.ToString("yyyyMMdd HHmmss")),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.RegistrarControlesMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.RegistrarControlesHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("provisionarSuscripcionSN")
                    }
                }
            };

            //objRegistrarControlesCvRequest.audit.transaction

            WebApplication.Transac.Service.FixedTransacService.RegistrarControlesBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.RegistrarControlesBodyRequest()
            {
                registrarControlesCvRequest = new RegistrarControlesCvRequest()
                {
                    transaccionId = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.transaccionId,
                    flagTransaccion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.flagTransaccion,
                    tipoTransaccion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.tipoTransaccion,
                    documentoVenta = (objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.documentoVenta == null ? "" : objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.documentoVenta),
                    nombreAplicacion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.nombreAplicacion,
                    operacionSuscripcion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.operacionSuscripcion,
                    nombreServicio = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.nombreServicio,
                    nombrePdv = (objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.nombrePdv == null ? "" : objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.nombrePdv),
                    custormerId = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.custormerId,
                    linea = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.linea,
                    estadoTransaccion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.estadoTransaccion,
                    mensajeTransaccion = objRegistrarControlesRequest.MessageRequest.Body.registrarControlesCvRequest.mensajeTransaccion

                }
            };

            objRegistrarControlesCvRequest.MessageRequest.Body = objBodyRequest;
            objRegistrarControlesCvRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                //  Claro.Web.Logging.Info(strIdSession, objConsultFormRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
                objRegistrarcontrolescvResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.RegistrarControlesResponse>(() =>
                {
                    return _oServiceFixed.RegistrarControles(objRegistrarControlesCvRequest);
                });
                // objRegisterClientSNResponse = _oServiceFixed.RegisterClientSN(objRegisterClientSNRequest);

                // Claro.Web.Logging.Info(strIdSession, objSearchStateLineEmailRequest.audit.transaction, "Busqueda de formulario : " + objConsultFormRequest.MessageRequest.Body.descripcionFiltro + " - " + objConsultFormRequest.MessageRequest.Body.codigoFiltro);
            }
            catch (Exception ex)
            {
                objRegistrarcontrolescvResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRegistrarControlesCvRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRegistrarControlesCvRequest.audit.transaction);
            }

            return Json(new { data = objRegistrarcontrolescvResponse.MessageResponse.Body });
        }

        #region HPxtream
        //INICIO Luis D

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strTypeTransaction"></param>
        /// <param name="oModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGenerateContancy(string strIdSession, MODELS.Fixed.ParametersGenerateContancy objParametersGenerateContancy)
        {
            GenerateConstancyResponseCommon response = new GenerateConstancyResponseCommon();

            CommonServicesController objCommon = new CommonServicesController();
            List<GenerateConstancyResponseCommon> listConstancyResponse = new List<GenerateConstancyResponseCommon>();

            ConstancyClaroVideoHelper objConstancia = new ConstancyClaroVideoHelper();
            Boolean FlagConstancia = false;

            switch (objParametersGenerateContancy.strTipoConstanciaAMCO)
            {
                case "1":
                    // 1 CONSTANCIA_CAMBIO_CONTRASENA_CLARO_VIDEO

                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; //"1";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; //strNomTransaSusCambioPass;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion; // "CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular; // "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strRepresentante; //"EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc;  //"DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString(); //"18/08/2019";
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroCaso; // "997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc; //"70403635";
                    objConstancia.strEmail = objParametersGenerateContancy.strEmail; //"juanperez@gmail.com";
                    FlagConstancia = true;
                    //listConstancy.Add(objSuscripcionCambioPass);
                    break;
                case "2":
                    // CONSTANCIA_CAMBIO_CORREO_CLARO_VIDEO
                    // ConstancyClaroVideoHelper objSuscripcionCambioCorreo = new ConstancyClaroVideoHelper();
                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; //"2";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; // strNomTransaSusCambioCorreo;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion; //"CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular; //  "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strRepresentante; //  "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc; //  "DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString();
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;// strInteraccionId;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroServicio; //  "997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc;  // "70403635";
                    objConstancia.strEmail = objParametersGenerateContancy.strEmail; // "juanperez@gmail.com";
                    FlagConstancia = true;
                    //listConstancy.Add(objSuscripcionCambioCorreo);
                    break;
                case "3":
                    // 3 CONSTANCIA_CANCELACION_CLARO_VIDEO
                    //ConstancyClaroVideoHelper objSuscripcionCancelacion = new ConstancyClaroVideoHelper();
                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; // "3";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; //strNomTransaSusCancelacion;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion;  // "CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular;  //"EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strRepresentante; // "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc;  //"DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString();
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroServicio; // "997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc;  //"70403635";
                    objConstancia.strEmail = objParametersGenerateContancy.strEmail; //"juanperez@gmail.com";

                    if (objParametersGenerateContancy.ListService != null)
                    {
                        objConstancia.ListService = new List<ClaroVideoServiceConstancyHelper>();

                        foreach (var item in objParametersGenerateContancy.ListService)
                        {
                            ClaroVideoServiceConstancyHelper objService = new ClaroVideoServiceConstancyHelper() { strBajaServicios = item.strBajaServicios };
                            objConstancia.ListService.Add(objService);
                        }
                        FlagConstancia = true;

                    }
                    else
                    {
                        FlagConstancia = false;
                    }
                   
                    break;
               
                case "4":
                    // 4 CONSTANCIA_DESVINCULAR_DISPOSITIVO_CLARO_VIDEO
                    //ConstancyClaroVideoHelper objSuscripcioDesvDispo = new ConstancyClaroVideoHelper();
                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; //"4";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; //strNomTransaSusDesvDispositivo;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion; //"CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular;// "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strRepresentante; //"EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc;   //"DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString(); //"18/08/2019";
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroServicio;// "997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc; //"70403635";

                    if (objParametersGenerateContancy.ListDevice != null)
                    {
                        objConstancia.ListDevice = new List<ClaroVideoDeviceConstancyHelper>();

                        foreach (var item in objParametersGenerateContancy.ListDevice)
                        {
                            ClaroVideoDeviceConstancyHelper objDevice = new ClaroVideoDeviceConstancyHelper() { strDispotisitivoID = item.strDispotisitivoID, strDispotisitivoNom = item.strDispotisitivoNom, strFechaDesac = item.strFechaDesac };

                            objConstancia.ListDevice.Add(objDevice);

                        }
                        FlagConstancia = true;

                    }
                    else
                    {
                        FlagConstancia = false;
                    }        
                   
                    break;
              
                case "5":
                    // 5 CONSTANCIA_SUSCRIPCION_ADICIONALES_CLARO_VIDEO
                    //ConstancyClaroVideoHelper objSuscripcionAdicional = new ConstancyClaroVideoHelper();
                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; //"5";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; // strNomTransaSusAdicional;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion;  // "CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular; // "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strTitular; // "EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc;  //"DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString();  //"18/08/2019";
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroServicio; //"997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc;  // "70403635";

                    if (objParametersGenerateContancy.ListSuscriptcionAdicionales != null)
                    {
                        objConstancia.ListSuscriptcion = new List<ClaroVideoSubscriptionConstancyHelper>();

                        foreach (var item in objParametersGenerateContancy.ListSuscriptcionAdicionales)
                        {
                            ClaroVideoSubscriptionConstancyHelper objSuscriptcion = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = item.strSuscTitulo, strSuscEstado = item.strSuscEstado, strSuscPeriodo = item.strSuscPeriodo, strSuscServicio = item.strSuscServicio, strSuscPrecio = item.strSuscPrecio, strSuscFechReg = item.strSuscFechReg };
                            objConstancia.ListSuscriptcion.Add(objSuscriptcion);
                        }

                        FlagConstancia = true;

                    }
                    else
                    {
                        FlagConstancia = false;
                    }
            

                    break;
              
                case "6":
                    // 6 CONSTANCIA_SUSCRIPCION_CLARO_VIDEO
                    //ConstancyClaroVideoHelper objSuscripcion = new ConstancyClaroVideoHelper();
                    objConstancia.strTipoConstanciaAMCO = objParametersGenerateContancy.strTipoConstanciaAMCO; //"6";
                    objConstancia.StrNombreArchivoTransaccion = objParametersGenerateContancy.StrNombreArchivoTransaccion; // strNomTransaSuscripcion;
                    objConstancia.strPuntoAtencion = objParametersGenerateContancy.strPuntoAtencion;  //"CAC BEGONIAS";
                    objConstancia.strTitular = objParametersGenerateContancy.strTitular; //"EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strRepresentante = objParametersGenerateContancy.strTitular; //"EDITH ROSMERY LAVADO CASTILLO";
                    objConstancia.strTipoDoc = objParametersGenerateContancy.strTipoDoc;  // "DNI";
                    objConstancia.strFechaAct = DateTime.Now.ToShortDateString(); //"18/08/2019";
                    objConstancia.strNroCaso = objParametersGenerateContancy.strNroCaso;
                    objConstancia.strNroServicio = objParametersGenerateContancy.strNroServicio; // "997058474";
                    objConstancia.strNroDoc = objParametersGenerateContancy.strNroDoc;  //"70403635";
                    objConstancia.strEmail = objParametersGenerateContancy.strEmail; //"juanperez@gmail.com";


                    if (objParametersGenerateContancy.ListSuscriptcion != null)
                    {
                        objConstancia.ListSuscriptcion = new List<ClaroVideoSubscriptionConstancyHelper>();

                        foreach (var item in objParametersGenerateContancy.ListSuscriptcion)
                        {
                            ClaroVideoSubscriptionConstancyHelper objSuscriptcion = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = item.strSuscTitulo, strSuscEstado = item.strSuscEstado, strSuscPeriodo = item.strSuscPeriodo, strSuscServicio = item.strSuscServicio, strSuscPrecio = item.strSuscPrecio, strSuscFechReg = item.strSuscFechReg };
                            objConstancia.ListSuscriptcion.Add(objSuscriptcion);
                        }
                        FlagConstancia = true;

                    }
                    else
                    {
                        FlagConstancia = false;
                    }
                                        
                   
                    break;

                default:
                    FlagConstancia = false;
                    break;
            }

            //INICIO DATA de PRUEBA
            #region PRUEBA
            // 1 CONSTANCIA_CAMBIO_CONTRASENA_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcionCambioPass = new ConstancyClaroVideoHelper();
            //objSuscripcionCambioPass.strTipoConstanciaAMCO = "1";
            //objSuscripcionCambioPass.StrNombreArchivoTransaccion = strNomTransaSusCambioPass;
            //objSuscripcionCambioPass.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcionCambioPass.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCambioPass.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCambioPass.strTipoDoc = "DNI";
            //objSuscripcionCambioPass.strFechaAct = "18/08/2019";
            //objSuscripcionCambioPass.strNroCaso = strInteraccionId;
            //objSuscripcionCambioPass.strNroServicio = "997058474";
            //objSuscripcionCambioPass.strNroDoc = "70403635";
            //objSuscripcionCambioPass.strEmail = "juanperez@gmail.com";
            //listConstancy.Add(objSuscripcionCambioPass);

            // 2 CONSTANCIA_CAMBIO_CORREO_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcionCambioCorreo = new ConstancyClaroVideoHelper();
            //objSuscripcionCambioCorreo.strTipoConstanciaAMCO = "2";
            //objSuscripcionCambioCorreo.StrNombreArchivoTransaccion = strNomTransaSusCambioCorreo;
            //objSuscripcionCambioCorreo.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcionCambioCorreo.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCambioCorreo.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCambioCorreo.strTipoDoc = "DNI";
            //objSuscripcionCambioCorreo.strFechaAct = "18/08/2019";
            //objSuscripcionCambioCorreo.strNroCaso = strInteraccionId;
            //objSuscripcionCambioCorreo.strNroServicio = "997058474";
            //objSuscripcionCambioCorreo.strNroDoc = "70403635";
            //objSuscripcionCambioCorreo.strEmail = "juanperez@gmail.com";
            //listConstancy.Add(objSuscripcionCambioCorreo);

            // 3 CONSTANCIA_CANCELACION_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcionCancelacion = new ConstancyClaroVideoHelper();
            //objSuscripcionCancelacion.strTipoConstanciaAMCO = "3";
            //objSuscripcionCancelacion.StrNombreArchivoTransaccion = strNomTransaSusCancelacion;
            //objSuscripcionCancelacion.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcionCancelacion.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCancelacion.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionCancelacion.strTipoDoc = "DNI";
            //objSuscripcionCancelacion.strFechaAct = "18/08/2019";
            //objSuscripcionCancelacion.strNroCaso = strInteraccionId;
            //objSuscripcionCancelacion.strNroServicio = "997058474";
            //objSuscripcionCancelacion.strNroDoc = "70403635";
            //objSuscripcionCancelacion.strEmail = "juanperez@gmail.com";
            //objSuscripcionCancelacion.ListService = new List<ClaroVideoServiceConstancyHelper>();
            //ClaroVideoServiceConstancyHelper objSevice1 = new ClaroVideoServiceConstancyHelper() { strBajaServicios = "HBO" };
            //ClaroVideoServiceConstancyHelper objSevice2 = new ClaroVideoServiceConstancyHelper() { strBajaServicios = "FOX" };
            //ClaroVideoServiceConstancyHelper objSevice3 = new ClaroVideoServiceConstancyHelper() { strBajaServicios = "FOX +" };
            //objSuscripcionCancelacion.ListService.Add(objSevice1);
            //objSuscripcionCancelacion.ListService.Add(objSevice2);
            //objSuscripcionCancelacion.ListService.Add(objSevice3);
            //listConstancy.Add(objSuscripcionCancelacion);

            // 4 CONSTANCIA_DESVINCULAR_DISPOSITIVO_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcioDesvDispo = new ConstancyClaroVideoHelper();
            //objSuscripcioDesvDispo.strTipoConstanciaAMCO = "4";
            //objSuscripcioDesvDispo.StrNombreArchivoTransaccion = strNomTransaSusDesvDispositivo;
            //objSuscripcioDesvDispo.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcioDesvDispo.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcioDesvDispo.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcioDesvDispo.strTipoDoc = "DNI";
            //objSuscripcioDesvDispo.strFechaAct = "18/08/2019";
            //objSuscripcioDesvDispo.strNroCaso = strInteraccionId;
            //objSuscripcioDesvDispo.strNroServicio = "997058474";
            //objSuscripcioDesvDispo.strNroDoc = "70403635";
            //objSuscripcioDesvDispo.ListDevice = new List<ClaroVideoDeviceConstancyHelper>();
            //ClaroVideoDeviceConstancyHelper objDevice1 = new ClaroVideoDeviceConstancyHelper() { strDispotisitivoID = "D001", strDispotisitivoNom = "ANDROID", strFechaDesac = "16/11/2019" };
            //ClaroVideoDeviceConstancyHelper objDevice2 = new ClaroVideoDeviceConstancyHelper() { strDispotisitivoID = "D002", strDispotisitivoNom = "SMART", strFechaDesac = "16/11/2019" };
            //ClaroVideoDeviceConstancyHelper objDevice3 = new ClaroVideoDeviceConstancyHelper() { strDispotisitivoID = "D003", strDispotisitivoNom = "WATCH", strFechaDesac = "16/11/2019" };
            //objSuscripcioDesvDispo.ListDevice.Add(objDevice1);
            //objSuscripcioDesvDispo.ListDevice.Add(objDevice2);
            //objSuscripcioDesvDispo.ListDevice.Add(objDevice3);
            //listConstancy.Add(objSuscripcioDesvDispo);

            // 5 CONSTANCIA_SUSCRIPCION_ADICIONALES_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcionAdicional = new ConstancyClaroVideoHelper();
            //objSuscripcionAdicional.strTipoConstanciaAMCO = "5";
            //objSuscripcionAdicional.StrNombreArchivoTransaccion = strNomTransaSusAdicional;
            //objSuscripcionAdicional.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcionAdicional.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionAdicional.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcionAdicional.strTipoDoc = "DNI";
            //objSuscripcionAdicional.strFechaAct = "18/08/2019";
            //objSuscripcionAdicional.strNroCaso = strInteraccionId;
            //objSuscripcionAdicional.strNroServicio = "997058474";
            //objSuscripcionAdicional.strNroDoc = "70403635";
            //objSuscripcionAdicional.ListSuscriptcion = new List<ClaroVideoSubscriptionConstancyHelper>();
            //ClaroVideoSubscriptionConstancyHelper objSuscriptcion1 = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = "Suscripción HBO", strSuscEstado = "Activado", strSuscPeriodo = "El producto HBO tiene un periodo promocional(Gratis) del  18/08/2019 hasta 18/09/2019 Después de la fecha indica el costo será S/35.00", strSuscServicio = "Móvil", strSuscPrecio = "S/ 35.00", strSuscFechReg = "18/08/2019" };
            //ClaroVideoSubscriptionConstancyHelper objSuscriptcion2 = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = "Suscripción FOX", strSuscEstado = "Activado", strSuscPeriodo = "El producto FOX tiene un periodo promocional(Gratis) del  18/08/2019 hasta 18/09/2019 Después de la fecha indica el costo será S/36.00", strSuscServicio = "Móvil", strSuscPrecio = "S/ 36.00", strSuscFechReg = "18/08/2019" };
            //objSuscripcionAdicional.ListSuscriptcion.Add(objSuscriptcion1);
            //objSuscripcionAdicional.ListSuscriptcion.Add(objSuscriptcion2);
            //listConstancy.Add(objSuscripcionAdicional);

            // 6 CONSTANCIA_SUSCRIPCION_CLARO_VIDEO
            //ConstancyClaroVideoHelper objSuscripcion = new ConstancyClaroVideoHelper();
            //objSuscripcion.strTipoConstanciaAMCO = "6";
            //objSuscripcion.StrNombreArchivoTransaccion = strNomTransaSuscripcion;
            //objSuscripcion.strPuntoAtencion = "CAC BEGONIAS";
            //objSuscripcion.strTitular = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcion.strRepresentante = "EDITH ROSMERY LAVADO CASTILLO";
            //objSuscripcion.strTipoDoc = "DNI";
            //objSuscripcion.strFechaAct = "18/08/2019";
            //objSuscripcion.strNroCaso = strInteraccionId;
            //objSuscripcion.strNroServicio = "997058474";
            //objSuscripcion.strNroDoc = "70403635";
            //objSuscripcionCancelacion.strEmail = "juanperez@gmail.com";
            //objSuscripcion.ListSuscriptcion = new List<ClaroVideoSubscriptionConstancyHelper>();
            //ClaroVideoSubscriptionConstancyHelper objSuscriptcion01 = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = "Suscripción Claro Video", strSuscEstado = "Activado", strSuscPeriodo = "El producto Claro Video tiene un periodo promocional (Gratis) del 18/08/2019 hasta 18/08/2021 Después de la fecha indicada el costo será S/22.00", strSuscServicio = "Móvil", strSuscPrecio = "S/ 22.00", strSuscFechReg = "18/08/2019" };
            //ClaroVideoSubscriptionConstancyHelper objSuscriptcion02 = new ClaroVideoSubscriptionConstancyHelper() { strSuscTitulo = "Suscripción Claro Video +", strSuscEstado = "Activado", strSuscPeriodo = "El producto Claro Video + tiene un periodo promocional (Gratis) del 19/08/2019 hasta 19/08/2021 Después de la fecha indicada el costo será S/23.00", strSuscServicio = "Móvil", strSuscPrecio = "S/ 23.00", strSuscFechReg = "18/08/2019" };
            //objSuscripcion.ListSuscriptcion.Add(objSuscriptcion01);
            //objSuscripcion.ListSuscriptcion.Add(objSuscriptcion02);
            //listConstancy.Add(objSuscripcion);
            #endregion

            GenerateConstancyResponseCommon responseSuscripcionResponse = new GenerateConstancyResponseCommon();
            if (FlagConstancia)
            {
                ParametersGeneratePDF objGeneratePDFSuscripcion = GetParameterGeneratePDF(objConstancia);
                responseSuscripcionResponse = objCommon.GetGenerateContancyNamePDF(strIdSession, objGeneratePDFSuscripcion, objParametersGenerateContancy.StrNombreArchivoPDF);
            }
            ////FIN DATA DE PRUEBA
            //if(listConstancy != null ? listConstancy.Count() > 0 ? true : false : false)
            //{
            //    foreach (ConstancyClaroVideoHelper item in listConstancy)
            //    {
            //        responseSuscripcionResponse = new GenerateConstancyResponseCommon();

            //        ParametersGeneratePDF objGeneratePDFSuscripcion = GetParameterGeneratePDF(listConstancy);
            //         responseSuscripcionResponse = objCommon.GetGenerateContancyPDF(strIdSession, objGeneratePDFSuscripcion);
            //        if(responseSuscripcionResponse != null)
            //        {
            //            listConstancyResponse.Add(responseSuscripcionResponse);
            //        }
            //    }
            //}

            //return Json(new { codeResponse = "0", Constancia = responseSuscripcionResponse });

            //byte[] objFile = null;

            //objCommon.DisplayFileFromServerSharedFile(strIdSession, strIdSession, responseSuscripcionResponse.FullPathPDF, out objFile);

            //if (objFile != null)
            //{

            //    //objRequest.Path = response.Document;
            //    //objRequest.TipoOperacion = KEY.AppSettings("strLeyPromoTypeOnBase");
            //    //objRequest.FechaRegistro = DateTime.Now.ToString("o");

            //    // return Json(new { codeResponse = "0", OnBase = CargarOnBase(objRequest, objFile), Constancia = response });
            //    return Json(new { codeResponse = "0", Constancia = responseSuscripcionResponse });
            //}
            //else
            //    throw new Exception("No se genero la constancia");
            return Json(new { codeResponse = "0", Constancia = responseSuscripcionResponse, TipoConstancia = objParametersGenerateContancy.strTipoConstanciaAMCO });
        }
        public FileContentResult DownloadFileServer(string strIdSession, string strPath)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO: DownloadFileServer");
            byte[] objFile = null;
            Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
            oCommonHandler.DisplayFileFromServerSharedFile(strIdSession, strIdSession, strPath, out objFile);
            Claro.Web.Logging.Info(strIdSession, strIdSession, "FIN: DownloadFileServer ");
            return File(objFile, Tools.Utils.Functions.f_obtieneContentType(Path.GetExtension(strPath)));

        }

        public ParametersGeneratePDF GetParameterGeneratePDF(ConstancyClaroVideoHelper item)
        {
            ParametersGeneratePDF objGeneratePDF = new ParametersGeneratePDF();
            try
            {
                if (item != null)
                {
                    objGeneratePDF.strTipoConstanciaAMCO = item.strTipoConstanciaAMCO;
                    objGeneratePDF.StrNombreArchivoTransaccion = item.StrNombreArchivoTransaccion;
                    objGeneratePDF.strPuntoAtencion = item.strPuntoAtencion;
                    objGeneratePDF.strTitular = item.strTitular;
                    objGeneratePDF.strRepresentante = item.strRepresentante;
                    objGeneratePDF.strTipoDoc = item.strTipoDoc;
                    objGeneratePDF.strFechaAct = item.strFechaAct;
                    objGeneratePDF.StrNroCaso = item.strNroCaso;
                    objGeneratePDF.StrCasoInter = item.strNroCaso;
                    objGeneratePDF.StrNroServicio = item.strNroServicio;
                    objGeneratePDF.strNroDoc = item.strNroDoc;
                    objGeneratePDF.StrEmail = item.strEmail;
                    objGeneratePDF.StrCarpetaTransaccion = KEY.AppSettings("strCarpetaClaroVideoConsta");
                    if (item.ListService != null ? item.ListService.Count() > 0 ? true : false : false)
                    {
                        objGeneratePDF.ListService = new List<ClaroVideoServiceConstancy>();
                        foreach (ClaroVideoServiceConstancyHelper obj in item.ListService)
                        {
                            ClaroVideoServiceConstancy objItem = new ClaroVideoServiceConstancy() { strBajaServicios = obj.strBajaServicios };
                            objGeneratePDF.ListService.Add(objItem);
                        }
                    }

                    if (item.ListDevice != null ? item.ListDevice.Count() > 0 ? true : false : false)
                    {
                        objGeneratePDF.ListDevice = new List<ClaroVideoDeviceConstancy>();
                        foreach (ClaroVideoDeviceConstancyHelper obj in item.ListDevice)
                        {
                            ClaroVideoDeviceConstancy objItem = new ClaroVideoDeviceConstancy() { strDispotisitivoID = obj.strDispotisitivoID, strDispotisitivoNom = obj.strDispotisitivoNom, strFechaDesac = obj.strFechaDesac };
                            objGeneratePDF.ListDevice.Add(objItem);
                        }
                    }

                    if (item.ListSuscriptcion != null ? item.ListSuscriptcion.Count() > 0 ? true : false : false)
                    {
                        objGeneratePDF.ListSuscriptcion = new List<ClaroVideoSubscriptionConstancy>();
                        foreach (ClaroVideoSubscriptionConstancyHelper obj in item.ListSuscriptcion)
                        {
                            ClaroVideoSubscriptionConstancy objItem = new ClaroVideoSubscriptionConstancy() { strSuscTitulo = obj.strSuscTitulo, strSuscEstado = obj.strSuscEstado, strSuscPeriodo = obj.strSuscPeriodo, strSuscServicio = obj.strSuscServicio, strSuscPrecio = obj.strSuscPrecio, strSuscFechReg = obj.strSuscFechReg };
                            objGeneratePDF.ListSuscriptcion.Add(objItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return objGeneratePDF;
        }
        //FIN Luis D
        #endregion

        #region OnBase

        public JsonResult getKeyValue(string strNamekey, string strIdSession)
        {
            string strValueKey;
            try
            {
                strValueKey = KEY.AppSettings(strNamekey);
            }
            catch (Exception ex)
            {
                strValueKey = null;
                Claro.Web.Logging.Info(strIdSession, strIdSession, "ERROR: GetKey" + ex.Message);
            }
            return Json(new { data = strValueKey });
        }
        public JsonResult GenerarConstanciaOnBase(OnBaseTargetModel objRequest)
        {
            Claro.Web.Logging.Info(objRequest.IdSession, objRequest.FormatoTransaccion, "Execute GenerarConstanciaOnBase");
            CommonTransacService.ParametersGeneratePDF parameters = new CommonTransacService.ParametersGeneratePDF();
            byte[] objFile = null;
            CommonTransacService.GenerateConstancyResponseCommon response = new GenerateConstancyResponseCommon();
            OnBaseCargaResponse objResponse = new OnBaseCargaResponse();
            try
            {
                parameters.StrCasoInter = objRequest.Constancia.CASO_INTER;
                parameters.StrCarpetaTransaccion = KEY.AppSettings("strClaroVideoCarpetaTransaccion");
                parameters.StrNombreArchivoTransaccion = objRequest.FormatoTransaccion == null ? "CONSTANCIA_CLARO_VIDEO" : objRequest.FormatoTransaccion;
                parameters.StrCarpetaPDFs = KEY.AppSettings("strClaroVideoCarpetaPDFs");
                parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");

                objRequest.Constancia.FORMATO_TRANSACCION = objRequest.FormatoTransaccion;
                objRequest.Constancia.TRANSACCION = KEY.AppSettings("strClaroVideoConstanciaTransaccion") + objRequest.FormatoTransaccion;
                objRequest.Constancia.FECHA_SOLICITUD = DateTime.Now.ToString("dd/MM/yyyy");

                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                oCommonHandler.DisplayFileFromServerSharedFile(objRequest.IdSession, objRequest.IdSession, objRequest.FullPathPDF, out objFile);
                string strExtencion = KEY.AppSettings("strClaroVideoExtencionPDF");
                if (objFile != null)
                {
                    objRequest.Path = (objRequest.Document.ToUpper().Contains(strExtencion.ToUpper()) ? objRequest.Document : objRequest.Document + strExtencion);
                    objRequest.TipoOperacion = KEY.AppSettings("strClaroVideoConstanciaTransaccion") + objRequest.FormatoTransaccion;
                    objRequest.FechaRegistro = DateTime.Now.ToString("o");

                    return Json(new { codeResponse = "0", OnBase = OnBaseTarget(objRequest, objFile), Constancia = response });
                }
                else
                {
                    throw new Exception("GenerarConstanciaOnBase: No se genero la constancia " + objRequest.FormatoTransaccion);
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.IdSession, objRequest.FormatoTransaccion, ex.Message);
                throw new Claro.MessageException(objRequest.FormatoTransaccion);
            }
        }

        public OnBaseCargaResponse OnBaseTarget(OnBaseTargetModel objRequest, byte[] file)
        {
            Claro.Web.Logging.Info(objRequest.IdSession, objRequest.FormatoTransaccion, "Execute OnBaseTarget");
            Claro.Web.Logging.Info(objRequest.IdSession, objRequest.FormatoTransaccion,
            string.Format("Execute OnBaseTarget - Parámetros de entrada objRequest: [CodigoAsesor] - [{0}] ; [FormatoTransaccion] - [{1}] ; [metaDataName] - [{2}] ; [metaDataValue] - [{3}] ; [metaDatalength] - [{4}] ", objRequest.CodigoAsesor, objRequest.FormatoTransaccion, objRequest.strKeyWorkName, objRequest.strKeyWorkValue, objRequest.strKeyWorkLeng));
            FixedTransacService.OnBaseCargaResponse objResponse = new OnBaseCargaResponse();

            try
            {
                if (objRequest != null)
                {
                    string[] strMetadatosName = (objRequest.strKeyWorkName.Length > 0 ? objRequest.strKeyWorkName.Split(',') : null);
                    string[] strMetadatosValue = (objRequest.strKeyWorkValue.Length > 0 ? objRequest.strKeyWorkValue.Split(',') : null);
                    string[] strMetadatosLength = (objRequest.strKeyWorkLeng.Length > 0 ? objRequest.strKeyWorkLeng.Split(',') : null);

                    string valor;
                    List<FixedTransacService.metadatosOnBase> metaDatos = new List<FixedTransacService.metadatosOnBase>();

                    objRequest.FormatoTransaccion = objRequest.FormatoTransaccion;

                    for (int i = 0; i < strMetadatosValue.Length; i++)
                    {
                        try
                        {
                            valor = (string)objRequest.Constancia.GetType().GetProperty(strMetadatosValue[i]).GetValue(objRequest.Constancia);

                            if (valor != null) if (!valor.Trim().Equals("")) if (strMetadatosLength[i] != "0")
                                        valor = valor.Substring(0, int.Parse(strMetadatosLength[i]));

                            metaDatos.Add(new FixedTransacService.metadatosOnBase()
                            {
                                attributeName = strMetadatosName[i],
                                attributeValue = valor
                            });
                        }
                        catch (Exception ex)
                        {
                            valor = "";
                        }
                    }

                    //DATA DE PRUEBA
                    //OnBaseCargaRequest objRequestOn = new OnBaseCargaRequest();
                    //objRequestOn.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(objRequest.IdSession);
                    //objRequestOn.HeaderDPService = App_Code.Common.GetHeaderDataPowerRequest<FixedTransacService.HeaderDPRequest>(objRequest.Modulo, "CargaOnBase");
                    //objRequestOn.user = objRequest.CodigoAsesor;
                    //objRequestOn.metadatosOnBase = metaDatos;
                    //objRequestOn.SpecificationAttachmentOnBase = new FixedTransacService.SpecificationAttachmentOnBase()
                    //    {
                    //        name = objRequest.Path,
                    //        type = objRequest.FormatoTransaccion,//KEY.AppSettings("strLeyPromoTypeOnBase"),//Nombre de plantilla
                    //        listEntitySpectAttach = new FixedTransacService.entitySpecAttachExtensionOnBase()
                    //        {
                    //            ID = objRequest.IdSession,
                    //            fileBase64 = System.Convert.ToBase64String(file)
                    //        }
                    //    };

                    //objRequestOn.HeaderDPService.wsIp = "172.19.35.134";//PRUEBA
                    //objRequestOn.HeaderDPService.modulo = "OM";
                    //objRequestOn.HeaderDPService.userId = "USRTCRM";
                    //DATA DE PRUEBA

                    objResponse = _oServiceFixed.TargetDocumentoOnBase(new OnBaseCargaRequest()
                    {

                        audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(objRequest.IdSession),
                        HeaderDPService = App_Code.Common.GetHeaderDataPowerRequest<FixedTransacService.HeaderDPRequest>(objRequest.Modulo, "CargaOnBase"),
                        user = objRequest.CodigoAsesor,
                        metadatosOnBase = metaDatos,
                        SpecificationAttachmentOnBase = new FixedTransacService.SpecificationAttachmentOnBase()
                        {
                            name = objRequest.Path,
                            type = objRequest.FormatoTransaccion,
                            listEntitySpectAttach = new FixedTransacService.entitySpecAttachExtensionOnBase()
                            {
                                ID = objRequest.IdSession,
                                fileBase64 = System.Convert.ToBase64String(file)
                            }
                        }

                    });

                    //DATA DE PRUEBA
                    //objResponse = _oServiceFixed.TargetDocumentoOnBase(objRequestOn);
                    //DATA DE PRUEBA

                    Claro.Web.Logging.Info(objRequest.IdSession, objRequest.FormatoTransaccion, string.Format("Execute objResponse - Parámetros de salida objRequest: [CodigoOnBase] - [{0}] ;", objResponse.codeOnBase));

                    return objResponse;
                }
                else
                {
                    throw new Exception("objRequest es null");
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.IdSession, objRequest.IdSession, "ERROR: CargarOnBase" + ex.Message);
                return new FixedTransacService.OnBaseCargaResponse() { codeOnBase = "", codeResponse = "1", descriptionResponse = ex.Message };
            }
        }
        #endregion


        public JsonResult PersonalizaMensajeOTT(string strIdSession, WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTRequest objPersonalizaMensajeOTTRequest)
        {
            PersonalizaMensajeOTTResponse objResponse = new PersonalizaMensajeOTTResponse();
            PersonalizaMensajeOTTRequest objRequest = new PersonalizaMensajeOTTRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("personalizarMensaje")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTBodyRequest()
            {
                personalizarMensajeRequest = new PersonalizarMensajeRequest()
                {
                    correlatorId = objPersonalizaMensajeOTTRequest.MessageRequest.Body.personalizarMensajeRequest.correlatorId,
                    employeeId = objPersonalizaMensajeOTTRequest.MessageRequest.Body.personalizarMensajeRequest.employeeId,
                    mensajeAmco = objPersonalizaMensajeOTTRequest.MessageRequest.Body.personalizarMensajeRequest.mensajeAmco
  
                }

            };


            objRequest.MessageRequest.Body = objBodyRequest;
            objRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.PersonalizaMensajeOTTResponse>(() =>
                {
                    return _oServiceFixed.PersonalizaMensajeOTT(objRequest);
                });
            }
            catch (Exception ex)
            {
                objResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.PersonalizaMensajeOTTModel objPersonalizaMensajeOTTModel = new Models.Fixed.PersonalizaMensajeOTTModel();

            if (objResponse.MessageResponse != null)
            {
                if (objResponse.MessageResponse.Body != null)
                {
                    if (objResponse.MessageResponse.Body.personalizarMensajeResponse != null)
                    {
                        objPersonalizaMensajeOTTModel.PersonalizarMensajeResponse = new HELPER_ITEM.PersonalizarMensajeResponseHelper();
                        objPersonalizaMensajeOTTModel.PersonalizarMensajeResponse.codRpta = objResponse.MessageResponse.Body.personalizarMensajeResponse.codRpta;
                        objPersonalizaMensajeOTTModel.PersonalizarMensajeResponse.msjRpta = objResponse.MessageResponse.Body.personalizarMensajeResponse.msjRpta;
                        objPersonalizaMensajeOTTModel.PersonalizarMensajeResponse.mensajePersonalizado = objResponse.MessageResponse.Body.personalizarMensajeResponse.mensajePersonalizado;
                      
                    }
                }
            }

            JsonResult json = Json(new { data = objPersonalizaMensajeOTTModel });

            return json;
        }

        #region PROY-140510 - AMCO - Modulo de consulta y eliminar cuenta de Claro Video
        public JsonResult DeleteClientSN(string strIdSession, WebApplication.Transac.Service.FixedTransacService.DeleteClientSNRequest objRequestClientSN)
        {
            DeleteClientSNResponse objDeleteClientSNResponse = null;
            DeleteClientSNRequest objDeleteClientSNRequest = new DeleteClientSNRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new DeleteClientSNMessageRequest()
                {
                    Header = new DeleteClientSNHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("eliminarClienteSN")
                    },
                    Body = new DeleteClientSNBodyRequest()
                    {
                        deleteUserOttRequest = new deleteUserOttRequest()
                        {
                            invokeMethod = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.invokeMethod,
                            correlatorId = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.correlatorId,
                            countryId = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.countryId,
                            paymentMethod = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.paymentMethod,
                            userId = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.userId,
                            account = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.account,
                            employeeId = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.employeeId,
                            origin = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.origin,
                            serviceName = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.serviceName,
                            providerId = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.providerId,
                            iccidManager = objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.iccidManager,
                            extensionInfo = (objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.extensionInfo == null ? new List<WebApplication.Transac.Service.FixedTransacService.extensionInfo>() : objRequestClientSN.MessageRequest.Body.deleteUserOttRequest.extensionInfo)
                        }
                    }
                }

            };
            objDeleteClientSNRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            try
            {
                objDeleteClientSNResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.DeleteClientSNResponse>(() =>
                {
                    return _oServiceFixed.DeleteClientSN(objDeleteClientSNRequest);
                });

            }
            catch (Exception ex)
            {
                objDeleteClientSNResponse = null;
                Claro.Web.Logging.Error(strIdSession, objDeleteClientSNRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDeleteClientSNRequest.audit.transaction);
            }

            Areas.Transactions.Models.Fixed.DeleteClientSN objDeleteClientSN = new Models.Fixed.DeleteClientSN();

            if (objDeleteClientSNResponse.MessageResponse != null)
            {
                if (objDeleteClientSNResponse.MessageResponse.Body != null)
                {
                    if (objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse != null)
                    {
                        objDeleteClientSN.deleteUserOttResponse = new HELPER_ITEM.DeleteUserOttResponseHelper();
                        objDeleteClientSN.deleteUserOttResponse.resultCode = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.resultCode;
                        objDeleteClientSN.deleteUserOttResponse.resultMessage = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.resultMessage;
                        objDeleteClientSN.deleteUserOttResponse.correlatorId = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.correlatorId;
                        objDeleteClientSN.deleteUserOttResponse.countryId = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.countryId;
                        objDeleteClientSN.deleteUserOttResponse.serviceName = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.serviceName;
                        objDeleteClientSN.deleteUserOttResponse.providerId = objDeleteClientSNResponse.MessageResponse.Body.deleteUserOttResponse.providerId;

                    }
                }
            }

            return Json(new { data = objDeleteClientSN });

        }

        public JsonResult ConsultarLineaCuenta(string strIdSession, string tipo, string Valor)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            serviceAmco.ConsultaLineaResponse objResponse = new serviceAmco.ConsultaLineaResponse();
            serviceAmco.ConsultaLineaRequest objRequest = new serviceAmco.ConsultaLineaRequest();
            objRequest.Type = tipo;
            objRequest.Value = Valor;
            objRequest.audit = audit;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<serviceAmco.ConsultaLineaResponse>(() =>
                {
                    return _oServiceFixed.ConsultarLineaCuenta(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }
            return Json(new { data = objResponse });
        }

        public JsonResult ConsultarContrato(string strIdSession, string strIdTransaction, string strTelephone)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            serviceAmco.GetTypeProductDatResponse objResponse = null;
            serviceAmco.GetTypeProductDatRequest Request = new serviceAmco.GetTypeProductDatRequest()
            {
                IdTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss"),
                MsgId = audit.userName,
                TimesTamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                UserId = audit.userName,
                Channel = ConfigurationManager.AppSettings("ApplicationName"),
                IpAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                contrato = new serviceAmco.GetTypeProductDatRequest.Contrato()
                {
                    idContrato = "",
                    ofertaProducto = new serviceAmco.GetTypeProductDatRequest.OfertaProducto()
                    {
                        producto = new serviceAmco.GetTypeProductDatRequest.Producto()
                        {
                            recursoLogico = new serviceAmco.GetTypeProductDatRequest.RecursoLogico()
                            {
                                numeroLinea = strTelephone
                            }
                        }
                    },
                    caracteristicaAdicional = new serviceAmco.GetTypeProductDatRequest.CaracteristicaAdicional()
                    {
                        descripcion = KEY.AppSettings("keyTipoBusqueda").ToString(),
                        valor = KEY.AppSettings("keyContractSearchWithoutHistory").ToString()
                    }
                }
            };
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<serviceAmco.GetTypeProductDatResponse>(() =>
                {
                    return _oServiceFixed.ConsultarContrato(strIdSession, strIdTransaction, Request);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                throw new Claro.MessageException(strIdTransaction);
            }

            return Json(new { data = objResponse });
        }

        public JsonResult GetConsultaDatosLinea(string strIdSession, string strTelephone)
        {
            FixedTransacService.AuditRequest1 audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            serviceAmco.consultarDatosLineaResponse objResponse = null;
            audit.applicationName = "SIACPRE";
            audit.userName = "USRSIACPREPAGO";
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<serviceAmco.consultarDatosLineaResponse>(() =>
                {
                    return _oServiceFixed.GetConsultaDatosLinea(audit, strTelephone);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }
            var linea = objResponse.datoLineaField.tipoField;
            return Json(new { data = objResponse });
        }
        #endregion

        //INICIATIVA-794
        public JsonResult ValidarServicioIPTV(string strIdSession, string strCodNum, string strOpc)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            serviceAmco.ValidateIPTVResponse objResponse = new serviceAmco.ValidateIPTVResponse();
            serviceAmco.ValidateIPTVRequest objRequest = new serviceAmco.ValidateIPTVRequest();

            objRequest.strCodNum = strCodNum; //"36248716"; // 362487169 = 0 / 36248716 = 1;
            objRequest.strOpc = strOpc;
            objRequest.audit = audit;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<serviceAmco.ValidateIPTVResponse>(() =>
                {
                    return _oServiceFixed.ValidarServicioIPTV(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }
            
            return Json(new { data = objResponse.lstValidateIPTV[0].VALIDACION });
        }

        public JsonResult ConsultarServicioIPTV(string strIdSession, string strProducto)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            serviceAmco.ConsultIPTVResponse objResponse = new serviceAmco.ConsultIPTVResponse();
            serviceAmco.ConsultIPTVRequest objRequest = new serviceAmco.ConsultIPTVRequest();

            objRequest.strProducto = strProducto;
            objRequest.audit = audit;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<serviceAmco.ConsultIPTVResponse>(() =>
                {
                    return _oServiceFixed.ConsultarServicioIPTV(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }

            return Json(new { data = objResponse.lstConsultIPTV });
        }
    }
}