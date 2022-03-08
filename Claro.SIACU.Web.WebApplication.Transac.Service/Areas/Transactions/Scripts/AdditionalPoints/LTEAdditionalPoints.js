function CloseValidation(obj, pag, controls) {
    var mensaje;
    if (obj.hidAccion === 'G') {// Correcto

    } else { //if (obj.hidAccion == 'F') {
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        $('#cboSchedule option[value="-1"]').prop('selected', true);
        return;
    }
};

(function ($, undefined) {
    

    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    
    var tb_AdditionalPoints;

    //URL
    var URL_GetJobType = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetJobType';
    var URL_GetOrderType = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetOrderType';
    var URL_GetMotiveSot = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetMotiveSOTByTypeJob2';
    var URL_GetDocumentType = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetDocumentType';
    var URL_GetAttachedQuantity = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetAttachedQuantity';
    var URL_GetCacDat = window.location.protocol + '//' + window.location.host + '/Transactions/CommonServices/GetCacDacType';
    var URL_GetValidateETA = window.location.protocol + '//' + window.location.host + '/Transactions/SchedulingToa/GetValidateETA';
    var URL_GetParameter = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetParameter';
    var URL_GetProductDetailt = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetProductDetailt';
    var URL_AdditionalPointsSave = window.location.protocol + '//' + window.location.host + "/Transactions/LTE/AdditionalPoints/AdditionalPointsSave";
    var URL_GetConsultIGV = window.location.protocol + '//' + window.location.host + '/Transactions/CommonServices/GetConsultIGV';
    var URL_Constancy = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/LTEAdditionalPointsConstPrint';
    var URL_Constancy_PDF = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GenerateContancy';
    //Model
    var AdditionalPointsModel = {};

    // Response
    AdditionalPointsModel.strJobTypeComplementarySalesLTE = "";
    AdditionalPointsModel.strInternetValue = "";
    
    AdditionalPointsModel.strCableValue = "";
    AdditionalPointsModel.strCustomerRequestId = "667";
    AdditionalPointsModel.strServerName = "";
    AdditionalPointsModel.strLocalAddress = "";
    AdditionalPointsModel.strHostName = "";
    AdditionalPointsModel.strTitlePageAdditionalPoints = "";
    AdditionalPointsModel.strMessageConfirmAdditionsPoints = "";
    AdditionalPointsModel.strMessageEnterMail = "";
    AdditionalPointsModel.strMessageValidateMail = "";
    AdditionalPointsModel.strMessageValidatePointCare = "";
    AdditionalPointsModel.strMessageValidatePhone = "";
    AdditionalPointsModel.strMessageValidateTimeZone = "";
    AdditionalPointsModel.strMessageValidateSchedule = "";
    AdditionalPointsModel.strDateServer = "";
    AdditionalPointsModel.strMessageConsultationDisabilityNotAvailable = "";
    AdditionalPointsModel.strMessageOK = "";
    AdditionalPointsModel.strRouteSiteInitial = "";
    AdditionalPointsModel.strJobTypeMaintenance = "";
    AdditionalPointsModel.strJobTypeMaintenance_Bs = "";
    AdditionalPointsModel.strJobTypeRetention = "";
    AdditionalPointsModel.strJobTypePoints = "";
    AdditionalPointsModel.strJobTypeDefault = "";
    AdditionalPointsModel.strJobTypeLoyalty = "";
    AdditionalPointsModel.strMessageMaxProgDay = "";
    AdditionalPointsModel.strMessageDateAppNotLowerNow = "";
    AdditionalPointsModel.strMessageGenericBackOffice = "";
    AdditionalPointsModel.strMessageGenericBackOfficeBucked = "";
    AdditionalPointsModel.strMessageCustomerContractEmpty = "";
    AdditionalPointsModel.strMessageNotServiceCableInternet = "";
    AdditionalPointsModel.strHourServer = "";
    AdditionalPointsModel.strMessageForcesSSTTETA = "";
    AdditionalPointsModel.strMessageNotTimeZoneHourETA = "";

    AdditionalPointsModel.bErrorTransac = false;
    AdditionalPointsModel.strMessageErrorTransac = "";
    //Request
    AdditionalPointsModel.strJobTypes = "";
    AdditionalPointsModel.strInternetValue = "";
    AdditionalPointsModel.strCellPhoneValue = "";
    AdditionalPointsModel.strCableValue = "";
    AdditionalPointsModel.strCodePlanInst = "";

    AdditionalPointsModel.strTelephone = "";
    AdditionalPointsModel.strCurrentUser = "";
    AdditionalPointsModel.strFullName = "";
    AdditionalPointsModel.strLastName = "";
    AdditionalPointsModel.strBusinessName = "";
    AdditionalPointsModel.strDocumentType = "";
    AdditionalPointsModel.strDocumentNumber = "";
    AdditionalPointsModel.strAddress = "";
    AdditionalPointsModel.strDistrict = "";
    AdditionalPointsModel.strDepartament = "";
    AdditionalPointsModel.strProvince = "";
    AdditionalPointsModel.strModality = "";
    AdditionalPointsModel.strCustomerId = "";
    AdditionalPointsModel.strContractId = "";
    AdditionalPointsModel.strCaseID = "";
    AdditionalPointsModel.strValidateETA = "";
    AdditionalPointsModel.strRequestActId = "";
    AdditionalPointsModel.strDateProgramming = "";
    AdditionalPointsModel.strSchedule = "";
    AdditionalPointsModel.strMotiveSot = "";
    AdditionalPointsModel.strAttachedQuantity = "";
    AdditionalPointsModel.strCodSOT = "";
    AdditionalPointsModel.bErrorGenericCodSot = false;
    AdditionalPointsModel.strMonthEmision = "";
    AdditionalPointsModel.strYearEmision = "";
    AdditionalPointsModel.strTransaction = "";
    AdditionalPointsModel.strCodeTipification = "";
    AdditionalPointsModel.strNote = "";
    AdditionalPointsModel.strPlan = "";
    AdditionalPointsModel.strEmail = "";
    AdditionalPointsModel.strLegalRepresent = "";
    AdditionalPointsModel.strLegalDepartament = "";
    AdditionalPointsModel.strLegalProvince = "";
    AdditionalPointsModel.strLegalDistrict = "";
    AdditionalPointsModel.strDescCacDac = "";
    AdditionalPointsModel.strDescJobType = "";
    AdditionalPointsModel.strDescMotive = "";
    AdditionalPointsModel.strAmount = "";
    addEventListener.strFirstName = "";
    AdditionalPointsModel.strDescServicesType = "";
    AdditionalPointsModel.strAddressInst = "";
    AdditionalPointsModel.strUbigeoInst = "";
    AdditionalPointsModel.strCountry = "";

    AdditionalPointsModel.bGeneratedPDF = false;
    AdditionalPointsModel.strFullPathPDF ="";     
    
    AdditionalPointsModel.strHistoryETA = "";
 
    AdditionalPointsModel.strIGV = "";
    AdditionalPointsModel.strMensajeErrorConsultaIGV = "";

    var Form = function ($element, options) {

        $.extend(this, $.fn.LTEAdditionalPoints.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            // Hidden
            //, hdnRequestActId               : $("#hdnRequestActId", $element)
            //, hndServerDate                 : $("#hndServerDate", $element)
            //, hdnValidateHistory            : $("#hdnValidateHistory", $element)
            //, hdnValidateETA                : $("#hdnValidateETA", $element)
            //ComboBox
            , cboCacDac                     : $('#ddlCACDAC', $element)
            , cboMotivoSot                  : $('#cboMotivoSot', $element)
            , cboJobTypes                   : $('#cboJobTypes', $element)
            , cboServicesType               : $('#cboServicesType', $element)
            , cboAttachedQuantity           : $('#cboAttachedQuantity', $element)
            , cboSubJobTypes                : $('#cboSubJobTypes', $element)            

            // TextBox
             , txtNote                      : $("#txtNote", $element)
             , txt_SendMail                 : $('#txt_SendMail', $element)
             , txt_phone_references         : $('#txt_phone_references')
             , txt_monto                    : $("#txt_monto", $element)
            // Button

            , btnCerrar                     : $('#btnCerrar', $element)
            , btnConstancia                 : $('#btnConstancia', $element)
            , btnGuardar                    : $('#btnGuardar', $element)

            // Table 
            , tb_ProductDetailts            : $('#tb_ProductDetailts', $element)

            // Modal
            , ModalLoading                  : $('#ModalLoading', $element)
            //UC - Combos            
            , cboSchedule                   : $("#cboSchedule", $element)
            //UC -TextBox
            , txt_dDateProgramming          : $("#txt_dDateProgramming", $element)

            // UC - Buttom
            , btnValidateSchedule           : $('#btnValidateSchedule', $element)

            //Label - Customer
            , lblContact                    : $("#lblContact", $element)
            , lblContract                   : $('#lblContract', $element)
            , lblCustomerName               : $("#lblCustomerName", $element)
            , lblIdentificationDocument     : $("#lblIdentificationDocument", $element)
            , lblTypeCustomer               : $("#lblTypeCustomer", $element)
            , lblDateActivation             : $("#lblDateActivation", $element)
            , lblCycleBilling               : $("#lblCycleBilling", $element)
            , lblReprLegal                  : $("#lblReprLegal", $element)
            , lblDocReprLegal               : $("#lblDocReprLegal", $element)
            , lblPlanName                   : $("#lblPlanName", $element)
            , lblCycleFacture               : $("#lblCycleFacture", $element)
            , lblLimitCredit                : $("#lblLimitCredit", $element)
            , lblCustomerId                 : $("#lblCustomerId", $element)

            //Label - Direccion de Instalación
            , lblAddress                    : $("#lblAddress", $element)
            , lblAddressNote                : $("#lblAddressNote", $element)
            , lblPais                       : $("#lblPais", $element)
            , lblDepartamento               : $("#lblDepartamento", $element)
            , lblProvincia                  : $("#lblProvincia", $element)
            , lblDistrito                   : $("#lblDistrito", $element)
            , lblCodePlans                  : $("#lblCodePlans", $element)
            , lblUbigeo                     : $("#lblUbigeo", $element)

            //CheckBox
            , chk_SendMail                  : $("#chk_SendMail", $element)
            , chk_Fidelidad                 : $("#chk_Fidelidad", $element)

            , spnMainTitle                  : $('#lblTitle', $element)
            , lblLTENroSot                  : $("#lblLTENroSot", $element)
        });
    }

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
                controls = this.getControls();
            that.f_Loading();
            controls.btnGuardar.addEvent(that, 'click', that.btnSave_Click);
            controls.btnConstancia.addEvent(that, 'click', that.AdditionPointPrint_Click);
            controls.btnCerrar.addEvent(that, 'click', that.btnClose_Click);
            controls.cboJobTypes.addEvent(that, 'change', that.cboJobTypes_Change);
            controls.cboSubJobTypes.addEvent(that, 'change', that.cboSubJobTypes_Change);
            controls.chk_SendMail.addEvent(that, 'click', that.f_ActiveEmail);
            controls.chk_Fidelidad.addEvent(that, 'click', that.f_ActiveFidelidad);

            that.windowAutoSize();
            that.maximizarWindow();
            that.f_GetDataSession();
            that.f_GetPlugin();
            that.render();
            $.unblockUI();

        },

        render: function () {
            var that = this,
                controls = this.getControls();
          
            that.f_PageLoad();
            that.getIGV();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },

        f_GetDataSession: function () {
            var that = this,
                controls = this.getControls();

            controls.spnMainTitle.text("PUNTO ADICIONAL - LTE");

            //console.log"Redireccionó a la Transacion");

            Session.IDSESSION = '20170518150515737831';
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;

            Session.codOpcion = "srtCodOpcionPuntosAdicionalesLTE";

            Session.contractID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;

        },
        f_PageLoad: function () {
            var that = this,
              controls = that.getControls(),
              oCustomer = Session.DATACUSTOMER,
              oUserAccess = Session.USERACCESS,
              oDataService = Session.DATASERVICE;

            that.f_GetParameter();
          
            if (oUserAccess == null || oCustomer == null) {
                that.btnClose_Click();
                opener.parent.top.location.href = AdditionalPointsModel.strRouteSiteInitial;
                controls.btnGuardar.prop("disabled", true);
                return;
            }
            if (oUserAccess.userId == null || oUserAccess.userId == '0' || oUserAccess.userId == '' || oUserAccess.userId == '&nbsp;') {
                that.btnClose_Click();
                opener.parent.top.location.href = AdditionalPointsModel.strRouteSiteInitial;
                controls.btnGuardar.prop("disabled", true);
                return;
            }

            if (oCustomer.ContractID == null || oCustomer.ContractID == '0' || oCustomer.ContractID == '' || oCustomer.ContractID == '&nbsp;') {
                alert(AdditionalPointsModel.strMessageCustomerContractEmpty, "Alerta", function () { that.btnClose_Click(); });
                controls.btnGuardar.prop("disabled", true);
                return;
            }


            if (oDataService.CableValue == null && oDataService.InternetValue == null) {
                alert(AdditionalPointsModel.strMessageNotServiceCableInternet, "Alerta", function () { that.btnClose_Click(); });
                controls.btnGuardar.prop("disabled", true);
                return;
            }

            if (oDataService.CableValue == 'F' && oDataService.InternetValue == 'F') {
                alert(AdditionalPointsModel.strMessageNotServiceCableInternet, "Alerta", function () { that.btnClose_Click(); });
                controls.btnGuardar.prop("disabled", true);
                return;
            }            

            document.getElementById(controls.txt_SendMail.attr("id")).style.display = 'none';

            controls.btnConstancia.prop("disabled", true);

            that.f_Initial();
            that.f_GetAddress();
            that.f_GetJobType();
            that.f_GetMotiveSot(controls.cboJobTypes.val());
            that.getCACDAC();
            that.f_GetProductDetailt();

        },
        f_Initial: function () {
            var that = this,
             controls = that.getControls(),
             oCustomer = Session.DATACUSTOMER,
             oUserAccess = Session.USERACCESS,
             oDataService = Session.DATASERVICE;
            $("#txt_SendMail").val(oCustomer.Email);
        },
        f_GetAddress: function () {
            var that = this,
             controls = that.getControls(),
             oCustomer = Session.DATACUSTOMER,
             oUserAccess = Session.USERACCESS;
        },
        f_GetParameter: function () {
            var that = this,
              controls = that.getControls(),
              oCustomer = Session.DATACUSTOMER,
              oUserAccess = Session.USERACCESS,
              oDataService = Session.DATASERVICE,
                Model = {};

            var myUrl = URL_GetParameter;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(Model),
                url: myUrl,
                async: false,
                success: function (response) {
                    if (response != null) {

                        Session.ServerDate = response.strDateServer;
                        Session.DateNew = response.strDateNew;

                        AdditionalPointsModel.strInternetValue = oDataService.InternetValue; //(oDataService.InternetValue == null) ? "F" : oDataService.InternetValue;
                        AdditionalPointsModel.strCellPhoneValue = oDataService.TelephonyValue; //(oDataService.TelephonyValue == null) ? "F" : oDataService.TelephonyValue;
                        AdditionalPointsModel.strCableValue = oDataService.CableValue; //(oDataService.CableValue == null) ? "F" : oDataService.CableValue;

                        if ($.trim(response.strJobTypeComplementarySalesLTE) == "") { response.strJobTypeComplementarySalesLTE = 700; }
                        AdditionalPointsModel.strJobTypeComplementarySalesLTE = response.strJobTypeComplementarySalesLTE;
                        AdditionalPointsModel.strCustomerRequestId = response.strCustomerRequestId;
                        AdditionalPointsModel.strServerName = response.strServerName;
                        AdditionalPointsModel.strLocalAddress = response.strLocalAddress;
                        AdditionalPointsModel.strHostName = response.strHostName;
                        AdditionalPointsModel.strTitlePageAdditionalPoints = response.strTitlePageAdditionalPoints;
                        AdditionalPointsModel.strMessageConfirmAdditionsPoints = response.strMessageConfirmAdditionsPoints;
                        AdditionalPointsModel.strMessageEnterMail = response.strMessageEnterMail;
                        AdditionalPointsModel.strMessageValidateMail = response.strMessageValidateMail;
                        AdditionalPointsModel.strMessageValidatePointCare = response.strMessageValidatePointCare;
                        AdditionalPointsModel.strMessageValidatePhone = response.strMessageValidatePhone;
                        AdditionalPointsModel.strMessageValidateTimeZone = response.strMessageValidateTimeZone;
                        AdditionalPointsModel.strMessageValidateSchedule = response.strMessageValidateSchedule;
                        AdditionalPointsModel.strDateServer = response.strDateServer;
                        AdditionalPointsModel.strMessageConsultationDisabilityNotAvailable = response.strMessageConsultationDisabilityNotAvailable;
                        AdditionalPointsModel.strMessageOK = response.strMessageOK;
                        AdditionalPointsModel.strRouteSiteInitial = response.strRouteSiteInitial;

                        AdditionalPointsModel.strJobTypeMaintenance = response.strJobTypeMaintenance;
                        AdditionalPointsModel.strJobTypeMaintenance_Bs = response.strJobTypeMaintenance_Bs;
                        AdditionalPointsModel.strJobTypeRetention = response.strJobTypeRetention;
                        AdditionalPointsModel.strJobTypePoints = response.strJobTypePoints;
                        AdditionalPointsModel.strJobTypeDefault = response.strJobTypeDefault;
                        AdditionalPointsModel.strJobTypeLoyalty = response.strJobTypeLoyalty;

                        AdditionalPointsModel.strMessageMaxProgDay = response.strMessageMaxProgDay;
                        AdditionalPointsModel.strMessageDateAppNotLowerNow = response.strMessageDateAppNotLowerNow;
                        AdditionalPointsModel.strMessageGenericBackOffice = response.strMessageGenericBackOffice;
                        AdditionalPointsModel.strMessageGenericBackOfficeBucked = response.strMessageGenericBackOfficeBucked;
                        AdditionalPointsModel.strMessageCustomerContractEmpty = response.strMessageCustomerContractEmpty;
                        AdditionalPointsModel.strMessageNotServiceCableInternet = response.strMessageNotServiceCableInternet;
                        AdditionalPointsModel.strHourServer = response.strHourServer;
                        AdditionalPointsModel.strMessageForcesSSTTETA = response.strMessageForcesSSTTETA;
                        AdditionalPointsModel.strMessageNotTimeZoneHourETA = response.strMessageNotTimeZoneHourETA;
                        AdditionalPointsModel.strMessageValidationETA = response.strMessageValidationETA;
                        that.strAdditionalPointLTECosto = response.strAdditionalPointLTECost;
                        controls.txt_monto.val(response.strAdditionalPointLTECost);
                        AdditionalPointsModel.strMensajeErrorConsultaIGV = response.strMensajeErrorConsultaIGV;
                        Session.ServiceType = response.strServicesType;
                    }
                    else {
                        alert("Hubo un problema al cargar las variables.", "Alerta");
                        that.btnClose_Click();
                    }
                }
            });
        },
        f_GetPlugin: function () {

            var that = this,
              controls = that.getControls();

            tb_AdditionalPoints = controls.tb_ProductDetailts.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollX: true,
                scrollY: 200,
                scrollCollapse: true,
                destroy: true,
                autoWidth: true,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos.",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });

            //that.f_GetProductDetailt();
        },
        //Event
        btnSave_Click: function () {
            var that = this,
             controls = that.getControls(),
             oCustomer = Session.DATACUSTOMER,
             oUserAccess = Session.USERACCESS,
             oDataService = Session.DATASERVICE;

            if (that.f_ValidateMail()) {
                if ($.isNumeric(controls.txt_phone_references.val()) || controls.txt_phone_references.val() == "") {                    
                    if (controls.cboCacDac.val() == "-1" || controls.cboCacDac.val() == "" || controls.cboCacDac.val() == null) {
                        alert(AdditionalPointsModel.strMessageValidatePointCare, "Alerta");
                        return false;
                    }

                    if (Session.ValidateETA == '2') {
                        if (controls.cboSchedule.val() == "-1" || controls.cboSchedule.val() == "" || controls.txt_dDateProgramming.val() == "") {
                            $.each(Session.vMessageValidationList, function (index, value) {
                                if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                                    alert(value.CODIGOC, "Alerta");
                                    return false;
                                }
                            });
                        }
                    }

                    if (controls.cboJobTypes.val() == "480.|" || controls.cboJobTypes.val() == AdditionalPointsModel.strJobTypeComplementarySalesLTE) {
                        if (controls.cboServicesType.val() == "" || controls.cboServicesType.val() == "-1") {
                            alert("Necesita seleccionar un tipo de servicio.", "Alerta");
                            return false;
                        }
                        if (controls.cboJobTypes.val() == AdditionalPointsModel.strJobTypeComplementarySalesLTE && controls.cboAttachedQuantity.val() == "-1") {
                            alert("Necesita seleccionar la cantidad de anexos.", "Alerta");
                            return false;
                        }
                    }


                    confirm(AdditionalPointsModel.strMessageConfirmAdditionsPoints, 'Confirmar', function (result) {
                        if (result == true) {
                            var model = {};
                            that.f_Loading();
                          
                            model.strTransaction = "TRANSACCION_PUNTO_ADICIONAL_LTE";
                            model.strTelephone = oCustomer.PhoneContact;
                            model.strNote = controls.txtNote.val();
                            model.strEmail = controls.txt_SendMail.val();
                            model.strCodeTipification = "TRANSACCION_PUNTO_ADICIONAL_LTE";
                            model.strModality = 'Presencial';//model.strModality = oCustomer.Modality;                            
                            model.strCacDac = controls.cboCacDac.val();
                            model.strServerName = AdditionalPointsModel.strServerName;
                            model.strLocalAddress = AdditionalPointsModel.strLocalAddress;
                            model.strCustomerId = oCustomer.CustomerID;
                            model.strContractId = oCustomer.ContractID;
                            model.IdSession = Session.IDSESSION;
                            model.strCodePlanInst = oCustomer.CodeCenterPopulate;
                            model.strPlan = oDataService.Plan;
                            model.strlogin = oUserAccess.login;
                            model.strFullName = oCustomer.FullName;
                            model.strBusinessName = oCustomer.BusinessName;
                            model.strDocumentNumber = oCustomer.DocumentNumber;
                            model.strDocumentType = oCustomer.DocumentType;
                            model.strLegalRepresent = oCustomer.LegalAgent;
                            model.strDescCacDac = $('#ddlCACDAC option:selected').text();
                            model.strAttachedQuantity = controls.cboAttachedQuantity.val();
                            model.strServicesType = controls.cboServicesType.val();
                            model.strMotiveSot = controls.cboMotivoSot.val();
                            model.strJobTypes = controls.cboJobTypes.val();

//Puntos Adicionales LTE INI
                            if (AdditionalPointsModel.strValidateETA != '0')
                                {
                            model.strSchedule = $("#cboSchedule option:selected").attr("idHorario");
                                }
                            else
                                {
                                    model.strSchedule = $("#cboSchedule option:selected").val();
                                }
//Puntos Adicionales LTE FIN

                            model.strScheduleValue = controls.cboSchedule.val();
                            model.strScheduleGet = controls.cboSchedule.html();
                            model.strValidateETA = AdditionalPointsModel.strValidateETA;
                            model.strDateProgramming = controls.txt_dDateProgramming.val();
                            model.strSubTypeWork = controls.cboSubJobTypes.val();                            
                            model.strRequestActId = Session.RequestActId;
                            model.strLastName = oCustomer.LastName;
                            model.strFirstName = oCustomer.Name;
                            model.strLegalDepartament = oCustomer.LegalDepartament;
                            model.strLegalProvince = oCustomer.LegalProvince;
                            model.strLegalDistrict = oCustomer.LegalDistrict;
                            model.strLegalBuilding = oCustomer.LegalUrbanization;
                            model.strUbigeoInst = (oCustomer.InstallUbigeo == null) ? "" : oCustomer.InstallUbigeo;
                            model.strAddressInst = controls.lblAddress.text();
                            model.strDescJobType = $('#cboJobTypes option:selected').text();
                            model.strDescMotive = $('#cboMotivoSot option:selected').text();
                            model.strDescServicesType = $('#cboServicesType option:selected').text();
                            model.strAddress = oCustomer.Address;
                            model.strDistrict = oCustomer.InvoiceDistrict;
                            model.strDepartament = oCustomer.InvoiceDepartament;
                            model.strProvince = oCustomer.InvoiceProvince;
                            model.strCountry = oCustomer.InvoiceCountry;
                            model.strPostalCode = (oCustomer.PlaneCodeInstallation == null || oCustomer.PlaneCodeInstallation === "null") ? "" : oCustomer.PlaneCodeInstallation;//controls.lblCodePlans.text();
                            model.strIGV = AdditionalPointsModel.strIGV;
                            model.strtypeCliente = oCustomer.CustomerType == 'Consumer' ? 'MASIVO' : 'CORPORATIVO';

                            if (document.getElementById('chk_SendMail').checked == true) {
                                model.bSendMail = true;
                            }
                            else {
                                model.bSendMail = false;
                            }

                            if (document.getElementById(controls.chk_Fidelidad.attr("id")).checked == true) {
                                model.iFidelidad = 1;
                                model.strAmount = "0";
                            }
                            else {
                                model.iFidelidad = 0;
                                model.strAmount = controls.txt_monto.val();
                            }
                            
                            model.strReference = oCustomer.Reference;
                            model.strCicloFact = oCustomer.BillingCycle;

                            model.strValidateETA = AdditionalPointsModel.strValidateETA;
                          
                            $.app.ajax({
                                type: 'POST',
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                data: JSON.stringify(model),
                                url: URL_AdditionalPointsSave,
                                error: function (request, status, error) {
                                    alert(error, "Alerta");
                                },
                                success: function (response) {

                                    if (response != null) {
                                        if (!response.bErrorTransac) {

                                            if (response.strCaseID != "") {
                                                controls.btnGuardar.prop("disabled", true);
                                            } else {
                                            controls.btnGuardar.prop("disabled", false);
                                            }

                                            controls.btnConstancia.prop("disabled", false);
                                            controls.cboJobTypes.prop("disabled", true);
                                            controls.cboMotivoSot.prop("disabled", true);                                            

                                            AdditionalPointsModel.strCaseID = response.strCaseID;

                                            if (response.bGeneratedPDF) {
                                                AdditionalPointsModel.bGeneratedPDF = true;
                                                AdditionalPointsModel.strFullPathPDF = response.strFullPathPDF;
                                            }

                                            if (!response.bErrorGenericCodSot) {
                                                AdditionalPointsModel.strCodSOT = response.strCodSOT;
                                                controls.lblLTENroSot.text(response.strCodSOT);
                                                alert(AdditionalPointsModel.strMessageOK, "Informativo");
                                            }
                                            else {
                                                alert("Hubo un Problema al Generar el Codigo SOT.", "Alerta");//controls.btnGuardar.prop("disabled", false);
                                            }
                                        }
                                        else {
                                            controls.btnGuardar.prop("disabled", false);
                                            controls.btnConstancia.prop("disabled", true);
                                            //controls.btnCerrar.prop("disabled", true);
                                            //controls.txtNote.prop("disabled", true);
                                            //controls.chk_SendMail.prop("disabled", true);
                                            //controls.txt_SendMail.prop("disabled", true);
                                            //controls.txt_phone_references.prop("disabled", true);
                                            alert(response.strMessageErrorTransac, "Alerta");
                                        }
                                    }
                                    else {
                                        controls.btnGuardar.prop("disabled", false);
                                        controls.btnConstancia.prop("disabled", true);
                                        //controls.btnCerrar.prop("disabled", true);
                                        //controls.txtNote.prop("disabled", true);
                                        //controls.chk_SendMail.prop("disabled", true);
                                        //controls.txt_SendMail.prop("disabled", true);
                                        //controls.txt_phone_references.prop("disabled", true);
                                        alert(response.strMessageErrorTransac, "Alerta");
                                    }
                                }
                            });
                        }
                    });
                }
                else {
                    alert(AdditionalPointsModel.strMessageValidatePhone, "Alerta");
                }
            }
        },
        AdditionPointPrint_Click: function () {
            var that = this,
                 controls = that.getControls(),
                 oCustomer = Session.DATACUSTOMER,
                 oUserAccess = Session.USERACCESS,
                 oDataService = Session.DATASERVICE;


            if (AdditionalPointsModel.bGeneratedPDF) {
                that.f_Loading();
                ReadRecordSharedFile(Session.IDSESSION, AdditionalPointsModel.strFullPathPDF);
                $.unblockUI();
            }
            else {
                alert('Error para generar la constancia de atención.', "Alerta");
            }
        },
        btnClose_Click: function () {
            parent.window.close();
        },
        cboJobTypes_Change: function () {
            var that = this,
                   controls = that.getControls(),
                   oCustomer = Session.DATACUSTOMER,
                   oDataLine = Session.DATLINE,
                   model = {};

            that.f_Loading();

            controls.cboSchedule.html("");
            controls.cboSchedule.append($('<option>', { value: '-1', html: 'Seleccionar' }));

            model.IdSession = Session.IDSESSION;

            if (controls.cboJobTypes.val() == "480.|" || controls.cboJobTypes.val() == AdditionalPointsModel.strJobTypeComplementarySalesLTE) {
                $("#tr_ServicesType").show();

                model.strJobTypes = AdditionalPointsModel.strJobTypeComplementarySalesLTE; //controls.cboJobTypes.val();
                model.strInternetValue = AdditionalPointsModel.strInternetValue;
                model.strCellPhoneValue = AdditionalPointsModel.strCellPhoneValue;
                that.f_LoadCombo(URL_GetDocumentType, model, controls.cboServicesType, true);

                if (controls.cboJobTypes.val() == AdditionalPointsModel.strJobTypeComplementarySalesLTE) {
                    that.f_GetAttachedQuantity();
                    $("#tdTextoCantidadAnexos").show();
                    $("#tdddlCantidadAnexos").show();
                    controls.cboMotivoSot.val(AdditionalPointsModel.strCustomerRequestId);
                    controls.cboMotivoSot.prop("disabled", true);
                }
                else {
                    $("#tdTextoCantidadAnexos").hide();
                    $("#tdddlCantidadAnexos").hide();
                    controls.cboMotivoSot.val("-1");
                    controls.cboMotivoSot.prop("disabled", false);
                }
            }
            else {

                $("#tdTextoCantidadAnexos").hide();
                $("#tdddlCantidadAnexos").hide();
                $("#tr_ServicesType").hide();
                controls.cboServicesType.val("-1");
                controls.cboMotivoSot.val("-1");
                controls.cboMotivoSot.prop("disabled", false);
                var fechaServidor = new Date(Session.ServerDate);
                var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
                controls.txt_dDateProgramming.val([that.f_pad(fechaServidorMas7Dias.getDate()), that.f_pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));
            }

            if (controls.cboJobTypes.val().indexOf(".|") == -1) {
                controls.btnValidateSchedule.prop("disabled", true);
                Session.VALIDATE = "1";

            } else {
                controls.btnValidateSchedule.prop("disabled", false);
                Session.VALIDATE = "0";
            }


            if (controls.cboJobTypes.val() != "-1") {
                that.f_ValidacionETA();
            } else {
                controls.cboSubJobTypes.html("");
                controls.cboSubJobTypes.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                controls.cboSchedule.html("");
                controls.cboSchedule.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            }
        },
        cboSubJobTypes_Change: function () {
            var that = this,
                controls = that.getControls(),
                strUrl = '';
            if (controls.cboSubJobTypes.val() == "-1") {
                return false;
            }
            if (Session.ValidateETA == '1') {
                if (controls.cboSubJobTypes.val() != "-1") {
                    if (controls.txt_dDateProgramming.val() != "") {
                        //InitFranjasHorario();
                    }
                }
            }
        },
        //Method

        //Functions
        f_GetJobType: function () {
            var that = this,
                controls = that.getControls(),
                model = {};

            model.strIdSession = Session.IDSESSION;
            that.f_LoadCombo(URL_GetJobType, model, controls.cboJobTypes, false, null, false);
            if (!(AdditionalPointsModel.strJobTypeDefault == ''))
                $("#cboJobTypes").val(AdditionalPointsModel.strJobTypeDefault);
        },
        f_GetJobTypeSub: function () {
            var that = this,
               controls = that.getControls(),
               model = {};
            model.IdSession = Session.IDSESSION;
            model.strJobTypes = controls.cboJobTypes.val();
            model.strContractId = Session.DATACUSTOMER.ContractID;
            that.f_LoadCombo(URL_GetOrderType, model, controls.cboSubJobTypes, false, 3);
        },
        f_GetMotiveSot: function (IdTipoTrabajo) {
            var that = this,
                controls = that.getControls(),
                model = {};

            model.strIdSession = Session.IDSESSION;
            model.strIdTipoTrabajo = IdTipoTrabajo;

            that.f_LoadCombo(URL_GetMotiveSot, model, controls.cboMotivoSot, true, 1, false);
        },

        f_GetAttachedQuantity: function () {
            var that = this,
             controls = that.getControls(),
             model = {};
            model.strIdSession = Session.IDSESSION;
            that.f_LoadCombo(URL_GetAttachedQuantity, model, controls.cboAttachedQuantity);
        },
        f_GetCacDat: function () {
            var that = this,
            controls = that.getControls(),
            model = {};
            model.strIdSession = Session.IDSESSION;
            that.f_LoadCombo(URL_GetCacDat, model, controls.cboCacDac, true, 2);
        },

        getCACDAC: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;
            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',

                success: function (results) {
                    var cacdac = results.Cac;
                    //console.log"cacdac: " + cacdac);
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }

            });
        },

        f_GetProductDetailt: function () {
            var that = this,
             controls = that.getControls(),
             oCustomer = Session.DATACUSTOMER,
             model = {};

            model.IdSession = Session.IDSESSION;
            model.strCustomerId = oCustomer.CustomerID;
            model.strContractId = oCustomer.ContractID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: URL_GetProductDetailt,
                error: function (data) {
                    alert("Error JS: en llamar al GetProductDetailt.", "Alerta");
                },
                success: function (response) {
                    if (response.data != null) {
                        if (response.data.length > 0) {
                            $.each(response.data, function (i, r) {
                                tb_AdditionalPoints.row.add([
                                    r.codigo_material
                                  , r.codigo_sap
                                  , r.numero_serie
                                  , r.macadress
                                  , r.descripcion_material
                                  , r.tipo_equipo
                                  , r.id_producto
                                  , r.modelo
                                  , r.convertertype
                                  , r.servicio_principal
                                  , r.headend
                                  , r.ephomeexchange
                                  , r.numero
                                ]).draw(false);
                            });
                        }
                    }
                }
            });

        },
        f_ValidacionETA: function () {
            var that = this,
               controls = that.getControls(),
               oCustomer = Session.DATACUSTOMER,
               model = {};

            model.IdSession = Session.IDSESSION;
            model.strJobTypes = controls.cboJobTypes.val();
            model.strCodeUbigeo = oCustomer.CodeCenterPopulate;
            model.StrTypeService = Session.ServiceType;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: URL_GetValidateETA,
                success: function (response) {
                    var oItem = response.data;
                    if (oItem.Codigo == '2' || oItem.Codigo == '1' || oItem.Codigo == '0') {
                        //  Session.VALIDAETA = 
                        AdditionalPointsModel.strValidateETA = oItem.Codigo;
                        AdditionalPointsModel.strHistoryETA = oItem.Codigo2;

                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;

                        if (oItem.Codigo == '2') {
                            that.f_GetJobTypeSub();
                            //$("#tr_SubWorkType").show();
                            $("#tr_SubWorkType").attr("disabled", false);
                            controls.cboSubJobTypes.prop('disabled', false);
                            that.f_EnableAgendamiento(true);
                        }
                        else if (oItem.Codigo == '1') {
                            that.f_GetJobTypeSub();
                            //$("#tr_SubWorkType").show();
                            $("#tr_SubWorkType").attr("disabled", false);
                            controls.cboSubJobTypes.prop('disabled', false);
                            that.f_EnableAgendamiento(true);
                        }
                        else {
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Informativo");
                            // $("#tr_SubWorkType").hide();
                            $("#tr_SubWorkType").attr("disabled", true);
                            //InitFranjasHorario();
                            AdditionalPointsModel.strValidateETA = "0";

                            Session.ValidateETA = "0";
                            Session.History = "";

                            controls.cboSubJobTypes.prop('disabled', true);
                            that.f_EnableAgendamiento(false);
                        }
                    } else {
                        if (oItem.Descripcion == null) {
                            oItem.Descripcion = " ";
                        }
                        alert(AdditionalPointsModel.strMessageValidationETA, "Alerta");
                        $("#tr_SubWorkType").attr("disabled", true);
                        //InitFranjasHorario();

                        controls.cboSubJobTypes.prop('disabled', true);
                        that.f_EnableAgendamiento(false);

                        AdditionalPointsModel.strValidateETA = "0";
                        AdditionalPointsModel.strHistoryETA = oItem.Codigo2;

                        Session.ValidateETA = "0";
                        Session.History = oItem.Codigo2;
                    }


                }
            });

        },
         
        f_LoadCombo: function (URL, BE, Combo, bSelect, Event, basync) {
            var that = this,
              controls = that.getControls();

            if (bSelect == null) { bSelect = true; }
            if (basync == null) { basync = true }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(BE),
                url: URL,
                async: basync,
                error: function (error) {
                    alert('Error en: ' + URL, "Alerta");

                },
                success: function (response) {
                    if (bSelect == true) {
                        Combo.html("");
                        Combo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    }
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            Combo.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                    if (Event != null) {
                        switch (Event) {
                            case 1:
                                that.cboJobTypes_Change();
                                break;
                            case 2:
                                if (response.data != null) {
                                    $.each(response.data.CacDacTypes, function (index, value) {
                                        Combo.append($('<option>', { value: value.Code, html: value.Description }));
                                    });
                                }
                                break;
                            case 3:
                                if (response.data != null) {
                                    Combo.empty();
                                    Combo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                                    $.each(response.data, function (index, value) {
                                        var codTipoSubTrabajo = value.Code.split("|");
                                        if (response.typeValidate.COD_SP == "0" && codTipoSubTrabajo[0] == response.typeValidate.COD_SUBTIPO_ORDEN) {
                                            Combo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2, selected: true }));
                                            Combo.attr('disabled', true);
                                        }
                                        else {
                                            Combo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2 }));
                                        }
                                    });
                                }
                                break;
                        }
                    }

                }
            });
        },
        f_Loading: function () {
            var that = this,
             controls = that.getControls();
            $.blockUI({
                message: controls.ModalLoading,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000000',
                    '-webkit-border-radius': '50px',
                    '-moz-border-radius': '50px',
                    opacity: .7,
                    color: '#fff'
                }
            });
        },
        f_EnableAgendamiento: function (bool) {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,

                strUrl = '';
            if (bool == true) {
                controls.txt_dDateProgramming.prop("disabled", false);
                controls.cboSchedule.prop("disabled", false);
                controls.btnValidateSchedule.prop("disabled", true);

            } else {

                var fechaServidor = new Date(Session.ServerDate);
                controls.txt_dDateProgramming.val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                if (controls.cboJobTypes.val().indexOf(".|") == -1) {
                    controls.btnValidateSchedule.prop("disabled", true);
                    Session.VALIDATE = "1";
                }
                else {
                    controls.btnValidateSchedule.prop("disabled", false);
                    Session.VALIDATE = "0";
                }
            }
        },
        f_ValidateMail: function () {
            var that = this,
             controls = that.getControls();

            if ($.trim(controls.txt_SendMail.val()) == '') {
                if (document.getElementById('chk_SendMail').checked == true) {

                    alert(AdditionalPointsModel.strMessageEnterMail, "Alerta");
                    return false;
                } else {
                    return true;
                }
            } else {
                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test(document.getElementById('txt_SendMail').value);
                if (blvalidar == false) {

                    alert(AdditionalPointsModel.strMessageValiateMail, "Alerta");
                    document.getElementById('txt_SendMail').focus();
                    return false;
                } else { return true; }
            }

        },
        f_ActiveEmail: function () {
            var that = this,
            controls = that.getControls();
            var objEnviarEmail = document.getElementById(controls.chk_SendMail.attr("id"));
            if (objEnviarEmail.checked == true) {
                document.getElementById(controls.txt_SendMail.attr("id")).style.display = '';

            } else {
                document.getElementById(controls.txt_SendMail.attr("id")).style.display = 'none';
            }

        },
        f_ActiveFidelidad: function () {
            var that = this,
            controls = that.getControls();
            var chkFidelidad = document.getElementById(controls.chk_Fidelidad.attr("id"));
            if (chkFidelidad.checked == true) {
                controls.txt_monto.val("0.00");
                controls.txt_monto.prop("disabled", true);
            } else {
                controls.txt_monto.val(that.strAdditionalPointHFCCosto);
                controls.txt_monto.prop("disabled", false);


            }
        },
        f_pad: function (s) { return (s < 10) ? '0' + s : s; },
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },

        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        },

        getIGV: function () {
            var that = this,
                controls = that.getControls(),
                objIGV = {};

            objIGV.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIGV),
                url: URL_GetConsultIGV,
                success: function (result) {
                    if (result.data != null) {
                        var igv = parseFloat(result.data.igvD);
                        var igvEnt = igv + 1;
                        AdditionalPointsModel.strIGV = igvEnt;
                    }
                    else {
                        controls.btnGuardar.prop("disabled", true);
                        alert(AdditionalPointsModel.strMensajeErrorConsultaIGV, "Alerta");
                    }
                },
                error: function (error) {
                    controls.btnGuardar.prop("disabled", true);
                    alert(AdditionalPointsModel.strMensajeErrorConsultaIGV, "Alerta");

                }
            });
        },
        f_pad: function (s) { return (s < 10) ? '0' + s : s; },
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },
        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        },
        strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
            (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/')),
         strAdditionalPointLTECosto: ''
    };
    
    $.fn.LTEAdditionalPoints = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('LTEAdditionalPoints'),
                options = $.extend({}, $.fn.LTEAdditionalPoints.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LTEAdditionalPoints', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {

                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.LTEAdditionalPoints.defaults = {
    }

    $('#divBody').LTEAdditionalPoints();

})(jQuery);
