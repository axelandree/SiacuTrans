sessionTransacLTE = JSON.parse(sessionStorage.getItem("SessionTransac"));
//console.logsessionTransacLTE);
//if (sessionTransacLTE.UrlParams.IDSESSION == null || sessionTransacLTE.UrlParams.IDSESSION == undefined) {
//    sessionTransacLTE.UrlParams.IDSESSION = '0';
//}

var IsPostBack = true;
var SessionCommercialServices = {};
var userValidatorAuth = "";
var HiddenPageHtml = {};

function CloseValidation(obj, pag, controls) {
    var oHidden = Session.SessionParams.HIDDEN;
    obj.hidAccion = 'G';
    userValidatorAuth = obj.EmailUserValidator;
    if (obj.hidAccion === 'G') {
        oHidden.hdnDateProg = $("#txtProgramDate").val();
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = $("#hidDescripcionProceso_Validar").val();
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '' && typeof (description) != typeof (undefined)) {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }

        alert(mensaje, "Informativo");

        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
}

function GetCamapaign(snCode) {
    var strParam = {};
    strParam.strContractId = sessionTransacLTE.SessionParams.DATACUSTOMER.ContractID;
    strParam.strSncode = snCode; //sncode;
    strParam.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(strParam),
        url: '/Transactions/LTE/AdditionalServices/GetCamapaign',
        success: function (response) {
            var item = "";
            if (response.length > 0) {
                $("#hdnCampaing").show("fade");
                item = "<option>--Seleccione--</option>";
                $.each(response,
                    function (index, value) {
                        item += "<option value='" + value.Codigo + "'>" + value.Descripcion + "</option>";
                    });
                $("#ddlCamapaign").html(item);
            } else {
                $("#hdnCampaing").hide();
            }
        }
    });
}

function setDataToTables(data, oUserAccess, oHidden) {
    var totalsin = 0;
    var totalcon = 0;
    var valorAux = 0;

    var filas = data;
    var strParam;
    if (filas.length == 0) {
        strParam = {
            strIdSession: sessionTransacLTE.UrlParams.IDSESSION,
            strIdTransaction: oUserAccess.userId,
            cargoSinIgv: "00.00",
            cargoConIgv: "00.00"
        };
        $.ajax({
            type: "POST",
            url: '/Transactions/LTE/AdditionalServices/LogTotalFixedCharge',
            data: JSON.stringify(strParam),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
            }
        });
    }

    var gruposdeservicio = new Array();
    var existe = 0;
    var indiceGrupos = 0;
    // SE ARMAN LOS GRUPOS DE SERVICIO EN gruposdeservicio
    for (var i = 0; i < filas.length; i++) {
        existe = 0;

        for (var e = 0; e < gruposdeservicio.length; e++) {
            if (gruposdeservicio[e] == filas[i].DE_GRP) {
                existe = 1;
            }
        }

        if (existe == 0) {
            gruposdeservicio[indiceGrupos] = filas[i].DE_GRP;
            indiceGrupos++;
        }
    }

    var index = 0;
    if (filas.length == 0) {
        var htmlNoData = '<tr><td colspan="9" style="color:black; text-align: center;"> No existen servicios adicionales para este contrato.</td></tr>';
        $("#tblServBody").html(htmlNoData);
        alert("No hay servicios disponibles para el contrato.", "Alerta");
        return false;
    }
    if (filas[0].DE_SER == "Error") {
        alert("Ocurrió un error al cargar los servicios.", "Alerta");
        return false;
    }
    var html = "";
    var a = "";

    var htmlgrupos = new Array();
    var aux1 = "";
    for (var u = 0; u < gruposdeservicio.length; u++) {
        aux1 = "";
        aux1 = aux1 + "<tr>";
        aux1 = aux1 + "<td colspan='9' style='color:white;background-color:#a7a8aa;'>" + gruposdeservicio[u] + "</td>";
        aux1 = aux1 + "</tr>";
        htmlgrupos[u] = aux1;
    }

    var vacio = "&nbsp;";
    $.each(filas, function (key, value) {
        var gruposerv = filas[index].DE_GRP;
        var coSer = filas[index].CO_SER;
        var SNCODE = filas[index].SNCODE;
        var SPCODE = filas[index].SPCODE;
        var estado = filas[index].ESTADO;
        var bloqact = filas[index].BLOQ_ACT;
        var bloqdes = filas[index].BLOQ_DES;
        var valorpvu = filas[index].VALORPVU;
        var costopvu = filas[index].COSTOPVU;
        var descoser = filas[index].DESCOSER;
        var cargoFijo = filas[index].CUOTA;
        var tipoServicio = filas[index].TIPOSERVICIO;
        var tiposervpvu = filas[index].TIPO_SERVICIO;

        //Poryecto LTE: vmg
        var codservpvu = filas[index].CODSERPVU;
        var motivoporpquete = filas[index].DE_EXCL;
        var validodesde = filas[index].VALIDO_DESDE;
        var periodo = filas[index].PERIODOS;

        var color = "black";
        var fondo = "white";

        var flagEstado = "";
        valorAux = 0;
        if (estado == "A") {
            color = "green";
            flagEstado = "A";
            //totalsin = parseFloat(totalsin) + parseFloat(cargoFijo.replace(",", "."));
            valorAux = cargoFijo;
        }
        if (estado == "D") {
            color = "red";
            flagEstado = "D";
        }
        if (bloqact == "S" && bloqdes == "S" || tiposervpvu == "CORE ADICIONAL" || tiposervpvu == "CORE") {
            fondo = "#FAFAB1";
            flagEstado = "N";
        } else {
            if (bloqact == "S") {
                fondo = "#E9E9E7"; //plomo
                flagEstado = "N";
            } else {
                if (bloqdes == "S") {
                    fondo = "Fuchsia";
                    flagEstado = "N";
                }
            }
        }

        if (estado == "" && bloqact != "S" && bloqdes != "S") {
            flagEstado = "D";
        }

        html = "";
        html = html + "<tr>";
        var idRadio = "rdb_" + coSer + "_" + SNCODE + "_" + SPCODE + "_" + index.toString() + "_" + valorpvu + "_" + costopvu + "_" + descoser + "_" + cargoFijo + "_" + flagEstado + "_" + tipoServicio + "_" + codservpvu + "_" + gruposerv + "_" + motivoporpquete + "_" + validodesde + "_" + periodo;
        //var idRadio = "rdb_" + coSer + "_" + SNCODE + "_" + SPCODE + "_" + index.toString() + "_" + valorpvu + "_" + costopvu + "_" + descoser + "_" + cargoFijo + "_" + flagEstado + "_" + tipoServicio;
        html = html + "<td style='width:30px; text-align:center; color:" + color + ";'><input id='" + idRadio + "' onclick='SetValue(this.value)' name='group3' value='" + idRadio + "' type='radio' /></td>";

        a = descoser;
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:150px; text-align:center; color:" + color + ";background-color:" + fondo + ";' >" + a + "</td>";
        a = filas[index].DE_EXCL;
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = flagEstado;
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:40px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = filas[index].VALIDO_DESDE;
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:70px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = "";
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = cargoFijo;
        if (a == "") {
            a = vacio;
        }

        html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = "";
        if (estado == "A") {
            a = a + cargoFijo;
            valorAux = a;
        } else {
            a = a + costopvu;
        }

        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        a = filas[index].PERIODOS;
        if (a == "") {
            a = vacio;
        }
        html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
        html = html + "</tr>";

        if (valorAux != 0) {
            totalsin = parseFloat(totalsin) + parseFloat(valorAux.replace(",", "."));
        }

        for (var t = 0; t < gruposdeservicio.length; t++) {
            if (gruposerv == gruposdeservicio[t]) {
                htmlgrupos[t] = htmlgrupos[t] + html;
            }
        }


        index++;
    });

    var valorIgv = oHidden.hdnValorIGV; // SessionCommercialServices.hdnValorIGV;

    totalcon = parseFloat(totalsin) * parseFloat(1 + parseFloat(valorIgv));

    document.getElementById('lblBuyWithoutIGV').innerHTML = parseFloat(totalsin).toFixed(2);
    document.getElementById('lblBuyWithIGV').innerHTML = parseFloat(totalcon).toFixed(2);

    strParam = {
        strIdSession: "50548795",//sessionTransacLTE.UrlParams.IDSESSION,
        strIdTransaction: oUserAccess.userId,//cambiar
        cargoSinIGV: parseFloat(totalsin).toFixed(2),
        cargoConIGV: parseFloat(totalcon).toFixed(2)
    };
    $.ajax({
        type: "POST",
        url: '/Transactions/LTE/AdditionalServices/LogTotalFixedCharge',
        data: JSON.stringify(strParam),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
        }
    });

    var htmlFinal = "";
    for (var k = 0; k < gruposdeservicio.length; k++) {
        htmlFinal = htmlFinal + htmlgrupos[k];
    }
    $("#tblServBody").html(htmlFinal);

    if (index == 0) {
        alert("No hay servicios disponibles para el contrato.", "Alerta");
    }
    return false;
}

