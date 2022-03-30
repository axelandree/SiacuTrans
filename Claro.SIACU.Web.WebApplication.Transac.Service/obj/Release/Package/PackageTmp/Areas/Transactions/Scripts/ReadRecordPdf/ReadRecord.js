function ReadRecord(IdSession , Path)
{ 
    $.app.ajax({
        type: 'GET',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: '/Transactions/CommonServices/ExistFile',
        data: { strFilePath: Path, strIdSession: IdSession },
        success: function (result) {
            if (result.Exist == false) {
                alert('No se encuentra el Archivo.', "Alerta");
            } else {
                         
                window.open('/Transactions/CommonServices/ShowRecord' + "?strFilePath=" + Path + "&strIdSession=" + IdSession, "Constancia");
            }
        },
        error: function (ex) {
            alert('No se ecuentra el Archivo.', "Alerta");
        }
    });
}

function ReadRecordSharedFile(IdSession, Path) {
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
}