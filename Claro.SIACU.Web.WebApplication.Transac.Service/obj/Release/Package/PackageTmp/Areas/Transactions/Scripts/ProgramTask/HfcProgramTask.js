var hdnPermisoExp;
var hdnPermisoBus;
var sessionTransac = {};
var userValidatorAuth = "";
var dataSearch;

sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
sessionTransac.HiddensTransact = {};

function eliminaProg(codservi, codid, servcestado, pag, controls) {
    confirm(sessionTransac.HiddensTransact.hdnMensaje3, 'Confirmar', function () {
        var paramDelete = {
            srtIdSession: Session.IDSESSION,
            strTransaction: Session.IDSESSION,
            vstrCodServ: codservi,
            vstrCoId: codid,
            vstrServCEstado: servcestado,
            vstrFecDesde: $('#txtFromDate').val(),
            vstrFecHasta: $('#txtToDate').val(),
            fullName: sessionTransac.SessionParams.DATACUSTOMER.FullName,
            currentUser: sessionTransac.SessionParams.USERACCESS.login,
            customerId: sessionTransac.SessionParams.DATACUSTOMER.CustomerID,
            flagTfi: sessionTransac.SessionParams.DATASERVICE.FlagTFI,
            codePlanInst: sessionTransac.SessionParams.DATASERVICE.CodePlanTariff,
            nroCelular: sessionTransac.SessionParams.DATASERVICE.CellPhone,
            socialReason: sessionTransac.SessionParams.DATACUSTOMER.Name,
            representanteLegal: sessionTransac.SessionParams.DATACUSTOMER.Name,
            tipoDoc: sessionTransac.SessionParams.DATACUSTOMER.DocumentType ,
            nroDoc: sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber,
            plan: sessionTransac.SessionParams.DATASERVICE.Plan,
            cicloFacturacion: sessionTransac.SessionParams.DATACUSTOMER.BillingCycle
        }

        var strUrlController = window.location.protocol + '//' + window.location.host + '/Transactions/HFC/' + '/ProgramTask/HfcProgramTask_Delete';
        $.ajax({
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            url: strUrlController,
            data: JSON.stringify(paramDelete),
            success: function (response) {
                alert(response, "Informativo");
                LoadTableTask(pag, controls);
            },
            error: function () {
                alert(sessionTransac.HiddensTransact.hdnMensaje1, "Alerta");
            }
        });
    });
}