function getObjectCustomerData(oUserAccess, oDataCustomer, oHidden) {
    var BE = {};
    BE.StrUser = oUserAccess.login;
    BE.StrName = oDataCustomer.Name;
    BE.StrLastName = oDataCustomer.LastName;
    BE.StrBusinessName = oDataCustomer.BusinessName;
    BE.StrDocumentType = oDataCustomer.DocumentType;
    BE.StrDocumentNumber = oDataCustomer.DocumentNumber;
    BE.StrAccount = oDataCustomer.Account;
    BE.StrDistrict = oDataCustomer.District;
    BE.StrDepartament = oDataCustomer.Departament;
    BE.StrProvince = oDataCustomer.Province;
    BE.StrCustomerId = oDataCustomer.CustomerID;
    BE.StrTelephone = oHidden.hdnTelephone;
    BE.StrFirtName = oDataCustomer.Name;
    BE.StrPhoneReference = oDataCustomer.PhoneReference;
    BE.StrCustomerContact = oDataCustomer.CustomerContact;
    BE.StrCacDac = $('#cboCACDAC option:selected').text();

    BE.StrActivationDate = oDataCustomer.ActivationDate;
    BE.StrAddress = oDataCustomer.Address;
    BE.StrAssessor = oDataCustomer.Assessor;
    BE.StrBillingCycle = oDataCustomer.BillingCycle;


    BE.StrCivilStatus = oDataCustomer.CivilStatus;
    BE.StrCivilStatusId = oDataCustomer.CivilStatusID;
    BE.StrCodCustomerType = oDataCustomer.CodCustomerType;
    BE.StrCodeCenterPopulate = oDataCustomer.CodeCenterPopulate;
    BE.StrContactCode = oDataCustomer.ContactCode;
    BE.StrContractId = oDataCustomer.ContractID;


    BE.StrCustomerType = oDataCustomer.CustomerType;
    BE.StrDniruc = oDataCustomer.DNIRUC;

    if (oDataCustomer.Email == "") {
        BE.StrEmail = $("#txtEmail").val();
    } else {
        BE.StrEmail = oDataCustomer.Email;
    }

    BE.StrFax = oDataCustomer.Fax;
    BE.StrFullName = oDataCustomer.FullName;
    BE.StrInstallUbigeo = oDataCustomer.InstallUbigeo;
    BE.StrInvoiceAddress = oDataCustomer.InvoiceAddress;
    BE.StrInvoiceCountry = "";
    BE.StrInvoiceDepartament = "";
    BE.StrInvoiceDistrict = "";
    BE.StrInvoicePostal = "";
    BE.StrInvoiceProvince = "";
    BE.StrInvoiceUbigeo = "";
    BE.StrInvoiceUrbanization = "";
    BE.StrLegalAddress = "";
    BE.StrLegalAgent = "";
    BE.StrLegalCountry = "";
    BE.StrLegalDepartament = "";
    BE.StrLegalDistrict = "";
    BE.StrLegalPostal = "";
    BE.StrLegalProvince = "";
    BE.StrLegalUrbanization = "";
    BE.StrModality = "";
    BE.StrOfficeAddress = "";
    BE.StrPaymentMethod = "";
    BE.StrPhoneContact = "";

    BE.StrPlaneCodeBilling = "";
    BE.StrPlaneCodeInstallation = "";
    BE.StrPosition = "";
    BE.StrProductType = "";

    BE.StrReference = "";
    BE.StrSegment2 = "";
    BE.StrSex = "";
    BE.StrSiteCode = "";

    BE.StrTradename = "";
    BE.StrUrbanization = "";
    BE.StrAddressee = "";
    BE.StrLegalRepresent = oDataCustomer.LegalAgent;
    BE.StrPhone = "";
    return BE;
}

