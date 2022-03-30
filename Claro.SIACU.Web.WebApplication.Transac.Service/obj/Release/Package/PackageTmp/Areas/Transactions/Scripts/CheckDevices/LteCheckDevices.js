var SessionCheckDevices = {};

var hdnPermisoExp;
var hdnPermisoBus;
var sessionTransac = {};

var userValidatorAuth = "";
sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
////console.logsessionTransac);

(function ($) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.LteCheckDevices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            //DIVS
            divContentProgramDate: $('#divContentProgramDate', $element),

            //LABELS
            lblCustomerId: $('#lblCustomerId', $element),
            lblAccountNumber: $('#lblAccountNumber', $element),
            lblCustomerName: $('#lblCustomerName', $element),
            lblContactName: $('#lblContactName', $element),
            tblCheckDevicesLte: $('#tblCheckDevicesLte', $element),
            btnClose: $('#btnClose', $element),

            //MODAL LOAD
            myModalLoad: $('#myModalLoad', $element),

            spnMainTitle: $('#spnMainTitle'),
            lblTitle: $('#lblTitle', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();           
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            that.maximizarWindow();
            that.render();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
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
            that.getPageLoad_HFC();
        },

        btnClose_click: function () {
            parent.window.close();
        },                
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            controls.spnMainTitle.html('<b>' + titlePage + '</b>');
        },
        getCustomerData: function () {
            var that = this, controls = that.getControls();
            controls.lblCustomerId.text(sessionTransac.SessionParams.DATACUSTOMER.CustomerID);
            controls.lblAccountNumber.text(sessionTransac.SessionParams.DATACUSTOMER.Account);
            controls.lblCustomerName.text(sessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblContactName.text(sessionTransac.SessionParams.DATACUSTOMER.FullName);
        },
        getPageLoad_HFC: function () {
            var that = this, param = {};
            param.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: location.protocol + "//" + location.host + "/Transactions/LTE/CheckDevices/PageLoad_LTE",
                success: function (response) {
                    if (Session.USERACCESS == {} && Session.USERACCESS.CODEUSER == "") {
                        parent.window.close();
                        opener.parent.top.location.href = response.hdnSiteUrl;
                        return;
                    }
                    that.setMainTitle(response.hdnTituloPagina);
                    that.getCustomerData();
                    that.LoadDataCheckDevices();
                }
            });

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


        LoadDataCheckDevices: function () {
            var that = this;
            var param = {
                strIdSession : Session.IDSESSION,
                strCoid: sessionTransac.SessionParams.DATACUSTOMER.ContractID,
                strCustomerId: sessionTransac.SessionParams.DATACUSTOMER.CustomerID,
                currentUser: sessionTransac.SessionParams.USERACCESS.login,
                strFullName: sessionTransac.SessionParams.USERACCESS.fullName
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: location.protocol + "//" + location.host + "/Transactions/LTE/CheckDevices/GetProductDetails",
                success: function (response) {
                    //console.logresponse);
                    that.InitDataTable(response);
                }
            });
        },
        InitDataTable: function (rowsData) {
            var that = this, controls = that.getControls();
            controls.tblCheckDevicesLte.dataTable({
                info: false,
                select: "single",
                paging: true,
                searching: false,
                scrollX: true,
                scrollY: 300,
                scrollCollapse: true,
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
                    { "data": "codigo_material" },
                    { "data": "codigo_sap" },
                    { "data": "numero_serie" },
                    { "data": "macadress" },
                    { "data": "descripcion_material" },
                    { "data": "tipo_equipo" },
                    { "data": "id_producto" },
                    { "data": "modelo" },
                    { "data": "convertertype" },
                    { "data": "servicio_principal" },
                    { "data": "headend" },
                    { "data": "ephomeexchange" },
                    { "data": "numero" }
                ]
            });
        }
    };

    $.fn.LteCheckDevices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('LteCheckDevices'),
                options = $.extend({}, $.fn.LteCheckDevices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LteCheckDevices', data);
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

    $.fn.LteCheckDevices.defaults = {
    };

    $('#divBody').LteCheckDevices();
})(jQuery);