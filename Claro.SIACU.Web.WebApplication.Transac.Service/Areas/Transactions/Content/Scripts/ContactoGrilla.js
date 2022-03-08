/*@cc_on@*/
//"use strict";

// Avoid `console` errors in browsers that lack a console.
(function () {
    var method;
    var noop = function () { };
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    var console = (window.console = window.console || {});

    while (length--) {
        method = methods[length];

        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }
}());

if (!Object.create) {
    Object.create = function (proto, props) {
        if (typeof props !== "undefined") {
            throw "The multiple-argument version of Object.create is not provided by this browser and cannot be shimmed.";
        }
        function ctor() { }
        ctor.prototype = proto;
        return new ctor();
    };
}
function createDelegate(instance, method, args) {
    return function () {
        return method.apply(instance, arguments);
    }
}

function ContactoGrilla(elementId, tiposDatoOpcional, tiposContacto, tiposDocumento) {
    this._elementId = elementId;

    this._arrTipoContacto = tiposContacto;
    this._arrTipoDatoOpcional = tiposDatoOpcional;
    this._arrTipoDocumento = tiposDocumento;
    this._arrContacto = null;
    this._arrFila = null;
    this._permiteMultipleEdicion = false;
    this._filaNueva = null;

    this._columnas = null;
    this._contactos = null;

    this._comandoEjecutadoManejador = null;

    var $table = $('#' + this._elementId);

    $('#' + elementId + ' > thead').find('tr').remove();
    $('#' + elementId + ' > thead').remove();
    $('#' + elementId + ' > tbody').find('tr').remove();
    $('#' + elementId + ' > tbody').remove();

    $('#' + elementId).append('<thead><tr class="claro-bg-primary TablaTitulos"><th>Tipo de Contacto</th></tr></thead>');
    $('#' + elementId).append('<tbody></tbody>');
}
ContactoGrilla.prototype = {
    contactos: function (valor) {
        if (arguments.length === 0) {
            return this._contactos;
        }
        this._contactos = arguments[0];
    },
    tiposContacto: function () {
        return this._arrTipoContacto;
    },
    tiposDato: function () {
        return this._arrTipoDato;
    },
    tiposDatoOpcional: function () {
        return this._arrTipoDatoOpcional;
    },
    tiposDocumento: function () {
        return this._arrTipoDocumento;
    },
    columnas: function () {
        if (this._columnas === null) {
            this._columnas = new ContactoGrillaColumnaColeccion(this);
        }
        return this._columnas;
    },
    filas: function () {
        if (this._arrFila === null) {
            this._arrFila = new ContactoGrillaFilaColeccion(this);
        }

        return this._arrFila;
    },
    permiteMultipleEdicion: function (valor) {
        if (arguments.length === 0) {
            return this._permiteMultipleEdicion;
        }
        this._permiteMultipleEdicion = arguments[0];
    },

    f_validarMinimoRegistrosRequerido: function (posicion) {
        var intTipoContactoSelectedIndex = $($('#tbContactPost > tbody > tr')[posicion]).find('td:first > select > option:selected').index()
            , objTipoContacto = this.tiposContacto()[intTipoContactoSelectedIndex]
            , intCantidadRegistrosMinimo = objTipoContacto['TCCTI_MINREGISTROS']
            , intCantidadRegistros = 0;

        $('#tbContactPost > tbody > tr').each(function (index, element) {
            if (objTipoContacto['TCCTN_CODIGO'] === parseFloat($(element).find('td:first > select > option:selected').val())) {
                intCantidadRegistros++;
            }
        });

        return { 'success': (intCantidadRegistros > intCantidadRegistrosMinimo), 'nombre': objTipoContacto['TCCTV_NOMBRE'], 'minimo': intCantidadRegistrosMinimo };
    },
    f_validarMaximoRegistrosRequerido: function (posicion) {
        var arrFila = $("#tbContactPost > tbody > tr");

        if (arrFila.length > 0) {
            var intTipoContactoSelectedIndex = $(arrFila[posicion]).find('td:first > select > option:selected').index()

            if (intTipoContactoSelectedIndex === null) {
                var objTipoContacto = this.tiposContacto()[intTipoContactoSelectedIndex]
                , intCantidadRegistrosMaximo = objTipoContacto['TCCTI_MAXREGISTROS']
                , intCantidadRegistros = 0;

                $('#tbContactPost > tbody > tr').each(function (index, element) {
                    if (objTipoContacto['TCCTN_CODIGO'] == $(element).find('td:first > select > option:selected').val()) {
                        intCantidadRegistros++;
                    }
                });

                return { 'success': (intCantidadRegistros <= intCantidadRegistrosMaximo), 'nombre': objTipoContacto['TCCTV_NOMBRE'], 'maximo': intCantidadRegistrosMaximo };
            }
            else
                return null;
        }

        return null;
    },

    agregarContacto: function (contacto) {
        this.filas().agregar(contacto);

        var maximoRegistrosRequeridos = this.f_validarMaximoRegistrosRequerido(this.filas().conteo() - 1);

        if (maximoRegistrosRequeridos !== null && !maximoRegistrosRequeridos.success) {
            if (!confirm('Cantidad maxima de registros para\rel tipo contacto: ' + maximoRegistrosRequeridos.nombre + ' es: ' + maximoRegistrosRequeridos.maximo)) {
                this.filas().trae(this.filas().conteo() - 1).eliminar();
            }
        }

        this._redimencionarCabeceraFlotante();
    },
    agregarContactos: function (contactos) {
        for (var i = 0, l = contactos.length; i < l; i++) {
            this.agregarContacto(contactos[i]);
        }
    },
    AdicionarContacto: function ()
    {
		var strRegistro='';
		var strFila='';
		var Inicio=-1;
		var intTotalRegistros=this.filas().conteo();
		vintContTemp=vintAgregar;
		
		if(vintContBD!=0)
		{
			if(vintContBD==vintContTemp)
			{
				Inicio=vintContBD;
			}else
			{
				if(vintContBD==intTotalRegistros)
				{
					Inicio=intTotalRegistros;
				}else
				{
					if(vintContBD+vintContTemp==intTotalRegistros)
					{
						Inicio=intTotalRegistros-vintContTemp;
					}else{
						Inicio=intTotalRegistros-1;
					}
				}	
			}
		}else
		{
			Inicio=0;
		}
		
		if(Inicio<0)
		{Inicio=Inicio*-1;}
		for (var intFila = Inicio; intFila < this.filas().conteo(); intFila++) {
		    var objContacto = this.filas().trae(intFila)._contacto;
		  
		   
			var arrAdicional = objContacto['CamposAdicionales'];
			if((objContacto !=null) && (objContacto.CamposAdicionales!=null))
			{
				strFila='|';
				strRegistro=strRegistro+strFila;
				for(var arrItem=0;arrItem<arrAdicional.length;arrItem++)
				{
					var objAdicional = arrAdicional[arrItem];
					if(objAdicional['TCCVV_VALOR'] != undefined)
					{ 
						strRegistro = strRegistro + objAdicional['TCXCN_CODIGO']+ ',' + objAdicional['TCCVV_VALOR'] + ' ;';
					}
				}
				strRegistro = strRegistro + '||';
			}
		}
		document.getElementById('hidNuevo').value=document.getElementById('hidNuevo').value+strRegistro;
    },
    crearContacto: function () {
        this.agregarContacto({ 'CamposAdicionales': [{ 'TCCCN_CODIGO': 4, 'TCCVV_VALOR': '01'}] });

        this._filaNueva = this.filas().trae(this.filas().conteo() - 1);
        this._redimencionarCabeceraFlotante();
    },

    comandoEjecutado: function (manejador) {
        this._comandoEjecutadoManejador = manejador;
    },
    _elevarComandoEjecutado: function (comando, argumento) {
        if (this._comandoEjecutadoManejador !== null) {
            this._comandoEjecutadoManejador(this, { 'comando': comando, 'argumento': argumento });
        }

        this._redimencionarCabeceraFlotante();
    },

    _redimencionarCabeceraFlotante: function () {
        var $table = $('#' + this._elementId);
    },

    llenarVistaDeEstado: function () {
		//Obteniendo los Nuevos
		grdContactos.AdicionarContacto();
		//Obteniendo los Editar
		var arrAdicional = [];
		var valor=[];
		var strRegistro='';
		var strTramaFinal='';
		if(($('#hidId').val()!='') || ($('#hidId').val()!=null) || ($('#hidId').val()!='undefined'))
		{
			for (var intFila = 0; intFila < this.filas().conteo(); intFila++) {
				var objContacto = this.filas().trae(intFila)._contacto;
				arrAdicional = objContacto['CamposAdicionales'];
				if((objContacto !=null) && (objContacto.CamposAdicionales!=null))
				{
					valor=$('#hidId').val().split(",");
					for(var val=0;val<valor.length;val++)
					{
						if(objContacto['CSCTN_CODIGO']===parseInt(valor[val]))
						{
							strRegistro=strRegistro+objContacto['CSCTN_CODIGO']+'|';
							for(var arrItem=0;arrItem<arrAdicional.length;arrItem++)
							{
								var objAdicional = arrAdicional[arrItem];
								if(objAdicional['TCCVV_VALOR'] != undefined)
								{ 
	 								strRegistro = strRegistro + objAdicional['TCXCN_CODIGO']+ ',' + objAdicional['TCCVV_VALOR'] + ' ;';
								}
							}
							strRegistro = strRegistro + '||';		
						}
					}
				}
			}
			var arrEditar=[];
			var arrEditarNuevo=[];
			arrEditar=$('#hidEditar').val().split("||");
			for(var intAnt=0;intAnt<arrEditar.length;intAnt++)
			{
				if(arrEditar[intAnt]!="")
				{
					arrEditarNuevo=strRegistro.split("||");
					for(var intNue=0;intNue<arrEditarNuevo.length;intNue++)
					{
						if(arrEditarNuevo[intNue]!="")
						{
							if(arrEditar[intAnt].split("|")[0].indexOf(arrEditarNuevo[intNue].split("|")[0])>-1)
							{
								if(arrEditar[intAnt].split("|")[1].indexOf(arrEditarNuevo[intNue].split("|")[1])==-1)
								{
									strTramaFinal=strTramaFinal+arrEditarNuevo[intNue]+'||';
								}
							}	
						}	
					}
				}
			}
			
		}
		var vstrTrama=(strTramaFinal+$('#hidNuevo').val());
		var vstrCuenta = $('#hidNroCuenta').val();
		if((vstrTrama=='') || (vstrTrama==null) || (vstrTrama=='undefined'))
		{
		    $('#SavebtnPostPaid').attr('disabled', false);
		}else
		{
			var vstrTramaRegistro=vstrTrama.substring(0,vstrTrama.length-2).toUpperCase();
			var strUrl = window.location.href;
			var urlAdd = strUrl + '/Dashboard/PostpaidDataCustomer/ContactSave';
			$.app.ajax({
			    type: 'POST',
			    url: urlAdd,
			    data: {
			        strIdSession: Session.IDSESSION,
			        strSave: vstrTramaRegistro,
			        strCuenta: vstrCuenta,
			        strtelephone: $('#hidTelefono').val(),
			        strDelete: ''
			    },
			    success: function (result) {
			       
			        alert(result.data, 'Alerta', function () {
			            $('#SavebtnPostPaid').attr('disabled', false);
			            $('#PostpaidContact').FormPostpaidContact('fn_CargarInfoContacto');
			        }); 
			    },
			    error: function (ex) {
			        error({
			            id: 'PostpaidContact',
			            message: ex
			        });
			    }

			});
		}
    },
    validarContactos: function () {
        for (var intContactoPosicion = 0, intContactoCantidad = this.filas().conteo() ; intContactoPosicion < intContactoCantidad; intContactoPosicion++) {
            var objFila = this.filas().trae(intContactoPosicion)
                , strTipoDocumento = null
                , strNroDocumento = null;

            for (var intCeldaPosicion = 0, intCeldaCantidad = objFila.celdas().conteo(); intCeldaPosicion < intCeldaCantidad; intCeldaPosicion++) {
                var objCelda = objFila.celdas().trae(intCeldaPosicion)
                    , arrAdicional = objCelda.fila()._contacto['CamposAdicionales']
                    , valor = null;

                for (var intAdicionalPosicion = 0, intAdicionalCantidad = arrAdicional.length; intAdicionalPosicion < intAdicionalCantidad; intAdicionalPosicion++) {
                    var objAdicional = arrAdicional[intAdicionalPosicion];

                    if (parseInt(objAdicional['TCCCN_CODIGO']) === objCelda.codigo()) {
                        valor = objAdicional['TCCVV_VALOR'];

                        if (parseInt(objAdicional['TCCCN_CODIGO']) === 4 && strTipoDocumento === null) {
                            strTipoDocumento = valor;
                        } else if (parseInt(objAdicional['TCCCN_CODIGO']) === 5 && strNroDocumento === null) {
                            strNroDocumento = valor;
                        } else {
                            break;
                        }
                    }
                }

                if (objCelda.esObligatorio()) {
                    if (typeof (valor) === 'undefined' || valor === null || $.trim(valor).length === 0) {
                        alert('El campo "' + objCelda.columna().texto() + '"\res obligatorio para el contacto en la fila ' + (intContactoPosicion + 1));
                        return false;
                    }
                }

                if (objCelda._columna._tipoContactoCampo['TCCCN_TIPODATO'] == 10027 && $.trim(valor).length > 0) {
                    var esValido = (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor));

                    if (!esValido) {
                        alert('El campo "' + objCelda.columna().texto() + '"\r de la fila ' + (intContactoPosicion + 1) + ' de Datos de Contacto, no tiene el formato correcto.');
                        return false;
                    }
                }
            }

            if (strTipoDocumento !== null && strNroDocumento !== null) {
                var blnNroDocumentoValido = true;
                var VConstCodTipoDocumentoDNI = $('#hidDocumentTypeDNI').val();
                var VConstCodTipoDocumentoRUC = $('#hidDocumentTypeRUC').val();
                var VConstCodTipoDocumentoPAS = $('#hidDocumentTypePAS').val();
                var VConstCodTipoDocumentoCEX = $('#hidDocumentTypeCEX').val();
                var VConstCodTipoDocumentoCIP = $('#hidDocumentTypeCIP').val();
                var VConstCodTipoDocumentoCFA = $('#hidDocumentTypeCFA').val();

                if (strTipoDocumento === VConstCodTipoDocumentoDNI) {
                    if ($.trim(strNroDocumento).length !== 8) {
                        alert('Nro de DNI de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                } else if (strTipoDocumento === VConstCodTipoDocumentoRUC) {
                    if ($.trim(strNroDocumento).length !== 11) {
                        alert('Nro de RUC de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                } else if (strTipoDocumento === VConstCodTipoDocumentoPAS) {
                    if ($.trim(strNroDocumento).length < 10) {
                        alert('Nro de Pasaporte de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                } else if (strTipoDocumento === VConstCodTipoDocumentoCEX) {
                    if (($.trim(strNroDocumento).length < 4 || $.trim(strNroDocumento).length > 9)
                        || $.trim(strNroDocumento).length === 7 && $.trim(strNroDocumento).substr(0, 1) !== 'N') {

                        alert('Nro de CE de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                } else if (strTipoDocumento === VConstCodTipoDocumentoCIP) {
                    if ($.trim(strNroDocumento).length !== 6) {
                        alert('Nro de CIP de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                } else if (strTipoDocumento === VConstCodTipoDocumentoCFA) {
                    if ($.trim(strNroDocumento).length !== 8) {
                        alert('Nro de FA de la contacto de la fila ' + (intContactoPosicion + 1) + ' es no v\xe1lido');
                        blnNroDocumentoValido = false;
                    }
                }

                if (!blnNroDocumentoValido) {
                    return false;
                }
            }

            if (intContactoPosicion > 0) {
                var objContacto = objFila._contacto
                    , intTipoContacto = objContacto['TCCTN_CODIGO']
                    , arrAdicional = objContacto['CamposAdicionales'];

                strTipoDocumento = null;
                strNroDocumento = null;

                for (var intAdicionalPosicion = 0, intAdicionalCantidad = arrAdicional.length; intAdicionalPosicion < intAdicionalCantidad; intAdicionalPosicion++) {
                    var objAdicional = arrAdicional[intAdicionalPosicion];

                    if (objAdicional['TCCCN_CODIGO'] === 4 && strTipoDocumento === null) {
                        strTipoDocumento = objAdicional['TCCVV_VALOR'];
                    } else if (objAdicional['TCCCN_CODIGO'] === 5 && strNroDocumento === null) {
                        strNroDocumento = objAdicional['TCCVV_VALOR'];
                    } else if (strTipoDocumento !== null && strNroDocumento !== null) {
                        break;
                    }
                }

                for (var intContactoPosicion1 = 0; intContactoPosicion1 < intContactoPosicion; intContactoPosicion1++) {

                    var objContacto1 = this.filas().trae(intContactoPosicion1)._contacto
                        , intTipoContacto1 = objContacto1['TCCTN_CODIGO']
                        , arrAdicional1 = objContacto1['CamposAdicionales']
                        , strTipoDocumento1 = null
                        , strNroDocumento1 = null;

                    for (var intAdicionalPosicion1 = 0, intAdicionalCantidad1 = arrAdicional1.length; intAdicionalPosicion1 < intAdicionalCantidad1; intAdicionalPosicion1++) {
                        var objAdicional1 = arrAdicional1[intAdicionalPosicion1];

                        if (objAdicional1['TCCCN_CODIGO'] === 4 && strTipoDocumento1 === null) {
                            strTipoDocumento1 = objAdicional1['TCCVV_VALOR'];
                        } else if (objAdicional1['TCCCN_CODIGO'] === 5 && strNroDocumento1 === null) {
                            strNroDocumento1 = objAdicional1['TCCVV_VALOR'];
                        } else if (strTipoDocumento1 !== null && strNroDocumento1 !== null) {
                            if (intTipoContacto === intTipoContacto1
                                && strTipoDocumento === strTipoDocumento1
                                && strNroDocumento === strNroDocumento1) {

                                var strTipoContacto = null;

                                for (var intTipoContactoPosicion = 0, intTipoContactoCantidad = this.tiposContacto().length; intTipoContactoPosicion < intTipoContactoCantidad; intTipoContactoPosicion++) {
                                    var objTipoContacto = this.tiposContacto()[intTipoContactoPosicion];

                                    if (objTipoContacto['TCCTN_CODIGO'] === intTipoContacto) {
                                        strTipoContacto = objTipoContacto['TCCTV_NOMBRE'];
                                        break;
                                    }
                                }
                                for (var intTipoDocumentoPosicion = 0, intTipoDocumentoCantidad = this.tiposDocumento().length; intTipoDocumentoPosicion < intTipoDocumentoCantidad; intTipoDocumentoPosicion++) {
                                    var objTipoDocumento = this.tiposDocumento()[intTipoDocumentoPosicion];

                                    if (objTipoDocumento['Codigo'] === strTipoDocumento) {
                                        strTipoDocumento = objTipoDocumento['Descripcion'];
                                        break;
                                    }
                                }

                                alert('Contacto duplicado'
                                        + '\rLos datos del contacto de la fila ' + (intContactoPosicion + 1) + ' ya existe en el contacto de la fila: ' + (intContactoPosicion1 + 1) + '\r'
                                        + '\rTipo de Contacto: ' + (strTipoContacto || intTipoContacto)
                                        + '\rTipo Documento: ' + strTipoDocumento
                                        + '\rNro. Documento: ' + strNroDocumento);

                                return false;
                            }

                            break;
                        }
                    }
                }
            }
        }
       for (var intTipoContactoPosicion = 0, intTipoContactoCantidad = this.tiposContacto().length; intTipoContactoPosicion < intTipoContactoCantidad; intTipoContactoPosicion++) {
            var objTipoContacto = this.tiposContacto()[intTipoContactoPosicion]
                , strNombre = objTipoContacto['TCCTV_NOMBRE']
                , intCantidad = 0
                , intMinimo = objTipoContacto['TCCTI_MINREGISTROS']
                , intMaximo = objTipoContacto['TCCTI_MAXREGISTROS']
                , blnObligatorio = objTipoContacto['TCCTC_OBLIGATORIO'];
			if(blnObligatorio==true)
			{
				for (var intContactoPosicion = 0, intContactoCantidad = this.filas().conteo(); intContactoPosicion < intContactoCantidad; intContactoPosicion++) {
					var objContacto = this.filas().trae(intContactoPosicion)._contacto;

					if (objTipoContacto['TCCTN_CODIGO'] === objContacto['TCCTN_CODIGO']) {
						intCantidad++;
					}
				}

				if (intCantidad < intMinimo) {
				    alert('Cantidad de registros m\u00EDnima para\rel tipo contacto: ' + strNombre + ' es: ' + intMinimo);
					return false;
				} else if (intCantidad > intMaximo) {
				    alert('Cantidad maxima de registros para\rel tipo contacto: ' + strNombre + ' es: ' + intMaximo);
					return false;
				}
            }
        }

        return true;
    }
}

/**********************************************************************************************************************************/
function ContactoGrillaColumnaColeccion(grilla) {
    this._columnas = [];
    this._grilla = grilla;
}
ContactoGrillaColumnaColeccion.prototype = {
    agregar: function (columna) {
        var columna = new ContactoGrillaColumna(columna);

        columna._grillaInterna(this._grilla);

        this._columnas.push(columna);

        columna.render();
    },
    remover: function (indice) {
        this._columnas.splice(indice, 1);
    },
    conteo: function () {
        return this._columnas.length;
    },
    trae: function (posicion) {
        return this._columnas[posicion];
    }
}

function ContactoGrillaColumna(tipoContactoCampo) {
    this._tipoContactoCampo = tipoContactoCampo;
    this._grilla = null;
}
ContactoGrillaColumna.prototype = {
    _grillaInterna: function (valor) {
        if (arguments.length === 0) {
            return this._grilla;
        }

        this._grilla = arguments[0];

        this._grillaInterna = function () {
            return this._grilla;
        }
    },
    campo: function () {
        return this._tipoContactoCampo['TCCCN_CODIGO'];
    },
    texto: function () {
        return this._tipoContactoCampo['TCCCV_NOMBRE'];
    },
    longitud: function (value) {
        return this._tipoContactoCampo['TCCCN_LONGITUD'];
    },
    tipoDato: function (value) {
        return this._tipoContactoCampo['TCCCN_TIPODATO']; //'10007';
    },
    tipoDatoOpcional: function (value) {
        return this._tipoContactoCampo['TCCCN_TIPODATOOPCIONAL'];
    },
    grilla: function () {
        return this._grillaInterna();
    },
    render: function () {
        $('#' + this._grilla._elementId + ' thead tr').append('<th id="cell_' + this.campo() + '">' + this.texto() + '</th>');
    }
}

/**********************************************************************************************************************************/
function ContactoGrillaFilaColeccion(grilla) {
    this._filas = [];
    this._grilla = grilla;
}
ContactoGrillaFilaColeccion.prototype = {
    agregar: function (contacto) {
        var fila = new ContactoGrillaFila();

        fila._grillaInterna(this._grilla);
        fila._contacto = contacto;

        this._filas.push(fila);

        fila.render();
    },
    remover: function (indice) {
        this._filas.splice(indice, 1);
    },
    conteo: function () {
        return this._filas.length;
    },
    trae: function (posicion) {
        return this._filas[posicion];
    }
}

/**********************************************************************************************************************************/
function ContactoGrillaFila() {

}
ContactoGrillaFila.prototype = {
    //_id: null,
    _posicionUltimoControlEnlzado: -1,
    _arrCeldas: null,
    _grilla: null,
    _contacto: { 'CamposAdicionales': [{ 'TCCCN_CODIGO': 4, 'TCCVV_VALOR': '01'}] },
    _element: null,
    _enEdicion: true,
    _grillaInterna: function (valor) {
        if (arguments.length === 0) {
            return arguments[0];
        }

        this._grilla = arguments[0];

        this._grillaInterna = function () {
            return this._grilla;
        }
    },
    dataIndex: function () {
        $('#' + this.grilla()._elementId).find()
    },
    celdas: function () {
        if (this._arrCeldas === null) {
            this._arrCeldas = new ContactoGrillaCeldaColeccion(this);
        }

        return this._arrCeldas;
    },
    id: function () {
        return (this._id = (this._id || 'tr_' + generateUUID()));
    },
    grilla: function () {
        return this._grillaInterna();
    },
    enEdicion: function () {
        return this._enEdicion;
    },
    _ensureElement: function () {
        var tbody = $('#' + this.grilla()._elementId).find('tbody')
                , id = this.id()
                , trTipoContacto = tbody.find('tr#' + id);

        if (trTipoContacto.length === 0) {
            tbody.append('<tr id="' + id + '"></tr>');
            trTipoContacto = tbody.find('tr#' + id);
        } else {
            $.each(trTipoContacto.children(), function () {
                var tdCelda = $(this);

                $.each(tdCelda.children(), function () {
                    var elemento = $(this);

                    elemento.unbind();
                    elemento.remove();
                });

                tdCelda.remove();
            });
        }
    },
    render: function () {
        this._ensureElement();

        var trTipoContacto = $('tr#' + this.id());

        trTipoContacto.append('<td></td>');

        var tdTipoContacto = trTipoContacto.find('td:first')
            , selTipoContacto = null;

        if (this.grilla().permiteMultipleEdicion() || this.enEdicion()) {
            tdTipoContacto.append('<select class="form-control-table "></select>');
            selTipoContacto = tdTipoContacto.find('select');
            if($('#hidAccion').val()=='N')
            {
				tdTipoContacto.find('select').attr('disabled',false);	
            }else if($('#hidAccion').val()==''){
                tdTipoContacto.find('select').attr('disabled', 'disabled');
                //cm
                tdTipoContacto.find('select').addClass('bg-success');
                
			}
            selTipoContacto.bind('change', createDelegate(this, this.selTipoContacto_PosicionSeleccionada));
        }

        this.celdas().limpiar();
        for (var i = 0, l = this.grilla().columnas().conteo(); i < l; i++) {
            var columna = this.grilla().columnas().trae(i);
            var celda = new ContactoGrillaCelda(columna);

            this.celdas().agregar(celda);
        }

        trTipoContacto.append("<td align='center'></td>");
		var opEdicion=$('#hidPerfil').val();
		trTipoContacto.find('td:last').append('<input id="chkCopiar" type="checkbox" />');
        trTipoContacto.find('td:last > input').bind('click', createDelegate(this, this.btnContactoCopiar_Click));
        trTipoContacto.append('<td></td>');

		var objFilaEd=this.id()+'U';
		var objFilaEl=this.id()+'E';

        if ((this.grilla().permiteMultipleEdicion() || this._enEdicion)) {
			
			switch(opEdicion)
			{
				case 'M':
				    trTipoContacto.find('td:last').append('<span   id=' + objFilaEd + ' class="glyphicon glyphicon-pencil" ></span>');
				    trTipoContacto.find('td:last > span#' + objFilaEd).on('click', createDelegate(this, this.btnContactoEditar_Click));
					break;
				case 'NM':
				    trTipoContacto.find('td:last').append('<span   id=' + objFilaEd + ' class="glyphicon glyphicon-pencil" ></span>');
				    trTipoContacto.find('td:last > span#' + objFilaEd).on('click', createDelegate(this, this.btnContactoEditar_Click));
					trTipoContacto.find('td:last').append('&nbsp;<span  id=' + objFilaEl + ' class="glyphicon glyphicon-remove" ></span>');
					trTipoContacto.find('td:last > span#' + objFilaEl).on('click', createDelegate(this, this.btnContactoEliminar_Click));
					break;					
			}
        }

        var intTipoContactoCodigo = (this._contacto.hasOwnProperty('TCCTN_CODIGO') ? this._contacto['TCCTN_CODIGO'] : 2)
            , tiposContacto = this.grilla().tiposContacto()
            , tipoContacto = null;

        if (selTipoContacto !== null) {
            for (i = 0, l = tiposContacto.length; i < l; i++) {
                tipoContacto = tiposContacto[i];
                selTipoContacto.append('<option value="' + tipoContacto['TCCTN_CODIGO'] + '">' + tipoContacto['TCCTV_NOMBRE'] + '</option>');
            }

            //nuevo begin
            if (this._contacto.hasOwnProperty('TCCTN_CODIGO')) {
                selTipoContacto.val(intTipoContactoCodigo);
            } else {
                var intTipoContactoDefecto;
                var boolContactoDespacho = false;
                for (i = 0, l = tiposContacto.length; i < l; i++) {
                    tipoContacto = tiposContacto[i];
                    if (i == 0) {
                        intTipoContactoDefecto = tipoContacto['TCCTN_CODIGO'];
                    }
                    if (tipoContacto['TCCTN_CODIGO'] == '2') {
                        boolContactoDespacho = true;
                    }
                }
                if (boolContactoDespacho) {
                    selTipoContacto.val(intTipoContactoCodigo);
                }
                else {
                    selTipoContacto.val(intTipoContactoDefecto);
                }
            }
            //nuevo end


            selTipoContacto.trigger('change');
        } else {
            for (i = 0, l = tiposContacto.length; i < l; i++) {
                tipoContacto = tiposContacto[i];

                if (intTipoContactoCodigo === tipoContacto['TCCTN_CODIGO']) {
                    tdTipoContacto.html(tipoContacto['TCCTV_NOMBRE']);
                    break;
                }
            }
            var strUrl = window.location.href;
            var urlAdd = strUrl + '/Dashboard/PostpaidDataCustomer/GetContactTypeByFiel';
            var $this = this;
            $.app.ajax({
                type: 'POST',
                url: urlAdd,
                data: {
                    strIdSession: Session.IDSESSION,
                    intCode: tipoContactoCodigo
                },
                success: function (result) {
                   
                    $this.tipoContactoCambiarCallback(result);
                }
            });
        }
    },
    selTipoContacto_PosicionSeleccionada: function (sender) {
    
        var intTipoContactoCodigo = parseFloat($(sender.target).val());

        this._contacto['TCCTN_CODIGO'] = intTipoContactoCodigo;

        this.tipoContactoCambiar();
    },
    btnContactoCopiar_Click: function (sender) {
        this.grilla()._elevarComandoEjecutado('contacto_copiando', this);
		$('#hidAccion').value='E';
		var trFilaActual = $('tr#' + this.id());
		
        for (var i = 0, l = this.grilla().filas().conteo() ; i < l; i++) {
            if (this.grilla().filas().trae(i).id() === this.id()) {

                if (i === 0){
                    alert('No existe contacto anterior para copiar');
                    $(sender.target).attr('checked', false);
                } else {
					/*Contacto actual:CSCTN_CODIGO*/
                    var intCodigoRegistro=this.grilla().filas().trae(i)._contacto.CSCTN_CODIGO;
					this._contacto = $.extend(true, {}, this.grilla().filas().trae(i - 1)._contacto);
                    this._contacto.CSCTN_CODIGO=intCodigoRegistro;
                    this.render();
                }

                break;
            }
        }

        this.grilla()._elevarComandoEjecutado('contacto_copiado', this);
    },
    btnContactoEditar_Click: function (sender) {
		var trFilaActual = $('tr#' + this.id());
		var intFilaActual=-1;
		document.getElementById('hidAccion').value='E';
		$('#tbContactPost tbody tr').each(function(i) {
			var $this = $(this);
			if(trFilaActual.index()===i)
			{
				$this.find('td').each(function(i) {
				    var $Celda = $(this);
				    $Celda.find('input').removeClass('bg-success');
				    $Celda.find('input').removeAttr("readonly");
				    $Celda.find('input').addClass('text-uppercase');
				    $Celda.find('select').attr('disabled', false);
				    //cm
				    $Celda.find('select').removeClass('bg-success');
				    
				}); 
			} 
		});
		this.contactoEditar(intFilaActual);
    },
    btnContactoEliminar_Click: function (sender) {

		this.contactoEliminar();
    },
    tipoContactoCambiar: function () {
        var tipoContactoCodigo = this._contacto['TCCTN_CODIGO'];
        var $this = this;
        var strUrl = window.location.href;
        var urlAdd = strUrl + '/Dashboard/PostpaidDataCustomer/GetContactTypeByFiel';
        $.app.ajax({
            type: 'POST',
            url: urlAdd,
            data: {
                strIdSession: Session.IDSESSION,
                intCode: tipoContactoCodigo
            },
            success: function (result) {
               
                $this.tipoContactoCambiarCallback(result);
            }
             });
    },
    tipoContactoCambiarCallback: function (sender) {
   
        var arrTiposContactoPorCampo = sender.value
            , intPosicionUltimoControlEnlazado = -1
            , arrCamposAdicionalesCopia = $.extend(true, [], this._contacto['CamposAdicionales']);

        this._contacto['CamposAdicionales'] = [];

        for (var i = 0, l = this.celdas().conteo(); i < l; i++) {
            var celda = this.celdas().trae(i)
                , esObligatorio = false
                , blnEnlazar = false;
			var intTotal=parseInt(arrTiposContactoPorCampo.length);
            for (var intTipoContactoPorCampoPosicion = 0, intTipoContactoPorCampoCantidad = intTotal; intTipoContactoPorCampoPosicion < intTipoContactoPorCampoCantidad; intTipoContactoPorCampoPosicion++) {
                var objTipoContactoPorCampo = arrTiposContactoPorCampo[intTipoContactoPorCampoPosicion];

                if (objTipoContactoPorCampo['TCCCN_CODIGO'] == celda.codigo()) {
                    intPosicionUltimoControlEnlazado = i;

                    esObligatorio = (objTipoContactoPorCampo['TCXCC_OBLIGATORIO'] === '1');
                    blnEnlazar = true;

                    var blnFounded = false;
                    for (var intCampoAdicionalPosicion = 0, intCampoAdicionalCantidad = arrCamposAdicionalesCopia.length; intCampoAdicionalPosicion < intCampoAdicionalCantidad; intCampoAdicionalPosicion++) {
                        var objCampoAdicional = arrCamposAdicionalesCopia[intCampoAdicionalPosicion];

                        if (objTipoContactoPorCampo['TCCCN_CODIGO'] == objCampoAdicional['TCCCN_CODIGO']) {
                           
                            this._contacto['CamposAdicionales'].push({
                                'SPERN_CODIGO': objCampoAdicional['SPERN_CODIGO']
                                , 'TCCCN_CODIGO': objTipoContactoPorCampo['TCCCN_CODIGO']
                                , 'TCCVN_CODIGO': objCampoAdicional['TCCVN_CODIGO']
                                , 'TCXCN_CODIGO': objTipoContactoPorCampo['TCXCN_CODIGO']
                                , 'TCCVV_VALOR': objCampoAdicional['TCCVV_VALOR']
                            });

                            blnFounded = true;
                            break;
                        }
                    }

                    if (!blnFounded) {
                        this._contacto['CamposAdicionales'].push({
                            'SPERN_CODIGO': null
                            , 'TCCCN_CODIGO': objTipoContactoPorCampo['TCCCN_CODIGO']
                            , 'TCCVN_CODIGO': null
                            , 'TCXCN_CODIGO': objTipoContactoPorCampo['TCXCN_CODIGO']
                        });
                    }

                    break;
                }
            }

            celda.esObligatorio(esObligatorio);
            celda.enlazar(blnEnlazar);
            celda.enlaza();
        }

        var blnCopiable = false;
        for (var intTipoContactoPosicion = 0, intTipoContactoCantidad = this.grilla().tiposContacto().length; intTipoContactoPosicion < intTipoContactoCantidad; intTipoContactoPosicion++) {
            var objTipoContacto = this.grilla().tiposContacto()[intTipoContactoPosicion];

            if (this._contacto['TCCTN_CODIGO'] === objTipoContacto['TCCTN_CODIGO']) {
                blnCopiable = objTipoContacto['TCCTC_COPIABLE'];
                break;
            }
        }
      
        if (blnCopiable && (this.grilla().permiteMultipleEdicion() || this._enEdicion)) {
            $('#' + this.id() + ' > td > input#chkCopiar').show();
        } else {
            $('#' + this.id() + ' > td > input#chkCopiar').hide();
        }

        this._posicionUltimoControlEnlzado = intPosicionUltimoControlEnlazado;
    },
    contactoGrabar: function () {
        PageMethods.GrabarContacto(this._contacto, createDelegate(this, this.contactoGrabarCallback));
    },
    contactoGrabarCallback: function (sender) {
        this._contacto = sender.value;
        this._enEdicion = false;
        this.render();

        alert('El contacto se grab\xf3 satisfactoriamente.');
    },
    contactoEditar: function (intFila) {
		var arrAdicional = [];
		var strRegistro='';
		
			var objContacto = this._contacto;
			arrAdicional = objContacto['CamposAdicionales'];
			if((objContacto !=null) && (objContacto.CamposAdicionales!=null))
            {
				var strFila='';
				var strId='';
				if((objContacto['CSCTN_CODIGO']==undefined) || (objContacto['CSCTN_CODIGO']==""))
				{
					strFila='|';
				}else{
					strFila=objContacto['CSCTN_CODIGO'] + '|';
					strId=objContacto['CSCTN_CODIGO'];
				}
				strRegistro=strRegistro+strFila;
				for(var arrItem=0;arrItem<arrAdicional.length;arrItem++)
				{
					var objAdicional = arrAdicional[arrItem];
					if(objAdicional['TCCVV_VALOR'] != undefined)
					{ 
						strRegistro = strRegistro + objAdicional['TCXCN_CODIGO']+ ',' + objAdicional['TCCVV_VALOR'] + ' ;';
					}
				}
				strRegistro = strRegistro + '||';
            }
            if((document.getElementById('hidId').value == null) || (document.getElementById('hidId').value == ''))
            {
				document.getElementById('hidId').value=document.getElementById('hidId').value+strId;
			}else
			{
				document.getElementById('hidId').value=document.getElementById('hidId').value+','+strId;
			}
            document.getElementById('hidEditar').value=document.getElementById('hidEditar').value+strRegistro;
    },
    contactoEliminar: function () {
        var $this = this;
        var trTipoContacto = $('tr#' + this.id())
            , minimoRegistrosRequerido = this.grilla().f_validarMinimoRegistrosRequerido(trTipoContacto.index())
            , eliminar = false;

        if (minimoRegistrosRequerido.success) {
            confirm('El contacto ser\xe1 eliminado.\n -Desea Continuar..?', 'Confirmar', function (result) {
                if (result == true) {
                    $this.ContactDelete();
                } else {
                }
            });

        } else {
            confirm('Cantidad de registros m\u00EDnima para\rel tipo contacto: ' + minimoRegistrosRequerido.nombre + ' es: ' + minimoRegistrosRequerido.minimo + '\rDesea elmininar de todas maneras?', 'Confirmar', function (result) {
                if (result == true) {
                    $this.ContactDelete();
                } else { }
            });
        }

      
    },
    ContactDelete:function(){
            this._grilla._elevarComandoEjecutado('contacto_eliminando', this);

            this.eliminar();

            this.grilla()._elevarComandoEjecutado('contacto_eliminado', null);

        	var objIdContacto=this._contacto.CSCTN_CODIGO;
        	var strUrl = window.location.href;
        	if((objIdContacto=='undefined') || (objIdContacto==null))
        	{
        		document.getElementById('hidTipoAccion').value='T';
        		vintContTemp=vintAgregar-1;
        		alert('El contacto se elimin\xf3 satisfactoriamente.','Alerta');
        	}else
        	{
        		var vstrCuenta = $('#hidNroCuenta').val();
        		var vstrElimina='';
        		vstrElimina = objIdContacto + '|';
        		var urlAdd = strUrl + '/Dashboard/PostpaidDataCustomer/ContactSave';
        		$.app.ajax({
        		    type: 'POST',
        		    url: urlAdd,
        		    data: {
        		        strIdSession: Session.IDSESSION,
        		        strSave: '',
        		        strCuenta: vstrCuenta,
        		        strtelephone: $('#hidTelefono').val(),
        		        strDelete: vstrElimina
        		    },
        		    success: function (result) {
        		        vintContBD = vintContBD - 1;
        		        document.getElementById('hidTipoAccion').value='BD';
        		        alert('El contacto se elimin\xf3 satisfactoriamente.','Alerta');
        		    },
        		    error: function (ex) {
        		        error({
        		            id: 'PostpaidContact',
        		            message: ex
        		        });
        		    }
        		});



        	}
    
    },
    eliminar: function () {
		var trTipoContacto = $('tr#' + this.id());
        trTipoContacto.remove();
        if(trTipoContacto.index()!=-1){
			for (var i = 0, l = this.grilla().filas().conteo() ; i < l; i++) {
				var $this = $(this);
					if (this.grilla().filas().trae(i).id() === this.id()) {
						this.grilla().filas().remover(i);

						break;
					}
			}
		}
    }
}

/**********************************************************************************************************************************/
function ContactoGrillaCeldaColeccion(fila) {
    this._celdas = [];
    this._parent = fila;
}
ContactoGrillaCeldaColeccion.prototype = {
    agregar: function (celda) {
        celda._filaInterna(this._parent);

        this._celdas.push(celda);

        celda.render();
    },
    remover: function (indice) {
        this._celdas.splice(indice, 1);
    },
    conteo: function () {
        return this._celdas.length;
    },
    trae: function (posicion) {
        return this._celdas[posicion];
    },
    limpiar: function () {
        this._celdas = [];
    }
}

function ContactoGrillaCelda(columna) {
    this._element = null;
    this._value = null;
    this._fila = null;
    this._columna = columna;
    this._esObligatorio = false;
    this._enlazar = false;
}
ContactoGrillaCelda.prototype = {
    _filaInterna: function (valor) {
        if (arguments.length === 0) {
            return arguments[0];
        }

        this._fila = arguments[0];

        this._filaInterna = function () {
            return this._fila;
        }
    },
    _columnaInterna: function (valor) {
        if (arguments.length === 0) {
            return this._columna;
        }

        this._columna = arguments[0];

        this._columnaInterna = function () {
            return this._columna;
        }
    },

    codigo: function () {
        return this._columna.campo();
    },
    tipoDato: function () {
        return this._columna.tipoDato();
    },
    valor: function (value) {
        if (arguments.length === 0) {
            return this._valor;
        }

        this._valor = arguments[0];
    },

    esObligatorio: function (value) {
        if (arguments.length === 0) {
            return this._esObligatorio;
        }

        this._esObligatorio = arguments[0];
    },

    fila: function () {
        return this._filaInterna();
    },
    columna: function () {
        return this._columnaInterna();
    },
    render: function () {
        var celda = null;

        if (this._element === null) {
            $('#' + this.fila()._id).append('<td id="td_' + this.codigo() + '"></td>');

            this._element = $('#' + this.fila()._id).find('td#td_' + this.codigo())[0];
        }
    },
    enlazar: function (valor) {
        if (arguments.length === 0) {
            return this._enlazar;
        }

        this._enlazar = arguments[0];
    },
    enlaza: function () {
        var celda = $('#' + this.fila()._id).find('td#td_' + this.codigo());
        celda.children().remove();

        if (this.enlazar()) {
            var objContactoCampoEnlazado = null;

            if (this.fila().grilla().permiteMultipleEdicion() || this.fila().enEdicion()) {

                switch (this.tipoDato()) {
                    case 10006: //SELECT
                        objContactoCampoEnlazado = new ContactoCampoListaDesplegable(this);
                        break;
                    case 10007: //CADENA
                        objContactoCampoEnlazado = new ContactoCampoCadena(this);
                        break;
                    case 10008: //NUMÉRICO
                        objContactoCampoEnlazado = new ContactoCampoNumerico(this);
                        break;
                    case 10026: //FECHA
                        objContactoCampoEnlazado = new ContactoCampoFecha(this);
                        break;
                    case 10027: //CORREO
                        objContactoCampoEnlazado = new ContactoCampoCorreo(this);
                        break;
                    default:
                        objContactoCampoEnlazado = new ContactoCampoEnlazado(this);
                        break;
                }
            } else {
                console.error('objContactoCampoEnlazado');
               
                objContactoCampoEnlazado = new ContactoCampoEnlazado(this);
            }

            objContactoCampoEnlazado.render();
        }
    }
}

/**********************************************************************************************************************************/
function ContactoCampoEnlazado(celda) {
    this._tagName = 'LABEL';
    this._celda = celda;
}
ContactoCampoEnlazado.prototype = {
    _isCampoNumerico: false,
    campo: function (valor) {
        if (arguments.length === 0) {
            return this._campo;
        }

        this._campo = arguments[0];
    },
    tagName: function () {
        return this._tagName;
    },

    preRender: function (celda) {
        var tagName = this._tagName;

		if(tagName==='SELECT')
		{
			$(celda._element).append('<' + tagName + '></' + tagName + '>');
			$(celda._element).children().addClass('form-control-table');
			if(($('#hidAccion').val()=='N') || ($('#hidAccion').val()=='E'))
			{
			    $(celda._element).children().attr('disabled', false);
			}else{
			    $(celda._element).children().attr('disabled', 'disabled');
                //cm
			    $(celda._element).children().addClass('bg-success');
			}
		}else{
			if(($('#hidAccion').val()=='N') || ($('#hidAccion').val()=='E'))
			{
			    $(celda._element).append('<' + tagName + '></' + tagName + '>');
			    $(celda._element).children().addClass('form-control-table text-uppercase');
			}else if($('#hidAccion').val()==''){
			    $(celda._element).append('<' + tagName + ' readonly></' + tagName + '>');
			    $(celda._element).children().addClass('form-control-table bg-success');
			}
		}
    },
    render: function () {
        var celda = this._celda
            , valor = null;

        this.preRender(celda);

        if (celda.fila()._contacto.hasOwnProperty('CamposAdicionales') && celda.fila()._contacto['CamposAdicionales'] !== null) {
            for (var i = 0, l = celda.fila()._contacto.CamposAdicionales.length; i < l; i++) {
	           if (celda.codigo() == parseInt(celda.fila()._contacto.CamposAdicionales[i]['TCCCN_CODIGO'])) {
                    valor = celda.fila()._contacto.CamposAdicionales[i]['TCCVV_VALOR'];
                    break;
                }
            }
        }

        this._configurarValor(celda, valor);
    },
    _configurarValor: function (celda, valor) {
   

        var controlCampo = $(this._celda._element).children()
            , campoValor = 'Codigo'
            , campoNombre = 'Descripcion'
            , tiposDatoOpcional = null;

        switch (this._celda._columna._tipoContactoCampo['TCCCN_TIPODATOOPC']) {
            case 10028:
                campoValor = 'TCCTN_CODIGO';
                campoNombre = 'TCCTV_NOMBRE';

                tiposDatoOpcional = this._celda.fila().grilla().tiposContacto();
                break;
            case 10029:
                tiposDatoOpcional = this._celda.fila().grilla().tiposDocumento();
                break;
            default:
                break;
        }

        if (tiposDatoOpcional !== null && tiposDatoOpcional.length > 0) {
            $.each(tiposDatoOpcional, function () {
                if (valor === this[campoValor]) {
                    valor = '' + valor + ' &ndash; ' + this[campoNombre];
                    return;
                }
            });
        }

        controlCampo.html(valor);
    }
}

/**********************************************************************************************************************************/
function ContactoCampoEnlazadoEditable(celda) {
    this._tagName = 'INPUT';
    this._celda = celda;
}
ContactoCampoEnlazadoEditable.prototype = Object.create(ContactoCampoEnlazado.prototype);
ContactoCampoEnlazadoEditable.prototype.preRender = function (celda) {
    ContactoCampoEnlazado.prototype.preRender.call(this, celda);

    var controlCampo = $(celda._element).children();

    if (this._celda._columna._tipoContactoCampo['SPERV_NOMBRE'] === 'SPERV_TEL1') {
        controlCampo.numeric();
    }

    var intMaximaLongitud = this._celda._columna._tipoContactoCampo['TCCCN_LONGITUD'];

    if (this._celda._columna._tipoContactoCampo['SPERV_NOMBRE'] === 'SPERV_NUM_DOC') {
        intMaximaLongitud = definirLongitudNroDocumento($('#' + this._celda.fila()._id).find('td#td_4>select').val());
    }

    if (intMaximaLongitud !== null) {
        controlCampo.attr('maxlength', intMaximaLongitud);
    }

    controlCampo.bind('change', createDelegate(this, this.control_Cambiar));
    controlCampo.bind('keydown', createDelegate(this, this.control_TeclaAbajo));
},
ContactoCampoEnlazadoEditable.prototype._configurarValor = function (celda, valor) {
    $(celda._element).children().val(valor);
}
ContactoCampoEnlazadoEditable.prototype.control_Cambiar = function (sender, args) {
    var celda = this._celda
        , objContacto = celda.fila()._contacto
        , strNuevoValor = $(sender.target).val()
        , blnValorCambiado = false;

    for (var i = 0, l = objContacto.CamposAdicionales.length; i < l; i++) {
        if (celda.codigo() === parseInt(objContacto.CamposAdicionales[i]['TCCCN_CODIGO'])) {
            objContacto.CamposAdicionales[i]['TCCVV_VALOR'] = strNuevoValor;
            blnValorCambiado = true;
            break;
        }
    }

    if (!blnValorCambiado) {
        objContacto.CamposAdicionales.push({
            'TCCCN_CODIGO': this._celda._columna._tipoContactoCampo['TCCCN_CODIGO']
            , 'TCCVV_VALOR': strNuevoValor
        });
    }
}
ContactoCampoEnlazadoEditable.prototype.control_PerderFoco = function (sender, args) {
    var fila = $('#' + this._celda.fila()._id)
        , message = validarNroDocumento(fila.find('td#td_4 > select')[0], fila.find('td#td_5 > input')[0]);

    if (this._celda._columna._tipoContactoCampo['SPERV_NOMBRE'] !== 'TDOCC_CODIGO') {
        if (message !== null) {
            alert(message);
        }
    }

}
ContactoCampoEnlazadoEditable.prototype.control_TeclaAbajo = function (args) {
    var intRowIndex = $(args.target).parent().parent().index()
        , intRowOldIndex = intRowIndex
        , intColIndex = $(args.target).parent().index()
        , intColOldIndex = intColIndex
        , tr = $(args.target).parent().parent().parent().find('tr');

    switch (args.keyCode) {
        case 9:
            if (!args.shiftKey
                && intRowIndex === tr.length - 1
                && this._celda._fila._posicionUltimoControlEnlzado === intColIndex - 1) {

                this._celda._fila._grilla._elevarComandoEjecutado('ultimaFila_teclaAbajo_tab');
            }
            break
        case 37: //Left
            if ($(args.target).getCursorPosition() === 0) {
                intColIndex--;
            }
            break;
        case 38: //Up
            if ($(args.target).attr('tagName') !== 'SELECT' && !this._isCampoNumerico) {
                intRowIndex--;
            }
            break;
        case 39: //Right
            if ($(args.target).attr('tagName') === 'SELECT' || $(args.target).getCursorPosition() === $(args.target).val().length) {
                intColIndex++;
            }
            break;
        case 40: //Down

            if ($(args.target).attr('tagName') !== 'SELECT' && !this._isCampoNumerico) {
                intRowIndex++;
            }
            break;
    }

    if ((intRowOldIndex !== intRowIndex && (intRowIndex > -1 && intRowIndex < tr.length))
        || (intColOldIndex !== intColIndex && (intColIndex > -1 && intColIndex < tr.find('td').length))) {

        var focusControl = $(tr[intRowIndex].cells[intColIndex].childNodes[0]);

        focusControl.focus();
    }

    if (intColOldIndex !== intColIndex) {
        switch (args.keyCode) {
            case 37:
                var strValor = focusControl.val();

                if (strValor !== null) {
                    focusControl.selectRange(strValor.length + 1);
                }
                break;
            case 38:
                focusControl.selectRange(0);
                break;
        }
    }
}

/**********************************************************************************************************************************/
function ContactoCampoListaDesplegable(celda) {
    this._tagName = 'SELECT';
    this._celda = celda;
}
ContactoCampoListaDesplegable.prototype = Object.create(ContactoCampoEnlazadoEditable.prototype);
ContactoCampoListaDesplegable.prototype.preRender = function (celda) {
    ContactoCampoEnlazadoEditable.prototype.preRender.call(this, celda);

    var controlCampo = $(celda._element).children()
        , campoValor = 'Codigo'
        , campoNombre = 'Descripcion';

    if (celda._columna._tipoContactoCampo['TCCCN_TIPODATOOPC'] === 10028) {
        campoValor = 'TCCTN_CODIGO';
        campoNombre = 'TCCTV_NOMBRE';
    }

    $.each(celda.fila().grilla().tiposDocumento(), function () {
        controlCampo.append('<option value="' + this[campoValor] + '">' + this[campoNombre] + '</option>');
    });
    if (this._celda._columna._tipoContactoCampo['SPERV_NOMBRE'] === 'TDOCC_CODIGO') {
        controlCampo.bind('change', createDelegate(this, this.control_PosicionSeleccionada));
    }
}
ContactoCampoListaDesplegable.prototype.control_PosicionSeleccionada = function (sender, args) {
    var message = validarNroDocumento($(this._celda._element).find('select')[0], $('#' + this._celda.fila()._id).find('td#td_5>input')[0]);

    var intMaximaLongitud = definirLongitudNroDocumento($(this._celda._element).find('select').val());
    $('#' + this._celda.fila()._id).find('td#td_5>input').prop('maxlength', intMaximaLongitud);

    if (this._celda._columna._tipoContactoCampo['SPERV_NOMBRE'] !== 'TDOCC_CODIGO') {
        if (message !== null) {
            alert(message);
        }
    }
    else {
        $('#' + this._celda.fila()._id).find('td#td_5>input').selectRange(0, 100);
        $('#' + this._celda.fila()._id).find('td#td_5>input').attr("value", "");
    }

}

/**********************************************************************************************************************************/
function ContactoCampoCadena(celda) {
    this._tagName = 'INPUT';
    this._celda = celda;
}
ContactoCampoCadena.prototype = Object.create(ContactoCampoEnlazadoEditable.prototype);

/**********************************************************************************************************************************/
function ContactoCampoNumerico(celda) {
    this._tagName = 'INPUT';
    this._celda = celda;
}
ContactoCampoNumerico.prototype = Object.create(ContactoCampoEnlazadoEditable.prototype);
ContactoCampoNumerico.prototype.preRender = function (celda) {
    ContactoCampoEnlazadoEditable.prototype.preRender.call(this, celda);
    var controlCampo = $(celda._element).children();

    controlCampo.numeric();
}

/**********************************************************************************************************************************/
function ContactoCampoFecha(celda) {
    this._tagName = 'INPUT';
    this._celda = celda;
}
ContactoCampoFecha.prototype = Object.create(ContactoCampoEnlazadoEditable.prototype);
ContactoCampoFecha.prototype.preRender = function (celda) {
    ContactoCampoEnlazadoEditable.prototype.preRender.call(this, celda);
    var controlCampo = $(celda._element).children();

    controlCampo.datepicker();
}

/**********************************************************************************************************************************/
function ContactoCampoCorreo(celda) {
    this._tagName = 'INPUT';
    this._celda = celda;
}
ContactoCampoCorreo.prototype = Object.create(ContactoCampoEnlazadoEditable.prototype);
ContactoCampoCorreo.prototype._isCampoNumerico = true;
ContactoCampoCorreo.prototype.preRender = function (celda) {
    ContactoCampoEnlazadoEditable.prototype.preRender.call(this, celda);
    var controlCampo = $(celda._element).children();

    controlCampo.addClass('ui-autocomplete-loading');
    var VConstEmailsDisponibles = $('#hidAvailableEmails').val();
    var str_emails_disponibles = VConstEmailsDisponibles;
    var emails_disponibles = str_emails_disponibles.split(',');

    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }
    controlCampo.keydown = validarCaracteresEmail();
    controlCampo.autocomplete({
        minLength: 0,
        close: function () {
            $(this).trigger('change');
        },
        source: function (request, response) {
            // delegate back to autocomplete, but extract the last term
            var pos = request.term.indexOf('@');

            var emails_disponibles_temp = Array();

            if (pos > -1) //&& (request.term.length -1 ) == pos )
            {
                $.each(emails_disponibles, function (index, value) {
                    emails_disponibles_temp.push((request.term.substring(0, pos) + "@" + value).toUpperCase());
                });

                response($.ui.autocomplete.filter(
                    emails_disponibles_temp, extractLast(request.term)));
                $('#ui-id-1').css('width', $("#txtCorreo").css('width'));
            } else {
                $('#ui-id-1').css('display', 'none');
                $('#ui-id-1').css('width', $("#txtCorreo").css('width'));
            }
        }
    });
    controlCampo.bind('keypress', validarCaracteresEmail);
}

/**********************************************************************************************************************************/
function generateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
}
function validarNroDocumento(eleTipoDocumento, eleNroDocumento) {
    var strTipo = $(eleTipoDocumento).val()
        , strNumero = $.trim($(eleNroDocumento).val())
        , intLongitud = strNumero.length
        , blnCorrecto = false;

    if (intLongitud > 0) {
        blnCorrecto = validarNroDocumentoValor(strTipo, strNumero)
    } else {
        blnCorrecto = true;
    }

    return (blnCorrecto ? null : 'El nro. de documento de documento de ' + $(eleTipoDocumento).find('option:selected').text() + ' no es v\xe1lido.');
}
function validarNroDocumentoValor(tipoDocumento, nroDocumento) {
    var blnCorrecto = false
        , intLongitud = $.trim(nroDocumento).length;
    switch (tipoDocumento) {
        case '01': //DNI
            blnCorrecto = (intLongitud === 8);
            break;
        case '02': //CIP
            blnCorrecto = (intLongitud === 6);
            break;
        case '04': //CE
            blnCorrecto = (intLongitud >= 4 && intLongitud <= 9);
            break;
        case '07': //Pasaporte
            blnCorrecto = (intLongitud <= 10);
            break;
    }

    return blnCorrecto;
}
function definirLongitudNroDocumento(tipoDocumento) {
    var intLongitud = null;

    switch (tipoDocumento) {
        case '01': //DNI
            intLongitud = 8;
            break;
        case '02': //CIP
            intLongitud = 6;
            break;
        case '04': //CE
            intLongitud = 9;
            break;
        case '07': //Pasaporte
            intLongitud = 10;
            break;
    }

    return intLongitud;
}


function validarCaracteresEmail() {
    var CaracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_.@";
    var key = String.fromCharCode(window.event.keyCode)
    var valid = new String(CaracteresPermitidos)
    var ok = "no";

    for (var i = 0; i < valid.length; i++) {
        if (key == valid.substring(i, i + 1))
            ok = "yes"
    }
    if (ok == "no")
        window.event.keyCode = 0

    if ((key > 0x60) && (key < 0x7B))
        window.event.keyCode = key - 0x20;
}

(function($, undefined) {  
	$.fn.getCursorPosition = function() {  
		var el = $(this).get(0);  
		var pos = 0;  
		if ('selectionStart' in el) {  
			pos = el.selectionStart;  
		} else if ('selection' in document) {  
			el.focus();  
			var Sel = document.selection.createRange();  
			var SelLength = document.selection.createRange().text.length;  
			Sel.moveStart('character', -el.value.length);  
			pos = Sel.text.length - SelLength;  
		}  
   		return pos;  
	}  
})(jQuery);

$.fn.selectRange = function(start, end) {
    if(!end) end = start; 
    return this.each(function() {
        if (this.setSelectionRange) {
            this.focus();
            this.setSelectionRange(start, end);
        } else if (this.createTextRange) {
            var range = this.createTextRange();
            range.collapse(true);
            range.moveEnd('character', end);
            range.moveStart('character', start);
            range.select();
        }
    });
};

