function confirmConsultarDescartes(message, title, callbackOk, callbackCancel, callbackClose) {
    $.window.open({
        autoSize: true,
        url: '',
        title: title,
        text: message,
        modal: true,
        controlBox: false,
        maximizeBox: false,
        minimizeBox: false,
        width: '400px',
        height: '300px',
        buttons: {
            Aceptar: {
                class: 'btn transaction-button btn-sm',
                id: 'btnconfirmYesCallCut',
                click: function (sender, args) {

                        this.close();
                        if (callbackOk != null) {
                            callbackOk.call(this, false);
                        }

                   
                }
            }
        }
    });
}

function GetKeyConfig(KeyName) {

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
        url: '/Transactions/Fixed/Discard/GetKeyConfig',
        data: JSON.stringify(objFilterType),
        success: function (response) {
            if (response.data != null)
                value = response.data;
        }
    }
    );
    return value;
}

function showLoading(Mensaje) {
    $.blockUI({
        message: Mensaje,
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
}

function hideLoading() {
    $.unblockUI();
}

