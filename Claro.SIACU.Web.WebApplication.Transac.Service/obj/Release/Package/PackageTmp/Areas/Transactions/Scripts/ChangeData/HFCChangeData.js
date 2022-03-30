var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
var mto, fec, intLon, intLonF, strReferencia, strDireccion, strReferenciaF, strDireccionF, Enviomail;
function CloseValidation(obj, pag, controls) {

    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {

        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';

        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function noCopyMouse(e) {
    var isRight = (e.button) ? (e.button == 2) : (e.which == 3);

    if (isRight) {
        return false;
    }
    return true;
}

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
    document.getElementById('hidAccion').value = 'OK';
    $("#txtDateChangeData").attr('disabled', false);
    $("#txtCalendarDateChangeData").attr('disabled', false);

};


function validateDateMask(strDate) {
    if (mask(strDate, '##/##/####') != 1)
        return false;
    else
        return true;
};

function mask(InString, Mask) {
    LenStr = InString.length;
    LenMsk = Mask.length;
    if ((LenStr == 0) || (LenMsk == 0))
        return 0;
    if (LenStr != LenMsk)
        return 0;
    TempString = ""
    for (Count = 0; Count <= InString.length; Count++) {
        StrChar = InString.substring(Count, Count + 1);
        MskChar = Mask.substring(Count, Count + 1);
        if (MskChar == '#') {
            if (!isNumberChar(StrChar))
                return 0;
        }
        else if (MskChar == '?') {
            if (!isAlphabeticChar(StrChar))
                return 0;
        }
        else if (MskChar == '!') {
            if (!isNumOrChar(StrChar))
                return 0;
        }
        else if (MskChar == '*') {
        }
        else {
            if (MskChar != StrChar)
                return 0;
        }
    }
    return 1;
};

function isNumberChar(InString) {
    if (InString.length != 1)
        return (false);
    RefString = "1234567890";
    if (RefString.indexOf(InString, 0) == -1)
        return (false);
    return (true);
};

function getValue(id) {
    var c = document.getElementById(id);
    if (c != null) return Trim(c.value);
    return '';
};

function isNumber(pString) {
    var ok = "yes"; var temp;
    var valid = "0123456789";
    for (var i = 0; i < pString.length ; i++) {
        temp = "" + pString.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1") ok = "no";
    }
    if (ok == "no") { return (false); } else { return (true); }

}

function getvalue(strData, intFieldNumber, separator) {
    var intCurrentField, intFoundPos, strValue, strNames;
    var bool = false;
    strNames = strData;
    intCurrentField = 0;
    while ((intCurrentField != intFieldNumber) && !bool) {
        intFoundPos = strNames.indexOf(separator);
        intCurrentField = intCurrentField + 1;
        if (intFoundPos != 0) {
            strValue = strNames.substring(0, intFoundPos);
            strNames = strNames.substring(intFoundPos + 1, strNames.length);
        }
        else {
            if (intCurrentField == intFieldNumber)
                strValue = strNames;
            else
                strValue = "";
            bool = true;
        }
    }
    if (strValue != "")
        return strValue;
    else
        return strNames;
};

function validarFecha(oControl) {
    var Day, Month, Year;
    var Fecha = document.getElementById(oControl);
    var valor = Fecha.value;
    var controlValida;
    controlValida = Fecha.id;

    if (validateDateMask(valor) == false) {
        alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
        return false;
    }

    Day = getvalue(valor, 1, "/");
    Month = getvalue(valor, 2, "/");
    Year = getvalue(valor, 3, "/");

    if ((isNumber(Day) && isNumber(Month) && isNumber(Year) && (Year.length == 4) && (Day.length <= 2) && (Month.length <= 2)) || ((Month == 2) && (Day <= 29))) {
        if ((Day != 0) && (Month != 0) && (Year != 0) && (Month <= 12) && (Day <= 31) && (Month != 2)) {

            if (Month == 4 || Month == 6 || Month == 9 || Month == 11) {
                if (Day > 30) {
                    alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
                    return false;
                }
            } else if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                if (Day > 31) {
                    alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
                    return false;
                }
            }
            return true;
        }
        else if ((Month == 2) && (Day <= 29) && ((Year % 4) == 0) && ((Year % 100) != 0))
            return true;
        else if ((Month == 2) && (Day <= 29) && ((Year % 400) == 0))
            return true;
        else if ((Month == 2) && (Day <= 28))
            return true;
        else {
            if (Month > 12) {
                alert('El campo de mes debe ser como máximo 12.');
            }
            else if (Year.length != 4) {
                alert("El año debe tener 4 cifras.");
            }
            else if ((Month == 2) && (Day == 29) && ((Year % 4) == 0) && (Year % 100) == 0) {
                alert('Año no es bisiesto.');
            }
            else {
                alert('Fecha no válida');
            }
            if (Fecha.disabled == false)
                Fecha.focus();

            Fecha.select();
            return false;
        }
    }
    else {
        alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)');
        return false;
    }
};

$(document).ready(function () {
    $("#txtNumeroCalle").keydown(function (event) {

        if (event.shiftKey) {
            event.preventDefault();
        }
        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (event.keyCode < 95) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    event.preventDefault();
                }
            }
            else {
                if (event.keyCode < 96 || event.keyCode > 105) {
                    event.preventDefault();
                }
            }
        }
    });

    $("#txtNewNroDoc").keydown(function (event) {

        var tipoDato = $("#hidTipoDato").val();
        var longDato = $("#hidLongDoc").val();

        if (event.shiftKey) {
            event.preventDefault();
        }
        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {

            if (tipoDato == 0) {

                if (event.keyCode < 95) {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        event.preventDefault();
                    }
                }
                else {
                    if (event.keyCode < 96 || event.keyCode > 105) {
                        event.preventDefault();
                    }
                }

            } else {
                var caracteres = /[a-zA-Z0-9]/;
                var valido = caracteres.test(event.key);
                if (!valido) {
                    event.preventDefault();
                }
            }

            if ($(this).val().length < longDato) {
            } else {
                event.preventDefault();
            }

            if ($(this).val().length == longDato - 1) {
                var opcion = $('select[name="nmcboTipDoc"] option:selected').text();


                if (opcion == 'RUC') {

                    var tpDoc = ($('#txtNewNroDoc').val()).substring(0, 2);

                    if (tpDoc == '10') {
                        $('#txtNewNombCli').prop('disabled', false);
                        $('#txtNewApeCli').prop('disabled', false);
                        $('#txtNewRs').prop('disabled', true);
                        $('#txtNameComChangeData').prop('disabled', true);
                        $('#divNameCli').css("display", "block");
                        $('#divNameC').css("display", "none");
                        $('#NuevosDatos').css("display", "none");
                    }
                    else {
                        if (tpDoc == '20') {
                            $('#txtNewNombCli').prop('disabled', true);
                            $('#txtNewApeCli').prop('disabled', true);
                            $('#txtNewRs').prop('disabled', false);
                            $('#txtNameComChangeData').prop('disabled', false);
                            $('#divNameCli').css("display", "none");
                            $('#divNameC').css("display", "block");
                            $('#NuevosDatos').css("display", "block");
                        } else {
                            $('#txtNewNombCli').prop('disabled', false);
                            $('#txtNewApeCli').prop('disabled', false);
                            $('#txtNewRs').prop('disabled', true);
                            $('#txtNameComChangeData').prop('disabled', true);
                            $('#divNameCli').css("display", "block");
                            $('#divNameC').css("display", "block");
                            $('#NuevosDatos').css("display", "none");
                        }

                    }
                }
                else {

                }

            }
        }


    });

    $("#txtEmailChangeData").keypress(function (event) {

        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var texto = $("#txtEmailChangeData").val() + event.key;
        var valido = caracteres.test(texto);
        if (!valido) {
            $("#txtEmailChangeData").closest('.error-input').addClass('has-error');
        } else {
            $("#txtEmailChangeData").closest('.error-input').removeClass('has-error');
        }

    });

    $("#txtConfirmarEmailChangeData").keypress(function (event) {

        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var texto = $("#txtConfirmarEmailChangeData").val() + event.key;
        var valido = caracteres.test(texto);
        if (!valido) {
            $("#txtConfirmarEmailChangeData").closest('.error-input').addClass('has-error');
        } else {
            $("#txtConfirmarEmailChangeData").closest('.error-input').removeClass('has-error');
        }

    });

    $("#txtEmail").keypress(function (event) {

        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var texto = $("#txtEmail").val() + event.key;
        var valido = caracteres.test(texto);
        if (!valido) {
            $("#txtEmail").closest('.error-input').addClass('has-error');
        } else {
            $("#txtEmail").closest('.error-input').removeClass('has-error');
        }

    });
});