function getObjectLineData(oDataLine) {
    var BE = {};
    BE.StrActivationDate = oDataLine.ActivationDate;
    BE.StrCableValue = oDataLine.CableValue;
    BE.StrCampaign = oDataLine.Campaign;
    BE.StrCellPhone = oDataLine.CellPhone;
    BE.StrChangedBy = oDataLine.ChangedBy;
    BE.StrCodePlanTariff = oDataLine.CodePlanTariff;
    BE.StrContractId = oDataLine.ContractID;
    BE.StrFlagPlatform = oDataLine.FlagPlatform;
    BE.StrFlagTfi = oDataLine.FlagTFI;
    BE.StrInternetValue = oDataLine.InternetValue;
    BE.StrIntroduced = oDataLine.Introduced;
    BE.StrIntroducedBy = oDataLine.IntroducedBy;
    BE.StrIsLte = oDataLine.IsLTE;
    BE.StrIsNot3Play = oDataLine.IsNot3Play;
    BE.StrMsisdn = Session.SessionParams.HIDDEN.hdnTelephone;//oDataLine.MSISDN;
    BE.StrNumberIccid = oDataLine.NumberICCID;
    BE.StrNumberImsi = oDataLine.NumberIMSI;
    BE.StrPin1 = oDataLine.PIN1;
    BE.StrPin2 = oDataLine.PIN2;
    BE.StrPuk1 = oDataLine.PUK1;
    BE.StrPuk2 = oDataLine.PUK2;
    BE.StrPlan = oDataLine.Plan;
    BE.StrPlanTariff = oDataLine.PlanTariff;
    BE.StrProviderId = oDataLine.ProviderID;
    BE.StrReason = oDataLine.Reason;
    BE.StrSeller = oDataLine.Seller;
    BE.StrStateAgreement = oDataLine.StateAgreement;
    BE.StrStateDate = oDataLine.StateDate;
    BE.StrStateLine = oDataLine.StateLine;
    BE.StrTelephonyValue = oDataLine.TelephonyValue;
    BE.StrTermContract = oDataLine.TermContract;
    BE.StrTypeProduct = oDataLine.TypeProduct;
    BE.StrValidFrom = oDataLine.ValidFrom;
    return BE;
}

function getObjectHiden(oHidden, that, oDataLine) {
    var BE = {};
    //Hidden

    BE.HdnType = oHidden.hdnType;
    BE.HdnClase = oHidden.hdnClase;
    BE.HdnSubClass = oHidden.hdnSubClass;
    BE.HdnTypeCode = oHidden.HdnTypeCode;
    BE.HdnClaseCode = oHidden.hdnClaseCode;
    BE.HdnSubClassCode = oHidden.hdnSubClassCode;
    BE.HdnInteractionCode = oHidden.hdnInteractionCode;


    BE.HdnCostoPvuSel = oHidden.hdnCostoPVUSel;
    BE.HdnDesCoSerSel = oHidden.hdnDesCoSerSel;
    BE.HdnCostoBscs = oHidden.hdnCostoBSCS;
    BE.HdnTipoTransaccion = HiddenPageHtml.hdnTipoTransaccion;
    BE.HdnCargoFijoSel = oHidden.hdnCargoFijoSel;
    BE.HdnCoSerSel = oHidden.hdnCoSerSel;
    BE.TxtNota = $('#txtNotesTask').val();
    BE.PvwNumeroTelefono = "";
    BE.Plan = oDataLine.Plan;
    BE.StrTypeProduct = oHidden.hdnDescrTypeService;

    if ($('#chkSendMail').prop('checked')) {
        BE.ChkEnviarPorEmail = true;
        BE.TxtEnviarporEmail = $('#txtEmail').val();
    }
    else {
        BE.ChkEnviarPorEmail = false;
        BE.TxtEnviarporEmail = "";
    }


    if ($('#chkProgram').prop('checked')) {
        BE.ChkProgramar = true;
        BE.HdnFechaProg = $('#txtProgramDate').val();
    }
    else {
        BE.ChkProgramar = false;
        BE.HdnFechaProg = "";
    }
    return BE;
}

function setActivateDesactive(controls, oHidden, oUserAccess, oDataCustomer, oDataLine, stateService, that) {

    if ($("#cboCACDAC").val() == "") {
        alert(oHidden.hdnSelectCacDac, "Alerta");
        return false;
    }

    if ($("#hdnCoSerSel").val() == "") {
        alert("Debe seleccionar un servicio.", "Alerta");
        return false;
    }

    if ($("#chkCampiagn").prop("checked")) {
        if ($("#ddlCamapaign").val() == "" || $("#ddlCamapaign").val() == "") {
            alert("Debe seleccionar una campaña.", "Alerta");
            return false;
        } else {
            oHidden.hdnCamapaign = $("#ddlCamapaign").val();
        }
    } else {
        oHidden.hdnCamapaign = "";
    }


    if ($("#chkSendMail").prop("checked")) {
        if ($("#chkSendMail").val() == "") {
            alert("Debe ingresar un email.", "Alerta");
            return false;
        }

        regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
        var blvalidar = regx.test($("#txtEmail").val());
        if (!blvalidar) {
            alert("Debe ingresar un email válido.", "Alerta");
            return false;
        } 
        //if (!ValidateEmail(controls.getControls().txtEmail.val())) {
        //    return false;
        //}
    }

    if ($("#chkProgram").prop("checked")) {
        if ($("#fechaProgramacion").val() == "") {
            alert("Debe seleccionar una fecha de programación.", "Alerta");
            return false;
        }
    }

    //HiddenPageHtml.hdnTipoTransaccion = "A"; //falta declara funciona aun
    if (HiddenPageHtml.hdnTipoTransaccion != "A" && HiddenPageHtml.hdnTipoTransaccion != "D") {
       alert("Debe seleccionar un servicio activo o desactivo.", "Alerta");
       return false; 
    }

    oHidden.hdnDateProg = $("#txtProgramDate").val();


    ////Si
    confirm("¿Está seguro que desea grabar la transaccion?", 'Confirmar', function (result) {
        if (result == true) {
            var model = {};
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
            model.oServerModel = {};
            model.oCustomersDataModel = {};
            model.oLineDataModel = {};
            model.oHiddenModel = {};
            model.oCustomersDataModel = getObjectCustomerData(oUserAccess, oDataCustomer, oHidden);
            model.oLineDataModel = getObjectLineData(oDataLine);
            model.oHiddenModel = getObjectHiden(oHidden, controls, oDataLine);

            model._StrMsisdn = oHidden.hdnTelephone;
            model.StrCoId = oDataCustomer.ContractID;
            model.StrCoSer = oHidden.hdnCoSerSel;

            switch (stateService)
            {
                case 1:
                    model.StrTypeRegistry = 'A';
                break;
               case 2:
                   model.StrTypeRegistry = 'D';
                break;  
            }

            model.StrCycleFacturation = "";
            model.StrDescriptioCoSer = "";
            model.StrNroAccount = "";
            model.StrTypeSerivice = "";
            model.TypeTransaction = HiddenPageHtml.hdnTipoTransaccion;
            model.StrNote = $('#txtNotesTask').val();
            model.StrInteractionId = "";
            model.StrStateService = stateService;
            if (document.getElementById('chkSendMail').checked == true) {
                model.ChkEnviarPorEmail = true;
            }
            else {
                model.ChkEnviarPorEmail = false;
            }

            if (document.getElementById('chkProgram').checked == true) {

                model.StrDateProgramation = controls.txtProgramDate.val();
            }
            else {
                model.StrDateProgramation = "";
            }

            //Usuarios
            model.oServerModel.StrIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            model.oServerModel.StrNameServer = oHidden.hdnServName;
            model.oServerModel.StrIpServer = oHidden.hdnLocalAdd;
            model.oServerModel.StrAccountUser = oUserAccess.login;

            $.ajax({
                url: '/Transactions/LTE/AdditionalServices/SaveTransaction',
                type: 'POST',
                data: JSON.stringify(model),
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {                    
                    HiddenPageHtml.hidInteractionId = response.strInteractionId;
                    if (response.btnConstancy) {
                        that.DisableButtons();
                        $("#btnConstancy").attr('disabled', false);

                    } else { 
                        $("#btnConstancy").attr('disabled', true);
                        that.DisableButtons();
                    }

                    sessionTransacLTE.FullPathPDF = response.FullPathPDF;
                    if (response.lblMsgEmail != null) {
                        $("#divErrorAlert").show();
                        $("#lblErrorMessage").text(response.lblMsgEmail);
                    }

                    alert(response.lblMessage, "Informativo");

                },
                error: function (request, status, error) {
                    alert("Ocurrió un error al ejecutar la programación.", "Alerta");
                }
            });
        }
    });

}

