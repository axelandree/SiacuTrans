(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.SearchIMEI.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , txtnumber: $("#txtnumber", $element)
          , tblListadoV: $("#tblListadoV", $element)
          , tblListadoVBody: $("#tblListadoVBody", $element)
          , hid_NRO_IMEI: $("#hid_NRO_IMEI", $element)
          , hid_MARCA_MODELO: $("#hid_MARCA_MODELO", $element)
          , btnSelectionImei: $("#btnSelectionImei", $element)
          , btnCancelImei: $("#btnCancelImei", $element)
          , txtImei: $("#txtImei", $element)
          , txtMarkModel: $("#txtMarkModel", $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                control = this.getControls();
                control.btnSelectionImei.addEvent(that, 'click', that.btnSelectionImei_click);
                control.btnCancelImei.addEvent(that, 'click', that.btnCancelImei_click);
                that.loadSessionData();
                that.render();
        },
        loadSessionData: function () {
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
        },
        render: function () {
            this.getDataTable();
        },
        btnSelectionImei_click: function () {
            var controls = this.getControls();
            if (controls.hid_NRO_IMEI.val().length <= 0 ) {
                alert("Debe seleccionar un elemento");
            } else {
                document.getElementById('txtImei').value = controls.hid_NRO_IMEI.val();
                if (controls.hid_MARCA_MODELO.val() == "null") {
                    controls.hid_MARCA_MODELO.val("");
                }
                document.getElementById('txtMarkModel').value = controls.hid_MARCA_MODELO.val();
                $.window.close();
            }
        },
        getDataTable: function () {
            var controls = this.getControls();
            var params = {
                strIdSession : Session.IDSESSION,
                number : controls.txtnumber.val(),
            };
            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '/Transactions/Common/SearchListIMEI',
                data: JSON.stringify(params),
                success: function (response) {
                    controls.tblListadoV.DataTable({
                "scrollCollapse": true,
                        "info": false,
                        "select": 'single',
                        "paging": false,
                         "searching": false,
                        "destroy": true,
                        "scrollX": true,
                        "scrollY":  "150px",
                        "scrollCollapse": true,
                        "data": response.data,
                        "columns": [
                               { "data": null },
                               { "data": "Date_hour_start" },
                               { "data": "Date_hour_end" },
                               { "data": "Nro_phone" },
                               { "data": "Nro_imei" },
                               { "data": "mark_model" },
                        ],
                        columnDefs : [
                            {
                                targets : 0,
                                render: function (data, type, full, meta) {
                                    return "<input type='radio' NRO_IMEI='" + full.Nro_imei + "' MARk_MODEL='" + full.mark_model + "' name='sel' onclick='f_getDataRoww(\"" + full.Nro_imei +"\",\"" + full.mark_model + "\");'></input>";                                 
                                }
                            }                              
                        ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No se encontraron registros.",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    },
                    });
                },
                error: function (errormessage) {

                    alert('Error', "Alerta");
                }
            });
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },
    };

    $.fn.SearchIMEI = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('SearchIMEI'),
                options = $.extend({}, $.fn.SearchIMEI.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('SearchIMEI', data);
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
    $.fn.SearchIMEI.defaults = {
    }
    $('#SearchImeiContainer').SearchIMEI();

})(jQuery);

function f_getDataRoww(nro_imei, mark_model) {
        document.getElementById('hid_NRO_IMEI').value = "";
        document.getElementById('hid_MARCA_MODELO').value = "";
        document.getElementById('hid_NRO_IMEI').value = nro_imei;
        document.getElementById('hid_MARCA_MODELO').value = mark_model;
}