(function ($, undefined) {

    var SessionRespuesta = function () { };
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    var SessionTransf = function () { };
    SessionTransf.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == '' || SessionTransac.UrlParams.IDSESSION == undefined ? "123456789874563211" : SessionTransac.UrlParams.IDSESSION;
    SessionTransf.PhonfNro = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
    SessionTransf.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
    SessionTransf.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
    SessionTransf.NameCustomer = SessionTransac.SessionParams.DATACUSTOMER.FullName;
    SessionTransf.TypDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
    SessionTransf.NumbDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DNIRUC;
    SessionTransf.cuentaCustomer = SessionTransac.SessionParams.DATACUSTOMER.Account;
    SessionTransf.RepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
    SessionTransf.NotAddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.CountryCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
    SessionTransf.CountryCustomerFac = SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry;
    SessionTransf.PlanoIDCustomer = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation;
    SessionTransf.urbLegalCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.DirecDespachoCustomer = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
    SessionTransf.DepCustomer = SessionTransac.SessionParams.DATACUSTOMER.Departament;
    SessionTransf.ProvCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalProvince;
    SessionTransf.DistCustomer = SessionTransac.SessionParams.DATACUSTOMER.District;
    SessionTransf.IdEdifCustomer = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeBilling;
    SessionTransf.EmailCustomer = SessionTransac.SessionParams.DATACUSTOMER.Email;
    SessionTransf.AccesPage = SessionTransac.SessionParams.USERACCESS.optionPermissions;
    SessionTransf.gConstOpcTIEHabFidelizar = null;
    SessionTransf.DatosLineatelefonia = SessionTransac.SessionParams.DATASERVICE.TelephonyValue;
    SessionTransf.DatosLineainternet = SessionTransac.SessionParams.DATASERVICE.InternetValue;
    SessionTransf.DatosLineacableTv = SessionTransac.SessionParams.DATASERVICE.CableValue;
    SessionTransf.DataCycleBilling = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
    SessionTransf.CodeUbigeo = SessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo;
    SessionTransf.Permiso = false;
    SessionTransf.EstadoCivil = "";
    SessionTransf.Nacionalidad = "";
    SessionTransf.TipDoc = "";
    SessionTransf.opcAct = "";
    SessionTransf.opcError = "";
    SessionTransf.LegalDepartament = SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament;
    SessionTransf.LegalDistrict = SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict;
    SessionTransf.BirthPlace = SessionTransac.SessionParams.DATACUSTOMER.BirthPlace;
    SessionTransf.LegalPostal = SessionTransac.SessionParams.DATACUSTOMER.LegalPostal;

    SessionTransf.InvoiceAddress = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
    SessionTransf.InvoiceCountry = SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry;
    SessionTransf.InvoiceDepartament = SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament;
    SessionTransf.InvoiceDistrict = SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict;
    SessionTransf.InvoicePostal = SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal;
    SessionTransf.InvoiceProvince = SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince;
    SessionTransf.InvoiceUrbanization = SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization;

    SessionTransf.TypeCustomer = SessionTransac.SessionParams.DATACUSTOMER.CustomerType;
    SessionTransf.FlagPlataforma = SessionTransac.SessionParams.DATASERVICE.FlagPlatform;

    SessionTransf.LegalAgent = SessionTransac.SessionParams.DATACUSTOMER.LegalAgent;
    SessionTransf.CustomerContact = SessionTransac.SessionParams.DATACUSTOMER.CustomerContact;
    SessionTransf.login = SessionTransac.SessionParams.USERACCESS.login;
    SessionTransf.AgentfullName = SessionTransac.SessionParams.USERACCESS.fullName;

    SessionTransf.tipoDato = 0; ////0: numero, 1: alfanumerico
    SessionTransf.DocLongitud = 0;
    SessionTransac.Reciboxcorreo = false;
    SessionTransac.Secuencia = 0;
    SessionTransac.Respuesta = 0;

    SessionTransac.MensajeEmail = "";
    SessionTransac.vDesInteraction = "";
    SessionTransac.InteractionId = "";
    SessionTransac.RutaArchivo = "";
    SessionTransac.FlagInsDom = 0;
    SessionTransac.FlagInsFact = 0;

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTChangeData.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , hidSupJef: $("#hidSupJef", $element)
            , cboCACDAC: $("#cboCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , cbocivilstatus: $("#cbocivilstatus", $element)
            , cboNacionalidadChangeData: $("#cboNacionalidadChangeData", $element)

            //textbox
            , txtCargoDesem: $("#txtCargoDesem", $element)
            , txtNameComChangeData: $('#txtNameComChangeData', $element)
            , txtPhoneChangeData: $('#txtPhoneChangeData', $element)
            , txtContactocliChangeData: $('#txtContactocliChangeData', $element)
            , txtMovilChangeData: $('#txtMovilChangeData', $element)
            , txtDateChangeData: $('#txtDateChangeData', $element)
            , txtNumberChangeData: $('#txtNumberChangeData', $element)
            , txtEmailChangeData: $('#txtEmailChangeData', $element)
            , txtEmail: $("#txtEmail", $element)
            , txtConfirmarEmailChangeData: $("#txtConfirmarEmailChangeData", $element)
            , txtNote: $('#txtNote', $element)

            //label : Datos cliente

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
            //////////////////////////////////////////////////
            , lblNroTelefono: $('#lblNroTelefono', $element)
            , lblContacto: $('#lblContacto', $element)
            , lblCargDesemp: $('#lblCargDesemp', $element)
            , lblTelefono: $('#lblTelefono', $element)
            , lblCelular: $('#lblCelular', $element)
            , lblFax: $('#lblFax', $element)
            , lblEmail: $('#lblEmail', $element)
            , lblNomComercial: $('#lblNomComercial', $element)
            , lblContactoCli: $('#lblContactoCli', $element)
            , lblFechaNac: $('#lblFechaNac', $element)
            , lblNacionalidad: $('#lblNacionalidad', $element)
            , lblSexoCli: $('#lblSexoCli', $element)
            , lblEstadoCiv: $('#lblEstadoCiv', $element)
            //radiobutton 
            , rdbHombre: $("#rdbHombre", $element)
            , rdbMujer: $("#rdbMujer", $element)
            , rdbNA: $("#rdbNA", $element)
            , chkEmail: $("#chkEmail", $element)


            , btnCerrar: $("#btnCerrar", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , myModalLoad: $("#myModalLoad", $element)
            , divReglas: $("#divReglas", $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , txtCalendarDateChangeData: $("#txtCalendarDateChangeData", $element)

            , cboMotivoChange: $("#cboMotivoChange", $element)
            , cboTipDoc: $("#cboTipDoc", $element)
            , txtNewNombCli: $("#txtNewNombCli", $element)
            , txtNewApeCli: $("#txtNewApeCli", $element)
            , txtNewRs: $("#txtNewRs", $element)
            , txtNewNroDoc: $("#txtNewNroDoc", $element)
            , txtTelefAdic1: $("#txtTelefAdic1", $element)
            , txtTelefAdic2: $("#txtTelefAdic2", $element)
            , txtEmailAdic1: $("#txtEmailAdic1", $element)
            , txtEmailAdic2: $("#txtEmailAdic2", $element)
            , txtDireccion: $("#txtDireccion", $element)
            , txtEmailChangeData: $("#txtEmailChangeData", $element)
            , txtNewNombRep: $("#txtNewNombRep", $element)
            , txtNewNombCont: $("#txtNewNombCont", $element)
             , cboPais: $("#cboPais", $element)
             , cboDepartamento: $("#cboDepartamento", $element)
             , cboProvincia: $("#cboProvincia", $element)
            , cboDistrito: $("#cboDistrito", $element)

            , NuevosDatos: $('#NuevosDatos', $element)
            , divNameC: $('#divNameC', $element)
            , divNameCli: $('#divNameCli', $element)
            , trRL: $('#trRL', $element)
            , trCC: $('#trCC', $element)
            // , txtNewNombCli: $("", $element)


            , cboPaisMod: $("#cboPaisMod", $element)
            , cboDepMod: $("#cboDepMod", $element)
            , cboProvinciaMod: $("#cboProvinciaMod", $element)
            , cboDistritoMod: $("#cboDistritoMod", $element)
            , txtCodPostalMod: $("#txtCodPostalMod", $element)
            , cboTipoVia: $("#cboTipoVia", $element)
            , txtVia: $("#txtVia", $element)
            , txtNumeroCalle: $("#txtNumeroCalle", $element)
            , chkSN: $("#chkSN", $element)
            , cboTipoMz: $("#cboTipoMz", $element)
            , txtNroMz: $("#txtNroMz", $element)
            , txtLote: $("#txtLote", $element)
            , cboTipoInterior: $("#cboTipoInterior", $element)
            , txtInterior: $("#txtInterior", $element)
            , txtContadorD1: $("#txtContadorD1", $element)
            , cboTipoUrb: $("#cboTipoUrb", $element)
            , txtUrb: $("#txtUrb", $element)
            , cboTipoZona: $("#cboTipoZona", $element)
            , txtZona: $("#txtZona", $element)
            , txtReferencia: $("#txtReferencia", $element)
            , txtContadorD2: $("#txtContadorD2", $element)
            , txtCodPostalMod: $("#txtCodPostalMod", $element)

            , lblCodPostal: $('#lblCodPostal', $element)
            , lblReferencia: $('#lblReferencia', $element)
            , lblDireccionLegal: $('#lblDireccionLegal', $element)
            , lblReferenciaLegal: $('#lblReferenciaLegal', $element)
            , lblCodPostalLegal: $('#lblCodPostalLegal', $element)
            , lblDepartamentoLegal: $('#lblDepartamentoLegal', $element)
            , lblProvinciaLegal: $('#lblProvinciaLegal', $element)
            , lblDistritoLegal: $('#lblDistritoLegal', $element)
            , txtNroDocRep: $('#txtNroDocRep', $element)
            , cboTipoDocRep: $('#cboTipoDocRep', $element)
            , lblTipoDoc: $('#lblTipoDoc', $element)

            , lblRepLegal: $('#lblRepLegal', $element)
            , lblContCliente: $('#lblContCliente', $element)

            , chkFacturacion: $("#chkFacturacion", $element)
            , chkCopy: $("#chkCopy", $element)
            , divFacturacion: $('#divFacturacion', $element)

            , cboPaisModF: $("#cboPaisModF", $element)
            , cboDepModF: $("#cboDepModF", $element)
            , cboProvinciaModF: $("#cboProvinciaModF", $element)
            , cboDistritoModF: $("#cboDistritoModF", $element)
            , txtCodPostalModF: $("#txtCodPostalModF", $element)
            , cboTipoViaF: $("#cboTipoViaF", $element)
            , txtViaF: $("#txtViaF", $element)
            , txtNumeroCalleF: $("#txtNumeroCalleF", $element)
            , chkSNF: $("#chkSNF", $element)
            , cboTipoMzF: $("#cboTipoMzF", $element)
            , txtNroMzF: $("#txtNroMzF", $element)
            , txtLoteF: $("#txtLoteF", $element)
            , cboTipoInteriorF: $("#cboTipoInteriorF", $element)
            , txtInteriorF: $("#txtInteriorF", $element)
            , txtContadorFD1: $("#txtContadorFD1", $element)
            , cboTipoUrbF: $("#cboTipoUrbF", $element)
            , txtUrbF: $("#txtUrbF", $element)
            , cboTipoZonaF: $("#cboTipoZonaF", $element)
            , txtZonaF: $("#txtZonaF", $element)
            , txtReferenciaF: $("#txtReferenciaF", $element)
            , txtContadorFD2: $("#txtContadorFD2", $element)
            , txtCodPostalModF: $("#txtCodPostalModF", $element)

        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            controls.txtDateChangeData.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateChangeData.addEvent(that, 'change', that.changeDateChangeData);
            controls.txtCalendarDateChangeData.addEvent(that, 'click', that.txtCalendarDateChangeData_click);
            controls.cboMotivoChange.addEvent(that, 'change', that.cboMotivoChange_Change);

            controls.cboDepMod.addEvent(that, 'change', that.InitProvincias);
            controls.cboProvinciaMod.addEvent(that, 'change', that.InitDistritos);
            controls.cboDistritoMod.addEvent(that, 'change', that.InitPostalCode);
            controls.cboTipoVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtVia.addEvent(that, 'change', that.ContadorD1);
            controls.txtNumeroCalle.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoMz.addEvent(that, 'change', that.change_manzana);
            controls.cboTipoMz.addEvent(that, 'click', that.ContadorD1);
            controls.txtNroMz.addEvent(that, 'change', that.ContadorD1);
            controls.txtLote.addEvent(that, 'change', that.ContadorD1);
            controls.chkSN.addEvent(that, 'change', that.CheckSiNo);
            controls.cboTipoInterior.addEvent(that, 'change', that.ContadorD1);
            controls.txtInterior.addEvent(that, 'change', that.ContadorD1);
            controls.cboTipoUrb.addEvent(that, 'click', that.ValidaTipoUrb);
            controls.cboTipoZona.addEvent(that, 'click', that.ValidaTipoZona);
            controls.txtUrb.addEvent(that, 'change', that.ContadorD2);
            controls.txtZona.addEvent(that, 'change', that.ContadorD2);
            controls.txtReferencia.addEvent(that, 'change', that.ContadorD2);

            controls.cboDepModF.addEvent(that, 'change', that.InitProvinciasF);
            controls.cboProvinciaModF.addEvent(that, 'change', that.InitDistritosF);
            controls.cboDistritoModF.addEvent(that, 'change', that.InitPostalCodeF);
            controls.cboTipoViaF.addEvent(that, 'change', that.ContadorFD1);
            controls.txtViaF.addEvent(that, 'change', that.ContadorFD1);
            controls.txtNumeroCalleF.addEvent(that, 'change', that.ContadorFD1);
            controls.cboTipoMzF.addEvent(that, 'change', that.change_manzanaF);
            controls.cboTipoMzF.addEvent(that, 'click', that.ContadorFD1);
            controls.txtNroMzF.addEvent(that, 'change', that.ContadorFD1);
            controls.txtLoteF.addEvent(that, 'change', that.ContadorFD1);
            controls.chkSNF.addEvent(that, 'change', that.CheckSiNoF);
            controls.cboTipoInteriorF.addEvent(that, 'change', that.ContadorFD1);
            controls.txtInteriorF.addEvent(that, 'change', that.ContadorFD1);
            controls.cboTipoUrbF.addEvent(that, 'click', that.ValidaTipoUrbF);
            controls.cboTipoZonaF.addEvent(that, 'click', that.ValidaTipoZonaF);
            controls.txtUrbF.addEvent(that, 'change', that.ContadorFD2);
            controls.txtZonaF.addEvent(that, 'change', that.ContadorFD2);
            controls.txtReferenciaF.addEvent(that, 'change', that.ContadorFD2);

            controls.cboTipDoc.addEvent(that, 'change', that.cboTipDoc_change);

            controls.chkFacturacion.addEvent(that, 'change', that.chkFacturacion_Change);
            controls.chkCopy.addEvent(that, 'change', that.chkCopy_Change);

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        changeDateChangeData: function () {
            var that = this,
                controls = this.getControls();
        },
        modelChangeData: {},
        modelAddressCustomer: {},
        modelAddressInvoice: {},
        modelDataCustomerResponse: {},
        render: function () {

            var that = this,
            control = that.getControls();
            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            control.divErrorAlert.hide();

            that.Loading();

            that.loadCustomerData();
            //that.loadAditionalData();
            that.hidAccion = 'R';

            that.InitVias();
            that.InitManzanas();
            that.InitInteriores();
            that.InitUrbs();
            that.InitZonas();
            that.InitDepartamentos();
            that.InitPostalCode();

            that.loadTypification();
            that.ValidateSendxEmail();

            control.chkEmail.attr("checked", true);
            if (control.chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
            // that.getRulesControls();
            that.getMotivoCambio();
            that.getTypeDocument();
            that.cboMotivoChange_Change();
            that.cboTipDoc_change();
        },
        loadTypification: function () {
            var that = this;
            var controls = this.getControls();

            var obj = { strIdSession: SessionTransf.IDSESSION };
            $.app.ajax({
                type: 'POST',
                async: false,
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/Transactions/HFC/ChangeData/PageLoad',
                success: function (response) {


                    if (response.data != null) {

                        TYPIFICATION.ClaseId = response.data.CLASE_CODE;
                        TYPIFICATION.SubClaseId = response.data.SUBCLASE_CODE;
                        TYPIFICATION.Tipo = response.data.TIPO;
                        TYPIFICATION.ClaseDes = response.data.CLASE;
                        TYPIFICATION.SubClaseDes = response.data.SUBCLASE;
                        TYPIFICATION.TipoId = response.data.TIPO_CODE;

                        that.modelChangeData.tipo = response.data.TIPO;
                        that.modelChangeData.tipoCode = response.data.TIPO_CODE;
                        that.modelChangeData.claseDes = response.data.CLASE;
                        that.modelChangeData.claseCode = response.data.CLASE_CODE;
                        that.modelChangeData.subClaseDes = response.data.SUBCLASE;
                        that.modelChangeData.subClaseCode = response.data.SUBCLASE_CODE;

                        /*that.modelChangeData.tipo = 'POSTPAGO';
                        that.modelChangeData.claseDes = 'VARIACIÓN - ESTADO DE LA LÍNEA/CLIENTE';
                        that.modelChangeData.subClaseDes = 'CAMBIO DE DATOS';*/

                    }

                }

            });
        },
        txtCalendarDateChangeData_click: function () {
            var that = this,
                control = that.getControls();


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
        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },
        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("CAMBIO DE DATOS");

            var obj = {
                strIdSession: SessionTransf.IDSESSION,
                strTelefono: SessionTransf.PhonfNro,
                strCustomerId: SessionTransf.CUSTOMER_ID,
                account: SessionTransf.cuentaCustomer
            };
            $.app.ajax({
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/Transactions/HFC/ChangeData/GetCustomerChangeData',
                success: function (response) {

                    if (response.objCus.FECHA_NAC != "") {
                        var fec = response.objCus.FECHA_NAC.substring(0, 10);
                        response.objCus.FECHA_NAC = fec;
                    }

                    that.modelDataCustomerResponse.Cliente = response.objCus;

                    SessionRespuesta = response.objCus;
                    controls.txtNewApeCli.val(response.objCus.APELLIDOS);

                    controls.txtNewNombCli.val(response.objCus.NOMBRES);
                    controls.txtNewRs.val(response.objCus.RAZON_SOCIAL);
                    SessionTransf.TipDoc = response.objCus.TIPO_DOC;
                    SessionTransf.TipDocRl = response.objCus.TIPO_DOC_RL;
                    controls.txtNewNroDoc.val(response.objCus.NRO_DOC);

                    controls.txtCargoDesem.val(response.objCus.CARGO);
                    controls.txtNameComChangeData.val(response.objCus.NOMBRE_COMERCIAL);
                    controls.txtPhoneChangeData.val(response.objCus.TELEF_REFERENCIA);
                    controls.txtMovilChangeData.val(response.objCus.TELEFONO_CONTACTO);
                    controls.txtDateChangeData.val(response.objCus.FECHA_NAC);
                    controls.txtNumberChangeData.val(response.objCus.FAX);
                    controls.txtEmail.val(response.objCus.EMAIL);
                    controls.txtNewNombRep.val(response.objCus.REPRESENTANTE_LEGAL);
                    controls.txtNewNombCont.val(response.objCus.CONTACTO_CLIENTE);

                    SessionTransf.Nacionalidad = response.objCus.LUGAR_NACIMIENTO_ID;
                    SessionTransf.EstadoCivil = response.objCus.ESTADO_CIVIL_ID;

                    $("input[name=rdbSexo]").val([(response.objCus.SEXO == null) ? '' : response.objCus.SEXO]);

                    controls.txtEmailChangeData.val(response.objCus.EMAIL);

                    if (SessionTransf.AccesPage.indexOf(response.strPermiso) > 0) {
                        SessionTransf.Permiso = true;
                    } else {
                        SessionTransf.Permiso = false;
                    }

                    SessionTransf.opcAct = response.opcAct;
                    SessionTransf.opcError = response.opcError;
                },
                complete: function () {

                    //********** Datos del Formulario ***********/

                    controls.txtCodPostalMod.val(SessionTransf.LegalPostal);
                    that.InitEstCiv(SessionTransf.EstadoCivil);
                    that.InitNacionalidad(SessionTransf.Nacionalidad);
                    that.InitCacDaC();


                    $("#txtPhoneChangeData").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    })
                    $("#txtMovilChangeData").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    });
                    $("#txtNumberChangeData").on('input', function () {
                        this.value = this.value.replace(/[^0-9]/g, '');
                    });
                    //$("#txtNameComChangeData").on('input', function () {
                    //    this.value = this.value.replace(/[^a-zA-Z]/g, '');
                    //});
                    //$("#txtContactocliChangeData").on('input', function () {
                    //    this.value = this.value.replace(/[^a-zA-Z]/g, '');
                    //});



                }

            });


            //********** Datos del Cliente ***********/
            controls.lblContrato.html((SessionTransf.CONTRATO_ID == null) ? '' : SessionTransf.CONTRATO_ID);
            controls.lblCustomerID.html((SessionTransf.CUSTOMER_ID == null) ? '' : SessionTransf.CUSTOMER_ID);
            controls.lblTipoCliente.html((SessionTransac.SessionParams.DATACUSTOMER.CustomerType == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.lblCliente.html((SessionTransf.RepreCustomer == null) ? '' : SessionTransf.RepreCustomer);


            controls.lblRepren_Legal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalAgent == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblPlan.html((SessionTransac.SessionParams.DATASERVICE.Plan == null) ? '' : SessionTransac.SessionParams.DATASERVICE.Plan);
            controls.lblFechaActivacion.html((SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ActivationDate == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ActivationDate);
            controls.lblCicloFact.html((SessionTransf.DataCycleBilling == null) ? '' : SessionTransf.DataCycleBilling);
            controls.lblLimiteCred.html((SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit);


            controls.lblNroTelefono.html((SessionTransf.PhonfNro == null) ? '' : SessionTransf.PhonfNro);
            controls.lblContacto.html((SessionTransf.NameCustomer == null) ? '' : SessionTransf.NameCustomer);
            controls.lblCargDesemp.html((SessionTransac.SessionParams.DATACUSTOMER.Position == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.Position);
            controls.lblDNI_RUC.html((SessionTransf.NumbDocRepreCustomer == null) ? '' : SessionTransf.NumbDocRepreCustomer);
            controls.lblTelefono.html((SessionTransac.SessionParams.DATACUSTOMER.PhoneReference == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.PhoneReference);
            controls.lblCelular.html((SessionTransac.SessionParams.DATACUSTOMER.PhoneContact == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.PhoneContact);
            controls.lblFax.html((SessionTransac.SessionParams.DATACUSTOMER.Fax == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.Fax);
            controls.lblEmail.html((SessionTransac.SessionParams.DATACUSTOMER.Email == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.Email);
            controls.lblNomComercial.html((SessionTransac.SessionParams.DATACUSTOMER.Tradename == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.Tradename);
            controls.lblContactoCli.html((SessionTransac.SessionParams.DATACUSTOMER.CustomerContact == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.CustomerContact);
            controls.lblFechaNac.html((SessionTransac.SessionParams.DATACUSTOMER.BirthDate == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.BirthDate);
            controls.lblNacionalidad.html((SessionTransac.SessionParams.DATACUSTOMER.BirthPlace == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.BirthPlace);
            controls.lblTipoDoc.html((SessionTransac.SessionParams.DATACUSTOMER.DocumentType == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.DocumentType);

            controls.lblRepLegal.html((SessionTransf.LegalAgent == null) ? '' : SessionTransf.LegalAgent);
            controls.lblContCliente.html((SessionTransf.CustomerContact == null) ? '' : SessionTransf.CustomerContact);


            if (SessionTransac.SessionParams.DATACUSTOMER.DocumentType != null) {
                if (SessionTransac.SessionParams.DATACUSTOMER.DocumentType == 'RUC') {

                    if (SessionTransf.NumbDocRepreCustomer != null) {
                        var tpDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 1);

                        if (tpDoc != '2') {
                            controls.trRL.css("display", "none");
                            controls.trCC.css("display", "none");

                        } else {
                            //controls.trRL.css("display", "block");
                            //controls.trCC.css("display", "block");
                        }
                    }

                } else {
                    controls.trRL.css("display", "none");
                    controls.trCC.css("display", "none");

                }
            }

            if (SessionTransac.SessionParams.DATACUSTOMER.Sex == "F") {

                controls.lblSexoCli.html("FEMENINO");
            }
            else if (SessionTransac.SessionParams.DATACUSTOMER.Sex == "M") {
                controls.lblSexoCli.html("MASCULINO");
            }




            controls.lblEstadoCiv.html((SessionTransac.SessionParams.DATACUSTOMER.CivilStatus == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.CivilStatus);

            //********** Direccíón de Facturación ***********/
            controls.lblDireccion.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
            controls.lblReferencia.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCodPostal.html((SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal);
            controls.lblDepartamento.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvincia.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrito.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict);

            //********** Datos del Formulario ***********/

            //********** Direccíón de Domicilio ***********/
            controls.lblDireccionLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalAddress == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalAddress);
            controls.lblReferenciaLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization);
            controls.lblCodPostalLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalPostal == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalPostal);
            controls.lblDepartamentoLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament);
            controls.lblProvinciaLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalProvince == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalProvince);
            controls.lblDistritoLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict);

            //********** Datos del Formulario ***********/

            //if (SessionTransf.TypeCustomer.toUpperCase() == 'CONSUMER') {
            //    controls.divNameC.css("display", "none");
            //} else {
            //    controls.divNameC.css("display", "block");
            //}


            var opcion = SessionTransf.TipDoc.toUpperCase();
            if (opcion == 'DNI') {
                SessionTransf.tipoDato = 0;
                SessionTransf.DocLongitud = 8;

                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);

                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion == 'RUC') {
                SessionTransf.tipoDato = 0;
                SessionTransf.DocLongitud = 11;
                controls.txtNewRs.prop('disabled', false);
                controls.txtNameComChangeData.prop('disabled', false);

                controls.NuevosDatos.css("display", "block");
                controls.divNameCli.css("display", "none");
                controls.divNameC.css("display", "block");
            }
            if (opcion == 'PASAPORTE') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion.toUpperCase() == 'CARNET DE EXTRANJERÍA' || opcion == 'CE') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion == 'CIE' || opcion == 'CIRE' || opcion == 'CPP' || opcion == 'CTM') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }

            $("#hidTipoDato").val(SessionTransf.tipoDato);
            $("#hidLongDoc").val(SessionTransf.DocLongitud);


        },
        loadAditionalData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();



            $.app.ajax({
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: {
                    strSession: SessionTransf.IDSESSION,
                    strCustomer: SessionTransf.CUSTOMER_ID
                },
                url: '/Transactions/HFC/ChangeData/GetAditionalData',
                success: function (response) {

                    // that.setSesssionsCustomer(response.objCus, response.cambiarfecha);

                    controls.txtTelefAdic1.val(response.data.TELEFONO_REFERENCIA_1);
                    controls.txtTelefAdic2.val(response.data.TELEFONO_REFERENCIA_2);
                    //  controls.txtEmailAdic1.val(response.data.APELLIDOS);
                    //  controls.txtEmailAdic2.val(response.data.APELLIDOS);
                    // controls.txtDireccion.val(response.data.APELLIDOS);

                }

            });



        },
        resp: "",
        LoadPag: function () {
            var that = this, controls = this.getControls();
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

        },
        chkEmail_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkEmail = control.chkEmail;

            if (chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
        },
        chkFacturacion_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkFacturacion = control.chkFacturacion;

            if (chkFacturacion[0].checked == true) {
                control.divFacturacion.css("display", "block");
            } else {
                control.divFacturacion.css("display", "none");
            }
        },
        chkCopy_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkCopy = control.chkCopy;

            if (chkCopy[0].checked == true) {

                that.cloneAddress();
            } else {
                that.cloneAddressBack();
            }
        },
        cloneAddressBack: function () {
            var that = this,
                control = that.getControls();

            control.cboTipoViaF.val('');
            control.txtViaF.val('');
            control.txtNumeroCalleF.val('');
            control.chkSNF.prop('checked', false);
            control.cboTipoMzF.val('');
            control.txtNroMzF.val('');
            control.txtLoteF.val('');
            control.cboTipoInteriorF.val('');
            control.txtInteriorF.val('');
            control.cboTipoUrbF.val('');
            control.txtUrbF.val('');
            control.cboTipoZonaF.val('');
            control.txtZonaF.val('');
            control.txtReferenciaF.val('');
            control.cboDepModF.val('');
            control.cboProvinciaModF.val('');
            control.cboDistritoModF.val('');
            control.txtCodPostalModF.val('');
            that.ContadorFD1();
            that.ContadorFD2();
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
        btnConstancia_click: function () {
            var that = this;

            var PDFRoute = SessionTransac.RutaArchivo;
            var IdSession = SessionTransf.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },
        btnGuardar_click: function () {
            //  if ($('#frmChangeData').valid()) {
            var that = this,
                controls = this.getControls();

            that.isPostBackFlag = that.NumeracionUNO;
            //
            //    if (!that.ValidateControl()) {
            //        return false;
            //    }
            //

            //INC000002608479
            if (controls.chkEmail[0].checked) {
                if ($("#txtEmail").val() != "" || $("#txtEmail").val() != null) {
                    var _caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    var _texto = $("#txtEmail").val();
                    var _valido = _caracteres.test(_texto);
                    if (!_valido) {
                        alert("Ingrese un correo válido de Envío por E-Mail", "Informativo");
                        return false;
                    }
                } else {
                    alert("Ingrese un correo de Envío por E-Mail", "Informativo");
                    return false;
                }
            }
            //INC000002608479

            confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                that.Loading();
                SessionTransac.Secuencia = 0;
                SessionTransac.Respuesta = 0;
                that.SaveTransactionCambioDatosM();

            }, function () {
                $("#hidAccion").val("R");
                return false;
            });
            //}
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
        ValidateControl: function () {
            var that = this,
                control = that.getControls();


            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert("Ingresar email", 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            if (validarFecha('txtDateChangeData') == false) {
                document.frmMain.txtDateChangeData.focus();
                return false;
            }

            if (control.cboCACDAC.val() == "-1" || control.cboCACDAC.val() == "") {
                alert("Seleccione Punto de Atención.", "Alerta");
                return false;
            }

            if (control.cbocivilstatus.val() == "-1" || control.cbocivilstatus.val() == "") {
                alert("Porfavor Seleccione un Estado Civil.", "Alerta");
                return false;
            }

            if (control.cboNacionalidadChangeData.val() == "-1" || control.cboNacionalidadChangeData.val() == "") {
                alert("Seleccione la nacionalidad del cliente.", "Alerta");
                return false;
            }

            var strNotas = control.txtNote.val();

            if (strNotas.length > 3800) {
                alert("El maximo de caracteres permitidos en el campo notas es de 3800");
                control.txtNote.val(strNotas.substring(0, 3800));
                control.txtNote.focus();
                return false;
            }


            return true;
        },
        btnCerrar_Click: function () {
            parent.window.close();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        BlockControl: function () {
            var that = this,
            control = that.getControls();

            control.cboNacionalidadChangeData.prop('disabled', true);

            control.cbocivilstatus.prop('disabled', true);
            control.txtNote.prop('disabled', true);

            control.chkEmail.prop('disabled', true);
            control.rdbHombre.prop('disabled', true);
            control.rdbMujer.prop('disabled', true);
            control.rdbNA.prop('disabled', true);
        },

        SaveTransactionCambioDatosM: function () {

            //Variables para tipificacion

            var that = this,
                controls = that.getControls();
            var resp = 0;

            that.modelChangeData.strIdSession = SessionTransf.IDSESSION;
            that.modelChangeData.strCacDac = $('#cboCACDAC option:selected').text();
            that.modelChangeData.strCacDacId = $('#cboCACDAC option:selected').val();
            that.modelChangeData.strNote = $("#txtNote").val();
            that.modelChangeData.strPhone = SessionTransf.PhonfNro;
            that.modelChangeData.strCustomerId = SessionTransf.CUSTOMER_ID;

            var opcion = $('select[name="nmcboTipDoc"] option:selected').text();

            if (opcion == 'DNI') {
                that.modelChangeData.strRazonSocial = controls.txtNewNombCli.val() + ' ' + controls.txtNewApeCli.val();
            }
            else {
                if (opcion == 'RUC') {

                    var tpDoc = (controls.txtNewNroDoc.val()).substring(0, 2);

                    if (tpDoc == '10') {
                        that.modelChangeData.strRazonSocial = controls.txtNewNombCli.val() + ' ' + controls.txtNewApeCli.val();
                    }
                    else {
                        that.modelChangeData.strRazonSocial = controls.txtNewRs.val();
                    }
                }
                else {
                    that.modelChangeData.strRazonSocial = controls.txtNewRs.val();
                }
            }

            //that.modelChangeData.strRazonSocial = controls.txtNewRs.val();
            //that.modelChangeData.strObjidContacto = Session.CLIENTE.IdContactObject;
            that.modelChangeData.strNombres = controls.txtNewNombCli.val();
            that.modelChangeData.strApellidos = controls.txtNewApeCli.val();
            that.modelChangeData.strTipoDocumento = controls.cboTipDoc.val();
            that.modelChangeData.strTxtTipoDocumento = $('#cboTipDoc option:selected').text();
            that.modelChangeData.DNI_RUC = controls.txtNewNroDoc.val();
            that.modelChangeData.strCargo = controls.txtCargoDesem.val();
            that.modelChangeData.strTelefono = controls.txtPhoneChangeData.val();

            that.modelChangeData.strMovil = controls.txtMovilChangeData.val();
            that.modelChangeData.strFax = controls.txtNumberChangeData.val();
            that.modelChangeData.strMail = controls.txtEmailChangeData.val();
            that.modelChangeData.strNombreComercial = controls.txtNameComChangeData.val();
            that.modelChangeData.strContactoCliente = controls.txtNewNombCont.val();
            that.modelChangeData.dateFechaNacimiento = controls.txtDateChangeData.val();
            that.modelChangeData.strNacionalidad = $('#cboNacionalidadChangeData option:selected').val();
            that.modelChangeData.strSexo = $('input[name=rdbSexo]:checked').val();
            that.modelChangeData.strEstadoCivilId = $('#cbocivilstatus option:selected').val();
            that.modelChangeData.strEstadoCivil = $('#cbocivilstatus option:selected').text();
            that.modelChangeData.RepresentLegal = controls.txtNewNombRep.val();
            that.modelChangeData.strDNI = controls.txtNroDocRep.val();
            that.modelChangeData.strCuenta = SessionTransf.cuentaCustomer;
            that.modelChangeData.strTipoCliente = SessionTransf.TypeCustomer;
            that.modelChangeData.strMotivo = $('#cboMotivoChange option:selected').text();
            that.modelChangeData.strFlagPlataforma = SessionTransf.FlagPlataforma;
            that.modelChangeData.Accion = "R";

            that.modelChangeData.strCodAgente = SessionTransf.login;
            that.modelChangeData.strNombAgente = SessionTransf.AgentfullName;

            that.modelChangeData.strTipoDocumentoRL = controls.cboTipoDocRep.val();
            that.modelChangeData.strTxtTipoDocumentoRL = $('#cboTipoDocRep option:selected').text();

            that.modelChangeData.chkEmail = controls.chkEmail.is(':checked');

            if (controls.chkEmail[0].checked) {
                that.modelChangeData.Flag_Email = 'true';
                that.modelChangeData.strEmailSend = controls.txtEmail.val();
            } else {
                that.modelChangeData.Flag_Email = 'false';
            }

            if (that.validateForm()) {

                if (controls.cboMotivoChange.val() == SessionTransf.opcError) {

                    if (that.validateChangeName()) {
                        //that.SaveNameCustomer(that.modelChangeData);
                        resp = SessionTransac.Respuesta;
                    }
                    if (that.validateChangeMinor()) {

                        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        var texto = $("#txtEmailChangeData").val();
                        var valido = caracteres.test(texto);
                        if (!valido) {
                            alert("Ingrese un correo válido", "Informativo");
                            return false;
                        }
                        if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {
                            alert("Los correos no coinciden", "Informativo");
                            return false;
                        }


                        if (SessionTransac.Respuesta == 0) {
                            //that.SaveDataMinorCustomer(that.modelChangeData);
                        }
                        resp = SessionTransac.Respuesta;
                    }

                    if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {

                        that.modelAddressCustomer.strSessionId = SessionTransf.IDSESSION;
                        that.modelAddressCustomer.strCustomerId = SessionTransf.CUSTOMER_ID;
                        that.modelAddressCustomer.strDireccion = strDireccion.toUpperCase();
                        that.modelAddressCustomer.strReferencia = strReferencia.toUpperCase();
                        that.modelAddressCustomer.strPais = $('#cboPaisMod option:selected').text();
                        that.modelAddressCustomer.strDepartamento = $('#cboDepMod option:selected').text();
                        that.modelAddressCustomer.strProvincia = $('#cboProvinciaMod option:selected').text();
                        that.modelAddressCustomer.strDistrito = $('#cboDistritoMod option:selected').text();
                        that.modelAddressCustomer.strCodPostal = controls.txtCodPostalMod.val();
                        that.modelAddressCustomer.strMotivo = $('#cboMotivoChange option:selected').text();
                        that.modelAddressCustomer.strTipo = 'LEGAL';

                        if (that.validarMaxCaracteres() && that.validarMaxCaracteres2()) {

                            if (SessionTransac.Respuesta == 0) {
                                //that.SaveAddressCustomer(that.modelAddressCustomer);
                                SessionTransac.FlagInsDom = 1;

                            }
                            resp = SessionTransac.Respuesta;
                        }
                    }

                    if (controls.chkFacturacion[0].checked == true) {

                        if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                            that.modelAddressInvoice.strSessionId = SessionTransf.IDSESSION;
                            that.modelAddressInvoice.strCustomerId = SessionTransf.CUSTOMER_ID;
                            that.modelAddressInvoice.strDireccion = strDireccionF.toUpperCase();
                            that.modelAddressInvoice.strReferencia = strReferenciaF.toUpperCase();
                            that.modelAddressInvoice.strPais = $('#cboPaisModF option:selected').text();
                            that.modelAddressInvoice.strDepartamento = $('#cboDepModF option:selected').text();
                            that.modelAddressInvoice.strProvincia = $('#cboProvinciaModF option:selected').text();
                            that.modelAddressInvoice.strDistrito = $('#cboDistritoModF option:selected').text();
                            that.modelAddressInvoice.strCodPostal = controls.txtCodPostalModF.val();
                            that.modelAddressInvoice.strMotivo = $('#cboMotivoChange option:selected').text();
                            that.modelAddressInvoice.strTipo = 'FACT';

                            if (that.validarMaxCaracteresF() && that.validarMaxCaracteresF2()) {

                                if (SessionTransac.Respuesta == 0) {
                                    //that.SaveAddressCustomer(that.modelAddressInvoice);

                                    SessionTransac.FlagInsFact = 1;
                                }
                                resp = SessionTransac.Respuesta;
                            }
                        }


                    }




                } else {
                    if (that.validateChangeMinor()) {

                        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        var texto = $("#txtEmailChangeData").val();
                        var valido = caracteres.test(texto);
                        if (!valido) {
                            alert("Ingrese un correo válido", "Informativo");
                            return false;
                        }
                        if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {
                            alert("Los correos no coinciden", "Informativo");
                            return false;
                        }

                        if (SessionTransac.Respuesta == 0) {
                            //that.SaveDataMinorCustomer(that.modelChangeData);
                        }
                        resp = SessionTransac.Respuesta;
                    }

                    if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {

                        that.modelAddressCustomer.strSessionId = SessionTransf.IDSESSION;
                        that.modelAddressCustomer.strCustomerId = SessionTransf.CUSTOMER_ID;
                        that.modelAddressCustomer.strDireccion = strDireccion.toUpperCase();
                        that.modelAddressCustomer.strReferencia = strReferencia.toUpperCase();
                        that.modelAddressCustomer.strPais = $('#cboPaisMod option:selected').text();
                        that.modelAddressCustomer.strDepartamento = $('#cboDepMod option:selected').text();
                        that.modelAddressCustomer.strProvincia = $('#cboProvinciaMod option:selected').text();
                        that.modelAddressCustomer.strDistrito = $('#cboDistritoMod option:selected').text();
                        that.modelAddressCustomer.strCodPostal = controls.txtCodPostalMod.val();
                        that.modelAddressCustomer.strMotivo = $('#cboMotivoChange option:selected').text();
                        that.modelAddressCustomer.strTipo = 'LEGAL';


                        if (that.validarMaxCaracteres() && that.validarMaxCaracteres2()) {
                            if (SessionTransac.Respuesta == 0) {
                                //that.SaveAddressCustomer(that.modelAddressCustomer);
                                SessionTransac.FlagInsDom = 1;

                            }
                            resp = SessionTransac.Respuesta;
                        }


                    }

                    if (controls.chkFacturacion[0].checked == true) {

                        if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                            that.modelAddressInvoice.strSessionId = SessionTransf.IDSESSION;
                            that.modelAddressInvoice.strCustomerId = SessionTransf.CUSTOMER_ID;
                            that.modelAddressInvoice.strDireccion = strDireccionF.toUpperCase();
                            that.modelAddressInvoice.strReferencia = strReferenciaF.toUpperCase();
                            that.modelAddressInvoice.strPais = $('#cboPaisModF option:selected').text();
                            that.modelAddressInvoice.strDepartamento = $('#cboDepModF option:selected').text();
                            that.modelAddressInvoice.strProvincia = $('#cboProvinciaModF option:selected').text();
                            that.modelAddressInvoice.strDistrito = $('#cboDistritoModF option:selected').text();
                            that.modelAddressInvoice.strCodPostal = controls.txtCodPostalModF.val();
                            that.modelAddressInvoice.strMotivo = $('#cboMotivoChange option:selected').text();
                            that.modelAddressInvoice.strTipo = 'FACT';

                            if (that.validarMaxCaracteresF() && that.validarMaxCaracteresF2()) {

                                if (SessionTransac.Respuesta == 0) {
                                    //that.SaveAddressCustomer(that.modelAddressInvoice);

                                    SessionTransac.FlagInsFact = 1;
                                }
                                resp = SessionTransac.Respuesta;
                            }
                        }

                    }






                }


                if (resp == 0) {

                    //alert("Se actualizaron los datos correctamente", "Informativo");
                    //console.log(that.modelChangeData);
                    //console.log(that.modelAddressCustomer);
                    //console.log(that.modelAddressInvoice);
                    var objParametro = {};

                    objParametro.oModel = that.modelChangeData;
                    objParametro.oModelD = that.modelAddressCustomer;
                    objParametro.oModelF = that.modelAddressInvoice;
                    objParametro.DataOld = that.modelDataCustomerResponse;
                    objParametro.FlagD = SessionTransac.FlagInsDom;
                    objParametro.FlagF = SessionTransac.FlagInsFact;

                    $.app.ajax({
                        type: 'POST',
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objParametro),
                        url: '/Transactions/HFC/ChangeData/RegistarInteracion',
                        error: function (data) {
                            //alert(that.strMensajeDeError, "Alerta");
                            $("#btnGuardar").attr('disabled', true);
                            $("#btnConstancia").attr('disabled', true);
                        },
                        success: function (response) {
                            SessionTransac.MensajeEmail = response.MensajeEmail;
                            SessionTransac.vDesInteraction = response.vDesInteraction;
                            SessionTransac.InteractionId = response.vInteractionId;
                            SessionTransac.RutaArchivo = response.strRutaArchivo;
                            if (response.vInteractionId != '' || response.vInteractionId != "null") {
                                //$("#btnConstancia").attr('disabled', false); //Activa                        
                                var msj = response.vDesInteraction + ' Codigo de interaccion: ' + response.vInteractionId;
                                alert(msj, "Informativo");  //that.vDesInteraction

                                that.BlockControl();
                                $("#btnGuardar").attr('disabled', true);
                            }

                            if (response.RutaArchivo != '' || response.RutaArchivo != "null") {
                                $("#btnConstancia").attr('disabled', false); //Activa 
                            }
                        },
                        complete: function () {
                            $.unblockUI();
                        }
                    });
                } else {
                    alert("Ocurrió un error al tratar de actualizar la información", "Informativo");
                    $.unblockUI();
                }


            }
            //////

        },

        SaveNameCustomer: function (modelChangeData) {

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(modelChangeData),
                url: '/Transactions/HFC/ChangeData/UpdateNameCustomer',
                error: function (data) {
                    alert("Ocurrió un error al actualizar el nombre", "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                success: function (response) {
                    /* that.MensajeEmail = response.MensajeEmail;
                     that.vDesInteraction = response.vDesInteraction;
                     that.InteractionId = response.vInteractionId;
                     that.RutaArchivo = response.strRutaArchivo;*/
                    /*if (that.InteractionId != '' || that.InteractionId != "null") {
                        $("#btnConstancia").attr('disabled', false); //Activa                        
                        alert(that.vDesInteraction, "Informativo");  //that.vDesInteraction

                        that.BlockControl();
                        $("#btnGuardar").attr('disabled', true);
                    }*/

                    if (response != null) {
                        SessionTransac.Secuencia = response.seq;
                        SessionTransac.Respuesta = response.result;
                    }



                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },

        SaveDataMinorCustomer: function (modelChangeData) {

            //SessionTransac.Secuencia = response.seq;
            //SessionTransac.Respuesta = response.result;
            modelChangeData.intSeqIn = SessionTransac.Secuencia;

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(modelChangeData),
                url: '/Transactions/HFC/ChangeData/UpdateDataMinorCustomer',
                error: function (data) {
                    alert("Ocurrio un error al actualizar los datos del cliente", "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                success: function (response) {
                    /* that.MensajeEmail = response.MensajeEmail;
                     that.vDesInteraction = response.vDesInteraction;
                     that.InteractionId = response.vInteractionId;
                     that.RutaArchivo = response.strRutaArchivo;*/
                    /* if (that.InteractionId != '' || that.InteractionId != "null") {
                         $("#btnConstancia").attr('disabled', false); //Activa                        
                         alert(that.vDesInteraction, "Informativo");  //that.vDesInteraction
 
                         that.BlockControl();
                         $("#btnGuardar").attr('disabled', true);
                     }*/
                    if (response != null) {
                        SessionTransac.Secuencia = response.seq;
                        SessionTransac.Respuesta = response.result;
                    }

                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },

        SaveAddressCustomer: function (modelAddressCustomer) {

            modelAddressCustomer.intSeqIn = SessionTransac.Secuencia;

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(modelAddressCustomer),
                url: '/Transactions/HFC/ChangeData/UpdateAddressCustomer',
                error: function (data) {
                    alert("Ocurrió un error al actualizar la dirección", "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);
                },
                success: function (response) {
                    /* that.MensajeEmail = response.MensajeEmail;
                     that.vDesInteraction = response.vDesInteraction;
                     that.InteractionId = response.vInteractionId;
                     that.RutaArchivo = response.strRutaArchivo;*/
                    /*  if (that.InteractionId != '' || that.InteractionId != "null") {
                          $("#btnConstancia").attr('disabled', false); //Activa                        
                          alert(that.vDesInteraction, "Informativo");  //that.vDesInteraction
  
                          that.BlockControl();
                          $("#btnGuardar").attr('disabled', true);
                      }*/
                    if (response != null) {
                        SessionTransac.Secuencia = response.seq;
                        SessionTransac.Respuesta = response.result;
                    }
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },

        validateForm: function () {
            var that = this,
                controls = that.getControls();
            var result = true;

            if (that.validateChangeName()) {

                if (controls.txtNewNroDoc.val() == "") {
                    alert("Debe ingresar el número de documento del cliente", "Informativo");
                    $.unblockUI();
                    return false;
                }
                if (controls.txtNewNroDoc.val().length < SessionTransf.DocLongitud) {
                    alert("Debe ingresar un numero de documento correcto", "Informativo");
                    $.unblockUI();
                    return false;
                }

                var opcion = $('select[name="nmcboTipDoc"] option:selected').text();


                if (opcion == 'RUC') {

                    var tpDoc = ($('#txtNewNroDoc').val()).substring(0, 2);

                    if (tpDoc == '10') {
                        if (controls.txtNewNombCli.val() == "") {
                            alert("Debe ingresar el nombre del cliente", "Informativo");
                            $.unblockUI();
                            return false;
                        }
                        if (controls.txtNewApeCli.val() == "") {
                            alert("Debe ingresar el apellido del cliente", "Informativo");
                            $.unblockUI();
                            return false;
                        }
                    }
                    else {
                        if (controls.txtNewRs.val() == "") {
                            alert("Debe ingresar la razón social del cliente", "Informativo");
                            $.unblockUI();
                            return false;
                        }
                        if (controls.txtNameComChangeData.val() == "") {
                            alert("Debe ingresar el nombre comercial", "Informativo");
                            $.unblockUI();
                            return false;
                        }
                    }

                } else {
                    if (controls.txtNewNombCli.val() == "") {
                        alert("Debe ingresar el nombre del cliente", "Informativo");
                        $.unblockUI();
                        return false;
                    }
                    if (controls.txtNewApeCli.val() == "") {
                        alert("Debe ingresar el apellido del cliente", "Informativo");
                        $.unblockUI();
                        return false;
                    }
                }



            }
            if (that.validateChangeMinor()) {

                var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                var texto = $("#txtEmailChangeData").val();
                var valido = caracteres.test(texto);
                if (!valido) {
                    alert("Ingrese un correo válido", "Informativo");
                    $.unblockUI();
                    return false;
                }
                if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {
                    alert("Los correos no coinciden", "Informativo");
                    $.unblockUI();
                    return false;
                }

            }

            if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {

                if (controls.txtContadorD2.val() < 1 || controls.txtContadorD2.val() == "") {
                    alert("Debe ingresar una referencia del domicilio", "Informativo");
                    $.unblockUI();
                    return false;
                }
                if (controls.txtContadorD1.val() < 1 || controls.txtContadorD1.val() == "") {
                    alert("Debe ingresar una dirección de domicilio", "Informativo");
                    $.unblockUI();
                    return false;
                }

                if (that.validarMaxCaracteres()) {
                    result = true;
                } else {
                    $.unblockUI();
                    return false;
                }
                if (that.validarMaxCaracteres2()) {
                    result = true;
                } else {
                    $.unblockUI();
                    return false;
                }


            }

            if (controls.chkFacturacion[0].checked == true) {

                if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                    if (controls.txtContadorFD2.val() < 1 || controls.txtContadorFD2.val() == "") {
                        alert("Debe ingresar una referencia de dirección de facturación", "Informativo");
                        $.unblockUI();
                        return false;
                    }
                    if (controls.txtContadorFD1.val() < 1 || controls.txtContadorFD1.val() == "") {
                        alert("Debe ingresar una dirección de facturación", "Informativo");
                        $.unblockUI();
                        return false;
                    }

                    if (that.validarMaxCaracteresF()) {
                        result = true;
                    } else {
                        $.unblockUI();
                        return false;
                    }
                    if (that.validarMaxCaracteresF2()) {
                        result = true;
                    } else {
                        $.unblockUI();
                        return false;
                    }


                }

            }




            return result;
        },
        validateChangeName: function () {
            var that = this,
                controls = that.getControls();
            var result = false;

            if (SessionRespuesta.RAZON_SOCIAL != controls.txtNewRs.val()) {
                result = true;
            }
            if (SessionRespuesta.NOMBRES != controls.txtNewNombCli.val()) {
                result = true;
            }
            if (SessionRespuesta.APELLIDOS != controls.txtNewApeCli.val()) {
                result = true;
            }
            var opcion = $('select[name="nmcboTipDoc"] option:selected').text();
            if (SessionRespuesta.TIPO_DOC_C != opcion) {
                result = true;
            }
            if (SessionRespuesta.NRO_DOC != controls.txtNewNroDoc.val()) {
                result = true;
            }

            return result;
        },
        validateChangeMinor: function () {
            var that = this,
                controls = that.getControls();
            var result = false;

            if (SessionRespuesta.NOMBRE_COMERCIAL != controls.txtNameComChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.TELEF_REFERENCIA != controls.txtPhoneChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.TELEFONO_CONTACTO != controls.txtMovilChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.FECHA_NAC != controls.txtDateChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.EMAIL != controls.txtEmailChangeData.val()) {
                if (SessionTransac.Reciboxcorreo) {
                    alert("El cliente está afiliado al envío de recibo por correo electrónico. Se actualizará la dirección de envío", "Informativo");
                }

                result = true;
            }
            if (SessionRespuesta.REPRESENTANTE_LEGAL != controls.txtNewNombRep.val()) {
                result = true;
            }
            if (SessionRespuesta.CONTACTO_CLIENTE != controls.txtNewNombCont.val()) {
                result = true;
            }
            if (SessionRespuesta.LUGAR_NACIMIENTO_ID != controls.cboNacionalidadChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.ESTADO_CIVIL_ID != controls.cbocivilstatus.val()) {
                result = true;
            }
            if (SessionRespuesta.SEXO != $('input[name=rdbSexo]:checked').val()) {
                result = true;
            }

            return result;
        },

        ValidateSendxEmail: function () {

            var parameters = {};
            parameters.strIdSession = SessionTransf.IDSESSION;
            parameters.strCustomerId = SessionTransf.CUSTOMER_ID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(parameters),
                url: '/Transactions/HFC/ChangeData/GetValidateEnvioxMail',
                error: function (data) {
                },
                success: function (response) {
                    if (response.data != null) {

                        if (response.data == "A") {
                            SessionTransac.Reciboxcorreo = true;
                        } else {
                            SessionTransac.Reciboxcorreo = false;
                        }
                    }
                },
                complete: function () {

                }
            });
        },

        setSesssionsCustomer: function (objCus, cambiarfecha) {

            Session.CLIENTE.Position = objCus.CARGO;
            Session.CLIENTE.Tradename = objCus.NOMBRE_COMERCIAL;
            Session.CLIENTE.PhoneReference = objCus.TELEF_REFERENCIA;
            Session.CLIENTE.CustomerContact = objCus.CONTACTO_CLIENTE;
            Session.CLIENTE.PhoneContact = objCus.TELEFONO_CONTACTO;
            Session.CLIENTE.Fax = objCus.FAX;
            Session.CLIENTE.BirthDate = cambiarfecha;
            Session.CLIENTE.Email = objCus.EMAIL;
            Session.CLIENTE.Sex = objCus.SEXO;
            Session.CLIENTE.BirthPlaceID = objCus.LUGAR_NACIMIENTO_ID;
            Session.CLIENTE.CivilStatusID = objCus.ESTADO_CIVIL_ID;

        },
        InitCacDaC: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;
            $.ajax({
                type: 'POST',
                async: false,
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
                            controls.cboCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        InitEstCiv: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objEstCivType = {},
                parameters = {};

            objEstCivType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var EstCiv = results.MAS_DES;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objEstCivType),
                        url: '/Transactions/HFC/ChangeData/GetCivilStatus',
                        success: function (response) {
                            controls.cbocivilstatus.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.objLista != null) { }
                            var itemSelect;
                            $.each(response.objLista, function (index, value) {

                                if (EstCiv === value.Description) {
                                    controls.cbocivilstatus.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cbocivilstatus.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cbocivilstatus option[value=" + itemSelect + "]").attr("selected", true);
                            }

                            if (pid != "") { $("#cbocivilstatus option[value=" + pid + "]").attr("selected", true); }
                        }
                    });
                }
            });
        },

        InitNacionalidad: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }
            //alert(pid);
            var that = this,
                controls = that.getControls(),
                objNacType = {},
                parameters = {};

            objNacType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            var Nac = SessionTransf.BirthPlace;
            var country = SessionTransf.CountryCustomer;
            var countryF = SessionTransf.InvoiceCountry;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(objNacType),
                url: '/Transactions/HFC/ChangeData/GetNacType',
                success: function (response) {
                    controls.cboNacionalidadChangeData.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboPaisMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboPaisModF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelec2;
                    var itemSelec3;
                    $.each(response.objLista, function (index, value) {

                        if (Nac === value.Description) {
                            controls.cboNacionalidadChangeData.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            controls.cboNacionalidadChangeData.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                        if (country != null && country != "") {
                            if (country === value.Description) {

                                itemSelec2 = value.Code;
                            }
                        } else {
                            itemSelec2 = 154;
                        }
                        if (countryF != null && countryF != "") {
                            if (countryF === value.Description) {

                                itemSelec3 = value.Code;
                            }
                        } else {
                            itemSelec3 = 154;
                        }

                        controls.cboPaisMod.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboPaisModF.append($('<option>', { value: value.Code, html: value.Description }));

                        if (itemSelec2 != null && itemSelec2.toString != "undefined") {
                            $("#cboPaisMod option[value=" + itemSelec2 + "]").attr("selected", true);
                            controls.cboDepMod.prop('disabled', false);
                        }
                        if (itemSelec3 != null && itemSelec3.toString != "undefined") {
                            $("#cboPaisModF option[value=" + itemSelec3 + "]").attr("selected", true);
                            controls.cboDepModF.prop('disabled', false);
                        }


                        //$("#cboNacionalidadChangeData option[value=154]").attr("selected", true);
                        controls.cboPaisMod.prop('disabled', true);
                        controls.cboPaisModF.prop('disabled', true);
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboNacionalidadChangeData option[value=" + itemSelect + "]").attr("selected", true);
                    }

                    if (pid != "") { $("#cboNacionalidadChangeData option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitVias: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objViasType = {},
                parameters = {};

            objViasType.strIdSession = SessionTransf.IDSESSION;
            objViasType.idList = "TIPO_VIA";


            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(objViasType),
                url: '/Transactions/HFC/ChangeData/GetStateType',
                success: function (response) {

                    controls.cboTipoVia.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoViaF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }

                    $.each(response.data, function (index, value) {


                        controls.cboTipoVia.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboTipoViaF.append($('<option>', { value: value.Code, html: value.Description }));

                    });


                    if (pid != "") { $("#cboTipoVia option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitManzanas: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objManzanasType = {};

            objManzanasType.strIdSession = SessionTransf.IDSESSION;



            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(objManzanasType),
                url: '/Transactions/HFC/ChangeData/GetMzBloEdiType',
                success: function (response) {
                    controls.cboTipoMz.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoMzF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        //if (Manzana === value.Description) {
                        //    controls.cboTipoMz.append($('<option>', { value: value.Code, html: value.Description }));
                        //    itemSelect = value.Code;

                        //} else {
                        controls.cboTipoMz.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboTipoMzF.append($('<option>', { value: value.Code, html: value.Description }));
                        // }
                    });
                    //if (itemSelect != null && itemSelect.toString != "undefined") {
                    //    $("#cboTipoMz option[value=" + itemSelect + "]").attr("selected", true);
                    //}

                    if (pid != "") { $("#cboTipoMz option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitInteriores: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objInterioresType = {},
                parameters = {};

            objInterioresType.strIdSession = SessionTransf.IDSESSION;

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objInterioresType),
                url: '/Transactions/HFC/ChangeData/GetTipDptInt',
                success: function (response) {
                    controls.cboTipoInterior.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoInteriorF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {
                        controls.cboTipoInterior.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboTipoInteriorF.append($('<option>', { value: value.Code, html: value.Description }));
                    });

                    if (pid != "") { $("#cboTipoInterior option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitUrbs: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUrbsType = {},
                parameters = {};

            objUrbsType.strIdSession = SessionTransf.IDSESSION;
            objUrbsType.idList = "TIPO_URB";

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUrbsType),
                url: '/Transactions/HFC/ChangeData/GetStateType',
                success: function (response) {

                    controls.cboTipoUrb.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoUrbF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }

                    $.each(response.data, function (index, value) {

                        controls.cboTipoUrb.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboTipoUrbF.append($('<option>', { value: value.Code, html: value.Description }));

                    });

                    if (pid != "") { $("#cboTipoUrb option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitZonas: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objZonasType = {},
                parameters = {};

            objZonasType.strIdSession = SessionTransf.IDSESSION;


            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objZonasType),
                url: '/Transactions/HFC/ChangeData/GetZoneTypes',
                success: function (response) {
                    controls.cboTipoZona.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoZonaF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        //if (Zone === value.Description) {
                        //    controls.cboTipoZona.append($('<option>', { value: value.Code, html: value.Description }));
                        //    itemSelect = value.Code;

                        //} else {
                        controls.cboTipoZona.append($('<option>', { value: value.Code, html: value.Description }));
                        controls.cboTipoZonaF.append($('<option>', { value: value.Code, html: value.Description }));
                        //}
                    });
                    //if (itemSelect != null && itemSelect.toString != "undefined") {
                    //    $("#cboTipoZona option[value=" + itemSelect + "]").attr("selected", true);
                    //}

                    if (pid != "") { $("#cboTipoZona option[value=" + pid + "]").attr("selected", true); }
                }
            });

        },

        InitDepartamentos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                parameters = {};

            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            var Ubigeo1 = SessionTransf.LegalDepartament;
            var Ubigeo2 = SessionTransf.InvoiceDepartament;
            // console.log(results);
            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/HFC/ChangeData/GetDepartments',
                success: function (response) {
                    //console.log(response);
                    controls.cboDepMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboDepModF.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) { }
                    var itemSelect;
                    var itemSelect2;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo1 === value.Description) {
                            controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                            controls.cboProvinciaMod.prop('disabled', false);

                        } else {
                            controls.cboDepMod.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                        if (Ubigeo2 === value.Description) {
                            controls.cboDepModF.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect2 = value.Code;
                            controls.cboProvinciaModF.prop('disabled', false);

                        } else {
                            controls.cboDepModF.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboDepMod option[value=" + itemSelect + "]").attr("selected", true);
                        that.InitProvincias();
                    }
                    if (itemSelect2 != null && itemSelect2.toString != "undefined") {
                        $("#cboDepModF option[value=" + itemSelect2 + "]").attr("selected", true);
                    }

                    if (pid != "") { $("#cboDepMod option[value=" + pid + "]").attr("selected", true); }
                    //controls.cboProvinciaMod.prop('disabled', true);
                    //controls.cboDistritoMod.prop('disabled', true);
                }
            });

        },

        InitProvincias: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {};


            objUbigeos2Type.strIdSession = SessionTransf.IDSESSION;
            objUbigeos2Type.strDepartments = controls.cboDepMod.val();

            var Ubigeo2 = SessionTransf.ProvCustomer;
            //console.log(results);
            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos2Type),
                url: '/Transactions/HFC/ChangeData/GetProvinces',
                success: function (response) {
                    //console.log(response);
                    controls.cboProvinciaMod.empty();
                    controls.cboDistritoMod.empty();
                    controls.cboProvinciaMod.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo2 === value.Description) {
                            controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                            controls.cboDistritoMod.prop('disabled', false);

                        } else {
                            controls.cboProvinciaMod.append($('<option>', { value: value.Code, html: value.Description }));
                        }

                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboProvinciaMod option[value=" + itemSelect + "]").attr("selected", true);
                        that.InitDistritos();
                    }


                }
            });

            controls.cboProvinciaMod.prop('disabled', false);
            //that.getRulesUbigeoProv();
            //controls.cboDistritoMod.prop('disabled', true);

        },

        InitDistritos: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

            objUbigeos3Type.strIdSession = SessionTransf.IDSESSION;
            objUbigeos3Type.strDepartments = controls.cboDepMod.val();
            objUbigeos3Type.strProvinces = controls.cboProvinciaMod.val();

            var Ubigeo3 = SessionTransf.LegalDistrict;
            //console.log(results);
            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos3Type),
                url: '/Transactions/HFC/ChangeData/GetDistricts',
                success: function (response) {
                    //console.log(response);
                    controls.cboDistritoMod.empty();
                    controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo3 === value.Description) {
                            controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            controls.cboDistritoMod.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboDistritoMod option[value=" + itemSelect + "]").attr("selected", true);
                    }

                }
            });

            controls.cboDistritoMod.prop('disabled', false);
            //that.getRulesUbigeoDist();
        },

        InitPostalCode: function (pid) {

            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objPostalCode = {};

            objPostalCode.strIdSession = SessionTransf.IDSESSION;
            objPostalCode.vstrDisID = controls.cboDistritoMod.val();

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPostalCode),
                url: '/Transactions/HFC/ChangeData/ObtenerCodigoPostal',
                success: function (response) {
                    //console.log(response);
                    controls.txtCodPostalMod.val(response.data);

                }
            });

        },

        CheckSiNo: function () {
            var that = this,
            controls = that.getControls();
            var chksn = document.getElementById("chkSN");
            var objhidCheck = document.getElementById('hidCheck');
            objhidCheck.value = 'SI';
            if (chksn.checked == true) {
                controls.txtNumeroCalle.prop('disabled', true);
                controls.cboTipoMz.prop('disabled', true);
                controls.cboTipoMz.prop('selectedIndex', 0);
                controls.txtNroMz.prop('disabled', true);
                controls.txtNroMz.val('');
                controls.txtLote.prop('disabled', true);
                controls.txtLote.val('');
                controls.txtNumeroCalle.val('S/N');
                that.ContadorD1();
            }
            else {
                controls.txtNumeroCalle.prop('disabled', false);
                controls.cboTipoMz.prop('disabled', false);
                controls.txtNroMz.prop('disabled', false);
                controls.txtNroMz.val('');
                controls.txtLote.prop('disabled', false);
                controls.txtLote.val('');
                controls.txtNumeroCalle.val('');
                that.ContadorD1();
            }
        },



        InitProvinciasF: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos2Type = {};


            objUbigeos2Type.strIdSession = SessionTransf.IDSESSION;
            objUbigeos2Type.strDepartments = controls.cboDepModF.val();

            var Ubigeo2 = SessionTransf.InvoiceProvince;
            //console.log(results);
            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos2Type),
                url: '/Transactions/HFC/ChangeData/GetProvinces',
                success: function (response) {
                    //console.log(response);
                    controls.cboProvinciaModF.empty();
                    controls.cboDistritoModF.empty();
                    controls.cboProvinciaModF.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo2 === value.Description) {
                            controls.cboProvinciaModF.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            controls.cboProvinciaModF.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboProvinciaModF option[value=" + itemSelect + "]").attr("selected", true);
                    }


                }
            });

            controls.cboProvinciaModF.prop('disabled', false);
            //that.getRulesUbigeoProv();
            controls.cboDistritoModF.prop('disabled', true);

        },

        InitDistritosF: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objUbigeos3Type = {},
                parameters = {};

            objUbigeos3Type.strIdSession = SessionTransf.IDSESSION;
            objUbigeos3Type.strDepartments = controls.cboDepModF.val();
            objUbigeos3Type.strProvinces = controls.cboProvinciaModF.val();

            var Ubigeo3 = SessionTransf.InvoiceDistrict;
            //console.log(results);
            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos3Type),
                url: '/Transactions/HFC/ChangeData/GetDistricts',
                success: function (response) {
                    //console.log(response);
                    controls.cboDistritoModF.empty();
                    controls.cboDistritoModF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo3 === value.Description) {
                            controls.cboDistritoModF.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            controls.cboDistritoModF.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboDistritoModF option[value=" + itemSelect + "]").attr("selected", true);
                    }

                }
            });

            controls.cboDistritoModF.prop('disabled', false);
            //that.getRulesUbigeoDist();
        },

        InitPostalCodeF: function (pid) {

            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objPostalCode = {};

            objPostalCode.strIdSession = SessionTransf.IDSESSION;
            objPostalCode.vstrDisID = controls.cboDistritoModF.val();

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPostalCode),
                url: '/Transactions/HFC/ChangeData/ObtenerCodigoPostal',
                success: function (response) {
                    //console.log(response);
                    controls.txtCodPostalModF.val(response.data);

                }
            });

        },

        CheckSiNoF: function () {
            var that = this,
            controls = that.getControls();
            var chksn = document.getElementById("chkSNF");
            var objhidCheck = document.getElementById('hidCheckF');
            objhidCheck.value = 'SI';
            if (chksn.checked == true) {
                controls.txtNumeroCalleF.prop('disabled', true);
                controls.cboTipoMzF.prop('disabled', true);
                controls.cboTipoMzF.prop('selectedIndex', 0);
                controls.txtNroMzF.prop('disabled', true);
                controls.txtNroMzF.val('');
                controls.txtLoteF.prop('disabled', true);
                controls.txtLoteF.val('');
                controls.txtNumeroCalleF.val('S/N');
                that.ContadorFD1();
            }
            else {
                controls.txtNumeroCalleF.prop('disabled', false);
                controls.cboTipoMzF.prop('disabled', false);
                controls.txtNroMzF.prop('disabled', false);
                controls.txtNroMzF.val('');
                controls.txtLoteF.prop('disabled', false);
                controls.txtLoteF.val('');
                controls.txtNumeroCalleF.val('');
                that.ContadorFD1();
            }
        },


        getMotivoCambio: function () {
            var that = this,
               controls = that.getControls();

            $.app.ajax({
                type: "POST",
                async: false,
                url: "/Transactions/HFC/ChangeData/GetMotivoCambio",
                data: {
                    strIdSession: SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    if (response.objLista != null) {
                        var itemSelect;
                        $.each(response.objLista, function (index, value) {
                            if (value.Code == '2' && !SessionTransf.Permiso) {
                                controls.cboMotivoChange.append($('<option>', { value: value.Code, html: value.Description, 'disabled': true }));
                            } else {
                                controls.cboMotivoChange.append($('<option>', { value: value.Code, html: value.Description }));
                            }
                        });
                    }
                }
            });
        },

        getTypeDocument: function () {
            var that = this,
               controls = that.getControls();

            var TipDoc = SessionTransf.TipDoc;
            var TipDocRL = SessionTransf.TipDocRl;
            $.app.ajax({
                type: "POST",
                url: "/Transactions/HFC/ChangeData/getTypeDocument",
                data: {
                    strIdSession: SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {

                    controls.cboTipDoc.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboTipoDocRep.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    $.each(response.objLista, function (index, value) {

                        if (TipDoc === value.Description) {
                            controls.cboTipDoc.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTipDoc.append($('<option>', { value: value.Code, html: value.Description }));
                        }

                        if (TipDocRL === value.Description) {
                            controls.cboTipoDocRep.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        }
                        controls.cboTipoDocRep.append($('<option>', { value: value.Code, html: value.Description }));
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboTipDoc option[value=" + itemSelect + "]").attr("selected", true);
                    }
                    if (itemSelectRL != null && itemSelectRL.toString != "undefined") {
                        $("#cboTipoDocRep option[value=" + itemSelectRL + "]").attr("selected", true);
                    }




                }
            });

        },
        cboMotivoChange_Change: function () {
            var that = this,
               controls = that.getControls();

            if (controls.cboMotivoChange.val() == SessionTransf.opcAct) {
                controls.txtNewNombCli.prop("disabled", true);
                controls.cboTipDoc.prop("disabled", true);
                controls.txtNewApeCli.prop("disabled", true);
                controls.txtNewNroDoc.prop("disabled", true);
                controls.txtNewRs.prop("disabled", true);
                controls.txtDateChangeData.prop("disabled", true);



                if (SessionTransf.TipDoc.toUpperCase() == 'RUC') {

                    controls.NuevosDatos.css("display", "block");
                    controls.txtNewNombRep.prop("disabled", true);
                    controls.cboTipoDocRep.prop("disabled", true);
                    controls.txtNroDocRep.prop("disabled", true);
                    controls.txtNewNombCont.prop("disabled", true);

                } else {
                    controls.NuevosDatos.css("display", "none");
                }


            }
            if (controls.cboMotivoChange.val() == SessionTransf.opcError) {

                controls.txtNewNombCli.prop("disabled", false);
                controls.cboTipDoc.prop("disabled", false);
                controls.txtNewApeCli.prop("disabled", false);
                controls.txtNewNroDoc.prop("disabled", false);
                controls.txtNewRs.prop("disabled", false);
                controls.txtDateChangeData.prop("disabled", false);

                if (SessionTransf.TipDoc.toUpperCase() == 'RUC') {
                    controls.NuevosDatos.css("display", "block");
                    controls.txtNewNombRep.prop("disabled", false);
                    controls.cboTipoDocRep.prop("disabled", false);
                    controls.txtNroDocRep.prop("disabled", false);
                    controls.txtNewNombCont.prop("disabled", false);
                } else {
                    controls.NuevosDatos.css("display", "none");
                }

            }


        },
        cboTipDoc_change: function () {
            var that = this,
              controls = that.getControls();

            var opcion = $('select[name="nmcboTipDoc"] option:selected').text();


            if (opcion == 'DNI') {
                SessionTransf.tipoDato = 0;
                SessionTransf.DocLongitud = 8;

                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);

                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion == 'RUC') {
                SessionTransf.tipoDato = 0;
                SessionTransf.DocLongitud = 11;
                controls.txtNewRs.prop('disabled', false);
                controls.txtNameComChangeData.prop('disabled', false);

                controls.divNameCli.css("display", "none");
                controls.divNameC.css("display", "block");
                controls.NuevosDatos.css("display", "block");
            }
            if (opcion == 'PASAPORTE') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion.toUpperCase() == 'CARNET DE EXTRANJERÍA' || opcion == 'CE') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }
            if (opcion == 'CIE' || opcion == 'CIRE' || opcion == 'CPP' || opcion == 'CTM') {
                SessionTransf.tipoDato = 1;
                SessionTransf.DocLongitud = 12;
                controls.txtNewRs.prop('disabled', true);
                controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }

            $("#hidTipoDato").val(SessionTransf.tipoDato);
            $("#hidLongDoc").val(SessionTransf.DocLongitud);

        },
        getBusinessRules: function (SubClaseCode) {
            var that = this, controls = that.getControls();
            controls.divReglas.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/Transactions/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: SessionTransac.SessionParams.USERACCESS.userId,
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
        ShowConstancy: function (filepath, Filename) {
            var that = this,
               controls = that.getControls();
            var url = that.strUrl + '/GenerateRecord/ExistFile';

            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: url,
                data: { strFilePath: filepath, strFileName: Filename, strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.Exist == false) {
                        alert('No existe el Archivo.', "Alerta");
                    } else {
                        var url = that.strUrl + '/GenerateRecord/showInvoice';
                        window.open(url + "?strFilePath=" + Session.filepath + "&strFileName=" + Session.Filename + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                    }

                },
                error: function (ex) {
                    alert('No existe el Archivo.', "Alerta");
                }
            });
        },
        Round: function (cantidad, decimales) {

            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },
        pad: function (s) { return (s < 10) ? '0' + s : s; },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
        change_manzana: function () {
            var that = this,
            controls = that.getControls();

            document.getElementById('txtNroMz').value = '';
            document.getElementById('txtLote').value = '';

            if (document.getElementById('cboTipoMz').value == 'MZ') {
                document.getElementById('txtNumeroCalle').value = '';
                document.getElementById('txtNumeroCalle').disabled = true;
                document.getElementById('txtLote').disabled = false;
            }
            else {
                document.getElementById('txtNumeroCalle').disabled = false;
                document.getElementById('txtLote').value = '';
                document.getElementById('txtLote').disabled = true;
            }
        },
        change_manzanaF: function () {
            var that = this,
            controls = that.getControls();

            document.getElementById('txtNroMzF').value = '';
            document.getElementById('txtLoteF').value = '';

            if (document.getElementById('cboTipoMzF').value == 'MZ') {
                document.getElementById('txtNumeroCalleF').value = '';
                document.getElementById('txtNumeroCalleF').disabled = true;
                document.getElementById('txtLoteF').disabled = false;
            }
            else {
                document.getElementById('txtNumeroCalleF').disabled = false;
                document.getElementById('txtLoteF').value = '';
                document.getElementById('txtLoteF').disabled = true;
            }
        },

        ValidaTipoUrb: function () {

            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');

            if (objTipoUrbanizacion.selectedIndex > 0) {
                objNombreUrbanizacion.disabled = false;
            }
            else {
                objNombreUrbanizacion.disabled = true;
            }
        },

        ValidaTipoUrbF: function () {

            var objTipoUrbanizacion = document.getElementById('cboTipoUrbF');
            var objNombreUrbanizacion = document.getElementById('txtUrbF');

            if (objTipoUrbanizacion.selectedIndex > 0) {
                objNombreUrbanizacion.disabled = false;
            }
            else {
                objNombreUrbanizacion.disabled = true;
            }
        },

        ValidaTipoZona: function () {

            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreZona = document.getElementById('txtZona');

            if (objTipoZona.selectedIndex > 0) {
                objNombreZona.disabled = false;
            }
            else {
                objNombreZona.disabled = true;
            }
        },
        ValidaTipoZonaF: function () {

            var objTipoZona = document.getElementById('cboTipoZonaF');
            var objNombreZona = document.getElementById('txtZonaF');

            if (objTipoZona.selectedIndex > 0) {
                objNombreZona.disabled = false;
            }
            else {
                objNombreZona.disabled = true;
            }
        },

        valida_notas_direccion: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');
            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreEtapa = document.getElementById('txtZona');
            var objReferencias = document.getElementById('txtReferencia');
            var objTipoManzana = document.getElementById('cboTipoMz');
            var objLote = document.getElementById('txtLote');
            var ret = true;

            var strCad;

            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value == '') {
                ret = false;
                alert('Debe especificar el nombre de la urbanización');
                objNombreUrbanizacion.focus();
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value == '') {
                ret = false;
                alert('Debe especificar el nombre de la etapa');
                objNombreEtapa.focus();
            }
            if (objTipoManzana.value == 'MZ' && objLote.value == '') {
                ret = false;
                alert('Debe especificar el Lote');
                objLote.focus();
            }

            if (objReferencias.value == '') {
                ret = false;
                alert('Debe especificar las referencias de la dirección');
                objReferencias.focus();
            }

            strCad = '';
            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value != '') {
                strCad = strCad + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
                strCad = strCad + ' ' + objNombreUrbanizacion.value;
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value != '') {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
                strCad = strCad + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '') {
                if (strCad.length == 0) {
                    strCad = objReferencias.value;
                }
                else {
                    strCad = strCad + ' ' + objReferencias.value;
                }
            }


            if (ret) {
                var strNotas = new String(strCad);
                var intLon;
                intLon = strNotas.length;
                if (intLon > 40) {
                    ret = false;
                    alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                }
            }
            return ret;


        },

        valida_direccion: function () {
            var objTipoVia = document.getElementById('cboTipoVia');
            var objNombreCalle = document.getElementById('txtVia');
            var objNroCalle = document.getElementById('txtNumeroCalle');
            var objCheck = document.getElementById('chkSN');
            var objTipoMz = document.getElementById('cboTipoMz');
            var objManzana = document.getElementById('txtNroMz');
            var objLote = document.getElementById('txtLote');
            var objTipoInterior = document.getElementById('cboTipoInterior');
            var objNroInterior = document.getElementById('txtInterior');
            var objDireccion = document.getElementById('txtDireccion');
            var strDireccion = "";
            var ret = true;

            strCad = '';
            strCad = strCad + objTipoVia.options[objTipoVia.selectedIndex].value;
            strCad = strCad + ' ' + objNombreCalle.value;
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N') {
                    strCad = strCad + ' ' + objNroCalle.value;
                }
            }
            if (objCheck.checked) {
                strCad = strCad + ' ' + 'S/N';
            }
            if (objTipoMz.value != 'Seleccionar') {
                strCad = strCad + ' ' + objTipoMz.value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCad = strCad + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCad = strCad + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objNroInterior.value != '') {
                strCad = strCad + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
                strCad = strCad + ' ' + objNroInterior.value;
            }

            var strDireccion = new String(strCad);
            intLon = strDireccion.length;
            if (intLon > 40) {
                ret = false;
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
            }
            //console.log(strDireccion);
            objDireccion.value = strDireccion;
            return ret;

        },

        ContadorD1: function () {
            var objTipoVia = document.getElementById('cboTipoVia');
            var objNombreCalle = document.getElementById('txtVia');
            var objNroCalle = document.getElementById('txtNumeroCalle');
            var objCheck = document.getElementById('chkSN');
            var objTipoManzana = document.getElementById('cboTipoMz');
            var objManzana = document.getElementById('txtNroMz');
            var objLote = document.getElementById('txtLote');
            var objTipoInterior = document.getElementById('cboTipoInterior');
            var objNroInterior = document.getElementById('txtInterior');

            strCad = '';
            if (objTipoVia.selectedIndex > 0 && objTipoVia.value != '') {
                strCad = strCad + objTipoVia.options[objTipoVia.selectedIndex].value;
            }
            if (objNombreCalle.value != '') {
                strCad = strCad + ' ' + objNombreCalle.value;
            }
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N')
                { strCad = strCad + ' ' + objNroCalle.value; }
            } else if (objNroCalle.disabled != false) {
                strCad = strCad
            }
            if (objCheck.checked) {
                strCad = strCad + ' ' + 'S/N';
            }
            if (objTipoManzana.selectedIndex > 0 && objTipoManzana.value != '') {
                strCad = strCad + ' ' + objTipoManzana.options[objTipoManzana.selectedIndex].value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCad = strCad + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCad = strCad + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objTipoInterior.value != '') {
                strCad = strCad + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
            }
            if (objNroInterior.value != '' && objNroInterior.disabled != true) {
                strCad = strCad + ' ' + objNroInterior.value;
            }
            var strDireccion2 = new String(strCad);
            intLon = strDireccion2.length;
            document.getElementById('hidContadorD1').value = intLon;
            document.getElementById('txtContadorD1').value = intLon;
            strDireccion = strCad;
        },

        ContadorD2: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrb');
            var objNombreUrbanizacion = document.getElementById('txtUrb');
            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreEtapa = document.getElementById('txtZona');
            var objReferencias = document.getElementById('txtReferencia');

            strCad = '';
            if (objTipoUrbanizacion.selectedIndex > 0 && objTipoUrbanizacion.value != '') {
                strCad = strCad + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
            }
            if (objNombreUrbanizacion.value != '' && objNombreUrbanizacion.disabled != true) {
                strCad = strCad + ' ' + objNombreUrbanizacion.value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex > 0 && objTipoZona.value != '') {
                strCad = strCad + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex == 0) {
                strCad = strCad + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objNombreEtapa.value != '' && objNombreEtapa.disabled != true) {
                strCad = strCad + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '' && (objTipoUrbanizacion.selectedIndex > 0 || objTipoZona.selectedIndex > 0)) {
                strCad = strCad + ' ' + objReferencias.value;
            }
            else {
                strCad = strCad + objReferencias.value;
            }
            //console.log(strCad);
            var strNotas = new String(strCad);
            //console.log(strNotas);
            intLon = strNotas.length;
            document.getElementById('hidContadorD2').value = intLon;
            document.getElementById('txtContadorD2').value = intLon;
            strReferencia = strCad;
        },

        valida_notas_direccionF: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrbF');
            var objNombreUrbanizacion = document.getElementById('txtUrbF');
            var objTipoZona = document.getElementById('cboTipoZonaF');
            var objNombreEtapa = document.getElementById('txtZonaF');
            var objReferencias = document.getElementById('txtReferenciaF');
            var objTipoManzana = document.getElementById('cboTipoMzF');
            var objLote = document.getElementById('txtLoteF');
            var retF = true;

            var strCadF;

            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value == '') {
                retF = false;
                alert('Debe especificar el nombre de la urbanización');
                objNombreUrbanizacion.focus();
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value == '') {
                retF = false;
                alert('Debe especificar el nombre de la etapa');
                objNombreEtapa.focus();
            }
            if (objTipoManzana.value == 'MZ' && objLote.value == '') {
                retF = false;
                alert('Debe especificar el Lote');
                objLote.focus();
            }

            if (objReferencias.value == '') {
                retF = false;
                alert('Debe especificar las referencias de la dirección');
                objReferencias.focus();
            }

            strCadF = '';
            if (objTipoUrbanizacion.selectedIndex > 0 && objNombreUrbanizacion.value != '') {
                strCadF = strCadF + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
                strCadF = strCadF + ' ' + objNombreUrbanizacion.value;
            }

            if (objTipoZona.selectedIndex > 0 && objNombreEtapa.value != '') {
                strCadF = strCadF + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
                strCadF = strCadF + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '') {
                if (strCadF.length == 0) {
                    strCadF = objReferencias.value;
                }
                else {
                    strCadF = strCadF + ' ' + objReferencias.value;
                }
            }


            if (retF) {
                var strNotas = new String(strCadF);
                var intLonF;
                intLonF = strNotas.length;
                if (intLonF > 40) {
                    retF = false;
                    alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                }
            }
            return retF;


        },

        valida_direccionF: function () {
            var objTipoVia = document.getElementById('cboTipoViaF');
            var objNombreCalle = document.getElementById('txtViaF');
            var objNroCalle = document.getElementById('txtNumeroCalleF');
            var objCheck = document.getElementById('chkSNF');
            var objTipoMz = document.getElementById('cboTipoMzF');
            var objManzana = document.getElementById('txtNroMzF');
            var objLote = document.getElementById('txtLoteF');
            var objTipoInterior = document.getElementById('cboTipoInteriorF');
            var objNroInterior = document.getElementById('txtInteriorF');
            var objDireccion = document.getElementById('txtDireccionF');
            //var strDireccionF = "";
            var retF = true;

            strCadF = '';
            strCadF = strCadF + objTipoVia.options[objTipoVia.selectedIndex].value;
            strCadF = strCadF + ' ' + objNombreCalle.value;
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N') {
                    strCadF = strCadF + ' ' + objNroCalle.value;
                }
            }
            if (objCheck.checked) {
                strCadF = strCadF + ' ' + 'S/N';
            }
            if (objTipoMz.value != 'Seleccionar') {
                strCadF = strCadF + ' ' + objTipoMz.value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCadF = strCadF + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCadF = strCadF + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objNroInterior.value != '') {
                strCadF = strCadF + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
                strCadF = strCadF + ' ' + objNroInterior.value;
            }

            var strDireccionF = new String(strCadF);
            intLonF = strDireccionF.length;
            if (intLonF > 40) {
                retF = false;
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
            }

            objDireccion.value = strDireccionF;
            return retF;

        },

        ContadorFD1: function () {
            var objTipoVia = document.getElementById('cboTipoViaF');
            var objNombreCalle = document.getElementById('txtViaF');
            var objNroCalle = document.getElementById('txtNumeroCalleF');
            var objCheck = document.getElementById('chkSNF');
            var objTipoManzana = document.getElementById('cboTipoMzF');
            var objManzana = document.getElementById('txtNroMzF');
            var objLote = document.getElementById('txtLoteF');
            var objTipoInterior = document.getElementById('cboTipoInteriorF');
            var objNroInterior = document.getElementById('txtInteriorF');

            strCadF = '';
            if (objTipoVia.selectedIndex > 0 && objTipoVia.value != '') {
                strCadF = strCadF + objTipoVia.options[objTipoVia.selectedIndex].value;
            }
            if (objNombreCalle.value != '') {
                strCadF = strCadF + ' ' + objNombreCalle.value;
            }
            if (objNroCalle.value != '' && objNroCalle.disabled != true) {
                if (objNroCalle.value != 'S/N')
                { strCadF = strCadF + ' ' + objNroCalle.value; }
            } else if (objNroCalle.disabled != false) {
                strCadF = strCadF
            }
            if (objCheck.checked) {
                strCadF = strCadF + ' ' + 'S/N';
            }
            if (objTipoManzana.selectedIndex > 0 && objTipoManzana.value != '') {
                strCadF = strCadF + ' ' + objTipoManzana.options[objTipoManzana.selectedIndex].value;
            }
            if (objManzana.value != '' && objManzana.disabled != true) {
                strCadF = strCadF + ' ' + objManzana.value;
            }
            if (objLote.value != '' && objLote.disabled != true) {
                strCadF = strCadF + ' LT ' + objLote.value;
            }
            if (objTipoInterior.selectedIndex > 0 && objTipoInterior.value != '') {
                strCadF = strCadF + ' ' + objTipoInterior.options[objTipoInterior.selectedIndex].value;
            }
            if (objNroInterior.value != '' && objNroInterior.disabled != true) {
                strCadF = strCadF + ' ' + objNroInterior.value;
            }
            var strDireccionN = new String(strCadF);
            intLonF = strDireccionN.length;
            document.getElementById('hidContadorFD1').value = intLonF;
            document.getElementById('txtContadorFD1').value = intLonF;
            strDireccionF = strCadF;
        },

        ContadorFD2: function () {
            var objTipoUrbanizacion = document.getElementById('cboTipoUrbF');
            var objNombreUrbanizacion = document.getElementById('txtUrbF');
            var objTipoZona = document.getElementById('cboTipoZonaF');
            var objNombreEtapa = document.getElementById('txtZonaF');
            var objReferencias = document.getElementById('txtReferenciaF');

            strCadF = '';
            if (objTipoUrbanizacion.selectedIndex > 0 && objTipoUrbanizacion.value != '') {
                strCadF = strCadF + objTipoUrbanizacion.options[objTipoUrbanizacion.selectedIndex].value;
            }
            if (objNombreUrbanizacion.value != '' && objNombreUrbanizacion.disabled != true) {
                strCadF = strCadF + ' ' + objNombreUrbanizacion.value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex > 0 && objTipoZona.value != '') {
                strCadF = strCadF + ' ' + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objTipoZona.selectedIndex > 0 && objTipoUrbanizacion.selectedIndex == 0) {
                strCadF = strCadF + objTipoZona.options[objTipoZona.selectedIndex].value;
            }
            if (objNombreEtapa.value != '' && objNombreEtapa.disabled != true) {
                strCadF = strCadF + ' ' + objNombreEtapa.value;
            }

            if (objReferencias.value != '' && (objTipoUrbanizacion.selectedIndex > 0 || objTipoZona.selectedIndex > 0)) {
                strCadF = strCadF + ' ' + objReferencias.value;
            }
            else {
                strCadF = strCadF + objReferencias.value;
            }

            var strNotasF = new String(strCadF);
            //console.log(strNotasF);
            intLonF = strNotasF.length;
            document.getElementById('hidContadorFD2').value = intLonF;
            document.getElementById('txtContadorFD2').value = intLonF;
            strReferenciaF = strCadF;
        },

        validarMaxCaracteres: function () {

            nombre = $('#txtContadorD1').val();
            if (nombre < 41) {
                return true;
            }
            else {
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteres2: function () {
            nombre2 = $('#txtContadorD2').val();
            if (nombre2 < 41) {
                return true;
            }
            else {
                alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteresF: function () {

            nombre = $('#txtContadorFD1').val();
            if (nombre < 41) {
                return true;
            }
            else {
                alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteresF2: function () {
            nombre2 = $('#txtContadorFD2').val();
            if (nombre2 < 41) {
                return true;
            }
            else {
                alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        cloneAddress: function () {
            var that = this,
              controls = that.getControls();

            controls.cboTipoViaF.val(controls.cboTipoVia.val());
            controls.txtViaF.val(controls.txtVia.val());
            controls.txtNumeroCalleF.val(controls.txtNumeroCalle.val());

            var chksn = controls.chkSN.prop("checked");

            controls.chkSNF.prop("checked", chksn);

            controls.cboTipoMzF.val(controls.cboTipoMz.val());
            controls.txtNroMzF.val(controls.txtNroMz.val());

            controls.txtLoteF.val(controls.txtLote.val());

            controls.cboTipoInteriorF.val(controls.cboTipoInterior.val());
            controls.txtInteriorF.val(controls.txtInterior.val());

            controls.cboTipoUrbF.val(controls.cboTipoUrb.val());
            controls.txtUrbF.val(controls.txtUrb.val());
            that.ValidaTipoUrbF();

            controls.cboTipoZonaF.val(controls.cboTipoZona.val());
            controls.txtZonaF.val(controls.txtZona.val());

            that.ValidaTipoZonaF();

            controls.txtReferenciaF.val(controls.txtReferencia.val());


            controls.cboDepModF.val(controls.cboDepMod.val());


            $("#cboProvinciaModF").html($("#cboProvinciaMod").html());
            var item7 = $('#cboProvinciaMod option:selected').val();
            $("#cboProvinciaModF option[value=" + item7 + "]").attr("selected", true);

            $("#cboDistritoModF").html($("#cboDistritoMod").html());
            var item8 = $('#cboDistritoMod option:selected').val();
            $("#cboDistritoModF option[value=" + item8 + "]").attr("selected", true);

            controls.txtCodPostalModF.val(controls.txtCodPostalMod.val());

            that.ContadorFD1();
            that.ContadorFD2();

        }

    };


    $.fn.INTChangeData = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTChangeData'),
                options = $.extend({}, $.fn.INTChangeData.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTChangeData', data);
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

    $.fn.INTChangeData.defaults = {
    }

    $('#divBody').INTChangeData();
})(jQuery);