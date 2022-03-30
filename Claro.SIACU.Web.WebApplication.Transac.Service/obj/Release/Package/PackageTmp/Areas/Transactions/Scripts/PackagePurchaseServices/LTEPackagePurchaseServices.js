(function ($, undefined) {

    var modelPurchaseService = {}
    var SessionValidate = function () { };
    SessionValidate.strMsgTranGrabSatis = "La transacción se ha grabado satisfactoriamente.";
    SessionValidate.strMensajeDeError = "No se pudo ejecutar la transacción. Informe o vuelva a intentar.";
    SessionValidate.strMensajeSeleTipoVenta = "Seleccione Tipo de Venta.";
    SessionValidate.strMensajeSeleCACDAC = "Seleccione Punto de atención.";
    SessionValidate.strMensajePqtVelocidadDegradacion = "Seleccione Paquete Adicional.";
    var Form = function ($element, options) {
        $.extend(this, $.fn.LTEPackagePurchaseServices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            //labels
            , lblContrato: $("#lblContrato", $element)
            , lblTipoCliente: $("#lblTipoCliente", $element)
            , lblContacto: $("#lblContacto", $element)
            , lblCodUbigeo: $("#lblCodUbigeo", $element)
            , lblCliente: $("#lblCliente", $element)
            , lblPlan: $("#lblPlan", $element)
            , lblFechaActivacion: $("#lblFechaActivacion", $element)
            , lblEstLinea: $("#lblEstLinea", $element)
            , lblTipAcu: $("#lblTipAcu", $element)
            , lblActivo: $("#lblActivo", $element)
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , lblCodPlano: $("#lblCodPlano", $element)
            , lblPais: $("#lblPais", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblTipAcuerdo: $("#lblTipAcuerdo", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , lblTitle: $('#lblTitle', $element)
            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDNI_RUC: $('#lblDNI_RUC', $element)
            , lblRepren_Legal: $('#lblRepren_Legal', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblLimiteCred: $('#lblLimiteCred', $element)
            , divReglas: $('#divReglas', $element)
            //textBox
            , txtPlanVelocidadDegradada: $("#txtPlanVelocidadDegradada", $element)
            , txtMotivoDegradacion: $("#txtMotivoDegradacion", $element)
            , txtPqtVelocidad: $("#txtPqtVelocidad", $element)
            , txtEmail: $("#txtEmail", $element)
            , txtNote: $('#txtNote', $element)

            //comboBox
            , cboPuntoAtencion: $("#cboPuntoAtencion", $element)
            , cboTipoVenta: $("#cboTipoVenta", $element)

            //CheckBox
            , chkEmail: $("#chkEmail", $element)

            //Botones
            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)

            //Tablas
            , divTblPackageRV: $("#divTblPackageRV", $element)
            , txtSaldoPuntos: $("#txtSaldoPuntos", $element)

        });
    };
    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_Click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.cboTipoVenta.addEvent(that, 'change', that.cboTipoVenta_Change);

            that.initTipoVenta();
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        render: function () {
            var that = this,

            control = that.getControls();
            control.btnConstancia.prop('disabled', true);
            control.txtNote.prop('disabled', true);
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            console.log(SessionTransac)

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == "" ? "1" : SessionTransac.UrlParams.IDSESSION;

            that.Loading();
            that.loadData();
            that.ConsultarPCRFDegradacion();
            //that.ConsultarClaroPuntos();
            that.ConsultarPaqDisponibles();
            that.LoadTipificacion();
            that.initCacDat();
            $("#divPqtVelocidad").hide();
            $("#txtMotivoDegradacion").prop('disabled', true);
            $("#chkEmail").prop("checked", true)
        },
        ConsultarPCRFDegradacion: function () {
            
            var ContractID = Session.DATACUSTOMER.ContractID;//Session.DATACUSTOMER.Telephone;

            console.log("Session.DATACUSTOMER====>" + Session.DATACUSTOMER);
            console.log("Session.DATACUSTOMER.strphonespeed====>" + Session.DATACUSTOMER.strphonespeed);
            var that = this,
                           controls = that.getControls(),
                           objConsultarPCRFDegradacion = {
                               IdSession: Session.IDSESSION,
                               ContractIDSession: ContractID
                           };

         
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(objConsultarPCRFDegradacion),
                url: '/Transactions/LTE/PackagePurchaseServices/ConsultarPCRFDegradacion',
                success: function (response) {
                    controls.btnGuardar.prop('disabled', true);
                    if (response.codRespuesta = '0') {
                        controls.txtPlanVelocidadDegradada.val(response.DegradacionPlan);
                        controls.txtMotivoDegradacion.val(strMotivoDegradacion);
                        controls.txtPqtVelocidad.val(response.DegradacionPaquetes);

                        if (response.DegradacionPaquetes != '') {                      
                            $("#divPqtVelocidad").show();                         
                        }

                        if ((response.DegradacionPlan == 'SI') && (response.DegradacionPaquetes == 'SI' || response.DegradacionPaquetes == ''))
                        {
                            controls.btnGuardar.prop('disabled', false);
                        }
                        else
                        {
                            alert(strNoPresentaVelocidadDegradada);
                            controls.btnGuardar.prop('disabled', true);
                            $("LTEPackagePurchaseServices table, #LTEPackagePurchaseServices textarea, #LTEPackagePurchaseServices input, #LTEPackagePurchaseServices select").attr('disabled', true);
                        }
                        
                        
                    } else {
                        controls.txtPlanVelocidadDegradada.val('-');
                        controls.txtMotivoDegradacion.val(strMotivoDegradacion);
                        controls.txtPqtVelocidad.val('-');
                    }
                }
            });

        },

        ConsultarClaroPuntos: function () {

            var that = this,
                           controls = that.getControls(),
                           objConsultarClaroPuntos = {
                               IdSession: Session.IDSESSION,
                               tipoDocumento: Session.DATACUSTOMER.DocumentType == "DNI" ? "2" : "3",
                               numeroDocumento: Session.DATACUSTOMER.DocumentNumber
                           };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(objConsultarClaroPuntos),
                url: '/Transactions/LTE/PackagePurchaseServices/ConsultarClaroPuntos',
                success: function (response) {
                    if (response != null) {
                        if (response.data.MessageResponse.Body.codigoRespuesta == '0')
                            $("#txtSaldoPuntos").val(response.data.MessageResponse.Body.saldo);
                        else
                            $("#txtSaldoPuntos").val('0');
                    }
                    else
                        $("#txtSaldoPuntos").val('0');


                }
            });

        },

        ConsultarPaqDisponibles: function () {

            var that = this,
                           controls = that.getControls(),
                           oCustomer = Session.DATACUSTOMER,
                           objConsultarPaqDisponibles = {
                               IdSession: Session.IDSESSION,
                               idCategoria: "783",//<-----------------------
                               idContrato: Session.DATACUSTOMER.ContractID,
                               codigoCategoria: "24",//<-----------------------
                               prepagoCode: '',
                               tmCode:  Session.DATASERVICE.CodePlanTariff 
                           };
            var list = [];
            var ListPaqDisponible = new Array();
            var BillingCycle = (oCustomer.objPostDataAccount.BillingCycle == null) ? '' : oCustomer.objPostDataAccount.BillingCycle;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(objConsultarPaqDisponibles),
                url: '/Transactions/LTE/PackagePurchaseServices/ConsultarPaqDisponibles',
                success: function (response) {
                    if (response.data.MessageResponse.Body.defaultServiceResponse.idRespuesta = '0') {

                        if (response.data.MessageResponse.Body.listaProductosDeCompra != null) {
                            debugger;
                            var day = parseInt(response.day);
                            var month = parseInt(response.month) - 1;
                            var year = parseInt(response.year);


                            var BillingCycle_date = new Date(year, month, BillingCycle);
                            var fecVigencia;

                            if (day > parseInt(BillingCycle)) {
                                if (month >= 11) {
                                    month = 0;
                                }
                                else {
                                    month = month + 1;
                                }
                                fecVigencia = new Date(year, parseInt(month), BillingCycle);
                            }
                            else {
                                fecVigencia = BillingCycle_date;
                            }

                            $.each(response.data.MessageResponse.Body.listaProductosDeCompra, function (index1, value1) {

                                list.PAQUETE = value1.catvTitulo != '' ? value1.catvTitulo : '-';
                               if (value1.listaCaracteristicasProducto != null) {
                                   list.PRECIO = value1.listaCaracteristicasProducto[2].nombre != '' ? value1.listaCaracteristicasProducto[2].nombre : '-';
                                   list.MBINCLUIDOS = value1.listaCaracteristicasProducto[0].nombre != '' ? value1.listaCaracteristicasProducto[0].nombre.replace('ADICIONALES', '') : '-';
                               }
                               var fecActual = new Date();
                               //var fecVigencia = new Date();
                               //var dias = parseInt(value1.vigencia);
                                //fecVigencia.setDate(fecActual.getDate() + dias);
                               var dd = fecVigencia.getDate();
                               var mm = fecVigencia.getMonth() + 1;

                               var yyyy = fecVigencia.getFullYear();
                               if (dd < 10) {
                                   dd = '0' + dd;
                               }
                               if (mm < 10) {
                                   mm = '0' + mm;
                               }
                               var fechaVigenciaFormat = dd + '/' + mm + '/' + yyyy;

                               //list.VIGENCIA = fecVigencia.toLocaleDateString("es-ES"); //('0' + fecVigencia.getDate()).slice(-2) + '/' + ('0' + (fecVigencia.getMonth() + 1)).slice(-2) + '/' + fecVigencia.getFullYear();
                               list.VIGENCIA = fechaVigenciaFormat;

                               ListPaqDisponible.push(list);
                            });

                        }
                        else {
                            alert(strMsjNoPaquetesDisponibles)
                        }
                    }
                    else {
                        alert("Error al consultar Paquetes Disponible.")
                    }
                    that.SetDataToTable(ListPaqDisponible);
                }
            });

        },

        SetDataToTable: function (listPaquetes) {
            var that = this,
            controls = that.getControls();


            var listPaquetesFinal = new Array();


            for (var i in listPaquetes) {
                listPaquetes[i].PAQUETE;
                listPaquetes[i].VIGENCIA;
                listPaquetes[i].PRECIO = listPaquetes[i].PRECIO;
                listPaquetes[i].MBINCLUIDOS;
                listPaquetesFinal.push(listPaquetes[i]);
            }

            var table = $("#tblPackagePurchaseServices").dataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "select": {
                    "style": "os",
                    "info": false
                },
 
                data: listPaquetesFinal,
                language: {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen paquetes disponibles",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": null },
                    { "data": "PAQUETE" }, 
                    { "data": "VIGENCIA" }, 
                    { "data": "PRECIO" }, 
                    { "data": "MBINCLUIDOS" }
                ],
                "columnDefs": [{
                    "orderable": false,
                    "className": 'select-radio',
                    "targets": 0,
                    "defaultContent": "",
                    "visible": true

                }
                ]
            });


            $("#tblPackagePurchaseServices").on('click', 'tr', function (e) {
                e.preventDefault();
                var paquete, vigencia, precio, MBIncluidos;
                $(this).children("td").each(function (i) {
                    switch (i) {
                        case 1:
                            paquete = $(this).text();
                            break;
                        case 2:
                            vigencia = $(this).text();
                            break;
                        case 3:
                            precio = $(this).text();
                            break;
                        case 4:
                            MBIncluidos = $(this).text();
                            break;
                    }
                    
                })
                modelPurchaseService.PqtVelocidadDegradacion = paquete;
                modelPurchaseService.strVigencia = vigencia;
                modelPurchaseService.strPrecioPaquete = precio.replace('S/ ', '').trim();
                modelPurchaseService.strPrecioPaquete = modelPurchaseService.strPrecioPaquete.replace('S/. ', '').trim();
                modelPurchaseService.strMBIncluidos = MBIncluidos;
                modelPurchaseService.strPlanBase = Session.DATASERVICE.CodePlanTariff;
                var txtNoteDesc = "Compra Paquete Fija | Paquete Datos:" + paquete + " | Vigencia: " + vigencia + " | Precio:" + precio.replace('S/.', '') + "| MB Incluidos:" + MBIncluidos + " | Tipo de venta:" + $("#cboTipoVenta option:selected").text() + ". El Cliente aceptó las condiciones incluidas en la Declaración de Aceptación.";
                $("#txtNote").val(txtNoteDesc);
                
            });

        },

        LoadTipificacion: function () {

            var that = this,
                           controls = that.getControls(),
                           objLoadTipificacion = {
                               strIdSession: Session.IDSESSION,
                               strTransactionName: strCodTransPackagePurchaseFixedServLTE,
                               strType: 'LTE'
                           };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(objLoadTipificacion),
                url: '/Transactions/CommonServices/GetTypification',
                success: function (response) {
                    var list = response.ListTypification;
                    if (response.ListTypification.length > 0) {
                        modelPurchaseService.CLASE = list[0].CLASE;
                        modelPurchaseService.CLASE_CODE = list[0].CLASE_CODE;
                        modelPurchaseService.SUBCLASE = list[0].SUBCLASE;
                        modelPurchaseService.SUBCLASE_CODE = list[0].SUBCLASE_CODE;
                        modelPurchaseService.TIPO = list[0].TIPO;
                        modelPurchaseService.TIPO_CODE = list[0].TIPO_CODE;
                        that.getBusinessRules(list[0].SUBCLASE_CODE);
                    }
                }
            });

        },

        getBusinessRules: function (SubClaseCode) {
            var that = this, controls = that.getControls();
            controls.divReglas.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/Transactions/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: Session.IDSESSION,
                    strSubClase: SubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divReglas.html(list[0].REGLA);
                        }
                    }

                }
            });
        },

        initTipoVenta: function () {

            var that = this,
                controls = that.getControls(),
                value = strTipoVentaReinicia;

            arrayCod = value.split('|');


            for (var i in arrayCod) {
                controls.cboTipoVenta.append($('<option>', { value: i, html: arrayCod[i] }));
            }
            $("#cboTipoVenta option[value=" + 1 + "]").attr("selected", true);
            $("#IdShowHideSaldoPuntos").hide();
        },

        initCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {
                    strIdSession: Session.IDSESSION
                };

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.Login;

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
                        cache: false,
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboPuntoAtencion.append($('<option>', { value: '-1', html: 'SELECCIONAR' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboPuntoAtencion.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboPuntoAtencion.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboPuntoAtencion option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        cboTipoVenta_Change: function () {

            var that = this,
                controls = that.getControls(),
                value = $('#cboTipoVenta option:selected').val(),
                arrayCod = value.split('|');

            if (arrayCod[0] == strFacturada || arrayCod[0] == strClaroPuntos) {

                if (arrayCod[0] == strFacturada) {
                    $("#divTblPackageRV").show();
                    $("#IdShowHideSaldoPuntos").hide();

                } else if (arrayCod[0] == strClaroPuntos) {
                    $("#divTblPackageRV").show();
                    $("#IdShowHideSaldoPuntos").show();

                    if ($("#txtSaldoPuntos").val() == '0') {
                        $("#divTblPackageRV").hide();
                        controls.btnGuardar.prop('disabled', true);
                        alert(strMsjClaroPuntos);
                    }
                }
            }
        },


        loadData: function () {
            var that = this,
                controls = this.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE,
                oUserAccess = Session.USERACCESS;

            controls.lblTitle.text("COMPRA DE PAQUETES FIJA");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            //********** Datos del Cliente ***********/
            controls.lblContrato.html((oCustomer.ContractID == null) ? '' : oCustomer.ContractID);
            controls.lblCustomerID.html((oCustomer.CustomerID == null) ? '' : oCustomer.CustomerID);
            controls.lblTipoCliente.html((oCustomer.CustomerType == null) ? '' : oCustomer.CustomerType);
            controls.lblCliente.html((oCustomer.BusinessName == null) ? '' : oCustomer.BusinessName);
            controls.lblContacto.html((oCustomer.FullName == null) ? '' : oCustomer.FullName);
            controls.lblDNI_RUC.html((oCustomer.DNIRUC == null) ? '' : oCustomer.DNIRUC);
            controls.lblRepren_Legal.html((oCustomer.LegalAgent == null) ? '' : oCustomer.LegalAgent);
            controls.lblPlan.html((oDataService.Plan == null) ? '' : oDataService.Plan);
            controls.lblFechaActivacion.html((oCustomer.ActivationDate == null) ? '' : oCustomer.ActivationDate);
            controls.lblCicloFact.html((oCustomer.objPostDataAccount.BillingCycle == null) ? '' : oCustomer.objPostDataAccount.BillingCycle);
            controls.lblLimiteCred.html((oCustomer.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + oCustomer.objPostDataAccount.CreditLimit);
            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((oCustomer.InvoiceAddress == null) ? '' : oCustomer.InvoiceAddress);
            controls.lblNotasDirec.html((oCustomer.LegalUrbanization == null) ? '' : oCustomer.LegalUrbanization);
            controls.lblPais.html((oCustomer.Country == null) ? '' : oCustomer.Country);
            controls.lblDepartamento.html((oCustomer.InvoiceDepartament == null) ? '' : oCustomer.InvoiceDepartament);
            controls.lblProvincia.html((oCustomer.InvoiceProvince == null) ? '' : oCustomer.InvoiceProvince);
            controls.lblDistrito.html((oCustomer.InvoiceDistrict == null) ? '' : oCustomer.InvoiceDistrict);
            controls.lblCodUbigeo.html((oCustomer.InvoiceUbigeo == null) ? '' : oCustomer.InvoiceUbigeo);
            controls.txtEmail.val((oCustomer.Email == null) ? '' : oCustomer.Email);
            //********** Reglas atencion ***********/
        },

        chkEmail_Change: function (sender, arg) {
            var that = this,
                controls = that.getControls(),
                chkEmail = controls.chkEmail;

            if (chkEmail[0].checked == true) {
                controls.txtEmail.css("display", "block");
            } else {
                controls.txtEmail.css("display", "none");
            }
        },

        btnConstancia_Click: function () {

            var that = this;
            var PDFRoute = modelPurchaseService.FullPathPDF;
            var IdSession = Session.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        pad: function (s) { return (s < 10) ? '0' + s : s; },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

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

        btnGuardar_Click: function () {
            var that = this, controls = that.getControls();

            var validacion = that.ValidateControl();

            if (validacion) {
                confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                    that.LoadPag();
                    that.SaveTransactionPackagePurchaseService();
                }, function () {
                    return false;
                });
            }

        },

        SaveTransactionPackagePurchaseService: function () {
            var that = this,
                oCustomer = Session.DATACUSTOMER,
                oUserAccess = Session.USERACCESS,
                controls = that.getControls();

            modelPurchaseService.strIdSession = Session.IDSESSION;
            modelPurchaseService.strPuntoAtencion = $("#cboPuntoAtencion option:selected").text();
            modelPurchaseService.strNombreCliente = oCustomer.BusinessName;
            modelPurchaseService.strContacto = oCustomer.FullName;
            modelPurchaseService.strRepresentanteLegal = oCustomer.LegalAgent;
            modelPurchaseService.strTipoDocIdentidad = oCustomer.DocumentType;
            modelPurchaseService.strNumeroDoc = oCustomer.DNIRUC;
            modelPurchaseService.strTelefono = oCustomer.Telephone;
            modelPurchaseService.strNumeroCuenta = oCustomer.Account;
            modelPurchaseService.strNumeroServicio = oCustomer.Telephone;
            modelPurchaseService.strTipoCliente = oCustomer.CustomerType;
            //Datos del Ascesor
            modelPurchaseService.CurrentUser = oUserAccess.login;
            modelPurchaseService.strCodigoAsesor = oUserAccess.login;
            modelPurchaseService.strNombreAsesor = oUserAccess.fullName;
            //Datos para registrar en la interaccion
            modelPurchaseService.strContrato = oCustomer.ContractID;
            modelPurchaseService.strCustomerID = oCustomer.CustomerID;
            modelPurchaseService.strPlan = oCustomer.Plan;
            modelPurchaseService.strFechaActivacion = oCustomer.ActivationDate;
            modelPurchaseService.strCicloFacturacion = oCustomer.objPostDataAccount.BillingCycle;
            modelPurchaseService.strLimiteCrediticio = oCustomer.objPostDataAccount.CreditLimit;
            modelPurchaseService.strDireccion = oCustomer.InvoiceAddress;
            modelPurchaseService.strNotasDireccion = oCustomer.LegalUrbanization;
            modelPurchaseService.strPais = oCustomer.Country;
            modelPurchaseService.strDepartamento = oCustomer.InvoiceDepartament;
            modelPurchaseService.strProvincia = oCustomer.InvoiceProvince;
            modelPurchaseService.strDistrito = oCustomer.InvoiceDistrict;
            modelPurchaseService.strCodUbigeo = oCustomer.InvoiceUbigeo;

            // Datos Modificados
            modelPurchaseService.strNotas = $("#txtNote").val();


            if (controls.cboTipoVenta.val() == '1') {
                modelPurchaseService.strTipoVenta = $("#cboTipoVenta option:selected").text();
            }
            else {
                modelPurchaseService.strTipoVenta = $("#cboTipoVenta option:selected").text();
                modelPurchaseService.strSaldoPuntos = $("#txtSaldoPuntos").val();
            }

            modelPurchaseService.strEmailCliente = $("#txtEmail").val();

            if (controls.chkEmail[0].checked) {
                modelPurchaseService.ChkEmail = 'true';
            }
            else {
                modelPurchaseService.ChkEmail = 'false';
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(modelPurchaseService),
                url: that.strUrlLTEPackPur + '/SaveTransaccionLTEPackagePurchase',
                success: function (response) {
                    if (response != null) {

                        if (response.data.strErrorMessage == '') {
                            alert("Se registro de forma correcta la transaccion");
                            controls.btnGuardar.prop('disabled', true);
                            $("#LTEPackagePurchaseServices table, #LTEPackagePurchaseServices textarea, #LTEPackagePurchaseServices input, #LTEPackagePurchaseServices select").attr('disabled', true);
                            if (response.data.strFullPathPDF != '') {
                                controls.btnConstancia.prop('disabled', false);
                                 
                                //Onbase
                                modelPurchaseService.strCodOnBase = response.data.strCodOnBase;
                                modelPurchaseService.FullPathPDF = response.data.strFullPathPDF;
                                modelPurchaseService.strCasoInteraccion = response.data.strCasoInteraccion;

                                console.log(modelPurchaseService);
                                if (modelPurchaseService.strCodOnBase !='' &&  modelPurchaseService.strCodOnBase != null) {
                                    that.GenerarInteraccionPlus();
                                }
                            } else
                            {
                                controls.btnConstancia.prop('disabled', true);
                            }

                            } else {
                            alert(response.data.strErrorMessage);
                            }
                        }
                }
            });


        },
        AboveZero: function (i) {

            if (i < 10) {
                i = '0' + i;
            }
            return i;
        },
        GetKeyConfig: function (KeyName) {
            var that = this, controls = that.getControls();
            var objFilterType = {
                strIdSession: Session.IDSESSION,
                strfilterKeyName: KeyName
            };

            var value = '';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: 'json',
                url: that.strUrlLTEPackPur + '/GetKeyConfig',
                data: JSON.stringify(objFilterType),
                success: function (response) {
                    if (response.data != null)
                        value = response.data;
                }
            }
            );
            return value;
        },

  
        GenerarInteraccionPlus: function () {
            console.log(modelPurchaseService);

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/LTE/PackagePurchaseServices/GetInsertInteractionTemplate',
                data: JSON.stringify(modelPurchaseService),
                complete: function () {
                },
                success: function (response) {
                    console.log('response GenerarInteraccionPlus');
                    console.log(response);
                },
                error: function (msger) {
                }
            });

        },

        ValidateControl: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboTipoVenta.val() == "-1" || controls.cboTipoVenta.val() == "0") {
                alert(SessionValidate.strMensajeSeleTipoVenta, "Alerta")
                return false;
            }
            if (modelPurchaseService.PqtVelocidadDegradacion == '') {
                alert(SessionValidate.strMensajePqtVelocidadDegradacion, "Alerta")
                return false;
            }

            if (controls.cboPuntoAtencion.val() == "-1" || controls.cboPuntoAtencion.val() == "0") {
                alert(SessionValidate.strMensajeSeleCACDAC, "Alerta")
                return false;
            }

            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert("Ingresar E-mail", 'Alert', function () {
                        controls.txtEmail.focus();
                    });
                    return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert("Ingresar E-mail válido", 'Alerta', function () {
                        controls.txtEmail.select();
                    }); return false;
                }
            }
            return true;
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },

        maximizarWindow: function () {

            window.moveTo(0, 0);
            window.resizeTo(screen.width, screen.height);

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

        btnCerrar_Click: function () {

            parent.window.close();
        },

        LoadPag: function () {
            var that = this, controls = this.getControls();
            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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

        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        strUrlLTEPackPur: '/Transactions/PackagePurchaseServices'
    };

    $.fn.LTEPackagePurchaseServices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('LTEPackagePurchaseServices'),
                options = $.extend({}, $.fn.LTEPackagePurchaseServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LTEPackagePurchaseServices', data);
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

    $.fn.LTEPackagePurchaseServices.defaults = {
    }

    $('#divBody').LTEPackagePurchaseServices();
})(jQuery);