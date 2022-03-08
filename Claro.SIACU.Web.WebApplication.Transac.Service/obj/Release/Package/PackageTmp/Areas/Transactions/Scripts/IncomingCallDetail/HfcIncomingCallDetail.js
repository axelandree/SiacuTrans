(function ($, undefined) {
    
    var HFC = {};
    HFC.CobroOCC = true;
    HFC.LstPhoneCall = [];
    HFC.SessionTransac = {};
    HFC.Igv = 0;


    var Form = function ($element, options) {

        $.extend(this, $.fn.HfcIncomingCallDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblContact: $('#lblContact', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            , txtStartDate: $('#txtStartDate', $element)
            , txtEndDate: $('#txtEndDate', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , chkGeneraOCC: $('#chkGeneraOCC', $element)
            , tblIncomingCalls: $('#tblIncomingCalls', $element)
            , tareaNotas: $('#tareaNotas', $element)
            , btnConsult: $('#btnConsult', $element)
            , btnSave: $('#btnSave', $element)
            , btnPrint: $('#btnPrint', $element)
            , btnExport: $('#btnExport', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , calendartxtStartDate: $('#calendartxtStartDate', $element)
            , calendartxtEndDate: $('#calendartxtEndDate', $element)
            , myModalLoad: $('#myModalLoad', $element)
            , txtSendMail: $('#txtSendMail', $element)
            , chkSendMail: $('#chkSendMail', $element)
            , txtOcc: $('#txtOcc', $element)
            , chkAllRecords: $('#chkAllRecords', $element)
            , lblTitle: $('#lblTitle', $element)
            , lblDireccion: $('#lblDireccion', $element)
            , lblNotaDireccion: $('#lblNotaDireccion', $element)
            , lblDepartamento: $('#lblDepartamento', $element)
            , lblPais: $('#lblPais', $element)
            , lblDistrito: $('#lblDistrito', $element)
            , lblProvincia: $('#lblProvincia', $element)
            , lblCodPlano: $('#lblCodPlano', $element)
            , lblCodUbigeo: $('#lblCodUbigeo', $element)
            , lblContrato: $('#lblContrato', $element)
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblTipoCliente: $('#lblTipoCliente', $element)
            , lblPlan: $('#lblPlan', $element)
            , lblCicloFacturacion: $('#lblCicloFacturacion', $element)
            , lblLimiteCredito: $('#lblLimiteCredito', $element)
            , IdlblCodPlano: $('#IdlblCodPlano', $element)
            , IdlblCodUbigeo: $('#IdlblCodUbigeo', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            $.datepicker.regional['10250'];
            controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy', endDate: '0' });
            controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', endDate: '0' });
            controls.btnConsult.addEvent(that, 'click', that.btnConsult_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.chkSendMail.addEvent(that, 'change', that.btnCheckSendMail_change);
            controls.chkGeneraOCC.addEvent(that, 'change', that.changeGeneraOCC);
            controls.chkAllRecords.addEvent(that, 'change', that.refreshRecords_change);

            controls.btnPrint.prop('disabled', true);
            controls.btnExport.prop('disabled', true);
            controls.btnConstancy.prop('disabled', true);
            controls.btnSave.prop('disabled', true);           
            controls.txtSendMail.hide();
            controls.IdlblCodUbigeo.hide();
            controls.IdlblCodPlano.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            that.IniGetConsultIGV();
            that.render();

        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
            that.loadSessionData();
            that.Loading();
            that.getCACDAC();
            that.getLoad();
            that.LoadtblIncomingCalls();
        },

        IniGetConsultIGV: function () {
            var that = this,
            controls = that.getControls(),
            param = {};

            param.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (response) {
                    if (response.data != null) {
                        HFC.Igv = parseFloat(response.data.igvD);
                        controls.btnConsult.prop('disabled', false);
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



        changeGeneraOCC: function () {
            var that = this, controls = this.getControls();
            if (controls.chkGeneraOCC.is(':checked')) {
                controls.txtOcc.val(HFC.IdMonto);
            } else {
                controls.txtOcc.val("0.00");
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


        ValidateDate: function () {

            var that = this, controls = this.getControls();

            if (controls.txtStartDate.val() == '') {
                alert("Debe ingresar una fecha inicio.", "Alerta");
                controls.txtStartDate.focus();
                return false;
            }
            if (controls.txtEndDate.val() == '') {
                alert("Debe ingresar la fecha fin.", "Alerta");
                controls.txtEndDate.focus();
                return false;
            }
            if (!that.ComparaFecha(controls.txtStartDate.val(), controls.txtEndDate.val(), '0')) {
                alert("La fecha inicial debe ser menor o igual que la fecha final.", "Alerta");
                return false;
            }

            return true;
        },

        loadSessionData: function () {

            var that = this,
            controls = this.getControls();
            ////console.log"Redireccionó a la Transacion");        
            HFC.SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.IDSESSION = HFC.SessionTransac.UrlParams.IDSESSION;            


            controls.lblTitle.text("DETALLE DE LLAMADAS ENTRANTES ");
            if (HFC.SessionTransac.UrlParams.SUREDIRECT == "HFC") {                
                $('#header-text').text('Detalle de Llamadas Entrantes HFC');
                controls.IdlblCodPlano.show();

            } else {                
                $('#header-text').text('Detalle de Llamadas Entrantes LTE');
                controls.IdlblCodUbigeo.show();
            }



        },

        Round: function (cantidad, decimales) {
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

        ComparaFecha: function (fechainicio, fechafin, flag) {
            var comp1, comp2;
            comp1 = fechainicio.substr(6, 4) + '' + fechainicio.substr(3, 2) + '' + fechainicio.substr(0, 2);
            comp2 = fechafin.substr(6, 4) + '' + fechafin.substr(3, 2) + '' + fechafin.substr(0, 2);
            if (flag == '0') {
                if ((comp1) > (comp2)) {
                    return false;
                }
            }
            if (flag == '1') {
                if ((comp1) >= (comp2)) {
                    return false;
                }
            }
            return true;
        },



        btnCheckSendMail_change: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.chkSendMail.is(':checked')) {
                controls.txtSendMail.show();
            } else {
                controls.txtSendMail.hide();
            }
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
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',


        getLoad: function () {

            if (HFC.SessionTransac.SessionParams.USERACCESS.userId == null || HFC.SessionTransac.SessionParams.USERACCESS.userId == "" || HFC.SessionTransac.SessionParams.USERACCESS.userId == undefined) {
                alert("Codigo de usuario vacio.", "Alerta");
                parent.window.close();
            }



            var that = this;
            var controls = this.getControls();
            var objIncomingCallRequest = {
                strIdSession: Session.IDSESSION,
                flagPlataforma: HFC.SessionTransac.SessionParams.DATASERVICE.FlagPlatform || 'C',
                fecActivacion: HFC.SessionTransac.SessionParams.DATACUSTOMER.ActivationDate,
                fechaActivacionPrepago: HFC.SessionTransac.SessionParams.DATACUSTOMER.ActivationDate,
                contratoId: HFC.SessionTransac.SessionParams.DATACUSTOMER.ContractID,
                pageAccess: HFC.SessionTransac.SessionParams.USERACCESS.optionPermissions,
                typeProduct: HFC.SessionTransac.UrlParams.SUREDIRECT
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIncomingCallRequest),
                url: that.strUrl + '/GetLoad',
                success: function (response) {

                    if (response.statusMessage != "") {
                        alert(response.statusMessage, "Alerta", function () {
                            parent.window.close();
                        });
                    }

                    HFC.IdTelefono = response.msisdn;
                    HFC.IdContrato = HFC.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
                    HFC.IdCodOpcion = HFC.SessionTransac.UrlParams.CO;
                    HFC.IdNombreCompleto = HFC.SessionTransac.SessionParams.DATACUSTOMER.FullName;
                    HFC.IdMonto = response.idMonto;
                    HFC.IdPerfilExportar = response.idPerfilExportar;
                    HFC.IdPerfilImprimir = response.idPerfilImprimir;

                    HFC.Login = HFC.SessionTransac.SessionParams.USERACCESS.login;
                    HFC.ObjIdContacto = HFC.SessionTransac.SessionParams.DATACUSTOMER.ContatCode;
                    HFC.TipificacionConsulta = response.strTipificacionConsulta;
                    HFC.TipificacionGuardar = response.strTipificacionGuardar;

                    //Monto de OCC
                    controls.txtOcc.val(HFC.IdMonto);

                    //Datos de Cliente
                    controls.lblCustomerName.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
                    controls.lblContact.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.FullName);
                    controls.lblLegalRepresentative.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
                    controls.lblIdentificationDocument.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
                    controls.lblActivationDate.html(response.FecActivacion);
                    controls.lblContrato.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.ContractID);
                    controls.lblCustomerId.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.CustomerID);
                    controls.lblTipoCliente.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
                    controls.lblPlan.html(HFC.SessionTransac.SessionParams.DATASERVICE.Plan);
                    controls.lblCicloFacturacion.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.BillingCycle);
                    controls.lblLimiteCredito.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit);


                    //Datos de Busqueda
                    controls.txtStartDate.val(response.FechaInicio);
                    controls.txtEndDate.val(response.FechaFin);
                    controls.txtSendMail.val(HFC.SessionTransac.SessionParams.DATACUSTOMER.Email);

                    
                    //Datos de Direccion
                    controls.lblDireccion.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.LegalAddress);
                    controls.lblNotaDireccion.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.Reference);
                    controls.lblDepartamento.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament);
                    controls.lblPais.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.LegalCountry);
                    controls.lblDistrito.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.District);
                    controls.lblProvincia.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.Province);
                    controls.lblCodPlano.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation);
                    controls.lblCodUbigeo.html(HFC.SessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo);

                    //Config Antiguedad de busqueda
                    controls.txtStartDate.datepicker('setStartDate', response.startDateConfig);
                    controls.txtEndDate.datepicker('setStartDate', response.startDateConfig);
                    

                    //Mensaje de Validacion IGV
                    HFC.msgIgvError = response.msgIgvError;                                 
                }
            });

        },



        getCACDAC: function () {
            var that = this,
            objCacDacType = {
                strIdSession: Session.IDSESSION
            };

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = HFC.SessionTransac.SessionParams.USERACCESS.login;

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
                            $("#ddlCACDAC").append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    $("#ddlCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    $("#ddlCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
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

        refreshRecords_change: function () {
            var that = this;
            that.LoadtblIncomingCalls(HFC.LstPhoneCall);
        },

        LoadtblIncomingCalls: function (data) {
            var controls = this.getControls();
            controls.tblIncomingCalls.DataTable({
                scrollY: "150px"
                , scrollCollapse: true
                , paging: !controls.chkAllRecords.is(':checked')
                , searching: false
                , destroy: true
                , data: data
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "NroOrd" },
                    { "data": "MSISDN" },
                    { "data": "CallDate", "type": "date", "sortable": "true" },                    
                    { "data": "CallTime" },
                    {
                        "data": "CallNumber", render: function (data, type, row) {
                            return data.substring(0, data.length - 4) + 'XXXX';
                        }
                    },
                    { "data": "CallDuration" }
                ]
            });
        },

        createDropdownCACDAC: function (response, cacdac) {
            var that = this,
            controls = that.getControls();

            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.CacDacTypes, function (index, value) {
                    $("#ddlCACDAC").html("");
                    var item = "<option value='-1'>--Seleccionar--</option>";
                    $.each(response.data.CacDacTypes, function (index, value) {
                        if (cacdac === value.Code) {
                            item += "<option selected value='" + value.Code + "'>" + value.Description + "</option>";
                        } else {
                            item += "<option value='" + value.Code + "'>" + value.Description + "</option>";
                        }
                    });
                    $("#ddlCACDAC").html(item);
                });
            }
        },

        //Consultar
        btnConsult_click: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            confirm('¿Está seguro de realizar la consulta?',
                'Confirmar',
                function (result) {
                    if (result) {
                        var objIncomingCallRequest = {
                            strIdSession: Session.IDSESSION,
                            typeProduct: HFC.SessionTransac.UrlParams.SUREDIRECT,
                            transactionName: HFC.TipificacionConsulta
                        };

                        that.Loading();
                        $.app.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            data: JSON.stringify(objIncomingCallRequest),
                            url: that.strUrl + '/LoadTypificationFixed',
                            success: function (response) {
                                if (response.success) {
                                    HFC.Tipo = response.oTypification.TIPO;
                                    HFC.ClaseDes = response.oTypification.CLASE;
                                    HFC.SubClaseDes = response.oTypification.SUBCLASE;
                                    that.processSearch();
                                } else {
                                    alert("No se reconoce la tipificación de esta transacción.", "Alerta");
                                    return;
                                }                                
                            }
                        });

                    } else {
                        alert('No se realizó consulta.', "Alerta");
                    }


                });


        },


        processSearch: function () {

            var that = this;
            var controls = that.getControls();

            var objIncomingCallRequest = {
                strIdSession: Session.IDSESSION,
                phone: HFC.IdTelefono,
                type: HFC.Tipo,
                note: controls.tareaNotas.val() || '',
                fecStart: controls.txtStartDate.val(),
                fecEnd: controls.txtEndDate.val(),
                claseDes: HFC.ClaseDes,
                subClaseDes: HFC.SubClaseDes,
                chkGeneraOCC: controls.chkGeneraOCC.is(':checked'),
                cboCACDAC: $('#ddlCACDAC option:selected').text(),
                idMonto: parseFloat(HFC.IdMonto),
                flagTfi: Session.FLAG_TFI,
                currentUser: HFC.Login,
                firstName: HFC.SessionTransac.SessionParams.DATACUSTOMER.Name,
                lastName: HFC.SessionTransac.SessionParams.DATACUSTOMER.LastName,
                documentNumber: HFC.SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber,
                referencePhone: HFC.SessionTransac.SessionParams.DATACUSTOMER.PhoneReference,
                codOpcion: HFC.IdCodOpcion,
                contratoId: HFC.IdContrato,
                chkSendMail: controls.chkSendMail.is(':checked'),
                objIdContacto: HFC.ObjIdContacto,
                razonSocial: HFC.SessionTransac.SessionParams.DATACUSTOMER.BusinessName,
                domicilio: HFC.SessionTransac.SessionParams.DATACUSTOMER.Address,
                departamento: HFC.SessionTransac.SessionParams.DATACUSTOMER.Departament,
                provincia: HFC.SessionTransac.SessionParams.DATACUSTOMER.Province,
                modalidad: HFC.SessionTransac.SessionParams.DATACUSTOMER.Modality,
                tipoDocumento: HFC.SessionTransac.SessionParams.DATACUSTOMER.DocumentType,

                customerId: HFC.SessionTransac.SessionParams.DATACUSTOMER.CustomerID,
                typeProduct: HFC.SessionTransac.UrlParams.SUREDIRECT,

                tipificacion:
                {
                    destinatario: controls.txtSendMail.val()
                }
            };


            //VALIDAR PUNTO DE ATENCION            
            if ($('#ddlCACDAC').val() == '-1') {
                alert("Seleccione un punto de atención.", "Alerta");
                return false;
            }

            //VALIDAR CORREO
            var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
            var blvalidate = regx.test(controls.txtSendMail.val());
            if (controls.chkSendMail.is(':checked')) {
                if (blvalidate == false) {
                    alert('Ingresar un correo válido.', "Alerta");
                    return false;
                }
            }


            //Validacion de fechas
            if (!that.ValidateDate()) {
                return false;
            }

            that.Loading();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIncomingCallRequest),
                url: that.strUrl + '/GetSearch',
                success: function (response) {
                    if (response.codigoRespuesta == "0") {
                        alert("Se genera tipificación informativa.", "Informativo");
                        if (response.message == 0) {
                            controls.btnSave.prop('disabled', true);                            
                            that.LoadtblIncomingCalls(response.data);
                            return false;
                        } else {
                            if (response.data.length > 0) {
                                controls.btnSave.prop('disabled', false);                                
                            }
                            HFC.LstPhoneCall = response.data;                            
                            that.LoadtblIncomingCalls(response.data);
                            sessionStorage.setItem("HFCLTELstPhoneCall", JSON.stringify(HFC.LstPhoneCall));
                        }
                    } else {
                        alert(response.descripcionRespuesta, "Alerta");
                    }
                },
                error: function () {
                    alert('Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.',"Informativo");
                }
            });
        },



        //Grabar
        btnSave_click: function (sender, arg) {


            var that = this;
            var controls = that.getControls();

            //VALIDAR PUNTO DE ATENCION
            if ($('#ddlCACDAC').val() == '-1') {
                alert("Seleccione un punto de atención", "Alerta");
                return false;
            }

            //VALIDAR CORREO
            var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
            var blvalidate = regx.test(controls.txtSendMail.val());
            if (controls.chkSendMail.is(':checked')) {
                if (blvalidate == false) {
                    alert('Ingresar un correo válido.', "Alerta");
                    return false;
                }
            }




            var objIncomingCallRequest = {
                strIdSession: Session.IDSESSION,
                typeProduct: HFC.SessionTransac.UrlParams.SUREDIRECT,
                transactionName: HFC.TipificacionGuardar
            };

            that.Loading();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIncomingCallRequest),
                url: that.strUrl + '/LoadTypificationFixed',
                success: function (response) {
                    if (response.success) {
                        HFC.Tipo = response.oTypification.TIPO;
                        HFC.ClaseDes = response.oTypification.CLASE;
                        HFC.SubClaseDesCode = response.oTypification.SUBCLASE_CODE;
                        HFC.SubClaseDes = response.oTypification.SUBCLASE;
                        that.processTransactionsIterations();
                    } else {
                        alert("No se reconoce la tipificación de esta transacción.", "Alerta");
                        return false;
                    }
                },
                error: function () {
                    alert('Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.');                    
                }
            });
        },

        processTransactionsIterations: function () {

            var controls = this.getControls();
            var that = this;

            //Validar IGV si Generar OCC
            if (controls.chkGeneraOCC.is(':checked')) {
                if (HFC.Igv == 0) {
                    alert(HFC.msgIgvError, "Alerta");
                    return false;
                }
            }

            var objIncomingCallRequest = {
                strIdSession: Session.IDSESSION,
                phone: HFC.IdTelefono,
                type: HFC.Tipo,
                note: controls.tareaNotas.val() || '',
                fecStart: controls.txtStartDate.val(),
                fecEnd: controls.txtEndDate.val(),
                claseDes: HFC.ClaseDes,
                subClaseDes: HFC.SubClaseDes,
                chkGeneraOCC: controls.chkGeneraOCC.is(':checked'),
                cboCACDAC: $('#ddlCACDAC option:selected').text(),
                idMonto: parseFloat(HFC.IdMonto),
                idMontoConIGV: parseFloat(HFC.IdMonto),
                idMontoSinIGV: that.Round(parseFloat(HFC.IdMonto) - (parseFloat(HFC.Igv) * parseFloat(HFC.IdMonto)), 2).toFixed(2),
                flagTfi: Session.FLAG_TFI,
                currentUser: HFC.Login,
                firstName: HFC.SessionTransac.SessionParams.DATACUSTOMER.Name,
                lastName: HFC.SessionTransac.SessionParams.DATACUSTOMER.LastName,
                documentNumber: HFC.SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber,
                referencePhone: HFC.SessionTransac.SessionParams.DATACUSTOMER.PhoneReference,
                codOpcion: HFC.IdCodOpcion,
                contratoId: HFC.IdContrato,
                chkSendMail: controls.chkSendMail.is(':checked'),
                objIdContacto: HFC.ObjIdContacto,
                customerId: HFC.SessionTransac.SessionParams.DATACUSTOMER.CustomerID,
                LegalAgent: HFC.SessionTransac.SessionParams.DATACUSTOMER.LegalAgent,
                tipoDocumento: HFC.SessionTransac.SessionParams.DATACUSTOMER.DocumentType,
                SubClaseDesCode: HFC.SubClaseDesCode,
                tipificacion:
                {
                    destinatario: controls.txtSendMail.val()
                }
            };

            HFC.cboCACDAC = objIncomingCallRequest.cboCACDAC;

            if (objIncomingCallRequest.chkSendMail && controls.txtSendMail.val() == '') {
                alert('Ingresar Email.', "Alerta");
                return false;
            }


            that.Loading();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIncomingCallRequest),
                url: that.strUrl + '/ProcessTransactionsIterations',
                error: function (ex) {
                    alert("Ha ocurrido un problema en la transacción, por favor volver a intentarlo más tarde.","Alerta");
                },
                success: function (response) {
                    if (response.data.estateButton == "Guardar") {
                        controls.btnSave.prop('disabled', true);
                        controls.btnPrint.prop('disabled', false);
                        controls.btnExport.prop('disabled', false);
                        controls.btnConstancy.prop('disabled', false);
                        controls.btnConsult.prop('disabled', true);
                        controls.chkGeneraOCC.prop('disabled', false);                        
                        controls.tareaNotas.prop('disabled', true);
                        alert(response.data.message, "Informativo", function () {
                            if (response.messageOCC != "") {
                                alert(response.messageOCC, "Informativo");
                            }
                        });

                    }
                    else {
                        alert(response.data.message, "Alerta");
                    }
                }
            });
        },
        //Imprimir
        btnPrint_click: function () {
            var controls = this.getControls();
            var that = this;

            if (HFC.IdPerfilImprimir != "1") {
                var param = {
                    "strIdSession": Session.IDSESSION,
                    'pag': '5',
                    'opcion': 'BUS',                    
                    'telefono': HFC.IdTelefono
                };

                ValidateAccess(that, controls, 'IMP', 'strLlamadasEntImprimir', '4', param, 'Fixed');
                return;
            } else {
                that.CallPrintIncomingCallDetail();
            }

            //that.CallPrintIncomingCallDetail();

        },

        btnExport_click: function (sender, arg) {

            var that = this;
            var controls = that.getControls();



            if (HFC.IdPerfilExportar != "1") {
                var param = {
                    "strIdSession": Session.IDSESSION,
                    'pag': '5',
                    'opcion': 'BUS',                  
                    'telefono': HFC.IdTelefono
                };
                ValidateAccess(that, controls, 'EXP', 'strLlamadasEntExportar', '4', param, 'Fixed');
                return;
            } else {
                that.CallExportIncomingCallDetail();
            }

            //that.CallExportIncomingCallDetail();

        },


        CallPrintIncomingCallDetail: function () {

            var controls = this.getControls();
            var that = this;

            var parm = '?phone=' + HFC.IdTelefono +
                                       '&cacdac=' + $('#ddlCACDAC option:selected').text() +
                                       '&fecStart=' + controls.txtStartDate.val() +
                                       '&fecEnd=' + controls.txtEndDate.val() +
                                       '&fullName=' + HFC.IdNombreCompleto;


            var view = that.strUrl + '/IncomingCallDetailPrint' + parm;
            window.open(view, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=750, height=600');
            
        },

        CallExportIncomingCallDetail: function () {

            var that = this;
            var controls = that.getControls();

            if (HFC.LstPhoneCall.length == 0) {
                alert('No Hay Registros.', "Alerta");
                return false;
            }

            that.ExportResult();
        },

        ExportResult: function () {
            var that = this;
            var controls = this.getControls();
            var strUrlModal = that.strUrl + '/GetIncomingCallExportExcel';
            var strUrlResult = '/Transactions/CommonServices/DownloadExcel';
            var objExportType = {};
            objExportType.idsession = Session.IDSESSION;
            objExportType.phone = HFC.IdTelefono;
            objExportType.fecStart = controls.txtStartDate.val();
            objExportType.fecEnd = controls.txtEndDate.val();
            objExportType.fullName = HFC.IdNombreCompleto;
            objExportType.cacdac = $('#ddlCACDAC option:selected').text();
            objExportType.typeProduct = HFC.SessionTransac.UrlParams.SUREDIRECT
            objExportType.LstPhoneCall = HFC.LstPhoneCall;

            that.Loading();

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(objExportType),
                success: function (path) {
                    window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ReporteDeLlamadas.xlsx";
                }
            });
        },

        btnConstancy_click: function (sender, arg) {
            var that = this;
            var controls = that.getControls();

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: that.strUrl + '/GetIncomingCallConstancy',
                data: {},
                success: function (response) {
                    ReadRecordSharedFile(Session.IDSESSION, response.strutaConstancy);
                }
            });
        },


        btnClose_click: function (sender, arg) {
            parent.window.close();
        },


        strUrl: window.location.protocol + '//' + window.location.host + '/Transactions/HFC/HfcIncomingCallDetail'
    }



    $.fn.HfcIncomingCallDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HfcIncomingCallDetail'),
                options = $.extend({}, $.fn.HfcIncomingCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HfcIncomingCallDetail', data);
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

    $.fn.HfcIncomingCallDetail.defaults = {
    }

    $('#divBody').HfcIncomingCallDetail();

})(jQuery);



function CloseValidation(obj, pag, controls) {
    obj.hidAccion = 'G';
    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = HiddenPageAuth.hidDescripcionProceso_Validar;
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '') {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function FC_GrabarCommit(pag, controls) {
    HiddenPageAuth.hidAccion = '';
    if (HiddenPageAuth.hidOpcion == 'EXP') {
        pag.CallExportIncomingCallDetail();
    } else if (HiddenPageAuth.hidOpcion == 'IMP') {
        pag.CallPrintIncomingCallDetail();
    }
    HiddenPageAuth.hidOpcion = "";
};

function alpha(e) {
    var k = document.all ? e.keyCode : e.which;
    if (k === 124)
        return false;
    else
        return true;    
}