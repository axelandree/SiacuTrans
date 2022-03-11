console.log('ingreso');
var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);
(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {
        $.extend(this, $.fn.RestricSellInfoPromotion.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element
            , ddlSearchType: $('#ddlSearchType', $element)
            , chkSentEmail: $('#chkSentEmail', $element)
            , txtSendforEmail: $('#txtSendforEmail', $element)
            , txtCriteriaValue: $('#txtCriteriaValue', $element)
            , btnBuscar: $('#btnBuscar', $element)
            , txtClientNameTitular: $('#txtClientNameTitular', $element)
            , txtNumberPhoneTitular: $('#txtNumberPhoneTitular', $element)
            , txtTypeDocTitular: $('#txtTypeDocTitular', $element)
            , txtEmailTitular: $('#txtEmailTitular', $element)
            , txtNumberDocTitular: $('#txtNumberDocTitular', $element)
            , rb_voz_permitido: $('#rb_voz_permitido', $element)
            , rb_voz_no_permitido: $('#rb_voz_no_permitido', $element)
            , rb_voz_pendiente: $('#rb_voz_pendiente', $element)
            , rb_sms_permitido: $('#rb_sms_permitido', $element)
            , rb_sms_no_permitido: $('#rb_sms_no_permitido', $element)
            , rb_sms_pendiente: $('#rb_sms_pendiente', $element)
            , rb_correo_permitido: $('#rb_correo_permitido', $element)
            , rb_correo_pendiente: $('#rb_correo_pendiente', $element)
            , rb_correo_no_permitido: $('#rb_correo_no_permitido', $element)
            , lblFechaVoz: $('#lblFechaVoz', $element)
            , lblCanalVoz: $('#lblCanalVoz', $element)
            , lblFechaSMS: $('#lblFechaSMS', $element)
            , lblCanalSMS: $('#lblCanalSMS', $element)
            , lblFechaCorreo: $('#lblFechaCorreo', $element)
            , lblCanalCorreo: $('#lblCanalCorreo', $element)
            , btnSave: $('#btnSave', $element)
            , chkUploadMassive: $('#chkUploadMassive', $element)
            , hdidTipoDocumentoTitular: $('#hdidTipoDocumentoTitular', $element)
            , RowVoz: $('#RowVoz', $element)
            , RowSMS: $('#RowSMS', $element)
            , RowCorreo: $('#RowCorreo', $element)
            , rb_RRLL: $('#rb_RRLL', $element)
            , rb_CA: $('#rb_CA', $element)
            , rb_CP: $('#rb_CP', $element)
            , txtContactName: $('#txtContactName', $element)
            , txtContactNumberDoc: $('#txtContactNumberDoc', $element)
            , txtTypeDocContacto: $('#txtTypeDocContacto', $element)
            , hdidTipoDocumentoContacto: $('#hdidTipoDocumentoContacto', $element)
            , hdidTipoCliente: $('#hdidTipoCliente', $element)
            , hdidTipoOrigen: $('#hdidTipoOrigen', $element)
            , PrePostContacto: $('#PrePostContacto', $element)
            , divCheckMasivo: $('#divCheckMasivo', $element)
            , hdidTipoLinea: $('#hdidTipoLinea', $element)
            , hdCAC: $('#hdCAC', $element)
            , hdidCliente: $('#hdidCliente', $element)
            , hdidContacto: $('#hdidContacto', $element)
            , txtNote: $('#txtNote', $element)            
            , btnConstancia: $('#btnConstancia', $element)
            , columPendiente: $('#columPendiente', $element)
            , columPendientevoz: $('#columPendientevoz', $element)
            , columPendientesms: $('#columPendientesms', $element)
            , columPendientecorreo: $('#columPendientecorreo', $element)
            , btnClose: $('#btnClose', $element)


        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.chkSentEmail.addEvent(that, 'click', that.chkSentEmail_click);
            controls.btnBuscar.addEvent(that, 'click', that.btnBuscar_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.chkUploadMassive.addEvent(that, 'click', that.chkUploadMassive_click);
            controls.rb_RRLL.addEvent(that, 'click', that.chkrb_RRLL_click);
            controls.rb_CA.addEvent(that, 'click', that.chkrb_CA_click);
            controls.rb_CP.addEvent(that, 'click', that.chkrb_CP_click);
            that.disableElements(controls.RowVoz.children());
            that.disableElements(controls.RowSMS.children());
            that.disableElements(controls.RowCorreo.children());          
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
            controls.ddlSearchType.addEvent(this, 'change', this.changeTipoBusqueda);
            that.addEventKeyPress(controls.txtNumberPhoneTitular, 'Phone');
            that.addEventKeyPress(controls.txtEmailTitular, 'Email');

            controls.txtTypeDocTitular.addEvent(this, 'change', that.changeTipoDocumentoTitular);
            controls.txtTypeDocContacto.addEvent(this, 'change', that.changeTipoDocumentoContacto);

            that.getTipoDocumento(controls.txtTypeDocTitular, controls.txtNumberDocTitular);
            that.getTipoDocumento(controls.txtTypeDocContacto, controls.txtContactNumberDoc);

            that.setControlIsNumeric(controls.txtNumberPhoneTitular);
            SetMaxLengthControl(controls.txtNumberPhoneTitular, GetKeyConfig('strLeyPromoLengthMaxNumeroTelefono'));
            var oCustomer = null;

            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
            }
                Lib_IdentificadorCorporativo = GetKeyConfig("strLeyPromoCodCorporativo").split(",");
            that.getCACDAC();
            $("#spanMensajeNoCliente").hide();
            if ($.isEmptyObject(oCustomer) == false) {
                that.searchOptionLoad();
            } else {
                console.log('session oCustomer vacia');
                that.clearDatosTitular();
                that.clearDatosContacto();
                that.clearListaServicio();
            }

            $("#PrePostClientInfo > a").css("background-color", "white");
            $("#PrePostClientInfo > a").css("color", "black");
            $("#lblTitle").text(GetKeyConfig('strLeyPromoTituloVentana'));
            $("#spanMensajeNoCliente").text(GetKeyConfig('strLeyPromoMensajeNoCliente'));

            controls.rb_voz_pendiente.prop("disabled", true);
            controls.rb_sms_pendiente.prop("disabled", true);
            controls.rb_correo_pendiente.prop("disabled", true);

        },
        btnConstancia_click: function () {
            var that = this;
            if (that.strRutaPDF != "") {
                var params = ['height=600',
                              'width=750',
                              'resizable=yes',
                              'location=yes'
                ].join(',');
                window.open('/Transactions/Fixed/RestricSellInfoPromotion/DownloadFileServer' + "?strPath=" + that.strRutaPDF + "&strIdSession=" + Session.IDSESSION, "_blank", params);
            } else {
                alert("El archivo no existe");
            }
        },
        chkrb_RRLL_click: function () {
            var that = this;
            that.loadContacts(Lib_Lista_Tipo_Contacto.RRLL.name);
            that.contactoDeshabilitar(false);
        },
        chkrb_CA_click: function () {
            var that = this;
            that.loadContacts(Lib_Lista_Tipo_Contacto.CA.name);
            that.contactoDeshabilitar(false);
        },
        chkrb_CP_click: function () {
            var that = this;
            that.loadContacts(Lib_Lista_Tipo_Contacto.CP.name);
            that.contactoDeshabilitar(false);
        },

        contactoDeshabilitar: function (sw) {
            $("#txtContactName").prop("disabled", sw);
            $("#txtTypeDocContacto").prop("disabled", sw);
            $("#txtContactNumberDoc").prop("disabled", sw);
        },

        loadContacts: function (TipoContacto) {

            var that = this,
            controls = that.getControls();

            if (($.isEmptyObject(that.ResponsedatosCliente)) == false) {
                if (($.isEmptyObject(that.ResponsedatosCliente.contactos)) == false) {
                    if (that.ResponsedatosCliente.contactos.length > 0) {

                        var Contactos = null;
                        Contactos = ($.map(that.ResponsedatosCliente.contactos, function (data) {
                            if (data.tipoContact == TipoContacto) {
                                return data;
                            }
                        }));

                        if (Contactos.length > 0) {
                            controls.txtContactName.val(Contactos[0].nombresContact);
                            controls.txtContactNumberDoc.val(Contactos[0].nroDocContact);
                            controls.txtTypeDocContacto.val(Contactos[0].tipoDocContact);
                            controls.hdidContacto.val(Contactos[0].contId);
                        } else {
                            controls.txtContactName.val('');
                            controls.txtContactNumberDoc.val('');
                            controls.txtTypeDocContacto.val('-1');
                            controls.hdidContacto.val('0');
                        }

                        console.log('carga de contactos');
                        console.log(Contactos);
                    }
                }
            }
        },
        searchOptionType: {},
        searchOptionLoad: function () {
            var that = this,
            controls = that.getControls();
            debugger;
            

            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

            if ($.isEmptyObject(oCustomer) == false) {

                if (oCustomer.Application == 'HFC') {
                    oCustomer.Telephone = oCustomer.ValueSearch;
                } else {
                    oCustomer.Telephone = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                    (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                    : oCustomer.Telephone);
                }

                showLoading('Cargando...');

                controls.txtCriteriaValue.val(oCustomer.Telephone);

                if(oCustomer.Telephone.length==9)
                controls.ddlSearchType.val(Lib_TipoBusqueda.Linea_Movil);
                else
                    controls.ddlSearchType.val(Lib_TipoBusqueda.Linea_Fija);

                that.searchOptionType = '';

                //siempre correo si es HFC
                that.buscarInfoCentralizada('', oCustomer.Telephone, function () {

                    if (that.searchOptionType == '') {
                        that.clearDatosTitular();
                        that.clearDatosContacto();
                        that.clearListaServicio();
                        controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);
                        that.searchOptionSession();

                    } else {
                        console.log(that.searchOptionType);
                        that.ClienteDeshabilitar(true);
                    }

                    if (controls.rb_RRLL.is(":checked") || controls.rb_CA.is(":checked") || controls.rb_CP.is(":checked")) {
                        that.contactoDeshabilitar(false);
                    } else { that.contactoDeshabilitar(true); }

                    hideLoading();

                });

            } else {

                that.clearDatosTitular();
                that.clearDatosContacto();
                that.clearListaServicio();;
                controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);
            }
        },
        searchOptionClarify: function () {
            var that = this,
            controls = that.getControls();


            //alert(GetKeyConfig('strMensajeNoCliente'), 'Búsqueda');
            that.clearDatosTitular();
            that.clearDatosContacto();
            that.clearListaServicio();

            var strfilterNroTelefono = '';
            var strfilterValue = '';

            var strTipoBusqueda = controls.ddlSearchType.val();

            if (strTipoBusqueda == Lib_TipoBusqueda.Linea_Movil || strTipoBusqueda == Lib_TipoBusqueda.Linea_Fija) {
                strfilterNroTelefono = controls.txtCriteriaValue.val().trim();
            }
            else if (strTipoBusqueda == Lib_TipoBusqueda.Email) {
                strfilterValue = controls.txtCriteriaValue.val().trim();
            }

            // SOLO BUSCARA EN CLARIFY LAS LINEAS
            if (strTipoBusqueda == Lib_TipoBusqueda.Linea_Movil || strTipoBusqueda == Lib_TipoBusqueda.Linea_Fija) {
                that.BuscarClienteClarify(strfilterNroTelefono, function () {
                    if (that.searchOptionType == '') {
                        that.ClienteDeshabilitar(false);
                    } else {
                        that.ClienteDeshabilitar(true);
                    }
                });
            } else {

                that.clearListaServicio();
                that.disableElements(controls.RowVoz.children());
                that.disableElements(controls.RowSMS.children());
                that.disableElements(controls.RowCorreo.children());
            }
        },
        searchOptionSession: function () {
            var that = this,
            controls = that.getControls();
            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

            if ($.isEmptyObject(oCustomer) == false) {

                if (oCustomer.Application == 'HFC') {
                    oCustomer.Telephone = oCustomer.ValueSearch;
                } else {
                    oCustomer.Telephone = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                    (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                    : oCustomer.Telephone);
                }
                
                var strEmail = oCustomer.Email == null ? '' : oCustomer.Email;
                var strTelefono = oCustomer.Telephone == null ? '' : oCustomer.Telephone;

                    that.searchOptionType = 'SE';

                    that.clearDatosTitular();
                    that.clearDatosContacto();
                    that.clearListaServicio();

                  
                that.MostrarTipoClienteCorportativo(oCustomer.Modality + '-' + oCustomer.CustomerType); //CodCustomerType

                    console.log('oCustomer');
                    console.log(oCustomer);

                    controls.hdidTipoOrigen.val(Lib_TipoOrigen.Cliente);
                

                if (oCustomer.Application == 'PREPAID') {
                    controls.txtClientNameTitular.val(oCustomer.FullName);
                    controls.txtNumberDocTitular.val(oCustomer.DNIRUC);
                    controls.txtNumberPhoneTitular.val(oCustomer.Telephone);
                    controls.txtEmailTitular.val(oCustomer.Email);
                    controls.hdidTipoLinea.val(oCustomer.ProductType);
                    controls.hdidTipoCliente.val(oCustomer.Modality);
                } else {
                    controls.txtClientNameTitular.val(oCustomer.BusinessName);
                    controls.txtNumberDocTitular.val(oCustomer.DNIRUC);
                    controls.txtNumberPhoneTitular.val(oCustomer.Telephone);
                    controls.txtEmailTitular.val(oCustomer.Email);
                    controls.hdidTipoLinea.val(oCustomer.ProductTypeText);
                    controls.hdidTipoCliente.val(oCustomer.Modality + '-' + oCustomer.CustomerType);
                }



                if (oCustomer.DNIRUC.length == 11) {
                    controls.txtTypeDocTitular.val(6);
                    oCustomer.DocumentType = "RUC";
                } else if (oCustomer.DNIRUC.length == 8) {
                    controls.txtTypeDocTitular.val(2);
                    oCustomer.DocumentType = "DNI";
                } else {
                    oCustomer.DocumentType = oCustomer.TypeDocument;
                }


                    var codigoTipoDocumento = null;

                    if (oCustomer.DocumentType != null) {
                        codigoTipoDocumento = ($.map(Lib_Lista_Tipo_Documento, function (data) {
                            if (data.name.toUpperCase().trim() == oCustomer.DocumentType.toUpperCase().trim()) {
                                return data;
                            }
                        }));
                    }
                    if (codigoTipoDocumento != null && codigoTipoDocumento.length > 0) {
                    controls.txtTypeDocTitular.val(codigoTipoDocumento[0].id);
                        that.setMaxLengthTipoDocumento(codigoTipoDocumento[0].id, controls.txtNumberDocTitular);
                    } else {
                    controls.txtTypeDocTitular.val('-1');
                    }


                    if (controls.txtNumberPhoneTitular.val().trim().length > 0) {
                        that.enableElements(controls.RowSMS.children());
                        that.enableElements(controls.RowVoz.children());
                    } else {
                        that.disableElements(controls.RowVoz.children());
                        that.disableElements(controls.RowSMS.children());
                    }

                    if (controls.txtEmailTitular.val().trim().length > 0) {
                        that.enableElements(controls.RowCorreo.children());
                        $(controls.chkSentEmail).prop("checked", true);
                        controls.txtSendforEmail.val(controls.txtEmailTitular.val().trim());
                        controls.txtSendforEmail.attr('disabled', false);
                    } else {
                        that.disableElements(controls.RowCorreo.children());
                        $(controls.chkSentEmail).prop("checked", false);
                        controls.txtSendforEmail.attr('disabled', true);
                        controls.txtSendforEmail.val('');
                    }

                

                that.ClienteDeshabilitar(true);

                } else {
                that.ClienteDeshabilitar(false);
            }
        },
        searchOptionSiacUnico: function () {
            var that = this,
            controls = that.getControls();

            var strTelefono = controls.txtCriteriaValue.val().trim()

            var objNode = {
                strIdSession: Session.IDSESSION,
                strSearchType: 1,
                strSearchValue: strTelefono,
                NotEvalState: false,
                FlagSearchType: true,
                userId: SessionTransac.SessionParams.USERACCESS.userId,
                IsPrepaid: false,
                IsPostPaid: false
            };
            debugger;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Home/ValidateQuery',
                data: JSON.stringify(objNode),
                complete: function () {
                    //callback();
                },
                success: function (response) {
                    console.log(response);

                    var oCustomer = response.data;

                    if ($.isEmptyObject(oCustomer) == false) {

                        that.searchOptionType = 'SU';

                    that.clearDatosTitular();
                    that.clearDatosContacto();
                    that.clearListaServicio();
                        SessionTransac.SessionParams.DATACUSTOMER = oCustomer;
                        that.MostrarTipoClienteCorportativo(oCustomer.Modality + '-' + oCustomer.CustomerType);

                        console.log('oCustomer');
                        console.log(oCustomer);

                        controls.hdidTipoOrigen.val(Lib_TipoOrigen.Cliente);


                        if (oCustomer.Application == 'HFC' || oCustomer.Application == 'LTE') {
                            oCustomer.Telephone = oCustomer.ValueSearch;
                        } else {
                            oCustomer.Telephone = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                            (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                            : oCustomer.Telephone);
                        }

                        if (oCustomer.Application == 'PREPAID') {
                            controls.txtClientNameTitular.val(oCustomer.FullName);
                            controls.txtNumberDocTitular.val(oCustomer.DNIRUC);
                            controls.txtNumberPhoneTitular.val(oCustomer.Telephone);
                            controls.txtEmailTitular.val(oCustomer.Email);
                            controls.hdidTipoLinea.val(oCustomer.ProductType);
                            controls.hdidTipoCliente.val(oCustomer.Modality);
                        } else {
                            controls.txtClientNameTitular.val(oCustomer.BusinessName);
                            controls.txtNumberDocTitular.val(oCustomer.DNIRUC);
                            controls.txtNumberPhoneTitular.val(oCustomer.Telephone);
                            controls.txtEmailTitular.val(oCustomer.Email);
                            controls.hdidTipoLinea.val(oCustomer.ProductTypeText);
                            controls.hdidTipoCliente.val(oCustomer.Modality + '-' + oCustomer.CustomerType);
                }

                        if (oCustomer.DNIRUC.length == 11) {
                            controls.txtTypeDocTitular.val(6);
                            oCustomer.DocumentType = "RUC";
                        } else if (oCustomer.DNIRUC.length == 8) {
                            controls.txtTypeDocTitular.val(2);
                            oCustomer.DocumentType = "DNI";
                        } else {
                            oCustomer.DocumentType = oCustomer.TypeDocument;
                        }


                        var codigoTipoDocumento = null;

                        if (oCustomer.DocumentType != null) {
                            codigoTipoDocumento = ($.map(Lib_Lista_Tipo_Documento, function (data) {
                                if (data.name.toUpperCase().trim() == oCustomer.DocumentType.toUpperCase().trim()) {
                                    return data;
                                }
                            }));
                        }

                        if (codigoTipoDocumento != null && codigoTipoDocumento.length > 0) {
                            controls.txtTypeDocTitular.val(codigoTipoDocumento[0].id);
                            that.setMaxLengthTipoDocumento(codigoTipoDocumento[0].id, controls.txtNumberDocTitular);
                        } else {
                            controls.txtTypeDocTitular.val('-1');
                        }

                        if (controls.txtNumberPhoneTitular.val().trim().length > 0) {
                            that.enableElements(controls.RowSMS.children());
                            that.enableElements(controls.RowVoz.children());
                        } else {
                            that.disableElements(controls.RowVoz.children());
                            that.disableElements(controls.RowSMS.children());
                        }

                        if (controls.txtEmailTitular.val().trim().length > 0) {
                            that.enableElements(controls.RowCorreo.children());
                            $(controls.chkSentEmail).prop("checked", true);
                            controls.txtSendforEmail.val(controls.txtEmailTitular.val().trim());
                            controls.txtSendforEmail.attr('disabled', false);
                        } else {
                            that.disableElements(controls.RowCorreo.children());
                            $(controls.chkSentEmail).prop("checked", false);
                            controls.txtSendforEmail.attr('disabled', true);
                            controls.txtSendforEmail.val('');
            }


                        that.ClienteDeshabilitar(true);

                    } else {
                        that.ClienteDeshabilitar(false);
                    }

                },
                error: function (msger) {
                    console.log(msger);
                }
            });

        },
        addEventKeyPress: function (Control, Option) {
            var that = this,
            controls = that.getControls();

            $(Control).keyup(function (event) {
                var filterNumber = controls.txtNumberPhoneTitular.val().trim().length;
                var filterEmail = controls.txtEmailTitular.val().trim().length;

                switch (Option) {
                    case 'Phone':
                        if (filterNumber > 0) {
                            that.enableElements(controls.RowVoz.children());
                            that.enableElements(controls.RowSMS.children());
                        } else {
                            that.disableElements(controls.RowVoz.children());
                            that.disableElements(controls.RowSMS.children());
                        }

                        break;
                    case 'Email':
                        if (that.isEmail(controls.txtEmailTitular.val())) {
                        if (filterEmail > 0) {
                            that.enableElements(controls.RowCorreo.children());
                        } else {
                            that.disableElements(controls.RowCorreo.children());
                        }
                        } else { that.disableElements(controls.RowCorreo.children()); }
                        break;
                }

                return true;
            });

        },
        isEmail: function (email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        },
        disableElements: function (el) {
            var that = this;
            for (var i = 0; i < el.length; i++) {
                el[i].disabled = true;
                that.disableElements(el[i].children);
            }
        },
        enableElements: function (el) {
            var that = this;
            for (var i = 0; i < el.length; i++) {
                el[i].disabled = false;

                that.enableElements(el[i].children);
            }
        },

        changeTipoDocumentoTitular: function () {
            var that = this, controls = that.getControls();
            that.setMaxLengthTipoDocumento(controls.txtTypeDocTitular.val(), controls.txtNumberDocTitular);
        },
        changeTipoDocumentoContacto: function () {
            var that = this, controls = that.getControls();
            that.setMaxLengthTipoDocumento(controls.txtTypeDocContacto.val(), controls.txtContactNumberDoc);
        },
        getTipoDocumento: function (ctrlSelect, ctrlTexto) {
            var that = this;

            that.Array_ListaTipoDocumento.TipoDocumento = [];

            $.each(Lib_Lista_Tipo_Documento, function (key, value) {

                var TipoDocumento = {
                    lbl: '',
                    codigo: ''
                }

                TipoDocumento.lbl = value.name;
                TipoDocumento.codigo = value.id;
                that.Array_ListaTipoDocumento.TipoDocumento.push(TipoDocumento);
            });

            $.each(that.Array_ListaTipoDocumento.TipoDocumento, function (i, item) {
                ctrlSelect.append($('<option>', {
                    value: item.codigo,
                    text: item.lbl
                }));
            });

        },
        getAutocompleteTipoDocumento: function (controlTipoDocumento, Hiddenvalue, controlNumeroDocumento) {
            var that = this;

            $.each(items, function (i, item) {
                controlTipoDocumento.append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            });

            controlNumeroDocumento.val('');
            Hiddenvalue.val('0');


            controlTipoDocumento.autocomplete({
                source: function (request, response) {

                    controlNumeroDocumento.val('');
                    Hiddenvalue.val('0');

                    that.Array_ListaTipoDocumento.TipoDocumento = [];

                    $.each(Lib_Lista_Tipo_Documento, function (key, value) {

                        var TipoDocumento = {
                            lbl: '',
                            codigo: ''
                        }

                        TipoDocumento.lbl = value.name;
                        TipoDocumento.codigo = value.id;
                        that.Array_ListaTipoDocumento.TipoDocumento.push(TipoDocumento);
                    });

                    response($.map(that.Array_ListaTipoDocumento.TipoDocumento, function (data) {
                        return {
                            label: data.lbl,
                            value: data.codigo
                        };

                    }));
                },
                minLength: 1,
                select: function (event, ui) {
                    controlTipoDocumento.val(ui.item.label);
                    Hiddenvalue.val(ui.item.value);

                    that.setMaxLengthTipoDocumento(ui.item.value, controlNumeroDocumento);

                    return false;
                }
            });
        },
        changeTipoBusqueda: function () {
            var that = this,
            controls = that.getControls();
            controls.txtCriteriaValue.val("");

            var strTipoBusqueda = controls.ddlSearchType.val();

            if (strTipoBusqueda == Lib_TipoBusqueda.Seleccionar) {
                $(controls.txtCriteriaValue).off("keypress");
                SetMaxLengthControl(controls.txtCriteriaValue, GetKeyConfig('strLeyPromomaxLengthEmail'));
            } else if (strTipoBusqueda == Lib_TipoBusqueda.Linea_Movil) {
                SetMaxLengthControl(controls.txtCriteriaValue, GetKeyConfig('strLeyPromomaxLengthMovil'));
                $(controls.txtCriteriaValue).keypress(function (event) {
                    return InNumeric(event);
                });
            } else if (strTipoBusqueda == Lib_TipoBusqueda.Linea_Fija) {
                SetMaxLengthControl(controls.txtCriteriaValue, GetKeyConfig('strLeyPromomaxLengthFijo'));
                $(controls.txtCriteriaValue).keypress(function (event) {
                    return InNumeric(event);
                });
            }
            else if (strTipoBusqueda == Lib_TipoBusqueda.Email) {
                SetMaxLengthControl(controls.txtCriteriaValue, GetKeyConfig('strLeyPromomaxLengthEmail'));
                $(controls.txtCriteriaValue).off("keypress");
            }
            //controls.ddlSearchType.selectpicker('refresh');
        },
        chkSentEmail_click: function () {
            console.log('click');
            var that = this,
            controls = that.getControls();

            if ($(controls.chkSentEmail).is(':checked')) {
                controls.txtSendforEmail.attr('disabled', false);
                controls.txtSendforEmail.val('');
            } else {
                controls.txtSendforEmail.attr('disabled', true);
                controls.txtSendforEmail.val('');
            }
        },
        chkUploadMassive_click: function () {

            console.log('click');
            var that = this,
            controls = that.getControls();

            if ($(controls.chkUploadMassive).is(':checked')) {

                var NumeroDocumento = controls.txtNumberDocTitular.val().trim();
                var telefono = controls.txtNumberPhoneTitular.val().trim();

                $.window.open({
                    modal: true,
                    title: "Confirmación carga Masiva",
                    id: 'divmodal',
                    url: '/Transactions/Fixed/RestricSellInfoPromotion/UploadInfoProm',
                    data: { strNumeroDocumento: NumeroDocumento, strTelefono: telefono },
                    type: 'POST',
                    width: '750px',
                    height: '350px',
                    minimizeBox: false,
                    maximizeBox: false,
                    buttons: {
                        Grabar: {
                            id: 'btnAgregarMaintenanceDiscardMatricesTree',
                            data: { 'loading-text': "<i class='fa fa-circle-o-notch fa-spin'></i> Procesando" },
                            click: function (sender, args) {
                                var params = { sender: sender, args: args };
                                $('#ContentRestricSellInfoPromotionUploadInfoProm').RestricSellInfoPromotionUploadInfoProm('btnGrabar_click', params)
                            }
                        },
                        Cerrar: {
                            id: 'btnCerrarUploadInfoProm',
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });

            } else {
                SessionTransac.LeyPromoListaLineas = [];
            }
        },
        validateRegisterForm: function (callback) {
            var that = this,
           controls = this.getControls();
            var Mensaje = '';

            var flag = false;

            var idTipoDocumentoTitular = controls.txtTypeDocTitular.val().trim();
            var TipoDocumentoTitular = $("#txtTypeDocTitular option:selected").text().trim();
            var NombreTitular = controls.txtClientNameTitular.val().trim();
            var TelefonoTitular = controls.txtNumberPhoneTitular.val().trim();
            var NumeroDocumentoTitular = controls.txtNumberDocTitular.val().trim();
            var EmailTitular = controls.txtEmailTitular.val().trim();
            
            var LengthMaxNumeroTelefono = GetKeyConfig('strLeyPromoLengthMaxNumeroTelefono');
            var LengthMinNumeroTelefono = GetKeyConfig('strLeyPromoLengthMinNumeroTelefono');

            if (controls.hdidTipoOrigen.val() == Lib_TipoOrigen.No_Cliente) {
                if (TelefonoTitular == '' && EmailTitular == '') {
                    Mensaje = 'Ingresar el Nro. Teléfono o Correo. ';
                    flag = true;
                }
            }
            if (Mensaje == '') {
            if (NombreTitular == '') {
                Mensaje = 'Debe ingresar el nombre cliente. ';
                flag = true;
            } else if (TelefonoTitular.length != 0 && (TelefonoTitular.length > LengthMaxNumeroTelefono || TelefonoTitular.length < LengthMinNumeroTelefono)) {
                Mensaje = 'La cantidad de digitos para el numero de telefono es incorrecto';
                flag = true;
            } else if (idTipoDocumentoTitular == '0' || TipoDocumentoTitular == '') {
                Mensaje = 'Debe ingresar el tipo de documento del cliente. ';
                flag = true;
            } else if (NumeroDocumentoTitular == '') {
                Mensaje = 'Debe ingresar el numero de documento del cliente. ';
                flag = true;
                } else if ((!$(controls.rb_sms_pendiente).is(':disabled') && $(controls.rb_sms_pendiente).is(':checked') && controls.txtNumberPhoneTitular.val().trim() != '') ||
                    (!$(controls.rb_voz_pendiente).is(':disabled') && $(controls.rb_voz_pendiente).is(':checked') && controls.txtNumberPhoneTitular.val().trim() != '') ||
                    (!$(controls.rb_correo_pendiente).is(':disabled') && $(controls.rb_correo_pendiente).is(':checked') && controls.txtEmailTitular.val().trim() != '')) {
                Mensaje = 'Seleccionar todas las opciones a permitido/no permitido ';
                flag = true;
            } else if (controls.chkSentEmail.is(":checked")) {
                    if (controls.txtSendforEmail.val().trim() == '') {
                    Mensaje = 'Ingrese el dato del Correo ';
                    flag = true;
                }
            }
         }




            debugger;
            if (!flag) {
                that.ValidarMinMaxLengthTipoDocumento(idTipoDocumentoTitular, NumeroDocumentoTitular.length, function (MensajeCallBack) {
                    Mensaje = MensajeCallBack;
                    if (Mensaje == '') {
                        if (that.SessionTipoCliente.IsMasivo == '1') {

                            var idTipoDocumentoContacto = controls.txtTypeDocContacto.val();
                            var TipoDocumentoContacto = $("#txtTypeDocContacto option:selected").text().trim();

                            var NombreContacto = controls.txtContactName.val().trim();
                            var NumeroDocumentoContacto = controls.txtContactNumberDoc.val().trim();

                            if (NombreContacto == '') {
                                Mensaje = 'Debe ingresar el nombre del contacto. ';
                                flag = true;
                            } else if (idTipoDocumentoContacto == '0' || TipoDocumentoContacto == '') {
                                Mensaje = 'Debe ingresar el tipo de documento del contacto. ';
                                flag = true;
                            } else if (NumeroDocumentoContacto == '') {
                                Mensaje = 'Debe ingresar el numero de documento del contacto. ';
                                flag = true;
                            }

                            if (!flag) {
                                that.ValidarMinMaxLengthTipoDocumento(idTipoDocumentoContacto, NumeroDocumentoContacto.length, function (MensajeCallBack) {
                                    console.log(MensajeCallBack);
                                });
                            }
                        }
                    }
                });
            }


            if (Mensaje != '') {
                flag = true;

                alert(Mensaje, 'Registrar');
            }
            callback(flag);
        },
        validateSearch: function () {
            var that = this,
            controls = this.getControls();
            var Mensaje = '';

            var filterTipoBusqueda = controls.ddlSearchType.val();
            var filtertxtCriteriaValue = controls.txtCriteriaValue.val().trim();

            var flag = false;

            if (filterTipoBusqueda == Lib_TipoBusqueda.Seleccionar) {
                Mensaje = 'Debe seleccionar un tipo de búsqueda';
                flag = true;
            } else if (filterTipoBusqueda == Lib_TipoBusqueda.Linea_Movil) {

                if (filtertxtCriteriaValue.length != GetKeyConfig('strLeyPromomaxLengthMovil')) {
                    Mensaje = GetKeyConfig('strLeyPromoMensajeMovilInvalido');
                    flag = true;
                }

            } else if (filterTipoBusqueda == Lib_TipoBusqueda.Linea_Fija) {
                if (filtertxtCriteriaValue.length != GetKeyConfig('strLeyPromomaxLengthFijo')) {
                    Mensaje = GetKeyConfig('strLeyPromoMensajeFijoInvalido');
                    flag = true;
                }
            }
            else if (filterTipoBusqueda == Lib_TipoBusqueda.Email) {

                if (filtertxtCriteriaValue.length == 0) {
                    Mensaje = GetKeyConfig('strLeyPromoMensajeEmailInvalido');
                    flag = true;
                }
            }

            if (Mensaje != '') {
                alert(Mensaje, 'Buscar');
            }

            return flag;
        },
        btnBuscar_click: function (sender, args) {
            var that = this,
            controls = that.getControls();

            if (that.validateSearch()) {
                return false;
            }

            var strTipoBusqueda = controls.ddlSearchType.val();
            var strfilterValue = controls.txtCriteriaValue.val().trim();

            that.searchOptionType = '';

            showLoading('Cargando...');

            if (strTipoBusqueda == Lib_TipoBusqueda.Email) { // si es por email solo busca en la centralizada es considerado no cliente

                controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);

                that.buscarInfoCentralizada(strfilterValue, '', function () {

                    if (that.searchOptionType == '') {
                        that.clearDatosTitular();
                        that.clearDatosContacto();
                        that.clearListaServicio();
                        controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);
                        $("#spanMensajeNoCliente").show();
                        that.ClienteDeshabilitar(false);
                        alert(GetKeyConfig('strLeyPromoMensajeNoClienteBuscar'));
                    } else {
                        console.log(that.searchOptionType);
                    }

                    if (controls.rb_RRLL.is(":checked") || controls.rb_CA.is(":checked") || controls.rb_CP.is(":checked")) {
                        that.contactoDeshabilitar(false);
                    } else { that.contactoDeshabilitar(true); }

                    hideLoading();

                });

            } else {



                that.buscarInfoCentralizada('', strfilterValue, function () {

                    if (that.searchOptionType == '') {
                        that.searchOptionSiacUnico();
                        if (that.searchOptionType == '') {
                            that.searchOptionClarify();
                        }
                    }

                    if (that.searchOptionType == '') {
                        that.clearDatosTitular();
                        that.clearDatosContacto();
                        that.clearListaServicio();
                        controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);
                        that.ClienteDeshabilitar(false);
                        $("#spanMensajeNoCliente").show();
                        alert(GetKeyConfig('strLeyPromoMensajeNoClienteBuscar'), 'Alerta');
                    } else {
                        console.log(that.searchOptionType);
                    }


                    if (controls.rb_RRLL.is(":checked") || controls.rb_CA.is(":checked") || controls.rb_CP.is(":checked")) {
                        that.contactoDeshabilitar(false);
                    } else { that.contactoDeshabilitar(true); }

                    hideLoading();

                });
            }

        },
        btnSave_click: function (sender, args) {

            var that = this;

            if (that.validateRegisterForm(function (flag) {

                if (flag) {
                return false;
            }

                that.Grabar();

            }));

        },
        GenerarAuditoria: function () {
            var that = this, controls = that.getControls();

            var objNodeRequest = {
                strIdSession: Session.IDSESSION,
                strTelephone: controls.txtNumberPhoneTitular.val().trim(),
                strUsuarioNombre: SessionTransac.SessionParams.USERACCESS.fullName,
                strUsuarioLogin: SessionTransac.SessionParams.USERACCESS.login
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/InsertAuditory',
                data: JSON.stringify(objNodeRequest),
                complete: function () {
                    console.log("Termino registro de auditoria");
                },
                success: function (response) {
                    if (response.codigoRespuesta != 0) {
                        alert('No se genero el registro de la auditoria \n Error: ' + response.mensajeRespuesta, 'Registrar Auditoria');
                    }
                },
                error: function (msger) {
                    alert('No se genero el registro de la auditoria, error desconocido', 'Registrar Auditoria');
                }
            });

        },        
        Grabar: function () {

            var that = this,
            controls = that.getControls();

            var Idinteraccion = "1"; 
            var LineaTelefono = controls.txtNumberPhoneTitular.val().trim();
            var swSinCambio = false;


                    if (Idinteraccion != '0' && Idinteraccion != '' && Idinteraccion != null) {

                        console.log('codigo de interaccion ' + Idinteraccion);

                        //REINICIAR VALORES
                        that.RequestUpdateStateLineEmail.tipoOperacion = '';
                        that.RequestUpdateStateLineEmail.listaServicios = [];
                        that.RequestUpdateStateLineEmail.origenFuente = '';
                        that.RequestUpdateStateLineEmail.usuario = '';
                        that.RequestUpdateStateLineEmail.fechaRegistro = '';

                if (!$("#rb_voz_permitido").is(":disabled"))
                        that.ConstruirRequestListaServicio(Lib_ListaServicio.VOZ, LineaTelefono, Idinteraccion, false);
                if (!$("#rb_sms_permitido").is(":disabled"))
                        that.ConstruirRequestListaServicio(Lib_ListaServicio.SMS, LineaTelefono, Idinteraccion, false);
                if (!$("#rb_correo_permitido").is(":disabled"))
                        that.ConstruirRequestListaServicio(Lib_ListaServicio.CORREO, LineaTelefono, Idinteraccion, false);

                       //validar si se hara registro masivo 
                       debugger;

                        if (($.isEmptyObject(that.RequestUpdateStateLineEmail)) == false) {
                            if (($.isEmptyObject(that.RequestUpdateStateLineEmail.listaServicios)) == false) {

                        that.StrMensajeConstancia.MensajeError = "";

                        if (that.RequestUpdateStateLineEmail.listaServicios.length == 0) {
                            alert('Debe eligir al menos un servicio a modificar', 'Registrar');
                            return;
                        }

                        showLoading('Cargando...');

                        confirm('¿Desea realizar el registro de Envío/No Envío de Publicidad?', 'Confirmar', function (result) {
                            if (result == true) {

                                setTimeout(function () {


                                if (!that.validarCambioServicio(that.RequestUpdateStateLineEmail.listaServicios)) {
                                    swSinCambio = true;
                                }

                                if (!swSinCambio) {
                                    if (controls.hdidTipoOrigen.val() == Lib_TipoOrigen.No_Cliente) {
                                            if (that.searchOptionType == 'SW') {
                                                Idinteraccion = that.GenerarInteraccion();
                                            } else {
                                        if (that.GenerarNoCliente()) Idinteraccion = that.GenerarInteraccion(); else Idinteraccion = '0';
                                            }
                                    } else {
                                        Idinteraccion = that.GenerarInteraccion();
                                    }
                                }


                                console.log('codigo de interaccion ' + Idinteraccion);

                                if (Idinteraccion == '0') {
                                    return;
                                }

                                for (var i = 0; i < that.RequestUpdateStateLineEmail.listaServicios.length; i++) {
                                    that.RequestUpdateStateLineEmail.listaServicios[i].interactId = Idinteraccion;
                                }

                                var objRequestOnly = jQuery.extend(true, {}, that.RequestUpdateStateLineEmail);

                                var strTipoCarga = that.ConstruirRequestListaServicioMasivo(Idinteraccion);
                                var objNodeRequest;
                                var objRequestOnlyMasive;

                                    if (strTipoCarga == Lib_TipoCargaMasiva.ArchivoAdjunto || strTipoCarga == Lib_TipoCargaMasiva.Todos) {

                                    objRequestOnlyMasive = jQuery.extend(true, {}, that.RequestUpdateStateLineEmail);
                                    var objRequestOnlyMasiveList;
                                    objRequestOnlyMasiveList = $.map(that.RequestUpdateStateLineEmail.listaServicios, function (obj) {
                                        if (obj.IsMasive)
                                            return obj;
                                    });

                                    objRequestOnlyMasive.listaServicios = objRequestOnlyMasiveList;
                                    
                                    objNodeRequest = {
                                        strIdSession: Session.IDSESSION,
                                        MessageRequest: {
                                            Body: objRequestOnly
                                        }
                                    };

                                } else if (strTipoCarga == Lib_TipoCargaMasiva.Todos) {

                                    objNodeRequest = {
                                        strIdSession: Session.IDSESSION,
                                        MessageRequest: {
                                            Body: that.RequestUpdateStateLineEmail
                                        }
                                    };

                                }
                                else if (strTipoCarga == Lib_TipoCargaMasiva.Ninguno) {

                                    objNodeRequest = {
                                        strIdSession: Session.IDSESSION,
                                        MessageRequest: {
                                            Body: that.RequestUpdateStateLineEmail
                                        }
                                    };

                                }

                                console.log(that.RequestUpdateStateLineEmail);

                                console.log(JSON.stringify(objNodeRequest));

                                $.app.ajax({
                                    type: 'POST',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    async: false,
                                    url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/UpdateStateLineEmail',
                                    data: JSON.stringify(objNodeRequest),
                                    complete: function () {
                                        console.log("Finish UpdateStateLineEmail");
                                        hideLoading();
                                    },
                                    success: function (response) {
                                        
                                        var Respuesta = '';
                                        if (response.data.codigoRespuesta != null) {
                                            if (response.data.codigoRespuesta == 0) {

                                                that.GenerarAuditoria();
                                               
                                                if (!swSinCambio) {
                                                that.GenerarConstancia(objNodeRequest);
                                                }

                                                if (that.StrMensajeConstancia.MensajeError != "") {
                                                    alert('Se registro correctamente la InfoPromociones. \n' + that.StrMensajeConstancia.MensajeError, 'Registrar');
                                                } else {
                                                    alert('Se registro correctamente la InfoPromociones.','Registrar');
                                                }

                                                    if (strTipoCarga == Lib_TipoCargaMasiva.ArchivoAdjunto || strTipoCarga == Lib_TipoCargaMasiva.Todos) {
                                                    that.GenerarArchivoCSV(objRequestOnlyMasive);
                                                }

                                                $("#btnSave").prop("disabled", true);
                                            } else {
                                                alert('Error en el servicio actualizar estado linea', 'Registrar');
                                            }
                                        }
                                        else {
                                            alert('Error en el servicio actualizar estado linea', 'Registrar');
                                        }
                                    },
                                    error: function (msger) {
                                        alert('Error en el servicio actualizar estado linea', 'Registrar');
                                    }
                                });

                                }, 100);

                            }
                        }, function () {
                                hideLoading();
                        });

                    } else {
                        alert('Debe eligir al menos un servicio a modificar', 'Registrar');
                        }
                    } else {
                    alert('Debe eligir al menos un servicio a modificar', 'Registrar');
                    }
            } else {
                console.log('codigo de interaccion ' + Idinteraccion);
                }


        },
        AccionEjecutada: function (a, b, c) {

            var habilitaR_SERVICIO = $.map(a, function (data) {
                if (data.estadoInfo == Lib_EstadoServicio.Permitido && !data.IsMasive) {
                    return data;
                }
            });

            var restringiR_SERVICIO = $.map(a, function (data) {
                if (data.estadoInfo == Lib_EstadoServicio.No_Permitido && !data.IsMasive) {
                    return data;
                }
            });

            for (var i in habilitaR_SERVICIO) {
                var t = (habilitaR_SERVICIO[i].servId == Lib_ListaServicio.VOZ ? 'VOZ' : (habilitaR_SERVICIO[i].servId == Lib_ListaServicio.SMS ? 'SMS' : (habilitaR_SERVICIO[i].servId == Lib_ListaServicio.CORREO ? 'EMAIL' : '')));
                b.push(t);
            }

            for (var i in restringiR_SERVICIO) {
                var t = (restringiR_SERVICIO[i].servId == Lib_ListaServicio.VOZ ? 'VOZ' : (restringiR_SERVICIO[i].servId == Lib_ListaServicio.SMS ? 'SMS' : (restringiR_SERVICIO[i].servId == Lib_ListaServicio.CORREO ? 'EMAIL' : '')));
                c.push(t);
            }

            b.sort();
            c.sort();

        },

        getTipoCliente: function (objServicio) {
            var that = this, controls = that.getControls();
            if (that.SessionTipoCliente.IsMasivo == '1') {
                try {
                    if (objServicio.nroDoc.substr(0, 2) == '10') return 'Juridico'; else return 'Corporativo';
                } catch (e) {
                    return 'Corporativo';
                }
            }
            else {
                if (controls.hdidTipoOrigen.val() == Lib_TipoOrigen.No_Cliente) {
                    return 'No Cliente';
                } else { return objServicio.tipoCliente; }
            }
        },
        getTipoClienteSiac: function (tipo) {
            if (tipo == 'Juridico') return 'Corporativo';
            else if (tipo == 'No Cliente') return tipo;
            else if (tipo != 'Corporativo') return 'Masivo';
            else return tipo;
        },
        GenerarConstancia: function (objNodeRequest) {
            var that = this, controls = that.getControls();

            var h = [];
            var hh = [];

            that.AccionEjecutada(objNodeRequest.MessageRequest.Body.listaServicios, h, hh);

            if (that.SessionTipoCliente.IsMasivo == '1') {
                var sw = true;
                for (var i = 0; i < SessionTransac.LeyPromoListaLineas.length; i++) {
                    if (SessionTransac.LeyPromoListaLineas[i] == objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn) {
                        sw = false;
                    }
                }
                if (sw) {
                    SessionTransac.LeyPromoListaLineas.push(objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn);
                }
            }

            var oOnBaseCargaModel = {
                idSession: objNodeRequest.strIdSession,
                codigoAsesor: SessionTransac.SessionParams.USERACCESS.login,
                nombreCliente: objNodeRequest.MessageRequest.Body.listaServicios[0].nombApell,
                canal: objNodeRequest.MessageRequest.Body.listaServicios[0].desContactCanal,
                tipoDocumento: objNodeRequest.MessageRequest.Body.listaServicios[0].tipoDocDes,
                NumeroLinea: objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn == '' ? '0' : objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn,
                numeroDocumento: objNodeRequest.MessageRequest.Body.listaServicios[0].nroDoc,
                tipoCliente: that.getTipoCliente(objNodeRequest.MessageRequest.Body.listaServicios[0]),
                Constancia: {
                    tipoDocDescContact: objNodeRequest.MessageRequest.Body.listaServicios[0].tipoDocDescContact,
                    nroDocContact: objNodeRequest.MessageRequest.Body.listaServicios[0].nroDocContact,
                    nombresContact: objNodeRequest.MessageRequest.Body.listaServicios[0].nombresContact,
                    msisdn: objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn,
                    canaL_ATENCION: objNodeRequest.MessageRequest.Body.listaServicios[0].desContactCanal,
                    titulaR_CLIENTE: objNodeRequest.MessageRequest.Body.listaServicios[0].nombApell,
                    nrO_SERVICIO: objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn,
                    tipO_DOC_IDENTIDAD: objNodeRequest.MessageRequest.Body.listaServicios[0].tipoDocDes,
                    fechA_SOLICITUD: '',
                    casO_INTER: objNodeRequest.MessageRequest.Body.listaServicios[0].interactId,
                    contactO_CLIENTE: objNodeRequest.MessageRequest.Body.listaServicios[0].nombresContact,
                    nrO_DOC_IDENTIDAD: objNodeRequest.MessageRequest.Body.listaServicios[0].nroDoc,
                    tipO_CLIENTE: that.getTipoCliente(objNodeRequest.MessageRequest.Body.listaServicios[0]),
                    nrO_CLARO: (that.SessionTipoCliente.IsMasivo=='1' ? 'Ver Anexo' : objNodeRequest.MessageRequest.Body.listaServicios[0].msisdn),
                    habilitaR_SERVICIO: h.toString(),
                    restringiR_SERVICIO: hh.toString(),
                    mediO_PERMITIDO: h.toString(),
                    mediO_NO_PERMITIDO: hh.toString(),
                    enviO_CORREO: (controls.chkSentEmail.prop("checked") ? 'SI' : 'NO'),
                    email: controls.txtEmailTitular.val(),
                    correO_SOLICITUD: controls.txtSendforEmail.val(),
                    coD_AGENTE: SessionTransac.SessionParams.USERACCESS.login,
                    nombrE_ASESOR: SessionTransac.SessionParams.USERACCESS.fullName,
                    lineaS_ASOCIADAS: SessionTransac.LeyPromoListaLineas
                }
            };
            console.log(oOnBaseCargaModel);
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/GenerarConstanciaLeyPromo',
                data: JSON.stringify(oOnBaseCargaModel),
                complete: function () {

                },
                success: function (response) {
                    console.log(response);
                    that.StrMensajeConstancia.MensajeError = "";
                    if (response.codeResponse == "0") {
                        that.strRutaPDF = response.Constancia.FullPathPDF;
                        controls.btnConstancia.prop("disabled", false);
                        that.GenerarInteraccionPlus(oOnBaseCargaModel, response.OnBase.codeOnBase);
                        if (!response.Constancia.Generated) {
                            that.StrMensajeConstancia.MensajeError = "No se genero la constancia.";
                        } else if (response.OnBase.codeResponse != "0") {
                            that.StrMensajeConstancia.MensajeError = "No se cargo la constancia a OnBase.";
                        }

                    }
                },
                error: function (msger) {
                    that.StrMensajeConstancia.MensajeError = "No se genero la constancia.";
                    console.log(msger);
                }
            });

            //ENVIO DE CORREO
            if ($(controls.chkSentEmail).is(':checked')) {

                var Asunto = GetKeyConfig("strLeyPromoAsuntoCorreo");
                var Motivo = GetKeyConfig("strLeyPromoMotivoCorreo");
                var Remitente = GetKeyConfig("strLeyPromoCorreoRemitente");
                var FlagHtml = GetKeyConfig("strLeyPromoFlagHtmlCorreo");

                that.EnvioCorreoInfoPromotion(Remitente, controls.txtSendforEmail.val(), Asunto, FormatBodyEmailInfoPromotion(controls.txtClientNameTitular.val(), Motivo, controls.txtSendforEmail.val()), FlagHtml, that.strRutaPDF);
            }


        },
        GenerarNota: function (objServiciosActual) {
            var that = this;
            var h = [];
            var hh = [];

            that.AccionEjecutada(that.Array_ListaServicicio.detalle, h, hh);

            var NotaHTML = '<table style="font-size:11px; color:navy">';
            NotaHTML += '<tr><td>' + '<b>Estado Anterior</b>' + '</td><td width="60px"></td><td>' + '<b>Estado Actual</b>' + '</td>';
            NotaHTML += '</tr>';
            NotaHTML += '<tr>';
            NotaHTML += '<tr><td>' + '<b>Medio permitido: </b>' + h.toString() + '</td><td width="60px"></td><td>' + '<b>Medio permitido: </b>' + objServiciosActual.habilitaR_SERVICIO + '</td>';
            NotaHTML += '</tr>';
            NotaHTML += '<tr>';
            NotaHTML += '<tr><td>' + '<b>Medio no permitido: </b>' + hh.toString() + '</td><td width="60px"></td><td>' + '<b>Medio no permitido: </b>' + objServiciosActual.restringiR_SERVICIO + '</td>';
            NotaHTML += '</tr>';
            NotaHTML += '</table>';
            return NotaHTML;
        },
        strRutaPDF: "",
        GenerarInteraccionPlus: function (objConstancia, codeOnBase) {
            var that = this;
            var onRequest = {
                strIdSession: Session.IDSESSION,
                template: {
                    _ID_INTERACCION: objConstancia.Constancia.casO_INTER,
                _X_REGISTRATION_REASON: GetKeyConfig("strLeyPromoConstanciaTransaccion"),
                _X_INTER_1: objConstancia.Constancia.msisdn,
                _X_EMAIL: objConstancia.Constancia.email,
                    _X_INTER_4: objConstancia.Constancia.tipO_DOC_IDENTIDAD, 
                    _X_INTER_3: objConstancia.Constancia.nrO_DOC_IDENTIDAD,
                    _X_INTER_6: moment().format('D/MM/YYYY'),
                _X_INTER_7: objConstancia.Constancia.enviO_CORREO,
                _X_INTER_15: objConstancia.Constancia.correO_SOLICITUD,
                _X_INTER_16: objConstancia.Constancia.canaL_ATENCION,
                    _X_INTER_30: objConstancia.Constancia.lineaS_ASOCIADAS.toString(),
                _X_INTER_17: codeOnBase,
                    _X_INTER_5: that.GenerarNota(objConstancia.Constancia),
                    _X_INTER_18: that.getTipoClienteSiac(objConstancia.Constancia.tipO_CLIENTE),
                    _X_INTER_19: objConstancia.Constancia.tipoDocDescContact,
                    _X_INTER_20: objConstancia.Constancia.nroDocContact,
                    _X_OPERATION_TYPE: objConstancia.Constancia.nombresContact,
                    _X_CLAROLOCAL2: objConstancia.Constancia.titulaR_CLIENTE
                }
            };

            console.log(onRequest);
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/GenerarTipificacionPlus',
                data: JSON.stringify(onRequest),
                complete: function () {

                },
                success: function (response) {
                    console.log(response);
                    if (response.codeResponse == "0") {



                    } else {
                        alert("");
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });

        },
        validarCambioServicio: function (listaServicios) {
            var that = this, controls = that.getControls();

            if (that.searchOptionType == 'SW') {
                var tipoLinea = controls.hdidTipoLinea.val();
                if (!((tipoLinea.toUpperCase().indexOf('LTE') != -1) || (tipoLinea.toUpperCase().indexOf('HFC') != -1))) {
            if (controls.txtNumberPhoneTitular.val().trim() == '') return false;
            }
            } else {
                if (!((SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText.indexOf('HFC') != -1) || (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText.indexOf('LTE') != -1))) {
                    if (controls.txtNumberPhoneTitular.val().trim() == '') return false;
                }
            }

            try {
                    if (that.Array_ListaServicicio.detalle.length == 0) 
                        return true;
            } catch (e) {
                console.log(that.Array_ListaServicicio.detalle);
            }
            
            for (var i in that.Array_ListaServicicio.detalle) {
                for (var ii in listaServicios) {
                    if (that.Array_ListaServicicio.detalle[i].servId == listaServicios[ii].servId) {
                        if (that.Array_ListaServicicio.detalle[i].estadoInfo != listaServicios[ii].estadoInfo) {
                            return true;
                        }
                    }
                }
            }

            if (that.Array_ListaServicicio.detalle.length != listaServicios.length) return true;

            return false;
        },
        ConstruirRequestListaServicio: function (strservId, LineaTelefono, idInteract, IsMasive) {

            var that = this,
            controls = that.getControls();

            var strEstadoInfo = '';
            var strTipoContacto = '';
            var strIdTipoContacto = '';
            var strTipoDocContacto = '';
            var strNombreContacto = '';
            var strNumeroDocumentoContacto = '';
            var strTipoLinea = '';
            var tipoCliente = '';
            var TipoOrigen = '';
            var strdesContactCanal = 'SIAC UNICO';
            var strCAC = '';
            var strCodigoCliente = null;
            var strMotivoHistorico = 'CAMBIO ESTADO INFO';
            var strMedioAprobacion = 'CONSTANCIA';
            var stridContacto = null;
            var strOrigenFuente = 'SIAC UNICO';



            if (strservId == Lib_ListaServicio.VOZ) {

                if ($(controls.rb_voz_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.Permitido;
                } else if ($(controls.rb_voz_no_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.No_Permitido;
                } else if ($(controls.rb_voz_pendiente).is(':checked')) {
                    strEstadoInfo = 'P';
                }

            } else if (strservId == Lib_ListaServicio.SMS) {

                if ($(controls.rb_sms_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.Permitido;
                } else if ($(controls.rb_sms_no_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.No_Permitido;
                } else if ($(controls.rb_sms_pendiente).is(':checked')) {
                    strEstadoInfo = 'P';
                }

            } else if (strservId == Lib_ListaServicio.CORREO) {

                if ($(controls.rb_correo_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.Permitido;
                } else if ($(controls.rb_correo_no_permitido).is(':checked')) {
                    strEstadoInfo = Lib_EstadoServicio.No_Permitido;
                } else if ($(controls.rb_correo_pendiente).is(':checked')) {
                    strEstadoInfo = 'P';
                }
            }

            if (that.SessionTipoCliente.IsMasivo == '1') {
                var flagContacto = '0';

                if ($(controls.rb_CA).is(':checked')) {
                    strTipoContacto = Lib_Lista_Tipo_Contacto.CA.name;
                    flagContacto = '1';
                } else if ($(controls.rb_CP).is(':checked')) {
                    strTipoContacto = Lib_Lista_Tipo_Contacto.CP.name;
                    flagContacto = '1';
                } else if ($(controls.rb_RRLL).is(':checked')) {
                    strTipoContacto = Lib_Lista_Tipo_Contacto.RRLL.name;
                    flagContacto = '1';
                }

                if (flagContacto == '1') {

                    //strIdTipoContacto = controls.hdidTipoDocumentoContacto.val().trim();
                    strIdTipoContacto = controls.txtTypeDocContacto.val();
                    strNombreContacto = controls.txtContactName.val().trim();
                    strNumeroDocumentoContacto = controls.txtContactNumberDoc.val().trim();
                    //strTipoDocContacto = controls.txtTypeDocContacto.val().trim();
                    strTipoDocContacto = $("#txtTypeDocContacto option:selected").text().trim();
                    stridContacto = controls.hdidContacto.val().trim();

                    if (($.isEmptyObject(that.ResponsedatosCliente.contactos)) == false) {
                        if (that.ResponsedatosCliente.contactos != null && that.ResponsedatosCliente.contactos.length > 0) {

                            var flagExistContact = false;

                            $.each(that.ResponsedatosCliente.contactos, function (key, value) {
                                if (strTipoContacto == value.tipoContact) {
                                    flagExistContact = true;
                                }
                            });

                            if (!flagExistContact) {
                                stridContacto = null;
                            }
                        }
                    }
                }

            } else {
                strTipoContacto = '';
                strIdTipoContacto = controls.txtTypeDocTitular.val();
                strNombreContacto = controls.txtClientNameTitular.val();
                strNumeroDocumentoContacto = controls.txtNumberDocTitular.val();
                strTipoDocContacto = $("#txtTypeDocTitular option:selected").text().trim();
                stridContacto = null;
            }

            tipoCliente = controls.hdidTipoCliente.val().trim();
            TipoOrigen = controls.hdidTipoOrigen.val().trim();
            strTipoLinea = controls.hdidTipoLinea.val().trim().toUpperCase();
            strCAC = controls.hdCAC.val().trim();
            strCodigoCliente = controls.hdidCliente.val().trim();



            if (strCodigoCliente == '0') {
                strCodigoCliente = null;
            }

            if (stridContacto == '0') {
                stridContacto = null;
            }


            if (strEstadoInfo != 'P') {

                var listaServicios = {
                    errorId: "",
                    msisdn: "",
                    tipoDoc: "",
                    tipoDocDes: "",
                    nroDoc: "",
                    nombApell: "",
                    email: "",
                    tipoCliente: "",
                    origen: "",
                    tipoLinea: "",
                    tipoDocContact: "",
                    tipoDocDescContact: "",
                    nroDocContact: "",
                    nombresContact: "",
                    tipoContact: "",
                    servId: "",
                    contact: "",
                    desContactCanal: "",
                    contactAplic: "",
                    fechaRespuesta: "",
                    estadoInfo: "",
                    motivoHist: "",
                    tipoOper: "",
                    motivoError: "",
                    estado: "",
                    estadoProceso: "",
                    cliId: "",
                    contId: "",
                    tipoMedioAprob: "",
                    interactId: ""
                }

                that.RequestUpdateStateLineEmail.tipoOperacion = 0;
                listaServicios.IsMasive = IsMasive
                listaServicios.errorId = '0';
                listaServicios.msisdn = LineaTelefono;
                listaServicios.tipoDoc = controls.txtTypeDocTitular.val().trim();
                listaServicios.tipoDocDes = $("#txtTypeDocTitular option:selected").text().trim();;
                listaServicios.nroDoc = controls.txtNumberDocTitular.val().trim();
                listaServicios.nombApell = controls.txtClientNameTitular.val().trim();
                listaServicios.email = controls.txtEmailTitular.val().trim();
                listaServicios.tipoCliente = tipoCliente;
                listaServicios.origen = TipoOrigen;
                listaServicios.tipoLinea = strTipoLinea;
                listaServicios.tipoDocContact = strIdTipoContacto;
                listaServicios.tipoDocDescContact = strTipoDocContacto;
                listaServicios.nroDocContact = strNumeroDocumentoContacto;
                listaServicios.nombresContact = strNombreContacto;
                listaServicios.tipoContact = strTipoContacto;
                listaServicios.servId = strservId;
                listaServicios.contact = (strservId == Lib_ListaServicio.CORREO ? '' : LineaTelefono);
                listaServicios.desContactCanal = strCAC;
                listaServicios.contactAplic = strdesContactCanal;
                listaServicios.fechaRespuesta = "";
                listaServicios.estadoInfo = strEstadoInfo;
                listaServicios.motivoHist = strMotivoHistorico;
                listaServicios.tipoOper = '';
                listaServicios.motivoError = '';
                listaServicios.estado = '';
                listaServicios.estadoProceso = ' ';
                listaServicios.cliId = strCodigoCliente;
                listaServicios.contId = stridContacto;
                listaServicios.tipoMedioAprob = strMedioAprobacion;
                listaServicios.interactId = idInteract;

                that.RequestUpdateStateLineEmail.listaServicios.push(listaServicios);

                that.RequestUpdateStateLineEmail.origenFuente = strOrigenFuente;
                that.RequestUpdateStateLineEmail.usuario = SessionTransac.SessionParams.USERACCESS.login == null ? '' : SessionTransac.SessionParams.USERACCESS.login;
                that.RequestUpdateStateLineEmail.fechaRegistro = '';
            }


        },
        ConstruirRequestListaServicioMasivo: function (Idinteraccion) {

            var that = this,
            controls = that.getControls();

            var strTipocargaMasiva = Lib_TipoCargaMasiva.Ninguno;

            if (($.isEmptyObject(SessionTransac.TipoCargaMasiva)) == false) {

                if (SessionTransac.TipoCargaMasiva != "" && SessionTransac.TipoCargaMasiva != Lib_TipoCargaMasiva.Ninguno) {
                    if ($(controls.chkUploadMassive).is(':checked')) {
                    strTipocargaMasiva = SessionTransac.TipoCargaMasiva;
                    }
                    if (($.isEmptyObject(SessionTransac.LeyPromoListaLineas)) == false) {
                        if (SessionTransac.LeyPromoListaLineas != "" && SessionTransac.LeyPromoListaLineas != null) {
                            for (var i = 0; i < SessionTransac.LeyPromoListaLineas.length; i++) {
                                console.log(SessionTransac.LeyPromoListaLineas[i]);
                                if (!$("#rb_voz_permitido").is(":disabled"))
                                that.ConstruirRequestListaServicio(Lib_ListaServicio.VOZ, SessionTransac.LeyPromoListaLineas[i], Idinteraccion, true);
                                if (!$("#rb_sms_permitido").is(":disabled"))
                                that.ConstruirRequestListaServicio(Lib_ListaServicio.SMS, SessionTransac.LeyPromoListaLineas[i], Idinteraccion, true);
                                if (!$("#rb_correo_permitido").is(":disabled"))
                                that.ConstruirRequestListaServicio(Lib_ListaServicio.CORREO, SessionTransac.LeyPromoListaLineas[i], Idinteraccion, true);
                            }
                        }
                    }
                }
            }


            return strTipocargaMasiva;

        },
        GenerarArchivoCSV: function (request) {

            var that = this,
            controls = that.getControls();

            var objNodeRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: request
                }
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/GenerarArchivoCsv',
                data: JSON.stringify(objNodeRequest),
                async: true,
                complete: function () {
                    //controls.btnregistrar.button('reset');
                },
                success: function (response) {
                    var Respuesta = '';
                    if (response.ResultResponse != null) {
                        if (response.ResultResponse == "0") {
                            console.log("Se subio correctamente el archivo al sftp")
                        } else {
                            console.log('Error al subir el archivo al SFTP');
                        }
                    }
                    else {
                        console.log('Error al subir el archivo al SFTP');
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });
        },
        getTipificacionInfo: function () {
            var that = this,
            controls = that.getControls();
            var tipoCliente = "";

            if (that.searchOptionType == 'SW') {

                var tipoLinea = controls.hdidTipoLinea.val();

                if (tipoLinea.toUpperCase().indexOf("DTH")!=-1) {
                    tipoCliente = GetKeyConfig("strLeyPromDTHPOST").split("|");
                } else if (tipoLinea.toUpperCase().indexOf('INTERNET INALAMBRICO') != -1) {
                    tipoCliente = GetKeyConfig("strLeyPromIFI").split("|");
                } else if (tipoLinea.toUpperCase().indexOf('LTE') != -1) {
                    tipoCliente = GetKeyConfig("strLeyPromLTE").split("|");
                } else if (tipoLinea.toUpperCase().indexOf('HFC') != -1) {
                    tipoCliente = GetKeyConfig("strLeyPromHFC").split("|");
                } else if (tipoLinea.toUpperCase().indexOf('PREPAGO') != -1) {
                    if (tipoLinea.toUpperCase().indexOf('FTI') != -1) {
                        tipoCliente = GetKeyConfig("strLeyPromFIJO").split("|");
                    } else {
                        tipoCliente = GetKeyConfig("strLeyPromPREPAGO").split("|");
                    }
                } else if (tipoLinea.toUpperCase().indexOf('POSTPAGO') != -1) {
                    if (tipoLinea.toUpperCase().indexOf('FTI') != -1) {
                        tipoCliente = GetKeyConfig("strLeyPromFIJO").split("|");
                    } else {
                        tipoCliente = GetKeyConfig("strLeyPromPOSTPAGO").split("|");
                    }
                } else {
                    tipoCliente = GetKeyConfig("strLeyPromNOCLIENTE").split("|");
                }

            } else {
            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf(' DTH') != -1) {
                tipoCliente = GetKeyConfig("strLeyPromDTHPOST").split("|");
                } else if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf('INTERNET INALAMBRICO') != -1) {
                tipoCliente = GetKeyConfig("strLeyPromIFI").split("|");
                } else if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf('LTE') != -1) {
                tipoCliente = GetKeyConfig("strLeyPromLTE").split("|");
                } else if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf('HFC') != -1) {
                tipoCliente = GetKeyConfig("strLeyPromHFC").split("|");
            } else if (oCustomer.Application === 'PREPAID') {
                    if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf(' TFI') != -1) {
                    tipoCliente = GetKeyConfig("strLeyPromFIJO").split("|");
                } else {
                    tipoCliente = GetKeyConfig("strLeyPromPREPAGO").split("|");
                }
            } else if (oCustomer.Application === 'POSTPAID') {
                    if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && oCustomer.ProductTypeText.indexOf(' TFI') != -1) {
                    tipoCliente = GetKeyConfig("strLeyPromFIJOPOST").split("|");
                } else {
                    tipoCliente = GetKeyConfig("strLeyPromPOSTPAGO").split("|");
                }
            } else {

                    if (oCustomer.Application != undefined && oCustomer.Application == 'HFC') {
                        tipoCliente = GetKeyConfig("strLeyPromHFC").split("|");
                    } else if (oCustomer.Application != undefined && oCustomer.Application == 'LTE') {
                        tipoCliente = GetKeyConfig("strLeyPromLTE").split("|");
                    } else {
                tipoCliente = GetKeyConfig("strLeyPromNOCLIENTE").split("|");
            }


                }
            }

            

            return tipoCliente;
        },
        GenerarNoCliente: function () {

            var that = this,
            controls = that.getControls(), resultado = false;

            var oModel = {
                idSession: Session.IDSESSION,
                Telefono: controls.txtNumberPhoneTitular.val().trim(),
                Usuario: SessionTransac.SessionParams.USERACCESS.login,
                Nombre: controls.txtClientNameTitular.val().trim(),
                Apellidos: " ",
                RazonSocial: controls.txtClientNameTitular.val().trim(),
                DocumentoTipo: $("#txtTypeDocTitular option:selected").text().trim().toUpperCase(),
                DocumentoNumero: controls.txtNumberDocTitular.val().trim(),
                Direccion: '',
                Distrito: '',
                Departamento: '',
                Modalidad: 'No Usuario'
            };

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/RegistrarNoCliente',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin RegistrarNoCliente");
                },
                success: function (response) {
                    console.log(response);
                    if (response.codigoRespuesta == "0") {
                        resultado = true;
                    } else {
                        alert("No se pudo registrar al No cliente");
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });

            return resultado;
        },
        GenerarInteraccion: function () {

            var that = this,
            controls = that.getControls();

            var idInteraccion = '0';

            var strClaseInfo = that.getTipificacionInfo();
            var strCustomerTelefono = '';

            if (that.searchOptionType == 'SW') {
                var tipoLinea = controls.hdidTipoLinea.val();
                if (tipoLinea.indexOf('HFC') != -1 || tipoLinea.indexOf('LTE') != -1) {
                    strCustomerTelefono = 'H' + SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
                } else if (tipoLinea.indexOf(' TFI') != -1) {
                    strCustomerTelefono = '000' + controls.txtNumberPhoneTitular.val().trim();
            } else {
                strCustomerTelefono = controls.txtNumberPhoneTitular.val().trim();
            }
            } else {
                if ((SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText.indexOf('HFC') != -1) || (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText.indexOf('LTE') != -1)) {
                    strCustomerTelefono = 'H' + SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
                } else if (SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText != undefined && SessionTransac.SessionParams.DATACUSTOMER.ProductTypeText.indexOf(' TFI') != -1) {
                    strCustomerTelefono = '000' + controls.txtNumberPhoneTitular.val().trim();
            } else {

                    var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                    if ((oCustomer.Application != undefined && oCustomer.Application == 'HFC') || (oCustomer.Application != undefined && oCustomer.Application == 'LTE')) {
                        strCustomerTelefono = 'H' + SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
                    }
                    else {
                strCustomerTelefono = controls.txtNumberPhoneTitular.val().trim();
            }
            }
            }


            var oModel = {
                strIdSession: Session.IDSESSION,
                objIdContacto: '0',
                Type: strClaseInfo[0].split(",")[0],
                Class: strClaseInfo[0].split(",")[1],
                SubClass: strClaseInfo[0].split(",")[2],
                Note: controls.txtNote.val(),
                CustomerId: strCustomerTelefono,
                Plan: '',
                ContractId: '',
                CurrentUser: SessionTransac.SessionParams.USERACCESS.login == null ? GetKeyConfig('strLeyPromoDefaultCodigoEmpleadoInterac') : SessionTransac.SessionParams.USERACCESS.login,
                objIdSite: '0',
                Cuenta: ''
            }

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/GenerarTipificacion',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin GenerarTipificacion");
                },
                success: function (response) {
                    console.log(response);
                    if (response.codeResponse == "0") {
                        idInteraccion = response.idInteraction;
                    } else {
                        idInteraccion = "0";
                        alert("No se generó la interacción");
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });

            return idInteraccion;
        },
        setMaxLengthTipoDocumento: function (idTipoDocumento, controlNumeroDocumento) {

            var MaxLongitud = null;
            MaxLongitud = ($.map(Lib_Lista_Tipo_Documento, function (data) {
                if (data.id == idTipoDocumento) {
                    if (data.IsNumeric == '1') {
                        $(controlNumeroDocumento).keypress(function (event) {
                            return InNumeric(event);
                        });
                    } else {
                        $(controlNumeroDocumento).off("keypress");
                    }

                    SetMaxLengthControl(controlNumeroDocumento, data.maxlength);
                    return data;
                }
            }));

        },
        setControlIsNumeric: function (control) {
            $(control).keypress(function (event) {
                return InNumeric(event);
            });
        },
        ValidarMinMaxLengthTipoDocumento: function (idTipoDocumento, Controllength, callback) {

            var ArrayMinMaxLongitud = null;
            var minLongitud = 0;
            var MaxLongitud = 0;

            var flag = false;
            var mensaje = '';

            ArrayMinMaxLongitud = ($.map(Lib_Lista_Tipo_Documento, function (data) {
                if (data.id == idTipoDocumento) {

                    minLongitud = data.minlength;
                    MaxLongitud = data.maxlength;

                    if (Controllength < minLongitud) {
                        mensaje = 'El valor minimo del campo debe ser de ' + minLongitud + ' caracteres.';
                        flag = true;
                        callback(mensaje);

                    }

                    if (Controllength > MaxLongitud) {
                        mensaje = 'El valor maximo del campo debe ser de ' + MaxLongitud + ' caracteres.';
                        flag = true;
                        callback(mensaje);
                    }

                    return data;
                }
            }));
            callback(mensaje);

            return flag;
        },
        BuscarClienteClarify: function (Telefono, callback) {

            var that = this,
           controls = that.getControls();

            var objNode = {
                srtIdSession: Session.IDSESSION,
                strTransaction: Session.IDSESSION,
                strTelefono: Telefono,
                strCuenta: '',
                strcontactobjid_1: '',
                strflag_registrado: '2'
            };

            debugger;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/ConsultarDatosUsuarioClarify',
                data: JSON.stringify(objNode),
                complete: function () {
                    callback();
                },
                success: function (response) {
                    console.log(response.data.listClient);

                    if (($.isEmptyObject(response.data.listClient)) == false) {
                        if (response.data.listClient != undefined && response.data.listClient != null) {

                            that.searchOptionType = 'CL';
                            controls.hdidTipoOrigen.val(Lib_TipoOrigen.Cliente);

                            that.clearDatosTitular();
                            that.clearDatosContacto();

                            controls.txtClientNameTitular.val(response.data.listClient.S_NOMBRES.concat(' ', response.data.listClient.S_APELLIDOS));
                            controls.txtNumberPhoneTitular.val(response.data.listClient.TELEFONO);


                            if (response.data.listClient.TIPO_DOC != null) {
                                var codigoTipoDocumento = null;
                                codigoTipoDocumento = ($.map(Lib_Lista_Tipo_Documento, function (data) {
                                    if (data.name == response.data.listClient.TIPO_DOC) {
                                        return data;
                                    }
                                }));

                                if (codigoTipoDocumento != null && codigoTipoDocumento.length > 0) {
                                    controls.txtTypeDocTitular.val(codigoTipoDocumento[0].id);
                                    that.setMaxLengthTipoDocumento(codigoTipoDocumento[0].id, controls.txtNumberDocTitular);

                                } else {
                                    controls.txtTypeDocTitular.val('-1');
                                }

                            }

                            controls.hdidTipoLinea.val(response.data.listClient.MODALIDAD);

                            controls.txtNumberDocTitular.val(response.data.listClient.NRO_DOC);

                            controls.txtEmailTitular.val(response.data.listClient.EMAIL);

                            if (controls.txtNumberPhoneTitular.val().trim().length > 0) {
                                that.enableElements(controls.RowSMS.children());
                                that.enableElements(controls.RowVoz.children());
                            } else {
                                that.disableElements(controls.RowVoz.children());
                                that.disableElements(controls.RowSMS.children());
                            }

                            if (controls.txtEmailTitular.val().trim().length > 0) {

                                that.enableElements(controls.RowCorreo.children());
                                $(controls.chkSentEmail).prop("checked", true);
                                controls.txtSendforEmail.val(controls.txtEmailTitular.val().trim());
                                controls.txtSendforEmail.attr('disabled', false);
                            } else {

                                that.disableElements(controls.RowCorreo.children());
                                $(controls.chkSentEmail).prop("checked", false);
                                controls.txtSendforEmail.attr('disabled', true);
                                controls.txtSendforEmail.val('');
                            }

                            controls.hdidTipoCliente.val('Particular-Prepago'); 
                            that.MostrarTipoClienteCorportativo(controls.hdidTipoCliente.val());

                            that.ClienteDeshabilitar(true);
                        } else {
                            console.log('No hay datos en el servicio de clarify');
                        }
                    }
                },
                error: function (msger) {
                    console.log(msger);

                }
            });
        },
        buscarInfoCentralizada: function (BusquedaCorreo, BusquedaTelefono, callback) {

            var that = this,
            controls = this.getControls();

            that.clearResponsedatosCliente();

            $("#btnSave").prop("disabled", false);

            var objNode = {
                strIdSession: Session.IDSESSION,
                strNroTelefono: BusquedaTelefono,
                strcorreo: BusquedaCorreo
            };


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/RestricSellInfoPromotion/SearchStateLineEmail',
                data: JSON.stringify(objNode),
                complete: function () {
                    callback();
                },
                success: function (response) {
                    console.log(response.data.datosCliente);

                    if (response.data.datosCliente != null) {

                        that.clearDatosTitular();
                        that.clearDatosContacto();

                        if (response.data.datosCliente.detalle.length != 0) {
                            if ((!!response.data.datosCliente.detalle[0].tipoLinea && (response.data.datosCliente.detalle[0].tipoLinea.indexOf('HFC') != -1 || response.data.datosCliente.detalle[0].tipoLinea.indexOf('LTE') != -1))) {
                                var objNode = {
                                    strIdSession: Session.IDSESSION,
                                    strSearchType: 1,
                                    strSearchValue: controls.txtCriteriaValue.val().trim(),
                                    NotEvalState: false,
                                    FlagSearchType: true,
                                    userId: SessionTransac.SessionParams.USERACCESS.userId,
                                    IsPrepaid: false,
                                    IsPostPaid: false
                                };
                                debugger;
                                $.app.ajax({
                                    type: 'POST',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: 'json',
                                    async: false,
                                    url: '/Home/ValidateQuery',
                                    data: JSON.stringify(objNode),
                                    complete: function () {
                                    },
                                    success: function (response) {
                                        console.log(response);
                                        var oCustomer = response.data;
                                        if ($.isEmptyObject(oCustomer) == false) {
                                            SessionTransac.SessionParams.DATACUSTOMER = oCustomer;
                                        }
                                    },
                                    error: function (msger) {
                                        console.log(msger);
                                    }
                                });
                            }
                        }

                        that.searchOptionType = 'SW';

                        controls.hdidTipoOrigen.val(response.data.datosCliente.origen);
                        controls.hdidTipoLinea.val(response.data.datosCliente.tipoLinea);
                        if (response.data.datosCliente.origen == Lib_TipoOrigen.Cliente) {
                            $("#spanMensajeNoCliente").hide();
                        } else {
                            $("#spanMensajeNoCliente").show();
                            SessionTransac.SessionParams.DATACUSTOMER.Application = 'NOCLIENTE';
                        }

                        that.ResponsedatosCliente.cliId = response.data.datosCliente.cliId;
                        that.ResponsedatosCliente.tipoDoc = response.data.datosCliente.tipoDoc;
                        that.ResponsedatosCliente.tipoDocDesc = response.data.datosCliente.tipoDocDesc;
                        that.ResponsedatosCliente.nroDocumento = response.data.datosCliente.nroDocumento;
                        that.ResponsedatosCliente.nombresYApellidos = response.data.datosCliente.nombresYApellidos;
                        that.ResponsedatosCliente.email = response.data.datosCliente.email;
                        that.ResponsedatosCliente.tipoCliente = response.data.datosCliente.tipoCliente;
                        that.ResponsedatosCliente.origen = response.data.datosCliente.origen;
                        that.ResponsedatosCliente.usuarioCrea = response.data.datosCliente.usuarioCrea;
                        that.ResponsedatosCliente.fechaCrea = response.data.datosCliente.fechaCrea;
                        that.ResponsedatosCliente.usuarioModi = response.data.datosCliente.usuarioModi;
                        that.ResponsedatosCliente.fechaModi = response.data.datosCliente.fechaModi;
                        that.ResponsedatosCliente.fechaCrea = response.data.datosCliente.fechaCrea;


                        if (($.isEmptyObject(response.data.datosCliente.contactos)) == false) {
                            if (response.data.datosCliente.contactos != null && response.data.datosCliente.contactos.length > 0) {

                                for (var i = 0; i < response.data.datosCliente.contactos.length; i++) {

                                    var contactos = {
                                        contId: '',
                                        cliId: '',
                                        tipoDocContact: '',
                                        tipoDocDescContact: '',
                                        nroDocContact: '',
                                        nombresContact: '',
                                        tipoContact: ''
                                    }

                                    contactos.contId = response.data.datosCliente.contactos[i].contId;
                                    contactos.cliId = response.data.datosCliente.contactos[i].cliId;
                                    contactos.tipoDocContact = response.data.datosCliente.contactos[i].tipoDocContact;
                                    contactos.tipoDocDescContact = response.data.datosCliente.contactos[i].tipoDocDescContact;
                                    contactos.nroDocContact = response.data.datosCliente.contactos[i].nroDocContact;
                                    contactos.nombresContact = response.data.datosCliente.contactos[i].nombresContact;
                                    contactos.tipoContact = response.data.datosCliente.contactos[i].tipoContact;

                                    that.ResponsedatosCliente.contactos.push(contactos);
                                }
                            }
                        }



                        if (($.isEmptyObject(response.data.datosCliente.detalle)) == false) {
                            if (response.data.datosCliente.contactos != null && response.data.datosCliente.detalle.length > 0) {
                                for (var i = 0; i < response.data.datosCliente.detalle.length; i++) {

                                    var detalle = {
                                        listId: '',
                                        cliId: '',
                                        contId: '',
                                        msisdn: '',
                                        servId: '',
                                        contacto: '',
                                        desContactCanal: '',
                                        contactAplic: '',
                                        tipoMedioAprob: '',
                                        interacId: '',
                                        fechaRespuesta: '',
                                        estadoInfo: ''
                                    }

                                    detalle.listId = response.data.datosCliente.detalle[i].listId;
                                    detalle.cliId = response.data.datosCliente.detalle[i].cliId;
                                    detalle.contId = response.data.datosCliente.detalle[i].contId;
                                    detalle.msisdn = response.data.datosCliente.detalle[i].msisdn;
                                    detalle.servId = response.data.datosCliente.detalle[i].servId;
                                    detalle.contacto = response.data.datosCliente.detalle[i].contacto;
                                    detalle.desContactCanal = response.data.datosCliente.detalle[i].desContactCanal;
                                    detalle.contactAplic = response.data.datosCliente.detalle[i].contactAplic;
                                    detalle.tipoMedioAprob = response.data.datosCliente.detalle[i].tipoMedioAprob;
                                    detalle.interacId = response.data.datosCliente.detalle[i].interacId;
                                    detalle.fechaRespuesta = response.data.datosCliente.detalle[i].fechaRespuesta;
                                    detalle.estadoInfo = response.data.datosCliente.detalle[i].estadoInfo;

                                    that.ResponsedatosCliente.detalle.push(detalle);
                                }
                            }
                        }

                        console.log('ResponsedatosCliente');
                        console.log(that.ResponsedatosCliente);

                        controls.hdidCliente.val(response.data.datosCliente.cliId);
                        controls.txtClientNameTitular.val(response.data.datosCliente.nombresYApellidos);

                        controls.txtTypeDocTitular.val(response.data.datosCliente.tipoDoc);
                        controls.txtNumberDocTitular.val(response.data.datosCliente.nroDocumento);

                        that.setMaxLengthTipoDocumento(response.data.datosCliente.tipoDoc, controls.txtNumberDocTitular);

                        controls.txtEmailTitular.val(response.data.datosCliente.email);

                        controls.hdidTipoCliente.val(response.data.datosCliente.tipoCliente); // TIPO CLIENTE
                        that.MostrarTipoClienteCorportativo(response.data.datosCliente.tipoCliente);


                        if (($.isEmptyObject(response.data.datosCliente.detalle)) == false) {
                            if (response.data.datosCliente.detalle != null) {
                                that.cargarListarServicios(response.data.datosCliente.detalle);
                                that.Array_ListaServicicio.detalle = jQuery.extend(true, [], response.data.datosCliente.detalle);
                            } else {
                                that.cargarListarServicios(null);
                            }
                        } else {
                            that.cargarListarServicios(null);
                        }


                        if (controls.txtNumberPhoneTitular.val().trim().length > 0) {
                            that.enableElements(controls.RowSMS.children());
                            that.enableElements(controls.RowVoz.children());
                        } else {
                            that.disableElements(controls.RowVoz.children());
                            that.disableElements(controls.RowSMS.children());
                        }

                        if (controls.txtEmailTitular.val().trim().length > 0) {
                            that.enableElements(controls.RowCorreo.children());

                            $(controls.chkSentEmail).prop("checked", true);
                            controls.txtSendforEmail.val(controls.txtEmailTitular.val().trim());
                            controls.txtSendforEmail.attr('disabled', false);
                        } else {

                            that.disableElements(controls.RowCorreo.children());
                            $(controls.chkSentEmail).prop("checked", false);
                            controls.txtSendforEmail.attr('disabled', true);
                            controls.txtSendforEmail.val('');

                        }


                        if (($.isEmptyObject(response.data.datosCliente.contactos)) == false) {
                            if (response.data.datosCliente.contactos != null && response.data.datosCliente.contactos.length > 0) {
                                for (var i = 0 ; i < response.data.datosCliente.contactos.length ; i++) {
                                    var flagContacto = false;
                                    if (response.data.datosCliente.contactos[i].tipoContact == Lib_Lista_Tipo_Contacto.RRLL.name) {
                                        $(controls.rb_RRLL).prop('checked', true);
                                        flagContacto = true;
                                    } else if (response.data.datosCliente.contactos[i].tipoContact == Lib_Lista_Tipo_Contacto.CA.name) {
                                        $(controls.rb_CA).prop('checked', true);
                                        flagContacto = true;
                                    } else if (response.data.datosCliente.contactos[i].tipoContact == Lib_Lista_Tipo_Contacto.CP.name) {
                                        $(controls.rb_CP).prop('checked', true);
                                        flagContacto = true;
                                    } else {
                                        $(controls.rb_RRLL).prop('checked', false);
                                        $(controls.rb_CA).prop('checked', false);
                                        $(controls.rb_CP).prop('checked', false);
                                        flagContacto = false;
                                    }

                                    if (flagContacto) {

                                        controls.txtContactName.val(response.data.datosCliente.contactos[i].nombresContact);
                                        controls.txtTypeDocContacto.val(response.data.datosCliente.contactos[i].tipoDocContact);
                                        controls.txtContactNumberDoc.val(response.data.datosCliente.contactos[i].nroDocContact);
                                        controls.hdidContacto.val(response.data.datosCliente.contactos[i].contId);
                                    }

                                }

                            } 
                        } 
                        that.ClienteDeshabilitar(true);
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });
        },
        ClienteDeshabilitar: function (sw) {
            var that = this, controls = that.getControls();

            controls.txtClientNameTitular.prop("disabled", sw);
            controls.txtNumberDocTitular.prop("disabled", sw);
            controls.txtTypeDocTitular.prop("disabled", sw);

            if (controls.txtNumberPhoneTitular.val().trim() == '') controls.txtNumberPhoneTitular.prop("disabled", false); else controls.txtNumberPhoneTitular.prop("disabled", sw);

            controls.txtEmailTitular.prop("disabled", false);

            if (controls.hdidTipoOrigen.val() == Lib_TipoOrigen.No_Cliente) {
                controls.txtClientNameTitular.prop("disabled", false);
                controls.txtNumberDocTitular.prop("disabled", false);
                controls.txtTypeDocTitular.prop("disabled", false);
                controls.txtNumberPhoneTitular.prop("disabled", false);
            }

        },
        clearResponsedatosCliente: function () {
            var that = this;
            that.ResponsedatosCliente.cliId = '';
            that.ResponsedatosCliente.tipoDoc = '';
            that.ResponsedatosCliente.tipoDocDesc = '';
            that.ResponsedatosCliente.nroDocumento = '';
            that.ResponsedatosCliente.nombresYApellidos = '';
            that.ResponsedatosCliente.email = '';
            that.ResponsedatosCliente.tipoCliente = '';
            that.ResponsedatosCliente.origen = '';
            that.ResponsedatosCliente.usuarioCrea = '';
            that.ResponsedatosCliente.fechaCrea = '';
            that.ResponsedatosCliente.usuarioModi = '';
            that.ResponsedatosCliente.fechaModi = '';
            that.ResponsedatosCliente.fechaCrea = '';
            that.ResponsedatosCliente.contactos = [];
            that.ResponsedatosCliente.detalle = [];
        },
        MostrarTipoClienteCorportativo: function (TipoCliente) {

            var that = this,
            controls = this.getControls();

            that.SessionTipoCliente.IsMasivo = '0';
                $(controls.PrePostContacto).addClass('hide');
                $(controls.divCheckMasivo).addClass('hide');

            if (TipoCliente != null) {
                $(controls.PrePostContacto).addClass('hide');
                $(controls.divCheckMasivo).addClass('hide');
                $.each(Lib_IdentificadorCorporativo, function (key, value) {
                    if (value.toUpperCase().trim() == TipoCliente.toUpperCase().trim()) {
                                $(controls.PrePostContacto).removeClass('hide');
                                $(controls.divCheckMasivo).removeClass('hide');
                        that.SessionTipoCliente.IsMasivo = '1';
                    }
                });
            }

        },
        cargarListarServicios: function (ListaServicio) {

            debugger;
            var that = this,
            controls = this.getControls();

            if (ListaServicio != null) {

                that.clearListaServicio();

                var contador = 0;
                for (var i = 0 ; i < ListaServicio.length ; i++) {

                    if (ListaServicio[i].servId == Lib_ListaServicio.VOZ) {

                        if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.Permitido) {

                            $(controls.rb_voz_permitido).prop('checked', true);
                            $(controls.rb_voz_pendiente).css('display', 'none');
                            contador = contador + 1;

                        } else if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.No_Permitido) {

                            $(controls.rb_voz_no_permitido).prop('checked', true);
                            $(controls.rb_voz_pendiente).css('display', 'none');
                            contador = contador + 1;

                        } else {

                            $(controls.rb_voz_pendiente).prop('checked', true);
                            $(controls.rb_voz_pendiente).css('display', '');

                        }

                        $(controls.lblFechaVoz)[0].innerHTML = ListaServicio[i].fechaRespuesta;
                        $(controls.lblCanalVoz)[0].innerHTML = ListaServicio[i].contactAplic;

                        controls.txtNumberPhoneTitular.val(ListaServicio[i].msisdn);
                        controls.hdidTipoLinea.val(ListaServicio[i].tipoLinea);

                    } else if (ListaServicio[i].servId == Lib_ListaServicio.SMS) {

                        if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.Permitido) {

                            $(controls.rb_sms_permitido).prop('checked', true);
                            $(controls.rb_sms_pendiente).css('display', 'none');
                            contador = contador + 1;

                        } else if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.No_Permitido) {

                            $(controls.rb_sms_no_permitido).prop('checked', true);
                            $(controls.rb_sms_pendiente).css('display', 'none');
                            contador = contador + 1;

                        } else {

                            $(controls.rb_sms_pendiente).prop('checked', true);
                            $(controls.rb_sms_pendiente).css('display', '');

                        }

                        $(controls.lblFechaSMS)[0].innerHTML = ListaServicio[i].fechaRespuesta;
                        $(controls.lblCanalSMS)[0].innerHTML = ListaServicio[i].contactAplic;

                        controls.txtNumberPhoneTitular.val(ListaServicio[i].msisdn)
                        controls.hdidTipoLinea.val(ListaServicio[i].tipoLinea);


                    } else if (ListaServicio[i].servId == Lib_ListaServicio.CORREO) {

                        if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.Permitido) {

                            $(controls.rb_correo_permitido).prop('checked', true);
                            $(controls.rb_correo_pendiente).css('display', 'none');
                            contador = contador + 1;

                        } else if (ListaServicio[i].estadoInfo == Lib_EstadoServicio.No_Permitido) {

                            $(controls.rb_correo_no_permitido).prop('checked', true);
                            $(controls.rb_correo_pendiente).css('display', 'none');
                            contador = contador + 1;
                        } else {

                            $(controls.rb_correo_pendiente).prop('checked', true);
                            $(controls.rb_correo_pendiente).css('display', '');

                        }

                        $(controls.lblFechaCorreo)[0].innerHTML = ListaServicio[i].fechaRespuesta;
                        $(controls.lblCanalCorreo)[0].innerHTML = ListaServicio[i].contactAplic;

                        controls.txtNumberPhoneTitular.val(ListaServicio[i].msisdn)
                        controls.hdidTipoLinea.val(ListaServicio[i].tipoLinea);

                    }
                }

                if (contador == 3) {
                    $(controls.columPendiente).addClass('hide');
                    $(controls.columPendientevoz).addClass('hide');
                    $(controls.columPendientesms).addClass('hide');
                    $(controls.columPendientecorreo).addClass('hide');
                }

            } else {

                that.clearListaServicio();
            }
        },
        clearDatosTitular: function () {
            var that = this,
           controls = this.getControls();

            controls.txtClientNameTitular.val("");
            controls.txtNumberPhoneTitular.val("");
            controls.txtTypeDocTitular.val("-1");
            controls.txtEmailTitular.val("");
            controls.txtNumberDocTitular.val("");
            controls.hdidTipoCliente.val("");
            controls.hdidTipoOrigen.val(Lib_TipoOrigen.No_Cliente);
            controls.hdidTipoLinea.val("");
            controls.hdidCliente.val("0");
            controls.txtNote.val("");
            $(controls.chkSentEmail).prop("checked", false);
            $(controls.chkUploadMassive).prop("checked", false);
            SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            that.MostrarTipoClienteCorportativo(null);
            controls.btnConstancia.prop("disabled", true);
            controls.txtSendforEmail.val("");
            controls.txtSendforEmail.prop("disabled", true);
            $('#divDashboardTabs a[href="#divpreposttitular"]').tab('show');
        },
        clearDatosContacto: function () {
            var that = this,
           controls = this.getControls();

            controls.txtContactName.val('');
            controls.txtContactNumberDoc.val('');
            controls.txtTypeDocContacto.val('-1');
            controls.hdidContacto.val('0');
        },
        clearListaServicio: function () {

            var that = this,
            controls = this.getControls();

            SessionTransac.LeyPromoListaLineas = [];
            that.Array_ListaServicicio.detalle = [];

            $(controls.rb_correo_pendiente).prop('checked', true);
            $(controls.rb_sms_pendiente).prop('checked', true);
            $(controls.rb_voz_pendiente).prop('checked', true);

            $(controls.rb_correo_pendiente).css('display', '');
            $(controls.rb_sms_pendiente).css('display', '');
            $(controls.rb_voz_pendiente).css('display', '');

            $(controls.columPendientevoz).removeClass('hide');
            $(controls.columPendientesms).removeClass('hide');
            $(controls.columPendientecorreo).removeClass('hide');

            $(controls.columPendiente).removeClass('hide');

            $(controls.lblFechaVoz)[0].innerHTML = "";
            $(controls.lblCanalVoz)[0].innerHTML = "";
            $(controls.lblFechaSMS)[0].innerHTML = "";
            $(controls.lblCanalSMS)[0].innerHTML = "";
            $(controls.lblFechaCorreo)[0].innerHTML = "";
            $(controls.lblCanalCorreo)[0].innerHTML = "";

            $(controls.rb_correo_pendiente).prop('disabled', true);
            $(controls.rb_sms_pendiente).prop('disabled', true);
            $(controls.rb_voz_pendiente).prop('disabled', true);

            that.disableElements(controls.RowVoz.children());
            that.disableElements(controls.RowSMS.children());
            that.disableElements(controls.RowCorreo.children());


        },
        getCACDAC: function () {

            var that = this,
                controls = that.getControls();

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',

                success: function (results) {

                    if (($.isEmptyObject(results)) == false) {
                        if (results != null) {
                            var cacdac = results.Cac;
                            if (cacdac != null) {
                                controls.hdCAC.val(cacdac);
                            } else {
                                controls.hdCAC.val('');
                            }

                        }
                    }
                },
                error: function (msger) {
                    console.log(msger);
                }

            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,
        Array_ListaServicicio: {
            detalle: []
        },
        Array_ListaTipoDocumento: {
            TipoDocumento: []
        },
        RequestUpdateStateLineEmail: {
            tipoOperacion: 0,
            listaServicios: [],
            origenFuente: "",
            usuario: "",
            fechaRegistro: ""
        },
        ResponsedatosCliente: {
            cliId: '',
            tipoDoc: '',
            tipoDocDesc: '',
            nroDocumento: '',
            nombresYApellidos: '',
            email: '',
            tipoCliente: '',
            origen: '',
            usuarioCrea: '',
            fechaCrea: '',
            usuarioModi: '',
            fechaModi: '',
            contactos: [],
            detalle: []
        },
        SessionTipoCliente: {
            IsMasivo: '0'
        },
        StrMensajeConstancia: {
            MensajeError: '0'
        },
        LeerLineasCliente: function () {

            $.ajax({
                url: '/Transactions/RestricSellInfoPromotion/LeerLineasCliente',
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                method: 'POST',
                type: 'POST',
                success: function (response) {

                    //Hacer Algo
                    if (response != null && response != undefined) {
                        console.log('Leer Session');
                        console.log(response);
                        var tipo = $.trim(response.data.xxtipo);
                        var mensaje = $.trim(response.data.mensaje);

                        if (tipo == "C") {
                            console.log(mensaje);
                            var data = jQuery.parseJSON(mensaje);
                            for (var i = 0; i < data.length; i++) {
                                console.log(data[i]);
                            }
                        }
                        else if (tipo == "I") {
                            alert(mensaje);
                        }
                        else if (tipo == "E") {
                            alert(mensaje);
                        }

                    }

                }
            });

        },
        EnvioCorreoInfoPromotion: function (Remitente, Destinatario, Asunto, Mensaje, HTMLFlag, FullPathPDF) {
            var that = this;

            var oModel = {
                srtIdSession: Session.IDSESSION,
                strRemitente: Remitente,
                strDestinatario: Destinatario,
                strAsunto: Asunto,
                strMensaje: Mensaje,
                strHTMLFlag: HTMLFlag,
                strFullPathPDF: FullPathPDF
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/RestricSellInfoPromotion/GetSendEmailSBLeyPromociones',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin GetSendEmailSBLeyPromociones");
                },
                success: function (response) {
                    if (response.codigoRespuesta != null) {
                        if (response.codigoRespuesta == 0) {
                            console.log("correo enviado");
                        } else {
                            console.log("correo no enviado");
                        }
                    }
                    else {
                        console.log("correo no enviado");
                    }
                },
                error: function (msger) {
                    console.log("correo no enviado");
                }
            });


        },
        btnClose_click: function () {
            window.close();
        },
    };
    $.fn.RestricSellInfoPromotion = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('RestricSellInfoPromotion'),
                options = $.extend({}, $.fn.RestricSellInfoPromotion.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('RecordEquipment', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {

                var timeReady = setInterval(function () {
                    if (!!$.fn.addEvent) {
                        clearInterval(timeReady);
                data.init();
                    }
                }, 100);

                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };
    $.fn.RestricSellInfoPromotion.defaults = {}
    $('#ContentRestricSellInfoPromotion').RestricSellInfoPromotion();

})(jQuery);