//f_asigna
function SetValue(values) {
    var oHidden = Session.SessionParams.HIDDEN;
    HiddenPageHtml.hidCheckTable = "1";
    $("#chkProgram").attr('disabled', false);
    var valores = values.split("_");
    oHidden.hdnSNCodeSel = valores[2];
    oHidden.hdnSPCodeSel = valores[3];
    oHidden.hdnCoSerSel = valores[1];
    oHidden.hdnIndiceSeleccionado = valores[4];
    oHidden.hdnValorPVUMatch = valores[5];
    oHidden.hdnCostoPVUSel = valores[6];
    oHidden.hdnDesCoSerSel = valores[7];
    oHidden.hdnCargoFijoSel = valores[8];
    oHidden.hdnCostoBSCS = valores[8];
    oHidden.hdnTipiServicio = valores[10];
    HiddenPageHtml.hdnEstadoSerSel = valores[9];
    oHidden.hdnCodSerPVU = valores[11];
    oHidden.hdnDescrTypeService = valores[12];
    oHidden.hdnMotivoPorPaquete = valores[13];
    oHidden.hdnValidoDesde = valores[14];
    oHidden.hdnPeriodo = valores[15];

    if (valores[9] == "A") {
        HiddenPageHtml.hdnTipoTransaccion ="A";
        $("#btnDesactive").attr('disabled', false);
        $("#btnActive").attr('disabled', true);
        $("#hdnCampaing").hide();
    } else {
        if (valores[9] == "D") {
            HiddenPageHtml.hdnTipoTransaccion = "A";
            $("#btnActive").attr('disabled', false);
            $("#btnDesactive").attr('disabled', true);
            GetCamapaign(oHidden.hdnSNCodeSel);
        } else {
            $("#chkProgram").attr('disabled', true);
            $("#chkProgram").attr('checked', false);
            HiddenPageHtml.hdnTipoTransaccion = "";
            oHidden.hdnTipoTransaccion = "";
            $("#btnActive").attr('disabled', true);
            $("#btnDesactive").attr('disabled', true);
            $("#btnProActive").attr('disabled', true);
            $("#btnProDesactive").attr('disabled', true);
            $("#hdnCampaing").hide("fade");
        }
    }
    if (!($("#btnProActive").attr('disabled')) || !($("#btnProDesactive").attr('disabled'))) {
        $("#btnDesactive").attr('disabled', true);
        $("#btnActive").attr('disabled', true);
    }
    return false;
}

//ADRadioButton
function AdRadioButton(accion, estado) {
    var arrListaServicios = [];
    var count = 0;
    if (accion == 'DESACTIVAR') { //Accion de desactivar los servicios diferentes al seleccionado
        for (var i = 0; i < (parseInt($("#tblServicios tr").length, 10) + parseInt(1), 10) ; i++) {
            if ($("#tblServicios tr").eq(i).children("td").length > 1) {
                if (estado == 'D') { // estados desactivo
                    if ($("#tblServicios tr")[i].cells[3].innerText == 'D' || $.trim($("#tblServicios tr")[i].cells[3].innerText) == '')//Valida si la celda es D = desactivo o vacio
                    { $("#tblServBody input[type='radio']").eq(i - count).prop('disabled', false); }//se le resta las cabeceras de los servicios que no contienen radioButton
                    else
                    { $("#tblServBody input[type='radio']").eq(i - count).prop('disabled', true); }
                }
                else if (estado == 'A') { //Estado Activo
                    if ($("#tblServicios tr")[i].cells[3].innerText == 'A')//Valida si la celda es A = Activo
                    { $("#tblServBody input[type='radio']").eq(i - count).prop('disabled', false); }
                    else
                    { $("#tblServBody input[type='radio']").eq(i - count).prop('disabled', true); }
                }

            }
            else {
                count = count + 1;// suma si encuentra alguna cabecera
            }
        }
    }
    else if (accion == 'ACTIVAR') { // Accion de activar todos los servicios
        $("#tblServBody input[type='radio']").attr('disabled', false);
    }
}

function RunNextScheduledDate(e, controls) {
    if (controls.cboCACDAC.val() == "") {
        alert(Session.SessionParams.HIDDEN.hdnSelectCacDac, "Alerta");
        return false;
    }

    if (controls.chkSendMail.prop('checked')) {
        if (!ValidateEmail(controls.txtEmail.val())) {
            return false;
        }
    }
     
    $('.next-step-scheduleddate').addClass("next-step");
    navigateTabs(e);
    $('.next-step-scheduleddate').removeClass("next-step");
}

function ValidateEmail(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (email.length == 0) {
        alert("Ingrese una dirección de correo valida.", "Alerta");
        return false;
    }
    if (filter.test(email))
        return true;
    else
        alert("Ingrese una dirección de correo valida.", "Alerta");
    return false;
}

