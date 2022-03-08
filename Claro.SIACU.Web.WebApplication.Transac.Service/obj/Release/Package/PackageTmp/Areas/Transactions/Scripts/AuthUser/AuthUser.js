var HiddenPageAuth = {};

function ValidateAccess(pag, controls, action, opcion, pagina, param, product) {
    HiddenPageAuth.hidOpcion = action;
    //document.getElementById("hidOpcion").value = action;
    var strUrlModal = location.protocol + '//' + location.host + '/Transactions/AuthUser/Auth/AuthUserHtml';
    confirm('Se requiere autorización del Jefe/Supervisor.', 'Confirmar', function () {
        $.window.open({
            modal: true,
            type: 'post',
            title: "SIACUNICO - Autenticación",
            url: strUrlModal,
            data: {},
            width: 360,
            height: 310,
            buttons: {
                Validar: {
                    id: 'btnSigInAuth',
                    click: function () {
                        if (AuthenticationUser(pag, controls, product)) {
                            this.close();
                        }; 
                    }
                },
                Cancelar: {
                    id: 'btnCancelAuth',
                    click: function (sender, args) {
                        this.close();
                    }
                }
            },
            complete: function () {
                var strUrlController = '/Transactions/CommonServices/UserValidate_PageLoad';
                //strIdSession, transaction, pag, paginadcm, monto, opcion, modalidad, tipo, motivo, telefono, loginS, co, migracion, descripcion, transaccion, detEntAccion, tipotx, unidad, hidOpcion, hidAccion               
                
                $.ajax({
                    type: 'POST',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    url: strUrlController,
                    data: JSON.stringify(param),
                    success: function (response) {
                        //$('#hidPagina_Validar').val(response.hidPagina);
                        HiddenPageAuth.hidPagina_Validar = response.hidPagina;
                        
                        //$('#hidPagDCM_Validar').val(response.hidPagDCM);
                        HiddenPageAuth.hidPagDCM_Validar = response.hidPagDCM;

                        //$('#hidMonto_Validar').val(response.hidMonto);
                        HiddenPageAuth.hidMonto_Validar = response.hidMonto;

                        //$('#hidUnidad_Validar').val(response.hidUnidad);
                        HiddenPageAuth.hidUnidad_Validar = response.hidUnidad;

                        //$('#hidOpcion_Validar').val(response.hidOpcion);
                        HiddenPageAuth.hidOpcion_Validar = response.hidOpcion;

                        //$('#hidModalidad_Validar').val(response.hidModalidad);
                        HiddenPageAuth.hidModalidad_Validar = response.hidModalidad;

                        //$('#hidLogin_Validar').val(response.hidLogin);
                        HiddenPageAuth.hidLogin_Validar = response.hidLogin;

                        //$('#hidCO_Validar').val(response.hidCO);
                        HiddenPageAuth.hidCO_Validar = response.hidCO;

                        //$('#hidMigracion_Validar').val(response.hidMigracion);
                        HiddenPageAuth.hidMigracion_Validar = response.hidMigracion;

                        //$('#hidDescripcionProceso_Validar').val(response.hidDescripcionProceso);
                        HiddenPageAuth.hidDescripcionProceso_Validar = response.hidDescripcionProceso;

                        //$('#hidConcepto_Validar').val(response.hidConcepto);
                        HiddenPageAuth.hidConcepto_Validar = response.hidConcepto;

                        //$('#hidAccionDetEnt_Validar').val(response.hidAccionDetEnt);
                        HiddenPageAuth.hidAccionDetEnt_Validar = response.hidAccionDetEnt;

                        //$('#lblTitulo_Validar').val(response.lblTitulo);
                        HiddenPageAuth.lblTitulo_Validar = response.lblTitulo;

                        //$('#hidAccion_Validar').val(response.hidAccion);
                        HiddenPageAuth.hidAccion_Validar = response.hidAccion;

                        //$('#hidTelefono_Validar').val(response.hidTelefono);
                        HiddenPageAuth.hidTelefono_Validar = response.hidTelefono;

                        //$('#hidMotivoA_Validar').val(response.hidMotivoA);
                        HiddenPageAuth.hidMotivoA_Validar = response.hidMotivoA;

                        //$('#hidTipoA_Validar').val(response.hidTipoA);
                        HiddenPageAuth.hidTipoA_Validar = response.hidTipoA;

                        if (response.ReseteoLinea) {
                            alert("Reseteo de Linea.", "Informativo");
                            return;
                        }
                    },
                    error: function (error) {
                        alert('Error: ' + error + ".", "Alerta");
                    }
                });
            }
        });
        return;
    });
}

