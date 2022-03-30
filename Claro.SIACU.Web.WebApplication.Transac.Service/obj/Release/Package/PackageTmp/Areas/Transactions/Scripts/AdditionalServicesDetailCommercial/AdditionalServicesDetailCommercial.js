
var sessionTransacPost = {};
sessionTransacPost = JSON.parse(sessionStorage.getItem("SessionTransac"));
(function ($) {


    var Form = function ($element, options) {
        $.extend(this, $.fn.AdditionalServicesDetailCommercial.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            divErrorAlert: $('#divErrorAlert', $element),

            lblServiceCommercial: $('#lblServiceCommercial', $element),
            tblAdditionalServicesDetailCommercial: $('#tblAdditionalServicesDetailCommercial', $element),
            btnCloseDetailCommercial: $('#btnCloseDetailCommercial', $element),
            lblErrorMessage: $('#lblErrorMessage', $element),

            myModalLoad: $('#myModalLoad', $element)

            , lblTitle: $('#lblTitle', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnCloseDetailCommercial.addEvent(that, 'click', that.btnCloseDetailCommercial_click);
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

        btnCloseDetailCommercial_click: function () {
            window.close();
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

        getPageLoad: function () {
            //console.log"Codigo de Servicios");
            //console.logAdditionalServices.HidCodService);

            var that = this,
                model = {
                    IdSession: "54654654",
                    UserLogin: sessionTransacPost.SessionParams.USERACCESS.login,
                    StrCodSer: AdditionalServices.HidCodService//"143" //debe venir del flujo del proceso
                };
            
            var controls = that.getControls();
            controls.divErrorAlert.hide();
            controls.lblTitle.text("Servicios BSCS Relacionados al Servicio Comercial");
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: location.protocol + "//" + location.host + "/Transactions/Postpaid/AdditionalServicesDetailCommercial/Page_Load",
                success: function (response) {
                    
                    that.InitDataTable(response);
                    controls.lblServiceCommercial.text(response.StrlblServiceCommercial);
                    if (response.MessageCode == "E") {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.text(response.MessageLabel);
                        return;
                    } else if (response.MessageCode == "A") {
                        alert(response.Message, "Error",
                            function () {
                                parent.window.close();
                            });
                        return;
                    }
                }
            });

        },
        InitDataTable: function (rowsData) {
            var that = this, controls = that.getControls();
            controls.tblAdditionalServicesDetailCommercial.find('tbody').html('');
            controls.tblAdditionalServicesDetailCommercial.dataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                data: rowsData.ListServiceBSCS,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "StrService" },
                    { "data": "StrPackage" },
                    { "data": "StrStatus" }
                ]
            });
        }
    };

    $.fn.AdditionalServicesDetailCommercial = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('AdditionalServicesDetailCommercial'),
                options = $.extend({}, $.fn.AdditionalServicesDetailCommercial.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('AdditionalServicesDetailCommercial', data);
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

    $.fn.AdditionalServicesDetailCommercial.defaults = {
    };

    $('#divBody').AdditionalServicesDetailCommercial();
})(jQuery);