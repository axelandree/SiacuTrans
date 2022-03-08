(function ($, undefined) {


    var Form = function ($element, options) {

        $.extend(this, $.fn.HFCConfigurationIPConstPrint.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element



            , lblAttentionCenter: $("#lblAttentionCenter", $element)
            , lblDate: $('#lblDate', $element)
            , lblBusinessName: $("#lblBusinessName", $element)
            , lblInteration: $("#lblInteration", $element)
            , lblContact: $("#lblContact", $element)
            , lblContract: $("#lblContract", $element)
            , lblDocumentType: $("#lblDocumentType", $element)
            , lblNumberDocument: $("#lblNumberDocument", $element)
            , lblJobType: $("#lblJobType", $element)
            , lblServiceType: $("#lblServiceType", $element)
            , lblMotiveSot: $("#lblMotiveSot", $element)
            , lblInstallationAddress: $("#lblInstallationAddress", $element)
            , lblPhoneReference: $("#lblPhoneReference", $element)
            , lblCommitmentDate: $("#lblCommitmentDate", $element)
            , lblAttachmentsNumber: $("#lblAttachmentsNumber", $element)
            , lblSot: $("#lblSot", $element)

            , lblFidelidad: $("#lblFidelidad", $element)
            , lblMonto: $("#lblMonto", $element)
            , trServicesType: $("#trServicesType", $element)
            , lblClosing: $("#lblClosing", $element)
            , lblMessage: $("#lblMessage", $element)

            , lblTitle: $('#lblTitle', $element)   //Redirect 1.0
        });
    }

    Form.prototype = {
        constructor: Form,

        init: function () {
           
            var that = this,
                controls = this.getControls();
            that.render();
        },

        render: function () {
            var that = this, controls = this.getControls();
            this.f_Load();
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        f_Load: function () {

            var that = this,
             controls = that.getControls(),
             strUrl = '',
             model = {};

            model.strIdSession = Session.IDSESSION;
            model.strTranscacion = CaseId;

            strUrl = "/Transactions/HFC/ConfigurationIP/LoadConstPrint";
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: strUrl,
                error: function (request, status, error) {

                    alert(error,"Alerta");
                },
                success: function (response) {
                    if (response != null) {
                        var data = response.data;
                        if (response.Items[0] == "1") {
                            controls.lblMessage.text("");
                            if (data.ID_INTERACCION != null) {
                                if (data.ID_INTERACCION != "") {

                                    controls.lblBusinessName.text(Session.DATACUSTOMER.BusinessName);
                                    controls.lblContact.text(Session.DATACUSTOMER.CustomerContact);
                                    controls.lblContract.text(Session.DATACUSTOMER.ContractID);
                                    controls.lblAttentionCenter.text(data.X_INTER_15);
                                    controls.lblDocumentType.text(data.X_TYPE_DOCUMENT);
                                    controls.lblNumberDocument.text(data.X_DOCUMENT_NUMBER);
                                    controls.lblInteration.text(data.X_CLARO_NUMBER);
                                    controls.lblDate.text(data.X_MARITAL_STATUS);
                                    controls.lblInteration.text(Session.CASE_ID);
                                    controls.lblPhoneReference.text(data.X_INTER_18);
                                    var strSol = data.X_INTER_29.split("-");
                                    controls.lblInstallationAddress.text(data.X_ADDRESS5 + " " + data.X_DISTRICT);
                                    controls.lblJobType.text(data.X_INTER_17);
                                    if (data.X_INTER_21 != null) {
                                        if (data.X_INTER_21 != "") {
                                            controls.lblServiceType.text(data.X_INTER_21);
                                        }
                                    }
                                    
                              
                                }
                            }

                        }
                        else {
                            controls.lblMessage.text(response.Items[1]);
                        }


                    }
                    controls.lblClosing.text(strClosing);

                }
            });

        },

        strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
            (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))
    };


    $.fn.HFCConfigurationIPConstPrint = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCConfigurationIPConstPrint'),
                options = $.extend({}, $.fn.HFCConfigurationIPConstPrint.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCConfigurationIPConstPrint', data);
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

    $.fn.HFCConfigurationIPConstPrint.defaults = {
    }

    $('#divBody').HFCConfigurationIPConstPrint();
})(jQuery);