function editaProg(codservi, codid, servcestado) {
    var urlSuspensionService = location.protocol + "//" + location.host + "/Transactions/HFC/SuspensionService/HfcSuspensionService?mode=Edi&tipoServi=" + codservi + "&estadoServi=" + servcestado;
    window.open(urlSuspensionService, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1200, height=640');
}

function LoadTableTask(pag, controls) {
    $.blockUI({
        message: controls.myModalLoad,
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

    var params =
    {
        srtIdSession: Session.IDSESSION,
        strTransaction: "1234",
        vstrCoId: $("#txtIdContract").val(),
        vstrCuenta: $("#txtAccount").val(),
        vstrFDesde: $("#txtFromDate").val(),
        vstrFHasta: $("#txtToDate").val(),
        vstrEstado: $("#cboState").val(),
        vstrAsesor: $("#txtAdviser").val(),
        vstrTipoTran: $("#cboTypeTransaction").val(),
        vstrCodInter: $("#txtInteractionCode").val(),
        vstrCacDac: $("#cboAtentionLocal").val(),
        fullName: sessionTransac.SessionParams.DATACUSTOMER.FullName,
        currentUser: sessionTransac.SessionParams.USERACCESS.login,
        customerId: sessionTransac.SessionParams.DATACUSTOMER.CustomerID
    };

    var url = pag.strUrl + 'ProgramTask/HfcProgramTask_ListTask';
    var num = 0;

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(params),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: true,
        error: function (error) {
            alert(sessionTransac.HiddensTransact.hdnMensaje1, "Alerta");
        },
        success: function (data) {
            if (data.LstTask.length > 0) {
                var arrayData = data.LstTask;
                var objToDataTable = [];
                $.each(arrayData, function (i, item) {
                    var ultimaCol = "";
                    var hayElim = false;
                    var hayEdi = false;
                    if (item.SERVC_ESTADO !== "3" && item.SERVC_ESTADO !== "4") {
                        if (item.DESC_SERVI !== "REACTIVACION") {
                            //Topes de Consumo
                            if (item.SERVC_CO_SER === "1236") {
                                if (item.SERVC_ESTADO === "1") {
                                    ultimaCol = ultimaCol + '<a id="link_' + num + '" href="javascript:void(0);"><span class="glyphicon glyphicon-trash"></span></a>';
                                    hayElim = true;
                                }
                            }
                            else {
                                ultimaCol = ultimaCol + '<a id="link_' + num + '" href="javascript:void(0);"><span class="glyphicon glyphicon-trash"></span></a>';
                                hayElim = true;
                            }
                        }
                        if (item.DESC_SERVI === "REACTIVACION" || item.DESC_SERVI === "SUSPENSION") {
                            if (hayElim) {
                                ultimaCol = ultimaCol + ' / <a id="linkEd_' + num + '" href="javascript:void(0);"><span class="glyphicon glyphicon-pencil"></span></a>';
                            } else {
                                ultimaCol = '<a id="linkEd_' + num + '" href="javascript:void(0);"><span class="glyphicon glyphicon-pencil"></span></a>';
                            }
                            hayEdi = true;
                        }

                    } else {
                        ultimaCol = ultimaCol + "-";
                    }

                    if (hayElim) {
                        $(document).on('click', '#link_' + num, function () {
                            //Si es que tiene permisos de Eliminar.
                            if (sessionTransac.HiddensTransact.hdnPermisoEliminar === "1") {
                                eliminaProg(item.SERVI_COD, item.CO_ID, item.SERVC_ESTADO, pag, controls);
                            }
                            else {
                                alert(sessionTransac.HiddensTransact.hdnMensajeNoTienePermisoEliminar, "Alerta");
                            }
                        });
                    }

                    if (hayEdi) {
                        $(document).on('click', '#linkEd_' + num, function () {
                            //Si es que tiene permisos de Editar.
                            if (sessionTransac.HiddensTransact.hdnPermisoEditar === "1") {
                                editaProg(item.SERVI_COD, item.CO_ID, item.SERVC_ESTADO, pag, controls);
                            }
                            else {
                                alert(sessionTransac.HiddensTransact.hdnMensajeNoTienePermisoEditar, "Alerta");
                            }
                        });
                    }

                    num++;

                    var itemToDataTable =
                    {
                        CO_ID: item.CO_ID,
                        CUSTOMER_ID: item.CUSTOMER_ID,
                        SERVD_FECHAPROG: item.SERVD_FECHAPROG,
                        SERVD_FECHA_REG: item.SERVD_FECHA_REG,
                        SERVD_FECHA_EJEC: item.SERVD_FECHA_EJEC,
                        DESC_ESTADO: item.DESC_ESTADO,
                        DESC_SERVI: item.DESC_SERVI,
                        SERVC_NROCUENTA: item.SERVC_NROCUENTA,
                        SERVC_TIPO_SERV: item.SERVC_TIPO_SERV,
                        UltimaCol: ultimaCol
                    };
                    objToDataTable.push(itemToDataTable);
                });

                pag.InitDataTableTableProgramTask(objToDataTable);
                $("#btnExport").show("fade");
            } else {
                pag.InitDataTableTableProgramTask();
                alert(sessionTransac.HiddensTransact.hdnMensaje2, "Alerta");
                $("#btnExport").hide("fade");
            }

        }
    });
}

(function ($) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.ProgramTask.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblPhone: $('#lblPhone', $element)
            , lblContract: $('#lblContract', $element)
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblTypeCustomer: $('#lblTypeCustomer', $element)

            , spnMainTitle: $('#spnMainTitle')

            , myModalLoad: $('#myModalLoad', $element)

            , txtIdContract: $('#txtIdContract', $element)
            , txtAccount: $('#txtAccount', $element)
            , txtTelephone: $('#txtTelephone', $element)
            , txtFromDate: $('#txtFromDate', $element)
            , txtToDate: $('#txtToDate', $element)
            , txtInteractionCode: $('#txtInteractionCode', $element)
            , txtAdviser: $('#txtAdviser', $element)

            , cboState: $('#cboState', $element)
            , cboTypeTransaction: $('#cboTypeTransaction', $element)
            , cboAtentionLocal: $('#cboAtentionLocal', $element)

            , btnSearch: $('#btnSearch', $element)
            , btnClose: $('#btnClose', $element)
            , btnExport: $('#btnExport', $element)
            , tblProgramTask: $('#tblProgramTask', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_Click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_Click);
            controls.txtFromDate.datepicker({ format: 'dd/mm/yyyy', endDate: '+0d' });
            controls.txtToDate.datepicker({ format: 'dd/mm/yyyy', endDate: '+0d' });
            that.maximizarWindow();
            that.render();
        },
        render: function () {
            var that = this, controls = this.getControls();
            that.getCustomerData();
            that.InitDataTableTableProgramTask();
        },
        btnSearch_Click: function () {
            var that = this, controls = this.getControls();
            LoadTableTask(that, controls);
        },
        btnExport_Click: function () {
            var that = this, controls = that.getControls();
            $.blockUI({
                message: controls.myModalLoad,
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
            var params =
            {
                srtIdSession: Session.IDSESSION,
                strTransaction: "1234",
                vstrCoId: $("#txtIdContract").val(),
                vstrCuenta: $("#txtAccount").val(),
                vstrFDesde: $("#txtFromDate").val(),
                vstrFHasta: $("#txtToDate").val(),
                vstrEstado: $("#cboState").val(),
                vstrAsesor: $("#txtAdviser").val(),
                vstrTipoTran: $("#cboTypeTransaction").val(),
                vstrCodInter: $("#txtInteractionCode").val(),
                vstrCacDac: $("#cboAtentionLocal").val(),
                strCuentUser: sessionTransac.SessionParams.USERACCESS.login,
                strTelephone: sessionTransac.SessionParams.DATACUSTOMER.CustomerID,
                strNameComplet: sessionTransac.SessionParams.USERACCESS.fullName
        };

            var myUrlDowload = '/Transactions/CommonServices/DownloadExcel';

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '/Transactions/HFC/ProgramTask/GetExportExcel',
                data: JSON.stringify(params),
                success: function (path) {
                    window.location = myUrlDowload + '?strPath=' + path + "&strNewfileName=HcfScheduledTask.xlsx";
                }
            });
        },
        btnClose_Click: function () {
            parent.window.close();
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
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            controls.spnMainTitle.html('<b>' + titlePage + '</b>');
        },
        getLoadProgramTask: function () {
            var controls = this.getControls(), that = this;
            if (Session.USERACCESS == {} && Session.USERACCESS.CODEUSER == "") {
                parent.window.close();
                opener.parent.top.location.href = Session.RouteSiteStart;
                return;
            }

            var strUrlController = that.strUrl + '/ProgramTask/HfcProgramTask_PageLoad';
            var param = { "strIdSession": Session.IDSESSION, "strTransaction": "1234", 'strPermisos': sessionTransac.SessionParams.USERACCESS.optionPermissions }

            $.blockUI({
                message: controls.myModalLoad,
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

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    if (response.lblMensajeVisible) {
                        alert(response.lblMensajeText, "Informativo");
                        return;
                    }

                    sessionTransac.HiddensTransact.hdnPermisoEliminar = response.hdnPermisoEliminar;
                    sessionTransac.HiddensTransact.hdnPermisoEditar = response.hdnPermisoEditar;

                    sessionTransac.HiddensTransact.hdnTituloPagina = response.hdnTituloPagina;
                    sessionTransac.HiddensTransact.hdnMensaje1 = response.hdnMensaje1;
                    sessionTransac.HiddensTransact.hdnMensaje2 = response.hdnMensaje2;
                    sessionTransac.HiddensTransact.hdnMensaje3 = response.hdnMensaje3;
                    sessionTransac.HiddensTransact.hdnMensajeNoTienePermisoEliminar = response.hdnMensajeNoTienePermisoEliminar;
                    sessionTransac.HiddensTransact.hdnMensajeNoTienePermisoEditar = response.hdnMensajeNoTienePermisoEditar;

                    that.setMainTitle(response.hdnTituloPagina);
                    that.InitCacDat();
                    that.InitLoadCombos();
                    that.getCustomerPhone();
                },
                error: function (error) {
                    alert('Error', error);
                }
            });
        },
        getCustomerPhone: function () {
            var that = this, paramters = {};
            paramters.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            paramters.intIdContract = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
            paramters.strTypeProduct = sessionTransac.UrlParams.SUREDIRECT;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(paramters),
                url: '/Transactions/LTE/AdditionalServices/GetCustomerPhone',
                success: function (response) {
                    if (response !== "") {
                        sessionTransac.HiddensTransact.hdCustomerPhone = response;
                        $('#lblPhone').text(sessionTransac.HiddensTransact.hdCustomerPhone);
                        $('#txtTelephone').val(sessionTransac.HiddensTransact.hdCustomerPhone);
                    }
                }
            });
        },
        getCustomerData: function () {
            var that = this, controls = that.getControls();

            var oCustomer = sessionTransac.SessionParams.DATACUSTOMER;
            var oUserAccess = sessionTransac.SessionParams.USERACCESS;
            var oDataService = sessionTransac.SessionParams.DATASERVICE;

            $("#lblContract").text((oCustomer.ContractID == null) ? "" : oCustomer.ContractID);
            $("#lblContact").text((oCustomer.FullName == null) ? "" : oCustomer.FullName);
            $("#lblCustomerName").text((oCustomer.BusinessName == null) ? "" : oCustomer.BusinessName);
            $("#lblDateActivation").text((oCustomer.ActivationDate == null) ? "" : oCustomer.ActivationDate);
            $("#lblReprLegal").text((oCustomer.LegalAgent == null) ? "" : oCustomer.LegalAgent);
            $("#lblIdentificationDocument").text((oCustomer.DocumentNumber == null) ? "" : oCustomer.DocumentNumber);


            if (oCustomer.CustomerType != null) {
                if (oCustomer.CustomerType != "") {
                    $("#lblTypeCustomer").text(oCustomer.CustomerType);
                }
                else {
                    if (oCustomer.objPostDataAccount.CustomerType != "") {
                        $("#lblTypeCustomer").text(oCustomer.objPostDataAccount.CustomerType);
                    }
                }
            }
            else {
                if (oCustomer.objPostDataAccount.CustomerType != "") {
                    $("#lblTypeCustomer").text(oCustomer.objPostDataAccount.CustomerType);
                }
            }


            $("#lblDocReprLegal").text((oCustomer.DNIRUC == null) ? "" : oCustomer.DNIRUC);
            $("#lblCycleBilling").text((oCustomer.CustomerContact == null) ? "" : oCustomer.CustomerContact);
            $("#lblPlanName").text((oDataService.Plan == null) ? "" : oDataService.Plan);

            if (oCustomer.BillingCycle != null) {
                if (oCustomer.BillingCycle != "") {
                    $("#lblCycleFacture").text(oCustomer.BillingCycle);
                }
                else {
                    if (oCustomer.objPostDataAccount.BillingCycle != "") {
                        $("#lblCycleFacture").text(oCustomer.objPostDataAccount.BillingCycle);
                    }
                }
            }
            else {
                if (oCustomer.objPostDataAccount.BillingCycle != "") {
                    $("#lblCycleFacture").text(oCustomer.objPostDataAccount.BillingCycle);
                }
            }

            $("#lblLimitCredit").text((oCustomer.objPostDataAccount.CreditLimit == null) ? "" : oCustomer.objPostDataAccount.CreditLimit);
            $("#lblCustomerId").text((oCustomer.CustomerID == null) ? "" : oCustomer.CustomerID);
            $("#lblAddress").text((oCustomer.InvoiceAddress == null) ? "" : oCustomer.InvoiceAddress);
            $("#lblAddressNote").text((oCustomer.Reference == null) ? "" : oCustomer.Reference);
            $("#lblDepartamento").text((oCustomer.InvoiceDepartament == null) ? "" : oCustomer.InvoiceDepartament);
            $("#lblDistrito").text((oCustomer.InvoiceDistrict == null) ? "" : oCustomer.InvoiceDistrict);
            $("#lblCodePlans").text((oCustomer.CodeCenterPopulate == null) ? "" : oCustomer.CodeCenterPopulate);
            $("#lblPais").text((oCustomer.InvoiceCountry == null) ? "" : oCustomer.InvoiceCountry);
            $("#lblProvincia").text((oCustomer.InvoiceProvince == null) ? "" : oCustomer.InvoiceProvince);
            $("#lblUbigeo").text((oCustomer.InstallUbigeo == null) ? "" : oCustomer.InstallUbigeo);


            //controls.lblPhone.text(sessionTransac.SessionParams.DATACUSTOMER.Telephone);
            //controls.lblContract.text(sessionTransac.SessionParams.DATACUSTOMER.ContractID);
            //controls.lblCustomerName.text(sessionTransac.SessionParams.DATACUSTOMER.FullName);
            //controls.lblIdentificationDocument.text(sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
            //controls.lblTypeCustomer.text(sessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.txtAccount.val(sessionTransac.SessionParams.DATACUSTOMER.Account);
        },
        InitCacDat: function () {
            var that = this, controls = that.getControls(), objCacDacType = {};

            objCacDacType.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            var parameters = {};
            parameters.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = sessionTransac.SessionParams.USERACCESS.login;

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
                            $("#cboAtentionLocal").append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    $("#cboAtentionLocal").append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    $("#cboAtentionLocal").append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            //if (itemSelect != null && itemSelect.toString != "undefined") {
                            //    //console.log"valor itemSelect: " + itemSelect);
                            //    $("#cboAtentionLocal option[value=" + itemSelect + "]").attr("selected", true);
                            //}
                        }
                    });
                }

                
            });
        },
        InitLoadCombos: function () {
            var that = this, controls = that.getControls();
            var parameters = {};
            var strUrlController = that.strUrl + '/ProgramTask/HfcProgramTask_ComboBoxLoad';
            parameters.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            parameters.strTransaction = sessionTransac.SessionParams.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: strUrlController,
                success: function (response) {
                    if (response != null) {
                        $("#cboState").html("");
                        var item = '<option value="-1">Seleccionar..</option>';
                        $.each(response.LstEstado, function (index, value) {
                            item += '<option value="' + value.Code + '">' + value.Description + '</option>';
                        });

                        $("#cboState").html(item);

                        $("#cboTypeTransaction").html("");

                        item = '<option value="-1">Seleccionar..</option>';
                        $.each(response.LstTipoTransacciones, function (index, value) {
                            item += '<option value="' + value.Code + '">' + value.Description + '</option>';
                        });

                        $("#cboTypeTransaction").html(item);
                    }
                }
            });
        },
        InitDataTableTableProgramTask: function (rowsData) {
            var that = this, controls = that.getControls();
            controls.tblProgramTask.dataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                //scrollX: true,
                //scrollY: 300,
                scrollCollapse: false,
                destroy: true,
                data: rowsData,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "CO_ID" },
                    { "data": "CUSTOMER_ID" },
                    { "data": "SERVD_FECHAPROG" },
                    { "data": "SERVD_FECHA_REG" },
                    { "data": "SERVD_FECHA_EJEC" },
                    { "data": "DESC_ESTADO" },
                    { "data": "DESC_SERVI" },
                    { "data": "SERVC_NROCUENTA" },
                    { "data": "SERVC_TIPO_SERV" },
                    { "data": "UltimaCol" }
                ]
            });

            that.getLoadProgramTask();

        },
        strUrl: window.location.protocol + '//' + window.location.host + '/Transactions/HFC/'
    };

    $.fn.ProgramTask = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ProgramTask'),
                options = $.extend({}, $.fn.ProgramTask.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('ProgramTask', data);
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

    $.fn.ProgramTask.defaults = {
    }
    $('#divBody').ProgramTask();
})(jQuery);