(function ($) {
    
    var Form = function ($element, options) {
        $.extend(this, $.fn.LTEActiveDesactiveService.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            //DIVS
            divContentProgramDate: $('#divContentProgramDate', $element),

            //LABELS
            lblCustomerId: $('#lblCustomerId', $element),
            lblContactName: $('#lblContactName', $element),
            lblCustomerName: $('#lblCustomerName', $element),
            lblPlanName: $('#lblPlanName', $element),
            lblCycleFacture: $('#lblCycleFacture', $element),
            lblDateActivation: $('#lblDateActivation', $element),
            lblLteContract: $('#lblLteContract', $element),
            lblHeaderTypeCustomer: $('#lblHeaderTypeCustomer', $element),
            lblDocument: $('#lblDocument', $element),
            lblLimitCredit: $('#lblLimitCredit', $element),
            lblRepreLegal: $('#lblRepreLegal', $element),
            lblDocRepresLegal: $('#lblDocRepresLegal', $element),
            divErrorAlert: $('#divErrorAlert', $element),
            divErrorAlertTable: $('#divErrorAlertTable', $element),
            //TEXTS
            txtEmail: $('#txtEmail', $element),
            txtNotesTask: $('#txtNotesTask', $element),
            txtProgramDate: $('#txtProgramDate', $element),
            txtDireccion: $('#txtDireccion', $element),
            txtNotaDireccion: $('#txtNotaDireccion', $element),
            txtDepartamento: $('#txtDepartamento', $element),
            txtPais: $('#txtPais', $element),
            txtDistrito: $('#txtDistrito', $element),
            txtProvincia: $('#txtProvincia', $element),
            txtCodigoPlano: $('#txtCodigoPlano', $element),
            txtCodigoUbigeo: $('#txtCodigoUbigeo', $element),

            //CHECKBOX
            //chkModificarCouta: $('#chkModificarCouta', $element),
            chkSendMail: $('#chkSendMail', $element),
            chkProgram: $('#chkProgram', $element),
            chkCampiagn: $('#chkCampiagn', $element),

            //SELECTS
            cboCACDAC: $('#cboCACDAC', $element),

            //BUTTONS
            btnModificarCuota: $('#btnModificarCuota', $element),
            btnCheckDevices: $('#btnCheckDevices', $element),
            btnProgramTask: $('#btnProgramTask', $element),
            btnProActive: $('#btnProActive', $element),
            btnProDesactive: $('#btnProDesactive', $element),
            btnActive: $('#btnActive', $element),
            btnDesactive: $('#btnDesactive', $element),
            btnConstancy: $('#btnConstancy', $element),
            btnClose: $('#btnClose', $element),
            btnNextForm: $('#btnNextForm', $element),
            btnNexResumen: $('#btnNexResumen', $element),
            //MODAL LOAD
            myModalLoad: $('#myModalLoad', $element),

            //Table
            lblCustomerIds: $('#lblCustomerIds', $element),
            lblContract: $('#lblContract', $element),
            lblName: $('#lblName', $element),
            lblState: $('#lblState', $element),
            lblDate: $('#lblDate', $element),
            lblMotive: $('#lblMotive', $element),
            spnMainTitle: $('#spnMainTitle'),
            divRules: $('#divRules', $element)
        });
    };

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.txtProgramDate.datepicker({ format: 'dd/mm/yyyy', startDate: '+0d', endDate: '+60d' });
            //Events Buttons
            controls.btnModificarCuota.addEvent(that, 'click', that.btnModificarCuota_click);
            controls.btnCheckDevices.addEvent(that, 'click', that.btnCheckDevices_click);
            controls.btnProgramTask.addEvent(that, 'click', that.btnProgramTask_click);
            controls.btnProActive.addEvent(that, 'click', that.btnProActive_click);
            controls.btnProDesactive.addEvent(that, 'click', that.btnProDesactive_click);
            controls.btnActive.addEvent(that, 'click', that.btnActive_click);
            controls.btnDesactive.addEvent(that, 'click', that.btnDesactive_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnNextForm.addEvent(that, 'click', that.btnNextForm_click);
            controls.chkSendMail.addEvent(that, 'change', that.chkSendMail_Change);
            controls.chkProgram.addEvent(that, 'click', that.chkProgram_click);
            controls.txtProgramDate.addEvent(that, 'change', that.txtProgramDate_Change);
            controls.chkCampiagn.addEvent(that, 'click', that.chkCampiagn_click);
            controls.btnNexResumen.addEvent(that, 'click', that.btnNexResumen_click);

           
            //$('.next-step-scheduleddate, .prev-step-scheduleddate').on('click', RunNextScheduledDate(e));
            $('.next-step-scheduleddate').on('click', function (e) {
                RunNextScheduledDate(e, controls);
            });

            controls.txtEmail.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
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
            that.Page_Load();
            $("#txtProgramDate").attr("disabled", true);

        },

        //======= Inicio ====
        Page_Load: function () {
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oUserAccess = sessionTransacLTE.SessionParams.USERACCESS,
                oDataCustomer = sessionTransacLTE.SessionParams.DATACUSTOMER,
                oDataLine = sessionTransacLTE.SessionParams.DATASERVICE;

            var parameter = {};
            parameter.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            parameter.strStateLine = "";

            //Traer los AppConfig
            $.app.ajax({
                url: '/Transactions/LTE/AdditionalServices/Page_Load',
                data: JSON.stringify(parameter),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {

                    if (response.hdnValorIGV == "0") {
                        alert("El servicio de consulta IGV no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.", "Alerta", function () {
                            parent.window.close();
                        });

                        return;
                    }

                    oHidden.hdnStateLine = response.hdnStateLine;
                    oHidden.hdnRouteSiteStart = response.hdnRouteSiteStart;
                    oHidden.hdnValorIGV = response.hdnValorIGV;
                    oHidden.hdnTitleActandDesact = response.hdnTitleActandDesact;
                    oHidden.hdnSelectCacDac = response.hdnSelectCacDac;
                    oHidden.hdnLocalAddr = response.hdnLocalAddr;
                    oHidden.hdnServerName = response.hdnServerName;
                    oHidden.hdnProblemLoad = response.hdnProblemLoad;
                    oHidden.hdnCodSegHabiliCheckCampLte = response.hdnCodSegHabiliCheckCampLte;
                    oHidden.hdnTranstionActDesacServLte = response.hdnTranstionActDesacServLte;
                    oHidden.hdnConstTypeLte = response.hdnConstTypeLte;
                    oHidden.hdnMsgAjustNrRecon = response.hdnAjustNrRecon;
                    oHidden.hdnProductoLTE = response.hdnProductoLTE;
                    HiddenPageAuth.hidOpcion = response.hdnProductoLTE;
                    HiddenPageAuth.hidCodOpcion = response.hdnProductoLTE;
                    controls.spnMainTitle.html('<b>' + response.hdnTitleActandDesact + '</b>');
                    if (oUserAccess.userId == null || oDataCustomer === {}) {
                        IsPostBack = false;
                        alert(oHidden.hdnRouteSiteStart, "Alerta");
                        parent.window.close();
                        return false;
                    }
                    if (IsPostBack) {
                        that.Start(controls, oHidden, oDataCustomer, oDataLine, oUserAccess);
                        that.LoadTyping(oHidden, oUserAccess);
                        that.DisableButtons();
                        that.CACDAC(oUserAccess);
                        that.HideInputText();
                        that.ServiceCommercial(oUserAccess, oHidden, oDataCustomer);
                    }
                    return false;
                }
            });
        },
        Start: function (controls, oHidden, oDataCustomer, oDataLine, oUserAccess) {
            controls.lblLteContract.text(oDataCustomer.ContractID);
            controls.lblCustomerId.text(oDataCustomer.CustomerID);
            controls.lblContactName.text(oDataCustomer.FullName);
            controls.lblCustomerName.text(oDataCustomer.BusinessName);
            controls.lblPlanName.text(oDataLine.Plan);
            controls.lblCycleFacture.text(oDataCustomer.BillingCycle);
            controls.lblDateActivation.text(oDataCustomer.ActivationDate);
            controls.lblHeaderTypeCustomer.text(oDataCustomer.CustomerType);
            controls.lblDocument.text(oDataCustomer.DNIRUC);
            controls.lblLimitCredit.text(oDataCustomer.objPostDataAccount.CreditLimit);
            controls.lblRepreLegal.text(oDataCustomer.LegalAgent);
            controls.lblDocRepresLegal.text(oDataCustomer.DocumentNumber);
            //Direccion
            controls.txtDireccion.text(oDataCustomer.InvoiceAddress);
            controls.txtNotaDireccion.text(oDataCustomer.Reference);
            controls.txtDepartamento.text(oDataCustomer.Departament);
            controls.txtPais.text(oDataCustomer.LegalCountry);
            controls.txtDistrito.text(oDataCustomer.District);
            controls.txtProvincia.text(oDataCustomer.Province);
            controls.txtCodigoPlano.text(oDataCustomer.CodeCenterPopulate);
            controls.txtCodigoUbigeo.text(oDataCustomer.InstallUbigeo);
            controls.divErrorAlert.hide();
            controls.divErrorAlertTable.hide();
            HiddenPageHtml.hidCheckTable = "0";

            oHidden.hdnTelephone = oDataLine.CellPhone;
            if (oHidden.hdnTelephone == null) {
                var paramters = {};
                paramters.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
                paramters.intIdContract = oDataCustomer.ContractID;
                paramters.strTypeProduct = sessionTransacLTE.UrlParams.SUREDIRECT;
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(paramters),
                    url: '/Transactions/LTE/AdditionalServices/GetCustomerPhone',
                    success: function (response) {
                        oHidden.hdnTelephone = response;
                        sessionTransacLTE.SessionParams.DATASERVICE.MSISDN = response;
                    }
                });
            }
            controls.lblCustomerIds.text(oDataCustomer.CustomerID);
            controls.lblContract.text(oDataCustomer.ContractID);
            controls.lblName.text(oDataCustomer.FullName);
            controls.lblState.text(oDataLine.StateLine);
            controls.lblDate.text(oDataLine.ActivationDate);
            controls.lblMotive.text(oDataLine.StateLine);//Motivo
            $("#txtEmail").val(oDataCustomer.Email);
            oHidden.hdnCycleFacturation = oDataCustomer.BillingCycle;

            if (oUserAccess.optionPermissions.indexOf(oHidden.hdnCodSegHabiliCheckCampLte)) {
                $("#chkCampiagn").attr('disabled', false);
            } else {
                $("#chkCampiagn").attr('disabled', true);
            }
            var strCycleFacturation = oHidden.hdnCycleFacturation;
            var programDate = new Date();
            var dateProgramationComp = new Date(programDate.getFullYear(), programDate.getMonth(), (parseInt(strCycleFacturation, 10) - 1));
            if (programDate == dateProgramationComp) {
                programDate.setDate(programDate.getDate() + 1);
            }
            programDate.setDate(programDate.getDate() + 1);
            $('#txtProgramDate').datepicker({ format: 'dd/mm/yyyy', culture: "es-PE", min: programDate });
            oHidden.hdnProgramDate = programDate;

            $("#chkProgram").attr('disabled', true);
            $("#chckContract").attr("checked", true);

            $("#hdnCampaing").hide();
            $("#ddlCamapaign").attr('disabled', true);
        },
        LoadTyping: function (oHidden, oUserAccess) {
            var that = this;
            var parameters = {};
            parameters.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            parameters.strTransactionName = oHidden.hdnTranstionActDesacServLte;
            parameters.strType = oHidden.hdnConstTypeLte;

            $.app.ajax({
                url: '/Transactions/CommonServices/GetTypification',
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    if (response.ListTypification.length > 0)
                    {
                        $.each(response.ListTypification,
                            function (index, value) {
                                if (value.CLASE != null) {
                                    oHidden.hdnClaseCode = value.CLASE_CODE;
                                    oHidden.hdnSubClassCode = value.SUBCLASE_CODE;
                                    oHidden.hdnType = value.TIPO;
                                    oHidden.hdnSubClass = value.SUBCLASE;
                                    oHidden.hdnInteractionCode = value.INTERACCION_CODE;
                                    oHidden.hdnClase = value.CLASE;
                                    oHidden.HdnTypeCode = value.TIPO_CODE;
                                } else {
                                    alert(oHidden.hdnMsgAjustNrRecon, "Alerta");
                                    $("#btnConstancy").attr('disabled', false);
                                    that.DisableButtons();
                                }
                            });
                        that.getBusinessRules(oUserAccess.userId, oHidden.hdnSubClassCode);
                    } else
                    {
                        alert(oHidden.hdnMsgAjustNrRecon, "Alerta");
                        $("#btnConstancy").attr('disabled', false);
                        that.DisableButtons();
                    }
                }
            });
        },
        DisableButtons: function () {
            $("#btnActive").attr('disabled', true);
            $("#btnDesactive").attr('disabled', true);
            $("#btnProActive").attr('disabled', true);
            $("#btnProDesactive").attr('disabled', true);
            $("#btnConstancy").attr('disabled', true);
        },
        HideInputText: function () {
            var controls = this.getControls();
            //controls.txtEmail.prop("style").display = "none";
            //controls.divContentProgramDate.prop("style").display = "none";
        },
        chkSendMail_Change: function (sender, arg) {
            var controls = this.getControls();
            if (sender.prop("checked")) {
                controls.txtEmail.show();
                $("#txtEmail").attr("disabled", false);
            } else {
                controls.txtEmail.hide();
                $("#txtEmail").attr("disabled", true);
            }
        },
	CACDAC: function () {
            var that = this, objCacDacType = {
                strIdSession: "5051879654"
            };

            var parameters = {};
            parameters.strIdSession = "5051879654";
            parameters.strCodeUser = sessionTransacLTE.SessionParams.USERACCESS.login;

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
                            $("#cboCACDAC").append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {
                                if (cacdac === value.Description) {
                                    $("#cboCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    $("#cboCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
        ServiceCommercial: function (oUserAccess, oHidden, oDataCustomer) {
            var that = this, objCacDacType = {}, controls = that.getControls();
            objCacDacType.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            objCacDacType.strContractId = oDataCustomer.ContractID; //CAMBIO
            objCacDacType.strProductoLte = oHidden.hdnProductoLTE;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/LTE/AdditionalServices/LteGetCommercialSercices',
                success: function (data) {
                    setDataToTables(data, oUserAccess, oHidden);
                }
            });
        },
        getBusinessRules: function (userId, hdnSubClassCode) {
            var that = this,
                controls = that.getControls(),
                objRules = {};

            objRules.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            objRules.strSubClase = hdnSubClassCode;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }
                }
            });
        },

        //======= Act/Desact Service Add =======

        chkCampiagn_click: function () {
            var chkCampiagn = $("#chkCampiagn").prop("checked");
            if (chkCampiagn) {
                $("#ddlCamapaign").attr('disabled', false);
            } else {
                $("#ddlCamapaign").attr('disabled', true);
            }
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
        chkProgram_click: function () {
            var that = this, controls = this.getControls();

            var oHidden = Session.SessionParams.HIDDEN;
            var chkProgramar = $("#chkProgram").prop("checked");

            that.StateButton(chkProgramar);

            if (chkProgramar) {
                var strCycleFacturation = oHidden.hdnCycleFacturation;
                $("#txtProgramDate").attr("disabled", false);
                var dateCurrent = new Date();
                var dateFacturation = new Date(dateCurrent.getFullYear(), (dateCurrent.getMonth()), strCycleFacturation);
                var dateFactureComp = new Date(dateCurrent.getFullYear(), dateCurrent.getMonth(), (parseInt(strCycleFacturation, 10) - 1));
                if (dateCurrent >= dateFactureComp) {
                    dateFacturation.setMonth(dateFacturation.getMonth() + 1);
                }
                dateFacturation.setDate(dateFacturation.getDate() - 1);
                var month;
                if (parseInt(dateFacturation.getMonth(), 10) < 9) {
                    month = '0' + (parseInt(dateFacturation.getMonth(), 10) + 1);
                } else {
                    month = (parseInt(dateFacturation.getMonth(), 10) + 1);
                }
                var day;
                if (parseInt(dateFacturation.getDate(), 10) < 10) {
                    day = '0' + (parseInt(dateFacturation.getDate(), 10));
                } else {
                    day = (parseInt(dateFacturation.getDate(), 10));
                }

                $("#txtProgramDate").val(day + '/' + month + '/' + dateFacturation.getFullYear());
            } else {
                $('#txtProgramDate').datepicker({ format: 'dd/mm/yyyy' }).val("");
                oHidden.hdnDateProg = "";
                $("#txtProgramDate").prop("disabled", true);
            }
        },
        txtProgramDate_Change: function () {
            var that = this, controls = that.getControls();
            var oUserAccess = sessionTransacLTE.SessionParams.USERACCESS;
            var oHidden = Session.SessionParams.HIDDEN;
            var fechaCambiada = $("#txtProgramDate").val().split('/');
            var date = new Date(parseInt(fechaCambiada[2], 10), (parseInt(fechaCambiada[1], 10) - 1), parseInt(fechaCambiada[0], 10));
            var dateCompar = new Date(oHidden.hdnProgramDate.getFullYear(), oHidden.hdnProgramDate.getMonth(), oHidden.hdnProgramDate.getDate());//Se crea para no tomar en cuenta las horas
            if (date < dateCompar) {
                alert("La fecha de programación no debe ser menor al siguiente día de la fecha actual.", "Alerta");
                $("#txtProgramDate").val(oHidden.hdnProgramDate.getDate() + '/' + (oHidden.hdnProgramDate.getMonth() + 1) + '/' + oHidden.hdnProgramDate.getFullYear());
            } else {
                if (oUserAccess.optionPermissions.indexOf(oHidden.hdnCodSegHabiliCheckCampLte) > -1) {                   
                    oHidden.hdnDateProg = $("#txtProgramDate").val();
                } else {
                    var co = document.getElementById("hidCodOpcion").value;
                    var param =
                    {
                        "strIdSession": sessionTransacLTE.SessionParams.USERACCESS.userId,
                        'pag': '1',
                        'opcion': 'BUS',
                        'co': co,
                        'telefono': Session.SessionParams.HIDDEN.hdntelephone
                    };
                    ValidateAccess(that, controls, 'BUS', 'gConstEvtBuscarDetaLlamadaLin', '1', param, 'Fixed');
                    oHidden.hdnDateProg = $("#txtProgramDate").val();
                }
            }
        },
        StateButton: function (state) {
            if (state) {
                $("#btnActive").attr('disabled', true);
                $("#btnDesactive").attr('disabled', true);

                if (HiddenPageHtml.hdnEstadoSerSel == "D") {
                    $("#btnProActive").attr('disabled', false);
                    $("#btnProDesactive").attr('disabled', true);
                }
                else if (HiddenPageHtml.hdnEstadoSerSel == "A") {
                    $("#btnProActive").attr('disabled', true);
                    $("#btnProDesactive").attr('disabled', false);
                }
                else {
                    return;
                }
                AdRadioButton('DESACTIVAR', HiddenPageHtml.hdnEstadoSerSel);
            }
            else {
                $("#btnProActive").attr('disabled', true);
                $("#btnProDesactive").attr('disabled', true);

                if (HiddenPageHtml.hdnEstadoSerSel == "D") {
                    $("#btnActive").attr('disabled', false);
                    $("#btnDesactive").attr('disabled', true);
                }
                else if (HiddenPageHtml.hdnEstadoSerSel == "A") {
                    $("#btnActive").attr('disabled', true);
                    $("#btnDesactive").attr('disabled', false);
                }
                else {
                    return;
                }
                AdRadioButton('ACTIVAR', HiddenPageHtml.hdnEstadoSerSel);
            }
        },
        btnActive_click: function () {
            var stateService = 0;
            if (!$("#btnProActive").prop('disabled') || !$("#btnActive").prop('disabled')) {
                stateService = 1;
            } else {
                stateService = 2;
            }
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oUserAccess = sessionTransacLTE.SessionParams.USERACCESS,
                oDataCustomer = sessionTransacLTE.SessionParams.DATACUSTOMER,
                oDataLine = sessionTransacLTE.SessionParams.DATASERVICE;

            setActivateDesactive(controls, oHidden, oUserAccess, oDataCustomer, oDataLine, stateService, that);
        },
        btnDesactive_click: function () {
            var stateService = 2;
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oUserAccess = sessionTransacLTE.SessionParams.USERACCESS,
                oDataCustomer = sessionTransacLTE.SessionParams.DATACUSTOMER,
                oDataLine = sessionTransacLTE.SessionParams.DATASERVICE;
            setActivateDesactive(controls, oHidden, oUserAccess, oDataCustomer, oDataLine, stateService, that);
        },
        btnProActive_click: function () {

            var stateService = 1;
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oUserAccess = sessionTransacLTE.SessionParams.USERACCESS,
                oDataCustomer = sessionTransacLTE.SessionParams.DATACUSTOMER,
                oDataLine = sessionTransacLTE.SessionParams.DATASERVICE;

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

            if ($("#cboCACDAC").val() == "") {
                alert(oHidden.hdnSelectCacDac, "Alerta");
                return;
            }

            var flag;
            var paramter = {};
            paramter.strCoSer = oHidden.hdnCoSerSel;
            paramter.strState = HiddenPageHtml.hdnEstadoSerSel;
            paramter.strMsIsdn = oHidden.hdnTelephone;
            paramter.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            paramter.strContractId = oDataCustomer.ContractID;

            $.ajax({
                type: "POST",
                url: '/Transactions/LTE/AdditionalServices/ValidateActDesactService',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(paramter),
                success: function (data) {
                    var resp = data;
                    var arr = resp.split('|');
                    if (arr[0] == '1') {
                        flag = true;
                    }
                    else if (arr[0] == '0') {
                        if (HiddenPageHtml.hdnTipoTransaccion == 'A') {
                            alert("Ya existe programación de Activación pendiente para este Servicio.", "Alerta");
                            flag = false;
                        }
                        else if (HiddenPageHtml.hdnTipoTransaccion == 'D') {
                            alert("Ya existe programación de Desactivación pendiente para este Servicio.", "Alerta");
                            flag = false;
                        }
                    }
                    else {
                        alert("Hubo un error al validar Activacion / Desactivacion de serivicios adicionales.", "Alerta");
                        flag = false;
                    }

                    if (flag) {
                        setActivateDesactive(controls, oHidden, oUserAccess, oDataCustomer, oDataLine, stateService, that);
                    }
                },
                error: function () {
                    alert("Ocurrió un error al ejecutrar la programación.", "Alerta");
                }
            });
        },
        btnProDesactive_click: function () {
            var stateService = 2;
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oUserAccess = sessionTransacLTE.SessionParams.USERACCESS,
                oDataCustomer = sessionTransacLTE.SessionParams.DATACUSTOMER,
                oDataLine = sessionTransacLTE.SessionParams.DATASERVICE;
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
            if ($("#cboCACDAC").val() == "") {
                alert(oHidden.hdnSelectCacDac, "Alerta");
                return;
            }

            var flag;
            var paramter = {};
            paramter.strCoSer = oHidden.hdnCoSerSel;
            paramter.strState = HiddenPageHtml.hdnEstadoSerSel;
            paramter.strMsIsdn = oHidden.hdnTelephone;
            paramter.strIdSession = "5051879654"; //sessionTransacLTE.UrlParams.IDSESSION;
            paramter.strContractId = oDataCustomer.ContractID;

            $.ajax({
                type: "POST",
                url: '/Transactions/LTE/AdditionalServices/ValidateActDesactService',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(paramter),
                success: function (data) {
                    var resp = data;
                    var arr = resp.split('|');
                    if (arr[0] == '1') {
                        flag = true;
                    }
                    else if (arr[0] == '0') {
                        if (HiddenPageHtml.hdnTipoTransaccion == 'A') {
                            alert("Ya existe programación de activación pendiente para este Servicio.", "Alerta");
                            flag = false;
                        }
                        else if (HiddenPageHtml.hdnTipoTransaccion == 'D') {
                            alert("Ya existe programación de desactivación pendiente para este Servicio.", "Alerta");
                            flag = false;
                        }
                    }
                    else {
                        alert(arr[1], "Alerta");
                        flag = false;
                    }

                    if (flag) {
                        setActivateDesactive(controls, oHidden, oUserAccess, oDataCustomer, oDataLine, stateService, that);
                    }
                },
                error: function () {
                    alert("Ocurrió un error al ejecutar la programación.", "Alerta");
                }
            });
        },
        btnClose_click: function () {
            parent.window.close();
        },
        btnCheckDevices_click: function () {
            var urlCheckDevices = location.protocol + "//" + location.host + "/Transactions/LTE/CheckDevices/LteCheckDevices";
            window.open(urlCheckDevices, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1200, height=640');
        },
        btnProgramTask_click: function () {
            var urlProgramTask = location.protocol + "//" + location.host + "/Transactions/LTE/ProgramTask/LteProgramTask?coid=" + sessionTransacLTE.SessionParams.DATACUSTOMER.ContractID;
            window.open(urlProgramTask, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1200, height=640');
        },
        btnNextForm_click: function (e) {
            var result = HiddenPageHtml.hidCheckTable;
            if (result == "1") {
                navigateTabs(e);
                $('#divErrorAlertTable').hide();
            } else {
                $('#divErrorAlertTable').show();
                $('#lblErrorMessageTable').text("Debes seleccionar al menos un servicios de la lista.");
            }
        },
        btnConstancy_click: function () {
            var that = this, controls = this.getControls();
            ReadRecordSharedFile(sessionTransacLTE.SessionParams.USERACCESS.userId, sessionTransacLTE.FullPathPDF);
        },
        btnNexResumen_click: function (e) {
            navigateTabs(e);
            $('#tblResumen').html('');
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN;

            var costoPVUSel = "";
            if (HiddenPageHtml.hdnEstadoSerSel=='A')
                costoPVUSel = oHidden.hdnCargoFijoSel;
            else 
                costoPVUSel = oHidden.hdnCostoPVUSel;

            var item = "";
            item += "<tr><td colspan='9' style='color:white;background-color:#a7a8aa;'>" + oHidden.hdnDescrTypeService + "</td></tr>";
            item +=
                "<tr>" +
                       "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + oHidden.hdnDesCoSerSel + "</td>" +
                        "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + oHidden.hdnMotivoPorPaquete + "</td>" +
                        "<td style='width:40px; text-align:center; color:black;background-color:white;'>" + HiddenPageHtml.hdnEstadoSerSel + "</td>" +
                        "<td style='width:70px; text-align:center; color:black;background-color:white;'>" + oHidden.hdnValidoDesde + "</td>" +
                        "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + "" + "</td>" +
                        "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + oHidden.hdnCargoFijoSel + "</td>" +
                        "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + costoPVUSel + "</td>" +
                        "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + oHidden.hdnPeriodo + "</td>" +
                "</tr>";
            $('#tblResumen').html(item);

            //Campaña
            if ($("#chkCampiagn").is(':checked')) {
                $('#hdnCampañaResumen').show();
                $('#lblCampañaResumen').text($('#ddlCamapaign option:selected').text());
            } else {
                $('#hdnCampañaResumen').hide();
            }

            //Email
            if ($('#chkSendMail').is(':checked')) {
                $('#lblEmailResumen').text($('#txtEmail').val());
                $('#hdnEmailResumen').show();
            } else {
                $('#hdnEmailResumen').hide();
            }

            //Fecha Programada
            if ($('#chkProgram').is(':checked')) {
                $('#lblProgramarResumen').text($('#txtProgramDate').val());
                $('#hdnProgramarResumen').show();
            } else {
                $('#hdnProgramarResumen').hide();
            }

            //Punto de atencion
            if ($("#cboCACDAC").val() == "") {
                $('#hdnPuntoAtencionResumen').hide();
            } else {
                $('#hdnPuntoAtencionResumen').show();
                $('#lblPuntoAtencioResumen').text($('#cboCACDAC option:selected').text());
            }

            //Nota
            if ($('#txtNotesTask').val() == "") {
                $('#hdnNotaResumen').hide();
            } else {
                $('#hdnNotaResumen').show();
                $('#lblNotaResumen').text($('#txtNotesTask').val());
            }
        }
    };

    $.fn.LTEActiveDesactiveService = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('LTEActiveDesactiveService'),
                options = $.extend({}, $.fn.LTEActiveDesactiveService.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LTEActiveDesactiveService', data);
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

    $.fn.LTEActiveDesactiveService.defaults = {
    };

    $('#divBody').LTEActiveDesactiveService();
})(jQuery);