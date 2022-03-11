
(function ($, undefined) {
    var Smmry = new Summary('transfer');
    var hdnMontoFideFinal;
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    var SessionTransf = function () { };
    SessionTransf.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == '' || SessionTransac.UrlParams.IDSESSION == undefined ? "123456789874563211" : SessionTransac.UrlParams.IDSESSION;
    SessionTransf.PhonfNro = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
    SessionTransf.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
    SessionTransf.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
    SessionTransf.NameCustomer = SessionTransac.SessionParams.DATACUSTOMER.FullName;
    SessionTransf.TypDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
    SessionTransf.NumbDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DNIRUC;
    SessionTransf.cuentaCustomer = SessionTransac.SessionParams.DATACUSTOMER.Account;
    SessionTransf.RepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
    SessionTransf.NotAddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.CountryCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
    SessionTransf.CountryCustomerFac = SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry;
    SessionTransf.PlanoIDCustomer = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation;
    SessionTransf.urbLegalCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.DirecDespachoCustomer = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
    SessionTransf.DepCustomer = SessionTransac.SessionParams.DATACUSTOMER.Departament;
    SessionTransf.ProvCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalProvince;
    SessionTransf.DistCustomer = SessionTransac.SessionParams.DATACUSTOMER.District;
    SessionTransf.IdEdifCustomer = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeBilling;
    SessionTransf.EmailCustomer = SessionTransac.SessionParams.DATACUSTOMER.Email;
    SessionTransf.AccesPage = SessionTransac.SessionParams.USERACCESS.optionPermissions;
    SessionTransf.gConstOpcTIEHabFidelizar = null;
    SessionTransf.DatosLineatelefonia = SessionTransac.SessionParams.DATASERVICE.TelephonyValue;
    SessionTransf.DatosLineainternet = SessionTransac.SessionParams.DATASERVICE.InternetValue;
    SessionTransf.DatosLineacableTv = SessionTransac.SessionParams.DATASERVICE.CableValue;
    SessionTransf.DataCycleBilling = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
    SessionTransf.CodeUbigeo = SessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo;
    SessionTransf.hdnPermisos = null;
    SessionTransf.hdnTienePerfilJefe = "1";
    SessionTransf.hdnHayFidelizaFinal = "0";
    SessionTransf.hdnMensaje7 = null;
    SessionTransf.hdnMensaje8 = null;
    SessionTransf.strValidateETA = null;
    SessionTransf.ConsultationCoverageTitle = null
    SessionTransf.ConsultationCoverageURL = null;
    SessionTransf.hdnChangeEquip = '4';
    SessionTransf.hdnTagSelection = '4';
    SessionTransf.strMsgTranGrabSatis = null;
    SessionTransf.hdnMessageSendMail = null;
    SessionTransf.hdnddlSOT = null;
    SessionTransf.CriterioMensajeOK = null;
    SessionTransf.hdnMensajeCapacidadFull = null;
    SessionTransf.hdnFecAgCU = null;
    SessionTransf.hdnCUbiCU = null;
    SessionTransf.hdnMontoFideFinal = null;
    SessionTransf.strPath = null;
    SessionTransf.hdnProDes = "";
    SessionTransf.hdnCenPobDes = "";
    SessionTransf.hdnCodEdi = null;
    SessionTransf.hdnUbiID = null;
    SessionTransf.hdnCodPla = null;
    SessionTransf.hdnIDPlano = null;
    SessionTransf.gConstIndTE1P = null;
    SessionTransf.gConstIndTE2P = null;
    SessionTransf.gConstIndTE3P = null;
    SessionTransf.gConstIndTI = null;
    SessionTransf.hdnMessageErrorIgv = null;
    SessionTransf.strMessageValidationETA = null;
    SessionTransf.hdnFranjaHorariaCU = null;
    SessionTransf.hdnValidado = '0';
    SessionTransf.agendaGetFecha = null;
    SessionTransf.agendaGetCodigoFranja = null;
    SessionTransf.flagvalidation = null;
    SessionTransf.flagSaveTransactions = null;
    SessionTransf.hdnValidaEta = "";
    SessionTransf.hdnHistorialEta = "";
    SessionTransf.hdnTipoTrabCU = null;
    SessionTransf.hdnSubTipOrdCU = "";
    SessionTransf.strTipificacion = "";

    SessionTransf.strMensajeTransaccionFTTH = ""; //RONALDRR
    SessionTransf.strPlanoFTTH = ""; //RONALDRR
    Session.hdntelephone = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
    Session.userId = SessionTransac.SessionParams.USERACCESS.userId;
    

    var Form = function ($element, options) {

        $.extend(this, $.fn.HFCChangeEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            form: $element
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblContact: $('#lblContact', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , CodeBuilding: $('#CodeBuilding', $element)
            , lblBillingAmount: $('#lblBillingAmount', $element)
            , lblCycleBilling: $('#lblCycleBilling', $element)
            , lblAddress: $('#lblAddress', $element)
            , lblAddressNote: $('#lblAddressNote', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblDepartament: $('#lblDepartament', $element)
            , lblRepLegal: $('#lblRepLegal', $element)
            , lblCodeUbigeo: $('#lblCodeUbigeo', $element)
            , lblHUB: $('#lblHUB', $element)
            , lblBelt: $('#lblBelt', $element)
            , lblCMTS: $('#lblCMTS', $element)
            , lbltypeCustomer: $('#lbltypeCustomer', $element)
            , lblCodeFlat: $('#lblCodeFlat', $element)
            , lblStatus: $('#lblStatus', $element)
            , lblIdContract: $('#lblIdContract', $element)
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDateAct: $('#lblDateAct', $element)
            , lblPlanActual: $('#lblPlanActual', $element)
            , lblDateVcto: $('#lblDateVcto', $element)
            , lblCuenta: $('#lblCuenta', $element)
            , lblProvince: $('#lblProvince', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblCodePlane: $('#lblCodePlane', $element)
            , ddlTypeWork: $('#ddlTypeWork', $element)
            , ddlTypeSubWork: $('#ddlTypeSubWork', $element)
            , ddlReasonSot: $('#ddlReasonSot', $element)
            //, chkLoyalty: $('#chkLoyalty', $element)
            //, txChargeAmount: $('#txChargeAmount', $element)
            , ddlSchedule: $('#ddlSchedule', $element)
            , dContenedorEmail: $('#dContenedorEmail', $element)
            , chkSendMail: $('#chkSendMail', $element)
            , chkUseChangeBilling: $('#chkUseChangeBilling', $element)
            , txtSendMail: $('#txtSendMail', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , txtNote: $('#txtNote', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , btnSave: $('#btnSave', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , divRules: $('#BussinessRule', $element)
            , cboFranjaHoraria: $('#cboFranjaHoraria', $element)
            , btnValidateSchedule: $('#btnValidateSchedule', $element)
            , lnkNumSot: $('#lnkNumSot', $element)
            , txtFProgramacion: $("#txtFProgramacion", $element)
            , closeErrorAlert: $("#closeErrorAlert", $element)
            , lblTitle: $('#lblTitle', $element)
            , tblExtransf: $('#tblExtransf', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.chkSendMail.addEvent(that, 'change', that.chkSendMail_Change);
            controls.ddlTypeWork.change(function () { that.f_SetTipoTrabajo(); });
            controls.ddlTypeSubWork.change(function () { that.f_SetSubTipoTrabajo(); });
            //controls.ddlCACDAC.change(function () { that.f_SeleccionarCACDAC(); });
            //controls.chkLoyalty.addEvent(that, 'change', that.f_ValidaterPermLoyalty);
            controls.ddlTypeWork.empty();
            that.maximizarWindow();
            that.windowAutoSize();

            document.addEventListener('keyup', that.shortCutStep, false);
            controls.closeErrorAlert.click(function () { return that.f_closeErrorAlert(); });

            that.render();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
            var controls = that.getControls();
            controls.lblTitle.text("CAMBIO DE EQUIPO POR TECNOLOGÍA");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });

            that.f_ValidateStatusLinea();
            that.getCustomerData();
            that.InitGetMessageConfiguration();

            controls.txtSendMail.val(SessionTransf.EmailCustomer);
            //controls.txChargeAmount.val('0.00');
            controls.btnConstancy.attr('disabled', true);
            //controls.chkLoyalty.attr('disabled', true);
            //controls.chkLoyalty.prop("checked", true);
            //$("#txChargeAmount").val("0.00");

            $("#IDdivtabProducts").show();
            $("#IDdivtabProductsCE").after($("#IDdivtabProducts"));

            $("#IDdivtabTypeWork").show();
            $("#IDdivtabTypeWorkCE").after($("#IDdivtabTypeWork"));
            $("#divErrorAlert").hide();
            $("#divErrorCE").after($("#divErrorAlert"));
            $("#btnConstancy").prop("disabled", true);
            
           
            
        },
        InitGetMessageConfiguration: function () {
            var that = this, controls = this.getControls();
            var objMessageConfiguration = {};
            objMessageConfiguration.strIdSession = Session.IDSESSION;

            var urlBase = '/Transactions/HFC/ChangeEquipment';
            $.app.ajax({
                async: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objMessageConfiguration),
                url: urlBase + '/InitGetMessageConfiguration',
                success: function (response) {
                    SessionTransf.hdnMensaje7 = response[13];
                    SessionTransf.hdnChangeEquip= response[0];
                    SessionTransf.strMsgTranGrabSatis = response[1];
                    SessionTransf.hdnMessageSendMail = response[2];
                    SessionTransf.CriterioMensajeOK = response[3];
                    SessionTransf.hdnMensajeCapacidadFull = response[4];

                    SessionTransf.hdnIDTrabTI = response[5];
                    SessionTransf.strMensajeEmail = response[6];
                    SessionTransf.gConstOpcTIEHabFidelizar = response[7];
                    SessionTransf.hdnHayFidelizaFinal = response[8];
                    
                    Session.ServerDate = response[9];
                    SessionTransf.hdnMessageErrorIgv = response[10];
                    SessionTransf.strMessageValidationETA = response[11];
                    SessionTransf.strTipificacion = response[12];

                    SessionTransf.strMensajeTransaccionFTTH = response[14];//RONALDRR 
                    SessionTransf.strPlanoFTTH = response[15];//RONALDRR
                    Session.CodUbigeo = SessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo;
                    Session.tipoAutorizacion = 0;

                    controls.txtFProgramacion.val(Session.ServerDate);

                    that.f_ValidateStatusProductFTTH(); //VALIDAR SI APLICA FTTH. RONALDRR

                    that.getWorkType(SessionTransf.hdnChangeEquip);
                    that.getReasonSot();
                    that.InitCacDac();
                    that.getTypification(SessionTransf.strTipificacion);
                    that.f_getListDetailsProducto();
                    
                   // that.getIGV();
                   // that.getLoadMont();

                }
            });


        },
      //RONALDRR - INICIO - CAMBIO DE EQUIPO POR TECNOLOGIA
        f_ValidateStatusProductFTTH: function () {                  
            
            var strPlano = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation;
            var strPlanoFTTH = SessionTransf.strPlanoFTTH;
            if (strPlano.search(strPlanoFTTH) > 0 && SessionTransf.strMensajeTransaccionFTTH != '') {
                alert(SessionTransf.strMensajeTransaccionFTTH.replace('{0}', "CAMBIO DE EQUIPO POR TECNOLOGIA"), "Alerta", function () {
                    parent.window.close();
                });
                return false;
            }
            //RONALDRR - FIN

        },
        f_ValidateStatusLinea: function () {

            var pageIsPostBack = true;

            if (SessionTransac.SessionParams.DATASERVICE.StateLine == 'Desactivo') {

                alert("Contrato está inactivo, no puede completar la operación.", "Alerta", function () {//strMsgValidacionContratoInactivo
                    parent.window.close();
                });

                pageIsPostBack = false;
                return false;
            }
            else {
                if (SessionTransac.SessionParams.DATASERVICE.StateLine == 'Reservado') {
                    alert("Contrato estár reservado, no puede completar la operación.", "Alerta", function () {//strMsgValidacionContratoReservado
                        parent.window.close();
                    });

                    pageIsPostBack = false;
                    return false;
                }
                else {

                    if (SessionTransac.SessionParams.DATASERVICE.StateLine == 'Suspendido' || (SessionTransf.DatosLineatelefonia == 'F' && SessionTransf.DatosLineainternet == 'F' && SessionTransf.DatosLineacableTv == 'F')) {
                        alert("No puede completar la operación de cambio de equipo.", "Alerta", function () {//strMsgValidacionContratoReservado
                            parent.window.close();
                        });

                        pageIsPostBack = false;
                        return false;
                    }
                }

            }


        },
        chkSendMail_Change: function (sender, arg) {
            var that = this;
            var controls = that.getControls();
            if (sender.prop("checked")) {
                controls.txtSendMail.prop("style").display = "block";
            } else {
                controls.chkSendMail.html("");
                controls.txtSendMail.prop("style").display = "none";
            }

        },
        f_SetSubTipoTrabajo: function () {
            var that = this, controls = that.getControls();;
            SessionTransf.hdnSubTipOrdCU = $("#ddlTypeSubWork").val();
            if (controls.ddlTypeSubWork.val() == '') {
                //$("#ddlHorarioUsrCtr").html("");
                //$("#ddlHorarioUsrCtr").html("<option value='-1'>-- Seleccionar --</option>");
                return false;
            }
            if (SessionTransf.strValidateETA != '0') {
                InitFranjasHorario();
            }
        },
        f_SetTipoTrabajo: function () {

            var that = this, controls = that.getControls();
            if (controls.ddlTypeWork.val() != "-1" && controls.ddlTypeWork.val() != '') {


                controls.ddlTypeSubWork.empty();
                that.getWorkSubType(controls.ddlTypeWork.val());

            } else {
                controls.ddlTypeSubWork.empty();
            }

        },
        getTypification: function (strTransactionNameType) {
            var that = this,
                controls = that.getControls(),
                objTypi = {};
            
            objTypi.strIdSession = SessionTransf.IDSESSION;
            objTypi.strTransactionName = strTransactionNameType;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetTypification',
                data: JSON.stringify(objTypi),
                success: function (result) {
                    var list = result.ListTypification;
                    if (list != null) {
                        if (list.length > 0) {
                            that.strClaseCode = list[0].CLASE_CODE;
                            that.strSubClaseCode = list[0].SUBCLASE_CODE;
                            that.strTipo = list[0].TIPO;
                            that.strClase = list[0].CLASE;
                            that.strSubClase = list[0].SUBCLASE;

                            that.getBusinessRules();
                        } else {
                            var msg = 'No se reconoce la tipificación de esta transacción.';
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                                controls.btnSave.attr("disabled", true);
                                controls.btnConstancy.attr("disabled", true);

                        }
                    } else {
                        var msg = 'No se reconoce la tipificación de esta transacción.';
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                            controls.btnSave.attr("disabled", true);
                            controls.btnConstancy.attr("disabled", true);
                    }
                }
            });
        },
        getBusinessRules: function () {
            var that = this,
                controls = that.getControls(),
                objRules = {};
            controls.divRules.empty();
            objRules.strIdSession = Session.IDSESSION;
            objRules.strSubClase = that.strSubClaseCode;
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }

                }
            });
        },
        getCustomerData: function () {
            var that = this,
                controls = that.getControls();
            controls.lblCodeUbigeo.text(SessionTransf.CodeUbigeo);

            controls.lblCustomerName.text(SessionTransf.NameCustomer);
            controls.lblIdentificationDocument.text(SessionTransf.NumbDocRepreCustomer);
            controls.CodeBuilding.text(SessionTransf.IdEdifCustomer);
            controls.lblBillingAmount.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit);
            controls.lblStatus.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.AccountStatus);
            controls.lblIdContract.text(SessionTransac.SessionParams.DATACUSTOMER.ContractID);
            controls.lblCustomerID.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerID);

            controls.lblDateAct.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ActivationDate);
            controls.lblDateVcto.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ExpirationDate);
            controls.lblCuenta.text(SessionTransac.SessionParams.DATACUSTOMER.Account);

            controls.lblCycleBilling.text(SessionTransf.DataCycleBilling);
            controls.lblAddress.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
            controls.lblAddressNote.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCountry.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartament.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvince.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrict.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict);
            controls.lblRepLegal.text(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblPlanActual.text(SessionTransac.SessionParams.DATASERVICE.Plan);

            controls.lblCodePlane.text(SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation);


            //controls.lblDistrict.text(SessionTransf.DistCustomer);
            controls.lblCodeFlat.text(SessionTransf.PlanoIDCustomer);


            controls.lbltypeCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.lblContact.text(SessionTransac.SessionParams.DATACUSTOMER.BusinessName);

            controls.lblHUB.text('');
            controls.lblBelt.text('');
            controls.lblCMTS.text('');



        },
        f_getListDetailsProducto: function () {

            var self = this;
            var that = this,
                objDetProduct = {};
            objDetProduct.strIdSession = Session.IDSESSION;

            objDetProduct.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;//"6426786";//
            objDetProduct.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;//"5654684";//

            var strParam = '';
            var strUrl = '/Transactions/HFC/ChangeEquipment/GetListDataProducts';
            $.ajax({
                type: 'Post',
                url: strUrl,
                data: JSON.stringify(objDetProduct),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                success: function (response) {
                    if (response.data != null) {
                        var registros = response.data.ListDataProducts;
                        that.tblExtransf_Load(registros);
                    }
                }
            });
            
           
        },
        tblExtransf_Load: function (data) {
            var that = this,
                controls = that.getControls();

            controls.tblExtransf.DataTable({
                scrollCollapse: true
                , paging: false
                , scrollY: 180
                , scrollX: true
                , searching: false
                , destroy: true
                , data: data
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtrado de  _MAX_ total registros)"
                }
                , columns: [
                    { "data": "MaterialCode", "width": "100px" },
                    { "data": "SapCode", "width": "100px" },
                    { "data": "SerieNumber", "width": "100px" },
                    { "data": "AdressMac", "width": "100px" },
                    { "data": "MaterialDescripcion", "width": "200px" },
                    { "data": "EquipmentType", "width": "200px" },
                    { "data": "ProductId", "width": "200px" },
                    { "data": "Type", "width": "100px" },//Modelo
                    { "data": "ConvertType", "width": "100px" },
                    { "data": "ServiceType", "width": "100px" },
                    { "data": "Headend", "width": "50px" },
                    { "data": "PricipalService", "width": "100px" },//Perfil
                    { "data": "EphomeexChange", "width": "50px" },//Número                                 
                ],

            });
        },
        F_ValidateETA: function () {
            var that = this, model = {};
            model.IdSession = SessionTransf.IDSESSION;;
            model.strJobTypes = $('#ddlTypeWork').val();
            model.strCodePlanInst = $('#lblCodePlane').text();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/SchedulingToa/GetValidateETA',
                success: function (response) {
                    var oItem = response.data;
                    var fechaServidor = new Date(Session.ServerDate);
                    $('#txtFProgramacion').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
                    if (oItem.Codigo == '1' || oItem.Codigo == '0' || oItem.Codigo == '2') {
                        SessionTransf.strValidateETA = oItem.Codigo;
                        SessionTransf.strHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;
                        if (oItem.Codigo == '2') {
                            $("#tr_SubWorkType").show();
                            that.f_EnableAgendamiento(true);
                            $("#txtFProgramacion").trigger('change');
                        }
                        else if (oItem.Codigo == '1') {
                            $("#tr_SubWorkType").show();
                            that.f_EnableAgendamiento(true);
                            $("#txtFProgramacion").trigger('change');
                        }
                        else {
                            Session.ValidateETA = "0";
                            Session.History = "";
                            SessionTransf.strValidateETA = "0";
                            $("#tr_SubWorkType").prop('disabled', true);
                            that.f_EnableAgendamiento(false);
                            InitFranjasHorario();
                            $("#ddlTypeSubWork").prop('disabled', true);
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                        }
                    }
                    else {
                        if (oItem.Descripcion == null)
                            oItem.Descripcion = " ";

                        Session.ValidateETA = "0";
                        SessionTransf.strValidateETA = "0";
                        SessionTransf.strHistoryETA = oItem.Codigo2;
                        that.f_EnableAgendamiento(false);
                        Session.History = oItem.Codigo2;
                        InitFranjasHorario();
                        $("#ddlTypeSubWork").prop('disabled', true);
                        alert(SessionTransf.strMessageValidationETA, "Alerta");
                    }
                }
            });
        },
        f_pad: function (s) { return (s < 10) ? '0' + s : s; },
        
        //BUTTON------------------------------------
        btnConstancy_click: function () {
            var that = this;
            
            ReadRecordSharedFile(Session.IDSESSION, SessionTransf.strPath);
            
        },
        btnClose_click: function () {
            parent.window.close();
        },
        btnSave_click: function () {
            var that = this,
                controls = that.getControls();
            $('#ddlCACDAC').trigger("change");
          /*  if (!controls.chkLoyalty.prop('checked')) {
                if (that.strIGV == null || that.strIGV == '') {

                    
                    $("#btnSave").attr("disabled", true);
                   
                    alert(SessionTransf.hdnMessageErrorIgv, "Alerta");
                    controls.lblErrorMessage.html(SessionTransf.hdnMessageErrorIgv);
                    controls.divErrorAlert.show();
                    return false;
                }
            }*/
            
            if (SessionTransf.strValidateETA == '2') {
                if (controls.ddlTypeSubWork.val() == '') {
                    alert("Seleccione Sub tipo de trabajo.", "Alerta");
                    return false;
                }

                if ($('#cboFranjaHoraria').val() == '-1' || $('#txtFProgramacion').val() == '') {
                    $.each(Session.vMessageValidationList, function (index, value) {
                        if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                            alert(value.CODIGOC, "Alerta");
                        }
                    });
                    return false;
                }
            }
            
            if (controls.ddlCACDAC.val() == "-1" || controls.ddlCACDAC.val() == "") {
                alert("Seleccione punto de atención.", "Alerta");
                return false;
            }
            if (!this.f_ValidateEmail())
                return;

            SessionTransf.hdnHayFidelizaFinal = "0";
            //if ($("#chkLoyalty").prop("checked")) {
            //    SessionTransf.hdnHayFidelizaFinal = "1";
            //}
            //SessionTransf.hdnMontoFideFinal = controls.txChargeAmount.val();
            that.GetRecordTransaction();

        },
        
        GetRecordTransaction: function () {
            var that = this;
            var controls = that.getControls();
            var urlBase = '/Transactions/HFC/ChangeEquipment';
            var objHfcBETransfer = {};
            objHfcBETransfer.strIdSession = SessionTransf.IDSESSION;
            objHfcBETransfer.strtypetransaction = SessionTransf.hdnTagSelection;
            objHfcBETransfer.ConID = SessionTransf.CONTRATO_ID;
            objHfcBETransfer.CustomerID = SessionTransf.CUSTOMER_ID;
            objHfcBETransfer.InterCasoID = "";
            objHfcBETransfer.Telephone = SessionTransf.PhonfNro;
            objHfcBETransfer.Ubigeo = SessionTransf.hdnUbiID;
            objHfcBETransfer.ZonaID = "";
            objHfcBETransfer.PlanoID = SessionTransf.hdnCodPla;
            objHfcBETransfer.EdificioID = SessionTransf.hdnCodEdi;
            objHfcBETransfer.Observacion = controls.txtNote.val();
            objHfcBETransfer.MotivoID = controls.ddlReasonSot.val();
            objHfcBETransfer.chkUseChangeBillingChecked = controls.chkUseChangeBilling.prop('checked');
            objHfcBETransfer.chkEmailChecked = controls.chkSendMail.prop('checked');
            objHfcBETransfer.Email = controls.txtSendMail.val();
            objHfcBETransfer.txtNotText = controls.txtNote.val();
            objHfcBETransfer.hdnCodigoRequestAct = Session.RequestActId == '' || Session.RequestActId == null ? '0' : Session.RequestActId;// SessionTransf.CodigoRequestAct;
            objHfcBETransfer.Cargo = '0.00';//SessionTransf.hdnMontoFideFinal;
            objHfcBETransfer.DescripCADDAC = $("#ddlCACDAC option:selected").html();
            objHfcBETransfer.hdnCodEdi = SessionTransf.hdnCodEdi;
            objHfcBETransfer.hdnCenPobDes = SessionTransf.hdnCenPobDes;
            objHfcBETransfer.hdnCodPla = SessionTransf.hdnCodPla;
            objHfcBETransfer.hdnUbiID = SessionTransf.hdnUbiID;
            objHfcBETransfer.NameCustomer = SessionTransf.NameCustomer;
            objHfcBETransfer.NotAddressCustomer = SessionTransf.NotAddressCustomer;
            objHfcBETransfer.CountryCustomer = SessionTransf.CountryCustomer;
            objHfcBETransfer.PAIS_LEGAL = SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
            objHfcBETransfer.DepCustomer = SessionTransf.DepCustomer;
            objHfcBETransfer.ProvCustomer = SessionTransf.ProvCustomer;
            objHfcBETransfer.DistCustomer = SessionTransf.DistCustomer;
            objHfcBETransfer.IdEdifCustomer = SessionTransf.IdEdifCustomer;
            objHfcBETransfer.EmailCustomer = SessionTransf.EmailCustomer;
            objHfcBETransfer.PlanoIDCustomer = SessionTransf.PlanoIDCustomer;
            objHfcBETransfer.urbLegalCustomer = SessionTransf.urbLegalCustomer;
            objHfcBETransfer.DirecDespachoCustomer = objHfcBETransfer.DirecDespachoCustomer;
            objHfcBETransfer.TypDocRepreCustomer = SessionTransf.TypDocRepreCustomer;
            objHfcBETransfer.NumbDocRepreCustomer = SessionTransf.NumbDocRepreCustomer;
            objHfcBETransfer.RepreCustomer = SessionTransf.RepreCustomer;
            objHfcBETransfer.cuenta = SessionTransf.cuentaCustomer;
            objHfcBETransfer.CurrentUser = SessionTransac.SessionParams.USERACCESS.login;
            objHfcBETransfer.USRREGIS = SessionTransac.SessionParams.USERACCESS.login;
            objHfcBETransfer.agendaGetFecha = $('#txtFProgramacion').val();
            objHfcBETransfer.FechaProgramada = $('#txtFProgramacion').val();
            objHfcBETransfer.agendaGetCodigoFranja = $("#cboFranjaHoraria option:selected").val();
            objHfcBETransfer.strSubTypeWork = $("#ddlTypeSubWork option:selected").val();
            objHfcBETransfer.agendaGetValidaEta = SessionTransf.strValidateETA;
            objHfcBETransfer.agendaGetTipoTrabajo = controls.ddlTypeWork.val();
            objHfcBETransfer.ObtenerHoraAgendaETA = $("#cboFranjaHoraria option:selected").html();
            objHfcBETransfer.FranjaHora = $("#cboFranjaHoraria option:selected").attr("idHorario");
            objHfcBETransfer.CicloFact = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
            //objHfcBETransfer.chkLoyalty = controls.chkLoyalty.prop('checked');
            objHfcBETransfer.DEPARTAMENTO = SessionTransac.SessionParams.DATACUSTOMER.Departament;
            objHfcBETransfer.CodPos = SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
            objHfcBETransfer.RefAddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
            objHfcBETransfer.AddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
            objHfcBETransfer.strIgv = that.strIGV == null || that.strIGV == '' ? '0.00' : that.strIGV;
            objHfcBETransfer.strtypeCliente = SessionTransac.SessionParams.DATACUSTOMER.CustomerType == 'Consumer' ? 'MASIVO' : 'CORPORATIVO';
            confirm(SessionTransf.hdnMensaje7, 'Confirmar', function (result) {
                if (result == true) {
                    $.blockUI({
                        message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                        css: {
                            border: 'none',
                            padding: '15px',
                            backgroundColor: '#000',
                            '-webkit-border-radius': '10px',
                            '-moz-border-radius': '10px',
                            opacity: .5,
                            color: '#fff'
                        }
                    });
                    $.app.ajax({
                        async: true,
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objHfcBETransfer),

                        url: urlBase + '/GetRecordTransaction',
                        success: function (response) {
                            that.ReturnGetRecordTransaction(response);
                        }
                    });
                }
            });
        },
        ReturnGetRecordTransaction: function (response) {
            var that = this,
                controls = that.getControls();
            
            var message;
            if (response.data != null) {

                if (response.data.ItemGeneric.Number > 0)//CriterioMensajeOK
                {
                    message = string.format("{0}. - Nro. Sot: {1}.", response.data.ItemGeneric.Description, response.data.ItemGeneric.Number);
                    SessionTransf.strPath = response.data.ItemGeneric.Code3;
                    controls.lnkNumSot.html(response.data.ItemGeneric.Number);
                    
                        controls.btnConstancy.attr("disabled", false); 
                        controls.btnSave.attr("disabled", true);
                        $("#IDdivtabTypeWork textarea, #IDdivtabTypeWork input, #IDdivtabTypeWork select").attr('disabled', true);
                        SessionTransf.flagSaveTransactions = 'I';

                    alert(response.data.ItemGeneric.Description, "Informativo");
                }
                else {
                        controls.btnSave.attr("disabled", true);
                        controls.btnConstancy.attr("disabled", true);
                    
                    alert(response.data.ItemGeneric.Description, "Alerta");
                    message = response.data.ItemGeneric.Description;
                }

                if (response.data.ItemGeneric.Code2 == "0" || response.data.ItemGeneric.Code2 == null) {
                    alert(response.data.ItemGeneric.Description2, "Alerta");
                    message = message + "<br>" + response.data.ItemGeneric.Description2;
                }
                else {
                    SessionTransf.hdnInterID = response.data.ItemGeneric.Code2;
                }
                controls.lblErrorMessage.html(message);
                controls.divErrorAlert.show();
            }

        },
        getReasonSot: function () {
            var that = this, objReasonSot = {}, controls = that.getControls();
            objReasonSot.strIdSession = Session.IDSESSION;
            objReasonSot.IdTipoTrabajo = controls.ddlTypeWork.val();
          //  controls.ddlReasonSot.append($('<option>', { value: '', html: 'Seleccionar' }));

            var urlBase = '/Transactions/HFC/ChangeEquipment';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objReasonSot), 
                url: urlBase + '/GetMotiveSOTByTypeJob',
                success: function (response) {
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            //if (value.Description == SessionTransf.hdnddlSOT) {
                            //    controls.ddlReasonSot.append($('<option>', { value: value.Code, html: value.Description }).attr('selected', 'selected'));
                            //}
                            controls.ddlReasonSot.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                }
            });
        },
        getWorkSubType: function (pstrWorkType) {

            var that = this, controls = that.getControls();
            var strIdSession = Session.IDSESSION;
            var strContractID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;

            controls.ddlTypeSubWork.empty();
            controls.ddlTypeSubWork.append($('<option>', { value: '', html: 'Seleccionar' }));

            var urlBase = '/Transactions/HFC/ChangeEquipment';
            $.app.ajax({
                async: false,
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "',strCodTypeWork:'" + pstrWorkType + "',strContractID:'" + strContractID + "'}",
                url: urlBase + '/GetWorkSubType',
                success: function (response) {
                    if (response.data != null) {
                        $.each(response.data.ListGeneric, function (index, value) {
                            var codTipoSubTrabajo = value.Code.split("|");
                            if (response.typeValidate.COD_SP == "0" && codTipoSubTrabajo[0] == response.typeValidate.COD_SUBTIPO_ORDEN) {
                                controls.ddlTypeSubWork.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2, selected: true }));
                                /*controls.ddlTypeSubWork.attr('disabled', true);*/
                            }
                            else {
                                controls.ddlTypeSubWork.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2 }));
                            }
                        });
                    }
                }
            });
        },
        getWorkType: function (strTransacType) {
            var that = this, controls = that.getControls();;
            var strIdSession = Session.IDSESSION;

            var urlBase = '/Transactions/HFC/ChangeEquipment';

            $.app.ajax({
                async: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{strIdSession:'" + strIdSession + "',strTransacType:'" + strTransacType + "'}",
                url: urlBase + '/GetWorkType',
                success: function (response) {
                    if (response.data != null) {
                        $.each(response.data.ListGeneric, function (index, value) {
                            controls.ddlTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                    that.getWorkSubType(controls.ddlTypeWork.val());
                    that.F_ValidateETA();
                }
            });
        },
        InitCacDac: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {};
            objCacDacType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            var parameters = {};
            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        //VALIDATION--------------------
        f_ValidateEmail: function () {
            if ($('#chkSendMail').is(':checked')) {
                var regExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/
                if ($('#txtSendMail').val() != '') {
                    if (regExp.test($('#txtSendMail').val()))
                        return true;
                    else
                        alert(SessionTransf.strMensajeEmail, "Alerta");
                    //$("#ErrorMessageEmail").text("Ingrese una direccion de correo valida.");
                    return false;
                }
                else {
                    alert(SessionTransf.hdnMessageSendMail, "Alerta");
                    return false;
                }
            } else {
                return true;
            }
        },
        f_MostrarMensajeError: function (o) {

            alert(o.msn, "Alerta");
            o.obj.addClass('error');
            o.obj.focus();
        },
        f_closeErrorAlert: function () {
            var that = this;
            var controls = that.getControls();
            controls.divErrorAlert.hide();
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
        f_EnableAgendamiento: function (bool) {
            var that = this;

            if (bool == true) {
                $('#txtFProgramacion').prop("disabled", false);
                $('#cboFranjaHoraria').prop("disabled", false);
                $('#btnValidateSchedule').prop("disabled", true);
            }
            else {
                var fechaServidor = new Date(Session.ServerDate);

                $('#txtFProgramacion').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                if ($('#ddlTypeWork').val().indexOf(".|") == -1) {
                    $('#btnValidateSchedule').prop("disabled", true);
                    Session.VALIDATE = "1";
                }
                else {
                    $('#btnValidateSchedule').prop("disabled", false);
                    Session.VALIDATE = "0";
                }
            }
        },
        
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        strIGV: '',
        strClaseCode: '',
        strSubClaseCode: '',
        strTipo: '',
        strClase: '',
        strSubClase: ''
    };

    
    $.fn.HFCChangeEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCChangeEquipment'),
                options = $.extend({}, $.fn.HFCChangeEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCChangeEquipment', data);
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

    $.fn.HFCChangeEquipment.defaults = {
    }

    $('#divBody').HFCChangeEquipment();

})(jQuery);

