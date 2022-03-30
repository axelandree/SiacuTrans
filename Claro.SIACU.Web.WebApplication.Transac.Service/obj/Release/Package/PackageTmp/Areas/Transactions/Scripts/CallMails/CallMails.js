(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.CallMailsServiceTransaction.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , btnbuscar: $('#btnbuscar', $element)
          , txtRemitente: $('#txtRemitente', $element)
          , txtPara: $('#txtPara', $element)
          , txtCC: $('#txtCC', $element)
          , txtBCC: $('#txtBCC', $element)
          , txtAsunto: $('#txtAsunto', $element)
          , txtAdjunto: $('#txtAdjunto', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnbuscar.addEvent(that, 'click', that.btnbuscar_click);
            //controls.btnbuscar.addEvent(that, 'click', that.lblPost_CustomerID_Click);
            //controls.btnbuscar.addEvent(that, 'click', that.btnbuscar_Click);

            //controls.chkSentEmail.addEvent(that, 'change', that.ocultarCorreo);
            //that.ocultarCheckCorreo();
            //that.render();
        },
        btnbuscar_click: function () {
            var controls = this.getControls();
            var strRemitente = control.txtRemitente.val(); 
            var strPara = controls.txtPara.val();
            var strCC = controls.txtCC.val();
            var strBCC = controls.txtBCC.val();
            var strAsunto = controls.txtAsunto.val();
            var strAdjunto = controls.txtAdjunto.val();
            if (strRemitente == "" && strPara == "") {
                alert('Debe ingresar los datos del remitente y destino.', this.strTitleMessage);
                return;
            }

            this.getCallMails(strRemitente, strPara, strCC, strBCC, strAsunto, strAdjunto);
        },
        render: function () {
        },
        applicativeRoute: window.location.href,

        mostrarMensaje: function () {
            alert("Se generará envio de email", "Informativo");
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

        getCallMails: function (strRemitente, strPara, strCC, strBCC, strAsunto, strAdjunto) {
            var that = this;
            var controls = this.getControls(),
             strSendMails = this.strUrl + '/Transaction/CallMailsServiceTransaction/SendMails';

            var p_IdSession = '1';          //Session.IdSession
            var p_Transaction = '1';        //Session.Transaction
            var p_Remitente = strRemitente;
            var p_Para = strPara;
            var p_CC = strCC;
            var p_BCC = strBCC;
            var p_Asunto = strAsunto;
            var p_Adjunto = strAdjunto;

            var data = {
                strIdSession: p_IdSession,
                strTransaction: p_Transaction,
                strRemitente: p_strCustomerId,
                strPara: p_strStatus,
                strCC: p_strContract,
                strBCC: p_strType,
                strAsunto: p_Asunto,
                strAdjunto: p_Adjunto
            };
            alert(data, "Informativo");

            //$.app.ajax({
            //    async: true,
            //    type: 'POST',
            //    url: strSendMails,
            //    //data: JSON.stringify(data),
            //    data: data,
            //    //contentType: "application/json; charset=utf-8",
            //    //dataType: 'json',
            //    //cache: true,
            //    //data: JSON.stringify(strIdSession, Transaction, strCustomerId, strContract, strStatus, strNumberTickler, strType),
            //    //data:JSON.stringify(PruebaPostpaidPenals),

            //    success: function (result) {
            //        that.createTableClientData(result);
            //    },
            //    error: function (ex) {
            //        //controls.divVistaParcialNombres.find('table tbody').html('');
            //        //error({ id: 'dvMensajeSeleccion', message: ex });
            //    }
            //});
        },
        createTableClientData: function (result) {
            this.getControls().tblDetailVisualizePenal.DataTable({
                //$('#tblDetailVisualizePenal').DataTable({
                //"order": [[7, "asc"], [0, 'asc']],
                "scrollY": "100px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "select": "single",
                "destroy": true,
                "searching": false,
                "data": result.data.listPenalsPast,
                //"data": listPenalsPast,
                "columns": [
                   //{ "data": null },
                   { "data": "Code" },
                   { "data": "Status" },
                   { "data": "Priority" },
                   { "data": "Description" },
                   { "data": "UserTracing" },
                   { "data": "DateTracing" },
                   { "data": "ActionTracing" },
                   { "data": "NumberTickler" }
                ],
                //"columnDefs": [
                //    {
                //        targets: 0,
                //        orderable: false,
                //        className: 'select-radio',
                //        defaultContent: ""
                //    }
                //],
                drawCallback: function (settings) {
                    $(this).DataTable().row(0).select();
                },
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }

            });
        },
        strTitleMessage: "Alert",
        strUrl: window.location.protocol + '//' + window.location.host

    };
    $.fn.CallMailsServiceTransaction = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getControls'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('CallMailsServiceTransaction'),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CallMailsServiceTransaction', data);
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

    $.fn.CallMailsServiceTransaction.defaults = {
    }

    // $('#contenedor-customerBusinessNames', $('.modal:last')).form();
    $('#CallMailsServiceTransaction-Contenedor').CallMailsServiceTransaction();
})(jQuery);