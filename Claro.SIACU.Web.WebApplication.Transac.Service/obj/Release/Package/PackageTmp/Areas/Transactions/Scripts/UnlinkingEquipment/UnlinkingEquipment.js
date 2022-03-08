var yourwindowname;
var objInfoBioTrazabilidad = {};
var objInfoUser = {};
var objReg = {};
var flagProcBio = 0;
var resultGetUser = false;
(function ($, undefined) {
    var maximunrecords;
    var Form = function ($element, options) {
        $.extend(this, $.fn.UnlinkingEquipmentMovil.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblPhoneNumber: $('#lblPhoneNumber', $element)
            , lblCustomerType: $('#lblCustomerType', $element)
            , lblContact: $('#lblContact', $element)
            , lblCustomer: $('#lblCustomer', $element)
            , lblDocumentType: $('#lblDocumentType', $element)
            , lblDocumentTypeRRLL: $('#lblDocumentTypeRRLL', $element)
            , lblNumberDocument: $('#lblNumberDocument', $element)
            , lblNumberDocumentRRLL: $('#lblNumberDocumentRRLL', $element)
            , lblLegalAgent: $('#lblLegalAgent', $element)
            , lblBirthDate: $('#lblBirthDate', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblQuantityTrios: $('#lblQuantityTrios', $element)
            , tblDetailRecordEquipment: $('#tblDetailRecordEquipment', $element)
            , spnMainTitle: $('#spnMainTitle')
            , btnValidar: $('#btnValidar', $element)
            , btnSearchIMEI: $('#btnSearchIMEI', $element)
            , lblTitle: $('#lblTitle', $element)
            , txtFirstName: $('#txtFirstName', $element)
            , txtLastName: $('#txtLastName', $element)
            , cbDocumentType: $('#cbDocumentType', $element)
            , txtDocumentNumber: $('#txtDocumentNumber', $element)
            , txtReferencePhone: $('#txtReferencePhone', $element)
            , cbReasonParient: $('#cbReasonParient', $element)
            , txtImei: $('#txtImei', $element)
            , txtMarkModel: $('#txtMarkModel', $element)
            , txtArea: $('#txtArea', $element)
            , txtNotes: $('#txtNotes', $element)
            , btnSave: $('#btnSave', $element)
            , btnClose: $('#btnClose', $element)
            , lblTitular: $('#lblTitular', $element)
            , lblUsuario: $('#lblUsuario', $element)
            , lblRRLLCartaPoder: $('#lblRRLLCartaPoder', $element)
            , divtextRelationshipOwner: $('#divtextRelationshipOwner', $element)
            , lblRelationshipOwner: $('#lblRelationshipOwner', $element)
            , txtRelationshipOwner: $('#txtRelationshipOwner', $element)
            , btnQuestionCall: $('#btnQuestionCall', $element)
            , hdnstrTranEquipoExtranjeroPre: $('#hdnstrTranEquipoExtranjeroPre', $element)
            , hdnstrTranEquipoExtranjeroPost: $('#hdnstrTranEquipoExtranjeroPost', $element)
            , hdnstrTipoCACCallCenter: $('#hdnstrTipoCACCallCenter', $element)
            , hidstrFlagPermisoBiometria: $('#hidstrFlagPermisoBiometria', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSearchIMEI.addEvent(that, 'click', that.btnSearchIMEI_click);
            controls.txtImei.addEvent(that, 'blur', that.txtImei_blur);
            controls.btnSave.addEvent(that, "click", that.registerEquipment_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnQuestionCall.addEvent(that, "click", that.btnQuestionCall_click);
            document.addEventListener('keydown', that.shortCutClose, false);

            that.maximizarWindow();
            that.windowAutoSize();
            that.loadSessionData();
            that.setMainTitle();

            that.render();
        },
        render: function () {
            var that = this;

            $.blockUI({
                message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '50px',
                    opacity: .5,
                    color: '#fff'
                }
            });

            that.getDocument();
            that.getReasonParient();
            that.loadHeaderTransaction();            
            that.onChangeCboDocument();
            that.onChangeRadioOption();
            that.ValidateLengthInput();
            that.loadCboDocument();
            that.fnGetUser();
        },
        loadSessionData: function () {
            var controls = this.getControls();
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.SUREDIRECT = SessionTransac.UrlParams.SUREDIRECT;

            if (Session.SUREDIRECT == "PREPAID") {
                Session.Parameters.UrlParams.NRODOC = Session.DATACUSTOMER.NumberDocument;
                Session.Parameters.UrlParams.PSTRFLAGOTRAPERSONA = '0';
                Session.Parameters.UrlParams.PSTRDC = Session.DATACUSTOMER.NumberDocument;
                Session.Parameters.UrlParams.PSTRNC = Session.DATACUSTOMER.Name;
                Session.Parameters.UrlParams.PSTRAC = Session.DATACUSTOMER.Lastname;
            }
            else if (Session.SUREDIRECT == "POSTPAID") {
                Session.Parameters.UrlParams.NRODOC = Session.DATACUSTOMER.DocumentNumber;
                Session.Parameters.UrlParams.PSTRFLAGOTRAPERSONA = '0';
            }

        },
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            controls.spnMainTitle.html('DESVINCULACIÓN DEL EQUIPO MÓVIL');
        },
        btnSearchIMEI_click: function () {
            var NumberTelephone;
            if (Session.SUREDIRECT == "PREPAID") {
                NumberTelephone = Session.DATACUSTOMER.TelephoneCustomer;
            }
            else if (Session.SUREDIRECT == "POSTPAID") {
                NumberTelephone = Session.DATACUSTOMER.Telephone;
            }

            $.window.open({
                type: 'post',
                modal: false,
                data: { number: NumberTelephone },
                title: "Búsqueda IMEI",
                url: '/Transactions/Common/SearchImei',
                width: 1200,
                height: 500,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                },
                minimizeBox: false
            });
        },
        loadHeaderTransaction: function () {
            var that = this,
                controls = that.getControls();

            if (Session.SUREDIRECT == "PREPAID") {
                customerId = Session.DATACUSTOMER.CustomerCode;
                customerTelephone = Session.DATACUSTOMER.TelephoneCustomer;
                customerFullName = Session.DATACUSTOMER.FullName;
                customerName = Session.DATACUSTOMER.Name;
                customerLastName = Session.DATACUSTOMER.LastName;
                customerNumberDocument = Session.DATACUSTOMER.DNIRUC;
                customerNumberDocumentRRLL = Session.DATACUSTOMER.NumberDocument;
                nameTransaction = controls.hdnstrTranEquipoExtranjeroPre.val();
                statusLinea = Session.DATASERVICE.StateLine;
                if (Session.DATACUSTOMER.LegalAgent == null) {
                    customerLegalAgent = "";
                } else {
                    customerLegalAgent = Session.DATACUSTOMER.LegalAgent;
                }

                controls.lblPhoneNumber.text(Session.DATASERVICE.NumberCellphone);
                controls.lblCustomerType.text(Session.DATACUSTOMER.Modality);
                controls.lblContact.text(Session.DATACUSTOMER.FullName);
                controls.lblCustomer.text(Session.DATACUSTOMER.FullName);
                controls.lblDocumentType.text(Session.DATACUSTOMER.TypeDocument);
                controls.lblNumberDocument.text(Session.DATACUSTOMER.DNIRUC);
                controls.lblLegalAgent.text(customerLegalAgent);
                controls.lblNumberDocumentRRLL.text(Session.DATACUSTOMER.NumberDocument);
                controls.lblBirthDate.text(Session.DATACUSTOMER.DateBirth);
                controls.lblCountry.text(Session.DATACUSTOMER.PlaceBirth);
                controls.lblQuantityTrios.text(Session.DATASERVICE.QuantityTrios);
                controls.txtFirstName.val(Session.DATACUSTOMER.Name);
                controls.txtLastName.val(Session.DATACUSTOMER.Lastname);
                controls.txtDocumentNumber.val(Session.DATACUSTOMER.DNIRUC);
            }
            else if (Session.SUREDIRECT == "POSTPAID") {
                customerId = Session.DATACUSTOMER.CustomerID;
                customerTelephone = Session.DATACUSTOMER.Telephone;
                customerFullName = Session.DATACUSTOMER.FullName;
                customerName = Session.DATACUSTOMER.Name;
                customerLastName = Session.DATACUSTOMER.LastName;
                customerNumberDocument = Session.DATACUSTOMER.DNIRUC;
                customerNumberDocumentRRLL = Session.DATACUSTOMER.DocumentNumber;
                nameTransaction = controls.hdnstrTranEquipoExtranjeroPost.val();
                statusLinea = Session.DATASERVICE.StateLine;
                if (Session.DATACUSTOMER.LegalAgent == null) {
                    customerLegalAgent = "";
                } else {
                    customerLegalAgent = Session.DATACUSTOMER.LegalAgent;
                }

                controls.lblPhoneNumber.text(Session.DATACUSTOMER.Telephone);
                controls.lblCustomerType.text(Session.DATACUSTOMER.CustomerType);
                controls.lblContact.text(Session.DATACUSTOMER.FullName);
                controls.lblCustomer.text(Session.DATACUSTOMER.BusinessName);
                controls.lblDocumentType.text(Session.DATACUSTOMER.DocumentType);
                controls.lblNumberDocument.text(Session.DATACUSTOMER.DNIRUC);
                controls.lblNumberDocumentRRLL.text(Session.DATACUSTOMER.DocumentNumber);
                controls.lblLegalAgent.text(customerLegalAgent);
                controls.lblBirthDate.text(Session.DATACUSTOMER.BirthDate);
                controls.lblCountry.text(Session.DATACUSTOMER.BirthPlace);
                controls.txtFirstName.val(Session.DATACUSTOMER.Name);
                controls.txtLastName.val(Session.DATACUSTOMER.LastName);
                controls.txtDocumentNumber.val(Session.DATACUSTOMER.DNIRUC);
            }
            controls.lblTitular.attr("style", "font-weight: bold;");
        },
        fnGetUser: function () {
            var that = this,
                controls = that.getControls();
            var parameters = {};
            parameters.strIdSession = Session.USERACCESS.userId;
            parameters.strCodeUser = Session.USERACCESS.login;
            $.app.ajax({
                async: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                error: function (xhr, status, error) {
                    resultGetUser = false;
                },
                success: function (results) {
                    objInfoUser = {
                        CodigoTipoCAC: results.CodeTypeCac,
                        CodigoCAC: results.CodeCac,
                        CodigoUsuario: results.CodeUser,
                        NombreTipoCAC: results.TypeCac,
                        NombreCAC: results.Cac,
                        CodigoRol: results.CodeRol
                    };
                    resultGetUser = true;
                }
            });
            return resultGetUser;
        },
        getDocument: function () {
            var that = this,
            controls = that.getControls();
            objReg = {};
            objReg.sessionId = '';
            if (Session.IDSESSION != 'undefined' && Session.IDSESSION != null && Session.IDSESSION != '') {
            objReg.sessionId = Session.IDSESSION;
            }
            $.app.ajax({
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetListDocument',
                data: JSON.stringify(objReg),
                success: function (result) {
                    controls.cbDocumentType.append($('<option>', { value: '', html: '--Seleccionar--' }));
                    if (result.data != null) {
                        $.each(result.data.ListProgramTask, function (index, value) {
                            controls.cbDocumentType.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                },
            });

        },
        getReasonParient: function () {
            var that = this,
              controls = that.getControls();
            model = {};
            model.strIdSession = Session.IDSESSION;
            model.strNameFunction = "ListaRelacionTitular";
            model.strFlagCode = "";
            model.fileName = "Data.xml";

            $.app.ajax({
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetListValueXmlMethod',
                data: JSON.stringify(model),
                success: function (result) {
                    controls.cbReasonParient.append($('<option>', { value: '', html: '--Seleccionar--' }));
                    if (result.data != null) {
                        $.each(result.data, function (index, value) {
                            controls.cbReasonParient.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                    }
                },
            });
        },

        cleanForm: function () {
            var that = this,
                      controls = that.getControls();
            $("#formGen")[0].reset();
            $('input:radio[name="optradio"]').change();
            that.loadHeaderTransaction();

        },
        registerEquipment_click: function () {
            var that = this,
             controls = that.getControls();

            if (that.validarRegisterEquipment() == true) {
              
                txtFirstName = controls.txtFirstName.val();
                txtLastName = controls.txtLastName.val();
                cbDocumentType = controls.cbDocumentType.val();
                cbDocumentTypeText = $("#cbDocumentType option:selected").text();
                txtDocumentNumber = controls.txtDocumentNumber.val();
                txtReferencePhone = controls.txtReferencePhone.val();
                cbReasonParient = controls.cbReasonParient.val();
                txtImei = controls.txtImei.val();
                txtMarkModel = controls.txtMarkModel.val();
                txtArea = controls.txtArea.val();
                txtNotes = controls.txtNotes.val();
                txtRelationshipOwner = controls.txtRelationshipOwner.val();
                rdoTipoPersona = $('input[name=optradio]:checked').val();
             
                if (resultGetUser == true) {
                    objReg = {
                        firstName: txtFirstName,
                        lastName: txtLastName,
                        documentType: cbDocumentType,
                        documentTypeText: cbDocumentTypeText,
                        documentNumber: txtDocumentNumber,
                        referencePhone: txtReferencePhone,
                        parient: cbReasonParient,
                        imei: txtImei,
                        imeiFisico: "",
                        area: txtArea,
                        markModel: txtMarkModel,
                        notes: txtNotes,
                        relationshipOwner: txtRelationshipOwner,
                        codeadviser: Session.USERACCESS.login,
                        adviser: Session.USERACCESS.fullName,
                        numberclaro: controls.lblPhoneNumber.val(),
                        idSession: Session.IDSESSION,
                        userAccesslogin: Session.USERACCESS.login,
                        customerId: customerId,
                        customerTelephone: customerTelephone,
                        customerFullName: customerFullName,
                        customerName: customerName,
                        customerLastName: customerLastName,
                        customerNumberDocument: customerNumberDocument,
                        typeCac: objInfoUser.NombreTipoCAC,
                        nameCac: objInfoUser.NombreCAC,
                        codeCac: objInfoUser.CodigoCAC,
                        nameTransaction: nameTransaction,
                        tipoPersona: rdoTipoPersona,
                        customerLegalAgent: customerLegalAgent,
                        customerNumberDocumentRRLL: customerNumberDocumentRRLL,
                        strStatusLinea: statusLinea
                    }

                    
                    if (Session.SUREDIRECT == "PREPAID") {                     

                        objReg.flagNoBiometria = true;
                        if (cbDocumentType == 2) {
                            var result = that.fnConsultPointOfSale(objReg.codeCac);
                            if (result == true) {
                                var objPre = {
                                    FlagOtraPersona: '1',
                                    NumeroDocumento: txtDocumentNumber,
                                    Nombres: txtFirstName,
                                    Apellidos: txtLastName,
                                    TransaccionOrigen: 'TRANSACCION_DESBLOQUEO_LINEA_EQUIPO_PRE',
                                    Telefono: customerTelephone
                                };
                                that.fnRedirectBiometriaPre(objPre);
                            }
                            else if (res == false) {
                                alert("No está autorizado a realizar la validación biométrica.", "Mensaje de Registro de Equipos");
                            } else {
                                alert("Error de conexión, vuelva a intentarlo.", "Mensaje de Registro de Equipos");
                            }
                            
                        } else {    
                            objReg.flagFirmaDigital = "0";
                            that.fnSaveEquipmentUnlinking(objReg);
                        }
                    } else if (Session.SUREDIRECT == "POSTPAID") {
                        if ($('#cboRRLLCartaPoder').is(":checked")) {

                            var obj = {
                                TipoDocumento: cbDocumentType,
                                NumeroDocumento: txtDocumentNumber,
                                Nombres: txtFirstName,
                                Apellidos: txtLastName,
                                TransaccionOrigen: '',
                                objDatosSolicitante: {
                                    Tipo: ''
                                },
                                Telefono: customerTelephone,
                                MotivoVal: ''
                            };

                            $.InitValidation(obj, function (response) {
                                if (typeof response != null && response != null && response != 'undefined' && response != 'Q') {
                                    
                                    objReg.listLegalAgent = response;
                                    var resp = JSON.parse(response);
                                   
                                    objReg.firstName = resp[0].Nombres;
                                    objReg.lastName = resp[0].Apellidos;
                                    objReg.documentType = resp[0].TipoDoc;
                                    objReg.documentTypeText = resp[0].TipoDocumento;
                                    objReg.documentNumber = resp[0].NroDocumento;
                                   
                                    if (resp[0].flagFirmaDigital == '1') {
                                        objReg.flagFirmaDigital = '1';
                                        objReg.strHuellaMinucia = resp[0].strHuellaMinucia;
                                        objReg.strHuellaEncode = resp[0].strHuellaEncode;
                                    }
                                    else {
                                        objReg.flagFirmaDigital = "0";
                                        if (resp[0].rp1 != null && resp[0].rp1 != 'undefined')
                                        {
                                            objReg.rp1 = resp[0].rp1;
                                            objReg.rp2 = resp[0].rp2;
                                            objReg.rp3 = resp[0].rp3;
                                        }                                            
                                    }
                                    that.fnSaveEquipmentUnlinking(objReg);
                                }
                            }, function () {
                                alert("Ventana de opción Tipo de Solicitante cerrada.", "Mensaje de Desvinculación");
                            });
                        }
                        else {
                            if (cbDocumentType == 2) {

                                objReg.flagNoBiometria = null;

                                var res = that.fnValidateBlackListCAC();
                                if (res == true) {
                                    if (typeof Session.objBiometria == 'undefined' || Session.objBiometria == null) {
                                        Session.objBiometria = new Object();
                                    }

                                    Session.objBiometria.TransaccionOrigen = 'TRANSAC';
                                    Session.objBiometria.FlagOtraPersona = '1';
                                    Session.objBiometria.Telefono = customerTelephone;
                                    Session.objBiometria.NumeroDocumento = txtDocumentNumber;
                                    Session.objBiometria.Nombres = txtFirstName;
                                    Session.objBiometria.Apellidos = txtLastName;
                                    Session.objBiometria.TipoDocumento = cbDocumentType;

                                    $.ValidationBiometrica.getConfigBiometria();
                                    var bioConfig = Session.jsonConfigBiometria;

                                    if (typeof bioConfig != undefined && bioConfig != null) {
                                        if (bioConfig.status != null && bioConfig.status.estado == 0) {
                                            if (bioConfig.data[0].soxpnFlagBiometria == '0' &&
                                                bioConfig.data[0].soxpnFlagIdValidator == '0' &&
                                                bioConfig.data[0].soxpnFlagNoBiometriaReniec == '0' &&
                                                bioConfig.data[0].soxpnFlagNoBiometriaDc == '0') {

                                                if (bioConfig.data[0].soxpvMensaje != '') {
                                                    alert(bioConfig.data[0].soxpvMensaje);
                                                }
                                                if (bioConfig.data[0].soxpnFlagFinVenta == '1') {
                                                    that.fnRedirectBiometriaPost();
                                                } else {
                                                    if (bioConfig.data[0].soxpnFlagIdValidator == '1') {
                                                        if (bioConfig.data[0].soxpnFlagNoBiometriaReniec == "1") {

                                                            that.fnRedirectNoBiometria();
                                                        }
                                                    } else if (bioConfig.data[0].soxpnFlagNoBiometriaReniec == "1") {

                                                        that.fnRedirectNoBiometria();
                                                    } else {

                                                        if (bioConfig.data.soxpnFlagFinVenta == '1') {
                                                            return true;
                                                        } else {
                                                            return;
                                                        }
                                                    }
                                                }
                                            } else {
                                                that.fnRedirectBiometriaPost();
                                            }
                                        } else {
                                            alert(bioConfig.status.descripcionRespuesta);
                                            return false;
                                        }
                                    } else {
                                        alert("No se pudo obtener la configuración biométrica, vuelva a intentarlo más tarde.", "Mensaje de Registro de Equipos");
                                        return false;
                                    }
                                } else if (res == false) {
                                    alert("Su Centro de Atención al Cliente se encuentra inhabilitado para pasar Validación Biométrica.", "Mensaje de Registro de Equipos");
                                } else {
                                    alert("Error al consultar información, vuelva a intentarlo más tarde.", "Mensaje de Registro de Equipos");
                                }

                            } else {
                                objReg.flagFirmaDigital = "0";
                                that.fnSaveEquipmentUnlinking(objReg);
                            }

                        }
                    }
                } else {
                    alert("No se obtuvo datos de Asesor.");
                }
            }

        },

        btnClose_click: function () {
            window.close();
        },

        validarRegisterEquipment: function () {
            var that = this,
                     controls = that.getControls();
            txtFirstName = controls.txtFirstName.val();
            txtLastName = controls.txtLastName.val();
            cbDocumentType = controls.cbDocumentType.val();
            txtDocumentNumber = controls.txtDocumentNumber.val();
            txtReferencePhone = controls.txtReferencePhone.val();
            cbReasonParient = controls.cbReasonParient.val();
            txtImei = controls.txtImei.val();
            txtMarkModel = controls.txtMarkModel.val();
            txtNotes = controls.txtNotes.val();
            txtArea = controls.txtArea.val();
            rdoTipoPersona = $('input[name=optradio]:checked').val();

            if ($.trim(txtFirstName) == "") {
                alert('Ingrese Nombres.');
                return false;
            }
            if ($.trim(txtLastName) == "") {
                alert('Ingrese Apellidos.');
                return false;
            }
            if (cbDocumentType == "") {
                alert('Seleccione Tipo de Documento.');
                return false;
            } 
            if ($.trim(txtDocumentNumber) == "") {
                alert('Ingrese Nº de Documento.');
                return false;
            }
			if (cbDocumentType != 2 && cbDocumentType != 0) {
            if ($.trim(txtDocumentNumber).length < 4) {
                alert('Ingrese como mínimo 4 dígitos para el tipo de documento.');
                return false;
            }
			}
            if (cbDocumentType == 2) {
				if ($.trim(txtDocumentNumber).length < 8) {
					alert('Ingrese como mínimo 8 dígitos para el tipo de documento.');
					return false;
				}
            }
			if (cbDocumentType == 0) {
				if ($.trim(txtDocumentNumber).length < 11) {
					alert('Ingrese como mínimo 11 dígitos para el tipo de documento.');
					return false;
				}
            }
            if (cbDocumentType == 2) {
        	if ($.trim(txtDocumentNumber).length < 8) {
        		alert('Ingrese como mínimo 8 dígitos para el tipo de documento.');
        		return false;
        	}
            }
            if (rdoTipoPersona == "2" || rdoTipoPersona == "4") {
                if (cbReasonParient == "") {
                    alert('Seleccione Relación con el Titular');
                    return false;
                }
            }
            if ($.trim(txtReferencePhone) != "" && $.trim(txtReferencePhone).length > 20) {
                alert('El telefono de referencia no puede tener más de 20 dígitos.');
                return false;
            }
            if (objInfoUser.NombreTipoCAC == controls.hdnstrTipoCACCallCenter.val()) {
            if ($.trim(txtArea) == "") {
                alert('Debe responder las preguntas de seguridad.');
                return false;
            }
            }
            if ($.trim(txtImei) == "") {
                alert('Ingrese Código IMEI.');
                return false;
            }
            if ($.trim(txtImei).length != 15) {
                alert('Número de IMEI incompleto, por favor ingrese los 15 dígitos.');
                return false;
            }
            if ($.isNumeric(txtImei) == false) {
                alert('Número de IMEI no válido, por favor ingrese sólo números.');
                return false;
            }
            if (validateRepeatNumber($.trim(txtImei)) === true) {
                alert('Número de IMEI incorrecto');
                return false;
            }
            if ($.trim(txtMarkModel) == "") {
                alert('Ingrese los datos de Marca - Modelo del equipo.');
                return false;
            }

            return true;
        },
        loadCboDocument: function () {
            var that = this,
            controls = that.getControls();
            valuedocument = controls.lblDocumentType.text();
            var value = $('#cbDocumentType option:contains(' + valuedocument + ')').val()
            controls.cbDocumentType.val(value);
        },
        onChangeCboDocument: function () {
            var that = this,
            controls = that.getControls();
            controls.cbDocumentType.change(function () { controls.txtDocumentNumber.val(""); });
        },
        ValidateLengthInput: function () {
            var that = this,
                controls = that.getControls();

            $("#txtFirstName,#txtLastName").keypress(function (key) {
                if ((key.charCode < 97 || key.charCode > 122)
                    && (key.charCode < 65 || key.charCode > 90) && (key.charCode != 211) && (key.charCode != 218)
                    && (key.charCode != 45) && (key.charCode != 241) && (key.charCode != 209) && (key.charCode != 32)
                     && (key.charCode != 225) && (key.charCode != 233) && (key.charCode != 237) && (key.charCode != 243)
                     && (key.charCode != 250) && (key.charCode != 193) && (key.charCode != 201) && (key.charCode != 205)
                    )
                { return false; }
            });
        },
        onChangeRadioOption: function () {
            var that = this,
                controls = that.getControls();

            $('input:radio[name="optradio"]').change(
            function () {
                if (this.checked && this.value == '1') {
                    controls.lblTitular.attr("style", "font-weight: bold;");
                    controls.lblUsuario.removeAttr("style");
                    controls.lblRRLLCartaPoder.removeAttr("style");
                    controls.txtFirstName.val(Session.DATACUSTOMER.Name);
                    if (Session.SUREDIRECT == "PREPAID") {
                        controls.txtDocumentNumber.val(Session.DATACUSTOMER.NumberDocument);
                        controls.txtLastName.val(Session.DATACUSTOMER.Lastname);
                    } else if (Session.SUREDIRECT == "POSTPAID") {
                        controls.txtDocumentNumber.val(Session.DATACUSTOMER.DNIRUC);
                        controls.txtLastName.val(Session.DATACUSTOMER.LastName);
                    }

                    controls.txtReferencePhone.val("");
                    controls.divtextRelationshipOwner.hide();
                    controls.lblRelationshipOwner.hide();
                    that.loadCboDocument();
                }
                if (this.checked && this.value == '2') {
                    controls.lblUsuario.attr("style", "font-weight: bold;");
                    controls.lblTitular.removeAttr("style");
                    controls.lblRRLLCartaPoder.removeAttr("style");
                    controls.txtFirstName.val("");
                    controls.txtLastName.val("");
                    controls.txtDocumentNumber.val("");
                    controls.cbDocumentType.val("");
                    controls.txtReferencePhone.val("");
                    controls.divtextRelationshipOwner.show();
                    controls.lblRelationshipOwner.show();
                    controls.cbReasonParient.val("");
                }
                if (this.checked && this.value == '4') {
                    controls.lblRRLLCartaPoder.attr("style", "font-weight: bold;");
                    controls.lblUsuario.removeAttr("style");
                    controls.lblTitular.removeAttr("style");
                    controls.txtFirstName.val("");
                    controls.txtLastName.val("");
                    controls.txtDocumentNumber.val("");
                    controls.cbDocumentType.val("");
                    controls.txtReferencePhone.val("");
                    controls.divtextRelationshipOwner.show();
                    controls.lblRelationshipOwner.show();
                    controls.cbReasonParient.val("");
                }
            });        
        },
        fnConsultPointOfSale: function (CodCAC) {
            var that = this,
                controls = that.getControls();

            var result = '';
            var respOk = controls.hidstrFlagPermisoBiometria.val();

            var params = {
                idSession: Session.IDSESSION,
                CustomerId: customerId,
                CodeCAC: CodCAC
            };

            $.ajax({
                async: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(params),
                url: '/Transactions/Common/GetConsultPointOfSale',
                error: function (xhr, status, error) {
                    result = '';
                },
                success: function (response) {
                    console.log(response);
                    if (response == respOk) {
                        result = true;
                    } else {
                        result = false;
                    }
                }
            });
            return result;
        },
        fnValidateBlackListCAC: function () {
            var result = '';

            var params = {
                idSession: Session.IDSESSION,
                CustomerId: customerId,
                strCadenaOpciones: Session.USERACCESS.optionPermissions,
                CodigoRol: objInfoUser.CodigoRol
            };

            $.ajax({
                async: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(params),
                url: '/Transactions/RecordEquipmentForeign/SetByParamGroup',
                error: function (xhr, status, error) {
                    result = false;
                },
                success: function (response) {
                    if (response == true) {
                        result = true;
                    }
                    else { result = false }
                }
            });
            return result;
        },
        fnSaveEquipmentUnlinking: function (objReg) {
        
            var that = this,
            controls = that.getControls();
            confirm('Se realizará la desvinculación del equipo móvil. ¿Deseas continuar?', 'Mensaje de Desvinculación del Equipo Móvil', function () {

                $.app.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/UnlinkingEquipment/UnlinkingEquipmentJson',
                    data: JSON.stringify(objReg),
                    complete: function () {
                        $.unblockUI();
                    },
                    beforeSend: function () {
                        $.blockUI({
                            message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                            baseZ: $.app.getMaxZIndex() + 1,
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
                    },
                    success: function (result) {
                        flagProcBio = 0;
                        if (result.data.ProcesSucess == true && result.data.codeResult == 0) {
                            var IdSession = Session.IDSESSION;
                            var Path = result.data.namePDF;
                            console.log(Path);
                            that.ReadRecordSharedFile(IdSession, Path);
                            if (objReg.documentType == 2 && objReg.flagFirmaDigital == 0) {
                                that.fnDeclaracionNoJuradaNoBiometrica(objReg);
                            }
                            alert("Se realizó la desvinculación del equipo móvil satisfactoriamente.", "Mensaje de Desvinculación del Equipo Móvil");
                            $("textarea,select,input").attr("disabled", true);
                            $("#btnSave").attr("disabled", true);
                            $("#btnSearchIMEI").attr("disabled", true);
                        }
                        else if (result.data.ProcesSucess == true && result.data.codeResult == 2) {
                            alert("El IMEI se encuentra registrado.", "Mensaje de Desvinculación del Equipo Móvil");
                        }
                        else if (result.data.ProcesSucess == false && result.data.codeResult == 1) {
                            if (result.data.codeFailed == 1) {
                                alert("Error al registrar la Interacción.", "Mensaje de Registro Vinculación");
                            } else if (result.data.codeFailed == 2) {
                                alert("Error al registrar la Interacción Detalle.", "Mensaje de Registro Vinculación");
                            } else if (result.data.codeFailed == 3) {
                                alert("Error al Obtener datos del BRMS.", "Mensaje de Registro Vinculación");
                            } else if (result.data.codeFailed == 4) {
                                alert("Error al generar constancia.", "Mensaje de Registro Vinculación");
                            } else {
                                alert("Error al registrar datos vuelve intentarlo.", "Mensaje de Registro Vinculación");
                            }

                        } 
                    },
                    error: function (result) {
                        alert("No se puede realizar la operación.", "Mensaje de Desvinculación del Equipo Móvil");
                    }
                });
            });
        },
        fnRedirectBiometriaPost: function () {
            var that = this,
                controls = this.getControls();

            $.ValidationBiometrica.init(
                function (response) {
                    var arrDatosValidacion = JSON.parse(response);
                    that.fn_MostrarAlertasOContingencia(arrDatosValidacion);
                },
                function () {
                    alert('La ventana de Biometría ha sido cerrada, inténtalo nuevamente.');
                    return false;
                }
            );
        },
        fn_MostrarAlertasOContingencia: function (resp) {
            var that = this,
                controls = that.getControls();
            ValOk = resp.valida;
            var ConfiguracionBiometrica = Session.jsonConfigBiometria.data[0];

            if (flagProcBio == 0) {                
                if (ValOk == '0') {
                    flagProcBio = 1;
                    objReg.flagFirmaDigital = "1";
                    objReg.strHuellaMinucia = resp.huellaTemplate; // huellaDerechaWSQ 
                    objReg.strHuellaEncode = resp.varHuellaIndiceDerImagen;
                    that.fnSaveEquipmentUnlinking(objReg);
                } else if (ValOk == '3') {
                    if (ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                        flagProcBio = 1;
                        that.fnRedirectNoBiometria();
                        return;
                    } else {
                        if (ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        flagProcBio = 1;
                        that.fnSaveEquipmentUnlinking(objReg);
                    } else {
                        return;
                    }
                } else if (ValOk == '4') {
                    return;
                } else if (ValOk == '-1') {
                    return;
                } else if (ValOk == '-4') {
                    if (ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                        flagProcBio = 1;
                        that.fnRedirectNoBiometria();
                        return;
                    } else {
                        if (ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        flagProcBio = 1;
                        that.fnSaveEquipmentUnlinking(objReg);
                    } else {
                        return;
                    }
                } else if (ValOk == '-2') {
                    if (ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                        flagProcBio = 1;
                        that.fnRedirectNoBiometria();
                        return;
                    } else {
                        if (ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        flagProcBio = 1;
                        that.fnSaveEquipmentUnlinking(objReg);
                    } else {
                        return;
                    }
                } else if (ValOk == '-5') {
                    return;
                } else {
                    alert(Session.objBiometria.strMensajeValidacionBiometricaOtros);
                    return;
                }
            }            
        },
        fnRedirectBiometriaPre: function (objPre) {
            var that = this,
                controls = that.getControls();

            $.ValidationBiometrica.init(function (response) {
                var arrDatosValidacion = JSON.parse(response);
                
                that.fn_MostrarAlertasOContingenciaPre(arrDatosValidacion, objReg);

            }, function (response) {
                alert('La ventana de Biometría ha sido cerrada, inténtalo nuevamente.');

            }, objPre);
        },
        fn_MostrarAlertasOContingenciaPre: function (arrDatosValidacion, objReg) {
            var that = this,
                controls = that.getControls();
            var ValOk = "";
            var strFlagCaidaConexionRENIEC = "";
            ValOk = arrDatosValidacion["valida"];
           
            strFlagCaidaConexionRENIEC = arrDatosValidacion["flagCaidaConexionRENIEC"];
            if (flagProcBio == 0) {
                if (ValOk == '1') {
                    flagProcBio = 1;
                    objReg.flagFirmaDigital = "1";
                    objReg.strHuellaMinucia = arrDatosValidacion["huellaIzquierdaMinucia"];
                    objReg.strHuellaEncode = arrDatosValidacion["huellaBase64"];
                    that.fnSaveEquipmentUnlinking(objReg);
                }
                else if (ValOk == '2') {
                    alert(Session.objBiometria.strMensajeValidacionBiometrica2);
                }
                else if (ValOk == '3' && strFlagCaidaConexionRENIEC == "1") {
                    alert(Session.objBiometria.strMensajeValidacionBiometrica3);
                }
                else if (ValOk == '3' && strFlagCaidaConexionRENIEC != "1") {
                    alert(Session.objBiometria.strMensajeValidacionBiometrica3);
                }
                else if (ValOk == '-2') {
                    alert(Session.objBiometria.strMensajeValidacionBiometricaMenos2);
                }
                else if (ValOk == '-4') {
                    flagProcBio = 1;
                    that.fnRedirectNoBiometria();
                }
                else if (ValOk == '0') {
                    alert(Session.objBiometria.strMensajeValidacionBiometrica0);
                }
                else if (ValOk == 'NoBiometria') {
                    flagProcBio = 1;
                    that.fnRedirectNoBiometria();
                }
                else {
                    alert(Session.objBiometria.strMensajeValidacionBiometricaOtros);
                }
            }


        },
        fnRedirectNoBiometria: function () {
            var that = this,
                controls = that.getControls();

            if (typeof Session.objBiometria == 'undefined') Session.objBiometria = new Object();
            Session.objBiometria.FlagOtraPersona = '1';
            Session.objBiometria.NumeroDocumento = objReg.documentNumber;
            if (typeof Session.objBiometria.objDatosSolicitante == 'undefined') {
                Session.objBiometria.objDatosSolicitante = new Object();
            }

            Session.objBiometria.objDatosSolicitante.Nombres = objReg.firstName;
            Session.objBiometria.objDatosSolicitante.Apellidos = objReg.lastName;
            Session.objBiometria.objDatosSolicitante.Tipo = objReg.documentType;
            Session.objBiometria.objDatosSolicitante.Documento = objReg.documentNumber;

            Session.objBiometria.TransaccionOrigen = 'TRANSAC';
            Session.objBiometria.Telefono = objReg.customerTelephone;
            Session.objBiometria.MotivoVal = 'R';

            $.ValidacionNoBiometria.init({ TransaccionOrigen: 'jbq', MotivoVal: '11' }, function (response) {
                var ValOk = "";
                var resp = JSON.parse(response);
            
                ValOk = resp.valida;
                if (ValOk == '1') {
                    objReg.flagFirmaDigital = "0";
                    objReg.rp1 = resp.Questions[0]['ResponseUsuario'];
                    objReg.rp2 = resp.Questions[1]['ResponseUsuario'];
                    objReg.rp3 = resp.Questions[2]['ResponseUsuario'];
                    that.fnSaveEquipmentUnlinking(objReg);
                }
                else if (ValOk == '3') {
                    alert('Se ha cancelado la validación No Biométrica');
                }
                flagProcBio = 0;

            }, function () {
                alert('La ventana de No Biometría ha sido cerrada, inténtalo nuevamente.')
            }, objReg.flagNoBiometria);
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

        btnQuestionCall_click: function () {
            $.window.open({
                type: 'post',
                modal: false,
                title: "Preguntas de Seguridad",
                url: '/Transactions/Common/SearchQuestionsAnswer',
                width: 1300,
                height: 500,
                minimizeBox: false
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
        ReadRecordSharedFile: function (IdSession, Path) {
            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '/Transactions/CommonServices/ExistFileSharedFile',
                data: { strFilePath: Path, strIdSession: IdSession },
                success: function (result) {
                    if (result.Exist == false) {
                        alert('No se encuentra el Archivo.', "Alerta");
                    } else {
                        var params = ['height=600',
                                      'width=750',
                                      'resizable=yes',
                                      'location=yes'
                        ].join(',');
                        window.open('/Transactions/CommonServices/ShowRecordSharedFile' + "?strFilePath=" + Path + "&strIdSession=" + IdSession, "_blank", params);
                    }
                },
                error: function (ex) {
                    alert('Ocurrió un error en la previsualización de la constancia.', "Alerta");
                }
            });
        },
        fnDeclaracionNoJuradaNoBiometrica: function (objReg) {
        var width = 750;
        var height = 700;
        var left = ((screen.width - parseInt(width)) / 2);
        var top = ((screen.height - parseInt(height)) / 2);
        var phone = objReg.customerTelephone;
        var fullName = objReg.firstName + ' ' + objReg.lastName;
        var rp1 = objReg.rp1;
        var rp2 = objReg.rp2;
        var rp3 = objReg.rp3;
        var dni = objReg.documentNumber;
        var motivo = 1;
        var options = 'location=si,menubar=no,scrollbars=yes,titlebar=no,resizable=si,toolbar=no, menubar=no,width=' + width + ',height=' + height;
        window.open('/Transactions/Common/DeclaracionJuradaNoBiometria' + "?phone=" + phone + "&fullName=" + fullName + "&rp1=" + rp1 + "&rp2=" + rp2 + "&rp3=" + rp3 + "&motivo=" + motivo + "&dni=" + dni, "_blank", options);

    }
    };

    $.fn.UnlinkingEquipmentMovil = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('UnlinkingEquipmentMovil'),
                options = $.extend({}, $.fn.UnlinkingEquipmentMovil.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('UnlinkingEquipmentMovil', data);
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

    $.fn.UnlinkingEquipmentMovil.defaults = {
    }

    var i;
    if (i = 1) {
        $('#UnlinkingEquipmentMovilContainer').UnlinkingEquipmentMovil();
    }
    else {
        $('#UnlinkingEquipmentMovilContainer').UnlinkingEquipmentMovil();
        i = 1;
    }

})(jQuery);