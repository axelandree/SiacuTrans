(function ($, undefined) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.PrepaidTFIPlanMigration.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            lblPhoneNumber: $('#lblPhoneNumber', $element),
            lblCustomerType: $('#lblCustomerType', $element),
            lblContactCust: $('#lblContactCust', $element),
            lblCustomer: $('#lblCustomer', $element),
            lblDateActivation: $('#lblDateActivation', $element),
            lblPlan: $('#lblPlan', $element),
            lblDueDate: $('#lblDueDate', $element),
            lblDocument: $('#lblDocument', $element),
            lblStatus: $('#lblStatus', $element),
            ddlNewPlan: $('#ddlNewPlan', $element),
            txtSendMail: $('#txtSendMail', $element),
            chkSendMail: $('#chkSendMail', $element),
            ddlPuntosAtencion: $('#ddlPuntosAtencion', $element),
            txtNotes: $('#txtNotes', $element),
            btnSave: $('#btnSave', $element),
            btnConstancy: $('#btnConstancy', $element),
            btnClose: $('#btnClose', $element),
            lblTitle: $('#lblTitle', $element) 
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.chkSendMail.addEvent(that, 'change', that.btnCheckSendMail_change);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        modelTFIPlanMigration:{},
        render: function () {
            var that = this,
                SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.URLPARAMS = SessionTransac.UrlParams;
            
            that.validateState();
            that.loadSessionData();
            that.getNewPlan();
            that.getPuntosAtencion();
        },

        loadSessionData: function (){
            var that = this,
                controls = this.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE,
                UrlParams = Session.URLPARAMS;

            controls.lblTitle.text("CAMBIO DE PLAN TFI");
      
            controls.lblPhoneNumber.text(UrlParams.TELEFONO);
            controls.lblCustomerType.text(UrlParams.ORIGINAPP);
            controls.lblContactCust.text(oCustomer.Name);
            controls.lblCustomer.text(oCustomer.FullName);
            controls.lblDateActivation.text(oDataService.DateActivation);
            controls.lblPlan.text(oDataService.Plan);
            controls.lblDueDate.text(oDataService.DateExpirationLine);
            controls.lblDocument.text(oCustomer.DNIRUC);
            controls.btnConstancy.prop('disabled', true);
        },

        validateState: function (){
            var that = this,
                controls = this.getControls(),
                strSessionState = Session.DATASERVICE.StateLine;
            objStateLine = { strIdSession: Session.IDSESSION, strSessionState: strSessionState }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateLine),
                url: '/Transactions/Prepaid/TFIPlanMigration/GetValidateState',
                success: function (response) {
                    if (response.data.bValidateState) {
                        controls.lblStatus.text(response.data.strStateLine);
                    }
                    else {
                        controls.lblStatus.text(response.data.strStateLine);
                        alert(response.data.strMessageValidateState, "Alerta", function () { parent.window.close(); });
                return;
            }

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


        getNewPlan: function () {
            var that = this,
                controls = this.getControls(),
                objNewPlan = {
                    strIdSession: Session.IDSESSION,
                    strSuscriber: Session.DATASERVICE.SubscriberStatus,
                    strProveedor: Session.DATASERVICE.ProviderID,
                    strTarifa: Session.DATASERVICE.PlanRate,
                };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objNewPlan),
                url: '/Transactions/Prepaid/TFIPlanMigration/GetPlanTFI',
                success: function (response) {
                    if (response != null) {
                        $.each(response, function (index, value) {
                            controls.ddlNewPlan.append($('<option>', {value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
           
        },


        getPuntosAtencion: function () {
            var that = this, 
                controls = this.getControls(),
                userArea = Session.USERACCESS.areaName,
                objPuntosAtencion = {strIdSession: Session.IDSESSION};
                controls.ddlPuntosAtencion.append($('<option>', { value: '0', html: userArea }));

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPuntosAtencion),
                url: '/Transactions/CommonServices/GetPuntosAtencion',
                success: function (response) {
                    that.createDropdownPuntosAtencion(response);
                }
            });
         
        },

        createDropdownPuntosAtencion: function (response) {
            var that = this,
                controls = that.getControls();

            if (response != null) {
                $.each(response, function (index, value) {
                    controls.ddlPuntosAtencion.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },

        btnCheckSendMail_change: function (sender, arg) {
            var that = this, controls = that.getControls();
            var email = controls.txtSendMail;
            if (controls.chkSendMail.prop('checked')) {
                email.css('display', '');
                email.focus();
            }
            else {
                email.css('display', 'none');
            }
        },
            
        btnSave_click: function () {
            var that = this,
                controls = that.getControls();
            var email = controls.txtSendMail.val();
            
            var strRoutePath;
            if (controls.ddlNewPlan.val() == null) {
                alert('Seleccione el nuevo plan.', "Alerta");
                return;
            }

            if (controls.chkSendMail.prop('checked')) {
                that.modelTFIPlanMigration.bSendMail = 1;
                that.modelTFIPlanMigration.strEmail = email;
                if (!that.ValidateEmail(email)) {
                    return;
                }
            }
            else
            {
                that.modelTFIPlanMigration.bSendMail = 0;
            }
            
            if (controls.ddlPuntosAtencion.val() == null) {
                alert('Seleccione el Punto de Atención.', "Alerta");
                return;
            }
           
            var strValue = controls.ddlNewPlan.val().split('|');
            that.modelTFIPlanMigration.strProvider = strValue[0];
            that.modelTFIPlanMigration.strTariff = strValue[1];
            that.modelTFIPlanMigration.strSuscriber = strValue[2];

            that.modelTFIPlanMigration.strTelephone = Session.URLPARAMS.TELEFONO;
            that.modelTFIPlanMigration.strNote = controls.txtNotes.val();
            that.modelTFIPlanMigration.strPuntoAtencion = $('#ddlPuntosAtencion option:selected').text();
            that.modelTFIPlanMigration.strDocument = Session.DATACUSTOMER.DNIRUC;
            that.modelTFIPlanMigration.strFullName = Session.DATACUSTOMER.FullName;
            that.modelTFIPlanMigration.strLegalAgent = Session.DATACUSTOMER.Name;
            that.modelTFIPlanMigration.strCustomerCode = Session.DATACUSTOMER.CustomerCode;
        
            that.modelTFIPlanMigration.strNewPlanDescription = $('#ddlNewPlan option:selected').text();
            that.modelTFIPlanMigration.IdSession = Session.IDSESSION
            that.modelTFIPlanMigration.strDocumentType = Session.DATACUSTOMER.TypeDocument;
            that.modelTFIPlanMigration.strCurrentUser = Session.USERACCESS.login;
            that.modelTFIPlanMigration.strNameUser = Session.USERACCESS.fullName;
            that.modelTFIPlanMigration.strisTFI = Session.DATASERVICE.IsTFI;
            that.modelTFIPlanMigration.strPlanDescription = Session.DATASERVICE.Plan;
            that.modelTFIPlanMigration.strDateActivation = Session.DATASERVICE.DateActivation;
            that.modelTFIPlanMigration.strStateLine = controls.lblStatus.text();
	    that.Loading();
          
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelTFIPlanMigration),
                url: '/Transactions/TFIPlanMigration/SavePlanMigrationTFI',
          
                success: function (response) {
                    if (response.data.bErrorTransac)
                    {
                        if (response.data.bErrorInteract)
                        {
                            if (response.data.bGeneratedPDF)
                            {
                                that.modelTFIPlanMigration.strRoutePDF = response.data.strRoutePDF;
                                that.modelTFIPlanMigration.bGeneratedPDF = response.data.bGeneratedPDF;

                                controls.btnConstancy.prop("disabled", false);
                                controls.btnSave.prop("disabled", true);
                                controls.txtNotes.prop("disabled", true);
                                controls.ddlNewPlan.prop("disabled", true);
                                controls.ddlPuntosAtencion.prop("disabled", true);
                                controls.chkSendMail.prop("disabled", true);
                                controls.txtSendMail.prop("disabled", true);
                                alert(response.data.strMessageError, 'Información');
                            }
                            else
                            {
                                controls.btnConstancy.prop("disabled", true);
                                controls.btnSave.prop("disabled", true);
                                controls.txtNotes.prop("disabled", true);
                                controls.ddlNewPlan.prop("disabled", true);
                                controls.ddlPuntosAtencion.prop("disabled", true);
                                controls.chkSendMail.prop("disabled", true);
                                controls.txtSendMail.prop("disabled", true);
                                alert('Se presentó un inconveniente en la creación de la Constancia.', 'Alerta');
                            }
                           
                        }
                        else {
                            controls.btnConstancy.prop("disabled", true);
                            controls.btnSave.prop("disabled", true);
                            controls.txtNotes.prop("disabled", true);
                            controls.ddlNewPlan.prop("disabled", true);
                            controls.ddlPuntosAtencion.prop("disabled", true);
                            controls.chkSendMail.prop("disabled", true);
                            controls.txtSendMail.prop("disabled", true);
                            alert('Se presentó un inconveniente en la creación de la Interacción y Constancia.', 'Alerta');
                        }                       
                    }
                    else {
                        that.modelTFIPlanMigration = {};
                        controls.btnConstancy.prop("disabled", true);
                        controls.btnSave.prop("disabled", true);
                        controls.txtNotes.prop("disabled", true);
                        controls.ddlNewPlan.prop("disabled", true);
                        controls.ddlPuntosAtencion.prop("disabled", true);
                        controls.chkSendMail.prop("disabled", true);
                        controls.txtSendMail.prop("disabled", true);
                        alert('Error al guardar el Cambio de Plan TFI','Alerta');
                    }
                   
                }
            });         
        },

        ValidateEmail: function (email) {
            var s = email;

            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/

            if (filter.test(s))
                return true;
            else
                alert('Ingrese email válido.', "Alerta");
            return false;
        },


        btnConstancy_click: function () {
            var that = this,
                controls = that.getControls; 
            var strFilePath = that.modelTFIPlanMigration.strRoutePDF;
            var strIdSession = that.modelTFIPlanMigration.IdSession;
            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '/Transactions/CommonServices/ExistFileSharedFile',
                data: { strFilePath: strFilePath, strIdSession: strIdSession },
                
                success: function (result) {
                   
                    if (result.Exist == false) {
                        alert('No se encuentra el Archivo.', "Alerta");
                    } else {
                        var params = ['height=600',
                                      'width=750',
                                      'resizable=yes',
                                      'location=yes'
                        ].join(',');
                        window.open('/Transactions/CommonServices/ShowRecordSharedFile' + "?strFilePath=" + strFilePath + "&strIdSession=" + strIdSession, "_blank", params);
                    }
                },
                error: function (ex) {
                    alert('Ocurrió un error en la previsualización de la constancia.', "Alerta");
                }
            });
            
        },

        btnClose_click: function () {
            parent.window.close();
           
        },

        Loading: function () {
            var that = this;
            var controls = that.getControls();


            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },

        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
    };
    $.fn.PrepaidTFIPlanMigration = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidTFIPlanMigration'),
                options = $.extend({}, $.fn.PrepaidTFIPlanMigration.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidTFIPlanMigration', data);
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
    $.fn.PrepaidTFIPlanMigration.defaults = {
    }
    $('#divBody').PrepaidTFIPlanMigration();
})(jQuery);