function AuthenticationUser(pag, controls, product) {
    var user = $("#txtUsernameAuth").val();
    var password = $("#txtPasswordAuth").val();
    if (user === "") {
        alert('Debe ingresar un nombre de usuario.', "Alerta");
        return false;
    }

    if (password === "") {
        alert('Debe ingresar una contraseña.', "Alerta");
        return false;
    }

    var transaccion = "";
    var strUrlController = '/CommonServices/ValidateUser';

    var strUrlLogo = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';

    //strIdSession, transaction, txtUsuario, txtPass, hidPagina, hidMonto, hidUnidad, hidModalidad, hidDescripcionProceso, hidTipoA, hidCo, hidMotivoA, hidTelefono, hidAccion, hidVeces, hidOpcion, hidPagDCM, hidConcepto, transaccion
    var param = {
        "strIdSession": "1234567",
        'transaction': '018462319841578347854',
        'txtUsuario': user,
        'txtPass': password,
        'hidPagina': HiddenPageAuth.hidPagina_Validar, //$('#hidPagina_Validar').val(),
        'hidMonto': HiddenPageAuth.hidMonto_Validar, //$('#hidMonto_Validar').val(),
        'hidUnidad': HiddenPageAuth.hidUnidad_Validar, //$('#hidUnidad_Validar').val(),
        'hidModalidad': HiddenPageAuth.hidModalidad_Validar, //$('#hidModalidad_Validar').val(),
        'hidDescripcionProceso': HiddenPageAuth.hidDescripcionProceso_Validar, //$('#hidDescripcionProceso_Validar').val(),
        'hidTipoA': HiddenPageAuth.hidTipoA_Validar, //$('#hidTipoA_Validar').val(),
        'hidCo': HiddenPageAuth.hidCO_Validar, //$('#hidCO_Validar').val(),
        'hidMotivoA': HiddenPageAuth.hidMotivoA_Validar, //$('#hidMotivoA_Validar').val(),
        'hidTelefono': HiddenPageAuth.hidTelefono_Validar, //$('#hidTelefono_Validar').val(),
        'hidAccion': HiddenPageAuth.hidAccion_Validar, //$('#hidAccion_Validar').val(),
        'hidVeces': HiddenPageAuth.hidVeces_Validar, //$('#hidVeces_Validar').val(),
        'hidOpcion': HiddenPageAuth.hidOpcion_Validar, //$('#hidOpcion_Validar').val(),
        'hidPagDCM': HiddenPageAuth.hidPagDCM_Validar, //$('#hidPagDCM_Validar').val(),
        'hidConcepto': HiddenPageAuth.hidConcepto_Validar, //$('#hidConcepto_Validar').val(),
        'transaccion': transaccion,
        'tecnologia': product
    };

    $.blockUI({
        message: '<div align="center"><img src="' + strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
        css:
        {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    $.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlController,
        data: JSON.stringify(param),
        success: function (response) {
            CloseValidation(response, pag, controls);
        },
        error: function () {
            alert('Error', 'Ocurrió un error realizando la operación.', "Alerta");
        }
    });
    return true;
}

function LoadCacDac(userId, login, idDiv) {
    var objCacDacType = {};
    objCacDacType.strIdSession = userId;

    var parameters = {};
    parameters.strIdSession = userId;
    parameters.strCodeUser = login;
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(parameters),
        url: '/Transactions/CommonServices/GetUsers',
        success: function (results) {
            var cacdac = results.Cac;
            //console.log"cacdac: " + cacdac);
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/CommonServices/GetCacDacType',
                success: function (response) {
                    $("#" + idDiv).append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data.CacDacTypes, function (index, value) {

                        if (cacdac === value.Description) {
                            $("#" + idDiv).append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            $("#" + idDiv).append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        //console.log"valor itemSelect: " + itemSelect);
                        $("#" + idDiv + " option[value=" + itemSelect + "]").attr("selected", true);
                    }
                }
            });
        }
        //success: function (results) {
        //    var cacdacId = results.CodeCac;
        //    $.ajax({
        //        type: 'POST',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: 'json',
        //        data: JSON.stringify(objCacDacType),
        //        url: '/Transactions/CommonServices/GetCacDacType',
                
        //        //success: function (response) {
        //        //    //console.logresponse);
        //        //    if (response.data != null) {
        //        //        $("#" + idDiv).html("");
        //        //        var item = "<option value=''>Seleccionar..</option>";
        //        //        var valueCacDac = false;
        //        //        $.each(response.data.CacDacTypes, function (index, value) {
        //        //            if (cacdacId === value.Code) {
        //        //                item += "<option selected value='" + value.Code + "'>" + value.Description + "</option>";
        //        //                valueCacDac = true;
        //        //            } else {
        //        //                item += "<option value='" + value.Code + "'>" + value.Description + "</option>";
        //        //            }
        //        //        });
        //        //        $("#" + idDiv).html(item);
        //        //        if (valueCacDac) {
        //        //            $("#" + idDiv).attr('disabled', true);
        //        //        }
        //        //    }
        //        //}
        //    });
        //}

        
    });
}

function SaveAudtiCacDac(strTransaction, strService, strText, strTelephone, strNameCustomer, strIdSession, strIpCustomer, strCuentUser, strMontoInput) {
    var param = {};
    param.strTransaction = strTransaction;
    param.strService = strService;
    param.strText = strText;
    param.strTelephone = strTelephone;
    param.strNameCustomer = strNameCustomer;
    param.strIdSession = strIdSession;
    param.strIpCustomer = strIpCustomer;
    param.strCuentUser = strCuentUser;
    param.strMontoInput = strMontoInput;

    $.ajax({
        url: '/Transactions/CommonServices/SaveAuditMJson',
        type: 'POST',
        data: JSON.stringify(param),
        contentType: 'application/json charset=utf-8',
        dataType: 'JSON',
        success: function(result) {
            //console.log"Se registro correcto el Punto de Atención en Auditoria");
        },error: function(xerror) {
            //console.log"Error al guardar el Punto de Atención en Auditoria");
        }
    });
}