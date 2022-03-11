(function ($, undefined) {


    var Form = function ($element, options) {
        $.extend(this, $.fn.HFCListUninstallEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,

            //Tablas
            tblBajaEquipos: $('#tblBajaEquipos', $element),

            chkBajaEquipos: $('#chkBajaEquipos', $element),
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                control = this.getControls();

            control.chkBajaEquipos.addEvent(that, 'change', that.chkBajaEquipos_Click);

            that.render();
        },
        render: function () {
            var that = this,
                control = that.getControls();

            var image = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';
            var load = '<div id="dvLoadModal" class="modalInsDes" style="display:none;" align="center"><span><img src="' + image + '" width="25" height="25" />  Cargando ....</span></div>';
            $('.modal-dialog').append(load);

            that.IniLoadUninstallEquipment();
        },

        IniLoadUninstallEquipment: function () {
            var that = this,
                control = that.getControls();

            var tblBajaEquipos = control.tblBajaEquipos.dataTable({
                "scrollY": "225px",
                "scrollCollapse": true,
                "bLengthChange": true,
                "searching": true,
                "bProcessing": true,
                "bAutoWidth": false,
                "bDestroy": true,
                "bPaginate": false,
                "sPaginationType": "full_numbers",
                "oLanguage": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ Registros.",
                    "sSearch": "Buscar: ",
                    "sZeroRecords": "No existen datos.",
                    "sInfo": "Mostrando _START_ hasta _END_ de _TOTAL_ registros.",
                    "sInfoEmpty": "No hay registros para mostrar.",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "sEmptyTable": "No existen datos"
                },
                'aoColumnDefs': [
                        { "aTargets": [0], "sClass": "text-center", "bSortable": false },
                        { "aTargets": [1] },
                        { "aTargets": [2], "sClass": "text-right" },
                ]
            });

            that.GetUninstallEquipment(tblBajaEquipos);
        },

        GetUninstallEquipment: function (tabla) {
            var that = this,
                control = that.getControls(),
                param = {};

            $('#dvLoadModal').show();

            tabla.fnClearTable();

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strContratoID = HFCPOST_Session.DatosCliente.CONTRATO_ID;
            param.strCustomerID = HFCPOST_Session.DatosCliente.CUSTOMER_ID;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/UnistallInstallationOfDecoder/GetProductDown",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (data) {
                    $('#dvLoadModal').hide();
                    alert("Error al recuperar datos.", "Alerta");
                },
                success: function (response) {
                    var filas = response.data,
                        index = 0;

                    HFCPOST_Session.ListaEquiposBajaServer = filas;

                    // Falta configurar
                    $.each(filas, function (key, r) {
                        
                        // tipoServicio = 0 => Es un equipo ADICIONAL
                        if (r.tipoServicio == HFCPOST_Session.strNumeroCero) {
                            var identificador = r.codigo_material + r.codigo_sap + r.numero_serie;
                            var radio = "<input id='servEquipo" + "_" + identificador + "' name='group10' type='checkbox' />";

                            tabla.fnAddData([radio, r.TIPODECO, that.Round(parseFloat(r.CARGO_FIJO_IGV), 2).toFixed(2)]);
                            $("#servEquipo" + "_" + identificador).click(function () {
                                var isChecked = $(this)[0].checked;
                                that.setSelectedEquipment(identificador, isChecked);
                            });
                        }
                    });

                    //Recorriendo tablas y comparando si ya fue agregado
                    $("#tblEquipos tbody tr").each(function (index) {
                        var identificador1 = $($(this).find("td")[0].children).attr('id');
                        identificador1 = String(identificador1).replace('servEquipoBajaTabla_', '');
                        $("#tblBajaEquipos tbody tr").each(function (index) {
                            var identificador2 = $($(this).find("td")[0].children).attr('id');
                            identificador2 = String(identificador2).replace('servEquipo_', '');
                            if (identificador1 == identificador2) {
                                $("#servEquipo_" + identificador2).attr('disabled', 'disabled')
                                HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
                            }
                        });
                    });

                    $('#dvLoadModal').hide();
                }
            });
        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        setSelectedEquipment: function (identificador, isChecked) {
            var equiposSeleccionados = HFCPOST_Session.CodEquipAlSelec.substring(1, HFCPOST_Session.CodEquipAlSelec.length).split('|');
            var count = 0;

            if (isChecked) {
                for (var i = 0 ; i < equiposSeleccionados.length ; i++) {
                    if (identificador == equiposSeleccionados[i])
                        count++;
                }

                if (count == 0)
                    HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + identificador;
            } else {
                HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
                for (var i = 0 ; i < equiposSeleccionados.length ; i++) {
                    if (identificador != equiposSeleccionados[i])
                        HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + equiposSeleccionados[i];
                }
            }
        },

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

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

        Round: function (cantidad, decimales) {
            //donde: decimales > 2
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

        chkBajaEquipos_Click: function () {
            var that = this,
                control = that.getControls(),
                lista = HFCPOST_Session.ListaEquiposBajaServer,
                isChecked = control.chkBajaEquipos[0].checked;

            if (isChecked) {
                //$("#tblBajaEquipos tbody tr td input[type=checkbox]").prop("checked", true);
                
                var identificador1 = HFCPOST_Session.strVariableEmpty;
                var identificador2 = HFCPOST_Session.strVariableEmpty;
                $("#tblEquipos tbody tr").each(function (index) {
                    identificador1 = identificador1 + '§' + String($($(this).find("td")[0].children).attr('id')).replace('servEquipoBajaTabla_', '');
                });

                identificador1 = identificador1.substring(1, identificador1.length).split('§');
                $("#tblBajaEquipos tbody tr").each(function (index) {
                    identificador2 = String($($(this).find("td")[0].children).attr('id')).replace('servEquipo_', '');
                    if ($.inArray(identificador2, identificador1) == -1) {
                        $(this).find("td :input[type=checkbox]").prop("checked", true);
                    }
                });

                $.each(lista, function (key, value) {
                    var identificador = value.codigo_material + value.codigo_sap + value.numero_serie;
                    if ($.inArray(identificador, identificador1) == -1) {
                        HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + identificador;
                    }
                });
            }
            else {
                $("#tblBajaEquipos tbody tr td input[type=checkbox]").prop("checked", false);
                HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
            }
        },
    };

    $.fn.HFCListUninstallEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCListUninstallEquipment'),
                options = $.extend({}, $.fn.HFCListUninstallEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCListUninstallEquipment', data);
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

    $.fn.HFCListUninstallEquipment.defaults = {
    }

    $('#HFCListUninstallEquipment').HFCListUninstallEquipment();

})(jQuery);