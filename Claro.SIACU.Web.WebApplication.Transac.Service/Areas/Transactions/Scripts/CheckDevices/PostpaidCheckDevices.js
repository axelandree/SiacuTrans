
(function ($) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.PostpaidCheckDevices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            //divErrorAlert: $('#divErrorAlert', $element),

            lblErrorMessage: $('#lblErrorMessage', $element),
            lblCustomerId: $('#lblCustomerId', $element),
            lblAccountNumber: $('#lblAccountNumber', $element),
            lblCustomerName: $('#lblCustomerName', $element),
            lblContactName: $('#lblContactName', $element),
            tblCheckDevicesPostpaid: $('#tblCheckDevicesPostpaid', $element),
            btnCloseCheckDevices: $('#btnCloseCheckDevices', $element),
           
            myModalLoad: $('#myModalLoad', $element)

            ,lblTitle: $('#lblTitle', $element)
            
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnCloseCheckDevices.addEvent(that, 'click', that.btnCloseCheckDevices_click);
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
            
            that.getPageLoad();
           
        },

        btnCloseCheckDevices_click: function () {
            window.close();
        },
        getPageLoad: function () {
            var that = this, param = {};
            var controls = that.getControls();
            controls.divErrorAlert.hide();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: location.protocol + "//" + location.host + "/Transactions/Postpaid/CheckDevices/Page_Load",
                success: function (response) {
                    
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
            var controls = that.getControls();




            controls.lblCustomerId.text(sessionTransacPost.SessionParams.DATACUSTOMER.CustomerID);
            controls.lblAccountNumber.text(sessionTransacPost.SessionParams.DATACUSTOMER.Account);
            controls.lblCustomerName.text(sessionTransacPost.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblContactName.text(sessionTransacPost.SessionParams.DATACUSTOMER.FullName);
            controls.lblTitle.text("Consultar Equipos Postpago");
            var param = {
                StrIdSession: "123",
                StrCoId: sessionTransacPost.SessionParams.DATACUSTOMER.ContractID,
                StrMsisdn: ""
        };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: location.protocol + "//" + location.host + "/Transactions/Postpaid/CheckDevices/GetProductDetails",
                success: function (response) {
                    that.InitDataTable(response);
                    var msg = "Total de Registros: " + response.StrCountItems;
                    controls.divErrorAlert.show();
                    controls.lblErrorMessage.text(msg);

                }
            });
        },
        InitDataTable: function (rowsData) {
            var that = this, controls = that.getControls();
            
            controls.tblCheckDevicesPostpaid.dataTable({
                info: false,
                select: "single",
                paging: true,
                searching: false,
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                data: rowsData.ListServicesDTH,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "COD_DECO" },
                    { "data": "TIP_DECO" },
                    { "data": "COD_TARJETA" },
                    { "data": "TIP_EQUIPO" },
                    { "data": "ESTADO" },
                    { "data": "FEC_ESTADO" }
                ]
            });
        }
    };

    $.fn.PostpaidCheckDevices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidCheckDevices'),
                options = $.extend({}, $.fn.PostpaidCheckDevices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidCheckDevices', data);
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

    $.fn.PostpaidCheckDevices.defaults = {
    };

    $('#divBody').PostpaidCheckDevices();
})(jQuery);