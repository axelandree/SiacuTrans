(function ($, undefined) {
    var pregunta = "";
    
    var Form = function ($element, options) {
        $.extend(this, $.fn.RecordEquipmentSearchQuestionsAnswerdefaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , TblPreguntas: $('#TblPreguntas', $element)
            , lblTipoClienteModal: $('#lblTipoClienteModal', $element)
            , btnConfirmar: $('#btnConfirmar', $element)
            , btnCerrar: $('#btnCerrar', $element)
            , lblNroAfirmaciones: $('#lblNroAfirmaciones', $element)
            });
        }

        Form.prototype = {
            constructor: Form,
            init: function () {
                var that = this,
                    control = this.getControls();

                control.btnConfirmar.addEvent(that, "click", that.btnConfirmar_click);
                control.btnCerrar.addEvent(that, "click", that.btnCerrar_click);
                that.loadSessionData();
                that.render();
            },
            loadSessionData: function () {
                var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
                Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
                Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
                Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
                Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
                Session.SUREDIRECT = SessionTransac.UrlParams.SUREDIRECT;

            },
            render: function () {
            this.getDataTable();
            },
            

            getControls: function () {
                return this.m_controls || {};
            },

            setControls: function (value) {
                this.m_controls = value;
            },

           
            btnCerrar_click: function () {
                $.window.close()
            },

            getDataTable: function () {
                var that = this, controls = that.getControls();
                document.getElementById("lblTipoClienteModal").innerHTML = document.getElementById("lblCustomerType").innerHTML;

                var strTipoCliente = "";
                var strGrupoCliente = "";
                if (Session.SUREDIRECT == "PREPAID") {
                    strTipoCliente = "PREPAGO";
                    strGrupoCliente = "PARTICULAR";
                }
                else if (Session.SUREDIRECT == "POSTPAID") {
                    strTipoCliente = Session.DATACUSTOMER.CustomerType;
                    strGrupoCliente = Session.DATACUSTOMER.Modality;
                }
                params = {
                    idSession: Session.IDSESSION,
                    surRedirect: Session.SUREDIRECT,
                    tipoCliente: strTipoCliente,
                    grupoCliente: strGrupoCliente
                };

                $.app.ajax({
                    type: "POST",
                    async:false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Common/SearchQuestionsAnswerSecurityJson',
                    data: JSON.stringify(params),
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    },
                    success: function (response) {
                        var result = response.data;
                                                
                        respuesta = $.comb;
                        controls.TblPreguntas.DataTable({
                            "scrollCollapse": true,
                            "info": false,
                            "select": 'single',
                            "paging": false,
                            "searching": false,
                            "destroy": true,
                            "scrollX": true,
                            "scrollY": 300,
                            "data": result.ListQuestionsSecurity,
                            "columns": [
                                   { "data": "Description" },
                                   { "data": "IdAnswer" }
                            ],
                            "language": {
                                "lengthMenu": "Mostrar _MENU_ registros por página.",
                                "zeroRecords": "error",
                                "info": " ",
                                "infoEmpty": " ",
                                "infoFiltered": "(filtered from _MAX_ total records)",
                            },
                            "columnDefs": [
	                        {
	                            "targets": 1,
	                            "searchable": false,
	                            "orderable": false,
	                            "className": 'text-center',
	                            "render": function (data, type, full, meta) {
	                                return '<select class="form-control input-sm" style="width:90%;" value=' + full.IdQuestions + '><option value="-1" >-- Seleccionar --</option></select>';
	                            }
	                        }]
                        });

                        controls.TblPreguntas.find('tbody > tr').each(function (x, value) {
                            var comb = $(value).find('select');
                            var $id = $(comb).attr('value');
                            console.log($id);
                            var listfiltro = result.ListAnswerSecurity.filter(function (val, y) {
                                return val.IdAnswer == $id;
                            });

                            $.each(listfiltro, function (index, item) {
                                comb.append($("<option>", {

                                    value: item.IdAnswer,
                                    text: item.Description,

                                }));
                            });

                        });

                    },
                });

                
            },
            btnConfirmar_click: function () {
            var conteoAfirmaciones = 0,
                text = "";

                $('#TblPreguntas tbody tr').each(function (x, value) {
                    Session.Element = value;
                    text = $(Session.Element).find("select").find('option:selected').text();
                    var question = $(Session.Element).find('td[class="sorting_1"]').html();

                    if (text != '-- Seleccionar --' && text != 'INCORRECTO/NO') {
                        conteoAfirmaciones++;
                        pregunta = pregunta + question + " - " + text + "\n";
                        document.getElementById("txtArea").innerHTML = "";
                    }                    
                });
                if (conteoAfirmaciones < 1) {
                    alert('No ha respondido correctamente las preguntas de seguridad');
                    return false;
                }
                else {
                    document.getElementById("txtArea").innerHTML = "";
                    document.getElementById("txtArea").innerHTML = pregunta;
                    $.window.close({
                        type: 'post',
                        modal: false,
                    });
                }
               }
               
        };
        
    

        $.fn.SearchQuestionsAnswer = function () {
            var option = arguments[0],
                args = arguments,
                value,
                allowedMethods = [];

            this.each(function () {

                var $this = $(this),
                    data = $this.data('SearchQuestionsAnswer'),
                    options = $.extend({}, $.fn.SearchQuestionsAnswer.defaults,
                        $this.data(), typeof option === 'object' && option);

                if (!data) {
                    data = new Form($this, options);
                    $this.data('SearchQuestionsAnswer', data);
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

        $.fn.SearchQuestionsAnswer.defaults = {
        }

        $('#SearchQuestionsAnswerContainer').SearchQuestionsAnswer();
       
})(jQuery);