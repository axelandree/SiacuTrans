var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};
var mto, fec, intLon, intLonF, strReferencia, strDireccion, strReferenciaF, strDireccionF, Enviomail;
var flag = 0;

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

function process(input) {
    //var value = input.value;
    //var numbers = value.replace(/[^0-9]/g, "");
    //input.value = numbers;
}

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
    $("#txtNewNroDoc").select(function () {
        $("#hidflagdniruc").val(1);
        //selectedText = document.getSelection();
        //alert("Se ha seleccionado el texto " + selectedText);
    });
    $("#txtNewNroDoc").keydown(function (event) {

        var key = event.which || event.keyCode; // keyCode detection
        var ctrl = event.ctrlKey ? event.ctrlKey : ((event.keyCode === 17) ? true : false); // ctrl detection

        var tipoDato = $("#hidTipoDato").val();
        var longDato = $("#hidLongDoc").val();
        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDato == 0) {
                if (event.keyCode < 95) {
                    if ((event.ctrlKey || event.metaKey)) {
                    } else {
                        if (event.keyCode < 48 || event.keyCode > 57) {
                            event.preventDefault();
                        }
                    }

                }
                else {
                    if (event.keyCode < 96 || event.keyCode > 105) {
                        if ((event.ctrlKey || event.metaKey)) {
                        } else {
                            event.preventDefault();
                        }
                    }
                }

            } else {
                var caracteres = /[a-zA-Z0-9]/;
                var valido = caracteres.test(event.key);
                if (!valido) {
                    if ((event.ctrlKey || event.metaKey)) {
                    } else {
                        event.preventDefault();
                    }

                }
            }

            if ((event.ctrlKey || event.metaKey)) {// && event.keyCode == 86) {
            } else {
                if ($(this).val().length < longDato) {
                } else {
                    if ($("#hidflagdniruc").val() == 1) {
                    } else {
                        event.preventDefault();
                    }
                }
            }
            $("#hidflagdniruc").val(0);
            if ($(this).val().length == longDato - 1) {
                var opcion = $('select[name="nmcboTipDoc"] option:selected').text();
                var ValidDoc = "";

                if (opcion == "RUC") {
                    if ($(this).val().length == "10") {
                        if ($(this).val().substring(0, 2) == "10") {
                            ValidDoc = "10";
                        } else {
                            ValidDoc = "X";
                        }

                        if (ValidDoc == 'X') {
                            $('#divNameCli').css("display", "none");
                            $('#divNameC').css("display", "block");
                            if (parseInt($("#hidflagrl").val()) > 0) {
                                $('#NuevosDatos').css("display", "none");
                            } else {
                                $('#NuevosDatos').css("display", "block");
                            }
                        }
                        if (ValidDoc == '10') {
                            $('#txtNewNombCli').prop('disabled', false);
                            $('#txtNewApeCli').prop('disabled', false);
                            $('#divNameCli').css("display", "block");
                            $('#divNameC').css("display", "none");
                            $('#NuevosDatos').css("display", "none");
                        }
                    }
                } else {
                    $('#txtNewNombCli').prop('disabled', false);
                    $('#txtNewApeCli').prop('disabled', false);
                }
            }
        }
    });

    $("#txtNroDocRep").keydown(function (event) {
        var tipoDatoRL = $("#hidTipoDatoRL").val();
        var longDatoRL = $("#hidLongDocRL").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
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
    $("#txtEmailChangeData").keyup(function (event) {

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

    $("#txtConfirmarEmailChangeData").keyup(function (event) {

        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var texto = $("#txtConfirmarEmailChangeData").val() + event.key;
        var valido = caracteres.test(texto);
        if (!valido) {
            $("#txtConfirmarEmailChangeData").closest('.error-input').addClass('has-error');
        } else {
            $("#txtConfirmarEmailChangeData").closest('.error-input').removeClass('has-error');
        }

    });

    $("#txtEmail").keyup(function (event) {

        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var texto = $("#txtEmail").val() + event.key;
        var valido = caracteres.test(texto);
        if (!valido) {
            $("#txtEmail").closest('.error-input').addClass('has-error');
        } else {
            $("#txtEmail").closest('.error-input').removeClass('has-error');
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

    /*Inicio: Representante Legal*/
    $("#txtND1").keydown(function (event) {
        var tipoDatoRL = $("#hidTipDocRRLL1").val();
        var longDatoRL = $("#hidLongRRLL1").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
            }
        }
    });

    $("#txtND2").keydown(function (event) {
        var tipoDatoRL = $("#hidTipDocRRLL2").val();
        var longDatoRL = $("#hidLongRRLL2").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
            }
        }
    });

    $("#txtND3").keydown(function (event) {
        var tipoDatoRL = $("#hidTipDocRRLL3").val();
        var longDatoRL = $("#hidLongRRLL3").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
            }
        }
    });

    $("#txtND4").keydown(function (event) {
        var tipoDatoRL = $("#hidTipDocRRLL4").val();
        var longDatoRL = $("#hidLongRRLL4").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
            }
        }
    });

    $("#txtND5").keydown(function (event) {
        var tipoDatoRL = $("#hidTipDocRRLL5").val();
        var longDatoRL = $("#hidLongRRLL5").val();

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (tipoDatoRL == 0) {
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

            if ($(this).val().length < longDatoRL) {
            } else {
                event.preventDefault();
            }
        }
    });

    /*Fin: Representante Legal*/
});

