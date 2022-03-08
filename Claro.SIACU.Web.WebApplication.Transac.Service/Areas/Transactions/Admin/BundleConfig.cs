using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Admin
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/bootstrap-addon-css-siacu-txs").Include(
                "~/Areas/Transactions/Content/css/bootstrap.css",
                "~/Areas/Transactions/Content/css/font-awesome.css",
                "~/Areas/Transactions/Content/css/dataTables.bootstrap.min.css",
                "~/Areas/Transactions/Content/css/jquery.dataTables.select.css",
                "~/Areas/Transactions/Content/css/datepicker.css",
                "~/Areas/Transactions/Content/css/jquery.smartmenus.bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-addon-css-siacu-txs")
                .Include("~/Areas/Transactions/Content/css/jquery-ui.css",
                    "~/Areas/Transactions/Content/css/jquery.bar.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/Site-css-siacu-txs").Include(
                "~/Areas/Transactions/Content/css/Site.css",
                "~/Areas/Transactions/Content/css/Header.css",
                "~/Areas/Transactions/Content/css/Footer.css",
                "~/Areas/Transactions/Content/css/MyContainer.css",
                "~/Areas/Transactions/Content/css/TreeView.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/Site-Transactions-txs").Include(
               "~/Areas/Transactions/ContentTransac/css/Site-Transaction.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-siacu").Include(
                "~/Areas/Transactions/Content/Lib/jquery-2.0.0.js",
                "~/Areas/Transactions/Content/Lib/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval-siacu").Include(
                "~/Areas/Transactions/Content/Lib/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-siacu").Include(
                "~/Areas/Transactions/Content/Lib/bootstrap.js",
                "~/Areas/Transactions/Content/Lib/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-addon-siacu").Include(
                "~/Areas/Transactions/Content/Lib/jquery.dataTables.min.js",
                "~/Areas/Transactions/Content/Lib/jquery.dataTables.select.js",
                "~/Areas/Transactions/Content/Lib/jquery.blockUI.js",
                "~/Areas/Transactions/Content/Lib/jquery.smartmenus.js",
                "~/Areas/Transactions/Content/Lib/jquery.smartmenus.bootstrap.js",
                "~/Areas/Transactions/Content/Lib/jquery.numeric.js",
                "~/Areas/Transactions/Content/Lib/dataTables.bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment-js-siacu")
                .Include("~/Areas/Transactions/Content/Lib/moment.js",
                    "~/Areas/Transactions/Content/Lib/moment-es.js"));


            bundles.Add(new ScriptBundle("~/bundles/Claro-siacu")
                .Include("~/Areas/Transactions/Content/Scripts/ClaroSession.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroAppCommon.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroRedirect.js",
                    "~/Areas/Transactions/Content/Scripts/plupload.full.min.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroModalTemplate.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroModalLoad.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroCommon.js",
                    "~/Areas/Transactions/Content/Scripts/ClaroUtils.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker-siacu")
                .Include("~/Areas/Transactions/Content/Lib/bootstrap-datepicker.js"));


            bundles.Add(new ScriptBundle("~/bundles/steps").Include(
                "~/Areas/Transactions/ContentTransac/lib/steps.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/Redirect/Redirect")
             .Include("~/Areas/Transactions/Scripts/Redirect/Redirect.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/Postpaid/ChangePhoneNumber")
             .Include("~/Areas/Transactions/Scripts/ChangePhoneNumber/PostpaidChangePhoneNumber.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/ChangePhoneNumber")
                .Include("~/Areas/Transactions/Scripts/ChangePhoneNumber/HFCChangePhoneNumber.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/ChangePhoneNumber")
                .Include("~/Areas/Transactions/Scripts/ChangePhoneNumber/LTEChangePhoneNumber.js"));




            bundles.Add(new ScriptBundle("~/bundles/numeric").Include(
            "~/Content/Lib/jquery.numeric.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/IncomingCallDetail").Include("~/Areas/Transactions/Scripts/IncomingCallDetail.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/PostPlanMigration/PostPlanMigration").Include(
                "~/Areas/Transactions/Scripts/PostpaidPlanMigration/PostpaidPlanMigration.js",
                "~/Areas/Transactions/Scripts/PostpaidPlanMigration/PostpaidPlansMigrations.js"
                ));

            
            bundles.Add(new ScriptBundle("~/bundles/Transactions/PostpaidChangeTypeCustomer/PostpaidChangeTypeCustomer")
                .Include("~/Areas/Transactions/Scripts/PostpaidChangeTypeCustomer/PostpaidChangeTypeCustomer.js"));

         
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/AdditionalServices")
                .Include("~/Areas/Transactions/Scripts/AdditionalServices/HFCAdditionalServices.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/Prepaid/BilledOutCallDetail")
                .Include("~/Areas/Transactions/Scripts/BilledOutCallDetail/PrepaidBilledOutCallDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/Postpaid/BilledOutCallDetail")
                .Include("~/Areas/Transactions/Scripts/BilledOutCallDetail/PostpaidBilledOutCallDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ExternalInternalTransfer/HFCExternalInternalTransfer")
                .Include("~/Areas/Transactions/Scripts/ExternalInternalTransfer/HFCExternalInternalTransfer.js")); 

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ExternalInternalTransfer/LTEExternalInternalTransfer")
               .Include("~/Areas/Transactions/Scripts/ExternalInternalTransfer/LTEExternalInternalTransfer.js"));

           
            bundles.Add(new ScriptBundle("~/bundles/Transactions/Postpaid/IncomingCallDetail")
                .Include("~/Areas/Transactions/Scripts/IncomingCallDetail/PostpaidIncomingCallDetail.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/Transactions/IncomingCallDetail/PrepaidIncomingCallDetail").Include("~/Areas/Transactions/Scripts/IncomingCallDetail/PrepaidIncomingCallDetail.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/UnbilledOutCallDetail/PostpaidUnbilledOutCallDetail").Include("~/Areas/Transactions/Scripts/UnbilledOutCallDetail/PostpaidUnbilledOutCallDetail.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/Maintenance").Include("~/Areas/Transactions/Scripts/Maintenance/MaintenanceHFC.js"));

            #region CambioPlanes
            bundles.Add(new StyleBundle("~/bundles/HfcMigracionPlan").Include(
                "~/Areas/Transactions/Content/css/HfcMigracionPlan.css",
                "~/Areas/Transactions/Content/css/jquery-ui-1.10.3.custom.css"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Transaction/HfcPlanMigration/PlanMigration")
                .Include("~/Areas/Transactions/ContentTransac/lib/steps.js",
                         "~/Areas/Transactions/Scripts/AuthUser/AuthUser.js",
                         "~/Areas/Transactions/Scripts/ReadRecordPdf/ReadRecord.js",
                         "~/Areas/Transactions/Scripts/PlanMigration/HFCPlanMigration.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transaction/HfcPlanMigration/ChoosePlan").Include("~/Areas/Transactions/Scripts/HfcPlanMigration/SelectPlan.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transaction/PrepaidTFIPlanMigration/PlanMigration").Include(
                 "~/Areas/Transactions/Scripts/PlanMigration/PrepaidTFIPlanMigration.js"));
            #endregion

            #region CambioPlanesLTE
            bundles.Add(new ScriptBundle("~/bundles/Transaction/LtePlanMigration/PlanMigration").Include("~/Areas/Transactions/Scripts/PlanMigration/LTEPlanMigration.js"));
            #endregion


            
            #region AdditionalPoints
            bundles.Add(new ScriptBundle("~/bundles/Transactions/AdditionalPoints/HFCAdditionalPoints").Include(
                "~/Areas/Transactions/Scripts/AdditionalPoints/HFCAdditionalPoints.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/AdditionalPoints/HFCAdditionalPointsConstPrint").Include(
                    "~/Areas/Transactions/Scripts/AdditionalPoints/HFCAdditionalPointsConstPrint.js"));


            bundles.Add(new ScriptBundle("~/bundles/Transactions/AdditionalPoints/LTEAdditionalPoints").Include(
                    "~/Areas/Transactions/Scripts/AdditionalPoints/LTEAdditionalPoints.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/AdditionalPoints/LTEAdditionalPointsConstPrint").Include(
                  "~/Areas/Transactions/Scripts/AdditionalPoints/LTEAdditionalPointsConstPrint.js"));
            #endregion

            #region ConfigurationIP
            bundles.Add(new ScriptBundle("~/bundles/Transactions/ConfigurationIP/HFCConfigurationIP").Include(
                    "~/Areas/Transactions/Scripts/ConfigurationIP/HFCConfigurationIP.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ConfigurationIP/HFCConfigurationIPConstPrint").Include(
                        "~/Areas/Transactions/Scripts/ConfigurationIP/HFCConfigurationIPConstPrint.js"));

            #endregion
           

            bundles.Add(new ScriptBundle("~/bundles/Transactions/Auth/AuthUser")
                .Include("~/Areas/Transactions/Scripts/AuthUser/AuthUser.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCUnbilledCallDetail")
                .Include("~/Areas/Transactions/Scripts/CallDetails/HFCUnbilledCallDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCBilledCallsDetail")
                .Include("~/Areas/Transactions/Scripts/SessionArea.js")
                .Include("~/Areas/Transactions/Scripts/CallDetails/HFCBilledCallsDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/LTEAdditionalServices")
                .Include("~/Areas/Transactions/Scripts/SessionArea.js")
                .Include("~/Areas/Transactions/Scripts/AdditionalServices/LTEAdditionalServices.js"));

            #region LLamadas entrantes HFC/LTE
            bundles.Add(new ScriptBundle("~/bundles/Transactions/Hfc/HfcIncomingCallDetail")
                .Include("~/Areas/Transactions/Scripts/ReadRecordPdf/ReadRecord.js")
                .Include("~/Areas/Transactions/Scripts/AuthUser/AuthUser.js")
                .Include("~/Areas/Transactions/Scripts/IncomingCallDetail/HfcIncomingCallDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HfcIncomingCallDetailPrint")
                .Include("~/Areas/Transactions/Scripts/IncomingCallDetail/HfcIncomingCallDetailPrint.js"));


            #endregion

            #region Tareas Programadas

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HfcProgramTask")
                .Include("~/Areas/Transactions/Scripts/ProgramTask/HfcProgramTask.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/LteProgramTask")
                .Include("~/Areas/Transactions/Scripts/ProgramTask/LteProgramTask.js"));

            #endregion

            #region Instalacion Desinstalacion de Decos
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/UnistallInstallationOfDecoder")
                .Include("~/Areas/Transactions/Scripts/UnistallInstallationOfDecoder/HFCPOSTSession.js")
                .Include("~/Areas/Transactions/Scripts/UnistallInstallationOfDecoder/UnistallInstallationOfDecoder.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCListAddtionalEquipment")
                .Include("~/Areas/Transactions/Scripts/UnistallInstallationOfDecoder/HFCListAddtionalEquipment.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCListUninstallEquipment")
                .Include("~/Areas/Transactions/Scripts/UnistallInstallationOfDecoder/HFCListUninstallEquipment.js"));
            #endregion

            #region Retención Cancelación
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCRetentionCancelServices")
                .Include("~/Areas/Transactions/Scripts/RetentionCancelServices/HFCRetentionCancelServices.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/LTERetentionCancelServices")
            .Include("~/Areas/Transactions/Scripts/RetentionCancelServices/LTERetentionCancelServices.js"));
            
            #endregion

            #region Proy-32650 Fidelización
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HFCFidelizacionServices")
                .Include("~/Areas/Transactions/Scripts/FidelizacionServices/HFCFidelizacionServices.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/LTEFidelizacionServices")
                .Include("~/Areas/Transactions/Scripts/FidelizacionServices/LTEFidelizacionServices.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/HfcBilledCallsDetailPrint")
                .Include("~/Areas/Transactions/Scripts/CallDetails/HFCBilledCallsDetailPrint.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ReadRecordPDF/ReadRecord")
                .Include("~/Areas/Transactions/Scripts/ReadRecordPdf/ReadRecord.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/HFC/CheckDevices")
                .Include("~/Areas/Transactions/Scripts/CheckDevices/HfcCheckDevices.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/CheckDevices")
                .Include("~/Areas/Transactions/Scripts/CheckDevices/LteCheckDevices.js"));

            #region Cambio de Equipo
            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/ChangeEquipment")
                .Include("~/Areas/Transactions/Scripts/ChangeEquipment/LTEChangeEquipment.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTE/ChangeEquipmentAssociate")
                .Include("~/Areas/Transactions/Scripts/ChangeEquipment/LTEChangeEquipmentAssociate.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/DTH/ChangeEquipment")
                .Include("~/Areas/Transactions/Scripts/ChangeEquipment/DTHChangeEquipment.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/DTH/ChangeEquipmentAssociate")
                .Include("~/Areas/Transactions/Scripts/ChangeEquipment/DTHChangeEquipmentAssociate.js"));
            #endregion

            #region Reposicion de Equipo
            bundles.Add(new ScriptBundle("~/bundles/Transactions/Fixed/ReplaceEquipment")
                .Include("~/Areas/Transactions/Scripts/ReplaceEquipment/FixedReplaceEquipment.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/Fixed/ReplaceEquipmentAssociate")
                .Include("~/Areas/Transactions/Scripts/ReplaceEquipment/FixedReplaceEquipmentAssociate.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/Transactions/RecordEquipmentForeign/RecordEquipmentForeign")
               .Include("~/Areas/Transactions/Scripts/RecordEquipmentForeign/RecordEquipmentForeign.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/UnlinkingEquipment/UnlinkingEquipment")
               .Include("~/Areas/Transactions/Scripts/UnlinkingEquipment/UnlinkingEquipment.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/SearchIMEI/SearchIMEI")
                .Include("~/Areas/Transactions/Scripts/SearchIMEI/SearchIMEI.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/SearchQuestionsAnswerSecurityRequest/SearchQuestionsAnswerSecurityRequest")
                .Include("~/Areas/Transactions/Scripts/SearchQuestionsAnswerSecurityRequest/SearchQuestionsAnswerSecurityRequest.js"));

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ReceptionEquipment/ReceptionEquipment")
               .Include("~/Areas/Transactions/Scripts/ReceptionEquipment/ReceptionEquipment.js"));

           

            bundles.Add(new ScriptBundle("~/bundles/Transactions/ContentInfoPromotion/ContentInfoPromotion").Include(
                "~/Areas/Transactions/Scripts/InfoPromotionPrePost/InfoPromotion.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/Transactions/RestricSellInfoPromotion").Include("~/Areas/Transactions/Scripts/RestricSellInfoPromotion/RestricSellInfoPromotion.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/RestricSellInfoPromotionLib").Include("~/Areas/Transactions/Scripts/RestricSellInfoPromotion/RestricSellInfoPromotionLib.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/UploadInfoProm").Include("~/Areas/Transactions/Scripts/RestricSellInfoPromotion/UploadInfoProm.js"));



            bundles.Add(new ScriptBundle("~/bundles/Transactions/DiscardListPost").Include("~/Areas/Transactions/Scripts/Discard/DiscardListPost.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/DiscardListPre").Include("~/Areas/Transactions/Scripts/Discard/DiscardListPre.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/DiscardLib").Include("~/Areas/Transactions/Scripts/Discard/DiscardLib.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/Transactions/SuscripcionClaroVideo").Include("~/Areas/Transactions/Scripts/ClaroVideo/SuscripcionClaroVideo.js"));                   
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HistoryActivateService").Include("~/Areas/Transactions/Scripts/ClaroVideo/HistoryActivateService.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HistoryRentalUser").Include("~/Areas/Transactions/Scripts/ClaroVideo/HistoryRentalUser.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/HistoryVisualizationClient").Include("~/Areas/Transactions/Scripts/ClaroVideo/HistoryVisualizationClient.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/ViewHistoryDevice").Include("~/Areas/Transactions/Scripts/ClaroVideo/ViewHistoryDevice.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/ClaroVideoLib").Include("~/Areas/Transactions/Scripts/ClaroVideo/ClaroVideoLib.js"));
            bundles.Add(new ScriptBundle("~/bundles/Transactions/ViewServiceAdditional").Include("~/Areas/Transactions/Scripts/ClaroVideo/ViewServiceAdditional.js"));   
            bundles.Add(new ScriptBundle("~/bundles/Transactions/LTEPackagePurchaseServices").Include(
                "~/Areas/Transactions/Scripts/PackagePurchaseServices/LTEPackagePurchaseServices.js",
                "~/Areas/Transactions/Scripts/ReadRecordPdf/ReadRecord.js"
                ));   
        }
    }
}