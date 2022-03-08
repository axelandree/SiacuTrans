var dialogClaro = function (a) {
    alert('El componente se encuentra desfazado, cambiar por $.window.open', 'everis', function () {
        $.window.open({
            modal: a.modal,
            title: a.title,
            url: a.url,
            data: a.data,
            width: 800,
            height: 880,
            buttons: a.buttons
        });

        return;
    });
},

closeSelect = function () {
    var ele = $('div.modal.in:last'),
        id = ele.attr("id");

    var $modals = $.window.modals;

    if (!$.array.isEmptyOrNull($modals)) {
        for (var i = 0, j = $modals.length; i < j; i++) {
            var $modal = $modals[i];

            if ($modal.getId() == id) {
                $modal.close();
            }
        }
    }

    alert('closeSelect se encuentra desfazadado, para cerrar el formulario seleccionado utilizar $.window.close();');
   
},

err = function (r) {
    return '<div align="center">Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo  <br> por favor intentelo otra vez en unos minutos  ... <br><label class="control-label">Id de Transacción : </label>' + ' <span> ' + $($(r.responseText)[1]).text() + '</span> </div>';
},
error = function (d) {
    u = { c: d.id, r: d.message, f: d.click };
    var d = $('<div align="center"></div>'),e;
    u.f != null && (e = $('<button type="button" class="btn claro-btn-info btn-sm" >Volver a cargar</button>').on("click", u.f), d.html(e));
    return $('#' + u.c).html('<br/><br/><div align="center">Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo  <br> por favor intentelo otra vez en unos minutos  ... <br><label class="control-label">Id de Transacción : </label>' + ' <span> ' + $($(u.r.responseText)[1]).text() + '</span> </div><br />').append(d);
},
message = function (d) {
    return 'Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo  /n por favor intentelo otra vez en unos minutos  ... Id de Transacción : '  + $($(d.responseText)[1]).text();
},
rowSelection = function (id_, class_, group_) {
    var tb = null != id_ ? id_ : 'table.' + class_;
    tb = tb + ' tbody tr';
    $(tb).on('click', function () {
        $(this).find('td:eq(0)').find('input:radio[name=' + group_ + ']').prop('checked', true);
        $('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    });

};