(function ($, undefined) {

    var SessionRespuesta = function () { };
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    var SessionTransf = function () { };
    SessionTransf.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == '' || SessionTransac.UrlParams.IDSESSION == undefined ? "123456789874563211" : SessionTransac.UrlParams.IDSESSION;
    SessionTransf.PhonfNro = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
    SessionTransf.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
    SessionTransf.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
    SessionTransf.NameCustomer = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
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
    SessionTransf.TipDocRl = "";
    SessionTransf.opcAct = "";
    SessionTransf.opcError = "";
    SessionTransf.LegalDepartament = SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament;
    SessionTransf.LegalDistrict = SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict;
    SessionTransf.BirthPlace = SessionTransac.SessionParams.DATACUSTOMER.BirthPlace;
    SessionTransf.BirthPlaceId = SessionTransac.SessionParams.DATACUSTOMER.BirthPlaceID;
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
    
    SessionTransf.login = SessionTransac.SessionParams.USERACCESS.login;
    SessionTransf.AgentfullName = SessionTransac.SessionParams.USERACCESS.fullName;

    //INI-217
    SessionTransf.plataformaAT = SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT;
    SessionTransf.csIdPub = SessionTransac.SessionParams.DATACUSTOMER.csIdPub;
    SessionTransf.UserId = SessionTransac.SessionParams.USERACCESS.userId
    SessionTransf.BirthDate = SessionTransac.SessionParams.DATACUSTOMER.BirthDate;
    SessionTransf.Sex = SessionTransac.SessionParams.DATACUSTOMER.Sex;
    SessionTransf.CivilStatusID = SessionTransac.SessionParams.DATACUSTOMER.CivilStatusID;
    SessionTransf.LastName = SessionTransac.SessionParams.DATACUSTOMER.LastName;
    SessionTransf.Name = SessionTransac.SessionParams.DATACUSTOMER.Name;
    SessionTransf.BusinessName = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
    SessionTransf.DocumentType = SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
    SessionTransf.DNIRUC = SessionTransac.SessionParams.DATACUSTOMER.DNIRUC;
    SessionTransf.DocumentNumber = SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
    SessionTransf.DocumentTypeid = SessionTransac.SessionParams.DATACUSTOMER.DocumentTypeId;
    //SessionTransf.ContactNumberReference1 = SessionTransac.SessionParams.DATACUSTOMER.ContactNumberReference1;
    SessionTransf.ContactNumberReference1 = SessionTransac.SessionParams.DATACUSTOMER.PhoneReference;
    SessionTransf.PhoneContact = SessionTransac.SessionParams.DATACUSTOMER.PhoneContact;
    SessionTransf.Fax = SessionTransac.SessionParams.DATACUSTOMER.Fax;
    SessionTransf.Email = SessionTransac.SessionParams.DATACUSTOMER.Email;
    SessionTransf.CustomerContact = SessionTransac.SessionParams.DATACUSTOMER.CustomerContact;
    SessionTransf.LegalDepartament = SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament;
    SessionTransf.InvoiceDepartament = SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament;
    SessionTransf.LegalProvince = SessionTransac.SessionParams.DATACUSTOMER.LegalProvince;
    SessionTransf.InvoiceProvince = SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince;
    SessionTransf.LegalDistrict = SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict;
    SessionTransf.InvoiceDistrict = SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict;
    SessionTransf.LegalUrbanization = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.LegalPostal = SessionTransac.SessionParams.DATACUSTOMER.LegalPostal;
    SessionTransf.InvoiceAddress = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
    SessionTransf.InvoiceUrbanization = SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization;
    //SessionTransf.InvoicePostal = SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal;
    SessionTransf.lblDireccionLegal = SessionTransac.SessionParams.DATACUSTOMER.LegalAddress;
    SessionTransf.EstadoCivil = SessionTransac.SessionParams.DATACUSTOMER.CivilStatus;
    SessionTransf.EstadoCivilID = SessionTransac.SessionParams.DATACUSTOMER.CivilStatusID;
    SessionTransf.ReferenciaFac = SessionTransac.SessionParams.DATACUSTOMER.Reference;
    //SessionTransac.SessionParams.DATACUSTOMER.CivilStatusID == "" ? "1" : SessionTransac.SessionParams.DATACUSTOMER.CivilStatusID;
    SessionTransf.UserWhitStatusCivil = SessionTransf.EstadoCivil;
    SessionTransf.ActivationDate = SessionTransac.SessionParams.DATACUSTOMER.ActivationDate;
    //FIN-217

    SessionTransf.tipoDato = 0; ////0: numero, 1: alfanumerico
    SessionTransf.DocLongitud = 0;
    SessionTransf.tipoDatoRL = 0;
    SessionTransf.DocLongitudRL = 0;
    SessionTransf.tdCboTDO = 0;
    SessionTransf.tdCboTDOLong = 0;
    SessionTransf.tdCboTDT = 0;
    SessionTransf.tdCboTDTLong = 0;
    SessionTransf.tdCboTDW = 0;
    SessionTransf.tdCboTDWLong = 0;
    SessionTransf.tdCboTDJ = 0;
    SessionTransf.tdCboTDJLong = 0;

    SessionTransf.CambioCorreoReciboDigital = "";
    SessionTransf.CambioCorreoReciboFisico = "";
    SessionTransf.NroDocRepreLegal = "";
    SessionTransf.UpdateDataMinor = "";
    SessionTransf.UpdateRepLegal = "";
    SessionTransf.UpdateContac = "";
    SessionTransac.Reciboxcorreo = false;
    SessionTransac.Secuencia = 0;
    SessionTransac.Respuesta = 0;

    SessionTransac.MensajeEmail = "";
    SessionTransac.vDesInteraction = "";
    SessionTransac.InteractionId = "";
    SessionTransac.RutaArchivo = "";
    SessionTransac.FlagInsDom = 0;
    SessionTransac.FlagInsFact = 0;
    SessionTransac.UserWhitStatusCivil = "";

    var listaRL = {};
    var objSessions = {};
    var objRegistro;
    var totReg = 0;
    if ((SessionTransac !== undefined) &&
             (SessionTransac.SessionParams.DATACUSTOMER.listaRepresentanteLegal !== undefined)) {
        if (SessionTransac.SessionParams.DATACUSTOMER.listaRepresentanteLegal !== null) {
            listaRL = SessionTransac.SessionParams.DATACUSTOMER.listaRepresentanteLegal;
            totReg = listaRL.length;

            for (var x = 0; x < totReg; x++) {
                objRegistro = listaRL[x];
                objSessions[x] = listaRL[x];
            }
            console.log(totReg);
            document.getElementById("divRRLL").style.display = "";
            document.getElementById("NuevosDatos").style.display = "none";
            switch (totReg) {
                case 1:
                    document.getElementById("divRL1").style.display = "";
                    document.getElementById("divRL2").style.display = "none";
                    document.getElementById("divRL3").style.display = "none";
                    document.getElementById("divRL4").style.display = "none";
                    document.getElementById("divRL5").style.display = "none";
                    $("#hidflagrl").val(1);
                    break;
                case 2:
                    document.getElementById("divRL1").style.display = "";
                    document.getElementById("divRL2").style.display = "";
                    document.getElementById("divRL3").style.display = "none";
                    document.getElementById("divRL4").style.display = "none";
                    document.getElementById("divRL5").style.display = "none";
                    $("#hidflagrl").val(2);
                    break;
                case 3:
                    document.getElementById("divRL1").style.display = "";
                    document.getElementById("divRL2").style.display = "";
                    document.getElementById("divRL3").style.display = "";
                    document.getElementById("divRL4").style.display = "none";
                    document.getElementById("divRL5").style.display = "none";
                    $("#hidflagrl").val(3);
                    break;
                case 4:
                    document.getElementById("divRL1").style.display = "";
                    document.getElementById("divRL2").style.display = "";
                    document.getElementById("divRL3").style.display = "";
                    document.getElementById("divRL4").style.display = "";
                    document.getElementById("divRL5").style.display = "none";
                    $("#hidflagrl").val(4);
                    break;
                case 5:
                    document.getElementById("divRL1").style.display = "";
                    document.getElementById("divRL2").style.display = "";
                    document.getElementById("divRL3").style.display = "";
                    document.getElementById("divRL4").style.display = "";
                    document.getElementById("divRL5").style.display = "";
                    $("#hidflagrl").val(5);
                    break;
            }
            SessionTransf.listaRL = objSessions;
        } else {
            document.getElementById("divRRLL").style.display = "none";
            document.getElementById("NuevosDatos").style.display = "none";
            document.getElementById("divRL1").style.display = "none";
            document.getElementById("divRL2").style.display = "none";
            document.getElementById("divRL3").style.display = "none";
            document.getElementById("divRL4").style.display = "none";
            document.getElementById("divRL5").style.display = "none";
            document.getElementById("divRLC").style.display = "none";
        }

    } else {
        document.getElementById("divRRLL").style.display = "none";
        document.getElementById("NuevosDatos").style.display = "none";
        document.getElementById("divRL1").style.display = "none";
        document.getElementById("divRL2").style.display = "none";
        document.getElementById("divRL3").style.display = "none";
        document.getElementById("divRL4").style.display = "none";
        document.getElementById("divRL5").style.display = "none";
        document.getElementById("divRLC").style.display = "none";
    }

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
            //, lblCliente: $("#lblCliente", $element)
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
            , txtNewApeCliPat: $("#txtNewApeCliPat", $element)
            , txtNewApeCliMat: $("#txtNewApeCliMat", $element)
            , txtNewRs: $("#txtNewRs", $element)
            , txtNewNroDoc: $("#txtNewNroDoc", $element)
            , txtTelefAdic1: $("#txtTelefAdic1", $element)
            , txtTelefAdic2: $("#txtTelefAdic2", $element)
            , txtEmailAdic1: $("#txtEmailAdic1", $element)
            , txtEmailAdic2: $("#txtEmailAdic2", $element)
            , txtDireccion: $("#txtDireccion", $element)
            //, txtEmailChangeData: $("#txtEmailChangeData", $element)
            , txtNewNombRep: $("#txtNewNombRep", $element)
            , txtNewNombCont: $("#txtNewNombCont", $element)
             , cboPais: $("#cboPais", $element)
             , cboDepartamento: $("#cboDepartamento", $element)
             , cboProvincia: $("#cboProvincia", $element)
            , cboDistrito: $("#cboDistrito", $element)

            , NuevosDatos: $('#NuevosDatos', $element)
            , divRRLL: $('divRRLL',$element)
            , divRL1: $('#divRL1', $element)
            , divRL2: $('#divRL2', $element)
            , divRL3: $('#divRL3', $element)
            , divRL4: $('#divRL4', $element)
            , divRL4: $('#divRL5', $element)            
            , cboTD1: $('#cboTD1', $element)
            , cboTD2: $('#cboTD2', $element)
            , cboTD3: $('#cboTD3', $element)
            , cboTD4: $('#cboTD4', $element)
            , cboTD5: $('#cboTD5', $element)
            , txtND1: $('#txtND1', $element)
            , txtND2: $('#txtND2', $element)
            , txtND3: $('#txtND3', $element)
            , txtND4: $('#txtND4', $element)
            , txtND5: $('#txtND5', $element)
            , txtN1: $('#txtN1', $element)
            , txtN2: $('#txtN2', $element)
            , txtN3: $('#txtN3', $element)
            , txtN4: $('#txtN4', $element)
            , txtN5: $('#txtN5', $element)
            , txtAP1: $('#txtAP1', $element)
            , txtAP2: $('#txtAP2', $element)
            , txtAP3: $('#txtAP3', $element)
            , txtAP4: $('#txtAP4', $element)
            , txtAP5: $('#txtAP5', $element)
            , txtAM1: $('#txtAM1', $element)
            , txtAM2: $('#txtAM2', $element)
            , txtAM3: $('#txtAM3', $element)
            , txtAM4: $('#txtAM4', $element)
            , txtAM5: $('#txtAM5', $element)
            , txtNC: $('#txtNC', $element)
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
            controls.cboTipoDocRep.addEvent(that, 'change', that.cboTipDocRep_change);
            controls.chkFacturacion.addEvent(that, 'change', that.chkFacturacion_Change);
            controls.chkCopy.addEvent(that, 'change', that.chkCopy_Change);

            controls.cboTD1.addEvent(that, 'change', that.cboTD1_change);
            controls.cboTD2.addEvent(that, 'change', that.cboTD2_change);
            controls.cboTD3.addEvent(that, 'change', that.cboTD3_change);
            controls.cboTD4.addEvent(that, 'change', that.cboTD4_change);
            controls.cboTD5.addEvent(that, 'change', that.cboTD5_change);

            controls.cboNacionalidadChangeData.prop('disabled', true);
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
        modelListaRL:{},
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
            if (SessionTransf.plataformaAT == 'TOBE') {
                $(".clasis").hide();
                if (SessionTransf.NumbDocRepreCustomer.length == 11 && SessionTransf.NumbDocRepreCustomer.substring(0, 2) == "20") {
                    $(".cltobe").hide();
                } else {
                    $(".cltobe").show();
                }
            } else {
                $(".cltobe").hide();
                $(".clasis").show();
            }
            that.InitVias();
            that.InitManzanas();
            that.InitInteriores();
            that.InitUrbs();
            that.InitZonas();
            that.InitDepartamentos();
            that.InitPostalCode();
            that.InitPostalCodeF();

            that.loadTypification();

            if (SessionTransf.plataformaAT == 'TOBE') {
                that.ValidateEmailTobe() ///LHV: Validar afiliacion de correo electronico
            } else {
                that.ValidateSendxEmail();
            }

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
            that.cboTipDocRep_change();
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
                url: '/Transactions/Postpaid/ChangeData/PageLoad',
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

        loadDataToBE: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();

            var response = {
                objCus: {
                    FECHA_ACT: SessionTransf.ActivationDate,
                    FECHA_NAC: SessionTransf.BirthDate,
                    SEXO: SessionTransf.Sex,
                    ESTADO_CIVIL_ID: SessionTransf.CivilStatusID,
                    TIPO_DOC_RL: '',
                    APELLIDOS: SessionTransf.LastName,
                    NOMBRES: SessionTransf.Name,
                    RAZON_SOCIAL: SessionTransf.BusinessName,
                    TIPO_DOC: SessionTransf.DocumentType,
                    DNI_RUC: SessionTransf.DNIRUC,
                    NRO_DOC: SessionTransf.DocumentNumber,
                    CARGO: '',
                    NOMBRE_COMERCIAL: SessionTransf.BusinessName,
                    TELEF_REFERENCIA: SessionTransf.ContactNumberReference1,//PhoneReference(2)
                    TELEFONO_CONTACTO: SessionTransf.PhoneContact, //phoneconatct (1)
                    FAX: SessionTransf.Fax,
                    EMAIL: SessionTransf.Email == null ? "" : SessionTransf.Email,
                    REPRESENTANTE_LEGAL: SessionTransf.LegalAgent,
                    CONTACTO_CLIENTE: (SessionTransf.CustomerContact == null || SessionTransf.CustomerContact == "") ? SessionTransf.Name + " " + SessionTransf.LastName : SessionTransf.CustomerContact,
                    LUGAR_NACIMIENTO_ID: SessionTransf.BirthPlaceId,   //pendiente de agregar al servicio
                    TELEFONO_REFERENCIA_1: SessionTransf.ContactNumberReference1,
                    DEPARTEMENTO_LEGAL: SessionTransf.LegalDepartament,
                    DEPARTEMENTO_FAC: SessionTransf.InvoiceDepartament,
                    PROVINCIA_LEGAL: SessionTransf.LegalProvince,
                    PROVINCIA_FAC: SessionTransf.InvoiceProvince,
                    DISTRITO_LEGAL: SessionTransf.LegalDistrict,
                    DISTRITO_FAC: SessionTransf.InvoiceDistrict,
                    CALLE_LEGAL: SessionTransf.lblDireccionLegal,
                    URBANIZACION_LEGAL: SessionTransf.LegalUrbanization,
                    POSTAL_LEGAL: SessionTransf.LegalPostal,
                    CALLE_FAC: SessionTransf.InvoiceAddress,
                    URBANIZACION_FAC: SessionTransf.InvoiceUrbanization,
                    POSTAL_FAC: SessionTransac.SessionParams.DATACUSTOMER.InvoiceCode,
                    ESTADO_CIVIL_ID: SessionTransf.EstadoCivil,
                    ESTADO_CIVIL: SessionTransf.EstadoCivil,
                    CONTRATO_ID: SessionTransf.CONTRATO_ID,
                    LISTA_RL: SessionTransf.listaRL
                },
                strPermiso: 'SU_ACP_CDER',
                opcAct: '1',
                opcError: '2'
            };

            if (response.objCus.FECHA_NAC != "" && response.objCus.FECHA_NAC != null) {
                var fec = response.objCus.FECHA_NAC.substring(0, 10);
                response.objCus.FECHA_NAC = fec;
            }

            that.modelDataCustomerResponse.Cliente = response.objCus;
            SessionRespuesta = response.objCus;
            if (SessionRespuesta.SEXO == "") {
                SessionRespuesta.SEXO = "U";
            }

            if (that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID == null || that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID == "") {
                that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID = "-1";
            }
            if (that.modelDataCustomerResponse.Cliente.TIPO_DOC_RL == "Carnet Extranjería") {
                that.modelDataCustomerResponse.Cliente.TIPO_DOC_RL = "Carnet de Extranjería";
                SessionRespuesta.TIPO_DOC_RL = "Carnet de Extranjería";
            }


            controls.txtNewApeCli.val(response.objCus.APELLIDOS);


            controls.txtNewApeCliPat.val(SessionTransac.SessionParams.DATACUSTOMER.apellido_pat_tobe);
            controls.txtNewApeCliMat.val(SessionTransac.SessionParams.DATACUSTOMER.apellido_mat_tobe);


            controls.txtNewNombCli.val(response.objCus.NOMBRES);
            controls.txtNewRs.val(response.objCus.RAZON_SOCIAL);
            SessionTransf.TipDoc = response.objCus.TIPO_DOC;
            SessionTransf.TipDocRl = response.objCus.TIPO_DOC_RL;

            controls.txtNewNroDoc.val(response.objCus.DNI_RUC);
            controls.txtNroDocRep.val(response.objCus.NRO_DOC);
            SessionTransf.NroDocRepreLegal = response.objCus.NRO_DOC;
            controls.txtCargoDesem.val(response.objCus.CARGO);
            controls.txtNameComChangeData.val(response.objCus.NOMBRE_COMERCIAL);
            controls.txtPhoneChangeData.val(response.objCus.TELEF_REFERENCIA);
            controls.txtMovilChangeData.val(response.objCus.TELEFONO_CONTACTO);
            controls.txtDateChangeData.val(response.objCus.FECHA_NAC);
            controls.txtNumberChangeData.val(response.objCus.FAX);
            controls.txtEmail.val(response.objCus.EMAIL);
            controls.txtNewNombRep.val(response.objCus.REPRESENTANTE_LEGAL);
            controls.txtNewNombCont.val(response.objCus.CONTACTO_CLIENTE);
            SessionTransf.CustomerContact = response.objCus.CONTACTO_CLIENTE;
            SessionTransf.Nacionalidad = response.objCus.LUGAR_NACIMIENTO_ID;
            SessionTransf.EstadoCivil = response.objCus.ESTADO_CIVIL_ID;
            controls.txtNC.val(response.objCus.CONTACTO_CLIENTE);
            $("input[name=rdbSexo]").val([(response.objCus.SEXO == null || response.objCus.SEXO == "") ? 'U' : response.objCus.SEXO]);

            controls.txtEmailChangeData.val(response.objCus.EMAIL);

            if (SessionTransf.AccesPage.indexOf(response.strPermiso) > 0) {
                SessionTransf.Permiso = true;
            } else {
                SessionTransf.Permiso = false;
            }

            SessionTransf.opcAct = response.opcAct;
            SessionTransf.opcError = response.opcError;

            if (response.objCus.SEXO == "F") {

                controls.lblSexoCli.html("FEMENINO");
            }
            if (response.objCus.SEXO == "M") {
                controls.lblSexoCli.html("MASCULINO");
            }
            if (response.objCus.SEXO == "U") {
                controls.lblSexoCli.html("NA");
            }
            SessionTransf.LegalDepartament = response.objCus.DEPARTEMENTO_LEGAL;//DEPARTAMENTO LEGAL
            SessionTransf.InvoiceDepartament = response.objCus.DEPARTEMENTO_FAC;//DEPARTAMENTO FAC
            SessionTransf.ProvCustomer = response.objCus.PROVINCIA_LEGAL;//PROVINCIA LEGAL
            SessionTransf.InvoiceProvince = response.objCus.PROVINCIA_FAC;//PROVINCIA FAC
            SessionTransf.LegalDistrict = response.objCus.DISTRITO_LEGAL;//DISTRITO LEGAL
            SessionTransf.InvoiceDistrict = response.objCus.DISTRITO_FAC;//DISTRITO FAC

            controls.lblDireccionLegal.html(response.objCus.CALLE_LEGAL);//CALLE LEGAL
            controls.lblReferenciaLegal.html(response.objCus.URBANIZACION_LEGAL);//URBANIZACION LEGAL
            controls.lblCodPostalLegal.html(response.objCus.POSTAL_LEGAL);//CODIGO POSTAL LEGAL
            controls.lblDepartamentoLegal.html(response.objCus.DEPARTEMENTO_LEGAL);//DEPARTAMENTO LEGAL
            controls.lblProvinciaLegal.html(response.objCus.PROVINCIA_LEGAL);//PROVINCIA LEGAL
            controls.lblDistritoLegal.html(response.objCus.DISTRITO_LEGAL);//DISTRITO LEGAL

            controls.lblDireccion.html(response.objCus.CALLE_FAC);//CALLE FAC
            controls.lblReferencia.html(response.objCus.URBANIZACION_FAC);//URBANIZACION FAC
            controls.lblCodPostal.html(response.objCus.POSTAL_FAC);//CODIGO POSTAL FAC
            controls.lblDepartamento.html(response.objCus.DEPARTEMENTO_FAC);//DEPARTAMENTO FAC
            controls.lblProvincia.html(response.objCus.PROVINCIA_FAC);//PROVINCIA FAC
            controls.lblDistrito.html(response.objCus.DISTRITO_FAC);//DISTRITO FAC

            if ((response.objCus !== null) && (response.objCus.LISTA_RL !== undefined) && (response.objCus.LISTA_RL !== null)) {
                if (response.objCus.DNI_RUC.length === 11 && response.objCus.DNI_RUC.substring(0,2) === "20") {
                    $("#divRRLL").css("display", "");
                    var objRegRL = {};
                    var objReg;
                    var objTotReg = 0;
                    objRegRL = response.objCus.LISTA_RL;
                    objTotReg = parseInt($("#hidflagrl").val());
                    if (objTotReg > 0) {
                        $("#NuevosDatos").css("display", "none");
                    }
                    $("#hidrlIdCur").val(parseInt(objRegRL[0].idCurep));
                    switch (objTotReg) {
                        case 1:
                            controls.txtAP1.val(((objRegRL[0].cuRepapepat !== null) || (objRegRL[0].cuRepapepat !== undefined)) ? objRegRL[0].cuRepapepat : "");
                            controls.txtAM1.val(((objRegRL[0].cuRepapemat !== null) || (objRegRL[0].cuRepapemat !== undefined)) ? objRegRL[0].cuRepapemat : "");
                            controls.txtN1.val(((objRegRL[0].cuRepnombres !== null) || (objRegRL[0].cuRepnombres !== undefined)) ? objRegRL[0].cuRepnombres : "");
                            controls.txtND1.val(((objRegRL[0].cuRepnumdoc !== null) || (objRegRL[0].cuRepnumdoc !== undefined)) ? objRegRL[0].cuRepnumdoc : "");
                            that.InitTDO(parseInt(objRegRL[0].cuReptipdoc));
                            break;
                        case 2:
                            controls.txtAP1.val(((objRegRL[0].cuRepapepat !== null) || (objRegRL[0].cuRepapepat !== undefined)) ? objRegRL[0].cuRepapepat : "");
                            controls.txtAM1.val(((objRegRL[0].cuRepapemat !== null) || (objRegRL[0].cuRepapemat !== undefined)) ? objRegRL[0].cuRepapemat : "");
                            controls.txtN1.val(((objRegRL[0].cuRepnombres !== null) || (objRegRL[0].cuRepnombres !== undefined)) ? objRegRL[0].cuRepnombres : "");
                            controls.txtND1.val(((objRegRL[0].cuRepnumdoc !== null) || (objRegRL[0].cuRepnumdoc !== undefined)) ? objRegRL[0].cuRepnumdoc : "");
                            that.InitTDO(parseInt(objRegRL[0].cuReptipdoc));

                            controls.txtAP2.val(((objRegRL[1].cuRepapepat !== null) || (objRegRL[1].cuRepapepat !== undefined)) ? objRegRL[1].cuRepapepat : "");
                            controls.txtAM2.val(((objRegRL[1].cuRepapemat !== null) || (objRegRL[1].cuRepapemat !== undefined)) ? objRegRL[1].cuRepapemat : "");
                            controls.txtN2.val(((objRegRL[1].cuRepnombres !== null) || (objRegRL[1].cuRepnombres !== undefined)) ? objRegRL[1].cuRepnombres : "");
                            controls.txtND2.val(((objRegRL[1].cuRepnumdoc !== null) || (objRegRL[1].cuRepnumdoc !== undefined)) ? objRegRL[1].cuRepnumdoc : "");
                            that.InitTDT(parseInt(objRegRL[1].cuReptipdoc));

                            break;
                        case 3:
                            controls.txtAP1.val(((objRegRL[0].cuRepapepat !== null) || (objRegRL[0].cuRepapepat !== undefined)) ? objRegRL[0].cuRepapepat : "");
                            controls.txtAM1.val(((objRegRL[0].cuRepapemat !== null) || (objRegRL[0].cuRepapemat !== undefined)) ? objRegRL[0].cuRepapemat : "");
                            controls.txtN1.val(((objRegRL[0].cuRepnombres !== null) || (objRegRL[0].cuRepnombres !== undefined)) ? objRegRL[0].cuRepnombres : "");
                            controls.txtND1.val(((objRegRL[0].cuRepnumdoc !== null) || (objRegRL[0].cuRepnumdoc !== undefined)) ? objRegRL[0].cuRepnumdoc : "");
                            that.InitTDO(parseInt(objRegRL[0].cuReptipdoc));

                            controls.txtAP2.val(((objRegRL[1].cuRepapepat !== null) || (objRegRL[1].cuRepapepat !== undefined)) ? objRegRL[1].cuRepapepat : "");
                            controls.txtAM2.val(((objRegRL[1].cuRepapemat !== null) || (objRegRL[1].cuRepapemat !== undefined)) ? objRegRL[1].cuRepapemat : "");
                            controls.txtN2.val(((objRegRL[1].cuRepnombres !== null) || (objRegRL[1].cuRepnombres !== undefined)) ? objRegRL[1].cuRepnombres : "");
                            controls.txtND2.val(((objRegRL[1].cuRepnumdoc !== null) || (objRegRL[1].cuRepnumdoc !== undefined)) ? objRegRL[1].cuRepnumdoc : "");
                            that.InitTDT(parseInt(objRegRL[1].cuReptipdoc));

                            controls.txtAP3.val(((objRegRL[2].cuRepapepat !== null) || (objRegRL[2].cuRepapepat !== undefined)) ? objRegRL[2].cuRepapepat : "");
                            controls.txtAM3.val(((objRegRL[2].cuRepapemat !== null) || (objRegRL[2].cuRepapemat !== undefined)) ? objRegRL[2].cuRepapemat : "");
                            controls.txtN3.val(((objRegRL[2].cuRepnombres !== null) || (objRegRL[2].cuRepnombres !== undefined)) ? objRegRL[2].cuRepnombres : "");
                            controls.txtND3.val(((objRegRL[2].cuRepnumdoc !== null) || (objRegRL[2].cuRepnumdoc !== undefined)) ? objRegRL[2].cuRepnumdoc : "");
                            that.InitTDW(parseInt(objRegRL[2].cuReptipdoc));

                            break;
                        case 4:
                            controls.txtAP1.val(((objRegRL[0].cuRepapepat !== null) || (objRegRL[0].cuRepapepat !== undefined)) ? objRegRL[0].cuRepapepat : "");
                            controls.txtAM1.val(((objRegRL[0].cuRepapemat !== null) || (objRegRL[0].cuRepapemat !== undefined)) ? objRegRL[0].cuRepapemat : "");
                            controls.txtN1.val(((objRegRL[0].cuRepnombres !== null) || (objRegRL[0].cuRepnombres !== undefined)) ? objRegRL[0].cuRepnombres : "");
                            controls.txtND1.val(((objRegRL[0].cuRepnumdoc !== null) || (objRegRL[0].cuRepnumdoc !== undefined)) ? objRegRL[0].cuRepnumdoc : "");
                            that.InitTDO(parseInt(objRegRL[0].cuReptipdoc));

                            controls.txtAP2.val(((objRegRL[1].cuRepapepat !== null) || (objRegRL[1].cuRepapepat !== undefined)) ? objRegRL[1].cuRepapepat : "");
                            controls.txtAM2.val(((objRegRL[1].cuRepapemat !== null) || (objRegRL[1].cuRepapemat !== undefined)) ? objRegRL[1].cuRepapemat : "");
                            controls.txtN2.val(((objRegRL[1].cuRepnombres !== null) || (objRegRL[1].cuRepnombres !== undefined)) ? objRegRL[1].cuRepnombres : "");
                            controls.txtND2.val(((objRegRL[1].cuRepnumdoc !== null) || (objRegRL[1].cuRepnumdoc !== undefined)) ? objRegRL[1].cuRepnumdoc : "");
                            that.InitTDT(parseInt(objRegRL[1].cuReptipdoc));

                            controls.txtAP3.val(((objRegRL[2].cuRepapepat !== null) || (objRegRL[2].cuRepapepat !== undefined)) ? objRegRL[2].cuRepapepat : "");
                            controls.txtAM3.val(((objRegRL[2].cuRepapemat !== null) || (objRegRL[2].cuRepapemat !== undefined)) ? objRegRL[2].cuRepapemat : "");
                            controls.txtN3.val(((objRegRL[2].cuRepnombres !== null) || (objRegRL[2].cuRepnombres !== undefined)) ? objRegRL[2].cuRepnombres : "");
                            controls.txtND3.val(((objRegRL[2].cuRepnumdoc !== null) || (objRegRL[2].cuRepnumdoc !== undefined)) ? objRegRL[2].cuRepnumdoc : "");
                            that.InitTDW(parseInt(objRegRL[2].cuReptipdoc));

                            controls.txtAP4.val(((objRegRL[3].cuRepapepat !== null) || (objRegRL[3].cuRepapepat !== undefined)) ? objRegRL[3].cuRepapepat : "");
                            controls.txtAM4.val(((objRegRL[3].cuRepapemat !== null) || (objRegRL[3].cuRepapemat !== undefined)) ? objRegRL[3].cuRepapemat : "");
                            controls.txtN4.val(((objRegRL[3].cuRepnombres !== null) || (objRegRL[3].cuRepnombres !== undefined)) ? objRegRL[3].cuRepnombres : "");
                            controls.txtND4.val(((objRegRL[3].cuRepnumdoc !== null) || (objRegRL[3].cuRepnumdoc !== undefined)) ? objRegRL[3].cuRepnumdoc : "");
                            that.InitTDJ(parseInt(objRegRL[3].cuReptipdoc));

                            break;
                        case 5:
                            controls.txtAP1.val(((objRegRL[0].cuRepapepat !== null) || (objRegRL[0].cuRepapepat !== undefined)) ? objRegRL[0].cuRepapepat : "");
                            controls.txtAM1.val(((objRegRL[0].cuRepapemat !== null) || (objRegRL[0].cuRepapemat !== undefined)) ? objRegRL[0].cuRepapemat : "");
                            controls.txtN1.val(((objRegRL[0].cuRepnombres !== null) || (objRegRL[0].cuRepnombres !== undefined)) ? objRegRL[0].cuRepnombres : "");
                            controls.txtND1.val(((objRegRL[0].cuRepnumdoc !== null) || (objRegRL[0].cuRepnumdoc !== undefined)) ? objRegRL[0].cuRepnumdoc : "");
                            that.InitTDO(parseInt(objRegRL[0].cuReptipdoc));

                            controls.txtAP2.val(((objRegRL[1].cuRepapepat !== null) || (objRegRL[1].cuRepapepat !== undefined)) ? objRegRL[1].cuRepapepat : "");
                            controls.txtAM2.val(((objRegRL[1].cuRepapemat !== null) || (objRegRL[1].cuRepapemat !== undefined)) ? objRegRL[1].cuRepapemat : "");
                            controls.txtN2.val(((objRegRL[1].cuRepnombres !== null) || (objRegRL[1].cuRepnombres !== undefined)) ? objRegRL[1].cuRepnombres : "");
                            controls.txtND2.val(((objRegRL[1].cuRepnumdoc !== null) || (objRegRL[1].cuRepnumdoc !== undefined)) ? objRegRL[1].cuRepnumdoc : "");
                            that.InitTDT(parseInt(objRegRL[1].cuReptipdoc));

                            controls.txtAP3.val(((objRegRL[2].cuRepapepat !== null) || (objRegRL[2].cuRepapepat !== undefined)) ? objRegRL[2].cuRepapepat : "");
                            controls.txtAM3.val(((objRegRL[2].cuRepapemat !== null) || (objRegRL[2].cuRepapemat !== undefined)) ? objRegRL[2].cuRepapemat : "");
                            controls.txtN3.val(((objRegRL[2].cuRepnombres !== null) || (objRegRL[2].cuRepnombres !== undefined)) ? objRegRL[2].cuRepnombres : "");
                            controls.txtND3.val(((objRegRL[2].cuRepnumdoc !== null) || (objRegRL[2].cuRepnumdoc !== undefined)) ? objRegRL[2].cuRepnumdoc : "");
                            that.InitTDW(parseInt(objRegRL[2].cuReptipdoc));

                            controls.txtAP4.val(((objRegRL[3].cuRepapepat !== null) || (objRegRL[3].cuRepapepat !== undefined)) ? objRegRL[3].cuRepapepat : "");
                            controls.txtAM4.val(((objRegRL[3].cuRepapemat !== null) || (objRegRL[3].cuRepapemat !== undefined)) ? objRegRL[3].cuRepapemat : "");
                            controls.txtN4.val(((objRegRL[3].cuRepnombres !== null) || (objRegRL[3].cuRepnombres !== undefined)) ? objRegRL[3].cuRepnombres : "");
                            controls.txtND4.val(((objRegRL[3].cuRepnumdoc !== null) || (objRegRL[3].cuRepnumdoc !== undefined)) ? objRegRL[3].cuRepnumdoc : "");
                            that.InitTDJ(parseInt(objRegRL[3].cuReptipdoc));

                            controls.txtAP5.val(((objRegRL[4].cuRepapepat !== null) || (objRegRL[4].cuRepapepat !== undefined)) ? objRegRL[4].cuRepapepat : "");
                            controls.txtAM5.val(((objRegRL[4].cuRepapemat !== null) || (objRegRL[4].cuRepapemat !== undefined)) ? objRegRL[4].cuRepapemat : "");
                            controls.txtN5.val(((objRegRL[4].cuRepnombres !== null) || (objRegRL[4].cuRepnombres !== undefined)) ? objRegRL[4].cuRepnombres : "");
                            controls.txtND5.val(((objRegRL[4].cuRepnumdoc !== null) || (objRegRL[4].cuRepnumdoc !== undefined)) ? objRegRL[4].cuRepnumdoc : "");
                            that.InitTDJU(parseInt(objRegRL[4].cuReptipdoc));

                            break;
                    }
                } else { $("#divRRLL").css("display", "none"); }
            }

            controls.txtCodPostalMod.val(SessionTransf.LegalPostal);
            that.GetUserCD();
            that.GetStatusCivilToBe(SessionTransf.CivilStatusID);

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


            if (SessionTransf.TipDoc == "2") {
                controls.lblTipoDoc.html(("DNI"));
                opcion = "DNI";
            } else if (SessionTransf.TipDoc == "1") {
                controls.lblTipoDoc.html(("PASAPORTE"));
                opcion = "PASAPORTE";
            } else if (SessionTransf.TipDoc == "4") {
                controls.lblTipoDoc.html(("Carnet de Extranjería"));
                opcion = "Carnet de Extranjería";
            } else if (SessionTransf.TipDoc == "11") {
                controls.lblTipoDoc.html(("CIRE"));
                opcion = "CIRE";
            } else if (SessionTransf.TipDoc == "13") {
                controls.lblTipoDoc.html(("CPP"));
                opcion = "CPP";
            } else if (SessionTransf.TipDoc == "14") {
                controls.lblTipoDoc.html(("CTM"));
                opcion = "CTM";
            }


        },

        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("CAMBIO DE DATOS");

            //cm
            if (SessionTransf.plataformaAT == 'TOBE') {



                that.loadDataToBE();



            } else {

                var obj = {
                    strIdSession: SessionTransf.IDSESSION,
                    strTelefono: SessionTransf.PhonfNro,
                    strCustomerId: SessionTransf.CUSTOMER_ID
                };
                $.app.ajax({
                    type: 'POST',
                    cache: false,
                    async: false,
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(obj),
                    url: '/Transactions/Postpaid/ChangeData/GetCustomerChangeData',
                    success: function (response) {
                        if (response.objCus.FECHA_NAC != "" && response.objCus.FECHA_NAC != null) {
                            var fec = response.objCus.FECHA_NAC.substring(0, 10);
                            response.objCus.FECHA_NAC = fec;
                        }

                        that.modelDataCustomerResponse.Cliente = response.objCus;
                        SessionRespuesta = response.objCus;
                        if (SessionRespuesta.SEXO == "") {
                            SessionRespuesta.SEXO = "-1";
                        }

                        if (that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID == null || that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID == "") {
                            that.modelDataCustomerResponse.Cliente.ESTADO_CIVIL_ID = "-1";
                        }
                        if (that.modelDataCustomerResponse.Cliente.TIPO_DOC_RL == "Carnet Extranjería") {
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC_RL = "Carnet de Extranjería";
                            SessionRespuesta.TIPO_DOC_RL = "Carnet de Extranjería";
                        }


                        controls.txtNewApeCli.val(response.objCus.APELLIDOS);

                        controls.txtNewNombCli.val(response.objCus.NOMBRES);
                        controls.txtNewRs.val(response.objCus.RAZON_SOCIAL);
                        SessionTransf.TipDoc = response.objCus.TIPO_DOC;
                        SessionTransf.TipDocRl = response.objCus.TIPO_DOC_RL;
                        //alert('Prueba: ' + response.objCus.TIPO_DOC_RL);
                        //alert('Prueba2: ' + SessionTransf.TipDocRl);
                        controls.txtNewNroDoc.val(response.objCus.DNI_RUC);
                        controls.txtNroDocRep.val(response.objCus.NRO_DOC);
                        SessionTransf.NroDocRepreLegal = response.objCus.NRO_DOC;
                        controls.txtCargoDesem.val(response.objCus.CARGO);
                        controls.txtNameComChangeData.val(response.objCus.NOMBRE_COMERCIAL);
                        controls.txtPhoneChangeData.val(response.objCus.TELEF_REFERENCIA);
                        controls.txtMovilChangeData.val(response.objCus.TELEFONO_CONTACTO);
                        controls.txtDateChangeData.val(response.objCus.FECHA_NAC);
                        controls.txtNumberChangeData.val(response.objCus.FAX);
                        controls.txtEmail.val(response.objCus.EMAIL);
                        controls.txtNewNombRep.val(response.objCus.REPRESENTANTE_LEGAL);
                        controls.txtNewNombCont.val(response.objCus.CONTACTO_CLIENTE);
                        SessionTransf.CustomerContact = response.objCus.CONTACTO_CLIENTE;
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

                        if (response.objCus.SEXO == "F") {

                            controls.lblSexoCli.html("FEMENINO");
                        }
                        if (response.objCus.SEXO == "M") {
                            controls.lblSexoCli.html("MASCULINO");
                        }
                        if (response.objCus.SEXO == "U") {
                            controls.lblSexoCli.html("NA");
                        }
                        SessionTransf.LegalDepartament = response.objCus.DEPARTEMENTO_LEGAL;//DEPARTAMENTO LEGAL
                        SessionTransf.InvoiceDepartament = response.objCus.DEPARTEMENTO_FAC;//DEPARTAMENTO FAC
                        SessionTransf.ProvCustomer = response.objCus.PROVINCIA_LEGAL;//PROVINCIA LEGAL
                        SessionTransf.InvoiceProvince = response.objCus.PROVINCIA_FAC;//PROVINCIA FAC
                        SessionTransf.LegalDistrict = response.objCus.DISTRITO_LEGAL;//DISTRITO LEGAL
                        SessionTransf.InvoiceDistrict = response.objCus.DISTRITO_FAC;//DISTRITO FAC

                        controls.lblDireccionLegal.html(response.objCus.CALLE_LEGAL);//CALLE LEGAL
                        controls.lblReferenciaLegal.html(response.objCus.URBANIZACION_LEGAL);//URBANIZACION LEGAL
                        controls.lblCodPostalLegal.html(response.objCus.POSTAL_LEGAL);//CODIGO POSTAL LEGAL
                        controls.lblDepartamentoLegal.html(response.objCus.DEPARTEMENTO_LEGAL);//DEPARTAMENTO LEGAL
                        controls.lblProvinciaLegal.html(response.objCus.PROVINCIA_LEGAL);//PROVINCIA LEGAL
                        controls.lblDistritoLegal.html(response.objCus.DISTRITO_LEGAL);//DISTRITO LEGAL

                        controls.lblDireccion.html(response.objCus.CALLE_FAC);//CALLE FAC
                        controls.lblReferencia.html(response.objCus.URBANIZACION_FAC);//URBANIZACION FAC
                        controls.lblCodPostal.html(response.objCus.POSTAL_FAC);//CODIGO POSTAL FAC
                        controls.lblDepartamento.html(response.objCus.DEPARTEMENTO_FAC);//DEPARTAMENTO FAC
                        controls.lblProvincia.html(response.objCus.PROVINCIA_FAC);//PROVINCIA FAC
                        controls.lblDistrito.html(response.objCus.DISTRITO_FAC);//DISTRITO FAC
                    },
                    complete: function () {

                        //********** Datos del Formulario ***********/

                        controls.txtCodPostalMod.val(SessionTransf.LegalPostal);
                        //that.InitEstCiv(SessionTransf.EstadoCivil);
                        that.GetUserCD();
                        that.GetStatusCivil(SessionTransf.EstadoCivil);
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
            }


            //********** Datos del Cliente ***********/
            controls.lblContrato.html((SessionTransf.CONTRATO_ID == null) ? '' : SessionTransf.CONTRATO_ID);
            controls.lblCustomerID.html((SessionTransf.CUSTOMER_ID == null) ? '' : SessionTransf.CUSTOMER_ID);
            controls.lblTipoCliente.html((SessionTransac.SessionParams.DATACUSTOMER.CustomerType == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            //controls.lblCliente.html((SessionTransf.RepreCustomer == null) ? '' : SessionTransf.RepreCustomer);


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

            //controls.lblTipoDoc.html((SessionTransac.SessionParams.DATACUSTOMER.DocumentType == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.DocumentType);
            var ValidDoc = "";
            var opcion = "";//SessionTransf.TipDoc.toUpperCase();
            if (SessionTransf.NumbDocRepreCustomer != null) {

                $("#hidDNIRUC").val(SessionTransf.NumbDocRepreCustomer);

                if (SessionTransf.plataformaAT == 'TOBE') {
                
                        if (SessionTransf.TypDocRepreCustomer.indexOf('Carnet') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CARNET') > -1 ||
                            SessionTransf.TypDocRepreCustomer.indexOf('ce') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CE') > -1) {
                            controls.lblTipoDoc.html(("Carnet de Extranjería"));
                            opcion = "Carnet de Extranjería";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "Carnet de Extranjería";
                            SessionRespuesta.TIPO_DOC = "Carnet de Extranjería";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cie') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIE') > -1)) {
                            controls.lblTipoDoc.html(("CIE"));
                            opcion = "CIE";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CIE";
                            SessionRespuesta.TIPO_DOC = "CIE";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cire') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIRE') > -1)) {
                            controls.lblTipoDoc.html(("CIRE"));
                            opcion = "CIRE";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CIRE";
                            SessionRespuesta.TIPO_DOC = "CIRE";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cpp') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CPP') > -1)) {
                            controls.lblTipoDoc.html(("CPP"));
                            opcion = "CPP";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CPP";
                            SessionRespuesta.TIPO_DOC = "CPP";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ctm') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CTM') > -1)) {
                            controls.lblTipoDoc.html(("CTM"));
                            opcion = "CTM";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CTM";
                            SessionRespuesta.TIPO_DOC = "CTM";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Dni') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('DNI') > -1)) {
                            controls.lblTipoDoc.html(("DNI"));
                            opcion = "DNI";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "DNI";
                            SessionRespuesta.TIPO_DOC = "DNI";
                        } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Pasaporte') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('PASAPORTE') > -1)) {
                            controls.lblTipoDoc.html(("Pasaporte"));
                            opcion = "Pasaporte";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "Pasaporte";
                            SessionRespuesta.TIPO_DOC = "Pasaporte";
                        }
                        else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ruc') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('RUC') > -1)) {
                            controls.lblTipoDoc.html(("RUC"));
                            opcion = "RUC";
                            that.modelDataCustomerResponse.Cliente.TIPO_DOC = "RUC";
                            SessionRespuesta.TIPO_DOC = "RUC";
                        }
            }else{
                //ASIS
                if (SessionTransf.NumbDocRepreCustomer.length == 11)
                {
                    ValidDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                    if (ValidDoc != "10") {
                        controls.lblTipoDoc.html(("RUC"));
                        opcion = "RUC";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "RUC";
                        SessionRespuesta.TIPO_DOC = "RUC";
                    } else if (ValidDoc == "10") {
                        controls.lblTipoDoc.html(("RUC"));
                        opcion = "RUC";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "RUC";
                        SessionRespuesta.TIPO_DOC = "RUC";
                    }
                } else {
                    if (SessionTransf.TypDocRepreCustomer.indexOf('Carnet') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CARNET') > -1 ||
                        SessionTransf.TypDocRepreCustomer.indexOf('ce') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CE') > -1) {
                        controls.lblTipoDoc.html(("Carnet de Extranjería"));
                        opcion = "Carnet de Extranjería";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "Carnet de Extranjería";
                        SessionRespuesta.TIPO_DOC = "Carnet de Extranjería";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cie') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIE') > -1)) {
                        controls.lblTipoDoc.html(("CIE"));
                        opcion = "CIE";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CIE";
                        SessionRespuesta.TIPO_DOC = "CIE";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cire') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIRE') > -1)) {
                        controls.lblTipoDoc.html(("CIRE"));
                        opcion = "CIRE";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CIRE";
                        SessionRespuesta.TIPO_DOC = "CIRE";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cpp') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CPP') > -1)) {
                        controls.lblTipoDoc.html(("CPP"));
                        opcion = "CPP";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CPP";
                        SessionRespuesta.TIPO_DOC = "CPP";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ctm') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CTM') > -1)) {
                        controls.lblTipoDoc.html(("CTM"));
                        opcion = "CTM";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "CTM";
                        SessionRespuesta.TIPO_DOC = "CTM";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Dni') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('DNI') > -1)) {
                        controls.lblTipoDoc.html(("DNI"));
                        opcion = "DNI";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "DNI";
                        SessionRespuesta.TIPO_DOC = "DNI";
                    } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Pasaporte') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('PASAPORTE') > -1)) {
                        controls.lblTipoDoc.html(("Pasaporte"));
                        opcion = "Pasaporte";
                        that.modelDataCustomerResponse.Cliente.TIPO_DOC = "Pasaporte";
                        SessionRespuesta.TIPO_DOC = "Pasaporte";
                    }
                }
            }
            }

            if (SessionTransf.NumbDocRepreCustomer != null) {
                if (SessionTransf.NumbDocRepreCustomer.length == 11) {

                    ValidDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                    if (ValidDoc != "10") {
                        //controls.trRL.css('display', '');
                        //controls.trCC.css('trCC', '');
                        controls.lblRepLegal.html((SessionTransf.LegalAgent == null) ? '' : SessionTransf.LegalAgent);
                        controls.lblContCliente.html((SessionTransf.CustomerContact == null) ? '' : SessionTransf.CustomerContact);
                        document.getElementById("trRL").style.display = '';
                        document.getElementById("trCC").style.display = '';
                    } else {
                        //controls.trRL.css('display', 'none');
                        //controls.trCC.css('display', 'none');
                        document.getElementById("trRL").style.display = 'none';
                        document.getElementById("trCC").style.display = 'none';
                    }
                } else {
                    //controls.trRL.css('display', 'none');
                    //controls.trCC.css('display', 'none');
                    document.getElementById("trRL").style.display = 'none';
                    document.getElementById("trCC").style.display = 'none';
                }
            }







            controls.lblEstadoCiv.html((SessionTransac.SessionParams.DATACUSTOMER.CivilStatus == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.CivilStatus);

            //********** Direccíón de Facturación ***********/
            //controls.lblDireccion.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
            //controls.lblReferencia.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization);
            //controls.lblCodPostal.html((SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoicePostal);
            //controls.lblDepartamento.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament);
            //controls.lblProvincia.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince);
            //controls.lblDistrito.html((SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict);

            //********** Datos del Formulario ***********/

            //********** Direccíón de Domicilio ***********/
            //controls.lblDireccionLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalAddress == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalAddress);
            //controls.lblReferenciaLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization);
            //controls.lblCodPostalLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalPostal == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalPostal);
            //controls.lblDepartamentoLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament);
            //controls.lblProvinciaLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalProvince == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalProvince);
            //controls.lblDistritoLegal.html((SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict == null) ? '' : SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict);

            //********** Datos del Formulario ***********/

            //if (SessionTransf.TypeCustomer.toUpperCase() == 'CONSUMER') {
            //    controls.divNameC.css("display", "none");
            //} else {
            //    controls.divNameC.css("display", "block");
            //}



            if (opcion == 'DNI') {
                //??SessionTransf.tipoDato = 0;
                $("#hidTipoDato").val(0);
                //??SessionTransf.DocLongitud = 8;
                $("#hidLongDoc").val(8);

                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);

                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            } else if (opcion == 'RUC') {
                //??SessionTransf.tipoDato = 0;
                $("#hidTipoDato").val(0);
                //??SessionTransf.DocLongitud = 11;
                $("#hidLongDoc").val(11);

                //controls.txtNewRs.prop('disabled', false);
                //controls.txtNameComChangeData.prop('disabled', false);
                if (SessionTransf.NumbDocRepreCustomer.length == 11) {
                    if (ValidDoc == "10") {
                        controls.NuevosDatos.css("display", "none");
                        controls.divNameCli.css("display", "block");
                        controls.divNameC.css("display", "none");
                    } else {
                        if (parseInt($("#hidflagrl").val()) > 0) {
                            controls.NuevosDatos.css("display", "none");
                        } else {
                            controls.NuevosDatos.css("display", "block");
                        }
                        controls.divNameCli.css("display", "none");
                        controls.divNameC.css("display", "block");
                    }
                }


            } else if (opcion == 'Pasaporte') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 12;
                $("#hidLongDoc").val(12);

                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            } else if (opcion == 'Carnet de Extranjería' || opcion == 'CE') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 9;
                $("#hidLongDoc").val(9);

                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            } else if (opcion == 'CIE' || opcion == 'CIRE' || opcion == 'CPP' || opcion == 'CTM') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 12;
                $("#hidLongDoc").val(12);

                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            } else {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 12;
                $("#hidLongDoc").val(12);

                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
            }

            //??$("#hidTipoDato").val(SessionTransf.tipoDato);
            //??$("#hidLongDoc").val(SessionTransf.DocLongitud);

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
                url: '/Transactions/Postpaid/ChangeData/GetAditionalData',
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

            control.cboTipoVia.val('');
            control.txtVia.val('');
            control.txtNumeroCalle.val('');
            control.chkSN.prop('checked', false);
            control.cboTipoMz.val('');
            control.txtNroMz.val('');
            control.txtLote.val('');
            control.cboTipoInterior.val('');
            control.txtInterior.val('');
            control.cboTipoUrb.val('');
            control.txtUrb.val('');
            control.cboTipoZona.val('');
            control.txtZona.val('');
            control.txtReferencia.val('');
            control.cboDepMod.val('');
            control.cboProvinciaMod.val('');
            control.cboDistritoMod.val('');
            control.txtCodPostalMod.val('');
            control.txtContadorD1.val('');
            control.txtContadorD2.val('');
            control.txtNumeroCalle.prop('disabled', false);
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

            var that = this,
                controls = this.getControls();
var msgConfirmacion="¿Seguro que desea guardar la transacción?";
            that.isPostBackFlag = that.NumeracionUNO;

            if (controls.cboNacionalidadChangeData.val() == "-1" || controls.cboNacionalidadChangeData.val() == "") {
                alert("Seleccione la nacionalidad del cliente.", "Alerta");
                return false;
            }

            if (controls.cboCACDAC.val() == "-1" || controls.cboCACDAC.val() == "") {
                alert("Seleccione Punto de Atención.", "Alerta");
                return false;
            }
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
                    alert("Ingrese un de Envío por E-Mail", "Informativo");
                    return false;
                }
            }

            //------------------------------------------------------------------------------------------
            //Variables para tipificacion


            var resp = 0;

            //Datos Sensibles Cel.1
            that.modelChangeData.strTipoDocumentoAnt = SessionTransf.DocumentTypeid
            that.modelChangeData.strNroDocAnt = SessionTransf.DocumentNumber
            that.modelChangeData.NombCompleto = (controls.txtNewNombCli.val() + ' ' + controls.txtNewApeCliPat.val() + ' ' + controls.txtNewApeCliMat.val());
            var opcion = $('select[name="nmcboTipDoc"] option:selected').text();
            if (opcion !== 'RUC') {
                that.modelChangeData.strRazonSocialNew = that.modelChangeData.NombCompleto;
            } else {
                var tpDoc = (controls.txtNewNroDoc.val()).substring(0, 2);
                if (tpDoc == '10') {
                    that.modelChangeData.strRazonSocialNew = that.modelChangeData.NombCompleto;
                }
                else {
                    that.modelChangeData.strRazonSocialNew = controls.txtNewRs.val();
                }
            }
            if (that.modelChangeData.NombCompleto != "") {
                if (that.modelChangeData.NombCompleto.length > 20) {
                    that.modelChangeData.strNombreComercialNew = that.modelChangeData.NombCompleto.substring(0, 20);
                } else {
                    that.modelChangeData.strNombreComercialNew = that.modelChangeData.NombCompleto;
                }
            }
            that.modelChangeData.direccionLegal = "";
            that.modelChangeData.direccionReferenciaLegal = "";
            that.modelChangeData.paisLegal = "";
            that.modelChangeData.departamentoLegal = "";
            that.modelChangeData.provinciaLegal = "";
            that.modelChangeData.distritoLegal = "";
            that.modelChangeData.urbanizacionLegal = "";
            that.modelChangeData.codigoPostalLegal = "";
            that.modelChangeData.DNIRUCNew = controls.txtNewNroDoc.val();
            //------------------------INI DATOS GENERALES----------------------

            that.modelChangeData.strIdSession = SessionTransf.IDSESSION;
            that.modelChangeData.strCacDac = $('#cboCACDAC option:selected').text();
            that.modelChangeData.strCacDacId = $('#cboCACDAC option:selected').val();
            that.modelChangeData.strNote = $("#txtNote").val();
            that.modelChangeData.strPhone = SessionTransf.PhonfNro;
            that.modelChangeData.strCustomerId = SessionTransf.CUSTOMER_ID;
            that.modelChangeData.adrNote1 = $('#cboTipoViaF option:selected').text() + ' ' + $("#txtViaF").val() + ' ' + $('#txtNumeroCalleF').val();
            

            if (opcion !== 'RUC') {
                that.modelChangeData.strRazonSocial = controls.txtNewNombCli.val() + ' ' + controls.txtNewApeCli.val();
            } else {
                //if (opcion == 'RUC') {
                var tpDoc = (controls.txtNewNroDoc.val()).substring(0, 2);
                if (tpDoc == '10') {
                    that.modelChangeData.strRazonSocial = controls.txtNewNombCli.val() + ' ' + controls.txtNewApeCli.val();
                }
                else {
                    that.modelChangeData.strRazonSocial = controls.txtNewRs.val();
                }

            }

            that.modelChangeData.strNombres = controls.txtNewNombCli.val();

            if (controls.txtNewApeCliPat.val() !== '') {
                that.modelChangeData.strApellidos = controls.txtNewApeCliPat.val() + ' ' + controls.txtNewApeCliMat.val();

            } else {
                that.modelChangeData.strApellidos = controls.txtNewApeCli.val();
            }

            that.modelChangeData.strApellidosPat = controls.txtNewApeCliPat.val();
            that.modelChangeData.strApellidosMat = controls.txtNewApeCliMat.val();
            that.modelChangeData.strTipoDocumento = controls.cboTipDoc.val();
            that.modelChangeData.strTxtTipoDocumento = $('#cboTipDoc option:selected').text();
            that.modelChangeData.DNI_RUC = controls.txtNewNroDoc.val();
            that.modelChangeData.strCargo = controls.txtCargoDesem.val();
            that.modelChangeData.strTelefono = controls.txtPhoneChangeData.val();

            that.modelChangeData.strMovil = controls.txtMovilChangeData.val();
            that.modelChangeData.strFax = controls.txtNumberChangeData.val();
            that.modelChangeData.strMail = controls.txtEmailChangeData.val();
            if (controls.txtNameComChangeData.val() != "") {
                if (controls.txtNameComChangeData.val().length > 20) {
                    that.modelChangeData.strNombreComercial = controls.txtNameComChangeData.val().substring(0, 20);
                } else {
                    that.modelChangeData.strNombreComercial = controls.txtNameComChangeData.val();
                }
            }

            /*Inicio: Contacto*/
            if (controls.txtNC.val() != "") {
                if (controls.txtNC.val().length > 40) {
                    that.modelChangeData.strContactoCliente = controls.txtNC.val().substring(0, 40);
                } else {
                    that.modelChangeData.strContactoCliente = controls.txtNC.val();
                }
            }
            /*Fin: Contacto*/
            that.modelChangeData.dateFechaNacimiento = controls.txtDateChangeData.val();
            that.modelChangeData.strNacionalidad = $('#cboNacionalidadChangeData option:selected').val();
            //cm
            that.modelChangeData.lugarNacimiento = $('#cboNacionalidadChangeData option:selected').text();
            //cm
            that.modelChangeData.strSexo = $('input[name=rdbSexo]:checked').val();
            that.modelChangeData.strEstadoCivilId = $('#cbocivilstatus option:selected').val();
            that.modelChangeData.strEstadoCivil = $('#cbocivilstatus option:selected').text();
            /*Inicio: RL*/
            if (controls.txtNewNombRep.val() != "") {
                if (controls.txtNewNombRep.val().length > 20) {
                    that.modelChangeData.RepresentLegal = controls.txtNewNombRep.val().substring(0, 20);
                    that.modelChangeData.NameRepLegalCurrent = controls.txtNewNombRep.val().substring(0, 20);
                } else {
                    that.modelChangeData.RepresentLegal = controls.txtNewNombRep.val();
                    that.modelChangeData.NameRepLegalCurrent = controls.txtNewNombRep.val();
                }
            }
            /*Inicio: RL*/
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

            if (SessionRespuesta.EMAIL != controls.txtEmailChangeData.val()) {
                if (SessionTransac.Reciboxcorreo) {
                    SessionTransf.CambioCorreoReciboDigital = "Cliente se encuentra afiliado a recibo por correo, se procede a actualizar la dirección de envío";
                }
            }

            //-------------------------------FIN DATOS GENERALES------------------------------------------

            //-------------------------------INI DIRECCION LEGAL------------------------------------------

            that.modelAddressCustomer.strSessionId = SessionTransf.IDSESSION;
            that.modelAddressCustomer.strCustomerId = SessionTransf.CUSTOMER_ID;
            that.modelAddressCustomer.strPais = SessionTransf.plataformaAT == 'TOBE' ? $('#cboPaisMod option:selected').val() : $('#cboPaisMod option:selected').text();
            that.modelAddressCustomer.strDepartamento = $('#cboDepMod option:selected').text();
            that.modelAddressCustomer.strProvincia = $('#cboProvinciaMod option:selected').text();
            that.modelAddressCustomer.strDistrito = $('#cboDistritoMod option:selected').text();
            that.modelAddressCustomer.strCodPostal = controls.txtCodPostalMod.val();
            that.modelAddressCustomer.strMotivo = $('#cboMotivoChange option:selected').text();
            that.modelAddressCustomer.strTipo = 'LEG';
            that.modelAddressCustomer.strNacionalidad = $('#cboPaisMod').val();

            //cm
            that.modelAddressCustomer.strStreet = $('#cboTipoViaF option:selected').val() + ' ' + $("#txtViaF").val() + ' ' + $("#txtNumeroCalleF").val() + ' ' + $("#cboTipoMzF option:selected").val() + ' ' + $("#txtNroMzF").val();
            that.modelAddressCustomer.csIdPub = SessionTransac.SessionParams.DATACUSTOMER.csIdPub;
            that.modelAddressCustomer.userId = SessionTransac.SessionParams.USERACCESS.userId;

            //cm end

            //-------------------------------FIN DIRECCION LEGAL------------------------------------------

            //-------------------------------INI DIRECCION FACTURACION------------------------------------------

            that.modelAddressInvoice.strSessionId = SessionTransf.IDSESSION;
            that.modelAddressInvoice.strCustomerId = SessionTransf.CUSTOMER_ID;
            that.modelAddressInvoice.strPais = $('#cboPaisModF option:selected').text();
            that.modelAddressInvoice.strDepartamento = $('#cboDepModF option:selected').text();
            that.modelAddressInvoice.strProvincia = $('#cboProvinciaModF option:selected').text();
            that.modelAddressInvoice.strDistrito = $('#cboDistritoModF option:selected').text();
            that.modelAddressInvoice.strCodPostal = controls.txtCodPostalModF.val();
            that.modelAddressInvoice.strMotivo = $('#cboMotivoChange option:selected').text();
            that.modelAddressInvoice.strTipo = 'FACT';
            that.modelAddressInvoice.adrSeq = Session.adrSeq;
            that.modelAddressInvoice.adrStreet = $('#txtViaF').val();
            that.modelAddressInvoice.adrStreetNo = $('#txtNumeroCalleF').val();
            that.modelAddressInvoice.adrNote1 = $('#cboTipoViaF option:selected').val();
            that.modelAddressInvoice.adrNote2 = $('#cboTipoMzF option:selected').val();
            var lote = "";
            if ($('#cboTipoMzF option:selected').val() == "MZ") {
                lote = 'LT ' + $('#txtLoteF').val() + ' ';
            }
            that.modelAddressInvoice.adrNote3 = $('#txtNroMzF').val() + '  ' + lote + $('#cboTipoInteriorF option:selected').val() + '  ' + $('#txtInteriorF').val();
            that.modelAddressInvoice.adrJbdes = $('#cboTipoUrbF option:selected').val() + '  ' + $('#txtUrbF').val() + ' ' + $('#cboTipoZonaF option:selected').val() + ' ' + $('#txtZonaF').val() + ' ' + $('#txtReferenciaF').val();

            //-------------------------------FIN DIRECCION FACTURACION------------------------------------------

            //----------------------------------------------------------------contiene validacion de validateForm


            if (that.validateChangeName()) {
                if (controls.txtNewNroDoc.val() == "") {
                    alert("Debe ingresar el número de documento del cliente", "Informativo");
                    //$.unblockUI();
                    return false;
                }
                //Quitar Comentado KGAYTAN
                //////if (controls.txtNewNroDoc.val().length < SessionTransf.DocLongitud) {
                //////    alert("Debe ingresar un numero de documento correcto", "Informativo");
                //////    //$.unblockUI();
                //////    return false;
                //////}

                //if (controls.cbocivilstatus.val() == "-1") {
                //    alert("Seleccione Estado Civil", "Informativo");
                //    return false;
                //}

                var opcion = $('select[name="nmcboTipDoc"] option:selected').text();
                if (opcion == 'RUC') {

                    var tpDoc = ($('#txtNewNroDoc').val()).substring(0, 2);

                    if (tpDoc == '10') {
                        if (controls.txtNewNombCli.val() == "") {
                            alert("Debe ingresar el nombre del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                        if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT == "ASIS") {
                            if (controls.txtNewApeCli.val() == "") {
                                alert("Debe ingresar el apellido del cliente", "Informativo");
                                //$.unblockUI();
                                return false;
                            }
                        } else {
                            if (controls.txtNewApeCliPat.val() == "") {
                                alert("Debe ingresar el apellido del cliente", "Informativo");
                                //$.unblockUI();
                                return false;
                            }
                            if (controls.txtNewApeCliMat.val() == "") {
                                alert("Debe ingresar el apellido del cliente", "Informativo");
                                //$.unblockUI();
                                return false;
                            }
                        }
                    }
                    else {
                        if (controls.txtNewRs.val() == "") {
                            alert("Debe ingresar la razón social del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                        //if (controls.txtNameComChangeData.val() == "") {
                        //    alert("Debe ingresar el nombre comercial", "Informativo");
                        //    //$.unblockUI();
                        //    return false;
                        //}
                    }

                } else {

                    if (controls.txtNewNombCli.val() == "") {
                        alert("Debe ingresar el nombre del cliente", "Informativo");
                        //$.unblockUI();
                        return false;
                    }
                    if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT == "ASIS") {
                        if (controls.txtNewApeCli.val() == "") {
                            alert("Debe ingresar el apellido del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                    } else {
                        if (controls.txtNewApeCliPat.val() == "") {
                            alert("Debe ingresar el apellido del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                        if (controls.txtNewApeCliMat.val() == "") {
                            alert("Debe ingresar el apellido del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                    }


                }
            }

            if (that.validateChangeMinor()) {

                if ($("#txtEmailChangeData").val() != "") {
                    var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    var texto = $("#txtEmailChangeData").val();
                    var valido = caracteres.test(texto);
                    if (!valido) {
                        alert("Ingrese un correo válido", "Informativo");
                        //$.unblockUI();
                        return false;
                    }
                }
                if (SessionRespuesta.EMAIL != $("#txtEmailChangeData").val()) {
                    if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {
                        alert("Los correos no coinciden", "Informativo");
                        //$.unblockUI();
                        return false;
                    }
                }
            }


            if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                if (controls.txtContadorFD2.val() < 1 || controls.txtContadorFD2.val() == "") {
                    alert("Debe ingresar una referencia de dirección de facturación", "Informativo");
                    //$.unblockUI();
                    return false;
                }
                if (controls.txtContadorFD1.val() < 1 || controls.txtContadorFD1.val() == "") {
                    alert("Debe ingresar una dirección de facturación", "Informativo");
                    //$.unblockUI();
                    return false;
                }

                if (!(that.validarMaxCaracteresF())) {
                    alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                    return false;
                }
                if (!(that.validarMaxCaracteresF2())) {
                    alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                    return false;
                }
            }
            //}


            //-----------------------------------------------------------
            if (controls.cboMotivoChange.val() == SessionTransf.opcError) {
                console.log(71);
                //1313
                if (that.validateChangeName()) {

                    console.log(1);
                    that.modelChangeData.FLAG_CUSTOMER = "CUSTOMER";
                    //url: '/Transactions/Postpaid/ChangeData/UpdateNameCustomer'
                }
                if (that.validateChangeMinor()) {
                    console.log(2);
                    if ($("#txtEmailChangeData").val() != "") {
                        var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        var texto = $("#txtEmailChangeData").val();
                        var valido = caracteres.test(texto);
                        if (!valido) {
                            alert("Ingrese un correo válido", "Informativo");
                            return false;
                        }
                    }
                    if (SessionRespuesta.EMAIL != $("#txtEmailChangeData").val()) {
                        if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {

                            alert("Los correos no coinciden", "Informativo");
                            return false;
                        }
                    }

                    that.modelChangeData.FLAG_DATA_MINOR = "DATA_MINOR";
                    //url: '/Transactions/Postpaid/ChangeData/UpdateDataMinorCustomer';

                }



                //if (controls.chkFacturacion[0].checked == true) {
                if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {
                    if (that.validarMaxCaracteresF() && that.validarMaxCaracteresF2()) {
                        that.modelAddressInvoice.strDireccion = strDireccionF.toUpperCase();
                        that.modelAddressInvoice.strReferencia = strReferenciaF.toUpperCase();
                        that.modelAddressInvoice.FLAG_FACTURACION = "FACTURACION";
                        SessionTransac.FlagInsFact = 1;
                        //UpdateAddressCustomer'		//FACT

                        //if (SessionRespuesta.EMAIL != controls.txtEmailChangeData.val()) {
                        if (!SessionTransac.Reciboxcorreo) {
                            SessionTransf.CambioCorreoReciboFisico = "Cliente se encuentra afiliado a recibo físico, se procede a actualizar la dirección de envío";
                        }
                        //}
                    }
                }

                if (controls.chkFacturacion[0].checked == true) {
                    if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {
                        if (that.validarMaxCaracteres() && that.validarMaxCaracteres2()) {
                            that.modelAddressCustomer.strDireccion = strDireccion.toUpperCase();
                            console.log("direccion de domcicilio mg13: 2", that.modelAddressCustomer.strDireccion);
                            that.modelAddressCustomer.strReferencia = strReferencia.toUpperCase();
                            that.modelAddressCustomer.FLAG_LEGAL = "LEGAL";
                            SessionTransac.FlagInsDom = 1;
                            //UpdateAddressCustomer'		//LEGAL
                        }
                    }
                }

            } else {
                if (that.validateChangeMinor()) {
                    console.log(66);
                    var caracteres = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if ($("#txtEmailChangeData").val() != "") {
                        console.log(67);
                        var texto = $("#txtEmailChangeData").val();
                        var valido = caracteres.test(texto);
                        if (!valido) {
                            alert("Ingrese un correo válido", "Informativo");
                            return false;
                        }
                    }
                    if (SessionRespuesta.EMAIL != $("#txtEmailChangeData").val()) {
                        if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {

                            alert("Los correos no coinciden", "Informativo");
                            return false;
                        }
                    }

                    that.modelChangeData.FLAG_DATA_MINOR = "DATA_MINOR";
                    //url: '/Transactions/Postpaid/ChangeData/UpdateDataMinorCustomer';

                }



                //if (controls.chkFacturacion[0].checked == true) {
                if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                    if (that.validarMaxCaracteresF() && that.validarMaxCaracteresF2()) {

                        that.modelAddressInvoice.strDireccion = strDireccionF.toUpperCase();
                        that.modelAddressInvoice.strReferencia = strReferenciaF.toUpperCase();
                        //url: '/Transactions/Postpaid/ChangeData/UpdateAddressCustomer'		//FACT
                        that.modelAddressInvoice.FLAG_FACTURACION = "FACTURACION";
                        SessionTransac.FlagInsFact = 1;
                        //UpdateAddressCustomer'		//FACT

                        //if (SessionRespuesta.EMAIL != controls.txtEmailChangeData.val()) {
                        if (!SessionTransac.Reciboxcorreo) {

                            SessionTransf.CambioCorreoReciboFisico = "Cliente se encuentra afiliado a recibo físico, se procede a actualizar la dirección de envío";
                        }
                        //}

                    }
                }

                if (controls.chkFacturacion[0].checked == true) {

                    if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {

                        if (that.validarMaxCaracteres() && that.validarMaxCaracteres2()) {


                            //that.modelAddressCustomer.strDireccion = strDireccion.toUpperCase();
                            //console.log("direccion de domcicilio mg13: 1", that.modelAddressCustomer.strDireccion);
                            //that.modelAddressCustomer.strReferencia = strReferencia.toUpperCase();
                            //that.modelAddressCustomer.FLAG_LEGAL = "LEGAL";
                            //SessionTransac.FlagInsDom = 1;
                            ////url: '/Transactions/Postpaid/ChangeData/UpdateAddressCustomer'		//LEGAL
                            //that.modelChangeData.direccionLegal = $('#cboTipoVia option:selected').val() + ' ' + $("#txtVia").val() + ' ' + $('#txtNumeroCalle').val() + ' ' + $("#cboTipoMz option:selected").val() + ' ' + $("#txtNroMz").val() + ' ' + $('#txtLote').val() + ' ' + $("#cboTipoInterior option:selected").val() + ' ' + $("#txtInterior").val()
                            //that.modelChangeData.direccionReferenciaLegal = $('#cboTipoUrb option:selected').val() + '  ' + $('#txtUrb').val() + ' ' + $('#cboTipoZona option:selected').val() + ' ' + $('#txtZona').val() + ' ' + $('#txtReferencia').val();

                            //that.modelChangeData.paisLegal = $('#cboPaisMod option:selected').text();
                            //that.modelChangeData.departamentoLegal = $('#cboDepMod option:selected').text();
                            //that.modelChangeData.provinciaLegal = $('#cboProvinciaMod option:selected').text();
                            //that.modelChangeData.distritoLegal = $('#cboDistritoMod option:selected').text();


                            that.modelAddressCustomer.strDireccion = strDireccion.toUpperCase();
                            console.log("direccion de domcicilio mg13: 1", that.modelAddressCustomer.strDireccion);
                            that.modelAddressCustomer.strReferencia = strReferencia.toUpperCase();
                            that.modelAddressCustomer.FLAG_LEGAL = "LEGAL";
                            SessionTransac.FlagInsDom = 1;
                            //url: '/Transactions/Postpaid/ChangeData/UpdateAddressCustomer'		//LEGAL
                            that.modelChangeData.direccionLegal = strDireccion.toUpperCase(); //$('#cboTipoVia option:selected').val() + ' ' + $("#txtVia").val() + ' ' + $('#txtNumeroCalle').val() + ' ' + $("#cboTipoMz option:selected").val() + ' ' + $("#txtNroMz").val() + ' ' + $('#txtLote').val() + ' ' + $("#cboTipoInterior option:selected").val() + ' ' + $("#txtInterior").val()
                            that.modelChangeData.direccionReferenciaLegal = strReferencia.toUpperCase(); //$('#cboTipoUrb option:selected').val() + '  ' + $('#txtUrb').val() + ' ' + $('#cboTipoZona option:selected').val() + ' ' + $('#txtZona').val() + ' ' + $('#txtReferencia').val();

                            that.modelChangeData.paisLegal = $('#cboPaisMod option:selected').text();
                            that.modelChangeData.departamentoLegal = $('#cboDepMod option:selected').text();
                            that.modelChangeData.provinciaLegal = $('#cboProvinciaMod option:selected').text();
                            that.modelChangeData.distritoLegal = $('#cboDistritoMod option:selected').text();

                            that.modelChangeData.codigoPostalLegal = controls.txtCodPostalMod.val();
                        }
                    }
                }

            }
            that.modelChangeData.MessageSelectDate = SessionTransf.UpdateDataMinor;
            that.modelChangeData.MessageSelectJobTypes = SessionTransf.UpdateRepLegal;
            that.modelChangeData.MessageValidate = SessionTransf.UpdateContac;
            //-----------------------------------------------------------
			if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT == "ASIS") {var Time=0;}
            if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
				var Time=500;
                var tipo = controls.cboTipDoc.val();
                var numDocumento = controls.txtNewNroDoc.val();
                var obj = { strTipoDocumento: tipo, strNumDocumento: numDocumento, strIdSession: SessionTransf.IDSESSION };

                $.app.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(obj),
                    url: '/Transactions/Postpaid/ChangeData/GetParticipante',
                    success: function (data) {
                        if (data.Existente == false) {
                            msgConfirmacion = $("#hidNoExisteParticipante").val() + ", ¿Seguro que desea guardar la transacción?";
                            that.modelChangeData.strParticipante = false;
							Time=0;
                        }else{                           
                            msgConfirmacion = $("#hidExisteParticipante").val() + ", ¿Desea Guardar?";
							that.modelChangeData.strParticipante = true;							 
						}
                    }
                });
            }
			setTimeout(function(){
            confirm(msgConfirmacion, 'Confirmar', function () {

                $.blockUI({
                    message: '<div align="center"><img src="../../Images/loading2.gif"  width="25" height="25" /> Guardando .... </div>',
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



                //that.Loading();
                SessionTransac.Secuencia = 0;
                SessionTransac.Respuesta = 0;
                //that.SaveTransactionCambioDatosM();
                //vtorremo

                var objModelIni = {};
                objModelIni.strCustomerId = SessionTransf.CUSTOMER_ID;
                objModelIni.strRazonSocial = SessionTransf.NameCustomer;
                objModelIni.strNombres = SessionTransf.Name;
                objModelIni.strApellidos = SessionTransf.LastName;
                objModelIni.strTipoDocumento = SessionTransf.DocumentTypeid;
                objModelIni.strNacionalidad = SessionTransf.BirthPlaceId;
                objModelIni.lugarNacimiento = SessionTransf.BirthPlace;
                objModelIni.strEstadoCivilId = SessionTransf.EstadoCivilID;
                objModelIni.DNI_RUC = SessionTransf.DNIRUC;
                objModelIni.strMotivo = that.modelChangeData.strMotivo;

                //alert(objModelIni.strMotivo);

                objModelIni.dateFechaNacimiento = SessionTransf.BirthDate;
                objModelIni.strSexo = SessionTransf.Sex;

                if ((SessionTransf !== null) && (SessionTransf.listaRL !== undefined)) {
                    var listRL = [];
                    var lstaRLOLD = [];
                    var objRL = {};

                    $.each(SessionTransf.listaRL, function (index, value) {
                        index++;
                        objRL = {};
                        objRL.idCurep = index;
                        value.idCurep = index;
                            if (value.cuRepnumdoc !== $("#txtND" + index).val()) {
                                objRL.cuReptipdoc = value.cuReptipdoc;
                                objRL.cuRepnumdoc = value.cuRepnumdoc;
                                objRL.cuRepnombres = value.cuRepnombres;
                                objRL.cuRepapepat = value.cuRepapepat;
                                objRL.cuRepapemat = value.cuRepapemat;
                                objRL.cuRepstatus = 0;
                                lstaRLOLD.push(value);
                                listRL.push(objRL);
                                objRL = {};
                                objRL.idCurep = index;
                                objRL.cuReptipdoc = $("#cboTD" + index).val();
                                objRL.cuRepnumdoc = $("#txtND" + index).val();
                                objRL.cuRepnombres = $("#txtN" + index).val();
                                objRL.cuRepapepat = $("#txtAP" + index).val();
                                objRL.cuRepapemat = $("#txtAM" + index).val();
                                objRL.cuRepstatus = 1;
                                objRL.cuRepAction = "M";
                                lstaRLOLD.push(value);
                                listRL.push(objRL);
                            } else {
                                if (value.cuRepnombres !== $("#txtN" + index).val() || value.cuRepapepat !== $("#txtAP" + index).val() || value.cuRepapemat !== $("#txtAM" + index).val()) {
                                    objRL.cuReptipdoc = $("#cboTD" + index).val();
                                    objRL.cuRepnumdoc = $("#txtND" + index).val();
                                    objRL.cuRepnombres = $("#txtN" + index).val();
                                    objRL.cuRepapepat = $("#txtAP" + index).val();
                                    objRL.cuRepapemat = $("#txtAM" + index).val();
                                    objRL.cuRepAction = "M";
                                    objRL.cuRepstatus = value.cuRepstatus;
                                    lstaRLOLD.push(value);
                                    listRL.push(objRL);
                                } else {
                                    objRL.cuReptipdoc = value.cuReptipdoc;
                                    objRL.cuRepnumdoc = value.cuRepnumdoc;
                                    objRL.cuRepnombres = value.cuRepnombres;
                                    objRL.cuRepapepat = value.cuRepapepat;
                                    objRL.cuRepapemat = value.cuRepapemat;
                                    objRL.cuRepstatus = value.cuRepstatus;
                                    lstaRLOLD.push(value);
                                    listRL.push(objRL);
                                }                                
                            }
                    });

                    that.modelChangeData.listaRL = JSON.stringify(listRL);
                    that.modelChangeData.listaRLOLD = JSON.stringify(lstaRLOLD);

                } else {
                    that.modelChangeData.listaRL = "";
                    that.modelChangeData.listaRLOLD = "";
                }

                var objParametro = {};

                objParametro.oModel = that.modelChangeData;
                objParametro.oModelD = that.modelAddressCustomer;
                objParametro.oModelF = that.modelAddressInvoice;
                objParametro.DataOld = that.modelDataCustomerResponse;
                objParametro.FlagD = SessionTransac.FlagInsDom;

                console.log("objParametro.oModel");
                console.log(objParametro.oModel);

                console.log("objParametro.oModelD");
                console.log(objParametro.oModelD);

                console.log("objParametro.oModelF");
                console.log(objParametro.oModelF);

                console.log("objParametro.DataOld");
                console.log(objParametro.DataOld);

                console.log("objParametro.FlagD");
                console.log(objParametro.FlagD);
                //alert(objParametro.FlagD);

                objParametro.FlagF = SessionTransac.FlagInsFact;
                //alert(objParametro.FlagF);

                //objParametro.plataformaAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
                //var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac")).SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT;
                objParametro.plataformaAT = SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT;
                objParametro.flagconvivencia = SessionTransac.SessionParams.DATACUSTOMER.flagConvivencia;
                objParametro.oModelIni = objModelIni;
                console.log(objParametro);
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(objParametro),
                    url: '/Transactions/Postpaid/ChangeData/RegistarInteracion',
                    success: function (response) {
                        if (response.strResultTransCustomer != "" || response.strResultTransCustomer == null) {
                            alert(response.strResultTransCustomer, "Informativo");
                        } else {
                            SessionTransac.MensajeEmail = response.MensajeEmail;
                            SessionTransac.vDesInteraction = response.vDesInteraction;
                            SessionTransac.InteractionId = response.vInteractionId;
                            SessionTransac.RutaArchivo = response.strRutaArchivo;
                            $("#hidrlIdCur").val("");
                            if (response.vInteractionId != '' || response.vInteractionId != "null") {
                                var msj = response.vDesInteraction + ' Codigo de interaccion: ' + response.vInteractionId;
                                if (SessionTransf.CambioCorreoReciboDigital != "" && (SessionTransac.Reciboxcorreo)) {
                                    alert(msj + ', <br><strong>' + '\"\n\n\n' + SessionTransf.CambioCorreoReciboDigital + ' ' + '\"\n\n\n' + ' </strong>', "Informativo");
                                } else if (SessionTransf.CambioCorreoReciboFisico != "" && (!SessionTransac.Reciboxcorreo)) {
                                    alert(msj + ', <br><strong>' + '\"\n\n\n' + SessionTransf.CambioCorreoReciboFisico + ' ' + '\"\n\n\n' + ' </strong>', "Informativo");
                                } else {
                                    alert(msj, "Informativo");  //that.vDesInteraction
                                }

                                that.BlockControl();
                                $("#btnGuardar").attr('disabled', true);

                            }

                            if (response.RutaArchivo != '' || response.RutaArchivo != "null") {
                                $("#btnConstancia").attr('disabled', false); //Activa 
                            }
                        }
                    }
                });

            });},Time);

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


            control.cbocivilstatus.prop('disabled', true);
            control.txtNote.prop('disabled', true);

            control.chkEmail.prop('disabled', true);
            control.rdbHombre.prop('disabled', true);
            control.rdbMujer.prop('disabled', true);
            control.rdbNA.prop('disabled', true);
        },

        SaveNameCustomer: function (modelChangeData) {

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(modelChangeData),
                url: '/Transactions/Postpaid/ChangeData/UpdateNameCustomer',
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

            alert(modelChangeData.intSeqIn);

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(modelChangeData),
                url: '/Transactions/Postpaid/ChangeData/UpdateDataMinorCustomer',
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
                        alert("valida datos menores");
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
                url: '/Transactions/Postpaid/ChangeData/UpdateAddressCustomer',
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
                    //$.unblockUI();
                    return false;
                }
                if (controls.txtNewNroDoc.val().length < $("#hidLongDoc").val()) { //??SessionTransf.DocLongitud) {
                    alert("Debe ingresar un numero de documento correcto", "Informativo");
                    //$.unblockUI();
                    return false;
                }

                var opcion = $('select[name="nmcboTipDoc"] option:selected').text();


                if (opcion == 'RUC') {

                    var tpDoc = ($('#txtNewNroDoc').val()).substring(0, 2);

                    if (tpDoc == '10') {
                        if (controls.txtNewNombCli.val() == "") {
                            alert("Debe ingresar el nombre del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                        if (controls.txtNewApeCli.val() == "") {
                            alert("Debe ingresar el apellido del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                    }
                    else {
                        if (controls.txtNewRs.val() == "") {
                            alert("Debe ingresar la razón social del cliente", "Informativo");
                            //$.unblockUI();
                            return false;
                        }
                        //if (controls.txtNameComChangeData.val() == "") {
                        //    alert("Debe ingresar el nombre comercial", "Informativo");
                        //    //$.unblockUI();
                        //    return false;
                        //}
                    }

                } else {
                    if (controls.txtNewNombCli.val() == "") {
                        alert("Debe ingresar el nombre del cliente", "Informativo");
                        //$.unblockUI();
                        return false;
                    }
                    if (controls.txtNewApeCli.val() == "") {
                        alert("Debe ingresar el apellido del cliente", "Informativo");
                        //$.unblockUI();
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
                    //$.unblockUI();
                    return false;
                }
                if ($("#txtEmailChangeData").val() != $("#txtConfirmarEmailChangeData").val()) {

                    alert("Los correos no coinciden", "Informativo");
                    //$.unblockUI();
                    return false;
                }

            }

            if (controls.txtContadorD1.val() > 0 || controls.txtContadorD2.val() > 0) {

                if (controls.txtContadorD2.val() < 1 || controls.txtContadorD2.val() == "") {
                    alert("Debe ingresar una referencia del domicilio", "Informativo");
                    //$.unblockUI();
                    return false;
                }
                if (controls.txtContadorD1.val() < 1 || controls.txtContadorD1.val() == "") {
                    alert("Debe ingresar una dirección de domicilio", "Informativo");
                    //$.unblockUI();
                    return false;
                }

                if (that.validarMaxCaracteres()) {
                    result = true;
                } else {
                    //$.unblockUI();
                    return false;
                }
                if (that.validarMaxCaracteres2()) {
                    result = true;
                } else {
                    //$.unblockUI();
                    return false;
                }


            }

            if (controls.chkFacturacion[0].checked == true) {

                if (controls.txtContadorFD1.val() > 0 || controls.txtContadorFD2.val() > 0) {

                    if (controls.txtContadorFD2.val() < 1 || controls.txtContadorFD2.val() == "") {
                        alert("Debe ingresar una referencia de dirección de facturación", "Informativo");
                        //$.unblockUI();
                        return false;
                    }
                    if (controls.txtContadorFD1.val() < 1 || controls.txtContadorFD1.val() == "") {
                        alert("Debe ingresar una dirección de facturación", "Informativo");
                        //$.unblockUI();
                        return false;
                    }

                    if (that.validarMaxCaracteresF()) {
                        result = true;
                    } else {
                        //$.unblockUI();
                        return false;
                    }
                    if (that.validarMaxCaracteresF2()) {
                        result = true;
                    } else {
                        //$.unblockUI();
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
                console.log(16);
                result = true;
            }
            if (SessionRespuesta.NOMBRES != controls.txtNewNombCli.val()) {
                console.log(17);
                result = true;
            }
            if (SessionTransf.plataformaAT == "TOBE") {
                if (SessionRespuesta.APELLIDOS != (controls.txtNewApeCliPat.val() + ' ' + controls.txtNewApeCliMat.val())) {
                    console.log(41);
                    result = true;
                }
            } else {
                if (SessionRespuesta.APELLIDOS != controls.txtNewApeCli.val()) {
                    console.log(18);
                    result = true;
                }
            }

            var opcion = $('select[name="nmcboTipDoc"] option:selected').text();
            if (SessionRespuesta.TIPO_DOC != opcion) {
                console.log(19);
                result = true;
            }
            if (SessionRespuesta.DNI_RUC != controls.txtNewNroDoc.val()) {
                console.log(20);
                result = true;
            }
            return result;
        },
        validateChangeMinor: function () {
            var that = this,
                controls = that.getControls();
            var result = false;
            console.log(SessionRespuesta.LUGAR_NACIMIENTO_ID);
            console.log(controls.cboNacionalidadChangeData.val());
            SessionTransf.UpdateDataMinor = "0";
            SessionTransf.UpdateRepLegal = "0";
            SessionTransf.UpdateContac = "0";

            if (SessionRespuesta.NOMBRE_COMERCIAL != controls.txtNameComChangeData.val()) {
                result = true;
            }
            if (SessionRespuesta.TELEF_REFERENCIA != controls.txtPhoneChangeData.val()) {
                SessionTransf.UpdateDataMinor = "1";
                result = true;
            }
            if (SessionRespuesta.TELEFONO_CONTACTO != controls.txtMovilChangeData.val()) {
                SessionTransf.UpdateDataMinor = "1";
                result = true;
            }
            if (SessionRespuesta.FECHA_NAC != controls.txtDateChangeData.val()) {
                console.log(6);
                result = true;
            }
            if ($('#hidflagrl').val() < 1) {

                var opcion2 = $('select[name="nmcboTipoDocRep"] option:selected').text();
                if (SessionRespuesta.TIPO_DOC_RL != "" && SessionRespuesta.TIPO_DOC_RL != undefined && SessionRespuesta.TIPO_DOC_RL != opcion2) {
                    console.log(SessionRespuesta.TIPO_DOC_RL);
                    console.log(opcion2);
                    SessionTransf.UpdateRepLegal = "1";
                    result = true;
                }
                if (SessionRespuesta.NRO_DOC != controls.txtNroDocRep.val()) {
                    SessionTransf.UpdateRepLegal = "1";
                    result = true;
                }
                if (SessionRespuesta.REPRESENTANTE_LEGAL != controls.txtNewNombRep.val()) {
                    console.log(9);
                    SessionTransf.UpdateRepLegal = "1";
                    result = true;
                }
            }
            if (SessionRespuesta.CONTACTO_CLIENTE != controls.txtNewNombCont.val()) {
                SessionTransf.UpdateContac = "1";
                result = true;
            }
            if (SessionRespuesta.LUGAR_NACIMIENTO_ID != undefined && SessionRespuesta.LUGAR_NACIMIENTO_ID != controls.cboNacionalidadChangeData.val()) {
                console.log(SessionRespuesta.LUGAR_NACIMIENTO_ID);
                console.log(controls.cboNacionalidadChangeData.val());
                console.log(11);
                result = true;
            }
            if ($('#hidflagrl').val() < 1) {
                if (SessionRespuesta.ESTADO_CIVIL_ID != $("#cbocivilstatus option:selected").text()) {
                    console.log(SessionRespuesta.ESTADO_CIVIL_ID);
                    console.log($("#cbocivilstatus option:selected").text());
                    console.log(12);
                    result = true;
                }
            }
            var sexo = $('input[name=rdbSexo]:checked').val();
            if (sexo == undefined) {
                sexo = "-1";
            }
            if (SessionRespuesta.SEXO != sexo) {
                result = true;
            }
            if (SessionRespuesta.EMAIL != controls.txtEmailChangeData.val()) {
                SessionTransf.UpdateDataMinor = "1";
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
                url: '/Transactions/Postpaid/ChangeData/GetValidateEnvioxMail',
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

        ValidateEmailTobe: function () {
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            SessionTransac.Reciboxcorreo = false;
            var parameters = {
                obtenerDatosFacturacionRequestType: {
                    csId: SessionTransf.CUSTOMER_ID,
                    csIdPub: SessionTransf.csIdPub
                },
                strIdSession: SessionTransf.IDSESSION,
                strUser: SessionTransf.UserId
            };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(parameters),
                url: '/Transactions/Postpaid/ChangeData/GetDataBilling',
                error: function (data) {
                },
                success: function (response) {

                    if (response != null && response.data != null && response.data.obtenerDatosFacturacionResponseType != null && response.data.obtenerDatosFacturacionResponseType.listaDireccionesFacturacion != null) {
                        Session.adrSeq = response.data.obtenerDatosFacturacionResponseType.listaDireccionesFacturacion[0].adrSeq;
                        if (response.data.obtenerDatosFacturacionResponseType.listaCuentaFacturacion != null) {
                            $.each(response.data.obtenerDatosFacturacionResponseType.listaCuentaFacturacion, function (index, value) {
                                if (value.bmId === 19) {
                                    console.log('Esta afiliado a correo electronico');
                                    SessionTransac.Reciboxcorreo = true;
                                }
                            });
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
                        url: '/Transactions/Postpaid/ChangeData/GetCivilStatus',
                        success: function (response) {
                            controls.cbocivilstatus.append($('<option>', { value: '-1', html: 'Seleccionar' }));
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

        GetUserCD: function () {

            var that = this,
                controls = that.getControls(),
                parameters = {};

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
                    SessionTransac.UserWhitStatusCivil = results.MAS_DES;
                }
            });
        },

        GetStatusCivil: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objEstCivType = {}

            objEstCivType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objEstCivType),
                url: '/Transactions/Postpaid/ChangeData/GetCivilStatus',
                success: function (response) {
                    controls.cbocivilstatus.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    $.each(response.objLista, function (index, value) {

                        if (SessionTransac.UserWhitStatusCivil === value.Description) {
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
        },
        GetStatusCivilToBe: function (pid) {
            if (pid == null || pid.toString == "undefined") { pid = ""; }

            var that = this,
                controls = that.getControls(),
                objEstCivType = {}

            objEstCivType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objEstCivType),
                url: '/Transactions/Postpaid/ChangeData/GetCivilStatusToBe',
                success: function (response) {
                    controls.cbocivilstatus.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response != null && response.objLista != null) {
                        var itemSelect;
                        $.each(response.objLista, function (index, value) {

                            if (SessionTransf.UserWhitStatusCivil === value.Description) {
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
                url: '/Transactions/Postpaid/ChangeData/GetNacType',
                success: function (response) {
                    controls.cboNacionalidadChangeData.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboPaisMod.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboPaisModF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelec2;
                    var itemSelec3 = 154;// FYAURIBA - A LA ESPERA QUE datosCliente TRAIGA ESTE VALOR
                    $.each(response.objLista, function (index, value) {
                        if (Nac === value.Description) {
                            controls.cboNacionalidadChangeData.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;

                        } else {
                            controls.cboNacionalidadChangeData.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                        if (country.length !== 0) {
                            if (country === value.Description) {

                                itemSelec2 = value.Code;
                            }
                        } else {
                            itemSelec2 = 154;
                        }
                        if (countryF.length !== 0) {
                            if (countryF === value.Description) {

                                itemSelec3 = value.Code;
                            }
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
                url: '/Transactions/Postpaid/ChangeData/GetStateType',
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
                url: '/Transactions/Postpaid/ChangeData/GetMzBloEdiType',
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
                url: '/Transactions/Postpaid/ChangeData/GetTipDptInt',
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
                url: '/Transactions/Postpaid/ChangeData/GetStateType',
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
                url: '/Transactions/Postpaid/ChangeData/GetZoneTypes',
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

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/Postpaid/ChangeData/GetDepartments',
                success: function (response) {

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
                        that.InitProvinciasF();
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

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos2Type),
                url: '/Transactions/Postpaid/ChangeData/GetProvinces',
                success: function (response) {

                    controls.cboProvinciaMod.empty();
                    controls.cboDistritoMod.empty();
                    controls.cboDistritoMod.append($('<option>', { value: '', html: 'Seleccionar' }));
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

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos3Type),
                url: '/Transactions/Postpaid/ChangeData/GetDistricts',
                success: function (response) {

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
                url: '/Transactions/Postpaid/ChangeData/ObtenerCodigoPostal',
                success: function (response) {

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
                //controls.cboTipoMz.prop('disabled', true);
                //controls.cboTipoMz.prop('selectedIndex', 0);
                //controls.txtNroMz.prop('disabled', true);
                //controls.txtNroMz.val('');
                //controls.txtLote.prop('disabled', true);
                //controls.txtLote.val('');
                controls.txtNumeroCalle.val('S/N');
                that.ContadorD1();
            }
            else {
                controls.txtNumeroCalle.prop('disabled', false);
                //controls.cboTipoMz.prop('disabled', false);
                //controls.txtNroMz.prop('disabled', false);
                //controls.txtNroMz.val('');
                //controls.txtLote.prop('disabled', false);
                //controls.txtLote.val('');
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

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos2Type),
                url: '/Transactions/Postpaid/ChangeData/GetProvinces',
                success: function (response) {

                    controls.cboProvinciaModF.empty();
                    controls.cboDistritoModF.empty();
                    controls.cboDistritoModF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    controls.cboProvinciaModF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) { }
                    var itemSelect;
                    $.each(response.data, function (index, value) {

                        if (Ubigeo2 === value.Description) {
                            controls.cboProvinciaModF.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                            controls.cboDistritoModF.prop('disabled', false);

                        } else {
                            controls.cboProvinciaModF.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect != null && itemSelect.toString != "undefined") {
                        $("#cboProvinciaModF option[value=" + itemSelect + "]").attr("selected", true);
                        that.InitDistritosF();
                    }


                }
            });

            controls.cboProvinciaModF.prop('disabled', false);
            //that.getRulesUbigeoProv();
            //controls.cboDistritoModF.prop('disabled', true);

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

            $.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objUbigeos3Type),
                url: '/Transactions/Postpaid/ChangeData/GetDistricts',
                success: function (response) {

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
                url: '/Transactions/Postpaid/ChangeData/ObtenerCodigoPostal',
                success: function (response) {
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
                //controls.cboTipoMzF.prop('disabled', true);
                //controls.cboTipoMzF.prop('selectedIndex', 0);
                //controls.txtNroMzF.prop('disabled', true);
                //controls.txtNroMzF.val('');
                //controls.txtLoteF.prop('disabled', true);
                //controls.txtLoteF.val('');
                controls.txtNumeroCalleF.val('S/N');
                that.ContadorFD1();
            }
            else {
                controls.txtNumeroCalleF.prop('disabled', false);
                //controls.cboTipoMzF.prop('disabled', false);
                //controls.txtNroMzF.prop('disabled', false);
                //controls.txtNroMzF.val('');
                //controls.txtLoteF.prop('disabled', false);
                //controls.txtLoteF.val('');
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
                url: "/Transactions/Postpaid/ChangeData/GetMotivoCambio",
                data: {
                    strIdSession: SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    if (response.objLista != null) {
                        var itemSelect;
                        $.each(response.objLista, function (index, value) {
                            if (value.Code == '2' && !SessionTransf.Permiso) {
                                //controls.cboMotivoChange.append($('<option>', { value: value.Code, html: value.Description, 'disabled': true }));
                            } else {
                                controls.cboMotivoChange.append($('<option>', { value: value.Code, html: value.Description }));
                            }
                        });
                    }
                }
            });
        },

        InitTDO: function (oValor) {
            var that = this;
            var that = this,
               controls = that.getControls();
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/GetTypeDocumentTOBE",
                data: {
                    strIdSession: "20210430152623"//SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    controls.cboTD1.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    var itemCodigo;
                    $.each(response.objLista, function (index, value) {
                        itemCodigo = parseInt(value.Code);
                        if (oValor === itemCodigo)
                        {
                            controls.cboTD1.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTD1.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    
                    if (itemSelect !== null && itemSelect.toString !== "undefined") {
                        $("#cboTD1 option[value=" + itemSelect + "]").attr("selected", true);
                        that.cboTD1_change();
                    }
                    $("#cboTD1 option[value='6']").remove();
                }
            });
        },

        InitTDT: function (oValor) {
            var that = this;
            var that = this,
               controls = that.getControls();
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/GetTypeDocumentTOBE",
                data: {
                    strIdSession: "20210430152624" //SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    controls.cboTD2.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    var itemCodigo;
                    $.each(response.objLista, function (index, value) {
                        itemCodigo = parseInt(value.Code);
                        if (oValor === itemCodigo) {
                            controls.cboTD2.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTD2.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect !== null && itemSelect.toString !== "undefined") {
                        $("#cboTD2 option[value=" + itemSelect + "]").attr("selected", true);
                        that.cboTD2_change();
                    }
                    $("#cboTD2 option[value='6']").remove();
                }
            });
        },

        InitTDW: function (oValor) {
            var that = this;
            var controls = this.getControls();
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/GetTypeDocumentTOBE",
                data: {
                    strIdSession: "123456"//SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    controls.cboTD3.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    var itemCodigo;
                    $.each(response.objLista, function (index, value) {
                        itemCodigo = parseInt(value.Code);
                        if (oValor === itemCodigo) {
                            controls.cboTD3.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTD3.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect !== null && itemSelect.toString !== "undefined") {
                        $("#cboTD3 option[value=" + itemSelect + "]").attr("selected", true);
                        that.cboTD3_change();
                    }
                    $("#cboTD3 option[value='6']").remove();
                }
            });
        },

        InitTDJ: function (oValor) {
            var that = this;
            var controls = this.getControls();
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/GetTypeDocumentTOBE",
                data: {
                    strIdSession: "1235649"//SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    controls.cboTD4.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    var itemCodigo;
                    $.each(response.objLista, function (index, value) {
                        itemCodigo = parseInt(value.Code);
                        if (oValor === itemCodigo) {
                            controls.cboTD4.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTD4.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect !== null && itemSelect.toString !== "undefined") {
                        $("#cboTD4 option[value=" + itemSelect + "]").attr("selected", true);
                        that.cboTD4_change();
                    }
                    $("#cboTD4 option[value='6']").remove();
                }
            });
        },

        InitTDJU: function (oValor) {
            var that = this;
            var controls = this.getControls();
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/GetTypeDocumentTOBE",
                data: {
                    strIdSession: "1235649"//SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {
                    controls.cboTD5.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    var itemCodigo;
                    $.each(response.objLista, function (index, value) {
                        itemCodigo = parseInt(value.Code);
                        if (oValor === itemCodigo) {
                            controls.cboTD5.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTD5.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                    });
                    if (itemSelect !== null && itemSelect.toString !== "undefined") {
                        $("#cboTD5 option[value=" + itemSelect + "]").attr("selected", true);
                        that.cboTD5_change();
                    }
                    $("#cboTD5 option[value='6']").remove();
                }
            });
        },
        getTypeDocument: function () {
            var that = this,
               controls = that.getControls();

            var TipDoc = "";
            var ValidDoc = "";

            if (SessionTransf.plataformaAT == 'TOBE') {

                if (SessionTransf.TypDocRepreCustomer.indexOf('Carnet') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CARNET') > -1 ||
                    SessionTransf.TypDocRepreCustomer.indexOf('ce') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CE') > -1) {
                    TipDoc = "Carnet de Extranjería";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cie') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIE') > -1)) {
                    TipDoc = "CIE";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cire') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIRE') > -1)) {
                    TipDoc = "CIRE";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cpp') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CPP') > -1)) {
                    TipDoc = "CPP";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ctm') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CTM') > -1)) {
                    TipDoc = "CTM";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Dni') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('DNI') > -1)) {
                    TipDoc = "DNI";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Pasaporte') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('PASAPORTE') > -1)) {
                    TipDoc = "Pasaporte";
                }
                else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ruc') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('RUC') > -1)) {
                    TipDoc = "RUC";
                }
            }

            else {
                    //ASIS
            if (SessionTransf.NumbDocRepreCustomer.length == 11) {

                ValidDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                if (ValidDoc != "10") {
                    TipDoc = "RUC";
                        }
                        else if (ValidDoc == "10") {
                    TipDoc = "RUC";
                }

                    }
                    else {
                if (SessionTransf.TypDocRepreCustomer.indexOf('Carnet') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CARNET') > -1 ||
                    SessionTransf.TypDocRepreCustomer.indexOf('ce') > -1 || SessionTransf.TypDocRepreCustomer.indexOf('CE') > -1) {
                    TipDoc = "Carnet de Extranjería";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cie') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIE') > -1)) {
                    TipDoc = "CIE";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cire') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CIRE') > -1)) {
                    TipDoc = "CIRE";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Cpp') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CPP') > -1)) {
                    TipDoc = "CPP";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Ctm') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('CTM') > -1)) {
                    TipDoc = "CTM";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Dni') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('DNI') > -1)) {
                    TipDoc = "DNI";
                } else if ((SessionTransf.TypDocRepreCustomer.indexOf('Pasaporte') > -1) || (SessionTransf.TypDocRepreCustomer.indexOf('PASAPORTE') > -1)) {
                    TipDoc = "Pasaporte";
                }
            }
                }


            //var TipDoc = SessionTransf.TipDoc;
            var TipDocRL = "";
                if (SessionTransf.TypDocRepreCustomer == "DNI")
                {
                TipDocRL = "DNI";
                SessionTransf.tipoDatoRL = 0;
                SessionTransf.DocLongitudRL = 8;
            } else if (SessionTransf.TypDocRepreCustomer == "Carnet Extranjería") {
                TipDocRL = "Carnet de Extranjería";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 9;
            } else if (SessionTransf.TypDocRepreCustomer == "CTM") {
                TipDocRL = "CTM";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            } else if (SessionTransf.TypDocRepreCustomer == "CIE") {
                TipDocRL = "CIE";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            } else if (SessionTransf.TypDocRepreCustomer == "CIRE") {
                TipDocRL = "CIRE";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            } else if (SessionTransf.TypDocRepreCustomer == "CPP") {
                TipDocRL = "CPP";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            } else if (SessionTransf.TypDocRepreCustomer == "Pasaporte") {
                TipDocRL = "Pasaporte";
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            }

            $("#hidTipoDatoRL").val(SessionTransf.tipoDatoRL);
            $("#hidLongDocRL").val(SessionTransf.DocLongitudRL);
            var plataforma = SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.plataformaAT;
            var urls = plataforma == "TOBE" ? "GetTypeDocumentTOBE" : "getTypeDocument";
            $.app.ajax({
                type: "POST",
                url: "/Transactions/Postpaid/ChangeData/" + urls,
                data: {
                    strIdSession: SessionTransac.SessionParams.USERACCESS.userId
                },
                success: function (response) {

                    controls.cboTipDoc.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    controls.cboTipoDocRep.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objLista != null) { }
                    var itemSelect;
                    var itemSelectRL;
                    $.each(response.objLista, function (index, value) {
                        if (TipDoc === value.Description || SessionTransf.TypDocRepreCustomer === value.Code) { // FYAURIBA - REVERTIR AL ORIGINAL CUANDO EL SERVICIO TRAIGA EL VALOR CORRECTO
                            controls.cboTipDoc.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelect = value.Code;
                        } else {
                            controls.cboTipDoc.append($('<option>', { value: value.Code, html: value.Description }));
                        }
                        //alert(TipDocRL + '-' + value.Description);
                        if (TipDocRL === value.Description) {
                            controls.cboTipoDocRep.append($('<option>', { value: value.Code, html: value.Description }));
                            itemSelectRL = value.Code;
                        } else {
                            if (value.Code == 0 || value.Code == "0") {
                            } else {
                                controls.cboTipoDocRep.append($('<option>', { value: value.Code, html: value.Description }));
                            }
                        }

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
            var ValidDoc = "";

            if (controls.cboMotivoChange.val() == SessionTransf.opcAct) {
                controls.txtNewNombCli.prop("disabled", true);
                controls.cboTipDoc.prop("disabled", true);
                controls.txtNewApeCli.prop("disabled", true);
                controls.txtNewNroDoc.prop("disabled", true);
                controls.txtNewRs.prop("disabled", true);
                controls.txtNameComChangeData.prop("disabled", true);
                controls.txtDateChangeData.prop("disabled", true);
                controls.cboNacionalidadChangeData.prop("disabled", true);
                controls.txtNewApeCliPat.prop("disabled", true);
                controls.txtNewApeCliMat.prop("disabled", true);

                //Nuevo Bloqueo
                controls.cboTD1.prop("disabled", true);
                controls.cboTD2.prop("disabled", true);
                controls.cboTD3.prop("disabled", true);
                controls.cboTD4.prop("disabled", true);
                controls.cboTD5.prop("disabled", true);
                controls.txtND1.prop("disabled", true);
                controls.txtND2.prop("disabled", true);
                controls.txtND3.prop("disabled", true);
                controls.txtND4.prop("disabled", true);
                controls.txtND5.prop("disabled", true);
                controls.txtN1.prop("disabled", true);
                controls.txtN2.prop("disabled", true);
                controls.txtN3.prop("disabled", true);
                controls.txtN4.prop("disabled", true);
                controls.txtN5.prop("disabled", true);
                controls.txtAP1.prop("disabled", true);
                controls.txtAP2.prop("disabled", true);
                controls.txtAP3.prop("disabled", true);
                controls.txtAP4.prop("disabled", true);
                controls.txtAP5.prop("disabled", true);
                controls.txtAM1.prop("disabled", true);
                controls.txtAM2.prop("disabled", true);
                controls.txtAM3.prop("disabled", true);
                controls.txtAM4.prop("disabled", true);
                controls.txtAM5.prop("disabled", true);
                controls.txtNC.prop("disabled", true);

                if (SessionTransf.NumbDocRepreCustomer.length == 11) {
                    ValidDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                    if (ValidDoc != "10") {

                        if (parseInt($("#hidflagrl").val()) > 0) {
                            controls.NuevosDatos.css("display", "none");
                        } else {
                            controls.NuevosDatos.css("display", "block");
                        }
                        controls.txtNewNombRep.prop("disabled", true);
                        controls.cboTipoDocRep.prop("disabled", true);
                        controls.txtNroDocRep.prop("disabled", true);
                        controls.txtNewNombCont.prop("disabled", true);
                    } else {
                        controls.NuevosDatos.css("display", "none");
                    }
                } else {
                    controls.NuevosDatos.css("display", "none");
                }
            }

            if (controls.cboMotivoChange.val() == SessionTransf.opcError) {
                //vtorres
                controls.txtNewNombCli.prop("disabled", false);
                controls.cboTipDoc.prop("disabled", false);
                controls.txtNewApeCli.prop("disabled", false);
                controls.txtNewNroDoc.prop("disabled", false);
                controls.txtNewRs.prop("disabled", false);
                controls.txtNameComChangeData.prop("disabled", false);
                controls.txtDateChangeData.prop("disabled", false);
                controls.cboNacionalidadChangeData.prop("disabled", false);
                controls.txtNewApeCliPat.prop("disabled", false);
                controls.txtNewApeCliMat.prop("disabled", false);

                //Nuevo Bloqueo
                controls.cboTD1.prop("disabled", false);
                controls.cboTD2.prop("disabled", false);
                controls.cboTD3.prop("disabled", false);
                controls.cboTD4.prop("disabled", false);
                controls.cboTD5.prop("disabled", false);
                controls.txtND1.prop("disabled", false);
                controls.txtND2.prop("disabled", false);
                controls.txtND3.prop("disabled", false);
                controls.txtND4.prop("disabled", false);
                controls.txtND5.prop("disabled", false);
                controls.txtN1.prop("disabled", false);
                controls.txtN2.prop("disabled", false);
                controls.txtN3.prop("disabled", false);
                controls.txtN4.prop("disabled", false);
                controls.txtN5.prop("disabled", false);
                controls.txtAP1.prop("disabled", false);
                controls.txtAP2.prop("disabled", false);
                controls.txtAP3.prop("disabled", false);
                controls.txtAP4.prop("disabled", false);
                controls.txtAP5.prop("disabled", false);
                controls.txtAM1.prop("disabled", false);
                controls.txtAM2.prop("disabled", false);
                controls.txtAM3.prop("disabled", false);
                controls.txtAM4.prop("disabled", false);
                controls.txtAM5.prop("disabled", false);
                controls.txtNC.prop("disabled", false);

                if (SessionTransf.NumbDocRepreCustomer.length == 11) {
                    ValidDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                    if (ValidDoc != "10") {
                        if (parseInt($("#hidflagrl").val()) > 0) {
                            controls.NuevosDatos.css("display", "none");
                        } else {
                            controls.NuevosDatos.css("display", "block");
                        }
                        controls.txtNewNombRep.prop("disabled", false);
                        controls.cboTipoDocRep.prop("disabled", false);
                        controls.txtNroDocRep.prop("disabled", false);
                        controls.txtNewNombCont.prop("disabled", false);
                    } else {
                        controls.NuevosDatos.css("display", "none");
                    }
                } else {
                    controls.NuevosDatos.css("display", "none");
                }
            }

        },
        
        cboTipDoc_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            var tipDoc = "";

            //PCTL-2782 REINICIANDO EL CAMPO NRO DOCUMENTO
            //==============================================
            if (flag > 0) {
                $("#txtNewNroDoc").val('');
            }
            //==============================================

            if (SessionTransf.NumbDocRepreCustomer.length == 11) {
                tipDoc = SessionTransf.NumbDocRepreCustomer.substring(0, 2);
                //if (tipDoc != "10") {
                //    tipDoc = "RUC";
                //} else {
                //    tipDoc = "DNI";
                //}
            }

            opcion = $('select[name="nmcboTipDoc"] option:selected').text();//$('#cboTipDoc option:selected').text();                              

            //alert('Prueba: ' + opcion);

            if (opcion == 'DNI') {
                //??SessionTransf.tipoDato = 0;
                $("#hidTipoDato").val(0);
                //??SessionTransf.DocLongitud = 8;
                $("#hidLongDoc").val(8);
                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);

                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
                controls.NuevosDatos.css("display", "none");
                controls.divRRLL.css("display", "none");
            }
            if (opcion == 'RUC') {
                //??SessionTransf.tipoDato = 0;
                $("#hidTipoDato").val(0);
                //??SessionTransf.DocLongitud = 11;
                $("#hidLongDoc").val(11);
                //controls.txtNewRs.prop('disabled', false);
                //controls.txtNameComChangeData.prop('disabled', false);

                if (SessionTransf.NumbDocRepreCustomer.length == 11) {
                    if (tipDoc == "10") {
                        controls.divNameCli.css("display", "block");
                        controls.divNameC.css("display", "none");
                        controls.NuevosDatos.css("display", "none");
                        controls.divRRLL.css("display", "none");
                    } else {
                        controls.divNameCli.css("display", "none");
                        controls.divNameC.css("display", "block");
                        if (parseInt($("#hidflagrl").val()) > 0) {
                            controls.NuevosDatos.css("display", "none");
                        } else {
                            controls.divRRLL.css("display", "none");
                        }
                    }
                } else {
                    controls.divNameCli.css("display", "none");
                    controls.divNameC.css("display", "block");
                    controls.NuevosDatos.css("display", "none");
                    controls.divRRLL.css("display", "block");
                }


            }
            if (opcion == 'Pasaporte') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 12;
                $("#hidLongDoc").val(12);
                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
                controls.NuevosDatos.css("display", "none");
                controls.divRRLL.css("display", "none");
            }
            if (opcion == 'Carnet de Extranjería' || opcion == 'CE') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 9;
                $("#hidLongDoc").val(9);
                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
                controls.NuevosDatos.css("display", "none");
                controls.divRRLL.css("display", "none");
            }
            if (opcion == 'CIE' || opcion == 'CIRE' || opcion == 'CPP' || opcion == 'CTM') {
                //??SessionTransf.tipoDato = 1;
                $("#hidTipoDato").val(1);
                //??SessionTransf.DocLongitud = 12;
                $("#hidLongDoc").val(12);
                //controls.txtNewRs.prop('disabled', true);
                //controls.txtNameComChangeData.prop('disabled', true);
                controls.divNameCli.css("display", "block");
                controls.divNameC.css("display", "none");
                controls.NuevosDatos.css("display", "none");
                controls.divRRLL.css("display", "none");
            }

            //??$("#hidTipoDato").val(SessionTransf.tipoDato);
            //??$("#hidLongDoc").val(SessionTransf.DocLongitud);
            flag++;
        },
        cboTipDocRep_change: function () {
            var that = this,
              controls = that.getControls();
            var opcionRL = "";

            opcionRL = $('select[name="nmcboTipoDocRep"] option:selected').text();//$('#cboTipDoc option:selected').text();                  

            if (opcionRL == 'DNI') {
                SessionTransf.tipoDatoRL = 0;
                SessionTransf.DocLongitudRL = 8;
            }
            if (opcionRL == 'RUC') {
                SessionTransf.tipoDatoRL = 0;
                SessionTransf.DocLongitudRL = 11;
            }
            if (opcionRL == 'Carnet de Extranjería' || opcionRL == 'CE') {
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 9;
            }
            if (opcionRL == 'Pasaporte') {
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            }
            if (opcionRL == 'CIE' || opcionRL == 'CIRE' || opcionRL == 'CPP' || opcionRL == 'CTM') {
                SessionTransf.tipoDatoRL = 1;
                SessionTransf.DocLongitudRL = 12;
            }

            $("#hidTipoDatoRL").val(SessionTransf.tipoDatoRL);
            $("#hidLongDocRL").val(SessionTransf.DocLongitudRL);

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
                //objNombreUrbanizacion.disabled = true;
            }
        },

        ValidaTipoUrbF: function () {

            var objTipoUrbanizacion = document.getElementById('cboTipoUrbF');
            var objNombreUrbanizacion = document.getElementById('txtUrbF');

            if (objTipoUrbanizacion.selectedIndex > 0) {
                objNombreUrbanizacion.disabled = false;
            }
            else {
                //objNombreUrbanizacion.disabled = true;
            }
        },

        ValidaTipoZona: function () {

            var objTipoZona = document.getElementById('cboTipoZona');
            var objNombreZona = document.getElementById('txtZona');

            if (objTipoZona.selectedIndex > 0) {
                objNombreZona.disabled = false;
            }
            else {
                //objNombreZona.disabled = true;
            }
        },
        ValidaTipoZonaF: function () {

            var objTipoZona = document.getElementById('cboTipoZonaF');
            var objNombreZona = document.getElementById('txtZonaF');

            if (objTipoZona.selectedIndex > 0) {
                objNombreZona.disabled = false;
            }
            else {
                //objNombreZona.disabled = true;
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
            if (objLote.value != '') {
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
            console.log("strDireccion", strCad);
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
            var strNotas = new String(strCad);
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
                //alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteres2: function () {
            nombre2 = $('#txtContadorD2').val();
            if (nombre2 < 41) {
                return true;
            }
            else {
                //alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteresF: function () {

            nombre = $('#txtContadorFD1').val();
            if (nombre < 41) {
                return true;
            }
            else {
                //alert('La dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        validarMaxCaracteresF2: function () {
            nombre2 = $('#txtContadorFD2').val();
            if (nombre2 < 41) {
                return true;
            }
            else {
                //alert('Las notas de la dirección ingresada supera la longitud máxima permitida de 40 Caracteres.');
                return false;
            }
        },

        cloneAddress: function () {
            var that = this,
              controls = that.getControls();
            $("#cboTipoVia option").filter(function () { return (this.text == $("#cboTipoViaF option:selected").text()) }).attr('selected', true);
            //controls.cboTipoVia.text(controls.cboTipoViaF.text());

            controls.txtVia.val(controls.txtViaF.val());
            controls.txtNumeroCalle.val(controls.txtNumeroCalleF.val());

            var chksn = controls.chkSNF.prop("checked");

            controls.chkSN.prop("checked", chksn);
            if (chksn == true) {
                controls.txtNumeroCalle.prop('disabled', true);
            }

            controls.cboTipoMz.val(controls.cboTipoMzF.val());
            controls.txtNroMz.val(controls.txtNroMzF.val());

            controls.txtLote.val(controls.txtLoteF.val());

            controls.cboTipoInterior.val(controls.cboTipoInteriorF.val());
            controls.txtInterior.val(controls.txtInteriorF.val());

            controls.cboTipoUrb.val(controls.cboTipoUrbF.val());
            controls.txtUrb.val(controls.txtUrbF.val());
            that.ValidaTipoUrb();

            controls.cboTipoZona.val(controls.cboTipoZonaF.val());
            controls.txtZona.val(controls.txtZonaF.val());

            that.ValidaTipoZona();

            controls.txtReferencia.val(controls.txtReferenciaF.val());

            controls.cboPaisMod.val(controls.cboPaisModF.val());
            controls.cboDepMod.val(controls.cboDepModF.val());


            $("#cboProvinciaMod").html($("#cboProvinciaModF").html());
            var item7 = $('#cboProvinciaModF option:selected').val();
            $("#cboProvinciaMod option[value=" + item7 + "]").attr("selected", true);

            $("#cboDistritoMod").html($("#cboDistritoModF").html());
            var item8 = $('#cboDistritoModF option:selected').val();
            $("#cboDistritoMod option[value=" + item8 + "]").attr("selected", true);

            controls.txtCodPostalMod.val(controls.txtCodPostalModF.val());
            controls.txtContadorD1.val(controls.txtContadorFD1.val());
            controls.txtContadorD2.val(controls.txtContadorFD2.val());

            that.ContadorD1();
            that.ContadorD2();

        },
        cboTD1_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            opcion = $('#cboTD1').val();

            switch (opcion) {
                case "4":
                    $("#hidTipDocRRLL1").val(4);
                    $("#hidLongRRLL1").val(12);
                    break;
                case "6":
                    $("#hidTipDocRRLL1").val(6);
                    $("#hidLongRRLL1").val(11);
                    break;
                case "1":
                    $("#hidTipDocRRLL1").val(1);
                    $("#hidLongRRLL1").val(12);
                    break;
                case "2":
                    $("#hidTipDocRRLL1").val(2);
                    $("#hidLongRRLL1").val(8);
                    break;
                default:
                    $("#hidTipDocRRLL1").val(99);
                    $("#hidLongRRLL1").val(20);
                    break;
            }
        },
        cboTD2_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            opcion = $('#cboTD2').val();

            switch (opcion) {
                case "4":
                    $("#hidTipDocRRLL2").val(4);
                    $("#hidLongRRLL2").val(12);
                    break;
                case "6":
                    $("#hidTipDocRRLL2").val(6);
                    $("#hidLongRRLL2").val(11);
                    break;
                case "1":
                    $("#hidTipDocRRLL2").val(1);
                    $("#hidLongRRLL2").val(12);
                    break;
                case "2":
                    $("#hidTipDocRRLL2").val(2);
                    $("#hidLongRRLL2").val(8);
                    break;
                default:
                    $("#hidTipDocRRLL2").val(99);
                    $("#hidLongRRLL2").val(20);
                    break;
            }
        },
        cboTD3_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            opcion = $('#cboTD3').val();

            switch (opcion) {
                case "4":
                    $("#hidTipDocRRLL3").val(4);
                    $("#hidLongRRLL3").val(12);
                    break;
                case "6":
                    $("#hidTipDocRRLL3").val(6);
                    $("#hidLongRRLL3").val(11);
                    break;
                case "1":
                    $("#hidTipDocRRLL3").val(1);
                    $("#hidLongRRLL3").val(12);
                    break;
                case "2":
                    $("#hidTipDocRRLL3").val(2);
                    $("#hidLongRRLL3").val(8);
                    break;
                default:
                    $("#hidTipDocRRLL3").val(99);
                    $("#hidLongRRLL3").val(20);
                    break;
            }
        },
        cboTD4_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            opcion = $('#cboTD4').val();

            switch (opcion) {
                case "4":
                    $("#hidTipDocRRLL4").val(4);
                    $("#hidLongRRLL4").val(12);
                    break;
                case "6":
                    $("#hidTipDocRRLL4").val(6);
                    $("#hidLongRRLL4").val(11);
                    break;
                case "1":
                    $("#hidTipDocRRLL4").val(1);
                    $("#hidLongRRLL4").val(12);
                    break;
                case "2":
                    $("#hidTipDocRRLL4").val(2);
                    $("#hidLongRRLL4").val(8);
                    break;
                default:
                    $("#hidTipDocRRLL4").val(99);
                    $("#hidLongRRLL4").val(20);
                    break;
            }
        },
        cboTD5_change: function () {
            var that = this,
              controls = that.getControls();
            var opcion = "";

            opcion = $('#cboTD5').val();

            switch (opcion) {
                case "4":
                    $("#hidTipDocRRLL5").val(4);
                    $("#hidLongRRLL5").val(12);
                    break;
                case "6":
                    $("#hidTipDocRRLL5").val(6);
                    $("#hidLongRRLL5").val(11);
                    break;
                case "1":
                    $("#hidTipDocRRLL5").val(1);
                    $("#hidLongRRLL5").val(12);
                    break;
                case "2":
                    $("#hidTipDocRRLL5").val(2);
                    $("#hidLongRRLL5").val(8);
                    break;
                default:
                    $("#hidTipDocRRLL5").val(99);
                    $("#hidLongRRLL5").val(20);
                    break;
            }
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